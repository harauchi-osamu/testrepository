using System.Windows.Forms;

namespace EntryCommon
{
    public partial class ReportViewer : Form
    {
        public ReportViewer(CrystalDecisions.CrystalReports.Engine.ReportClass crystalReport1)
        {
            InitializeComponent();

            this.crystalReportViewer.ReportSource = crystalReport1;
            this.crystalReportViewer.Zoom(2);
        }

    }
}
