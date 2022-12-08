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

namespace CTRImgListForm
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

        /// <summary>引数：業務ID</summary>
        public int GymId { get; set; } = -1;


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
            // 第1引数：業務ID
            //        ：1:持出、2:持帰
            string menuNo = args[0];
            int gymId = 0;
            if (!Int32.TryParse(args[1], out gymId))
            {
                return false;
            }
            if (!(gymId == GymParam.GymId.持出 || gymId == GymParam.GymId.持帰))
            {
                return false;
            }
            this.MenuNumber = menuNo;
            this.GymId = gymId;

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
    }
}
