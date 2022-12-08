using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;

namespace ImageImportTotal
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        public List<TBL_BANKMF> _bankMF = null;

        /// <summary>一覧画面パラメータ</summary>
        public ListDisplayParams ListDispParams { get; set; }

        /// <summary>合計票インプット画面パラメータ</summary>
        public TotalInputParams InputParams { get; set; }

        /// <summary>一覧画面表示データ</summary>
        public Dictionary<string, TBL_BR_TOTAL> scan_total { get; private set; }

        /// <summary>合計票インプット表示データ</summary>
        public TBL_BR_TOTAL InputTotalData { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.ListDispParams = new ListDisplayParams();
            this.InputParams = new TotalInputParams();
            this.scan_total = new Dictionary<string, TBL_BR_TOTAL>();
            this.InputTotalData = new TBL_BR_TOTAL();
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        public override void FetchAllData()
        {
            FetchBankMF();
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
        /// 合計票一覧取得
        /// </summary>
        public bool FetchTotalControl(FormBase form = null)
        {
            // SELECT実行
            string strSQL = TBL_BR_TOTAL.GetSelectQueryListData(AppInfo.Setting.GymId, ListDispParams.ScanDate);
            scan_total = new Dictionary<string, TBL_BR_TOTAL>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BR_TOTAL ctl = new TBL_BR_TOTAL(tbl.Rows[i]);

                        string key = CommonUtil.GenerateKey("|", ctl._OPERATION_DATE, ctl._SCAN_IMG_FLNM);
                        scan_total.Add(key, ctl);
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
            return UpdateStatusWait(AppInfo.Setting.GymId, ListDispParams.OperationDate, ListDispParams.FileName, form);
        }

        /// <summary>
        /// 処理待更新SQL実行(合計票入力画面)
        /// </summary>
        public int UpdateStatusWaitInput(FormBase form = null)
        {
            return UpdateStatusWait(AppInfo.Setting.GymId, InputParams.OperationDate, InputParams.TargetFileName, form);
        }

        /// <summary>
        /// 処理待更新SQL実行
        /// </summary>
        private int UpdateStatusWait(int GymId, int operationdate, string filename, FormBase form = null)
        {
            int rtnValue = 0;

            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = TBL_BR_TOTAL.GetUpdateQueryStatusChg(GymId, operationdate, filename,
                                                                         TBL_BR_TOTAL.enumStatus.Processing, TBL_BR_TOTAL.enumStatus.Wait, "");
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
                        strSQL = TBL_BR_TOTAL.GetUpdateQueryStatusChg(AppInfo.Setting.GymId, InputParams.OperationDate, InputParams.TargetFileName,
                                                                      TBL_BR_TOTAL.enumStatus.Processing, TBL_BR_TOTAL.enumStatus.Delete, "");
                    }
                    else
                    {
                        //削除解除
                        strSQL = TBL_BR_TOTAL.GetUpdateQueryStatusChg(AppInfo.Setting.GymId, InputParams.OperationDate, InputParams.TargetFileName,
                                                                      TBL_BR_TOTAL.enumStatus.Delete, TBL_BR_TOTAL.enumStatus.Processing, Lock);
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
                    string strSQL = InputTotalData.GetUpdateQueryStatus(TBL_BR_TOTAL.enumStatus.Processing);
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
                string strSQL = InputTotalData.GetUpdateQuery();
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
        /// 処理対象の合計票を取得
        /// InputModeにより処理を変更
        /// </summary>
        public bool GetTargetTotalControl(string TermNum, FormBase form = null)
        {
            // 取得処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    if (this.InputParams.Mode == TotalInputParams.InputMode.Auto)
                    {
                        // 自動取得
                        for (int i = 0; i <= 10; i++)
                        {
                            if (GetTargetTotalControlAuto(dbp, auto, TermNum))
                            {
                                // 対象データが取得できた場合
                                // 処理対象データの取得
                                InputParams.TargetFileName = InputTotalData._SCAN_IMG_FLNM;
                                InputParams.OperationDate = InputTotalData._OPERATION_DATE;

                                // 取得データのコピー(一覧画面に戻った際の復元のため)
                                ListDispParams.FileName = InputParams.TargetFileName;
                                ListDispParams.OperationDate = InputParams.OperationDate;

                                return true;
                            }
                            if (InputParams.DispMode == TotalInputParams.DispErrMode.TotalDataNoData)
                            {
                                // 取得データがなしの場合
                                return false;
                            }
                        }

                        // 所定回数実行して取得不可の場合
                        throw new Exception("合計票の取得エラー");
                    }
                    else
                    {
                        // 選択取得
                        if (GetTargetTotalControlSelect(dbp, auto, TermNum))
                        {
                            // 対象データが取得できた場合
                            // 処理対象データの取得
                            InputParams.TargetFileName = InputTotalData._SCAN_IMG_FLNM;
                            InputParams.OperationDate = InputTotalData._OPERATION_DATE;

                            // 取得データのコピー(一覧画面に戻った際の復元のため)
                            ListDispParams.FileName = InputParams.TargetFileName;
                            ListDispParams.OperationDate = InputParams.OperationDate;

                            return true;
                        }
                        if (InputParams.DispMode == TotalInputParams.DispErrMode.TotalDataNoData)
                        {
                            // 取得データがなしの場合
                            return false;
                        }

                        // 実行して取得エラーの場合
                        throw new Exception("合計票の取得エラー");
                    }
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    InputParams.DispMode = TotalInputParams.DispErrMode.TotalDataGetErr;
                    return false;
                }
            }
        }

        /// <summary>
        /// 対象合計票データ取得
        /// </summary>
        public bool GetTargetTotalData(FormBase form = null)
        {
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // 処理対象データの取得
                    string strSQL = TBL_BR_TOTAL.GetSelectQuery(AppInfo.Setting.GymId, InputParams.OperationDate, InputParams.TargetFileName);
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    InputTotalData = new TBL_BR_TOTAL(tbl.Rows[0]);
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    InputParams.DispMode = TotalInputParams.DispErrMode.TotalDataGetErr;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 処理対象の合計票を取得（自動取得）
        /// </summary>
        public bool GetTargetTotalControlAuto(AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto, string TermNum)
        {
            // 初期化
            this.InputParams.Init(this.InputParams.Mode);
            InputTotalData = new TBL_BR_TOTAL();

            // 取得処理
            try
            {
                string imgname = string.Empty;
                int operationdate = 0;
                string strSQL = string.Empty;

                //対象データの取得
                if (ListDispParams.FileSelected)
                {
                    // 初回実行時選択行がある場合
                    // OperationDateとFileNameの組み合わせ以上のデータを対象に抽出
                    strSQL = TBL_BR_TOTAL.GetSelectQueryScanDateStatus(AppInfo.Setting.GymId, ListDispParams.ScanDate,
                                                                       ListDispParams.AutoSelectOpeDate, ListDispParams.AutoSelectFileName, TBL_BR_TOTAL.enumStatus.Wait);
                }
                else
                {
                    // 初回実行時選択行がない場合
                    // ScanDateで対象データを抽出(先頭から取得)
                    strSQL = TBL_BR_TOTAL.GetSelectQueryScanDateStatus(AppInfo.Setting.GymId, ListDispParams.ScanDate, TBL_BR_TOTAL.enumStatus.Wait);
                }
                using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                {
                    if (tbl.Rows.Count == 0)
                    {
                        // データなしの場合
                        InputParams.DispMode = TotalInputParams.DispErrMode.TotalDataNoData;
                        return false;
                    }
                    TBL_BR_TOTAL wk = new TBL_BR_TOTAL(tbl.Rows[0]);

                    imgname = wk._SCAN_IMG_FLNM;
                    operationdate = wk._OPERATION_DATE;
                }

                // 対象データの行ロック取得
                strSQL = TBL_BR_TOTAL.GetSelectQueryStatus(AppInfo.Setting.GymId, operationdate, imgname,
                                                            TBL_BR_TOTAL.enumStatus.Wait, true);
                using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                {
                    if (tbl.Rows.Count == 0)
                    {
                        // 行ロックでデータなしの場合
                        return false;
                    }
                    // 取得データの設定
                    InputTotalData = new TBL_BR_TOTAL(tbl.Rows[0]);
                }

                // ステータスの更新
                strSQL = TBL_BR_TOTAL.GetUpdateQueryStatusChg(AppInfo.Setting.GymId, operationdate, imgname,
                                                              TBL_BR_TOTAL.enumStatus.Processing, TermNum);
                if (dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans) == 0)
                {
                    // 更新データなしの場合
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        /// <summary>
        /// 処理対象の合計票を取得（選択取得）
        /// </summary>
        public bool GetTargetTotalControlSelect(AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto, string TermNum)
        {
            // 初期化
            this.InputParams.Init(this.InputParams.Mode);
            InputTotalData = new TBL_BR_TOTAL();

            // 取得処理
            try
            {
                // 対象データの行ロック取得
                string strSQL = TBL_BR_TOTAL.GetSelectQueryStatus(AppInfo.Setting.GymId, ListDispParams.OperationDate, ListDispParams.FileName,
                                                                  new List<TBL_BR_TOTAL.enumStatus>() { TBL_BR_TOTAL.enumStatus.Wait, TBL_BR_TOTAL.enumStatus.Hold, TBL_BR_TOTAL.enumStatus.Delete },
                                                                  true);
                using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                {
                    if (tbl.Rows.Count == 0)
                    {
                        // 行ロックでデータなしの場合
                        InputParams.DispMode = TotalInputParams.DispErrMode.TotalDataNoData;
                        return false;
                    }
                    // 取得データの設定
                    InputTotalData = new TBL_BR_TOTAL(tbl.Rows[0]);
                }

                if (InputTotalData.m_STATUS == (int)TBL_BR_TOTAL.enumStatus.Wait ||
                    InputTotalData.m_STATUS == (int)TBL_BR_TOTAL.enumStatus.Hold)
                {
                    // 処理待ち・保留の場合、ステータスの更新
                    strSQL = TBL_BR_TOTAL.GetUpdateQueryStatusChg(AppInfo.Setting.GymId, InputTotalData._OPERATION_DATE, InputTotalData._SCAN_IMG_FLNM,
                                                                  TBL_BR_TOTAL.enumStatus.Processing, TermNum);
                    if (dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans) == 0)
                    {
                        // 更新データなしの場合
                        InputParams.DispMode = TotalInputParams.DispErrMode.TotalDataNoData;
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        /// <summary>
        /// 対象合計票のステータス確認
        /// </summary>
        public bool ChkTotalDataStatus(TBL_BR_TOTAL.enumStatus Status, FormBase form = null)
        {
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // 処理対象データの取得
                    string strSQL = TBL_BR_TOTAL.GetSelectQueryStatus(AppInfo.Setting.GymId, InputParams.OperationDate, InputParams.TargetFileName, Status);
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
        /// OCRデータ取得
        /// </summary>
        public string GetOCRData(string field_name, FormBase form = null)
        {
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    //検索条件
                    int opeDate = InputTotalData.m_SCAN_DATE;
                    if (InputTotalData.m_SCAN_DATE <= 0)
                    {
                        //「支店別合計票」テーブルのスキャン日
                        // スキャン日が未入力の場合は合計票一覧で入力したスキャン日
                        opeDate = ListDispParams.ScanDate; 
                    }

                    // 対象データの取得
                    string strSQL = TBL_OCR_DATA.GetSelectQueryImgName(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Other, opeDate, InputParams.TargetFileName, field_name);
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
        /// 対象のフォルダパスを取得
        /// </summary>
        public string TargetFolderPath()
        {
            return NCR.Server.ScanTotalImageRoot;
        }

        /// <summary>
        /// 一覧画面パラメーター
        /// </summary>
        public class ListDisplayParams
        {
            public int ScanDate { get; set; } = 0;
            public int OperationDate { get; set; } = 0;
            public string FileName { get; set; } = "";
            public string Status { get; set; } = "";
            public bool FileSelected { get; set; } = false;
            public int AutoSelectOpeDate { get; set; } = 0;
            public string AutoSelectFileName { get; set; } = "";

            public void SelectDataInit()
            {
                OperationDate = 0;
                FileName = "";
                Status = "";
                FileSelected = false;
                AutoSelectOpeDate = 0;
                AutoSelectFileName = "";
            }
        }

        /// <summary>
        /// 合計票インプット画面パラメーター
        /// </summary>
        public class TotalInputParams
        {

            #region 取得モード関連定義

            public enum InputMode
            {
                Auto = 1,
                Select = 2,
            }

            #endregion

            #region 表示エラー関連定義

            public enum DispErrMode
            {
                None = 0,
                TotalDataNoData = 1,
                TotalDataGetErr = 2,
                TotalImageNoData = 5,
            }

            #endregion

            public InputMode Mode { get; set; } = InputMode.Auto;

            public int OperationDate { get; set; } = 0;

            public string TargetFileName { get; set; } = string.Empty;

            public DispErrMode DispMode { get; set; } = DispErrMode.None;

            public void Init(InputMode _Mode)
            {
                Mode = Mode;
                OperationDate = 0;
                TargetFileName = string.Empty;
                DispMode = DispErrMode.None;
            }
        }
    }
}
