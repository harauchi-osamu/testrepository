using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;

namespace CTROcBatchView
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;
        private Controller _ctl = null;

        /// <summary>支店マスタ（key=BR_NO, val=TBL_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_BRANCHMF> mst_branches { get; set; }
        /// <summary>支店マスタ[スキャン支店]（key=BR_NO, val=TBL_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_BRANCHMF> mst_scanbranches { get; set; }
        public DataTable tbl_trbatches { get; set; }
        public SortedDictionary<int, TBL_TRBATCHIMG> batimges { get; set; }
        public SortedDictionary<int, ImageInfo> ImageInfos { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>バッチ情報</summary>
        public BatchInfos BatchInfo { get; set; }
        /// <summary>入力バッチ情報</summary>
        public InputBatchInfos InputBatchInfo { get; set; }

        public bool IsBatchUpdate { get; set; }

        private const string FIX_TRIGGER = "持出バッチ照会";

        /// <summary>一時テーブル補助項目</summary>
        private string _TMP_UNIQUEITEM = string.Empty;

        // *******************************************************************
        // 一時テーブル
        // *******************************************************************
        ///// <summary>一時テーブル</summary>
        //private const string TMP_TRBATCH = "TMP_TRBATCH";

        /// <summary>
        /// 一時テーブル：TMP_TRBATCH_{IPアドレス}
        /// </summary>
        private string TMP_TRBATCH
        {
            get { return string.Format("TMP_TRBATCH_{0}", _TMP_UNIQUEITEM); }
        }

        /// <summary>
		/// コンストラクタ
		/// </summary>
		public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            this.DispParams = new DisplayParams();
            this.DispParams.Clear();
            this.BatchInfo = new BatchInfos();
            this.BatchInfo.Clear();
            this.InputBatchInfo = new InputBatchInfos();
            this.InputBatchInfo.Clear();

            mst_branches = new SortedDictionary<int, TBL_BRANCHMF>();
            mst_scanbranches = new SortedDictionary<int, TBL_BRANCHMF>();
            ImageInfos = new SortedDictionary<int, ImageInfo>();
            // 一時テーブルの補助項目設定
            _TMP_UNIQUEITEM = ImportFileAccess.GetTermIPAddress().Replace(".", "_");
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void FetchAllData(Controller ctl)
        {
            _ctl = ctl;
            if (_ctl.IsIniErr) { return; }

            Fetch_mst_branches();
            Fetch_mst_scanbranches();
        }

        /// <summary>
        /// 支店情報の取得
        /// </summary>
        public bool GetBranch(int BankCd, int Branch, out string BranchName, FormBase form = null)
        {
            //初期化
            BranchName = string.Empty;

            // SQL文取得
            string strSQL = TBL_BRANCHMF.GetSelectQuery(Branch, BankCd);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        TBL_BRANCHMF BranchMF = new TBL_BRANCHMF(tbl.Rows[0], BankCd);

                        BranchName = BranchMF.m_BR_NAME_KANJI;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_mst_branches()
        {
            mst_branches = new SortedDictionary<int, TBL_BRANCHMF>();
            string strSQL = TBL_BRANCHMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BRANCHMF data = new TBL_BRANCHMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        mst_branches.Add(data._BR_NO, data);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_mst_scanbranches()
        {
            mst_scanbranches = new SortedDictionary<int, TBL_BRANCHMF>();
            string strSQL = TBL_BRANCHMF.GetSelectQuery(ServerIni.Setting.ContractBankCd);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BRANCHMF data = new TBL_BRANCHMF(tbl.Rows[i], ServerIni.Setting.ContractBankCd);
                        mst_scanbranches.Add(data._BR_NO, data);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納（バッチデータ）
        /// </summary>
        public bool GetOcBatList(EntryCommonFormBase form)
        {
            // 集計結果を検索条件とするので２回に分けてデータ抽出する
            // 1回目：集計しないで取得可能な条件で絞り込む
            // 2回目：集計条件で絞り込む

            tbl_trbatches = new DataTable();

            // SELECT実行
            string strSQL = "";
            string strSELECT = "";
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // 一時テーブル削除＆作成
                    DropTmpTable(dbp, non);
                    CreateTmpTable(dbp, non);

                    // 1回目抽出
                    // 抽出データ取得して一時テーブルに登録する
                    strSELECT = SQLSearch.GetOcBatchViewSelectBatList1(DispParams.ImportDate, DispParams.ScanBrCd, DispParams.ScanDate, DispParams.OcBrCd, DispParams.ClearlingDate, DispParams.BatCnt, DispParams.BatKingaku, AppInfo.Setting.SchemaBankCD);
                    strSQL = SQLSearch.GetInsertTmpTable(strSELECT, TMP_TRBATCH, TBL_TRBATCH.ALL_COLUMNS);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計1
                    strSQL = SQLSearch.GetOcBatchViewUpdateTmpBatchList1(TMP_TRBATCH, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計2
                    strSQL = SQLSearch.GetOcBatchViewUpdateTmpBatchList2(TMP_TRBATCH);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計3
                    strSQL = SQLSearch.GetOcBatchViewUpdateTmpBatchList3(TMP_TRBATCH);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 2回目抽出
                    strSQL = SQLSearch.GetOcBatchViewSelectBatList2(TMP_TRBATCH, DispParams.SaMaisu, DispParams.SaKingaku, DispParams.Status, AppConfig.ListDispLimit);
                    tbl_trbatches = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 一時テーブル削除
                    DropTmpTable(dbp, non);

                    // 参照のみなのでコミットしない
                    non.Trans.Rollback();
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納（バッチデータ）
        /// </summary>
        public bool GetOcBat(int gymid, int opedate, string scanterm, int batid, EntryCommonFormBase form)
        {
            // SELECT実行
            string strSQL = "";
            string strSELECT = "";
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // 一時テーブル削除＆作成
                    DropTmpTable(dbp, non);
                    CreateTmpTable(dbp, non);

                    // 抽出データ取得して一時テーブルに登録する
                    strSELECT = TBL_TRBATCH.GetSelectQuery(gymid, opedate, scanterm, batid, AppInfo.Setting.SchemaBankCD);
                    strSQL = SQLSearch.GetInsertTmpTable(strSELECT, TMP_TRBATCH, TBL_TRBATCH.ALL_COLUMNS);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計1
                    strSQL = SQLSearch.GetOcBatchViewUpdateTmpBatchList1(TMP_TRBATCH, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計2
                    strSQL = SQLSearch.GetOcBatchViewUpdateTmpBatchList2(TMP_TRBATCH);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計3
                    strSQL = SQLSearch.GetOcBatchViewUpdateTmpBatchList3(TMP_TRBATCH);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 集計結果を抽出
                    strSQL = " SELECT * FROM " + TMP_TRBATCH;
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), non.Trans);
                    if (tbl.Rows.Count > 0)
                    {
                        BatchInfo.BatRow = tbl.Rows[0];
                        BatchInfo.trbat = new TBL_TRBATCH(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }

                    // 一時テーブル削除
                    DropTmpTable(dbp, non);

                    // 参照のみなのでコミットしない
                    non.Trans.Rollback();
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 一時テーブル作成
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        public void CreateTmpTable(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            CreateTmpTrBatTable(TMP_TRBATCH, dbp, non);
        }

        /// <summary>
        /// 一時テーブル削除
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        public void DropTmpTable(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            DropTmpTable(TMP_TRBATCH, dbp, non);
        }

        /// <summary>
        /// 一時テーブルを作成する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void CreateTmpTrBatTable(string tableName, AdoDatabaseProvider dbp, AdoNonCommitTransaction auto)
        {
            string strSQL = "";
            try
            {
                strSQL = SQLSearch.GetCreateTMP_TRBATCH(tableName);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// 一時テーブルを削除する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void DropTmpTable(string tableName, AdoDatabaseProvider dbp, AdoNonCommitTransaction auto)
        {
            string strSQL = "";
            try
            {
                strSQL = DBCommon.GetDropTmpTableSQL(tableName);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception)
            {
                // 初回はテーブルないので例外になる
                //LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.Message, 2);
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void Fetch_batimges(int gymid, int opedate, string scanterm, int batid)
        {
            // イメージ情報
            ImageInfos = new SortedDictionary<int, ImageInfo>();
            ImageInfos.Add(TrMeiImg.ImgKbn.表, new ImageInfo(TrMeiImg.ImgKbn.表));
            ImageInfos.Add(TrMeiImg.ImgKbn.裏, new ImageInfo(TrMeiImg.ImgKbn.裏));

            string strSQL = TBL_TRBATCHIMG.GetSelectQuery(gymid, opedate, scanterm, batid, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            batimges = new SortedDictionary<int, TBL_TRBATCHIMG>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_TRBATCHIMG data = new TBL_TRBATCHIMG(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        batimges.Add(data._IMG_KBN, data);

                        // イメージ情報
                        if (ImageInfos.ContainsKey(data._IMG_KBN))
                        {
                            ImageInfos[data._IMG_KBN].BatImage = data;
                            ImageInfos[data._IMG_KBN].HasImage = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        /// <remarks>未使用</remarks>
        public bool CanBatchEdit(int gymid, int opedate, string scanterm, int batid)
        {
            bool retVal = false;
            string strSQL = SQLSearch.GetOcBatchViewCanBatchEditQuery(gymid, opedate, scanterm, batid, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    retVal = (tbl.Rows.Count == 0);
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return retVal;
        }

        /// <summary>
        /// バッチ配下にアップロード済(アップロード中・結果正常)の証券が存在するか確認
        /// </summary>
        public bool ChkBatchImgUpload(int gymid, int opedate, string scanterm, int batid)
        {
            bool retVal = false;
            string strSQL = SQLSearch.GetOcBatchViewBatchImgUploadQuery(gymid, opedate, scanterm, batid, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    retVal = (tbl.Rows.Count > 0);
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return retVal;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public bool CanBatchDelete1(int gymid, int opedate, string scanterm, int batid)
        {
            // 対象バッチデータの「ステータスが「10:入力完了」かつ未削除」以外の場合は削除不可
            bool retVal = false;
            string strSQL = "";

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    strSQL = SQLSearch.GetOcBatchViewCanBatchDeleteQuery1(gymid, opedate, scanterm, batid, AppInfo.Setting.SchemaBankCD);
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    retVal = (tbl.Rows.Count > 0);
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return retVal;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public DataTable CanBatchDelete2(int gymid, int opedate, string scanterm, int batid)
        {
            // 取得データに「5(ファイル作成), 10(アップロード)」が存在している場合は削除不可
            DataTable tbl_trimg = new DataTable();
            string strSQL = "";

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    strSQL = SQLSearch.GetOcBatchViewCanBatchDeleteQuery2(gymid, opedate, scanterm, batid, AppInfo.Setting.SchemaBankCD);
                    tbl_trimg = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return null;
                }
            }
            return tbl_trimg;
        }

        /// <summary>
        /// ステータスを処理中に更新する
        /// </summary>
        /// <returns></returns>
        public bool UpdateTrBatchStatusInput(int gymid, int opedate, string scanterm, int batid)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // 排他制御
                    strSQL = SQLSearch.GetOcBatchViewSelectTrBatchQuery(gymid, opedate, scanterm, batid, AppInfo.Setting.SchemaBankCD);
                    strSQL += DBConvert.QUERY_LOCK;
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    if (tbl.Rows.Count < 1)
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        auto.isCommitEnd = false;
                        auto.Trans.Rollback();
                        // メッセージ表示
                        ComMessageMgr.MessageWarning("対象のバッチ票は訂正中または削除済のため訂正できません。");
                        return false;
                    }

                    // 状態更新
                    TBL_TRBATCH newBat = new TBL_TRBATCH(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    newBat.m_STS = TrBatch.Sts.入力中;

                    // UPDATE
                    strSQL = newBat.GetUpdateQuery();
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    // 取得した行ロックを解除するためロールバック
                    // メッセージボックス表示前に実施
                    auto.isCommitEnd = false;
                    auto.Trans.Rollback();
                    // メッセージ表示
                    if (ex.Message.IndexOf(Const.ORACLE_ERR_LOCK) != -1)
                    {
                        // ロック中
                        ComMessageMgr.MessageWarning(ComMessageMgr.E01003);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    }
                    else
                    {
                        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ステータスを入力完了に更新する
        /// </summary>
        /// <returns></returns>
        public bool UpdateTrBatchStatusComp(TBL_TRBATCH bat)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // 状態更新
                    strSQL = SQLSearch.GetOcBatchViewUpdateTrBatchStatusCompQuery(bat, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// バッチ情報を更新する
        /// </summary>
        /// <returns></returns>
        public bool UpdateTrBatch(TBL_TRBATCH bat)
        {
            // 読替処理
            EntryReplacer er = new EntryReplacer();
            string sClearingDate = "";
            string sWarekiDate = "";
            string sBusinessDate = "";

            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // TRBATCH 更新
                    {
                        bat.m_SCAN_BR_NO = InputBatchInfo.ScanBrCd;
                        bat.m_SCAN_DATE = InputBatchInfo.ScanDate;
                        bat.m_SCAN_COUNT = InputBatchInfo.ScanCnt;
                        bat.m_OC_BR_NO = InputBatchInfo.OcBrCd;
                        bat.m_CLEARING_DATE = InputBatchInfo.ClearingDate;
                        bat.m_TOTAL_COUNT = InputBatchInfo.BatCount;
                        bat.m_TOTAL_AMOUNT = InputBatchInfo.BatKingaku;
                        bat.m_STS = TrBatch.Sts.入力完了;

                        // UPDATE
                        strSQL = bat.GetUpdateQuery();
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }

                    // TRITEM 更新
                    {
                        SortedDictionary<int, MeisaiInfos> meisais = new SortedDictionary<int, MeisaiInfos>();
                        strSQL = SQLSearch.GetOcBatchViewSelectTrItem(bat, InputBatchInfo.BefClearingDate, AppInfo.Setting.SchemaBankCD);

                        // 更新対象データ取得
                        DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        foreach (DataRow row in tbl.Rows)
                        {
                            TBL_TRITEM tritem = new TBL_TRITEM(row, AppInfo.Setting.SchemaBankCD);
                            TBL_TRMEI trmei = new TBL_TRMEI(tritem._GYM_ID, tritem._OPERATION_DATE, tritem._SCAN_TERM, tritem._BAT_ID, tritem._DETAILS_NO, AppInfo.Setting.SchemaBankCD);

                            // 明細取得
                            MeisaiInfos mei = null;
                            if (meisais.ContainsKey(trmei._DETAILS_NO))
                            {
                                mei = meisais[trmei._DETAILS_NO];
                            }
                            else
                            {
                                mei = new MeisaiInfos();
                                mei.trmei = trmei;
                                meisais.Add(trmei._DETAILS_NO, mei);
                            }

                            // アイテム取得
                            if (!mei.tritems.ContainsKey(tritem._ITEM_ID))
                            {
                                mei.tritems.Add(tritem._ITEM_ID, tritem);
                            }
                        }

                        // 項目トランザクション更新
                        foreach (MeisaiInfos meisai in meisais.Values)
                        {
                            // 読替処理
                            sClearingDate = InputBatchInfo.ClearingDate.ToString();
                            sWarekiDate = "";
                            sBusinessDate = "";
                            er.ReplaceClearingDate(sClearingDate, ref sWarekiDate, ref sBusinessDate);

                            TBL_TRITEM cDateInput = meisai.tritems[DspItem.ItemId.入力交換希望日];
                            cDateInput.m_END_DATA = sClearingDate;
                            cDateInput.m_FIX_TRIGGER = FIX_TRIGGER;
                            strSQL = cDateInput.GetUpdateQuery();
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                            TBL_TRITEM cDateWareki = meisai.tritems[DspItem.ItemId.和暦交換希望日];
                            cDateWareki.m_END_DATA = sWarekiDate;
                            cDateWareki.m_FIX_TRIGGER = FIX_TRIGGER;
                            strSQL = cDateWareki.GetUpdateQuery();
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                            TBL_TRITEM cDateEdit = meisai.tritems[DspItem.ItemId.交換日];
                            cDateEdit.m_END_DATA = sBusinessDate.Replace(".", "");
                            cDateEdit.m_FIX_TRIGGER = FIX_TRIGGER;
                            strSQL = cDateEdit.GetUpdateQuery();
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }
                    }
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }

            // 更新フラグ
            this.IsBatchUpdate = true;
            return true;
        }

        /// <summary>
        /// バッチ情報を削除する
        /// </summary>
        /// <returns></returns>
        public bool DeleteTrBatch(TBL_TRBATCH bat)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // TRBATCH 更新
                    strSQL = SQLSearch.GetOcBatchViewDeleteTrBatchQuery(bat, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // TRMEI 更新
                    strSQL = SQLSearch.GetOcBatchViewDeleteTrMeiQuery(bat, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // TRMEIIMG 更新
                    strSQL = SQLSearch.GetOcBatchViewDeleteTrImageQuery(bat, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }

            // 更新フラグ
            this.IsBatchUpdate = true;
            return true;
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            public int ImportDate { get; set; }
            public int ScanBrCd { get; set; }
            public int ScanDate { get; set; }
            public int OcBrCd { get; set; }
            public int ClearlingDate { get; set; }
            public int BatCnt { get; set; }
            public long BatKingaku { get; set; }
            public int SaMaisu { get; set; }
            public int SaKingaku { get; set; }
            public int Status { get; set; }
            public string Key { get; set; }

            public void Clear()
			{
                this.ImportDate = -1;
                this.ScanBrCd = -1;
                this.ScanDate = -1;
                this.OcBrCd = -1;
                this.ClearlingDate = -1;
                this.BatCnt = -1;
                this.BatKingaku = -1;
                this.SaMaisu = -1;
                this.SaKingaku = -1;
                this.Status = -1;
                this.Key = string.Empty;
            }
        }

        /// <summary>
        /// バッチ情報
        /// </summary>
        public class BatchInfos
        {
            public int GymId { get; set; }
            public int OpeDate { get; set; }
            public string ScanTerm { get; set; }
            public int BatId { get; set; }
            public DataRow BatRow { get; set; }
            public TBL_TRBATCH trbat { get; set; }

            public void Clear()
            {
                this.GymId = -1;
                this.OpeDate = -1;
                this.ScanTerm = "";
                this.BatId = -1;
                this.BatRow = null;
                this.trbat = null;
            }
        }

        public class ImageInfo
        {
            public int ImgKbn { get; private set; }
            public bool HasImage { get; set; } = false;
            public TBL_TRBATCHIMG BatImage { get; set; } = null;

            public ImageInfo(int imgKbm)
            {
                ImgKbn = imgKbm;
            }
        }

        /// <summary>
        /// 入力バッチ情報
        /// </summary>
        public class InputBatchInfos
        {
            public int ScanBrCd { get; set; }
            public int ScanDate { get; set; }
            public int ScanCnt { get; set; }
            public int OcBrCd { get; set; }
            public int ClearingDate { get; set; }
            public int BatCount { get; set; }
            public long BatKingaku { get; set; }
            public bool IsClearingDateUpdate { get; set; }
            public bool IsOcBrCdUpdate { get; set; }

            /// <summary>変更前：交換希望日</summary>
            public int BefClearingDate { get; set; }
            /// <summary>変更前：持出支店コード</summary>
            public int BefOcBrCd { get; set; }

            public void Clear()
            {
                this.ScanBrCd = 1;
                this.ScanDate = 1;
                this.ScanCnt = 1;
                this.OcBrCd = -1;
                this.ClearingDate = -1;
                this.BatCount = -1;
                this.BatKingaku = -1;
                this.BefClearingDate = -1;
                this.BefOcBrCd = -1;
                this.IsClearingDateUpdate = false;
                this.IsOcBrCdUpdate = false;
            }
        }

        /// <summary>
        /// 明細情報
        /// </summary>
        public class MeisaiInfos
        {
            public TBL_TRMEI trmei { get; set; }
            public SortedDictionary<int, TBL_TRITEM> tritems { get; set; }

            public MeisaiInfos()
            {
                trmei = new TBL_TRMEI(AppInfo.Setting.SchemaBankCD);
                tritems = new SortedDictionary<int, TBL_TRITEM>();
            }
        }

    }
}
