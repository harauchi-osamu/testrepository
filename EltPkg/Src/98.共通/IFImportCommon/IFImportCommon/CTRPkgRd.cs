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
    /// パッケージ交換所値取得制御クラス
    /// </summary>
    public class CTRPkgRd
    {

        #region クラス変数
        private List<TBL_CTR_OCR_PARAM> ocrParam;
        private List<TBL_CTR_OCRCONF_PARAM> ocrConfParam;
        private List<TBL_CTR_MICRCUTINFO_PARAM> micrCutInfoParam;
        private Dictionary<string, CTRRdCommon.ReName> ReNameItem;
        
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CTRPkgRd(int SchemaBankCD, AdoDatabaseProvider dbp)
        {
            ocrParam = CTRRdCommon.GetOCRParam(SchemaBankCD, dbp);
            ocrConfParam = CTRRdCommon.GetOCRConfParam(SchemaBankCD, dbp);
            micrCutInfoParam = CTRRdCommon.GetMICRCutInfoParam(SchemaBankCD, dbp);
            ReNameItem = CTRRdCommon.GetReNameItemDB();
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// パッケージ値取得
        /// </summary>
        public string GetText(Dictionary<string, string> data, string itemname, string CTRData, string DefValue = "")
        {
            return GetTextCut(data, itemname, CTRData, new Dictionary<CTRRdCommon.CutDataType, string>(), DefValue);
        }

        /// <summary>
        /// パッケージ値取得(切り出し箇所あり)
        /// </summary>
        public string GetTextCut(Dictionary<string, string> data, string itemname, string CTRData, Dictionary<CTRRdCommon.CutDataType, string> cutpos, string DefValue = "")
        {
            Dictionary<string, ChkConf> ChkList = new Dictionary<string, ChkConf>();
            foreach (TBL_CTR_OCR_PARAM param in ocrParam.Where(x => x._ITEM_NAME == itemname).OrderBy(x => x.m_CTR_PRIORITY))
            {
                if (!ReNameItem.ContainsKey(param._ITEM_NAME + "|" + param._ITEM_TYPE)) continue;
                CTRRdCommon.ReName item = ReNameItem[param._ITEM_NAME + "|" + param._ITEM_TYPE];

                //対象項目取得
                string value = CTRRdCommon.GetItemText(data, item, param._ITEM_NAME, cutpos, micrCutInfoParam);

                if (!(string.IsNullOrWhiteSpace(value) || (value.ToCharArray().Distinct().Count() == 1 && value.ToCharArray().Distinct().First() == 'Z')))
                {
                    // 全て"Z"以外の場合、電子交換所確信度を取得
                    // 初期値としてCTR_OCRCONF_PARAMに定義の最大値を設定
                    int chkConf = GetCtrMaxConf(param._ITEM_NAME + "|" + param._ITEM_TYPE);
                    if (!string.IsNullOrEmpty(item.ConfName))
                    {
                        if (!int.TryParse(data[item.ConfName], out chkConf))
                        {
                            // 確信度の値が数値変換できない場合は0扱い
                            chkConf = 0;
                        }
                    }
                    // 対象パッケージ確信度を取得
                    int conf = GetConf(param._ITEM_NAME + "|" + param._ITEM_TYPE, chkConf);

                    if (ChkList.ContainsKey(value))
                    {
                        ChkList[value].SumConf += conf;
                        ChkList[value].pkgPriority = Math.Min(ChkList[value].pkgPriority, param.m_PKG_PRIORITY);
                    }
                    else
                    {
                        ChkList.Add(value, new ChkConf(conf, param.m_PKG_PRIORITY));
                    }
                }
            }
            if (ChkList.Count() == 0) return DefValue;

            //確信度集計値の最大値の値を取得
            KeyValuePair<string, ChkConf> GetValue = ChkList.Where(x => x.Value.SumConf == ChkList.Max(y => y.Value.SumConf)).OrderBy(x => x.Value.pkgPriority).First();

            //電子交換所値比較
            if (CTRData != GetValue.Key) return DefValue;

            // 確信度閾値チェック
            if (GetValue.Value.SumConf < ocrParam.Where(x => x._ITEM_NAME == itemname).Max(x => x.m_LOWER_LIMIT)) return DefValue;

            return GetValue.Key;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// 確信度取得
        /// </summary>
        /// <returns></returns>
        private int GetConf(string type, int ctrcnf)
        {
            IEnumerable<TBL_CTR_OCRCONF_PARAM> ie = ocrConfParam.Where(x => (x._ITEM_NAME + "|" + x._ITEM_TYPE) == type && (x._CTR_MIN_CONF <= ctrcnf && x.m_CTR_MAX_CONF >= ctrcnf));
            if (ie.Count() == 0) return 0;

            return ie.First().m_CONF;
        }

        /// <summary>
        /// 最大確信度取得
        /// CTR_OCRCONF_PARAMから認識項目・認識タイプ毎の最大確信度の大きい値を取得
        /// </summary>
        /// <returns></returns>
        private int GetCtrMaxConf(string type)
        {
            IEnumerable<TBL_CTR_OCRCONF_PARAM> ie = ocrConfParam.Where(x => (x._ITEM_NAME + "|" + x._ITEM_TYPE) == type);
            if (ie.Count() == 0) return 0;
            return ie.Max(x => x.m_CTR_MAX_CONF);
        }

        /// <summary>
        /// チェック結果
        /// </summary>
        private class ChkConf
        {
            public int SumConf { get; set; }
            public int pkgPriority { get; set; }
            public ChkConf(int Conf, int Priority)
            {
                SumConf = Conf;
                pkgPriority = Priority;
            }
        }
    }
}
