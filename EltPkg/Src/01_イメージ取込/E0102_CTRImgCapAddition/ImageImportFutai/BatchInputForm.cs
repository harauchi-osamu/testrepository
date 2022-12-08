using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using System.Text.RegularExpressions;
using System.Linq;
using ImageController;

namespace ImageImportFutai
{
    /// <summary>
    /// ＸＸＸＸＸ画面
    /// </summary>
    public partial class BatchInputForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private ImageEditor _editor = null;
        private ImageCanvas _canvas = null;

        public enum ImageType
        {
            None = 0,
            Front = 1,
            Back = 2,
        }

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

        private const int CF1_ = 1;
        private const int CF2_ = 2;
        private const int CF3_ = 3;
        private const int CF4_ = 4;
        private const int CF5_ = 5;
        private const int CF6_ = 6;
        private const int CF7_ = 7;
        private const int CF8_ = 8;
        private const int CF9_ = 9;
        private const int CF10_ = 10;
        private const int CF11_ = 11;
        private const int CF12_ = 12;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BatchInputForm()
        {
            InitializeComponent();

            _editor = new ImageEditor();
            _canvas = new ImageCanvas(_editor);
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

            // ここで処理対象データの取得
            GetBatchData();

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
            base.SetDispName2("イメージ取込　バッチ票入力");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (IsNotPressCSAKey)
            {
                // 通常状態
                SetFunctionName(F1_, "終了");
                SetFunctionName(F2_, string.Empty);
                SetFunctionName(F3_, string.Empty);
                if (_itemMgr.InputBatchData.m_STATUS == (int)TBL_SCAN_BATCH_CTL.enumStatus.Delete)
                {
                    SetFunctionName(F4_, "削除取消", true, Const.FONT_SIZE_FUNC_LOW);
                }
                else
                {
                    SetFunctionName(F4_, "削除", true);
                }
                SetFunctionName(F5_, string.Empty);
                SetFunctionName(F6_, string.Empty);
                SetFunctionName(F7_, string.Empty);
                SetFunctionName(F8_, "保留");
                SetFunctionName(F9_, string.Empty);
                SetFunctionName(F10_, string.Empty);
                SetFunctionName(F11_, string.Empty);
                SetFunctionName(F12_, "確定");
            }
            else if (IsPressShiftKey)
            {
                // Shiftキー押下
                SetFunctionName(SF1_, string.Empty);
                SetFunctionName(SF2_, string.Empty);
                SetFunctionName(SF3_, string.Empty);
                SetFunctionName(SF4_, string.Empty);
                SetFunctionName(SF5_, string.Empty);
                SetFunctionName(SF6_, string.Empty);
                SetFunctionName(SF7_, string.Empty);
                SetFunctionName(SF8_, string.Empty);
                SetFunctionName(SF9_, string.Empty);
                SetFunctionName(SF10_, string.Empty);
                SetFunctionName(SF11_, string.Empty);
                SetFunctionName(SF12_, string.Empty);
            }
            else if (IsPressCtrlKey)
            {
                // Ctrlキー押下
                SetFunctionName(CF1_, string.Empty);
                SetFunctionName(CF2_, string.Empty);
                SetFunctionName(CF3_, string.Empty);
                SetFunctionName(CF4_, string.Empty);
                SetFunctionName(CF5_, string.Empty);
                SetFunctionName(CF6_, string.Empty);
                SetFunctionName(CF7_, string.Empty);
                SetFunctionName(CF8_, string.Empty);
                SetFunctionName(CF9_, string.Empty);
                SetFunctionName(CF10_, string.Empty);
                SetFunctionName(CF11_, string.Empty);
                SetFunctionName(CF12_, string.Empty);
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            if (IsNotPressCSAKey)
            {
                // F8ファンクション
                if (_itemMgr.InputBatchData.m_STATUS == (int)TBL_SCAN_BATCH_CTL.enumStatus.Delete)
                {
                    SetFunctionState(F8_, false);
                }
                else
                {
                    SetFunctionState(F8_, true);
                }

                // F12ファンクション
                if (_itemMgr.InputBatchData.m_STATUS == (int)TBL_SCAN_BATCH_CTL.enumStatus.Delete ||
                    !string.IsNullOrEmpty(GetDispModeErrMsg()))
                {
                    // 削除　または　初期表示時エラー
                    SetFunctionState(F12_, false);
                }
                else
                {
                    SetFunctionState(F12_, true);
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
            // 活性・非活性処理

            if (_itemMgr.InputBatchData.m_STATUS == (int)TBL_SCAN_BATCH_CTL.enumStatus.Delete ||
                !string.IsNullOrEmpty(GetDispModeErrMsg()))
            {
                // 削除　または　初期表示時エラー
                // 画面の入力項目を使用不可

                txtOC_BK_NO.Enabled = false;
                txtOC_BR_NO.Enabled = false;
                txtSCAN_BR_NO.Enabled = false;
                txtSCAN_DATE.Enabled = false;
                txtCLEARING_DATE.Enabled = false;
                txtSCAN_COUNT.Enabled = false;
                txtTOTAL_COUNT.Enabled = false;
                txtTOTAL_AMOUNT.Enabled = false;
            }
            else
            {
                txtOC_BK_NO.Enabled = true;
                txtOC_BR_NO.Enabled = true;
                txtSCAN_BR_NO.Enabled = true;
                txtSCAN_DATE.Enabled = true;
                txtCLEARING_DATE.Enabled = true;
                txtSCAN_COUNT.Enabled = true;
                txtTOTAL_COUNT.Enabled = true;
                txtTOTAL_AMOUNT.Enabled = true;
            }

            // フォーカス初期位置設定
            SetFocusValidation(txtOC_BK_NO);

            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            // 画面項目を設定する処理はまとめてここに実装してこのメソッドを呼ぶ

            // 一旦、最終フィールドに遷移
            SetFocusValidation(txtCheck);

            // 入力データ表示
            txtOC_BK_NO.Text = DispDataFormatZero(_itemMgr.InputBatchData, _itemMgr.InputBatchData.m_OC_BK_NO, "");
            txtOC_BR_NO.Text = DispDataFormatZero(_itemMgr.InputBatchData, _itemMgr.InputBatchData.m_OC_BR_NO, "");
            txtSCAN_BR_NO.Text = DispDataFormatZero(_itemMgr.InputBatchData, _itemMgr.InputBatchData.m_SCAN_BR_NO, "");
            txtSCAN_DATE.Text = DispDataFormatZero(_itemMgr.InputBatchData, _itemMgr.InputBatchData.m_SCAN_DATE, "D8");
            txtCLEARING_DATE.Text = DispDataFormatZero(_itemMgr.InputBatchData, _itemMgr.InputBatchData.m_CLEARING_DATE, "D8");
            txtSCAN_COUNT.Text = DispDataFormatZero(_itemMgr.InputBatchData, _itemMgr.InputBatchData.m_SCAN_COUNT, "0");
            txtTOTAL_COUNT.Text = DispDataFormatZero(_itemMgr.InputBatchData, _itemMgr.InputBatchData.m_TOTAL_COUNT, "0");
            txtTOTAL_AMOUNT.Text = DispDataFormatZero(_itemMgr.InputBatchData, _itemMgr.InputBatchData.m_TOTAL_AMOUNT, "0");
            lblOC_BK_NM.Text = string.Empty;
            lblOC_BR_NM.Text = string.Empty;
            lblSCAN_BR_NM.Text = string.Empty;
            txtCheck.Text = string.Empty;

            //初期値定義
            //持出銀行
            if (string.IsNullOrEmpty(txtOC_BK_NO.ToString()))
            {
                //DBデータがない場合
                //OCR値を取得
                string BK_NO = _itemMgr.GetBatchOCRData(_ctl.SettingData.OCRSettingData.OC_BK_NO, this);
                if (string.IsNullOrEmpty(BK_NO))
                {
                    //OCR値もない場合、受託元銀行番号
                    BK_NO = NCR.Server.ContractBankCd;
                }

                txtOC_BK_NO.Text = BK_NO;
            }

            //持出支店
            if (string.IsNullOrEmpty(txtOC_BR_NO.ToString()))
            {
                //DBデータがない場合
                //OCR値を取得
                string BR_NO = _itemMgr.GetBatchOCRData(_ctl.SettingData.OCRSettingData.OC_BR_NO, this);

                txtOC_BR_NO.Text = BR_NO;
            }

            //スキャン支店
            if (string.IsNullOrEmpty(txtSCAN_BR_NO.ToString()))
            {
                //DBデータがない場合
                //OCR値を取得
                string SCAN_BR_NO = _itemMgr.GetBatchOCRData(_ctl.SettingData.OCRSettingData.SCAN_BR_NO, this);

                txtSCAN_BR_NO.Text = SCAN_BR_NO;
            }

            //スキャン日
            if (string.IsNullOrEmpty(txtSCAN_DATE.ToString()))
            {
                //DBデータがない場合
                //OCR値を取得
                string SCAN_DATE = _itemMgr.GetBatchOCRData(_ctl.SettingData.OCRSettingData.SCAN_DATE, this);
                if (string.IsNullOrEmpty(SCAN_DATE))
                {
                    //OCR値もない場合、システム日付
                    SCAN_DATE = AplInfo.OpDate().ToString();
                }

                txtSCAN_DATE.Text = SCAN_DATE;
            }

            //交換希望日
            if (string.IsNullOrEmpty(txtCLEARING_DATE.ToString()))
            {
                //DBデータがない場合
                //OCR値を取得
                string CLEARING_DATE = _itemMgr.GetBatchOCRData(_ctl.SettingData.OCRSettingData.CLEARING_DATE, this);

                txtCLEARING_DATE.Text = CLEARING_DATE;
            }

            //スキャン枚数
            if (string.IsNullOrEmpty(txtSCAN_COUNT.ToString()))
            {
                //DBデータがない場合
                //OCR値を取得
                string SCAN_COUNT = _itemMgr.GetBatchOCRData(_ctl.SettingData.OCRSettingData.SCAN_COUNT, this);

                txtSCAN_COUNT.Text = SCAN_COUNT;
            }

            //合計枚数
            if (string.IsNullOrEmpty(txtTOTAL_COUNT.ToString()))
            {
                //DBデータがない場合
                //OCR値を取得
                string TOTAL_COUNT = _itemMgr.GetBatchOCRData(_ctl.SettingData.OCRSettingData.TOTAL_COUNT, this);

                txtTOTAL_COUNT.Text = TOTAL_COUNT;
            }

            //合計金額
            if (string.IsNullOrEmpty(txtTOTAL_AMOUNT.ToString()))
            {
                //DBデータがない場合
                //OCR値を取得
                string TOTAL_AMOUNT = _itemMgr.GetBatchOCRData(_ctl.SettingData.OCRSettingData.TOTAL_AMOUNT, this);

                txtTOTAL_AMOUNT.Text = TOTAL_AMOUNT;
            }

            //ラベル表示
            if (!string.IsNullOrEmpty(txtOC_BK_NO.ToString()))
            {
                DispBank(txtOC_BK_NO.getInt(), lblOC_BK_NM);
            }
            if (!string.IsNullOrEmpty(txtOC_BR_NO.ToString()))
            {
                DispBranch(txtOC_BK_NO.getInt(), txtOC_BR_NO.getInt(), lblOC_BR_NM);
            }
            if (!string.IsNullOrEmpty(txtSCAN_BR_NO.ToString()))
            {
                DispBranch(int.Parse(NCR.Server.ContractBankCd), txtSCAN_BR_NO.getInt(), lblSCAN_BR_NM);
            }

            // バッチ票イメージ表示
            if (!DispBatchImage(pbScanImage, this))
            {
                // イメージ表示でエラーの場合、イメージNoDataと同じ扱い
                _itemMgr.InputParams.DispMode = ItemManager.BatchInputParams.DispErrMode.BatchImageNoData;
            }

        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 入力データの取得
            GetInputData();

            return true;
        }

        // <summary>
        /// 入力値の取得
        /// </summary>
        private bool GetInputData()
        {
            // 持出銀行
            if (!string.IsNullOrEmpty(txtOC_BK_NO.ToString()))
            {
                _itemMgr.InputBatchData.m_OC_BK_NO = txtOC_BK_NO.getInt();
            }
            // 持出支店
            if (!string.IsNullOrEmpty(txtOC_BR_NO.ToString()))
            {
                _itemMgr.InputBatchData.m_OC_BR_NO = txtOC_BR_NO.getInt();
            }
            // スキャン支店
            if (!string.IsNullOrEmpty(txtSCAN_BR_NO.ToString()))
            {
                _itemMgr.InputBatchData.m_SCAN_BR_NO = txtSCAN_BR_NO.getInt();
            }
            // スキャン日
            if (!string.IsNullOrEmpty(txtSCAN_DATE.ToString()))
            {
                _itemMgr.InputBatchData.m_SCAN_DATE = txtSCAN_DATE.getInt();
            }
            // 交換希望日
            if (!string.IsNullOrEmpty(txtCLEARING_DATE.ToString()))
            {
                _itemMgr.InputBatchData.m_CLEARING_DATE = txtCLEARING_DATE.getInt();
            }
            // スキャン枚数
            if (!string.IsNullOrEmpty(txtSCAN_COUNT.ToString()))
            {
                _itemMgr.InputBatchData.m_SCAN_COUNT = txtSCAN_COUNT.getInt();
            }
            // 合計枚数
            if (!string.IsNullOrEmpty(txtTOTAL_COUNT.ToString()))
            {
                _itemMgr.InputBatchData.m_TOTAL_COUNT = txtTOTAL_COUNT.getInt();
            }
            // 合計金額
            if (!string.IsNullOrEmpty(txtTOTAL_AMOUNT.ToString()))
            {
                _itemMgr.InputBatchData.m_TOTAL_AMOUNT = txtTOTAL_AMOUNT.getLong();
            }

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
            //初期表示時のエラーメッセージ表示
            string ErrMsg = GetDispModeErrMsg();
            if (!string.IsNullOrEmpty(ErrMsg))
            {
                this.SetStatusMessage(ErrMsg);
            }
        }

        /// <summary>
        /// 各種入力チェック
        /// </summary>
        private void txtBox_IValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
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
            else if (((BaseTextBox)sender).Name == "txtCLEARING_DATE")
            {
                if (!CheckCLEARING_DATE())
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
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            //KeyDownClearStatusMessage(e);
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

        /// <summary>
        /// フォーカスがあたったら背景を緑色にする
        /// </summary>
        private void txt_Enter(object sender, EventArgs e)
        {
            SetFocusBackColor((Control)sender);
        }

        /// <summary>
        /// フォーカスが外れたら背景を白色にする
        /// </summary>
        private void txt_Leave(object sender, EventArgs e)
        {
            RemoveFocusBackColor((Control)sender);
        }

        /// <summary>
        /// 最終項目 KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Last_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Down:
                    if (base.btnFunc[F12_].Enabled) btnFunc12_Click(null, null);
                    break;
                case Keys.Up:
                    // 前項目へ移動
                    this.SelectNextControl((Control)sender, false, true, true, false);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Tab:
                    bool forward = true;
                    if (e.Shift) forward = false;
                    this.SelectNextControl((Control)sender, forward, true, true, false);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                default:
                    break;
            }
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        #region ファンクション

        /// <summary>
        /// F1：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                //確認メッセージ表示
                if (ComMessageMgr.MessageQuestion( MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "バッチ一覧に戻りますが、よろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);

                // 更新処理実行
                _itemMgr.UpdateStatusWaitInput(this);

                //画面表示終了
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F4：削除/削除取消
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                if (_itemMgr.InputBatchData.m_STATUS == (int)TBL_SCAN_BATCH_CTL.enumStatus.Delete)
                {
                    // 削除取消処理

                    //確認メッセージ表示
                    if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "バッチを削除解除してもよろしいですか？") == DialogResult.No)
                    {
                        return;
                    }

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "削除取消", 1);

                    // 更新処理実行
                    if (_itemMgr.UpdateStatusDelete(1, NCR.Terminal.Number, this) == 0)
                    {
                        // 更新データがない場合

                        //エラーを表示して一覧画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("他オペレータにより状態が変更された可能性があります。最新状態を確認してください。");
                        this.DialogResult = DialogResult.Abort;
                        this.Close();
                        return;
                    }

                    //最新データを取得して再表示
                    if (_itemMgr.GetTargetBatchData(this))
                    {
                        // 画面再描画
                        this.InitializeFunction();
                        this.ResetForm();
                    }
                }
                else
                {
                    // 削除処理

                    //確認メッセージ表示
                    if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "バッチを削除してもよろしいですか？") == DialogResult.No)
                    {
                        return;
                    }

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "削除", 1);

                    // 更新処理実行
                    if (_itemMgr.UpdateStatusDelete(0, NCR.Terminal.Number, this) == 0)
                    {
                        // 更新データがない場合

                        //エラーを表示して一覧画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("他オペレータにより状態が変更された可能性があります。最新状態を確認してください。");
                        this.DialogResult = DialogResult.Abort;
                        this.Close();
                        return;
                    }

                    //処理成功
                    NextInput(TBL_SCAN_BATCH_CTL.enumStatus.Delete);
                }
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F8：保留
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                //確認メッセージ表示
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "バッチを保留してもよろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "保留", 1);
               

                //ステータス等の変更
                _itemMgr.InputBatchData.m_STATUS = (int)TBL_SCAN_BATCH_CTL.enumStatus.Hold;
                _itemMgr.InputBatchData.m_LOCK_TERM = string.Empty;

                // 更新処理実行
                if (_itemMgr.UpdateStatusHold(this) == 0)
                {
                    // 更新データがない場合

                    //エラーを表示して一覧画面に戻る。
                    CommonClass.ComMessageMgr.MessageWarning("他オペレータにより状態が変更された可能性があります。最新状態を確認してください。");
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                    return;
                }

                //処理成功
                NextInput(TBL_SCAN_BATCH_CTL.enumStatus.Hold);
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
                //全入力項目チェック
                if (!CheckInputAll())
                {
                    return;
                }

                // スキャン枚数チェック
                int FileCount = Directory.EnumerateFiles(_itemMgr.TargetFolderPath(), "*.jpg").Count();
                if ((txtSCAN_COUNT.getInt() * 2) != FileCount)
                {
                    ComMessageMgr.MessageWarning("スキャン枚数入力とイメージ数が一致しません");
                    return;
                }

                //確認メッセージ表示
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "バッチ票入力を確定してもよろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);

                //入力データの取得
                GetDisplayParams();

                //確定処理
                if (!BatchInput())
                {
                    return;
                }

                // 最終項目のクリア
                txtCheck.Text = string.Empty;

                // 業務日付を設定
                _itemMgr.AssignParams.GymDate = AplInfo.OpDate();

                //　明細紐づけ画面表示
                ImageAssignForm form = new ImageAssignForm();
                form.InitializeForm(_ctl);
                form.ResetForm();
                DialogResult result = form.ShowDialog();
                if(result != DialogResult.OK)
                {
                    return;
                }

                //処理成功
                NextInput(TBL_SCAN_BATCH_CTL.enumStatus.Complete);
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        #endregion

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 確定処理
        /// </summary>
        private bool BatchInput()
        {
            // 対象データのステータス確認
            if (!_itemMgr.ChkBatchDataStatus(TBL_SCAN_BATCH_CTL.enumStatus.Processing, this))
            {
                //エラーを表示して画面に戻る。
                CommonClass.ComMessageMgr.MessageWarning("他オペレータにより状態が変更された可能性があります。最新状態を確認してください。");
                return false;
            }

            // 対象バッチフォルダの存在チェック
            string ImgFolderPath = _itemMgr.TargetFolderPath();
            if (!Directory.Exists(ImgFolderPath))
            {
                //エラーを表示して画面に戻る。
                CommonClass.ComMessageMgr.MessageWarning("イメージファイルの格納フォルダが確認できません");
                return false;
            }

            // スキャン枚数チェック
            int FolderFileCount = Directory.EnumerateFiles(ImgFolderPath, "*.jpg").Count();
            if ((txtSCAN_COUNT.getInt() * 2) != FolderFileCount)
            {
                //エラーを表示して画面に戻る。
                CommonClass.ComMessageMgr.MessageWarning("スキャン枚数入力とイメージ数が一致しません");
                return false;
            }

            //ステータス等の変更
            _itemMgr.InputBatchData.m_IMAGE_COUNT = FolderFileCount;
            //更新処理実行
            if (_itemMgr.UpdateInputComplete() == 0)
            {
                //エラーを表示して画面に戻る。
                CommonClass.ComMessageMgr.MessageWarning("ステータス更新に失敗しました");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 次の処理
        /// </summary>
        private void NextInput(TBL_SCAN_BATCH_CTL.enumStatus status)
        {
            if (_itemMgr.InputParams.Mode == ItemManager.BatchInputParams.InputMode.Select)
            {
                // 選択取得

                //一覧画面に戻る。
                switch (status)
                {
                    case TBL_SCAN_BATCH_CTL.enumStatus.Complete:
                        // 取込成功時
                        this.DialogResult = DialogResult.OK;
                        break;
                    default:
                        this.DialogResult = DialogResult.Cancel;
                        break;
                }
                this.Close();
                return;
            }
            else
            {
                // 自動取得

                // 初期処理から別のバッチに対する処理を実行
                GetBatchData();

                // 取得結果表示
                if (_itemMgr.InputParams.DispMode == ItemManager.BatchInputParams.DispErrMode.BatchDataGetErr)
                {
                    //エラーを表示して一覧画面に戻る。
                    CommonClass.ComMessageMgr.MessageWarning("データの取得に失敗しました");
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                    return;
                }
                else if (_itemMgr.InputParams.DispMode == ItemManager.BatchInputParams.DispErrMode.BatchDataNoData)
                {
                    // 取得バッチデータがない場合一覧画面に戻る。
                    switch (status)
                    {
                        case TBL_SCAN_BATCH_CTL.enumStatus.Complete:
                            // 取込成功時
                            this.DialogResult = DialogResult.OK;
                            break;
                        default:
                            this.DialogResult = DialogResult.Cancel;
                            break;
                    }
                    this.Close();
                    return;
                }

                switch (status)
                {
                    case TBL_SCAN_BATCH_CTL.enumStatus.Complete:
                        // 取込成功時
                        // 完了メッセージ表示
                        this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.I00001, "データ取込"), Color.Transparent);
                        break;
                    default:
                        break;
                }

                // 画面再描画
                this.InitializeFunction();
                this.ResetForm();

                //初期表示時のエラーメッセージ表示
                string ErrMsg = GetDispModeErrMsg();
                if (!string.IsNullOrEmpty(ErrMsg))
                {
                    this.SetStatusMessage(ErrMsg);
                }
            }
        }

        /// <summary>
        /// 表示データの取得
        /// </summary>
        private void GetBatchData()
        {
            // 対象データの取得
            if (!_itemMgr.GetTargetBatchControl(NCR.Terminal.Number)) return;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("処理バッチ情報取得 InputRoute:{0} BatchFolderName:{1}", _itemMgr.InputParams.TargetRoute.ToString("D"), _itemMgr.InputParams.TargetBatchFolderName), 1);

            // 対象バッチフォルダの存在チェック
            string FolderPath = _itemMgr.TargetFolderPath();
            if (!Directory.Exists(FolderPath))
            {
                _itemMgr.InputParams.DispMode = ItemManager.BatchInputParams.DispErrMode.FolderNotExists;
                return;
            }

            // イメージ一覧を作成
            IEnumerable<string> FileList = Directory.EnumerateFiles(FolderPath, "*").Select(name => Path.GetFileName(name)).OrderBy(name => name);
            Dictionary<string, ItemManager.BatchInputParams.ImageData> ImageList;
            // ファイル名順で取得
            ImageList = MakeImageListRenban(FileList);

            // Batch票イメージ設定
            if (ImageList.Count > 0)
            {
                _itemMgr.InputParams.BatchImage = ImageList.First().Value;
            }
            if (!_itemMgr.InputParams.BatchImage.ChkFrontInput())
            {
                _itemMgr.InputParams.DispMode = ItemManager.BatchInputParams.DispErrMode.BatchImageNoData;
                return;
            }

            _itemMgr.InputParams.DispMode = ItemManager.BatchInputParams.DispErrMode.None;
            return;
        }

        /// <summary>
        /// 表裏の一覧作成(連番)
        /// </summary>
        private Dictionary<string, ItemManager.BatchInputParams.ImageData> MakeImageListRenban(IEnumerable<string> FileList)
        {
            Dictionary<string, ItemManager.BatchInputParams.ImageData> ImageList = new Dictionary<string, ItemManager.BatchInputParams.ImageData>();


            ItemManager.BatchInputParams.ImageData ImgData = new ItemManager.BatchInputParams.ImageData();
            foreach (string name in FileList)
            {
                if (!name.EndsWith(".jpg"))
                {
                    continue;
                }

                if (!ImgData.ChkFrontInput())
                {
                    //表箇所が空の場合設定
                    ImgData.Front = name;
                }
                else if (!ImgData.ChkBackInput())
                {
                    //裏箇所が空の場合設定
                    ImgData.Back = name;

                    // 一覧に設定
                    ImageList.Add((ImageList.Count + 1).ToString(), ImgData);
                    ImgData = new ItemManager.BatchInputParams.ImageData();
                }
            }

            return ImageList;
        }

        /// <summary>
        /// 表示時のエラーメッセージ取得
        /// </summary>
        private string GetDispModeErrMsg()
        {
            string ErrMsg;
            switch (_itemMgr.InputParams.DispMode) {
            case ItemManager.BatchInputParams.DispErrMode.FolderNotExists:
                // バッチフォルダが存在しない場合
                ErrMsg = "イメージファイルの格納フォルダが確認できません";
                break;
            case ItemManager.BatchInputParams.DispErrMode.BatchImageNoData:
                // バッチイメージが取得できない場合
                ErrMsg = "バッチ票イメージが取得できませんでした";
                break;
            default:
                ErrMsg = string.Empty;
                break;
            }

            return ErrMsg;
        }

        /// <summary>
        /// 画面表示データ整形
        /// </summary>
        private string DispDataFormat(long Data, string Format)
        {
            // 0以下は空
            if (Data <= 0) return "";

            return Data.ToString(Format);
        }

        /// <summary>
        /// 画面表示データ整形
        /// Zero表記あり
        /// </summary>
        private string DispDataFormatZero(TBL_SCAN_BATCH_CTL batctl, long Data, string Format)
        {
            if (batctl.m_SCAN_DATE > 0 && Data == 0)
            {
                //スキャン日が入力されているケースではZeroで表示
                return Data.ToString(Format);
            }

            return DispDataFormat(Data, Format);
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
        /// バッチ票イメージ表示
        /// </summary>
        private bool DispBatchImage(PictureBox DispPicture, FormBase form = null)
        {
            //バッチイメージ表示
            if (!_itemMgr.InputParams.BatchImage.ChkFrontInput())
            {
                DispPicture.Image = null;
                return true;
            }
            try
            {
                // 画像読込
                _canvas.InitializeCanvas(Path.Combine(_itemMgr.TargetFolderPath(), _itemMgr.InputParams.BatchImage.Front));
                _canvas.SetDefaultReSize(DispPicture.Parent.Width, DispPicture.Parent.Height);
                // 縮小率の小さい方にフィットさせる
                float hReduce = (float)DispPicture.Parent.Width / (float)_canvas.ResizeCanvas.Width;
                float vReduce = (float)DispPicture.Parent.Height / (float)_canvas.ResizeCanvas.Height;
                ImageCanvas.DirectionType defaulttype = hReduce > vReduce ? ImageCanvas.DirectionType.Vertical : ImageCanvas.DirectionType.Horizontal;
                // 全体表示
                _canvas.ToFitCanvas(defaulttype, DispPicture.Parent.Width, DispPicture.Parent.Height);
                DispPicture.Width = _canvas.ResizeInfo.PicBoxWidth;
                DispPicture.Height = _canvas.ResizeInfo.PicBoxHeight;
                DispPicture.Image = _canvas.ResizeCanvas;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
            return true;
        }

        #region 入力チェック

        /// <summary>
        /// KeyDown時のクリアメッセージ
        /// </summary>
        private void KeyDownClearStatusMessage(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                    break;
                default:
                    this.ClearStatusMessage();
                    break;
            }
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

            return true;
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

            // 範囲チェック
            if (ChkIntText < AplInfo.OpDate())
            {
                this.SetStatusMessage(string.Format("交換希望日は処理日以降を設定する必要があります", lblCLEARING_DATE.Text));
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

            // ファイル数取得
            float FileCount = Directory.EnumerateFiles(_itemMgr.TargetFolderPath(), "*.jpg").Count();

            // 奇数チェック
            if (FileCount % 2 == 1)
            {
                this.SetStatusMessage(string.Format("イメージファイルが奇数です。イメージ枚数：{0}枚", FileCount));
                return false;
            }

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

            // 範囲チェック
            if (ChkIntText < 1)
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02006, lblSCAN_COUNT.Text));
                return false;
            }

            // 整合性チェック
            // スキャン枚数チェック
            if (ChkIntText != (FileCount / 2))
            {
                this.SetStatusMessage(string.Format("実際のスキャン枚数と相違があります。実際のスキャン枚数：{0}枚", (FileCount / 2)));
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


        #endregion
    }
}
