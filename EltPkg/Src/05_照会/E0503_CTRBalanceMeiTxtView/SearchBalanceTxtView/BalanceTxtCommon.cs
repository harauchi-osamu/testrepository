using CommonTable.DB;
using EntryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBalanceTxtView
{
    class BalanceTxtCommon
    {
        /// <summary>
        /// 画面表示データ整形
        /// </summary>
        public static string DispFormat(string Data, string Format)
        {
            if (!long.TryParse(Data, out long ChgData))
            {
                return Data;
            }

            return ChgData.ToString(Format);
        }

        /// <summary>
        /// 持出時接続方式表示データ整形
        /// </summary>
        public static string DispOCMethod(string OCMethod)
        {
            switch (OCMethod)
            {
                case "1":
                    return "1:Webブラウザ";
                case "2":
                    return "2:ファイル転送";
                case "3":
                    return "3:タブレットアプリ";
                default:
                    return OCMethod;
            }
        }

        /// <summary>
        /// 決済対象区分表示データ整形
        /// </summary>
        public static string DispPayKbn(string PayKbn)
        {
            switch (PayKbn)
            {
                case "0":
                    return "0:決済対象";
                case "1":
                    return "1:決済を伴わないイメージ交換";
                case "2":
                    return "2:破産・脱退に伴う一時停止";
                case "3":
                    return "3:業務停止に伴う一時停止";
                case "4":
                    return "4:保険事故に伴う一時停止";
                case "5":
                    return "5:決済受託銀行の一時停止";
                case "9":
                    return "9:脱退(承継なし)による停止";
                default:
                    return PayKbn;
            }
        }

        /// <summary>
        /// 持帰状況フラグ表示データ整形
        /// </summary>
        public static string DispICFlg(string ICFlg)
        {
            switch (ICFlg)
            {
                case "0":
                    return "0:未持帰";
                case "1":
                    return "1:持帰済";
                default:
                    return ICFlg;
            }
        }

        /// <summary>
        /// 不渡表示データ整形
        /// </summary>
        public static string DispFuwatari(ItemManager.DetailData Detail)
        {
            // パッケージデータがない場合、空文字
            if (!Detail.PackageExists) return string.Empty;

            if (Detail.MeiDelFlg == 1 && (Detail.MeiGRAConfirmDate > 0 || Detail.MeiGRADate > 0))
            {
                // 不渡対象データ場合
                return "●";
            }

            return string.Empty;
        }

        /// <summary>
        /// 日付画面表示データ整形
        /// </summary>
        public static string DispDate(string Date, string DefValue)
        {
            if (!int.TryParse(Date, out int intDate))
            {
                return DefValue;
            }
            return DispDate(intDate, DefValue);
        }

        /// <summary>
        /// 日付画面表示データ整形
        /// </summary>
        public static string DispDate(int Date, string DefValue)
        {
            if (Date > 0)
            {
                return CommonUtil.ConvToDateFormat(Date, 3);
            }
            return DefValue;
        }

        /// <summary>
        /// 決済対象区分チェック
        /// </summary>
        public static bool ChkPayKbn(string PayKbn)
        {
            switch (PayKbn)
            {
                case "0":
                    return true;
                case "":
                    // 空もtrue
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 入力値チェック
        /// </summary>
        public static bool ChkInputData(string CTRData, string PKGData, int Length)
        {
            if (string.IsNullOrEmpty(CTRData))
            {
                CTRData = CommonUtil.PadLeft(CTRData, Length, "Z");
            }
            if (string.IsNullOrEmpty(PKGData))
            {
                PKGData =  CommonUtil.PadLeft(PKGData, Length, "Z");
            }
            if (CTRData == PKGData)
            {
                return true;
            }
            return false;
        }

    }
}
