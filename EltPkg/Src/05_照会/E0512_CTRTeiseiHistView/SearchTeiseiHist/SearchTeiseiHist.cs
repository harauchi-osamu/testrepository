using System;
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
using System.Collections;
using System.Data;

namespace SearchTeiseiHist
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchTeiseiHist : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private DispMode _curFlg = DispMode.Input;
        private int RadioButtonSelect = 0;

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
        public SearchTeiseiHist()
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
            switch (AppInfo.Setting.GymId)
            {
                case GymParam.GymId.持出:
                    base.SetDispName2("持出訂正履歴照会");
                    break;
                case GymParam.GymId.持帰:
                    base.SetDispName2("持帰訂正履歴照会");
                    break;
            }
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
                    SetFunctionName(6, "検索条件", true, Const.FONT_SIZE_FUNC_LOW);
                    SetFunctionName(7, string.Empty);
                    SetFunctionName(8, string.Empty);
                    SetFunctionName(9, string.Empty);
                    SetFunctionName(10, string.Empty);
                    SetFunctionName(11, string.Empty);
                    SetFunctionName(12, string.Empty);
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
            //処理日初期表示
            dtOperatedate.Text = AplInfo.OpDate().ToString();
            _itemMgr.DispParams.operationdate = dtOperatedate.getInt();
            //項目名取得
            if (!_itemMgr.FetchItemName(this))
            {
                return;
            }
            cmbITEM_NAME.Items.Add("");
            foreach (DataRow row in _itemMgr.ListData)
            {
                string ItemName = DBConvert.ToStringNull(row["ITEM_NAME"]);
                cmbITEM_NAME.Items.Add(ItemName);
            }
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
            // 一覧表示処理を実装
          
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            //クリア
            _itemMgr.DispParams.Clear();

            //ラジオボタン選択判断
            if (rbfront.Checked)
            {
                RadioButtonSelect = 0;
            }
            if (rbback.Checked)
            {
                RadioButtonSelect = 1;
            }
            if (rbsame.Checked)
            {
                RadioButtonSelect = 2;
            }
            //入力値の取得
            if (!string.IsNullOrEmpty(dtOperatedate.ToString()))
            {
                _itemMgr.DispParams.operationdate = dtOperatedate.getInt();
            }
            if (!string.IsNullOrEmpty(ntBAT_ID.ToString()))
            {
                _itemMgr.DispParams.batid = ntBAT_ID.getInt();
            }
            if (!string.IsNullOrEmpty(ntDETAILS_NO.ToString()))
            {
                _itemMgr.DispParams.detailsno = ntDETAILS_NO.getInt();
            }
            if (!string.IsNullOrEmpty(dtUPDATE_DATE.ToString()))
            {
                _itemMgr.DispParams.updatedate = dtUPDATE_DATE.getInt();
            }
            if (!string.IsNullOrEmpty(dtUPDATE_TIME.ToString()))
            {
                _itemMgr.DispParams.updatetime = dtUPDATE_TIME.getInt();
            }
            if (!string.IsNullOrEmpty(dtUPDATE_TIME2.ToString()))
            {
                _itemMgr.DispParams.updatetime2 = dtUPDATE_TIME2.getInt();
            }
            _itemMgr.DispParams.itemname = cmbITEM_NAME.Text;
            _itemMgr.DispParams.itemvalue = txtITEM_NAME.Text;
            _itemMgr.DispParams.imgflnm = txtIMG_FLNM.Text;
            _itemMgr.DispParams.radioselect = RadioButtonSelect;
            SetDisplayParams();
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

            if (((BaseTextBox)sender).Name == "dtOperatedate")
            {
                if (!CheckOperatedate())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntBAT_ID")
            {
                if (!CheckBAT_ID())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "ntDETAILS_NO")
            {
                if (!CheckDETAILS_NO())
                {
                    e.Cancel = true;
                }
            } 
            else if (((BaseTextBox)sender).Name == "dtUPDATE_DATE")
            {
                if (!CheckUPDATE_DATE())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "dtUPDATE_TIME")
            {
                if (!CheckFromUPDATE_TIME())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "dtUPDATE_TIME2")
            {
                if (!CheckToUPDATE_TIME())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtIMG_FLNM")
            {
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
        /// フォーカス（検索結果リスト）
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
        /// フォーカス（項目名称）
        /// </summary>
        private void cmbITEM_NAME_Enter(object sender, EventArgs e)
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
        /// フォーカスが外れたら背景を白色にする
        /// </summary>
        private void txt_Leave(object sender, EventArgs e)
        {
            RemoveFocusBackColor((Control)sender);
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
                dtOperatedate.Focus();
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
                DispTeiseiList(true);
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

        #region 一覧表示

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
        /// 更新区分データ整形
        /// </summary>
        private string DispDataUPDATEKBN(int Kbn)
        {
            string DispValue = string.Empty;
            switch (Kbn)
            {
                case 1:
                    DispValue = "1：新規登録";
                    break;
                case 2:
                    DispValue = "2：アップデート";
                    break;
                default:
                    DispValue = Kbn.ToString();
                    break;
            }

            return DispValue;
        }

        /// <summary>
        /// 一覧表示
        /// </summary>
        private void DispTeiseiList(bool showMessage)
        {          
            if (!_itemMgr.FetchListData(_ctl.SettingData.ListDispLimit, out bool LimitOver, this))
            {
                return;
            }

            int cnt = 0;
            int CurrentColorItem = 0;
            int PreColorKey = 0;
            Dictionary<int, int> ListColor = GetListColor();
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[_itemMgr.tritem_hist.Count];
            foreach (string Key in _itemMgr.tritem_hist.Keys)
            {
                TBL_TRITEM_HIST param = _itemMgr.tritem_hist[Key];

                string key = Key;
                string GYM_ID = param._GYM_ID.ToString("D3");
                string OPERATION_DATE = CommonUtil.ConvToDateFormat(param._OPERATION_DATE.ToString(), 3);
                string SCAN_TERM = param._SCAN_TERM.ToString();
                string BAT_ID = param._BAT_ID.ToString("D6");
                string DETAILS_NO = param._DETAILS_NO.ToString("D6");
                string ITEM_ID = param._ITEM_ID.ToString("D3");
                string SEQ = param._SEQ.ToString("D3");
                string ITEM_NAME = param.m_ITEM_NAME.ToString();
                string OCR_ENT_DATA = param.m_OCR_ENT_DATA.ToString();
                string OCR_VFY_DATA = param.m_OCR_VFY_DATA.ToString();
                string ENT_DATA = param.m_ENT_DATA.ToString();
                string VFY_DATA = param.m_VFY_DATA.ToString();
                string END_DATA = param.m_END_DATA.ToString();
                string BUA_DATA = param.m_BUA_DATA.ToString();
                string CTR_DATA = param.m_CTR_DATA.ToString();
                string ICTEISEI_DATA = param.m_ICTEISEI_DATA.ToString();
                string MRC_CHG_BEFDATA = param.m_MRC_CHG_BEFDATA.ToString();
                string E_TERM = param.m_E_TERM.ToString();
                string E_OPENO = param.m_E_OPENO.ToString();
                string E_STIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_E_STIME, ""));
                string E_ETIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_E_ETIME, ""));
                string E_YMD = CommonUtil.ConvToDateFormat(DispDataFormat(param.m_E_YMD, ""), 3);
                string E_TIME = CommonUtil.ConvSecondToMiliTimeFormat(DispDataFormat(param.m_E_TIME, ""));
                string V_TERM = param.m_V_TERM.ToString();
                string V_OPENO = param.m_V_OPENO.ToString();
                string V_STIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_V_STIME, ""));
                string V_ETIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_V_ETIME, ""));
                string V_YMD = CommonUtil.ConvToDateFormat(DispDataFormat(param.m_V_YMD, ""), 3);
                string V_TIME = CommonUtil.ConvSecondToMiliTimeFormat(DispDataFormat(param.m_V_TIME, ""));
                string C_TERM = param.m_C_TERM.ToString();
                string C_OPENO = param.m_C_OPENO.ToString();
                string C_STIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_C_STIME, ""));
                string C_ETIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_C_ETIME, ""));
                string C_YMD = CommonUtil.ConvToDateFormat(DispDataFormat(param.m_C_YMD, ""), 3);
                string C_TIME = CommonUtil.ConvSecondToMiliTimeFormat(DispDataFormat(param.m_C_TIME, ""));
                string O_TERM = param.m_O_TERM.ToString();
                string O_OPENO = param.m_O_OPENO.ToString();
                string O_STIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_O_STIME, ""));
                string O_ETIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_O_ETIME, ""));
                string O_YMD = CommonUtil.ConvToDateFormat(DispDataFormat(param.m_O_YMD, ""), 3);
                string O_TIME = CommonUtil.ConvSecondToMiliTimeFormat(DispDataFormat(param.m_O_TIME, ""));
                string ITEM_TOP = param.m_ITEM_TOP.ToString("#0.0");
                string ITEM_LEFT = param.m_ITEM_LEFT.ToString("#0.0");
                string ITEM_WIDTH = param.m_ITEM_WIDTH.ToString("#0.0");
                string ITEM_HEIGHT = param.m_ITEM_HEIGHT.ToString("#0.0");
                string UPDATE_DATE = CommonUtil.ConvToDateFormat(DispDataFormat(param.m_UPDATE_DATE, ""), 3);
                string UPDATE_TIME = CommonUtil.ConvToMiliTimeFormat(DispDataFormat(param.m_UPDATE_TIME, ""));
                string UPDATE_KBN = DispDataUPDATEKBN(param.m_UPDATE_KBN);
                string FIX_TRIGGER = param.m_FIX_TRIGGER.ToString();

                listItem.Clear();
                listItem.Add(Key);
                listItem.Add(GYM_ID);
                listItem.Add(OPERATION_DATE);
                listItem.Add(SCAN_TERM);
                listItem.Add(BAT_ID);
                listItem.Add(DETAILS_NO);
                listItem.Add(ITEM_ID);
                listItem.Add(SEQ);
                listItem.Add(ITEM_NAME);
                listItem.Add(OCR_ENT_DATA);
                listItem.Add(OCR_VFY_DATA);
                listItem.Add(ENT_DATA);
                listItem.Add(VFY_DATA);
                listItem.Add(END_DATA);
                listItem.Add(BUA_DATA);
                listItem.Add(CTR_DATA);
                listItem.Add(ICTEISEI_DATA);
                listItem.Add(MRC_CHG_BEFDATA);
                listItem.Add(E_TERM);
                listItem.Add(E_OPENO);
                listItem.Add(E_STIME);
                listItem.Add(E_ETIME);
                listItem.Add(E_YMD);
                listItem.Add(E_TIME);
                listItem.Add(V_TERM);
                listItem.Add(V_OPENO);
                listItem.Add(V_STIME);
                listItem.Add(V_ETIME);
                listItem.Add(V_YMD);
                listItem.Add(V_TIME);
                listItem.Add(C_TERM);
                listItem.Add(C_OPENO);
                listItem.Add(C_STIME);
                listItem.Add(C_ETIME);
                listItem.Add(C_YMD);
                listItem.Add(C_TIME);
                listItem.Add(O_TERM);
                listItem.Add(O_OPENO);
                listItem.Add(O_STIME);
                listItem.Add(O_ETIME);
                listItem.Add(O_YMD);
                listItem.Add(O_TIME);
                listItem.Add(ITEM_TOP);
                listItem.Add(ITEM_LEFT);
                listItem.Add(ITEM_WIDTH);
                listItem.Add(ITEM_HEIGHT);
                listItem.Add(UPDATE_DATE);
                listItem.Add(UPDATE_TIME);
                listItem.Add(UPDATE_KBN);
                listItem.Add(FIX_TRIGGER);
                listView[cnt] = new ListViewItem(listItem.ToArray());

                //色設定
                if (cnt == 0)
                {
                    PreColorKey = param._ITEM_ID;
                }
                if (PreColorKey != param._ITEM_ID)
                {
                    CurrentColorItem++;
                    CurrentColorItem %= 2;
                    PreColorKey = param._ITEM_ID;
                }
                if (ListColor.ContainsKey(CurrentColorItem) && ListColor[CurrentColorItem] > 0)
                {
                    listView[cnt].UseItemStyleForSubItems = true;
                    listView[cnt].BackColor = Color.FromArgb(ListColor[CurrentColorItem]);
                }

                cnt++;
            }

            this.lvTeiseiList.BeginUpdate();
            this.lvTeiseiList.Items.Clear();
            this.lvTeiseiList.Items.AddRange(listView);
            this.lvTeiseiList.Enabled = true;          
            this.lvTeiseiList.Refresh();
            this.lvTeiseiList.Select();

            //列幅自動調整
            List<int> HdWidth = new List<int>();
            this.lvTeiseiList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            for (int i = 0; i < this.lvTeiseiList.Columns.Count; i++)
            {
                HdWidth.Add(this.lvTeiseiList.Columns[i].Width);
            }
            this.lvTeiseiList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            for (int i = 0; i < this.lvTeiseiList.Columns.Count; i++)
            {
                this.lvTeiseiList.Columns[i].Width = Math.Max(this.lvTeiseiList.Columns[i].Width, HdWidth[i]);
            }
            this.lvTeiseiList.Columns[0].Width = 0;
            this.lvTeiseiList.EndUpdate();

            if (this.lvTeiseiList.Items.Count > 0)
            {
                this.lvTeiseiList.Items[0].Selected = true;
                this.lvTeiseiList.Items[0].Focused = true;
            }

            // 結果メッセージ表示
            if (showMessage)
            {
                SetDispResultMessage(LimitOver);
            }
        }

        /// <summary>
        /// 色一覧取得
        /// </summary>
        private Dictionary<int, int> GetListColor()
        {
            Dictionary<int, int> ColorList = new Dictionary<int, int>();

            foreach (string Data in this._ctl.SettingData.TeiseiListBackColor.Split(','))
            {
                if (Data.StartsWith("0x"))
                {
                    string RemoveData = Data.Remove(0, 2);
                    if (int.TryParse(RemoveData, System.Globalization.NumberStyles.HexNumber, new System.Globalization.CultureInfo("ja-JP"), out int Color))
                    {
                        ColorList.Add(ColorList.Count(), Color);
                    }
                    else
                    {
                        //「コード:色」デフォルト(白)
                        ColorList.Add(ColorList.Count(), -1);
                    }
                }
                else
                {
                    //「コード:色」デフォルト(白)
                    ColorList.Add(ColorList.Count(), -1);
                }
            }

            return ColorList;
        }

        /// <summary>
        /// 検索結果メッセージ表示
        /// </summary>
        private void SetDispResultMessage(bool LimitOver)
        {
            // [検索結果]　件数：ZZZ,ZZ9件
            string msg = string.Format(ComMessageMgr.I00006, "検索結果", _itemMgr.tritem_hist.LongCount());

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

        #region 条件クリア処理

        /// <summary>
        /// 全入力項目クリア
        /// </summary>
        private void ClearInputAll()
        {
            // TextBox・ComboBoxクリア
            ClearInputBox();
            // ラジオボタンクリア
            rbfront.Checked = true;

            //先頭にフォーカス遷移
            this.AutoValidate = AutoValidate.Disable;
            dtOperatedate.Focus();
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
        /// 処理日入力チェック
        /// </summary>
        private bool CheckOperatedate()
        {
            string ChkText = dtOperatedate.ToString();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblOperatedate.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblOperatedate.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// バッチ番号入力チェック
        /// </summary>
        private bool CheckBAT_ID()
        {
            string ChkText = ntBAT_ID.ToString();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblBAT_ID.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 明細番号力チェック
        /// </summary>
        private bool CheckDETAILS_NO()
        {
            string ChkText = ntDETAILS_NO.ToString();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblDETAILS_NO.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目名称チェック
        /// </summary>
        private bool CheckITEM_NAME()
        {
            string ChkText = cmbITEM_NAME.ToString();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }       
            return true;
        }

        /// <summary>
        /// 項目値チェック
        /// </summary>
        private bool ChecktxtITEM_NAME()
        {
            string ChkText = txtITEM_NAME.Text;

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }
            return true;
        }

        /// <summary>
        /// 更新日入力チェック
        /// </summary>
        private bool CheckUPDATE_DATE()
        {
            string ChkText = dtUPDATE_DATE.ToString();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblUPDATE_DATE.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblUPDATE_DATE.Text));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新時刻From入力チェック
        /// </summary>
        private bool CheckFromUPDATE_TIME()
        {
            string ChkText = dtUPDATE_TIME.ToString();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, string.Format("{0}(開始)", lblUPDATE_TIME.Text)));
                return false;
            }

            //時間チェック
            
            // 時分秒補完
            ChkText = GetTimeFormat(ChkText, false);
            // 時分秒チェック
            if (!CheckTime(ChkText))
            {
                this.SetStatusMessage("更新時刻は有効な時刻ではありません。");
                return false;
            }

            // 時間チェックOKの場合補完した内容を設定
            dtUPDATE_TIME.Text = ChkText;

            return true;
        }

        /// <summary>
        /// 更新時刻To入力チェック
        /// </summary>
        private bool CheckToUPDATE_TIME()
        {
            string ChkText = dtUPDATE_TIME2.ToString();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                // 空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, string.Format("{0}(終了)", lblUPDATE_TIME.Text)));
                return false;
            }

            //時間チェック

            // 時分秒補完
            ChkText = GetTimeFormat(ChkText, true);
            // 時分秒チェック
            if (!CheckTime(ChkText))
            {
                this.SetStatusMessage("更新時刻は有効な時刻ではありません。");
                return false;
            }

            // 時間チェックOKの場合補完した内容を設定
            dtUPDATE_TIME2.Text = ChkText;

            //開始時間との比較チェエク
            if (dtUPDATE_TIME2.getInt() < dtUPDATE_TIME.getInt())
            {
                this.SetStatusMessage("更新時刻（開始）以上を入力してください");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 時分秒チェック
        /// </summary>
        private bool CheckTime(string Time)
        {
            if (!uint.TryParse(Time, out uint intTime))
            {
                return false;
            }

            uint Hour = Convert.ToUInt32(Math.Floor((decimal)(intTime / 10000)));
            uint Min = Convert.ToUInt32(Math.Floor((decimal)((intTime - (Hour * 10000)) / 100)));
            uint Sec = intTime % 100;

            //有効範囲チェック
            if (Sec >= 60 || Min >= 60 || Hour >= 24)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 時分秒補完
        /// </summary>
        private string GetTimeFormat(string Time, bool isToTime)
        {
            string rtnValue = Time;
            switch (Time.Length)
            {
                case 1:
                case 2:
                    // 1・2桁入力は時のみ入力済と判断
                    // 分秒を追加
                    rtnValue += (isToTime ? "5959" : "0000");
                    break;
                case 3:
                case 4:
                    // 3・4桁入力は時分のみ入力済と判断
                    // 秒を追加
                    rtnValue += (isToTime ? "59" : "00");
                    break;
                case 0:
                    // 0桁入力時
                    rtnValue = (isToTime ? "235959" : "000000");
                    break;
                default:
                    // 5桁以上入力は時分秒のみ入力済と判断
                    // そのまま
                    break;
            }

            // 前ゼロ6桁に補完して返す
            return CommonUtil.PadLeft(rtnValue, 6, "0");
        }

        #endregion
    }
}
