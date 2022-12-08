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

namespace SearchOcMeiView
{
	/// <summary>
	/// 検索結果一覧画面
	/// </summary>
    public partial class SearchOcMeiList : EntryCommonFormBase
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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchOcMeiList()
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
			base.SetDispName1("交換持出");
		}

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("持出明細照会");
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
                    SetFunctionName(2, "イメージ\n 印刷", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(3, "PDF出力", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(4, string.Empty);
                    SetFunctionName(5, string.Empty);
                    SetFunctionName(6, "検索条件", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(7, string.Empty);
                    SetFunctionName(8, "連続訂正", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(9, "ロック解除", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(10, string.Empty);
                    SetFunctionName(11, "明細\nイメージ一覧", true, Const.FONT_SIZE_FUNC_LOW);
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
            if (_itemMgr.DispParams.Rdate > -1)
            {
                dtRdate.setText(_itemMgr.DispParams.Rdate);
            }
            // バッチ番号
            if (_itemMgr.DispParams.BatNo > -1)
            {
                ntBatNo.setText(_itemMgr.DispParams.BatNo);
            }

            // コンボボックス設定
            //エントリー状態設定 
            SetEntStatus(cmbStatus, _itemMgr.DispParams.Status);
            //削除
            SetDELETE(cmbDelete, _itemMgr.DispParams.Delete);
            //アップロード状況
            SetIFStatus(cmbBUASTS, _itemMgr.DispParams.BUASts);
            //持出取消結果
            SetOCCancelIFStatus(cmbBCASTS, _itemMgr.DispParams.BCASts);
            //二重持出
            SetExists(cmbBUA, _itemMgr.DispParams.BUA);
            //持帰訂正
            SetExists(cmbGMB, _itemMgr.DispParams.GMB);
            //不渡返還結果
            SetExists(cmbGRASTS, _itemMgr.DispParams.GRASts);
            //決済後訂正
            SetExists(cmbGXA, _itemMgr.DispParams.GXA);
            //省略値
            SetDefExists(cmbDef, _itemMgr.DispParams.Def);
            //編集フラグ
            SetEditFlg(cmbEditFlg, _itemMgr.DispParams.EditFlg);
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
            if (_itemMgr.DispParams.BatNo > -1)
            {
                // バッチ番号が設定されている場合は初期表示で一覧表示
                DispMeiList(true);
            }
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            return GetDisplayParams(false);
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected bool GetDisplayParams(bool ListSelect)
        {
            //クリア
            _itemMgr.DispParams.Clear();

            // 入力データを取得する
            if (!string.IsNullOrEmpty(dtRdate.ToString()))
            {
                _itemMgr.DispParams.Rdate = dtRdate.getInt();
            }
            if (!string.IsNullOrEmpty(ntScanBrNo.ToString()))
            {
                _itemMgr.DispParams.ScanBrNo = ntScanBrNo.getInt();
            }
            if (!string.IsNullOrEmpty(dtScanDate.ToString()))
            {
                _itemMgr.DispParams.ScanDate = dtScanDate.getInt();
            }
            if (!string.IsNullOrEmpty(ntOCBRNoFrom.ToString()))
            {
                _itemMgr.DispParams.OCBRNoFrom = ntOCBRNoFrom.getInt();
            }
            if (!string.IsNullOrEmpty(ntOCBRNoTo.ToString()))
            {
                _itemMgr.DispParams.OCBRNoTo = ntOCBRNoTo.getInt();
            }
            if (!string.IsNullOrEmpty(ntBatNo.ToString()))
            {
                _itemMgr.DispParams.BatNo = ntBatNo.getInt();
            }
            if (!string.IsNullOrEmpty(ntICBKNo.ToString()))
            {
                _itemMgr.DispParams.ICBKNo = ntICBKNo.getInt();
            }
            if (!string.IsNullOrEmpty(dtClearingDataFrom.ToString()))
            {
                _itemMgr.DispParams.ClearingDataFrom = dtClearingDataFrom.getInt();
            }
            if (!string.IsNullOrEmpty(dtClearingDataTo.ToString()))
            {
                _itemMgr.DispParams.ClearingDataTo = dtClearingDataTo.getInt();
            }
            if (!string.IsNullOrEmpty(ntAmountFrom.ToString()))
            {
                _itemMgr.DispParams.AmountFrom = ntAmountFrom.getLong();
            }
            if (!string.IsNullOrEmpty(ntAmountTo.ToString()))
            {
                _itemMgr.DispParams.AmountTo = ntAmountTo.getLong();
            }
            _itemMgr.DispParams.Status = ((ComboBoxItemInt)cmbStatus.SelectedItem).ID;
            _itemMgr.DispParams.Delete = ((ComboBoxItemInt)cmbDelete.SelectedItem).ID;
            _itemMgr.DispParams.ntOpeNumer = txtOpeNumer.Text;
            _itemMgr.DispParams.Memo = txtMemo.Text;
            if (rdoMemoOpt1.Checked)
            {
                _itemMgr.DispParams.MemoOpt = 1;
            }
            else if (rdoMemoOpt2.Checked)
            {
                _itemMgr.DispParams.MemoOpt = 2;
            }
            else
            {
                _itemMgr.DispParams.MemoOpt = 3;
            }
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
            _itemMgr.DispParams.BUASts = ((ComboBoxItemInt)cmbBUASTS.SelectedItem).ID;
            _itemMgr.DispParams.BCASts = ((ComboBoxItemInt)cmbBCASTS.SelectedItem).ID;
            _itemMgr.DispParams.BUA = ((ComboBoxItemInt)cmbBUA.SelectedItem).ID;
            _itemMgr.DispParams.GMB = ((ComboBoxItemInt)cmbGMB.SelectedItem).ID;
            _itemMgr.DispParams.GRASts = ((ComboBoxItemInt)cmbGRASTS.SelectedItem).ID;
            _itemMgr.DispParams.GXA = ((ComboBoxItemInt)cmbGXA.SelectedItem).ID;
            _itemMgr.DispParams.Def = ((ComboBoxItemInt)cmbDef.SelectedItem).ID;
            _itemMgr.DispParams.EditFlg = ((ComboBoxItemInt)cmbEditFlg.SelectedItem).ID;

            if (ListSelect)
            {
                _itemMgr.DetailParams.Key = string.Empty;
                _itemMgr.DetailParams.TeiseiInptSts = -1;
                _itemMgr.DetailParams.DeleteFlg = -1;
                _itemMgr.DetailParams.EditFlg = -1;

                // 選択行の情報を取得する
                if (this.lvResultList.SelectedIndices.Count < 1)
                {
                    ComMessageMgr.MessageInformation("対象行が選択されていません。");
                    return false;
                }
                _itemMgr.DetailParams.Key = this.lvResultList.SelectedItems[0].SubItems[COL_KEY].Text;
                if (_itemMgr.MeiDetails.ContainsKey(_itemMgr.DetailParams.Key))
                {
                    _itemMgr.DetailParams.TeiseiInptSts = _itemMgr.MeiDetails[_itemMgr.DetailParams.Key].TeiseiInptSts;
                    _itemMgr.DetailParams.DeleteFlg = _itemMgr.MeiDetails[_itemMgr.DetailParams.Key].MeiDelete;
                    _itemMgr.DetailParams.EditFlg = _itemMgr.MeiDetails[_itemMgr.DetailParams.Key].EditFlg;
                }
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
            // バッチ番号
            if (_itemMgr.DispParams.BatNo > -1)
            {
                dtRdate.ReadOnly = true;
                ntBatNo.ReadOnly = true;
            }

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

            if (((BaseTextBox)sender).Name == "dtRdate")
            {
                if (!CheckRdate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntScanBrNo")
            {
                if (!CheckScanBrNo())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "dtScanDate")
            {
                if (!CheckScanDate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntOCBRNoFrom")
            {
                if (!CheckOCBRNoFrom())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntOCBRNoTo")
            {
                if (!CheckOCBRNoTo())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntBatNo")
            {
                if (!CheckBatNo())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntICNo")
            {
                if (!CheckICNo())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "dtClearingDataFrom")
            {
                if (!CheckClearingDataFrom())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "dtClearingDataTo")
            {
                if (!CheckClearingDataTo())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntAmountFrom")
            {
                if (!CheckAmountFrom())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntAmountTo")
            {
                if (!CheckAmountTo())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtOpeNumer")
            {
                // チェックなし
            }
            else if (((BaseTextBox)sender).Name == "txtMemo")
            {
                // チェックなし
            }
            else if (((BaseTextBox)sender).Name == "txtImgFLNm")
            {
                // チェックなし
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
            if (sender is TextBox && ((TextBox)sender).ReadOnly)
            {
                // テキストボックス & ReadOnlyの場合は色設定不要
            }
            else
            {
                SetFocusBackColor((Control)sender);
            }
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
            if (sender is TextBox && ((TextBox)sender).ReadOnly)
            {
                // テキストボックス & ReadOnlyの場合は色設定不要
            }
            else
            {
                RemoveFocusBackColor((Control)sender);
            }
        }

        /// <summary>
        /// コンボフォーカス設定
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
                if (ReferenceEquals(sender, cmbGRASTS))
                {
                    // 不渡りComboBox
                    ChgFuwatariFlg();
                }
                else if (ReferenceEquals(sender, cmbBCASTS))
                {
                    // 持出取消ComboBox
                    ChgTorikeshiFlg();
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
        /// F2：イメージ印刷
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ印刷", 1);
                if (_itemMgr.MeiDetails.Where(x => x.Value.MeiDelete == 0).LongCount() == 0)
                {
                    ComMessageMgr.MessageWarning("印刷するデータがありません");
                    return;
                }

                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "表示している明細を印刷しますが、よろしいですか？") == DialogResult.No)
                {
                    return;
                }

                //対象データを明細イメージリストに登録
                if (!_itemMgr.InsertWKImgList(_itemMgr.MeiDetails, this))
                {
                    return;
                }

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00003);

                    //印刷処理実施
                    string Argument = string.Format("{0} {1}", GymParam.GymId.持出, 1);
                    if (!_itemMgr.RunProcess("CTRMeiList.exe", Argument, this))
                    {
                        ComMessageMgr.MessageWarning("印刷処理に失敗しました");
                        return;
                    }
                }
                finally
                {
                    //メッセージ初期化
                    EndProcessing(CommonClass.ComMessageMgr.I00003);
                }

                ComMessageMgr.MessageInformation("印刷処理が終了しました");
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F3：PDF出力
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "PDF出力", 1);
                if (_itemMgr.MeiDetails.Where(x => x.Value.MeiDelete == 0).LongCount() == 0)
                {
                    ComMessageMgr.MessageWarning("PDF出力するデータがありません");
                    return;
                }

                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "表示している明細をPDF出力しますが、よろしいですか？") == DialogResult.No)
                {
                    return;
                }

                //対象データを明細イメージリストに登録
                if (!_itemMgr.InsertWKImgList(_itemMgr.MeiDetails, this))
                {
                    return;
                }

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00004);

                    //PDF印刷処理実施
                    string Argument = string.Format("{0} {1}", GymParam.GymId.持出, 2);
                    if (!_itemMgr.RunProcess("CTRMeiList.exe", Argument, this))
                    {
                        ComMessageMgr.MessageWarning("PDF出力処理に失敗しました");
                        return;
                    }
                }
                finally
                {
                    //メッセージ初期化
                    EndProcessing(CommonClass.ComMessageMgr.I00004);
                }

                ComMessageMgr.MessageInformation("PDF出力処理が終了しました");
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
        /// F6：検索条件
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "検索条件", 1);
                dtRdate.Focus();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F8：連続訂正
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                if (_itemMgr.MeiDetails.Where(x => x.Value.MeiDelete == 0).LongCount() == 0)
                {
                    ComMessageMgr.MessageWarning("連続訂正を行うデータがありません");
                    return;
                }

                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "表示している明細で連続訂正を行いますが、よろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "連続訂正", 1);

                //対象データを明細イメージリストに登録
                if (!_itemMgr.InsertWKImgList(_itemMgr.MeiDetails, this))
                {
                    return;
                }

                //連続訂正処理実施
                string Argument = string.Format("{0} {1} {2} {3}", _ctl.MenuNumber, 1, AppInfo.Setting.GymId, 5);
                if (!_itemMgr.RunProcess("CTRHoseiEntry.exe", Argument, this))
                {
                    ComMessageMgr.MessageWarning("補正エントリーの起動に失敗しました");
                }

                //画面再更新
                DispMeiList(true);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
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
                // データ取得
                if (!GetDisplayParams(true)) return;

                // 削除チェック
                if (_itemMgr.DetailParams.DeleteFlg == 1)
                {
                    //削除データ
                    ComMessageMgr.MessageWarning("選択明細はロック解除対象外です");
                    return;
                }

                List<string> chkType = new List<string>();
                string MsgKoukanjiri = "交換尻";
                string MsgEditFlg = "編集フラグ";

                //ステータスチェック
                if (_itemMgr.DetailParams.TeiseiInptSts == HoseiStatus.InputStatus.完了訂正中)
                {
                    //完了訂正中
                    chkType.Add(MsgKoukanjiri);
                }

                //編集フラグチェック
                if (_itemMgr.DetailParams.EditFlg == 1)
                {
                    //編集中
                    chkType.Add(MsgEditFlg);
                }

                if (chkType.Count() == 0)
                {
                    //対象外明細
                    ComMessageMgr.MessageWarning("選択明細はロック解除対象外です");
                    return;
                }

                //確認メッセージ表示
                string Msg = string.Format("{0}のロックを解除してもよろしいですか？", string.Join("・", chkType));
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, Msg) == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("{0}ロック解除", string.Join("・", chkType)), 1);

                // 更新処理実行
                if (chkType.Contains(MsgKoukanjiri))
                {
                    _itemMgr.UnLockKoukanJiri(this);
                }
                if (chkType.Contains(MsgEditFlg))
                {
                    _itemMgr.UnLockEditFlg(this);
                }

                // 一覧再表示
                DispResultList();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F11：明細イメージ一覧
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "明細イメージ一覧", 1);
                if (_itemMgr.MeiDetails.Where(x => x.Value.MeiDelete == 0).LongCount() == 0)
                {
                    ComMessageMgr.MessageWarning("一覧表示する明細データがありません");
                    return;
                }

                //対象データを明細イメージリストに登録
                if (!_itemMgr.InsertWKImgList(_itemMgr.MeiDetails, this))
                {
                    return;
                }
                //明細イメージ一覧実行
                string Argument = string.Format("{0} {1}", _ctl.MenuNumber, GymParam.GymId.持出);
                if (!_itemMgr.RunProcess("CTRImgList.exe", Argument, this))
                {
                    ComMessageMgr.MessageWarning("明細イメージ一覧の起動に失敗しました");
                }

                //画面再更新
                DispMeiList(false);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：検索/詳細
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
                    DispMeiList(true);
                }
                else
                {
                    // 詳細
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "詳細", 1);

                    if (!GetDisplayParams(true)) return;
                    // 明細画面表示
                    SearchOcMeiDetail form = new SearchOcMeiDetail();
                    form.InitializeForm(_ctl);
                    form.ResetForm();
                    form.ShowDialog();

                    // 現在取得済のデータで再描画
                    DispResultList();
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

        #region 一覧表示

        /// <summary>
        /// 一覧表示
        /// </summary>
        private void DispMeiList(bool showMessage)
        {
            if (!_itemMgr.FetchMeiList(_ctl.SettingData.ListDispLimit, out bool LimitOver, this))
            {
                return;
            }

            // 一覧設定
            DispResultList();

            // 結果メッセージ表示
            if (showMessage)
            {
                SetDispResultMessage(LimitOver);
            }
        }

        /// <summary>
        /// リストビューの一覧表示
        /// </summary>
        private void DispResultList()
        {
            int cnt = 0;
            int SelectItem = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[_itemMgr.MeiDetails.Count];
            foreach (string Key in _itemMgr.MeiDetails.Keys)
            {
                ItemManager.DetailData param = _itemMgr.MeiDetails[Key];
                listItem.Clear();

                // データ設定
                listItem.Add(Key);
                listItem.Add(OcMeiListCommon.DispDate(param.OpeDate, ""));
                listItem.Add(param.BatID.ToString("D6"));
                listItem.Add(OcMeiListCommon.DispDate(param.ClearingDate, ""));
                listItem.Add(_itemMgr.GeScanBranch(param.ScanBRNo));
                listItem.Add(OcMeiListCommon.DispDate(param.ScanDate, ""));
                listItem.Add(_itemMgr.GeBranch(param.OCBRNo));
                listItem.Add(param.DetailNo.ToString("D6"));
                listItem.Add(_itemMgr.GetBank(param.ICBKNo));
                listItem.Add(OcMeiListCommon.DispFormat(param.Amount, "#,##0"));
                listItem.Add(OcMeiListCommon.DispDeleteFlgFormat(param));
                listItem.Add(TrMei.Sts.GetName(param.ImgUploadSts));
                listItem.Add(TrMei.Sts.GetNameOCCancel(param.BCASts));
                listItem.Add(OcMeiListCommon.DispDate(param.BUADate, "警告なし"));
                listItem.Add(OcMeiListCommon.DispDate(param.GMADate, "訂正なし"));
                listItem.Add(OcMeiListCommon.DispDate(param.GRADate, "なし"));
                listItem.Add(OcMeiListCommon.DispDate(param.GXADate, "訂正なし"));
                listItem.Add(HoseiStatus.InputStatus.GetName(param.ICBKInptSts));
                listItem.Add(HoseiStatus.InputStatus.GetName(param.AmountInptSts));
                listItem.Add(HoseiStatus.InputStatus.GetName(param.TeiseiInptSts));
                listItem.Add(param.TeiseiTMNO);
                listItem.Add(OcMeiListCommon.DispOperatorFormat(param.ICBKOpeNo, _itemMgr));
                listItem.Add(OcMeiListCommon.DispOperatorFormat(param.AmountOpeNo, _itemMgr));

                listView[cnt] = new ListViewItem(listItem.ToArray());

                // 前選択状態の復元
                if (Key == _itemMgr.DetailParams.Key)
                {
                    SelectItem = cnt;
                }
                // 背景色設定
                if (param.MeiDelete == 1)
                {
                    // 削除データ
                    listView[cnt].BackColor = System.Drawing.Color.FromArgb(191,191,191);
                }

                cnt++;
            }

            this.lvResultList.BeginUpdate();
            this.lvResultList.Items.Clear();
            this.lvResultList.Items.AddRange(listView);
            this.lvResultList.Enabled = true;
            this.lvResultList.Refresh();
            this.lvResultList.Select();

            //列幅自動調整
            List<int> DefWidth = new List<int>();
            for (int i = 0; i < this.lvResultList.Columns.Count; i++)
            {
                DefWidth.Add(this.lvResultList.Columns[i].Width);
            }
            this.lvResultList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            for (int i = 0; i < this.lvResultList.Columns.Count; i++)
            {
                this.lvResultList.Columns[i].Width = Math.Max(this.lvResultList.Columns[i].Width, DefWidth[i]);
            }
            this.lvResultList.Columns[0].Width = 0;

            if (this.lvResultList.Items.Count > 0)
            {
                this.lvResultList.Items[SelectItem].Selected = true;
                this.lvResultList.Items[SelectItem].Focused = true;
                //初期表示位置調整
                SetListDefPositon(lvResultList);
            }

            this.lvResultList.EndUpdate();
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
        private void SetDispResultMessage(bool LimitOver)
        {
            // [検索結果]　件数：ZZZ,ZZ9件　金額合計：ZZZ,ZZZ,ZZZ,ZZZ,ZZZ,ZZZ,ZZ9円
            string msg = string.Format(ComMessageMgr.I00007, "検索結果", _itemMgr.MeiDetails.LongCount(),
                                                                         _itemMgr.MeiDetails.Sum(x => DBConvert.ToLongNull(x.Value.Amount)));

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

        #region コンボボックス設定

        /// <summary>
        /// エントリー状態設定
        /// </summary>
        private void SetEntStatus(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(1, "入力待"));
            combo.Items.Add(new ComboBoxItemInt(2, "入力中"));
            combo.Items.Add(new ComboBoxItemInt(10, "完了"));
            combo.Items.Add(new ComboBoxItemInt(20, "完了訂正"));

            // 初期設定
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 削除データ設定
        /// </summary>
        private void SetDELETE(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(9, "削除含む"));

            // 初期設定
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// IFステータス状況設定
        /// </summary>
        private void SetIFStatus(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(0, "未作成"));
            combo.Items.Add(new ComboBoxItemInt(1, "再作成対象"));
            combo.Items.Add(new ComboBoxItemInt(5, "ファイル作成"));
            combo.Items.Add(new ComboBoxItemInt(10, "アップロード"));
            combo.Items.Add(new ComboBoxItemInt(19, "結果エラー"));
            combo.Items.Add(new ComboBoxItemInt(20, "結果正常"));

            // 初期設定
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// IFステータス状況設定(持出取消)
        /// </summary>
        private void SetOCCancelIFStatus(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(0, "未作成"));
            combo.Items.Add(new ComboBoxItemInt(1, "作成対象"));
            combo.Items.Add(new ComboBoxItemInt(5, "ファイル作成"));
            combo.Items.Add(new ComboBoxItemInt(10, "アップロード"));
            combo.Items.Add(new ComboBoxItemInt(19, "結果エラー"));
            combo.Items.Add(new ComboBoxItemInt(20, "結果正常"));

            // 初期設定
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// ありなし設定
        /// </summary>
        private void SetExists(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(0, "なし"));
            combo.Items.Add(new ComboBoxItemInt(1, "あり"));

            // 初期設定
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 省略値ありなし設定
        /// </summary>
        private void SetDefExists(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(0, "省略値あり"));
            combo.Items.Add(new ComboBoxItemInt(1, "省略値なし"));

            // 初期設定
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 編集フラグ設定
        /// </summary>
        private void SetEditFlg(ComboBox combo, int Data)
        {
            combo.Items.Clear();
            combo.Items.Add(new ComboBoxItemInt(-1, ""));
            combo.Items.Add(new ComboBoxItemInt(1, "編集中"));

            // 初期設定
            combo.SelectedIndex = 0;
            int index = combo.Items.IndexOf(new ComboBoxItemInt(Data, ""));
            if (index >= 0)
            {
                combo.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 持出取消変更処理
        /// </summary>
        private void ChgTorikeshiFlg()
        {
            int index = -1;
            if (((ComboBoxItemInt)cmbBCASTS.SelectedItem).ID == 20)
            {
                // 持出取消結果 結果正常に変更した場合、削除データを削除含むに変更
                index = cmbDelete.Items.IndexOf(new ComboBoxItemInt(9, ""));
                if (index >= 0)
                {
                    cmbDelete.SelectedIndex = index;
                }
            }
        }

        /// <summary>
        /// 不渡入力変更処理
        /// </summary>
        private void ChgFuwatariFlg()
        {
            int index = -1;
            if (((ComboBoxItemInt)cmbGRASTS.SelectedItem).ID == 1)
            {
                // 不渡ありに変更した場合、削除データを削除含むに変更
                index = cmbDelete.Items.IndexOf(new ComboBoxItemInt(9, ""));
                if (index >= 0)
                {
                    cmbDelete.SelectedIndex = index;
                }
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
            rdoMemoOpt1.Checked = true;
            rdoImgOpt1.Checked = true;

            //先頭にフォーカス遷移
            this.AutoValidate = AutoValidate.Disable;
            dtRdate.Focus();
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
            if (!combo.Enabled || combo.Items.Count == 0 ) return;
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
        private bool CheckRdate()
        {
            string ChkText = dtRdate.ToString();
            int ChkIntText = dtRdate.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblRdate.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblRdate.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// スキャン支店入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckScanBrNo()
        {
            string ChkText = ntScanBrNo.ToString();
            int ChkIntText = ntScanBrNo.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblScanBrNo.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// スキャン日入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckScanDate()
        {
            string ChkText = dtScanDate.ToString();
            int ChkIntText = dtScanDate.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblScanDate.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblScanDate.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 持出支店コード入力チェック(開始)
        /// </summary>
        /// <returns></returns>
        private bool CheckOCBRNoFrom()
        {
            string ChkText = ntOCBRNoFrom.ToString();
            int ChkIntText = ntOCBRNoFrom.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblOCBRNo.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 持出支店コード入力チェック(終了)
        /// </summary>
        /// <returns></returns>
        private bool CheckOCBRNoTo()
        {
            string ChkText = ntOCBRNoTo.ToString();
            int ChkIntText = ntOCBRNoTo.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblOCBRNo.Text));
                return false;
            }

            //開始との比較チェエク
            if (ntOCBRNoTo.getInt() < ntOCBRNoFrom.getInt())
            {
                this.SetStatusMessage(string.Format("{0}(開始)以上を入力してください", lblOCBRNo.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// バッチ番号入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckBatNo()
        {
            string ChkText = ntBatNo.ToString();
            int ChkIntText = ntBatNo.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblBatNo.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 持帰銀行コード入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckICNo()
        {
            string ChkText = ntICBKNo.ToString();
            int ChkIntText = ntICBKNo.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblICNo.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 交換希望日From入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckClearingDataFrom()
        {
            string ChkText = dtClearingDataFrom.ToString();
            int ChkIntText = dtClearingDataFrom.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, string.Format("{0}(開始)", lblClearingData.Text)));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, string.Format("{0}(開始)", lblClearingData.Text)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 交換希望日To入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckClearingDataTo()
        {
            string ChkText = dtClearingDataTo.ToString();
            int ChkIntText = dtClearingDataTo.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, string.Format("{0}(終了)", lblClearingData.Text)));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, string.Format("{0}(終了)", lblClearingData.Text)));
                return false;
            }

            //開始との比較チェエク
            if (dtClearingDataTo.getInt() < dtClearingDataFrom.getInt())
            {
                this.SetStatusMessage(string.Format("{0}(開始)以上を入力してください", lblClearingData.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 金額From入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckAmountFrom()
        {
            string ChkText = ntAmountFrom.ToString();
            long ChkLongText = ntAmountFrom.getLong();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!long.TryParse(ChkText, out long i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, string.Format("{0}(開始)", lblAmount.Text)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 金額To入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckAmountTo()
        {
            string ChkText = ntAmountTo.ToString();
            long ChkLongText = ntAmountTo.getLong();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!long.TryParse(ChkText, out long i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, string.Format("{0}(終了)", lblAmount.Text)));
                return false;
            }

            //開始との比較チェエク
            if (ntAmountTo.getLong() < ntAmountFrom.getLong())
            {
                this.SetStatusMessage(string.Format("{0}(開始)以上を入力してください", lblAmount.Text));
                return false;
            }

            return true;
        }

        #endregion

    }
}
