using System;
using System.Collections.Generic;
using System.Text;

namespace NCR.Reporting
{
    /// <summary>
    /// ＰＤＦレポート管理クラス
    /// </summary>
    public class PDFRpt : IAPReport
    {
        public PDFRpt()
        {
        }

        protected string m_FileName = "";
        protected string m_OutFileName = "";

        public string Type
        {
            get { return "pdf"; }
        }

        /// <summary>
        /// ファイル名（ﾌﾙﾊﾟｽ）
        /// </summary>
        public string FileName
        {
            get { return this.m_FileName; }
        }

        /// <summary>
        /// 出力ファイル名（ﾌﾙﾊﾟｽ）
        /// </summary>
        public string OutFileName
        {
            get { return m_OutFileName; }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        public bool Initialize(string fileName)
        {
            m_FileName = fileName;
            return true;
        }

        /// <summary>
        /// 印刷
        /// </summary>
        /// <returns></returns>
        public bool PrintOut(PrintParameter printParameter)
        {
            return true;
        }

        /// <summary>
        /// 総ページ数を取得します。
        /// </summary>
        /// <returns></returns>
        public int GetTotalPageCount()
        {
            return 0;
        }

        /// <summary>
        /// PDFとして出力します。
        /// </summary>
        /// <param name="pdfFileName">PDFファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <returns></returns>
        public bool WritePDF(string pdfFileName)
        {
            return true;
        }
    }
}
