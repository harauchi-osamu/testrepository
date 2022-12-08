using System;
using System.Drawing;
using System.Windows.Forms;
using IIPCommonClass;
using IIPCommonClass.Log;
using IIPCommonClass.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIPReference
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class AppSubForm : FormBase
    {
        string _gym_Id1 = "";
        string _gym_Name = "";
        string _ope_Date = "";
        List<DBManager> _dBs;
        int _serCnt;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AppSubForm(ListViewItem _item, List<DBManager> dB, int serverCnt)
        {
            _gym_Name = _item.SubItems[1].Text;
            if (_gym_Name == "持出　通常")
            {
                _gym_Id1 = "001";
            }
            else if (_gym_Name == "持出　付帯")
            {
                _gym_Id1 = "002";
            }
            else if (_gym_Name == "持出　合計票")
            {
                _gym_Id1 = "003";
            }
            else if (_gym_Name == "持帰")
            {
                _gym_Id1 = "004";
            }
            else if (_gym_Name == "期日管理　キャプチャー")
            {
                _gym_Id1 = "005";
            }
            
            _ope_Date = AppInfo.OpeDate();

            InitializeComponent();
            _dBs = dB;
            _serCnt = serverCnt;
        }

        #region メンバー

        private System.Windows.Forms.Label _lblSubDispName;
        protected BatchListControl _theBatchListControl;

        /// <summary>Shiftキーが押されているかどうか</summary>
        protected bool IsPressShiftKey { get { return (((Control.ModifierKeys & Keys.Shift) == Keys.Shift)); } }


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
                base.SetServerName(Properties.Settings.Default.DISPENV);
                base.SetOpDateLabel(int.Parse(_ope_Date));
                EnabledButtons();
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

        public bool InitializeForm()
        {
            return _theBatchListControl.InitializeControl(_gym_Id1, _ope_Date, _dBs); ;
        }

        public void ShowLogList()
        {
            SearchLogList thefrm = null;

            try
            {
                thefrm = new SearchLogList();
                if (thefrm.InitializeForm())
                {
                    thefrm.ShowDialog();
                }
                else
                {

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        
        /// <summary>
        /// フォームキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            base.ChangeButtonName(e, IsPressShiftKey);
            checkEnabledButtons(IsPressShiftKey);

        }

        /// <summary>
        /// フォームキーアップイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            base.ChangeButtonName(e, IsPressShiftKey);
            checkEnabledButtons(IsPressShiftKey);
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
                    case Keys.F4://ｽﾃｰﾀｽ更新
                        if (IsPressShiftKey)
                        {
                            _theBatchListControl.updateOcrStatus();
                            _theBatchListControl.SetDispItem(_gym_Id1);
                        }
                        break;
                    case Keys.F5://更新
                        _theBatchListControl.SetDispItem(_gym_Id1);
                        break;
                    case Keys.F9://バッチイメージ表示
                        _theBatchListControl.ShowBatchImageView(_gym_Id1);
                        break;
                    case Keys.F12://ログ表示
                        this.ShowLogList();
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

        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected override void btn_Click(object sender, EventArgs e)
        //{
        //    this.SetFunctionButtonName(IsPressShiftKey);
        //}


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
            if (_lblSubDispName.Text.Equals("\"\""))
            {
                _lblSubDispName.Text = _gym_Name;
            }
        }
        
        /// <summary>
        /// 画面項目設定
        /// </summary>
        //protected override void SetDspItem()
        //{
        //    this.SetDspItem;
        //}

        /// <summary>
        /// Functionボタン名設定
        /// </summary>
        /// <param name="isPress">ShiftキーPressフラグ</param>
        protected override void SetFunctionButtonName(bool isPress)
        {
            if (isPress)
            {
                for (int i = 0; i < AppSubConst.FUNCTEXTshift.Length; i++)
                {
                    btnFunc[i + 1].Text = AppSubConst.FUNCTEXTshift[i];
                    btnFunc[i + 1].Font = new Font(AppSubConst.FONT, AppSubConst.FUNCFontSIZEshift[i]);
                }
                this.btnFunc[4].Enabled = true;
            }
            else
            {
                for (int i = 0; i < AppSubConst.FUNCTEXT.Length; i++)
                {
                    btnFunc[i + 1].Text = AppSubConst.FUNCTEXT[i];
                    btnFunc[i + 1].Font = new Font(AppSubConst.FONT, AppSubConst.FUNCFontSIZE[i]);
                }
                this.btnFunc[5].Enabled = true;
                this.btnFunc[9].Enabled = true;
                this.btnFunc[12].Enabled = true;
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
            btnFunc[9].Enabled = true;
            btnFunc[10].Enabled = false;
            btnFunc[11].Enabled = false;
            //2022.6.29 ログ表示ボタン非活性化 S
            //btnFunc[12].Enabled = true;
            btnFunc[12].Enabled = false;
            //2022.6.29 ログ表示ボタン非活性化 E
        }

        protected void checkEnabledButtons(bool isPress)
        {
            foreach (Button bt in btnFunc.Values)
            {
                bt.Enabled = false;
            }
            if (isPress)
            {
                btnFunc[1].Enabled = false;
                btnFunc[2].Enabled = false;
                btnFunc[3].Enabled = false;
                btnFunc[4].Enabled = true;
                btnFunc[5].Enabled = false;
                btnFunc[6].Enabled = false;
                btnFunc[7].Enabled = false;
                btnFunc[8].Enabled = false;
                btnFunc[9].Enabled = false;
                btnFunc[10].Enabled = false;
                btnFunc[11].Enabled = false;
                btnFunc[12].Enabled = false;

            }
            else
            {
                btnFunc[1].Enabled = true;
                btnFunc[2].Enabled = false;
                btnFunc[3].Enabled = false;
                btnFunc[4].Enabled = false;
                btnFunc[5].Enabled = true;
                btnFunc[6].Enabled = false;
                btnFunc[7].Enabled = false;
                btnFunc[8].Enabled = false;
                btnFunc[9].Enabled = true;
                btnFunc[10].Enabled = false;
                btnFunc[11].Enabled = false;
                //2022.6.29 ログ表示ボタン非活性化 S
                //btnFunc[12].Enabled = true;
                btnFunc[12].Enabled = false;
                //2022.6.29 ログ表示ボタン非活性化 E

            }
        }


        public class AppSubConst
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
                "F9:ﾊﾞｯﾁｲﾒｰｼﾞ",
                "F10:",
                "F11:",
                //2022.6.17 ログ表示ボタン無効化 S
                //"F12:ログ表示"
                "F12:"
                //2022.6.17 ログ表示ボタン無効化 E
            };
            public static string[] FUNCTEXTshift = new string[]
            { "F1:",
                "F2:",
                "F3:",
                "F4:ｽﾃｰﾀｽ更新",
                "F5:",
                "F6:",
                "F7:",
                "F8:",
                "F9:",
                "F10:",
                "F11:",
                "F12:"
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
                10.0F,// F9
                14.0F,// F10
                14.0F,// F11
                12.0F// F12
            };
            public static float[] FUNCFontSIZEshift = new float[]
            {14.0F,// F1
                14.0F,// F2
                14.0F,// F3
                9.0F,// F4
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
    }
}

