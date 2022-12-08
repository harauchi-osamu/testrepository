using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using Common;
using CommonTable.DB;
using EntryCommon;

namespace SearchICReqResult
{
    /// <summary>
    /// 共通処理クラス
    /// </summary>
    public class SearchICReqResultCommon
    {
        /// <summary>
        /// 検索一覧データ取得
        /// </summary>
        public static List<string> GetSearchListData(ItemManager itemMgr, string Key, TBL_ICREQ_CTL param)
        {
            List<string> Item = new List<string>();

            // データ設定
            Item.Add(Key);
            Item.Add(CommonUtil.ConvToDateFormat(DispDataFormat(param.m_REQ_DATE, "D8"), 3));
            Item.Add(CommonUtil.ConvMiliTimeToTimeFormat(DispDataFormat(param.m_REQ_TIME, "D9")));
            Item.Add(param._REQ_TXT_NAME);
            Item.Add(itemMgr.GetGeneralTextICREQRETHeader(ItemManager.ICREQRETHeaderNo.FILE_CHK_CODE, param.m_RET_FILE_CHK_CODE, ItemManager.DispType.ABBREVIATECODE));
            Item.Add(itemMgr.GetGeneralTextICREQRETHeader(ItemManager.ICREQRETHeaderNo.PROC_RETCODE, param.m_RET_PROC_RETCODE, ItemManager.DispType.ABBREVIATECODE));
            Item.Add(CommonUtil.ConvToDateFormat(DispDataFormat(param.m_RET_DATE, "D8"), 3));
            Item.Add(CommonUtil.ConvMiliTimeToTimeFormat(DispDataFormat(param.m_RET_TIME, "D9")));
            Item.Add(DispDataFormat(param.m_RET_COUNT.ToString(), "#,##0"));

            return Item;
        }

        /// <summary>
        /// 詳細項目欄更新
        /// </summary>
        public static void UpdateReqResult(ItemManager itemMgr, Panel info)
        {
            TBL_ICREQ_CTL Data = itemMgr.ICReqData;

            // 設定
            info.Controls["lblreqtextname"].Text = Data._REQ_TXT_NAME;
            info.Controls["lblreqdate"].Text = CommonUtil.ConvToDateFormat(DispDataFormat(Data.m_REQ_DATE, "D8"), 3);
            info.Controls["lblreqtime"].Text = CommonUtil.ConvMiliTimeToTimeFormat(DispDataFormat(Data.m_REQ_TIME, "D9"));
            info.Controls["lblclearingdatestart"].Text = CommonUtil.ConvToDateFormat(DispDataFormat(Data.m_CLEARING_DATE_S, "D8"), 3);
            info.Controls["lblclearingdateover"].Text = CommonUtil.ConvToDateFormat(DispDataFormat(Data.m_CLEARING_DATE_E, "D8"), 3);
            info.Controls["lblstocktype"].Text = itemMgr.GetGeneralTextICREQData(ItemManager.ICREQDataNo.BILL_CODE, Data.m_BILL_CODE, ItemManager.DispType.ABBREVIATECODE);
            info.Controls["lblprocfilenamedivide"].Text = itemMgr.GetGeneralTextICREQData(ItemManager.ICREQDataNo.IC_TYPE, Data.m_IC_TYPE.ToString(), ItemManager.DispType.ABBREVIATECODE);
            info.Controls["lblstockimagedivide"].Text = itemMgr.GetGeneralTextICREQData(ItemManager.ICREQDataNo.IMG_NEED, Data.m_IMG_NEED.ToString(), ItemManager.DispType.ABBREVIATECODE);
            info.Controls["lblretmakedate"].Text = CommonUtil.ConvToDateFormat(DispDataFormat(Data.m_RET_DATE, "D8"), 3);
            info.Controls["lblretmaketime"].Text = CommonUtil.ConvMiliTimeToTimeFormat(DispDataFormat(Data.m_RET_TIME, "D9"));
            info.Controls["lblretfilechk"].Text = itemMgr.GetGeneralTextICREQRETHeader(ItemManager.ICREQRETHeaderNo.FILE_CHK_CODE, Data.m_RET_FILE_CHK_CODE, ItemManager.DispType.ABBREVIATECODE);
            info.Controls["lblretproccode"].Text = itemMgr.GetGeneralTextICREQRETHeader(ItemManager.ICREQRETHeaderNo.PROC_RETCODE, Data.m_RET_PROC_RETCODE, ItemManager.DispType.ABBREVIATECODE);
            info.Controls["lblretrecordcount"].Text = DispDataFormat(Data.m_RET_COUNT.ToString(), "#,##0 件");
        }

        /// <summary>
        /// 画面表示データ整形
        /// </summary>
        private static string DispDataFormat(long Data, string Format)
        {
            // 0以下は空
            if (Data <= 0) return "";

            return Data.ToString(Format);
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