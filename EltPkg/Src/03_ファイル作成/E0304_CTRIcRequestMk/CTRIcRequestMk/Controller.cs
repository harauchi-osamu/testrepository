using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using NCR;

namespace CTRIcRequestMk
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private EntryCommonFormBase _form = null;

        /// <summary>設定ファイル情報</summary>
        public SettingData SettingData { get; private set; } = new SettingData();
        public bool IsIniErr { get { return (!string.IsNullOrEmpty(this.SettingData.CheckParamMsg) || !this.SettingData.ChkServerIni); } }

        public ClearingType ProcType { get; set; }
        public bool IsSilent { get; set; } = true;

        public enum ClearingType
        {
            翌日交換,
            当日交換,
            指定交換
        }


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 管理クラスを設定する
        /// </summary>
        /// <param name="mst"></param>
        /// <param name="item"></param>
        public override void SetManager(MasterManager mst, ManagerBase item)
		{
			base.SetManager(mst, item);
			_masterMgr = MasterMgr;
			_itemMgr = (ItemManager)ItemMgr;
		}

        /// <summary>
        /// フォームを設定する
        /// </summary>
        /// <param name="form"></param>
        public void SetForm(EntryCommonFormBase form)
        {
            _form = form;
        }

		/// <summary>
		/// 引数を設定する
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public override bool SetArgs(string[] args)
        {
			string MenuNumber = args[0];
			base.MenuNumber = MenuNumber;

            int clearingType = 0;
            if (!Int32.TryParse(args[1], out clearingType))
            {
                return false;
            }
            int silentMode = 0;
            if (!Int32.TryParse(args[2], out silentMode))
            {
                return false;
            }
            if (!(silentMode == 0 || silentMode == 1))
            {
                return false;
            }

            // サイレントモード（当日交換のみ）
            this.IsSilent = (silentMode != 0);

            // 交換モード
            switch (clearingType)
            {
                case 0:
                    this.ProcType = ClearingType.翌日交換;
                    if (this.IsSilent)
                    {
                        return false;
                    }
                    break;
                case 1:
                    this.ProcType = ClearingType.当日交換;
                    break;
                case 2:
                    this.ProcType = ClearingType.指定交換;
                    if (this.IsSilent)
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Operator.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckOperatorIni()
        {
            SettingData.ChkParam(NCR.Operator.UserID, "ユーザーID");
            SettingData.ChkParam(NCR.Operator.UserName, "ユーザー名");
            SettingData.ChkParam(NCR.Operator.BankCD, "銀行コード");
            return true;
        }

        /// <summary>
        /// Term.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckTermIni()
        {
            SettingData.ChkParam(NCR.Terminal.Number, "端末番号");
            SettingData.ChkParam(NCR.Terminal.ServeriniPath, "CtrServer.iniパス");
            return true;
        }

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckServerIni()
        {
            // ServerIniファイル存在チェック
            if (!SettingData.ServerIniExists())
            {
                return false;
            }

            SettingData.ChkParam(NCR.Server.IOSendRoot, "IO配信フォルダ(銀行別)");
            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            GetAppSettingsInt("ClearingMonthMax", "処理可能交換希望月数");
            GetAppSettingsInt("ClearingDefDateDiff", "指定交換での交換日To初期値営業日差日数");
            return true;
        }

        /// <summary>
        /// configの取得
        /// </summary>
        private int GetAppSettingsInt(string Key, string ItemName, bool EmptyChk = true)
        {
            int iWork = -1;

            try
            {
                string sKeyData = ConfigurationManager.AppSettings[Key];
                if (string.IsNullOrWhiteSpace(sKeyData) && !EmptyChk)
                {
                    sKeyData = "0";
                }
                if (!int.TryParse(sKeyData, out iWork))
                {
                    throw new Exception("Error");
                }
            }
            catch
            {
                SetCheckParamMsg(ItemName);
            }

            return iWork;
        }

        /// <summary>
        /// 結果メッセージへの設定
        /// </summary>
        private void SetCheckParamMsg(string Msg)
        {
            if (string.IsNullOrEmpty(this.SettingData.CheckParamMsg))
            {
                this.SettingData.CheckParamMsg = Msg;
            }
            else
            {
                this.SettingData.CheckParamMsg += "," + Msg;
            }
        }

        /// <summary>
        /// 証券イメージアーカイブ作成処理
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool MakeTextFile(EntryCommonFormBase form)
        {
            _form = form;
            _itemMgr.DispParams.CreateFileName = "";
            _itemMgr.DispParams.if203 = null;

            // UPDATE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    //システム日付≠営業日＆サイレントモードの場合は処理中断
                    if (IsSilent)
                    {
                        int SystemDate = int.Parse(System.DateTime.Now.ToString("yyyyMMdd"));
                        if (!Calendar.IsBusinessDate(SystemDate))
                        {
                            // ロールバック
                            RollbackTerminate(dbp, non);
                            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("非営業日のためサイレントモード起動中断　システム日付[{0}]", SystemDate), 1);
                            return true;
                        }
                    }

                    // テキストファイル作成
                    if (!DoCreate(dbp, non))
                    {
                        // ロールバック
                        RollbackTerminate(dbp, non);
                        return false;
                    }

                    // コミット
                    CommitTerminate(dbp, non);
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf(Const.ORACLE_ERR_LOCK) != -1)
                    {
                        // ロック中
                        if (!IsSilent) ComMessageMgr.MessageWarning(ComMessageMgr.E01003);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        _form.SetStatusMessage(ComMessageMgr.E01003);
                    }
                    else
                    {
                        if (!IsSilent) ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        _form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    }
                    // ロールバック
                    RollbackTerminate(dbp, non);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ファイルを作成する
        /// </summary>
        /// <returns></returns>
        public bool DoCreate(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // ファイル一連番号採番
            int fileSeq = ImportFileAccess.GetFileSeq(FileParam.FileId.IF203, FileParam.FileKbn.GDA, 1, dbp, non);
            // 持帰要求テキスト
            FileGenerator if203 = new FileGenerator(fileSeq, FileParam.FileId.IF203, FileParam.FileKbn.GDA, Operator.BankCD, ".txt");
            _itemMgr.DispParams.if203 = if203;
            // 結果テキスト
            FileGenerator if207 = new FileGenerator(fileSeq, FileParam.FileId.IF207, FileParam.FileKbn.GDA, Operator.BankCD, ".txt");

            // テキストファイル作成
            CreateTextFiles(if203, dbp, non);

            // ＤＢ更新
            _itemMgr.RegistMeisaiItems(if203, if207, dbp, non);

            return true;
        }

        /// <summary>
        /// テキストファイルを生成する
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateTextFiles(FileGenerator if203, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string data = "";

            // ファイル名
            string filePath = Path.Combine(ServerIni.Setting.IOSendRoot, if203.FileName);

            // データ作成
            data += CreateHeaderRecord(if203, dbp, non);
            data += CreateDataRecord();
            data += CreateTrailerRecord();
            data += CreateEndRecord();

            // ファイル保存
            FileGenerator.WriteAllTextStream(filePath, data);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル保存=[{0}]", filePath), 1);

            // ファイルサイズ算出
            if203.SetFileSize(filePath);

            // 結果ファイル名
            _itemMgr.DispParams.CreateFileName = if203.FileName;
            return true;
        }

        /// <summary>
        /// ヘッダーレコード作成
        /// </summary>
        /// <returns></returns>
        private string CreateHeaderRecord(FileGenerator if203, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            TBL_CTRUSERINFO userinfo = _masterMgr.GetCtrUserInfo(dbp, non);
            string userId = CommonUtil.BPadRight(userinfo._USERID, Const.CTRUSER_USERID_LEN, " ");
            string password = CommonUtil.BPadRight(userinfo.m_PASSWORD, Const.CTRUSER_PASSWORD_LEN, " ");
            string dummey = CommonUtil.BPadRight(string.Empty, 25, " ");

            StringBuilder data = new StringBuilder();
            data.Append("1");
            data.Append(FileParam.FileId.IF203);
            data.Append(FileParam.FileKbn.GDA);
            data.Append(ServerIni.Setting.BankCd.ToString(Const.BANK_NO_LEN_STR));
            data.Append(AplInfo.OpDate().ToString("D8"));
            data.Append(if203.Seq);
            data.Append(userId);
            data.Append(password);
            data.Append(dummey);
            data.Append(FileGenerator.CRLF);

            return data.ToString();
        }

        /// <summary>
        /// データレコード作成
        /// </summary>
        /// <returns></returns>
        private string CreateDataRecord()
        {
            string clearingDate1 = _itemMgr.DispParams.ClearingDateFrom.ToString("D8");
            string clearingDate2 = _itemMgr.DispParams.ClearingDateTo.ToString("D8");
            if (_itemMgr.DispParams.ClearingDateFrom == _itemMgr.DispParams.ClearingDateTo)
            {
                clearingDate2 = "ZZZZZZZZ";
            }
            string clearingType = CommonUtil.BPadLeft(_itemMgr.DispParams.BillCode, 3, "0");
            if (string.IsNullOrEmpty(_itemMgr.DispParams.BillCode))
            {
                clearingType = "ZZZ";
            }
            string icKbn = CommonUtil.BPadLeft(_itemMgr.DispParams.IcType, 1, "0");
            string imageYouhi = CommonUtil.BPadLeft(_itemMgr.DispParams.ImageNeed, 1, "0");
            string dummey = CommonUtil.BPadRight(string.Empty, 78, " ");

            StringBuilder data = new StringBuilder();
            data.Append("2");
            data.Append(clearingDate1);
            data.Append(clearingDate2);
            data.Append(clearingType);
            data.Append(icKbn);
            data.Append(imageYouhi);
            data.Append(dummey);
            data.Append(FileGenerator.CRLF);

            return data.ToString();
        }

        /// <summary>
        /// トレーラーレコード作成
        /// </summary>
        /// <returns></returns>
        private string CreateTrailerRecord()
        {
            string recCnt = 1.ToString("D8");
            string dummey = CommonUtil.BPadRight(string.Empty, 91, " ");

            StringBuilder data = new StringBuilder();
            data.Append("8");
            data.Append(recCnt);
            data.Append(dummey);
            data.Append(FileGenerator.CRLF);
            return data.ToString();
        }

        /// <summary>
        /// エンドレコード作成
        /// </summary>
        /// <returns></returns>
        private string CreateEndRecord()
        {
            string dummey = CommonUtil.BPadRight(string.Empty, 99, " ");

            StringBuilder data = new StringBuilder();
            data.Append("9");
            data.Append(dummey);
            data.Append(FileGenerator.CRLF);
            return data.ToString();
        }


        /// <summary>
        /// ロールバックと後処理を行う（異常終了時）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        private void RollbackTerminate(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // ロールバック前にテーブル削除すると、先にコミットされてしまうので注意

            // ロールバック
            non.Trans.Rollback();

            // ファイル削除
            if (_itemMgr.DispParams.if203 != null)
            {
                string filePath = Path.Combine(ServerIni.Setting.IOSendRoot, _itemMgr.DispParams.if203.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        /// <summary>
        /// コミットと後処理を行う（正常終了時）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        private void CommitTerminate(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // コミット
            non.Trans.Commit();
        }
    }
}
