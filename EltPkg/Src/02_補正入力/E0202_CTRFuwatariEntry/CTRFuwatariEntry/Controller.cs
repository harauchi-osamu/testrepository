using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using NCR;

namespace CTRFuwatariEntryForm
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

        /// <summary>引数：業務ID</summary>
        public int GymId { get; set; } = -1;
        /// <summary>引数：処理日</summary>
        public int OpeDate { get; set; } = -1;
        /// <summary>引数：イメージ取込端末</summary>
        public string ScanTerm { get; set; } = "";
        /// <summary>引数：バッチ番号</summary>
        public int BatId { get; set; } = -1;
        /// <summary>引数：明細番号</summary>
        public int DetailsNo { get; set; } = -1;


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
            // 第1引数：メニュー番号
            // 第2引数：明細キー
            //        ：{処理日}|{イメージ取込端末}|{バッチ番号}|{明細番号}
            string menuNo = args[0];
            this.MenuNumber = menuNo;

            string[] keys = CommonUtil.DivideKeys(args[1], "|");
            if (keys.Length < 4) { return false; }
            this.GymId = GymParam.GymId.持帰;
            this.OpeDate = DBConvert.ToIntNull(keys[0]);
            this.ScanTerm = DBConvert.ToStringNull(keys[1]);
            this.BatId = DBConvert.ToIntNull(keys[2]);
            this.DetailsNo = DBConvert.ToIntNull(keys[3]);

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
            return true;
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
        /// 証券種類名を取得する
        /// </summary>
        /// <param name="billcd"></param>
        /// <returns></returns>
        public string GetBillName(int billcd)
        {
            if (!_itemMgr.mst_bills.ContainsKey(billcd)) { return ""; }
            return _itemMgr.mst_bills[billcd].m_STOCK_NAME;
        }

        /// <summary>
        /// 項目定義桁数を取得する
        /// </summary>
        /// <param name="billcd"></param>
        /// <returns></returns>
        public int GetDSPItemLen(int itemid)
        {
            if (!_itemMgr.mst_dspitems.ContainsKey(itemid)) { return 0; }
            return _itemMgr.mst_dspitems[itemid].m_ITEM_LEN;
        }

    }
}
