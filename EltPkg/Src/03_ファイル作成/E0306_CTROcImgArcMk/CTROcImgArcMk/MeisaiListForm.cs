using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace CTROcImgArcMk
{
	/// <summary>
	/// 証券イメージアーカイブ作成画面
	/// </summary>
    public partial class MeisaiListForm : EntryCommonFormBase
	{
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private bool _isComplete = false;
        private bool _isSearch = false;

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

        public enum GymType
        {
            未指定,
            交換持出,
            期日管理
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MeisaiListForm()
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
            base.SetDispName2("証券イメージアーカイブ作成");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
		{
            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, string.Empty);
            SetFunctionName(F3_, string.Empty);
            SetFunctionName(F4_, string.Empty);
            SetFunctionName(F5_, "最新表示", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F6_, "絞込");
            SetFunctionName(F7_, "絞込条件", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F8_, string.Empty);
            SetFunctionName(F9_, string.Empty);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "実行");
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
		{
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            //画面更新・絞込・絞込条件は実行済の場合不可
            SetFunctionState(F5_, !_isComplete);
            SetFunctionState(F6_, !_isComplete);
            SetFunctionState(F7_, !_isComplete);

            //実行は未検索 OR 実行済の場合不可
            SetFunctionState(F12_, !(!_isSearch || _isComplete));

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
            txtClearinte.Text = "";
            _itemMgr.DispParams.Clear();
            _itemMgr.DispParams.InputRoute = GymType.未指定;
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("実行モード 行内交換={0}", ServerIni.Setting.InternalExchange), 1);
        }

        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        protected override void RefreshDisplayData()
		{
            RefreshDisplayData(false);
        }

        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        private void RefreshDisplayData(bool Flg)
        {
            // 画面項目設定
            SetDisplayParams(Flg);
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
            SetDisplayParams(false);
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        private void SetDisplayParams(bool Flg)
        {
            if (_ctl.IsIniErr) { return; }

            // 初期表示時は表示処理なし
            if (!Flg) { return; }

            // 予定件数表示は行内交換実施有無に関わらず全件表示する

            // データ取得
            if (!_itemMgr.FetchTrMei(ItemManager.ArchiveType.全データ, this))
            {
                return;
            }

            // 予定件数集計
            SortedDictionary<int, ItemManager.CreateData> dataList = _itemMgr.CalcCreateDate(_itemMgr.MeisaiList1);

            // 予定件数表示
            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[dataList.Count];
            foreach (ItemManager.CreateData data in dataList.Values)
            {
                string key = data.ClearingDate.ToString();
                string clearingDate = CommonUtil.ConvToDateFormat(data.ClearingDate, 3);
                string meiCnt = string.Format("{0:###,##0}", data.MeisaiCount);
                string imgCnt = string.Format("{0:###,##0}", data.ImageCount);

                listItem.Clear();
                listItem.Add(key);              // 隠しキー
                listItem.Add(clearingDate);     // 交換希望日
                listItem.Add(meiCnt);           // 明細数
                listItem.Add(imgCnt);           // イメージファイル数
                listView[cnt] = new ListViewItem(listItem.ToArray());
                cnt++;

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数 交換希望日={0}、明細={1}件、イメージ={2}件", data.ClearingDate, meiCnt, imgCnt), 1);
            }
            this.lvBatList.Items.Clear();
            this.lvBatList.Items.AddRange(listView);
            this.lvBatList.Enabled = true;
            this.lvBatList.Refresh();

            // 予定：合計
            _itemMgr.DispParams.YoteiMeiCnt = dataList.Values.Sum(p => p.MeisaiCount);
            _itemMgr.DispParams.YoteiImgCnt = dataList.Values.Sum(p => p.ImageCount);
            lblYoteiMeiCnt.Text = string.Format("{0:###,##0}", _itemMgr.DispParams.YoteiMeiCnt);
            lblYoteiImgCnt.Text = string.Format("{0:###,##0}", _itemMgr.DispParams.YoteiImgCnt);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数 明細合計={0}件、イメージ合計={1}件", _itemMgr.DispParams.YoteiMeiCnt, _itemMgr.DispParams.YoteiImgCnt), 1);

            // 結果：電子交換所
            lblResEltMeiCnt.Text = string.Format("{0:###,##0}", 0);
            lblResEltImgCnt.Text = string.Format("{0:###,##0}", 0);
            // 結果：行内交換
            lblResInnerMeiCnt.Text = string.Format("{0:###,##0}", 0);
            lblResInnerImgCnt.Text = string.Format("{0:###,##0}", 0);
            // 結果：合計
            lblResTotalMeiCnt.Text = string.Format("{0:###,##0}", 0);
            lblResTotalImgCnt.Text = string.Format("{0:###,##0}", 0);

            // 予定明細件数があれば検索済設定
            _isSearch = _itemMgr.DispParams.YoteiMeiCnt > 0;
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        private void SetDisplayParamsResult()
        {
            // 結果件数集計（電子交換所）
            SortedDictionary<int, ItemManager.CreateData> dataList1 = _itemMgr.CalcCreateDate(_itemMgr.MeisaiList1);
            // 結果件数集計（自行）
            SortedDictionary<int, ItemManager.CreateData> dataList2 = _itemMgr.CalcCreateDate(_itemMgr.MeisaiList2);

            // 結果：電子交換所
            int eltMeiCnt = dataList1.Values.Sum(p => p.MeisaiCount);
            int eltImgCnt = dataList1.Values.Sum(p => p.ImageCount);
            lblResEltMeiCnt.Text = string.Format("{0:###,##0}", eltMeiCnt);
            lblResEltImgCnt.Text = string.Format("{0:###,##0}", eltImgCnt);
            // 結果：行内交換
            int ownMeiCnt = dataList2.Values.Sum(p => p.MeisaiCount);
            int ownImgCnt = dataList2.Values.Sum(p => p.ImageCount);
            lblResInnerMeiCnt.Text = string.Format("{0:###,##0}", ownMeiCnt);
            lblResInnerImgCnt.Text = string.Format("{0:###,##0}", ownImgCnt);

            // 結果：合計
            int meiTotal = eltMeiCnt + ownMeiCnt;
            int imgTotal = eltImgCnt + ownImgCnt;
            lblResTotalMeiCnt.Text = string.Format("{0:###,##0}", meiTotal);
            lblResTotalImgCnt.Text = string.Format("{0:###,##0}", imgTotal);

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果件数 電子交換 明細={0}件、イメージ={1}件", eltMeiCnt, eltImgCnt), 1);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果件数 行内交換 明細={0}件、イメージ={1}件", ownMeiCnt, ownImgCnt), 1);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果件数 合計 明細={0}件、イメージ={1}件", meiTotal, imgTotal), 1);

            // アーカイブファイル
            StringBuilder fileList = new StringBuilder();
            foreach (ItemManager.ArchiveInfos archive in _itemMgr.ArchiveList.Values)
            {
                fileList.AppendLine(archive.if101.FileName);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("作成ファイル=[{0}]", archive.if101.FileName), 1);
            }
            txtImageFile.Text = fileList.ToString();
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 交換希望日
            if (!CheckInputAll())
            {
                return false;
            }
            _itemMgr.DispParams.ClearingDate = DBConvert.ToIntNull(txtClearinte.Text.Replace(".", ""));

            // 業務
            if (!string.IsNullOrEmpty(txtGymId.Text))
            {
                int nVal = DBConvert.ToIntNull(txtGymId.Text);
                switch (nVal)
                {
                    case 1:
                        _itemMgr.DispParams.InputRoute = GymType.交換持出;
                        break;
                    case 2:
                        _itemMgr.DispParams.InputRoute = GymType.期日管理;
                        break;
                }
            }
            else
            {
                _itemMgr.DispParams.InputRoute = GymType.未指定;
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("絞込条件 交換希望日=[{0}]、業務=[{1}]", _itemMgr.DispParams.ClearingDate, txtGymId.Text), 1);
            return true;
        }


        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeisaiListForm_Shown(object sender, EventArgs e)
        {
            try
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

                SetTextFocus(txtClearinte);
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
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (((BaseTextBox)sender).Name == "txtGymId")
                    {
                        if (base.btnFunc[F6_].Enabled) btnFunc06_Click(sender, e);
                        return;
                    }
                    break;
                default:
                    break;
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
        /// 各種入力チェック
        /// </summary>
        private void txtBox_IValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            this.ClearStatusMessage();

            if (((BaseTextBox)sender).Name == "txtClearinte")
            {
                if (!CheckClearinte())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtGymId")
            {
                if (!CheckGymId())
                {
                    e.Cancel = true;
                }
            }
        }


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F1：終了
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
        /// F5：最新表示
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 画面更新
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "最新表示", 1);

                // 画面表示データ更新
                RefreshDisplayData(true);
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F6：絞込
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 絞込
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "絞込", 1);

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return;
                }

                // 画面表示データ更新
                RefreshDisplayData(true);
                RefreshDisplayState();

                // 予定件数にフォーカス
                this.lvBatList.Select();
                if (this.lvBatList.Items.Count > 0)
                {
                    this.lvBatList.Items[0].Selected = true;
                    this.lvBatList.Items[0].Focused = true;
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
        /// F7：絞込条件
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 絞込条件
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "絞込条件", 1);

                SetTextFocus(txtClearinte);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：実行
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 実行
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "実行", 1);

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return;
                }

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00002);

                    // アーカイブ作成
                    if (!_ctl.MakeArchive(this))
                    {
                        // 画面表示データ更新しない
                        //RefreshDisplayData();
                        return;
                    }

                    // 完了メッセージ
                    if ((_itemMgr.MeisaiList1.Count > 0) || (_itemMgr.MeisaiList2.Count > 0))
                    {
                        string msg = ComMessageMgr.Msg(ComMessageMgr.I00001, "イメージアーカイブ作成");
                        ComMessageMgr.MessageInformation(msg);
                        this.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                        _isComplete = true;
                    }

                    // 画面項目設定
                    SetDisplayParamsResult();

                    // ファンクションキー状態設定
                    SetFunctionState();
                }
                finally
                {
                    //メッセージ初期化
                    EndProcessing(CommonClass.ComMessageMgr.I00002);
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
        /// 交換希望日入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckClearinte()
        {
            string sVal = txtClearinte.ToString();
            if (string.IsNullOrEmpty(sVal)) { return true; }

            // 数値チェック
            if (!Int32.TryParse(sVal, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblClearinte.Text));
                return false;
            }
            // 日付チェック
            if (!EntryCommon.Calendar.IsDate(sVal))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblClearinte.Text));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 業務入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckGymId()
        {
            string sVal = txtGymId.ToString();
            if (string.IsNullOrEmpty(sVal)) { return true; }

            int nVal = DBConvert.ToIntNull(sVal);
            if ((nVal != 1) && (nVal != 2))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02007, lblGymId.Text, "1か2"));
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

    }
}
