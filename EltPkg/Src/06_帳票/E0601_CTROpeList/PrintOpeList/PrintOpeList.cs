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

namespace PrintOpeList
{
    /// <summary>
    /// オペレータ統計画面
    /// </summary>
    public partial class PrintOpeList : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private CTROpeListDataSet _ds = null;

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
        public PrintOpeList()
        {
            InitializeComponent();
            _ds = new CTROpeListDataSet();
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
            _itemMgr.DispParams.DateFrom = AplInfo.OpDate();
            _itemMgr.DispParams.DateTo = AplInfo.OpDate();
            txtInputDATEFrom.Text = _itemMgr.DispParams.DateFrom.ToString();
            txtInputDATETo.Text = _itemMgr.DispParams.DateTo.ToString();

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
            base.SetDispName1("業務共通");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("オペレータ統計");
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
            // 基準日
            _itemMgr.DispParams.DateFrom = txtInputDATEFrom.getInt();
            _itemMgr.DispParams.DateTo = txtInputDATETo.getInt();

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

            if (((BaseTextBox)sender).Name == "txtInputDATEFrom")
            {
                if (!CheckDateFrom())
                {
                    e.Cancel = true;
                }
            }
            else if (((BaseTextBox)sender).Name == "txtInputDATETo")
            {
                if (!CheckDateTo())
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
            CTROpeListDataSet.HeaderDataTable hTable = _ds.Header;
            CTROpeListDataSet.HeaderRow hRow = hTable.NewHeaderRow();
            //作成日
            hRow.作成日 = CommonUtil.ConvToDateFormat(AplInfo.OpDate(), 3);
            //基準日開始
            hRow.基準日開始 = CommonUtil.ConvToDateFormat(_itemMgr.DispParams.DateFrom, 3);
            //基準日終了
            hRow.基準日終了 = CommonUtil.ConvToDateFormat(_itemMgr.DispParams.DateTo, 3);
            hTable.AddHeaderRow(hRow);

            // フッター
            CTROpeListDataSet.FooterDataTable fTable = _ds.Footer;
            CTROpeListDataSet.FooterRow fRow = fTable.NewFooterRow();

            // フッダー固定箇所設定
            fRow.フッター固定 = string.Format("＜{0}【{1}】＞ [{2}] {3}",
                                    _itemMgr.GetBank(AppInfo.Setting.SchemaBankCD),
                                    NCR.Server.Environment,
                                    NCR.Operator.UserName,
                                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            // 追加
            fTable.AddFooterRow(fRow);

            //明細データ作成
            MkPrintData(details);

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
            // 明細
            CTROpeListDataSet.DetailDataTable dTable = _ds.Detail;
            foreach (ItemManager.PrintDetail detail in details)
            {
                //明細データ処理
                CTROpeListDataSet.DetailRow dRow = dTable.NewDetailRow();
                // 業務項目ID
                dRow.業務項目ID = (detail.GymID * 10000) + detail.SortNo;
                // 入力項目
                dRow.入力項目 = string.Format("{0}業務 {1}", GymParam.GymId.GetName(detail.GymID), detail.ItemName);
                // オペレーター番号
                dRow.オペレーター番号 = detail.OpeNo;
                // オペレーター名
                dRow.オペレーター名 = _itemMgr.GeOperator(detail.OpeNo);
                // エントリー処理件数
                dRow.エントリー処理件数 = detail.E_Count;
                // エントリー処理時間
                dRow.エントリー処理時間 = CommonUtil.ConvSecondToMiliTimeFormat(detail.E_Time.ToString(), "hh':'mm':'ss");
                // エントリー秒/件
                dRow._エントリー秒_件 = GetAvgTime(detail.E_Count, detail.E_Time);
                // ベリファイ処理件数
                dRow.ベリファイ処理件数 = detail.V_Count;
                // ベリファイ処理時間
                dRow.ベリファイ処理時間 = CommonUtil.ConvSecondToMiliTimeFormat(detail.V_Time.ToString(), "hh':'mm':'ss");
                // ベリファイ秒/件
                dRow._ベリファイ秒_件 = GetAvgTime(detail.V_Count, detail.V_Time);
                // 訂正処理件数
                dRow.訂正処理件数 = detail.TeiseiCount;
                // 訂正率
                dRow.訂正率 = GetTeiseiRate(detail.E_Count, detail.V_Count, detail.TeiseiCount);

                // 追加
                dTable.Rows.Add(dRow);
            }
        }

        /// <summary>
        /// 秒/件データ取得
        /// </summary>
        private long GetAvgTime(long Count, long MiliTime)
        {
            if (Count == 0)
            {
                return 0;
            }

            return MiliTime / Count / 1000;
        }

        /// <summary>
        /// 訂正率データ取得
        /// </summary>
        private decimal GetTeiseiRate(long E_Count, long V_Count, long TeiseiCount)
        {
            if (E_Count + V_Count == 0)
            {
                return 0;
            }

            return (decimal)TeiseiCount / (E_Count + V_Count) * 100;
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
        /// 基準日開始入力チェック
        /// </summary>
        private bool CheckDateFrom()
        {
            string ChkText = txtInputDATEFrom.ToString();
            int ChkIntText = txtInputDATEFrom.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, string.Format("{0}（開始）", lblInputDATE.Text)));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, string.Format("{0}（開始）", lblInputDATE.Text)));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, string.Format("{0}（開始）", lblInputDATE.Text)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 基準日終了入力チェック
        /// </summary>
        private bool CheckDateTo()
        {
            string ChkText = txtInputDATETo.ToString();
            int ChkIntText = txtInputDATETo.getInt();

            //空チェック
            if (string.IsNullOrEmpty(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02001, string.Format("{0}（終了）", lblInputDATE.Text)));
                return false;
            }

            //数値チェック
            if (!Int32.TryParse(ChkText, out int i))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02002, string.Format("{0}（終了）", lblInputDATE.Text)));
                return false;
            }

            //日付チェック
            if (!EntryCommon.Calendar.IsDate(ChkText))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E02003, string.Format("{0}（終了）", lblInputDATE.Text)));
                return false;
            }

            //開始との比較チェエク
            if (ChkIntText < txtInputDATEFrom.getInt())
            {
                this.SetStatusMessage(string.Format("{0}(開始)以上を入力してください。", lblInputDATE.Text));
                return false;
            }

            return true;
        }

        #endregion

    }
}
