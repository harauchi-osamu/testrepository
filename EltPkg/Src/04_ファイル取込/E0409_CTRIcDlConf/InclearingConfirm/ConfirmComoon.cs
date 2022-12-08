using System;
using CommonClass;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace InclearingConfirm
{
    /// <summary>
    /// 設定情報管理クラス
    /// </summary>
    public class OCRSetting
    {
        /// <summary>
        /// exe.config OCR読取フィールド名設定（持帰銀行コード）
        /// </summary>
        public string IC_BK_NO { get; private set; } = "";

        /// <summary>
        /// exe.config OCR読取フィールド名設定（金額）
        /// </summary>
        public string AMOUNT { get; private set; } = "";

        /// <summary>
        /// exe.config OCR読取フィールド名設定（交換希望日）
        /// </summary>
        public string CLEARING_DATE { get; private set; } = "";

        /// <summary>
        /// exe.config OCR読取フィールド名設定（交換証券種類コード）
        /// </summary>
        public string BILL_CODE { get; private set; } = "";

        /// <summary>
        /// exe.config OCR読取フィールド名設定（持帰支店コード）
        /// </summary>
        public string IC_BR_NO { get; private set; } = "";

        /// <summary>
        /// exe.config OCR読取フィールド名設定（口座番号）
        /// </summary>
        public string ACCOUNT { get; private set; } = "";

        /// <summary>
        /// exe.config OCR読取フィールド名設定（手形番号）
        /// </summary>
        public string BILL_NO { get; private set; } = "";

        public OCRSetting(string IC_BkNo, string Amt, string ClearingDate, string BillCode,
                                 string IC_BrNo, string Account, string BillNo)
        {
            IC_BK_NO = IC_BkNo;
            AMOUNT = Amt;
            CLEARING_DATE = ClearingDate;
            BILL_CODE = BillCode;
            IC_BR_NO = IC_BrNo;
            ACCOUNT = Account;
            BILL_NO = BillNo;
        }
    }
}
