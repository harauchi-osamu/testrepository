using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace CTRIcFuwatariMk
{
    /// <summary>
    /// 不渡返還テキスト作成画面
    /// </summary>
    public partial class FileCreateForm : EntryCommonFormBase
	{
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private bool _isComplete = false;

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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileCreateForm()
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
			base.SetDispName1("交換持帰");
		}

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("不渡返還テキスト作成");
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
            SetFunctionName(F6_, string.Empty);
            SetFunctionName(F7_, string.Empty);
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
            SetFunctionState(F5_, !_isComplete);
            SetFunctionState(F12_, !_isComplete);

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
            _itemMgr.DispParams.Clear();
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("実行モード 行内交換={0}", ServerIni.Setting.InternalExchange), 1);
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
            if (_ctl.IsIniErr) { return; }

            // 予定件数表示は行内交換実施有無に関わらず全件表示する

            // データ取得
            if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.登録データ, ItemManager.CreateType.全データ, this))
            {
                return;
            }
            if (!_itemMgr.FetchTrMeiIC(ItemManager.DataType.取消データ, ItemManager.CreateType.全データ, this))
            {
                return;
            }

            // 予定
            _itemMgr.DispParams.YoteiRegistCnt = _itemMgr.ICMeisaiList1.Count;
            _itemMgr.DispParams.YoteiCancelCnt = _itemMgr.ICMeisaiList2.Count;
            _itemMgr.DispParams.YoteiTotalCnt = _itemMgr.DispParams.YoteiRegistCnt + _itemMgr.DispParams.YoteiCancelCnt;
            lblYoteiRegistCnt.Text = string.Format("{0:###,##0}", _itemMgr.DispParams.YoteiRegistCnt);
            lblYoteiCancelCnt.Text = string.Format("{0:###,##0}", _itemMgr.DispParams.YoteiCancelCnt);
            lblYoteiTotalCnt.Text = string.Format("{0:###,##0}", _itemMgr.DispParams.YoteiTotalCnt);

            // 結果
            lblResEltCnt.Text = string.Format("{0:###,##0}", 0);
            lblResInnerCnt.Text = string.Format("{0:###,##0}", 0);
            lblResTotalCnt.Text = string.Format("{0:###,##0}", 0);

            // 作成ファイル
            lblFileName.Text = "";

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数 登録={0}件、取消={1}件",
                _itemMgr.DispParams.YoteiRegistCnt, _itemMgr.DispParams.YoteiCancelCnt), 1);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数 合計={0}件",
                _itemMgr.DispParams.YoteiTotalCnt), 1);
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        private void SetDisplayParamsResult()
        {
            // 結果
            int resEltCnt = _itemMgr.ICMeisaiList1.Count + _itemMgr.ICMeisaiList2.Count;
            int resOwnCnt = _itemMgr.ICMeisaiOwnList1.Count + _itemMgr.ICMeisaiOwnList2.Count;
            int resTotal = resEltCnt + resOwnCnt;

            lblResEltCnt.Text = string.Format("{0:###,##0}", resEltCnt);
            lblResInnerCnt.Text = string.Format("{0:###,##0}", resOwnCnt);
            lblResTotalCnt.Text = string.Format("{0:###,##0}", resTotal);

            // 作成ファイル
            lblFileName.Text = _itemMgr.DispParams.CreateFileName;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果件数 電子交換 登録={0}件、取消={1}件、合計={2}件",
                _itemMgr.ICMeisaiList1.Count, _itemMgr.ICMeisaiList2.Count, resEltCnt), 1);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果件数 行内交換 登録={0}件、取消={1}件、合計={2}件",
                _itemMgr.ICMeisaiOwnList1.Count, _itemMgr.ICMeisaiOwnList2.Count, resOwnCnt), 1);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果件数 合計={0}件", resTotal), 1);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("作成ファイル=[{0}]", _itemMgr.DispParams.CreateFileName), 1);
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
                // 最新表示
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "最新表示", 1);

                // 画面表示データ更新
                RefreshDisplayData();
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

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00002);

                    // テキストファイル作成
                    if (!_ctl.MakeTextFile(this))
                    {
                        // 画面表示データ更新しない
                        //RefreshDisplayData();
                        return;
                    }

                    // 完了メッセージ
                    int total = 0;
                    total += _itemMgr.ICMeisaiList1.Count;
                    total += _itemMgr.ICMeisaiList2.Count;
                    total += _itemMgr.ICMeisaiOwnList1.Count;
                    total += _itemMgr.ICMeisaiOwnList2.Count;
                    if (total > 0)
                    {
                        string msg = ComMessageMgr.Msg(ComMessageMgr.I00001, "不渡返還テキスト作成");
                        ComMessageMgr.MessageInformation(msg);
                        this.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                        _isComplete = true;
                    }
                    else
                    {
                        string msg = "作成対象データがありません。";
                        ComMessageMgr.MessageInformation(msg);
                        this.SetStatusMessage(msg, System.Drawing.Color.Transparent);
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
