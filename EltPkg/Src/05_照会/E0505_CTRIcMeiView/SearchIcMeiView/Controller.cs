using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using EntryCommon;

namespace SearchIcMeiView
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
        /// <summary>処理日</summary>
        public int Date { get; set; } = -1;
        /// <summary>交換希望日</summary>
        public int ClearingDate { get; set; } = -1;
        /// <summary>持出銀行ｺｰﾄﾞ</summary>
        public int OcBkNo { get; set; } = -1;

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

            // 引数の処理日・交換希望日・持出銀行ｺｰﾄﾞを設定
            if (Date > 0)
            {
                this._itemMgr.DispParams.Rdate = Date;
            }
            else if (Date == -1)
            {
                this._itemMgr.DispParams.Rdate = AplInfo.OpDate();
            }
            if (ClearingDate > 0)
            {
                this._itemMgr.DispParams.ClearingDate = ClearingDate;
            }
            if (OcBkNo > -1)
            {
                this._itemMgr.DispParams.OcBkNo = OcBkNo;
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

            if (args.Length >= 2)
            {
                Date = 0;
                OcBkNo = 0;
                ClearingDate = 0;
                string[] Keys = CommonUtil.DivideKeys(args[1] + "_", "_");
                if (Keys.Length >= 3)
                {
                    if (int.TryParse(Keys[0], out int intbkno))
                    {
                        OcBkNo = intbkno;
                    }
                    if (int.TryParse(Keys[1], out int intcdate))
                    {
                        ClearingDate = intcdate;
                    }
                    if (int.TryParse(Keys[2], out int intdate))
                    {
                        Date = intdate;
                    }
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
            SettingData.ChkParam(NCR.Server.BankConfirmImageRoot, "持帰ダウンロード確定イメージルート(銀行別)");
            SettingData.ChkParam(NCR.Server.ReportFileOutPutPath, "ファイル出力パス");

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
