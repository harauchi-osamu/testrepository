using CommonTable.DB;
using EntryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchOcMeiView
{
    public class OcMeiListCommon
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
        /// エントリー状態画面表示データ整形
        /// </summary>
        public static string DispEntryStsFormat(ItemManager.DetailData Detail)
        {
            if (Detail.TeiseiInptSts > HoseiStatus.InputStatus.完了)
            {
                // 交換尻訂正が完了より大きい場合はそちらを優先
                return HoseiStatus.InputStatus.GetName(Detail.TeiseiInptSts);
            }

            if (Detail.ICBKInptSts == HoseiStatus.InputStatus.エントリ待 && Detail.AmountInptSts == HoseiStatus.InputStatus.エントリ待)
            {
                // 持帰銀行・金額共にエントリー待の場合はエントリー待
                return HoseiStatus.InputStatus.GetName(HoseiStatus.InputStatus.エントリ待);
            }
            else if (Detail.ICBKInptSts == HoseiStatus.InputStatus.完了 && Detail.AmountInptSts == HoseiStatus.InputStatus.完了)
            {
                // 持帰銀行・金額共にエントリー完了の場合は完了
                return HoseiStatus.InputStatus.GetName(HoseiStatus.InputStatus.完了);
            }
            else
            {
                // 上記以外はエントリー中
                return HoseiStatus.InputStatus.GetName(HoseiStatus.InputStatus.エントリ中);
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
    }
}
