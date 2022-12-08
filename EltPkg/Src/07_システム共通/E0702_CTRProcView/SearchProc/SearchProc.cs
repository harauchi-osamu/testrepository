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

namespace SearchProc
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchProc : EntryCommonFormBase
    {

        #region クラス変数

        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private DispMode _curFlg = DispMode.DATE;

        #endregion

        #region enum

        public enum DispMode
        {
            ///<summary>日付指定</summary>
            DATE = 1,
            ///<summary>全量</summary>
            ALL = 2,
        }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchProc()
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
            base.SetDispName1("システム共通");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            if (_curFlg == DispMode.DATE)
            {
                // 日付指定
                base.SetDispName2("進捗状況（当日・翌営）");
            }
            else
            {
                // 全量
                base.SetDispName2("進捗状況（全量）");
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
                SetFunctionName(11, "当日・\n   翌営", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(12, "全量");
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
            // 初期表示設定
            SetTimerFirst();
            ////結果表示
            //DispText();

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
        /// Timerイベント
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.btnFunc[5].Enabled)
            {
                this.btnFunc05_Click(null, null);
            }
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
                DispText();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F11：当日・翌営
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "当日・翌営", 1);
                _curFlg = DispMode.DATE;
                DispText();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：全量
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "全量", 1);
                _curFlg = DispMode.ALL;
                DispText();
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
        /// 内容表示
        /// </summary>
        private void DispText()
        {
            try
            {
                //タイマー停止
                SetTimer(false);

                this.SuspendLayout();

                //翌営業日取得
                iBicsCalendar cal = new iBicsCalendar();
                cal.SetHolidays();
                int AfterDate = cal.getBusinessday(AplInfo.OpDate(), 1);

                //前営業日取得
                int BeforeDate = cal.getBusinessday(AplInfo.OpDate(), -1);

                // 【イメージ取込】
                if (!_itemMgr.FetchImgImport(this))
                {
                    return;
                }
                //イメージ取込
                DispImgImport(AplInfo.OpDate(), AfterDate);

                // 持出:【補正入力・持出アップロード】
                if (!_itemMgr.FetchOCInputData(this))
                {
                    return;
                }
                //持出:持帰銀行
                DispOCICBkText(AplInfo.OpDate(), AfterDate);
                //持出:金額
                DispOCAmtText(AplInfo.OpDate(), AfterDate);
                //持出:完了訂正
                DispOCTeiseiText(AplInfo.OpDate(), AfterDate);
                //持出:アップロード
                DispOCUploadText(AplInfo.OpDate(), AfterDate);

                //【持出取消アップロード状況】
                if (!_itemMgr.FetchOCDeleteData(this))
                {
                    return;
                }
                // 持出:取消
                DispOCDeleteUploadText(AplInfo.OpDate(), AfterDate);

                // 持帰:【ダウンロード】
                if (!_itemMgr.FetchICDownLoadData(this))
                {
                    return;
                }
                //持帰:ダウンロード
                DispICDownLoad(AplInfo.OpDate(), AfterDate, BeforeDate);

                // 持帰:【補正入力（交換尻関連）】
                if (!_itemMgr.FetchICInputData(this))
                {
                    return;
                }
                //持帰:持帰銀行
                DispICICBkText(AplInfo.OpDate(), AfterDate);
                //持帰:交換日
                DispICCDateText(AplInfo.OpDate(), AfterDate);
                //持帰:金額
                DispICAmtText(AplInfo.OpDate(), AfterDate);
                //持帰:完了訂正
                DispICTeiseiText(AplInfo.OpDate(), AfterDate);
                //持帰:自行
                DispICJikouText(AplInfo.OpDate(), AfterDate);

                // 訂正データファイル
                if (!_itemMgr.FetchICTeiseiiData(this))
                {
                    return;
                }
                //訂正データ
                DispICTeiseiDataText(AplInfo.OpDate(), AfterDate);

                // 持帰:【削除済】
                if (!_itemMgr.FetchICDeleteData(this))
                {
                    return;
                }
                //削除データ
                DispICDeleteText(AplInfo.OpDate(), AfterDate);

                // 【不渡返還登録】
                if (!_itemMgr.FetchICFuwatariData(this))
                {
                    return;
                }
                //持帰:不渡返還
                DispICFuwatariText(AplInfo.OpDate(), AfterDate);

            }
            finally
            {
                //表示制御
                SetDispItem();

                //タイマー再開
                SetTimer(true);
                this.ResumeLayout();
            }
        }

        #region 表示制御

        /// <summary>
        /// 表示制御
        /// </summary>
        private void SetDispItem()
        {
            //タイトル更新
            SetDispName2("");

            //表示制御
            if (_curFlg == DispMode.DATE)
            {
                // 日付指定
                // 持出
                pnlOC02.Visible = true;
                pnlOC03.Visible = true;
                pnlOC04.Visible = true;
                lblOCTitle01.Text = "当日交換分";
                lblOCTitle02.Text = "当日交換分";
                lblOCTitle03.Text = "当日交換分";
                lblOCTitle04.Text = "";
                lblOCTitle03.Visible = true;
                lblOCTitle04.Visible = false;

                // 持帰
                pnlIC02.Visible = true;
                pnlIC03.Visible = true;
                lblICTitle01.Text = "当日交換分";
                lblICTitle02.Text = "当日交換分";
                lblICTitle03.Text = "当日交換分";
                lblICTitle04.Text = "当日交換分";
                lblICTitle05.Text = "当日交換分";
                lblICTitle06.Text = "";
                lblICTitle05.Visible = true;
                lblICTitle06.Visible = false;
            }
            else
            {
                // 全量
                // 持出
                pnlOC02.Visible = false;
                pnlOC03.Visible = false;
                pnlOC04.Visible = false;
                lblOCTitle01.Text = "全量";
                lblOCTitle02.Text = "全量";
                lblOCTitle03.Text = "";
                lblOCTitle04.Text = "";
                lblOCTitle03.Visible = false;
                lblOCTitle04.Visible = true;

                // 持帰
                pnlIC02.Visible = false;
                pnlIC03.Visible = false;
                lblICTitle01.Text = "全量";
                lblICTitle02.Text = "全量";
                lblICTitle03.Text = "全量";
                lblICTitle04.Text = "";
                lblICTitle05.Text = "";
                lblICTitle06.Text = "";
                lblICTitle05.Visible = false;
                lblICTitle06.Visible = true;
            }
        }

        #endregion

        #region 表示(イメージ取込)

        /// <summary>
        /// 内容表示(イメージ取込)
        /// </summary>
        private void DispImgImport(int OpeDate, int AfterDate)
        {
            //2022.11.08 SP.harauchi No.152 表示速度改善対応
            lblImgNotComp.Text = SearchResultCommon.DispDataFormat(ItemManager.imgImp.UnPro, "#,##0");

            if (_curFlg == DispMode.DATE)
            {
                //当日・翌営
                //処理済バッチ(当日)
                lblImgCompBat.Text = SearchResultCommon.DispDataFormat(ItemManager.imgImp.Today_Batch, "#,##0");
                //処理済明細(当日)
                lblImgCompMei.Text = SearchResultCommon.DispDataFormat(ItemManager.imgImp.Today_Mei, "#,##0");
                //処理済バッチ(翌日)
                lblImgCompBatDate.Text = SearchResultCommon.DispDataFormat(ItemManager.imgImp.Nextday_Batch, "#,##0");
                //処理済明細(翌日)
                lblImgCompMeiDate.Text = SearchResultCommon.DispDataFormat(ItemManager.imgImp.Nextday_Mei, "#,##0");
            }
            else
            {
                // 全量
                //処理済バッチ(全量)
                lblImgCompBat.Text = SearchResultCommon.DispDataFormat(ItemManager.imgImp.All_Batch, "#,##0");
                //処理済明細(全量)
                lblImgCompMei.Text = SearchResultCommon.DispDataFormat(ItemManager.imgImp.All_Mei, "#,##0");
                //未使用項目
                lblImgCompBatDate.Text = string.Empty;
                lblImgCompMeiDate.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持出・持帰銀行)

        /// <summary>
        /// 内容表示(持出・持帰銀行)
        /// </summary>
        private void DispOCICBkText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.OCInput> OCInputDataICBk = _itemMgr.OCInputData.Where(x => x.InputMode == HoseiStatus.HoseiInputMode.持帰銀行);

            //持帰銀行
            IEnumerable<ItemManager.OCInput> OCICBkWait = OCInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ待);
            IEnumerable<ItemManager.OCInput> OCICBkHoryu = OCInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ保留);
            IEnumerable<ItemManager.OCInput> OCICBkInput = OCInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ中);
            IEnumerable<ItemManager.OCInput> OCICBkComp = OCInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.完了);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //持帰銀行(当日)
                IEnumerable<ItemManager.OCInput> OCICBkWaitOpeDate = OCICBkWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCInput> OCICBkHoryuOpeDate = OCICBkHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCInput> OCICBkInputOpeDate = OCICBkInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCInput> OCICBkCompOpeDate = OCICBkComp.Where(x => x.ClearingDate == OpeDate);

                //持帰銀行・エントリ待
                lblOCIcBk01.Text = SearchResultCommon.DispDataFormat(OCICBkWaitOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ保留
                lblOCIcBk02.Text = SearchResultCommon.DispDataFormat(OCICBkHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ中
                lblOCIcBk03.Text = SearchResultCommon.DispDataFormat(OCICBkInputOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・完了
                lblOCIcBk04.Text = SearchResultCommon.DispDataFormat(OCICBkCompOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                //持帰銀行(翌日)
                IEnumerable<ItemManager.OCInput> OCICBkWaitAfterDate = OCICBkWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCInput> OCICBkHoryuAfterDate = OCICBkHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCInput> OCICBkInputAfterDate = OCICBkInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCInput> OCICBkCompAfterDate = OCICBkComp.Where(x => x.ClearingDate == AfterDate);

                //持帰銀行・エントリ待
                lblOCIcBkDate01.Text = SearchResultCommon.DispDataFormat(OCICBkWaitAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ保留
                lblOCIcBkDate02.Text = SearchResultCommon.DispDataFormat(OCICBkHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ中
                lblOCIcBkDate03.Text = SearchResultCommon.DispDataFormat(OCICBkInputAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・完了
                lblOCIcBkDate04.Text = SearchResultCommon.DispDataFormat(OCICBkCompAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量
                IEnumerable<ItemManager.OCInput> OCICBkWaitAll = OCICBkWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCInput> OCICBkHoryuAll = OCICBkHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCInput> OCICBkInputAll = OCICBkInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCInput> OCICBkCompAll = OCICBkComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //持帰銀行(全量)
                //持帰銀行・エントリ待
                lblOCIcBk01.Text = SearchResultCommon.DispDataFormat(OCICBkWaitAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ保留
                lblOCIcBk02.Text = SearchResultCommon.DispDataFormat(OCICBkHoryuAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ中
                lblOCIcBk03.Text = SearchResultCommon.DispDataFormat(OCICBkInputAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・完了
                lblOCIcBk04.Text = SearchResultCommon.DispDataFormat(OCICBkCompAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblOCIcBkDate01.Text = string.Empty;
                lblOCIcBkDate02.Text = string.Empty;
                lblOCIcBkDate03.Text = string.Empty;
                lblOCIcBkDate04.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持出・金額)

        /// <summary>
        /// 内容表示(持出・金額)
        /// </summary>
        private void DispOCAmtText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.OCInput> OCInputDataAmt = _itemMgr.OCInputData.Where(x => x.InputMode == HoseiStatus.HoseiInputMode.金額);

            //金額
            IEnumerable<ItemManager.OCInput> OCAmtWait = OCInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ待);
            IEnumerable<ItemManager.OCInput> OCAmtHoryu = OCInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ保留);
            IEnumerable<ItemManager.OCInput> OCAmtInput = OCInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ中);
            IEnumerable<ItemManager.OCInput> OCAmtComp = OCInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.完了);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //金額(当日)
                IEnumerable<ItemManager.OCInput> OCAmtWaitOpeDate = OCAmtWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCInput> OCAmtHoryuOpeDate = OCAmtHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCInput> OCAmtInputOpeDate = OCAmtInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCInput> OCAmtCompOpeDate = OCAmtComp.Where(x => x.ClearingDate == OpeDate);

                //金額・エントリ待
                lblOCAmt01.Text = SearchResultCommon.DispDataFormat(OCAmtWaitOpeDate.Sum(x => x.Count), "#,##0");
                //金額・エントリ保留
                lblOCAmt02.Text = SearchResultCommon.DispDataFormat(OCAmtHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //金額・エントリ中
                lblOCAmt03.Text = SearchResultCommon.DispDataFormat(OCAmtInputOpeDate.Sum(x => x.Count), "#,##0");
                //金額・完了
                lblOCAmt04.Text = SearchResultCommon.DispDataFormat(OCAmtCompOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                //金額(翌日)
                IEnumerable<ItemManager.OCInput> OCAmtWaitAfterDate = OCAmtWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCInput> OCAmtHoryuAfterDate = OCAmtHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCInput> OCAmtInputAfterDate = OCAmtInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCInput> OCAmtCompAfterDate = OCAmtComp.Where(x => x.ClearingDate == AfterDate);

                //金額・エントリ待
                lblOCAmtDate01.Text = SearchResultCommon.DispDataFormat(OCAmtWaitAfterDate.Sum(x => x.Count), "#,##0");
                //金額・エントリ保留
                lblOCAmtDate02.Text = SearchResultCommon.DispDataFormat(OCAmtHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //金額・エントリ中
                lblOCAmtDate03.Text = SearchResultCommon.DispDataFormat(OCAmtInputAfterDate.Sum(x => x.Count), "#,##0");
                //金額・完了
                lblOCAmtDate04.Text = SearchResultCommon.DispDataFormat(OCAmtCompAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量
                IEnumerable<ItemManager.OCInput> OCAmtWaitAll = OCAmtWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCInput> OCAmtHoryuAll = OCAmtHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCInput> OCAmtInputAll = OCAmtInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCInput> OCAmtCompAll = OCAmtComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //金額(全量)
                //金額・エントリ待
                lblOCAmt01.Text = SearchResultCommon.DispDataFormat(OCAmtWaitAll.Sum(x => x.Count), "#,##0");
                //金額・エントリ保留
                lblOCAmt02.Text = SearchResultCommon.DispDataFormat(OCAmtHoryuAll.Sum(x => x.Count), "#,##0");
                //金額・エントリ中
                lblOCAmt03.Text = SearchResultCommon.DispDataFormat(OCAmtInputAll.Sum(x => x.Count), "#,##0");
                //金額・完了
                lblOCAmt04.Text = SearchResultCommon.DispDataFormat(OCAmtCompAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblOCAmtDate01.Text = string.Empty;
                lblOCAmtDate02.Text = string.Empty;
                lblOCAmtDate03.Text = string.Empty;
                lblOCAmtDate04.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持出・完了訂正)

        /// <summary>
        /// 内容表示(持出・完了訂正)
        /// </summary>
        private void DispOCTeiseiText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.OCInput> OCInputDataTeisei = _itemMgr.OCInputData.Where(x => x.InputMode == HoseiStatus.HoseiInputMode.交換尻);

            //完了訂正
            IEnumerable<ItemManager.OCInput> OCTeiseiHoryu = OCInputDataTeisei.Where(x => x.InputSts == HoseiStatus.InputStatus.完了訂正保留);
            IEnumerable<ItemManager.OCInput> OCTeiseiInput = OCInputDataTeisei.Where(x => x.InputSts == HoseiStatus.InputStatus.完了訂正中);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                IEnumerable<ItemManager.OCInput> OCTeiseiHoryuOpeDate = OCTeiseiHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCInput> OCTeiseiInputOpeDate = OCTeiseiInput.Where(x => x.ClearingDate == OpeDate);

                //完了訂正(当日)
                lblOCTeisei01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})", 
                                                    SearchResultCommon.DispDataFormat(OCTeiseiHoryuOpeDate.Sum(x => x.Count), "#,##0"), 
                                                    SearchResultCommon.DispDataFormat(OCTeiseiInputOpeDate.Sum(x => x.Count), "#,##0"));

                // 【翌日】
                IEnumerable<ItemManager.OCInput> OCTeiseiHoryuAfterDate = OCTeiseiHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCInput> OCTeiseiInputAfterDate = OCTeiseiInput.Where(x => x.ClearingDate == AfterDate);

                //完了訂正(翌日)
                lblOCTeiseiDate01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})",
                                                    SearchResultCommon.DispDataFormat(OCTeiseiHoryuAfterDate.Sum(x => x.Count), "#,##0"),
                                                    SearchResultCommon.DispDataFormat(OCTeiseiInputAfterDate.Sum(x => x.Count), "#,##0"));
            }
            else
            {
                // 全量
                IEnumerable<ItemManager.OCInput> OCTeiseiHoryuAll = OCTeiseiHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCInput> OCTeiseiInputAll = OCTeiseiInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //完了訂正(全量)
                lblOCTeisei01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})",
                                                    SearchResultCommon.DispDataFormat(OCTeiseiHoryuAll.Sum(x => x.Count), "#,##0"),
                                                    SearchResultCommon.DispDataFormat(OCTeiseiInputAll.Sum(x => x.Count), "#,##0"));

                //未使用項目
                lblOCTeiseiDate01.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持出・アップロード)

        /// <summary>
        /// 内容表示(持出・アップロード)
        /// </summary>
        private void DispOCUploadText(int OpeDate, int AfterDate)
        {
            //IEnumerable<ItemManager.OCUpload> OCUploadDataKanryou = _itemMgr.OCUploadData.Where(x => x.TeiseiInpuSts == HoseiStatus.InputStatus.完了);
            IEnumerable<ItemManager.OCUpload> OCUploadDataWait = _itemMgr.OCUploadData.Where(x => x.BUASts == TrMei.Sts.未作成 || x.BUASts == TrMei.Sts.再作成対象);
            IEnumerable<ItemManager.OCUpload> OCUploadDataMkFile = _itemMgr.OCUploadData.Where(x => x.BUASts == TrMei.Sts.ファイル作成 || x.BUASts == TrMei.Sts.アップロード);
            IEnumerable<ItemManager.OCUpload> OCUploadDataErr = _itemMgr.OCUploadData.Where(x => x.BUASts == TrMei.Sts.結果エラー);
            IEnumerable<ItemManager.OCUpload> OCUploadDataSeijyou = _itemMgr.OCUploadData.Where(x => x.BUASts == TrMei.Sts.結果正常);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //アップロード(当日)
                IEnumerable<ItemManager.OCUpload> OCUploadDataHoseiOpeDate = _itemMgr.OCUploadData.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCUpload> OCUploadDataWaitOpeDate = OCUploadDataWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCUpload> OCUploadDataMkFileOpeDate = OCUploadDataMkFile.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCUpload> OCUploadDataErrOpeDate = OCUploadDataErr.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCUpload> OCUploadDataSeijyouOpeDate = OCUploadDataSeijyou.Where(x => x.ClearingDate == OpeDate);

                //アップロード・補正完了
                lblOCUpload01.Text = SearchResultCommon.DispDataFormat(OCUploadDataHoseiOpeDate.Sum(x => x.Count), "#,##0");
                //アップロード・作成待
                lblOCUpload02.Text = SearchResultCommon.DispDataFormat(OCUploadDataWaitOpeDate.Sum(x => x.Count), "#,##0");
                //アップロード・結果待
                lblOCUpload03.Text = SearchResultCommon.DispDataFormat(OCUploadDataMkFileOpeDate.Sum(x => x.Count), "#,##0");
                //アップロード・結果エラー
                lblOCUpload04.Text = SearchResultCommon.DispDataFormat(OCUploadDataErrOpeDate.Sum(x => x.Count), "#,##0");
                //アップロード・結果正常
                lblOCUpload05.Text = SearchResultCommon.DispDataFormat(OCUploadDataSeijyouOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                //アップロード(翌日)
                IEnumerable<ItemManager.OCUpload> OCUploadDataHoseiAfterDate = _itemMgr.OCUploadData.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCUpload> OCUploadDataWaitAfterDate = OCUploadDataWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCUpload> OCUploadDataMkFileAfterDate = OCUploadDataMkFile.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCUpload> OCUploadDataErrAfterDate = OCUploadDataErr.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCUpload> OCUploadDataSeijyouAfterDate = OCUploadDataSeijyou.Where(x => x.ClearingDate == AfterDate);

                //アップロード・補正完了
                lblOCUploadDate01.Text = SearchResultCommon.DispDataFormat(OCUploadDataHoseiAfterDate.Sum(x => x.Count), "#,##0");
                //アップロード・作成待
                lblOCUploadDate02.Text = SearchResultCommon.DispDataFormat(OCUploadDataWaitAfterDate.Sum(x => x.Count), "#,##0");
                //アップロード・結果待
                lblOCUploadDate03.Text = SearchResultCommon.DispDataFormat(OCUploadDataMkFileAfterDate.Sum(x => x.Count), "#,##0");
                //アップロード・結果エラー
                lblOCUploadDate04.Text = SearchResultCommon.DispDataFormat(OCUploadDataErrAfterDate.Sum(x => x.Count), "#,##0");
                //アップロード・結果正常
                lblOCUploadDate05.Text = SearchResultCommon.DispDataFormat(OCUploadDataSeijyouAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量
                IEnumerable<ItemManager.OCUpload> OCUploadDataHoseiAll = _itemMgr.OCUploadData.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCUpload> OCUploadDataWaitAll = OCUploadDataWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCUpload> OCUploadDataMkFileAll = OCUploadDataMkFile.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCUpload> OCUploadDataErrAll = OCUploadDataErr.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCUpload> OCUploadDataSeijyouAll = OCUploadDataSeijyou.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //アップロード(全量)
                //アップロード・補正完了
                lblOCUpload01.Text = SearchResultCommon.DispDataFormat(OCUploadDataHoseiAll.Sum(x => x.Count), "#,##0");
                //アップロード・作成待
                lblOCUpload02.Text = SearchResultCommon.DispDataFormat(OCUploadDataWaitAll.Sum(x => x.Count), "#,##0");
                //アップロード・結果待
                lblOCUpload03.Text = SearchResultCommon.DispDataFormat(OCUploadDataMkFileAll.Sum(x => x.Count), "#,##0");
                //アップロード・結果エラー
                lblOCUpload04.Text = SearchResultCommon.DispDataFormat(OCUploadDataErrAll.Sum(x => x.Count), "#,##0");
                //アップロード・結果正常
                lblOCUpload05.Text = SearchResultCommon.DispDataFormat(OCUploadDataSeijyouAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblOCUploadDate01.Text = string.Empty;
                lblOCUploadDate02.Text = string.Empty;
                lblOCUploadDate03.Text = string.Empty;
                lblOCUploadDate04.Text = string.Empty;
                lblOCUploadDate05.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持出・取消)

        /// <summary>
        /// 内容表示(持出・取消)
        /// </summary>
        private void DispOCDeleteUploadText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataWait = _itemMgr.OCDeleteUploadData.Where(x => x.BCASts == TrMei.Sts.作成対象);
            IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataMkFile = _itemMgr.OCDeleteUploadData.Where(x => x.BCASts == TrMei.Sts.ファイル作成 || x.BCASts == TrMei.Sts.アップロード);
            IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataErr = _itemMgr.OCDeleteUploadData.Where(x => x.BCASts == TrMei.Sts.結果エラー);
            IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataComp = _itemMgr.OCDeleteUploadData.Where(x => x.BCASts == TrMei.Sts.結果正常);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataWaitOpeDate = OCDeleteUploadDataWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataMkFileOpeDate = OCDeleteUploadDataMkFile.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataErrOpeDate = OCDeleteUploadDataErr.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataCompOpeDate = OCDeleteUploadDataComp.Where(x => x.ClearingDate == OpeDate);

                //取消・作成待
                lblOCDel01.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataWaitOpeDate.Sum(x => x.Count), "#,##0");
                //取消・結果待
                lblOCDel02.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataMkFileOpeDate.Sum(x => x.Count), "#,##0");
                //取消・結果エラー
                lblOCDel03.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataErrOpeDate.Sum(x => x.Count), "#,##0");
                //取消・結果正常
                lblOCDel04.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataCompOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataWaitAfterDate = OCDeleteUploadDataWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataMkFileAfterDate = OCDeleteUploadDataMkFile.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataErrAfterDate = OCDeleteUploadDataErr.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataCompAfterDate = OCDeleteUploadDataComp.Where(x => x.ClearingDate == AfterDate);

                //取消・作成待
                lblOCDelDate01.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataWaitAfterDate.Sum(x => x.Count), "#,##0");
                //取消・結果待
                lblOCDelDate02.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataMkFileAfterDate.Sum(x => x.Count), "#,##0");
                //取消・結果エラー
                lblOCDelDate03.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataErrAfterDate.Sum(x => x.Count), "#,##0");
                //取消・結果正常
                lblOCDelDate04.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataCompAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataWaitAll = OCDeleteUploadDataWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataMkFileAll = OCDeleteUploadDataMkFile.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataErrAll = OCDeleteUploadDataErr.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.OCDeleteUpload> OCDeleteUploadDataCompAll = OCDeleteUploadDataComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //取消・作成待
                lblOCDel01.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataWaitAll.Sum(x => x.Count), "#,##0");
                //取消・結果待
                lblOCDel02.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataMkFileAll.Sum(x => x.Count), "#,##0");
                //取消・結果エラー
                lblOCDel03.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataErrAll.Sum(x => x.Count), "#,##0");
                //取消・結果正常
                lblOCDel04.Text = SearchResultCommon.DispDataFormat(OCDeleteUploadDataCompAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblOCDelDate01.Text = string.Empty;
                lblOCDelDate02.Text = string.Empty;
                lblOCDelDate03.Text = string.Empty;
                lblOCDelDate04.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・ダウンロード)

        /// <summary>
        /// 内容表示(持帰・ダウンロード)
        /// </summary>
        /// <param name="OpeDate">処理日</param>
        /// <param name="AfterDate">処理日の翌営業日</param>
        /// <param name="BeforeDate">処理日の前営業日</param>
        private void DispICDownLoad(int OpeDate, int AfterDate, int BeforeDate)
        {
            IEnumerable<ItemManager.ICDownLoad> ICDownLoadNotComp = _itemMgr.ICDownLoadData.Where(x => x.KakuteiFlg == 0);
            IEnumerable<ItemManager.ICDownLoad> ICDownLoadComp = _itemMgr.ICDownLoadData.Where(x => x.KakuteiFlg == 1);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】(ClearingDateが前営業日より大きい & 営業日以下)
                IEnumerable<ItemManager.ICDownLoad> ICDownLoadNotCompOpeDate = ICDownLoadNotComp.Where(x => x.ClearingDate > BeforeDate && x.ClearingDate <= OpeDate);
                IEnumerable<ItemManager.ICDownLoad> ICDownLoadCompOpeDate = ICDownLoadComp.Where(x => x.ClearingDate > BeforeDate && x.ClearingDate <= OpeDate);

                //ダウンロード・未確定
                lblICDownloadNotComp.Text = SearchResultCommon.DispDataFormat(ICDownLoadNotCompOpeDate.Sum(x => x.Count), "#,##0");
                //ダウンロード・確定
                lblICDownloadComp.Text = SearchResultCommon.DispDataFormat(ICDownLoadCompOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】(ClearingDateが処理日より大きい & 翌営業日以下)
                IEnumerable<ItemManager.ICDownLoad> ICDownLoadNotCompAfterDate = ICDownLoadNotComp.Where(x => x.ClearingDate > OpeDate && x.ClearingDate <= AfterDate);
                IEnumerable<ItemManager.ICDownLoad> ICDownLoadCompAfterDate = ICDownLoadComp.Where(x => x.ClearingDate > OpeDate && x.ClearingDate <= AfterDate);

                //ダウンロード・未確定
                lblICDownloadNotCompDate.Text = SearchResultCommon.DispDataFormat(ICDownLoadNotCompAfterDate.Sum(x => x.Count), "#,##0");
                //ダウンロード・確定
                lblICDownloadCompDate.Text = SearchResultCommon.DispDataFormat(ICDownLoadCompAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量(ClearingDateが前営業日より大きい または 空[初期値])
                IEnumerable<ItemManager.ICDownLoad> ICDownLoadNotCompAll = ICDownLoadNotComp.Where(x => x.ClearingDate > BeforeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICDownLoad> ICDownLoadCompAll = ICDownLoadComp.Where(x => x.ClearingDate > BeforeDate || x.ClearingDate == 0);

                //ダウンロード・未確定
                lblICDownloadNotComp.Text = SearchResultCommon.DispDataFormat(ICDownLoadNotCompAll.Sum(x => x.Count), "#,##0");
                //ダウンロード・確定
                lblICDownloadComp.Text = SearchResultCommon.DispDataFormat(ICDownLoadCompAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblICDownloadNotCompDate.Text = string.Empty;
                lblICDownloadCompDate.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・持帰銀行)

        /// <summary>
        /// 内容表示(持帰・持帰銀行)
        /// </summary>
        private void DispICICBkText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.ICInput> ICInputDataICBk = _itemMgr.ICInputData.Where(x => x.InputMode == HoseiStatus.HoseiInputMode.持帰銀行);

            //持帰銀行
            IEnumerable<ItemManager.ICInput> ICBkEntWait = ICInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ待);
            IEnumerable<ItemManager.ICInput> ICBkEntHoryu = ICInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ保留);
            IEnumerable<ItemManager.ICInput> ICBkEntInput = ICInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ中);
            IEnumerable<ItemManager.ICInput> ICBkVeriWait = ICInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ待);
            IEnumerable<ItemManager.ICInput> ICBkVeriHoryu = ICInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ保留);
            IEnumerable<ItemManager.ICInput> ICBkVeriInput = ICInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ中);
            IEnumerable<ItemManager.ICInput> ICBkComp = ICInputDataICBk.Where(x => x.InputSts == HoseiStatus.InputStatus.完了);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //持帰銀行(当日)
                IEnumerable<ItemManager.ICInput> ICBkEntWaitOpeDate = ICBkEntWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> ICBkEntHoryuOpeDate = ICBkEntHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> ICBkEntInputOpeDate = ICBkEntInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> ICBkVeriWaitOpeDate = ICBkVeriWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> ICBkVeriHoryuOpeDate = ICBkVeriHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> ICBkVeriInputOpeDate = ICBkVeriInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> ICBkCompOpeDate = ICBkComp.Where(x => x.ClearingDate == OpeDate);

                //持帰銀行・エントリ待
                lblICIcBk01.Text = SearchResultCommon.DispDataFormat(ICBkEntWaitOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ保留
                lblICIcBk02.Text = SearchResultCommon.DispDataFormat(ICBkEntHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ中
                lblICIcBk03.Text = SearchResultCommon.DispDataFormat(ICBkEntInputOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ待
                lblICIcBk04.Text = SearchResultCommon.DispDataFormat(ICBkVeriWaitOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ保留
                lblICIcBk05.Text = SearchResultCommon.DispDataFormat(ICBkVeriHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ中
                lblICIcBk06.Text = SearchResultCommon.DispDataFormat(ICBkVeriInputOpeDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・完了
                lblICIcBk07.Text = SearchResultCommon.DispDataFormat(ICBkCompOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                //持帰銀行(翌日)
                IEnumerable<ItemManager.ICInput> ICBkEntWaitAfterDate = ICBkEntWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> ICBkEntHoryuAfterDate = ICBkEntHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> ICBkEntInputAfterDate = ICBkEntInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> ICBkVeriWaitAfterDate = ICBkVeriWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> ICBkVeriHoryuAfterDate = ICBkVeriHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> ICBkVeriInputAfterDate = ICBkVeriInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> ICBkCompAfterDate = ICBkComp.Where(x => x.ClearingDate == AfterDate);

                //持帰銀行・エントリ待
                lblICIcBkDate01.Text = SearchResultCommon.DispDataFormat(ICBkEntWaitAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ保留
                lblICIcBkDate02.Text = SearchResultCommon.DispDataFormat(ICBkEntHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ中
                lblICIcBkDate03.Text = SearchResultCommon.DispDataFormat(ICBkEntInputAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ待
                lblICIcBkDate04.Text = SearchResultCommon.DispDataFormat(ICBkVeriWaitAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ保留
                lblICIcBkDate05.Text = SearchResultCommon.DispDataFormat(ICBkVeriHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ中
                lblICIcBkDate06.Text = SearchResultCommon.DispDataFormat(ICBkVeriInputAfterDate.Sum(x => x.Count), "#,##0");
                //持帰銀行・完了
                lblICIcBkDate07.Text = SearchResultCommon.DispDataFormat(ICBkCompAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量

                //持帰銀行(全量)
                IEnumerable<ItemManager.ICInput> ICBkEntWaitAll = ICBkEntWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> ICBkEntHoryuAll = ICBkEntHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> ICBkEntInputAll = ICBkEntInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> ICBkVeriWaitAll = ICBkVeriWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> ICBkVeriHoryuAll = ICBkVeriHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> ICBkVeriInputAll = ICBkVeriInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> ICBkCompAll = ICBkComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //持帰銀行・エントリ待
                lblICIcBk01.Text = SearchResultCommon.DispDataFormat(ICBkEntWaitAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ保留
                lblICIcBk02.Text = SearchResultCommon.DispDataFormat(ICBkEntHoryuAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・エントリ中
                lblICIcBk03.Text = SearchResultCommon.DispDataFormat(ICBkEntInputAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ待
                lblICIcBk04.Text = SearchResultCommon.DispDataFormat(ICBkVeriWaitAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ保留
                lblICIcBk05.Text = SearchResultCommon.DispDataFormat(ICBkVeriHoryuAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・ベリ中
                lblICIcBk06.Text = SearchResultCommon.DispDataFormat(ICBkVeriInputAll.Sum(x => x.Count), "#,##0");
                //持帰銀行・完了
                lblICIcBk07.Text = SearchResultCommon.DispDataFormat(ICBkCompAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblICIcBkDate01.Text = string.Empty;
                lblICIcBkDate02.Text = string.Empty;
                lblICIcBkDate03.Text = string.Empty;
                lblICIcBkDate04.Text = string.Empty;
                lblICIcBkDate05.Text = string.Empty;
                lblICIcBkDate06.Text = string.Empty;
                lblICIcBkDate07.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・交換日)

        /// <summary>
        /// 内容表示(持帰・交換日)
        /// </summary>
        private void DispICCDateText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.ICInput> ICInputDataCDate = _itemMgr.ICInputData.Where(x => x.InputMode == HoseiStatus.HoseiInputMode.交換希望日);

            //交換日
            IEnumerable<ItemManager.ICInput> CDateEntWait = ICInputDataCDate.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ待);
            IEnumerable<ItemManager.ICInput> CDateEntHoryu = ICInputDataCDate.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ保留);
            IEnumerable<ItemManager.ICInput> CDateEntInput = ICInputDataCDate.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ中);
            IEnumerable<ItemManager.ICInput> CDateVeriWait = ICInputDataCDate.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ待);
            IEnumerable<ItemManager.ICInput> CDateVeriHoryu = ICInputDataCDate.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ保留);
            IEnumerable<ItemManager.ICInput> CDateVeriInput = ICInputDataCDate.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ中);
            IEnumerable<ItemManager.ICInput> CDateComp = ICInputDataCDate.Where(x => x.InputSts == HoseiStatus.InputStatus.完了);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //交換日(当日)
                IEnumerable<ItemManager.ICInput> CDateEntWaitOpeDate = CDateEntWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> CDateEntHoryuOpeDate = CDateEntHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> CDateEntInputOpeDate = CDateEntInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> CDateVeriWaitOpeDate = CDateVeriWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> CDateVeriHoryuOpeDate = CDateVeriHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> CDateVeriInputOpeDate = CDateVeriInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> CDateCompOpeDate = CDateComp.Where(x => x.ClearingDate == OpeDate);

                //交換日・エントリ待
                lblICCD01.Text = SearchResultCommon.DispDataFormat(CDateEntWaitOpeDate.Sum(x => x.Count), "#,##0");
                //交換日・エントリ保留
                lblICCD02.Text = SearchResultCommon.DispDataFormat(CDateEntHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //交換日・エントリ中
                lblICCD03.Text = SearchResultCommon.DispDataFormat(CDateEntInputOpeDate.Sum(x => x.Count), "#,##0");
                //交換日・ベリ待
                lblICCD04.Text = SearchResultCommon.DispDataFormat(CDateVeriWaitOpeDate.Sum(x => x.Count), "#,##0");
                //交換日・ベリ保留
                lblICCD05.Text = SearchResultCommon.DispDataFormat(CDateVeriHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //交換日・ベリ中
                lblICCD06.Text = SearchResultCommon.DispDataFormat(CDateVeriInputOpeDate.Sum(x => x.Count), "#,##0");
                //交換日・完了
                lblICCD07.Text = SearchResultCommon.DispDataFormat(CDateCompOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                //交換日(翌日)
                IEnumerable<ItemManager.ICInput> CDateEntWaitAfterDate = CDateEntWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> CDateEntHoryuAfterDate = CDateEntHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> CDateEntInputAfterDate = CDateEntInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> CDateVeriWaitAfterDate = CDateVeriWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> CDateVeriHoryuAfterDate = CDateVeriHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> CDateVeriInputAfterDate = CDateVeriInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> CDateCompAfterDate = CDateComp.Where(x => x.ClearingDate == AfterDate);

                //交換日・エントリ待
                lblICCDDate01.Text = SearchResultCommon.DispDataFormat(CDateEntWaitAfterDate.Sum(x => x.Count), "#,##0");
                //交換日・エントリ保留
                lblICCDDate02.Text = SearchResultCommon.DispDataFormat(CDateEntHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //交換日・エントリ中
                lblICCDDate03.Text = SearchResultCommon.DispDataFormat(CDateEntInputAfterDate.Sum(x => x.Count), "#,##0");
                //交換日・ベリ待
                lblICCDDate04.Text = SearchResultCommon.DispDataFormat(CDateVeriWaitAfterDate.Sum(x => x.Count), "#,##0");
                //交換日・ベリ保留
                lblICCDDate05.Text = SearchResultCommon.DispDataFormat(CDateVeriHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //交換日・ベリ中
                lblICCDDate06.Text = SearchResultCommon.DispDataFormat(CDateVeriInputAfterDate.Sum(x => x.Count), "#,##0");
                //交換日・完了
                lblICCDDate07.Text = SearchResultCommon.DispDataFormat(CDateCompAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量

                //交換日(全量)
                IEnumerable<ItemManager.ICInput> CDateEntWaitAll = CDateEntWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> CDateEntHoryuAll = CDateEntHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> CDateEntInputAll = CDateEntInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> CDateVeriWaitAll = CDateVeriWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> CDateVeriHoryuAll = CDateVeriHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> CDateVeriInputAll = CDateVeriInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> CDateCompAll = CDateComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //交換日・エントリ待
                lblICCD01.Text = SearchResultCommon.DispDataFormat(CDateEntWaitAll.Sum(x => x.Count), "#,##0");
                //交換日・エントリ保留
                lblICCD02.Text = SearchResultCommon.DispDataFormat(CDateEntHoryuAll.Sum(x => x.Count), "#,##0");
                //交換日・エントリ中
                lblICCD03.Text = SearchResultCommon.DispDataFormat(CDateEntInputAll.Sum(x => x.Count), "#,##0");
                //交換日・ベリ待
                lblICCD04.Text = SearchResultCommon.DispDataFormat(CDateVeriWaitAll.Sum(x => x.Count), "#,##0");
                //交換日・ベリ保留
                lblICCD05.Text = SearchResultCommon.DispDataFormat(CDateVeriHoryuAll.Sum(x => x.Count), "#,##0");
                //交換日・ベリ中
                lblICCD06.Text = SearchResultCommon.DispDataFormat(CDateVeriInputAll.Sum(x => x.Count), "#,##0");
                //交換日・完了
                lblICCD07.Text = SearchResultCommon.DispDataFormat(CDateCompAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblICCDDate01.Text = string.Empty;
                lblICCDDate02.Text = string.Empty;
                lblICCDDate03.Text = string.Empty;
                lblICCDDate04.Text = string.Empty;
                lblICCDDate05.Text = string.Empty;
                lblICCDDate06.Text = string.Empty;
                lblICCDDate07.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・金額)

        /// <summary>
        /// 内容表示(持帰・金額)
        /// </summary>
        private void DispICAmtText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.ICInput> ICInputDataAmt = _itemMgr.ICInputData.Where(x => x.InputMode == HoseiStatus.HoseiInputMode.金額);

            //金額
            IEnumerable<ItemManager.ICInput> AmtEntWait = ICInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ待);
            IEnumerable<ItemManager.ICInput> AmtEntHoryu = ICInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ保留);
            IEnumerable<ItemManager.ICInput> AmtEntInput = ICInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ中);
            IEnumerable<ItemManager.ICInput> AmtVeriWait = ICInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ待);
            IEnumerable<ItemManager.ICInput> AmtVeriHoryu = ICInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ保留);
            IEnumerable<ItemManager.ICInput> AmtVeriInput = ICInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ中);
            IEnumerable<ItemManager.ICInput> AmtComp = ICInputDataAmt.Where(x => x.InputSts == HoseiStatus.InputStatus.完了);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //金額(当日)
                IEnumerable<ItemManager.ICInput> AmtEntWaitOpeDate = AmtEntWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> AmtEntHoryuOpeDate = AmtEntHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> AmtEntInputOpeDate = AmtEntInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> AmtVeriWaitOpeDate = AmtVeriWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> AmtVeriHoryuOpeDate = AmtVeriHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> AmtVeriInputOpeDate = AmtVeriInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> AmtCompOpeDate = AmtComp.Where(x => x.ClearingDate == OpeDate);

                //金額・エントリ待
                lblICAmt01.Text = SearchResultCommon.DispDataFormat(AmtEntWaitOpeDate.Sum(x => x.Count), "#,##0");
                //金額・エントリ保留
                lblICAmt02.Text = SearchResultCommon.DispDataFormat(AmtEntHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //金額・エントリ中
                lblICAmt03.Text = SearchResultCommon.DispDataFormat(AmtEntInputOpeDate.Sum(x => x.Count), "#,##0");
                //金額・ベリ待
                lblICAmt04.Text = SearchResultCommon.DispDataFormat(AmtVeriWaitOpeDate.Sum(x => x.Count), "#,##0");
                //金額・ベリ保留
                lblICAmt05.Text = SearchResultCommon.DispDataFormat(AmtVeriHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //金額・ベリ中
                lblICAmt06.Text = SearchResultCommon.DispDataFormat(AmtVeriInputOpeDate.Sum(x => x.Count), "#,##0");
                //金額・完了
                lblICAmt07.Text = SearchResultCommon.DispDataFormat(AmtCompOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                //金額(翌日)
                IEnumerable<ItemManager.ICInput> AmtEntWaitAfterDate = AmtEntWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> AmtEntHoryuAfterDate = AmtEntHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> AmtEntInputAfterDate = AmtEntInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> AmtVeriWaitAfterDate = AmtVeriWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> AmtVeriHoryuAfterDate = AmtVeriHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> AmtVeriInputAfterDate = AmtVeriInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> AmtCompAfterDate = AmtComp.Where(x => x.ClearingDate == AfterDate);

                //金額・エントリ待
                lblICAmtDate01.Text = SearchResultCommon.DispDataFormat(AmtEntWaitAfterDate.Sum(x => x.Count), "#,##0");
                //金額・エントリ保留
                lblICAmtDate02.Text = SearchResultCommon.DispDataFormat(AmtEntHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //金額・エントリ中
                lblICAmtDate03.Text = SearchResultCommon.DispDataFormat(AmtEntInputAfterDate.Sum(x => x.Count), "#,##0");
                //金額・ベリ待
                lblICAmtDate04.Text = SearchResultCommon.DispDataFormat(AmtVeriWaitAfterDate.Sum(x => x.Count), "#,##0");
                //金額・ベリ保留
                lblICAmtDate05.Text = SearchResultCommon.DispDataFormat(AmtVeriHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //金額・ベリ中
                lblICAmtDate06.Text = SearchResultCommon.DispDataFormat(AmtVeriInputAfterDate.Sum(x => x.Count), "#,##0");
                //金額・完了
                lblICAmtDate07.Text = SearchResultCommon.DispDataFormat(AmtCompAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量

                //金額(全量)
                IEnumerable<ItemManager.ICInput> AmtEntWaitAll = AmtEntWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> AmtEntHoryuAll = AmtEntHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> AmtEntInputAll = AmtEntInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> AmtVeriWaitAll = AmtVeriWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> AmtVeriHoryuAll = AmtVeriHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> AmtVeriInputAll = AmtVeriInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> AmtCompAll = AmtComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //金額・エントリ待
                lblICAmt01.Text = SearchResultCommon.DispDataFormat(AmtEntWaitAll.Sum(x => x.Count), "#,##0");
                //金額・エントリ保留
                lblICAmt02.Text = SearchResultCommon.DispDataFormat(AmtEntHoryuAll.Sum(x => x.Count), "#,##0");
                //金額・エントリ中
                lblICAmt03.Text = SearchResultCommon.DispDataFormat(AmtEntInputAll.Sum(x => x.Count), "#,##0");
                //金額・ベリ待
                lblICAmt04.Text = SearchResultCommon.DispDataFormat(AmtVeriWaitAll.Sum(x => x.Count), "#,##0");
                //金額・ベリ保留
                lblICAmt05.Text = SearchResultCommon.DispDataFormat(AmtVeriHoryuAll.Sum(x => x.Count), "#,##0");
                //金額・ベリ中
                lblICAmt06.Text = SearchResultCommon.DispDataFormat(AmtVeriInputAll.Sum(x => x.Count), "#,##0");
                //金額・完了
                lblICAmt07.Text = SearchResultCommon.DispDataFormat(AmtCompAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblICAmtDate01.Text = string.Empty;
                lblICAmtDate02.Text = string.Empty;
                lblICAmtDate03.Text = string.Empty;
                lblICAmtDate04.Text = string.Empty;
                lblICAmtDate05.Text = string.Empty;
                lblICAmtDate06.Text = string.Empty;
                lblICAmtDate07.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・完了訂正)

        /// <summary>
        /// 内容表示(持帰・完了訂正)
        /// </summary>
        private void DispICTeiseiText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.ICInput> ICInputDataTeisei = _itemMgr.ICInputData.Where(x => x.InputMode == HoseiStatus.HoseiInputMode.交換尻);

            //完了訂正
            IEnumerable<ItemManager.ICInput> ICTeiseiHoryu = ICInputDataTeisei.Where(x => x.InputSts == HoseiStatus.InputStatus.完了訂正保留);
            IEnumerable<ItemManager.ICInput> ICTeiseiInput = ICInputDataTeisei.Where(x => x.InputSts == HoseiStatus.InputStatus.完了訂正中);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                IEnumerable<ItemManager.ICInput> ICTeiseiHoryuOpeDate = ICTeiseiHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> ICTeiseiInputOpeDate = ICTeiseiInput.Where(x => x.ClearingDate == OpeDate);


                //完了訂正(当日)
                lblICTeisei01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})",
                                                    SearchResultCommon.DispDataFormat(ICTeiseiHoryuOpeDate.Sum(x => x.Count), "#,##0"),
                                                    SearchResultCommon.DispDataFormat(ICTeiseiInputOpeDate.Sum(x => x.Count), "#,##0"));

                // 【翌日】
                IEnumerable<ItemManager.ICInput> ICTeiseiHoryuAfterDate = ICTeiseiHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> ICTeiseiInputAfterDate = ICTeiseiInput.Where(x => x.ClearingDate == AfterDate);

                //完了訂正(翌日)
                lblICTeiseiDate01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})",
                                                    SearchResultCommon.DispDataFormat(ICTeiseiHoryuAfterDate.Sum(x => x.Count), "#,##0"),
                                                    SearchResultCommon.DispDataFormat(ICTeiseiInputAfterDate.Sum(x => x.Count), "#,##0"));
            }
            else
            {
                // 全量
                IEnumerable<ItemManager.ICInput> ICTeiseiHoryuAll = ICTeiseiHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> ICTeiseiInputAll = ICTeiseiInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);


                //完了訂正(当日)
                lblICTeisei01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})",
                                                    SearchResultCommon.DispDataFormat(ICTeiseiHoryuAll.Sum(x => x.Count), "#,##0"),
                                                    SearchResultCommon.DispDataFormat(ICTeiseiInputAll.Sum(x => x.Count), "#,##0"));

                //未使用項目
                lblICTeiseiDate01.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・自行)

        /// <summary>
        /// 内容表示(持帰・自行)
        /// </summary>
        private void DispICJikouText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.ICInput> ICInputDataJikou = _itemMgr.ICInputData.Where(x => x.InputMode == HoseiStatus.HoseiInputMode.自行情報);

            //自行
            IEnumerable<ItemManager.ICInput> JikouEntWait = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ待);
            IEnumerable<ItemManager.ICInput> JikouEntHoryu = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ保留);
            IEnumerable<ItemManager.ICInput> JikouEntInput = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.エントリ中);
            IEnumerable<ItemManager.ICInput> JikouVeriWait = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ待);
            IEnumerable<ItemManager.ICInput> JikouVeriHoryu = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ保留);
            IEnumerable<ItemManager.ICInput> JikouVeriInput = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.ベリファイ中);
            IEnumerable<ItemManager.ICInput> JikouComp = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.完了 || 
                                                                                     x.InputSts == HoseiStatus.InputStatus.完了訂正保留 || 
                                                                                     x.InputSts == HoseiStatus.InputStatus.完了訂正中);
            IEnumerable<ItemManager.ICInput> JikouCompHoryu = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.完了訂正保留);
            IEnumerable<ItemManager.ICInput> JikouCompInput = ICInputDataJikou.Where(x => x.InputSts == HoseiStatus.InputStatus.完了訂正中);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //自行(当日)
                IEnumerable<ItemManager.ICInput> JikouEntWaitOpeDate = JikouEntWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> JikouEntHoryuOpeDate = JikouEntHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> JikouEntInputOpeDate = JikouEntInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> JikouVeriWaitOpeDate = JikouVeriWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> JikouVeriHoryuOpeDate = JikouVeriHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> JikouVeriInputOpeDate = JikouVeriInput.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> JikouCompOpeDate = JikouComp.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> JikouCompHoryuOpeDate = JikouCompHoryu.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICInput> JikouCompInputOpeDate = JikouCompInput.Where(x => x.ClearingDate == OpeDate);

                //自行・エントリ待
                lblICJikou01.Text = SearchResultCommon.DispDataFormat(JikouEntWaitOpeDate.Sum(x => x.Count), "#,##0");
                //自行・エントリ保留
                lblICJikou02.Text = SearchResultCommon.DispDataFormat(JikouEntHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //自行・エントリ中
                lblICJikou03.Text = SearchResultCommon.DispDataFormat(JikouEntInputOpeDate.Sum(x => x.Count), "#,##0");
                //自行・ベリ待
                lblICJikou04.Text = SearchResultCommon.DispDataFormat(JikouVeriWaitOpeDate.Sum(x => x.Count), "#,##0");
                //自行・ベリ保留
                lblICJikou05.Text = SearchResultCommon.DispDataFormat(JikouVeriHoryuOpeDate.Sum(x => x.Count), "#,##0");
                //自行・ベリ中
                lblICJikou06.Text = SearchResultCommon.DispDataFormat(JikouVeriInputOpeDate.Sum(x => x.Count), "#,##0");
                //自行・完了
                lblICJikou07.Text = SearchResultCommon.DispDataFormat(JikouCompOpeDate.Sum(x => x.Count), "#,##0");
                //自行・完了訂正
                lblICJikouTeisei01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})",
                                                    SearchResultCommon.DispDataFormat(JikouCompHoryuOpeDate.Sum(x => x.Count), "#,##0"),
                                                    SearchResultCommon.DispDataFormat(JikouCompInputOpeDate.Sum(x => x.Count), "#,##0"));

                // 【翌日】
                //自行(翌日)
                IEnumerable<ItemManager.ICInput> JikouEntWaitAfterDate = JikouEntWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> JikouEntHoryuAfterDate = JikouEntHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> JikouEntInputAfterDate = JikouEntInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> JikouVeriWaitAfterDate = JikouVeriWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> JikouVeriHoryuAfterDate = JikouVeriHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> JikouVeriInputAfterDate = JikouVeriInput.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> JikouCompAfterDate = JikouComp.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> JikouCompHoryuAfterDate = JikouCompHoryu.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICInput> JikouCompInputAfterDate = JikouCompInput.Where(x => x.ClearingDate == AfterDate);

                //自行・エントリ待
                lblICJikouDate01.Text = SearchResultCommon.DispDataFormat(JikouEntWaitAfterDate.Sum(x => x.Count), "#,##0");
                //自行・エントリ保留
                lblICJikouDate02.Text = SearchResultCommon.DispDataFormat(JikouEntHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //自行・エントリ中
                lblICJikouDate03.Text = SearchResultCommon.DispDataFormat(JikouEntInputAfterDate.Sum(x => x.Count), "#,##0");
                //自行・ベリ待
                lblICJikouDate04.Text = SearchResultCommon.DispDataFormat(JikouVeriWaitAfterDate.Sum(x => x.Count), "#,##0");
                //自行・ベリ保留
                lblICJikouDate05.Text = SearchResultCommon.DispDataFormat(JikouVeriHoryuAfterDate.Sum(x => x.Count), "#,##0");
                //自行・ベリ中
                lblICJikouDate06.Text = SearchResultCommon.DispDataFormat(JikouVeriInputAfterDate.Sum(x => x.Count), "#,##0");
                //自行・完了
                lblICJikouDate07.Text = SearchResultCommon.DispDataFormat(JikouCompAfterDate.Sum(x => x.Count), "#,##0");
                //自行・完了訂正
                lblICJikouTeiseiDate01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})",
                                                    SearchResultCommon.DispDataFormat(JikouCompHoryuAfterDate.Sum(x => x.Count), "#,##0"),
                                                    SearchResultCommon.DispDataFormat(JikouCompInputAfterDate.Sum(x => x.Count), "#,##0"));
            }
            else
            {
                // 全量

                //自行(全量)
                IEnumerable<ItemManager.ICInput> JikouEntWaitAll = JikouEntWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> JikouEntHoryuAll = JikouEntHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> JikouEntInputAll = JikouEntInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> JikouVeriWaitAll = JikouVeriWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> JikouVeriHoryuAll = JikouVeriHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> JikouVeriInputAll = JikouVeriInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> JikouCompAll = JikouComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> JikouCompHoryuAll = JikouCompHoryu.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICInput> JikouCompInputAll = JikouCompInput.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //自行・エントリ待
                lblICJikou01.Text = SearchResultCommon.DispDataFormat(JikouEntWaitAll.Sum(x => x.Count), "#,##0");
                //自行・エントリ保留
                lblICJikou02.Text = SearchResultCommon.DispDataFormat(JikouEntHoryuAll.Sum(x => x.Count), "#,##0");
                //自行・エントリ中
                lblICJikou03.Text = SearchResultCommon.DispDataFormat(JikouEntInputAll.Sum(x => x.Count), "#,##0");
                //自行・ベリ待
                lblICJikou04.Text = SearchResultCommon.DispDataFormat(JikouVeriWaitAll.Sum(x => x.Count), "#,##0");
                //自行・ベリ保留
                lblICJikou05.Text = SearchResultCommon.DispDataFormat(JikouVeriHoryuAll.Sum(x => x.Count), "#,##0");
                //自行・ベリ中
                lblICJikou06.Text = SearchResultCommon.DispDataFormat(JikouVeriInputAll.Sum(x => x.Count), "#,##0");
                //自行・完了
                lblICJikou07.Text = SearchResultCommon.DispDataFormat(JikouCompAll.Sum(x => x.Count), "#,##0");
                //自行・完了訂正
                lblICJikouTeisei01.Text = string.Format("(完了保留：{0} 完了訂正中：{1})",
                                                    SearchResultCommon.DispDataFormat(JikouCompHoryuAll.Sum(x => x.Count), "#,##0"),
                                                    SearchResultCommon.DispDataFormat(JikouCompInputAll.Sum(x => x.Count), "#,##0"));

                //未使用項目
                lblICJikouDate01.Text = string.Empty;
                lblICJikouDate02.Text = string.Empty;
                lblICJikouDate03.Text = string.Empty;
                lblICJikouDate04.Text = string.Empty;
                lblICJikouDate05.Text = string.Empty;
                lblICJikouDate06.Text = string.Empty;
                lblICJikouDate07.Text = string.Empty;
                lblICJikouTeiseiDate01.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・訂正データ)

        /// <summary>
        /// 内容表示(持帰・訂正データ)
        /// </summary>
        private void DispICTeiseiDataText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.ICTeisei> ICTeiseiWait = _itemMgr.ICWaitTeiseiData;
            IEnumerable<ItemManager.ICTeisei> ICTeiseiWaitComp = _itemMgr.ICWaitTeiseiData.Where(x => x.GMASts == TrMei.Sts.結果正常);
            IEnumerable<ItemManager.ICTeisei> ICTeiseiMkFile = _itemMgr.ICTeiseiData.Where(x => x.GMASts == TrMei.Sts.ファイル作成 || x.GMASts == TrMei.Sts.アップロード);
            IEnumerable<ItemManager.ICTeisei> ICTeiseiErr = _itemMgr.ICTeiseiData.Where(x => x.GMASts == TrMei.Sts.結果エラー);
            IEnumerable<ItemManager.ICTeisei> ICTeiseiComp = _itemMgr.ICTeiseiData.Where(x => x.GMASts == TrMei.Sts.結果正常);

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //訂正データ(当日)
                IEnumerable<ItemManager.ICTeisei> ICTeiseiWaitOpeDate = ICTeiseiWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiWaitCompOpeDate = ICTeiseiWaitComp.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiMkFileOpeDate = ICTeiseiMkFile.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiErrOpeDate = ICTeiseiErr.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiCompOpeDate = ICTeiseiComp.Where(x => x.ClearingDate == OpeDate);

                //訂正データ・作成待
                lblICUploadTeisei01.Text = SearchResultCommon.DispDataFormat(ICTeiseiWaitOpeDate.Sum(x => x.Count), "#,##0");
                //訂正データ・結果待
                lblICUploadTeisei02.Text = SearchResultCommon.DispDataFormat(ICTeiseiMkFileOpeDate.Sum(x => x.Count), "#,##0");
                //訂正データ・結果エラー
                lblICUploadTeisei03.Text = SearchResultCommon.DispDataFormat(ICTeiseiErrOpeDate.Sum(x => x.Count), "#,##0");
                //訂正データ・結果正常(結果正常だが作成待対象のデータは除外)
                lblICUploadTeisei04.Text = SearchResultCommon.DispDataFormat(ICTeiseiCompOpeDate.Sum(x => x.Count) - ICTeiseiWaitCompOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                //訂正データ(翌日)
                IEnumerable<ItemManager.ICTeisei> ICTeiseiWaitAfterDate = ICTeiseiWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiWaitCompAfterDate = ICTeiseiWaitComp.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiMkFileAfterDate = ICTeiseiMkFile.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiErrAfterDate = ICTeiseiErr.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiCompAfterDate = ICTeiseiComp.Where(x => x.ClearingDate == AfterDate);

                //訂正データ・作成待
                lblICUploadTeiseiDate01.Text = SearchResultCommon.DispDataFormat(ICTeiseiWaitAfterDate.Sum(x => x.Count), "#,##0");
                //訂正データ・結果待
                lblICUploadTeiseiDate02.Text = SearchResultCommon.DispDataFormat(ICTeiseiMkFileAfterDate.Sum(x => x.Count), "#,##0");
                //訂正データ・結果エラー
                lblICUploadTeiseiDate03.Text = SearchResultCommon.DispDataFormat(ICTeiseiErrAfterDate.Sum(x => x.Count), "#,##0");
                //訂正データ・結果正常(結果正常だが作成待対象のデータは除外)
                lblICUploadTeiseiDate04.Text = SearchResultCommon.DispDataFormat(ICTeiseiCompAfterDate.Sum(x => x.Count) - ICTeiseiWaitCompAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量

                //訂正データ(全量)
                IEnumerable<ItemManager.ICTeisei> ICTeiseiWaitAll = ICTeiseiWait.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiWaitCompAll = ICTeiseiWaitComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiMkFileAll = ICTeiseiMkFile.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiErrAll = ICTeiseiErr.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);
                IEnumerable<ItemManager.ICTeisei> ICTeiseiCompAll = ICTeiseiComp.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //訂正データ・作成待
                lblICUploadTeisei01.Text = SearchResultCommon.DispDataFormat(ICTeiseiWaitAll.Sum(x => x.Count), "#,##0");
                //訂正データ・結果待
                lblICUploadTeisei02.Text = SearchResultCommon.DispDataFormat(ICTeiseiMkFileAll.Sum(x => x.Count), "#,##0");
                //訂正データ・結果エラー
                lblICUploadTeisei03.Text = SearchResultCommon.DispDataFormat(ICTeiseiErrAll.Sum(x => x.Count), "#,##0");
                //訂正データ・結果正常(結果正常だが作成待対象のデータは除外)
                lblICUploadTeisei04.Text = SearchResultCommon.DispDataFormat(ICTeiseiCompAll.Sum(x => x.Count) - ICTeiseiWaitCompAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblICUploadTeiseiDate01.Text = string.Empty;
                lblICUploadTeiseiDate02.Text = string.Empty;
                lblICUploadTeiseiDate03.Text = string.Empty;
                lblICUploadTeiseiDate04.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・削除)

        /// <summary>
        /// 内容表示(持帰・削除)
        /// </summary>
        private void DispICDeleteText(int OpeDate, int AfterDate)
        {

            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                IEnumerable<ItemManager.ICDelete> ICDeleteDataOpeDate = _itemMgr.ICDeleteData.Where(x => x.ClearingDate == OpeDate);

                //削除済
                lblICDel01.Text = SearchResultCommon.DispDataFormat(ICDeleteDataOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                IEnumerable<ItemManager.ICDelete> ICDeleteDataAfterDate = _itemMgr.ICDeleteData.Where(x => x.ClearingDate == AfterDate);

                //削除済
                lblICDelDate01.Text = SearchResultCommon.DispDataFormat(ICDeleteDataAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量
                IEnumerable<ItemManager.ICDelete> ICDeleteDataAll = _itemMgr.ICDeleteData.Where(x => x.ClearingDate >= OpeDate || x.ClearingDate == 0);

                //削除済
                lblICDel01.Text = SearchResultCommon.DispDataFormat(ICDeleteDataAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblICDelDate01.Text = string.Empty;
            }
        }

        #endregion

        #region 表示(持帰・不渡)

        /// <summary>
        /// 内容表示(持帰・不渡)
        /// </summary>
        private void DispICFuwatariText(int OpeDate, int AfterDate)
        {
            IEnumerable<ItemManager.ICFuwatari> ICFuwatariWait = _itemMgr.ICFuwatariData.Where(x => x.GRASts == TrMei.Sts.未作成 || x.GRASts == TrMei.Sts.再作成対象);
            IEnumerable<ItemManager.ICFuwatari> ICFuwatariMkFile = _itemMgr.ICFuwatariData.Where(x => x.GRASts == TrMei.Sts.ファイル作成 || x.GRASts == TrMei.Sts.アップロード);
            IEnumerable<ItemManager.ICFuwatari> ICFuwatariErr = _itemMgr.ICFuwatariData.Where(x => x.GRASts == TrMei.Sts.結果エラー);
            IEnumerable<ItemManager.ICFuwatari> ICFuwatariSeijyou = _itemMgr.ICFuwatariData.Where(x => x.GRASts == TrMei.Sts.結果正常);


            if (_curFlg == DispMode.DATE)
            {
                // 日付指定

                // 【当日】
                //不渡(当日)
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariWaitOpeDate = ICFuwatariWait.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariMkFileOpeDate = ICFuwatariMkFile.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariErrOpeDate = ICFuwatariErr.Where(x => x.ClearingDate == OpeDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariSeijyouOpeDate = ICFuwatariSeijyou.Where(x => x.ClearingDate == OpeDate);

                //不渡返還・作成待
                lblICFuwatari01.Text = SearchResultCommon.DispDataFormat(ICFuwatariWaitOpeDate.Sum(x => x.Count), "#,##0");
                //不渡返還・結果待
                lblICFuwatari02.Text = SearchResultCommon.DispDataFormat(ICFuwatariMkFileOpeDate.Sum(x => x.Count), "#,##0");
                //不渡返還・結果エラー
                lblICFuwatari03.Text = SearchResultCommon.DispDataFormat(ICFuwatariErrOpeDate.Sum(x => x.Count), "#,##0");
                //不渡返還・結果正常
                lblICFuwatari04.Text = SearchResultCommon.DispDataFormat(ICFuwatariSeijyouOpeDate.Sum(x => x.Count), "#,##0");

                // 【翌日】
                //不渡(翌日)
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariWaitAfterDate = ICFuwatariWait.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariMkFileAfterDate = ICFuwatariMkFile.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariErrAfterDate = ICFuwatariErr.Where(x => x.ClearingDate == AfterDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariSeijyouAfterDate = ICFuwatariSeijyou.Where(x => x.ClearingDate == AfterDate);

                //不渡返還・作成待
                lblICFuwatariDate01.Text = SearchResultCommon.DispDataFormat(ICFuwatariWaitAfterDate.Sum(x => x.Count), "#,##0");
                //不渡返還・結果待
                lblICFuwatariDate02.Text = SearchResultCommon.DispDataFormat(ICFuwatariMkFileAfterDate.Sum(x => x.Count), "#,##0");
                //不渡返還・結果エラー
                lblICFuwatariDate03.Text = SearchResultCommon.DispDataFormat(ICFuwatariErrAfterDate.Sum(x => x.Count), "#,##0");
                //不渡返還・結果正常
                lblICFuwatariDate04.Text = SearchResultCommon.DispDataFormat(ICFuwatariSeijyouAfterDate.Sum(x => x.Count), "#,##0");
            }
            else
            {
                // 全量(不渡返還登録日が本日以降すべて[一応削除日が本日以降も])
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariWaitAll = ICFuwatariWait.Where(x => x.FuwatariEYMD >= OpeDate || x.FuwatariDelDate >= OpeDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariMkFileAll = ICFuwatariMkFile.Where(x => x.FuwatariEYMD >= OpeDate || x.FuwatariDelDate >= OpeDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariErrAll = ICFuwatariErr.Where(x => x.FuwatariEYMD >= OpeDate || x.FuwatariDelDate >= OpeDate);
                IEnumerable<ItemManager.ICFuwatari> ICFuwatariSeijyouAll = ICFuwatariSeijyou.Where(x => x.FuwatariEYMD >= OpeDate || x.FuwatariDelDate >= OpeDate);

                //不渡返還・作成待
                lblICFuwatari01.Text = SearchResultCommon.DispDataFormat(ICFuwatariWaitAll.Sum(x => x.Count), "#,##0");
                //不渡返還・結果待
                lblICFuwatari02.Text = SearchResultCommon.DispDataFormat(ICFuwatariMkFileAll.Sum(x => x.Count), "#,##0");
                //不渡返還・結果エラー
                lblICFuwatari03.Text = SearchResultCommon.DispDataFormat(ICFuwatariErrAll.Sum(x => x.Count), "#,##0");
                //不渡返還・結果正常
                lblICFuwatari04.Text = SearchResultCommon.DispDataFormat(ICFuwatariSeijyouAll.Sum(x => x.Count), "#,##0");

                //未使用項目
                lblICFuwatariDate01.Text = string.Empty;
                lblICFuwatariDate02.Text = string.Empty;
                lblICFuwatariDate03.Text = string.Empty;
                lblICFuwatariDate04.Text = string.Empty;
            }
        }

        #endregion

        #region タイマー

        /// <summary>
        /// 初期表示タイマー設定
        /// </summary>
        private void SetTimerFirst()
        {
            //初期表示制御
            SetDispItem();

            Timer.Enabled = true;
            Timer.Interval = 10;
        }

        /// <summary>
        /// タイマー設定
        /// </summary>
        private void SetTimer(bool Enable)
        {
            if (Enable)
            {
                Timer.Enabled = true;
                Timer.Interval = 60000;
            }
            else
            {
                Timer.Enabled = false;
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

        #endregion

    }
}

