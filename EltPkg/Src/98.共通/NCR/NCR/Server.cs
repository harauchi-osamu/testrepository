namespace NCR
{
    public class Server
    {
        private static string setIniPath = string.Empty;
        public static string IniPath
        {
            get
            {
                if (string.IsNullOrEmpty(setIniPath))
                {
                    return NCR.Terminal.ServeriniPath;
                }
                else
                {
                    return setIniPath;
                }
            }
            set
            {
                setIniPath = value;
                // Hulft関連設定にパス設定
                NCR.Hulft.IniPath = value;
            }
        }

        public static string HULFTLogSectionName
        {
            get
            {
                return NCR.Hulft.HULFTLogSectionName;
            }
            set
            {
                NCR.Hulft.HULFTLogSectionName = value;
            }
        }

        static Server()
        {
            // Hulft関連設定にパス設定
            NCR.Hulft.IniPath = IniPath;
        }

        // *******************************************************************
        // COMMON
        // *******************************************************************
        /// <summary>
        /// 銀行コード
        /// </summary>
        public static int BankCD
        {
            get { return Operator.BankCD; }
        }
        /// <summary>
        /// 銀行名
        /// </summary>
        public static string BankNM
        {
            get { return Operator.BankNM; }
        }
        /// <summary>
        /// 環境
        /// </summary>
        public static string Environment
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "Environment"); }
        }
        /// <summary>
        /// 接続先DB
        /// </summary>
        public static string DBDataSource
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "DBDataSource"); }
        }
        /// <summary>
        /// 接続先DBユーザー
        /// </summary>
        public static string DBUserID
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "DBUserID"); }
        }
        /// <summary>
        /// 接続先DBパスワード
        /// </summary>
        public static string DBPassword
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "DBPassword"); }
        }
        /// <summary>
        /// プログラムパス
        /// </summary>
        public static string ExePath
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "EXECPATH"); }
        }
        /// <summary>
        /// ログ出力パス
        /// </summary>
        public static string EXELogRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "EXELogRoot", @"\..\LOG\"); }
        }
        /// <summary>
        /// ヘッダー背景色
        /// </summary>
        public static int BackColor
        {
            get { return IniFileAccess.GetKeyByInt(IniPath, "Common", "BackColor"); }
        }

        // *******************************************************************
        // HULFT
        // *******************************************************************
        /// <summary>
        /// HULFTプログラム
        /// </summary>
        public static string HulftExePath
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "HULFT", "EXECPATH"); }
        }
        /// <summary>
        /// HULFT集信フォルダ
        /// </summary>
        public static string ReceiveRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "HULFT", "ReceiveRoot"); }
        }
        /// <summary>
        /// HULFT配信フォルダ
        /// </summary>
        public static string SendRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "HULFT", "SendRoot"); }
        }
        /// <summary>
        /// 配信HULFTID
        /// </summary>
        public static string SHulftID
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "HULFT", "SHULFTID"); }
        }
        /// <summary>
        /// 集信HULFTID
        /// </summary>
        public static string RHulftID
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "HULFT", "RHULFTID"); }
        }
        ///// <summary>
        ///// HULFT集信退避フォルダ
        ///// </summary>
        //public static string BackupRoot
        //{
        //    get { return IniFileAccess.GetKeyByString(IniPath, "HULFT", "BackupRoot"); }
        //}
        ///// <summary>
        ///// HULFT集配信ログフォルダ
        ///// </summary>
        //public static string LogRoot
        //{
        //    get { return IniFileAccess.GetKeyByString(IniPath, "HULFT", "LogRoot"); }
        //}
        ///// <summary>
        ///// HULFT集配信エラーログフォルダ
        ///// </summary>
        //public static string LogErrRoot
        //{
        //    get { return IniFileAccess.GetKeyByString(IniPath, "HULFT", "LogErrRoot"); }
        //}

        // *******************************************************************
        // HULFT(セクション別設定)
        // *******************************************************************
        /// <summary>
        /// HULFT関連電子交換パッケージプログラムログフォルダ（他端末参照用）
        /// </summary>
        public static string HulftEXELogRoot
        {
            get { return NCR.Hulft.HulftEXELogRoot; }
        }
        /// <summary>
        /// HULFT配信フォルダ（他端末参照用）
        /// </summary>
        public static string RemoteSendRoot
        {
            get { return NCR.Hulft.SendRoot; }
        }
        /// <summary>
        /// HULFT集信フォルダ（他端末参照用）
        /// </summary>
        public static string RemoteReceiveRoot
        {
            get { return NCR.Hulft.ReceiveRoot; }
        }
        /// <summary>
        /// HULFT集信退避フォルダ
        /// </summary>
        public static string BackupRoot
        {
            get { return NCR.Hulft.BackupRoot; }
        }
        /// <summary>
        /// HULFT集配信ログフォルダ
        /// </summary>
        public static string LogRoot
        {
            get { return NCR.Hulft.LogRoot; }
        }
        /// <summary>
        /// HULFT集配信エラーログフォルダ
        /// </summary>
        public static string LogErrRoot
        {
            get { return NCR.Hulft.LogErrRoot; }
        }
        /// <summary>
        /// HULFT集信ログファイル名
        /// </summary>
        public static string LogRecvFileName
        {
            get { return NCR.Hulft.LogRecvFileName; }
        }
        /// <summary>
        /// HULFT集信エラーログファイル名
        /// </summary>
        public static string LogErrRecvFileName
        {
            get { return NCR.Hulft.LogErrRecvFileName; }
        }
        /// <summary>
        /// HULFT配信ログファイル名
        /// </summary>
        public static string LogSendFileName
        {
            get { return NCR.Hulft.LogSendFileName; }
        }
        /// <summary>
        /// HULFT配信エラーログファイル名
        /// </summary>
        public static string LogErrSendFileName
        {
            get { return NCR.Hulft.LogErrSendFileName; }
        }

        // *******************************************************************
        // Bank
        // *******************************************************************
        /// <summary>
        /// 受託元銀行番号
        /// </summary>
        public static string ContractBankCd
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Bank", "ContractBankCd"); }
        }
        /// <summary>
        /// 設定可能な銀行一覧
        /// </summary>
        public static string HandlingBankCdList
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Bank", "HandlingBankCdList"); }
        }
        /// <summary>
        /// 行内交換連携実施可否
        /// </summary>
        public static int InternalExchange
        {
            get { return IniFileAccess.GetKeyByInt(IniPath, "Bank", "InternalExchange"); }
        }

        // *******************************************************************
        // BankIO
        // *******************************************************************
        /// <summary>
        /// IO集信フォルダ(銀行別)
        /// </summary>
        public static string IOReceiveRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankIO", "IOReceiveRoot"); }
        }
        /// <summary>
        /// IO配信フォルダ(銀行別)
        /// </summary>
        public static string IOSendRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankIO", "IOSendRoot"); }
        }
        /// <summary>
        /// IO配信フォルダ(銀行別)BK
        /// </summary>
        public static string IOSendRootBk
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankIO", "IOSendRootBk"); }
        }

        // *******************************************************************
        // ScanImage
        // *******************************************************************
        /// <summary>
        /// 通常バッチルート情報(スキャン)
        /// </summary>
        public static string ScanNormalImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ScanImage", "NormalImageRoot"); }
        }
        /// <summary>
        /// 付帯バッチルート情報(スキャン)
        /// </summary>
        public static string ScanFutaiImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ScanImage", "FutaiImageRoot"); }
        }
        /// <summary>
        /// 期日管理バッチルート情報(スキャン)
        /// </summary>
        public static string ScanInventoryImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ScanImage", "InventoryImageRoot"); }
        }
        /// <summary>
        /// 持出支店別合計票パス情報(スキャン)
        /// </summary>
        public static string ScanTotalImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ScanImage", "TotalImageRoot"); }
        }
        /// <summary>
        /// スキャン退避フォルダ情報
        /// </summary>
        public static string ScanImageBackUpRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ScanImage", "ImageBackUpRoot"); }
        }
        /// <summary>
        /// スキャン差替フォルダ情報
        /// </summary>
        public static string ScanImageReplaceRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ScanImage", "ImageReplaceRoot"); }
        }
        /// <summary>
        /// 持帰OCR取込用ルート情報
        /// </summary>
        public static string ScanImageICOCRRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ScanImage","ImageICOCRRoot"); }
        }

        // *******************************************************************
        // BankImage
        // *******************************************************************
        /// <summary>
        /// 通常バッチルート情報(銀行別)
        /// </summary>
        public static string BankNormalImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankImage", "NormalImageRoot"); }
        }
        /// <summary>
        /// 付帯バッチルート情報(銀行別)
        /// </summary>
        public static string BankFutaiImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankImage", "FutaiImageRoot"); }
        }
        /// <summary>
        /// 期日管理バッチルート情報(銀行別)
        /// </summary>
        public static string BankInventoryImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankImage", "InventoryImageRoot"); }
        }
        /// <summary>
        /// 持出支店別合計票パス情報(銀行別)
        /// </summary>
        public static string BankTotalImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankImage", "TotalImageRoot"); }
        }
        /// <summary>
        /// 持帰ダウンロード確定前イメージルート(銀行別)
        /// </summary>
        public static string BankCheckImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankImage", "CheckImageRoot"); }
        }
        /// <summary>
        /// 持帰ダウンロード確定イメージルート(銀行別)
        /// </summary>
        public static string BankConfirmImageRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankImage", "ConfirmImageRoot"); }
        }
        /// <summary>
        /// サンプルイメージパス
        /// </summary>
        public static string BankSampleImagePath
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "BankImage", "SampleImagePath"); }
        }

        // *******************************************************************
        // ImageCut
        // *******************************************************************
        /// <summary>
        /// イメージ切取手形枠
        /// </summary>
        public static string Tegata
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ImageCut", "Tegata"); }
        }
        /// <summary>
        /// イメージ切取小切手枠
        /// </summary>
        public static string Kogitte
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ImageCut", "Kogitte"); }
        }
        /// <summary>
        /// イメージ切取解像度
        /// </summary>
        public static string DstDpi
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ImageCut", "DstDpi"); }
        }
        /// <summary>
        /// イメージ切取クオリティ
        /// </summary>
        public static string Quality
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "ImageCut", "Quality"); }
        }

        // *******************************************************************
        // OCR
        // *******************************************************************
        /// <summary>
        /// OCRオプション導入
        /// </summary>
        public static int OCROption
        {
            get { return IniFileAccess.GetKeyByInt(IniPath, "OCR", "OCROption"); }
        }
        /// <summary>
        /// OCR信頼度比較(持出)
        /// </summary>
        public static int OC_OCRLevel
        {
            get { return IniFileAccess.GetKeyByInt(IniPath, "OCR", "OC_OCRLevel"); }
        }
        /// <summary>
        /// OCR信頼度比較(持帰)
        /// </summary>
        public static int IC_OCRLevel
        {
            get { return IniFileAccess.GetKeyByInt(IniPath, "OCR", "IC_OCRLevel"); }
        }
        /// <summary>
        /// 持帰OCRコピー件数単位
        /// </summary>
        public static int ICOCRCopyUnitCount
        {
            get { return IniFileAccess.GetKeyByInt(IniPath, "OCR", "ICOCRCopyUnitCount"); }
        }

        // *******************************************************************
        // REPORT
        // *******************************************************************
        /// <summary>
        /// レポートパス
        /// </summary>
        public static string ReportPath
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "REPORT", "ReportPath"); }
        }
        /// <summary>
        /// レポートパスログ
        /// </summary>
        public static string ReportLog
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "REPORT", "ReportLog"); }
        }
        /// <summary>
        /// ファイル出力パス
        /// </summary>
        public static string ReportFileOutPutPath
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "REPORT", "FileOutPutPath"); }
        }
    }
}
