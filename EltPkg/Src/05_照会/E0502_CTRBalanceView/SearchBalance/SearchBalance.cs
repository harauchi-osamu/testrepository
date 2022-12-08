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

namespace SearchBalance
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchBalance : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        #region クラス定数

        private const int BANKCODE = 1;

        #endregion

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
        public SearchBalance()
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
            base.SetDispName1("決済業務");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("交換尻照会");
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
                SetFunctionName(12, string.Empty);
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
            if (_ctl.SearchType ==  Controller.enumSearchMode.Balance)
            {
                // 参加銀行用
                lblTytle.Text = "交換尻情報（参加銀行用）";
                this.lvDataList.Columns[BANKCODE].Text = "銀行コード";
            }
            else
            {
                // 決済受託銀行用
                lblTytle.Text = "交換尻情報（決済受託銀行用）";
                this.lvDataList.Columns[BANKCODE].Text = "決済委託銀行";
            }
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
            SearchBalanceCommon.UpdatelInfo(lblBKNO, lblOpDate);

            string FileID = string.Empty;
            if (_ctl.SearchType == Controller.enumSearchMode.Balance)
            {
                // 参加銀行用
                FileID = "IF209";
            }
            else
            {
                // 決済受託銀行用
                FileID = "IF210";
            }

            if (!_itemMgr.FetchBalanceText(FileID, this))
            {
                return;
            }

            int cnt = 0;
            ListViewItem[] listView = new ListViewItem[_itemMgr.Balance.Count];
            foreach (string Key in _itemMgr.Balance.Keys.OrderBy(x => x))
            {
                TBL_BALANCETXT param = _itemMgr.Balance[Key];

                List<string> item = SearchBalanceCommon.GetDispValueData(_itemMgr, param);
                item.Insert(0, Key);

                listView[cnt] = new ListViewItem(item.ToArray());
                cnt++;
            }

            this.lvDataList.Items.Clear();
            this.lvDataList.Items.AddRange(listView);
            this.lvDataList.Enabled = true;
            this.lvDataList.Refresh();
            this.lvDataList.Select();

            if (this.lvDataList.Items.Count > 0)
            {
                this.lvDataList.Items[0].Selected = true;
                this.lvDataList.Items[0].Focused = true;
            }

            // 結果メッセージ表示
            if (showMessage)
            {
                SetDispResultMessage();
            }
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
        /// 設定ファイル読み込みでエラーメッセージ表示
        /// </summary>
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
                // 再表示
                ResetForm();
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
            // [結果]　件数：ZZZ,ZZ9件
            string msg = string.Format(ComMessageMgr.I00006, "結果", _itemMgr.Balance.LongCount());
            this.SetStatusMessage(msg, Color.Transparent);
        }

    }
}
