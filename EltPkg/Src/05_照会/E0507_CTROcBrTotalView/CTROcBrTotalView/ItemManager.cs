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

namespace CTROcBrTotalView
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        private Controller _ctl = null;

        public List<TBL_BANKMF> _bankMF = null;
        /// <summary>銀行マスタ（key=BK_NO, val=TBL_BANKMF）</summary>
        public SortedDictionary<int, TBL_BANKMF> mst_banks { get; set; }
        /// <summary>支店マスタ（key=BR_NO, val=TBL_BT_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_BT_BRANCHMF> mst_branches { get; set; }
        /// <summary>支店マスタ[スキャン支店]（key=BR_NO, val=TBL_BT_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_BT_BRANCHMF> mst_scanbranches { get; set; }
        public DataTable tbl_brtotals { get; set; }
        public SortedDictionary<int, ImageInfo> ImageInfos { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>支店別合計票情報</summary>
        public BrTotalInfos BrTotalInfo { get; set; }
        /// <summary>入力支店別合計票情報</summary>
        public InputBrTotalInfos InputBrTotalInfo { get; set; }

        public bool IsBatchUpdate { get; set; }

        private const string FIX_TRIGGER = "持出支店別合計票照会";

        /// <summary>一時テーブル補助項目</summary>
        private string _TMP_UNIQUEITEM = string.Empty;

        // *******************************************************************
        // 一時テーブル
        // *******************************************************************
        ///// <summary>一時テーブル</summary>
        //private const string TMP_BRTOTAL = "TMP_BRTOTAL";

        /// <summary>
        /// 一時テーブル：TMP_BRTOTAL_{IPアドレス}
        /// </summary>
        private string TMP_BRTOTAL
        {
            get { return string.Format("TMP_BRTOTAL_{0}", _TMP_UNIQUEITEM); }
        }

        /// <summary>
		/// コンストラクタ
		/// </summary>
		public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.DispParams = new DisplayParams();
            this.DispParams.Clear();
            this.BrTotalInfo = new BrTotalInfos();
            this.BrTotalInfo.Clear();
            this.InputBrTotalInfo = new InputBrTotalInfos();
            this.InputBrTotalInfo.Clear();
            this.tbl_brtotals = null;

            mst_banks = new SortedDictionary<int, TBL_BANKMF>();
            mst_branches = new SortedDictionary<int, TBL_BT_BRANCHMF>();
            mst_scanbranches = new SortedDictionary<int, TBL_BT_BRANCHMF>();
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

            FetchBankMF();
            Fetch_mst_banks();
            Fetch_mst_branches();
            Fetch_mst_scanbranches();
        }

        /// <summary>
        /// 銀行情報の取得
        /// </summary>
        public void FetchBankMF()
        {
            // SELECT実行
            string strSQL = TBL_BANKMF.GetSelectQuery();
            _bankMF = new List<TBL_BANKMF>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _bankMF.Add(new TBL_BANKMF(tbl.Rows[i]));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_mst_banks()
        {
            mst_banks = new SortedDictionary<int, TBL_BANKMF>();
            string strSQL = TBL_BANKMF.GetSelectQuery();

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BANKMF data = new TBL_BANKMF(tbl.Rows[i]);
                        mst_banks.Add(data._BK_NO, data);
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
        private void Fetch_mst_branches()
        {
            mst_branches = new SortedDictionary<int, TBL_BT_BRANCHMF>();
            string strSQL = TBL_BT_BRANCHMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BT_BRANCHMF data = new TBL_BT_BRANCHMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
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
            mst_scanbranches = new SortedDictionary<int, TBL_BT_BRANCHMF>();
            string strSQL = TBL_BT_BRANCHMF.GetSelectQuery(ServerIni.Setting.ContractBankCd);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BT_BRANCHMF data = new TBL_BT_BRANCHMF(tbl.Rows[i], ServerIni.Setting.ContractBankCd);
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
        /// 銀行情報の取得
        /// </summary>
        public string GetBank(int BankCd, FormBase form = null)
        {
            IEnumerable<TBL_BANKMF> Data = _bankMF.Where(x => x._BK_NO == BankCd);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_BK_NAME_KANJI;
        }

        /// <summary>
        /// 支店情報の取得
        /// </summary>
        public bool GetBranch(int BankCd, int Branch, out string BranchName, FormBase form = null)
        {
            //初期化
            BranchName = string.Empty;

            // SQL文取得
            string strSQL = TBL_BT_BRANCHMF.GetSelectQuery(Branch, BankCd);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        TBL_BT_BRANCHMF BranchMF = new TBL_BT_BRANCHMF(tbl.Rows[0], BankCd);

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
        /// ＤＢからデータ取得してデータセットに格納（バッチデータ）
        /// </summary>
        public bool GetOcBatList(EntryCommonFormBase form)
        {
            // 集計結果を検索条件とするので２回に分けてデータ抽出する
            // 1回目：集計しないで取得可能な条件で絞り込む
            // 2回目：集計条件で絞り込む

            tbl_brtotals = new DataTable();

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
                    strSELECT = SQLSearch.OcBrTotalViewSelectBatList1(GymParam.GymId.持出, NCR.Operator.BankCD, DispParams.ScanDate, DispParams.OcBrCd, AppInfo.Setting.SchemaBankCD);
                    strSQL = SQLSearch.GetInsertTmpTable(strSELECT, TMP_BRTOTAL, TBL_BR_TOTAL.ALL_COLUMNS);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // BT_BRANCHMFに存在しないデータを削除する
                    strSQL = SQLSearch.OcBrTotalViewDeleteBtBranch(TMP_BRTOTAL, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // BT_BRANCHMFデータを取得して一時テーブルに登録する
                    strSELECT = SQLSearch.OcBrTotalViewSelectBatList1BtBranch(TMP_BRTOTAL, GymParam.GymId.持出, NCR.Operator.BankCD, DispParams.ScanDate, DispParams.OcBrCd, AppInfo.Setting.SchemaBankCD);
                    strSQL = SQLSearch.GetInsertTmpTable(strSELECT, TMP_BRTOTAL, TBL_BR_TOTAL.ALL_COLUMNS + ",NOT_TOTAL");
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計1
                    strSQL = SQLSearch.OcBrTotalViewUpdateTmpBatchList1(TMP_BRTOTAL, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計2
                    strSQL = SQLSearch.OcBrTotalViewUpdateTmpBatchList2(TMP_BRTOTAL);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 2回目抽出
                    strSQL = SQLSearch.OcBrTotalViewSelectBatList2(TMP_BRTOTAL, DispParams.TotalUmu, DispParams.SaMaisu, DispParams.SaKingaku, AppConfig.ListDispLimit);
                    tbl_brtotals = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), non.Trans);

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
        public bool GetOcBat(int gymid, int opedate, string imgname, EntryCommonFormBase form)
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
                    strSELECT = TBL_BR_TOTAL.GetSelectQuery(gymid, opedate, imgname);
                    strSQL = SQLSearch.GetInsertTmpTable(strSELECT, TMP_BRTOTAL, TBL_BR_TOTAL.ALL_COLUMNS);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計1
                    strSQL = SQLSearch.OcBrTotalViewUpdateTmpBatchList1(TMP_BRTOTAL, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 抽出結果を集計2
                    strSQL = SQLSearch.OcBrTotalViewUpdateTmpBatchList2(TMP_BRTOTAL);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 集計結果を抽出
                    strSQL = " SELECT * FROM " + TMP_BRTOTAL;
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), non.Trans);
                    if (tbl.Rows.Count > 0)
                    {
                        BrTotalInfo.BrtRow = tbl.Rows[0];
                        BrTotalInfo.brtotal = new TBL_BR_TOTAL(tbl.Rows[0]);
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
            CreateTmpTrBatTable(TMP_BRTOTAL, dbp, non);
        }

        /// <summary>
        /// 一時テーブル削除
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        public void DropTmpTable(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            DropTmpTable(TMP_BRTOTAL, dbp, non);
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
                strSQL = SQLSearch.GetCreateTMP_BRTOTAL(tableName);
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
        public bool CanBatchDelete1(int gymid, int opedate, string imgname)
        {
            // 対象バッチデータの「ステータスが「10:入力完了」かつ未削除」以外の場合は削除不可
            bool retVal = false;
            string strSQL = "";

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    strSQL = SQLSearch.OcBrTotalViewSelectTrBatchQuery(gymid, opedate, imgname);
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
        public bool UpdateTrBatchStatusInput(int gymid, int opedate, string imgname)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // 排他制御
                    strSQL = SQLSearch.OcBrTotalViewSelectTrBatchQuery(gymid, opedate, imgname);
                    strSQL += DBConvert.QUERY_LOCK;
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    if (tbl.Rows.Count < 1)
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        auto.isCommitEnd = false;
                        auto.Trans.Rollback();
                        // メッセージ表示
                        ComMessageMgr.MessageWarning("対象の合計票は訂正中または削除済のため訂正できません。");
                        return false;
                    }

                    // 状態更新
                    TBL_BR_TOTAL newBat = new TBL_BR_TOTAL(tbl.Rows[0]);
                    newBat.m_STATUS = (int)TBL_BR_TOTAL.enumStatus.Processing;
                    newBat.m_LOCK_TERM = AplInfo.TermNo;

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
        public bool UpdateTrBatchStatusComp(TBL_BR_TOTAL brt)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // 状態更新
                    strSQL = SQLSearch.OcBrTotalViewUpdateTrBatchStatusCompQuery(brt);
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
        /// 支店別合計票の全データロック
        /// </summary>
        /// <returns></returns>
        public bool GetLockAllBrTotal(out List<TBL_BR_TOTAL> Totals, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            // 初期化
            Totals = new List<TBL_BR_TOTAL>();

            try
            {
                // ロック取得処理
                string strSQL = TBL_BR_TOTAL.GetSelectQuery(true);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    Totals.Add(new TBL_BR_TOTAL(tbl.Rows[i]));
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 合計票情報を更新する
        /// </summary>
        /// <returns></returns>
        public bool UpdateTrBatch(TBL_BR_TOTAL brt, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // UPDATE実行
            string strSQL = "";
            try
            {
                // BR_TOTAL 更新
                brt.m_BK_NO = InputBrTotalInfo.OcBankCd;
                brt.m_BR_NO = InputBrTotalInfo.OcBrCd;
                brt.m_SCAN_DATE = InputBrTotalInfo.ScanDate;
                brt.m_SCAN_BR_NO = InputBrTotalInfo.ScanBrCd;
                brt.m_TOTAL_COUNT = InputBrTotalInfo.TotalCount;
                brt.m_TOTAL_AMOUNT = InputBrTotalInfo.TotalAmount;
                brt.m_STATUS = (int)TBL_BR_TOTAL.enumStatus.Complete;
                brt.m_LOCK_TERM = "";

                // UPDATE
                strSQL = brt.GetUpdateQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                //ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }

            // 更新フラグ
            this.IsBatchUpdate = true;
            return true;
        }

        /// <summary>
        /// 合計票情報を削除する
        /// </summary>
        /// <returns></returns>
        public bool DeleteTrBatch(TBL_BR_TOTAL brt)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // BR_TOTAL 更新
                    strSQL = SQLSearch.OcBrTotalViewDeleteTrBatchQuery(brt);
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
            public int ScanDate { get; set; }
            public int OcBrCd { get; set; }
            public int TotalUmu { get; set; }
            public int SaMaisu { get; set; }
            public int SaKingaku { get; set; }
            public string Key { get; set; }

            public void Clear()
            {
                this.ScanDate = -1;
                this.OcBrCd = -1;
                this.TotalUmu = -1;
                this.SaMaisu = -1;
                this.SaKingaku = -1;
                this.Key = string.Empty;
            }
        }

        /// <summary>
        /// 支店別合計票情報
        /// </summary>
        public class BrTotalInfos
        {
            public int GymId { get; set; }
            public int OpeDate { get; set; }
            public string ImageNmae { get; set; }
            public DataRow BrtRow { get; set; }
            public TBL_BR_TOTAL brtotal { get; set; }

            public void Clear()
            {
                this.GymId = -1;
                this.OpeDate = -1;
                this.ImageNmae = "";
                this.BrtRow = null;
                this.brtotal = null;
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
        /// 入力支店別合計票情報
        /// </summary>
        public class InputBrTotalInfos
        {
            public int OcBankCd { get; set; }
            public int OcBrCd { get; set; }
            public int ScanBrCd { get; set; }
            public int ScanDate { get; set; }
            public int TotalCount { get; set; }
            public long TotalAmount { get; set; }

            public void Clear()
            {
                this.OcBankCd = -1;
                this.OcBrCd = -1;
                this.ScanBrCd = -1;
                this.ScanDate = -1;
                this.TotalCount = -1;
                this.TotalAmount = -1;
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
