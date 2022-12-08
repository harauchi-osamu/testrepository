using System;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;

namespace ParamMaint
{
    /// <summary>
    /// 業務登録画面
    /// </summary>
    public partial class GymParamForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private TBL_GYM_PARAM org_gym_param = null;
        private TBL_GYM_PARAM form_gym_param = null;
        private ItemManager.DisplayParams _dp { get { return _itemMgr.DispParams; } }

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
        public GymParamForm()
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
            base.SetDispName1("業務メンテナンス");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("業務登録");
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
            SetFunctionName(F5_, string.Empty);
            SetFunctionName(F6_, "前へ");
            SetFunctionName(F7_, "次へ");
            SetFunctionName(F8_, string.Empty);
            SetFunctionName(F9_, "プレビュー", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F10_, "印刷");
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "更新");
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            bool isPrev = (_dp.PrevGymId > 0);
            bool isNext = (_dp.NextGymId > 0);

            SetFunctionState(F6_, isPrev);
            SetFunctionState(F7_, isNext);
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
            // 入力モード
            SetEntryModeKana(txtGymKana);
            SetEntryModeKanji(txtGymKanji);
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
            lblGymID2.Text = _dp.DspGymId;
            switch (_dp.ProcGymType)
            {
                case AplInfo.EditType.NEW:
                    org_gym_param = new TBL_GYM_PARAM(_dp.GymId, AppInfo.Setting.SchemaBankCD);
                    break;
                case AplInfo.EditType.UPDATE:
                    org_gym_param = _itemMgr.GetGymParam(_dp.GymId);
                    break;
                case AplInfo.EditType.COPY:
                    org_gym_param = _itemMgr.GetGymParam(_dp.SrcGymId);
                    break;
                default:
                    return;
            }

            // 画面項目設定
            SetDisplayParams(org_gym_param);
        }

        /// <summary>
        /// 画面表示状態更新
        /// </summary>
        protected override void RefreshDisplayState()
        {
            // ファンクション状態設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        /// <param name="gym_param"></param>
        private void SetDisplayParams(TBL_GYM_PARAM gym_param)
        {
            // 業務名
            txtGymKana.Text = gym_param.m_GYM_KANA;
            txtGymKanji.Text = gym_param.m_GYM_KANJI;
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        private new TBL_GYM_PARAM GetDisplayParams()
        {
            TBL_GYM_PARAM gym_param = new TBL_GYM_PARAM(_itemMgr.DispParams.GymId, AppInfo.Setting.SchemaBankCD);

            // 業務名
            gym_param.m_GYM_KANA = txtGymKana.Text.Trim();
            gym_param.m_GYM_KANJI = txtGymKanji.Text.Trim();

            return gym_param;
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
                case Keys.Down:
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                    break;
                case Keys.Up:
                    // +({TAB})で[Shift+TAB]の意味になる
                    e.SuppressKeyPress = true;
                    SendKeys.Send("+({TAB})");
                    break;
                case Keys.ShiftKey:
                case Keys.ControlKey:
                    if (ChangeFunction(e)) return;
                    break;
                default:
                    break;
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

                if (!CheckEdited() || ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0106).Equals(DialogResult.Yes))
                {
                    this.Close();
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
        /// F6：前へ
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 前へ
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "前へ", 1);

                switch (_dp.ProcGymType)
                {
                    case AplInfo.EditType.NEW:
                        if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0117, _dp.GymId).Equals(DialogResult.Yes))
                        {
                            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _itemMgr.DispParams.PrevGymId);
                            RefreshDisplayData();
                            RefreshDisplayState();
                            return;
                        }
                        break;
                    case AplInfo.EditType.UPDATE:
                        if (!CheckEdited() || ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0106).Equals(DialogResult.Yes))
                        {
                            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _itemMgr.DispParams.PrevGymId);
                            RefreshDisplayData();
                            RefreshDisplayState();
                            return;
                        }
                        break;
                    case AplInfo.EditType.COPY:
                        if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0118, _dp.GymId).Equals(DialogResult.Yes))
                        {
                            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _itemMgr.DispParams.PrevGymId);
                            RefreshDisplayData();
                            RefreshDisplayState();
                            return;
                        }
                        break;
                    default:
                        return;
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
        /// F7：次へ
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 次へ
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "次へ", 1);

                switch (_dp.ProcGymType)
                {
                    case AplInfo.EditType.NEW:
                        if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0117, _dp.GymId).Equals(DialogResult.Yes))
                        {
                            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _itemMgr.DispParams.NextGymId);
                            RefreshDisplayData();
                            RefreshDisplayState();
                            return;
                        }
                        break;
                    case AplInfo.EditType.UPDATE:
                        if (!CheckEdited() || ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0106).Equals(DialogResult.Yes))
                        {
                            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _itemMgr.DispParams.NextGymId);
                            RefreshDisplayData();
                            RefreshDisplayState();
                            return;
                        }
                        break;
                    case AplInfo.EditType.COPY:
                        if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0118, _dp.GymId).Equals(DialogResult.Yes))
                        {
                            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _itemMgr.DispParams.NextGymId);
                            RefreshDisplayData();
                            RefreshDisplayState();
                            return;
                        }
                        break;
                    default:
                        return;
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
        /// F9：プレビュー
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // プレビュー
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "プレビュー", 1);

                // 印刷処理
                PrintReport(true);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F10：印刷
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 印刷
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "印刷", 1);

                // 確認メッセージ
                if (MessageBox.Show("印刷を開始します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }

                // 印刷処理
                PrintReport(false);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：更新
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 更新
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "更新", 1);

                // 画面のデータを取得
                form_gym_param = GetDisplayParams();

                // 入力検証
                if (!CheckFormInput(form_gym_param))
                {
                    return;
                }

                // ＤＢ更新する
                switch (_dp.ProcGymType)
                {
                    case AplInfo.EditType.NEW:
                        ExecNew();


                        break;
                    case AplInfo.EditType.UPDATE:
                        ExecUpdate();
                        break;
                    case AplInfo.EditType.COPY:
                        ExecCopy();
                        break;
                    default:
                        break;
                }

                // 最新取得
                _itemMgr.FetchAllData();
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
        /// 編集されたかどうか判定する
        /// </summary>
        /// <returns></returns>
        private bool CheckEdited()
        {
            switch (_dp.ProcGymType)
            {
                case AplInfo.EditType.NEW:
                case AplInfo.EditType.UPDATE:
                    if (CheckItemsEdited())
                    {
                        return true;
                    }
                    break;
                case AplInfo.EditType.COPY:
                    if (CheckItemsEdited())
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        /// <summary>
        /// 画面項目が更新されたかどうかチェックする
        /// </summary>
        /// <returns></returns>
        private bool CheckItemsEdited()
        {
            // 画面のデータを取得
            form_gym_param = GetDisplayParams();

            // オリジナルと画面を比較する
            if (!form_gym_param.m_GYM_KANA.Equals(org_gym_param.m_GYM_KANA)) { return true; }
            if (!form_gym_param.m_GYM_KANJI.Equals(org_gym_param.m_GYM_KANJI)) { return true; }
            return false;
        }

        /// <summary>
        /// 印刷を行う
        /// </summary>
        /// <param name="isPreview"></param>
        private void PrintReport(bool isPreview)
        {
            //if (org_gym_param == null)
            //{
            //    return;
            //}

            //// ヘッダー
            //GymParamDataSet.HeaderDataTable hTable = _ds.Header;
            //GymParamDataSet.HeaderRow hRow = hTable.NewHeaderRow();
            //hTable.Rows.Clear();
            //hRow.作成日時 = DateTime.Now.ToString("作成日時：yyyy年MM月dd日 HH:mm:ss");
            //hTable.AddHeaderRow(hRow);

            //// 明細
            //GymParamDataSet.DetailDataTable dTable = _ds.Detail;
            //dTable.Rows.Clear();
            //GymParamDataSet.DetailRow dRow = dTable.NewDetailRow();
            //dRow.業務番号 = org_gym_param._GYM_ID.ToString(Const.GYM_ID_LEN_STR);
            //dRow.業務名カナ = org_gym_param.m_GYM_KANA;
            //dRow.業務名 = org_gym_param.m_GYM_KANJI;
            //dTable.Rows.Add(dRow);

            //// 用紙を横に設定
            //this.gymParamReport1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //this.gymParamReport1.SetDataSource(_ds);
            //this.gymParamReport1.Refresh();

            //// レポート出力
            //ReportPrint rpt = new ReportPrint(ComAplInfo.ReportDirPath);
            //rpt.Print(this.gymParamReport1, isPreview, 1);
        }

        /// <summary>
        /// 入力チェックを行う
        /// </summary>
        /// <param name="gym"></param>
        /// <returns></returns>
        private bool CheckFormInput(TBL_GYM_PARAM gym)
        {
            // 業務名カナ
            if (string.IsNullOrEmpty(gym.m_GYM_KANA))
            {
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0120));
                ComMessageMgr.MessageWarning(EntMessageMgr.I0120);
                txtGymKana.Focus();
                return false;
            }

            // 業務名
            if (string.IsNullOrEmpty(gym.m_GYM_KANJI))
            {
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0121));
                ComMessageMgr.MessageWarning(EntMessageMgr.I0121);
                txtGymKanji.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 業務登録
        /// </summary>
        private void ExecNew()
        {
            // DB更新
            form_gym_param.m_DONE_FLG = "1";
            _itemMgr.RegistGymParam(form_gym_param, true);

            ComMessageMgr.MessageInformation(EntMessageMgr.I0123, _dp.GymId);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "パラメータ新規作成成功: " + _dp.GymId, 1);

            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _dp.GymId);
            RefreshDisplayData();
        }

        /// <summary>
        /// 業務更新
        /// </summary>
        private void ExecUpdate()
        {
            // 更新がないと何もしない
            if (!CheckEdited())
            {
                return;
            }

            // DB更新
            form_gym_param.m_DONE_FLG = "1";
            _itemMgr.RegistGymParam(form_gym_param, false);

            ComMessageMgr.MessageInformation(EntMessageMgr.I0123, _dp.GymId);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "パラメータ更新成功: " + _dp.GymId, 1);

            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _dp.GymId);
            RefreshDisplayData();
        }

        /// <summary>
        /// 業務コピー
        /// </summary>
        private void ExecCopy()
        {
            // DB更新
            form_gym_param.m_DONE_FLG = "1";
            _itemMgr.CopyGymParam(form_gym_param);

            ComMessageMgr.MessageInformation(EntMessageMgr.I0123, _dp.GymId);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "パラメータコピー成功: " + _dp.GymId, 1);

            _ctl.SetDispPramsGym(AplInfo.EditType.UPDATE, 0, _dp.GymId);
            RefreshDisplayData();
        }

    }
}
