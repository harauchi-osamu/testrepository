using System;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace MainMenu
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        public int _BankCd { get; private set; } = -1;
        public string _TargetFilename { get; private set; } = string.Empty;

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

        /// <summary>
        /// exe.config リトライ回数
        /// </summary>
        public int RetryCount { get; private set; } = 0;

        /// <summary>
        /// exe.config スリープタイム
        /// </summary>
        public int SleepTime { get; private set; } = 0;

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// コントローラー初期化
        /// </summary>  
        /// <param name="mst"></param>
        /// <param name="item"></param>
        public override void SetManager(MasterManager mst, ManagerBase item)
        {
            base.SetManager(mst, item);
            _masterMgr = MasterMgr;
            _itemMgr = (ItemManager)ItemMgr;
        }

        /// <summary>
        /// 引数を設定する
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool SetArgs(string[] args)
        {
            string IniPath = args[0];
            NCR.Server.IniPath = IniPath;

            string BankCD = args[1];
            if (!int.TryParse(BankCD, out int intBank))
            {
                throw new Exception("銀行コードが不正です");
            }
            _BankCd = intBank;

            string FileName = args[2];
            _TargetFilename = FileName;

            return true;
        }

        #region 設定チェック

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckServerIni()
        {
            // ServerIniファイル存在チェック
            if (!ServerIniExists())
            {
                return false;
            }

            ChkParam(NCR.Server.BankNM, "銀行名");
            ChkParam(NCR.Server.Environment, "環境");
            ChkParam(NCR.Server.ExePath, "実行パス");
            ChkParam(NCR.Server.ReceiveRoot, "HULFT集信フォルダ");
            ChkParam(NCR.Server.IOReceiveRoot, "IO集信フォルダ(銀行別)");

            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            RetryCount = GetAppSettingsInt("RetryCount", "リトライ回数");
            SleepTime = GetAppSettingsInt("SleepTime", "スリープタイム（ミリ秒）");

            return true;
        }

        #endregion 

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// Server.ini 存在チェック
        /// </summary>
        /// <returns></returns>
        private bool ServerIniExists()
        {
            // ServerIniファイル存在チェック
            if (!System.IO.File.Exists(NCR.Server.IniPath))
            {
                ChkServerIni = false;
            }

            return true;
        }

        /// <summary>
        /// 設定内容チェック
        /// </summary>
        private bool ChkParam(string Item, string ItemName)
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
        private bool ChkParam(int Item, string ItemName)
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
        private string GetAppSettingsStr(string Key, string ItemName, bool EmptyChk = true)
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
        private int GetAppSettingsInt(string Key, string ItemName, bool EmptyChk = true)
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
