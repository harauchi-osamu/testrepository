using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using HulftIO;

namespace MainMenu
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        public int BankCd { get; private set; } = -1;

        public AppMain.Type IsSuccess { get; private set; }

        /// <summary>
        /// 設定ファイルチェックの結果メッセージ
        /// 空の場合チェックOK
        /// </summary>
        public string CheckParamMsg { get; private set; } = "";

        /// <summary>
        /// ServerIniファイル存在チェック結果
        /// True：OK/ False：NG
        /// </summary>
        public bool ChkServerIni { get; private set; } = true;


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
		/// 引数を設定する
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public override bool SetArgs(string[] args)
        {
            string IniPath = args[0];
            NCR.Server.IniPath = IniPath;

            string BankCD = args[1];
            if (!int.TryParse(BankCD, out int intBank))
            {
                throw new Exception("銀行コードが不正です");
            }

            this.BankCd = intBank;
            this.IsSuccess = AppMain.Type.Success;
            return true;
        }

        #region 設定チェック

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckServerIni()
        {
            // ServerIniファイル存在チェック
            if (!ServerIniExists())
            {
                return false;
            }

            ChkParam(NCR.Server.BankNM, "銀行名");
            ChkParam(NCR.Server.Environment, "環境");
            ChkParam(NCR.Server.HulftExePath, "HALFTプログラム");
            ChkParam(NCR.Server.SendRoot, "HALFT配信フォルダ");
            ChkParam(NCR.Server.IOSendRoot, "IO配信フォルダ(銀行別)");
            ChkParam(NCR.Server.IOSendRootBk, "IO配信フォルダ(銀行別)BK");
            ChkParam(NCR.Server.SHulftID, "配信HULFTID");

            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            return true;
        }

        #endregion

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// Server.ini 存在チェック
        /// </summary>
        /// <returns></returns>
        private bool ServerIniExists()
        {
            // ServerIniファイル存在チェック
            if (!System.IO.File.Exists(NCR.Server.IniPath))
            {
                ChkServerIni = false;
            }

            return true;
        }

        /// <summary>
        /// 設定内容チェック
        /// </summary>
        private bool ChkParam(string Item, string ItemName)
        {
            if (string.IsNullOrEmpty(Item))
            {
                SetCheckParamMsg(ItemName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 設定内容チェック
        /// </summary>
        private bool ChkParam(int Item, string ItemName)
        {
            if (Item == 0)
            {
                SetCheckParamMsg(ItemName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// configの取得
        /// </summary>
        private string GetAppSettingsStr(string Key, string ItemName, bool EmptyChk = true)
        {
            string strWork = "";

            try
            {
                string sKeyData = ConfigurationManager.AppSettings[Key];
                if (string.IsNullOrWhiteSpace(sKeyData) && EmptyChk)
                {
                    throw new Exception("Error");
                }
                strWork = sKeyData;
            }
            catch
            {
                SetCheckParamMsg(ItemName);
            }

            return strWork;
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
            if (string.IsNullOrEmpty(CheckParamMsg))
            {
                CheckParamMsg = Msg;
            }
            else
            {
                CheckParamMsg += "," + Msg;
            }
        }

        /// <summary>
        /// ファイル配信処理を行う
        /// </summary>
        /// <returns></returns>
        public bool ExecHulftSend()
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ファイル配信処理 開始", 1);
            _itemMgr.DispParams.Clear();

            //// ユーザー情報を取得
            //if (!GetUserInfo(out TBL_CTRUSERINFO usrinfo))
            //{
            //    // 取得ユーザーなし
            //    this.IsSuccess = AppMain.Type.Error;
            //    return false;
            //}
            // 配信HULFTIDチェック
            if (string.IsNullOrEmpty(NCR.Server.SHulftID))
            {
                // 配信HULFTIDなし
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "配信HULFTIDが未定義です", 1);
                this.IsSuccess = AppMain.Type.Error;
                return false;
            }

            // ファイル集配管理取得
            if (!_itemMgr.Fetch_file_ctls())
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("配信対象ファイル {0}件", 0), 1);
                return true;
            }
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("配信対象ファイル {0}件", _itemMgr.file_ctls.Count), 1);

            try
            {
                // ファイル集配管理件数
                foreach (TBL_FILE_CTL fctl in _itemMgr.file_ctls.Values)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル配信処理 開始（配信中）：FileId=[{0}], FileName=[{1}]", fctl._FILE_ID, fctl._SEND_FILE_NAME), 1);

                    // ステータス更新＆コミット（配信中）
                    if (!UpdateFileCtlStatusSending(fctl))
                    {
                        // ステータス更新できない場合は次のファイルを配信する
                        continue;
                    }

                    // ファイル存在チェック
                    string srcFilePath = Path.Combine(ServerIni.Setting.IOSendRoot, fctl._SEND_FILE_NAME);
                    if (!File.Exists(srcFilePath))
                    {
                        // ステータス更新＆コミット（配信エラー）
                        UpdateFileCtlStatus(fctl, FileCtl.SendSts.配信エラー);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル配信処理 失敗（送信ファイルなし）"), 1);
                        this.IsSuccess = AppMain.Type.Error;
                        continue;
                    }

                    // HULFT配信
                    if (!HulftSend(fctl, srcFilePath))
                    {
                        // ステータス更新＆コミット（配信エラー）
                        UpdateFileCtlStatus(fctl, FileCtl.SendSts.配信エラー);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル配信処理 失敗（HULFTエラー）"), 1);
                        this.IsSuccess = AppMain.Type.Error;
                        continue;
                    }

                    // ステータス更新＆コミット（配信済）
                    UpdateFileCtlStatus(fctl, FileCtl.SendSts.配信済);

                    // 処理が速すぎるとHULFTログがつかみっぱなしになる(？)ので少し待つ
                    System.Threading.Thread.Sleep(100);

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル配信処理 正常終了（配信済）"), 1);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ファイル配信処理エラー：" + ex.ToString(), 3);
                this.IsSuccess = AppMain.Type.Error;
                return false;
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ファイル配信処理 終了", 1);
            return true;
        }

        /// <summary>
        /// HULFT配信を行う
        /// </summary>
        /// <param name="fctl"></param>
        /// <returns></returns>
        private bool HulftSend(TBL_FILE_CTL fctl, string srcFilePath)
        {
            // コピー先パス
            string dstFilePath = Path.Combine(ServerIni.Setting.SendRoot, fctl._SEND_FILE_NAME);
            string dstFilePathBk = Path.Combine(ServerIni.Setting.IOSendRootBk, fctl._SEND_FILE_NAME);

            try
            {
                // ファイルコピー
                File.Copy(srcFilePath, dstFilePath, true);
                //File.Copy(srcFilePath, dstFilePathBk, true);

                // ファイル配信
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("HULFT配信 開始：File=[{0}]", dstFilePath), 1);

                    // HULFT パラメーター
                    HulftIO.ItemManager hulftItem = new HulftIO.ItemManager(_masterMgr);
                    hulftItem.DispParams.IsAutoExec = true;
                    hulftItem.DispParams.TransType = HulftIO.ItemManager.TransParams.配信;
                    hulftItem.DispParams.FileId = fctl._FILE_ID;
                    hulftItem.DispParams.FileName = fctl._SEND_FILE_NAME;
                    hulftItem.DispParams.SendDirPath = ServerIni.Setting.SendRoot;
                    hulftItem.DispParams.SHulftID = NCR.Server.SHulftID;
                    hulftItem.DispParams.RHulftID = NCR.Server.RHulftID;

                    // HULFT コントローラー
                    HulftIO.Controller hulftCtl = new HulftIO.Controller();
                    hulftCtl.SetManager(_masterMgr, hulftItem);

                    // HULFT 伝送フォーム
                    HulftIOForm form = new HulftIOForm();
                    form.InitializeForm(hulftCtl);
                    form.RunHulftProcess();

                    // HULFT 実行結果
                    _itemMgr.DispParams.Result = form.Result;
                    _itemMgr.DispParams.ErrMsg = form.ErrMsg;
                    if (!string.IsNullOrEmpty(form.ErrMsg))
                    {
                        // 配信エラー
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("HULFT配信 異常終了：Result=[{0}], ErrMsg=[{1}]", form.Result, form.ErrMsg), 3);
                        return false;
                    }

                    // BKフォルダにコピー
                    File.Copy(srcFilePath, dstFilePathBk, true);
                    // ファイル削除（コピー元）
                    CommonUtil.DeleteFile(srcFilePath);

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("HULFT配信 正常終了：Result=[{0}]", form.Result), 1);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            finally
            {
                // ファイル削除（コピー先）
                CommonUtil.DeleteFile(dstFilePath);
            }
            return true;
        }

        /// <summary>
        /// ファイル集配信管理のステータスを更新する（配信中）
        /// </summary>
        /// <param name="fctl"></param>
        /// <param name="sendsts"></param>
        private bool UpdateFileCtlStatusSending(TBL_FILE_CTL fctl)
        {
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // レコードロック
                    string strSQL = TBL_FILE_CTL.GetSelectQuery(fctl._FILE_ID, fctl._FILE_DIVID, fctl._SEND_FILE_NAME, fctl._CAP_FILE_NAME, AppInfo.Setting.SchemaBankCD);
                    strSQL += DBConvert.QUERY_LOCK;
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), non.Trans);
                    if (tbl.Rows.Count > 0)
                    {
                        fctl = new TBL_FILE_CTL(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }
                    // ステータス確認
                    if ((fctl.m_SEND_STS != FileCtl.SendSts.ファイル作成) &&
                        (fctl.m_SEND_STS != FileCtl.SendSts.配信エラー))
                    {
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("配信対象外 SEND_STS=[{0}]", fctl.m_SEND_STS), 2);
                        return false;
                    }

                    // ステータス更新
                    SetFileCtlStatus(fctl, FileCtl.SendSts.配信中);

                    // ファイル集配管理を更新する
                    _itemMgr.UpdateFileCtlStatus(fctl, dbp, non);

                    // コミット
                    CommitTerminate(dbp, non);
                }
                catch (Exception ex)
                {
                    // ロールバック
                    RollbackTerminate(dbp, non);

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ファイル集配信管理のステータスを更新する
        /// </summary>
        /// <param name="fctl"></param>
        /// <param name="sendsts"></param>
        private bool UpdateFileCtlStatus(TBL_FILE_CTL fctl, int sendsts)
        {
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // ステータス更新
                    SetFileCtlStatus(fctl, sendsts);

                    // トランザクションのステータス更新処理実施
                    UpdateSendResultStatus(fctl, dbp, non);

                    // ファイル集配管理を更新する
                    _itemMgr.UpdateFileCtlStatus(fctl, dbp, non);

                    // コミット
                    CommitTerminate(dbp, non);
                }
                catch (Exception ex)
                {
                    // ロールバック
                    RollbackTerminate(dbp, non);

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ファイル集配信管理のステータスを設定する
        /// </summary>
        /// <param name="fctl"></param>
        /// <param name="sts"></param>
        private void SetFileCtlStatus(TBL_FILE_CTL fctl, int sts)
        {
            fctl.m_SEND_STS = sts;
            if (sts == FileCtl.SendSts.配信済)
            {
                fctl.m_SEND_DATE = AplInfo.OpDate();
                fctl.m_SEND_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
            }
        }

        /// <summary>
        /// ファイル配信時のステータス更新処理
        /// </summary>
        /// <param name="fctl"></param>
        /// <param name="sendsts"></param>
        private void UpdateSendResultStatus(TBL_FILE_CTL fctl, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            switch (fctl.m_SEND_STS)
            {
                case FileCtl.SendSts.配信済:
                    // 配信成功時

                    if (fctl._FILE_ID == FileParam.FileId.IF101 && fctl._FILE_DIVID == FileParam.FileKbn.BUB)
                    {
                        // 持出証券イメージアーカイブの場合
                        // 持出アップロード状態を10(アップロード)に更新
                        Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                        UpdateData.Add(TBL_TRMEIIMG.BUA_STS, TrMei.Sts.アップロード);
                        int UpdCnt = _itemMgr.UpdateTRMeiImgOCStatus(fctl, UpdateData, dbp, non);

                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル配信 成功時トランザクション更新 FileId=[{0}], FileDivid=[{1}] 更新件数=[{2}]", fctl._FILE_ID, fctl._FILE_DIVID, UpdCnt), 1);
                    }
                    // 2022/03/28 銀行導入工程_不具合管理表No97 対応
                    else if (fctl._FILE_ID == FileParam.FileId.IF202 && fctl._FILE_DIVID == FileParam.FileKbn.BCA)
                    {
                        // 持出取消テキストの場合
                        // 持出取消アップロード状態を10(アップロード)に更新
                        Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                        UpdateData.Add(TBL_TRMEI.BCA_STS, TrMei.Sts.アップロード);
                        int UpdCnt = _itemMgr.UpdateTRMeiSendStatus(fctl, UpdateData, dbp, non);

                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル配信 成功時トランザクション更新 FileId=[{0}], FileDivid=[{1}] 更新件数=[{2}]", fctl._FILE_ID, fctl._FILE_DIVID, UpdCnt), 1);
                    }
                    // 2022/03/28 銀行導入工程_不具合管理表No97 対応
                    else if (fctl._FILE_ID == FileParam.FileId.IF204 && fctl._FILE_DIVID == FileParam.FileKbn.GMA)
                    {
                        // 証券データ訂正テキストの場合
                        // 持帰訂正データアップロード状態を10(アップロード)に更新
                        Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                        UpdateData.Add(TBL_TRMEI.GMA_STS, TrMei.Sts.アップロード);
                        int UpdCnt = _itemMgr.UpdateTRMeiSendStatus(fctl, UpdateData, dbp, non);

                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル配信 成功時トランザクション更新 FileId=[{0}], FileDivid=[{1}] 更新件数=[{2}]", fctl._FILE_ID, fctl._FILE_DIVID, UpdCnt), 1);
                    }
                    // 2022/03/28 銀行導入工程_不具合管理表No97 対応
                    else if (fctl._FILE_ID == FileParam.FileId.IF205 && fctl._FILE_DIVID == FileParam.FileKbn.GRA)
                    {
                        // 不渡返還テキストの場合
                        // 不渡返還登録アップロード状態を10(アップロード)に更新
                        Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                        UpdateData.Add(TBL_TRMEI.GRA_STS, TrMei.Sts.アップロード);
                        int UpdCnt = _itemMgr.UpdateTRMeiSendStatus(fctl, UpdateData, dbp, non);

                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル配信 成功時トランザクション更新 FileId=[{0}], FileDivid=[{1}] 更新件数=[{2}]", fctl._FILE_ID, fctl._FILE_DIVID, UpdCnt), 1);
                    }

                    break;
            }
        }

        /// <summary>
        /// ユーザー情報を取得
        /// </summary>
        /// <remarks>未使用</remarks>
        private bool GetUserInfo(out TBL_CTRUSERINFO usrinfo)
        {
            //初期化
            usrinfo = new TBL_CTRUSERINFO(AppInfo.Setting.SchemaBankCD);

            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // ユーザー情報取得
                    usrinfo = _masterMgr.GetCtrUserInfo(dbp, non);

                    if (string.IsNullOrEmpty(usrinfo._USERID))
                    {
                        // 取得データなし
                        throw new Exception("ユーザー情報が取得できませんでした");
                    }

                    // コミット
                    CommitTerminate(dbp, non);
                }
                catch (Exception ex)
                {
                    // ロールバック
                    RollbackTerminate(dbp, non);

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ロールバックと後処理を行う（異常終了時）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        private void RollbackTerminate(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // ロールバック
            non.Trans.Rollback();
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
