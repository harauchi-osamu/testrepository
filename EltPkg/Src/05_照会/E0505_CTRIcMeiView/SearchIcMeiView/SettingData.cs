using System;
using CommonClass;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace SearchIcMeiView
{

    /// <summary>
    /// 設定ファイル情報
    /// </summary>
    public class SettingData
    {

        #region enum

        public enum FileOutType
        {
            ///<summary>csv</summary>
            Csv = 1,
            ///<summary>tsv</summary>
            Tsv = 2,
            ///<summary>固定</summary>
            Txt = 3,
        }

        #endregion

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
        /// exe.config 一覧表示上限
        /// </summary>
        public int ListDispLimit { get; private set; } = 0;
        /// <summary>
        /// exe.config 印鑑照合モードの背景色設定
        /// </summary>
        public string InkanSortBackColor { get; private set; } = "";
        /// <summary>
        /// exe.config ファイル出力形式
        /// </summary>
        public int FileOutPutType { get; private set; } = 0;
        /// <summary>
        /// exe.config イメージ縮小率
        /// </summary>
        public int ImageReduceRate { get; private set; } = 100;
        /// <summary>
        /// exe.config 印鑑照合モードの証券種類条件
        /// </summary>
        public string InkanSortBillNoList { get; private set; } = "";

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
            ListDispLimit = GetAppSettingsInt("ListDispLimit", "一覧表示上限");
            InkanSortBackColor = GetAppSettingsStr("InkanSortBackColor", "印鑑照合モードの背景色設定");
            FileOutPutType = GetAppSettingsInt("FileOutPutType", "ファイル出力形式");
            ImageReduceRate = GetAppSettingsInt("ImageReduceRate", "イメージ縮小率");
            InkanSortBillNoList = GetAppSettingsStr("InkanSortBillNoList", "印鑑照合モードの証券種類条件");

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
