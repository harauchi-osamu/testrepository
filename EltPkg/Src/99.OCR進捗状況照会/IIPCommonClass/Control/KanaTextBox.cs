using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IIPCommonClass
{
    /// <summary>
    /// カナテキストボックス（カナモード入力　対応版）
    /// </summary>
    public class KanaTextBox : iBicsTextBox
    {

        protected static string ERROR_MSG0 = "TextBox処理でエラーが発生しました。";

        private ENTRYMODE m_mode;

        private bool mKanaLock=false;

        private Timer myTimer;

        protected static int VK_KANA = 0x15;
        protected const int KEYEVENTF_KEYDOWN = 0x0;
        protected const int KEYEVENTF_EXTENDEDKEY = 0x1;
        protected const int KEYEVENTF_KEYUP = 0x2;
        protected const int INPUT_MOUSE = 0;
        protected const short INPUT_KEYBOARD = 1;
        protected const int INPUT_HARDWARE = 2;

        #region プロパティ

        public bool KanaLock
        {
            get { return mKanaLock; }
            set { mKanaLock = value; }
        }

        #endregion

        /// <summary>
        /// 入力の際のIMEモードを設定します。
        /// </summary>
        [Category("動作")]
        [Description("入力の際のIMEモードを設定します。")]
        [DefaultValue(ENTRYMODE.IMEOFF_ALPHA)]
        public ENTRYMODE EntryMode
        {
            get { return m_mode; }
            set { m_mode = value; }
        }

        private bool m_bCheck;
        /// <summary>
        /// 半角カナ入力のとき、濁点・半濁点の入力の整合性をチェックするかどうかを示します。（例: ｱ゛, ﾀ゜の禁止など）
        /// </summary>
        [Category("動作")]
        [Description("半角カナ入力のとき、濁点・半濁点の入力の整合性をチェックするかどうかを示します。（例: ｱ゛, ﾀ゜の禁止など）")]
        [DefaultValue(false)]
        public bool CharCheck
        {
            set { m_bCheck = value; }
            get { return m_bCheck; }
        }

        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public KanaTextBox()
        {
            InitializeComponent();
            m_bCheck = false;
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        override protected void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        override protected void OnGotFocus(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnGotFocus:" + e.ToString());

            base.OnGotFocus(e);
            setImeMode();
            SelectionStart = this.TextLength;
            if (m_IsAllSelect) { this.SelectAll(); }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.setImeMode();
            this.RefreshIME();
        }

        private void setImeMode()
        {
            switch (m_mode)
            {
                case ENTRYMODE.IMEON_HANKAKU_KANA:
                    this.ImeMode = ImeMode.KatakanaHalf;
                    KanaLock = true;
                    break;
                case ENTRYMODE.IMEON_ROMAN_HANKAKU_KANA:
                    this.ImeMode = ImeMode.KatakanaHalf;
                    KanaLock = false;
                    break;
                case ENTRYMODE.IMEON_ROMAN_ZENKAKU_HIRAGANA:
                    this.ImeMode = ImeMode.Hiragana;
                    KanaLock = false;
                    break;
                case ENTRYMODE.IMEON_ZENKAKU_HIRAGANA:
                    this.ImeMode = ImeMode.Hiragana;
                    KanaLock = true;
                    break;
                case ENTRYMODE.IMEOFF_ALPHA:
                case ENTRYMODE.IMEOFF_KANA:
                    this.ImeMode = ImeMode.Disable;
                    break;
                case ENTRYMODE.IMEON:
                    this.ImeMode = ImeMode.On;
                    break;
                default:
                    break;
            }
        }

        override protected void OnKeyPress(KeyPressEventArgs e)
        {

            int iCur = SelectionStart;

            if (m_mode == ENTRYMODE.IMEOFF_KANA)
            {
                switch (e.KeyChar)
                {
                    case 'ﾞ':
                    case 'ﾟ':
                        if ((true == m_bCheck)
                            && (false == isRightChar(e.KeyChar, Text.Length - 1)))
                        {
                            Win32.iBicsBeep();
                            e.Handled = true;
                        }
                        break;
                    case 'ｧ':
                    case 'ｨ':
                    case 'ｩ':
                    case 'ｪ':
                    case 'ｫ':
                    case 'ｬ':
                    case 'ｯ':
                        Win32.iBicsBeep();
                        e.Handled = true;
                        break;
                    case 'ｭ':
                        Text = Text.Insert(iCur, "(");
                        e.Handled = true;
                        SelectionStart = iCur + 1;
                        break;
                    case 'ｮ':
                        Text = Text.Insert(iCur, ")");
                        e.Handled = true;
                        SelectionStart = iCur + 1;
                        break;
                    default:
                        break;
                }
            }
            base.OnKeyPress(e);
        }

        protected bool isRightChar(char e, int iPos)
        {
            bool bRtn = false;
            char theTarget;

            if (iPos > -1)
            {

                theTarget = Text[iPos];
                if (e == 'ﾞ')
                {
                    switch (theTarget)
                    {
                        case 'ｳ':
                        case 'ｶ':
                        case 'ｷ':
                        case 'ｸ':
                        case 'ｹ':
                        case 'ｺ':
                        case 'ｻ':
                        case 'ｼ':
                        case 'ｽ':
                        case 'ｾ':
                        case 'ｿ':
                        case 'ﾀ':
                        case 'ﾁ':
                        case 'ﾂ':
                        case 'ﾃ':
                        case 'ﾄ':
                        case 'ﾊ':
                        case 'ﾋ':
                        case 'ﾌ':
                        case 'ﾍ':
                        case 'ﾎ':
                            bRtn = true;
                            break;
                        default:
                            break;
                    }
                }
                else if (e == 'ﾟ')
                {
                    switch (theTarget)
                    {
                        case 'ﾊ':
                        case 'ﾋ':
                        case 'ﾌ':
                        case 'ﾍ':
                        case 'ﾎ':
                            bRtn = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return bRtn;
        }

        internal bool isNotHogeChar(char e)
        {
            bool bRtn = true;

            /*0xff71 0xff9d 0xff9e 0x28 0x29 0x2d 0x30 0x39*/
            /* 0xff67*/

            if (((e < 0xff71) || (e > 0xff9f))			/*カナ大文字*/
                && (e != 0xff66)							/* ｦ */
                && (e != 0x28)								/* ( */
                && (e != 0x29)								/* ) */
                && (e != 0x2c)								/* , */
                && (e != 0x2d)								/* - */
                && (e != 0x2e)								/* . */
                && (e != 0x2f)								/* / */
                && (e != 0x2a)								/* * */
                && ((e < 0x30 || e > 0x39))				/*数字*/
                && ((e < 0x41 || e > 0x5a))				/*英大文字*/
                )
            {

                bRtn = false;
            }
            return bRtn;
        }

        internal int checkTheHogeChar()
        {

            int iRtn = -1;
            bool bRtn;
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == 'ﾟ' || Text[i] == 'ﾞ')
                {
                    bRtn = isRightChar(Text[i], i - 1);
                    if (!bRtn)
                    {
                        iRtn = i;
                        break;
                    }
                }
                else if (Text[i] == ' ')
                {
                    if (i == 0 || i == (Text.Length - 1))
                    {
                        iRtn = i;
                        break;
                    }
                    //スペース連続をＯＫ
                    //					else if ( Text[i-1] == ' ' ){
                    //						iRtn = i;
                    //						break;
                    //					}
                }
                else
                {
                    bRtn = isNotHogeChar(Text[i]);
                    if (!bRtn)
                    {
                        iRtn = i;
                        break;
                    }
                }
            }
            return iRtn;
        }

        internal int checkJISX0208()
        {

            int iRtn = -1;
            uint iWk1;
            uint iWk2;
            uint iWk3;

            Encoding encoding = Encoding.GetEncoding(932);
            byte[] myByte = encoding.GetBytes(Text);

            for (int i = 0; i < myByte.Length; i = i + 2)
            {
                iWk1 = myByte[i];
                if (i + 1 < myByte.Length)
                {
                    iWk2 = myByte[i + 1];
                    iWk3 = iWk1 << 8;
                    iWk3 |= iWk2;

                    if (((iWk3 < 0x8140) || (iWk3 > 0x84be))
                        && ((iWk3 < 0x889f) || (iWk3 > 0x9872))
                        && ((iWk3 < 0x989f) || (iWk3 > 0x9ffc))
                        && ((iWk3 < 0xe040) || (iWk3 > 0xeaa4))
                        )
                    {
                        iRtn = i / 2;
                        break;
                    }
                    else if (iWk3 == 0x0814a || iWk3 == 0x0814b)
                    {
                        iRtn = i / 2;
                        break;
                    }
                    else if (iWk3 == 0x8140)
                    {
                        if (i == 0 || i + 2 == myByte.Length)
                        {
                            iRtn = i / 2;
                            break;
                        }
                    }
                }
                else
                {
                    iRtn = myByte.Length / 2;
                }
            }
            return iRtn;
        }

        public void RefreshIME()
        {
            switch (m_mode)
            {
                case ENTRYMODE.IMEON_HANKAKU_KANA:
                case ENTRYMODE.IMEON_ROMAN_HANKAKU_KANA:
                case ENTRYMODE.IMEON_ZENKAKU_HIRAGANA:
                case ENTRYMODE.IMEON_ROMAN_ZENKAKU_HIRAGANA:
                    preSetIme();
                    break;
                case ENTRYMODE.IMEON:
                    break;
                default:
                    KanaModeOFF();
                    break;
            }
        }

        public void preSetIme()
        {
            if (myTimer == null)
            {
                myTimer = new Timer();
            }
            else
            {
                myTimer.Stop();
                myTimer = new Timer();
            }

            myTimer.Tick += new System.EventHandler(SetIme);

            myTimer.Interval = 50;
            myTimer.Start();
        }

        private void SetIme(object sender, EventArgs e)
        {
            byte[] keystate = new byte[256];

            myTimer.Stop();

            if (!Imm.GetKeyboardState(keystate))
            {
                MessageBox.Show("エラーです");
                return;
            }

            if (keystate[VK_KANA] == 0x1)
            {
                if (!KanaLock)
                    ToggleKanaLock();
            }
            else
            {
                if (KanaLock)
                    ToggleKanaLock();
            }

            if (!Imm.GetKeyboardState(keystate))
            {
                MessageBox.Show("エラーです");
                return;
            }

            if (keystate[VK_KANA] == 0x1)
            {
                if (!KanaLock)
                {
                    System.Diagnostics.Debug.Write("カナロック失敗");
                }
            }
            else
            {
                if (KanaLock)
                {
                    System.Diagnostics.Debug.Write("カナロック失敗");
                }
            }
        }

        private void ToggleKanaLock()
        {
            INPUT input = new INPUT();

            input.Type = INPUT_KEYBOARD;
            input.ki.wScan = 0;
            input.ki.time = 0;
            input.ki.dwExtraInfo = Imm.GetMessageExtraInfo();
            input.ki.wVk = (short)Keys.KanaMode;
            input.ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN;
            Imm.SendInput(1, ref input, Marshal.SizeOf(input));
            System.Threading.Thread.Sleep(50);
            Application.DoEvents();

            input.Type = INPUT_KEYBOARD;
            input.ki.wScan = 0;
            input.ki.time = 0;
            input.ki.dwExtraInfo = Imm.GetMessageExtraInfo();
            input.ki.wVk = (short)Keys.KanaMode;
            input.ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
            Imm.SendInput(1, ref input, Marshal.SizeOf(input));
            System.Threading.Thread.Sleep(50);
            Application.DoEvents();
        }

        private void KanaModeOFF()
        {
            if (myTimer == null)
                myTimer = new Timer();
            else
            {
                myTimer.Stop();
                myTimer = new Timer();
            }

            myTimer.Tick += new EventHandler(SetKanaOFF);

            myTimer.Interval = 50;
            myTimer.Start();
        }

        private void SetKanaOFF(object sender, EventArgs e)
        {
            byte[] keystate = new byte[256];

            myTimer.Stop();

            if (!Imm.GetKeyboardState(keystate))
            {
                System.Diagnostics.Debug.Write("エラーです");
                return;
            }

            if (keystate[VK_KANA] == 0x1)
                ToggleKanaLock();

            if (!Imm.GetKeyboardState(keystate))
            {
                System.Diagnostics.Debug.Write("エラーです");
                return;
            }

            if (keystate[VK_KANA] == 0x1)
                System.Diagnostics.Debug.Write("カナロック失敗");
        }

    }
}
