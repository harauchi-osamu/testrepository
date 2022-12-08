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

namespace ImageKijituImport
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        public List<TBL_BANKMF> _bankMF = null;
        public List<TBL_CHANGE_DSPIDMF> _chgdspidMF = null;

        private const string APPID = "CTRImgCapInventory";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
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
        /// 対象データが登録済か確認
        /// </summary>
        /// <returns></returns>
        public bool ChkImportDetail(int BankCd, string folderName, string fileName,
                                    out string ImportKey, out bool DeleteFlg, out bool UploadFlg, 
                                    FormBase form = null)
        {
            // 初期化
            ImportKey = string.Empty;
            DeleteFlg = false;
            UploadFlg = false;

            // SQL文取得
            string strSQL = SQLImageImport.Get_KijituImportChk(AppInfo.Setting.GymId, AplInfo.OpDate(), folderName, fileName, BankCd);
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        TBL_TRMEI mei = new TBL_TRMEI(tbl.Rows[0], BankCd);

                        // 登録済キー設定(GYM_ID|OPERATION_DATE|SCAN_TERM|BAT_ID|DETAILS_NO)
                        ImportKey = CommonUtil.GenerateKey("|", mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO);
                        // 登録済削除フラグ設定
                        DeleteFlg = (mei.m_DELETE_FLG != 0);
                        // アップロードフラグ設定(表面のアップロード状態が結果正常の場合True)
                        UploadFlg = (DBConvert.ToIntNull(tbl.Rows[0][TBL_TRMEIIMG.BUA_STS]) == TrMei.Sts.結果正常);

                        if (DeleteFlg)
                        {
                            if (UploadFlg)
                            {
                                // 削除でアップロード済の場合は登録済
                                return true;
                            }
                            else
                            {
                                // 削除で未アップロードの場合は未登録扱い
                                return false;
                            }
                        }
                        else
                        {
                            // 未削除の場合は登録済
                            return true;
                        }
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
        /// 登録済のバッチ番号を取得
        /// </summary>
        /// <returns></returns>
        public bool GetImportBatchNumber(int operation_date, string folderName, int Schemabankcd, out string ScanTerm, out int BatchNumber, out int DetailNo, FormBase form = null)
        {
            // 初期化
            ScanTerm = string.Empty;
            BatchNumber = 0;
            DetailNo = 1;

            // SQL文取得
            string strSQL = SQLImageImport.Get_KijituImportData(AppInfo.Setting.GymId, operation_date, folderName, Schemabankcd);
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        ScanTerm = DBConvert.ToStringNull(tbl.Rows[0][TBL_TRBATCH.SCAN_TERM]);
                        BatchNumber = DBConvert.ToIntNull(tbl.Rows[0][TBL_TRBATCH.BAT_ID]);
                        DetailNo = DBConvert.ToIntNull(tbl.Rows[0][TBL_TRMEI.DETAILS_NO]) + 1;
                        return true;
                    }
                    return false;
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
        /// 期日管理ロックの取得
        /// </summary>
        /// <returns></returns>
        public bool GetLockProcessSystem(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // 行ロック取得処理
                string strSQL = TBL_LOCK_SYSTEM_PROCESS.GetSelectQuery(AppInfo.Setting.GymId, APPID, true);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                if (tbl.Rows.Count == 0)
                {
                    DBManager.InitializeSql1(NCR.Server.DBDataSource, NCR.Server.DBUserID, NCR.Server.DBPassword);
                    DBManager.dbs1.Open();

                    // 無ければ登録してもう一度実施
                    using (AdoDatabaseProvider dbs = GenDbProviderFactory.CreateAdoProvider2())
                    using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbs))
                    {
                        // 無ければ登録してもう一度実施
                        strSQL = TBL_LOCK_SYSTEM_PROCESS.GetInsertQuery(AppInfo.Setting.GymId, APPID);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                    }
                    // 再ロック
                    strSQL = TBL_LOCK_SYSTEM_PROCESS.GetSelectQuery(AppInfo.Setting.GymId, APPID, true);
                    tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                }

                return true;
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
        }

        /// <summary>
        /// 対象のフォルダパスを取得
        /// </summary>
        public string TargetFolderPath()
        {
            return NCR.Server.ScanInventoryImageRoot;
        }

        /// <summary>
        /// 取込対象データ
        /// </summary>
        public class ImportData
        {
            //フォルダフルパス
            public string FolderName { get; set; } = "";

            //対象フォルダ
            public string BatchFolderName { get; set; } = "";

            //イメージ情報
            public List<ImportImgInfo> Detail { get; set; } = new List<ImportImgInfo>();
        }

        /// <summary>
        /// 取込対象イメージ情報
        /// </summary>
        public class ImportImgInfo
        {
            //表イメージ
            public string FrontFileName { get; set; } = "";
            //メモ
            public string Memo { get; set; } = "";
            //登録済データキー情報(GYM_ID|OPERATION_DATE|SCAN_TERM|BAT_ID|DETAILS_NO)
            public string ImportKey { get; set; } = "";
            //登録済データ削除情報
            public bool DeleteFlg { get; set; } = false;
            //登録済データアップロード情報
            public bool UploadFlg { get; set; } = false;

            //imgocr
            public List<imgocr> Ocr { get; set; } = new List<imgocr>();

            public ImportImgInfo(string frontname, string memo, string importkey, bool deleteflg, bool uploadflg, List<imgocr> ocr)
            {
                FrontFileName = frontname;
                Memo = memo;
                ImportKey = importkey;
                DeleteFlg = deleteflg;
                UploadFlg = uploadflg;
                Ocr = ocr;
            }
        }

        /// <summary>
        /// 取込結果データ
        /// </summary>
        public class ImportResult
        {
            //バッチ票成功数
            public int BatchImportSuccess { get; set; } = 0;
            //バッチ票失敗数
            public int BatchImportFail { get; set; } = 0;

            //明細取込成功数
            public int DetailImportSuccess { get; set; } = 0;

            //明細取込成功金額
            public long DetailImportTotalAmt { get; set; } = 0;
        }

    }
}
