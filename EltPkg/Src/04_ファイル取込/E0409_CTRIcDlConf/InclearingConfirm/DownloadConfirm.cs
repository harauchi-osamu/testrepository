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
using IFImportCommon;

namespace InclearingConfirm
{
    /// <summary>
    /// 持帰ダウンロード確定
    /// </summary>
    public partial class DownloadConfirm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private bool _ConfirmFlg = false;

        #region 連携データ関連プロパティ

        /// <summary>
        /// 電子交換所連携データ
        /// </summary>
        private IEnumerable<ItemManager.ConfirmData> ConfirmDataKbn0
        {
            get { return _itemMgr._ConfirmData.Where(x => x.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN] == "0"); }
        }

        /// <summary>
        /// 行内交換連携データ
        /// </summary>
        private IEnumerable<ItemManager.ConfirmData> ConfirmDataKbn1
        {
            get { return _itemMgr._ConfirmData.Where(x => x.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN] == "1"); }
        }

        /// <summary>
        /// 電子交換所連携データ
        /// 持帰要求結果あり
        /// </summary>
        private IEnumerable<ItemManager.ConfirmData> ConfirmDataKbn0CTLFlg1
        {
            get { return ConfirmDataKbn0.Where(x => x.LineData["CTLFLG"] == "1"); }
        }

        /// <summary>
        /// 電子交換所連携データ
        /// 持帰要求結果なし
        /// </summary>
        private IEnumerable<ItemManager.ConfirmData> ConfirmDataKbn0CTLFlg0
        {
            get { return ConfirmDataKbn0.Where(x => x.LineData["CTLFLG"] == "0"); }
        }

        /// <summary>
        /// 電子交換所連携データ
        /// 持帰要求結果あり
        /// 未持帰
        /// </summary>
        private IEnumerable<ItemManager.ConfirmData> ConfirmDataKbn0CTLFlg1NotAvailableIMG
        {
            get { return ConfirmDataKbn0CTLFlg1.Where(x => !_itemMgr.ChkSkipConfirmData(x.LineData, _itemMgr._ConfirmData)); }
        }

        /// <summary>
        /// 電子交換所連携データ
        /// 持帰要求結果あり
        /// 持帰済
        /// </summary>
        private IEnumerable<ItemManager.ConfirmData> ConfirmDataKbn0CTLFlg1AvailableIMG
        {
            get { return ConfirmDataKbn0CTLFlg1.Where(x => _itemMgr.ChkSkipConfirmData(x.LineData, _itemMgr._ConfirmData)); }
        }

        /// <summary>
        /// 電子交換所連携データ
        /// 持帰要求結果なし
        /// 未持帰
        /// </summary>
        private IEnumerable<ItemManager.ConfirmData> ConfirmDataKbn0CTLFlg0NotAvailableIMG
        {
            get { return ConfirmDataKbn0CTLFlg0.Where(x => !_itemMgr.ChkSkipConfirmData(x.LineData, _itemMgr._ConfirmData)); }
        }

        /// <summary>
        /// 電子交換所連携データ
        /// 持帰要求結果なし
        /// 持帰済
        /// </summary>
        private IEnumerable<ItemManager.ConfirmData> ConfirmDataKbn0CTLFlg0AvailableIMG
        {
            get { return ConfirmDataKbn0CTLFlg0.Where(x => _itemMgr.ChkSkipConfirmData(x.LineData, _itemMgr._ConfirmData)); }
        }

        #endregion

        #region データエラー関連プロパティ

        /// <summary>
        /// 集信エラー関連
        /// </summary>
        private bool ICReqRetErr
        {
            get
            {
                // 未取込データチェック
                // イメージファイル突合せチェック
                return (_itemMgr._ConfirmData.Count(x => x.ImgExistChk == false) > 0 || _itemMgr._NotImportCnt > 0);
            }
        }

        /// <summary>
        /// 重複データあり
        /// </summary>
        private bool OverRide
        {
            get
            {
                return (_itemMgr._ConfirmData.Where(x => _itemMgr.ChkSkipConfirmData(x.LineData, _itemMgr._ConfirmData)).Count() > 0);
            }
        }

        /// <summary>
        /// OCR未決エラー関連
        /// </summary>
        private bool ICOCRFinishErr
        {
            get
            {
                if (NCR.Server.OCROption == 1)
                {
                    // OCROptionありの場合
                    return (_itemMgr._ConfirmData.Where(x => x.ICOcrFinish == false).Count() > 0);
                }
                return false;
            }
        }

        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DownloadConfirm()
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
            base.SetDispName1("交換持帰");
        }
        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("持帰ダウンロード確定");
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
                SetFunctionName(5, "最新表示", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(6, string.Empty);
                SetFunctionName(7, string.Empty);
                SetFunctionName(8, string.Empty);
                SetFunctionName(9, string.Empty);
                SetFunctionName(10, string.Empty);
                SetFunctionName(11, string.Empty);
                SetFunctionName(12, "確定");
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
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            if (this._ctl.SettingData.ChkServerIni == false || !string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                // 設定ファイル読み込みでエラー発生時はF1以外Disable
                DisableAllFunctionState(true);
            }
            else
            {
                if (IsNotPressCSAKey)
                {
                    // 通常状態
                    switch (_ctl.ConfirmMode)
                    {
                        case Controller.RunMode.NormalOcr:
                            // 通常（OCR済）
                            if (ICReqRetErr || ICOCRFinishErr)
                            {
                                //どれかエラーの場合、確定ファンクション不可
                                SetFunctionState(12, false);
                            }
                            else
                            {
                                SetFunctionState(12, true);
                            }

                            break;
                        case Controller.RunMode.NormalErrOcr:
                            // 通常（OCR未済）
                            if (ICReqRetErr)
                            {
                                //OCR未済以外のエラーがある場合、確定ファンクション不可
                                SetFunctionState(12, false);
                            }
                            else
                            {
                                SetFunctionState(12, true);
                            }

                            break;
                        case Controller.RunMode.ExceptionOcr:
                            // 異例（OCR済）
                            if (ICOCRFinishErr)
                            {
                                //OCR未済エラーがある場合、確定ファンクション不可
                                SetFunctionState(12, false);
                            }
                            else
                            {
                                SetFunctionState(12, true);
                            }

                            break;
                        case Controller.RunMode.ExceptionErrOcr:
                            // 異例（OCR未済）
                            break;
                    }
                }
            }
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
            //起動モード
            // 通常状態
            switch (_ctl.ConfirmMode)
            {
                case Controller.RunMode.NormalOcr:
                    // 通常（OCR済）
                    lblRunMode.Text = "【通常（OCR済）】";
                    break;
                case Controller.RunMode.NormalErrOcr:
                    // 通常（OCR未済）
                    lblRunMode.Text = "【通常（OCR未済）】";
                    break;
                case Controller.RunMode.ExceptionOcr:
                    // 異例（OCR済）
                    lblRunMode.Text = "【異例（OCR済）】";
                    break;
                case Controller.RunMode.ExceptionErrOcr:
                    // 異例（OCR未済）
                    lblRunMode.Text = "【異例（OCR未済）】";
                    break;
            }

            //画面表示
            GetConfirmData();
            DispInit();
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

            if (this._ctl.SettingData.ChkServerIni == false)
            {
                // 設定ファイル読み込みでエラー発生時
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00003));
            }
            else if (!string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                // 設定ファイル読み込みでエラー発生時
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E01001, this._ctl.SettingData.CheckParamMsg));
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
        /// F05：最新表示
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "最新表示", 1);

                //画面表示最新化
                ResetForm();
                SetFunctionState();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：確定
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                if (_ConfirmFlg)
                {
                    ComMessageMgr.MessageInformation("取込予定を最新化してください");
                    return;
                }

                if (_itemMgr._ConfirmData.Count == 0)
                {
                    ComMessageMgr.MessageInformation("取込データが存在しません");
                    return;
                }

                if (ICReqRetErr || ICOCRFinishErr)
                {
                    //どれかエラーの場合、確認メッセージ表示
                    if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "取込予定証券データで不整合データがありますが確定処理を行ってもよろしいですか？") == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    // 上記以外

                    //確認メッセージ表示
                    if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "確定処理を開始します。よろしいですか？") == DialogResult.No)
                    {
                        return;
                    }
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00002);

                    // 処理実行
                    if (!Confirm())
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
        private bool Confirm()
        {
            TRManager trManager = new TRManager(_ctl);

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
                        // 持帰ダウンロードでのロック取得
                        if (!_itemMgr.GetLockProcessBank(dbs2, Tran2))
                        {
                            // 取得した行ロックを解除するためロールバック
                            // メッセージボックス表示前に実施
                            Tran2.Trans.Rollback();
                            // メッセージ表示
                            ComMessageMgr.MessageWarning("他端末でデータ取込処理中です。しばらくお待ちください");
                            return false;
                        }

                        //確定処理実施
                        ItemManager.ImportResult result = new ItemManager.ImportResult();

                        //表証券単位でループ
                        foreach (IGrouping<string, ItemManager.ConfirmData> FrontNameGrp in _itemMgr._ConfirmData.GroupBy(x => x.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]))
                        {
                            // 登録処理
                            trManager.TRDataInput(FrontNameGrp, result);
                        }

                        // アーカイブフォルダの削除
                        foreach (IGrouping<string, ItemManager.ConfirmData> ArchGrp in _itemMgr._ConfirmData.GroupBy(x => x.LineData[TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME]))
                        {
                            string outExtensionName = Path.GetFileNameWithoutExtension(ArchGrp.Key);
                            InclearingFileAccess.DeleteArchFolder(_itemMgr.BankCheckImageRoot(), outExtensionName, NCR.Server.ScanImageBackUpRoot);
                        }

                        //結果表示
                        lblResultTotalMeiCnt.Text = DispDataFormat(result.DetailImportSuccess, "#,##0");
                        lblResultTotalImgCnt.Text = DispDataFormat(result.DetailImgImportSuccess, "#,##0");
                        _ConfirmFlg = true;

                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), 
                                           string.Format("明細取込成功件数:{0} イメージ取込成功件数:{1} 明細取込失敗件数:{2}", 
                                                         result.DetailImportSuccess, result.DetailImgImportSuccess, result.DetailImportFail), 1);
                        string msgAdd = string.Empty;
                        if (result.UnDeleteDetailSuccess > 0)
                        {
                            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("交換日訂正データが{0}件ありました", result.UnDeleteDetailSuccess), 1);
                            msgAdd = string.Format("\n（交換日訂正分が{0:#,0}件ありました。）", result.UnDeleteDetailSuccess);
                        }

                        // 取得した行ロックを解除するためコミット
                        // メッセージボックス表示前に実施
                        Tran2.Trans.Commit();
                        // メッセージ表示
                        if (result.DetailImportFail > 0)
                        {
                            this.SetStatusMessage(string.Format("取込エラー エラー明細件数：{0:#,0}件 {1}", result.DetailImportFail, msgAdd.Replace("\n", "")));
                            ComMessageMgr.MessageWarning(string.Format("取込エラーが発生しました。{0}", msgAdd));
                        }
                        else
                        {
                            this.SetStatusMessage(string.Format("取込完了 {0}", msgAdd.Replace("\n", "")), System.Drawing.Color.Transparent);
                            ComMessageMgr.MessageInformation(string.Format("取込が完了しました。{0}", msgAdd));
                        }
                    }
                    catch (Exception ex)
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran2.Trans.Rollback();
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
        /// 処理対象データ取得
        /// </summary>
        private void GetConfirmData()
        {
            //データ取得
            _itemMgr.GetConfirmData(this);
            _itemMgr.GetNotImportCnt(this);
            //イメージ突合せ
            ChkImageFile();
        }

        /// <summary>
        ///画面表示初期化
        /// </summary>
        private void DispInit()
        {
            //結果欄初期化
            lblResultTotalMeiCnt.Text = string.Empty;
            lblResultTotalImgCnt.Text = string.Empty;
            _ConfirmFlg = false;

            // 電子交換所・持帰要求結果あり・未持帰分 
            lblICRetInCompMeiCnt.Text = DispDataFormat(ConfirmDataKbn0CTLFlg1NotAvailableIMG.GroupBy(x => x.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]).LongCount(), "#,##0");
            lblICRetInCompImgCnt.Text = DispDataFormat(ConfirmDataKbn0CTLFlg1NotAvailableIMG.LongCount(), "#,##0");

            // 電子交換所・持帰要求結果あり・持帰済分
            lblICRetCompMeiCnt.Text = DispDataFormat(ConfirmDataKbn0CTLFlg1AvailableIMG.GroupBy(x => x.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]).LongCount(), "#,##0");
            lblICRetCompImgCnt.Text = DispDataFormat(ConfirmDataKbn0CTLFlg1AvailableIMG.LongCount(), "#,##0");

            // 持帰要求結果なし・未持帰分 
            lblNoRetInCompMeiCnt.Text = DispDataFormat(ConfirmDataKbn0CTLFlg0NotAvailableIMG.GroupBy(x => x.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]).LongCount(), "#,##0");
            lblNoRetInCompImgCnt.Text = DispDataFormat(ConfirmDataKbn0CTLFlg0NotAvailableIMG.LongCount(), "#,##0");

            // 持帰要求結果なし・持帰済分
            lblNoRetCompMeiCnt.Text = DispDataFormat(ConfirmDataKbn0CTLFlg0AvailableIMG.GroupBy(x => x.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]).LongCount(), "#,##0");
            lblNoRetCompImgCnt.Text = DispDataFormat(ConfirmDataKbn0CTLFlg0AvailableIMG.LongCount(), "#,##0");

            // 行内交換連携
            lblExchangeMeiCnt.Text = DispDataFormat(ConfirmDataKbn1.GroupBy(x => x.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]).LongCount(), "#,##0");
            lblExchangeImgCnt.Text = DispDataFormat(ConfirmDataKbn1.LongCount(), "#,##0");

            // 合計
            lblTotalMeiCnt.Text = DispDataFormat(_itemMgr._ConfirmData.GroupBy(x => x.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]).LongCount(), "#,##0");
            lblTotalImgCnt.Text = DispDataFormat(_itemMgr._ConfirmData.LongCount(), "#,##0");

            // 集配信未済あり
            lblICReqRetErr.Text = ICReqRetErr ? "●" : "";
            // 重複データあり
            lblOverRideErr.Text = OverRide ? "●" : "";
            // OCR未済あり
            lblICOCRFinishErr.Text = ICOCRFinishErr ? "●" : "";
        }

        /// <summary>
        /// イメージファイル突合せチェック
        /// </summary>
        private void ChkImageFile()
        {
            foreach(ItemManager.ConfirmData Data in _itemMgr._ConfirmData)
            {
                // フォルダ存在チェック
                string FolderPath = Path.Combine(_itemMgr.BankCheckImageRoot(), Path.GetFileNameWithoutExtension(Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME]));
                if (!Directory.Exists(FolderPath))
                {
                    Data.ImgExistChk = false;
                    continue;
                }
                
                // イメージファイル存在チェック
                string FilePath = Path.Combine(FolderPath, Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_NAME]);
                if (!File.Exists(FilePath))
                {
                    Data.ImgExistChk = false;
                    continue;
                }

                // イメージアーカイブ拡張子確認
                if (Path.GetExtension(Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME]) == ".zip")
                {
                    Data.ImgExistChk = false;
                    continue;
                }

                Data.ImgExistChk = true;
            }
        }

        /// <summary>
        /// 画面表示データ整形
        /// </summary>
        private string DispDataFormat(long Data, string Format)
        {
            return Data.ToString(Format);
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

    }
}

