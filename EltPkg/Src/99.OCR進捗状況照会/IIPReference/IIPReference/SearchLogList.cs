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
    internal partial class SearchLogList : UnclosableForm
    {

        internal SearchLogList()
        {
            InitializeComponent();
        }

        #region プロパティ

        #endregion

        #region イベント

        public bool InitializeForm()
        {
            return this.SetLstLogList();
        }

        private void SearchLogList_Load(object sender, EventArgs e)
        {
            ProcessLog.getInstance().writeLog(this.Text + "画面表示");

        }


        private void SearchLogList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnBack_Click(sender, e);
                    break;
                case Keys.F12:
                    btnShow_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void lstLogList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnShow_Click(sender, e);
                    break;
                case Keys.F1:
                    btnBack_Click(sender, e);
                    break;
                case Keys.F12:
                    btnShow_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void lstLogText_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnBack_Click(sender, e);
                    break;
                default:
                    break;
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchLogList_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProcessLog.getInstance().writeLog(this.Text + "画面終了");
        }

        private void lstLogList_DoubleClick(object sender, EventArgs e)
        {
            btnShow_Click(sender, e);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLogList.SelectedItems.Count > 0)
                {
                    //ログファイルオープン
                    this.LoadLogFile();
                    //Process.Start("notepad", this.lstLogList.FocusedItem.SubItems[1].Text + "\\" + this.lstLogList.FocusedItem.SubItems[0].Text);

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void lstLogList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.Bounds);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            e.Graphics.DrawString(e.Header.Text, lstLogList.Font, Brushes.Black, e.Bounds, sf);
        }

        private void lstLogList_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lstLogList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        #endregion

        #region ListView表示

        private bool SetLstLogList()
        {
            //string _opedate = DateTime.Today.ToString("yyyyMMdd");
            foreach (string _ErrLogFolderPath in Properties.Settings.Default.ErrLogFolderPath)
            {
            
                FileInfo[] files = GetLogFileList(_ErrLogFolderPath);

                if (files.Length == 0)
                {
                    MessageBox.Show("該当のデータがありません。", "ログファイルなし", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                files = files.OrderByDescending(fi => fi.LastWriteTime).ToArray();

                this.lstLogList.OwnerDraw = true;

                foreach (FileInfo fl in files)
                {
                    if ((DateTime.Now - fl.LastWriteTime).TotalDays <= Int32.Parse(AplInfo.ErrLogDays))
                    {
                        lstLogList.Items.Add(new ListViewItem(new string[3] { fl.Name.ToString(), fl.Directory.ToString(), fl.LastWriteTime.ToString() }));
                    }
                }
            }
            if (this.lstLogList.Items.Count > 0)
            {
                this.lstLogList.Items[0].Selected = true;
            }
            return true;
        }

        #endregion

        #region ファイル一覧取得

        private FileInfo[] GetLogFileList(string _ErrLogFolderPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(_ErrLogFolderPath);
                FileInfo[] files = di.GetFiles("*ERR.log", SearchOption.AllDirectories);
                return files;
            }
            catch (Exception ex)
            {
                IniFileAccess.ShowMsgBox("E0401", ex.Message);
                // 業務ログに一時的に吐く
                ProcessLog.getInstance().writeLog(ex.ToString());
                return new FileInfo[0];
            }
        }

        #endregion

        #region 表示

        /// <summary>
        /// ログリスト表示
        /// </summary>
        /// <returns></returns>
        public void LoadLogFile()
        {
            string LOG_FILE = this.lstLogList.FocusedItem.SubItems[1].Text + "\\" + this.lstLogList.FocusedItem.SubItems[0].Text;

            //ログファイル存在チェック
            if (!File.Exists(LOG_FILE))
            {
                MessageBox.Show("ログファイルが存在しません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProcessLog.getInstance().writeLog("「F5：確認」選択　確認処理未実行");
                return;
            }

            //リストクリア
            this.lstLogText.Clear();

            //リスト表示
            using (StreamReader sr = new StreamReader(LOG_FILE, Encoding.GetEncoding("Shift_JIS")))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    this.lstLogText.Items.Add(line);
                    //Application.DoEvents();
                }
            }

            //リストカラムセット
            SetlistLogViewColumn();

            this.lstLogText.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lstLogText.Refresh();
            ProcessLog.getInstance().writeLog("ログファイル表示完了");
        }

        /// <summary>
        /// ログリストカラム設定
        /// </summary>
        private void SetlistLogViewColumn()
        {
            ColumnHeader column = new ColumnHeader();
            column.Text = "";
            column.Width = this.lstLogText.Width - 25;
            column.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            lstLogText.Columns.Add(column);
        }


        #endregion
    }
}
