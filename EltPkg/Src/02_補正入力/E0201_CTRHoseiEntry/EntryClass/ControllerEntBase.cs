using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using NCR;

namespace EntryClass
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class ControllerEntBase : ControllerBase
    {
        /// <summary>設定ファイル情報</summary>
        public SettingData SettingData { get; set; } = new SettingData();

        /// <summary>業務ID</summary>
        public int GymId { get; set; } = -1;

        /// <summary>補正項目モード</summary>
        public int HoseiItemMode { get; set; } = -1;

        /// <summary>補正入力モード</summary>
        public int HoseiInputMode { get; set; } = -1;

        /// <summary>業務メンテナンス画面を起動するかどうか</summary>
        public bool IsMaint { get; set; } = false;

        /// <summary>完了訂正かどうか</summary>
        public bool IsKanryouTeisei { get; set; }
        /// <summary>連続訂正かどうか</summary>
        public bool IsRenzokuTeisei { get; set; }
        /// <summary>参照モードかどうか</summary>
        public bool IsDspReadOnly { get; set; }
        public int OpeDate { get; set; } = -1;
        public string ScanTerm { get; set; } = "";
        public int BatId { get; set; } = -1;
        public int DetailsNo { get; set; } = -1;
        public List<int> ReadOnlyItemIdList { get; set; } = null;

        protected const string COMMA = ",";


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
		}

        /// <summary>
        /// 引数を設定する
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool SetArgs(string[] args)
        {
            // 第1引数：メニュー番号
            // 第2引数：メンテナンスモード
            //        ：1:業務、2:メンテナンス
            // 第3引数：業務ID
            //        ：0:共通、1:持出、2:持帰
            // 第4引数：補正項目モード
            //        ：1:持帰銀行、2:交換日、3:金額、4:自行情報、5:完了訂正交換尻、6:完了訂正自行情報
            // 第5引数：明細キー
            //        ：{処理日}|{イメージ取込端末}|{バッチ番号}|{明細番号}（完了訂正時のみ指定）※ 省略は連続訂正
            string menuNo = args[0];
            int mainteType = 0;
            int gymId = 0;
            int hoseiItemMode = 0;
            if (!Int32.TryParse(args[1], out mainteType))
            {
                return false;
            }
            if (!Int32.TryParse(args[2], out gymId))
            {
                return false;
            }
            if (!Int32.TryParse(args[3], out hoseiItemMode))
            {
                return false;
            }
            this.MenuNumber = menuNo;
            this.IsMaint = (mainteType == 2);
            this.GymId = gymId;
            this.IsKanryouTeisei = false;
            this.IsRenzokuTeisei = false;
            this.IsDspReadOnly = false;
            this.ReadOnlyItemIdList = new List<int>();
            switch (hoseiItemMode)
            {
                case 1:
                    this.HoseiItemMode = HoseiParam.HoseiItemMode.持帰銀行;
                    this.HoseiInputMode = HoseiStatus.HoseiInputMode.持帰銀行;
                    break;
                case 2:
                    this.HoseiItemMode = HoseiParam.HoseiItemMode.交換希望日;
                    this.HoseiInputMode = HoseiStatus.HoseiInputMode.交換希望日;
                    break;
                case 3:
                    this.HoseiItemMode = HoseiParam.HoseiItemMode.金額;
                    this.HoseiInputMode = HoseiStatus.HoseiInputMode.金額;
                    break;
                case 4:
                    this.HoseiItemMode = HoseiParam.HoseiItemMode.自行情報;
                    this.HoseiInputMode = HoseiStatus.HoseiInputMode.自行情報;
                    break;
                case 5:
                    this.HoseiItemMode = HoseiParam.HoseiItemMode.交換尻;
                    this.HoseiInputMode = HoseiStatus.HoseiInputMode.交換尻;
                    this.IsKanryouTeisei = true;
                    break;
                case 6:
                    this.HoseiItemMode = HoseiParam.HoseiItemMode.自行情報;
                    this.HoseiInputMode = HoseiStatus.HoseiInputMode.自行情報;
                    this.IsKanryouTeisei = true;
                    break;
            }

            // 完了訂正
            if (!IsMaint && IsKanryouTeisei)
            {
                if (args.Length < 5)
                {
                    // 完了訂正で明細キーが省略されている場合は連続訂正
                    this.IsRenzokuTeisei = true;
                }
                else
                {
                    string[] keys = CommonUtil.DivideKeys(args[4]);
                    if (keys.Length < 4) { return false; }
                    this.OpeDate = DBConvert.ToIntNull(keys[0]);
                    this.ScanTerm = DBConvert.ToStringNull(keys[1]);
                    this.BatId = DBConvert.ToIntNull(keys[2]);
                    this.DetailsNo = DBConvert.ToIntNull(keys[3]);
                }
            }
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
            SettingData.ChkParam(NCR.Server.BankConfirmImageRoot, "持帰ダウンロード確定イメージルート(銀行別)");
            SettingData.ChkParam(NCR.Server.BankSampleImagePath, "サンプルイメージパス");
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
