using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IIPCommonClass
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

    }
}
