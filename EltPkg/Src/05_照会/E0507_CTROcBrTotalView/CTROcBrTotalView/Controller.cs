using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using EntryCommon;
using NCR;

namespace CTROcBrTotalView
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
            SettingData.ChkParam(NCR.Operator.BankCD, "銀行コード");
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

            SettingData.ChkParam(NCR.Server.ExePath, "実行プログラム");
            SettingData.ChkParam(NCR.Server.ContractBankCd, "受託元銀行番号");
            SettingData.ChkParam(NCR.Server.BankTotalImageRoot, "持出支店別合計票パス情報(銀行別)");
            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            return true;
        }

        /// <summary>
        /// 銀行名を取得する
        /// </summary>
        /// <param name="bkno"></param>
        /// <returns></returns>
        public string GetBankName(int bkno)
        {
            if (!_itemMgr.mst_banks.ContainsKey(bkno)) { return ""; }
            return _itemMgr.mst_banks[bkno].m_BK_NAME_KANJI;
        }

        /// <summary>
        /// 支店名を取得する
        /// </summary>
        /// <param name="brno"></param>
        /// <returns></returns>
        public string GetBranchName(int brno)
        {
            if (!_itemMgr.mst_branches.ContainsKey(brno)) { return ""; }
            return _itemMgr.mst_branches[brno].m_BR_NAME_KANJI;
        }

        /// <summary>
        /// 支店名を取得する
        /// </summary>
        /// <param name="brno"></param>
        /// <returns></returns>
        public string GetScanBranchName(int brno)
        {
            if (!_itemMgr.mst_scanbranches.ContainsKey(brno)) { return ""; }
            return _itemMgr.mst_scanbranches[brno].m_BR_NAME_KANJI;
        }

    }
}
