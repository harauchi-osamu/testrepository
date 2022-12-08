namespace CommonTable.DB
{
    /// <summary>GYM_PARAM</summary>
    public class GymParam
    {
        /// <summary>業務番号</summary>
        public class GymId
        {
            public const int 共通 = 0;
            public const int 持出 = 1;
            public const int 持帰 = 2;

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case 持出:
                        retVal = "持出";
                        break;
                    case 持帰:
                        retVal = "持帰";
                        break;
                }
                return retVal;
            }
        }
    }

    public class FileParam
    {
        public class FileId
        {
            public const string IF101 = "IF101";
            public const string IF201 = "IF201";
            public const string IF202 = "IF202";
            public const string IF203 = "IF203";
            public const string IF204 = "IF204";
            public const string IF205 = "IF205";
            public const string IF206 = "IF206";
            public const string IF207 = "IF207";
            public const string IF208 = "IF208";
            public const string IF209 = "IF209";
            public const string IF210 = "IF210";
            public const string IF211 = "IF211";
        }

        public class FileKbn
        {
            public const string BCA = "BCA";
            public const string BQA = "BQA";
            public const string BUA = "BUA";
            public const string BUB = "BUB";
            public const string GDA = "GDA";
            public const string GMA = "GMA";
            public const string GMB = "GMB";
            public const string GRA = "GRA";
            public const string GXA = "GXA";
            public const string GXB = "GXB";
            public const string MRA = "MRA";
            public const string MRB = "MRB";
            public const string MRC = "MRC";
            public const string MRD = "MRD";
            public const string SFA = "SFA";
            public const string SFB = "SFB";
            public const string SPA = "SPA";
            public const string SPB = "SPB";
            public const string YCA = "YCA";
            public const string YSA = "YSA";
            public const string YSB = "YSB";
            public const string YSC = "YSC";
        }
    }

    /// <summary>TRBATCH</summary>
    public class TrBatch
    {
        public class InputRoute
        {
            public const int 通常 = 1;
            public const int 付帯 = 2;
            public const int 期日管理 = 3;
        }
        public class Sts
        {
            public const int 入力待ち = 0;
            public const int 入力中 = 1;
            public const int 入力保留 = 5;
            public const int 入力完了 = 10;
        }
    }

    /// <summary>TRMEI</summary>
    public class TrMei
    {
        public class Sts
        {
            public const int 未作成 = 0;
            public const int 再作成対象 = 1; // 持出取消では作成対象
            public const int 作成対象 = 1; // 持出取消で使用
            public const int ファイル作成 = 5;
            public const int アップロード = 10;
            public const int 結果エラー = 19;
            public const int 結果正常 = 20;

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case 未作成:
                        retVal = "未作成";
                        break;
                    case 再作成対象:
                        retVal = "再作成対象";
                        break;
                    case ファイル作成:
                        retVal = "ファイル作成";
                        break;
                    case アップロード:
                        retVal = "アップロード";
                        break;
                    case 結果エラー:
                        retVal = "結果エラー";
                        break;
                    case 結果正常:
                        retVal = "結果正常";
                        break;
                }
                return retVal;
            }

            public static string GetNameOCCancel(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case 作成対象:
                        retVal = "作成対象";
                        break;
                    default:
                        retVal = GetName(cd);
                        break;
                }
                return retVal;
            }

        }
    }

    /// <summary>TRMEIIMG</summary>
    public class TrMeiImg
    {
        public class ImgKbn
        {
            public const int 表 = 1;
            public const int 裏 = 2;
            public const int 補箋 = 3;
            public const int 付箋 = 4;
            public const int 入金証明 = 5;
            public const int 表再送分 = 6;
            public const int 裏再送分 = 7;
            public const int その他1 = 8;
            public const int その他2 = 9;
            public const int その他3 = 10;
            public const int 予備1 = 11;
            public const int 予備2 = 12;
            public const int 予備3 = 13;

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case 表:
                        retVal = "表";
                        break;
                    case 裏:
                        retVal = "裏";
                        break;
                    case 補箋:
                        retVal = "補箋";
                        break;
                    case 付箋:
                        retVal = "付箋";
                        break;
                    case 入金証明:
                        retVal = "入金証明";
                        break;
                    case 表再送分:
                        retVal = "表再送分";
                        break;
                    case 裏再送分:
                        retVal = "裏再送分";
                        break;
                    case その他1:
                        retVal = "その他1";
                        break;
                    case その他2:
                        retVal = "その他2";
                        break;
                    case その他3:
                        retVal = "その他3";
                        break;
                    case 予備1:
                        retVal = "予備1";
                        break;
                    case 予備2:
                        retVal = "予備2";
                        break;
                    case 予備3:
                        retVal = "予備3";
                        break;
                }
                return retVal;
            }
        }
    }

    /// <summary>TRFUWATARI</summary>
    public class TrFuwatari
    {
        public class FubiKbn
        {
            public const int 不渡0号 = 0;
            public const int 不渡1号 = 1;
            public const int 不渡2号 = 2;
        }

        public class GraSts
        {
            public const int 未作成 = 0;
            public const int ファイル作成 = 1;
            public const int ファイル作成エラー = 9;
            public const int アップロード = 10;
            public const int 結果エラー = 19;
            public const int 結果正常 = 20;
        }

        public class Status
        {
            public const int 入力待ち = 0;
            public const int 入力中 = 1;
            public const int 入力保留 = 5;
            public const int 入力完了 = 10;

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case 入力待ち:
                        retVal = "入力待ち";
                        break;
                    case 入力中:
                        retVal = "入力中";
                        break;
                    case 入力保留:
                        retVal = "入力保留";
                        break;
                    case 入力完了:
                        retVal = "入力完了";
                        break;
                }
                return retVal;
            }
        }
    }

    /// <summary>TRITEM</summary>
    public class TrItem
    {
        public class FixTrigger
        {
            public const string 補正エントリ = "補正エントリ";
        }
    }

    /// <summary>HOSEIMODE_PARAM</summary>
    public class HoseiParam
    {
        public class HoseiItemMode
        {
            public const int 未定義 = 0;
            public const int 持帰銀行 = 1;
            public const int 交換希望日 = 2;
            public const int 金額 = 3;
            public const int 自行情報 = 4;
            public const int 交換尻 = 5;

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case 持帰銀行:
                        retVal = "持帰銀行";
                        break;
                    case 交換希望日:
                        retVal = "交換希望日";
                        break;
                    case 金額:
                        retVal = "金額";
                        break;
                    case 自行情報:
                        retVal = "自行情報";
                        break;
                    case 交換尻:
                        retVal = "交換尻";
                        break;
                }
                return retVal;
            }
        }
    }

    /// <summary>HOSEI_STATUS</summary>
    public class HoseiStatus
    {
        public class HoseiInputMode
        {
            public const int 持帰銀行 = 1;
            public const int 交換希望日 = 2;
            public const int 金額 = 3;
            public const int 自行情報 = 4;
            public const int 交換尻 = 5;

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case 持帰銀行:
                        retVal = "持帰銀行";
                        break;
                    case 交換希望日:
                        retVal = "交換希望日";
                        break;
                    case 金額:
                        retVal = "金額";
                        break;
                    case 自行情報:
                        retVal = "自行情報";
                        break;
                    case 交換尻:
                        retVal = "交換尻";
                        break;
                }
                return retVal;
            }
        }

        public class InputStatus
        {
            public const int エントリ待 = 1000;
            public const int エントリ中 = 1001;
            public const int エントリ保留 = 1005;
            public const int ベリファイ待 = 2000;
            public const int ベリファイ中 = 2001;
            public const int ベリファイ保留 = 2005;
            public const int 完了 = 3000;
            public const int 完了訂正中 = 3001;
            public const int 完了訂正保留 = 3005;
            public const int 持出エラー訂正中 = 4001; // 廃止
            public const int 持出エラー訂正保留 = 4005; // 廃止

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case エントリ待:
                        retVal = "エントリ待";
                        break;
                    case エントリ中:
                        retVal = "エントリ中";
                        break;
                    case エントリ保留:
                        retVal = "エントリ保留";
                        break;
                    case ベリファイ待:
                        retVal = "ベリファイ待";
                        break;
                    case ベリファイ中:
                        retVal = "ベリファイ中";
                        break;
                    case ベリファイ保留:
                        retVal = "ベリファイ保留";
                        break;
                    case 完了:
                        retVal = "完了";
                        break;
                    case 完了訂正中:
                        retVal = "完了訂正中";
                        break;
                    case 完了訂正保留:
                        retVal = "完了訂正保留";
                        break;
                }
                return retVal;
            }
        }
    }

    /// <summary>DSP_ITEM</summary>
    public class DspItem
    {
        public class ItemId
        {
            public const int 持帰銀行コード = 1;
            public const int 持帰銀行名 = 2;
            public const int 入力交換希望日 = 3;
            public const int 和暦交換希望日 = 4;
            public const int 交換日 = 5;
            public const int 金額 = 6;
            public const int 決済フラグ = 7;
            public const int 交換証券種類コード = 8;
            public const int 交換証券種類名 = 9;
            public const int 手形種類コード = 10;
            public const int 手形種類名 = 11;
            public const int 券面持帰支店コード = 12;
            public const int 持帰支店コード = 13;
            public const int 持帰支店名 = 14;
            public const int 券面口座番号 = 15;
            public const int 口座番号 = 16;
            public const int 支払人名 = 17;
            public const int 手形番号 = 18;
            public const int 券面持帰銀行コード = 19;
            public const int 最終項目 = 999;
        }

        public class ItemType
        {
            /// <summary>英数字（スペース含む）</summary>
            public const string A = "A";
            /// <summary>数字</summary>
            public const string N = "N";
            /// <summary>数字と半角ハイフン</summary>
            public const string S = "S";
            /// <summary>数字と半角空白</summary>
            public const string R = "R";
            /// <summary>数字と半角空白と半角ハイフン</summary>
            public const string T = "T";
            /// <summary>英数字とカナ</summary>
            public const string K = "K";
            /// <summary>基準がよく分からない漢字たち</summary>
            public const string J = "J";
            /// <summary>画面表示しない定数値（ファイル出力）</summary>
            public const string C = "C";
            /// <summary>画面表示のみ（ラベル）</summary>
            public const string AST = "*";
            /// <summary>ダミーテキストボックス（DB登録しない）</summary>
            public const string D = "D";
            /// <summary>読取専用テキストボックス</summary>
            public const string V = "V";
            /// <summary>読取専用テキストボックス(右揃え)</summary>
            public const string W = "W";
        }
    }

    public class FileCtl
    {
        public class SendSts
        {
            public const int ファイル作成 = 0;
            public const int 配信中 = 1;
            public const int 配信エラー = 9;
            public const int 配信済 = 10;

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case ファイル作成:
                        retVal = "ファイル作成";
                        break;
                    case 配信中:
                        retVal = "配信中";
                        break;
                    case 配信エラー:
                        retVal = "配信エラー";
                        break;
                    case 配信済:
                        retVal = "配信済";
                        break;
                }
                return retVal;
            }
        }

        public class CapSts
        {
            public const int 未取込 = 0;
            public const int 取込中 = 1;
            public const int 取込保留 = 5;
            public const int 取込エラー = 9;
            public const int 取込完了 = 10;

            public static string GetName(int cd)
            {
                string retVal = "";
                switch (cd)
                {
                    case 未取込:
                        retVal = "未取込";
                        break;
                    case 取込中:
                        retVal = "取込中";
                        break;
                    case 取込保留:
                        retVal = "取込保留";
                        break;
                    case 取込エラー:
                        retVal = "取込エラー";
                        break;
                    case 取込完了:
                        retVal = "取込完了";
                        break;
                }
                return retVal;
            }
        }
    }

    public class IcReqRetCtl
    {
        public class CapKbn
        {
            public const int 電子交換所 = 0;
            public const int 行内交換連携 = 1;
        }

        public class Sts
        {
            public const int 対象外 = -1;
            public const int 未取込 = 0;
            public const int OCR待 = 1;
            public const int ダウンロード確定待 = 5;
            public const int ダウンロード確定済 = 10;
        }
    }

    /// <summary>SUB_RTN</summary>
    public class SubRtn
    {
        /// <summary>サブルーチン：IMB文字コード変換</summary>
        public const string CONV = "CONV";
        /// <summary>サブルーチン：パック</summary>
        public const string PCK = "PCK";
        public class SubKbn
        {
            public const int ItemCheck = 1;
            public const int ItemReplace = 2;
        }
    }
}
