using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace ImageKijituImport
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class KijituImport : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private const string TITLEMSG1 = "期日管理システムからの連携データを取り込みます。";
        private const string TITLEMSG2 = "連携データ取り込み中。。。";
        private const string TITLEMSG3 = "連携データの取り込みが完了しました。";
        private const string TITLEMSG4 = "連携データの取り込みがエラー終了しました。";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KijituImport()
        {
            InitializeComponent();
        }
        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        public override void InitializeForm(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            base.InitializeForm(ctl);
        }
        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName1(string dispName)
        {
            base.SetDispName1("交換持出");
        }
        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("期日管理データ取込");
        }
        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (IsNotPressCSAKey)
            {
                // 通常状態
                SetFunctionName(1, "終了");
                SetFunctionName(2, string.Empty);
                SetFunctionName(3, string.Empty);
                SetFunctionName(4, string.Empty);
                SetFunctionName(5, string.Empty);
                SetFunctionName(6, string.Empty);
                SetFunctionName(7, string.Empty);
                SetFunctionName(8, string.Empty);
                SetFunctionName(9, string.Empty);
                SetFunctionName(10, string.Empty);
                SetFunctionName(11, string.Empty);
                SetFunctionName(12, "取込開始", true, Const.FONT_SIZE_FUNC_LOW);
            }
            else
            {
                // Shiftキー押下
                SetFunctionName(1, string.Empty);
                SetFunctionName(2, string.Empty);
                SetFunctionName(3, string.Empty);
                SetFunctionName(4, string.Empty);
                SetFunctionName(5, string.Empty);
                SetFunctionName(6, string.Empty);
                SetFunctionName(7, string.Empty);
                SetFunctionName(8, string.Empty);
                SetFunctionName(9, string.Empty);
                SetFunctionName(10, string.Empty);
                SetFunctionName(11, string.Empty);
                SetFunctionName(12, string.Empty);
            }
        }
        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
        }
        /// <summary>
        /// フォームを再描画する
        /// </summary>
        public override void ResetForm()
        {
            // 画面表示データ更新
            InitializeDisplayData();
            // 画面表示データ更新
            RefreshDisplayData();

            // 画面表示状態更新
            RefreshDisplayState();
        }
        /// <summary>
        /// コントロール初期化
        /// </summary>
        protected void InitializeControl()
        {
        }
        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
            SetFormMsg(TITLEMSG1);
        }
        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        protected override void RefreshDisplayData()
        {
            // 画面項目設定           
            SetDisplayParams();

        }
        /// <summary>
        /// 画面表示状態更新
        /// </summary>
        protected override void RefreshDisplayState()
        {
            // ファンクションキー状態を設定
            SetFunctionState();
        }
        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {

        }
        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            return true;
        }
        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            iBicsCalendar cal = new iBicsCalendar();
            cal.SetHolidays();

            // 設定ファイル読み込みでエラー発生時
            if (this._ctl.SettingData.ChkServerIni == false)
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00003));
                return;
            }
            if (!string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E01001, this._ctl.SettingData.CheckParamMsg));
                return;
            }
        }

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            //this.ClearStatusMessage();

            if (ChangeFunction(e)) SetFunctionState(); return;
        }

        /// <summary>
        /// [画面項目] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyUp(object sender, KeyEventArgs e)
        {
            if (ChangeFunction(e)) SetFunctionState(); return;
        }


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F12：取込開始
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                // 画面表示文字初期設定
                InitializeDisplayData();

                //確認メッセージ表示
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "期日管理データの取り込みを開始します。よろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "取込開始", 1);

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00002);

                    //確定処理
                    if (!BatchInput())
                    {
                        return;
                    }
                }
                finally
                {
                    //メッセージ初期化
                    EndProcessing(CommonClass.ComMessageMgr.I00002);
                }

            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }
        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 確定処理
        /// </summary>
        private bool BatchInput()
        {
            try
            {
                DBManager.InitializeSql2(NCR.Server.DBDataSource, NCR.Server.DBUserID, NCR.Server.DBPassword);
                DBManager.dbs2.Open();

                // 登録処理
                using (AdoDatabaseProvider dbs2 = GenDbProviderFactory.CreateAdoProvider3())
                using (AdoNonCommitTransaction Tran2 = new AdoNonCommitTransaction(dbs2))
                {
                    try
                    {
                        // 期日管理でのロック取得
                        if (!_itemMgr.GetLockProcessSystem(dbs2, Tran2))
                        {
                            // 取得した行ロックを解除するためロールバック
                            // メッセージボックス表示前に実施
                            Tran2.Trans.Rollback();
                            // メッセージ表示
                            ComMessageMgr.MessageWarning("他端末でデータ取込処理中です。しばらくお待ちください");
                            return false;
                        }

                        //取込対象データの取得
                        GetImportList(out List<ItemManager.ImportData> ImportList, out List<ItemManager.ImportData> ProcessedList);

                        if (ImportList.Count == 0)
                        {
                            // 取得した行ロックを解除するためロールバック
                            // メッセージボックス表示前に実施
                            Tran2.Trans.Rollback();
                            // メッセージ表示
                            ComMessageMgr.MessageInformation("取込対象のデータが存在しません");
                            return false;
                        }

                        if (ProcessedList.Count != 0)
                        {
                            //確認メッセージ表示
                            if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "取込済のデータが存在します。\n取込未済データのみ取込対象とします。よろしいですか？") == DialogResult.No)
                            {
                                return true;
                            }
                        }

                        // 画面表示文字処理中設定
                        SetFormMsg(TITLEMSG2);

                        //取込処理実施
                        ItemManager.ImportResult result = new ItemManager.ImportResult();
                        foreach (ItemManager.ImportData data in ImportList)
                        {
                            // 業務連携データの取得
                            if (!getXmlData(Path.Combine(data.FolderName, _ctl.SettingData.GymDataFileName), out Gymdata gymdata))
                            {
                                //業務連携ファイルがない場合は次のフォルダ
                                result.BatchImportFail += 1;
                                continue;
                            }

                            //業務データから持出銀行の取得
                            int BankCode = GetBankCode(gymdata);
                            if (BankCode == 0)
                            {
                                //持出銀行データが指定の銀行コード以外の場合は次のフォルダ
                                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "持出銀行が指定銀行コード以外です", 1);
                                result.BatchImportFail += 1;
                                continue;
                            }

                            // 登録済のデータのバッチ番号取得
                            bool BatchProcessedFlg = _itemMgr.GetImportBatchNumber(AplInfo.OpDate(), data.BatchFolderName, BankCode, out string ScanTerm, out int BatchNumber, out int DetailNo);
                            if (!BatchProcessedFlg)
                            {
                                //登録済のデータからバッチ番号が取得できない場合
                                DetailNo = 1;
                                for (int i = 1; i <= _ctl.SettingData.BatchSeqRetryCount; i++)
                                {
                                    if (_itemMgr.GetBatchNumber(AplInfo.OpDate(), BankCode, out BatchNumber))
                                    {
                                        break;
                                    }
                                }
                                if (BatchNumber <= 0)
                                {
                                    //バッチ番号が取得できない場合
                                    result.BatchImportFail += 1;
                                    continue;
                                }

                                // 端末番号設定
                                ScanTerm = ImportFileAccess.GetTermIPAddress().Replace(".", "_");
                            }

                            // データの登録処理
                            TRManager insTR = new TRManager(_ctl, AplInfo.OpDate(), BankCode, BatchNumber, ScanTerm, data, gymdata, BatchProcessedFlg, DetailNo);
                            if (!insTR.TRDataInput(ref result))
                            {
                                result.BatchImportFail += 1;
                                continue;
                            }

                            // イメージ退避処理
                            if (!ImportFileAccess.ImageBackUp(data.FolderName, NCR.Server.ScanImageBackUpRoot))
                            {
                                //退避処理エラーの場合
                                // DBの登録処理は完了しているため、エラーは表示するが次の処理を行う
                                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ退避処理でエラーが発生しました", 3);
                            }

                            // 業務連携ファイル退避処理
                            if (!ImportFileAccess.FileBackUp(data.FolderName, _ctl.SettingData.GymDataFileName, NCR.Server.ScanImageBackUpRoot))
                            {
                                //退避処理エラーの場合
                                // DBの登録処理は完了しているため、エラーは表示するが次の処理を行う
                                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "業務連携ファイル退避処理でエラーが発生しました", 3);
                            }
                        }

                        //結果表示
                        System.Drawing.Color color = System.Drawing.Color.Transparent;
                        string Msg = string.Empty;
                        if (result.DetailImportSuccess > 0)
                        {
                            Msg += string.Format("正常終了 取込データ総計：{0:#,0}件、{1:#,0}円 ", result.DetailImportSuccess, result.DetailImportTotalAmt);
                        }
                        if (result.BatchImportFail > 0)
                        {
                            color = System.Drawing.Color.Salmon;
                            Msg += string.Format("異常終了 エラーバッチ件数：{0:#,0}件 ", result.BatchImportFail);
                        }
                        this.SetStatusMessage(Msg, color);

                        // コミット
                        Tran2.Trans.Commit();

                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("確定処理終了 {0}", Msg), 1);

                        // 画面表示文字完了設定
                        SetFormMsg(TITLEMSG3);

                        // 削除・アップロード済の件数取得
                        long DelUploadCnt = ProcessedList.Sum(x => x.Detail.LongCount(y => y.DeleteFlg && y.UploadFlg));
                        if (DelUploadCnt != 0)
                        {
                            //メッセージ表示
                            ComMessageMgr.MessageInformation("持出取消済データが{0:#,#}件存在します。\n（該当データは取込対象外）", DelUploadCnt);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran2.Trans.Rollback();
                        // 画面表示文字エラー設定
                        SetFormMsg(TITLEMSG4);
                        // メッセージ表示
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        //エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                        return false;
                    }
                    //finally
                    //{
                    //    // コミット
                    //    Tran2.Trans.Commit();
                    //}
                }
            }
            finally
            {
                if (DBManager.dbs2 != null)
                {
                    DBManager.dbs2.Close();
                    DBManager.dbs2 = null;
                }
            }
            return true;
        }

        /// <summary>
        /// 取込対象一覧取得
        /// </summary>
        private void GetImportList(out List<ItemManager.ImportData> ImportList, out List<ItemManager.ImportData> ProcessedList)
        {
            ImportList = new List<ItemManager.ImportData>();
            ProcessedList = new List<ItemManager.ImportData>();
            ItemManager.ImportData importData = null;
            ItemManager.ImportData processedData = null;

            //対象一覧取得
            string Folder = _itemMgr.TargetFolderPath();
            string BatchFolder = Path.GetFileName(Folder);
            importData = new ItemManager.ImportData();
            processedData = new ItemManager.ImportData();
            importData.FolderName = Folder;
            importData.BatchFolderName = BatchFolder;
            processedData.FolderName = Folder;
            processedData.BatchFolderName = BatchFolder;

            if (!getXmlData(Path.Combine(Folder, _ctl.SettingData.GymDataFileName), out Gymdata gymdata))
            {
                //業務連携ファイルがない場合は終了
                return;
            }

            //業務データから持出銀行の取得
            int BankCode = GetBankCode(gymdata);
            if (BankCode == 0)
            {
                //持出銀行データが指定の銀行コード以外の場合は
                //一旦、すべてを取込対象とする。
                foreach (img img in gymdata.img)
                {
                    importData.Detail.Add(new ItemManager.ImportImgInfo(img.img_FrontName, img.img_memo, "", false, false, img.imgocr));
                }
                if (importData.Detail.Count > 0) ImportList.Add(importData);
                return;
            }

            foreach (img img in gymdata.img)
            {
                // 表証券イメージファイル名が登録済か確認
                if (_itemMgr.ChkImportDetail(BankCode, BatchFolder, img.img_FrontName, out string ImportKey, out bool DeleteFlg, out bool UploadFlg))
                {
                    processedData.Detail.Add(new ItemManager.ImportImgInfo(img.img_FrontName, img.img_memo, ImportKey, DeleteFlg, UploadFlg, img.imgocr));
                    continue;
                }

                // 未登録の場合
                importData.Detail.Add(new ItemManager.ImportImgInfo(img.img_FrontName, img.img_memo, ImportKey, DeleteFlg, UploadFlg, img.imgocr));
            }

            if (importData.Detail.Count > 0) ImportList.Add(importData);
            if (processedData.Detail.Count > 0) ProcessedList.Add(processedData);
        }

        /// <summary>
        /// 業務データの取得処理
        /// </summary>
        private bool getXmlData(string GymPath, out Gymdata gymdata)
        {
            gymdata = null;

            if (!File.Exists(GymPath))
            {
                //業務連携ファイルがない場合
                return false;
            }

            // 業務データの取得
            gymdata = LoadXmlFile(GymPath);

            return true;
        }

        /// <summary>
        /// 業務データから持出銀行の取得
        /// </summary>
        private int GetBankCode(Gymdata gymdata)
        {
            if (gymdata.batch.Count == 0 || gymdata.batch[0].batchocr.Count == 0)
            {
                // データがない場合ZEROを返す
                return 0;
            }
            //存在チェック
            int BankCode = gymdata.batch[0].batchocr[0].batchocr_ocbank;
            if (!NCR.Server.HandlingBankCdList.Split(',').Contains(BankCode.ToString(Const.BANK_NO_LEN_STR)))
            {
                // 設定銀行にない場合ZEROを返す
                return 0;
            }

            return BankCode;
        }

        /// <summary>
        /// 業務連携データの取得
        /// </summary>
        private Gymdata LoadXmlFile(string FilePath)
        {
            Gymdata xmlData = new Gymdata();

            // 対象データの取得
            string xmlText;
            using (StreamReader sr = new StreamReader(FilePath, System.Text.Encoding.GetEncoding("utf-8")))
            {
                xmlText = sr.ReadToEnd();
            }
            if (string.IsNullOrEmpty(xmlText)) return xmlData;

            XmlSerializer deserializer = new XmlSerializer(typeof(Gymdata));
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlText)))
            {
                xmlData = deserializer.Deserialize(stream) as Gymdata;
            }

            return xmlData;
        }

        /// <summary>
        /// 指定エレメント箇所の取得
        /// </summary>
        private string GetSubElement(string xmlText, string stText, string edText, ref int st, ref int ed)
        {
            string strTarget = string.Empty;
            st = xmlText.IndexOf(stText, st);
            ed = xmlText.IndexOf(edText, ed);

            if (st >= 0)
            {
                strTarget = xmlText.Substring(st, ed - st + edText.Length);
                st += stText.Length;
                ed += edText.Length;
            }

            return strTarget;
        }

        #region 処理中設定

        /// <summary>
        /// 処理中状態に設定
        /// </summary>
        private void Processing(string msg)
        {
            // ファンクションDisable
            DisableAllFunctionState(false);

            SetWaitCursor();
            this.SetStatusMessage(msg, System.Drawing.Color.Transparent);
            this.Refresh();
        }

        /// <summary>
        /// 処理中状態を解除する
        /// </summary>
        private void EndProcessing(string msg)
        {
            // Disableにしたファンクションを元に戻す
            InitializeFunction();
            SetFunctionState();

            if (this.GetStatusMessage() == msg)
            {
                //メッセージが同じ場合クリア
                this.ClearStatusMessage();
            }
            ResetCursor();
        }

        #endregion

        #region 画面メッセージ設定

        /// <summary>
        /// 画面メッセージ設定
        /// </summary>
        private void SetFormMsg(string msg)
        {
            lblTitle.Text = msg;
            this.Refresh();
        }

        #endregion

    }
}

