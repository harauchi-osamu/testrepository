using System;
using System.Collections.Generic;
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
    /// 業務画面選択画面
    /// </summary>
    public partial class DspSelectForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private int _srcdspid = 0;
        private int _dstdspid = 0;
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
        public DspSelectForm()
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
            base.SetDispName2("業務画面選択");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, string.Empty);
            SetFunctionName(F3_, "削除");
            SetFunctionName(F4_, string.Empty);
            SetFunctionName(F5_, string.Empty);
            SetFunctionName(F6_, string.Empty);
            SetFunctionName(F7_, string.Empty);
            SetFunctionName(F8_, string.Empty);
            SetFunctionName(F9_, string.Empty);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "編集");
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
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
            ntbSource.Text = "00000";
            ntbDest.Text = "00000";
            lblGymName.Text = _dp.DspGymId + " " + _ctl.GetGymName(_dp.GymId);
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
            ItemManager.GymParamas gym = _itemMgr.GymParam[_dp.GymId];

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[gym.DspInfos.Count];
            foreach (DspInfo dsp in gym.DspInfos.Values)
            {
                string key = dsp._DSP_ID.ToString();
                string dspid = dsp._DSP_ID.ToString(Const.DSP_ID_LEN_STR);
                string dspname = dsp.dsp_param.m_DSP_NAME;

                listItem.Clear();
                listItem.Add(key);              // 隠しキー
                listItem.Add(dspid);            // 画面番号
                listItem.Add(dspname);          // 画面名
                listView[cnt] = new ListViewItem(listItem.ToArray());
                cnt++;
            }
            this.lvBatList.Items.Clear();
            this.lvBatList.Items.AddRange(listView);
            this.lvBatList.Enabled = true;
            this.lvBatList.Refresh();
            this.lvBatList.Select();
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
        }

        /// <summary>
        /// [フォーム] ItemSelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                ntbSource.Text = e.Item.Text;
                ntbDest.Text = e.Item.Text;

                this.ntbSource_Leave(sender, e);
                this.ntbDest_Leave(sender, e);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [コピー元画面番号]テキスト KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntbSource_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                    case Keys.Down:
                    case Keys.Tab:
                        ntbDest.Focus();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Escape:
                        ntbSource.Text = "";
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
        /// [コピー元画面番号]テキスト Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntbSource_Leave(object sender, EventArgs e)
        {
            try
            {
                ntbSource.Text = ntbSource.Text.PadLeft(5, '0');
                _srcdspid = DBConvert.ToIntNull(ntbSource.Text.Trim());
                lblSourceName.Text = _ctl.GetDspName(_dp.GymId, _srcdspid);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [メンテナンス画面番号]テキスト KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntbDest_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        this.btnFunc12_Click(sender, e);
                        break;
                    case Keys.Up:
                        ntbSource.Focus();
                        break;
                    case Keys.Escape:
                        ntbDest.Text = "";
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
        /// [メンテナンス画面番号]テキスト Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntbDest_Leave(object sender, EventArgs e)
        {
            try
            {
                ntbDest.Text = ntbDest.Text.PadLeft(5, '0');
                _dstdspid = DBConvert.ToIntNull(ntbDest.Text.Trim());
                lblDestName.Text = _ctl.GetDspName(_dp.GymId, _dstdspid);
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
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
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

                if (_itemMgr.DispParams.IsDspUpdate)
                {
                    // 編集フラグを更新
                    if (!_itemMgr.UpdateDoneFlg(_dp.GymId, false))
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
        /// F3：削除
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 削除
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "削除", 1);

                // 画面項目取得
                ntbSource_Leave(sender, e);
                ntbDest_Leave(sender, e);

                // 削除可能かどうか
                if (!CanDeleted(_dstdspid))
                {
                    ntbDest.Focus();
                    return;
                }

                // バッチデータが存在するか
                if (_itemMgr.CheckTrMeiExists(_dp.GymId, _dstdspid))
                {
                    if (!DialogResult.Yes.Equals(ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0109, _dp.GymId, "画面削除の場合"))) { return; }
                }
                else
                {
                    if (!DialogResult.Yes.Equals(ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0104, _dstdspid))) { return; }
                }

                // 削除実行
                if (_itemMgr.DeleteDspID(_dp.GymId, _dstdspid))
                {
                    // 更新フラグ
                    _itemMgr.DispParams.IsDspUpdate = true;

                    // 正常
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0110, _dstdspid));
                    ComMessageMgr.MessageInformation(EntMessageMgr.I0110, _dstdspid);
                    ntbDest.Text = "00000";
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "正常に削除: 画面番号 " + _dstdspid, 1);
                }
                else
                {
                    // 失敗
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.E0104, _dstdspid));
                    ComMessageMgr.MessageWarning(EntMessageMgr.E0104, _dstdspid);
                    ntbDest.Focus();
                    return;
                }

                // 最新取得
                _itemMgr.FetchAllData();

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
        /// F12：編集
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 編集
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "編集", 1);

                // 画面項目取得
                ntbSource_Leave(sender, e);
                ntbDest_Leave(sender, e);

                // メンテナンス先が0はＮＧ
                if (_dstdspid == 0)
                {
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0106));
                    ComMessageMgr.MessageWarning(EntMessageMgr.I0106);
                    ntbDest.Focus();
                    return;
                }

                // 更新なのか新規作成なのか既存からのコピーなのか判定
                AplInfo.EditType type = AplInfo.EditType.NONE;
                if (_itemMgr.CheckDspId(_dp.GymId, _dstdspid))
                {
                    // メンテナンス先番号が存在する場合は基本的には更新だが
                    if (_dstdspid != _srcdspid && _itemMgr.CheckDspId(_dp.GymId, _srcdspid))
                    {
                        // 別番号でコピー元がある場合、すでにある番号にコピーしようとしているとみなしてＮＧ
                        SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0114, _dstdspid));
                        ComMessageMgr.MessageWarning(EntMessageMgr.I0114, _dstdspid);
                        ntbDest.Focus();
                        return;
                    }
                    // コピー元に変な番号が入っている場合は一応聞いておく
                    else if (_dstdspid != _srcdspid && _srcdspid != 0
                                && !DialogResult.Yes.Equals(ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0108, _srcdspid, _dstdspid)))
                    {
                        ntbSource.Focus();
                        return;
                    }

                    // メンテナンス先がある場合は既存の更新
                    type = AplInfo.EditType.UPDATE;
                }
                else
                {
                    // メンテナンス先がなければ新規作成かコピーか
                    if (_itemMgr.CheckDspId(_dp.GymId, _srcdspid))
                    {
                        // コピー元はあるのでコピー
                        type = AplInfo.EditType.COPY;
                    }
                    else
                    {
                        // コピー元が存在しない番号なので新規作成だろうけど、コピー元に変な番号が入っているので一応聞いておく
                        if (_srcdspid != 0 && !DialogResult.Yes.Equals(ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0105, _srcdspid, _dstdspid)))
                        {
                            ntbSource.Focus();
                            return;
                        }

                        // コピー元もメンテナンス先もないので新規作成
                        type = AplInfo.EditType.NEW;
                    }
                }

                // 業務登録画面
                _ctl.SetDispPramsDsp(type, _srcdspid, _dstdspid);
                DspParamForm form = new DspParamForm();
                form.InitializeForm(_ctl);
                form.ShowDialog();

                // 最新取得
                _itemMgr.FetchAllData();

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


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 削除できるか
        /// </summary>
        /// <param name="dspid"></param>
        /// <returns></returns>
        private bool CanDeleted(int dspid)
        {
            // 0はダメ
            if (dspid == 0)
            {
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0106));
                ComMessageMgr.MessageWarning(EntMessageMgr.I0106);
                return false;
            }

            // 存在しない画面ＩＤはダメ
            if (!_itemMgr.CheckDspId(_dp.GymId, dspid))
            {
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.I0107, dspid));
                ComMessageMgr.MessageWarning(EntMessageMgr.I0107, dspid);
                return false;
            }
            return true;
        }

    }
}
