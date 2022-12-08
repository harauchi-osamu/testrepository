using System;
using System.IO;
using System.Text;
using CommonClass;
using CommonTable.DB;

namespace EntryCommon
{
    /// <summary>
    /// 電子交換所インターフェース生成クラス
    /// </summary>
    public class FileGenerator
    {
        /// <summary>改行コード</summary>
        public const string CRLF = "\r\n";

        /// <summary>シーケンス番号（8桁）</summary>
        public string Seq { get; private set; }
        /// <summary>ファイルID（5桁）</summary>
        public string FileId { get; private set; }
        /// <summary>ファイル識別区分（3桁）</summary>
        public string FileDivid { get; private set; }
        /// <summary>銀行番号（4桁）</summary>
        public int BankCd { get; private set; }
        /// <summary>作成日（8桁）</summary>
        public int CreateDate { get; private set; }
        /// <summary>拡張子</summary>
        public string Extension { get; private set; }
        /// <summary>ファイルサイズ</summary>
        public long FileSize { get; set; }

        /// <summary>ファイル名</summary>
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(FileId))
                {
                    return "";
                }
                string wkBkCd = BankCd.ToString("D4");
                if (BankCd == 0)
                {
                    wkBkCd = "ZZZZ";
                }
                return string.Format("{0}{1}{2}{3}{4}{5}", FileId, FileDivid, wkBkCd, CreateDate, Seq, Extension);
            }
        }

        /// <summary>アーカイブ出力先フォルダ</summary>
        public string ArchiveOutputPath { get { return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "電子交換パッケージ"); } }
        /// <summary>アーカイブフォルダ名</summary>
        public string ArchiveDirName { get { return string.Format("{0}{1}{2:0000}{3}{4}", FileId, FileDivid, BankCd, CreateDate, Seq); } }
        /// <summary>アーカイブファイルパス</summary>
        public string ArchiveFilePath { get { return System.IO.Path.Combine(ArchiveOutputPath, FileName); } }

        /// <summary>ファイル名桁数</summary>
        private const int SEQ_LEN = 8;
        private const int FILE_ID_LEN = 5;
        private const int FILE_DIVID_LEN = 3;
        private const int BANK_CD_LEN = 4;
        private const int CREATE_DATE_LEN = 8;
        private const int FILE_NAME_MAX_LEN = SEQ_LEN + FILE_ID_LEN + FILE_DIVID_LEN + BANK_CD_LEN + CREATE_DATE_LEN;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="fileId"></param>
        /// <param name="fileDivid"></param>
        /// <param name="bankCd"></param>
        /// <param name="extension"></param>
        public FileGenerator(int seq, string fileId, string fileDivid, int bankCd, string extension)
        {
            this.Seq = seq.ToString("D8");
            this.FileId = fileId;
            this.FileDivid = fileDivid;
            this.BankCd = bankCd;
            this.CreateDate = AplInfo.OpDate();
            this.Extension = extension;
            this.FileSize = 0;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName"></param>
        public FileGenerator(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            if (name.Length < FILE_NAME_MAX_LEN) { return; }
            int idx = 0;

            FileId = name.Substring(idx, FILE_ID_LEN);
            idx += FILE_ID_LEN;

            FileDivid = name.Substring(idx, FILE_DIVID_LEN);
            idx += FILE_DIVID_LEN;

            BankCd = DBConvert.ToIntNull(name.Substring(idx, BANK_CD_LEN));
            idx += BANK_CD_LEN;

            CreateDate = DBConvert.ToIntNull(name.Substring(idx, CREATE_DATE_LEN));
            idx += CREATE_DATE_LEN;

            Seq = name.Substring(idx, SEQ_LEN);
            idx += SEQ_LEN;

            Extension = extension;
        }

        /// <summary>
        /// テキストに書き込みを行う（BOMなしUTF8、上書き）
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="msg"></param>
        public static void WriteAllTextStream(string filePath, string msg)
        {
            Encoding utf8 = new UTF8Encoding(false);
            using (StreamWriter sw = new StreamWriter(filePath, false, utf8))
            {
                sw.Write(msg);
                sw.Close();
            }
        }

        /// <summary>
        /// ファイルサイズを設定する
        /// </summary>
        /// <param name="filePath"></param>
        public void SetFileSize(string filePath)
        {
            // File.Exists() はえらい時間かかるのでやらない
            // 基本的にファイルがあるものとして処理し、存在しない場合はそのまま例外にする
            FileInfo file = new FileInfo(filePath);
            this.FileSize = file.Length;
        }
    }
}
