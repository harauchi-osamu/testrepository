using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;
using System.Linq;
using System.Drawing;

namespace SearchResultText
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchResultList : EntryCommonFormBase
    {

        #region クラス変数
        
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private DispMode _curFlg = DispMode.Input;

        #endregion

        #region enum

        public enum DispMode
        {
            ///<summary>入力項目</summary>
            Input = 1,
            ///<summary>結果一覧</summary>
            List = 2,
        }

        #endregion

        #region クラス定数

        private const int FILE_NAME = 2;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchResultList()
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
                    SetFunctionName(1, "終了");
                    SetFunctionName(2, string.Empty);
                    SetFunctionName(3, string.Empty);
                    SetFunctionName(4, string.Empty);
                    SetFunctionName(5, string.Empty);
                    SetFunctionName(6, string.Empty);
                    SetFunctionName(7, string.Empty);
                    SetFunctionName(8, string.Empty);
                    SetFunctionName(9, string.Empty);
                    SetFunctionName(10, "一覧");
                    SetFunctionName(11, string.Empty);
                    SetFunctionName(12, "絞り込み", true, Const.FONT_SIZE_FUNC_LOW);
                }
                else
                {
                    SetFunctionName(1, "終了");
                    SetFunctionName(2, string.Empty);
                    SetFunctionName(3, string.Empty);
                    SetFunctionName(4, string.Empty);
                    SetFunctionName(5, "最新表示", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(6, "絞り込み\n  条件", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(7, string.Empty);
                    SetFunctionName(8, string.Empty);
                    SetFunctionName(9, "再送登録", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(10, string.Empty);
                    SetFunctionName(11, string.Empty);
                    SetFunctionName(12, "結果内容", true, Const.FONT_SIZE_FUNC_LOW);
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
            // 取込日
            if (_itemMgr.ResultDispParams.RecvDate > 0)
            {
                dtRecvDate.setText(_itemMgr.ResultDispParams.RecvDate);
            }

            // コンボボックス設定
            SetFileDivid();
            SetErrFlg();
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
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            //クリア
            _itemMgr.ResultDispParams.Clear();

            // 入力データを取得する
            _itemMgr.ResultDispParams.FileDivid = ((ComboBoxItemString)cmbFILE_DIVID.SelectedItem).ID;
            _itemMgr.ResultDispParams.ErrFlg = ((ComboBoxItemInt)cmbErrFlg.SelectedItem).ID;
            if (!string.IsNullOrEmpty(dtRecvDate.ToString()))
            {
                _itemMgr.ResultDispParams.RecvDate = dtRecvDate.getInt();
            }

            // 選択行の情報を取得する
            if (this.lvResultList.SelectedIndices.Count < 1)
            {
                _itemMgr.RecordListDispParams.SEQ = 0;
                _itemMgr.RecordListDispParams.ListErrFlg = -1;
                return true;
            }
            _itemMgr.RecordListDispParams.SEQ = 0;
            _itemMgr.RecordListDispParams.ListErrFlg = -1;
            _itemMgr.ResultDispParams.FileName = this.lvResultList.SelectedItems[0].SubItems[FILE_NAME].Text;

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

            //一覧表示
            DispResultText(true);

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

            if (((BaseTextBox)sender).Name == "dtRecvDate")
            {
                if (!CheckRecvDate())
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

                //一覧表示
                DispResultText(true);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F6：絞り込み条件
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "絞り込み条件", 1);

                cmbFILE_DIVID.Focus();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F9：再送登録
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "再送登録", 1);

                // 選択行の情報を取得する
                if (this.lvResultList.SelectedIndices.Count < 1)
                {
                    ComMessageMgr.MessageInformation("対象行が選択されていません。");
                    return;
                }

                // データ取得
                GetDisplayParams();

                // 対象の管理情報を取得
                TBL_RESULTTXT_CTL TxtCtl = _itemMgr.GetResultTextControl(_itemMgr.ResultDispParams.FileName);

                if (!ChkResend(TxtCtl))
                {
                    ComMessageMgr.MessageWarning("対象データは再送不可の結果ファイルです。");
                    return;
                }

                //確認メッセージ表示
                if ((ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "配下の証券を再送登録します。よろしいですか？") == DialogResult.No))
                {
                    return;
                }

                bool flg = false;
                string procfilename = TxtCtl.m_PROC_FILE_NAME.Trim(); // 後ろスペース削除
                switch (TxtCtl.m_FILE_DIVID)
                {
                    case "BUB":
                        // 持出アップロード
                        flg = ReSendFileBUB(procfilename);
                        break;
                    case "BCA":
                        // 持出取消
                        flg = ReSendFileBCA(procfilename);
                        break;
                    case "GMA":
                        // 証券データ訂正
                        flg = ReSendFileGMA(procfilename);
                        break;
                    case "GRA":
                        // 不渡返還
                        flg = ReSendFileGRA(procfilename);
                        break;
                    default:
                        ComMessageMgr.MessageInformation("再送登録対象外データです。");
                        break;
                }
                if (flg)
                {
                    ComMessageMgr.MessageInformation("再送登録を行いました。");
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
        /// F10：一覧
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "一覧", 1);

                // フォーカス設定
                SetFocusValidation(lvResultList);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：絞り込み
        ///      結果内容
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

                    //入力内容チェック
                    if (!CheckInputAll()) return;

                    // データ取得
                    GetDisplayParams();

                    //一覧表示
                    DispResultText(true);
                }
                else
                {
                    // 結果内容
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "結果内容", 1);

                    // 選択行の情報を取得する
                    if (this.lvResultList.SelectedIndices.Count < 1)
                    {
                        ComMessageMgr.MessageInformation("対象行が選択されていません。");
                        return;
                    }
                    GetDisplayParams();

                    // レコード一覧画面表示
                    SearchResultRecordList form = new SearchResultRecordList();
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
        /// 結果内容表示
        /// </summary>
        private void DispResultText(bool showMessage)
        {
            if (!_itemMgr.FetchResultTextControl(_ctl.SettingData.ListDispLimit, out bool LimitOver)) 
            {
                return;
            }

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[_itemMgr.Result_Ctl.Count];
            foreach (string Key in _itemMgr.Result_Ctl.Keys)
            {
                TBL_RESULTTXT_CTL param = _itemMgr.Result_Ctl[Key];

                listItem = SearchResultCommon.GetSearchListData(_itemMgr, Key, param);
                listView[cnt] = new ListViewItem(listItem.ToArray());
                cnt++;
            }
            this.lvResultList.Items.Clear();
            this.lvResultList.Items.AddRange(listView);
            this.lvResultList.Enabled = true;
            this.lvResultList.Refresh();
            this.lvResultList.Select();

            if (this.lvResultList.Items.Count > 0)
            {
                this.lvResultList.Items[0].Selected = true;
                this.lvResultList.Items[0].Focused = true;
            }

            // 結果メッセージ表示
            if (showMessage)
            {
                SetDispResultMessage(LimitOver);
            }
        }

        /// <summary>
        /// 検索結果メッセージ表示
        /// </summary>
        private void SetDispResultMessage(bool LimitOver)
        {
            // [絞込結果]　件数：ZZZ,ZZ9件
            string msg = string.Format(ComMessageMgr.I00006, "絞込結果", _itemMgr.Result_Ctl.LongCount());

            if (LimitOver)
            {
                msg += string.Format(" (" + ComMessageMgr.W00003 + ")", _ctl.SettingData.ListDispLimit);
                this.SetStatusMessage(msg);
            }
            else
            {
                this.SetStatusMessage(msg, Color.Transparent);
            }
        }

        #region 再送登録関連

        /// <summary>
        /// 再送登録可否チェック
        /// </summary>
        private bool ChkResend(TBL_RESULTTXT_CTL TxtCtl)
        {
            List<string> ChkCode = new List<string>();
            switch (TxtCtl.m_FILE_DIVID)
            {
                case "BUB":
                    // 持出アップロード
                    ChkCode.Add("M1012000-I");
                    break;
                case "BCA":
                    // 持出取消
                    ChkCode.Add("M1052000-I");
                    break;
                case "GMA":
                    // 証券データ訂正
                    ChkCode.Add("M2042000-I");
                    break;
                case "GRA":
                    // 不渡返還
                    ChkCode.Add("M2052000-I");
                    break;
                default:
                    return false;
            }
            // コードチェック
            if (ChkCode.Contains(TxtCtl.m_FILE_CHK_CODE))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 結果ファイル単位での再送登録(BUB)
        /// </summary>
        private bool ReSendFileBUB(string procfilename)
        {
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果ファイル再送登録(BUB)：{0}", procfilename), 1);

                // 更新処理
                if (!_itemMgr.UpdateBUBFile(procfilename, out int CommitCount, this))
                {
                    ComMessageMgr.MessageError("更新処理に失敗しました。");
                    return false;
                }

                if (CommitCount == 0)
                {
                    ComMessageMgr.MessageInformation("更新データがありませんでした。");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 結果ファイル単位での再送登録(BCA)
        /// </summary>
        private bool ReSendFileBCA(string procfilename)
        {
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果ファイル再送登録(BCA)：{0}", procfilename), 1);

                // 更新処理
                if (!_itemMgr.UpdateBCAFile(procfilename, out int CommitCount, this))
                {
                    ComMessageMgr.MessageError("更新処理に失敗しました。");
                    return false;
                }

                if (CommitCount == 0)
                {
                    ComMessageMgr.MessageInformation("更新データがありませんでした。");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 結果ファイル単位での再送登録(GMA)
        /// </summary>
        private bool ReSendFileGMA(string procfilename)
        {
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果ファイル再送登録(GMA)：{0}", procfilename), 1);

                // 更新処理
                if (!_itemMgr.UpdateGMAFile(procfilename, out int CommitCount, this))
                {
                    ComMessageMgr.MessageError("更新処理に失敗しました。");
                    return false;
                }

                if (CommitCount == 0)
                {
                    ComMessageMgr.MessageInformation("更新データがありませんでした。");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 結果ファイル単位での再送登録(GRA)
        /// </summary>
        private bool ReSendFileGRA(string procfilename)
        {
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果ファイル再送登録(GRA)：{0}", procfilename), 1);

                // 更新処理
                if (!_itemMgr.UpdateGRAFile(procfilename, out int CommitCount, this))
                {
                    ComMessageMgr.MessageError("更新処理に失敗しました。");
                    return false;
                }

                if (CommitCount == 0)
                {
                    ComMessageMgr.MessageInformation("更新データがありませんでした。");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
        }

        #endregion

        #region コンボボックス設定

        /// <summary>
        /// ファイル識別区分設定
        /// </summary>
        private void SetFileDivid()
        {
            cmbFILE_DIVID.Items.Clear(); 
            cmbFILE_DIVID.Items.Add(new ComboBoxItemString("", ""));
            foreach (TBL_FILE_PARAM param in _itemMgr.FileParamMF)
            {
                cmbFILE_DIVID.Items.Add(new ComboBoxItemString(param._FILE_DIVID, param.m_FILE_NAME));
            }

            // 識別区分初期設定
            int index = cmbFILE_DIVID.Items.IndexOf(new ComboBoxItemString(_itemMgr.ResultDispParams.FileDivid, ""));
            if (index >= 0)
            {
                cmbFILE_DIVID.SelectedIndex = index;
            }
        }

        /// <summary>
        /// エラー設定
        /// </summary>
        private void SetErrFlg()
        {
            cmbErrFlg.Items.Clear();
            cmbErrFlg.Items.Add(new ComboBoxItemInt(-1, ""));
            cmbErrFlg.Items.Add(new ComboBoxItemInt(0, "エラーなし"));
            cmbErrFlg.Items.Add(new ComboBoxItemInt(9, "エラーあり"));
            cmbErrFlg.SelectedIndex = 0;
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
        /// 取込日入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckRecvDate()
        {
            string ChkText = dtRecvDate.ToString();
            int ChkIntText = dtRecvDate.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblRecvDate.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblRecvDate.Text));
                return false;
            }

            return true;
        }

        #endregion

    }
}

