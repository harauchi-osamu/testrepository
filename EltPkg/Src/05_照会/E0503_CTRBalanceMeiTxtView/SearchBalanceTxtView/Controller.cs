using System;
using System.Reflection;
using System.Text;
using Common;
using CommonClass;
using EntryCommon;
using CommonTable.DB;

namespace SearchBalanceTxtView
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        /// <summary>引数の業務ID</summary>
        public int GymID { get; private set; } = 0;

        /// <summary>設定ファイル情報</summary>
        public SettingData SettingData { get; private set; } = new SettingData();

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

            // 初期値を設定
            this._itemMgr.DispParams.CtlDate = AplInfo.OpDate();
            this._itemMgr.DispParams.PayKbn = 0;
            this._itemMgr.DispParams.Diff = 1;
            this._itemMgr.DispParams.Fuwatari = SettingData.FuwatariDef;
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

            // 持出・持帰のみOK
            if (!int.TryParse(args[1], out int intGymID))
            {
                return false;
            }
            if (!(intGymID == GymParam.GymId.持出 || intGymID == GymParam.GymId.持帰))
            {
                return false;
            }
            GymID = intGymID;

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
