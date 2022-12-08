using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace CTRIcRequestMk
{
    /// <summary>
    /// 持帰要求テキスト作成画面
    /// </summary>
    public partial class FileCreateForm : EntryCommonFormBase
	{
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private List<ItemSet> _billList = null;
        private bool _isComplete = false;

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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileCreateForm()
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
            InitializeControl();
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
            base.SetDispName2("持帰要求テキスト作成");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
		{
            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, string.Empty);
            SetFunctionName(F3_, string.Empty);
            SetFunctionName(F4_, string.Empty);
            SetFunctionName(F5_, string.Empty);
            SetFunctionName(F6_, string.Empty);
            SetFunctionName(F7_, string.Empty);
            SetFunctionName(F8_, string.Empty);
            SetFunctionName(F9_, string.Empty);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "実行");
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
		{
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            SetFunctionState(F12_, !_isComplete);

            // 設定ファイル読み込みでエラー発生時はF1以外Disable
            if (this._ctl.SettingData.ChkServerIni == false || !string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                DisableAllFunctionState(true);
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
            SetBillList();
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
            if (_ctl.IsIniErr) { return; }

            _itemMgr.DispParams.Clear();

            iBicsCalendar cal = new iBicsCalendar();
            int prevBizDate = 0;
            int bizNextDate = 0;

            switch (_ctl.ProcType)
            {
                case Controller.ClearingType.翌日交換:
                    // 翌日取得
                    int nextDate = CommonUtil.AddDate(AplInfo.OpDate(), 1);
                    txtClearingDateFrom.Text = CommonUtil.ConvToDateFormat(nextDate, 3);

                    // 翌営業日取得
                    int nextBizDate = cal.getBusinessday(AplInfo.OpDate(), 1);
                    txtClearingDateTo.Text = CommonUtil.ConvToDateFormat(nextBizDate, 3);
                    lblTitle.Text = "翌日交換";
                    txtMochikaeriKbn.Text = "1";
                    txtImageYouhi.Text = "1";
                    break;

                case Controller.ClearingType.当日交換:
                    // 前営業日の翌日
                    prevBizDate = cal.getBusinessday(AplInfo.OpDate(), -1);
                    bizNextDate = CommonUtil.AddDate(prevBizDate, 1);
                    txtClearingDateFrom.Text = CommonUtil.ConvToDateFormat(bizNextDate, 3);

                    // 当日
                    txtClearingDateTo.Text = CommonUtil.ConvToDateFormat(AplInfo.OpDate(), 3);
                    lblTitle.Text = "当日交換";
                    txtMochikaeriKbn.Text = "1";
                    txtImageYouhi.Text = "1";
                    break;

                case Controller.ClearingType.指定交換:
                    // 前営業日の翌日
                    prevBizDate = cal.getBusinessday(AplInfo.OpDate(), -1);
                    bizNextDate = CommonUtil.AddDate(prevBizDate, 1);
                    txtClearingDateFrom.Text = CommonUtil.ConvToDateFormat(bizNextDate, 3);

                    // 処理日から指定営業日動かした日付
                    txtClearingDateTo.Text = CommonUtil.ConvToDateFormat(cal.getBusinessday(AplInfo.OpDate(), AppConfig.ClearingDefDateDiff), 3);
                    lblTitle.Text = "指定交換";
                    txtMochikaeriKbn.Text = "1";
                    txtImageYouhi.Text = "1";
                    break;
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("{0} 交換希望日From=[{1}]、交換希望日To=[{2}]",
                lblTitle.Text,
                txtClearingDateFrom.Text.Replace(".", ""),
                txtClearingDateTo.Text.Replace(".", "")), 1);
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

            // イメージ要否は常に変更不可
            txtImageYouhi.ReadOnly = true;
            txtImageYouhi.TabStop = false;

            if ((_ctl.ProcType != Controller.ClearingType.指定交換))
            {
                txtClearingDateFrom.ReadOnly = true;
                txtClearingDateFrom.OnEnterSeparatorCut = false;
                txtClearingDateTo.ReadOnly = true;
                txtClearingDateTo.OnEnterSeparatorCut = false;
                cmbClearingType.Enabled = false;
                txtMochikaeriKbn.ReadOnly = true;
                //txtImageYouhi.ReadOnly = true;

                txtClearingDateFrom.TabStop = false;
                txtClearingDateTo.TabStop = false;
                cmbClearingType.TabStop = false;
                txtMochikaeriKbn.TabStop = false;
                //txtImageYouhi.TabStop = false;
            }
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            lblFileName.Text = "";
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        private void SetDisplayParamsResult()
        {
            lblFileName.Text = _itemMgr.DispParams.CreateFileName;
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 入力チェック
            if (!CheckInputAll())
            {
                return false;
            }

            // 入力項目
            _itemMgr.DispParams.ClearingDateFrom = DBConvert.ToIntNull(txtClearingDateFrom.Text.Replace(".", ""));
            _itemMgr.DispParams.ClearingDateTo = DBConvert.ToIntNull(txtClearingDateTo.Text.Replace(".", ""));
            _itemMgr.DispParams.BillCode = ((ItemSet)cmbClearingType.SelectedItem).ItemValue;
            _itemMgr.DispParams.IcType = txtMochikaeriKbn.Text;
            _itemMgr.DispParams.ImageNeed = txtImageYouhi.Text;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("作成条件 交換希望日From=[{0}]、交換希望日To=[{1}]、交換証券種類=[{2}]、持帰対象区分=[{3}]、証券イメージ要否=[{4}]",
                _itemMgr.DispParams.ClearingDateFrom,
                _itemMgr.DispParams.ClearingDateTo,
                _itemMgr.DispParams.BillCode,
                _itemMgr.DispParams.IcType,
                _itemMgr.DispParams.ImageNeed), 1);
            return true;
        }


        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
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
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 各種入力チェック
        /// </summary>
        private void root_I_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            this.ClearStatusMessage();

            if (((BaseTextBox)sender).Name == "txtClearingDateFrom")
            {
                if (!CheckClearingDateFrom())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtClearingDateTo")
            {
                if (!CheckClearingDateTo())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtMochikaeriKbn")
            {
                if (!CheckMochikaeriKbn())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtImageYouhi")
            {
                if (!CheckImageYouhi())
                {
                    e.Cancel = true;
                }
            }
        }


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F1：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 終了
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);

                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：実行
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 実行
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "実行", 1);

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return;
                }

                // 確認メッセージ
                DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "持帰要求テキスト作成処理を開始します。\nよろしいですか？");
                if (res == DialogResult.Cancel)
                {
                    return;
                }

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00002);

                    // テキストファイル作成
                    if (!_ctl.MakeTextFile(this))
                    {
                        // 画面表示データ更新
                        RefreshDisplayData();
                        return;
                    }

                    // 完了メッセージ
                    string msg = ComMessageMgr.Msg(ComMessageMgr.I00001, "持帰要求テキスト作成");
                    ComMessageMgr.MessageInformation(msg);
                    this.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                    _isComplete = true;

                    // 画面項目設定
                    SetDisplayParamsResult();

                    // ファンクションキー状態設定
                    SetFunctionState();
                }
                finally
                {
                    //メッセージ初期化
                    EndProcessing(CommonClass.ComMessageMgr.I00002);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// サイレントモード実行
        /// </summary>
        /// <returns></returns>
        public bool ExecSilentMode()
        {
            this.ClearStatusMessage();
            try
            {
                // 実行
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "サイレントモード実行", 1);

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return false;
                }

                // テキストファイル作成
                if (!_ctl.MakeTextFile(this))
                {
                    return false;
                }

                // 完了メッセージ
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format(ComMessageMgr.I00001,"サイレントモード実行"), 1);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 交換希望日入力チェック（From）
        /// </summary>
        /// <returns></returns>
        private bool CheckClearingDateFrom()
        {
            if (txtClearingDateFrom.ReadOnly) { return true; }

            string sVal = txtClearingDateFrom.ToString();
            int fromDate = 0;
            int addmonth = AppConfig.ClearingMonthMax;

            // 数値チェック
            if (!Int32.TryParse(sVal, out fromDate))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblClearingDate.Text + "（From）"));
                return false;
            }
            // 日付チェック
            if (!EntryCommon.Calendar.IsDate(sVal))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblClearingDate.Text + "（From）"));
                return false;
            }

            // 処理日の前営業日
            iBicsCalendar cal = new iBicsCalendar();
            int prevBizDate = cal.getBusinessday(AplInfo.OpDate(), -1);
            // 処理日の前営業日の翌日
            int bizNextDate = CommonUtil.AddDate(prevBizDate, 1);
            // Nヵ月後の日付
            int maxDate = CommonUtil.AddMonth(fromDate, addmonth);
            maxDate = CommonUtil.AddDate(maxDate, -1);
            // 範囲チェック
            if ((fromDate < bizNextDate) || (maxDate < fromDate))
            {
                this.SetStatusMessage(string.Format("{0}は処理日以降～{1}ヵ月未満を入力してください。", lblClearingDate.Text + "（From）", addmonth));
                return false;
            }

            // 入力日の前営業日
            int prevBizfromDate = cal.getBusinessday(fromDate, -1);
            // 入力日の前営業日の翌日
            int bizNextfromDate = CommonUtil.AddDate(prevBizfromDate, 1);
            // 交換希望日Fromの前日
            int prevDate = CommonUtil.AddDate(fromDate, -1);
            // 営業日チェック
            if (prevDate != prevBizfromDate)
            {
                this.SetStatusMessage(string.Format("{0}は {1} を含む必要があります。", lblClearingDate.Text + "（From）", bizNextfromDate));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 交換希望日入力チェック（To）
        /// </summary>
        /// <returns></returns>
        private bool CheckClearingDateTo()
        {
            if (txtClearingDateTo.ReadOnly) { return true; }

            string sVal = txtClearingDateTo.ToString();
            int toDate = 0;
            int addmonth = AppConfig.ClearingMonthMax;

            // 数値チェック
            if (!Int32.TryParse(sVal, out toDate))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblClearingDate.Text + "（To）"));
                return false;
            }
            // 日付チェック
            if (!EntryCommon.Calendar.IsDate(sVal))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblClearingDate.Text + "（To）"));
                return false;
            }

            // 処理日の前営業日
            iBicsCalendar cal = new iBicsCalendar();
            int prevBizDate = cal.getBusinessday(AplInfo.OpDate(), -1);
            // 交換希望日From
            int fromDate = DBConvert.ToIntNull(txtClearingDateFrom.ToString());
            // 前営業日の翌日
            int bizNextDate = CommonUtil.AddDate(prevBizDate, 1);
            // Nヵ月後の日付
            int maxDate = CommonUtil.AddMonth(fromDate, addmonth);
            maxDate = CommonUtil.AddDate(maxDate, -1);

            // 範囲チェック1
            if (toDate < fromDate)
            {
                this.SetStatusMessage(string.Format("{0}は{1}以降の日付を指定してください。", lblClearingDate.Text + "（To）", lblClearingDate.Text + "（From）"));
                return false;
            }
            // 範囲チェック2
            if ((toDate < bizNextDate) || (maxDate < toDate))
            {
                this.SetStatusMessage(string.Format("{0}は処理日以降～{1}ヵ月未満を入力してください。", lblClearingDate.Text + "（To）", addmonth));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 持帰対象区分入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckMochikaeriKbn()
        {
            if (txtMochikaeriKbn.ReadOnly) { return true; }

            string sVal = txtMochikaeriKbn.ToString();

            // 必須チェック
            if (string.IsNullOrEmpty(sVal))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblMochikaeriKbn.Text));
                return false;
            }

            int nVal = DBConvert.ToIntNull(sVal);
            if ((nVal != 0) && (nVal != 1))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02007, lblMochikaeriKbn.Text, "0か1"));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 証券イメージ要否入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckImageYouhi()
        {
            if (txtImageYouhi.ReadOnly) { return true; }

            string sVal = txtImageYouhi.ToString();

            // 必須チェック
            if (string.IsNullOrEmpty(sVal))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblImageYouhi.Text));
                return false;
            }

            int nVal = DBConvert.ToIntNull(sVal);
            if ((nVal != 0) && (nVal != 1))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02007, lblImageYouhi.Text, "0か1"));
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

        /// <summary>
        /// 交換証券種類プルダウン
        /// </summary>
        /// <returns></returns>
        private void SetBillList()
        {
            List<ItemSet> list = new List<ItemSet>();
            list.Add(new ItemSet("", ""));
            foreach (TBL_BILLMF bill in _itemMgr.mst_bills.Values)
            {
                ItemSet item = new ItemSet(bill._BILL_CODE.ToString(), bill.m_STOCK_NAME);
                list.Add(item);
            }

            _billList = list;
            cmbClearingType.DataSource = _billList;
            cmbClearingType.DisplayMember = "ItemDisp";
            cmbClearingType.ValueMember = "ItemValue";
        }


        public class ItemSet
        {
            public string ItemValue { get; set; }
            public string ItemDisp { get; set; }
            public ItemSet(string value, string disp)
            {
                ItemValue = value;
                ItemDisp = disp;
            }
        }
    }
}
