using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace SearchTxtView
{
    class SearchResultCommon
    {
        /// <summary>
        /// 検索一覧データ取得
        /// </summary>
        public static List<string> GetSearchListData(ItemManager itemMgr, string Key, TBL_TSUCHITXT_CTL param)
        {
            List<string> Item = new List<string>();

            // データ設定
            Item.Add(Key);
            Item.Add(itemMgr.GetFileParamName(param.m_FILE_DIVID));
            Item.Add(param._FILE_NAME);
            Item.Add(DispDataFormat(param.m_RECORD_COUNT.ToString(), "#,##0"));
            Item.Add(CommonUtil.ConvToDateFormat(param.m_RECV_DATE, 3));
            Item.Add(CommonUtil.ConvMiliTimeToTimeFormat(param.m_RECV_TIME));

            return Item;
        }

        /// <summary>
        /// 明細一覧データ取得
        /// </summary>
        public static List<string> GetSearchDetailListData(ItemManager itemMgr, string Key, TBL_TSUCHITXT param)
        {
            List<string> Item = new List<string>();

            // データ設定
            Item.Add(Key);
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.IMG_NAME, param.m_IMG_NAME, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.BK_NO_TEISEI_FLG, param.m_BK_NO_TEISEI_FLG, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_BEF_BK_NO, param.m_TEISEI_BEF_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_AFT_BK_NO, param.m_TEISEI_AFT_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.CLEARING_TEISEI_FLG, param.m_CLEARING_TEISEI_FLG, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_BEF_CLEARING_DATE, param.m_TEISEI_BEF_CLEARING_DATE, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_CLEARING_DATE, param.m_TEISEI_CLEARING_DATE, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.AMOUNT_TEISEI_FLG, param.m_AMOUNT_TEISEI_FLG, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_BEF_AMOUNT, param.m_TEISEI_BEF_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_AMOUNT, param.m_TEISEI_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.DUPLICATE_IMG_NAME, param.m_DUPLICATE_IMG_NAME, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_REG_KBN, param.m_FUBI_REG_KBN, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_01, param.m_FUBI_KBN_01, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZERO_FUBINO_01, param.m_ZERO_FUBINO_01, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_02, param.m_FUBI_KBN_02, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZRO_FUBINO_02, param.m_ZRO_FUBINO_02, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_03, param.m_FUBI_KBN_03, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZRO_FUBINO_03, param.m_ZRO_FUBINO_03, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_04, param.m_FUBI_KBN_04, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZRO_FUBINO_04, param.m_ZRO_FUBINO_04, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_05, param.m_FUBI_KBN_05, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZRO_FUBINO_05, param.m_ZRO_FUBINO_05, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.REV_CLEARING_FLG, param.m_REV_CLEARING_FLG, ItemManager.DispType.DESCRIPTION));

            return Item;
        }

        /// <summary>
        /// ファイル情報欄更新
        /// </summary>
        public static void UpdatepnlInfo(ItemManager itemMgr, Panel info)
        {
            // ファイル情報欄
            TBL_TSUCHITXT_CTL ctlparam = itemMgr.GetTsuchiTextControl();
            info.Controls["lblFILE_DIVID"].Text = itemMgr.GetFileParamName(ctlparam.m_FILE_DIVID);
            info.Controls["lblFILE_NAME"].Text = ctlparam._FILE_NAME;
            info.Controls["lblRECORD_COUNT"].Text = DispDataFormat(ctlparam.m_RECORD_COUNT.ToString(), "#,##0");
            info.Controls["lblRECV_DATE"].Text = CommonUtil.ConvToDateFormat(ctlparam.m_RECV_DATE, 3);
            info.Controls["lblRECV_TIME"].Text = CommonUtil.ConvMiliTimeToTimeFormat(ctlparam.m_RECV_TIME);
        }

        /// <summary>
        /// 画面表示データ整形
        /// </summary>
        public static string DispDataFormat(string Data, string Format)
        {
            if (!long.TryParse(Data, out long ChgData))
            {
                return Data;
            }

            return ChgData.ToString(Format);
        }

    }
}
