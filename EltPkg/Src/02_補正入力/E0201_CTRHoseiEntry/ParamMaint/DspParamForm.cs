using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
    /// 業務画面登録画面
    /// </summary>
    public partial class DspParamForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ItemManager.DisplayParams _dp { get { return _itemMgr.DispParams; } }
        private DspInfo _dsp { get { return _itemMgr.GymParam[_dp.GymId].DspInfos[_dp.DspId]; } }
        private string DspDstDspId { get { return _dp.DspId.ToString(Const.GYM_ID_LEN_STR); } }
        private List<FontDialog.ItemSet> _fontSizeList = null;

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
        public DspParamForm()
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
            base.SetDispName2("業務画面登録");
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
            SetFunctionName(F8_, "画面調整", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F9_, string.Empty);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "更新");
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            bool isPrev = (_dp.PrevDspId > 0);
            bool isNext = (_dp.NextDspId > 0);

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
            _fontSizeList = FontDialog.GetFontSizeList();
            cmbFontSize.DataSource = _fontSizeList;
            cmbFontSize.DisplayMember = "ItemDisp";
            cmbFontSize.ValueMember = "ItemValue";
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
            switch (_dp.ProcDspType)
            {
                case AplInfo.EditType.NEW:
                    SetNewDspParams(_dp.DspId);
                    break;
                case AplInfo.EditType.UPDATE:
                    break;
                case AplInfo.EditType.COPY:
                    SetCopyDspParams(_dp.SrcDspId, _dp.DspId);
                    break;
                default:
                    return;
            }

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

            lblGymName.Text = _dp.GymId.ToString(Const.GYM_ID_LEN_STR) + " " + gym.gym_param.m_GYM_KANJI;
            lblDspId.Text = _dp.DspId.ToString(Const.DSP_ID_LEN_STR);
            ktbDspName.Text = _dsp.dsp_param.m_DSP_NAME;
            lblHoseiItemMode.Text = string.Format("{0} {1}", _ctl.HoseiItemMode, HoseiParam.HoseiItemMode.GetName(_ctl.HoseiItemMode));
            chkAutoEnt.Checked = DBConvert.ToBoolNull(_dsp.hosei_param.m_AUTO_SKIP_MODE_ENT);
            chkAutoVfy.Checked = DBConvert.ToBoolNull(_dsp.hosei_param.m_AUTO_SKIP_MODE_VFY);
            chkVfy.Checked = DBConvert.ToBoolNull(_dsp.hosei_param.m_VERY_MODE);
            cmbFontSize.SelectedValue = _dsp.dsp_param.m_FONT_SIZE.ToString();
            tbImgFile.Text = _dsp.img_param.m_IMG_FILE;
            tbOCR.Text = _dsp.dsp_param.m_DSP_NAME;

            // DataGrid
            dgvDspItems.Rows.Clear();
            if (_dsp.hosei_items.Count < 1) { return; }
            dgvDspItems.Rows.Add(_dsp.hosei_items.Count);

            int row = 0;
            foreach (TBL_HOSEIMODE_DSP_ITEM hi in _dsp.hosei_items.Values)
            {
                TBL_DSP_ITEM di = _dsp.dsp_items[hi._ITEM_ID];

                // 項目パラメーター
                dgvDspItems["ITEM_ID", row].Value = hi._ITEM_ID;
                dgvDspItems["ITEM_DISPNAME", row].Value = di.m_ITEM_DISPNAME;
                dgvDspItems["ITEM_TYPE", row].Value = di.m_ITEM_TYPE;
                dgvDspItems["ITEM_LEN", row].Value = di.m_ITEM_LEN;
                dgvDspItems["POS", row].Value = di.m_POS;
                dgvDspItems["DUP", row].Value = DBConvert.ToBoolNull(di.m_DUP);
                dgvDspItems["AUTO_INPUT", row].Value = DBConvert.ToBoolNull(di.m_AUTO_INPUT);
                // 座標：ラベル
                dgvDspItems["NAME_POS_TOP", row].Value = hi.m_NAME_POS_TOP;
                dgvDspItems["NAME_POS_LEFT", row].Value = hi.m_NAME_POS_LEFT;
                // 座標：テキスト
                dgvDspItems["INPUT_POS_TOP", row].Value = hi.m_INPUT_POS_TOP;
                dgvDspItems["INPUT_POS_LEFT", row].Value = hi.m_INPUT_POS_LEFT;
                dgvDspItems["INPUT_WIDTH", row].Value = hi.m_INPUT_WIDTH;
                dgvDspItems["INPUT_HEIGHT", row].Value = hi.m_INPUT_HEIGHT;
                dgvDspItems["INPUT_SEQ", row].Value = hi.m_INPUT_SEQ;
                // サブルーチン
                dgvDspItems["ITEM_SUBRTN", row].Value = di.m_ITEM_SUBRTN;

                // イメージカーソル
                if (_dsp.img_cursor_params.ContainsKey(hi._ITEM_ID))
                {
                    TBL_IMG_CURSOR_PARAM cursor = _dsp.img_cursor_params[hi._ITEM_ID];
                    dgvDspItems["ITEM_TOP", row].Value = cursor.m_ITEM_TOP;
                    dgvDspItems["ITEM_LEFT", row].Value = cursor.m_ITEM_LEFT;
                    dgvDspItems["ITEM_WIDTH", row].Value = cursor.m_ITEM_WIDTH;
                    dgvDspItems["ITEM_HEIGHT", row].Value = cursor.m_ITEM_HEIGHT;
                }
                row++;
            }
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
        /// [項目追加]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // 項目追加
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "項目追加", 1);

                // 項目選択画面
                ItemMstDialog form = new ItemMstDialog();
                form.InitializeForm(_ctl);
                DialogResult res = form.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }

                // 重複チェック
                int itemid = form.ItemId;
                foreach (DataGridViewRow dgv in dgvDspItems.Rows)
                {
                    int gridid = DBConvert.ToIntNull(dgv.Cells["ITEM_ID"].Value);
                    if (gridid == itemid)
                    {
                        ComMessageMgr.MessageWarning(EntMessageMgr.ENT10003);
                        return;
                    }
                }

                // メモリデータ削除
                TBL_HOSEIMODE_DSP_ITEM hosei_item = CreateHoseiDspItems(itemid);
                TBL_DSP_ITEM dsp_item = CreateDspItems(itemid);
                TBL_IMG_CURSOR_PARAM cursor = CreateImgCorsorParams(itemid);
                _dsp.AddHoseiItem(hosei_item.ToDataRow(), false);
                _dsp.AddDspItem(dsp_item.ToDataRow(), false);
                _dsp.AddImgCursor(cursor.ToDataRow(), false);

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
        /// [項目削除]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 項目削除
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "項目削除", 1);

                // 削除対象取得
                if (dgvDspItems.RowCount < 1) { return; }
                int itemid = DBConvert.ToIntNull(dgvDspItems.CurrentRow.Cells["ITEM_ID"].Value);
                if (!_dsp.hosei_items.ContainsKey(itemid)) { return; }

                // 確認メッセージ
                string itemName = string.Format("項目番号={0}\n項目名={1}", itemid, _dsp.dsp_items[itemid].m_ITEM_DISPNAME);
                DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.ENT10004, itemName);
                if (res != DialogResult.Yes)
                {
                    return;
                }

                // メモリデータ削除（DSP_ITEM は他の HOSEIMODE_DSP_ITEM でも使用している可能性があるためここでは削除しない）
                _dsp.hosei_items.Remove(itemid);
                _dsp.img_cursor_params.Remove(itemid);

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
                    // 最新取得（編集情報破棄）
                    _itemMgr.FetchAllData();

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

                switch (_dp.ProcDspType)
                {
                    case AplInfo.EditType.NEW:
                        string msg = string.Format("新規画面：{0} はまだ保存していません。破棄しますか？", DspDstDspId);
                        if (!MessageBox.Show(msg, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                        {
                            return;
                        }
                        break;
                    case AplInfo.EditType.UPDATE:
                        if (CheckEdited() && !ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0106).Equals(DialogResult.Yes))
                        {
                            return;
                        }
                        break;
                    case AplInfo.EditType.COPY:
                        if (!ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0141, _dp.DspId).Equals(DialogResult.Yes))
                        {
                            return;
                        }
                        break;
                    default:
                        return;
                }

                // 最新取得（編集情報破棄）
                _itemMgr.FetchAllData();

                _ctl.SetDispPramsDsp(AplInfo.EditType.UPDATE, 0, _itemMgr.DispParams.PrevDspId);

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
        /// F7：次へ
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 次へ
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "次へ", 1);

                switch (_dp.ProcDspType)
                {
                    case AplInfo.EditType.NEW:
                        string msg = string.Format("新規画面：{0} はまだ保存していません。破棄しますか？", DspDstDspId);
                        if (!MessageBox.Show(msg, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                        {
                            return;
                        }
                        break;
                    case AplInfo.EditType.UPDATE:
                        if (CheckEdited() && !ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0106).Equals(DialogResult.Yes))
                        {
                            return;
                        }
                        break;
                    case AplInfo.EditType.COPY:
                        if (!ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0141, _dp.DspId).Equals(DialogResult.Yes))
                        {
                            return;
                        }
                        break;
                    default:
                        return;
                }

                // 最新取得（編集情報破棄）
                _itemMgr.FetchAllData();

                _ctl.SetDispPramsDsp(AplInfo.EditType.UPDATE, 0, _itemMgr.DispParams.NextDspId);

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
        /// F8：画面調整
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 画面調整
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面調整", 1);

                // 更新モードで更新がなくても進む
                if (!_dp.ProcDspType.Equals(AplInfo.EditType.UPDATE) || CheckEdited())
                {
                    // 更新する
                    if (!UpdateDsp())
                    {
                        return;
                    }

                    // 更新フラグ
                    _itemMgr.DispParams.IsDspUpdate = true;
                }

                // 最新取得
                _itemMgr.FetchAllData();

                // 業務画面調整画面
                DspAdjustForm form = new DspAdjustForm();
                form.InitializeForm(_ctl);
                form.ShowDialog();

                // 最新取得
                _itemMgr.FetchAllData();

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
        /// F12：更新
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 更新
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "更新", 1);

                // 更新モードで更新がない場合は何もしない
                if (_dp.ProcDspType.Equals(AplInfo.EditType.UPDATE) && !CheckEdited())
                {
                    return;
                }

                // 更新
                if (!UpdateDsp())
                {
                    return;
                }

                // 更新フラグ
                _itemMgr.DispParams.IsDspUpdate = true;

                // 最新取得
                _itemMgr.FetchAllData();

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
        /// 新規画面パラメータを作成する
        /// </summary>
        private void SetNewDspParams(int newDspId)
        {
            if (!_itemMgr.GymParam[_dp.GymId].DspInfos.ContainsKey(_dp.DspId))
            {
                DspInfo dsp = new DspInfo(_ctl.HoseiItemMode);
                _itemMgr.GymParam[_dp.GymId].DspInfos.Add(_dp.DspId, dsp);
            }
            _dsp.org_dsp_param = new TBL_DSP_PARAM(_dp.GymId, _dp.DspId, AppInfo.Setting.SchemaBankCD);
            _dsp.org_img_param = new TBL_IMG_PARAM(_dp.GymId, _dp.DspId, AppInfo.Setting.SchemaBankCD);
            _dsp.org_hosei_param = new TBL_HOSEIMODE_PARAM(_dp.GymId, _dp.DspId, _ctl.HoseiItemMode, AppInfo.Setting.SchemaBankCD);
            _dsp.org_dsp_items = new SortedList<int, TBL_DSP_ITEM>();
            _dsp.org_hosei_items = new SortedList<int, TBL_HOSEIMODE_DSP_ITEM>();
            _dsp.org_img_cursor_params = new SortedList<int, TBL_IMG_CURSOR_PARAM>();
        }

        /// <summary>
        /// コピー画面パラメータを作成する
        /// </summary>
        private void SetCopyDspParams(int srcDspId, int dstDspId)
        {
            string filter = "";
            string sort = "";
            DataRow[] filterRows;
            if (!_itemMgr.GymParam[_dp.GymId].DspInfos.ContainsKey(dstDspId))
            {
                DspInfo dsp = new DspInfo(_ctl.HoseiItemMode, true);
                _itemMgr.GymParam[_dp.GymId].DspInfos.Add(dstDspId, dsp);
            }

            // 画面パラメータ
            filter = "";
            filter += string.Format("GYM_ID={0}{1}", _dp.GymId, " AND ");
            filter += string.Format("DSP_ID={0}{1}", srcDspId, "");
            filterRows = _itemMgr.MasterDspParam.dsp_params.Select(filter, sort);
            if (filterRows.Length > 0)
            {
                _dsp.SetDspParam(filterRows[0]);
            }

            // イメージパラメータ
            filter = "";
            filter += string.Format("GYM_ID={0}{1}", _dp.GymId, " AND ");
            filter += string.Format("DSP_ID={0}{1}", srcDspId, "");
            sort = "";
            filterRows = _itemMgr.MasterDspParam.img_params.Select(filter, sort);
            if (filterRows.Length > 0)
            {
                _dsp.SetImgParam(filterRows[0]);
                // 0だといろいろ問題が起こるので1以上の値を設定する
                if (_dsp.img_param.m_REDUCE_RATE < 1)
                {
                    _dsp.img_param.m_REDUCE_RATE = 1;
                }
            }

            // 補正モードパラメータ
            filter = "";
            filter += string.Format("GYM_ID={0}{1}", _dp.GymId, " AND ");
            filter += string.Format("DSP_ID={0}{1}", srcDspId, " AND ");
            filter += string.Format("HOSEI_ITEMMODE={0}{1}", _ctl.HoseiItemMode, "");
            sort = "";
            filterRows = _itemMgr.MasterDspParam.hosei_params.Select(filter, sort);
            if (filterRows.Length > 0)
            {
                _dsp.SetHoseiParam(filterRows[0]);
            }

            // 画面項目定義
            filter = "";
            filter += string.Format("GYM_ID={0}{1}", _dp.GymId, " AND ");
            filter += string.Format("DSP_ID={0}{1}", srcDspId, "");
            sort = "ITEM_ID";
            filterRows = _itemMgr.MasterDspParam.dsp_items.Select(filter, sort);
            for (int j = 0; j < filterRows.Length; j++)
            {
                _dsp.AddDspItem(filterRows[j]);
            }

            // イメージカーソルパラメータ
            filter = "";
            filter += string.Format("GYM_ID={0}{1}", _dp.GymId, " AND ");
            filter += string.Format("DSP_ID={0}{1}", srcDspId, "");
            sort = "ITEM_ID";
            filterRows = _itemMgr.MasterDspParam.img_cursor_params.Select(filter, sort);
            for (int j = 0; j < filterRows.Length; j++)
            {
                _dsp.AddImgCursor(filterRows[j]);
            }

            // 補正モード画面項目定義
            filter = "";
            filter += string.Format("GYM_ID={0}{1}", _dp.GymId, " AND ");
            filter += string.Format("DSP_ID={0}{1}", srcDspId, " AND ");
            filter += string.Format("HOSEI_ITEMMODE={0}{1}", _ctl.HoseiItemMode, "");
            sort = "ITEM_ID";
            filterRows = _itemMgr.MasterDspParam.hosei_items.Select(filter, sort);
            for (int j = 0; j < filterRows.Length; j++)
            {
                _dsp.AddHoseiItem(filterRows[j]);
            }
        }

        /// <summary>
        /// 更新チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckEdited()
        {
            switch (_dp.ProcDspType)
            {
                case AplInfo.EditType.NEW:
                case AplInfo.EditType.UPDATE:
                    if (CheckItemsEdited() || CheckGridviewEdited())
                    {
                        return true;
                    }
                    break;
                case AplInfo.EditType.COPY:
                    if (CheckItemsEdited() || CheckGridviewEdited())
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
        /// 更新チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckItemsEdited()
        {
            // 画面のデータを取得
            TBL_DSP_PARAM form_dsp_param = GetFormDspParams();
            TBL_HOSEIMODE_PARAM form_hosei_param = GetFormHoseiParams();
            TBL_IMG_PARAM form_img_param = GetFormImageParams();
            // オリジナルを取得
            TBL_DSP_PARAM org_dsp_param = _dsp.org_dsp_param;
            TBL_HOSEIMODE_PARAM org_hosei_param = _dsp.org_hosei_param;
            TBL_IMG_PARAM org_img_param = _dsp.org_img_param;

            // オリジナルと画面を比較する
            // TBL_DSP_PARAM
            if (!form_dsp_param.m_DSP_NAME.Equals(org_dsp_param.m_DSP_NAME)) { return true; }
            if (!form_dsp_param.m_FONT_SIZE.Equals(org_dsp_param.m_FONT_SIZE)) { return true; }
            if (!form_dsp_param.m_DSP_WIDTH.Equals(org_dsp_param.m_DSP_WIDTH)) { return true; }
            if (!form_dsp_param.m_DSP_HEIGHT.Equals(org_dsp_param.m_DSP_HEIGHT)) { return true; }
            if (!form_dsp_param.m_OCR_NAME.Equals(org_dsp_param.m_OCR_NAME)) { return true; }
            // TBL_HOSEIMODE_PARAM
            if (!form_hosei_param.m_AUTO_SKIP_MODE_ENT.Equals(org_hosei_param.m_AUTO_SKIP_MODE_ENT)) { return true; }
            if (!form_hosei_param.m_AUTO_SKIP_MODE_VFY.Equals(org_hosei_param.m_AUTO_SKIP_MODE_VFY)) { return true; }
            if (!form_hosei_param.m_VERY_MODE.Equals(org_hosei_param.m_VERY_MODE)) { return true; }
            // TBL_IMG_PARAM
            if (!form_img_param.m_IMG_FILE.Equals(org_img_param.m_IMG_FILE)) { return true; }
            return false;
        }

        /// <summary>
        /// 更新チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckGridviewEdited()
        {
            // オリジナルとGridViewのデータとの比較
            // GridViewの列数を取得。最終列は追加列として自動的に入ったものなので-1する。
            // 行数が合わなければそもそも変更されている
            if (_dsp.org_hosei_items.Count != dgvDspItems.Rows.Count) { return true; }

            // 両方とも0件なら見るまでもなく変更されていない（Grid上も0行）
            if (_dsp.org_hosei_items.Count == 0) { return false; }

            // 画面のデータを取得
            foreach (TBL_HOSEIMODE_DSP_ITEM org_hosei_item in _dsp.org_hosei_items.Values)
            {
                int itemid = org_hosei_item._ITEM_ID;

                // TBL_HOSEIMODE_DSP_ITEM
                TBL_HOSEIMODE_DSP_ITEM form_hosei_item = GetFormHoseiDspItems(itemid);
                if (!form_hosei_item.m_NAME_POS_TOP.Equals(org_hosei_item.m_NAME_POS_TOP)) { return true; }
                if (!form_hosei_item.m_NAME_POS_LEFT.Equals(org_hosei_item.m_NAME_POS_LEFT)) { return true; }
                if (!form_hosei_item.m_INPUT_POS_TOP.Equals(org_hosei_item.m_INPUT_POS_TOP)) { return true; }
                if (!form_hosei_item.m_INPUT_POS_LEFT.Equals(org_hosei_item.m_INPUT_POS_LEFT)) { return true; }
                if (!form_hosei_item.m_INPUT_WIDTH.Equals(org_hosei_item.m_INPUT_WIDTH)) { return true; }
                if (!form_hosei_item.m_INPUT_HEIGHT.Equals(org_hosei_item.m_INPUT_HEIGHT)) { return true; }
                if (!form_hosei_item.m_INPUT_SEQ.Equals(org_hosei_item.m_INPUT_SEQ)) { return true; }

                // TBL_DSP_ITEM
                if (_dsp.org_dsp_items.ContainsKey(itemid))
                {
                    TBL_DSP_ITEM org_dsp_item = _dsp.org_dsp_items[itemid];
                    TBL_DSP_ITEM form_dsp_item = GetFormDspItems(org_dsp_item._ITEM_ID);
                    if (!form_dsp_item.m_ITEM_DISPNAME.Equals(org_dsp_item.m_ITEM_DISPNAME)) { return true; }
                    if (!form_dsp_item.m_ITEM_TYPE.Equals(org_dsp_item.m_ITEM_TYPE)) { return true; }
                    if (!form_dsp_item.m_ITEM_LEN.Equals(org_dsp_item.m_ITEM_LEN)) { return true; }
                    if (!form_dsp_item.m_POS.Equals(org_dsp_item.m_POS)) { return true; }
                    if (!form_dsp_item.m_DUP.Equals(org_dsp_item.m_DUP)) { return true; }
                    if (!form_dsp_item.m_AUTO_INPUT.Equals(org_dsp_item.m_AUTO_INPUT)) { return true; }
                    if (!form_dsp_item.m_ITEM_SUBRTN.Equals(org_dsp_item.m_ITEM_SUBRTN)) { return true; }
                }

                // TBL_IMG_CURSOR_PARAM
                if (_dsp.org_img_cursor_params.ContainsKey(itemid))
                {
                    TBL_IMG_CURSOR_PARAM org_img_cursor_param = _dsp.org_img_cursor_params[itemid];
                    TBL_IMG_CURSOR_PARAM form_img_cursor_param = GetFormImgCorsorParams(org_img_cursor_param._ITEM_ID);
                    if (!org_img_cursor_param.m_ITEM_TOP.Equals(org_img_cursor_param.m_ITEM_TOP)) { return true; }
                    if (!org_img_cursor_param.m_ITEM_LEFT.Equals(org_img_cursor_param.m_ITEM_LEFT)) { return true; }
                    if (!org_img_cursor_param.m_ITEM_WIDTH.Equals(org_img_cursor_param.m_ITEM_WIDTH)) { return true; }
                    if (!org_img_cursor_param.m_ITEM_HEIGHT.Equals(org_img_cursor_param.m_ITEM_HEIGHT)) { return true; }
                }
            }
            return false;
        }

        /// <summary>
        /// 画面項目を取得する（TBL_DSP_PARAM）
        /// </summary>
        /// <param name="dspid"></param>
        /// <returns></returns>
        private TBL_DSP_PARAM GetFormDspParams()
        {
            TBL_DSP_PARAM data = new TBL_DSP_PARAM(_dp.GymId, _dp.DspId, AppInfo.Setting.SchemaBankCD);
            data.m_DSP_NAME = ktbDspName.Text.Trim();
            data.m_FONT_SIZE = DBConvert.ToIntNull(cmbFontSize.SelectedValue.ToString());
            data.m_OCR_NAME = data.m_DSP_NAME;

            // 画面で編集しない項目
            data.m_DSP_WIDTH = _dsp.org_dsp_param.m_DSP_WIDTH;
            data.m_DSP_HEIGHT = _dsp.org_dsp_param.m_DSP_HEIGHT;
            return data;
        }

        /// <summary>
        /// 画面項目を取得する（TBL_HOSEIMODE_PARAM）
        /// </summary>
        /// <param name="dspid"></param>
        /// <returns></returns>
        private TBL_HOSEIMODE_PARAM GetFormHoseiParams()
        {
            TBL_HOSEIMODE_PARAM data = new TBL_HOSEIMODE_PARAM(_dp.GymId, _dp.DspId, _ctl.HoseiItemMode, AppInfo.Setting.SchemaBankCD);
            data.m_AUTO_SKIP_MODE_ENT = chkAutoEnt.Checked ? 1 : 0;
            data.m_AUTO_SKIP_MODE_VFY = chkAutoVfy.Checked ? 1 : 0;
            data.m_VERY_MODE = chkVfy.Checked ? 1 : 0;
            return data;
        }

        /// <summary>
        /// 画面項目を取得する（TBL_IMG_PARAM）
        /// </summary>
        /// <param name="dspid"></param>
        /// <returns></returns>
        private TBL_IMG_PARAM GetFormImageParams()
        {
            TBL_IMG_PARAM data = new TBL_IMG_PARAM(_dp.GymId, _dp.DspId, AppInfo.Setting.SchemaBankCD);
            data.m_IMG_FILE = tbImgFile.Text.Trim();

            // イメージ詳細画面で編集して保持している項目
            data.m_REDUCE_RATE = _dsp.img_param.m_REDUCE_RATE;
            data.m_IMG_TOP = _dsp.img_param.m_IMG_TOP;
            data.m_IMG_LEFT = _dsp.img_param.m_IMG_LEFT;
            data.m_IMG_WIDTH = _dsp.img_param.m_IMG_WIDTH;
            data.m_IMG_HEIGHT = _dsp.img_param.m_IMG_HEIGHT;
            data.m_IMG_BASE_POINT= _dsp.img_param.m_IMG_BASE_POINT;
            data.m_XSCROLL_LEFT = _dsp.img_param.m_XSCROLL_LEFT;
            data.m_XSCROLL_VALUE = _dsp.img_param.m_XSCROLL_VALUE;
            data.m_XSCROLL_RIGHT = _dsp.img_param.m_XSCROLL_RIGHT;
            return data;
        }

        /// <summary>
        /// 画面項目を取得する（HOSEIMODE_DSP_ITEM）
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="dsp_id"></param>
        /// <returns></returns>
        private TBL_HOSEIMODE_DSP_ITEM GetFormHoseiDspItems(int itemid)
        {
            TBL_HOSEIMODE_DSP_ITEM data = new TBL_HOSEIMODE_DSP_ITEM(_dp.GymId, _dp.DspId, _ctl.HoseiItemMode, itemid, AppInfo.Setting.SchemaBankCD);
            data.m_NAME_POS_TOP = GetDataGridIntValue("NAME_POS_TOP", itemid);
            data.m_NAME_POS_LEFT = GetDataGridIntValue("NAME_POS_LEFT", itemid);
            data.m_INPUT_POS_TOP = GetDataGridIntValue("INPUT_POS_TOP", itemid);
            data.m_INPUT_POS_LEFT = GetDataGridIntValue("INPUT_POS_LEFT", itemid);
            data.m_INPUT_WIDTH = GetDataGridIntValue("INPUT_WIDTH", itemid);
            data.m_INPUT_HEIGHT = GetDataGridIntValue("INPUT_HEIGHT", itemid);
            data.m_INPUT_SEQ = GetDataGridIntValue("INPUT_SEQ", itemid);
            return data;
        }

        /// <summary>
        /// 画面項目を取得する（TBL_DSP_ITEM）
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private TBL_DSP_ITEM GetFormDspItems(int itemid)
        {
            TBL_DSP_ITEM data = new TBL_DSP_ITEM(_dp.GymId, _dp.DspId, itemid, AppInfo.Setting.SchemaBankCD);
            data.m_ITEM_DISPNAME = GetDataGridStrValue("ITEM_DISPNAME", itemid);
            data.m_ITEM_TYPE = GetDataGridStrValue("ITEM_TYPE", itemid);
            data.m_ITEM_LEN = GetDataGridIntValue("ITEM_LEN", itemid);
            data.m_POS = GetDataGridIntValue("POS", itemid);
            data.m_DUP = GetDataGridBoolValue("DUP", itemid);
            data.m_AUTO_INPUT = GetDataGridBoolValue("AUTO_INPUT", itemid);
            data.m_ITEM_SUBRTN = GetDataGridStrValue("ITEM_SUBRTN", itemid);
            return data;
        }

        /// <summary>
        /// 画面項目を取得する（TBL_IMG_CURSOR_PARAM）
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="dsp_id"></param>
        /// <returns></returns>
        private TBL_IMG_CURSOR_PARAM GetFormImgCorsorParams(int itemid)
        {
            TBL_IMG_CURSOR_PARAM data = new TBL_IMG_CURSOR_PARAM(_dp.GymId, _dp.DspId, itemid, AppInfo.Setting.SchemaBankCD);
            TBL_IMG_CURSOR_PARAM org = null;
            if (_dsp.org_img_cursor_params.ContainsKey(itemid))
            {
                org = _dsp.org_img_cursor_params[itemid];
                data.m_ITEM_TOP = GetDataGridIntValue("ITEM_TOP", itemid);
                data.m_ITEM_LEFT = GetDataGridIntValue("ITEM_LEFT", itemid);
                data.m_ITEM_WIDTH = GetDataGridIntValue("ITEM_WIDTH", itemid);
                data.m_ITEM_HEIGHT = GetDataGridIntValue("ITEM_HEIGHT", itemid);
                data.m_LINE_WEIGHT = org.m_LINE_WEIGHT;
                data.m_LINE_COLOR = org.m_LINE_COLOR;
            }
            return data;
        }

        /// <summary>
        /// 画面項目を生成する（HOSEIMODE_DSP_ITEM）
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="dsp_id"></param>
        /// <returns></returns>
        private TBL_HOSEIMODE_DSP_ITEM CreateHoseiDspItems(int itemid)
        {
            TBL_HOSEIMODE_DSP_ITEM data = new TBL_HOSEIMODE_DSP_ITEM(_dp.GymId, _dp.DspId, _ctl.HoseiItemMode, itemid, AppInfo.Setting.SchemaBankCD);
            return data;
        }

        /// <summary>
        /// 画面項目を生成する（TBL_DSP_ITEM）
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private TBL_DSP_ITEM CreateDspItems(int itemid)
        {
            // 既に登録済みの場合は取得する
            if (_dsp.dsp_items.ContainsKey(itemid))
            {
                return _dsp.dsp_items[itemid];
            }

            // 未登録の場合は新規生成
            TBL_ITEM_MASTER itemMst = null;
            foreach (DataRow row in _itemMgr.MasterDspParam.item_masters.Rows)
            {
                TBL_ITEM_MASTER mst = new TBL_ITEM_MASTER(row, AppInfo.Setting.SchemaBankCD);
                if (mst._ITEM_ID == itemid)
                {
                    itemMst = mst;
                    break;
                }
            }
            TBL_DSP_ITEM data = new TBL_DSP_ITEM(_dp.GymId, _dp.DspId, itemid, AppInfo.Setting.SchemaBankCD);
            data.m_ITEM_DISPNAME = itemMst.m_ITEM_NAME;
            return data;
        }

        /// <summary>
        /// 画面項目を生成する（TBL_IMG_CURSOR_PARAM）
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="dsp_id"></param>
        /// <returns></returns>
        private TBL_IMG_CURSOR_PARAM CreateImgCorsorParams(int itemid)
        {
            TBL_IMG_CURSOR_PARAM data = new TBL_IMG_CURSOR_PARAM(_dp.GymId, _dp.DspId, itemid, AppInfo.Setting.SchemaBankCD);
            return data;
        }

        /// <summary>
        /// DataGrid の値を取得する
        /// </summary>
        /// <param name="colname"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetDataGridStrValue(string colname, int itemid)
        {
            foreach (DataGridViewRow dgv in dgvDspItems.Rows)
            {
                int id = DBConvert.ToIntNull(dgv.Cells["ITEM_ID"].Value);
                if (itemid == id)
                {
                    return DBConvert.ToStringNull(dgv.Cells[colname].Value);
                }
            }
            return "";
        }

        /// <summary>
        /// DataGrid の値を取得する
        /// </summary>
        /// <param name="colname"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private int GetDataGridIntValue(string colname, int itemid)
        {
            return DBConvert.ToIntNull(GetDataGridStrValue(colname, itemid));
        }

        /// <summary>
        /// DataGrid の値を取得する
        /// </summary>
        /// <param name="colname"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetDataGridBoolValue(string colname, int itemid)
        {
            return (GetDataGridStrValue(colname, itemid).Equals("True") ? "1" : "0");
        }

        /// <summary>
        /// 画面パラメーター更新
        /// </summary>
        /// <returns></returns>
        private bool UpdateDsp()
        {
            // データ入力検証
            if (!CheckFormInput() || !CheckGridInput())
            {
                return false;
            }

            if (_dp.ProcDspType.Equals(AplInfo.EditType.UPDATE))
            {
                if (_itemMgr.CheckTrMeiExists(_dp.GymId, _dp.DspId)
                    && !DialogResult.Yes.Equals(ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0109, _dp.GymId, "項目パラメータ修正の場合")))
                {
                    return false;
                }
            }

            // 画面パラメーターを登録する
            if (InsertDspInfo())
            {
                // 正常
                ComMessageMgr.MessageInformation(EntMessageMgr.I0122);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面パラメーター登録成功, 画面ID:" + DspDstDspId, 1);
            }
            else
            {
                // 失敗
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.E0104, DspDstDspId));
                ComMessageMgr.MessageWarning(EntMessageMgr.E0104, DspDstDspId);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面パラメーター登録失敗, 画面ID:" + DspDstDspId, 3);
                return false;
            }

            _ctl.SetDispPramsDsp(AplInfo.EditType.UPDATE, 0, _dp.DspId);
            return true;
        }

        /// <summary>
        /// 入力検証
        /// </summary>
        /// <returns></returns>
        private bool CheckFormInput()
        {
            if (string.IsNullOrEmpty(ktbDspName.Text.Trim()))
            {
                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.W0115));
                ComMessageMgr.MessageWarning(EntMessageMgr.W0115);
                ktbDspName.Focus();
                return false;
            }

            string[] OCRNames = tbOCR.Text.Split(Environment.NewLine.ToCharArray());
            for (int i = 0; i < OCRNames.Length; i++)
            {
                if (OCRNames[i].Length > 20)
                {
                    int no = DBConvert.ToIntNull(i + 1);
                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.W0137, no));
                    ComMessageMgr.MessageWarning(EntMessageMgr.W0137, no);
                    tbOCR.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 入力検証
        /// </summary>
        /// <returns></returns>
        private bool CheckGridInput()
        {
            // 各項目の検証
            foreach (DataGridViewRow dgv in dgvDspItems.Rows)
            {
                if (dgv.IsNewRow) { continue; }
                foreach (DataGridViewCell dgvc in dgv.Cells)
                {
                    switch (dgvc.OwningColumn.Name)
                    {
                        case "ITEM_DISPNAME":
                        case "ITEM_TYPE":
                        case "ITEM_LEN":
                        case "POS":
                        case "DUP":
                        case "AUTO_INPUT":
                        case "NAME_POS_TOP":
                        case "NAME_POS_LEFT":
                        case "INPUT_POS_TOP":
                        case "INPUT_POS_LEFT":
                        case "INPUT_WIDTH":
                        case "INPUT_HEIGHT":
                        case "INPUT_SEQ":
                            if (DBConvert.ToStringNull(dgvc.Value).Equals(""))
                            {
                                SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.W0117));
                                ComMessageMgr.MessageWarning(EntMessageMgr.W0117);
                                dgvc.Selected = true;
                                return false;
                            }
                            break;
                        case "ITEM_SUBRTN":
                            if (DBConvert.ToStringNull(dgvc.Value).Equals("")) { break; }
                            foreach (string val in DBConvert.ToStringNull(dgvc.Value).Split(','))
                            {
                                if (!TBL_SUB_RTN.CheckSubrtn(val.Trim()))
                                {
                                    SetStatusMessage(ComMessageMgr.Msg(EntMessageMgr.W0138));
                                    ComMessageMgr.MessageWarning(EntMessageMgr.W0138);
                                    dgvc.Selected = true;
                                    return false;
                                }
                            }
                            break;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 画面パラメーターを登録する
        /// </summary>
        /// <returns></returns>
        private bool InsertDspInfo()
        {
            ItemManager.DspParams dsp = new ItemManager.DspParams();
            dsp.dsp_param = GetFormDspParams();
            dsp.hosei_param = GetFormHoseiParams();
            dsp.img_param = GetFormImageParams();

            // アイテム
            dsp.dsp_items = new SortedDictionary<int, TBL_DSP_ITEM>();
            dsp.hosei_items = new SortedDictionary<int, TBL_HOSEIMODE_DSP_ITEM>();
            dsp.img_coursors = new SortedDictionary<int, TBL_IMG_CURSOR_PARAM>();
            foreach (DataGridViewRow dgv in dgvDspItems.Rows)
            {
                int itemid = DBConvert.ToIntNull(dgv.Cells["ITEM_ID"].Value);
                TBL_DSP_ITEM dsp_item = GetFormDspItems(itemid);
                TBL_HOSEIMODE_DSP_ITEM hosei_item = GetFormHoseiDspItems(itemid);
                TBL_IMG_CURSOR_PARAM img_cursor = GetFormImgCorsorParams(itemid);
                dsp.dsp_items.Add(dsp_item._ITEM_ID, dsp_item);
                dsp.hosei_items.Add(hosei_item._ITEM_ID, hosei_item);
                dsp.img_coursors.Add(img_cursor._ITEM_ID, img_cursor);
            }

            // DB更新
            if (!_itemMgr.RegistDspParam(dsp))
            {
                return false;
            }
            return true;
        }
    }
}
