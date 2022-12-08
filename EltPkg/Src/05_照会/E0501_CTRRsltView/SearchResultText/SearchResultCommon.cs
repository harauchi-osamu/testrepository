using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace SearchResultText
{
    /// <summary>
    /// 共通処理クラス
    /// </summary>
    public class SearchResultCommon
    {

        /// <summary>
        /// 検索一覧データ取得
        /// </summary>
        public static List<string> GetSearchListData(ItemManager itemMgr, string Key, TBL_RESULTTXT_CTL param)
        {
            List<string> Item = new List<string>();

            // データ設定
            Item.Add(Key);
            Item.Add(itemMgr.GetFileParamName(param.m_FILE_DIVID));
            Item.Add(param._FILE_NAME);
            Item.Add(param.m_BK_NO);
            Item.Add(itemMgr.GetGeneralTextHeader(ItemManager.HeaderNo.FILE_CHK_CODE, param.m_FILE_CHK_CODE, ItemManager.DispType.ABBREVIATE));
            Item.Add(DispDataFormat(param.m_RECORD_COUNT.ToString(), "#,##0"));
            Item.Add(CommonUtil.ConvToDateFormat(param.m_RECV_DATE, 3));
            Item.Add(CommonUtil.ConvMiliTimeToTimeFormat(param.m_RECV_TIME));

            return Item;
        }

        /// <summary>
        /// ファイル情報欄更新
        /// </summary>
        public static void UpdatepnlInfo(ItemManager itemMgr ,Panel info)
        {
            // ファイル情報欄
            TBL_RESULTTXT_CTL ctlparam = itemMgr.GetResultTextControl();
            info.Controls["lblFILE_DIVID"].Text = itemMgr.GetFileParamName(ctlparam.m_FILE_DIVID);
            info.Controls["lblFILE_NAME"].Text = ctlparam._FILE_NAME;
            info.Controls["lblBK_NO"].Text = ctlparam.m_BK_NO;
            info.Controls["lblFILE_CHK_CODE"].Text = itemMgr.GetGeneralTextHeader(ItemManager.HeaderNo.FILE_CHK_CODE, ctlparam.m_FILE_CHK_CODE, ItemManager.DispType.ABBREVIATE);
            info.Controls["lblRECORD_COUNT"].Text = DispDataFormat(ctlparam.m_RECORD_COUNT.ToString(), "#,##0");
            info.Controls["lblRECV_DATE"].Text = CommonUtil.ConvToDateFormat(ctlparam.m_RECV_DATE, 3);
            info.Controls["lblRECV_TIME"].Text = CommonUtil.ConvMiliTimeToTimeFormat(ctlparam.m_RECV_TIME);
        }

        /// <summary>
        /// 表示データ一式取得
        /// </summary>
        public static List<string> GetDispValueData(ItemManager itemMgr, TBL_RESULTTXT param)
        {
            List<string> Item = new List<string>();

            // データ設定
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.RECEPTION, param.m_RECEPTION, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.RET_CODE, param.m_RET_CODE, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.IMG_NAME, param.m_IMG_NAME, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FRONT_IMG_NAME, param.m_FRONT_IMG_NAME, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.IMG_KBN, param.m_IMG_KBN.ToString(), ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FILE_OC_BK_NO, param.m_FILE_OC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.CHG_OC_BK_NO, param.m_CHG_OC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OC_BR_NO, param.m_OC_BR_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OC_DATE, param.m_OC_DATE.ToString(), ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OC_METHOD, param.m_OC_METHOD, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OC_USERID, param.m_OC_USERID, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.PAY_KBN, param.m_PAY_KBN, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.BALANCE_FLG, param.m_BALANCE_FLG, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.MICR_IC_BK_NO, param.m_OCR_IC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OCR_IC_BK_NO, param.m_QR_IC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.QR_IC_BK_NO, param.m_MICR_IC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FILE_IC_BK_NO, param.m_FILE_IC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.CHG_IC_BK_NO, param.m_CHG_IC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_IC_BK_NO, param.m_TEISEI_IC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.PAY_IC_BK_NO, param.m_PAY_IC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.PAYAFT_REV_IC_BK_NO, param.m_PAYAFT_REV_IC_BK_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OCR_IC_BK_NO_CONF, param.m_OCR_IC_BK_NO_CONF, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OCR_AMOUNT, param.m_OCR_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.MICR_AMOUNT, param.m_MICR_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.QR_AMOUNT, param.m_QR_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FILE_AMOUNT, param.m_FILE_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_AMOUNT, param.m_TEISEI_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.PAY_AMOUNT, param.m_PAY_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.PAYAFT_REV_AMOUNT, param.m_PAYAFT_REV_AMOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OCR_AMOUNT_CONF, param.m_OCR_AMOUNT_CONF, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.OC_CLEARING_DATE, param.m_OC_CLEARING_DATE, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.TEISEI_CLEARING_DATE, param.m_TEISEI_CLEARING_DATE, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.CLEARING_DATE, param.m_CLEARING_DATE, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.QR_IC_BR_NO, param.m_QR_IC_BR_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.KAMOKU, param.m_KAMOKU, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ACCOUNT, param.m_ACCOUNT, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.BK_CTL_NO, param.m_BK_CTL_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FREEFIELD, param.m_FREEFIELD, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.BILL_CODE, param.m_BILL_CODE, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.BILL_CODE_CONF, param.m_BILL_CODE_CONF, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.QR, param.m_QR, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.MICR, param.m_MICR, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.MICR_CONF, param.m_MICR_CONF, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.BILL_NO, param.m_BILL_NO, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.BILL_NO_CONF, param.m_BILL_NO_CONF, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_01, param.m_FUBI_KBN_01, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZERO_FUBINO_01, param.m_ZERO_FUBINO_01.ToString(), ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_02, param.m_FUBI_KBN_02, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZRO_FUBINO_02, param.m_ZRO_FUBINO_02.ToString(), ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_03, param.m_FUBI_KBN_03, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZRO_FUBINO_03, param.m_ZRO_FUBINO_03.ToString(), ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_04, param.m_FUBI_KBN_04, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZRO_FUBINO_04, param.m_ZRO_FUBINO_04.ToString(), ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.FUBI_KBN_05, param.m_FUBI_KBN_05, ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.ZRO_FUBINO_05, param.m_ZRO_FUBINO_05.ToString(), ItemManager.DispType.DESCRIPTION));
            Item.Add(itemMgr.GetGeneralTextData(ItemManager.DataNo.IC_FLG, param.m_IC_FLG, ItemManager.DispType.DESCRIPTION));

            return Item;
        }

        /// <summary>
        /// 表示名称データ一式取得
        /// </summary>
        public static List<string> GetDispNameData()
        {
            List<string> Item = new List<string>();

            // データ設定
            Item.Add("受付内容");
            Item.Add("処理結果コード");
            Item.Add("証券イメージファイル名");
            Item.Add("表証券イメージファイル名");
            Item.Add("表裏等の別");
            Item.Add("ファイル名持出銀行コード");
            Item.Add("読替持出銀行コード");
            Item.Add("持出支店コード");
            Item.Add("持出日");
            Item.Add("持出時接続方式");
            Item.Add("ユーザID(持出者)");
            Item.Add("決済対象区分");
            Item.Add("交換尻確定フラグ");
            Item.Add("MICR持帰銀行コード");
            Item.Add("OCR持帰銀行コード");
            Item.Add("QRコード持帰銀行コード");
            Item.Add("ファイル名持帰銀行コード");
            Item.Add("読替持帰銀行コード");
            Item.Add("証券データ訂正持帰銀行コード");
            Item.Add("決済持帰銀行コード");
            Item.Add("決済後訂正持帰銀行コード");
            Item.Add("OCR持帰銀行コード確信度");
            Item.Add("OCR金額");
            Item.Add("MICR金額");
            Item.Add("QRコード金額");
            Item.Add("ファイル名金額");
            Item.Add("証券データ訂正金額");
            Item.Add("決済金額");
            Item.Add("決済後訂正金額");
            Item.Add("OCR金額確信度");
            Item.Add("持出時交換希望日");
            Item.Add("証券データ訂正交換希望日");
            Item.Add("交換日");
            Item.Add("QRコード持帰支店コード");
            Item.Add("科目コード");
            Item.Add("口座番号");
            Item.Add("銀行管理番号");
            Item.Add("自由記述欄");
            Item.Add("交換証券種類コード");
            Item.Add("交換証券種類コード確信度");
            Item.Add("QRコード情報");
            Item.Add("MICR情報");
            Item.Add("MICR情報確信度");
            Item.Add("手形・小切手番号");
            Item.Add("手形・小切手番号確信度");
            Item.Add("不渡返還区分１");
            Item.Add("0号不渡事由コード１");
            Item.Add("不渡返還区分２");
            Item.Add("0号不渡事由コード２");
            Item.Add("不渡返還区分３");
            Item.Add("0号不渡事由コード３");
            Item.Add("不渡返還区分４");
            Item.Add("0号不渡事由コード４");
            Item.Add("不渡返還区分５");
            Item.Add("0号不渡事由コード５");
            Item.Add("持帰状況フラグ");

            return Item;
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
