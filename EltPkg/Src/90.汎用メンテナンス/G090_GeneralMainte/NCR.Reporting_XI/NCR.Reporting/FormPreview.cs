using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NCR.Reporting
{
    /// <summary>
    /// クリスタルレポートプレビュー表示用のフォーム
    /// </summary>
    public partial class FormPreview : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormPreview()
        {
            InitializeComponent();
        }

        public int DefaultZoom
        {
            get { return 86; }
        }

        private CrystalRpt _CrystalRpt = null;
        private string _PrinterName = "";

        /// <summary>
        /// プレビュー表示
        /// </summary>
        /// <param name="fileName">ファイル名(ﾌﾙﾊﾟｽ)</param>
        public void Preview(string fileName)
        {
            Preview(fileName, "");
        }

        /// <summary>
        /// プレビュー表示
        /// </summary>
        /// <param name="fileName">ファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="printerName">プリンター名</param>
        public void Preview(string fileName, string printerName)
        {
            switch (System.IO.Path.GetExtension(fileName))
            {
                case ".rpt":
                    CrystalRpt rpt = new CrystalRpt();
                    rpt.Initialize(fileName);
                    this.crystalReportViewer1.ReportSource = rpt.theReportDocument;
                    _CrystalRpt = rpt;
                    _PrinterName = printerName;
                    break;
            }
            this.crystalReportViewer1.Zoom(DefaultZoom);
            this.ShowDialog();
        }

                /// <summary>
        /// プレビュー表示
        /// </summary>
        /// <param name="report">レポートクラス</param>
        public void Preview(IAPReport report)
        {
            Preview(report, "");
        }

        /// <summary>
        /// プレビュー表示
        /// </summary>
        /// <param name="report">レポートクラス</param>
        /// <param name="printerName">プリンター名</param>
        public void Preview(IAPReport report, string printerName)
        {
            switch (report.GetType().FullName.ToString())
            {
                case "NCR.Reporting.CrystalRpt":
                    this.crystalReportViewer1.ReportSource = ((CrystalRpt)report).theReportDocument;
                    _CrystalRpt = (CrystalRpt)report;
                    _PrinterName = printerName;
                    break;
            }
            this.crystalReportViewer1.Zoom(DefaultZoom);
            this.ShowDialog();
        }

        /// <summary>
        /// 印刷ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            FormPrintDialog pd = new FormPrintDialog();
            string printerName = _PrinterName;
            int lastPageNo = _CrystalRpt.GetTotalPageCount();
            int currentPageNo = this.crystalReportViewer1.GetCurrentPageNumber();
            PrintParameter pr = new PrintParameter();

            pd.SetPrintInfo(printerName, lastPageNo);
            DialogResult dr = pd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                pr.PrinterName = pd.PrinterName;

                switch(pd.PrintRange)
                {
                    case FormPrintDialog.PrintRangeType.ALL:
                        _CrystalRpt.PrintOut(pr.PrinterName);
                        break;
                    case FormPrintDialog.PrintRangeType.Range:
                        pr.StartPageN = pd.PageFrom;
                        pr.EndPageN = pd.PageTo;
                        _CrystalRpt.PrintOut(pr);
                        break;
                    case FormPrintDialog.PrintRangeType.Current:
                        pr.StartPageN = currentPageNo;
                        pr.EndPageN = currentPageNo;
                        _CrystalRpt.PrintOut(pr);
                        break;
                }
            }
        }

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

        private void FormPreview_Shown(object sender, EventArgs e)
        {
            btnPrint.Focus();
        }

    }
}
