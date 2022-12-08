using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace SearchBalance
{
    /// <summary>
    /// 共通処理クラス
    /// </summary>
    public class SearchBalanceCommon
    {
        /// <summary>
        /// 情報欄更新
        /// </summary>
        public static void UpdatelInfo(Control BKNO, Control OPDate)
        {
            BKNO.Text = NCR.Operator.BankCD.ToString("D4");
            OPDate.Text = CommonUtil.ConvToDateFormat(AplInfo.OpDate(), 3);
        }

        /// <summary>
        /// 表示データ一式取得
        /// </summary>
        public static List<string> GetDispValueData(ItemManager itemMgr, TBL_BALANCETXT param)
        {
            List<string> Item = new List<string>();

            // データ設定
            Item.Add(CommonUtil.PadLeft(param._BK_NO, Const.BANK_NO_LEN, "0"));
            string BK_NAME = string.Empty;
            if (int.TryParse(param._BK_NO, out int bkno))
            {
                BK_NAME = itemMgr.GetBankName(bkno);
            }
            Item.Add(BK_NAME);
            string KBN_NAME = param.m_LOAN_KBN.ToString();
            switch (param.m_LOAN_KBN)
            {
                case 1:
                    KBN_NAME = "1:借(負)";
                    break;
                case 2:
                    KBN_NAME = "2:貸(勝)";
                    break;
                default:
                    break;
            }
            Item.Add(KBN_NAME);
            string PAYAMT = param.m_PAY_AMOUNT;
            if (long.TryParse(param.m_PAY_AMOUNT, out long amt))
            {
                PAYAMT = amt.ToString("#,##0");
            }
            Item.Add(PAYAMT);

            return Item;
        }
    }
}
