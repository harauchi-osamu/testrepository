using System;
using System.Collections.Generic;
using System.Text;

namespace NCR.Reporting
{
    public interface IAPReport
    {
        string Type
        {
            get;
        }

        ///// <summary>
        ///// レポートＩＤ
        ///// </summary>
        //string ReportID
        //{
        //    get;
        //}

        /// <summary>
        /// ファイル名
        /// </summary>
        string FileName
        {
            get;
        }

        /// <summary>
        /// 出力ファイル名
        /// </summary>
        string OutFileName
        {
            get;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        bool Initialize(string fileName);

        /// <summary>
        /// 印刷
        /// </summary>
        /// <returns></returns>
        bool PrintOut(PrintParameter printParameter);

        ///// <summary>
        ///// プレビュー
        ///// </summary>
        ///// <returns></returns>
        //bool Preview();

        /// <summary>
        /// 総ページ数を取得します。
        /// </summary>
        /// <returns></returns>
        int GetTotalPageCount();

        /// <summary>
        /// PDFとして出力します。
        /// </summary>
        /// <param name="pdfFileName">PDFファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <returns></returns>
        bool WritePDF(string pdfFileName);
    }
}
