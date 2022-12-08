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

namespace CTROcBrTotalView
{
    /// <summary>
    /// 持出支店別合計票照会画面
    /// </summary>
    public partial class BranchGoukeiImgForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ImageHandler _imgHandler = null;
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
        public BranchGoukeiImgForm()
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
            base.SetDispName2("持出支店別合計票照会");
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
                txtOC_BK_NO.ReadOnly = true;
                txtOC_BK_NO.KeyControl = false;
                txtOC_BK_NO.TabStop = false;
                txtOC_BR_NO.ReadOnly = true;
                txtOC_BR_NO.KeyControl = false;
                txtOC_BR_NO.TabStop = false;
                txtSCAN_BR_NO.ReadOnly = true;
                txtSCAN_BR_NO.KeyControl = false;
                txtSCAN_BR_NO.TabStop = false;
                txtSCAN_DATE.ReadOnly = true;
                txtSCAN_DATE.KeyControl = false;
                txtSCAN_DATE.TabStop = false;
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
                ReadOnlyBackColor(txtOC_BK_NO);
                ReadOnlyBackColor(txtOC_BR_NO);
                ReadOnlyBackColor(txtSCAN_BR_NO);
                ReadOnlyBackColor(txtSCAN_DATE);
                ReadOnlyBackColor(txtTOTAL_COUNT);
                ReadOnlyBackColor(txtTOTAL_AMOUNT);
                ReadOnlyBackColor(txtFix);
            }
            else
            {
                // 訂正
                txtOC_BK_NO.ReadOnly = false;
                txtOC_BK_NO.KeyControl = true;
                txtOC_BK_NO.TabStop = true;
                txtOC_BR_NO.ReadOnly = false;
                txtOC_BR_NO.KeyControl = true;
                txtOC_BR_NO.TabStop = true;
                txtSCAN_BR_NO.ReadOnly = false;
                txtSCAN_BR_NO.KeyControl = true;
                txtSCAN_BR_NO.TabStop = true;
                txtSCAN_DATE.ReadOnly = false;
                txtSCAN_DATE.KeyControl = true;
                txtSCAN_DATE.TabStop = true;
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
                if (txtOC_BK_NO.Focused)
                {
                    SetFocusBackColor(txtOC_BK_NO);
                }
                else
                {
                    RemoveFocusBackColor(txtOC_BK_NO);
                }
                if (txtOC_BR_NO.Focused)
                {
                    SetFocusBackColor(txtOC_BR_NO);
                }
                else
                {
                    RemoveFocusBackColor(txtOC_BR_NO);
                }
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
            TBL_BR_TOTAL brt = _itemMgr.BrTotalInfo.brtotal;

            string sBkCd = brt.m_BK_NO.ToString();
            string sBkName = _ctl.GetBankName(brt.m_BK_NO);
            string sBrCd = brt.m_BR_NO.ToString(Const.BR_NO_LEN_STR);
            string sBrName = _ctl.GetBranchName(brt.m_BR_NO);
            string sScanBrCd = brt.m_SCAN_BR_NO.ToString(Const.BR_NO_LEN_STR);
            string sScanBrName = _ctl.GetScanBranchName(brt.m_SCAN_BR_NO);
            string sScanDate = (brt.m_SCAN_DATE == 0) ? "" : CommonUtil.ConvToDateFormat(brt.m_SCAN_DATE, 3);
            string sBatCount = string.Format("{0:###,##0}", brt.m_TOTAL_COUNT);
            string sBatAmount = string.Format("{0:###,###,###,###,##0}", brt.m_TOTAL_AMOUNT);

            // 画面項目
            txtOC_BK_NO.Text = sBkCd;
            lblOC_BK_NM.Text = sBkName;
            txtOC_BR_NO.Text = sBrCd;
            lblOC_BR_NM.Text = sBrName;
            txtSCAN_BR_NO.Text = sScanBrCd;
            lblSCAN_BR_NM.Text = sScanBrName;
            txtSCAN_DATE.Text = sScanDate;
            txtTOTAL_COUNT.Text = sBatCount;
            txtTOTAL_AMOUNT.Text = sBatAmount;
            txtFix.Text = string.Empty;

            // イメージ描画
            if (isImageRefresh)
            {
                MakeView(brt);
            }
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            _itemMgr.InputBrTotalInfo.Clear();

            // 入力チェック
            if (!CheckInputAll())
            {
                return false;
            }

            _itemMgr.InputBrTotalInfo.OcBankCd = txtOC_BK_NO.getInt();
            _itemMgr.InputBrTotalInfo.OcBrCd = txtOC_BR_NO.getInt();
            _itemMgr.InputBrTotalInfo.ScanBrCd = txtSCAN_BR_NO.getInt();
            _itemMgr.InputBrTotalInfo.ScanDate = txtSCAN_DATE.getInt();
            _itemMgr.InputBrTotalInfo.TotalCount = txtTOTAL_COUNT.getInt();
            _itemMgr.InputBrTotalInfo.TotalAmount = txtTOTAL_AMOUNT.getLong();
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

                    // 現在のフォーカスを外す
                    ActiveControl = null;

                    // 参照モードに変更
                    _dspMode = DispMode.参照;

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
                if (!_imgHandler.HasImage) { return; }

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
                if (!_imgHandler.HasImage) { return; }

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
                if (!_imgHandler.HasImage) { return; }

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
                if (!_imgHandler.HasImage) { return; }

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
                    SetTextFocus(txtOC_BK_NO);

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
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);
                    // 確定処理
                    if (!ExecFix())
                    {
                        return;
                    }

                    // 現在のフォーカスを外す
                    ActiveControl = null;

                    // 参照モードに変更
                    _dspMode = DispMode.参照;

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
        /// 編集モード切替
        /// </summary>
        /// <returns></returns>
        private bool ChangeEditMode()
        {
            int gymid = _itemMgr.BrTotalInfo.GymId;
            int opedate = _itemMgr.BrTotalInfo.OpeDate;
            string imgname = _itemMgr.BrTotalInfo.ImageNmae;

            // バッチ状態更新
            if (!_itemMgr.UpdateTrBatchStatusInput(gymid, opedate, imgname))
            {
                return false;
            }

            // 最新データ再取得
            if (!_itemMgr.GetOcBat(gymid, opedate, imgname, this))
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
            int gymid = _itemMgr.BrTotalInfo.GymId;
            int opedate = _itemMgr.BrTotalInfo.OpeDate;
            string imgname = _itemMgr.BrTotalInfo.ImageNmae;

            // バッチ状態更新
            if (!_itemMgr.UpdateTrBatchStatusComp(_itemMgr.BrTotalInfo.brtotal))
            {
                return false;
            }

            // 最新データ再取得
            if (!_itemMgr.GetOcBat(gymid, opedate, imgname, this))
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
            int gymid = _itemMgr.BrTotalInfo.GymId;
            int opedate = _itemMgr.BrTotalInfo.OpeDate;
            string imgname = _itemMgr.BrTotalInfo.ImageNmae;

            // バッチ状態チェック
            if (!_itemMgr.CanBatchDelete1(gymid, opedate, imgname))
            {
                ComMessageMgr.MessageWarning("対象の合計票は訂正中または削除済のため削除できません。");
                return false;
            }

            // 確認メッセージ
            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "削除しますが、よろしいですか？");
            if (res != DialogResult.OK)
            {
                return false;
            }

            // 合計票情報削除
            if (!_itemMgr.DeleteTrBatch(_itemMgr.BrTotalInfo.brtotal))
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
            int gymid = _itemMgr.BrTotalInfo.GymId;
            int opedate = _itemMgr.BrTotalInfo.OpeDate;
            string imgname = _itemMgr.BrTotalInfo.ImageNmae;

            // 入力チェック
            if (!GetDisplayParams())
            {
                return false;
            }

            // 確認メッセージ
            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "更新しますが、よろしいですか？");
            if (res != DialogResult.OK)
            {
                return false;
            }

            // 登録処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    //支店別合計票の全データロック
                    if (!_itemMgr.GetLockAllBrTotal(out List<TBL_BR_TOTAL> Totals, dbp, Tran))
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran.Trans.Rollback();
                        // エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("他端末で処理中のため確定処理が行えません。しばらくお待ちください");
                        return false;
                    }

                    // 「持出銀行・持出支店・スキャン日」単位での重複チェック(STATUSが完了のみ)
                    IEnumerable<TBL_BR_TOTAL> ie = Totals.Where(x => x.m_STATUS == (int)TBL_BR_TOTAL.enumStatus.Complete &&
                                                                     x.m_BK_NO == _itemMgr.InputBrTotalInfo.OcBankCd &&
                                                                     x.m_BR_NO == _itemMgr.InputBrTotalInfo.OcBrCd &&
                                                                     x.m_SCAN_DATE == _itemMgr.InputBrTotalInfo.ScanDate);
                    if (ie.Count() > 0)
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran.Trans.Rollback();
                        // エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("持出銀行・持出支店・スキャン日が同じ合計票が登録済です。\n登録内容を確認してください");
                        return false;
                    }

                    // バッチ情報更新
                    if (!_itemMgr.UpdateTrBatch(_itemMgr.BrTotalInfo.brtotal, dbp, Tran))
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran.Trans.Rollback();
                        // エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("バッチの更新に失敗しました");
                        return false;
                    }

                    // コミット
                    Tran.Trans.Commit();
                }
                catch (Exception ex)
                {
                    // ロールバック
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    //エラーを表示して画面に戻る。
                    CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                    return false;
                }
            }

            // 最新データ再取得
            if (!_itemMgr.GetOcBat(gymid, opedate, imgname, this))
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
        private void MakeView(TBL_BR_TOTAL brt)
        {
            // コントロール描画中断
            this.SuspendLayout();

            // 最初にコントロールを削除する
            this.RemoveImgControl(_imgHandler);

            // TBL_IMG_PARAM は 1 以上の値を入れておく
            TBL_IMG_PARAM imgparam = new TBL_IMG_PARAM(brt._GYM_ID, 1, AppInfo.Setting.SchemaBankCD);
            imgparam.m_REDUCE_RATE = AppConfig.BatImageReduceRate2;
            imgparam.m_IMG_TOP = 1;
            imgparam.m_IMG_LEFT = 1;
            imgparam.m_IMG_HEIGHT = 1;
            imgparam.m_IMG_WIDTH = 1;
            imgparam.m_XSCROLL_VALUE = 0;

            // イメージコントロール作成
            _imgHandler = new ImageHandler(_ctl);
            _imgHandler.InitializePanelSize(pnlImage.Width, pnlImage.Height);
            _imgHandler.CreateImageControl(brt, imgparam, true);

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

            if (((BaseTextBox)sender).Name == "txtOC_BK_NO")
            {
                if (!CheckOC_BK_NO())
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
            else if (((BaseTextBox)sender).Name == "txtSCAN_BR_NO")
            {
                if (!ChecktSCAN_BR_NO())
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
        /// 持出銀行入力チェック
        /// </summary>
        private bool CheckOC_BK_NO()
        {
            string ChkText = txtOC_BK_NO.ToString();
            string ChkLeftZeroText = txtOC_BK_NO.getInt().ToString("D4");
            int ChkIntText = txtOC_BK_NO.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblOC_BK_NO.Text));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblOC_BK_NO.Text));
                return false;
            }

            //存在チェック
            if (!NCR.Server.HandlingBankCdList.Split(',').Contains(ChkLeftZeroText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02004, lblOC_BK_NO.Text));
                return false;
            }

            //ラベル表示
            DispBank(ChkIntText, lblOC_BK_NM);

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
            if (!DispBranch(txtOC_BK_NO.getInt(), ChkIntText, lblOC_BR_NM, this))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02004, lblOC_BR_NO.Text));
                return false;

            }

            return true;
        }

        /// <summary>
        /// スキャン支店入力チェック
        /// </summary>
        /// <returns></returns>
        private bool ChecktSCAN_BR_NO()
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

            //前営業日取得
            iBicsCalendar cal = new iBicsCalendar();
            int BeforeDate = cal.getBusinessday(AplInfo.OpDate(), -1);

            // 範囲チェック
            if (!(ChkIntText == AplInfo.OpDate() || ChkIntText == BeforeDate))
            {
                this.SetStatusMessage(string.Format("処理日当日又は前営業日のみ有効", lblSCAN_DATE.Text));
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

            // 範囲チェック(ZeroはOK)
            if (ChkIntText < 0)
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

            // 範囲チェック(ZeroはOK)
            if (ChkIntText < 0)
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02006, lblTOTAL_AMOUNT.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 銀行名取得・表示
        /// </summary>
        private bool DispBank(int BankCd, Label DispLabel, FormBase form = null)
        {
            //銀行名設定
            DispLabel.Text = _itemMgr.GetBank(BankCd);

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
            bool rtnValue = _itemMgr.GetBranch(BankCd, Branch, out string BranchName);
            DispLabel.Text = BranchName;

            return rtnValue;
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
