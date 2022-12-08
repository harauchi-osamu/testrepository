using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Reflection;
using Common;
using CommonTable.DB;
using EntryCommon;

namespace CommonClass
{
    /// <summary>
    /// イメージ取込ファイル操作共通
    /// </summary>
    public class InclearingFileAccess
    {

        #region ファイル操作共通

        /// <summary>
        /// 持帰ダウンロードでの証券イメージの移動
        /// </summary>
        public static void DetailFileMove(string FolderPath, List<FileCtl> MoveList, string MoveFolderPath)
        {
            // 対象バッチフォルダの存在チェック
            if (!Directory.Exists(MoveFolderPath))
            {
                // なければ作る
                Directory.CreateDirectory(MoveFolderPath);
            }
            foreach (FileCtl FileData in MoveList)
            {
                try
                {
                    //ファイル移動
                    string sourceFileName = Path.Combine(FolderPath, FileData.ArchName, FileData.FileName);
                    string destFileName = Path.Combine(MoveFolderPath, FileData.FileName);
                    File.Copy(sourceFileName, destFileName, true);
                    File.Delete(sourceFileName);
                    FileData.Move = true;
                }
                catch (Exception ex)
                {
                    // エラーでもエラー終了はしない
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// 持帰ダウンロードでの証券イメージの移動戻し作業
        /// </summary>
        public static void MoveImageErrBack(string FolderPath, List<FileCtl> MoveList, string MoveFolderPath)
        {
            foreach (FileCtl FileData in MoveList.Where(l => l.Move == true))
            {
                //ファイル移動戻し
                File.Move(Path.Combine(MoveFolderPath, FileData.FileName), Path.Combine(FolderPath, FileData.ArchName, FileData.FileName));
            }
        }

        /// <summary>
        /// 持帰ダウンロードでのアーカイブフォルダの削除作業
        /// </summary>
        public static void DeleteArchFolder(string FolderPath, string ArchName, string BackUpFolder)
        {
            try
            {
                string ArchPath = Path.Combine(FolderPath, ArchName);

                if (!Directory.Exists(ArchPath)) { return; }
                if (Directory.EnumerateFiles(ArchPath, "*.*").Count() == 1 && File.Exists(Path.Combine(ArchPath, Const.FILE_NAME_USERID)))
                {
                    // フォルダ格納ファイルがUSERID.txtのみの場合
                    // txtファイルを削除(バックアップ)
                    ImportFileAccess.FileBackUp(ArchPath, Const.FILE_NAME_USERID, BackUpFolder, false);
                }

                // フォルダが空の場合アーカイブフォルダ削除
                CommonUtil.DeleteEmptyDirectory(ArchPath);
            }
            catch (Exception ex)
            {
                // エラーでもエラー終了はしない
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// 持帰ダウンロード処理での持帰OCR取込フォルダへのコピー処理
        /// </summary>
        /// <param name="ArchName">アーカイブ名</param>
        /// <param name="ArchFolderPath">アーカイブ展開フォルダ</param>
        /// <param name="ICOCRRootPath">持帰OCR取込用ルート</param>
        /// <param name="CopyUnit">持帰OCRコピー件数単位</param>
        public static bool ImageICOCRCopy(string ArchName, string ArchFolderPath, string ICOCRRootPath, int CopyUnit)
        {
            // アーカイブ展開フォルダのイメージ一覧作成
            List<ICOcrDetailCtl> Details;
            if (!MKICOCRCopyList(ArchName, ArchFolderPath, CopyUnit, out Details))
            {
                return false;
            }

            try
            {
                //ファイルコピー処理
                int FolderNameCnt = 1;
                int FolderCopyCnt = 0;
                foreach (ICOcrDetailCtl detailCtl in Details)
                {
                    // 格納件数を足しこむ
                    FolderCopyCnt += 1;
                    if (FolderCopyCnt > CopyUnit)
                    {
                        // コピー単位を超えていたら次の番号に初期化
                        FolderCopyCnt = 1;
                        FolderNameCnt++;
                    }

                    // コピー先フォルダ算出
                    detailCtl.ICOCRDirPath = Path.Combine(ICOCRRootPath, string.Format("{0}_{1:00000}", ArchName, FolderNameCnt));

                    // ファイルコピー
                    DetailFileCopy(detailCtl);

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持帰OCR取込フォルダコピー：{0}", detailCtl.ICOCRDirPath), 3);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                //エラー戻し作業
                CopyImageErrBack(Details);
                return false;
            }

            return true;
        }

        /// <summary>
        /// アーカイブ展開フォルダのイメージ一覧作成
        /// </summary>
        private static bool MKICOCRCopyList(string ArchName, string ArchFolderPath, int CopyUnit, out List<ICOcrDetailCtl> Details)
        {
            // 初期化
            Details = new List<ICOcrDetailCtl>();

            try
            {
                // アーカイブ展開フォルダのイメージ一覧取得
                IEnumerable<string> FileList = Directory.EnumerateFiles(ArchFolderPath, "*.jpg").Select(name => Path.GetFileName(name)).OrderBy(name => name);

                // 持帰OCR取込用ルートコピー対象ファイル一覧を作成
                foreach (string filename in FileList)
                {
                    string Key = filename.Substring(0, 56); // ファイルキー
                    string Kbn = filename.Substring(56, 2); // ファイル区分
                    int intKbn = int.Parse(Kbn);
                    if (!(intKbn == TrMeiImg.ImgKbn.表 || intKbn == TrMeiImg.ImgKbn.裏))
                    {
                        //表裏以外の場合は次へ
                        continue;
                    }

                    ICOcrFileCtl file = new ICOcrFileCtl(filename);
                    IEnumerable<ICOcrDetailCtl> ie = Details.Where(x => x.FileKey == Key);
                    if (ie.Count() == 0)
                    {
                        //キー単位で新規の場合
                        ICOcrDetailCtl ctl = new ICOcrDetailCtl();
                        ctl.FileKey = Key;
                        ctl.ImageDirPath = ArchFolderPath;
                        ctl.FileList.Add(file);
                        Details.Add(ctl);
                    }
                    else
                    {
                        //キー単位で存在している場合
                        ie.First().FileList.Add(file);
                    }
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
        /// 証券イメージのコピー
        /// </summary>
        private static void DetailFileCopy(ICOcrDetailCtl detailCtl)
        {
            // コピー先フォルダの存在チェック
            if (!Directory.Exists(detailCtl.ICOCRDirPath))
            {
                // なければ作る
                Directory.CreateDirectory(detailCtl.ICOCRDirPath);
            }

            foreach (ICOcrFileCtl FileData in detailCtl.FileList)
            {
                //ファイルコピー
                File.Copy(Path.Combine(detailCtl.ImageDirPath, FileData.FileName), Path.Combine(detailCtl.ICOCRDirPath, FileData.FileName), true);
                FileData.Copy = true;
            }
        }

        /// <summary>
        /// 証券イメージのコピー戻し作業
        /// </summary>
        private static void CopyImageErrBack(List<ICOcrDetailCtl> Details)
        {
            //フォルダ単位で削除
            foreach (var ICOcrParhGrp in Details.GroupBy(x => x.ICOCRDirPath))
            {
                if (string.IsNullOrEmpty(ICOcrParhGrp.Key))
                {
                    continue;
                }

                try
                {
                    // フォルダ存在チェック
                    if (!Directory.Exists(ICOcrParhGrp.Key))
                    {
                        continue;
                    }

                    // フォルダ削除
                    Directory.Delete(ICOcrParhGrp.Key);
                }
                catch (Exception ex)
                {
                    // エラーでもエラー終了はしない
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        #endregion

        #region クラス

        /// <summary>
        /// ファイル管理クラス
        /// </summary>
        public class FileCtl
        {
            /// <summary>要求結果テキストファイル名</summary>
            public string RetReqTxtName { get; set; } = string.Empty;
            /// <summary>証券明細テキストファイル名</summary>
            public string MeiTxtName { get; set; } = string.Empty;
            /// <summary>取込区分</summary>
            public string CapKbn { get; set; } = string.Empty;
            /// <summary>ファイル名</summary>
            public string FileName { get; set; } = string.Empty;
            /// <summary>アーカイブ名</summary>
            public string ArchName { get; set; } = string.Empty;
            /// <summary>移動済フラグ</summary>
            public bool Move { get; set; } = false;

            public FileCtl(string retreqtxtname, string meitxtname, string capkbn, string filename, string archname)
            {
                RetReqTxtName = retreqtxtname;
                MeiTxtName = meitxtname;
                CapKbn = capkbn;
                FileName = filename;
                ArchName = archname;
            }
        }

        /// <summary>
        /// 持帰OCRデータ管理クラス
        /// </summary>
        private class ICOcrDetailCtl
        {
            /// <summary>イメージファイルKey名</summary>
            public string FileKey { get; set; } = string.Empty;
            /// <summary>イメージファイル格納フォルダパス</summary>
            public string ImageDirPath { get; set; } = string.Empty;
            /// <summary>持帰OCRイメージファイル格納フォルダパス</summary>
            public string ICOCRDirPath { get; set; } = string.Empty;
            /// <summary>明細データのイメージファイルリスト</summary>
            public List<ICOcrFileCtl> FileList { get; set; } = new List<ICOcrFileCtl>();
        }

        /// <summary>
        /// 持帰OCRファイル管理クラス
        /// </summary>
        private class ICOcrFileCtl
        {
            /// <summary>ファイル名</summary>
            public string FileName { get; set; } = string.Empty;

            /// <summary>持帰OCR処理済フラグ</summary>
            public bool Copy { get; set; } = false;

            public ICOcrFileCtl(string fileName)
            {
                FileName = fileName;
            }
        }

        #endregion

    }
}
