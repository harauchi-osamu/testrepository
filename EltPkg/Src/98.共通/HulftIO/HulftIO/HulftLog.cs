using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CommonTable.DB;
using EntryCommon;

namespace HulftIO
{
    /// <summary>
    /// HULFT集配信ログ（V84フォーマット）
    /// </summary>
    public class HulftLog
    {
        /// <summary>配信ログ</summary>
        public SortedDictionary<int, RecordInfo> SendLogRecords { get; set; }
        /// <summary>配信エラーログ有無</summary>
        public bool IsSendErr { get; private set; }
        /// <summary>集信ログ</summary>
        public SortedDictionary<int, RecordInfo> RecvLogRecords { get; set; }
        /// <summary>集信エラーログ有無</summary>
        public bool IsRecvErr { get; private set; }
        /// <summary>集信ログ (FILE_CTLに存在してログファイルにないデータ)</summary>
        public SortedDictionary<int, RecordInfo> FCtlRecvLogRecords { get; set; }

        /// <summary>
        /// 集信ログ
        /// RecvLogRecordsとFCtlRecvLogRecordsの合計
        /// </summary>
        public Dictionary<int, RecordInfo> AllRecvLogRecords
        {
            get
            {
                // ファイル名単位で重複は除外
                return RecvLogRecords.Concat(FCtlRecvLogRecords).Distinct(new LogRecEqualityComparer()).OrderBy(x => x.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
                //return RecvLogRecords.Concat(FCtlRecvLogRecords).OrderBy(x => x.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }

        private string _sendLogPath = null;
        private string _recvLogPath = null;
        private string _sendErrLogPath = null;
        private string _recvErrLogPath = null;

        // ****************************
        // ログ項目桁数
        // ****************************
        private const int ファイルID_LEN = 50;
        private const int ホスト名_LEN = 68;
        private const int 集配信開始日_LEN = 10;
        private const int 集配信開始時刻_LEN = 8;
        private const int 集配信終了時刻_LEN = 8;
        private const int レコード件数_LEN = 18;
        private const int データサイズ_LEN = 18;
        private const int ステータス_LEN = 12;
        private const int 接続形態_LEN = 3;
        private const int ファイル名_LEN = 200;
        private const int 区切り文字_LEN = 2;    // 半角空白2桁
        private int RECORD_MAX_SIZE = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HulftLog(string sendLogPath, string recvLogPath, string sendErrLogPath, string recvErrLogPath)
        {
            _sendLogPath = sendLogPath;
            _recvLogPath = recvLogPath;
            _sendErrLogPath = sendErrLogPath;
            _recvErrLogPath = recvErrLogPath;

            SendLogRecords = new SortedDictionary<int, RecordInfo>();
            IsSendErr = false;
            RecvLogRecords = new SortedDictionary<int, RecordInfo>();
            FCtlRecvLogRecords = new SortedDictionary<int, RecordInfo>();
            IsRecvErr = false;
            RECORD_MAX_SIZE = 0;
            RECORD_MAX_SIZE += ファイルID_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += ホスト名_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += 集配信開始日_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += 集配信開始時刻_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += 集配信終了時刻_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += レコード件数_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += データサイズ_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += ステータス_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += 接続形態_LEN + 区切り文字_LEN;
            RECORD_MAX_SIZE += ファイル名_LEN;
        }

        /// <summary>
        /// 配信ログを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadSendLog()
        {
            if (!File.Exists(_sendLogPath)) { return; }

            SendLogRecords = new SortedDictionary<int, RecordInfo>();
            string[] lines = CommonUtil.ReadTextStream(_sendLogPath);
            for (int i = 0; i < lines.Length; i++)
            {
                int no = i + 1;
                string line = lines[i];
                if (line.Length < RECORD_MAX_SIZE) { continue; }
                RecordInfo rec = ReadLine(no, line);
                rec.FGen = new FileGenerator(Path.GetFileName(rec.ファイルパス));
                SendLogRecords.Add(no, rec);
            }
        }

        /// <summary>
        /// 配信エラーログを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadSendErrLog()
        {
            if (!File.Exists(_sendErrLogPath)) { return; }

            IsSendErr = File.Exists(_sendErrLogPath);
        }

        /// <summary>
        /// 配信エラーログを削除する
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteSendErrLog()
        {
            CommonUtil.DeleteFile(_sendErrLogPath);
            IsSendErr = false;
        }

        /// <summary>
        /// 集信ログを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadRecvLog()
        {
            if (!File.Exists(_recvLogPath)) { return; }

            RecvLogRecords = new SortedDictionary<int, RecordInfo>();
            string[] lines = CommonUtil.ReadTextStream(_recvLogPath);
            for (int i = 0; i < lines.Length; i++)
            {
                int no = i + 1;
                string line = lines[i];
                if (line.Length < RECORD_MAX_SIZE) { continue; }
                RecordInfo rec = ReadLine(no, line);
                rec.FGen = new FileGenerator(Path.GetFileName(rec.ファイルパス));
                RecvLogRecords.Add(no, rec);
            }
        }

        /// <summary>
        /// 集信ログを読み込む
        /// FILE_CTLに存在してログファイルにないデータを読み込む
        /// </summary>
        /// <param name="file_ctls"></param>
        public void ReadFCtlRecvLog(DataTable file_ctls)
        {
            FCtlRecvLogRecords = new SortedDictionary<int, RecordInfo>();

            // ログファイルのファイル一覧作成
            IEnumerable<string> files = RecvLogRecords.Values.Select(x => x.FGen.FileName);

            // FILE_CTLテーブルから「ログファイルにない」「取込完了・取込保留でない」データを抽出 (送信日時でソート[送信日時が空は後表示])
            var DataRows = file_ctls.AsEnumerable()
                             .Where(x => !files.Contains(DBConvert.ToStringNull(x[TBL_FILE_CTL.CAP_FILE_NAME])) &&
                                         ( DBConvert.ToIntNull(x[TBL_FILE_CTL.CAP_STS]) != FileCtl.CapSts.取込完了 &&
                                           DBConvert.ToIntNull(x[TBL_FILE_CTL.CAP_STS]) != FileCtl.CapSts.取込保留 ) )
                             .OrderBy(x => ConvDATE(x[TBL_FILE_CTL.SEND_DATE]))
                             .ThenBy(x => ConvTIME(x[TBL_FILE_CTL.SEND_TIME]));

            // RecordInfoのNo作成(集信ログの最大値 + 1)
            int RecNo = 1;
            if (RecvLogRecords.Count >= 1)
            {
                RecNo += RecvLogRecords.Keys.Max();
            }

            foreach (DataRow dr in DataRows)
            {
                // RecordInfo作成
                RecordInfo rec = new RecordInfo(RecNo);
                rec.ステータス = "000000-00000"; // ステータスに正常コード設定

                // FILE_CTL設定
                rec.fctl = new TBL_FILE_CTL(dr, AppInfo.Setting.SchemaBankCD);

                // FileGenerator設定
                rec.FGen = new FileGenerator(rec.fctl._CAP_FILE_NAME);

                // 追加
                FCtlRecvLogRecords.Add(RecNo, rec);

                RecNo++;
            }
        }

        /// <summary>
        /// 日付変換
        /// </summary>
        private long ConvDATE(object obj)
        {
            long date = DBConvert.ToLongNull(obj);
            if (date == 0) date = 99999999;
            return date;
        }

        /// <summary>
        /// 日付変換
        /// </summary>
        private long ConvTIME(object obj)
        {
            long time = DBConvert.ToLongNull(obj);
            if (time == 0) time = 999999999;
            return time;
        }

        /// <summary>
        /// 集信エラーログを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadRecvErrLog()
        {
            if (!File.Exists(_recvErrLogPath)) { return; }

            IsRecvErr = File.Exists(_recvErrLogPath);
        }

        /// <summary>
        /// 配信エラーログを削除する
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteRecvErrLog()
        {
            CommonUtil.DeleteFile(_recvErrLogPath);
            IsRecvErr = false;
        }

        /// <summary>
        /// 集配信ログの1レコードを読み込む
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private RecordInfo ReadLine(int no, string line)
        {
            int idx = 0;
            RecordInfo rec = new RecordInfo(no);
            rec.Line = line;

            rec.ファイルID = line.Substring(idx, ファイルID_LEN).TrimEnd();
            idx += ファイルID_LEN + 区切り文字_LEN;

            rec.ホスト名 = line.Substring(idx, ホスト名_LEN).TrimEnd();
            idx += ホスト名_LEN + 区切り文字_LEN;

            rec.集配信開始日 = line.Substring(idx, 集配信開始日_LEN).TrimEnd();
            idx += 集配信開始日_LEN + 区切り文字_LEN;

            rec.集配信開始時刻 = line.Substring(idx, 集配信開始時刻_LEN).TrimEnd();
            idx += 集配信開始時刻_LEN + 区切り文字_LEN;

            rec.集配信終了時刻 = line.Substring(idx, 集配信終了時刻_LEN).TrimEnd();
            idx += 集配信終了時刻_LEN + 区切り文字_LEN;

            rec.レコード件数 = DBConvert.ToLongNull(line.Substring(idx, レコード件数_LEN).TrimEnd());
            idx += レコード件数_LEN + 区切り文字_LEN;

            rec.データサイズ = DBConvert.ToLongNull(line.Substring(idx, データサイズ_LEN).TrimEnd());
            idx += データサイズ_LEN + 区切り文字_LEN;

            rec.ステータス = line.Substring(idx, ステータス_LEN).TrimEnd();
            idx += ステータス_LEN + 区切り文字_LEN;

            rec.接続形態 = line.Substring(idx, 接続形態_LEN).TrimEnd();
            idx += 接続形態_LEN + 区切り文字_LEN;

            rec.ファイルパス = line.Substring(idx, ファイル名_LEN).TrimEnd();
            idx += ファイル名_LEN + 区切り文字_LEN;

            return rec;
        }

        /// <summary>
        /// 集配信ログ情報（V84フォーマット）
        /// www.hulft.com/help/ja-jp/HULFT-V8/WIN-OPE/Content/HULFT_OPE/HULFTOpeCmd/SndLog_RcvLogListDipCmd.htm#TBL_V84
        /// </summary>
        public class RecordInfo
        {
            public int No { get; private set; } = 0;
            public string Line { get; set; } = "";

            public string ファイルID { get; set; } = "";
            public string ホスト名 { get; set; } = "";
            public string 集配信開始日 { get; set; } = "";
            public string 集配信開始時刻 { get; set; } = "";
            public string 集配信終了時刻 { get; set; } = "";
            public long レコード件数 { get; set; } = 0;
            public long データサイズ { get; set; } = 0;
            public string ステータス { get; set; } = "";
            public string 接続形態 { get; set; } = "";
            public string ファイルパス { get; set; } = "";

            public FileGenerator FGen { get; set; } = null;
            public TBL_FILE_CTL fctl { get; set; } = null;

            public bool IsErr { get { return !ステータス.Equals("000000-00000"); } }

            public RecordInfo(int no)
            {
                No = no;
            }
        }

        /// <summary>
        /// RecordInfoのKeyValuePair比較クラス
        /// </summary>
        private class LogRecEqualityComparer : IEqualityComparer<KeyValuePair<int, RecordInfo>>
        {
            public bool Equals(KeyValuePair<int, RecordInfo> info1, KeyValuePair<int, RecordInfo> info2)
            {
                // ファイル名が同じ場合、Equal
                return info1.Value.FGen.FileName == info2.Value.FGen.FileName;
            }

            public int GetHashCode(KeyValuePair<int, RecordInfo> info)
            {
                return info.Value.FGen.FileName.GetHashCode();
            }
        }

    }
}
