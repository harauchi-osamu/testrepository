using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using EntryCommon;
using IFFileOperation;

namespace MainMenu
{
    class FileImport
    {
        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private Dictionary<string, string> FileIDDividList = null;
        #endregion

        // XMLPath
        private string IF208XMLPath { get { return Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources/IF208LoadMonitor.xml"); } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileImport(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
            FileIDDividList = GetFileIDDividList();
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 取込処理
        /// この処理では対象銀行が不明のため銀行別のDB関連は使用不可
        /// </summary>
        public AppMain.Type Import()
        {
            IEnumerable<string> FileList = Directory.EnumerateFiles(_itemMgr.HULFTReceiveRoot(), "*.txt").OrderBy(name => File.GetCreationTime(name));
            foreach (string FullPathName in FileList)
            {
                string fileName = Path.GetFileName(FullPathName);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("処理開始：{0}", fileName), 3);

                // ファイル名から「ファイルID」「ファイル識別区分」「銀行番号」取得
                _itemMgr.GetFileIDDivIDBankCD(fileName, out string file_id, out string file_divid, out string bankcd);

                if (!FileIDDividList.ContainsKey(file_id + "|" + file_divid))
                {
                    //一覧になければ次のファイル
                    continue;
                }

                // 対象ファイルのロック確認
                bool LockChk = false;
                for (int i = 1; i <= _ctl.FileLockRetryCount; i++)
                {
                    // ロック確認
                    if (ChkFileLock(FullPathName))
                    {
                        LockChk = true;
                        break;
                    }

                    //指定時間スリープ
                    System.Threading.Thread.Sleep(_ctl.FileLockSleepTime);
                }
                if (!LockChk)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイルがロックされているため、処理が継続できませんでした{0}", fileName), 3);
                    return AppMain.Type.Warning;
                }

                // 銀行番号の確認
                switch (file_id)
                {
                    case "IF211":
                        bankcd = NCR.Server.ContractBankCd;
                        break;
                    case "IF208":
                        //設定可能な銀行一覧チェック
                        if (!NCR.Server.HandlingBankCdList.Split(',').Contains(bankcd))
                        {
                            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取得ファイルの銀行番号が不正です{0}", fileName), 3);
                            return AppMain.Type.Error;
                        }

                        // ヘッダ・レコードチェック
                        IFFileDataLoad LoadFile = new IFFileDataLoad(FullPathName, IF208XMLPath);
                        // データ読み込み
                        LoadFile.IFDataLoad();
                        if (!(LoadFile.LoadData.Count(x => x.KBN == "1") == 1 && LoadFile.LoadData.First().KBN == "1"))
                        {
                            throw new Exception("ヘッダ・レコードが不正です");
                        }
                        bankcd = LoadFile.LoadData.First().LineData["銀行コード"];
                        if (!NCR.Server.HandlingBankCdList.Split(',').Contains(bankcd))
                        {
                            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取得ファイルの銀行番号が不正です{0}", fileName), 3);
                            return AppMain.Type.Error;
                        }
                        break;
                    default:
                        // IF206YCAの場合は銀行コードを設定
                        if (file_id == "IF206" && file_divid == "YCA") { bankcd = NCR.Server.ContractBankCd; }

                        //設定可能な銀行一覧チェック
                        if (!NCR.Server.HandlingBankCdList.Split(',').Contains(bankcd))
                        {
                            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取得ファイルの銀行番号が不正です{0}", fileName), 3);
                            return AppMain.Type.Error;
                        }
                        break;
                }

                //実行処理
                string WorkDir = string.Format(NCR.Server.ExePath, bankcd);
                string Argument = string.Format("\"{0}\" {1} \"{2}\"", NCR.Server.IniPath, bankcd, fileName);
                string ExeName = string.Empty;
                switch (file_id)
                {
                    case "IF201":
                        // 証券明細テキスト取込
                        ExeName = "CTRBillMeiTxtRd.exe";
                        break;
                    case "IF206":
                        // 結果テキスト取込
                        ExeName = "CTRRsltTxtRd.exe";
                        break;
                    case "IF207":
                        // 持帰要求結果テキスト取込
                        ExeName = "CTRIcReqRsltRd.exe";
                        break;
                    case "IF208":
                        // 通知テキスト取込
                        ExeName = "CTRNoticeTxtRd.exe";
                        break;
                    case "IF209":
                    case "IF210":
                        // 交換尻データ取込
                        ExeName = "CTRBalanceTxtRd.exe";
                        break;
                    case "IF211":
                        // サービス通知
                        ExeName = "CTRServiceTxtRd.exe";
                        break;
                }
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("プログラム起動：{0} 引数：{1}", ExeName, Argument), 3);
                int Rtn = ProcessManager.RunProcess(Path.Combine(WorkDir, ExeName), WorkDir, Argument, true, false);
                if (Rtn != 0)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取込実行処理でエラーが発生しました{0} 戻り値：{1}", ExeName, Rtn), 3);
                    return AppMain.Type.Error;
                }
            }

            return AppMain.Type.Success;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// 「ファイルID」「ファイル識別区分」一覧取得
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetFileIDDividList()
        {
            Dictionary<string, string> Item = new Dictionary<string, string>();

            Item.Add("IF201|BQA", "証券明細テキスト 当日持出明細");
            Item.Add("IF201|SPA", "証券明細テキスト 交換尻データ(持出証券分速報版)");
            Item.Add("IF201|SPB", "証券明細テキスト 交換尻データ(持帰証券分速報版)");
            Item.Add("IF201|SFA", "証券明細テキスト 交換尻データ(持出証券分確定版)");
            Item.Add("IF201|SFB", "証券明細テキスト 交換尻データ(持帰証券分確定版)");
            Item.Add("IF206|BUB", "結果テキスト 持出アップロード");
            Item.Add("IF206|BCA", "結果テキスト 持出取消");
            Item.Add("IF206|GMA", "結果テキスト 証券データ訂正");
            Item.Add("IF206|GRA", "結果テキスト 不渡返還");
            Item.Add("IF206|YCA", "結果テキスト 判別不可");
            Item.Add("IF207|GDA", "持帰要求結果テキスト 持帰ダウンロード");
            Item.Add("IF208|BUA", "通知テキスト 二重持出通知(持出)");
            Item.Add("IF208|BUB", "通知テキスト 二重持出通知(持帰)");
            Item.Add("IF208|BCA", "通知テキスト 持出取消");
            Item.Add("IF208|GMA", "通知テキスト 証券データ訂正");
            Item.Add("IF208|GMB", "通知テキスト 証券データ訂正通知(持帰)");
            Item.Add("IF208|GRA", "通知テキスト 不渡返還");
            Item.Add("IF208|GXA", "通知テキスト 決済後訂正通知(持出)");
            Item.Add("IF208|GXB", "通知テキスト 決済後訂正通知(持帰)");
            Item.Add("IF208|MRA", "通知テキスト 金融機関読替情報変更通知(持出変更・承継銀行向け)");
            Item.Add("IF208|MRB", "通知テキスト 金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け)");
            Item.Add("IF208|MRC", "通知テキスト 金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け)");
            Item.Add("IF208|MRD", "通知テキスト 金融機関読替情報変更通知(持帰変更・承継銀行向け)");
            Item.Add("IF208|YCA", "通知テキスト 判定不可");
            Item.Add("IF209|SPA", "交換尻データ（参加銀行用）交換尻データ(速報版)");
            Item.Add("IF209|SFA", "交換尻データ（参加銀行用）交換尻データ(確定版)");
            Item.Add("IF210|SPA", "交換尻データ（決済受託銀行用）：交換尻データ(速報版)");
            Item.Add("IF210|SFA", "交換尻データ（決済受託銀行用）：交換尻データ(確定版)");
            Item.Add("IF211|YSA", "サービス開始終了通知テキスト サービス開始通知");
            Item.Add("IF211|YSB", "サービス開始終了通知テキスト サービス終了通知");
            Item.Add("IF211|YSC", "サービス開始終了通知テキスト 障害復旧通知");

            return Item;
        }

        /// <summary>
        /// ファイルロック確認
        /// </summary>
        /// <returns></returns>
        private bool ChkFileLock(string FullPathName)
        {
            try
            {
                //排他でファイルを開く
                using (FileStream fs = new FileStream(FullPathName, FileMode.Open, FileAccess.Read, FileShare.None)) { }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
