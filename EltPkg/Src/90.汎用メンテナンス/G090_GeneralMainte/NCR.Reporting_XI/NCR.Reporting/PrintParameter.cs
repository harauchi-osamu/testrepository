using System;
using System.Collections.Generic;
using System.Text;

namespace NCR.Reporting
{
    /// <summary>
    /// 印刷パラメーター
    /// </summary>
    public class PrintParameter
    {
        #region メンバ変数

        /// <summary>
        /// プリンター名
        /// </summary>
        public string m_PrinterName = "";

        /// <summary>
        /// ドライバー名
        /// </summary>
        public string m_DriverName = "";

        /// <summary>
        /// ポート
        /// </summary>
        public string m_Port = "";

        /// <summary>
        /// 印刷要求
        /// </summary>
        public bool m_PrintReq = true;

        /// <summary>
        /// 印刷する部数
        /// </summary>
        public int m_NumberOfCopies = 1;

        /// <summary>
        /// ページを部数単位で印刷するかどうか
        /// </summary>
        public bool m_Collated = false;

        /// <summary>
        /// 印刷を開始するページ
        /// </summary>
        public int m_StartPageN = 0;

        /// <summary>
        /// 印刷を終了するページ
        /// </summary>
        public int m_EndPageN = 0;

        #endregion

        #region プロパティ

        /// <summary>
        /// プリンター名
        /// </summary>
        public string PrinterName
        {
            get { return m_PrinterName; }
            set { m_PrinterName = value; }
        }

        /// <summary>
        /// ドライバー名
        /// </summary>
        public string DriverName
        {
            get { return m_DriverName; }
            set { m_DriverName = value; }
        }

        /// <summary>
        /// ポート
        /// </summary>
        public string Port
        {
            get { return m_Port; }
            set { m_Port = value; }
        }

        /// <summary>
        /// 印刷要求
        /// </summary>
        public bool PrintReq
        {
            get { return m_PrintReq; }
            set { m_PrintReq = value; }
        }

        /// <summary>
        /// 印刷する部数
        /// </summary>
        public int NumberOfCopies
        {
            get { return m_NumberOfCopies; }
            set { m_NumberOfCopies = value; }
        }

        /// <summary>
        /// ページを部数単位で印刷するかどうか
        /// </summary>
        public bool Collated
        {
            get { return m_Collated; }
            set { m_Collated = value; }
        }

        /// <summary>
        /// 印刷を開始するページ
        /// </summary>
        public int StartPageN
        {
            get { return m_StartPageN; }
            set { m_StartPageN = value; }
        }

        /// <summary>
        /// 印刷を終了するページ
        /// </summary>
        public int EndPageN
        {
            get { return m_EndPageN; }
            set { m_EndPageN = value; }
        }

        #endregion

        #region ActiveX版クリスタルレポート用メソッド

        /// <summary>
        /// 印刷する部数を取得する
        /// </summary>
        /// <returns>部数</returns>
        public object GetAxNumberOfCopies()
        {
            if (m_NumberOfCopies <= 1)
            {
                return null;
            }
            else
            {
                return m_NumberOfCopies;
            }
        }
        /// <summary>
        /// ページを部数単位で印刷するかどうかを示す値を取得する
        /// </summary>
        /// <returns>
        /// true  : 部数単位で印刷する
        /// false : 部数単位で印刷しない
        /// </returns>
        public object GetAxCollated()
        {
            if (m_Collated)
            {
                return m_Collated;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 印刷する最初のページを取得する
        /// </summary>
        /// <returns>ページ番号</returns>
        public object GetAxStartPageN()
        {
            if (m_StartPageN < 1)
            {
                return null;
            }
            else
            {
                return m_StartPageN;
            }
        }
        /// <summary>
        /// 印刷する最後のページを取得する
        /// </summary>
        /// <returns>ページ番号</returns>
        public object GetAxStopPageN()
        {
            if (m_EndPageN < 1)
            {
                return null;
            }
            else
            {
                return m_EndPageN;
            }
        }

        #endregion
    }
}
