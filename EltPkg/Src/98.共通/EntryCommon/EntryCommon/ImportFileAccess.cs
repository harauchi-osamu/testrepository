using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Reflection;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;

namespace CommonClass
{
    /// <summary>
    /// イメージ取込ファイル操作共通
    /// </summary>
    public class ImportFileAccess
    {
        #region ファイル操作共通

        /// <summary>
        /// 証券単位のFileリネーム処理
        /// </summary>
        public static void DetailFileRename(string ImgFolderPath, string TermIPAddress, DetailCtl Detail, int OC_BKNo, int OC_BRNo, int ClearingData, 
                                            int UniqueCodeSleepTime, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            DetailFileRename(ImgFolderPath, TermIPAddress, Detail,
                             OC_BKNo.ToString("D4"), OC_BRNo.ToString("D4"), new string('Z', 4), new string('Z', 8), ClearingData.ToString("D8"), new string('Z', 12), "0",
                             UniqueCodeSleepTime, dbp, Tran);
        }

        /// <summary>
        /// 証券単位のFileリネーム処理
        /// </summary>
        public static void DetailFileRename(string ImgFolderPath, string TermIPAddress, DetailCtl Detail, int OC_BKNo, int OC_BRNo, int IC_BKNo, int ClearingData,
                                            long Amount, int UniqueCodeSleepTime, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            DetailFileRename(ImgFolderPath, TermIPAddress, Detail,
                             OC_BKNo.ToString("D4"), OC_BRNo.ToString("D4"), IC_BKNo.ToString("D4"), new string('Z', 8), ClearingData.ToString("D8"), Amount.ToString("D12"), "0",
                             UniqueCodeSleepTime, dbp, Tran);
        }

        /// <summary>
        /// 証券単位のFileリネーム処理
        /// </summary>
        public static void DetailFileRename(string ImgFolderPath, string TermIPAddress, DetailCtl Detail, int OC_BKNo, int OC_BRNo, int IC_BKNo, int OC_Date, int ClearingData,
                                            long Amount, string PayKbn, int UniqueCodeSleepTime, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            DetailArchiveFileRename(ImgFolderPath, TermIPAddress, Detail,
                             OC_BKNo.ToString("D4"), OC_BRNo.ToString("D4"), IC_BKNo.ToString("D4"), OC_Date.ToString("D8"), ClearingData.ToString("D8"), Amount.ToString("D12"), PayKbn,
                             UniqueCodeSleepTime, dbp, Tran);
        }

        /// <summary>
        /// 証券単位のFileリネーム処理
        /// </summary>
        public static void DetailFileRenameArchive(string ImgFolderPath, string TermIPAddress, DetailCtl Detail, string OC_BKNo, string OC_BRNo, string IC_BKNo, string OC_Date, string ClearingData,
                                                   string Amount, string PayKbn, int UniqueCodeSleepTime, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            DetailArchiveFileRename(ImgFolderPath, TermIPAddress, Detail,
                             OC_BKNo, OC_BRNo, IC_BKNo, OC_Date, ClearingData, Amount, PayKbn,
                             UniqueCodeSleepTime, dbp, Tran);
        }

        /// <summary>
        /// 証券単位のFileリネーム処理
        /// </summary>
        public static void DetailFileRename(string ImgFolderPath, string TermIPAddress, DetailCtl Detail, int OC_BKNo, int OC_BRNo, 
                                            int UniqueCodeSleepTime, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            DetailFileRename(ImgFolderPath, TermIPAddress, Detail,
                             OC_BKNo.ToString("D4"), OC_BRNo.ToString("D4"), new string('Z', 4), new string('Z', 8), new string('Z', 8), new string('Z', 12), "0",
                             UniqueCodeSleepTime, dbp, Tran);
        }

        /// <summary>
        /// 証券単位のFileリネーム処理
        /// </summary>
        private static void DetailFileRename(string ImgFolderPath, string TermIPAddress, DetailCtl Detail, 
                                             string OC_BKNo, string OC_BRNo, string IC_BKNo, string OC_Date, string ClearingData, string Amount, string PayKbn,
                                             int UniqueCodeSleepTime, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            List<FileCtl> fileList = new List<FileCtl>();
            // 証券の一意キー取得
            if (!GetImageFileUniqueCode(UniqueCodeSleepTime, TermIPAddress.Last().ToString(), dbp, Tran, out string FileUniqueCode))
            {
                //呼び元のcatch でリネームの戻し作業等を行う
                throw new Exception("一意キー取得エラー");
            }

            foreach (FileCtl Data in Detail.FileList)
            {
                // 証券イメージのリネーム
                SetRenameFileInfo(OC_BKNo, OC_BRNo, IC_BKNo, OC_Date, ClearingData, Amount, PayKbn, FileUniqueCode, Data);
                RenameImageFile(ImgFolderPath, FileUniqueCode, Data);
            }
        }

        /// <summary>
        /// 証券イメージのFileリネーム処理
        /// </summary>
        private static void DetailArchiveFileRename(string ImgFolderPath, string TermIPAddress, DetailCtl Detail,
                                             string OC_BKNo, string OC_BRNo, string IC_BKNo, string OC_Date, string ClearingData, string Amount, string PayKbn,
                                             int UniqueCodeSleepTime, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            foreach (FileCtl Data in Detail.FileList)
            {
                // 証券イメージのリネーム
                SetRenameFileInfo(OC_BKNo, OC_BRNo, IC_BKNo, OC_Date, ClearingData, Amount, PayKbn, Data.UNIQUECODE, Data);
                RenameImageFile(ImgFolderPath, Data.UNIQUECODE, Data);
            }
        }

        /// <summary>
        /// リネームファイル情報設定
        /// </summary>
        private static void SetRenameFileInfo(string OC_BKNo, string OC_BRNo, string IC_BKNo, string OC_Date, string ClearingData, string Amount, string PayKbn,
                                              string FileUniqueCode, FileCtl fileCtl)
        {
            fileCtl.OC_BKNO = OC_BKNo;  // 持出銀行コード
            fileCtl.OC_BRNO = OC_BRNo;  // 持出支店コード
            fileCtl.IC_BKNO = IC_BKNo;  // 持帰銀行コード
            fileCtl.OC_DATE = OC_Date;  // 持出日
            fileCtl.CLEARING_DATE = ClearingData;  // 交換希望日
            fileCtl.AMOUNT = Amount;  // 金額
            fileCtl.PAY_KBN = PayKbn;  // 決済対象区分
            fileCtl.UNIQUECODE = FileUniqueCode;  // 持出銀行で一意となるコード
            fileCtl.IMG_KBN = ((int)fileCtl.IMG_KBNCD).ToString("D2");  // 表・裏等の別
            fileCtl.EXTENSION = ".jpg";  // 拡張子
            fileCtl.NewFileName = fileCtl.GetRenameFileName();
        }

        /// <summary>
        /// Fileリネーム処理
        /// </summary>
        private static void RenameImageFile(string ImgFolderPath, string FileUniqueCode, FileCtl fileCtl)
        {
            //ファイルリネーム
            fileCtl.NewFileName = fileCtl.GetRenameFileName();
            File.Move(Path.Combine(ImgFolderPath, fileCtl.OldFileName), Path.Combine(ImgFolderPath, fileCtl.NewFileName));

            //リネーム済フラグ設定
            fileCtl.Rename = true;
        }

        /// <summary>
        /// 証券イメージの移動
        /// バッチルート情報(銀行別)
        /// </summary>
        public static void DetailFileMoveBank(string FolderPath, Dictionary<string, DetailCtl> RenameList, string MoveFolderPath)
        {
            // 対象バッチフォルダの存在チェック
            if (!Directory.Exists(MoveFolderPath))
            {
                // なければ作る
                Directory.CreateDirectory(MoveFolderPath);
            }

            foreach (DetailCtl Detail in RenameList.Values)
            {
                foreach (FileCtl FileData in Detail.FileList)
                {
                    if (!string.IsNullOrEmpty(FileData.NewFileName))
                    {
                        //ファイル移動
                        File.Move(Path.Combine(FolderPath, FileData.NewFileName), Path.Combine(MoveFolderPath, FileData.NewFileName));
                        FileData.Move = true;
                    }
                }
            }
        }

        /// <summary>
        /// 証券イメージのリネーム戻し作業
        /// </summary>
        public static void RenameImageErrBack(string FolderPath, Dictionary<string, DetailCtl> RenameList, string MoveFolderPath)
        {

            foreach (FileCtl FileData in RenameList.SelectMany(l => l.Value.FileList).ToList().Where(l => l.Move == true))
            {
                //ファイル移動戻し
                File.Move(Path.Combine(MoveFolderPath, FileData.NewFileName), Path.Combine(FolderPath, FileData.NewFileName));
            }
            foreach (FileCtl FileData in RenameList.SelectMany(l => l.Value.FileList).ToList().Where(l => l.Rename == true))
            {
                //リネーム戻し
                File.Move(Path.Combine(FolderPath, FileData.NewFileName), Path.Combine(FolderPath, FileData.OldFileName));
            }
        }

        /// <summary>
        /// 証券イメージのリネーム戻し作業
        /// </summary>
        public static void RenameImageErrBack(DetailCtl Detail)
        {
            foreach (FileCtl FileData in Detail.FileList.Where(l => l.Rename == true))
            {
                //リネーム戻し
                File.Move(Path.Combine(Detail.ImageDirPath, FileData.NewFileName), Path.Combine(Detail.ImageDirPath, FileData.OldFileName));
            }
        }

        /// <summary>
        /// 未使用イメージの退避処理
        /// </summary>
        public static bool ImageBackUp(string FolderPath, string BackUpFolderPath)
        {
            try
            {
                // ファイル名一覧取得
                foreach (string FileName in Directory.EnumerateFiles(FolderPath, "*.jpg").Select(name => Path.GetFileName(name)))
                {
                    if (!FileBackUp(FolderPath, FileName, BackUpFolderPath)) return false;
                }
            }
            catch(Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }

            return true;
        }

        /// <summary>
        /// イメージの退避処理
        /// </summary>
        public static bool FileBackUp(string FolderPath, string FileName, string BackUpFolderPath, bool Copy = false)
        {
            try
            {
                if(!File.Exists(Path.Combine(FolderPath, FileName)))
                {
                    //退避ファイルがない場合
                    return true;
                }

                int i = 0;
                do
                {
                    string RenameKey = System.DateTime.Now.ToString("yyyyMMddHHmmssfff");

                    if (!File.Exists(Path.Combine(BackUpFolderPath, RenameKey + "_" + FileName)))
                    {
                        // ファイルが存在していない場合、処理実施
                        if (Copy)
                        {
                            File.Copy(Path.Combine(FolderPath, FileName), Path.Combine(BackUpFolderPath, RenameKey + "_" + FileName));
                        }
                        else
                        {
                            File.Move(Path.Combine(FolderPath, FileName), Path.Combine(BackUpFolderPath, RenameKey + "_" + FileName));
                        }
                        i = 999;
                        break;
                    }

                    //１秒スリープ
                    System.Threading.Thread.Sleep(1000);

                    i++;
                } while (i == 10);

                if (i == 10)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取込処理での処理済フォルダの削除作業
        /// </summary>
        public static void DeleteImportFolder(string FolderPath)
        {
            try
            {
                // 格納ファイルがなければ削除
                CommonUtil.DeleteEmptyDirectory(FolderPath);
            }
            catch (Exception ex)
            {
                // エラーでもエラー終了はしない
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        #endregion

        #region DBアクセス

        /// <summary>
        /// 一意コード取得
        ///   先頭1桁：端末IPアドレスの下一桁
        ///   2桁目以降：yymmddHHMMssff
        /// </summary>
        /// <returns></returns>
        private static bool GetImageFileUniqueCode(int UniqueCodeSleepTime, string LastIPAddress, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran,
                                                   out string FileUniqueCode, FormBase form = null)
        {
            // 初期化
            FileUniqueCode = string.Empty;

            try
            {
                //指定時間スリープ
                System.Threading.Thread.Sleep(UniqueCodeSleepTime);

                // SQL文取得
                string strSQL = SQLImageImport.Get_ImageFileUniqueCodeQuery();
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                if (tbl.Rows.Count == 0)
                {
                    // 無ければエラー処理
                    throw new Exception("一意コード取得エラー");
                }

                string UniqueCode = DBConvert.ToStringNull(tbl.Rows[0].ItemArray.First());
                FileUniqueCode = LastIPAddress + UniqueCode.Substring(0, 14);
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
        /// 端末のIPアドレス取得
        /// </summary>
        /// <returns></returns>
        public static string GetTermIPAddress()
        {
            string ipaddress = "";
            System.Net.IPHostEntry ipentry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ip in ipentry.AddressList)
            {
                if (ip.ToString().Contains(AppConfig.MustBeIncludedIp))
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipaddress = ip.ToString();
                        break;
                    }
                }
            }
            return ipaddress;
        }

        /// <summary>
        /// ファイル一連番号を採番する
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="fileDivid"></param>
        /// <param name="count"></param>
        /// <param name="dbp"></param>
        /// <param name="Tran"></param>
        /// <returns></returns>
        public static int GetFileSeq(string fileId, string fileDivid, int count, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            TBL_FILE_SEQ fseq = null;

            try
            {
                // 引数のDBProviderとは別のセッションを作成して一連番号を取得
                CommonClass.DB.DBManager.InitializeSql1(NCR.Server.DBDataSource, NCR.Server.DBUserID, NCR.Server.DBPassword);
                CommonClass.DB.DBManager.dbs1.Open();

                using (AdoDatabaseProvider dbp2 = GenDbProviderFactory.CreateAdoProvider2())
                using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp2))
                {
                    // 行ロック取得
                    string strSQL = TBL_FILE_SEQ.GetSelectQuery(AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD, true);
                    DataTable tbl = dbp2.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    if (tbl.Rows.Count < 1)
                    {
                        // INSERT
                        fseq = new TBL_FILE_SEQ(AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
                        fseq.m_LAST_SEQ = 0;
                        fseq.m_LAST_SEQ_DATE = AplInfo.OpDate();
                        fseq.m_LAST_SEQ_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
                        fseq.m_LAST_FILE_ID = fileId;
                        fseq.m_LAST_FILE_DIVID = fileDivid;

                        InsFileSeq(fseq, dbp2, auto);

                        // 行ロック取得
                        strSQL = TBL_FILE_SEQ.GetSelectQuery(AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD, true);
                        tbl = dbp2.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        if (tbl.Rows.Count < 1) throw new Exception("ファイル一連番号取得エラー"); // それでも取れない場合はエラーをthrow
                    }

                    // UPDATE
                    fseq = new TBL_FILE_SEQ(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    fseq.m_LAST_SEQ += count;
                    fseq.m_LAST_SEQ_DATE = AplInfo.OpDate();
                    fseq.m_LAST_SEQ_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
                    fseq.m_LAST_FILE_ID = fileId;
                    fseq.m_LAST_FILE_DIVID = fileDivid;

                    strSQL = fseq.GetUpdateQuery();
                    dbp2.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }

                return fseq.m_LAST_SEQ;
            }
            finally
            {
                if (DBManager.dbs1 != null)
                {
                    DBManager.dbs1.Close();
                    DBManager.dbs1 = null;
                }
            }

            //TBL_FILE_SEQ fseq = null;

            //string strSQL = TBL_FILE_SEQ.GetSelectQuery(AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
            //DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            //if (tbl.Rows.Count < 1)
            //{
            //    // INSERT
            //    fseq = new TBL_FILE_SEQ(AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
            //    fseq.m_LAST_SEQ = count;
            //    fseq.m_LAST_SEQ_DATE = AplInfo.OpDate();
            //    fseq.m_LAST_SEQ_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
            //    fseq.m_LAST_FILE_ID = fileId;
            //    fseq.m_LAST_FILE_DIVID = fileDivid;

            //    strSQL = fseq.GetInsertQuery();
            //    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            //}
            //else
            //{
            //    // UPDATE
            //    fseq = new TBL_FILE_SEQ(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
            //    fseq.m_LAST_SEQ += count;
            //    fseq.m_LAST_SEQ_DATE = AplInfo.OpDate();
            //    fseq.m_LAST_SEQ_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
            //    fseq.m_LAST_FILE_ID = fileId;
            //    fseq.m_LAST_FILE_DIVID = fileDivid;

            //    strSQL = fseq.GetUpdateQuery();
            //    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            //}
            //return fseq.m_LAST_SEQ;
        }

        /// <summary>
        /// ファイル一連番号登録
        /// </summary>
        /// <returns></returns>
        private static void InsFileSeq(TBL_FILE_SEQ fseq, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            try
            {
                // INSERT
                string strSQL = fseq.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception ex)
            {
                // エラーでも無視
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        #endregion

        #region クラス

        /// <summary>
        /// 明細データ管理クラス
        /// </summary>
        public class DetailCtl
        {
            /// <summary>明細データのイメージファイルリスト</summary>
            public List<FileCtl> FileList { get; set; } = new List<FileCtl>();
            /// <summary>イメージファイル格納フォルダパス</summary>
            public string ImageDirPath { get; set; } = string.Empty;
            /// <summary>イメージファイルリストのファイルサイズ合計</summary>
            public long TotalFileSize { get; set; } = 0;
        }

        /// <summary>
        /// ファイル管理クラス
        /// </summary>
        public class FileCtl
        {
            /// <summary>
            /// 表・裏等の別
            /// </summary>
            public enum ImgKbn
            {
                Omote = TrMeiImg.ImgKbn.表,
                Ura = TrMeiImg.ImgKbn.裏,
                Hosen = TrMeiImg.ImgKbn.補箋,
                Fusen = TrMeiImg.ImgKbn.付箋,
                Nyukin = TrMeiImg.ImgKbn.入金証明,
                SaiOmote = TrMeiImg.ImgKbn.表再送分,
                SaiUra = TrMeiImg.ImgKbn.裏再送分,
                Other1 = TrMeiImg.ImgKbn.その他1,
                Other2 = TrMeiImg.ImgKbn.その他2,
                Other3 = TrMeiImg.ImgKbn.その他3,
                Yobi1 = TrMeiImg.ImgKbn.予備1,
                Yobi2 = TrMeiImg.ImgKbn.予備2,
                Yobi3 = TrMeiImg.ImgKbn.予備3,
            }

            /// <summary>表・裏等の別（数値）</summary>
            public ImgKbn IMG_KBNCD { get; set; } = 0;

            /// <summary>変更前ファイル名</summary>
            public string OldFileName { get; set; } = string.Empty;

            /// <summary>変更後ファイル名</summary>
            public string NewFileName { get; set; } = string.Empty;

            /// <summary>持出銀行コード</summary>
            public string OC_BKNO { get; set; } = string.Empty;

            /// <summary>持出支店コード</summary>
            public string OC_BRNO { get; set; } = string.Empty;

            /// <summary>持帰銀行コード</summary>
            public string IC_BKNO { get; set; } = string.Empty;

            /// <summary>持出日</summary>
            public string OC_DATE { get; set; } = string.Empty;

            /// <summary>交換希望日</summary>
            public string CLEARING_DATE { get; set; } = string.Empty;

            /// <summary>金額</summary>
            public string AMOUNT { get; set; } = string.Empty;

            /// <summary>決済対象区分</summary>
            public string PAY_KBN { get; set; } = string.Empty;

            /// <summary>一意コード</summary>
            public string UNIQUECODE { get; set; } = string.Empty;

            /// <summary>表・裏等の別</summary>
            public string IMG_KBN { get; set; } = string.Empty;

            /// <summary>拡張子</summary>
            public string EXTENSION { get; set; } = string.Empty;

            /// <summary>リネーム済フラグ</summary>
            public bool Rename { get; set; } = false;

            /// <summary>バッチルート情報(銀行別)移動済フラグ</summary>
            public bool Move { get; set; } = false;

            /// <summary>ファイルサイズ</summary>
            public long FileSize { get; set; } = 0;

            public string GetRenameFileName()
            {
                return OC_BKNO + OC_BRNO + IC_BKNO + OC_DATE + CLEARING_DATE + AMOUNT + PAY_KBN + UNIQUECODE + IMG_KBN + EXTENSION;
            }

            public FileCtl(ImgKbn kbn, string FileName)
            {
                IMG_KBNCD = kbn;
                OldFileName = FileName;
            }
        }

        #endregion

    }
}
