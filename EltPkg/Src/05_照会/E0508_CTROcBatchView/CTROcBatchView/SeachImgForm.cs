using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace CTROcBatchView
{
    /// <summary>
    /// 持出バッチ照会画面
    /// </summary>
    public partial class SeachImgForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ImageHandler _imgHandler = null;
        private ItemManager.ImageInfo _curImage = null;
        private DispMode _dspMode = DispMode.参照;
        private bool _isRefMode { get { return (_dspMode == DispMode.参照); } }

        private const int F1_ = 1;
        private const int F2_ = 2;
        private const int F3_ = 3;
        private const int F4_ = 4;
        private const int F5_ = 5;
        private const int F6_ = 6;
        private const int F7_ = 7;
        private const int F8_ = 8;
        private const int F9_ = 9;
        private const int F10_ = 10;
        private const int F11_ = 11;
        private const int F12_ = 12;

        private const int SF1_ = 1;
        private const int SF2_ = 2;
        private const int SF3_ = 3;
        private const int SF4_ = 4;
        private const int SF5_ = 5;
        private const int SF6_ = 6;
        private const int SF7_ = 7;
        private const int SF8_ = 8;
        private const int SF9_ = 9;
        private const int SF10_ = 10;
        private const int SF11_ = 11;
        private const int SF12_ = 12;

        public enum DispMode
        {
            参照 = 1,
            編集 = 2,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SeachImgForm()
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

            // コントロール初期化
            InitializeControl();

            base.InitializeForm(ctl);
        }


        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

        /// <summary>
        /// 業務名を設定する
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
            base.SetDispName2("持出バッチ照会");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (IsNotPressCSAKey)
            {
                // 通常状態
                if (_isRefMode)
                {
                    // 参照
                    SetFunctionName(F1_, "一覧");
                    SetFunctionName(F2_, string.Empty);
                    SetFunctionName(F3_, string.Empty);
                    SetFunctionName(F4_, string.Empty);
                    SetFunctionName(F5_, "拡大");
                    SetFunctionName(F6_, "縮小");
                    SetFunctionName(F7_, "左表示\n回転", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(F8_, "右表示\n回転", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(F9_, string.Empty);
                    SetFunctionName(F10_, string.Empty);
                    SetFunctionName(F11_, string.Empty);
                    SetFunctionName(F12_, "訂正");
                }
                else
                {
                    // 編集
                    SetFunctionName(F1_, "キャンセル", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(F2_, string.Empty);
                    SetFunctionName(F3_, string.Empty);
                    SetFunctionName(F4_, string.Empty);
                    SetFunctionName(F5_, "拡大");
                    SetFunctionName(F6_, "縮小");
                    SetFunctionName(F7_, "左表示\n回転", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(F8_, "右表示\n回転", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(F9_, string.Empty);
                    SetFunctionName(F10_, string.Empty);
                    SetFunctionName(F11_, string.Empty);
                    SetFunctionName(F12_, "確定");
                }
            }
            else if (IsPressShiftKey)
            {
                // Shiftキー押下
                if (_isRefMode)
                {
                    // 参照
                    SetFunctionName(SF1_, string.Empty);
                    SetFunctionName(SF2_, string.Empty);
                    SetFunctionName(SF3_, string.Empty);
                    SetFunctionName(SF4_, string.Empty);
                    SetFunctionName(SF5_, string.Empty);
                    SetFunctionName(SF6_, string.Empty);
                    SetFunctionName(SF7_, string.Empty);
                    SetFunctionName(SF8_, string.Empty);
                    SetFunctionName(SF9_, "削除");
                    SetFunctionName(SF10_, string.Empty);
                    SetFunctionName(SF11_, string.Empty);
                    SetFunctionName(SF12_, string.Empty);
                }
                else
                {
                    // 編集
                    // 処理なし
                }
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            if (_isRefMode)
            {
                // 参照
                txtSCAN_BR_NO.ReadOnly = true;
                txtSCAN_BR_NO.KeyControl = false;
                txtSCAN_BR_NO.TabStop = false;
                txtSCAN_DATE.ReadOnly = true;
                txtSCAN_DATE.KeyControl = false;
                txtSCAN_DATE.TabStop = false;
                txtSCAN_COUNT.ReadOnly = true;
                txtSCAN_COUNT.KeyControl = false;
                txtSCAN_COUNT.TabStop = false;
                txtOC_BR_NO.ReadOnly = true;
                txtOC_BR_NO.KeyControl = false;
                txtOC_BR_NO.TabStop = false;
                txtCLEARING_DATE.ReadOnly = true;
                txtCLEARING_DATE.KeyControl = false;
                txtCLEARING_DATE.TabStop = false;
                txtTOTAL_COUNT.ReadOnly = true;
                txtTOTAL_COUNT.KeyControl = false;
                txtTOTAL_COUNT.TabStop = false;
                txtTOTAL_AMOUNT.ReadOnly = true;
                txtTOTAL_AMOUNT.KeyControl = false;
                txtTOTAL_AMOUNT.TabStop = false;
                txtFix.ReadOnly = true;
                txtFix.KeyControl = false;
                txtFix.TabStop = false;

                // 背景色
                ReadOnlyBackColor(txtSCAN_BR_NO);
                ReadOnlyBackColor(txtSCAN_DATE);
                ReadOnlyBackColor(txtSCAN_COUNT);
                ReadOnlyBackColor(txtOC_BR_NO);
                ReadOnlyBackColor(txtCLEARING_DATE);
                ReadOnlyBackColor(txtTOTAL_COUNT);
                ReadOnlyBackColor(txtTOTAL_AMOUNT);
                ReadOnlyBackColor(txtFix);
            }
            else
            {
                // 訂正
                txtSCAN_BR_NO.ReadOnly = false;
                txtSCAN_BR_NO.KeyControl = true;
                txtSCAN_BR_NO.TabStop = true;
                txtSCAN_DATE.ReadOnly = false;
                txtSCAN_DATE.KeyControl = true;
                txtSCAN_DATE.TabStop = true;
                txtSCAN_COUNT.ReadOnly = false;
                txtSCAN_COUNT.KeyControl = true;
                txtSCAN_COUNT.TabStop = true;
                txtOC_BR_NO.ReadOnly = false;
                txtOC_BR_NO.KeyControl = true;
                txtOC_BR_NO.TabStop = true;
                txtCLEARING_DATE.ReadOnly = false;
                txtCLEARING_DATE.KeyControl = true;
                txtCLEARING_DATE.TabStop = true;
                txtTOTAL_COUNT.ReadOnly = false;
                txtTOTAL_COUNT.KeyControl = true;
                txtTOTAL_COUNT.TabStop = true;
                txtTOTAL_AMOUNT.ReadOnly = false;
                txtTOTAL_AMOUNT.KeyControl = true;
                txtTOTAL_AMOUNT.TabStop = true;
                txtFix.ReadOnly = false;
                txtFix.KeyControl = false;
                txtFix.TabStop = true;

                // 背景色
                if (txtSCAN_BR_NO.Focused)
                {
                    SetFocusBackColor(txtSCAN_BR_NO);
                }
                else
                {
                    RemoveFocusBackColor(txtSCAN_BR_NO);
                }
                if (txtSCAN_DATE.Focused)
                {
                    SetFocusBackColor(txtSCAN_DATE);
                }
                else
                {
                    RemoveFocusBackColor(txtSCAN_DATE);
                }
                if (txtSCAN_COUNT.Focused)
                {
                    SetFocusBackColor(txtSCAN_COUNT);
                }
                else
                {
                    RemoveFocusBackColor(txtSCAN_COUNT);
                }
                if (txtOC_BR_NO.Focused)
                {
                    SetFocusBackColor(txtOC_BR_NO);
                }
                else
                {
                    RemoveFocusBackColor(txtOC_BR_NO);
                }
                if (txtCLEARING_DATE.Focused)
                {
                    SetFocusBackColor(txtCLEARING_DATE);
                }
                else
                {
                    RemoveFocusBackColor(txtCLEARING_DATE);
                }
                if (txtTOTAL_COUNT.Focused)
                {
                    SetFocusBackColor(txtTOTAL_COUNT);
                }
                else
                {
                    RemoveFocusBackColor(txtTOTAL_COUNT);
                }
                if (txtTOTAL_AMOUNT.Focused)
                {
                    SetFocusBackColor(txtTOTAL_AMOUNT);
                }
                else
                {
                    RemoveFocusBackColor(txtTOTAL_AMOUNT);
                }
                if (txtFix.Focused)
                {
                    SetFocusBackColor(txtFix);
                }
                else
                {
                    RemoveFocusBackColor(txtFix);
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
            _itemMgr.IsBatchUpdate = false;
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

            SetTabControl(lblImg1, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表]);
            SetTabControl(lblImg2, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏]);
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            SetDisplayParams(true);
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected void SetDisplayParams(bool isImageRefresh)
        {
            DataRow row = _itemMgr.BatchInfo.BatRow;
            TBL_TRBATCH bat = _itemMgr.BatchInfo.trbat;

            // バッチイメージ取得
            _itemMgr.Fetch_batimges(bat._GYM_ID, bat._OPERATION_DATE, bat._SCAN_TERM, bat._BAT_ID);

            string sImportDate = (bat._OPERATION_DATE == 0) ? "" : CommonUtil.ConvToDateFormat(bat._OPERATION_DATE, 3);
            string sBatId = bat._BAT_ID.ToString(Const.BAT_ID_LEN_STR);
            string sScanBrCd = bat.m_SCAN_BR_NO.ToString(Const.BR_NO_LEN_STR);
            string sScanBrName = _ctl.GetScanBranchName(bat.m_SCAN_BR_NO);
            string sScanDate = (bat.m_SCAN_DATE == 0) ? "" : CommonUtil.ConvToDateFormat(bat.m_SCAN_DATE, 3);
            string sOcBrCd = bat.m_OC_BR_NO.ToString(Const.BR_NO_LEN_STR);
            string sOcBrName = _ctl.GetBranchName(bat.m_OC_BR_NO);
            string sClearingDate = (bat.m_CLEARING_DATE == 0) ? "" : CommonUtil.ConvToDateFormat(bat.m_CLEARING_DATE, 3);
            string sScanCount = string.Format("{0:###,##0}", bat.m_SCAN_COUNT);
            string sBatCount = string.Format("{0:###,##0}", bat.m_TOTAL_COUNT);
            string sBatAmount = string.Format("{0:###,###,###,###,##0}", bat.m_TOTAL_AMOUNT);
            int nStatus = DBConvert.ToIntNull(row["TOTAL_STATUS"]);
            string sStatus = "";
            switch (nStatus)
            {
                case SQLSearch.BAT_STS_WAIT:
                    sStatus = "入力待ち";
                    break;
                case SQLSearch.BAT_STS_INPUT:
                    sStatus = "入力中";
                    break;
                case SQLSearch.BAT_STS_COMP:
                    sStatus = "入力完了";
                    break;
            }
            int nDiffRes = DBConvert.ToIntNull(row["DIFF_RESULT"]);
            string sDiffRes = "";
            switch (nDiffRes)
            {
                case SQLSearch.DIFF_RESULT_OK:
                    sDiffRes = "OK";
                    break;
                case SQLSearch.DIFF_RESULT_NG:
                    sDiffRes = "NG";
                    break;
            }

            // 画面項目
            lblImportDate.Text = sImportDate;
            lblBatId.Text = sBatId;
            txtSCAN_BR_NO.Text = sScanBrCd;
            lblSCAN_BR_NM.Text = sScanBrName;
            txtSCAN_DATE.Text = sScanDate;
            txtOC_BR_NO.Text = sOcBrCd;
            lblOC_BR_NM.Text = sOcBrName;
            txtCLEARING_DATE.Text = sClearingDate;
            txtSCAN_COUNT.Text = sScanCount;
            txtTOTAL_COUNT.Text = sBatCount;
            txtTOTAL_AMOUNT.Text = sBatAmount;
            lblStatus.Text = sStatus;
            lblResult.Text = sDiffRes;
            txtFix.Text = string.Empty;

            // イメージ描画
            if (isImageRefresh)
            {
                _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表];
                MakeView(_curImage);
            }
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            _itemMgr.InputBatchInfo.Clear();

            // 入力チェック
            if (!CheckInputAll())
            {
                return false;
            }

            _itemMgr.InputBatchInfo.ScanBrCd = txtSCAN_BR_NO.getInt();
            _itemMgr.InputBatchInfo.ScanDate = txtSCAN_DATE.getInt();
            _itemMgr.InputBatchInfo.ScanCnt = txtSCAN_COUNT.getInt();
            _itemMgr.InputBatchInfo.OcBrCd = txtOC_BR_NO.getInt();
            _itemMgr.InputBatchInfo.ClearingDate = txtCLEARING_DATE.getInt();
            _itemMgr.InputBatchInfo.BatCount = txtTOTAL_COUNT.getInt();
            _itemMgr.InputBatchInfo.BatKingaku = txtTOTAL_AMOUNT.getLong();
            _itemMgr.InputBatchInfo.BefClearingDate = _itemMgr.BatchInfo.trbat.m_CLEARING_DATE;
            _itemMgr.InputBatchInfo.BefOcBrCd = _itemMgr.BatchInfo.trbat.m_OC_BR_NO;
            _itemMgr.InputBatchInfo.IsClearingDateUpdate = (_itemMgr.InputBatchInfo.BefClearingDate != _itemMgr.InputBatchInfo.ClearingDate);
            _itemMgr.InputBatchInfo.IsOcBrCdUpdate = (_itemMgr.InputBatchInfo.BefOcBrCd != _itemMgr.InputBatchInfo.OcBrCd);

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
        }

        /// <summary>
        /// [タブ]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab_MouseClick(object sender, MouseEventArgs e)
        {
            if (_ctl.IsIniErr) { return; }

            this.ClearStatusMessage();
            try
            {
                switch (((Control)sender).Name)
                {
                    case "lblImg1":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表];
                        break;
                    case "lblImg2":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏];
                        break;
                }
                if (!_curImage.HasImage)
                {
                    return;
                }

                // 画面コントロール描画
                MakeView(_curImage);

                // 画面表示状態更新
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (_isRefMode) { return; }
                    if (txtFix.Focused)
                    {
                        btnFunc12_Click(sender, e);
                    }
                    break;
                case Keys.Up:
                    if (_isRefMode) { return; }
                    // +({TAB})で[Shift+TAB]の意味になる
                    e.SuppressKeyPress = true;
                    SendKeys.Send("+({TAB})");
                    break;
                case Keys.ShiftKey:
                case Keys.ControlKey:
                    if (ChangeFunction(e)) return;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// [画面項目] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyUp(object sender, KeyEventArgs e)
        {
            if (ChangeFunction(e)) return;
        }

        /// <summary>
        /// フォーカスがあたったら背景を緑色にする
        /// </summary>
        private void txt_Enter(object sender, EventArgs e)
        {
            if (_isRefMode)
            {
                // 参照
                ReadOnlyBackColor((Control)sender);
            }
            else
            {
                // 編集
                SetFocusBackColor((Control)sender);
            }
        }

        /// <summary>
        /// フォーカスが外れたら背景を白色にする
        /// </summary>
        private void txt_Leave(object sender, EventArgs e)
        {
            if (_isRefMode)
            {
                // 参照
                ReadOnlyBackColor((Control)sender);
            }
            else
            {
                // 編集
                RemoveFocusBackColor((Control)sender);
            }
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F01：参照モード　一覧
        /// F01：編集モード　キャンセル
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (_isRefMode)
                {
                    // 一覧
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "一覧", 1);

                    if (_itemMgr.IsBatchUpdate)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
                else
                {
                    // キャンセル
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "キャンセル", 1);

                    // モード切替
                    if (!ChangeReferenceMode())
                    {
                        return;
                    }

                    // 参照モードに変更
                    _dspMode = DispMode.参照;

                    // 現在のフォーカスを外す
                    ActiveControl = null;

                    // 画面表示データ更新
                    SetDisplayParams(false);

                    // ファンクション設定＆状態設定
                    InitializeFunction();
                    RefreshDisplayState();
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F05：拡大
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (_imgHandler.HasImage == false) { return; }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "拡大", 1);

                // 拡大
                _imgHandler.SizeChangeImage(Const.IMAGE_ZOOM_IN, pnlImage.Width, pnlImage.Height);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F06：縮小
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (_imgHandler.HasImage == false) { return; }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "縮小", 1);

                // 縮小
                _imgHandler.SizeChangeImage(Const.IMAGE_ZOOM_OUT, pnlImage.Width, pnlImage.Height);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F07：回転（左回り）
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (_imgHandler.HasImage == false) { return; }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "回転（左回り）", 1);

                // 左回転
                _imgHandler.RotateImage(1);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F08：回転（右回り）
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (_imgHandler.HasImage == false) { return; }

                // 回転（右回り）
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "回転（右回り）", 1);

                // 右回転
                _imgHandler.RotateImage(0);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// SF09：削除
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                if (_isRefMode)
                {
                    // 参照
                    if (IsPressShiftKey)
                    {
                        // ShiftKey押下
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "削除", 1);

                        // 削除処理
                        if (!ExecDelete())
                        {
                            return;
                        }

                        // 画面クローズ
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    // 編集
                    // 処理なし
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F12：参照モード　訂正
        /// F12：編集モード　確定
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                if (_isRefMode)
                {
                    // 訂正
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "訂正", 1);

                    // モード切替
                    if (!ChangeEditMode())
                    {
                        return;
                    }

                    // フォーカス
                    SetTextFocus(txtSCAN_BR_NO);

                    // 編集モードに変更
                    _dspMode = DispMode.編集;

                    // 画面表示データ更新
                    SetDisplayParams(false);

                    // ファンクション設定＆状態設定
                    InitializeFunction();
                    RefreshDisplayState();
                }
                else
                {
                    // 確定処理
                    if (!ExecFix())
                    {
                        return;
                    }
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);
                    // 参照モードに変更
                    _dspMode = DispMode.参照;

                    // 現在のフォーカスを外す
                    ActiveControl = null;

                    // 画面表示データ更新
                    SetDisplayParams(false);

                    // ファンクション設定＆状態設定
                    InitializeFunction();
                    RefreshDisplayState();
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// タブボタンの表示を切り替える
        /// </summary>
        /// <param name="lblTab"></param>
        /// <param name="imgInfo"></param>
        private void SetTabControl(Label lblTab, ItemManager.ImageInfo imgInfo)
        {
            if (imgInfo.HasImage)
            {
                if (_curImage.ImgKbn == imgInfo.ImgKbn)
                {
                    // 選択済み
                    lblTab.BackColor = Color.LightGreen;
                    lblTab.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    // 未選択
                    lblTab.BackColor = Color.Ivory;
                    lblTab.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            else
            {
                // 使用不可
                lblTab.BackColor = SystemColors.ScrollBar;
                lblTab.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        /// <summary>
        /// 編集モード切替
        /// </summary>
        /// <returns></returns>
        private bool ChangeEditMode()
        {
            int gymid = _itemMgr.BatchInfo.GymId;
            int opedate = _itemMgr.BatchInfo.OpeDate;
            string scanterm = _itemMgr.BatchInfo.ScanTerm;
            int batid = _itemMgr.BatchInfo.BatId;

            // アップロード済が存在しても変更可能にする
            //// アップロード状態チェック
            //if (!_itemMgr.CanBatchEdit(gymid, opedate, scanterm, batid))
            //{
            //    ComMessageMgr.MessageWarning("持出アップロード済の証券を持つバッチは訂正できません。");
            //    return false;
            //}

            // バッチ状態更新
            if (!_itemMgr.UpdateTrBatchStatusInput(gymid, opedate, scanterm, batid))
            {
                return false;
            }

            // 最新データ再取得
            if (!_itemMgr.GetOcBat(gymid, opedate, scanterm, batid, this))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 参照モード切替
        /// </summary>
        /// <returns></returns>
        private bool ChangeReferenceMode()
        {
            int gymid = _itemMgr.BatchInfo.GymId;
            int opedate = _itemMgr.BatchInfo.OpeDate;
            string scanterm = _itemMgr.BatchInfo.ScanTerm;
            int batid = _itemMgr.BatchInfo.BatId;

            // バッチ状態更新
            if (!_itemMgr.UpdateTrBatchStatusComp(_itemMgr.BatchInfo.trbat))
            {
                return false;
            }

            // 最新データ再取得
            if (!_itemMgr.GetOcBat(gymid, opedate, scanterm, batid, this))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <returns></returns>
        private bool ExecDelete()
        {
            int gymid = _itemMgr.BatchInfo.GymId;
            int opedate = _itemMgr.BatchInfo.OpeDate;
            string scanterm = _itemMgr.BatchInfo.ScanTerm;
            int batid = _itemMgr.BatchInfo.BatId;

            // バッチ状態チェック
            if (!_itemMgr.CanBatchDelete1(gymid, opedate, scanterm, batid))
            {
                ComMessageMgr.MessageWarning("対象のバッチ票は訂正中または削除済のため削除できません。");
                return false;
            }

            // アップロード状態チェック
            DataTable tbl_trimg = _itemMgr.CanBatchDelete2(gymid, opedate, scanterm, batid);
            if (tbl_trimg == null)
            {
                return false;
            }
            //string buasts = "";
            //buasts += TrMei.Sts.ファイル作成;
            //buasts += ",";
            //buasts += TrMei.Sts.アップロード;
            //DataRow[] rows = tbl_trimg.Select(string.Format("BUA_STS IN ({0})", buasts));
            //if (rows.Length > 0)
            //{
            //    ComMessageMgr.MessageWarning("対象バッチ票配下の証券が持出アップロード中のため削除できません。");
            //    return false;
            //}
            DataRow[] rows = tbl_trimg.Select(string.Format("BUA_STS NOT IN ({0})", TrMei.Sts.未作成));
            if (rows.Length > 0)
            {
                ComMessageMgr.MessageWarning("対象バッチ票配下の証券が未作成の場合のみ削除を行えます。");
                return false;
            }

            // 確認メッセージ
            string msg = "";
            //rows = tbl_trimg.Select(string.Format("BUA_STS={0}", TrMei.Sts.結果正常));
            //if (rows.Length > 0)
            //{
            //    msg += "バッチ票とバッチ票配下の証券すべてを削除します。よろしいですか？\n";
            //    msg += "アップロード済の証券は持出取消画面より取消処理を行ってください。";
            //}
            //else
            //{
            //    msg += "バッチ票とバッチ票配下の証券すべてを削除します。よろしいですか？";
            //}
            msg += "バッチ票とバッチ票配下の証券すべてを削除します。よろしいですか？";
            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, msg);
            if (res != DialogResult.OK)
            {
                return false;
            }

            // バッチ関連情報削除
            if (!_itemMgr.DeleteTrBatch(_itemMgr.BatchInfo.trbat))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <returns></returns>
        private bool ExecFix()
        {
            int gymid = _itemMgr.BatchInfo.GymId;
            int opedate = _itemMgr.BatchInfo.OpeDate;
            string scanterm = _itemMgr.BatchInfo.ScanTerm;
            int batid = _itemMgr.BatchInfo.BatId;

            // 入力チェック
            if (!GetDisplayParams())
            {
                return false;
            }

            // 確認メッセージ
            string msg = "";
            if (_itemMgr.ChkBatchImgUpload(gymid, opedate, scanterm, batid))
            {
                // 持出アップロード済証券あり
                msg += "バッチ配下の証券にアップロード済証券が含まれます。\n";
                if (_itemMgr.InputBatchInfo.IsOcBrCdUpdate)
                {
                    // 持出支店更新
                    msg += "電子交換所システムの持出支店は更新されません。\n";
                }
                if (_itemMgr.InputBatchInfo.IsClearingDateUpdate)
                {
                    // 交換希望日更新
                    msg += "バッチ配下のアップロード済証券の交換希望日は変更されません。\n";
                }
                msg += "更新します。よろしいですか？";
            }
            else
            {
                // 持出アップロード済証券なし
                if (_itemMgr.InputBatchInfo.IsClearingDateUpdate)
                {
                    msg += "バッチ票と同じ交換希望日が設定されている\n";
                    msg += "バッチ配下の証券の交換希望日も同時に更新されます。\n";
                    msg += "更新しますが、よろしいですか？";
                }
                else
                {
                    msg += "更新しますが、よろしいですか？";
                }
            }

            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, msg);
            if (res != DialogResult.OK)
            {
                return false;
            }

            // バッチ情報更新
            if (!_itemMgr.UpdateTrBatch(_itemMgr.BatchInfo.trbat))
            {
                return false;
            }

            // 最新データ再取得
            if (!_itemMgr.GetOcBat(gymid, opedate, scanterm, batid, this))
            {
                return false;
            }
            return true;
        }


        // *******************************************************************
        // 内部メソッド（イメージ関連）
        // *******************************************************************

        /// <summary>
        /// 画面コントロール描画
        /// </summary>
        private void MakeView(ItemManager.ImageInfo imgInfo)
        {
            if (!_itemMgr.batimges.ContainsKey(imgInfo.ImgKbn)) { return; }

            // コントロール描画中断
            this.SuspendLayout();

            // 最初にコントロールを削除する
            this.RemoveImgControl(_imgHandler);

            // TBL_IMG_PARAM は 1 以上の値を入れておく
            TBL_TRBATCH bat = _itemMgr.BatchInfo.trbat;
            TBL_IMG_PARAM imgparam = new TBL_IMG_PARAM(bat._GYM_ID, 1, AppInfo.Setting.SchemaBankCD);
            imgparam.m_REDUCE_RATE = AppConfig.BatImageReduceRate;
            imgparam.m_IMG_TOP = 1;
            imgparam.m_IMG_LEFT = 1;
            imgparam.m_IMG_HEIGHT = 1;
            imgparam.m_IMG_WIDTH = 1;
            imgparam.m_XSCROLL_VALUE = 0;

            // イメージコントロール作成
            TBL_TRBATCHIMG img = _itemMgr.batimges[imgInfo.ImgKbn];
            _imgHandler = new ImageHandler(_ctl);
            _imgHandler.InitializePanelSize(pnlImage.Width, pnlImage.Height);
            _imgHandler.CreateImageControl(bat, img, imgparam, true);

            // コントロール、コントロールのイベント設定
            this.PutImgControl(_imgHandler);

            // コントロール描画再開
            this.ResumeLayout();
        }

        /// <summary>
        /// 画面コントロールに画像イメージを追加する
        /// </summary>
        /// <param name="eiHandler"></param>
        internal void PutImgControl(ImageHandler eiHandler)
        {
            if (eiHandler == null) { return; }
            contentsPanel.Controls.Add(eiHandler.pcPanel);
            eiHandler.SetImagePosition(pnlImage.Top, pnlImage.Left);
            pnlImage.Visible = false;
        }

        /// <summary>
        /// 画面コントロールから画像イメージを削除する
        /// </summary>
        /// <param name="eiHandler"></param>
        internal void RemoveImgControl(ImageHandler eiHandler)
        {
            if (eiHandler == null) { return; }
            eiHandler.ClearImage();
            contentsPanel.Controls.Remove(eiHandler.pcPanel);
            pnlImage.Visible = true;
        }


        // *******************************************************************
        // 内部メソッド（入力チェック）
        // *******************************************************************

        /// <summary>
        /// 各種入力チェック
        /// </summary>
        private void root_I_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isRefMode) { return; }
            this.ClearStatusMessage();

            if (((BaseTextBox)sender).Name == "txtSCAN_BR_NO")
            {
                if (!CheckSCAN_BR_NO())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtSCAN_DATE")
            {
                if (!CheckSCAN_DATE())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtSCAN_COUNT")
            {
                if (!CheckSCAN_COUNT())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtOC_BR_NO")
            {
                if (!CheckOC_BR_NO())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtCLEARING_DATE")
            {
                if (!CheckCLEARING_DATE())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtTOTAL_COUNT")
            {
                if (!CheckTOTAL_COUNT())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtTOTAL_AMOUNT")
            {
                if (!CheckTOTAL_AMOUNT())
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// スキャン支店入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckSCAN_BR_NO()
        {
            string ChkText = txtSCAN_BR_NO.ToString();
            int ChkIntText = txtSCAN_BR_NO.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblSCAN_BR_NO.Text));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblSCAN_BR_NO.Text));
                return false;
            }

            //存在チェック＆ラベル表示
            if (!DispBranch(int.Parse(NCR.Server.ContractBankCd), ChkIntText, lblSCAN_BR_NM, this))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02004, lblSCAN_BR_NO.Text));
                return false;

            }

            return true;
        }

        /// <summary>
        /// スキャン日入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckSCAN_DATE()
        {
            string ChkText = txtSCAN_DATE.ToString();
            int ChkIntText = txtSCAN_DATE.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblSCAN_DATE.Text));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblSCAN_DATE.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblSCAN_DATE.Text));
                return false;
            }

            //営業日チェック
            if (!EntryCommon.Calendar.IsBusinessDate(ChkIntText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02005, lblSCAN_DATE.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// スキャン枚数入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckSCAN_COUNT()
        {
            string ChkText = txtSCAN_COUNT.ToString();
            int ChkIntText = txtSCAN_COUNT.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblSCAN_COUNT.Text));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblSCAN_COUNT.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 持出支店入力チェック
        /// </summary>
        private bool CheckOC_BR_NO()
        {
            string ChkText = txtOC_BR_NO.ToString();
            string ChkLeftZeroText = txtOC_BR_NO.getInt().ToString("D4");
            int ChkIntText = txtOC_BR_NO.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblOC_BR_NO.Text));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblOC_BR_NO.Text));
                return false;
            }

            //存在チェック＆ラベル表示
            if (!DispBranch(AppInfo.Setting.SchemaBankCD, ChkIntText, lblOC_BR_NM, this))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02004, lblOC_BR_NO.Text));
                return false;

            }

            return true;
        }

        /// <summary>
        /// 支店名取得・表示
        /// </summary>
        private bool DispBranch(int BankCd, int Branch, Label DispLabel, FormBase form = null)
        {
            if (BankCd <= 0)
            {
                // 銀行未設定の場合、クリア
                DispLabel.Text = string.Empty;
                return false;
            }

            //支店名設定
            bool rtnValue = _itemMgr.GetBranch(BankCd, Branch, out string BranchName, form);
            DispLabel.Text = BranchName;

            return rtnValue;
        }

        /// <summary>
        /// 交換希望日入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckCLEARING_DATE()
        {
            string ChkText = txtCLEARING_DATE.ToString();
            int ChkIntText = txtCLEARING_DATE.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblCLEARING_DATE.Text));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblCLEARING_DATE.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblCLEARING_DATE.Text));
                return false;
            }

            //営業日チェック
            if (!EntryCommon.Calendar.IsBusinessDate(ChkIntText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02005, lblCLEARING_DATE.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 合計枚数入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckTOTAL_COUNT()
        {
            string ChkText = txtTOTAL_COUNT.ToString();
            int ChkIntText = txtTOTAL_COUNT.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblTOTAL_COUNT.Text));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblTOTAL_COUNT.Text));
                return false;
            }

            // 範囲チェック
            if (ChkIntText < 1)
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02006, lblTOTAL_COUNT.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 合計金額入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckTOTAL_AMOUNT()
        {
            string ChkText = txtTOTAL_AMOUNT.ToString();
            long ChkIntText = txtTOTAL_AMOUNT.getLong();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblTOTAL_AMOUNT.Text));
                return false;
            }

            //数値チェック
            if (!long.TryParse(ChkText, out long i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblTOTAL_AMOUNT.Text));
                return false;
            }

            // 範囲チェック
            if (ChkIntText < 1)
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02006, lblTOTAL_AMOUNT.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 全入力項目チェック
        /// </summary>
        private bool CheckInputAll()
        {
            foreach (Control con in AllSubControls(this).OrderBy(c => c.TabIndex))
            {
                if (con is BaseTextBox)
                {
                    // BaseTextBoxを継承している場合は、チェックイベント強制発生
                    if (((BaseTextBox)con).RaiseI_Validating())
                    {
                        // 項目遷移時、遷移元のValidatingは発生させない
                        this.AutoValidate = AutoValidate.Disable;
                        ((BaseTextBox)con).Select();
                        this.AutoValidate = AutoValidate.EnablePreventFocusChange;

                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 全コントロール取得
        /// </summary>
        private static List<Control> AllSubControls(Control control)
        {
            var list = control.Controls.OfType<Control>().ToList();
            var deep = list.Where(o => o.Controls.Count > 0).SelectMany(AllSubControls).ToList();
            list.AddRange(deep);
            return list;
        }        
    }
}
