using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using System.Linq;
using System.Data;

namespace ImageImportFutai
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchListForm : EntryCommonFormBase
    {

        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        #endregion

        #region クラス定数

        private const int BATCH_FOLDER_NAME = 2;
        private const int STATUS = 12;
        
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchListForm()
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
            _itemMgr.ListDispParams.Route = TBL_SCAN_BATCH_CTL.InputRoute.Futai;

            // 初期値設定
            // スキャン日
            txtScandate.Text = AplInfo.OpDate().ToString();
            _itemMgr.ListDispParams.ScanDate = txtScandate.getInt();

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
            base.SetDispName2("イメージ取込　バッチ一覧（付帯）");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (!IsPressShiftKey && !IsPressCtrlKey)
            {
                // 通常状態
                SetFunctionName(1, "終了");
                SetFunctionName(2, string.Empty);
                SetFunctionName(3, string.Empty);
                SetFunctionName(4, string.Empty);
                SetFunctionName(5, "最新表示", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(6, "自動取得", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(7, "選択取得", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(8, string.Empty);
                SetFunctionName(9, string.Empty);
                SetFunctionName(9, "ロック解除", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(10, string.Empty);
                SetFunctionName(11, string.Empty);
                SetFunctionName(12, string.Empty);
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
            this.ChangeFunctionCausesValidation(1, false);

            // 所定の権限未満の場合、無効（表示なし）
            if (NCR.Operator.Priviledge < this._ctl.SettingData.UnlockLevel)
            {
                SetFunctionName(9, string.Empty);
            }
            // 設定ファイル読み込みでエラー発生時はF1以外Disable
            if (this._ctl.SettingData.ChkServerIni == false || !string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg) )
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
            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>

        protected override void SetDisplayParams()
        {
            if (!_itemMgr.FetchScanBatchControl(this))
            {
                return;
            }

            int cnt = 0;
            int SelectItem = 0;
            Dictionary<int, int> ListColor = GetListColor();
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[_itemMgr.scan_batch.Count];
            foreach (string Key in _itemMgr.scan_batch.Keys)
            {
                TBL_SCAN_BATCH_CTL param = _itemMgr.scan_batch[Key];

                string key = Key;
                string inputroute = param._INPUT_ROUTE.ToString();
                string BATCH_FOLDER_NAME = param._BATCH_FOLDER_NAME.ToString();
                string OC_BK_NO = DispDataFormatZero(param, param.m_OC_BK_NO, "D4");
                string OC_BR_NO = DispDataFormatZero(param, param.m_OC_BR_NO, "D4");
                string SCAN_BR_NO = DispDataFormatZero(param, param.m_SCAN_BR_NO, "D4");
                string SCAN_DATE = CommonUtil.ConvToDateFormat(DispDataFormatZero(param, param.m_SCAN_DATE, "D8"), 3);
                string CLEARING_DATE = CommonUtil.ConvToDateFormat(DispDataFormatZero(param, param.m_CLEARING_DATE, "D8"), 3);
                string SCAN_COUNT = DispDataFormatZero(param, param.m_SCAN_COUNT, "#,##0");
                string TOTAL_COUNT = DispDataFormatZero(param, param.m_TOTAL_COUNT, "#,##0");
                string TOTAL_AMOUNT = DispDataFormatZero(param, param.m_TOTAL_AMOUNT, "#,##0");
                string IMAGE_COUNT = DispDataFormatZero(param, param.m_IMAGE_COUNT, "#,##0");
                string STATUS = param.m_STATUS.ToString();
                string STATUSTEXT = TBL_SCAN_BATCH_CTL.GetStatusText((TBL_SCAN_BATCH_CTL.enumStatus)param.m_STATUS);
                string LOCK = param.m_LOCK_TERM.ToString();

                listItem.Clear();
                listItem.Add(key);
                listItem.Add(inputroute);
                listItem.Add(BATCH_FOLDER_NAME);
                listItem.Add(OC_BK_NO);
                listItem.Add(OC_BR_NO);
                listItem.Add(SCAN_BR_NO);
                listItem.Add(SCAN_DATE);
                listItem.Add(CLEARING_DATE);
                listItem.Add(SCAN_COUNT);
                listItem.Add(TOTAL_COUNT);
                listItem.Add(TOTAL_AMOUNT);
                listItem.Add(IMAGE_COUNT);
                listItem.Add(STATUS);
                listItem.Add(STATUSTEXT);
                listItem.Add(LOCK);
                listView[cnt] = new ListViewItem(listItem.ToArray());

                //色設定
                if (ListColor.ContainsKey(param.m_STATUS) )
                {
                    listView[cnt].UseItemStyleForSubItems = true;
                    listView[cnt].BackColor = System.Drawing.Color.FromArgb(ListColor[param.m_STATUS]);
                }

                // 前選択状態の復元
                if (inputroute == _itemMgr.ListDispParams.Route.ToString("d") && 
                    BATCH_FOLDER_NAME == _itemMgr.ListDispParams.BatchFolderName)
                {
                    SelectItem = cnt;
                }

                cnt++;
            }

            this.lvBatList.BeginUpdate();
            this.lvBatList.Items.Clear();
            this.lvBatList.Items.AddRange(listView);
            this.lvBatList.Enabled = true;
            this.lvBatList.Refresh();
            this.lvBatList.Select();

            if (this.lvBatList.Items.Count > 0)
            {
                this.lvBatList.Items[SelectItem].Selected = true;
                this.lvBatList.Items[SelectItem].Focused = true;
                //初期表示位置調整
                SetListDefPositon(lvBatList);
            }
            this.lvBatList.EndUpdate();
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        private Dictionary<int,int> GetListColor()
        {
            Dictionary<int, int> ColorList = new Dictionary<int, int>();

            foreach (string Data in this._ctl.SettingData.BatchListBackColor.Split(','))
            {
                List<string> SplitList = new List<string>(Data.Split(':'));

                if(SplitList.Count != 2)
                {
                    // 「コード:色」形式以外は無視
                    continue;
                }

                if (SplitList[1].StartsWith("0x"))
                {
                    if (int.TryParse(SplitList[0], out int Status) &&
                       int.TryParse(SplitList[1].Remove(0, 2), System.Globalization.NumberStyles.HexNumber, new System.Globalization.CultureInfo("ja-JP"), out int Color))
                    {
                        ColorList.Add(Status, Color);
                    }
                }
            }

            return ColorList;
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 選択行の情報を取得する
            if (!GetSelectListData())
            {
                MessageBox.Show("対象行が選択されていません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// 設定ファイル読み込みでエラーメッセージ表示
        /// </summary>
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
        /// 各種入力チェック
        /// </summary>
        private void txtBox_IValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ClearStatusMessage();

            if (((BaseTextBox)sender).Name == "txtScandate")
            {
                if (!ChkScanDate())
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// スキャン日 KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScandate_KeyDown(object sender, KeyEventArgs e)
        {
            //this.ClearStatusMessage();

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    // 最新表示実行
                    if (this.btnFunc[5].Enabled)
                    {
                        this.btnFunc05_Click(null, null);
                    }
                    break;
            }
        }

        /// <summary>
        /// 一覧ダブルクリック
        /// </summary>
        private void lv_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                SendKeys.Send("{Enter}");
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// 一覧 KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (this.btnFunc[7].Enabled)
                        {
                            this.btnFunc07_Click(null, null);
                        }
                        break;
                    default:
                        break;
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
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F5：最新表示
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "最新表示", 1);

                // 選択行の情報クリア
                _itemMgr.ListDispParams.SelectDataInit();

                // 入力チェック
                if (!CheckInputAll()) return;

                //スキャン日設定
                _itemMgr.ListDispParams.ScanDate = txtScandate.getInt();

                // 最新表示
                this.ResetForm();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F6：自動取得
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "自動取得", 1);

                // 再描画時の復元のため、選択行の情報のみを取得
                // 未選択でもエラーとしない
                GetSelectListData();

                if (_itemMgr.ListDispParams.BatchSelected)
                {
                    // 選択行がある場合、選択行以降のデータを取得するため
                    // 状態順に表示されるため対象が保留以上の場合は自動取得データがないので処理終了
                    if (DBConvert.ToIntNull(_itemMgr.ListDispParams.Status) >= (int)TBL_SCAN_BATCH_CTL.enumStatus.Hold)
                    {
                        return;
                    }
                }

                // Autoモード設定
                _itemMgr.InputParams.Mode = ItemManager.BatchInputParams.InputMode.Auto;

                // INPUT画面表示
                BatchInputForm form = new BatchInputForm();
                form.InitializeForm(_ctl);

                if (_itemMgr.InputParams.DispMode == ItemManager.BatchInputParams.DispErrMode.BatchDataGetErr)
                {
                    // バッチデータ取得エラー場合
                    this.SetStatusMessage("データの取得に失敗しました");
                    return;
                }
                else if (_itemMgr.InputParams.DispMode != ItemManager.BatchInputParams.DispErrMode.BatchDataNoData)
                {
                    // バッチデータがない以外の場合画面表示
                    form.ResetForm();
                    DialogResult result = form.ShowDialog();
                    // OK：処理成功 Abort：エラー終了 Cancel：終了/削除/保留等で終了
                    if (result == DialogResult.OK)
                    {
                        // 処理成功時、完了メッセージ表示
                        this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.I00001, "データ取込"), Color.Transparent);
                    }
                }

                // 戻ってきたら再描画
                this.ResetForm(); 
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F7：選択取得
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "選択取得", 1);

                // 選択行の情報を取得する
                if (!GetDisplayParams())
                {
                    return;
                }

                if (!(_itemMgr.ListDispParams.Status == TBL_SCAN_BATCH_CTL.enumStatus.Wait.ToString("d") ||
                    _itemMgr.ListDispParams.Status == TBL_SCAN_BATCH_CTL.enumStatus.Hold.ToString("d") ||
                    _itemMgr.ListDispParams.Status == TBL_SCAN_BATCH_CTL.enumStatus.Delete.ToString("d")))
                {
                    // 処理待ち・保留・削除以外の場合
                    return;
                }

                // Selectモード設定
                _itemMgr.InputParams.Mode = ItemManager.BatchInputParams.InputMode.Select;

                // INPUT画面表示
                BatchInputForm form = new BatchInputForm();
                form.InitializeForm(_ctl);

                if (_itemMgr.InputParams.DispMode == ItemManager.BatchInputParams.DispErrMode.BatchDataGetErr)
                {
                    // バッチデータ取得エラー場合
                    this.SetStatusMessage("データの取得に失敗しました");
                    return;
                }
                else if (_itemMgr.InputParams.DispMode == ItemManager.BatchInputParams.DispErrMode.BatchDataNoData)
                {
                    // バッチデータがない場合
                    this.SetStatusMessage("バッチ票入力できないバッチです。最新状態を確認してください。");
                }
                else
                {
                    // 上記以外の場合画面表示
                    form.ResetForm();
                    DialogResult result = form.ShowDialog();
                    // OK：処理成功 Abort：エラー終了 Cancel：終了/削除/保留等で終了
                    if (result == DialogResult.OK)
                    {
                        // 処理成功時、完了メッセージ表示
                        this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.I00001, "データ取込"), Color.Transparent);
                    }
                }

                // 戻ってきたら再描画
                this.ResetForm();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F9：ロック解除
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 選択行の情報を取得する
                if (!GetDisplayParams())
                {
                    return;
                }

                //ステータスチェック
                if (_itemMgr.ListDispParams.Status != TBL_SCAN_BATCH_CTL.enumStatus.Processing.ToString("D"))
                {
                    //処理中以外
                    ComMessageMgr.MessageWarning("選択バッチフォルダはロック解除対象外です");
                    return;
                }

                //確認メッセージ表示
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "ロック解除してもよろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ロック解除", 1);

                // 更新処理実行
                _itemMgr.UpdateStatusWaitSerrchList(this);

                // 戻ってきたら再描画
                this.ResetForm();
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
        /// 一覧初期位置設定
        /// </summary>
        private void SetListDefPositon(ListView list)
        {
            // 初期位置設定
            if (list.SelectedIndices.Count < 1)
            {
                // 選択行なし
                return;
            }

            // 選択行を中央表示(おおよそ)
            int i = list.SelectedItems[0].Index;
            Rectangle rect = list.GetItemRect(0);
            i += (int)Math.Floor((double)list.ClientSize.Height / rect.Height / 2) - 1;
            if (list.Items.Count - 1 < i)
            {
                i = list.Items.Count - 1;
            }
            list.Items[i].EnsureVisible();
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
        /// スキャン日入力チェック
        /// </summary>
        private bool ChkScanDate()
        {
            string ChkText = txtScandate.ToString();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblScandate.Text));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblScandate.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblScandate.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 一覧選択情報取得
        /// </summary>
        private bool GetSelectListData()
        {
            // クリア
            _itemMgr.ListDispParams.SelectDataInit();

            // 選択行の情報を取得する
            if (this.lvBatList.SelectedIndices.Count < 1)
            {
                return false;
            }
            _itemMgr.ListDispParams.BatchFolderName = this.lvBatList.SelectedItems[0].SubItems[BATCH_FOLDER_NAME].Text;
            _itemMgr.ListDispParams.Status = this.lvBatList.SelectedItems[0].SubItems[STATUS].Text;

            // 自動選択時の初期設定情報
            _itemMgr.ListDispParams.BatchSelected = true;
            _itemMgr.ListDispParams.AutoSelectBatchFolder = _itemMgr.ListDispParams.BatchFolderName;

            return true;
        }

    }
}

