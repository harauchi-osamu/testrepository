using CommonTable.DB;
using EntryCommon;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SearchIcMeiView
{
    class IcMeiListCommon
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
        /// 状態画面表示データ整形
        /// </summary>
        public static string DispInputStsFormat(ItemManager.DetailData Detail)
        {
            if (Detail.JikouInptSts > HoseiStatus.InputStatus.完了)
            {
                // 自行情報が完了より大きい場合はそちらを優先
                return string.Format("自行情報{0}", HoseiStatus.InputStatus.GetName(Detail.JikouInptSts));
            }
            if (Detail.KoukanjiriInptSts > HoseiStatus.InputStatus.完了)
            {
                // 交換尻訂正が完了より大きい場合はそちらを優先
                return string.Format("交換尻{0}", HoseiStatus.InputStatus.GetName(Detail.KoukanjiriInptSts));
            }

            if (Detail.ICBKInptSts == HoseiStatus.InputStatus.完了 && Detail.CDATEInptSts == HoseiStatus.InputStatus.完了 && Detail.AMTInptSts == HoseiStatus.InputStatus.完了 
                     && Detail.JikouInptSts == HoseiStatus.InputStatus.完了)
            {
                // すべて完了の場合は完了
                return "入力完了";
            }
            else if (Detail.JikouInptSts > HoseiStatus.InputStatus.エントリ待 && Detail.JikouInptSts < HoseiStatus.InputStatus.完了)
            {
                // 自行情報が入力中の場合は自行情報入力中
                return "自行情報入力中";
            }
            else if (Detail.ICBKInptSts == HoseiStatus.InputStatus.完了 && Detail.CDATEInptSts == HoseiStatus.InputStatus.完了 && Detail.AMTInptSts == HoseiStatus.InputStatus.完了 
                     && Detail.JikouInptSts == HoseiStatus.InputStatus.エントリ待)
            {
                // 交換尻が完了自行情報がエントリ待の場合は自行情報入力待ち
                return "自行情報入力待ち";
            }
            else if (Detail.ICBKInptSts == HoseiStatus.InputStatus.エントリ待 && Detail.CDATEInptSts == HoseiStatus.InputStatus.エントリ待 && Detail.AMTInptSts == HoseiStatus.InputStatus.エントリ待)
            {
                // すべてエントリー待の場合は入力待ち
                return "交換尻入力待ち";
            }
            else
            {
                // 上記以外は入力中
                return "交換尻入力中";
            }
        }

        /// <summary>
        /// 訂正有無画面表示データ整形
        /// </summary>
        public static string DispTeiseiFlgFormat(ItemManager.DetailData Detail)
        {
            if (Detail.ICBKInptSts != HoseiStatus.InputStatus.完了 || Detail.CDATEInptSts != HoseiStatus.InputStatus.完了 || Detail.AMTInptSts != HoseiStatus.InputStatus.完了)
            {
                // 交換尻未補正の場合
                return string.Empty;
            }

            if (Detail.CTRICBKNo != Detail.ICBKNo || Detail.CTRClearingDate != Detail.ClearingDate || Detail.CTRAmount != Detail.Amount)
            {
                return "訂正あり";
            }
            else
            {
                return "訂正なし";
            }
        }

        /// <summary>
        /// 不渡有無画面表示データ整形
        /// </summary>
        public static string DispFuwatariFlgFormat(ItemManager.DetailData Detail)
        {
            switch (Detail.Fuwatari)
            {
                case GymParam.GymId.持帰:
                    // 不渡データあり
                    if (Detail.FuwatariDelete == 1)
                    {
                        // 削除済
                        return "不渡取り消し";
                    }
                    else
                    {
                        // 未削除
                        return "不渡入力あり";
                    }
                default:
                    // 不渡データなし
                    return "不渡入力なし";
            }
        }

        /// <summary>
        /// 削除状態画面表示データ整形
        /// </summary>
        public static string DispDeleteFlgFormat(ItemManager.DetailData Detail)
        {
            if (Detail.MeiDelete == 1)
            {
                return "削除済み";
            }

            return "未削除";
        }

        /// <summary>
        /// 決済対象区分画面表示データ整形
        /// </summary>
        public static string DispPayKbnFormat(ItemManager.DetailData Detail)
        {
            switch (Detail.PayKbn)
            {
                case "0":
                    return "決済対象";
                case "1":
                    return "決済を伴わないイメージ交換";
                case "2":
                    return "破綻・脱退に伴う一時停止";
                case "3":
                    return "業務停止に伴う一時停止";
                case "4":
                    return "保険事故に伴う一時停止";
                case "5":
                    return "決済受託銀行の一時停止";
                case "9":
                    return "脱退";
                default:
                    return Detail.PayKbn;
            }
        }

        /// <summary>
        /// オペレータ画面表示データ整形
        /// </summary>
        public static string DispOperatorFormat(string OpeNo, ItemManager itemMgr)
        {
            return string.Format("{0} {1}", OpeNo, itemMgr.GeOperator(OpeNo));
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
        /// 検索モード表示
        /// </summary>
        public static void DispSearchSortMode(Label label, ItemManager itemMgr, Controller ctl)
        {
            if (itemMgr.DispParams.DispSortType)
            {
                //印鑑照合
                label.Text = "印鑑照合検索モード";
                label.BorderStyle = BorderStyle.FixedSingle;
                label.BackColor = GetSortColor(ctl.SettingData.InkanSortBackColor);
            }
            else
            {
                //通常
                label.Text = "通常検索モード";
                label.BorderStyle = BorderStyle.None;
                label.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// 印鑑照合検索モードの色取得
        /// </summary>
        private static Color GetSortColor(string backcolor)
        {
            Color rtnColor = SystemColors.Control;
            if (backcolor.StartsWith("0x"))
            {

                if (int.TryParse("FF" + backcolor.Remove(0, 2), System.Globalization.NumberStyles.HexNumber, new System.Globalization.CultureInfo("ja-JP"), out int intColor))
                {
                    rtnColor = Color.FromArgb(intColor);
                }
            }

            return rtnColor;
        }

    }
}
