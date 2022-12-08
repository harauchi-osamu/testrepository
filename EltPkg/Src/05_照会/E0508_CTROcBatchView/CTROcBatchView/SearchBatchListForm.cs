using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace CTROcBatchView
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchBatchListForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private List<ItemSet> _maisuList = null;
        private List<ItemSet> _kingakuList = null;
        private List<ItemSet> _statusList = null;

        private DispMode _dspMode = DispMode.入力項目;
        private bool _isInputMode { get { return (_dspMode == DispMode.入力項目); } }

        /// <summary>
        /// 表示件数
        /// </summary>
        public long DispCount
        {
            get
            {
                if (_itemMgr.tbl_trbatches == null) return 0;
                return Math.Min(_itemMgr.tbl_trbatches.Rows.Count, AppConfig.ListDispLimit);
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
        private const string EXE_NAME = "CTROcMeiView.exe";

        public enum DispMode
        {
            入力項目 = 1,
            結果一覧 = 2,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchBatchListForm()
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
            // 初期値設定
            _itemMgr.DispParams.ImportDate = AplInfo.OpDate();

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
            base.SetDispName2("持出バッチ照会");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // 通常状態
            if (_isInputMode)
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
                SetFunctionName(6, "検索条件", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(7, string.Empty);
                SetFunctionName(8, string.Empty);
                SetFunctionName(9, string.Empty);
                SetFunctionName(10, string.Empty);
                SetFunctionName(11, "明細一覧", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(12, "詳細");
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
            txtImportDate.setText(_itemMgr.DispParams.ImportDate);
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
            foreach (DataRow row in _itemMgr.tbl_trbatches.Rows)
            {
                if (DispCount <= cnt)
                {
                    //表示上限を越えた場合一覧表示終了
                    break;
                }

                TBL_TRBATCH bat = new TBL_TRBATCH(row, AppInfo.Setting.SchemaBankCD);
                string key = CommonUtil.GenerateKey(bat._GYM_ID, bat._OPERATION_DATE, bat._SCAN_TERM, bat._BAT_ID);

                string sImportDate = (bat._OPERATION_DATE == 0) ? "" : CommonUtil.ConvToDateFormat(bat._OPERATION_DATE, 3);
                string sBatId = bat._BAT_ID.ToString(Const.BAT_ID_LEN_STR);
                string sScanBrName = _ctl.GetScanBranchName(bat.m_SCAN_BR_NO);
                string sScanDate = (bat.m_SCAN_DATE == 0) ? "" : CommonUtil.ConvToDateFormat(bat.m_SCAN_DATE, 3);
                string sClearingDate = (bat.m_CLEARING_DATE == 0) ? "" : CommonUtil.ConvToDateFormat(bat.m_CLEARING_DATE, 3);
                string sOcBrName = _ctl.GetBranchName(bat.m_OC_BR_NO);
                string sBatCount = string.Format("{0:###,##0}", bat.m_TOTAL_COUNT);
                string sBatAmount = string.Format("{0:###,###,###,###,##0}", bat.m_TOTAL_AMOUNT);
                string sMeiCount = string.Format("{0:###,##0}", DBConvert.ToLongNull(row["MEI_COUNT"]));
                string sMeiAmount = string.Format("{0:###,###,###,###,##0}", DBConvert.ToLongNull(row["MEI_AMOUNT"]));
                string sDiffCount = string.Format("{0:###,##0}", DBConvert.ToLongNull(row["DIFF_COUNT"]));
                string sDiffAmount = string.Format("{0:###,###,###,###,##0}", DBConvert.ToLongNull(row["DIFF_AMOUNT"]));
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

                listItem.Clear();
                listItem.Add(key);              // 隠しキー
                listItem.Add(sImportDate);      // 取込日
                listItem.Add(sBatId);           // バッチ番号
                listItem.Add(sScanBrName);      // スキャン支店
                listItem.Add(sScanDate);        // スキャン日
                listItem.Add(sClearingDate);    // 交換希望日
                listItem.Add(sOcBrName);        // 持出支店
                listItem.Add(sBatCount);        // バッチ枚数
                listItem.Add(sBatAmount);       // バッチ金額
                listItem.Add(sMeiCount);        // 明細枚数
                listItem.Add(sMeiAmount);       // 明細金額
                listItem.Add(sDiffCount);       // 差枚数
                listItem.Add(sDiffAmount);      // 差金額
                listItem.Add(sStatus);          // 状態
                listItem.Add(sDiffRes);         // 照合
                listView[cnt] = new ListViewItem(listItem.ToArray());

                // 背景色設定
                if (nStatus != SQLSearch.BAT_STS_COMP)
                {
                    // 未補正データがあり
                    listView[cnt].BackColor = Color.Yellow;
                }
                if (nStatus == SQLSearch.BAT_STS_COMP && nDiffRes == SQLSearch.DIFF_RESULT_OK)
                {
                    // 未補正データがない & 照合OK
                    listView[cnt].BackColor = Color.LightBlue;
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

            if (!string.IsNullOrEmpty(txtImportDate.Text))
            {
                _itemMgr.DispParams.ImportDate = DBConvert.ToIntNull(txtImportDate.Text.Replace(".", ""));
            }
            if (!string.IsNullOrEmpty(txtScanBrCd.Text))
            {
                _itemMgr.DispParams.ScanBrCd = DBConvert.ToIntNull(txtScanBrCd.Text);
            }
            if (!string.IsNullOrEmpty(txtScanDate.Text))
            {
                _itemMgr.DispParams.ScanDate = DBConvert.ToIntNull(txtScanDate.Text.Replace(".", ""));
            }
            if (!string.IsNullOrEmpty(txtOcBrCd.Text))
            {
                _itemMgr.DispParams.OcBrCd = DBConvert.ToIntNull(txtOcBrCd.Text);
            }
            if (!string.IsNullOrEmpty(txtClearlingDate.Text))
            {
                _itemMgr.DispParams.ClearlingDate = DBConvert.ToIntNull(txtClearlingDate.Text.Replace(".", ""));
            }
            if (!string.IsNullOrEmpty(txtBatCnt.Text))
            {
                _itemMgr.DispParams.BatCnt = txtBatCnt.getInt();
            }
            if (!string.IsNullOrEmpty(txtBatKingaku.Text))
            {
                _itemMgr.DispParams.BatKingaku = txtBatKingaku.getLong();
            }
            if (!string.IsNullOrEmpty(cmbSaMaisu.Text))
            {
                _itemMgr.DispParams.SaMaisu = DBConvert.ToIntNull(((ItemSet)cmbSaMaisu.SelectedItem).ItemValue);
            }
            if (!string.IsNullOrEmpty(cmbSaKingaku.Text))
            {
                _itemMgr.DispParams.SaKingaku = DBConvert.ToIntNull(((ItemSet)cmbSaKingaku.SelectedItem).ItemValue);
            }
            if (!string.IsNullOrEmpty(cmbStatus.Text))
            {
                _itemMgr.DispParams.Status = DBConvert.ToIntNull(((ItemSet)cmbStatus.SelectedItem).ItemValue);
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
        private void SearchBatchListForm_Load(object sender, EventArgs e)
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
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
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
        /// [バッチ一覧] マウスダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBatList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnFunc12_Click(sender, e);
        }

        /// <summary>
        /// フォーカスがあたったら背景を緑色にする
        /// </summary>
        private void txt_Enter(object sender, EventArgs e)
        {
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
            RemoveFocusBackColor((Control)sender);
        }

        /// <summary>
        /// フォーカスがあたったら背景を緑色にする
        /// </summary>
        private void List_Enter(object sender, EventArgs e)
        {
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
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F06：検索条件
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
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
                    SetTextFocus(txtImportDate);
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
        /// F11：明細一覧
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                if (_isInputMode)
                {
                    // 処理なし
                }
                else
                {
                    // 明細一覧
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "明細一覧", 1);

                    // 持出明細照会画面起動
                    if (!ExecuteMeisaiList())
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

            // 状態
            _statusList = new List<ItemSet>();
            _statusList.Add(new ItemSet("", ""));
            _statusList.Add(new ItemSet(SQLSearch.BAT_STS_COMP.ToString(), "完了"));
            cmbStatus.DataSource = _statusList;
            cmbStatus.DisplayMember = "ItemDisp";
            cmbStatus.ValueMember = "ItemValue";
        }

        /// <summary>
        /// 持出明細照会画面を起動する
        /// </summary>
        /// <param name="mode"></param>
        private bool ExecuteMeisaiList()
        {
            // 選択行のバッチ情報を取得する
            if (lvBatList.SelectedIndices.Count < 1)
            {
                ComMessageMgr.MessageWarning(ComMessageMgr.W00002);
                return false;
            }

            // 引数取得
            string key = lvBatList.SelectedItems[0].SubItems[COL_KEY].Text;
            string[] keys = CommonUtil.DivideKeys(key);
            int opedate = DBConvert.ToIntNull(keys[1]);
            int bat_id = DBConvert.ToIntNull(keys[3]);
            string args = string.Format("{0} {1}_{2}", _ctl.MenuNumber, opedate.ToString("D8"), bat_id);

            // 選択行キー設定
            _itemMgr.DispParams.Key = key;

            // 持出明細照会画面起動
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("プログラム起動：{0} 引数：{1}", EXE_NAME, args), 3);
            string procPath = CommonUtil.ConcatPath(ServerIni.Setting.ExePath, EXE_NAME);
            string workDir = Path.GetDirectoryName(procPath);
            if (!File.Exists(procPath))
            {
                string msg = string.Format("持出明細照会プログラムが見つかりませんでした。[{0}]", procPath);
                ComMessageMgr.MessageError(msg);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), msg, 2);
                this.SetStatusMessage(msg);
                return false;
            }
            ProcessManager.RunProcess(procPath, workDir, args, true, false);
            return true;
        }

        /// <summary>
        /// バッチ詳細画面を表示する
        /// </summary>
        private bool ExecuteBatchDetail()
        {
            _itemMgr.BatchInfo.Clear();

            // 選択行のバッチ情報を取得する
            if (lvBatList.SelectedIndices.Count < 1)
            {
                ComMessageMgr.MessageWarning(ComMessageMgr.W00002);
                return false;
            }

            // 引数取得
            string key = lvBatList.SelectedItems[0].SubItems[COL_KEY].Text;
            string[] keys = CommonUtil.DivideKeys(key);
            _itemMgr.BatchInfo.GymId = DBConvert.ToIntNull(keys[0]);
            _itemMgr.BatchInfo.OpeDate = DBConvert.ToIntNull(keys[1]);
            _itemMgr.BatchInfo.ScanTerm = DBConvert.ToStringNull(keys[2]);
            _itemMgr.BatchInfo.BatId = DBConvert.ToIntNull(keys[3]);

            // 選択行キー設定
            _itemMgr.DispParams.Key = key;

            string filter = string.Format("GYM_ID={0} AND OPERATION_DATE={1} AND SCAN_TERM='{2}' AND BAT_ID={3}"
                , _itemMgr.BatchInfo.GymId
                , _itemMgr.BatchInfo.OpeDate
                , _itemMgr.BatchInfo.ScanTerm
                , _itemMgr.BatchInfo.BatId);
            DataRow[] rows = _itemMgr.tbl_trbatches.Select(filter);
            _itemMgr.BatchInfo.BatRow = rows[0];
            _itemMgr.BatchInfo.trbat = new TBL_TRBATCH(rows[0], AppInfo.Setting.SchemaBankCD);

            // バッチ詳細画面起動
            SeachImgForm form = new SeachImgForm();
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
        /// 検索結果メッセージ表示
        /// </summary>
        private void SetDispResultMessage(List<DataRow> DispData)
        {
            // [検索結果]　件数：ZZZ,ZZ9件　枚数合計：ZZZ,ZZ9枚　金額合計：ZZZ,ZZZ,ZZZ,ZZZ,ZZZ,ZZZ,ZZ9円
            string msg = string.Format(ComMessageMgr.I00008, "検索結果", DispData.LongCount(),
                                                                         DispData.Sum(row => DBConvert.ToLongNull(row[TBL_TRBATCH.TOTAL_COUNT])),
                                                                         DispData.Sum(row => DBConvert.ToLongNull(row[TBL_TRBATCH.TOTAL_AMOUNT])));

            if (_itemMgr.tbl_trbatches.Rows.Count > AppConfig.ListDispLimit)
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
        // 内部メソッド（入力チェック）
        // *******************************************************************

        #region 条件クリア処理

        /// <summary>
        /// 全入力項目クリア
        /// </summary>
        private void ClearInputAll()
        {
            // TextBox・ComboBoxクリア
            ClearInputBox();

            //先頭にフォーカス遷移
            this.AutoValidate = AutoValidate.Disable;
            txtImportDate.Focus();
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
        /// 各種入力チェック
        /// </summary>
        private void root_I_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            this.ClearStatusMessage();

            if (((BaseTextBox)sender).Name == "txtImportDate")
            {
                if (!CheckImportDate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtScanBrCd")
            {
                if (!CheckScanBrCd())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtScanDate")
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
            else if (((BaseTextBox)sender).Name == "txtClearlingDate")
            {
                if (!CheckClearlingDate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtBatCnt")
            {
                if (!CheckBatCnt())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtBatKingaku")
            {
                if (!CheckBatKingaku())
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckImportDate()
        {
            string itemName = "取込日";
            string text = txtImportDate.Text;
            System.Diagnostics.Debug.WriteLine(text);
            text = text.Replace(".", "");
            if (string.IsNullOrEmpty(text)) { return true; }

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
        private bool CheckScanBrCd()
        {
            string itemName = "スキャン支店";
            string text = txtScanBrCd.ToString();
            if (string.IsNullOrEmpty(text)) { return true; }

            if (!Int32.TryParse(text, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, itemName));
                return false;
            }
            return true;
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
            if (string.IsNullOrEmpty(text)) { return true; }

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
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckClearlingDate()
        {
            string itemName = "交換希望日";
            string text = txtClearlingDate.Text;
            text = text.Replace(".", "");
            if (string.IsNullOrEmpty(text)) { return true; }

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
        private bool CheckBatCnt()
        {
            string itemName = "バッチ枚数";
            string text = txtBatCnt.ToString();
            if (string.IsNullOrEmpty(text)) { return true; }

            if (!Int32.TryParse(text, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, itemName));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckBatKingaku()
        {
            string itemName = "バッチ金額";
            string text = txtBatKingaku.ToString();
            if (string.IsNullOrEmpty(text)) { return true; }

            if (!Int64.TryParse(text, out long i))
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

        #endregion

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
