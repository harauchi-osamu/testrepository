using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace CTROcBrTotalView
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchBranchGoukeiForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ImageHandler _imgHandler = null;
        private List<ItemSet> _totalumuList = null;
        private List<ItemSet> _maisuList = null;
        private List<ItemSet> _kingakuList = null;

        private DispMode _dspMode = DispMode.入力項目;
        private bool _isInputMode { get { return (_dspMode == DispMode.入力項目); } }

        /// <summary>
        /// 表示件数
        /// </summary>
        public long DispCount
        {
            get
            {
                if (_itemMgr.tbl_brtotals == null) return 0;
                return Math.Min(_itemMgr.tbl_brtotals.Rows.Count, AppConfig.ListDispLimit);
            }
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

        private const int COL_KEY = 0;
        private const int COL_TOTALCOUNT = 3;
        private const int COL_TOTALAMOUNT = 4;

        public enum DispMode
        {
            入力項目 = 1,
            結果一覧 = 2,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchBranchGoukeiForm()
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
            _imgHandler = new ImageHandler(_ctl);

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
            // 通常状態
            if (_isInputMode)
            {
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
                SetFunctionName(F12_, "検索");
            }
            else
            {
                SetFunctionName(F1_, "終了");
                SetFunctionName(F2_, "検索条件", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F3_, string.Empty);
                SetFunctionName(F4_, string.Empty);
                SetFunctionName(F5_, "拡大");
                SetFunctionName(F6_, "縮小");
                SetFunctionName(F7_, string.Empty);
                SetFunctionName(F8_, string.Empty);
                SetFunctionName(F9_, string.Empty);
                SetFunctionName(F10_, string.Empty);
                SetFunctionName(F11_, string.Empty);
                SetFunctionName(F12_, "詳細");
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
                return;
            }

            if (!_isInputMode)
            {
                // リスト選択状態の場合

                // 選択行のバッチ情報を取得する
                GetSelectedBatData(lvBatList, out TBL_BR_TOTAL brt);

                bool FuncFlg = true;
                if (_imgHandler.HasImage == false)
                {
                    //初期表示・合計票がないデータ選択時
                    //CreateImageControlが呼ばれるまでHasImageがtrueになることはないため留意が必要
                    FuncFlg = false;
                }

                // 拡大
                SetFunctionState(F5_, FuncFlg);
                // 縮小
                SetFunctionState(F6_, FuncFlg);
                // 詳細
                SetFunctionState(F12_, FuncFlg);
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
            //RefreshDisplayData();

            // 画面表示状態更新
            RefreshDisplayState();
        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        protected void InitializeControl()
        {
            if (_ctl.IsIniErr) { return; }

            SetComboBox();
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
            txtScanDate.Text = AplInfo.OpDate().ToString();
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
        /// 画面表示データ更新
        /// </summary>
        private void RefreshDisplayData(bool showMessage)
        {
            // 画面項目設定
            SetDisplayParams(showMessage);
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
        private void SetDisplayParams(bool showMessage)
        {
            // 画像イメージクリア
            this.RemoveImgControl(_imgHandler);

            // データ取得
            if (!_itemMgr.GetOcBatList(this))
            {
                return;
            }

            int cnt = 0;
            int SelectItem = 0;
            List<DataRow> DispData = new List<DataRow>();
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[DispCount];
            foreach (DataRow row in _itemMgr.tbl_brtotals.Rows)
            {
                if (DispCount <= cnt)
                {
                    //表示上限を越えた場合一覧表示終了
                    break;
                }

                TBL_BR_TOTAL brt = new TBL_BR_TOTAL(row);
                string key = CommonUtil.GenerateKey(brt._GYM_ID, brt._OPERATION_DATE, brt._SCAN_IMG_FLNM);
                string sScanDate = (brt.m_SCAN_DATE == 0) ? "" : CommonUtil.ConvToDateFormat(brt.m_SCAN_DATE, 3);
                string sOcBrName = _ctl.GetBranchName(brt.m_BR_NO);
                string sBatCount = chkBrTotal(row) ? string.Format("{0:###,##0}", brt.m_TOTAL_COUNT) : "";
                string sBatAmount = chkBrTotal(row) ? string.Format("{0:###,###,###,###,##0}", brt.m_TOTAL_AMOUNT) : "";
                string sMeiCount = string.Format("{0:###,##0}", DBConvert.ToLongNull(row["MEI_COUNT"]));
                string sMeiAmount = string.Format("{0:###,###,###,###,##0}", DBConvert.ToLongNull(row["MEI_AMOUNT"]));
                string sDiffCount = string.Format("{0:###,##0}", DBConvert.ToLongNull(row["DIFF_COUNT"]));
                string sDiffAmount = string.Format("{0:###,###,###,###,##0}", DBConvert.ToLongNull(row["DIFF_AMOUNT"]));

                listItem.Clear();
                listItem.Add(key);              // 隠しキー
                listItem.Add(sScanDate);        // スキャン日
                listItem.Add(sOcBrName);        // 持出支店
                listItem.Add(sBatCount);        // バッチ枚数
                listItem.Add(sBatAmount);       // バッチ金額
                listItem.Add(sMeiCount);        // 明細枚数
                listItem.Add(sMeiAmount);       // 明細金額
                listItem.Add(sDiffCount);       // 差枚数
                listItem.Add(sDiffAmount);      // 差金額
                listView[cnt] = new ListViewItem(listItem.ToArray());

                //色設定
                if (!chkBrTotal(row))
                {
                    listView[cnt].UseItemStyleForSubItems = false;
                    listView[cnt].SubItems[COL_TOTALCOUNT].BackColor = Color.Yellow;
                    listView[cnt].SubItems[COL_TOTALAMOUNT].BackColor = Color.Yellow;
                }

                // 前選択状態の復元
                if (key == _itemMgr.DispParams.Key)
                {
                    SelectItem = cnt;
                }

                DispData.Add(row);
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

            // 結果メッセージ表示
            if (showMessage)
            {
                SetDispResultMessage(DispData);
            }
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            _itemMgr.DispParams.Clear();

            // 入力チェック
            if (!CheckInputAll())
            {
                return false;
            }

            if (!string.IsNullOrEmpty(txtScanDate.Text))
            {
                _itemMgr.DispParams.ScanDate = DBConvert.ToIntNull(txtScanDate.Text.Replace(".", ""));
            }
            if (!string.IsNullOrEmpty(txtOcBrCd.Text))
            {
                _itemMgr.DispParams.OcBrCd = DBConvert.ToIntNull(txtOcBrCd.Text);
            }
            if (!string.IsNullOrEmpty(cmbTotalUmu.Text))
            {
                _itemMgr.DispParams.TotalUmu = DBConvert.ToIntNull(((ItemSet)cmbTotalUmu.SelectedItem).ItemValue);
            }
            if (!string.IsNullOrEmpty(cmbSaMaisu.Text))
            {
                _itemMgr.DispParams.SaMaisu = DBConvert.ToIntNull(((ItemSet)cmbSaMaisu.SelectedItem).ItemValue);
            }
            if (!string.IsNullOrEmpty(cmbSaKingaku.Text))
            {
                _itemMgr.DispParams.SaKingaku = DBConvert.ToIntNull(((ItemSet)cmbSaKingaku.SelectedItem).ItemValue);
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
        private void SearchBranchGoukeiForm_Load(object sender, EventArgs e)
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
        /// [バッチ一覧] SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 選択行のバッチ情報を取得する
                GetSelectedBatData(lvBatList, out TBL_BR_TOTAL brt);

                // イメージ描画
                MakeView(brt);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                //ファンクション状態更新
                SetFunctionState();
            }
        }

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            //KeyDownClearStatusMessage(e);

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (lvBatList.Focused)
                    {
                        if (this.btnFunc[12].Enabled)
                        {
                            btnFunc12_Click(sender, e);
                        }
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                        SendKeys.Send("{TAB}");
                    }
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
            if (_ctl.IsIniErr) { return; }

            if (ChangeFunction(e)) SetFunctionState(); return;
        }

        /// <summary>
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBatList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvBatList.Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// [バッチ一覧] マウスダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBatList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_ctl.IsIniErr) { return; }

            if (this.btnFunc[12].Enabled)
            {
                btnFunc12_Click(sender, e);
            }
        }

        /// <summary>
        /// フォーカスがあたったら背景を緑色にする
        /// </summary>
        private void txt_Enter(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }

            SetFocusBackColor((Control)sender);

            // ファンクション切り替え
            if (_dspMode != DispMode.入力項目)
            {
                _dspMode = DispMode.入力項目;
                InitializeFunction();
                SetFunctionState();
            }
        }

        /// <summary>
        /// ファンクションを切り替える
        /// </summary>
        private void combo_Enter(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }

            // ファンクション切り替え
            if (_dspMode != DispMode.入力項目)
            {
                _dspMode = DispMode.入力項目;
                InitializeFunction();
                SetFunctionState();
            }
        }

        /// <summary>
        /// フォーカスが外れたら背景を白色にする
        /// </summary>
        private void txt_Leave(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }

            RemoveFocusBackColor((Control)sender);
        }

        /// <summary>
        /// フォーカスがあたったら背景を緑色にする
        /// </summary>
        private void List_Enter(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }

            // ファンクション切り替え
            if (_dspMode != DispMode.結果一覧)
            {
                _dspMode = DispMode.結果一覧;
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
            e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F01：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);

                this.Close();
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
        /// F02：検索条件
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "検索条件", 1);
                if (_isInputMode)
                {
                    // 処理なし
                }
                else
                {
                    // 検索条件にフォーカス
                    SetTextFocus(txtScanDate);
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
                if (_isInputMode)
                {
                    // 処理なし
                }
                else
                {
                    if (_imgHandler.HasImage == false) { return; }

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "拡大", 1);

                    // 拡大
                    _imgHandler.SizeChangeImage(Const.IMAGE_ZOOM_IN, pnlImage.Width, pnlImage.Height);
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
        /// F06：縮小
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (_isInputMode)
                {
                    // 処理なし
                }
                else
                {
                    if (_imgHandler.HasImage == false) { return; }

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "縮小", 1);

                    // 縮小
                    _imgHandler.SizeChangeImage(Const.IMAGE_ZOOM_OUT, pnlImage.Width, pnlImage.Height);
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
        /// F12：検索／詳細
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                if (_isInputMode)
                {
                    // 検索
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "検索", 1);

                    // 画面項目取得
                    if (!GetDisplayParams())
                    {
                        return;
                    }

                    // 画面表示データ更新
                    RefreshDisplayData(true);
                }
                else
                {
                    // 詳細
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "詳細", 1);

                    // 詳細画面
                    if (!ExecuteBatchDetail())
                    {
                        return;
                    }

                    // 画面表示データ更新
                    RefreshDisplayData(false);
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
        /// プルダウン初期化
        /// </summary>
        /// <returns></returns>
        private void SetComboBox()
        {
            // 支店別合計票有無
            _totalumuList = new List<ItemSet>();
            _totalumuList.Add(new ItemSet("", ""));
            _totalumuList.Add(new ItemSet(SQLSearch.TOTAL_ARI.ToString(), "有"));
            _totalumuList.Add(new ItemSet(SQLSearch.TOTAL_NASHI.ToString(), "無"));
            cmbTotalUmu.DataSource = _totalumuList;
            cmbTotalUmu.DisplayMember = "ItemDisp";
            cmbTotalUmu.ValueMember = "ItemValue";

            // 枚数
            _maisuList = new List<ItemSet>();
            _maisuList.Add(new ItemSet("", ""));
            _maisuList.Add(new ItemSet(SQLSearch.DIFF_NASHI.ToString(), "差なし"));
            _maisuList.Add(new ItemSet(SQLSearch.DIFF_ARI.ToString(), "差あり"));
            cmbSaMaisu.DataSource = _maisuList;
            cmbSaMaisu.DisplayMember = "ItemDisp";
            cmbSaMaisu.ValueMember = "ItemValue";

            // 金額
            _kingakuList = new List<ItemSet>();
            _kingakuList.Add(new ItemSet("", ""));
            _kingakuList.Add(new ItemSet(SQLSearch.DIFF_NASHI.ToString(), "差なし"));
            _kingakuList.Add(new ItemSet(SQLSearch.DIFF_ARI.ToString(), "差あり"));
            cmbSaKingaku.DataSource = _kingakuList;
            cmbSaKingaku.DisplayMember = "ItemDisp";
            cmbSaKingaku.ValueMember = "ItemValue";
        }

        /// <summary>
        /// バッチ詳細画面を表示する
        /// </summary>
        private bool ExecuteBatchDetail()
        {
            _itemMgr.BrTotalInfo.Clear();

            // 選択行のバッチ情報を取得する
            if (lvBatList.SelectedIndices.Count < 1)
            {
                ComMessageMgr.MessageWarning(ComMessageMgr.W00002);
                return false;
            }

            // 引数取得
            string key = lvBatList.SelectedItems[0].SubItems[COL_KEY].Text;
            string[] keys = CommonUtil.DivideKeys(key);
            _itemMgr.BrTotalInfo.GymId = DBConvert.ToIntNull(keys[0]);
            _itemMgr.BrTotalInfo.OpeDate = DBConvert.ToIntNull(keys[1]);
            _itemMgr.BrTotalInfo.ImageNmae = DBConvert.ToStringNull(keys[2]);

            // 選択行キー設定
            _itemMgr.DispParams.Key = key;

            string filter = string.Format("GYM_ID={0} AND OPERATION_DATE={1} AND SCAN_IMG_FLNM='{2}'"
                , _itemMgr.BrTotalInfo.GymId
                , _itemMgr.BrTotalInfo.OpeDate
                , _itemMgr.BrTotalInfo.ImageNmae);
            DataRow[] rows = _itemMgr.tbl_brtotals.Select(filter);
            _itemMgr.BrTotalInfo.BrtRow = rows[0];
            _itemMgr.BrTotalInfo.brtotal = new TBL_BR_TOTAL(rows[0]);

            // バッチ詳細画面起動
            BranchGoukeiImgForm form = new BranchGoukeiImgForm();
            form.InitializeForm(_ctl);
            DialogResult res = form.ShowDialog();
            if (res != DialogResult.OK)
            {
                return false;
            }
            return true;
        }

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

            // 選択行を先頭表示
            int i = list.SelectedItems[0].Index;
            Rectangle rect = list.GetItemRect(0);
            i += (int)Math.Floor((double)list.ClientSize.Height / rect.Height) - 3;
            if (list.Items.Count - 1 < i)
            {
                i = list.Items.Count - 1;
            }
            list.Items[i].EnsureVisible();
        }

        /// <summary>
        /// 選択行のバッチデータを取得
        /// </summary>
        private void GetSelectedBatData(ListView list, out TBL_BR_TOTAL brt)
        {
            brt = new TBL_BR_TOTAL();

            // 選択行のバッチ情報を取得する
            if (list.SelectedIndices.Count < 1) { return; }

            // 引数取得
            string key = list.SelectedItems[0].SubItems[COL_KEY].Text;
            string[] keys = CommonUtil.DivideKeys(key);
            int gymid = DBConvert.ToIntNull(keys[0]);
            int opedate = DBConvert.ToIntNull(keys[1]);
            string fileName = DBConvert.ToStringNull(keys[2]);

            string filter = string.Format("GYM_ID={0} AND OPERATION_DATE={1} AND SCAN_IMG_FLNM='{2}'", gymid, opedate, fileName);
            DataRow[] rows = _itemMgr.tbl_brtotals.Select(filter);
            if (rows.Count() == 0)
            {
                //選択行が存在する場合は取得データがなくてもtrueを返す 
                return;
            }

            brt = new TBL_BR_TOTAL(rows[0]);
        }

        /// <summary>
        /// 合計票有無チェック
        /// </summary>
        private bool chkBrTotal(DataRow row)
        {
            return (DBConvert.ToIntNull(row["NOT_TOTAL"]) != 1);
        }

        /// <summary>
        /// 検索結果メッセージ表示
        /// </summary>
        private void SetDispResultMessage(List<DataRow> DispData)
        {
            // [検索結果]　件数：ZZZ,ZZ9件　枚数合計：ZZZ,ZZ9枚　金額合計：ZZZ,ZZZ,ZZZ,ZZZ,ZZZ,ZZZ,ZZ9円
            string msg = string.Format(ComMessageMgr.I00008, "検索結果", DispData.LongCount(),
                                                                         DispData.Sum(row => DBConvert.ToLongNull(row[TBL_BR_TOTAL.TOTAL_COUNT])),
                                                                         DispData.Sum(row => DBConvert.ToLongNull(row[TBL_BR_TOTAL.TOTAL_AMOUNT])));

            if (_itemMgr.tbl_brtotals.Rows.Count > AppConfig.ListDispLimit)
            {
                msg += string.Format(" (" + ComMessageMgr.W00003 + ")", AppConfig.ListDispLimit);
                this.SetStatusMessage(msg);
            }
            else
            {
                this.SetStatusMessage(msg, Color.Transparent);
            }
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
            imgparam.m_REDUCE_RATE = AppConfig.BatImageReduceRate1;
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
        /// 各種入力チェック
        /// </summary>
        private void root_I_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            this.ClearStatusMessage();

            if (((BaseTextBox)sender).Name == "txtScanDate")
            {
                if (!CheckScanDate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtOcBrCd")
            {
                if (!CheckOcBrCd())
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckScanDate()
        {
            string itemName = "スキャン日";
            string text = txtScanDate.Text;
            text = text.Replace(".", "");

            //空チェック
            if (string.IsNullOrEmpty(text))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, itemName));
                return false;
            }

            if (!Int32.TryParse(text, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, itemName));
                return false;
            }
            DateTime d1;
            string text2 = CommonUtil.ConvToDateFormat(text, 3);
            if (!DateTime.TryParse(text2, out d1))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, itemName));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckOcBrCd()
        {
            string itemName = "持出支店コード";
            string text = txtOcBrCd.ToString();
            if (string.IsNullOrEmpty(text)) { return true; }

            if (!Int32.TryParse(text, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, itemName));
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

        public class ItemSet
        {
            public string ItemValue { get; set; }
            public string ItemDisp { get; set; }
            public ItemSet(string value, string disp)
            {
                ItemValue = value;
                ItemDisp = disp;
            }

            //コンボボックス表示文字列
            public override string ToString()
            {
                return ItemDisp;
            }
        }

    }
}
