using System;
using CommonClass;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace ImageImportTotal
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
        /// exe.config ロック解除権限設定
        /// </summary>
        public int UnlockLevel { get; private set; } = 0;

        /// <summary>
        /// exe.config 一意コード算出時スリープタイム（ミリ秒）
        /// </summary>
        public int UniqueCodeSleepTime { get; private set; } = 0;

        /// <summary>
        /// exe.config 合計票一覧の背景色設定
        /// </summary>
        public string TotalListBackColor { get; private set; } = "";

        /// <summary>設定情報</summary>
        public ImportTRAccessCommon.OCRSettingData OCRSettingData { get; private set; } = null;

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
            UnlockLevel = GetAppSettingsInt("UnlockLevel", "ロック解除権限設定");
            UniqueCodeSleepTime = GetAppSettingsInt("UniqueCodeSleepTime", "一意コード算出時スリープタイム");
            TotalListBackColor = GetAppSettingsStr("TotalListBackColor", "合計票一覧の背景色設定");

            // OCR関連
            string OC_BK_NO = GetAppSettingsStr("OCRKey_OC_BK_NO", "OCR読取フィールド名設定（持出銀行）");
            string OC_BR_NO = GetAppSettingsStr("OCRKey_OC_BR_NO", "OCR読取フィールド名設定（持出支店）");
            string SCAN_BR_NO = GetAppSettingsStr("OCRKey_SCAN_BR_NO", "OCR読取フィールド名設定（スキャン支店）");
            string SCAN_DATE = GetAppSettingsStr("OCRKey_SCAN_DATE", "OCR読取フィールド名設定（スキャン日）");
            string TOTAL_COUNT = GetAppSettingsStr("OCRKey_TOTAL_COUNT", "OCR読取フィールド名設定（総枚数）");
            string TOTAL_AMOUNT = GetAppSettingsStr("OCRKey_TOTAL_AMOUNT", "OCR読取フィールド名設定（総金額）");

            OCRSettingData = new ImportTRAccessCommon.OCRSettingData(0, 999, OC_BK_NO, OC_BR_NO, "", SCAN_BR_NO, SCAN_DATE,
                                                                     "", "", TOTAL_COUNT, TOTAL_AMOUNT, "", "", "");

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
