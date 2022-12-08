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
        /// 交換尻登録時の削除データ一覧取得
        /// </summary>
        /// <returns></returns>
        public bool GetDeleteBalanceTxt(out List<TBL_BALANCETXT_CTL> CtlData , AdoDatabaseProvider dbp)
        {
            // 初期化
            CtlData = new List<TBL_BALANCETXT_CTL>();

            try
            {
                // データ取得
                string strSQL = SQLTextImport.GetBalanceTxtDeleteData(_file_id, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    CtlData.Add(new TBL_BALANCETXT_CTL(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
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
        /// 交換尻管理削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteBalanceTxtCtl(string Filename,
                                        AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // データ削除処理
                string strSQL = new TBL_BALANCETXT_CTL(Filename, AppInfo.Setting.SchemaBankCD).GetDeleteQuery();
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
        /// 交換尻削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteBalanceTxt(string Filename,
                                     AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // データ削除処理
                string strSQL = TBL_BALANCETXT.GetDeleteQueryfileName(Filename, AppInfo.Setting.SchemaBankCD);
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
        /// 交換尻管理登録
        /// </summary>
        /// <returns></returns>
        public bool InsertBalanceTxtCtl(TBL_BALANCETXT_CTL InsData,
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
        /// 交換尻登録
        /// </summary>
        /// <returns></returns>
        public bool InsertBalanceTxt(TBL_BALANCETXT InsData,
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

    }
}
