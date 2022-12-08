using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonClass
{
    /// <summary>
    /// 閉じる系の操作をできなくしただけのフォーム
    /// </summary>
    public partial class UnclosableForm : Form
    {
        public UnclosableForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォーム上部の閉じるボタン、システムメニューの閉じる、Alt+F4を禁止
        /// </summary>
        protected override CreateParams CreateParams
        {
            //[System.Security.Permissions.SecurityPermission(
            //        System.Security.Permissions.SecurityAction.LinkDemand,
            //        Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;
            }
        }

        /// <summary>
        /// 入力モードを設定する（カナ）
        /// </summary>
        /// <param name="kTextBox"></param>
        protected void SetEntryModeKana(KanaTextBox kTextBox)
        {
            kTextBox.EntryMode = (AplInfo.OP_ROMAN) ? ENTRYMODE.IMEON_ROMAN_HANKAKU_KANA : ENTRYMODE.IMEON_HANKAKU_KANA;
        }

        /// <summary>
        /// 入力モードを設定する（漢字）
        /// </summary>
        /// <param name="kTextBox"></param>
        protected void SetEntryModeKanji(KanaTextBox kTextBox)
        {
            kTextBox.EntryMode = (AplInfo.OP_ROMAN) ? ENTRYMODE.IMEON_ROMAN_ZENKAKU_HIRAGANA : ENTRYMODE.IMEON_ZENKAKU_HIRAGANA;
        }

    }
}
