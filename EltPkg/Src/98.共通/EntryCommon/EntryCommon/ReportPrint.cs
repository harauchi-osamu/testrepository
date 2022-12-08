using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClass;

namespace EntryCommon
{
    public class ReportPrint
    {
        public string FilePath { get; private set; } = string.Empty;

        /// <summary>
        /// 印刷タイプ
        /// </summary>
        public enum PrintType
        {
            Print = 0,
            Preview = 1,
            PDF = 2,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReportPrint()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filepath"></param>
        public ReportPrint(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// 印刷する
        /// </summary>
        /// <param name="crystalReport"></param>
        /// <param name="Type"></param>
        /// <param name="maisu"></param>
        public void Print(CrystalDecisions.CrystalReports.Engine.ReportClass crystalReport, PrintType Type, int maisu = 1, string PrinterName =  "")
        {
            switch (Type)
            {
                case PrintType.Print:
                    // 印刷
                    if (string.IsNullOrEmpty(PrinterName))
                    {
                        // デフォルトプリンタ名を設定
                        PrinterName = GetDefPrinterName();
                    }
                    crystalReport.PrintOptions.PrinterName = PrinterName;
                    crystalReport.PrintToPrinter(maisu, false, 0, 0);

                    break;
                case PrintType.Preview:
                    // プレビュー
                    using (ReportViewer from = new ReportViewer(crystalReport))
                    {
                        from.ShowDialog();
                    }

                    break;
                case PrintType.PDF:
                    CrystalDecisions.Shared.DiskFileDestinationOptions fileOption;
                    fileOption = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                    fileOption.DiskFileName = FilePath;

                    CrystalDecisions.Shared.ExportOptions option;
                    option = crystalReport.ExportOptions;
                    option.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                    option.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                    option.FormatOptions = new CrystalDecisions.Shared.PdfRtfWordFormatOptions();
                    option.DestinationOptions = fileOption;
                    crystalReport.Export();

                    break;
            }
        }

        /// <summary>
        /// デフォルトプリンタ名を取得
        /// </summary>
        private string GetDefPrinterName()
        {
            //PrintDocumentの作成
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            //プリンタ名の取得
            return pd.PrinterSettings.PrinterName;
        }
    }
}
