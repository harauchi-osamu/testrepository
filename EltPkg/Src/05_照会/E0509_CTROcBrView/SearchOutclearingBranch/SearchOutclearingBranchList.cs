﻿using System;
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

namespace SearchOutclearingBranch
{
	/// <summary>
	/// 検索結果一覧画面
	/// </summary>
    public partial class SearchOutclearingBranchList : EntryCommonFormBase
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
        public SearchOutclearingBranchList()
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
            base.SetDispName2("持出店別照会");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
		{
            if (IsNotPressCSAKey)
            {
                // 通常状態
                if (_curFlg == DispMode.Input)
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
                    SetFunctionName(F10_, "一覧");
                    SetFunctionName(F11_, string.Empty);
                    SetFunctionName(F12_, "検索");
                }
                else
                {
                    SetFunctionName(F1_, "終了");
                    SetFunctionName(F2_, string.Empty);
                    SetFunctionName(F3_, string.Empty);
                    SetFunctionName(F4_, string.Empty);
                    SetFunctionName(F5_, string.Empty);
                    SetFunctionName(F6_, "検索条件", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(F7_, string.Empty);
                    SetFunctionName(F8_, string.Empty);
                    SetFunctionName(F9_, string.Empty);
                    SetFunctionName(F10_, string.Empty);
                    SetFunctionName(F11_, string.Empty);
                    SetFunctionName(F12_, string.Empty);
                }
            }
            else
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
            dtRdate.Text = AplInfo.OpDate().ToString();
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

            // 入力データを取得する
            if (!string.IsNullOrEmpty(dtRdate.ToString()))
            {
                _itemMgr.DispParams.InputDate = dtRdate.getInt();
            }
            if (!string.IsNullOrEmpty(dtClearingDate.ToString()))
            {
                _itemMgr.DispParams.ClearingDate = dtClearingDate.getInt();
            }
            if (!string.IsNullOrEmpty(dtOC_BR_NO.ToString()))
            {
                _itemMgr.DispParams.BrCode = dtOC_BR_NO.getInt();
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
            if (((BaseTextBox)sender).Name == "dtRdate")
            {
                if (!CheckRdate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "dtClearingDate")
            {
                if (!CheckClearingDate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "dtOC_BR_NO")
            {
                if (!CheckOC_BR_NO())
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
        /// 一覧選択
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

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

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
        /// F10：一覧
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "一覧", 1);

                // フォーカス設定
                SetFocusValidation(lvListList);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：検索
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "検索", 1);
                //入力内容チェック
                if (!CheckInputAll()) return;
                // データ取得
                GetDisplayParams();

                //一覧表示
                DispListText(true);
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
            }
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 一覧表示
        /// </summary>
        private void DispListText(bool showMessage)
        {
            if (!_itemMgr.FetchListData(_ctl.SettingData.ListDispLimit, out bool LimitOver, this))
            {
                return;
            }
            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[_itemMgr.ListData.Count];
            foreach (DataRow row in _itemMgr.ListData)
            {
                string OPERATION_DATE = DispDate(DBConvert.ToIntNull(row["OPERATION_DATE"]), "");
                string OC_BR_NO = DBConvert.ToIntNull(row["OC_BR_NO"]).ToString("D4");
                string CLEARING_DATE = DispDate(DBConvert.ToIntNull(row["CLEARING_DATE"]), "");
                string MEICOUNT = DispDataFormat(DBConvert.ToIntNull(row["MEICOUNT"]).ToString(), "#,##0");
                string AMT = DispDataFormat(DBConvert.ToLongNull(row["AMT"]).ToString(), "#,##0");
                string Key = OPERATION_DATE + "_" + OC_BR_NO + "_" + CLEARING_DATE;

                listItem.Clear();
                listItem.Add(Key);
                listItem.Add(OPERATION_DATE);
                listItem.Add(OC_BR_NO);
                listItem.Add(CLEARING_DATE);
                listItem.Add(MEICOUNT);
                listItem.Add(AMT);
                listView[cnt] = new ListViewItem(listItem.ToArray());
                cnt++;
            }

            this.lvListList.Items.Clear();
            this.lvListList.Items.AddRange(listView);
            this.lvListList.Enabled = true;
            this.lvListList.Refresh();
            this.lvListList.Select();
            this.lvListList.Focus();

            if (this.lvListList.Items.Count > 0)
            {
                this.lvListList.Items[0].Selected = true;
                this.lvListList.Items[0].Focused = true;
            }

            // 結果メッセージ表示
            if (showMessage)
            {
                SetDispResultMessage(LimitOver);
            }
        }

        /// <summary>
        /// 画面表示データ整形
        /// </summary>
        public static string DispDataFormat(string Data, string Format)
        {
            if (!long.TryParse(Data, out long ChgData))
            {
                return Data;
            }

            return ChgData.ToString(Format);
        }

        /// <summary>
        /// 日付画面表示データ整形
        /// </summary>
        public static string DispDate(int Date, string DefValue)
        {
            if (Date > 0)
            {
                return CommonUtil.ConvToDateFormat(Date, 3);
            }
            return DefValue;
        }

        /// <summary>
        /// 検索結果メッセージ表示
        /// </summary>
        private void SetDispResultMessage(bool LimitOver)
        {
            // [検索結果]　件数：ZZZ,ZZ9件　枚数合計：ZZZ,ZZ9枚　金額合計：ZZZ,ZZZ,ZZZ,ZZZ,ZZZ,ZZZ,ZZ9円
            string msg = string.Format(ComMessageMgr.I00008, "検索結果", _itemMgr.ListData.LongCount(),
                                                                         _itemMgr.ListData.Sum(row => DBConvert.ToLongNull(row["MEICOUNT"])),
                                                                         _itemMgr.ListData.Sum(row => DBConvert.ToLongNull(row["AMT"])));

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
        private bool CheckRdate()
        {
            string ChkText = dtRdate.ToString();

            //空チェックは交換希望日で実施

            if (string.IsNullOrEmpty(ChkText))
            {
                // 対象項目が空の場合は以下のチェック不要
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
        /// 交換希望日入力チェック
        /// </summary>
        private bool CheckClearingDate()
        {
            string ChkText = dtClearingDate.ToString();
            string ChkRdateText = dtRdate.ToString();

            //空チェック(取込日か交換希望日どちらかが入力してあればOK)
            if (string.IsNullOrEmpty(ChkText) && string.IsNullOrEmpty(ChkRdateText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, lblRdate.Text + "または" + lblClearingDate.Text));
                return false;
            }

            if (string.IsNullOrEmpty(ChkText))
            {
                // 対象項目が空の場合は以下のチェック不要
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblClearingDate.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblClearingDate.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 持出支店コード入力チェック
        /// </summary>
        private bool CheckOC_BR_NO()
        {
            string ChkText = dtOC_BR_NO.ToString();

            if (string.IsNullOrEmpty(ChkText))
            {
                // 対象項目が空の場合は以下のチェック不要
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblOC_BR_NO.Text));
                return false;
            }

            return true;
        }

        #endregion

    }
}
