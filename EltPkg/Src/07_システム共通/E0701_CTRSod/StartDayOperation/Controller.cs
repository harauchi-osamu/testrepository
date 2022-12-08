using System;
using CommonClass;
using EntryCommon;

namespace StartDayOperation
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {

        #region 定義

        public enum enumExecMode
        {
            ///<summary>サイレントモード</summary>
            SilentMode = 1,
            ///<summary>確認モード</summary>
            ViewMode = 2,
            ///<summary>強制日付変更モード</summary>
            ChangeMode = 3,
        }

        #endregion

        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        /// <summary>設定ファイル情報</summary>
        public SettingData SettingData { get; private set; } = new SettingData();

        public enumExecMode ExecMode { get; set; } = enumExecMode.SilentMode;
        public int SetGymDate { get; set; } = 0;

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 初期処理
        /// </summary>  
        public override void SetManager(MasterManager mst, ManagerBase item)
		{
			base.SetManager(mst, item);
			_masterMgr = MasterMgr;
			_itemMgr = (ItemManager)ItemMgr;

            // 現在の業務日付を設定
            this._itemMgr.DispParams.CurGymDate = AplInfo.OpDate();

            // 更新処理日の初期設定
            iBicsCalendar cal = new iBicsCalendar();
            cal.SetHolidays();
            if (ExecMode == enumExecMode.SilentMode)
            {
                // サイレントモードの場合、引数の日付を初期設定
                this._itemMgr.DispParams.UpdateGymDate = SetGymDate;
            }
            else
            {
                // サイレントモード以外の場合、業務日付の翌営業日を初期設定
                this._itemMgr.DispParams.UpdateGymDate = cal.getBusinessday(AplInfo.OpDate(), 1);
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

            //起動モード設定
            if (!int.TryParse(args[1], out int Mode))
            {
                return false;
            }
            switch (Mode)
            {
                case (int)enumExecMode.SilentMode:
                case (int)enumExecMode.ViewMode:
                case (int)enumExecMode.ChangeMode:
                    ExecMode = (enumExecMode)Mode;
                    break;
                default:
                    return false;
            }

            // Serverini
            if (ExecMode == enumExecMode.SilentMode)
            {
                if (args.Length >= 4)
                {
                    string IniPath = args[2];
                    NCR.Server.IniPath = IniPath;

                    // 設定日付
                    if (!int.TryParse(args[3], out int SetDate))
                    {
                        // 数値チェックエラー
                        return false;
                    }
                    if (!EntryCommon.Calendar.IsDate(SetDate.ToString()))
                    {
                        // 日付チェックエラー
                        return false;
                    }
                    SetGymDate = SetDate;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        #region 設定チェック

        /// <summary>
        /// Operator.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckOperatorIni()
        {
            if (ExecMode != enumExecMode.SilentMode)
            {
                SettingData.ChkParam(NCR.Operator.UserID, "ユーザーID");
                SettingData.ChkParam(NCR.Operator.UserName, "ユーザー名");
            }

            return true;
        }

        /// <summary>
        /// Term.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckTermIni()
        {
            if (ExecMode != enumExecMode.SilentMode)
            {
                SettingData.ChkParam(NCR.Terminal.Number, "端末番号");
                SettingData.ChkParam(NCR.Terminal.ServeriniPath, "CtrServer.iniパス");
            }

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

        #endregion 

    }
}
