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

namespace ImageImportFutai
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        public List<TBL_BANKMF> _bankMF = null;
        public List<TBL_CHANGE_DSPIDMF> _chgdspidMF = null;

        /// <summary>一覧画面パラメータ</summary>
        public ListDisplayParams ListDispParams { get; set; }

        /// <summary>バッチ票インプット画面パラメータ</summary>
        public BatchInputParams InputParams { get; set; }

        /// <summary>一覧画面表示データ</summary>
        public Dictionary<string, TBL_SCAN_BATCH_CTL> scan_batch { get; private set; }

        /// <summary>バッチ票インプット表示データ</summary>
        public TBL_SCAN_BATCH_CTL InputBatchData { get; private set; }

        /// <summary>明細紐づけ画面パラメータ</summary>
        public ImageAssignParams AssignParams { get; set; }

        /// <summary>スキャン明細データ</summary>
        public Dictionary<string, TBL_SCAN_MEI> scan_mei { get; private set; }

        /// <summary>スキャン明細連番一覧データ</summary>
        public List<int> scanmei_renban { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.ListDispParams = new ListDisplayParams();
            this.InputParams = new BatchInputParams();
            this.scan_batch = new Dictionary<string, TBL_SCAN_BATCH_CTL>();
            this.InputBatchData = new TBL_SCAN_BATCH_CTL();
            this.AssignParams = new ImageAssignParams();
            this.scan_mei = new Dictionary<string, TBL_SCAN_MEI>();
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        public override void FetchAllData()
        {
            FetchBankMF();
            FetchCHGDSPIDMF();
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
        /// 画面番号取得
        /// </summary>
        public void FetchCHGDSPIDMF()
        {
            // SELECT実行
            string strSQL = TBL_CHANGE_DSPIDMF.GetSelectQuery();
            _chgdspidMF = new List<TBL_CHANGE_DSPIDMF>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _chgdspidMF.Add(new TBL_CHANGE_DSPIDMF(tbl.Rows[i]));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// スキャンバッチ管理一覧取得
        /// </summary>
        public bool FetchScanBatchControl(FormBase form = null)
        {
            // SELECT実行
            string strSQL = TBL_SCAN_BATCH_CTL.GetSelectQueryListData(ListDispParams.ScanDate, ListDispParams.Route);
            scan_batch = new Dictionary<string, TBL_SCAN_BATCH_CTL>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_SCAN_BATCH_CTL ctl = new TBL_SCAN_BATCH_CTL(tbl.Rows[i]);

                        string key = ctl._INPUT_ROUTE + "_" + ctl._BATCH_FOLDER_NAME;
                        scan_batch.Add(key, ctl);
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004 , ex.Message)); }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// スキャン明細取得
        /// </summary>
        public bool FetchScanMei(FormBase form = null)
        {
            // SELECT実行
            scan_mei = new Dictionary<string, TBL_SCAN_MEI>();
            scanmei_renban = new List<int>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // スキャン明細データ取得
                    string strSQL = TBL_SCAN_MEI.GetSelectQueryBatchFolder(AssignParams.GymDate, (int)InputParams.TargetRoute, InputParams.TargetBatchFolderName);
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_SCAN_MEI mei = new TBL_SCAN_MEI(tbl.Rows[i]);
                        string key = mei._IMG_NAME + "_" + mei._OPERATION_DATE;
                        scan_mei.Add(key, mei);
                    }

                    // スキャン明細連番一覧データ取得
                    tbl.Dispose();
                    strSQL = TBL_SCAN_MEI.GetSelectQueryRenban(AssignParams.GymDate, (int)InputParams.TargetRoute, InputParams.TargetBatchFolderName);
                    tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        scanmei_renban.Add(DBConvert.ToIntNull(tbl.Rows[i][TBL_SCAN_MEI.BATCH_UCHI_RENBAN]));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 処理待更新SQL実行(一覧画面)
        /// </summary>
        public int UpdateStatusWaitSerrchList(FormBase form = null)
        {
            return UpdateStatusWait(ListDispParams.Route, ListDispParams.BatchFolderName, form);
        }

        /// <summary>
        /// 処理待更新SQL実行(バッチ票入力画面)
        /// </summary>
        public int UpdateStatusWaitInput(FormBase form = null)
        {
            return UpdateStatusWait(InputParams.TargetRoute, InputParams.TargetBatchFolderName, form);
        }

        /// <summary>
        /// 処理待更新SQL実行
        /// </summary>
        private int UpdateStatusWait(TBL_SCAN_BATCH_CTL.InputRoute Route, string BatchFolderName, FormBase form = null)
        {
            int rtnValue = 0;

            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = TBL_SCAN_BATCH_CTL.GetUpdateQueryStatusChg(Route, BatchFolderName, 
                                                                               TBL_SCAN_BATCH_CTL.enumStatus.Processing, TBL_SCAN_BATCH_CTL.enumStatus.Wait, "");
                    rtnValue = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// 削除・削除解除更新SQL実行
        /// </summary>
        /// <param name="UpdType">0:削除、1:削除解除</param>
        public int UpdateStatusDelete(int UpdType, string Lock, FormBase form = null)
        {
            int rtnValue = 0;

            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = string.Empty;
                    if (UpdType == 0)
                    {
                        //削除
                        strSQL = TBL_SCAN_BATCH_CTL.GetUpdateQueryStatusChg(InputParams.TargetRoute, InputParams.TargetBatchFolderName,
                                                                            TBL_SCAN_BATCH_CTL.enumStatus.Processing, TBL_SCAN_BATCH_CTL.enumStatus.Delete, "");
                    }
                    else
                    {
                        //削除解除
                        strSQL = TBL_SCAN_BATCH_CTL.GetUpdateQueryStatusChg(InputParams.TargetRoute, InputParams.TargetBatchFolderName,
                                                                            TBL_SCAN_BATCH_CTL.enumStatus.Delete, TBL_SCAN_BATCH_CTL.enumStatus.Processing, Lock);
                    }

                    rtnValue = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// 保留更新SQL実行
        /// </summary>
        public int UpdateStatusHold(FormBase form = null)
        {
            int rtnValue = 0;

            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = InputBatchData.GetUpdateQueryHold(TBL_SCAN_BATCH_CTL.enumStatus.Processing);
                    rtnValue = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// バッチ票入力画面確定処理更新SQL実行
        /// </summary>
        public int UpdateInputComplete(FormBase form = null)
        {
            int rtnValue = 0;

            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = InputBatchData.GetUpdateQueryInputComplete(TBL_SCAN_BATCH_CTL.enumStatus.Processing);
                    rtnValue = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                }
            }
            return rtnValue;
        }

        /// <summary>
        /// 処理済更新SQL実行
        /// </summary>
        public int UpdateStatusComplete(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            int rtnValue = 0;

            try
            {
                string strSQL = TBL_SCAN_BATCH_CTL.GetUpdateQueryStatusChg(InputParams.TargetRoute, InputParams.TargetBatchFolderName,
                                                           TBL_SCAN_BATCH_CTL.enumStatus.Processing, TBL_SCAN_BATCH_CTL.enumStatus.Complete, "");
                rtnValue = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
            }
            return rtnValue;
        }

        /// <summary>
        /// 処理対象のバッチ票を取得
        /// InputModeにより処理を変更
        /// </summary>
        public bool GetTargetBatchControl(string TermNum, FormBase form = null)
        {
            // 初期化
            this.InputParams.Init(this.InputParams.Mode);
            InputBatchData = new TBL_SCAN_BATCH_CTL();

            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    string Procedure = "DBCTR.SCAN_BATCH_CTL_EXTRACTION";

                    //パラメータ設定
                    Dictionary<string, IDbDataParameter> param = new Dictionary<string, IDbDataParameter>();
                    if (InputParams.Mode == BatchInputParams.InputMode.Auto)
                    {
                        /* 自動取得 */
                        param.Add("i_MODE", dbp.CreateParameter("i_MODE", DbType.Int32, ParameterDirection.Input, int.Parse(InputParams.Mode.ToString("d"))));
                        param.Add("i_INPUT_ROUTE", dbp.CreateParameter("i_INPUT_ROUTE", DbType.Int32, ParameterDirection.Input, int.Parse(ListDispParams.Route.ToString("d"))));
                        param.Add("i_SCAN_DATE", dbp.CreateParameter("i_SCAN_DATE", DbType.Int32, ParameterDirection.Input, ListDispParams.ScanDate));
                        param.Add("i_LOCK", dbp.CreateParameter("i_LOCK", DbType.String, ParameterDirection.Input, TermNum));
                        if (ListDispParams.BatchSelected)
                        {
                            // 初回実行時選択行がある場合
                            // バッチフォルダを設定(指定のバッチフォルダ以降のバッチを取得)
                            param.Add("i_BATCH_FOLDER_NAME", dbp.CreateParameter("i_BATCH_FOLDER_NAME", DbType.String, ParameterDirection.Input, ListDispParams.AutoSelectBatchFolder));
                        }
                        else
                        {
                            // 初回実行時選択行がない場合
                            // NULLを設定(先頭からバッチを取得)
                            param.Add("i_BATCH_FOLDER_NAME", dbp.CreateParameter("i_BATCH_FOLDER_NAME", DbType.String, ParameterDirection.Input, DBNull.Value));
                        }
                        param.Add("o_INPUT_ROUTE", dbp.CreateParameter("o_INPUT_ROUTE", DbType.Int32, ParameterDirection.Output, 0, 1));
                        param.Add("o_BATCH_FOLDER_NAME", dbp.CreateParameter("o_BATCH_FOLDER_NAME", DbType.String, ParameterDirection.Output, "", 20));
                        param.Add("o_RESULT", dbp.CreateParameter("o_RESULT", DbType.Int32, ParameterDirection.Output, 0, 1));
                        param.Add("o_ERRCODE", dbp.CreateParameter("o_ERRCODE", DbType.Int32, ParameterDirection.Output, 0, 5));
                        param.Add("o_ERRMSG", dbp.CreateParameter("o_ERRMSG", DbType.String, ParameterDirection.Output, "", 2048));
                    }
                    else
                    {
                        /* 選択取得 */
                        param.Add("i_MODE", dbp.CreateParameter("i_MODE", DbType.Int32, ParameterDirection.Input, int.Parse(InputParams.Mode.ToString("d"))));
                        param.Add("i_INPUT_ROUTE", dbp.CreateParameter("i_INPUT_ROUTE", DbType.Int32, ParameterDirection.Input, int.Parse(ListDispParams.Route.ToString("d"))));
                        param.Add("i_SCAN_DATE", dbp.CreateParameter("i_SCAN_DATE", DbType.Int32, ParameterDirection.Input));
                        param.Add("i_LOCK", dbp.CreateParameter("i_LOCK", DbType.String, ParameterDirection.Input, TermNum));
                        param.Add("i_BATCH_FOLDER_NAME", dbp.CreateParameter("i_BATCH_FOLDER_NAME", DbType.String, ParameterDirection.Input, ListDispParams.BatchFolderName));
                        param.Add("o_INPUT_ROUTE", dbp.CreateParameter("o_INPUT_ROUTE", DbType.Int32, ParameterDirection.Output, 0, 1));
                        param.Add("o_BATCH_FOLDER_NAME", dbp.CreateParameter("o_BATCH_FOLDER_NAME", DbType.String, ParameterDirection.Output, "", 20));
                        param.Add("o_RESULT", dbp.CreateParameter("o_RESULT", DbType.Int32, ParameterDirection.Output, 0, 1));
                        param.Add("o_ERRCODE", dbp.CreateParameter("o_ERRCODE", DbType.Int32, ParameterDirection.Output, 0, 5));
                        param.Add("o_ERRMSG", dbp.CreateParameter("o_ERRMSG", DbType.String, ParameterDirection.Output, "", 2048));
                    }

                    // 実行
                    dbp.Procedure(Procedure, param.Values.ToList());

                    // 結果取得
                    int Result = int.Parse(param["o_RESULT"].Value.ToString());
                    if (Result == 1)
                    {
                        // データなしの場合
                        InputParams.DispMode = BatchInputParams.DispErrMode.BatchDataNoData;
                        return false;
                    }
                    else if (Result == 2)
                    {
                        // 実行エラーの場合
                        throw new Exception(param["o_ERRMSG"].Value.ToString());
                    }

                    // 処理対象データの取得
                    InputParams.TargetRoute = (TBL_SCAN_BATCH_CTL.InputRoute)int.Parse(param["o_INPUT_ROUTE"].Value.ToString());
                    InputParams.TargetBatchFolderName = param["o_BATCH_FOLDER_NAME"].Value.ToString();

                    // 取得データのコピー(一覧画面に戻った際の復元のため)
                    ListDispParams.Route = InputParams.TargetRoute;
                    ListDispParams.BatchFolderName = InputParams.TargetBatchFolderName;

                    return GetTargetBatchData(form);
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    InputParams.DispMode = BatchInputParams.DispErrMode.BatchDataGetErr;
                    return false;
                }
            }
        }

        /// <summary>
        /// 対象バッチ票データ取得
        /// </summary>
        public bool GetTargetBatchData(FormBase form = null)
        {
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // 処理対象データの取得
                    string strSQL = TBL_SCAN_BATCH_CTL.GetSelectQuery(InputParams.TargetRoute, InputParams.TargetBatchFolderName);
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    InputBatchData = new TBL_SCAN_BATCH_CTL(tbl.Rows[0]);
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    InputParams.DispMode = BatchInputParams.DispErrMode.BatchDataGetErr;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 対象バッチ票のステータス確認
        /// </summary>
        public bool ChkBatchDataStatus(TBL_SCAN_BATCH_CTL.enumStatus Status, FormBase form = null)
        {
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // 処理対象データの取得
                    string strSQL = TBL_SCAN_BATCH_CTL.GetSelectQueryStatus(InputParams.TargetRoute, InputParams.TargetBatchFolderName, Status);
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());

                    return tbl.Rows.Count > 0;
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
        }

        /// <summary>
        /// バッチ番号の取得
        /// </summary>
        /// <returns></returns>
        public bool GetBatchNumber(int operation_date, int Schemabankcd, out int BatchNumber, FormBase form = null)
        {
            // 初期化
            BatchNumber = 0;

            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    int LAST_SEQ = 0;

                    // 現在のデータ取得処理
                    string strSQL = TBL_BATCH_SEQ.GetSelectQuery(AppInfo.Setting.GymId, operation_date, Schemabankcd, true);
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                    if (tbl.Rows.Count == 0)
                    {
                        strSQL = TBL_BATCH_SEQ.GetInsertQuery(AppInfo.Setting.GymId, operation_date, LAST_SEQ, Schemabankcd);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                    }
                    else
                    {
                        LAST_SEQ = new TBL_BATCH_SEQ(tbl.Rows[0], Schemabankcd).m_LAST_SEQ;
                    }

                    // 更新処理
                    strSQL = TBL_BATCH_SEQ.GetUpdateQuery(AppInfo.Setting.GymId, operation_date, Schemabankcd);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                    BatchNumber = LAST_SEQ + 1;

                    // コミット
                    Tran.Trans.Commit();
                }
                catch (Exception ex)
                {
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 取扱端末ロックのロック取得
        /// </summary>
        /// <returns></returns>
        public bool GetLockTerm(int LastIPAddress, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // 行ロック取得処理
                string strSQL = TBL_TERM_LOCK.GetSelectQuery(LastIPAddress, true);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                if (tbl.Rows.Count == 0)
                {
                    DBManager.InitializeSql1(NCR.Server.DBDataSource, NCR.Server.DBUserID, NCR.Server.DBPassword);
                    DBManager.dbs1.Open();
                    
                    // 無ければ登録してもう一度実施
                    using (AdoDatabaseProvider dbs = GenDbProviderFactory.CreateAdoProvider2())
                    using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbs))
                    {
                        strSQL = TBL_TERM_LOCK.GetInsertQuery(LastIPAddress);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }
                    strSQL = TBL_TERM_LOCK.GetSelectQuery(LastIPAddress, true);
                    tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
            finally
            {
                if (DBManager.dbs1 != null)
                {
                    DBManager.dbs1.Close();
                    DBManager.dbs1 = null;
                }
            }
            return true;
        }


        /// <summary>
        /// スキャン明細の登録処理
        /// </summary>
        /// <returns></returns>
        public bool InsertScan_MeiData(TBL_SCAN_MEI InsScanmeiData,
                                       AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // スキャン明細データ登録処理
                string strSQL = InsScanmeiData.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
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
        /// スキャン明細の削除処理
        /// </summary>
        /// <returns></returns>
        public bool DeleteScan_MeiData(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // スキャン明細データ削除処理
                string strSQL = TBL_SCAN_MEI.GetDeleteQueryRenban(AssignParams.GymDate, (int)InputParams.TargetRoute, 
                                                                  InputParams.TargetBatchFolderName, AssignParams.CurrentDetail);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
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
        /// スキャン明細の更新処理
        /// 指定連番以降の連番を更新
        /// </summary>
        /// <returns></returns>
        public bool UpdateScan_MeiData(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // スキャン明細データ更新処理
                string strSQL = TBL_SCAN_MEI.GetUpdateQueryRenban(AssignParams.GymDate, (int)InputParams.TargetRoute,
                                                                  InputParams.TargetBatchFolderName, AssignParams.CurrentDetail);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
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
        /// バッチ票OCRデータ取得
        /// </summary>
        public string GetBatchOCRData(string field_name, FormBase form = null)
        {
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    //検索条件
                    int opeDate = InputBatchData.m_SCAN_DATE;
                    if (InputBatchData.m_SCAN_DATE <= 0)
                    {
                        //「スキャンバッチ管理」テーブルのスキャン日
                        // スキャン日が未入力の場合はバッチ一覧で入力したスキャン日
                        opeDate = ListDispParams.ScanDate; 
                    }

                    // 対象データの取得
                    string strSQL = TBL_OCR_DATA.GetSelectQuery(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Futai, 
                                                                opeDate, InputParams.TargetBatchFolderName, InputParams.BatchImage.Front, field_name);
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0)
                    {
                        return string.Empty;
                    }
                    TBL_OCR_DATA ocrData = new TBL_OCR_DATA(tbl.Rows[0]);
                    TBL_DSP_ITEM dsp = new TBL_DSP_ITEM(0);
                    dsp.m_ITEM_TYPE = DspItem.ItemType.N;

                    return CommonUtil.GetOcrValue(dsp, ocrData.m_OCR);
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 証券OCRデータ取得
        /// </summary>
        public List<TBL_OCR_DATA> GetMeiOCRData(string file_name, int scandate, string batchfolderName, AdoDatabaseProvider dbp)
        {
            List<TBL_OCR_DATA> ocrlist = new List<TBL_OCR_DATA>();

            try
            {
                // 対象データの取得
                string strSQL = TBL_OCR_DATA.GetSelectQueryDirImgName(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Futai, scandate, batchfolderName, file_name);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    ocrlist.Add(new TBL_OCR_DATA(tbl.Rows[i]));
                }
                return ocrlist;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return new List<TBL_OCR_DATA>();
            }
        }

        /// <summary>
        /// 対象のフォルダパスを取得
        /// </summary>
        public string TargetFolderPath()
        {
            return System.IO.Path.Combine(NCR.Server.ScanFutaiImageRoot, InputParams.TargetBatchFolderName);
        }

        /// <summary>
        /// 一覧画面パラメーター
        /// </summary>
        public class ListDisplayParams
        {
            public int ScanDate { get; set; } = 0;
            public TBL_SCAN_BATCH_CTL.InputRoute Route { get; set; } = TBL_SCAN_BATCH_CTL.InputRoute.Futai;
            public string BatchFolderName { get; set; } = "";
            public string Status { get; set; } = "";
            public bool BatchSelected { get; set; } = false;
            public string AutoSelectBatchFolder { get; set; } = "";

            public void SelectDataInit()
            {
                BatchFolderName = "";
                Status = "";
                BatchSelected = false;
                AutoSelectBatchFolder = "";
            }
        }

        /// <summary>
        /// バッチインプット画面パラメーター
        /// </summary>
        public class BatchInputParams
        {

            #region 取得モード関連定義

            public enum InputMode
            {
                Auto = 1,
                Select = 2,
            }

            #endregion

            #region パターン関連定義

            public class ImageData
            {
                public string Front = string.Empty;
                public string Back = string.Empty;


                public bool ChkFrontInput()
                {
                    return !string.IsNullOrWhiteSpace(Front);
                }
                public bool ChkBackInput()
                {
                    return !string.IsNullOrWhiteSpace(Back);
                }
                public bool ChkFBInput()
                {
                    return ChkFrontInput() && ChkBackInput();
                }
            }
            #endregion

            #region 表示エラー関連定義

            public enum DispErrMode
            {
                None = 0,
                BatchDataNoData = 1,
                BatchDataGetErr = 2,
                FolderNotExists = 3,
                PatternNoData = 4,
                BatchImageNoData = 5,
            }

            #endregion

            public InputMode Mode { get; set; } = InputMode.Auto;

            public TBL_SCAN_BATCH_CTL.InputRoute TargetRoute { get; set; } = TBL_SCAN_BATCH_CTL.InputRoute.Futai;
            public string TargetBatchFolderName { get; set; } = string.Empty;

            public DispErrMode DispMode { get; set; } = DispErrMode.None;

            public ImageData BatchImage { get; set; } =  new ImageData();

            public void Init(InputMode _Mode)
            {
                Mode = Mode;
                TargetRoute = TBL_SCAN_BATCH_CTL.InputRoute.Futai;
                TargetBatchFolderName = string.Empty;
                DispMode = DispErrMode.None;
                BatchImage = new ImageData();
            }
        }

        /// <summary>
        /// 明細紐づけ画面パラメーター
        /// </summary>
        public class ImageAssignParams
        {
            public int GymDate { get; set; } = 0;
            public int CurrentDetail { get; set; } = 0;

        }

    }
}
