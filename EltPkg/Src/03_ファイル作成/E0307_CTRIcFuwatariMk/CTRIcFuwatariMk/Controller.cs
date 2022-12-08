using System;
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

namespace CTRIcFuwatariMk
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
            return true;
        }

        /// <summary>
        /// 証券イメージアーカイブ作成処理
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool MakeTextFile(EntryCommonFormBase form)
        {
            _form = form;
            _itemMgr.ICMeisaiList1 = new SortedDictionary<string, ItemManager.MeisaiInfos>();
            _itemMgr.ICMeisaiList2 = new SortedDictionary<string, ItemManager.MeisaiInfos>();
            _itemMgr.ICMeisaiOwnList1 = new SortedDictionary<string, ItemManager.MeisaiInfos>();
            _itemMgr.ICMeisaiOwnList2 = new SortedDictionary<string, ItemManager.MeisaiInfos>();
            _itemMgr.OCMeisaiOwnList1 = new SortedDictionary<string, ItemManager.MeisaiInfos>();
            _itemMgr.OCMeisaiOwnList2 = new SortedDictionary<string, ItemManager.MeisaiInfos>();
            _itemMgr.DispParams.CreateFileName = "";
            _itemMgr.DispParams.if205 = null;

            // UPDATE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // 一時テーブル削除＆作成
                    _itemMgr.DropTmpTable(dbp, non);
                    _itemMgr.CreateTmpTable(dbp, non);

                    // テキストファイル作成
                    if (!DoCreate(dbp, non))
                    {
                        // ロールバック
                        RollbackTerminate(dbp, non);
                        return false;
                    }

                    // 行内交換連携
                    if (!DoExchange(dbp, non))
                    {
                        // ロールバック
                        RollbackTerminate(dbp, non);
                        return false;
                    }

                    // 一時テーブル削除
                    _itemMgr.DropTmpTable(dbp, non);
                    
                    // コミット
                    CommitTerminate(dbp, non);
                }
                catch (Exception ex)
                {
                    // 取得した行ロックを解除するためロールバック
                    // メッセージボックス表示前に実施
                    RollbackTerminate(dbp, non);
                    // メッセージ表示
                    if (ex.Message.IndexOf(Const.ORACLE_ERR_LOCK) != -1)
                    {
                        // ロック中
                        ComMessageMgr.MessageWarning(ComMessageMgr.E01003);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        _form.SetStatusMessage(ComMessageMgr.E01003);
                    }
                    else
                    {
                        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        _form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    }
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
            // 行内交換連携実施判定
            ItemManager.CreateType ctype;
            if (ServerIni.Setting.InternalExchange)
            {
                // 行内交換する場合は他行データのみアーカイブ作成する
                ctype = ItemManager.CreateType.他行データ;
            }
            else
            {
                // 行内交換しない場合は全データをアーカイブ作成する
                ctype = ItemManager.CreateType.全データ;
            }

            // 作成対象データ取得（他行）
            if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.登録データ, ctype, _form))
            {
                // 異常終了
                return false;
            }
            if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.取消データ, ctype, _form))
            {
                // 異常終了
                return false;
            }

            // 作成対象データ取得（自行）
            if (ServerIni.Setting.InternalExchange)
            {
                if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.登録データ, ItemManager.CreateType.自行データ, _form))
                {
                    // 異常終了
                    return false;
                }
                if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.取消データ, ItemManager.CreateType.自行データ, _form))
                {
                    // 異常終了
                    return false;
                }
            }

            // 件数チェック
            int totalCnt = 0;
            totalCnt += _itemMgr.ICMeisaiList1.Count;
            totalCnt += _itemMgr.ICMeisaiList2.Count;
            totalCnt += _itemMgr.ICMeisaiOwnList1.Count;
            totalCnt += _itemMgr.ICMeisaiOwnList2.Count;
            if (totalCnt != _itemMgr.DispParams.YoteiTotalCnt)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数相違 電子交換 訂正={0}件、取消={1}件、合計={2}件",
                    _itemMgr.ICMeisaiList1.Count, _itemMgr.ICMeisaiList2.Count, (_itemMgr.ICMeisaiList1.Count + _itemMgr.ICMeisaiList2.Count)), 1);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数相違 行内交換 訂正={0}件、取消={1}件、合計={2}件",
                    _itemMgr.ICMeisaiOwnList1.Count, _itemMgr.ICMeisaiOwnList2.Count, (_itemMgr.ICMeisaiOwnList1.Count + _itemMgr.ICMeisaiOwnList2.Count)), 1);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数相違 合計={0}件", totalCnt), 1);

                string msg = "予定件数（合計）に変更がありました。\n内容を確認し再度実行してください。";
                ComMessageMgr.MessageWarning(msg);
                _form.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                return false;
            }
            if (totalCnt < 1)
            {
                string msg = "作成対象データがありません。";
                ComMessageMgr.MessageInformation(msg);
                _form.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                return false;
            }

            // 確認メッセージ
            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "不渡返還テキスト作成処理を開始します。\nよろしいですか？");
            if (res == DialogResult.Cancel)
            {
                return false;
            }

            // レコードロック
            if (!_itemMgr.LockTables(ctype, dbp, non))
            {
                // データなし（正常終了）
                return true;
            }

            // ファイル一連番号採番
            int fileSeq = ImportFileAccess.GetFileSeq(FileParam.FileId.IF205, FileParam.FileKbn.GRA, 1, dbp, non);
            // 不渡返還テキスト
            FileGenerator if205 = new FileGenerator(fileSeq, FileParam.FileId.IF205, FileParam.FileKbn.GRA, Operator.BankCD, ".txt");
            _itemMgr.DispParams.if205 = if205;
            // 結果テキスト
            FileGenerator if206 = new FileGenerator(fileSeq, FileParam.FileId.IF206, FileParam.FileKbn.GRA, Operator.BankCD, ".txt");

            // テキストファイル作成
            CreateTextFiles1(if205, dbp, non);

            // 明細データ設定
            SetMeisaiDatas1();

            // ＤＢ更新
            _itemMgr.RegistMeisaiItems1(if205, if206, dbp, non);

            return true;
        }

        /// <summary>
        /// 行内交換連携を実施する
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        public bool DoExchange(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // 作成対象データ取得
            ItemManager.CreateType ctype;
            if (ServerIni.Setting.InternalExchange)
            {
                // 行内交換する場合は自行データのみデータ更新する
                ctype = ItemManager.CreateType.自行データ;
            }
            else
            {
                // 行内交換しない場合は処理しない（正常終了）
                return true;
            }

            // 作成対象データ取得（自行：持帰）
            if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.登録データ, ctype, _form))
            {
                // 異常終了
                return false;
            }
            if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.取消データ, ctype, _form))
            {
                // 異常終了
                return false;
            }

            // 作成対象データ取得（自行：持出）
            if (!_itemMgr.FetchTrMeiOC(ItemManager.DataType.登録データ, _form))
            {
                // 異常終了
                return false;
            }
            if (!_itemMgr.FetchTrMeiOC(ItemManager.DataType.取消データ, _form))
            {
                // 異常終了
                return false;
            }

            // 明細データ設定
            SetMeisaiDatas2();

            // ＤＢ更新
            _itemMgr.RegistMeisaiItems2(dbp, non);

            return true;
        }

        /// <summary>
        /// 明細データを設定する
        /// </summary>
        /// <returns></returns>
        private bool SetMeisaiDatas1()
        {
            // 明細件数（登録データ）
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiList1.Values)
            {
                mei.trmei.m_GRA_STS = TrMei.Sts.ファイル作成;
            }
            // 明細件数（取消データ）
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiList2.Values)
            {
                mei.trmei.m_GRA_STS = TrMei.Sts.ファイル作成;
            }
            return true;
        }

        /// <summary>
        /// 明細データを設定する
        /// </summary>
        /// <returns></returns>
        private bool SetMeisaiDatas2()
        {
            // 明細件数（登録データ）：持帰
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiOwnList1.Values)
            {
                mei.trmei.m_GRA_STS = TrMei.Sts.結果正常;
                mei.trmei.m_GRA_CONFIRMDATE = AplInfo.OpDate();
                // 削除フラグ設定
                mei.trmei.m_DELETE_FLG = 1;
                mei.trmei.m_DELETE_DATE = AplInfo.OpDate();
            }
            // 明細件数（取消データ）：持帰
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiOwnList2.Values)
            {
                mei.trmei.m_GRA_STS = TrMei.Sts.結果正常;
                mei.trmei.m_GRA_CONFIRMDATE = AplInfo.OpDate();

                // 不渡返還で削除されている場合は削除解除
                // (不渡返還以外で削除されている明細は処理対象外)
                mei.trmei.m_DELETE_FLG = 0;
                mei.trmei.m_DELETE_DATE = 0;
            }

            // 明細件数（登録データ）：持出
            foreach (ItemManager.MeisaiInfos ocmei in _itemMgr.OCMeisaiOwnList1.Values)
            {
                // 不渡返還通知日(持出)
                ocmei.trmei.m_GRA_DATE = AplInfo.OpDate();
                // 持出側の明細削除
                ocmei.trmei.m_DELETE_FLG = 1;
                ocmei.trmei.m_DELETE_DATE = AplInfo.OpDate();
            }
            // 明細件数（取消データ）：持出
            foreach (ItemManager.MeisaiInfos ocmei in _itemMgr.OCMeisaiOwnList2.Values)
            {
                // 不渡返還通知日(持出)
                // 取り消しの場合はクリア
                ocmei.trmei.m_GRA_DATE = 0;
                // 持出側の明細削除解除
                ocmei.trmei.m_DELETE_FLG = 0;
                ocmei.trmei.m_DELETE_DATE = 0;
            }
            return true;
        }

        /// <summary>
        /// テキストファイルを生成する
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateTextFiles1(FileGenerator if205, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string data = "";

            // ファイル名
            string filePath = Path.Combine(ServerIni.Setting.IOSendRoot, if205.FileName);

            // データ作成
            data += CreateHeaderRecord(if205, dbp, non);
            data += CreateDataRecord();
            data += CreateTrailerRecord();
            data += CreateEndRecord();

            // ファイル保存
            FileGenerator.WriteAllTextStream(filePath, data);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル保存=[{0}]", filePath), 1);

            // ファイルサイズ算出
            if205.SetFileSize(filePath);

            // 結果ファイル名（画面表示用）
            _itemMgr.DispParams.CreateFileName = if205.FileName;
            return true;
        }

        /// <summary>
        /// ヘッダーレコード作成
        /// </summary>
        /// <returns></returns>
        private string CreateHeaderRecord(FileGenerator if205, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            TBL_CTRUSERINFO userinfo = _masterMgr.GetCtrUserInfo(dbp, non);
            string userId = CommonUtil.BPadRight(userinfo._USERID, Const.CTRUSER_USERID_LEN, " ");
            string password = CommonUtil.BPadRight(userinfo.m_PASSWORD, Const.CTRUSER_PASSWORD_LEN, " ");
            string dummey = CommonUtil.BPadRight(string.Empty, 75, " ");

            StringBuilder data = new StringBuilder();
            data.Append("1");
            data.Append(FileParam.FileId.IF205);
            data.Append(FileParam.FileKbn.GRA);
            data.Append(ServerIni.Setting.BankCd.ToString(Const.BANK_NO_LEN_STR));
            data.Append(AplInfo.OpDate().ToString("D8"));
            data.Append(if205.Seq);
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
            StringBuilder data = new StringBuilder();

            // リストを連結してイメージファイル名の昇順でデータを作成する
            var mergeList = _itemMgr.ICMeisaiList1
                .Concat(_itemMgr.ICMeisaiList2.Where(p => !_itemMgr.ICMeisaiList1.ContainsKey(p.Key)))
                .ToDictionary(p => p.Key, pair => pair.Value);

            // 明細件数
            foreach (ItemManager.MeisaiInfos mei in mergeList.Values.OrderBy(p => p.trimg.m_IMG_FLNM))
            {
                TBL_TRFUWATARI fuwa = mei.trfuwa;
                string registKbn = GetRegistKbn(fuwa);
                string fileName = CommonUtil.BPadRight(mei.trimg.m_IMG_FLNM, 62, " ");
                string dummey = CommonUtil.BPadRight(string.Empty, 71, " ");

                data.Append("2");
                data.Append(registKbn);
                data.Append(fileName);
                data.Append(GetFubiKbn(registKbn, fuwa.m_FUBI_KBN_01));
                data.Append(GetFubiNo(registKbn, fuwa.m_FUBI_KBN_01, fuwa.m_ZERO_FUBINO_01));
                data.Append(GetFubiKbn(registKbn, fuwa.m_FUBI_KBN_02));
                data.Append(GetFubiNo(registKbn, fuwa.m_FUBI_KBN_02, fuwa.m_ZERO_FUBINO_02));
                data.Append(GetFubiKbn(registKbn, fuwa.m_FUBI_KBN_03));
                data.Append(GetFubiNo(registKbn, fuwa.m_FUBI_KBN_03, fuwa.m_ZERO_FUBINO_03));
                data.Append(GetFubiKbn(registKbn, fuwa.m_FUBI_KBN_04));
                data.Append(GetFubiNo(registKbn, fuwa.m_FUBI_KBN_04, fuwa.m_ZERO_FUBINO_04));
                data.Append(GetFubiKbn(registKbn, fuwa.m_FUBI_KBN_05));
                data.Append(GetFubiNo(registKbn, fuwa.m_FUBI_KBN_05, fuwa.m_ZERO_FUBINO_05));
                data.Append(dummey);
                data.Append(FileGenerator.CRLF);
            }
            return data.ToString();
        }

        /// <summary>
        /// トレーラーレコード作成
        /// </summary>
        /// <returns></returns>
        private string CreateTrailerRecord()
        {
            string recCnt = (_itemMgr.ICMeisaiList1.Count + _itemMgr.ICMeisaiList2.Count).ToString("D8");
            string dummey = CommonUtil.BPadRight(string.Empty, 141, " ");

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
            string dummey = CommonUtil.BPadRight(string.Empty, 149, " ");

            StringBuilder data = new StringBuilder();
            data.Append("9");
            data.Append(dummey);
            data.Append(FileGenerator.CRLF);
            return data.ToString();
        }

        /// <summary>
        /// 登録区分
        /// </summary>
        /// <param name="fuwa"></param>
        /// <returns></returns>
        private string GetRegistKbn(TBL_TRFUWATARI fuwa)
        {
            return (fuwa.m_DELETE_FLG == 0) ? "1" : "9";
        }

        /// <summary>
        /// 不渡返還区分
        /// </summary>
        /// <param name="registKbn"></param>
        /// <param name="fubiKbn"></param>
        /// <returns></returns>
        private string GetFubiKbn(string registKbn, int fubiKbn)
        {
            if (registKbn.Equals("9")) { return "Z"; }
            if (fubiKbn == -1) { return "Z"; }
            return fubiKbn.ToString();
        }

        /// <summary>
        /// 0号不渡事由コード
        /// </summary>
        /// <param name="registKbn"></param>
        /// <param name="fubiKbn"></param>
        /// <param name="fubiNo"></param>
        /// <returns></returns>
        private string GetFubiNo(string registKbn, int fubiKbn, int fubiNo)
        {
            if (registKbn.Equals("9")) { return "ZZ"; }
            if (fubiKbn != 0) { return "ZZ"; }
            return fubiNo.ToString("D2");
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
            // 一時テーブル削除
            _itemMgr.DropTmpTable(dbp, non);

            // ファイル削除
            if (_itemMgr.DispParams.if205 != null)
            {
                string filePath = Path.Combine(ServerIni.Setting.IOSendRoot, _itemMgr.DispParams.if205.FileName);
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

            // 一時テーブル削除
            _itemMgr.DropTmpTable(dbp, non);
        }
    }
}
