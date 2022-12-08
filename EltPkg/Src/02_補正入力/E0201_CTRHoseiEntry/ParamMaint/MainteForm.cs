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
using EntryClass;
using EntryCommon;

namespace ParamMaint
{
    /// <summary>
    /// 業務選択画面
    /// </summary>
    public partial class MainteForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private GymParamDataSet _ds = null;
        private int _srcGymId = 0;
        private int _dstGymId = 0;

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

        /// <summary>処理タイプ：業務登録</summary>
        private const int PROC_GYM = 0;
        /// <summary>処理タイプ：画面登録</summary>
        private const int PROC_DSP = 1;

        private string DspSrcGymId { get { return _srcGymId.ToString(Const.GYM_ID_LEN_STR); } }
        private string DspDstGymId { get { return _dstGymId.ToString(Const.GYM_ID_LEN_STR); } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainteForm()
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

            _ds = new GymParamDataSet();

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
            base.SetDispName2("業務選択");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, string.Empty);
            SetFunctionName(F3_, "業務削除", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F4_, string.Empty);
            SetFunctionName(F5_, "業務登録", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F6_, "画面選択", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F7_, string.Empty);
            SetFunctionName(F8_, string.Empty);
            SetFunctionName(F9_, "プレビュー", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "確定");
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
            txtGymIdDst.Focus();
        }

        /// <summary>
        /// [業務検索]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 業務検索画面
                if (!OpenGymSearchDialog())
                {
                    return;
                }
                _srcGymId = DBConvert.ToIntNull(txtGymIdSrc.Text);
                lblSourceName.Text = _ctl.GetGymName(_srcGymId);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [コピー元業務番号]テキスト KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGymIdSrc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                    case Keys.Down:
                    case Keys.Tab:
                        e.SuppressKeyPress = true;

                        if (string.IsNullOrEmpty(txtGymIdSrc.Text))
                        {
                            if (!OpenGymSearchDialog())
                            {
                                return;
                            }
                        }
                        _srcGymId = DBConvert.ToIntNull(txtGymIdSrc.Text);
                        txtGymIdSrc.Text = DspSrcGymId;
                        if (_srcGymId == 0 || _itemMgr.CheckGymId(_srcGymId))
                        {
                            txtGymIdDst.Focus();
                            lblSourceName.Text = "";
                            break;
                        }
                        else
                        {
                            SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0102, DspSrcGymId));
                            ComMessageMgr.MessageWarning(EntMessageMgr.I0102, DspSrcGymId);
                            lblSourceName.Text = "";
                            txtGymIdSrc.Focus();
                        }
                        break;
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        txtGymIdSrc.Text = "";
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
        /// [コピー元業務番号]テキスト Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGymIdSrc_Leave(object sender, EventArgs e)
        {
            try
            {
                _srcGymId = DBConvert.ToIntNull(txtGymIdSrc.Text);
                txtGymIdSrc.Text = DspSrcGymId;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [メンテ業務番号]テキスト KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGymIdDst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                    case Keys.Tab:
                        e.SuppressKeyPress = true;
                        txtGymIdSrc.Focus();
                        break;
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        txtGymIdDst.Text = "";
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
        /// [メンテ業務番号]テキスト Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGymIdDst_Leave(object sender, EventArgs e)
        {
            try
            {
                _dstGymId = DBConvert.ToIntNull(txtGymIdDst.Text);
                txtGymIdDst.Text = DspDstGymId;
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
        /// F1：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 通常処理
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);

                // まだ更新していない画面(DONE_FLG=0)があるなら警告
                int unupdatedGymno = _itemMgr.CheckDoneFlg();
                if (unupdatedGymno > 0)
                {
                    DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0130, unupdatedGymno.ToString(Const.GYM_ID_LEN_STR));
                    if (res == DialogResult.Cancel)
                    {
                        return;
                    }
                }
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
        /// F3：業務削除
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 業務削除
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "業務削除", 1);

                // 業務IDチェック
                _dstGymId = DBConvert.ToIntNull(txtGymIdDst.Text);
                txtGymIdDst.Text = DspDstGymId;

                // 削除用業務番号の整合性チェック
                if (ConsistDelGymno())
                {
                    if (_itemMgr.DeleteGymID(_dstGymId))
                    {
                        // 正常
                        SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0110, DspDstGymId));
                        ComMessageMgr.MessageInformation(EntMessageMgr.I0110, DspDstGymId);
                        txtGymIdDst.Text = 0.ToString(Const.GYM_ID_LEN_STR);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "正常に削除: 業務番号 " + DspDstGymId, 1);

                        // 最新取得
                        _itemMgr.FetchAllData();
                        return;
                    }
                    else
                    {
                        // 失敗
                        SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.E0103, DspDstGymId));
                        ComMessageMgr.MessageWarning(EntMessageMgr.E0103, DspDstGymId);
                        txtGymIdDst.Focus();
                        return;
                    }
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
        /// F5：業務登録
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 業務登録
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "業務登録", 1);

                // 業務番号の整合性チェック
                AplInfo.EditType type = ConsistGymno(PROC_GYM);
                if (type != AplInfo.EditType.NONE)
                {
                    // 業務選択画面
                    _ctl.SetDispPramsGym(type, _srcGymId, _dstGymId);
                    GymParamForm form = new GymParamForm();
                    form.InitializeForm(_ctl);
                    form.ShowDialog();
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

        /// <summary>
        /// F6：画面選択
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 画面選択
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面選択", 1);

                // 業務番号の整合性チェック
                AplInfo.EditType type = ConsistGymno(PROC_DSP);
                if (type != AplInfo.EditType.NONE)
                {
                    _ctl.SetDispPramsGym(type, _srcGymId, _dstGymId);
                    DspSelectForm form = new DspSelectForm();
                    form.InitializeForm(_ctl);
                    form.ShowDialog();
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
                //PrintReport(true);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：確定
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 確定
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);

                _dstGymId = DBConvert.ToIntNull(txtGymIdDst.Text);
                txtGymIdDst.Text = DspDstGymId;

                if (_dstGymId == 0)
                {
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0101));
                    ComMessageMgr.MessageWarning(EntMessageMgr.I0101);
                    txtGymIdDst.Focus();
                    return;
                }

                // 確定処理
                if (SetDoneFlg())
                {
                    ComMessageMgr.MessageInformation(EntMessageMgr.I0122);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "パラメーター更新成功", 1);
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
        /// 業務検索画面を開く
        /// </summary>
        private bool OpenGymSearchDialog()
        {
            int gymid = DBConvert.ToIntNull(txtGymIdSrc.Text);
            SearchGymDialog form = new SearchGymDialog(gymid, _ctl);
            DialogResult res = form.ShowDialog();
            if (res != DialogResult.OK)
            {
                return false;
            }
            txtGymIdSrc.Text = DBConvert.ToStringNull(form.GymId);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ConsistDelGymno()
        {
            _srcGymId = DBConvert.ToIntNull(txtGymIdSrc.Text);
            _dstGymId = DBConvert.ToIntNull(txtGymIdDst.Text);
            txtGymIdSrc.Text = DspSrcGymId;
            txtGymIdDst.Text = DspDstGymId;

            // 編集先業務番号が0はＮＧ
            if (_dstGymId == 0)
            {
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0101));
                ComMessageMgr.MessageWarning(EntMessageMgr.I0101);
                txtGymIdDst.Focus();
                return false;
            }

            // 編集先業務番号が存在するなら削除ＯＫ、存在しないのはＮＧ
            if (_itemMgr.CheckGymId(_dstGymId))
            {
                DialogResult dr = ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0101, DspDstGymId);
                if (dr.Equals(DialogResult.Yes)) { return true; }
                txtGymIdDst.Focus();
                return false;
            }
            else
            {
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0102, DspDstGymId));
                ComMessageMgr.MessageWarning(EntMessageMgr.I0102, DspDstGymId);
                txtGymIdDst.Focus();
                return false;
            }
        }

        /// <summary>
        /// 業務番号の整合性チェック
        /// </summary>
        /// <param name="procType"></param>
        /// <returns></returns>
        private AplInfo.EditType ConsistGymno(int procType)
        {
            _srcGymId = DBConvert.ToIntNull(txtGymIdSrc.Text);
            _dstGymId = DBConvert.ToIntNull(txtGymIdDst.Text);
            txtGymIdSrc.Text = DspSrcGymId;
            txtGymIdDst.Text = DspDstGymId;

            // コピー元が0
            if (_srcGymId == 0)
            {
                // 両方とも0はＮＧ
                if (_dstGymId == 0)
                {
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0104));
                    ComMessageMgr.MessageWarning(EntMessageMgr.I0104);
                    txtGymIdDst.Focus();
                    return AplInfo.EditType.NONE;
                }
                // 編集する業務番号が存在すればＯＫ
                if (_itemMgr.CheckGymId(_dstGymId)) { return AplInfo.EditType.UPDATE; }
                // 編集先が存在しない業務番号＝真っ白から新規作成の場合は業務情報のみＯＫ
                switch (procType)
                {
                    case PROC_GYM:
                        return AplInfo.EditType.NEW;
                    case PROC_DSP:
                        SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0105));
                        ComMessageMgr.MessageWarning(EntMessageMgr.I0105);
                        txtGymIdDst.Focus();
                        return AplInfo.EditType.NONE;
                    default:
                        break;
                }
            }

            // コピー元が0以外
            // 編集先も0なのはＮＧ
            if (_dstGymId == 0)
            {
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0101));
                ComMessageMgr.MessageWarning(EntMessageMgr.I0101);
                txtGymIdDst.Focus();
                return AplInfo.EditType.NONE;
            }

            // コピー元が存在する業務番号のとき
            if (_itemMgr.CheckGymId(_srcGymId))
            {
                // 編集する業務番号も存在する＝既存業務番号のコピー
                if (_itemMgr.CheckGymId(_dstGymId))
                {
                    if (_srcGymId == _dstGymId) { return AplInfo.EditType.UPDATE; }

                    // 存在する業務番号を新規作成するのはＮＧ
                    switch (procType)
                    {
                        case PROC_GYM:
                            SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0109, DspDstGymId));
                            ComMessageMgr.MessageWarning(EntMessageMgr.I0109, DspDstGymId);
                            txtGymIdDst.Focus();
                            return AplInfo.EditType.NONE;
                        case PROC_DSP:
                            return AplInfo.EditType.UPDATE;
                        default:
                            return AplInfo.EditType.NONE;
                    }
                }

                // 編集先の業務番号が存在しない＝その番号で新規作成の場合は業務情報のみＯＫ
                switch (procType)
                {
                    case PROC_GYM:
                        return AplInfo.EditType.COPY;
                    case PROC_DSP:
                        SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0105));
                        ComMessageMgr.MessageWarning(EntMessageMgr.I0105);
                        txtGymIdDst.Focus();
                        return AplInfo.EditType.NONE;
                    default:
                        break;
                }
            }

            // コピー元は存在しない業務番号のとき
            // 編集先は存在する業務番号
            if (_itemMgr.CheckGymId(_dstGymId))
            {
                // 編集先をただ修正したいだけ？
                switch (procType)
                {
                    case PROC_GYM:
                        DialogResult dr = ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0102, DspSrcGymId, DspDstGymId);
                        if (dr.Equals(DialogResult.Yes)) { return AplInfo.EditType.UPDATE; }
                        txtGymIdSrc.Focus();
                        return AplInfo.EditType.NONE;
                    case PROC_DSP:
                        return AplInfo.EditType.UPDATE;
                    default:
                        break;
                }
            }

            // 編集先も存在しない業務番号（＝業務情報での新規作成のみＯＫ）
            switch (procType)
            {
                case PROC_GYM:
                    DialogResult dr = ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0103, DspSrcGymId, DspDstGymId);
                    if (dr.Equals(DialogResult.Yes)) { return AplInfo.EditType.NEW; }
                    txtGymIdSrc.Focus();
                    return AplInfo.EditType.NONE;
                case PROC_DSP:
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0105));
                    ComMessageMgr.MessageWarning(EntMessageMgr.I0105);
                    return AplInfo.EditType.NONE;
                default:
                    break;
            }

            // 以上でMECEのはずです
            return AplInfo.EditType.NONE;
        }

        /// <summary>
        /// 印刷を行う
        /// </summary>
        /// <param name="isPreview"></param>
        private void PrintReport(bool isPreview)
        {
            // TBL_GYM_PARAM 取得
            _itemMgr.FetchAllData();

            // ヘッダー
            GymParamDataSet.HeaderDataTable hTable = _ds.Header;
            GymParamDataSet.HeaderRow hRow = hTable.NewHeaderRow();
            hTable.Rows.Clear();
            hRow.作成日時 = DateTime.Now.ToString("作成日時：yyyy年MM月dd日 HH:mm:ss");
            hTable.AddHeaderRow(hRow);

            // 明細
            GymParamDataSet.DetailDataTable dTable = _ds.Detail;
            dTable.Rows.Clear();
            foreach (ItemManager.GymParamas gym in _itemMgr.GymParam.Values)
            {
                // 明細
                GymParamDataSet.DetailRow dRow = dTable.NewDetailRow();
                dRow.業務番号 = gym.gym_param._GYM_ID.ToString(Const.GYM_ID_LEN_STR);
                dRow.業務名カナ = gym.gym_param.m_GYM_KANA;
                dRow.業務名 = gym.gym_param.m_GYM_KANJI;
                dTable.Rows.Add(dRow);
            }

            // 用紙を横に設定
            this.gymParamReport1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            this.gymParamReport1.SetDataSource(_ds);
            this.gymParamReport1.Refresh();

            // レポート出力
            ReportPrint rpt = new ReportPrint();

            ReportPrint.PrintType Type = ReportPrint.PrintType.Print;
            if (isPreview)
            {
                Type = ReportPrint.PrintType.Preview;
            }
            rpt.Print(this.gymParamReport1, Type, 1);
        }

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <returns></returns>
        private bool SetDoneFlg()
        {
            // 全パラメータ最新取得
            _itemMgr.FetchAllData(_ctl);

            // 当該の業務情報、画面情報の取得
            ItemManager.GymParamas gym = _itemMgr.GymParam[_dstGymId];

            // 画面情報の整合性をチェック
            bool res = CheckDspParam(gym);

            // ロックフラグ設定 or 解除
            _itemMgr.UpdateDoneFlg(_dstGymId, res);

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="gym_param"></param>
        /// <param name="tdps"></param>
        /// <returns></returns>
        //private bool CheckDspParam(int gymid, TBL_GYM_PARAM gym_param, Dictionary<int, TBL_DSP_PARAM> tdps)
        private bool CheckDspParam(ItemManager.GymParamas gym)
        {
            foreach (DspInfo dsp in gym.DspInfos.Values)
            {
                // 画面項目の検証
                if (dsp.dsp_items.Count < 1)
                {
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0142, gym._GYM_ID, dsp._DSP_ID));
                    ComMessageMgr.MessageWarning(EntMessageMgr.I0142, gym._GYM_ID, dsp._DSP_ID);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ComMessageMgr.Msg(EntMessageMgr.I0142, gym._GYM_ID, dsp._DSP_ID), 2);
                    return false;
                }

                // DUP項目とVFY項目の組み合わせ検証
                Dictionary<string, bool> dupvfy = CheckDspVfy(dsp.dsp_items);

                // DSP_ITEMの全項目でＤＵＰありはダメ
                if (dupvfy["DUP"].Equals(true))
                {
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0145, gym._GYM_ID, dsp._DSP_ID));
                    ComMessageMgr.MessageWarning(EntMessageMgr.I0145, gym._GYM_ID, dsp._DSP_ID);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ComMessageMgr.Msg(EntMessageMgr.I0145, gym._GYM_ID, dsp._DSP_ID), 2);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// DSP_ITEMの中ですべてDUP=1ならばDUPはtrue、VFYはVFY=1が一つでもあればtrue
        /// </summary>
        private Dictionary<string, bool> CheckDspVfy(SortedList<int, TBL_DSP_ITEM> dsp_items)
        {
            Dictionary<string, bool> dicsb = new Dictionary<string, bool>();
            dicsb.Add("DUP", true);
            dicsb.Add("VFY", false);
            foreach (TBL_DSP_ITEM di in dsp_items.Values)
            {
                if (!DBConvert.ToBoolNull(di.m_DUP))
                {
                    dicsb["DUP"] = false;
                }
            }
            return dicsb;
        }

    }
}
