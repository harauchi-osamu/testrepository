using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryClass;
using EntryCommon;

namespace CorrectInput
{
	/// <summary>
	/// 明細一覧画面
	/// </summary>
    public partial class BatchListForm : EntryCommonFormBase
	{
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private Dictionary<int, Color> _meicols;
        private bool _isVfy = false;

        private const int COL_KEY = 0;

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

        private SearchType _mode;


        /// <summary>検索タイプ</summary>
        public enum SearchType
        {
            /// <summary>残表示</summary>
            RemainData,
            /// <summary>全表示</summary>
            AllData
        }

        /// <summary>バッチ処理モード</summary>
        public enum ExecMode
        {
            FuncEnt,
            FuncVfy,
            Enter,
            Teisei,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BatchListForm()
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
            base.SetDispName1(string.Format("交換{0} 補正入力", CommonTable.DB.GymParam.GymId.GetName(_ctl.GymId)));
		}

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("明細一覧");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
		{
            _isVfy = _itemMgr.IsVerifyGym();

            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, "残表示", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F3_, "全表示", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F4_, string.Empty);
            SetFunctionName(F5_, "最新表示", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F6_, "エントリ\n自動取得", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F7_, "ベリファイ\n自動取得", _isVfy, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F8_, string.Empty);
            SetFunctionName(F9_, string.Empty);
            SetFunctionName(F10_, "復旧");
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, string.Empty);
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
		{
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
            // コントロール初期化
            InitializeControl();

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
            // 明細一覧の背景色定義を取得
            _meicols = new Dictionary<int, Color>();
            string[] colors = AppConfig.MeiListBackColor.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (string colSet in colors)
            {
                string[] set = colSet.Split(new string[] { ":" }, StringSplitOptions.None);
                if (set.Length < 2) { continue; }
                int sts = DBConvert.ToIntNull(set[0]);
                Color rgb = ColorTranslator.FromHtml("#" + set[1]);
                _meicols.Add(sts, rgb);
            }

            bool isOc = (_ctl.GymId == GymParam.GymId.持出);
            livBatListIc.Visible = !isOc;
            livBatListOc.Visible = isOc;
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
            _mode = SearchType.RemainData;
            lblGymName.Text = GymParam.GymId.GetName(_ctl.GymId);
            lblInputMode.Text = HoseiStatus.HoseiInputMode.GetName(_ctl.HoseiInputMode);
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
            DataTable tbl = _itemMgr.GetTrMei(_mode);
            bool isOc = (_ctl.GymId == GymParam.GymId.持出);

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[tbl.Rows.Count];
            foreach (DataRow row in tbl.Rows)
            {
                int GYM_ID = DBConvert.ToIntNull(row["GYM_ID"]);
                int OPERATION_DATE = DBConvert.ToIntNull(row["OPERATION_DATE"]);
                string SCAN_TERM = DBConvert.ToStringNull(row["SCAN_TERM"]);
                long BAT_ID = DBConvert.ToLongNull(row["BAT_ID"]);
                int DETAILS_NO = DBConvert.ToIntNull(row["DETAILS_NO"]);
                int INPT_STS = DBConvert.ToIntNull(row["INPT_STS"]);

                string key = CommonUtil.GenerateKey(GYM_ID, OPERATION_DATE, SCAN_TERM, BAT_ID, DETAILS_NO);
                string sBatid = BAT_ID.ToString(Const.BAT_ID_LEN_STR);
                string sDetailsno = DETAILS_NO.ToString(Const.DETAILS_NO_LEN_STR);
                string sOperationDate = CommonUtil.ConvToDateFormat(OPERATION_DATE, 3);

                // 共通項目
                listItem.Clear();
                listItem.Add(key);              // (0)隠しキー
                if (isOc)
                {
                    // 持出
                    listItem.Add(sBatid);       // (1)バッチ番号
                }
                else
                {
                    // 持帰
                    listItem.Add(sOperationDate);   // (1)処理日
                }
                listItem.Add(sDetailsno);       // (2)明細番号
                if (isOc)
                {
                    // 持出
                    SetOcMeisai(listItem, row);
                }
                else
                {
                    // 持帰
                    SetIcMeisai(listItem, row);
                }
                listView[cnt] = new ListViewItem(listItem.ToArray());

                // 背景色設定
                if (_meicols.ContainsKey(INPT_STS))
                {
                    listView[cnt].BackColor = _meicols[INPT_STS];
                }
                cnt++;
            }

            if (isOc)
            {
                // 持出
                livBatListOc.Items.Clear();
                livBatListOc.Items.AddRange(listView);
                livBatListOc.Enabled = true;
                livBatListOc.Refresh();
            }
            else
            {
                // 持帰
                livBatListIc.Items.Clear();
                livBatListIc.Items.AddRange(listView);
                livBatListIc.Enabled = true;
                livBatListIc.Refresh();
            }

            // 行選択状態を復帰
            RestoreSelectedInfo();
        }

        /// <summary>
        /// 持出データ設定
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="row"></param>
        private void SetOcMeisai(List<string> listItem, DataRow row)
        {
            string IC_BK_NAME = DBConvert.ToStringNull(row["IC_BK_NAME"]);
            string TMNO = DBConvert.ToStringNull(row["TMNO"]);

            string sOcBrNo = DispNumFormat(row, "OC_BR_NO", Const.BR_NO_LEN_STR);
            string sIcBkNo = DispNumFormat(row, "IC_BK_NO", Const.BANK_NO_LEN_STR);
            string sItmClearingdate = DispDateFormat(row, "ITM_CLEARING_DATE");
            string sAmount = DispNumFormat(row, "AMOUNT", "#,##0");
            string sInputStsName = DispStsFormat(row, "INPT_STS");

            listItem.Add(sOcBrNo);          // (3)持出支店
            listItem.Add(sIcBkNo);          // (4)持帰銀行
            listItem.Add(IC_BK_NAME);       // (5)持帰銀行名

            listItem.Add(sItmClearingdate); // (6)交換希望日
            listItem.Add(sAmount);          // (7)金額
            listItem.Add(sInputStsName);    // (8)入力状態
            listItem.Add(TMNO);             // (9)入力端末
        }

        /// <summary>
        /// 持帰データ設定
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="row"></param>
        private void SetIcMeisai(List<string> listItem, DataRow row)
        {
            string BILL_NAME = DBConvert.ToStringNull(row["BILL_NAME"]);
            string TMNO = DBConvert.ToStringNull(row["TMNO"]);

            string sBillCd = DispNumFormat(row, "BILL_CD", Const.BILL_CD_LEN_STR);
            string sIcOcBkNo = DispNumFormat(row, "IC_OC_BK_NO", Const.BANK_NO_LEN_STR);
            string sIcBkNo = DispNumFormat(row, "IC_BK_NO", Const.BANK_NO_LEN_STR);
            string sIcBrNo = DispNumFormat(row, "IC_BR_NO", Const.BR_NO_LEN_STR);
            string sCtrClearingdate = DispDateFormat(row, "CTR_CLEARING_DATE");
            string sItmClearingdate = DispDateFormat(row, "ITM_CLEARING_DATE");
            string sAmount = DispNumFormat(row, "AMOUNT", "#,##0");
            string sInputStsName = DispStsFormat(row, "INPT_STS");

            listItem.Add(sBillCd);          // (6)証券種類コード
            listItem.Add(BILL_NAME);        // (7)証券種類名称
            listItem.Add(sIcOcBkNo);        // (8)持出銀行
            listItem.Add(sIcBkNo);          // (9)持帰銀行
            listItem.Add(sIcBrNo);          // (10)持帰支店
            listItem.Add(sCtrClearingdate); // (11)電子交換所交換希望日

            listItem.Add(sItmClearingdate); // (12)交換希望日
            listItem.Add(sAmount);          // (13)金額
            listItem.Add(sInputStsName);    // (14)入力状態
            listItem.Add(TMNO);             // (15)入力端末
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 選択行のバッチ情報を取得する
            if (livBatListOc.Visible)
            {
                if (livBatListOc.SelectedIndices.Count < 1)
                {
                    ComMessageMgr.MessageWarning(ComMessageMgr.W00002);
                    return false;
                }
            }
            else if (livBatListIc.Visible)
            {
                if (livBatListIc.SelectedIndices.Count < 1)
                {
                    ComMessageMgr.MessageWarning(ComMessageMgr.W00002);
                    return false;
                }
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
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                case Keys.ControlKey:
                    if (ChangeFunction(e)) return;
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
            if (ChangeFunction(e)) return;
        }

        /// <summary>
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBatList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// [明細一覧]リスト KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void livBatList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        DoEntry(ExecMode.Enter);
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
        /// [明細一覧]リスト MouseDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void livBatList_MouseDoubleClick(object sender, MouseEventArgs e)
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


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F2：残表示
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 残表示
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "残表示", 1);

                // 選択行クリア
                ClearSelectedInfo();

                _mode = SearchType.RemainData;
                SetDisplayParams();
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

        /// <summary>
        /// F3：全表示
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 全表示
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "全表示", 1);

                // 選択行クリア
                ClearSelectedInfo();

                _mode = SearchType.AllData;
                SetDisplayParams();
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

        /// <summary>
        /// F5：最新表示
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 全表示
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "最新表示", 1);

                // 選択行クリア
                ClearSelectedInfo();

                // 最新表示
                SetDisplayParams();
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

        /// <summary>
        /// F6：エントリ開始
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // エントリ開始
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "エントリ開始", 1);

                DoEntry(ExecMode.FuncEnt);
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

        /// <summary>
        /// F7：ベリファイ開始
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ベリファイ開始
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ベリファイ開始", 1);

                DoEntry(ExecMode.FuncVfy);
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

        /// <summary>
        /// F10：復旧
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 復旧
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "復旧", 1);

                // 対象行チェック
                if (!GetDisplayParams())
                {
                    return;
                }

                // 状況復旧チェック
                TBL_HOSEI_STATUS sts = GetSelectedSts(ExecMode.Enter);
                if (sts == null) { return; }
                if (!CanRecovery(sts))
                {
                    ComMessageMgr.MessageWarning(EntMessageMgr.ENT10001);
                    return;
                }

                // 確認メッセージ
                int rcvSts = (sts.m_INPT_STS == HoseiStatus.InputStatus.エントリ中) ? HoseiStatus.InputStatus.エントリ保留 : HoseiStatus.InputStatus.ベリファイ保留;
                string msg = HoseiStatus.InputStatus.GetName(rcvSts);
                DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "「{0}」に状態を復旧します。\nよろしいですか？", msg);
                if (res != DialogResult.OK)
                {
                    return;
                }

                // 状況復旧
                if (!_itemMgr.UpdateRecoveryStatus(sts, rcvSts))
                {
                    return;
                }

                // 完了メッセージ
                ComMessageMgr.MessageInformation(ComMessageMgr.I00001, "復旧");

                // 再取得
                SetDisplayParams();
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
        /// エントリー処理
        /// </summary>
        /// <param name="execMode"></param>
        private void DoEntry(ExecMode execMode)
        {
            try
            {
                // 自動配信対象外リストクリア
                _itemMgr.IgnoreMeisaiList = new List<string>();

                TBL_HOSEI_STATUS sts = null;
                if (execMode == ExecMode.Enter)
                {
                    // Enter実行
                    // 自動配信しない
                    _itemMgr.DspParams.IsAutoReceiveBatch = false;

                    // 画面で選択された明細を表示する（保留含む）
                    sts = GetSelectedSts(execMode);
                    if (sts == null) { return; }
                    if (!CanEntryEnter(sts, execMode))
                    {
                        ComMessageMgr.MessageWarning(EntMessageMgr.ENT10001);
                        return;
                    }

                    // Enter押下時はどちらのモードで動くか判別する
                    switch (sts.m_INPT_STS)
                    {
                        case HoseiStatus.InputStatus.エントリ待:
                        case HoseiStatus.InputStatus.エントリ保留:
                            execMode = ExecMode.FuncEnt;
                            break;
                        case HoseiStatus.InputStatus.ベリファイ待:
                        case HoseiStatus.InputStatus.ベリファイ保留:
                            execMode = ExecMode.FuncVfy;
                            break;
                        default:
                            // 処理不可
                            return;
                    }
                }
                else
                {
                    // ファンクション実行
                    // 自動配信する（保留含まない）
                    _itemMgr.DspParams.IsAutoReceiveBatch = true;
                    bool isExistData = false;

                    // 選択した場合は処理可能かチェック
                    if (livBatListOc.Visible)
                    {
                        if (livBatListOc.SelectedIndices.Count > 0)
                        {
                            sts = GetSelectedSts(execMode);
                            if (CanEntryEnter(sts, execMode))
                            {
                                isExistData = true;
                            }
                        }
                    }
                    else if (livBatListIc.Visible)
                    {
                        if (livBatListIc.SelectedIndices.Count > 0)
                        {
                            sts = GetSelectedSts(execMode);
                            if (CanEntryEnter(sts, execMode))
                            {
                                isExistData = true;
                            }
                        }
                    }

                    // 選択データが有効でない場合は自動配信する
                    if (!isExistData)
                    {
                        // 未選択の場合は自動配信
                        sts = _ctl.GetAutoReceiveBatch(execMode, _ctl.GymId);
                        if (sts == null)
                        {
                            ComMessageMgr.MessageInformation(ComMessageMgr.W00001);
                            return;
                        }
                    }
                }

                // 画面パラメータ
                _itemMgr.DspParams.ExecMode = execMode;

                // エントリデータ取得
                MeisaiInfo mei = _ctl.GetNextEntryData(sts);

                try
                {
                    //　画面上の項目無効化
                    EntryProcessing();

                    // エントリ開始
                    using (EntryController econ = new EntryController(_ctl, this))
                    {
                        _itemMgr.DspParams.InitializeParams();
                        _itemMgr.EntParams.InitializeParams();
                        econ.SetEntryForm(execMode);
                        econ.StartControl(mei);
                    }
                }
                finally
                {
                    //　画面上の項目有効化
                    EntryEndProcessing();
                }
            }
            finally
            {
                // 画面表示データ更新
                RefreshDisplayData();
            }
        }

        /// <summary>
        /// 画面で選択中のバッチ情報を取得する
        /// </summary>
        /// <returns></returns>
        private TBL_HOSEI_STATUS GetSelectedSts(ExecMode execMode)
        {
            string curKey = "";
            if (livBatListOc.Visible)
            {
                if (livBatListOc.SelectedIndices.Count < 1) { return null; }
                curKey = livBatListOc.SelectedItems[0].SubItems[COL_KEY].Text;
            }
            else if (livBatListIc.Visible)
            {
                if (livBatListIc.SelectedIndices.Count < 1) { return null; }
                curKey = livBatListIc.SelectedItems[0].SubItems[COL_KEY].Text;
            }

            string[] keys = CommonUtil.DivideKeys(curKey);
            if (keys.Length < 5) { return null; }
            int gymid = DBConvert.ToIntNull(keys[0]);
            int opedate = DBConvert.ToIntNull(keys[1]);
            string scanid = DBConvert.ToStringNull(keys[2]);
            int batid = DBConvert.ToIntNull(keys[3]);
            int detno = DBConvert.ToIntNull(keys[4]);

            TBL_HOSEI_STATUS sts = _itemMgr.GetHoseiStatus(gymid, opedate, scanid, batid, detno, _ctl.HoseiInputMode);
            if (sts == null)
            {
                ComMessageMgr.MessageInformation(ComMessageMgr.W00001);
                return null;
            }

            // 対象行を選択した場合は選択行より上の行を自動配信対象外リストとして保持する
            if (livBatListOc.Visible)
            {
                foreach (ListViewItem item in livBatListOc.Items)
                {
                    string lvKey = item.Text;
                    if (curKey.Equals(lvKey))
                    {
                        break;
                    }
                    _itemMgr.IgnoreMeisaiList.Add(lvKey);
                }
            }
            else if (livBatListIc.Visible)
            {
                foreach (ListViewItem item in livBatListIc.Items)
                {
                    string lvKey = item.Text;
                    if (curKey.Equals(lvKey))
                    {
                        break;
                    }
                    _itemMgr.IgnoreMeisaiList.Add(lvKey);
                }
            }

            // 選択状態を保持する
            SaveFirstSelectedInfo(curKey);

            return sts;
        }

        /// <summary>
        /// 明細一覧の選択状態を保持する
        /// </summary>
        /// <param name="curKey"></param>
        private void ClearSelectedInfo()
        {
            // キーと行番号をクリアする
            _itemMgr.SelectedInfo = new ItemManager.SelectedInfos();
        }

        /// <summary>
        /// 明細一覧の選択状態を保持する
        /// </summary>
        /// <param name="curKey"></param>
        private void SaveFirstSelectedInfo(string curKey)
        {
            // キーと行番号を保持する
            _itemMgr.SelectedInfo = new ItemManager.SelectedInfos();
            _itemMgr.SelectedInfo.Key = curKey;
            _itemMgr.SelectedInfo.RowIdx = GetSelectedInfoKey(curKey);
        }

        /// <summary>
        /// 明細一覧の選択状態を復帰する
        /// </summary>
        /// <param name="execMode"></param>
        private void RestoreSelectedInfo()
        {
            // キーに合致する行が存在する場合は選択する
            int rowIdx = GetSelectedInfoKey(_itemMgr.SelectedInfo.Key);
            if (rowIdx < 0)
            {
                // キーに合致しない場合は保持した行番号を選択する
                rowIdx = _itemMgr.SelectedInfo.RowIdx;
            }
            if (rowIdx < 0)
            {
                // 対象がない場合は先頭
                rowIdx = 0;
            }
            // 選択行を復帰する
            if (_ctl.GymId == GymParam.GymId.持出)
            {
                if ((livBatListOc.Items.Count > 0))
                {
                    // 一覧にデータがある場合
                    if (rowIdx + 1 > livBatListOc.Items.Count)
                    {
                        // 行番号が範囲外の場合は先頭
                        rowIdx = 0;
                    }

                    livBatListOc.Items[rowIdx].Selected = true;
                    livBatListOc.Items[rowIdx].Focused = true;
                    livBatListOc.Select();
                    SetListDefPositon(livBatListOc);
                }
            }
            else if (_ctl.GymId == GymParam.GymId.持帰)
            {
                if ((livBatListIc.Items.Count > 0))
                {
                    // 一覧にデータがある場合
                    if ((rowIdx + 1 > livBatListIc.Items.Count))
                    {
                        // 行番号が範囲外の場合は先頭
                        rowIdx = 0;
                    }

                    livBatListIc.Items[rowIdx].Selected = true;
                    livBatListIc.Items[rowIdx].Focused = true;
                    livBatListIc.Select();
                    SetListDefPositon(livBatListIc);
                }
            }
        }

        /// <summary>
        /// 指定したキーに合致する行番号を取得する
        /// </summary>
        /// <param name="curKey"></param>
        /// <returns></returns>
        private int GetSelectedInfoKey(string curKey)
        {
            int rowIdx = -1;
            bool isExist = false;
            if (livBatListOc.Visible)
            {
                foreach (ListViewItem item in livBatListOc.Items)
                {
                    rowIdx++;
                    string lvKey = item.Text;
                    if (curKey.Equals(lvKey))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (livBatListOc.Items.Count <= rowIdx)
                {
                    rowIdx = livBatListOc.Items.Count - 1;
                }
            }
            else if (livBatListIc.Visible)
            {
                foreach (ListViewItem item in livBatListIc.Items)
                {
                    rowIdx++;
                    string lvKey = item.Text;
                    if (curKey.Equals(lvKey))
                    {
                        isExist = true;
                        break;
                    }
                }
            }
            return isExist ? rowIdx : -1;
        }

        /// <summary>
        /// Enterキー押下処理可能かどうか
        /// </summary>
        /// <param name="sts"></param>
        /// <returns></returns>
        private bool CanEntryEnter(TBL_HOSEI_STATUS sts, ExecMode execMode)
        {
            if (sts == null)
            {
                return false;
            }
            if (execMode == ExecMode.Enter)
            {
                if (((sts.m_INPT_STS != HoseiStatus.InputStatus.エントリ待) &&
                     (sts.m_INPT_STS != HoseiStatus.InputStatus.エントリ保留) &&
                     (sts.m_INPT_STS != HoseiStatus.InputStatus.ベリファイ待) &&
                     (sts.m_INPT_STS != HoseiStatus.InputStatus.ベリファイ保留)))
                {
                    return false;
                }
            }
            else if (execMode == ExecMode.FuncEnt)
            {
                if (sts.m_INPT_STS != HoseiStatus.InputStatus.エントリ待)
                {
                    return false;
                }
            }
            else if (execMode == ExecMode.FuncVfy)
            {
                if (sts.m_INPT_STS != HoseiStatus.InputStatus.ベリファイ待)
                {
                    return false;
                }

                // エントリユーザーと一致する場合はベリファイ不可
                if (sts.m_E_OPENO == AplInfo.OP_ID)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ファンクションキー押下処理可能かどうか
        /// </summary>
        /// <param name="sts"></param>
        /// <returns></returns>
        private bool CanEntryFunc(TBL_HOSEI_STATUS sts)
        {
            if (sts == null)
            {
                return false;
            }
            if (((sts.m_INPT_STS != HoseiStatus.InputStatus.エントリ待) &&
                 (sts.m_INPT_STS != HoseiStatus.InputStatus.ベリファイ待)))
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 状況復旧可能かどうか
        /// </summary>
        /// <param name="sts"></param>
        /// <returns></returns>
        private bool CanRecovery(TBL_HOSEI_STATUS sts)
        {
            if (sts == null)
            {
                return false;
            }
            if (((sts.m_INPT_STS != HoseiStatus.InputStatus.エントリ中) &&
                 (sts.m_INPT_STS != HoseiStatus.InputStatus.ベリファイ中)))
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


        #region エントリー処理中設定

        /// <summary>
        /// エントリー処理中設定
        /// 画面上の項目を無効化
        /// </summary>
        private void EntryProcessing()
        {
            // 画面表示箇所Disable
            contentsPanel.Enabled = false;
            // ファンクションDisable
            DisableAllFunctionState(false);
            SetDisableFunction(true);
        }

        /// <summary>
        /// エントリー処理中状態を解除する
        /// 画面上の項目を有効化
        /// </summary>
        private void EntryEndProcessing()
        {
            // 画面表示箇所有効化
            contentsPanel.Enabled = true;
            // Disableにしたファンクションを元に戻す
            InitializeFunction();
            SetFunctionState();
            SetDisableFunction(false);
        }

        #endregion

        #region 表示制御

        /// <summary>
        /// 画面表示データ整形(日付)
        /// </summary>
        private string DispDateFormat(DataRow row, string FieldName)
        {
            int Date = DBConvert.ToIntNull(row[FieldName]);
            if (Date == 0) return string.Empty;
            return CommonUtil.ConvToDateFormat(Date, 3);
        }

        /// <summary>
        /// 画面表示データ整形(数値)
        /// </summary>
        private string DispNumFormat(DataRow row, string FieldName, string Format)
        {
            string sNum = DBConvert.ToStringNull(row[FieldName]);
            if (string.IsNullOrEmpty(sNum)) return string.Empty; // 空の場合
            if (!long.TryParse(sNum, out long Num)) return string.Empty; // 数値以外の場合
            return Num.ToString(Format);
        }

        /// <summary>
        /// 画面表示データ整形(ステータス)
        /// </summary>
        private string DispStsFormat(DataRow row, string FieldName)
        {
            int sts = DBConvert.ToIntNull(row[FieldName]);
            return HoseiStatus.InputStatus.GetName(sts);
        }

        #endregion

    }
}
