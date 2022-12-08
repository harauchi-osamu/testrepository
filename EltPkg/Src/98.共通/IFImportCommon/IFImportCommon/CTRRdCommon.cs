using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using CommonTable.DB;
using EntryCommon;
using System.Data;
using System.Text.RegularExpressions;

namespace IFImportCommon
{
    /// <summary>
    /// 取得処理共通クラス
    /// </summary>
    public class CTRRdCommon
    {

        #region enum
        /// <summary>
        /// 切出タイプ
        /// </summary>
        public enum CutDataType
        {
            None = 0,
            MICR = 1,
            QR = 2,
            TXTBILLNO = 3, // 証券明細テキストの手形・小切手番号切り出し
        }
        #endregion

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        #region マスタデータ取得

        /// <summary>
        /// CTR_OCR_PARAMデータ取得
        /// </summary>
        /// <returns></returns>
        public static List<TBL_CTR_OCR_PARAM> GetOCRParam(int SchemaBankCD, AdoDatabaseProvider dbp)
        {
            List<TBL_CTR_OCR_PARAM> ocrParam = new List<TBL_CTR_OCR_PARAM>();

            // データ取得
            string strSQL = TBL_CTR_OCR_PARAM.GetSelectQuery(SchemaBankCD);
            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                ocrParam.Add(new TBL_CTR_OCR_PARAM(tbl.Rows[i], SchemaBankCD));
            }

            return ocrParam;
        }

        /// <summary>
        /// CTR_OCRCONF_PARAMデータ取得
        /// </summary>
        /// <returns></returns>
        public static List<TBL_CTR_OCRCONF_PARAM> GetOCRConfParam(int SchemaBankCD, AdoDatabaseProvider dbp)
        {
            List<TBL_CTR_OCRCONF_PARAM> ocrConfParam = new List<TBL_CTR_OCRCONF_PARAM>();

            // データ取得
            string strSQL = TBL_CTR_OCRCONF_PARAM.GetSelectQuery(SchemaBankCD);
            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                ocrConfParam.Add(new TBL_CTR_OCRCONF_PARAM(tbl.Rows[i], SchemaBankCD));
            }

            return ocrConfParam;
        }

        /// <summary>
        /// CTR_OCRCUTINFO_PARAMデータ取得
        /// </summary>
        /// <returns></returns>
        public static List<TBL_CTR_MICRCUTINFO_PARAM> GetMICRCutInfoParam(int SchemaBankCD, AdoDatabaseProvider dbp)
        {
            List<TBL_CTR_MICRCUTINFO_PARAM> micrCutInfoParam = new List<TBL_CTR_MICRCUTINFO_PARAM>();

            // データ取得
            string strSQL = TBL_CTR_MICRCUTINFO_PARAM.GetSelectQuery(SchemaBankCD);
            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                micrCutInfoParam.Add(new TBL_CTR_MICRCUTINFO_PARAM(tbl.Rows[i], SchemaBankCD));
            }

            return micrCutInfoParam;
        }

        #endregion

        #region 情報取得

        /// <summary>
        /// 対象のOCRCUTINFO取得
        /// </summary>
        /// <returns></returns>
        private static List<TBL_CTR_MICRCUTINFO_PARAM> GetMICRCutParam(string itemname, List<TBL_CTR_MICRCUTINFO_PARAM> micrCutInfoParam)
        {
            return micrCutInfoParam.Where(x => x._ITEM_NAME == itemname).ToList();
        }

        #endregion

        #region 値取得

        /// <summary>
        /// 対象項目の値取得
        /// </summary>
        public static string GetItemText(Dictionary<string, string> data, ReName item, 
                                         string fieldname, Dictionary<CutDataType, string> cutpos, List<TBL_CTR_MICRCUTINFO_PARAM> micrCutInfoParam)
        {
            string value = data[item.ItemName];

            if (item.CutType == CutDataType.MICR)
            {
                // MICR切出処理
                value = GetRegexMatchData(value, GetMICRCutParam(fieldname, micrCutInfoParam));
            }
            else if (item.CutType == CutDataType.QR)
            {
                // QR切出処理
                string CutPosData = string.Empty;
                if (cutpos.ContainsKey(CutDataType.QR))
                {
                    CutPosData = cutpos[CutDataType.QR];
                }

                // 指定箇所の切り出し
                value = GetCutData(value, CutPosData);
            }
            else if (item.CutType == CutDataType.TXTBILLNO)
            {
                // 証券明細テキストの手形・小切手番号切出処理
                string RegexPattern = string.Empty;
                if (cutpos.ContainsKey(CutDataType.TXTBILLNO))
                {
                    RegexPattern = cutpos[CutDataType.TXTBILLNO];
                }

                // 指定箇所の切り出し
                value = GetRegexMatchData(value, RegexPattern);
            }

            return value.Trim();
        }

        /// <summary>
        /// 文字列切り出しデータ取得
        /// </summary>
        /// <returns></returns>
        private static string GetCutData(string value, string cutpos)
        {
            List<string> cutlist = cutpos.Split(',').ToList();
            if (cutlist.Count < 2) return "Z";
            if (!(int.TryParse(cutlist[0], out int st) && int.TryParse(cutlist[1], out int len)))
            {
                return "Z";
            }
            if (value.Length < st)
            {
                return "Z";
            }
            if (len <= 0)
            {
                return "Z";
            }
            return value.Substring(st - 1, len);
        }

        /// <summary>
        /// 文字列切り出しデータ取得(正規表現)
        /// </summary>
        /// <returns></returns>
        private static string GetRegexMatchData(string value, List<TBL_CTR_MICRCUTINFO_PARAM> ocrCutParam)
        {

            foreach(TBL_CTR_MICRCUTINFO_PARAM cutInfo in ocrCutParam.OrderBy(x => x._SEQ))
            {
                string matchValue = GetRegexMatchValue(value, cutInfo.m_REGEXPATTERN);
                if (!string.IsNullOrEmpty(matchValue))
                {
                    // データが取得できた場合
                    return matchValue;
                }
            }

            return "Z";
        }

        /// <summary>
        /// 文字列切り出しデータ取得(正規表現)
        /// Pattern指定
        /// </summary>
        /// <returns></returns>
        private static string GetRegexMatchData(string value, string pattern)
        {
            if (!string.IsNullOrEmpty(pattern))
            {
                string matchValue = GetRegexMatchValue(value, pattern);
                if (!string.IsNullOrEmpty(matchValue))
                {
                    // データが取得できた場合
                    return matchValue;
                }
            }

            return "Z";
        }

        /// <summary>
        /// 正規表現文字列切り出し
        /// グループ化されている箇所を取得
        /// </summary>
        /// <returns></returns>
        private static string GetRegexMatchValue(string value, string pattern)
        {
            Regex regex = new Regex(pattern);
            Match m = regex.Match(value);

            if (!m.Success) return "";
            if (m.Groups.Count < 2) return "";

            return m.Groups[1].Value;
        }

        #endregion

        #region 読替情報定義

        /// <summary>
        /// 項目名の読替情報の取得(IFFile)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, ReName> GetReNameItemFile()
        {
            Dictionary<string, ReName> Item = new Dictionary<string, ReName>();
            Item.Add("金額|OCR", new ReName("OCR金額", "OCR金額確信度"));
            Item.Add("金額|MICR", new ReName("MICR金額", "MICR情報確信度"));
            Item.Add("金額|QR", new ReName("QRコード金額", ""));
            Item.Add("金額|ファイル名", new ReName("ファイル名金額", ""));
            Item.Add("金額|証券データ訂正", new ReName("証券データ訂正金額", ""));
            Item.Add("金額|決済", new ReName("決済金額", ""));
            Item.Add("金額|決済後訂正", new ReName("決済後訂正金額", ""));
            Item.Add("持帰銀行コード|MICR", new ReName("MICR持帰銀行コード", "MICR情報確信度"));
            Item.Add("持帰銀行コード|OCR", new ReName("OCR持帰銀行コード", "OCR持帰銀行コード確信度"));
            Item.Add("持帰銀行コード|QR", new ReName("QRコード持帰銀行コード", ""));
            Item.Add("持帰銀行コード|ファイル名", new ReName("ファイル名持帰銀行コード", ""));
            Item.Add("持帰銀行コード|読替", new ReName("読替持帰銀行コード", ""));
            Item.Add("持帰銀行コード|証券データ訂正", new ReName("証券データ訂正持帰銀行コード", ""));
            Item.Add("持帰銀行コード|決済", new ReName("決済持帰銀行コード", ""));
            Item.Add("持帰銀行コード|決済後訂正", new ReName("決済後訂正持帰銀行コード", ""));
            Item.Add("交換日|持出時", new ReName("持出時交換希望日", ""));
            Item.Add("交換日|証券データ訂正", new ReName("証券データ訂正交換希望日", ""));
            Item.Add("交換日|交換日", new ReName("交換日", ""));
            Item.Add("決済対象フラグ|決済対象区分", new ReName("決済対象区分", ""));
            Item.Add("交換証券種類コード|交換証券種類コード", new ReName("交換証券種類コード", "交換証券種類コード確信度"));
            Item.Add("種類コード|QR", new ReName("QRコード情報", "", CutDataType.QR));
            Item.Add("種類コード|MICR", new ReName("MICR情報", "MICR情報確信度", CutDataType.MICR));
            Item.Add("持帰支店コード|MICR", new ReName("MICR情報", "MICR情報確信度", CutDataType.MICR));
            Item.Add("持帰支店コード|QR", new ReName("QRコード持帰支店コード", ""));
            Item.Add("口座番号|MICR", new ReName("MICR情報", "MICR情報確信度", CutDataType.MICR));
            Item.Add("口座番号|QR", new ReName("口座番号", ""));
            Item.Add("手形番号|OCR", new ReName("手形・小切手番号", "手形・小切手番号確信度", CutDataType.TXTBILLNO));
            Item.Add("手形番号|MICR", new ReName("MICR情報", "MICR情報確信度", CutDataType.MICR));
            Item.Add("手形番号|QR", new ReName("QRコード情報", "", CutDataType.QR));

            return Item;
        }

        /// <summary>
        /// 項目名の読替情報の取得(DB)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, ReName> GetReNameItemDB()
        {
            Dictionary<string, ReName> Item = new Dictionary<string, ReName>();
            Item.Add("金額|OCR", new ReName("OCR_AMOUNT", "OCR_AMOUNT_CONF"));
            Item.Add("金額|MICR", new ReName("MICR_AMOUNT", "MICR_CONF"));
            Item.Add("金額|QR", new ReName("QR_AMOUNT", ""));
            Item.Add("金額|ファイル名", new ReName("FILE_AMOUNT", ""));
            Item.Add("金額|証券データ訂正", new ReName("TEISEI_AMOUNT", ""));
            Item.Add("金額|決済", new ReName("PAY_AMOUNT", ""));
            Item.Add("金額|決済後訂正", new ReName("PAYAFT_REV_AMOUNT", ""));
            Item.Add("持帰銀行コード|MICR", new ReName("MICR_IC_BK_NO", "MICR_CONF"));
            Item.Add("持帰銀行コード|OCR", new ReName("OCR_IC_BK_NO", "OCR_IC_BK_NO_CONF"));
            Item.Add("持帰銀行コード|QR", new ReName("QR_IC_BK_NO", ""));
            Item.Add("持帰銀行コード|ファイル名", new ReName("FILE_IC_BK_NO", ""));
            Item.Add("持帰銀行コード|読替", new ReName("CHG_IC_BK_NO", ""));
            Item.Add("持帰銀行コード|証券データ訂正", new ReName("TEISEI_IC_BK_NO", ""));
            Item.Add("持帰銀行コード|決済", new ReName("PAY_IC_BK_NO", ""));
            Item.Add("持帰銀行コード|決済後訂正", new ReName("PAYAFT_REV_IC_BK_NO", ""));
            Item.Add("交換日|持出時", new ReName("OC_CLEARING_DATE", ""));
            Item.Add("交換日|証券データ訂正", new ReName("TEISEI_CLEARING_DATE", ""));
            Item.Add("交換日|交換日", new ReName("CLEARING_DATE", ""));
            Item.Add("決済対象フラグ|決済対象区分", new ReName("PAY_KBN", ""));
            Item.Add("交換証券種類コード|交換証券種類コード", new ReName("BILL_CODE", "BILL_CODE_CONF"));
            Item.Add("種類コード|QR", new ReName("QR", "", CutDataType.QR));
            Item.Add("種類コード|MICR", new ReName("MICR", "MICR_CONF", CutDataType.MICR));
            Item.Add("持帰支店コード|MICR", new ReName("MICR", "MICR_CONF", CutDataType.MICR));
            Item.Add("持帰支店コード|QR", new ReName("QR_IC_BR_NO", ""));
            Item.Add("口座番号|MICR", new ReName("MICR", "MICR_CONF", CutDataType.MICR));
            Item.Add("口座番号|QR", new ReName("ACCOUNT", ""));
            Item.Add("手形番号|OCR", new ReName("BILL_NO", "BILL_NO_CONF", CutDataType.TXTBILLNO));
            Item.Add("手形番号|MICR", new ReName("MICR", "MICR_CONF", CutDataType.MICR));
            Item.Add("手形番号|QR", new ReName("QR", "", CutDataType.QR));

            return Item;
        }

        #endregion

        #region サブクラス

        /// <summary>
        /// リネームクラス
        /// </summary>
        public class ReName
        {
            public string ItemName { get; set; }
            public string ConfName { get; set; }
            public CutDataType CutType { get; set; }
            public ReName(string Item, string Conf)
            {
                ItemName = Item;
                ConfName = Conf;
                CutType = CutDataType.None;
            }
            public ReName(string Item, string Conf, CutDataType type)
            {
                ItemName = Item;
                ConfName = Conf;
                CutType = type;
            }
        }

        #endregion

    }
}
