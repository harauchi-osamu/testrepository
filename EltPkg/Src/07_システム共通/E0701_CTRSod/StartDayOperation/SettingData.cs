using System;
using CommonClass;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace StartDayOperation
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
        /// exe.config DB設定
        /// </summary>
        public List<ConfDBData> DBData { get; private set; } = new List<ConfDBData>();

        /// <summary>
        /// exe.config ファイル削除設定
        /// </summary>
        public List<ConfFileParam> FileParam { get; private set; } = new List<ConfFileParam>();

        /// <summary>
        /// exe.config イメージファイル削除定義
        /// </summary>
        public bool UnMatchTRImgDelete { get; private set; } = false;

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
            if (!System.IO.File.Exists(NCR.Server.IniPath))
            {
                ChkServerIni = false;
            }

            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public bool CheckAppData()
        {
            // 固定設定定義
            UnMatchTRImgDelete = GetAppSettingsBool("UNMATCH_TRIMGDELETE", "イメージファイル削除設定");

            // 動的設定定義
            foreach (string Key in ConfigurationManager.AppSettings)
            {
                try
                {
                    if (Key == "FILEDELETE")
                    {
                        SetFileParam(Key, GetAppSettingsStr(Key, Key), FileParam);
                    }
                    else if (Key == "UNMATCH_TRIMGDELETE")
                    {
                        // この定義は無視
                    }
                    else
                    {
                        DBData.Add(new ConfDBData(Key, GetAppSettingsStr(Key, Key)));
                    }
                }
                catch
                {
                    SetCheckParamMsg(Key);
                }
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
        /// configの取得
        /// True/Falseのみ有効
        /// </summary>
        public bool GetAppSettingsBool(string Key, string ItemName, bool EmptyChk = true)
        {
            bool bWork = false;

            try
            {
                string sKeyData = ConfigurationManager.AppSettings[Key];
                if (string.IsNullOrWhiteSpace(sKeyData) && !EmptyChk)
                {
                    sKeyData = "False";
                }
                if (!bool.TryParse(sKeyData, out bWork))
                {
                    throw new Exception("Error");
                }
            }
            catch
            {
                SetCheckParamMsg(ItemName);
            }

            return bWork;
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

        /// <summary>
        /// ファイル削除情報の設定
        /// </summary>
        private void SetFileParam(string Key, string Value, List<ConfFileParam> ListParam)
        {
            if (!Value.StartsWith("\"") || !Value.EndsWith("\""))
            {
                throw new Exception("定義エラー");
            }
            string[] sep = { "\",\"" };
            string[] Data = Value.Remove(Value.Length - 1, 1).Remove(1, 1).Split(sep, StringSplitOptions.None);

            foreach (string Param in Data)
            {
                ListParam.Add(new ConfFileParam(Param));
            }
        }

        /// <summary>
        /// DB更新パラメーター
        /// </summary>
        public class ConfDBData
        {
            public int Order { get; set; } = 0;
            public string TBLName { get; set; } = "";
            public int TBLType { get; set; } = 0;
            public int DiffDate { get; set; } = 0;
            public string SQL { get; set; } = "";

            public ConfDBData(string Key, string Value)
            {
                string[] Keys = Key.Split(':');
                Order = int.Parse(Keys[0]);
                TBLName = Keys[1];

                if (!Value.StartsWith("\"") || !Value.EndsWith("\""))
                {
                    throw new Exception("定義エラー");
                }
                string[] sep = { "\"|\"" };
                string[] Data = Value.TrimStart('"').TrimEnd('"').Split(sep, StringSplitOptions.None);
                TBLType = int.Parse(Data[0]);
                DiffDate = int.Parse(Data[1]);
                SQL = Data[2];
            }
        }

        /// <summary>
        /// ファイル削除パラメーター
        /// </summary>
        public class ConfFileParam
        {
            //「処理タイプ | 日付期間 | 削除ルートフォルダ設定」形式
            // "1"|"3"|"[BankIO][IOReceiveRoot]"
            // "2"|"3"|"\\127.0.0.1\HULFT\LOG"

            public int ProcessType { get; set; } = 0;
            public int DiffDate { get; set; } = 0;
            public string iniSectionName { get; set; } = "";
            public string iniKeyName { get; set; } = "";
            public string FolderPath { get; set; } = "";

            public ConfFileParam(string Value)
            {
                if (!Value.StartsWith("\"") || !Value.EndsWith("\""))
                {
                    throw new Exception("定義エラー");
                }
                string[] sep = { "\"|\"" };
                string[] Data = Value.TrimStart('"').TrimEnd('"').Split(sep, StringSplitOptions.None);

                ProcessType = int.Parse(Data[0]);
                DiffDate = int.Parse(Data[1]);

                switch (ProcessType)
                {
                    case 0:
                    case 1:
                        if (!Data[2].StartsWith("[") || !Data[2].EndsWith("]"))
                        {
                            throw new Exception("定義エラー");
                        }
                        string[] sepSub = { "][" };
                        string[] DataSub = Data[2].TrimStart('[').TrimEnd(']').Split(sepSub, StringSplitOptions.None);
                        iniSectionName = DataSub[0];
                        iniKeyName = DataSub[1];
                        break;
                    case 2:
                        FolderPath = Data[2];
                        break;
                    default:
                        throw new Exception("定義エラー");
                }
            }
        }

    }
}
