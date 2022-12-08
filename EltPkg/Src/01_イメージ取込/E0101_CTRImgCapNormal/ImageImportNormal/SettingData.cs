﻿using System;
using CommonClass;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace ImageImportNormal
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
        /// exe.config バッチ番号採番リトライ回数
        /// </summary>
        public int BatchSeqRetryCount { get; private set; } = 0;

        /// <summary>
        /// exe.config バッチ一覧の背景色設定
        /// </summary>
        public string BatchListBackColor { get; private set; } = "";


        /// <summary>設定情報</summary>
        public ImportTRAccessCommon.OCRSettingData OCRSettingData { get; private set; } = null;

        /// <summary>
        /// exe.config イメージファイル名定義
        /// </summary>
        public List<ScanImage> ScanImagePattern { get; private set; } = new List<ScanImage>();

        #endregion

        #region パターン構造体

        public struct ScanImage
        {
            public string Pattern;
            public string KeyFront;
            public string KeyBack;
            public int SerNoFlg;

            public ScanImage(string Pattern, string KeyFront, string KeyBack, int SerNoFlg)
            {
                this.Pattern = Pattern;
                this.KeyFront = KeyFront;
                this.KeyBack = KeyBack;
                this.SerNoFlg = SerNoFlg;
            }
        }

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
            BatchSeqRetryCount = GetAppSettingsInt("BatchSeqRetryCount", "バッチ番号採番リトライ回数");
            BatchListBackColor = GetAppSettingsStr("BatchListBackColor", "バッチ一覧の背景色設定");

            // OCR関連
            int DspIDDefault = GetAppSettingsInt("DspIDDefault", "画面ID設定");
            string OC_BK_NO = GetAppSettingsStr("OCRKey_OC_BK_NO", "OCR読取フィールド名設定（持出銀行）");
            string OC_BR_NO = GetAppSettingsStr("OCRKey_OC_BR_NO", "OCR読取フィールド名設定（持出支店）");
            string IC_BK_NO = GetAppSettingsStr("OCRKey_IC_BK_NO", "OCR読取フィールド名設定（持帰銀行）");
            string SCAN_BR_NO = GetAppSettingsStr("OCRKey_SCAN_BR_NO", "OCR読取フィールド名設定（スキャン支店）");
            string SCAN_DATE = GetAppSettingsStr("OCRKey_SCAN_DATE", "OCR読取フィールド名設定（スキャン日）");
            string CLEARING_DATE = GetAppSettingsStr("OCRKey_CLEARING_DATE", "OCR読取フィールド名設定（交換希望日）");
            string SCAN_COUNT = GetAppSettingsStr("OCRKey_SCAN_COUNT", "OCR読取フィールド名設定（スキャン枚数）");
            string TOTAL_COUNT = GetAppSettingsStr("OCRKey_TOTAL_COUNT", "OCR読取フィールド名設定（合計枚数）");
            string TOTAL_AMOUNT = GetAppSettingsStr("OCRKey_TOTAL_AMOUNT", "OCR読取フィールド名設定（合計金額）");
            string AMOUNT = GetAppSettingsStr("OCRKey_AMOUNT", "OCR読取フィールド名設定（金額）");
            string BILL = GetAppSettingsStr("OCRKey_BILL", "OCR読取フィールド名設定（交換証券種類）");
            string Memo = GetAppSettingsStr("OCRKey_MEMO", "OCR読取フィールド名設定（メモ情報）");
            int OCRLevel = GetAppSettingsInt("OCRLevel", "OCR信頼度閾値");
            OCRSettingData = new ImportTRAccessCommon.OCRSettingData(DspIDDefault, OCRLevel, OC_BK_NO, OC_BR_NO, IC_BK_NO, SCAN_BR_NO, SCAN_DATE,
                                                                     CLEARING_DATE, SCAN_COUNT, TOTAL_COUNT, TOTAL_AMOUNT, AMOUNT, BILL, Memo);

            bool rtn = CheckAppConfigImagePattern();

            return rtn;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckAppConfigImagePattern()
        {
            int PatternCount = GetAppSettingsInt("ScanImagePatternCount", "イメージファイル名定義件数", true);

            for (int i = 1; i <= PatternCount; i++)
            {
                string Pattern = GetAppSettingsStr(string.Format("ScanImagePattern{0}", i.ToString()), string.Format("イメージファイル名{0}", i.ToString()));
                string KeyFront = GetAppSettingsStr(string.Format("ScanImagePatternKeyFront{0}", i.ToString()), string.Format("表キーワード{0}", i.ToString()), false);
                string KeyBack = GetAppSettingsStr(string.Format("ScanImagePatternKeyBack{0}", i.ToString()), string.Format("裏キーワード{0}", i.ToString()), false);
                int SerNoFlg = GetAppSettingsInt(string.Format("ScanImagePatternSerNoFlg{0}", i.ToString()), string.Format("連番フラグ{0}", i.ToString()), false);

                ScanImagePattern.Add(new ScanImage(Pattern, KeyFront, KeyBack, SerNoFlg));
            }

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
