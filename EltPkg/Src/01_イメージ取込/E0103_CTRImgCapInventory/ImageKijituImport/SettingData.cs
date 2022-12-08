using System;
using CommonClass;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace ImageKijituImport
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
        /// exe.config 一意コード算出時スリープタイム（ミリ秒）
        /// </summary>
        public int UniqueCodeSleepTime { get; private set; } = 0;

        /// <summary>
        /// exe.config バッチ番号採番リトライ回数
        /// </summary>
        public int BatchSeqRetryCount { get; private set; } = 0;

        /// <summary>
        /// exe.config 業務連携データファイル名
        /// </summary>
        public string GymDataFileName { get; private set; } = string.Empty;

        /// <summary>
        /// exe.config 「表・裏等の別」ファイル位置
        /// </summary>
        public string FileNameKbnPosition { get; private set; } = string.Empty;

        /// <summary>
        /// exe.config 表の「表・裏等の別」値
        /// </summary>
        public string KbnCodeOmote { get; private set; } = string.Empty;

        /// <summary>
        /// exe.config 裏の「表・裏等の別」値
        /// </summary>
        public string KbnCodeUra { get; private set; } = string.Empty;

        /// <summary>
        /// exe.config 補箋の「表・裏等の別」値
        /// </summary>
        public string KbnCodeHosen { get; private set; } = string.Empty;

        /// <summary>
        /// exe.config 付箋の「表・裏等の別」値
        /// </summary>
        public string KbnCodeFusen { get; private set; } = string.Empty;

        /// <summary>
        /// exe.config 入金証明の「表・裏等の別」値
        /// </summary>
        public string KbnCodeNyukin { get; private set; } = string.Empty;

        /// <summary>設定情報</summary>
        public ImportTRAccessCommon.OCRSettingDataKijitu OCRSettingData { get; private set; } = null;

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
            UniqueCodeSleepTime = GetAppSettingsInt("UniqueCodeSleepTime", "一意コード算出時スリープタイム");
            BatchSeqRetryCount = GetAppSettingsInt("BatchSeqRetryCount", "バッチ番号採番リトライ回数");
            GymDataFileName = GetAppSettingsStr("GymDataFileName", "業務連携データファイル名");
            FileNameKbnPosition = GetAppSettingsStr("FileNameKbnPosition", "「表・裏等の別」ファイル位置");
            KbnCodeOmote = GetAppSettingsStr("KbnCodeOmote", "表の「表・裏等の別」値");
            KbnCodeUra = GetAppSettingsStr("KbnCodeUra", "裏の「表・裏等の別」値");
            KbnCodeHosen = GetAppSettingsStr("KbnCodeHosen", "補箋の「表・裏等の別」値");
            KbnCodeFusen = GetAppSettingsStr("KbnCodeFusen", "付箋の「表・裏等の別」値");
            KbnCodeNyukin = GetAppSettingsStr("KbnCodeNyukin", "入金証明の「表・裏等の別」値");

            // OCR関連
            int DspIDDefault = GetAppSettingsInt("DspIDDefault", "画面ID設定");
            string IC_BK_NO = GetAppSettingsStr("OCRKey_IC_BK_NO", "OCR読取フィールド名設定（持帰銀行コード）");
            string AMOUNT = GetAppSettingsStr("OCRKey_AMOUNT", "OCR読取フィールド名設定（金額）");
            string TEGATAKIJITU = GetAppSettingsStr("OCRKey_TEGATAKIJITU", "OCR読取フィールド名設定（手形期日）");
            string CLEARING_DATE = GetAppSettingsStr("OCRKey_CLEARING_DATE", "OCR読取フィールド名設定（交換希望日）");
            string TEGATA = GetAppSettingsStr("OCRKey_TEGATA", "OCR読取フィールド名設定（手形種類）");
            string IC_BR_NO = GetAppSettingsStr("OCRKey_IC_BR_NO", "OCR読取フィールド名設定（持帰支店コード）");
            string KOUZANUMBER = GetAppSettingsStr("OCRKey_KOUZANUMBER", "OCR読取フィールド名設定（口座番号）");
            string TEGATANUMBER = GetAppSettingsStr("OCRKey_TEGATANUMBER", "OCR読取フィールド名設定（手形番号）");

            OCRSettingData = new ImportTRAccessCommon.OCRSettingDataKijitu(DspIDDefault, IC_BK_NO, AMOUNT, TEGATAKIJITU, 
                                                                     CLEARING_DATE, TEGATA, IC_BR_NO, KOUZANUMBER, TEGATANUMBER);

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
