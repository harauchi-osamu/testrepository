using System;
using CommonClass;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace InclearingConfirm
{
    /// <summary>
    /// 設定ファイル情報
    /// </summary>
    public class SettingData
    {
        /// <summary>
        /// 設定ファイルチェックの結果メッセージ
        /// 空の場合チェックOK
        /// </summary>
        public string CheckParamMsg { get; private set; } = "";

        /// <summary>
        /// ServerIniファイル存在チェック結果
        /// True：OK/ False：NG
        /// </summary>
        public bool ChkServerIni { get; private set; } = true;

        #region 固有プロパティ

        /// <summary>
        /// exe.config QR情報読取設定(種類コード)
        /// </summary>
        public string QRSyuruiCode { get; private set; } = string.Empty;

        /// <summary>
        /// exe.config QR情報読取設定(手形番号)
        /// </summary>
        public string QRBillNo { get; private set; } = string.Empty;

        /// <summary>
        /// exe.config 行内連携での持出OCR信頼度閾値
        /// </summary>
        public int OCOCRLevel { get; private set; } = 0;

        /// <summary>
        /// exe.config 証券明細テキスト:手形・小切手番号の取得正規表現
        /// </summary>
        public string BillNo_RegexPattern { get; private set; } = string.Empty;

        /// <summary>設定情報</summary>
        public OCRSetting OCRSettingData { get; private set; } = null;

        #endregion 


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// Server.ini 存在チェック
        /// </summary>
        /// <returns></returns>
        public bool ServerIniExists()
        {
            // ServerIniファイル存在チェック
            if (!System.IO.File.Exists(NCR.Terminal.ServeriniPath))
            {
                ChkServerIni = false;

                return true;
            }

            return true;
        }


        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public bool CheckAppData()
        {
            QRSyuruiCode = GetAppSettingsStr("QR_SYURUI_CODE", "QR情報読取設定(種類コード)");
            QRBillNo = GetAppSettingsStr("QR_BILL_NO", "QR情報読取設定(手形番号)");
            OCOCRLevel = GetAppSettingsInt("OCOCRLevel", "持出OCR信頼度閾値");
            BillNo_RegexPattern = GetAppSettingsStr("BILLNO_REGEXPATTERN", "手形・小切手番号の取得(正規表現)");

            // OCR関連
            string IC_BK_NO = GetAppSettingsStr("OCRKey_IC_BK_NO", "OCR読取フィールド名設定（持帰銀行コード）");
            string AMOUNT = GetAppSettingsStr("OCRKey_AMOUNT", "OCR読取フィールド名設定（金額）");
            string CLEARING_DATE = GetAppSettingsStr("OCRKey_CLEARING_DATE", "OCR読取フィールド名設定（交換希望日）");
            string BILL_CODE = GetAppSettingsStr("OCRKey_BILL_CODE", "OCR読取フィールド名設定（手形種類）");
            string IC_BR_NO = GetAppSettingsStr("OCRKey_IC_BR_NO", "OCR読取フィールド名設定（持帰支店コード）");
            string ACCOUNT = GetAppSettingsStr("OCRKey_ACCOUNT", "OCR読取フィールド名設定（口座番号）");
            string BILL_NO = GetAppSettingsStr("OCRKey_BILL_NO", "OCR読取フィールド名設定（手形番号）");

            OCRSettingData = new OCRSetting(IC_BK_NO, AMOUNT, CLEARING_DATE, BILL_CODE, IC_BR_NO, ACCOUNT, BILL_NO);

            return true;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************


        /// <summary>
        /// 設定内容チェック
        /// </summary>
        public bool ChkParam(string Item, string ItemName)
        {
            if (string.IsNullOrEmpty(Item))
            {
                SetCheckParamMsg(ItemName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 設定内容チェック
        /// </summary>
        public bool ChkParam(int Item, string ItemName)
        {
            if (Item == 0)
            {
                SetCheckParamMsg(ItemName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// configの取得
        /// </summary>
        public string GetAppSettingsStr(string Key, string ItemName, bool EmptyChk = true)
        {
            string strWork = "";

            try
            {
                string sKeyData = ConfigurationManager.AppSettings[Key];
                if (string.IsNullOrWhiteSpace(sKeyData) && EmptyChk)
                {
                    throw new Exception("Error");
                }
                strWork = sKeyData;
            }
            catch
            {
                SetCheckParamMsg(ItemName);
            }

            return strWork;
        }

        /// <summary>
        /// configの取得
        /// </summary>
        public int GetAppSettingsInt(string Key, string ItemName, bool EmptyChk = true)
        {
            int iWork = -1;

            try
            {
                string sKeyData = ConfigurationManager.AppSettings[Key];
                if (string.IsNullOrWhiteSpace(sKeyData) && !EmptyChk)
                {
                    sKeyData = "0";
                }
                if (!int.TryParse(sKeyData, out iWork))
                {
                    throw new Exception("Error");
                }
            }
            catch
            {
                SetCheckParamMsg(ItemName);
            }

            return iWork;
        }

        /// <summary>
        /// 結果メッセージへの設定
        /// </summary>
        private void SetCheckParamMsg(string Msg)
        {
            if (string.IsNullOrEmpty(CheckParamMsg))
            {
                CheckParamMsg = Msg;
            }
            else
            {
                CheckParamMsg += "," + Msg;
            }
        }
    }
}
