using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using Common;
using CommonClass;

namespace EntryClass
{
    public class PrintForm
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private const int SRCCOPY = 0xCC0020;

        //フォームのイメージを保存する変数
        public Bitmap bm = null;

        /// <summary>
        /// フォームのイメージを印刷する。
        /// </summary>
        /// <param name="frm"></param>
        public void PrtForm(Form frm)
        {
            //フォームのイメージを取得する。
            bm = CaptureControl(frm);
            //フォームのイメージを印刷する。
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.DefaultPageSettings.Landscape = true;
            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes[0];
            try
            {
                PrinterSettings ps = new PrinterSettings();
                pd.PrinterSettings.PrinterName = ps.PrinterName;

                RegistryAccess ra = new RegistryAccess();
                pd.PrinterSettings.PrinterName = ra.getLMKeyValue("Printer", "LaserPrinterName");
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.Message, 3);
            }

            //印紙のサイズをA4に変更
            for (int i = 0; i < pd.PrinterSettings.PaperSizes.Count; i++)
            {
                if (pd.PrinterSettings.PaperSizes[i].Kind == PaperKind.A4)
                {
                    pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes[i];
                    break;
                }
            }

            pd.Print();

            bm.Dispose();
        }

        /// <summary>
        /// コントロールのイメージを取得する。
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public Bitmap CaptureControl(Control ctrl)
        {
            Graphics g = ctrl.CreateGraphics();
            Bitmap img = new Bitmap(ctrl.ClientRectangle.Width, ctrl.ClientRectangle.Height, g);
            Graphics memg = Graphics.FromImage(img);
            IntPtr dc1 = g.GetHdc();
            IntPtr dc2 = memg.GetHdc();
            BitBlt(dc2, 0, 0, img.Width, img.Height, dc1, 0, 0, SRCCOPY);
            g.ReleaseHdc(dc1);
            memg.ReleaseHdc(dc2);
            g.Dispose();
            return img;
        }

        //pdのPrintPageイベントハンドラ
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            float width = (float)bm.Size.Width;
            float height = (float)bm.Size.Height;
            float wzoom = 1f;
            float hzoom = 1f;
            float zoom = 1f;

            if (width != e.PageSettings.PrintableArea.Height)
            {
                wzoom = e.PageSettings.PrintableArea.Height / width;
            }

            if (height != e.PageSettings.PrintableArea.Width)
            {
                hzoom = e.PageSettings.PrintableArea.Width / height;
            }

            zoom = Math.Min(wzoom, hzoom);

            Rectangle rect = new Rectangle(0, 0, (int)(width * zoom), (int)(height * zoom));
            e.Graphics.DrawImage(bm, rect);
        }
    }
}
