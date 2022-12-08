using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace OcHoseiForce
{
    /// <summary>
    /// 不渡返還登録画面
    /// </summary>
    public partial class EntryForm : EntryCommonFormBase
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
        public EntryForm()
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
            base.SetDispName1("交換持出");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("補正状態強制完了更新");
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
                return;
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
            //初期設定
            rdbICBk.Checked = _itemMgr.DispParams.ICBk;
            rdbAmt.Checked = _itemMgr.DispParams.Amt;
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
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            // データ取得
            if (!GetUpdData(_itemMgr.UpdParams))
            {
                return;
            }

            // 持帰銀行・エントリ待
            lblICBkWait.Text = string.Format("{0:###,##0}", _itemMgr.UpdParams.ICBkWait);
            // 持帰銀行・エントリ保留
            lblICBkHoryu.Text = string.Format("{0:###,##0}", _itemMgr.UpdParams.ICBkHoryu);
            // 持帰銀行・エントリ中
            lblICBkEnt.Text = string.Format("{0:###,##0}", _itemMgr.UpdParams.ICBkEnt);

            // 金額・エントリ待
            lblAmtWait.Text = string.Format("{0:###,##0}", _itemMgr.UpdParams.AmtWait);
            // 金額・エントリ保留
            lblAmtHoryu.Text = string.Format("{0:###,##0}", _itemMgr.UpdParams.AmtHoryu);
            // 金額・エントリ中
            lblAmtEnt.Text = string.Format("{0:###,##0}", _itemMgr.UpdParams.AmtEnt);

            // 結果
            lblICBkRet.Text = "";
            lblAmtRet.Text = "";
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        private void SetDisplayParamsResult()
        {
            // 結果

            // 持帰銀行・更新
            lblICBkRet.Text = string.Format("{0:###,##0}", _itemMgr.UpdParams.ICBkUpdate);
            // 金額・更新
            lblAmtRet.Text = string.Format("{0:###,##0}", _itemMgr.UpdParams.AmtUpdate);
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            _itemMgr.DispParams.ICBk = rdbICBk.Checked;
            _itemMgr.DispParams.Amt = rdbAmt.Checked;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("条件 持帰銀行=[{0}]、金額=[{1}]", _itemMgr.DispParams.ICBk, _itemMgr.DispParams.Amt), 1);
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
        private void EntryForm_Load(object sender, EventArgs e)
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

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return;
                }

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

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return;
                }

                // 更新処理
                if (!ForceUpdate())
                {
                    // 画面表示データ更新なし
                    // RefreshDisplayData();
                    return;
                }

                // 完了メッセージ
                ComMessageMgr.MessageInformation(ComMessageMgr.I00001, "補正状態強制完了更新");
                _isComplete = true;

                // 画面項目設定
                SetDisplayParamsResult();

                // ファンクションキー状態設定
                SetFunctionState();
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
        /// 更新処理
        /// </summary>
        private bool ForceUpdate()
        {
            try
            {
                // 最新データ取得
                ItemManager.UpdateParams NewUpdParams = new ItemManager.UpdateParams();

                // データ取得
                if (!GetUpdData(NewUpdParams))
                {
                    return false;
                }

                //件数比較
                if (_itemMgr.UpdParams.ICBkWait != NewUpdParams.ICBkWait || _itemMgr.UpdParams.ICBkHoryu != NewUpdParams.ICBkHoryu ||
                    _itemMgr.UpdParams.ICBkEnt != NewUpdParams.ICBkEnt || _itemMgr.UpdParams.AmtWait != NewUpdParams.AmtWait ||
                    _itemMgr.UpdParams.AmtHoryu != NewUpdParams.AmtHoryu || _itemMgr.UpdParams.AmtEnt != NewUpdParams.AmtEnt)
                {
                    ComMessageMgr.MessageWarning("予定件数に変更がありました。\n内容を確認し再度実行してください。");
                    return false;
                }

                if (NewUpdParams.ICBkWait + NewUpdParams.ICBkHoryu + NewUpdParams.AmtWait + NewUpdParams.AmtHoryu < 1)
                {
                    ComMessageMgr.MessageInformation("更新対象データがありません。");
                    return false;
                }

                //確認メッセージ表示
                if ((ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "補正状態を強制完了しますがよろしいですか？") == DialogResult.No))
                {
                    return false;
                }

                // 更新実行
                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
                {
                    if (_itemMgr.DispParams.ICBk)
                    {
                        //持帰銀行更新
                        int Ret = _itemMgr.UpdateHoseiSts(HoseiStatus.HoseiInputMode.持帰銀行, dbp, non, this);
                        if (Ret < 0) return false;
                        _itemMgr.UpdParams.ICBkUpdate = Ret;
                    }
                    if (_itemMgr.DispParams.Amt)
                    {
                        //金額更新
                        int Ret = _itemMgr.UpdateHoseiSts(HoseiStatus.HoseiInputMode.金額, dbp, non, this);
                        if (Ret < 0) return false;
                        _itemMgr.UpdParams.AmtUpdate = Ret;
                    }

                    // コミット
                    non.Trans.Commit();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("強制完了終了 持帰銀行更新:{0} 金額更新:{1}", _itemMgr.UpdParams.ICBkUpdate, _itemMgr.UpdParams.AmtUpdate), 1);

                    return true;
                }
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
        /// データ取得処理
        /// </summary>
        private bool GetUpdData(ItemManager.UpdateParams param)
        {
            // 最新データ取得
            if (!_itemMgr.Fetch_HoseiSts(this))
            {
                return false;
            }

            // 件数比較
            // 予定件数
            IEnumerable<TBL_HOSEI_STATUS> ICBKSts = _itemMgr.hoseists.Where(x => x._HOSEI_INPTMODE == HoseiStatus.HoseiInputMode.持帰銀行);
            IEnumerable<TBL_HOSEI_STATUS> AmtSts = _itemMgr.hoseists.Where(x => x._HOSEI_INPTMODE == HoseiStatus.HoseiInputMode.金額);

            // 持帰銀行・エントリ待
            param.ICBkWait = ICBKSts.Where(x => x.m_INPT_STS == HoseiStatus.InputStatus.エントリ待).Count();
            // 持帰銀行・エントリ保留
            param.ICBkHoryu = ICBKSts.Where(x => x.m_INPT_STS == HoseiStatus.InputStatus.エントリ保留).Count();
            // 持帰銀行・エントリ中
            param.ICBkEnt = ICBKSts.Where(x => x.m_INPT_STS == HoseiStatus.InputStatus.エントリ中).Count();

            // 金額・エントリ待
            param.AmtWait = AmtSts.Where(x => x.m_INPT_STS == HoseiStatus.InputStatus.エントリ待).Count();
            // 金額・エントリ保留
            param.AmtHoryu = AmtSts.Where(x => x.m_INPT_STS == HoseiStatus.InputStatus.エントリ保留).Count();
            // 金額・エントリ中
            param.AmtEnt = AmtSts.Where(x => x.m_INPT_STS == HoseiStatus.InputStatus.エントリ中).Count();

            // 結果
            param.ICBkUpdate = 0;
            param.AmtUpdate = 0;

            return true;
        }
    }
}
