using System;
using System.Drawing;
using System.Windows.Forms;
using IIPCommonClass;
using IIPCommonClass.Log;
using IIPCommonClass.DB;
using System.Collections.Generic;

namespace IIPReference
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class AppMainForm : FormBase
    {
        List<DBManager> _dBs;
        int _serverCount;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AppMainForm(List<DBManager> listDb, int serCnt)
        {
            _dBs = listDb;
            _serverCount = serCnt;
       
            InitializeComponent();
            
        }

        #region メンバー

        private System.Windows.Forms.Label _lblMainDispName;
        protected GymListControl _theGymListControl;

        #endregion

        #region イベント処理

        /// <summary>
        /// フォームロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                // 画面初期化処理
                base.DspInit();
                base.SetOpDateLabel(int.Parse(AppInfo.OpeDate()));
                base.SetServerName(Properties.Settings.Default.DISPENV);
                EnabledButtons();
                //this.ServerConnent1.UpdateServerInfo();
            }
            catch (Exception ex)
            {
                //TODO:エラーログ
                this.SetStatusMessage(IniFileAccess.GetStatusMsg("E0401", ex.Message));
                IniFileAccess.ShowMsgBox("E0401", ex.Message);
                // 業務ログに一時的に吐く
                ProcessLog.getInstance().writeLog(ex.ToString());
            }
        }

        public bool InitializeForm() {
            _theGymListControl.InitializeControl(_dBs, _serverCount);
            return true;
        }

        /// <summary>
        /// フォームキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _theGymListControl.ShowBatchList();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// フォームキーアップイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            //base.ChangeButtonName(e, false);
        }

        virtual protected void btnFuncEvent(Keys _funcKey)
        {
            try
            {
                switch (_funcKey)
                {
                    case Keys.F1://戻る
                        this.Close();
                        break;
                    case Keys.F5://更新
                        _theGymListControl.SetDispItem();
                        _theGymListControl.Focus();
                        //this.ServerConnent1.UpdateServerInfo();
                        break;
                    case Keys.F12://詳細
                        _theGymListControl.ShowBatchList();
                        break;
                }
                //EnabledButtons();
            }
            catch (Exception ex)
            { // TODO:エラーログ
                this.SetStatusMessage(IniFileAccess.GetStatusMsg("E0401", ex.Message));
                IniFileAccess.ShowMsgBox("E0401", ex.Message);
                // 業務ログに一時的に吐く
                ProcessLog.getInstance().writeLog(ex.ToString());
            }
        }
        /// <summary>
        /// F1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            base.btnFunc01_Click(sender, e);
            btnFuncEvent(Keys.F1);
        }
        /// <summary>
        /// F2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            base.btnFunc02_Click(sender, e);
            btnFuncEvent(Keys.F2);
        }
        /// <summary>
        /// F3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            base.btnFunc03_Click(sender, e);
            btnFuncEvent(Keys.F3);
        }
        /// <summary>
        /// F4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            base.btnFunc04_Click(sender, e);
            btnFuncEvent(Keys.F4);
        }
        /// <summary>
        /// F5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            base.btnFunc05_Click(sender, e);
            btnFuncEvent(Keys.F5);
        }
        /// <summary>
        /// F6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            base.btnFunc06_Click(sender, e);
            btnFuncEvent(Keys.F6);
        }
        /// <summary>
        /// F7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            base.btnFunc07_Click(sender, e);
            btnFuncEvent(Keys.F7);
        }
        /// <summary>
        /// F8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            base.btnFunc08_Click(sender, e);
            btnFuncEvent(Keys.F8);
        }
        /// <summary>
        /// F9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            base.btnFunc09_Click(sender, e);
            btnFuncEvent(Keys.F9);
        }
        /// <summary>
        /// F10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            base.btnFunc10_Click(sender, e);
            btnFuncEvent(Keys.F10);
        }
        /// <summary>
        /// F11
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            base.btnFunc11_Click(sender, e);
            btnFuncEvent(Keys.F11);
        }
        /// <summary>
        /// F12
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            base.btnFunc12_Click(sender, e);
            btnFuncEvent(Keys.F12);
        }
        #endregion

        #region 画面共通
        /// <summary>
        /// 画面名称設定
        /// </summary>
        /// <param name="gym_id"></param>
        protected override void SetDspName()
        {
            if (_lblMainDispName.Text.Equals("\"\""))
            {
                _lblMainDispName.Text = AppMainConst.TITLE;
            }
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDspItem()
        {
        }

        /// <summary>
        /// Functionボタン名設定
        /// </summary>
        /// <param name="isPress">ShiftキーPressフラグ</param>
        protected override void SetFunctionButtonName(bool isPress)
        {
            for (int i = 0; i < AppMainConst.FUNCTEXT.Length; i++)
            {
                btnFunc[i + 1].Text = AppMainConst.FUNCTEXT[i];
                btnFunc[i + 1].Font = new Font(AppMainConst.FONT, AppMainConst.FUNCFontSIZE[i]);
            }
        }

        /// <summary>
        /// ボタン制御
        /// </summary>
        protected void EnabledButtons()
		{
            foreach (Button bt in btnFunc.Values)
            {
                bt.Enabled = false;
            }

            btnFunc[1].Enabled = true;
            btnFunc[2].Enabled = false;
            btnFunc[3].Enabled = false;
            btnFunc[4].Enabled = false;
            btnFunc[5].Enabled = true;
            btnFunc[6].Enabled = false;
            btnFunc[7].Enabled = false;
            btnFunc[8].Enabled = false;
            btnFunc[9].Enabled = false;
            btnFunc[10].Enabled = false;
            btnFunc[11].Enabled = false;
            btnFunc[12].Enabled = true;

        }
        public class AppMainConst
        {
            public const string TITLE = "OCR認識進捗状況照会";

            public const string FONT = "MS UI Gothic";
            public static string[] FUNCTEXT = new string[]
            { "F1:戻る",
                "F2:",
                "F3:",
                "F4:",
                "F5:更新",
                "F6:",
                "F7:",
                "F8:",
                "F9:",
                "F10:",
                "F11:",
                "F12:詳細"
            };
            public static float[] FUNCFontSIZE = new float[]
            {14.0F,// F1
                14.0F,// F2
                14.0F,// F3
                14.0F,// F4
                14.0F,// F5
                14.0F,// F6
                14.0F,// F7
                14.0F,// F8
                14.0F,// F9
                14.0F,// F10
                14.0F,// F11
                14.0F// F12
            };
        }
        #endregion

        private void _theGymListControl_Load(object sender, EventArgs e)
        {

        }
    }
}

