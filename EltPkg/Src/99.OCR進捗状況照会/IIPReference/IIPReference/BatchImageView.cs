using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Configuration;
using IIPCommonClass;
using IIPCommonClass.Log;

namespace IIPReference
{
    internal partial class BatchImageView : UnclosableForm
    {

        internal BatchImageView()
        {
            InitializeComponent();
        }

        #region プロパティ

        #endregion

        #region イベント
        public bool InitializeForm(string _imgpath)
        {
            pbScanImage.Image = System.Drawing.Image.FromFile(_imgpath);
            return true;
        }
        
        private void BatchImageView_Load(object sender, EventArgs e)
        {
            ProcessLog.getInstance().writeLog(this.Text + "画面表示");

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BatchImageView_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProcessLog.getInstance().writeLog(this.Text + "画面終了");
        }

        #endregion

    }
}
