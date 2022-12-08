using System;
using System.Reflection;
using System.Text;
using Common;
using CommonClass;
using EntryCommon;

namespace SearchOcMeiView
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
        /// <summary>バッチID</summary>
        public int BatID { get; set; } = -1;

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

            // 引数の処理日バッチ番号を設定
            if (Date > -1)
            {
                this._itemMgr.DispParams.Rdate = Date;
            }
            else
            {
                this._itemMgr.DispParams.Rdate = AplInfo.OpDate();
            }
            if (BatID > -1)
            {
                this._itemMgr.DispParams.BatNo = BatID;
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
                BatID = 0;
                string[] Keys = CommonUtil.DivideKeys(args[1], "_");
                if (Keys.Length >= 2)
                {
                    if (int.TryParse(Keys[0], out int intdate))
                    {
                        Date = intdate;
                    }
                    if (int.TryParse(Keys[1], out int intbat))
                    {
                        BatID = intbat;
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
            SettingData.ChkParam(NCR.Server.ScanImageReplaceRoot, "スキャン差替フォルダ情報");
            SettingData.ChkParam(NCR.Server.ScanImageBackUpRoot, "スキャン退避フォルダ情報");
            SettingData.ChkParam(NCR.Server.BankNormalImageRoot, "通常バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.BankFutaiImageRoot, "付帯バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.BankInventoryImageRoot, "期日管理バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.Tegata, "イメージ切取手形枠");
            SettingData.ChkParam(NCR.Server.Kogitte, "イメージ切取小切手枠");
            SettingData.ChkParam(NCR.Server.DstDpi, "イメージ切取解像度");
            SettingData.ChkParam(NCR.Server.Quality, "イメージ切取クオリティ");
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
