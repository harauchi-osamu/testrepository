using System;
using EntryCommon;

namespace InclearingConfirm
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        /// <summary>取込モード</summary>
        public RunMode ConfirmMode { get; private set; } = RunMode.NormalOcr;

        /// <summary>設定ファイル情報</summary>
        public SettingData SettingData { get; private set; } = new SettingData();

        /// <summary>
        /// 起動モード
        /// </summary>
        public enum RunMode
        {
            /// <summary>通常（OCR済）</summary>
            NormalOcr = 0,
            /// <summary>通常（OCR未済）</summary>
            NormalErrOcr = 1,
            /// <summary>異例（OCR済）</summary>
            ExceptionOcr = 2,
            /// <summary>異例（OCR未済）</summary>
            ExceptionErrOcr = 3,
        }

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
            string MenuNumber = args[0];
            base.MenuNumber = MenuNumber;

            if(!int.TryParse(args[1], out int intMode))
            {
                return false;
            }
            switch (intMode)
            {
                case (int)RunMode.NormalOcr:
                case (int)RunMode.NormalErrOcr:
                case (int)RunMode.ExceptionOcr:
                case (int)RunMode.ExceptionErrOcr:
                    ConfirmMode = (RunMode)intMode;
                    break;
                default:
                    return false;
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
            SettingData.ChkParam(NCR.Server.BankCheckImageRoot, "持帰ダウンロード確定前イメージルート(銀行別)");
            SettingData.ChkParam(NCR.Server.BankConfirmImageRoot, "持帰ダウンロード確定イメージルート(銀行別)");
            SettingData.ChkParam(NCR.Server.IC_OCRLevel, "OCR信頼度比較(持帰)");
            // OCRオプション導入はZeroOK

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

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

    }
}
