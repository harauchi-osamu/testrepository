using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace SearchBalanceTxtView
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchBalanceTxt : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private DispMode _curFlg = DispMode.Input;

        #region enum

        public enum DispMode
        {
            ///<summary>入力項目</summary>
            Input = 1,
            ///<summary>結果一覧</summary>
            List = 2,
        }

        #endregion

        private const int COL_KEY = 0;
        private const int COL_PAY_KBN = 3;
        private const int COL_ICBANK = 9;
        private const int COL_AMOUNT = 10;
        private const int COL_CLEARINGDATE = 11;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchBalanceTxt()
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
            switch (AppInfo.Setting.GymId)
            {
                case GymParam.GymId.持出:
                    base.SetDispName1("交換持出");
                    break;
                case GymParam.GymId.持帰:
                    base.SetDispName1("交換持帰");
                    break;
                default:
                    base.SetDispName1("");
                    break;
            }
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("交換尻証券テキスト照会");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (!IsPressShiftKey && !IsPressCtrlKey)
            {
                // 通常状態
                if (_curFlg == DispMode.Input)
                {
                    SetFunctionName(1, "終了");
                    SetFunctionName(2, string.Empty);
                    SetFunctionName(3, string.Empty);
                    SetFunctionName(4, "条件クリア", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(5, string.Empty);
                    SetFunctionName(6, string.Empty);
                    SetFunctionName(7, string.Empty);
                    SetFunctionName(8, string.Empty);
                    SetFunctionName(9, string.Empty);
                    SetFunctionName(10, string.Empty);
                    SetFunctionName(11, string.Empty);
                    SetFunctionName(12, "検索");
                }
                else
                {
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
                    SetFunctionName(12, "検索条件", true, Const.FONT_SIZE_FUNC_LOW);
                }
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
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
            // 作成日
            if (_itemMgr.DispParams.CtlDate > 0)
            {
                dtCTLDate.setText(_itemMgr.DispParams.CtlDate);
            }
            // コンボボックス設定
            SetPayKbn(cmbPayKbn, _itemMgr.DispParams.PayKbn);
            SetICFlg(cmbICFlg, _itemMgr.DispParams.ICFlg);
            SetDiff(cmbDiff, _itemMgr.DispParams.Diff);
            SetFuwatari(cmbFuwatari, _itemMgr.DispParams.Fuwatari);
            SetPkgOnly(cmbPkgOnly, _itemMgr.DispParams.PkgOnly);
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
            //クリア
            _itemMgr.DispParams.Clear();

            if (!string.IsNullOrEmpty(dtCTLDate.ToString()))
            {
                _itemMgr.DispParams.CtlDate = dtCTLDate.getInt();
            }
            if (!string.IsNullOrEmpty(ntOCBank.ToString()))
            {
                _itemMgr.DispParams.OCBKNo = ntOCBank.getInt();
            }
            if (!string.IsNullOrEmpty(dtCDate.ToString()))
            {
                _itemMgr.DispParams.Date = dtCDate.getInt();
            }
            if (!string.IsNullOrEmpty(ntICBank.ToString()))
            {
                _itemMgr.DispParams.ICBKNo = ntICBank.getInt();
            }
            if (!string.IsNullOrEmpty(ntAmount.ToString()))
            {
                _itemMgr.DispParams.Amount = ntAmount.getLong();
            }
            _itemMgr.DispParams.PayKbn = ((ComboBoxItemInt)cmbPayKbn.SelectedItem).ID;
            _itemMgr.DispParams.ICFlg = ((ComboBoxItemInt)cmbICFlg.SelectedItem).ID;
            _itemMgr.DispParams.Diff = ((ComboBoxItemInt)cmbDiff.SelectedItem).ID;
            _itemMgr.DispParams.Fuwatari = ((ComboBoxItemInt)cmbFuwatari.SelectedItem).ID;
            _itemMgr.DispParams.PkgOnly = ((ComboBoxItemInt)cmbPkgOnly.SelectedItem).ID;
            _itemMgr.DispParams.ImgFLNm = txtImgFLNm.Text;
            if (rdoImgOpt1.Checked)
            {
                _itemMgr.DispParams.ImgFLNmOpt = 1;
            }
            else if (rdoImgOpt2.Checked)
            {
                _itemMgr.DispParams.ImgFLNmOpt = 2;
            }
            else
            {
                _itemMgr.DispParams.ImgFLNmOpt = 3;
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
            // メッセージボックスを表示する関連で初回表示をShownイベントに移動
            ////一覧表示
            //DispResultText(true);

            // 設定ファイル読み込みでエラー発生時
            if (this._ctl.SettingData.ChkServerIni == false)
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00003));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E00003), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return;
            }
            if (!string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E01001, this._ctl.SettingData.CheckParamMsg));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E01001, this._ctl.SettingData.CheckParamMsg), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return;
            }
        }

        /// <summary>
        /// 画面初回表示
        /// </summary>
        private void Form_Shown(object sender, EventArgs e)
        {
            //一覧表示
            DispResultText(true);
        }

        /// <summary>
        /// 各種入力チェック
        /// </summary>
        private void txtBox_IValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ClearStatusMessage();

            if (((BaseTextBox)sender).Name == "dtCDate")
            {
                if (!CheckClearingDate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntICBank")
            {
                if (!CheckICBankCode())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntAmount")
            {
                if (!CheckAmount())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "dtCTLDate")
            {
                if (!CheckCreateDate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntOCBank")
            {
                if (!CheckOCBankCode())
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
            // ファンクション切り替え
            if (_curFlg != DispMode.Input)
            {
                _curFlg = DispMode.Input;
                InitializeFunction();
                SetFunctionState();
            }
        }

        /// <summary>
        /// フォーカスが外れたら背景を白色にする
        /// </summary>
        private void txt_Leave(object sender, EventArgs e)
        {
            RemoveFocusBackColor((Control)sender);
        }

        /// <summary>
        /// フォーカスのフォーカス設定
        /// </summary>
        private void cmb_Enter(object sender, EventArgs e)
        {
            // ファンクション切り替え
            if (_curFlg != DispMode.Input)
            {
                _curFlg = DispMode.Input;
                InitializeFunction();
                SetFunctionState();
            }
        }

        /// <summary>
        /// LISTのフォーカス設定
        /// </summary>
        private void List_Enter(object sender, EventArgs e)
        {
            // ファンクション切り替え
            if (_curFlg != DispMode.List)
            {
                _curFlg = DispMode.List;
                InitializeFunction();
                SetFunctionState();
            }
        }

        /// <summary>
        /// コンボKeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    SendKeys.Send("{TAB}");
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.F4:
                    // 条件クリア処理を優先
                    e.Handled = true;
                    break;
            }
        }

        /// <summary>
        /// dropdownlist設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();

            // 選択時は青い背景となるので、文字を白くする
            bool selected = DrawItemState.Selected == (e.State & DrawItemState.Selected);
            var brush = (selected) ? Brushes.White : Brushes.Black;
            if (!((ComboBox)sender).Enabled)
            {
                // 使用不可の場合
                brush = Brushes.Gray;
            }
            e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvDataList.Columns[e.ColumnIndex].Width;
        }

        bool cmbChange = false;
        /// <summary>
        /// Combo変更
        /// </summary>
        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Comboの再帰処理は不可
            if (cmbChange) return;

            try
            {
                cmbChange = true;
                if (ReferenceEquals(sender, cmbPkgOnly))
                {
                    // パッケージDBのみComboBox
                    ChgInputData();
                }
            }
            finally
            {
                cmbChange = false;
            }
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************     

        /// <summary>
        /// F04：条件クリア
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                //確認メッセージ表示
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "入力条件をクリアしてもよろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "条件クリア", 1);

                // クリア処理
                ClearInputAll();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                //this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                this.SetStatusMessageFontSizeAuto(string.Format(ComMessageMgr.E00004, ex.Message), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
            }
        }

        /// <summary>
        /// F12：検索/検索条件
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {               
                if (_curFlg == DispMode.Input)
                {
                    // 検索
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "検索", 1);
                    //入力内容チェック
                    if (!CheckInputAll()) return;

                    // データ取得
                    GetDisplayParams();

                    //一覧表示
                    DispResultText(true);
                }
                else
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "検索条件", 1);
                    // 検索条件
                    dtCTLDate.Focus();
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                //this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                this.SetStatusMessageFontSizeAuto(string.Format(ComMessageMgr.E00004, ex.Message), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
            }
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 内容表示
        /// </summary>
        private void DispResultText(bool showMessage)
        {
            bool PkgOnly = false;
            bool LimitOver = false;
            if (_itemMgr.DispParams.PkgOnly == 0)
            {
                // 通常検索
                if (!_itemMgr.FetchBalanceTextControl(_ctl.SettingData.ListDispLimit, out LimitOver, out PkgOnly, this))
                {
                    return;
                }
            }
            else
            {
                // パッケージのみ
                if (!_itemMgr.FetchBalanceTextControlPkgOnly(_ctl.SettingData.ListDispLimit, out LimitOver, this))
                {
                    return;
                }
            }

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[_itemMgr.BalanceTxt.Count];
            foreach (string Key in _itemMgr.BalanceTxt.Keys)
            {
                listItem = new List<string>();
                ItemManager.DetailData detail = _itemMgr.BalanceTxt[Key];

                // データ設定
                listItem.Add(Key);
                listItem.Add(detail.ImgName);
                listItem.Add(BalanceTxtCommon.DispOCMethod(detail.OCMethod));
                listItem.Add(BalanceTxtCommon.DispPayKbn(detail.PayKbn));
                listItem.Add(BalanceTxtCommon.DispICFlg(detail.ICFlg));
                listItem.Add(BalanceTxtCommon.DispFormat(detail.OCBKNo, "D4"));
                listItem.Add(BalanceTxtCommon.DispFormat(detail.PayICBKNo, "D4"));
                listItem.Add(BalanceTxtCommon.DispFormat(detail.PayAmount, "#,##0"));
                listItem.Add(BalanceTxtCommon.DispDate(detail.ClearingDate, ""));
                listItem.Add(BalanceTxtCommon.DispFormat(detail.EndICBKNo, "D4"));
                listItem.Add(BalanceTxtCommon.DispFormat(detail.EndAmount, "#,##0"));
                listItem.Add(BalanceTxtCommon.DispDate(detail.EndClearingDate, ""));
                listItem.Add(BalanceTxtCommon.DispFuwatari(detail));
                listView[cnt] = new ListViewItem(listItem.ToArray());

                //色設定
                if (!BalanceTxtCommon.ChkPayKbn(detail.PayKbn))
                {
                    listView[cnt].UseItemStyleForSubItems = false;
                    listView[cnt].SubItems[COL_PAY_KBN].BackColor = Color.Yellow;
                }
                if (!BalanceTxtCommon.ChkInputData(detail.PayICBKNo, detail.EndICBKNo, 4))
                {
                    listView[cnt].UseItemStyleForSubItems = false;
                    listView[cnt].SubItems[COL_ICBANK].BackColor = Color.Yellow;
                }
                if (!BalanceTxtCommon.ChkInputData(detail.PayAmount, detail.EndAmount, 12))
                {
                    listView[cnt].UseItemStyleForSubItems = false;
                    listView[cnt].SubItems[COL_AMOUNT].BackColor = Color.Yellow;
                }
                if (!BalanceTxtCommon.ChkInputData(detail.ClearingDate, detail.EndClearingDate, 8))
                {
                    listView[cnt].UseItemStyleForSubItems = false;
                    listView[cnt].SubItems[COL_CLEARINGDATE].BackColor = Color.Yellow;
                }

                cnt++;
            }
            this.lvDataList.BeginUpdate();
            this.lvDataList.Items.Clear();
            this.lvDataList.Items.AddRange(listView);
            this.lvDataList.Enabled = true;
            this.lvDataList.Refresh();
            this.lvDataList.Select();
            this.lvDataList.EndUpdate();


            if (this.lvDataList.Items.Count > 0)
            {
                this.lvDataList.Items[0].Selected = true;
                this.lvDataList.Items[0].Focused = true;
            }

            // 結果メッセージ表示
            if (showMessage)
            {
                SetDispResultMessage(LimitOver);

                if (PkgOnly)
                {
                    // パッケージのみデータあり
                    string msg = "指定作成日（交換日）でパッケージDBにのみ存在するデータがあります\n確認する場合は、パッケージDBのみへチェックを入れて検索してください";
                    ComMessageMgr.MessageInformation(msg);
                }
            }
        }

        /// <summary>
        /// 検索結果メッセージ表示
        /// </summary>
        private void SetDispResultMessage(bool LimitOver)
        {
            // [検索結果] 証券明細テキスト 合計枚数：ZZZ,ZZ9枚 合計金額：ZZZ,ZZ9円  パッケージ 合計枚数：ZZZ,ZZ9枚 合計金額：ZZZ,ZZ9円
            string msg = string.Format("[{0}]　証券明細テキスト 合計枚数：{1:#,0}枚 合計金額：{2:#,0}円  パッケージ 合計枚数：{3:#,0}枚 合計金額：{4:#,0}円", 
                                       "検索結果", 
                                       _itemMgr.BalanceTxt.LongCount(x => x.Value.TxtExists),
                                       _itemMgr.BalanceTxt.Sum(x => DBConvert.ToLongNull(x.Value.PayAmount)),
                                       _itemMgr.BalanceTxt.LongCount(x => x.Value.PackageExists),
                                       _itemMgr.BalanceTxt.Where(x => x.Value.PackageExists).Sum(x => DBConvert.ToLongNull(x.Value.EndAmount)));

            if (LimitOver)
            {
                msg += string.Format(" (" + ComMessageMgr.W00003 + ")", _ctl.SettingData.ListDispLimit);
                //this.SetStatusMessage(msg);
                this.SetStatusMessageFontSizeAuto(msg, Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
            }
            else
            {
                //this.SetStatusMessage(msg, Color.Transparent);
                this.SetStatusMessageFontSizeAuto(msg, Color.Transparent, ItemManager.MESSEGELBL_DEFSIZE);
            }
        }

        #region コンボボックス設定

        /// <summary>
        /// 決済対象区分設定
        /// </summary>
        private void SetPayKbn(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(0, "決済対象"));
            combo.Items.Add(new ComboBoxItemInt(1, "決済を伴わないイメージ交換"));
            combo.Items.Add(new ComboBoxItemInt(2, "破産・脱退に伴う一時停止"));
            combo.Items.Add(new ComboBoxItemInt(3, "業務停止に伴う一時停止"));
            combo.Items.Add(new ComboBoxItemInt(4, "保険事故に伴う一時停止"));
            combo.Items.Add(new ComboBoxItemInt(5, "決済受託銀行の一時停止"));
            combo.Items.Add(new ComboBoxItemInt(9, "脱退(承継なし)による停止"));
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 持帰状況フラグ設定
        /// </summary>
        private void SetICFlg(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(0, "未持帰"));
            combo.Items.Add(new ComboBoxItemInt(1, "持帰済み"));
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 差異設定
        /// </summary>
        private void SetDiff(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(0, "無"));
            combo.Items.Add(new ComboBoxItemInt(1, "有"));
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 不渡設定
        /// </summary>
        private void SetFuwatari(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(0, ""));
            combo.Items.Add(new ComboBoxItemInt(1, "含む"));
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// パッケージDBのみ
        /// </summary>
        private void SetPkgOnly(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(0, ""));
            combo.Items.Add(new ComboBoxItemInt(1, "指定"));
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 入力項目変更処理
        /// </summary>
        private void ChgInputData()
        {
            if (((ComboBoxItemInt)cmbPkgOnly.SelectedItem).ID == 1)
            {
                // パッケージDBのみが指定の場合、不要な項目を編集不可に変更
                ntOCBank.Enabled = false;
                dtCDate.Enabled = false;
                ntICBank.Enabled = false;
                ntAmount.Enabled = false;
                SetCmbEnabled(cmbPayKbn, false);
                SetCmbEnabled(cmbICFlg, false);
                SetCmbEnabled(cmbDiff, false);
                SetCmbEnabled(cmbFuwatari, false);
            }
            else
            {
                // 上記以外の場合、すべて編集可に変更
                ntOCBank.Enabled = true;
                dtCDate.Enabled = true;
                ntICBank.Enabled = true;
                ntAmount.Enabled = true;
                SetCmbEnabled(cmbPayKbn, true);
                SetCmbEnabled(cmbICFlg, true);
                SetCmbEnabled(cmbDiff, true);
                SetCmbEnabled(cmbFuwatari, true);
            }
        }

        /// <summary>
        /// コンボボックスのEnabled設定
        /// </summary>
        private void SetCmbEnabled(ComboBox cmb, bool Enabled)
        {
            cmb.Enabled = Enabled;
            // 背景色が変わらないため個別設定する
            if (Enabled)
            {
                cmb.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                cmb.BackColor = System.Drawing.SystemColors.MenuBar;
            }
        }

        #endregion

        #region コンボボックス関連クラス

        /// <summary>
        /// ComboBoxItem(文字列)
        /// </summary>
        private class ComboBoxItemString
        {
            public string ID { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;

            public ComboBoxItemString(string id, string name)
            {
                ID = id;
                Name = name;
            }

            //コンボボックス表示文字列
            public override string ToString()
            {
                return Name;
            }

            public override bool Equals(object obj)
            {
                //objがnullか、型が違うときは、等価でない
                if (obj == null || this.GetType() != obj.GetType()) return false;
                return ((ComboBoxItemString)obj).ID == this.ID;
            }

            public override int GetHashCode()
            {
                return this.ID.GetHashCode();
            }
        }

        /// <summary>
        /// ComboBoxItem(数値)
        /// </summary>
        private class ComboBoxItemInt
        {
            public int ID { get; set; } = 0;
            public string Name { get; set; } = string.Empty;

            public ComboBoxItemInt(int id, string name)
            {
                ID = id;
                Name = name;
            }

            //コンボボックス表示文字列
            public override string ToString()
            {
                return Name;
            }

            public override bool Equals(object obj)
            {
                //objがnullか、型が違うときは、等価でない
                if (obj == null || this.GetType() != obj.GetType()) return false;
                return ((ComboBoxItemInt)obj).ID == this.ID;
            }

            public override int GetHashCode()
            {
                return this.ID.GetHashCode();
            }
        }

        #endregion

        #region 条件クリア処理

        /// <summary>
        /// 全入力項目クリア
        /// </summary>
        private void ClearInputAll()
        {
            // TextBox・ComboBoxクリア
            ClearInputBox();
            // ラジオボタンクリア
            rdoImgOpt1.Checked = true;

            //先頭にフォーカス遷移
            this.AutoValidate = AutoValidate.Disable;
            dtCTLDate.Focus();
            this.AutoValidate = AutoValidate.EnablePreventFocusChange;
        }

        /// <summary>
        /// TextBox・ComboBoxクリア
        /// </summary>
        private bool ClearInputBox()
        {
            foreach (Control con in AllSubControls(this).OrderBy(c => c.TabIndex))
            {
                if (con is BaseTextBox)
                {
                    ClearTextBox((BaseTextBox)con);
                }
                if (con is ComboBox)
                {
                    ClearComboBox((ComboBox)con);
                }
            }
            return true;
        }

        /// <summary>
        /// ComboBoxクリア
        /// </summary>
        private void ClearComboBox(ComboBox combo, int DefItem = 0)
        {
            if (!combo.Enabled || combo.Items.Count == 0) return;
            // クリア処理
            combo.SelectedIndex = DefItem;
        }

        /// <summary>
        /// TextBoxクリア
        /// </summary>
        private void ClearTextBox(BaseTextBox TextBox, string DefValue = "")
        {
            if (!TextBox.Enabled || TextBox.ReadOnly) return;
            // クリア処理
            TextBox.Text = DefValue;
        }

        #endregion

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
                    if (((BaseTextBox)con).Enabled == false)
                    {
                        // 使用不可の場合、チェックなし
                        continue;
                    }

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
        /// 作成日入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckCreateDate()
        {
            string ChkText = dtCTLDate.ToString();
            int ChkIntText = dtCTLDate.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblCTLDate.Text));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E02002, lblCTLDate.Text), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblCTLDate.Text));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E02003, lblCTLDate.Text), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 持出銀行コード入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckOCBankCode()
        {
            string ChkText = ntOCBank.ToString();
            int ChkIntText = ntOCBank.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblOCBank.Text));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E02002, lblOCBank.Text), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 交換日入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckClearingDate()
        {
            string ChkText = dtCDate.ToString();
            int ChkIntText = dtCDate.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblCDate.Text));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E02002, lblCDate.Text), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblCDate.Text));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E02003, lblCDate.Text), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 決済持帰銀行コード入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckICBankCode()
        {
            string ChkText = ntICBank.ToString();
            int ChkIntText = ntICBank.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblICBank.Text));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E02002, lblICBank.Text), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 金額入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckAmount()
        {
            string ChkText = ntAmount.ToString();
            long ChkLongText = ntAmount.getLong();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!long.TryParse(ChkText, out long i))
            {
                //this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblAmount.Text));
                this.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E02002, lblAmount.Text), Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE);
                return false;
            }

            return true;
        }

        #endregion
    }
}
