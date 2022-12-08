using System;
using System.Collections.Generic;
using System.Text;

namespace NCR.Reporting
{
    /// <summary>
    /// ＷＭＩプリントジョブ情報を格納するクラス
    /// </summary>
    public class Win32_PrintJob
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mo">ＷＭＩインスンタンス</param>
        public Win32_PrintJob(System.Management.ManagementObject mo)
        {
            m_MO = mo;
        }

        private System.Management.ManagementObject m_MO;

        #region プロパティ
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return getMOValue("Name");
            }
        }
        /// <summary>
        /// JobId
        /// </summary>
        public int JobId
        {
            get
            {
                return Convert.ToInt32(getMOValue("JobId"));
            }
        }

        #endregion



        #region プライベートメソッド

        /// <summary>
        /// ManagementObjectよりデータ取得
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string getMOValue(string propertyName)
        {
            try
            {
                return Convert.ToString(m_MO[propertyName]);
            }
            catch
            {
                return "";
            }
        }

        #endregion

    }
}
