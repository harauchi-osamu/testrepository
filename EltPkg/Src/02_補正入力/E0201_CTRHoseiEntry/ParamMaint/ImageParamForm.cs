using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    /// イメージ詳細設定画面
    /// </summary>
    public partial class ImageParamForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ItemManager.DisplayParams _dp { get { return _itemMgr.DispParams; } }
        private DspInfo _dsp { get { return _itemMgr.GymParam[_dp.GymId].DspInfos[_dp.DspId]; } }

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
        public ImageParamForm()
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
            base.SetDispName2("イメージ詳細設定");
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
            SetFunctionName(F6_, string.Empty);
            SetFunctionName(F7_, string.Empty);
            SetFunctionName(F8_, string.Empty);
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
            TBL_IMG_PARAM img_param = _dsp.img_param;

            ntbReduction.Text = DBConvert.ToStringNull(img_param.m_REDUCE_RATE);
            ntbBaseTop.Text = DBConvert.ToStringNull(img_param.m_IMG_TOP);
            ntbBaseLeft.Text = DBConvert.ToStringNull(img_param.m_IMG_LEFT);
            ntbWidth.Text = DBConvert.ToStringNull(img_param.m_IMG_WIDTH);
            ntbHeight.Text = DBConvert.ToStringNull(img_param.m_IMG_HEIGHT);
            tbImgFile.Text = DBConvert.ToStringNull(img_param.m_IMG_FILE);
            ntbImgBase.Text = DBConvert.ToStringNull(img_param.m_IMG_BASE_POINT);
            ntbScroolPos.Text = DBConvert.ToStringNull(img_param.m_XSCROLL_VALUE);
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            TBL_IMG_PARAM img_param = _dsp.img_param;

            img_param.m_REDUCE_RATE = DBConvert.ToIntNull(ntbReduction.Text);
            // 0だといろいろ問題が起こるので1以上の値を設定する
            if (img_param.m_REDUCE_RATE < 1)
            {
                img_param.m_REDUCE_RATE = 1;
            }
            img_param.m_IMG_TOP = DBConvert.ToIntNull(ntbBaseTop.Text);
            img_param.m_IMG_LEFT = DBConvert.ToIntNull(ntbBaseLeft.Text);
            img_param.m_IMG_WIDTH = DBConvert.ToIntNull(ntbWidth.Text);
            img_param.m_IMG_HEIGHT = DBConvert.ToIntNull(ntbHeight.Text);
            img_param.m_IMG_FILE = tbImgFile.Text;
            img_param.m_IMG_BASE_POINT = DBConvert.ToIntNull(ntbImgBase.Text);
            img_param.m_XSCROLL_VALUE = DBConvert.ToIntNull(ntbScroolPos.Text);
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

                this.DialogResult = DialogResult.Cancel;
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
        /// F12：確定
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 確定
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);

                // 更新処理は行わずメモリ保持のみ
                GetDisplayParams();

                this.DialogResult = DialogResult.OK;
                this.Close();
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

    }
}
