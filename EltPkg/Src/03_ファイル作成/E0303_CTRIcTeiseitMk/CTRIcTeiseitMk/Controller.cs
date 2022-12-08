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

namespace CTRIcTeiseitMk
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

        public const string FIX_TRIGGER_STR = "持帰証券データ訂正";


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
            _itemMgr.DispParams.if204 = null;

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
            if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.訂正データ, ctype, _form))
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
                if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.訂正データ, ItemManager.CreateType.自行データ, _form))
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
            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "持帰証券データ訂正テキスト作成処理を開始します。\nよろしいですか？");
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
            int fileSeq = ImportFileAccess.GetFileSeq(FileParam.FileId.IF204, FileParam.FileKbn.GMA, 1, dbp, non);
            // 証券データ訂正テキストファイル
            FileGenerator if204 = new FileGenerator(fileSeq, FileParam.FileId.IF204, FileParam.FileKbn.GMA, Operator.BankCD, ".txt");
            _itemMgr.DispParams.if204 = if204;
            // 結果テキスト
            FileGenerator if206 = new FileGenerator(fileSeq, FileParam.FileId.IF206, FileParam.FileKbn.GMA, Operator.BankCD, ".txt");

            // テキストファイル作成
            CreateTextFiles1(if204, dbp, non);

            // 明細データ設定
            SetMeisaiDatas1();

            // ＤＢ更新
            _itemMgr.RegistMeisaiItems1(if204, if206, dbp, non);

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
            if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.訂正データ, ctype, _form))
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
            if (!_itemMgr.FetchTrMeiOC(ItemManager.DataType.訂正データ, _form))
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
            SetMeisaiDatas2(dbp);

            // ＤＢ更新
            _itemMgr.RegistMeisaiItems2(FIX_TRIGGER_STR, dbp, non);

            return true;
        }

        /// <summary>
        /// 明細データを設定する
        /// </summary>
        /// <returns></returns>
        private bool SetMeisaiDatas1()
        {
            // 明細件数（訂正データ）
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiList1.Values)
            {
                mei.trmei.m_GMA_STS = TrMei.Sts.ファイル作成;
            }
            // 明細件数（取消データ）
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiList2.Values)
            {
                mei.trmei.m_GMA_STS = TrMei.Sts.ファイル作成;
            }
            return true;
        }

        /// <summary>
        /// 明細データを設定する
        /// </summary>
        /// <returns></returns>
        private bool SetMeisaiDatas2(AdoDatabaseProvider dbp)
        {
            // 明細件数（訂正データ）：持帰
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiOwnList1.Values)
            {
                mei.trmei.m_GMA_STS = TrMei.Sts.結果正常;

                // 行内連携で持帰銀行が変更となった場合、削除フラグを設定
                if (mei.tritems.ContainsKey(DspItem.ItemId.持帰銀行コード))
                {
                    if (DBConvert.ToIntNull(mei.tritems[DspItem.ItemId.持帰銀行コード].m_END_DATA) != AppInfo.Setting.SchemaBankCD)
                    {
                        mei.trmei.m_DELETE_FLG = 1;
                        mei.trmei.m_DELETE_DATE = AplInfo.OpDate();
                    }
                }
            }

            // 明細件数（取消データ）：持帰
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiOwnList2.Values)
            {
                //訂正データに存在する明細の場合は重複フラグを設定
                if (_itemMgr.ICMeisaiOwnList1.ContainsKey(mei.Key))
                {
                    mei.DuplicateFlg = true;
                }
                mei.trmei.m_GMA_STS = TrMei.Sts.結果正常;
            }

            // ITEMの更新は明細データを元に一括更新するよう修正
            //// 明細件数（訂正データ）：持帰
            //foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiOwnList1.Values)
            //{
            //    foreach (TBL_TRITEM icitem in mei.tritems.Values)
            //    {
            //        // END_DATAの値をICTEISEI_DATAに設定
            //        icitem.m_ICTEISEI_DATA = icitem.m_END_DATA;
            //        icitem.m_FIX_TRIGGER = FIX_TRIGGER_STR;
            //        break;
            //    }
            //}
            //// 明細件数（取消データ）：持帰
            //foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiOwnList2.Values)
            //{
            //    foreach (TBL_TRITEM icitem in mei.tritems.Values)
            //    {
            //        // END_DATAの値をICTEISEI_DATAに設定
            //        icitem.m_ICTEISEI_DATA = icitem.m_END_DATA;
            //        icitem.m_FIX_TRIGGER = FIX_TRIGGER_STR;
            //        break;
            //    }
            //}

            // 明細件数（訂正データ）：持出
            foreach (ItemManager.MeisaiInfos ocmei in _itemMgr.OCMeisaiOwnList1.Values)
            {
                // 持出データの項目に持帰データの項目を設定する
                ItemManager.MeisaiInfos icmei = GetICMei(ItemManager.DataType.訂正データ, ocmei);

                //持帰の更新対象項目一覧を取得
                Dictionary<int, TBL_TRITEM> updIcItems = GetUpdICItems(icmei, dbp);

                foreach (TBL_TRITEM ocitem in ocmei.tritems.Values)
                {
                    // 後続のためFIX_TRIGGERを一旦クリア
                    ocitem.m_FIX_TRIGGER = string.Empty;
                    if (updIcItems.ContainsKey(ocitem._ITEM_ID))
                    {
                        // 持帰データの END_DATA で更新
                        ocitem.m_ICTEISEI_DATA = updIcItems[ocitem._ITEM_ID].m_END_DATA;
                        ocitem.m_END_DATA = updIcItems[ocitem._ITEM_ID].m_END_DATA;
                        ocitem.m_FIX_TRIGGER = FIX_TRIGGER_STR;

                        // 行内連携で持帰銀行が変更となった場合、持出アップロードを初期化(ocmeiには表面のみ設定)
                        if (ocitem._ITEM_ID.Equals(DspItem.ItemId.持帰銀行コード) &&
                            DBConvert.ToIntNull(ocitem.m_END_DATA) != AppInfo.Setting.SchemaBankCD)
                        {
                            ocmei.BUAReset = true;
                            ocmei.trimg.m_BUA_STS = TrMei.Sts.再作成対象;
                            ocmei.trimg.m_BUB_CONFIRMDATE = 0;
                            ocmei.trimg.m_BUA_DATE = 0;
                            ocmei.trimg.m_BUA_TIME = 0;
                        }
                    }
                }

                // 証券データ訂正通知日(持出)
                ocmei.trmei.m_GMA_DATE = AplInfo.OpDate();
            }

            // 明細件数（取消データ）：持出
            foreach (ItemManager.MeisaiInfos ocmei in _itemMgr.OCMeisaiOwnList2.Values)
            {
                // 持出データの項目に持帰データの項目を設定する
                ItemManager.MeisaiInfos icmei = GetICMei(ItemManager.DataType.取消データ, ocmei);

                //持帰の更新対象項目一覧を取得
                Dictionary<int, TBL_TRITEM> updIcItems = GetUpdICItems(icmei, dbp);

                foreach (TBL_TRITEM ocitem in ocmei.tritems.Values)
                {
                    // 後続のためFIX_TRIGGERを一旦クリア
                    ocitem.m_FIX_TRIGGER = string.Empty;
                    if (updIcItems.ContainsKey(ocitem._ITEM_ID))
                    {
                        // 持出データの CTR_DATA で更新
                        ocitem.m_END_DATA = ocitem.m_CTR_DATA;
                        ocitem.m_FIX_TRIGGER = FIX_TRIGGER_STR;
                    }
                }

                //訂正データに存在する明細の場合は重複フラグを設定
                if (_itemMgr.OCMeisaiOwnList1.ContainsKey(ocmei.Key))
                {
                    ocmei.DuplicateFlg = true;
                }

                // 証券データ訂正通知日(持出)
                ocmei.trmei.m_GMA_DATE = AplInfo.OpDate();
            }
            return true;
        }

        /// <summary>
        /// 明細データ（持帰）を取得する
        /// </summary>
        /// <param name="dtype"></param>
        /// <param name="mei"></param>
        /// <returns></returns>
        private ItemManager.MeisaiInfos GetICMei(ItemManager.DataType dtype, ItemManager.MeisaiInfos mei)
        {
            if (dtype == ItemManager.DataType.訂正データ)
            {
                var meis =_itemMgr.ICMeisaiOwnList1.Values.Where(p => p.trimg.m_IMG_FLNM.Equals(mei.trimg.m_IMG_FLNM));
                if (meis.Count() > 0)
                {
                    return meis.First();
                }
            }
            else if (dtype == ItemManager.DataType.取消データ)
            {
                var meis = _itemMgr.ICMeisaiOwnList2.Values.Where(p => p.trimg.m_IMG_FLNM.Equals(mei.trimg.m_IMG_FLNM));
                if (meis.Count() > 0)
                {
                    return meis.First();
                }
            }
            // null は起こりえない
            return null;
        }

        /// <summary>
        /// 更新対象項目データを取得する
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, TBL_TRITEM> GetUpdICItems(ItemManager.MeisaiInfos icmei, AdoDatabaseProvider dbp)
        {
            //持帰の項目一覧を取得
            Dictionary<int, TBL_TRITEM> icitems = _itemMgr.GetTrItemList(icmei.trmei, dbp);

            Dictionary<int, TBL_TRITEM> updIcItems = new Dictionary<int, TBL_TRITEM>();
            foreach (TBL_TRITEM icitem in icmei.tritems.Values)
            {
                switch (icitem._ITEM_ID)
                {
                    case DspItem.ItemId.持帰銀行コード:
                        updIcItems.Add(DspItem.ItemId.持帰銀行コード, icitems[DspItem.ItemId.持帰銀行コード]);
                        updIcItems.Add(DspItem.ItemId.持帰銀行名, icitems[DspItem.ItemId.持帰銀行名]);
                        updIcItems.Add(DspItem.ItemId.券面持帰銀行コード, icitems[DspItem.ItemId.券面持帰銀行コード]);

                        break;
                    case DspItem.ItemId.交換日:
                        updIcItems.Add(DspItem.ItemId.入力交換希望日, icitems[DspItem.ItemId.入力交換希望日]);
                        updIcItems.Add(DspItem.ItemId.和暦交換希望日, icitems[DspItem.ItemId.和暦交換希望日]);
                        updIcItems.Add(DspItem.ItemId.交換日, icitems[DspItem.ItemId.交換日]);

                        break;
                    case DspItem.ItemId.金額:
                        updIcItems.Add(DspItem.ItemId.金額, icitems[DspItem.ItemId.金額]);

                        break;
                }
            }

            return updIcItems;
        }

        /// <summary>
        /// テキストファイルを生成する
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateTextFiles1(FileGenerator if204, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string data = "";

            // ファイル名
            string filePath = Path.Combine(ServerIni.Setting.IOSendRoot, if204.FileName);

            // データ作成
            data += CreateHeaderRecord(if204, dbp, non);
            data += CreateDataRecord(dbp, non);
            data += CreateTrailerRecord();
            data += CreateEndRecord();

            // ファイル保存
            FileGenerator.WriteAllTextStream(filePath, data);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル保存=[{0}]", filePath), 1);

            // ファイルサイズ算出
            if204.SetFileSize(filePath);

            // 結果ファイル名（画面表示用）
            _itemMgr.DispParams.CreateFileName = if204.FileName;
            return true;
        }

        /// <summary>
        /// ヘッダーレコード作成
        /// </summary>
        /// <returns></returns>
        private string CreateHeaderRecord(FileGenerator if204, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            TBL_CTRUSERINFO userinfo = _masterMgr.GetCtrUserInfo(dbp, non);
            string userId = CommonUtil.BPadRight(userinfo._USERID, Const.CTRUSER_USERID_LEN, " ");
            string password = CommonUtil.BPadRight(userinfo.m_PASSWORD, Const.CTRUSER_PASSWORD_LEN, " ");
            string dummey = CommonUtil.BPadRight(string.Empty, 75, " ");

            StringBuilder data = new StringBuilder();
            data.Append("1");
            data.Append(FileParam.FileId.IF204);
            data.Append(FileParam.FileKbn.GMA);
            data.Append(ServerIni.Setting.BankCd.ToString(Const.BANK_NO_LEN_STR));
            data.Append(AplInfo.OpDate().ToString("D8"));
            data.Append(if204.Seq);
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
        private string CreateDataRecord(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            StringBuilder data = new StringBuilder();

            //訂正明細に存在する取消明細の場合は重複フラグを設定(持帰)
            foreach (ItemManager.MeisaiInfos mei in _itemMgr.ICMeisaiList2.Values)
            {
                if (_itemMgr.ICMeisaiList1.ContainsKey(mei.Key))
                {
                    mei.DuplicateFlg = true;
                }
            }

            // リストを連結してイメージファイル名の昇順でデータを作成する
            var ConcatList = _itemMgr.ICMeisaiList1.Values.Concat(_itemMgr.ICMeisaiList2.Values.Where(p => p.DuplicateFlg == false));
            // 明細件数
            foreach (ItemManager.MeisaiInfos mei in ConcatList.OrderBy(p => p.trimg.m_IMG_FLNM))
            {
                string fileName = CommonUtil.BPadRight(mei.trimg.m_IMG_FLNM, 62, " ");
                string dummey = CommonUtil.BPadRight(string.Empty, 36, " ");

                //処理対象のItem一覧を作成
                Dictionary<int, ItemManager.ItemInfos> items = new Dictionary<int, ItemManager.ItemInfos>();
                foreach (KeyValuePair<string, ItemManager.MeisaiInfos> Teiseimei in _itemMgr.ICMeisaiList1.Where(x => x.Key == mei.Key))
                {
                    foreach (TBL_TRITEM TeiseiItem in Teiseimei.Value.tritems.Values)
                    {
                        items.Add(TeiseiItem._ITEM_ID, new ItemManager.ItemInfos(TeiseiItem, Teiseimei.Value.Type));

                        // 対象が交換日の場合は入力交換希望日も設定
                        if (TeiseiItem._ITEM_ID == DspItem.ItemId.交換日)
                        {
                            TBL_TRITEM inpDate = _itemMgr.GetTrItem(Teiseimei.Value.trmei, DspItem.ItemId.入力交換希望日, dbp);
                            items.Add(DspItem.ItemId.入力交換希望日, new ItemManager.ItemInfos(inpDate, Teiseimei.Value.Type));
                        }
                    }
                }
                foreach (KeyValuePair<string, ItemManager.MeisaiInfos> Torikesimei in _itemMgr.ICMeisaiList2.Where(x => x.Key == mei.Key))
                {
                    foreach (TBL_TRITEM TeiseiItem in Torikesimei.Value.tritems.Values)
                    {
                        items.Add(TeiseiItem._ITEM_ID, new ItemManager.ItemInfos(TeiseiItem, Torikesimei.Value.Type));

                        // 対象が交換日の場合は入力交換希望日も設定
                        if (TeiseiItem._ITEM_ID == DspItem.ItemId.交換日)
                        {
                            TBL_TRITEM inpDate = _itemMgr.GetTrItem(Torikesimei.Value.trmei, DspItem.ItemId.入力交換希望日, dbp);
                            items.Add(DspItem.ItemId.入力交換希望日, new ItemManager.ItemInfos(inpDate, Torikesimei.Value.Type));
                        }
                    }
                }

                // データ
                data.Append("2");
                data.Append(fileName);
                data.Append(GetTeiseiBankCdFlg(items));
                data.Append(GetTeiseiBankCdBef(items));
                data.Append(GetTeiseiBankCdAft(items));
                data.Append(GetTeiseiClearingDateFlg(items));
                data.Append(GetTeiseiClearingBef(items));
                data.Append(GetTeiseiClearingAft(items));
                data.Append(GetTeiseiKingakuFlg(items));
                data.Append(GetTeiseiKingakuBef(items));
                data.Append(GetTeiseiKingakuAft(items));
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
            // リストを連結した件数を設定
            string recCnt = _itemMgr.ICMeisaiList1.Concat(_itemMgr.ICMeisaiList2.Where(p => !_itemMgr.ICMeisaiList1.ContainsKey(p.Key))).LongCount().ToString("D8");
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
        /// 持帰銀行コード訂正フラグ
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiBankCdFlg(Dictionary<int, ItemManager.ItemInfos> items)
        {
            return items.ContainsKey(DspItem.ItemId.持帰銀行コード) ? "1" : "0";
        }

        /// <summary>
        /// 訂正前持帰銀行コード
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiBankCdBef(Dictionary<int, ItemManager.ItemInfos> items)
        {
            if (GetTeiseiBankCdFlg(items).Equals("1"))
            {
                if (items[DspItem.ItemId.持帰銀行コード].Type == ItemManager.DataType.訂正データ)
                {
                    if (items[DspItem.ItemId.持帰銀行コード].tritem.m_ICTEISEI_DATA.Length > 0)
                    {
                        return CommonUtil.PadLeft(items[DspItem.ItemId.持帰銀行コード].tritem.m_ICTEISEI_DATA, 4, "0");
                    }
                    else
                    {
                        return CommonUtil.PadLeft(items[DspItem.ItemId.持帰銀行コード].tritem.m_CTR_DATA, 4, "0");
                    }
                }
            }
            return "ZZZZ";
        }

        /// <summary>
        /// 訂正後持帰銀行コード
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiBankCdAft(Dictionary<int, ItemManager.ItemInfos> items)
        {
            if (GetTeiseiBankCdFlg(items).Equals("1"))
            {
                if (items[DspItem.ItemId.持帰銀行コード].Type == ItemManager.DataType.訂正データ)
                {
                    return CommonUtil.PadLeft(items[DspItem.ItemId.持帰銀行コード].tritem.m_END_DATA, 4, "0");
                }
            }
            return "ZZZZ";
        }

        /// <summary>
        /// 交換希望日訂正フラグ
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiClearingDateFlg(Dictionary<int, ItemManager.ItemInfos> items)
        {
            if (!items.ContainsKey(DspItem.ItemId.交換日))
            {
                return "0";
            }

            switch (items[DspItem.ItemId.交換日].Type)
            {
                case ItemManager.DataType.訂正データ:
                    return "1";
                case ItemManager.DataType.取消データ:
                    return "9";
                default:
                    throw new Exception("交換日データエラー");
            }
        }

        /// <summary>
        /// 訂正前交換希望日
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiClearingBef(Dictionary<int, ItemManager.ItemInfos> items)
        {
            if (GetTeiseiClearingDateFlg(items).Equals("1"))
            {
                if (items[DspItem.ItemId.入力交換希望日].tritem.m_ICTEISEI_DATA.Length > 0)
                {
                    // １度訂正している場合は、交換希望日をセットしているため交換日を取得
                    return CommonUtil.PadLeft(items[DspItem.ItemId.交換日].tritem.m_ICTEISEI_DATA, 8, "0");
                }
                else
                {
                    // 訂正前は入力交換希望日から取得
                    return CommonUtil.PadLeft(items[DspItem.ItemId.入力交換希望日].tritem.m_CTR_DATA, 8, "0");
                }
            }
            return "ZZZZZZZZ";
        }

        /// <summary>
        /// 訂正後交換希望日
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiClearingAft(Dictionary<int, ItemManager.ItemInfos> items)
        {
            if (GetTeiseiClearingDateFlg(items).Equals("1"))
            {
                return CommonUtil.PadLeft(items[DspItem.ItemId.交換日].tritem.m_END_DATA, 8, "0");
            }
            return "ZZZZZZZZ";
        }

        /// <summary>
        /// 金額訂正フラグ
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiKingakuFlg(Dictionary<int, ItemManager.ItemInfos> items)
        {
            if (!items.ContainsKey(DspItem.ItemId.金額))
            {
                return "0";
            }

            switch (items[DspItem.ItemId.金額].Type)
            {
                case ItemManager.DataType.訂正データ:
                    return "1";
                case ItemManager.DataType.取消データ:
                    return "9";
                default:
                    throw new Exception("金額データエラー");
            }
        }

        /// <summary>
        /// 訂正前金額
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiKingakuBef(Dictionary<int, ItemManager.ItemInfos> items)
        {
            if (GetTeiseiKingakuFlg(items).Equals("1"))
            {
                if (items[DspItem.ItemId.金額].tritem.m_ICTEISEI_DATA.Length > 0)
                {
                    return CommonUtil.PadLeft(items[DspItem.ItemId.金額].tritem.m_ICTEISEI_DATA, 12, "0");
                }
                else
                {
                    return CommonUtil.PadLeft(items[DspItem.ItemId.金額].tritem.m_CTR_DATA, 12, "0");
                }
            }
            return "ZZZZZZZZZZZZ";
        }

        /// <summary>
        /// 訂正後金額
        /// </summary>
        /// <param name="mei"></param>
        /// <returns></returns>
        private string GetTeiseiKingakuAft(Dictionary<int, ItemManager.ItemInfos> items)
        {
            if (GetTeiseiKingakuFlg(items).Equals("1"))
            {
                return CommonUtil.PadLeft(items[DspItem.ItemId.金額].tritem.m_END_DATA, 12, "0");
            }
            return "ZZZZZZZZZZZZ";
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
            if (_itemMgr.DispParams.if204 != null)
            {
                string filePath = Path.Combine(ServerIni.Setting.IOSendRoot, _itemMgr.DispParams.if204.FileName);
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
