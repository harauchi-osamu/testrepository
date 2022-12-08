using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace SearchResultText
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchResultRecordList : EntryCommonFormBase
    {
        public object RESULTTXT_CTL { get; set; }

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

        private const int KEY = 0;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchResultRecordList()
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
            //対象ファイル情報設定
            _itemMgr.RecordListDispParams.TargetFileName = _itemMgr.ResultDispParams.FileName;
            _itemMgr.RecordListDispParams.TargetFileDicid = _itemMgr.GetResultTextControl().m_FILE_DIVID;
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
            base.SetDispName1("業務共通");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("結果照会");
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
                    SetFunctionName(1, "戻る");
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
                    SetFunctionName(12, "絞り込み", true, Const.FONT_SIZE_FUNC_LOW);
                }
                else
                {
                    SetFunctionName(1, "戻る");
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
                    SetFunctionName(12, "詳細");
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
            // コンボボックス設定
            SetErrFlg(cmbErrFlg, _itemMgr.RecordListDispParams.ListErrFlg);
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
            // ファイル情報欄
            SearchResultCommon.UpdatepnlInfo(_itemMgr, pnlInfo);

            //一覧更新
            if (!_itemMgr.FetchResultTextList(this))
            {
                return;
            }

            int cnt = 0;
            ListViewItem[] listView = new ListViewItem[_itemMgr.ResultTxtList.Count];
            foreach (string Key in _itemMgr.ResultTxtList.Keys)
            {
                TBL_RESULTTXT param = _itemMgr.ResultTxtList[Key];

                List<string> item = SearchResultCommon.GetDispValueData(_itemMgr, param);
                item.Insert(0, Key);

                listView[cnt] = new ListViewItem(item.ToArray());
                cnt++;
            }
            this.lvDataList.BeginUpdate();
            this.lvDataList.Items.Clear();
            this.lvDataList.Items.AddRange(listView);
            this.lvDataList.Enabled = true;
            this.lvDataList.Refresh();
            this.lvDataList.Select();

            //列幅自動調整
            List<int> HdWidth = new List<int>();
            this.lvDataList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            for (int i =0; i < this.lvDataList.Columns.Count; i++)
            {
                HdWidth.Add(this.lvDataList.Columns[i].Width);
            }
            this.lvDataList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            for (int i = 0; i < this.lvDataList.Columns.Count; i++)
            {
                this.lvDataList.Columns[i].Width = Math.Max(this.lvDataList.Columns[i].Width, HdWidth[i]);
            }
            this.lvDataList.Columns[0].Width = 0;

            if (this.lvDataList.Items.Count > 0)
            {
                this.lvDataList.Items[0].Selected = true;
                this.lvDataList.Items[0].Focused = true;
            }
            this.lvDataList.EndUpdate();

            // 結果メッセージ表示
            SetDispResultMessage();
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 入力データを取得する
            _itemMgr.RecordListDispParams.ListErrFlg = ((ComboBoxItemInt)cmbErrFlg.SelectedItem).ID;

            // 選択行の情報を取得する
            if (this.lvDataList.SelectedIndices.Count < 1)
            {
                _itemMgr.RecordListDispParams.SEQ = 0;
                return true;
            }
            _itemMgr.RecordListDispParams.SEQ = int.Parse(this.lvDataList.SelectedItems[0].SubItems[KEY].Text);

            return true;
        }

        // *******************************************************************
        // イベント
        // *******************************************************************

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
                        if (this.btnFunc[12].Enabled)
                        {
                            this.btnFunc12_Click(null, null);
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
        /// F12：絞り込み
        ///      詳細
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
                    // 絞り込み
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "絞り込み", 1);

                    // データ取得
                    GetDisplayParams();

                    // 画面更新
                    SetDisplayParams();
                }
                else
                {
                    // 詳細
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "詳細", 1);

                    // 選択行の情報を取得する
                    if (this.lvDataList.SelectedIndices.Count < 1)
                    {
                        ComMessageMgr.MessageInformation("対象行が選択されていません。");
                        return;
                    }
                    GetDisplayParams();

                    // レコード一覧画面表示
                    SearchResultDetail form = new SearchResultDetail();
                    form.InitializeForm(_ctl);
                    form.ResetForm();
                    form.ShowDialog();
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
        /// 検索結果メッセージ表示
        /// </summary>
        private void SetDispResultMessage()
        {
            // [絞込結果]　件数：ZZZ,ZZ9件
            string msg = string.Format(ComMessageMgr.I00006, "絞込結果", _itemMgr.ResultTxtList.LongCount());
            this.SetStatusMessage(msg, Color.Transparent);
        }

        #region コンボボックス設定

        /// <summary>
        /// エラー設定
        /// </summary>
        private void SetErrFlg(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(0, "エラーなし"));
            combo.Items.Add(new ComboBoxItemInt(9, "エラーあり"));

            // 初期設定
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
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

    }
}

