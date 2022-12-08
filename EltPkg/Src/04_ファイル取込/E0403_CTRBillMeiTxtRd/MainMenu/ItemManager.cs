using System;
using System.IO;
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

namespace MainMenu
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        private FileCtkKey _filectkkey = null;
        public List<TBL_CHANGE_DSPIDMF> _chgdspidMF = null;

        public int _BankCd = -1;
        public string _TargetFilename = string.Empty;
        public string _file_id = string.Empty;
        public string _file_divid = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst, int bankcd, string filename)
        {
            _masterMgr = mst;
            _BankCd = bankcd;
            _TargetFilename = filename;
            GetFileDivid(out _file_id, out _file_divid);
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        public override void FetchAllData()
        {
            FetchCHGDSPIDMF();
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
        /// ファイルコントロールキー情報設定
        /// </summary>
        public void SetFileCtlKey(string fileid, string filedivid, string sendfilename, string capfilename)
        {
            _filectkkey = new FileCtkKey(fileid, filedivid, sendfilename, capfilename);
        }

        /// <summary>
        /// ファイルコントロール登録
        /// </summary>
        public bool FileCtlInsert()
        {
            // Delete/Insert実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = string.Empty;
                    // 登録データ作成
                    TBL_FILE_CTL FileCtl = new TBL_FILE_CTL(_filectkkey.FileID, _filectkkey.FileDivID, _filectkkey.SendFileName, _filectkkey.CapFileName, AppInfo.Setting.SchemaBankCD);
                    FileCtl.m_CAP_FILE_LENGTH = GetFileSize();
                    FileCtl.m_CAP_STS = CommonTable.DB.FileCtl.CapSts.取込中;
                    FileCtl.m_SEND_STS = CommonTable.DB.FileCtl.SendSts.配信済;

                    //削除
                    strSQL = FileCtl.GetDeleteQuery();
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    //登録
                    strSQL = FileCtl.GetInsertQueryCAPData();
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    return true;

                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                    LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);

                    return false;
                }
            }
        }

        /// <summary>
        /// ファイルコントロール更新
        /// </summary>
        public bool FileCtlUpdate(int capsts)
        {
            // Update実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // 更新データ作成
                    TBL_FILE_CTL FileCtl = new TBL_FILE_CTL(_filectkkey.FileID, _filectkkey.FileDivID, _filectkkey.SendFileName, _filectkkey.CapFileName, AppInfo.Setting.SchemaBankCD);
                    FileCtl.m_CAP_STS = capsts;
                    if (capsts == 10)
                    {
                        //完了の場合設定
                        FileCtl.m_CAP_DATE = AplInfo.OpDate();
                        FileCtl.m_CAP_TIME = int.Parse(System.DateTime.Now.ToString("HHmmssfff"));
                    }

                    //更新
                    string strSQL = FileCtl.GetUpdateQueryCAPSTSData();
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    return true;
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                    LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);

                    return false;
                }
            }
        }

        /// <summary>
        /// ファイルパラメータ取得
        /// </summary>
        public bool GetfileParam(out int FileSize)
        {
            FileSize = 0;
            // SELECT実行
            string strSQL = TBL_FILE_PARAM.GetSelectQuery(_file_id, _file_divid, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0) return false;
                    FileSize = new TBL_FILE_PARAM(tbl.Rows[0], AppInfo.Setting.SchemaBankCD).m_FILE_LENGTH;

                    return true;
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                    LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                    return false;
                }
            }
        }

        /// <summary>
        /// 持帰要求結果管理からイメージアーカイブ名を取得
        /// </summary>
        public bool GetImgARCHName(out string ImgARCHName, AdoDatabaseProvider dbp)
        {
            ImgARCHName = string.Empty;
            try
            {
                string strSQL = TBL_ICREQRET_CTL.GetSelectQueryMeiTxtData(_TargetFilename, 0, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                if (tbl.Rows.Count == 0) return false;
                ImgARCHName = new TBL_ICREQRET_CTL(tbl.Rows[0], AppInfo.Setting.SchemaBankCD).m_IMG_ARCH_NAME;

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
        }

        /// <summary>
        /// 持帰要求結果管理更新SQL実行
        /// </summary>
        public int UpdateICREQRETCtl(int capsts, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            int rtnValue = 0;

            try
            {
                string strSQL = TBL_ICREQRET_CTL.GetUpdateQueryMeiTxtData(0, _TargetFilename, capsts, AppInfo.Setting.SchemaBankCD);
                rtnValue = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
            }
            return rtnValue;
        }

        /// <summary>
        /// 交換尻登録時の削除データ一覧取得
        /// </summary>
        /// <returns></returns>
        public bool GetDeleteBillMeiTxtBalance(int Type, out List<TBL_BILLMEITXT_CTL> CtlData , AdoDatabaseProvider dbp)
        {
            // 初期化
            CtlData = new List<TBL_BILLMEITXT_CTL>();

            try
            {
                // データ取得
                string strSQL;
                if (Type == 0)
                {
                    strSQL = SQLTextImport.GetIcTxtDeleteDataSPASFA(_file_id, AppInfo.Setting.SchemaBankCD);
                }
                else
                {
                    strSQL = SQLTextImport.GetIcTxtDeleteDataSPBSFB(_file_id, AppInfo.Setting.SchemaBankCD);
                }
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    CtlData.Add(new TBL_BILLMEITXT_CTL(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
        }

        /// <summary>
        /// 証券明細テキスト管理登録
        /// </summary>
        /// <returns></returns>
        public bool InsertBillMeiTxtCtl(TBL_BILLMEITXT_CTL InsData,
                                        AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // データ登録処理
                string strSQL = InsData.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
        }

        /// <summary>
        /// 証券明細テキスト登録
        /// </summary>
        /// <returns></returns>
        public bool InsertBillMeiTxt(TBL_BILLMEITXT InsData,
                                     AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // データ登録処理
                string strSQL = InsData.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
        }

        /// <summary>
        /// 証券明細テキスト管理削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteBillMeiTxtCtl(string Filename,
                                        AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // データ削除処理
                string strSQL = new TBL_BILLMEITXT_CTL(Filename, AppInfo.Setting.SchemaBankCD).GetDeleteQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
        }

        /// <summary>
        /// 証券明細テキスト削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteBillMeiTxt(string Filename,
                                     AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // データ削除処理
                string strSQL = TBL_BILLMEITXT.GetDeleteQuerytxtName(Filename, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
        }

        /// <summary>
        /// 持帰要求結果証券明細テキスト登録
        /// </summary>
        /// <returns></returns>
        public bool InsertICReqRetBillMeiTxt(TBL_ICREQRET_BILLMEITXT InsData,
                                             AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // データ登録処理
                string strSQL = InsData.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
        }

        /// <summary>
        /// ファイル名から対象のTRITEM取得
        /// </summary>
        /// <returns></returns>
        public bool GetItemDataFileName(string filename, out List<TBL_TRITEM> ItemData, AdoDatabaseProvider dbp)
        {
            // 初期化
            ItemData = new List<TBL_TRITEM>();

            try
            {
                // データ取得
                string strSQL = SQLTextImport.GetTRItemDatafileName(filename, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                if (tbl.Rows.Count == 0) return false;
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    ItemData.Add(new TBL_TRITEM(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
        }

        /// <summary>
        /// バッチ番号の取得
        /// </summary>
        /// <returns></returns>
        public bool GetBatchNumber(int operation_date, out int BatchNumber)
        {
            // 初期化
            BatchNumber = 0;

            try
            {
                DBManager.InitializeSql2(NCR.Server.DBDataSource, NCR.Server.DBUserID, NCR.Server.DBPassword);
                DBManager.dbs2.Open();

                using (AdoDatabaseProvider dbs2 = GenDbProviderFactory.CreateAdoProvider3())
                using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbs2))
                {
                    int LAST_SEQ = 0;

                    // 現在のデータ取得処理
                    string strSQL = TBL_BATCH_SEQ.GetSelectQuery(AppInfo.Setting.GymId, operation_date, AppInfo.Setting.SchemaBankCD, true);
                    DataTable tbl = dbs2.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                    if (tbl.Rows.Count == 0)
                    {
                        strSQL = TBL_BATCH_SEQ.GetInsertQuery(AppInfo.Setting.GymId, operation_date, LAST_SEQ, AppInfo.Setting.SchemaBankCD);
                        dbs2.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                    }
                    else
                    {
                        LAST_SEQ = new TBL_BATCH_SEQ(tbl.Rows[0], AppInfo.Setting.SchemaBankCD).m_LAST_SEQ;
                    }

                    // 更新処理
                    strSQL = TBL_BATCH_SEQ.GetUpdateQuery(AppInfo.Setting.GymId, operation_date, AppInfo.Setting.SchemaBankCD);
                    dbs2.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                    BatchNumber = LAST_SEQ + 1;

                    // コミット
                    Tran.Trans.Commit();
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return false;
            }
            finally
            {
                if (DBManager.dbs2 != null)
                {
                    DBManager.dbs2.Close();
                    DBManager.dbs2 = null;
                }
            }
            return true;
        }

        /// <summary>
        /// 表面イメージから対象証券の通知テキスト一覧を取得
        /// </summary>
        public bool GetIcTxtTsuchiTxt(string filename, out List<TsuchiTxtData> txtDatas, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 初期化
            txtDatas = new List<TsuchiTxtData>();
            try
            {
                // 対象データの取得
                string strSQL = SQLTextImport.GetIcTxtTsuchiTxt(filename, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    txtDatas.Add(new TsuchiTxtData(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
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
        /// 対象のファイルID/ファイル識別区分を取得
        /// </summary>
        private void GetFileDivid(out string file_id, out string file_divid)
        {
            file_id = _TargetFilename.Substring(0, 5);
            file_divid = _TargetFilename.Substring(5, 3);
        }

        /// <summary>
        /// 対象ファイルサイズ取得
        /// </summary>
        public long GetFileSize()
        {
            return (new FileInfo(Path.Combine(HULFTReceiveRoot(), _TargetFilename))).Length;
        }

        /// <summary>
        /// HULFT集信フォルダパスを取得
        /// </summary>
        public string HULFTReceiveRoot()
        {
            return NCR.Server.ReceiveRoot;
        }

        /// <summary>
        /// IO集信フォルダ(銀行別)フォルダパスを取得
        /// </summary>
        public string IOReceiveRoot()
        {
            return string.Format(NCR.Server.IOReceiveRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// ファイルコントロールキー情報
        /// </summary>
        private class FileCtkKey
        {
            //ファイルID
            public string FileID { get; set; } = "";

            //ファイル識別区分
            public string FileDivID { get; set; } = "";

            //配信ファイル名
            public string SendFileName { get; set; } = "";

            //取込ファイル名
            public string CapFileName { get; set; } = "";

            public FileCtkKey(string fileid, string filedivid, string sendfilename, string capfilename)
            {
                FileID = fileid;
                FileDivID = filedivid;
                SendFileName = sendfilename;
                CapFileName = capfilename;
            }
        }

        /// <summary>
        /// 通知テキストデータ
        /// </summary>
        public class TsuchiTxtData
        {
            public TBL_TSUCHITXT TsuchiTxt { get; set; }
            public string FILE_ID { get; set; }
            public string FILE_DIVID { get; set; }
            public int MAKE_DATE { get; set; }
            public int RECV_DATE { get; set; }
            public int RECV_TIME { get; set; }

            public TsuchiTxtData(DataRow dr, int Schemabankcd)
            {
                TsuchiTxt = new TBL_TSUCHITXT(dr, Schemabankcd);
                FILE_ID = DBConvert.ToStringNull(dr[TBL_TSUCHITXT_CTL.FILE_ID]);
                FILE_DIVID = DBConvert.ToStringNull(dr[TBL_TSUCHITXT_CTL.FILE_DIVID]);
                MAKE_DATE = DBConvert.ToIntNull(dr[TBL_TSUCHITXT_CTL.MAKE_DATE]);
                RECV_DATE = DBConvert.ToIntNull(dr[TBL_TSUCHITXT_CTL.RECV_DATE]);
                RECV_TIME = DBConvert.ToIntNull(dr[TBL_TSUCHITXT_CTL.RECV_TIME]);
            }
        }

    }
}
