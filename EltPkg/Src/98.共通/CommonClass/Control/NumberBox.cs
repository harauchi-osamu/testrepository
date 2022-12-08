using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CommonClass{

    public partial class NumberBox : TextBox
    {

        #region　PermitChars プロパティ

        protected char[] _PermitChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '\t', '\r', '\b', '\x7f', '%'};

        ///* 入力可能な文字を設定する */
        //public char[] PermitChars
        //{
        //    get { return this._PermitChars; }
        //    set { this._PermitChars = value; }
        //}

        #endregion

        public NumberBox()
        {
            InitializeComponent();
        }

        #region　OnKeyDown メソッド (override)

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            //// キーの変換
            //switch (e.KeyCode)
            //{
            //    // ↑をShift+TAB
            //    case Keys.Up:
            //        SendKeys.SendWait("+{TAB}");
            //            break;
            //    // ↓をTAB
            //    case Keys.Down:
            //        SendKeys.Send("{TAB}");
            //        break;
            //    // EnterをTAB
            //    case Keys.Enter:
            //        e.SuppressKeyPress = true;
            //        SendKeys.Send("{TAB}");
            //        break;
            //    // 他はそのまま処理
            //    default:
            //        base.OnKeyDown(e);
            //        //e.Handled = true;
            //        break;
            //}
        }

        #endregion

        #region　WndProc メソッド (override)

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            //const int WM_KEYDOWN = 0x0100;
            const int WM_CHAR = 0x0102;
            const int WM_PASTE = 0x0302;

            switch (m.Msg)
            {
                //case WM_KEYDOWN:
                //    if (m.WParam.ToInt32() >= 38 && m.WParam.ToInt32() <= 41)
                //    {
                //        KeyPressEventArgs eKeyPress = new KeyPressEventArgs((char)(m.WParam.ToInt32()));
                //        //eKeyPress.Handled = true;
                //    }
                //    break;

                case WM_CHAR:
                    // Accept Contorl Key
                    char c = (char)m.WParam.ToInt32();
                    if (char.IsControl(c))
                    {
                        base.WndProc(ref m);
                        return;
                    }

                    if ((_PermitChars != null) && (_PermitChars.Length > 0))
                    {
                        KeyPressEventArgs eKeyPress = new KeyPressEventArgs((char)(m.WParam.ToInt32()));
                        this.OnChar(eKeyPress);

                        if (eKeyPress.Handled)
                        {
                            return;
                        }
                    }
                    break;
                case WM_PASTE:
                    if ((_PermitChars != null) && (_PermitChars.Length > 0))
                    {
                        this.OnPaste(new System.EventArgs());
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region　OnChar メソッド (virtual)

        protected virtual void OnChar(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!HasPermitChars(e.KeyChar, _PermitChars))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region　OnPaste メソッド (virtual)

        protected virtual void OnPaste(System.EventArgs e)
        {
            string stString = Clipboard.GetDataObject().GetData(System.Windows.Forms.DataFormats.Text).ToString();

            if (stString != null)
            {
                this.SelectedText = GetPermitedString(stString, _PermitChars);
            }
        }

        #endregion

        #region　[S] HasPermitChars メソッド

        private static bool HasPermitChars(char chTarget, char[] chPermits)
        {
            foreach (char ch in chPermits)
            {
                if (chTarget == ch)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region　[S] GetPermitedString メソッド

        private static string GetPermitedString(string stTarget, char[] chPermits)
        {
            string stReturn = string.Empty;

            foreach (char chTarget in stTarget)
            {
                if (HasPermitChars(chTarget, chPermits))
                {
                    stReturn += chTarget;
                }
            }

            return stReturn;
        }

        #endregion

        private void NumberBox_Enter(object sender, EventArgs e)
        {
            this.SelectAll();
        }

    }
}
