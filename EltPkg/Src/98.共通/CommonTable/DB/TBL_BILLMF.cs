using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace CommonTable.DB
{
    /// <summary>
    /// TBL_BILLMF
    /// </summary>
    public class TBL_BILLMF
    {
        public TBL_BILLMF()
        {
        }

        public TBL_BILLMF(int bill_code)
        {
            m_BILL_CODE = bill_code;
      
        }

		public TBL_BILLMF(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

		public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
		public const string TABLE_PHYSICAL_NAME = "BILLMF";

        public const string BILL_CODE = "BILL_CODE";
        public const string KBN_NAME = "KBN_NAME";
		public const string STOCK_NAME = "STOCK_NAME";
		public const string STOCK_RYAKU_NAME = "STOCK_RYAKU_NAME";
        public const string DESCRIPTION = "DESCRIPTION";

        #endregion

        #region メンバ

        private int m_BILL_CODE = 0;
        public string m_KBN_NAME = "";
        public string m_STOCK_NAME = "";
        public string m_STOCK_RYAKU_NAME = "";
        public string m_DESCRIPTION = "";
        #endregion

        #region プロパティ

        public int _BILL_CODE
        {
            get { return m_BILL_CODE; }
        }

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_BILL_CODE = DBConvert.ToIntNull(dr[TBL_BILLMF.BILL_CODE]);
            m_KBN_NAME = DBConvert.ToStringNull(dr[TBL_BILLMF.KBN_NAME]);
            m_STOCK_NAME = DBConvert.ToStringNull(dr[TBL_BILLMF.STOCK_NAME]);
            m_STOCK_RYAKU_NAME = DBConvert.ToStringNull(dr[TBL_BILLMF.STOCK_RYAKU_NAME]);
            m_DESCRIPTION = DBConvert.ToStringNull(dr[TBL_BILLMF.DESCRIPTION]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSql = "SELECT * FROM " + TBL_BILLMF.TABLE_NAME +
                " ORDER BY " +
                TBL_BILLMF.BILL_CODE;
                

            return strSql;
        }


        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// <returns></returns>
        public static string GetSelectQuery(int bill_code)
        {
            string strSql = "SELECT * FROM " + TBL_BILLMF.TABLE_NAME +
                " WHERE " +
                TBL_BILLMF.BILL_CODE + "=" + bill_code;
            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_BILLMF.TABLE_NAME + " (" +
                TBL_BILLMF.BILL_CODE + "," +
                TBL_BILLMF.KBN_NAME + "," +
                TBL_BILLMF.STOCK_NAME + "," +
                TBL_BILLMF.STOCK_RYAKU_NAME + "," +
                TBL_BILLMF.DESCRIPTION + ") VALUES (" +
                m_BILL_CODE+ "," +
                m_KBN_NAME + "," +
                m_STOCK_NAME + "," +
                m_STOCK_RYAKU_NAME + "," +
                m_DESCRIPTION + ")";

			return strSql;
		}

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BILLMF.TABLE_NAME + " SET " +
                TBL_BILLMF.KBN_NAME + "=" + m_KBN_NAME + ", " +
                TBL_BILLMF.KBN_NAME + "='" + m_KBN_NAME + "' " +
                TBL_BILLMF.STOCK_RYAKU_NAME + "='" + m_STOCK_RYAKU_NAME + "' " +
                TBL_BILLMF.DESCRIPTION + "='" + m_DESCRIPTION + "' " +
                " WHERE " +
                TBL_BILLMF.BILL_CODE + "=" + m_BILL_CODE;

            return strSql;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>

        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_BILLMF.TABLE_NAME +
               " WHERE " +
           TBL_BILLMF.BILL_CODE + "=" + m_BILL_CODE;
            return strSql;
        }
        #endregion
    }
}
