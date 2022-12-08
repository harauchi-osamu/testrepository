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
        /// 開始時更新
        /// </summary>
        public int FileCtlStartUpdate()
        {
            // Delete/Insert実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // 更新データ作成
                    TBL_FILE_CTL FileCtl = new TBL_FILE_CTL(_filectkkey.FileID, _filectkkey.FileDivID, _filectkkey.SendFileName, _filectkkey.CapFileName, AppInfo.Setting.SchemaBankCD);
                    FileCtl.m_CAP_FILE_LENGTH = GetFileSize();
                    FileCtl.m_CAP_STS = 1;

                    //更新
                    string strSQL = FileCtl.GetUpdateQueryCAPFilesizeData();
                    return dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                    LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);

                    return -1;
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
        /// 結果テキスト管理登録
        /// </summary>
        /// <returns></returns>
        public bool InsertResultTxtCtl(TBL_RESULTTXT_CTL InsData,
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
        /// 結果テキスト登録
        /// </summary>
        /// <returns></returns>
        public bool InsertResultTxt(TBL_RESULTTXT InsData,
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
        /// TRMEIのステータス更新
        /// </summary>
        /// <returns></returns>
        public int UpdateTRMeiSTS(string FileName, string FieldName, int STS, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            Dictionary<string, int> Field = new Dictionary<string, int>() { { FieldName, STS } };
            return UpdateTRMeiSTS(FileName, Field, dbp, Tran);
        }

        /// <summary>
        /// TRMEIのステータス更新
        /// </summary>
        /// <returns></returns>
        public int UpdateTRMeiSTS(string FileName, Dictionary<string, int> Field, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                //更新
                string strSQL = SQLTextImport.GetUpdateTRMEIFileName(AppInfo.Setting.GymId, FileName, Field, AppInfo.Setting.SchemaBankCD);
                return dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return -1;
            }
        }

        /// <summary>
        /// TRMEIIMGのステータス更新
        /// </summary>
        /// <returns></returns>
        public int UpdateTRMeiImgSTS(string FileName, string FieldName, int STS, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            Dictionary<string, int> Field = new Dictionary<string, int>() { { FieldName, STS } };
            return UpdateTRMeiImgSTS(FileName, Field, dbp, Tran);
        }

        /// <summary>
        /// TRMEIIMGのステータス更新
        /// </summary>
        /// <returns></returns>
        public int UpdateTRMeiImgSTS(string FileName, Dictionary<string, int> Field, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                //更新
                string strSQL = SQLTextImport.GetUpdateTRMEIIMGFileName(AppInfo.Setting.GymId, FileName, Field, AppInfo.Setting.SchemaBankCD);
                return dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return -1;
            }
        }

        /// <summary>
        /// TRMEIIMGのステータス更新
        /// 対象イメージ(表面を想定)から対象証券すべてのTRMEIIMGを更新
        /// </summary>
        /// <returns></returns>
        public int UpdateTRMeiImgSTSFrontImg(string FileName, Dictionary<string, int> Field, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                //更新
                string strSQL = SQLTextImport.GetUpdateTRMEIIMGFrontFileName(AppInfo.Setting.GymId, FileName, Field, AppInfo.Setting.SchemaBankCD);
                return dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _TargetFilename, 3);
                return -1;
            }
        }

        /// <summary>
        /// TRITEMの項目更新
        /// 持帰訂正確定値を更新
        /// </summary>
        /// <returns></returns>
        public bool UpdateTRItemTeiseiData(string FileName, List<int> UpdItemID, bool SetTeiseiNull, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                //更新
                string strSQL = SQLTextImport.GetUpdateTRItemTeiseiData(AppInfo.Setting.GymId, FileName, UpdItemID, SetTeiseiNull, "訂正結果取込", AppInfo.Setting.SchemaBankCD);
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
                string strSQL = SQLTextImport.GetTRItemDatafileName(AppInfo.Setting.GymId, filename, AppInfo.Setting.SchemaBankCD);
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
        /// ファイル名から対象のTRMEI取得
        /// </summary>
        /// <returns></returns>
        public bool GetMeiDataFileName(string filename, out TBL_TRMEI MeiData, AdoDatabaseProvider dbp)
        {
            // 初期化
            MeiData = new TBL_TRMEI(AppInfo.Setting.SchemaBankCD);

            try
            {
                // データ取得
                string strSQL = SQLTextImport.GetTRMeiDatafileName(AppInfo.Setting.GymId, filename, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                if (tbl.Rows.Count == 0) return false;
                MeiData = new TBL_TRMEI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);

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
        /// 明細データの削除処理
        /// 持帰訂正確時の削除
        /// </summary>
        public bool DeleteTrMei(TBL_TRITEM ItemData, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                //更新
                string strSQL = string.Empty;

                // TRMEIの削除
                strSQL = SQLTextImport.GetUpdateTRMEIDelete(ItemData._GYM_ID, ItemData._OPERATION_DATE, ItemData._SCAN_TERM, 
                                                            ItemData._BAT_ID, ItemData._DETAILS_NO, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                // TRMEIMGの削除
                strSQL = SQLTextImport.GetUpdateTRMEIIMGDetailDelete(ItemData._GYM_ID, ItemData._OPERATION_DATE, ItemData._SCAN_TERM, 
                                                                     ItemData._BAT_ID, ItemData._DETAILS_NO, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
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
        /// OCR_DATA/IC_OCR_FINISHデータの削除処理
        /// 持帰訂正確時の削除
        /// </summary>
        public bool DeleteOCRDATA(List<string> DeleteFiles, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                if (DeleteFiles.LongCount() == 0)
                {
                    // 削除データがない場合
                    return true;
                }

                string strSQL = string.Empty;

                // OCR_DATAの削除
                strSQL = SQLTextImport.GetDeleteOCRDATAFileName(AppInfo.Setting.GymId, DeleteFiles);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                // IC_OCR_FINISHの削除
                strSQL = SQLTextImport.GetDeleteICOCRFINISHFileName(DeleteFiles);
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
        /// TRITEMの項目更新
        /// BUBでの項目更新
        /// </summary>
        /// <returns></returns>
        public bool UpdateTRItemBUB(string FileName, int ItemID, string UpdateData, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                //更新
                string strSQL = SQLTextImport.GetUpdateTRItemBUB(AppInfo.Setting.GymId, FileName, ItemID, UpdateData, "持出結果取込", AppInfo.Setting.SchemaBankCD);
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
        /// 対象の銀行が自行か判定
        /// 持帰銀行がなければエラーとなるためエラーハンドリンクしておくこと
        /// </summary>
        /// <returns>自行の場合、Trueを返す</returns>
        public bool ChkJikouBankCd(List<TBL_TRITEM> ItemData)
        {
            IEnumerable<TBL_TRITEM> ie = ItemData.Where(x => x._ITEM_ID == DspItem.ItemId.持帰銀行コード);
            return DBConvert.ToIntNull(ie.First().m_END_DATA) == AppInfo.Setting.SchemaBankCD;
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

    }
}
