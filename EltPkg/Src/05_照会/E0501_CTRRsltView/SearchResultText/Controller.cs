using System;
using CommonClass;
using EntryCommon;

namespace SearchResultText
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
        public string filedivid { get; set; } = string.Empty;

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

            // 引数の識別区分を設定
            this._itemMgr.ResultDispParams.FileDivid = filedivid;
            // 処理日を設定
            this._itemMgr.ResultDispParams.RecvDate = AplInfo.OpDate();
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

            if (args.Length >= 2)
            {
                filedivid = args[1];
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
            SettingData.ChkParam(NCR.Operator.UserID, "ユーザーID");
            SettingData.ChkParam(NCR.Operator.UserName, "ユーザー名");
            // 特権レベルは設定がなければ「0」扱い

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
            SettingData.ChkParam(NCR.Server.BankNormalImageRoot, "通常バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.BankFutaiImageRoot, "付帯バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.BankInventoryImageRoot, "期日管理バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.BankConfirmImageRoot, "持帰ダウンロード確定イメージルート(銀行別)");

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
