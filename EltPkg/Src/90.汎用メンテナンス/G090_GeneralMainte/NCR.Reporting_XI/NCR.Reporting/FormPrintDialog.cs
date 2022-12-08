using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using Common;

namespace NCR.Reporting
{
    public partial class FormPrintDialog : Form
    {
        public FormPrintDialog()
        {
            InitializeComponent();

            cbPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private System.Collections.Hashtable _Printers = null;
        private System.Collections.SortedList _PrinterNames = null;
        private string _PrinterName = "";

        public enum PrintRangeType
        {
            ALL = 0,
            Range = 1,
            Current = 2
        }

        #region プロパティ

        /// <summary>
        /// プリンター名
        /// </summary>
        public string PrinterName
        {
            get { return _PrinterName; }
        }

        /// <summary>
        /// 印刷範囲のタイプ
        /// </summary>
        public PrintRangeType PrintRange
        {
            get
            {
                if (rbCurrentPage.Checked)
                {
                    return PrintRangeType.Current;
                }
                else if (rbPage.Checked)
                {
                    return PrintRangeType.Range;
                }
                else
                {
                    return PrintRangeType.ALL;
                }
            }
        }

        /// <summary>
        /// 開始ページ
        /// </summary>
        public int PageFrom
        {
            get
            {
                int pageFrom = this.tbPageFrom.getInt();
                if (pageFrom < 1 || pageFrom > 9999)
                {
                    return 1;
                }
                else
                {
                    return pageFrom;
                }
            }
        }

        /// <summary>
        /// 終了ページ
        /// </summary>
        public int PageTo
        {
            get
            {
                int pageTo = this.tbPageTo.getInt();
                if (pageTo < 1 || pageTo > 9999)
                {
                    return 9999;
                }
                else
                {
                    return pageTo;
                }
            }
        }

        #endregion

        public void SetPrintInfo(string printerName)
        {
            SetPrintInfo(printerName, 0);
        }

        public void SetPrintInfo(string printerName, int maxPage)
        {
            //初期表示プリンター名の確定
            if (printerName == "")  // AP_Preview()から呼ばれた場合
            {
                //デフォルトプリンターの取得
                printerName = WMIManager.GetDefaultPrinter();
            }

            //プリンターの取得
            _Printers = this.getLocalPrinters();
            _PrinterNames = new SortedList();

            string printerX = "";
            foreach (DictionaryEntry de in _Printers)
            {
                printerX = ((Win32_Printer)de.Value).Name;

                _PrinterNames.Add(printerX, printerX);
            }

            foreach(DictionaryEntry de in _PrinterNames)
            {
                printerX = (string)de.Value;

                switch (printerX)
                {
                    case "FAX":
                    case "Fax":
                        break;
                    case "Microsoft XPS Document Writer":
                        break;
                    default:
                        cbPrinterName.Items.Add(printerX);
                        break;
                }
            }

            for (int i = 0; i < cbPrinterName.Items.Count; i++)
            {
                if(printerName == cbPrinterName.Items[i].ToString())
                {
                    cbPrinterName.SelectedIndex = i;
                    //プリンター情報の表示
                    dispPrinterInfo(printerName);
                }
            }
            
            //印刷範囲の設定
            if (maxPage < 1)
            {
                rbPage.Enabled = false;
                tbPageFrom.Text = "";
                tbPageTo.Text = "";
                tbPageFrom.Enabled = false;
                tbPageTo.Enabled = false;
                rbCurrentPage.Enabled = false;
            }
            else
            {
                tbPageFrom.setText(1);
                tbPageTo.setText(maxPage);
            }
            rbALL.Checked = true;
        }

        /// <summary>
        /// プリンター情報の表示
        /// </summary>
        /// <param name="printerName"></param>
        private void dispPrinterInfo(string printerName)
        {
            if (_Printers.Contains(printerName))
            {
                this.lblStatus.Text = ((Win32_Printer)_Printers[printerName]).GetDispDetectedErrorState();
                this.lblLocation.Text = "";
                this.lblComment.Text = "";
            }
        }

        /// <summary>
        /// ローカルプリンター情報を取得する
        /// </summary>
        /// <returns></returns>
        private System.Collections.Hashtable getLocalPrinters()
        {
            try
            {
                // Win32_Printerの取得
                return WMIManager.GetWin32_Printers();
            }
            catch (Exception ex)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ローカルプリンター情報取得エラー: " + ex.ToString(), 3);
                return new System.Collections.Hashtable();
            }
        }

        #region イベント

        private void FormPrintDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((this.ActiveControl), true, true, true, false);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string printer =  this.cbPrinterName.Text;
            if(printer == "")
            {
                System.Windows.Forms.MessageBox.Show("プリンターが選択されていません。");
                return;
            }

            _PrinterName = printer;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _PrinterName = "";

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbPrinterName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //プリンター名の取得
            string printerName = cbPrinterName.SelectedItem.ToString();

            //プリンター情報の表示
            dispPrinterInfo(printerName);
        }

        private void FormPrintDialog_Shown(object sender, EventArgs e)
        {
            btnOK.Focus();
        }

        private void tbPageFrom_TextChanged(object sender, EventArgs e)
        {
            rbPage.Checked = true;
        }

        private void tbPageTo_TextChanged(object sender, EventArgs e)
        {
            rbPage.Checked = true;
        }

        #endregion

        #region Alt+F4でのクローズを防止

        protected override void WndProc(ref Message m)
        {
            if (!this.ControlBox)
            {
                const int WM_SYSCOMMAND = 0x0112;
                const int SC_CLOSE = 0xF060;

                if ((m.Msg == WM_SYSCOMMAND) && (m.WParam.ToInt32() == SC_CLOSE))
                {
                    return; // Windows標準の処理は行わない
                }
            }
            base.WndProc(ref m);
        }

        #endregion

    }
}
