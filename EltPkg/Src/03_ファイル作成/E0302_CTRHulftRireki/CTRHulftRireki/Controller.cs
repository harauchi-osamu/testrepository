using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using EntryCommon;

namespace CTRHulftRireki
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        /// <summary>設定ファイル情報</summary>
        public SettingData SettingData { get; private set; } = new SettingData();

        public bool IsIniErr { get { return (!string.IsNullOrEmpty(this.SettingData.CheckParamMsg) || !this.SettingData.ChkServerIni); } }

        public string SendLogFilePath { get; set; } = "";
        public string RecvLogFilePath { get; set; } = "";
        public string SendErrLogFilePath { get; set; } = "";
        public string RecvErrLogFilePath { get; set; } = "";


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 管理クラスを設定する
        /// </summary>
        /// <param name="mst"></param>
        /// <param name="item"></param>
        public override void SetManager(MasterManager mst, ManagerBase item)
		{
			base.SetManager(mst, item);
			_masterMgr = MasterMgr;
			_itemMgr = (ItemManager)ItemMgr;

            // 一覧表示自動更新設定(0：チェックなし、1：チェックあり)
            switch (SettingData.AutoRefreshDef)
            {
                case 1:
                    _itemMgr.HeaderInfo.IsAutoRefresh = true;
                    break;
                default:
                    _itemMgr.HeaderInfo.IsAutoRefresh = false;
                    break;
            }
        }

        /// <summary>
        /// 引数を設定する
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool SetArgs(string[] args)
        {
			string MenuNumber = args[0];
			base.MenuNumber = MenuNumber;

            // セクション名の設定
            string LogSectionName = args[1];
            NCR.Server.HULFTLogSectionName = LogSectionName;

            return true;
        }

        /// <summary>
        /// Operator.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckOperatorIni()
        {
            SettingData.ChkParam(NCR.Operator.UserID, "ユーザーID");
            SettingData.ChkParam(NCR.Operator.UserName, "ユーザー名");
            return true;
        }

        /// <summary>
        /// Term.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckTermIni()
        {
            SettingData.ChkParam(NCR.Terminal.Number, "端末番号");
            SettingData.ChkParam(NCR.Terminal.ServeriniPath, "CtrServer.iniパス");
            return true;
        }

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckServerIni()
        {
            // ServerIniファイル存在チェック
            if (!SettingData.ServerIniExists())
            {
                return false;
            }

            SettingData.ChkParam(NCR.Server.BankNM, "銀行名");
            SettingData.ChkParam(NCR.Server.Environment, "環境");
            //SettingData.ChkParam(NCR.Server.SendRoot, "HULFT配信フォルダ");
            //SettingData.ChkParam(NCR.Server.ReceiveRoot, "HULFT集信フォルダ");
            //SettingData.ChkParam(NCR.Server.BackupRoot, "HULFT集信退避フォルダ");
            //SettingData.ChkParam(NCR.Server.LogRoot, "HULFT集配信ログフォルダ");
            //SettingData.ChkParam(NCR.Server.LogErrRoot, "HULFT集配信エラーログフォルダ");
            SettingData.ChkParam(NCR.Server.IOSendRoot, "IO配信フォルダ(銀行別)");

            // HULFTセクション別設定
            SettingData.ChkParam(NCR.Server.HulftEXELogRoot, "HULFT関連電子交換パッケージプログラムログフォルダ（他端末参照用）");
            SettingData.ChkParam(NCR.Server.RemoteSendRoot, "HULFT配信フォルダ（他端末参照用）");
            SettingData.ChkParam(NCR.Server.RemoteReceiveRoot, "HULFT集信フォルダ（他端末参照用）");
            SettingData.ChkParam(NCR.Server.BackupRoot, "HULFT集信退避フォルダ");
            SettingData.ChkParam(NCR.Server.LogRoot, "HULFT集配信ログフォルダ");
            SettingData.ChkParam(NCR.Server.LogErrRoot, "HULFT集配信エラーログフォルダ");
            SettingData.ChkParam(NCR.Server.LogRecvFileName, "HULFT集信ログファイル名");
            SettingData.ChkParam(NCR.Server.LogErrRecvFileName, "HULFT集信エラーログファイル名");
            SettingData.ChkParam(NCR.Server.LogSendFileName, "HULFT配信ログファイル名");
            SettingData.ChkParam(NCR.Server.LogErrSendFileName, "HULFT配信エラーログファイル名");

            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            return SettingData.CheckAppData();
        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        public void InitializeControl()
        {
            //string sOpedate = AplInfo.OpDate().ToString();
            //string logDirPath = Path.Combine(ServerIni.Setting.LogRoot, sOpedate);
            //string errLogDirPath = Path.Combine(ServerIni.Setting.LogErrRoot, sOpedate);

            string logDirPath = ServerIni.Setting.LogRoot;
            string errLogDirPath = ServerIni.Setting.LogErrRoot;

            // ログフォルダ作成
            Directory.CreateDirectory(logDirPath);
            Directory.CreateDirectory(errLogDirPath);

            SendLogFilePath = Path.Combine(logDirPath, ServerIni.Setting.LogSendFileName);
            RecvLogFilePath = Path.Combine(logDirPath, ServerIni.Setting.LogRecvFileName);
            SendErrLogFilePath = Path.Combine(errLogDirPath, ServerIni.Setting.LogErrSendFileName);
            RecvErrLogFilePath = Path.Combine(errLogDirPath, ServerIni.Setting.LogErrRecvFileName);
        }
    }
}
