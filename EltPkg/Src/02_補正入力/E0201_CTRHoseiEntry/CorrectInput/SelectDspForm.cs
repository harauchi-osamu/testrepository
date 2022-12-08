using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;
using ImageController;

namespace CorrectInput
{
    /// <summary>
    /// 証券種類選択画面
    /// </summary>
    public partial class SelectDspForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        protected EntryController _econ { get { return _itemMgr.EntController; } }
        protected EntryDspControl _dcon { get { return _itemMgr.DspControl; } }
        protected EntryImageHandler eiHandler { get { return _itemMgr.ImageHandler; } }
        protected EntryInputChecker eiChecker { get { return _itemMgr.Checker; } }
        protected EntryDataUpdater edUpdater { get { return _itemMgr.Updater; } }
        protected MeisaiInfo _curMei { get { return _itemMgr.CurBat.CurMei; } }

        public int DspId { get; private set; } = 0;
        public int BillCode { get; private set; } = 0;
        public string BillName { get; private set; } = "";

        private SortedDictionary<int, TBL_DSP_PARAM> _dsp_params = null;
        private SortedDictionary<int, TBL_IMG_PARAM> _img_params = null;
        private string _imgFilePath = "";
        private bool _isEventStop = false;

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
        public SelectDspForm()
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
            base.SetDispName1(string.Format("交換{0} 補正入力", CommonTable.DB.GymParam.GymId.GetName(_ctl.GymId)));
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("証券種類選択");
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
                SetFunctionName(F8_, string.Empty);
                SetFunctionName(F9_, string.Empty);
                SetFunctionName(F10_, string.Empty);
                SetFunctionName(F11_, string.Empty);
                SetFunctionName(F12_, "確定");
            }
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
            // 表再送分があれば表再送分を優先
            ImageInfo img = _curMei.GetImageInfo(TrMeiImg.ImgKbn.表再送分);
            if (img == null)
            {
                img = _curMei.GetImageInfo(TrMeiImg.ImgKbn.表);
            }
            _imgFilePath = _econ.GetImgFilePath(img);

            // TBL_DSP_PARAM
            _dsp_params = new SortedDictionary<int, TBL_DSP_PARAM>();
            foreach (DataRow row in _itemMgr.MasterDspParam.dsp_params.Rows)
            {
                TBL_DSP_PARAM data = new TBL_DSP_PARAM(row, AppInfo.Setting.SchemaBankCD);
                _dsp_params.Add(data._DSP_ID, data);
            }

            // TBL_IMG_PARAM
            _img_params = new SortedDictionary<int, TBL_IMG_PARAM>();
            foreach (DataRow row in _itemMgr.MasterDspParam.img_params.Rows)
            {
                TBL_IMG_PARAM data = new TBL_IMG_PARAM(row, AppInfo.Setting.SchemaBankCD);
                _img_params.Add(data._DSP_ID, data);
            }
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
            int Item = 0;
            int SelectItem = 0;
            ListViewItem[] lvts = new ListViewItem[_dsp_params.Count];
            livDspList.Items.Clear();
            foreach (TBL_DSP_PARAM dsp in _dsp_params.Values)
            {
                List<string> lvlist = new List<string>();
                lvlist.Add(dsp._DSP_ID.ToString(Const.DSP_ID_LEN_STR));
                lvlist.Add(dsp.m_DSP_NAME);

                ListViewItem lvi = new ListViewItem(lvlist.ToArray());
                livDspList.Items.Add(lvi);

                //選択状態の復元
                if (_curMei._DSP_ID == dsp._DSP_ID)
                {
                    SelectItem = Item;
                }

                Item++;
            }
            if (livDspList.Items.Count > 0)
            {
                _isEventStop = true;
                livDspList.Items[SelectItem].Selected = true;
                livDspList.Items[SelectItem].Focused = true;
                _isEventStop = false;
            }

            // 当該帳票の画像を表示
            SetCurrentImg();

            // サンプル画像を表示
            SetSampleImg();
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
        /// Form_Load
        /// </summary>
        private void SelectDspForm_Load(object sender, EventArgs e)
        {
            // リスト一覧を再表示
            // 原因は不明だが、
            // ここで再表示すると初回で矢印キーで移動する際、先頭に戻る動作が回避される
            SetDisplayParams();
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
                    btnFunc12_Click(sender, e);
                    break;
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
        /// [画面一覧]リスト SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void livDspList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isEventStop) { return; }
                if (livDspList.SelectedIndices.Count == 0) { return; }

                // サンプル画像を表示
                SetSampleImg();
            }
            catch (Exception ex)
            {
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// [イメージ] Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (((PictureBox)sender).SizeMode.Equals(PictureBoxSizeMode.Normal))
                {
                    ((PictureBox)sender).SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    ((PictureBox)sender).SizeMode = PictureBoxSizeMode.Normal;
                }
                ((PictureBox)sender).Refresh();
            }
            catch (Exception ex)
            {
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F01：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);

                DspId = _curMei._DSP_ID;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
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
        /// F12：確定
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);

                if (livDspList.SelectedItems.Count < 1)
                {
                    return;
                }
                DspId = DBConvert.ToIntNull(livDspList.SelectedItems[0].Text);
                // 交換証券種類設定
                SetBillCode(DspId);

                this.DialogResult = DialogResult.OK;
                this.Close();
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
        /// サンプルイメージを表示
        /// </summary>
        /// <param name="dspid"></param>
        private void SetSampleImg()
        {
            if (livDspList.SelectedIndices.Count == 0) { return; }
            int dspid = DBConvert.ToIntNull(livDspList.SelectedItems[0].Text);
            if (!_img_params.ContainsKey(dspid))
            {
                return;
            }
            string sampleFilePath = Path.Combine(ServerIni.Setting.BankSampleImagePath, _img_params[dspid].m_IMG_FILE);
            if (!File.Exists(sampleFilePath))
            {
                return;
            }
            CommonUtil.DisposeImage(pbSample.Image);
            pbSample.Image = ImageEditor.CreateBitmap(sampleFilePath);
            pbSample.Refresh();
        }

        /// <summary>
        /// 明細に表示されているイメージを表示
        /// </summary>
        private void SetCurrentImg()
        {
            if (!eiHandler.HasImage) { return; }
            if (!File.Exists(_imgFilePath))
            {
                return;
            }
            CommonUtil.DisposeImage(pbCurrent.Image);
            pbCurrent.Image = ImageEditor.CreateBitmap(_imgFilePath);
            pbCurrent.Refresh();
        }

        /// <summary>
        /// 交換証券種類を設定
        /// </summary>
        /// <param name="id"></param>
        private void SetBillCode(int id)
        {
            EntryReplacer er = _ctl.GetEntReplacer();

            // DSPIDから交換証券種類を取得
            int billCd = er.GetDspIDBillCode(id);
            if (billCd < 0)
            {
                // 交換証券種類取得エラー
                throw new Exception("交換証券種類コード取得エラー");
            }
            // 交換証券種類名
            string sBillName = er.GetBillName(DBConvert.ToIntNull(billCd));

            // 取得値設定
            this.BillCode = billCd;
            this.BillName = sBillName;
        }

    }
}
