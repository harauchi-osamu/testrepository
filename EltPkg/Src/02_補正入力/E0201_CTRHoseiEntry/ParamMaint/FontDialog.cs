using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// フォント設定ダイアログ
    /// </summary>
	public partial class FontDialog : EntryCommonDialogBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ItemManager.DisplayParams _dp { get { return _itemMgr.DispParams; } }
        private DspInfo _dsp { get { return _itemMgr.GymParam[_dp.GymId].DspInfos[_dp.DspId]; } }
        private List<FontDialog.ItemSet> _fontSizeList = null;

        public int FontSize { get; private set; } = 0;

        /// <summary>
        /// 選択可能なフォントサイズの配列
        /// </summary>
        public static string[] FontSizes
        {
            get { return new string[] { "10", "11", "12", "14" }; }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ctl"></param>
        public FontDialog()
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

        /// <summary>
        /// フォントサイズリストを取得する
        /// </summary>
        /// <returns></returns>
        public static List<ItemSet> GetFontSizeList()
        {
            List<ItemSet> fontSizeList = new List<ItemSet>();
            foreach (string sFontSize in FontSizes)
            {
                fontSizeList.Add(new ItemSet(sFontSize, sFontSize));
            }
            return fontSizeList;
        }


        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

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
            // 初期値設定
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
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            cmbFontSize.SelectedValue = _dsp.dsp_param.m_FONT_SIZE.ToString();
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
        private void Dialog_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// [確定]ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                this.FontSize = DBConvert.ToIntNull(cmbFontSize.SelectedValue.ToString());

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// [キャンセル]ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
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
        // 内部メソッド
        // *******************************************************************


        public class ItemSet
        {
            public string ItemValue { get; set; }
            public string ItemDisp { get; set; }
            public ItemSet(string value, string disp)
            {
                ItemValue = value;
                ItemDisp = disp;
            }
        }

    }
}
