using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using ImageController;
using NCR;

namespace CTRImgListForm
{
	/// <summary>
	/// 検索結果一覧画面
	/// </summary>
    public partial class ImageListForm : EntryCommonFormBase
	{
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private int _curPage = 1;
        private int _curIdx = -1;
        private DspMode _dspmode;

        private ImageCanvas _cvsImage1 = null;
        private ImageCanvas _cvsImage2 = null;
        private ImageCanvas _cvsImage3 = null;
        private ImageCanvas _cvsImage4 = null;
        private ImageCanvas _cvsImage5 = null;
        private ImageCanvas _cvsImage6 = null;
        private ImageCanvas _cvsImage7 = null;
        private ImageCanvas _cvsImage8 = null;
        private ImageCanvas _cvsImage9 = null;
        private ImageCanvas _cvsImage10 = null;

        private Bitmap _cvsCut1 = null;
        private Bitmap _cvsCut2 = null;
        private Bitmap _cvsCut3 = null;
        private Bitmap _cvsCut4 = null;
        private Bitmap _cvsCut5 = null;
        private Bitmap _cvsCut6 = null;
        private Bitmap _cvsCut7 = null;
        private Bitmap _cvsCut8 = null;
        private Bitmap _cvsCut9 = null;
        private Bitmap _cvsCut10 = null;

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
        private const string EXE_NAME = "CTRHoseiEntry.exe";

        public enum DspMode
        {
            持帰銀行,
            交換希望日,
            金額,
            種類,
            持帰支店,
            口座番号,
            手形番号,
        }

        public enum EntryMode
        {
            自行情報,
            交換尻,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageListForm()
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
			base.SetDispName1("明細照会");
		}

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            string dspname = string.Format("明細イメージ一覧（{0}）", GymParam.GymId.GetName(_ctl.GymId));
            base.SetDispName2(dspname);
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
		{
            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, "前頁", false);
            SetFunctionName(F3_, "次頁", false);
            SetFunctionName(F4_, "持帰銀行", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F5_, "交換\n希望日", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F6_, "金額");
            SetFunctionName(F7_, "種類");
            SetFunctionName(F8_, "持帰支店", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F9_, "口座番号", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F10_, "手形番号", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F11_, "交換尻\n訂正", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F12_, "自行情報\n訂正", true, Const.FONT_SIZE_FUNC_LOW);
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
		{
            bool isMochikaeri = (_ctl.GymId == GymParam.GymId.持帰);
            bool isFirstPage = (_curPage == 1);
            bool isLastPage = (_curPage == _itemMgr.PageInfo.MaxPageNo);

            SetFunctionState(F2_, !isFirstPage);
            SetFunctionState(F3_, !isLastPage);
            SetFunctionState(F7_, isMochikaeri);
            SetFunctionState(F8_, isMochikaeri);
            SetFunctionState(F9_, isMochikaeri);
            SetFunctionState(F10_, isMochikaeri);
            SetFunctionState(F12_, isMochikaeri);

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
            _curPage = 1;
            _dspmode = DspMode.持帰銀行;
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
            // データタイプ
            lblDataType.Text = GetDataTypeName();

            // 明細取得
            if (!_itemMgr.Fetch_wk_trmei(_curPage))
            {
                return;
            }

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[_itemMgr.PageInfo.wk_trmeis.Count];
            foreach (ItemManager.MeisaiInfos info in _itemMgr.PageInfo.wk_trmeis.Values)
            {
                string key = info.No.ToString();
                string opedate = CommonUtil.ConvToDateFormat(info.wk_trmei._OPERATION_DATE, 3);
                string batid = info.wk_trmei._BAT_ID.ToString(Const.BAT_ID_LEN_STR);
                string detailsno = info.wk_trmei._DETAILS_NO.ToString(Const.DETAILS_NO_LEN_STR);
                string sVal = "";
                long nVal = 0;
                switch (_dspmode)
                {
                    case DspMode.持帰銀行:
                        sVal = GetTrItemValue(info.tritems, DspItem.ItemId.券面持帰銀行コード);
                        break;
                    case DspMode.交換希望日:
                        sVal = GetTrItemValue(info.tritems, DspItem.ItemId.入力交換希望日);
                        sVal = CommonUtil.ConvToDateFormat(sVal, 3);
                        break;
                    case DspMode.金額:
                        nVal = GetTrItemLongValue(info.tritems, DspItem.ItemId.金額);
                        sVal = string.Format("{0:#,##0}", nVal);
                        break;
                    case DspMode.種類:
                        sVal = GetTrItemValue(info.tritems, DspItem.ItemId.手形種類コード);
                        break;
                    case DspMode.持帰支店:
                        sVal = GetTrItemValue(info.tritems, DspItem.ItemId.券面持帰支店コード);
                        break;  
                    case DspMode.口座番号:
                        sVal = GetTrItemValue(info.tritems, DspItem.ItemId.口座番号);
                        break;
                    case DspMode.手形番号:
                        sVal = GetTrItemValue(info.tritems, DspItem.ItemId.手形番号);
                        break;
                }
                string enddata = sVal;
                listItem.Clear();
                listItem.Add(key);          // 隠しキー
                listItem.Add(opedate);      // 取込日
                listItem.Add(batid);        // バッチ番号
                listItem.Add(detailsno);    // 明細連番
                listItem.Add(enddata);      // 入力値
                listView[cnt] = new ListViewItem(listItem.ToArray());
                cnt++;
            }
            this.lvBatList.Items.Clear();
            this.lvBatList.Items.AddRange(listView);
            this.lvBatList.Enabled = true;
            this.lvBatList.Refresh();
            this.lvBatList.Select();
            // リストボックスの高さを大きくする設定
            this.lvBatList.View = View.Details;
            this.lvBatList.SmallImageList.ImageSize = new Size(1, 70);

            // 選択行の復帰
            if ((this.lvBatList.Items.Count > 0) && (_curIdx > -1) && (this.lvBatList.Items.Count >= _curIdx + 1))
            {
                this.lvBatList.Items[_curIdx].Selected = true;
                this.lvBatList.Items[_curIdx].Focused = true;
            }

            // ページ番号
            lblPage.Text = string.Format("{0:#,##0}／{1:#,##0}", _curPage, _itemMgr.PageInfo.MaxPageNo);

            // イメージ描画
            MakeView();
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 選択行のバッチ情報を取得する
            _itemMgr.DispParams.Clear();
            if (this.lvBatList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("対象行が選択されていません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            _itemMgr.DispParams.Seq = DBConvert.ToIntNull(lvBatList.SelectedItems[0].SubItems[COL_KEY].Text);
            return true;
        }


        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// [明細一覧] SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _curIdx = GetCurIndex();
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
        /// F2：前頁
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 前頁
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "前頁", 1);

                _curPage--;
                if (_curPage < 1)
                {
                    _curPage = 1;
                }

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F3：次頁
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 次頁
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "次頁", 1);

                _curPage++;
                if (_itemMgr.PageInfo.MaxPageNo < _curPage)
                {
                    _curPage = _itemMgr.PageInfo.MaxPageNo;
                }

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F4：持帰銀行
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 持帰銀行
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "持帰銀行", 1);

                _dspmode = DspMode.持帰銀行;

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F5：交換希望日
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 交換希望日
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "交換希望日", 1);

                _dspmode = DspMode.交換希望日;

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F6：金額
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 金額
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "金額", 1);

                _dspmode = DspMode.金額;

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F7：種類
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 種類
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "種類", 1);

                _dspmode = DspMode.種類;

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F8：持帰支店
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 持帰支店
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "持帰支店", 1);

                _dspmode = DspMode.持帰支店;

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F9：口座番号
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 口座番号
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "口座番号", 1);

                _dspmode = DspMode.口座番号;

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F10：手形番号
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 手形番号
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "手形番号", 1);

                _dspmode = DspMode.手形番号;

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F11：交換尻訂正
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 交換尻訂正
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "交換尻訂正", 1);

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return;
                }

                // 完了訂正起動
                ExecuteTeiseiEntry(EntryMode.交換尻);

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// F12：自行情報訂正
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 自行情報訂正
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "自行情報訂正", 1);

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return;
                }

                // 完了訂正起動
                ExecuteTeiseiEntry(EntryMode.自行情報);

                // 画面表示データ更新
                RefreshDisplayData();
                RefreshDisplayState();
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
        /// 表示データ名称を取得する
        /// </summary>
        /// <returns></returns>
        private string GetDataTypeName()
        {
            string datatype = "";
            switch (_dspmode)
            {
                case DspMode.持帰銀行:
                    datatype = "持帰銀行";
                    break;
                case DspMode.交換希望日:
                    datatype = "交換希望日";
                    break;
                case DspMode.金額:
                    datatype = "金額";
                    break;
                case DspMode.種類:
                    datatype = "種類";
                    break;
                case DspMode.持帰支店:
                    datatype = "持帰支店";
                    break;
                case DspMode.口座番号:
                    datatype = "口座番号";
                    break;
                case DspMode.手形番号:
                    datatype = "手形番号";
                    break;
            }
            return datatype;
        }

        /// <summary>
        /// アイテムを取得する
        /// </summary>
        /// <param name="info"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetItem(ItemManager.MeisaiInfos info, int itemid)
        {
            return info.tritems.ContainsKey(itemid) ? info.tritems[itemid].m_END_DATA : "";
        }

        /// <summary>
        /// 完了訂正を実行する
        /// </summary>
        /// <param name="mode"></param>
        private bool ExecuteTeiseiEntry(EntryMode mode)
        {
            TBL_TRMEI mei = _itemMgr.PageInfo.wk_trmeis[_itemMgr.DispParams.Seq].trmei;
            TBL_HOSEI_STATUS hosei_status = null;
            TBL_TRMEI trmei = null;

            string key = string.Format("{0}|{1}|{2}|{3}", mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO);
            string args = "";
            switch (mode)
            {
                case EntryMode.自行情報:
                    args = string.Format("{0} {1} {2} {3} {4}", _ctl.MenuNumber, 1, AppInfo.Setting.GymId, 6, key);
                    hosei_status = _itemMgr.GetHoseiStatus(mei, HoseiStatus.HoseiInputMode.自行情報);
                    trmei = _itemMgr.GetTrMei(mei);
                    break;
                case EntryMode.交換尻:
                    args = string.Format("{0} {1} {2} {3} {4}", _ctl.MenuNumber, 1, AppInfo.Setting.GymId, 5, key);
                    hosei_status = _itemMgr.GetHoseiStatus(mei, HoseiStatus.HoseiInputMode.交換尻);
                    trmei = _itemMgr.GetTrMei(mei);
                    break;
            }

            // エントリ画面起動
            ExecuteProcess(args);
            return true;
        }

        /// <summary>
        /// 補正エントリープロセス起動
        /// </summary>
        private void ExecuteProcess(string args)
        {
            string procPath = CommonUtil.ConcatPath(ServerIni.Setting.ExePath, EXE_NAME);
            string workDir = Path.GetDirectoryName(procPath);
            if (!File.Exists(procPath))
            {
                string msg = string.Format("補正エントリープログラムが見つかりませんでした。[{0}]", procPath);
                ComMessageMgr.MessageError(msg);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), msg, 2);
                this.SetStatusMessage(msg);
                return;
            }
            ProcessManager.RunProcess(procPath, workDir, args, true, false);
        }

        // *******************************************************************
        // 内部メソッド（イメージ関連）
        // *******************************************************************

        /// <summary>
        /// イメージ画像を描画する
        /// </summary>
        private void MakeView()
        {
            // コントロール描画中断
            this.SuspendLayout();

            MakeView(ref _cvsImage1, ref _cvsCut1, pictureBox1, 1);
            MakeView(ref _cvsImage2, ref _cvsCut2, pictureBox2, 2);
            MakeView(ref _cvsImage3, ref _cvsCut3, pictureBox3, 3);
            MakeView(ref _cvsImage4, ref _cvsCut4, pictureBox4, 4);
            MakeView(ref _cvsImage5, ref _cvsCut5, pictureBox5, 5);
            MakeView(ref _cvsImage6, ref _cvsCut6, pictureBox6, 6);
            MakeView(ref _cvsImage7, ref _cvsCut7, pictureBox7, 7);
            MakeView(ref _cvsImage8, ref _cvsCut8, pictureBox8, 8);
            MakeView(ref _cvsImage9, ref _cvsCut9, pictureBox9, 9);
            MakeView(ref _cvsImage10, ref _cvsCut10, pictureBox10, 10);

            // コントロール描画再開
            this.ResumeLayout();
        }

        /// <summary>
        /// 指定した描画領域にイメージ画像を読み込む
        /// </summary>
        private void MakeView(ref ImageCanvas canvas, ref Bitmap cvsCut, PictureBox picBox, int no)
        {
            if (canvas != null)
            {
                canvas.Dispose();
                canvas = null;
            }
            if (cvsCut != null)
            {
                cvsCut.Dispose();
                cvsCut = null;
            }

            // 明細なし
            if (!_itemMgr.PageInfo.wk_trmeis.ContainsKey(no))
            {
                picBox.Image = null;
                return;
            }

            // データ種別
            int itemid = 0;
            switch (_dspmode)
            {
                case DspMode.持帰銀行:
                    itemid = DspItem.ItemId.券面持帰銀行コード;
                    break;
                case DspMode.交換希望日:
                    itemid = DspItem.ItemId.入力交換希望日;
                    break;
                case DspMode.金額:
                    itemid = DspItem.ItemId.金額;
                    break;
                case DspMode.種類:
                    itemid = DspItem.ItemId.手形種類コード;
                    break;
                case DspMode.持帰支店:
                    itemid = DspItem.ItemId.券面持帰支店コード;
                    break;
                case DspMode.口座番号:
                    itemid = DspItem.ItemId.券面口座番号;
                    break;
                case DspMode.手形番号:
                    itemid = DspItem.ItemId.手形番号;
                    break;
            }

            // 切出し画像を描画する
            ItemManager.MeisaiInfos mei = _itemMgr.PageInfo.wk_trmeis[no];
            if (!File.Exists(mei.ImgFilePath))
            {
                picBox.Image = null;
                return;
            }
            if (!mei.tritems.ContainsKey(itemid))
            {
                //口座番号等はないケースがあるため項目チェック
                picBox.Image = null;
                return;
            }

            ImageEditor editor = new ImageEditor();
            canvas = new ImageCanvas(editor, mei.imgparam.m_REDUCE_RATE);
            canvas.EnableCanvasCut = true;
            canvas.InitializeCanvas(mei.ImgFilePath);
            canvas.SetDefaultReSize(canvas.ResizeCanvas.Width, canvas.ResizeCanvas.Height);
            canvas.Cut(ImageCanvas.CanvasType.Color, GetCutRectangleInfoReal(mei.trmei, mei.tritems[itemid], canvas));

            // 切出がうまくできない場合
            if (canvas.CutCanvas == null)
            {
                picBox.Image = null;
                return;
            }

            // 縮小率の小さい方にフィットさせる
            ImageEditor.ImageInfo imgInfo = editor.GetImageInfo(canvas.CutCanvas);
            float hReduce = (float)picBox.Width / (float)canvas.CutCanvas.Width;
            float vReduce = (float)picBox.Height / (float)canvas.CutCanvas.Height;
            if (hReduce > vReduce)
            {
                // 縦に収まらない場合のみ縮小する
                if (vReduce < 1)
                {
                    imgInfo.Width = (int)Math.Round(imgInfo.Width * vReduce);
                    imgInfo.Height = (int)Math.Round(imgInfo.Height * vReduce);
                }
            }
            else
            {
                // 横に収まらない場合のみ縮小する
                if (hReduce < 1)
                {
                    imgInfo.Width = (int)Math.Round(imgInfo.Width * hReduce);
                    imgInfo.Height = (int)Math.Round(imgInfo.Height * hReduce);
                }
            }
            cvsCut = editor.CloneCanvas(canvas.CutCanvas, imgInfo);
            picBox.Image = cvsCut;
        }

        /// <summary>
        /// 切出座標を取得する
        /// </summary>
        /// <returns></returns>
        private ImageEditor.RectangleInfo GetCutRectangleInfoReal(TBL_TRMEI trmei, TBL_TRITEM tritem, ImageCanvas canvas)
        {
            ImageEditor.RectangleInfo cutRect = new ImageEditor.RectangleInfo();

            // IMG_CURSOR_PARAMから座標取得
            GetCutRectangleInfoImgCurParam(trmei, tritem, ref cutRect);

            if (cutRect.X2 <= 0 || cutRect.Y2 <= 0)
            {
                // IMG_CURSOR_PARAMの切出座標が不正の場合、
                // TRITEMから座標取得
                cutRect.X1 = (int)tritem.m_ITEM_LEFT;
                cutRect.Y1 = (int)tritem.m_ITEM_TOP;
                cutRect.Height = (int)tritem.m_ITEM_HEIGHT;
                cutRect.Width = (int)tritem.m_ITEM_WIDTH;
                cutRect.X2 = cutRect.X1 + cutRect.Width;
                cutRect.Y2 = cutRect.Y1 + cutRect.Height;
            }

            return cutRect;
        }

        /// <summary>
        /// IMG_CURSOR_PARAM から切出座標を取得する
        /// </summary>
        /// <returns></returns>
        private void GetCutRectangleInfoImgCurParam(TBL_TRMEI trmei, TBL_TRITEM tritem, ref ImageEditor.RectangleInfo cutRect)
        {

            TBL_IMG_CURSOR_PARAM ImgCurParam = _itemMgr.GetImgCursorParams(trmei.m_DSP_ID, tritem._ITEM_ID);
            cutRect.X1 = ImgCurParam.m_ITEM_LEFT;
            cutRect.Y1 = ImgCurParam.m_ITEM_TOP;
            cutRect.Height = ImgCurParam.m_ITEM_HEIGHT;
            cutRect.Width = ImgCurParam.m_ITEM_WIDTH;
            cutRect.X2 = cutRect.X1 + cutRect.Width;
            cutRect.Y2 = cutRect.Y1 + cutRect.Height;
        }

        /// <summary>
        /// TRITEM を取得する
        /// </summary>
        /// <param name="tritems"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetTrItemValue(SortedDictionary<int, TBL_TRITEM> tritems, int itemid)
        {
            if (!tritems.ContainsKey(itemid)) { return ""; }
            return tritems[itemid].m_END_DATA;
        }

        /// <summary>
        /// TRITEM を取得する
        /// </summary>
        /// <param name="tritems"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private long GetTrItemLongValue(SortedDictionary<int, TBL_TRITEM> tritems, int itemid)
        {
            return DBConvert.ToLongNull(GetTrItemValue(tritems, itemid));
        }

        /// <summary>
        /// 選択行を取得する
        /// </summary>
        /// <returns></returns>
        private int GetCurIndex()
        {
            if (lvBatList.SelectedIndices.Count < 1)
            {
                return -1;
            }
            return lvBatList.SelectedItems[0].Index;
        }

    }
}
