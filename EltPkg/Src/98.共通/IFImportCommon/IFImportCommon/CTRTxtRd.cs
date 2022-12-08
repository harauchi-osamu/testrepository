using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using CommonTable.DB;
using EntryCommon;
using System.Data;

namespace IFImportCommon
{
    /// <summary>
    /// 電子交換所認識値取得制御クラス
    /// </summary>
    public class CTRTxtRd
    {

        #region クラス変数
        private List<TBL_CTR_OCR_PARAM> ocrParam;
        private List<TBL_CTR_MICRCUTINFO_PARAM> micrCutInfoParam;
        private Dictionary<string, CTRRdCommon.ReName> ReNameItem;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CTRTxtRd(int SchemaBankCD, AdoDatabaseProvider dbp, int Type = 1)
        {
            ocrParam = CTRRdCommon.GetOCRParam(SchemaBankCD, dbp);
            micrCutInfoParam = CTRRdCommon.GetMICRCutInfoParam(SchemaBankCD, dbp);
            if (Type == 1 ) ReNameItem = CTRRdCommon.GetReNameItemFile();
            if (Type == 2) ReNameItem = CTRRdCommon.GetReNameItemDB();
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 電子交換所認識値取得
        /// </summary>
        public string GetText(Dictionary<string, string> data, string itemname, string DefValue = "")
        {
            return GetTextCut(data, itemname, new Dictionary<CTRRdCommon.CutDataType, string>(), DefValue);
        }

        /// <summary>
        /// 電子交換所認識値取得(切り出し箇所あり)
        /// </summary>
        public string GetTextCut(Dictionary<string, string> data, string itemname, Dictionary<CTRRdCommon.CutDataType, string> cutpos, string DefValue = "")
        {
            foreach (TBL_CTR_OCR_PARAM param in ocrParam.Where(x => x._ITEM_NAME == itemname).OrderBy(x => x.m_CTR_PRIORITY))
            {
                if (!ReNameItem.ContainsKey(param._ITEM_NAME + "|" + param._ITEM_TYPE)) continue;
                CTRRdCommon.ReName item = ReNameItem[param._ITEM_NAME + "|" + param._ITEM_TYPE];

                //対象項目取得
                string value = CTRRdCommon.GetItemText(data, item, param._ITEM_NAME, cutpos, micrCutInfoParam);

                if (!(string.IsNullOrWhiteSpace(value) || (value.ToCharArray().Distinct().Count() == 1 && value.ToCharArray().Distinct().First() == 'Z')))
                {
                    // 全て"Z"以外の場合は取得値を返して終了
                    return value;
                }
            }

            return DefValue;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

    }
}
