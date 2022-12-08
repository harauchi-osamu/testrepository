using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using System.Text.RegularExpressions;
using System.Linq;

namespace PrintOcBatchTotal
{
    /// <summary>
    /// 持出バッチ別合計表画面
    /// </summary>
    public partial class PrintBatchTotal : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private CTROcBatchTotaListDataSet _ds = null;

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
        public PrintBatchTotal()
        {
            InitializeComponent();
            _ds = new CTROcBatchTotaListDataSet();
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

            // 初期値設定
            _itemMgr.DispParams.Date = AplInfo.OpDate();
            txtInputDATE.Text = _itemMgr.DispParams.Date.ToString();

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
            base.SetDispName2("持出バッチ別合計表");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (IsNotPressCSAKey)
            {
                // 通常状態
                SetFunctionName(F1_, "終了");
                SetFunctionName(F2_, string.Empty);
                SetFunctionName(F3_, string.Empty);
                SetFunctionName(F4_, string.Empty);
                SetFunctionName(F5_, string.Empty);
                SetFunctionName(F6_, string.Empty);
                SetFunctionName(F7_, string.Empty);
                SetFunctionName(F8_, "プレビュー", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F9_, string.Empty);
                SetFunctionName(F10_, string.Empty);
                SetFunctionName(F11_, string.Empty);
                SetFunctionName(F12_, "印刷");
            }
            else if (IsPressShiftKey)
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
            else if (IsPressCtrlKey)
            {
                // Ctrlキー押下
                SetFunctionName(CF1_, string.Empty);
                SetFunctionName(CF2_, string.Empty);
                SetFunctionName(CF3_, string.Empty);
                SetFunctionName(CF4_, string.Empty);
                SetFunctionName(CF5_, string.Empty);
                SetFunctionName(CF6_, string.Empty);
                SetFunctionName(CF7_, string.Empty);
                SetFunctionName(CF8_, string.Empty);
                SetFunctionName(CF9_, string.Empty);
                SetFunctionName(CF10_, string.Empty);
                SetFunctionName(CF11_, string.Empty);
                SetFunctionName(CF12_, string.Empty);
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            if (this._ctl.SettingData.ChkServerIni == false || !string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                // 設定ファイル読み込みでエラー発生時はF1以外Disable
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
            // 画面項目を設定する処理はまとめてここに実装してこのメソッドを呼ぶ

        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 入力データの取得
            GetInputData();

            return true;
        }

        // <summary>
        /// 入力値の取得
        /// </summary>
        private bool GetInputData()
        {
            // 取込日
            _itemMgr.DispParams.Date = txtInputDATE.getInt();

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
            //初期表示時のエラーメッセージ表示
            if (this._ctl.SettingData.ChkServerIni == false)
            {
                // 設定ファイル読み込みでエラー発生時
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00003));
            }
            else if (!string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                // 設定ファイル読み込みでエラー発生時
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E01001, this._ctl.SettingData.CheckParamMsg));
            }
        }

        /// <summary>
        /// 各種入力チェック
        /// </summary>
        private void txtBox_IValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ClearStatusMessage();

            if (((BaseTextBox)sender).Name == "txtInputDATE")
            {
                if (!CheckDate())
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
        }

        /// <summary>
        /// フォーカスが外れたら背景を白色にする
        /// </summary>
        private void txt_Leave(object sender, EventArgs e)
        {
            RemoveFocusBackColor((Control)sender);
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        #region ファンクション

        /// <summary>
        /// F8：プレビュー
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "プレビュー", 1);
                //全入力項目チェック
                if (!CheckInputAll())
                {
                    return;
                }

                //入力データの取得
                GetDisplayParams();

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00002);

                    // 印刷処理
                    PrintList(true);
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

        /// <summary>
        /// F12：印刷
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "印刷", 1);
                //全入力項目チェック
                if (!CheckInputAll())
                {
                    return;
                }

                //入力データの取得
                GetDisplayParams();

                try
                {
                    //メッセージ設定
                    Processing(CommonClass.ComMessageMgr.I00003);

                    // 印刷処理
                    PrintList(false);
                }
                finally
                {
                    //メッセージ初期化
                    EndProcessing(CommonClass.ComMessageMgr.I00003);
                }

                this.SetStatusMessage("印刷完了", System.Drawing.Color.Transparent);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        #endregion

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 印刷を行う
        /// </summary>
        /// <param name="isPreview"></param>
        private void PrintList(bool isPreview)
        {
            // 対象データ取得
            if (!_itemMgr.GetPrintData(out List<ItemManager.PrintDetail> details, this)) return;

            //初期化
            _ds.Clear();

            // ヘッダー
            CTROcBatchTotaListDataSet.HeaderDataTable hTable = _ds.Header;
            CTROcBatchTotaListDataSet.HeaderRow hRow = hTable.NewHeaderRow();

            hRow.業務 = "業務：交換持出";
            hRow.データ件数 = details.LongCount();
            hRow.データなしメッセージ = "該当データなし";
            hTable.AddHeaderRow(hRow);

            // フッター
            CTROcBatchTotaListDataSet.FooterDataTable fTable = _ds.Footer;
            CTROcBatchTotaListDataSet.FooterRow fRow = fTable.NewFooterRow();

            // フッダー固定箇所設定
            fRow.フッター固定 = string.Format("＜{0}【{1}】＞ [{2}] {3}",
                                    _itemMgr.GetBank(AppInfo.Setting.SchemaBankCD),
                                    NCR.Server.Environment,
                                    NCR.Operator.UserName,
                                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            // 追加
            fTable.AddFooterRow(fRow);

            //明細データ作成
            if (details.LongCount() > 0)
            {
                MkPrintData(details);
            }
            else
            {
                MkPrintDataNoData();
            }

            // レポート設定
            CrystalDecisions.CrystalReports.Engine.ReportClass reportClass = new CrystalDecisions.CrystalReports.Engine.ReportClass();
            reportClass.FileName = _itemMgr.ReportPath();

            // 用紙を横に設定
            reportClass.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            reportClass.SetDataSource(_ds);
            reportClass.Refresh();

            // レポート出力
            ReportPrint rpt = new ReportPrint("");
            if (isPreview)
            {
                // プレビュー
                rpt.Print(reportClass, ReportPrint.PrintType.Preview, 1);
            }
            else
            {
                // 印刷
                rpt.Print(reportClass, ReportPrint.PrintType.Print, 1);
            }
        }

        /// <summary>
        /// 印刷用のデータ作成
        /// </summary>
        private void MkPrintData(List<ItemManager.PrintDetail> details)
        {
            // グループ・明細
            CTROcBatchTotaListDataSet.DetailDataTable dTable = _ds.Detail;
            foreach (ItemManager.PrintDetail detail in details)
            {
                //明細データ処理
                CTROcBatchTotaListDataSet.DetailRow dRow = dTable.NewDetailRow();

                // 取込日
                dRow.取込日 = CommonUtil.ConvToDateFormat(detail.OpeDate, 3);
                // バッチ番号
                dRow.バッチ番号 = detail.BatID.ToString("D6");
                // 持出銀行
                dRow.持出銀行 = GetBankOutput(detail.OCBKNo);
                // 持出支店
                dRow.持出支店 = GetBranchOutput(detail.OCBRNo);
                //交換日
                dRow.交換日 = CommonUtil.ConvToDateFormat(detail.ClearingDate, 3);
                // バッチ枚数
                dRow.バッチ枚数 = detail.TotalCount;
                // バッチ金額
                dRow.バッチ金額 = detail.TotalAmount;
                // 明細総枚数
                dRow.明細総枚数 = detail.Count;
                // 明細総金額
                dRow.明細総金額 = detail.Amount;
                // 差枚数
                dRow.差枚数 = detail.TotalCount - detail.Count;
                // 差金額
                dRow.差金額 = detail.TotalAmount - detail.Amount;

                // 追加
                dTable.Rows.Add(dRow);
            }
        }

        /// <summary>
        /// 印刷用のデータ作成(データなし)
        /// </summary>
        private void MkPrintDataNoData()
        {
            // グループ・明細
            CTROcBatchTotaListDataSet.DetailDataTable dTable = _ds.Detail;

            //明細データ処理
            CTROcBatchTotaListDataSet.DetailRow dRow = dTable.NewDetailRow();
            // 取込日
            dRow.取込日 = "";
            // 追加
            dTable.Rows.Add(dRow);
        }

        #region 個別出力制御

        /// <summary>
        /// 銀行出力データ取得
        /// </summary>
        private string GetBankOutput(string bkno)
        {
            if (int.TryParse(bkno, out int intbkno))
            {
                return GetBankOutput(intbkno);
            }
            return bkno;
        }

        /// <summary>
        /// 銀行出力データ取得
        /// </summary>
        private string GetBankOutput(int bkno)
        {
            return string.Format("{0:0000} {1}", bkno, _itemMgr.GetBank(bkno));
        }

        /// <summary>
        /// 支店出力データ取得
        /// </summary>
        private string GetBranchOutput(string brno)
        {
            if (int.TryParse(brno, out int intbrno))
            {
                return GetBranchOutput(intbrno);
            }
            return brno;
        }

        /// <summary>
        /// 支店出力データ取得
        /// </summary>
        private string GetBranchOutput(int brno)
        {
            return string.Format("{0:000} {1}", brno, _itemMgr.GeBranch(brno));
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
        private bool CheckDate()
        {
            string ChkText = txtInputDATE.ToString();
            int ChkIntText = txtInputDATE.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                //空はOK
                return true;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, lblInputDATE.Text));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, lblInputDATE.Text));
                return false;
            }

            return true;
        }

        #endregion

    }
}
