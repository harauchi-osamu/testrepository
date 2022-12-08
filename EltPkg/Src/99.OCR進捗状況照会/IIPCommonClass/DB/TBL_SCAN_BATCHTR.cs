using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
    /// スキャナ画面入力バッチ情報
	/// </summary>
	public class TBL_SCAN_BATCHTR
    {
        public TBL_SCAN_BATCHTR()
        {
        }
        public TBL_SCAN_BATCHTR(int _GYM_ID
            , int _OPERATION_DATE
            , string _SCANNER_ID
            , string _BATCH_NAME
            , int _BATCH_STATUS
            , int _ITEM_ID
            , string _ITEM_NAME
            , string _ITEM_VALUE)
        {
            m_GYM_ID = _GYM_ID;
            m_OPERATION_DATE = _OPERATION_DATE;
            m_SCANNER_ID = _SCANNER_ID;
            m_BATCH_NAME = _BATCH_NAME;
            m_BATCH_STATUS = _BATCH_STATUS;
            m_ITEM_ID = _ITEM_ID;
            m_ITEM_NAME = _ITEM_NAME;
            m_ITEM_VALUE = _ITEM_VALUE;
        }

		public TBL_SCAN_BATCHTR(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "SCAN_BATCHTR";
        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCANNER_ID = "SCANNER_ID";
        public const string BATCH_NAME = "BATCH_NAME";
		public const string BATCH_STATUS = "BATCH_STATUS";
		public const string ITEM_ID = "ITEM_ID";
		public const string ITEM_NAME = "ITEM_NAME";
		public const string ITEM_VALUE = "ITEM_VALUE";
		#endregion

		#region メンバ

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCANNER_ID = "";
        private string m_BATCH_NAME = "";
        private int m_BATCH_STATUS = 0;
        private int m_ITEM_ID = 0;
        private string m_ITEM_NAME = "";
        private string m_ITEM_VALUE = "";

		#endregion

        #region プロパティ
        
        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_SCAN_BATCHTR.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_SCAN_BATCHTR.OPERATION_DATE]);
            m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_SCAN_BATCHTR.SCANNER_ID]);
            m_BATCH_NAME = DBConvert.ToStringNull(dr[TBL_SCAN_BATCHTR.BATCH_NAME]);
            m_BATCH_STATUS = DBConvert.ToIntNull(dr[TBL_SCAN_BATCHTR.BATCH_STATUS]);
            m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_SCAN_BATCHTR.ITEM_ID]);
            m_ITEM_NAME = DBConvert.ToStringNull(dr[TBL_SCAN_BATCHTR.ITEM_NAME]);
            m_ITEM_VALUE = DBConvert.ToStringNull(dr[TBL_SCAN_BATCHTR.ITEM_VALUE]);

        }

		#endregion

		#region クエリ取得

		/// <summary>
		/// データを全て取得するSELECT文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetSelectAllQuery()
		{
			string strSql = "SELECT * FROM " + TBL_SCAN_BATCHTR.TABLE_NAME +
				" ORDER BY " + TBL_SCAN_BATCHTR.GYM_ID +
                " , " + TBL_SCAN_BATCHTR.OPERATION_DATE +
                " , " + TBL_SCAN_BATCHTR.SCANNER_ID +
                " , " + TBL_SCAN_BATCHTR.BATCH_NAME +
                " , " + TBL_SCAN_BATCHTR.ITEM_ID;

            return strSql;
		}

        /// <summary>
        /// バッチ情報キーにするSELECT文
        /// </summary>
        /// <param name="_GYM_ID"></param>
        /// <param name="_OPERATION_DATE"></param>
        /// <param name="_SCANNER_ID"></param>
        /// <param name="_BATCH_NAME"></param>
        /// <returns></returns>
        public string GetSelectQuery(int _GYM_ID, int _OPERATION_DATE, string _SCANNER_ID, string _BATCH_NAME)
        {
            string strSql = "SELECT * FROM " + TBL_SCAN_BATCHTR.TABLE_NAME +
                " WHERE " + TBL_SCAN_BATCHTR.GYM_ID + "='" + _GYM_ID.ToString() + "'" +
                " , " + TBL_SCAN_BATCHTR.OPERATION_DATE + "='" + _OPERATION_DATE.ToString() + "'" +
                " , " + TBL_SCAN_BATCHTR.SCANNER_ID + "='" + _SCANNER_ID + "'" +
                " , " + TBL_SCAN_BATCHTR.BATCH_NAME + "='" + _BATCH_NAME + "'" +
                " ORDER BY " + TBL_SCAN_BATCHTR.GYM_ID +
                " , " + TBL_SCAN_BATCHTR.OPERATION_DATE +
                " , " + TBL_SCAN_BATCHTR.SCANNER_ID +
                " , " + TBL_SCAN_BATCHTR.BATCH_NAME +
                " , " + TBL_SCAN_BATCHTR.ITEM_ID;
            return strSql;
        }

        public string GetLastBatchSelectQuery(int _GYM_ID, int _OPERATION_DATE, string _SCANNER_ID)
        {
            string strSql = "SELECT * " +
                " FROM " + TBL_SCAN_BATCHTR.TABLE_NAME +
                " WHERE " + TBL_SCAN_BATCHTR.GYM_ID + "='" + _GYM_ID.ToString() + "'" +
                " AND " + TBL_SCAN_BATCHTR.OPERATION_DATE + "='" + _OPERATION_DATE.ToString() + "'" +
                " AND " + TBL_SCAN_BATCHTR.SCANNER_ID + "='" + _SCANNER_ID + "'" +
                " AND " + TBL_SCAN_BATCHTR.BATCH_NAME + "=" +
                " (SELECT MAX(" + TBL_SCAN_BATCHTR.BATCH_NAME + ") " +
                " FROM " + TBL_SCAN_BATCHTR.TABLE_NAME +
                " WHERE " + TBL_SCAN_BATCHTR.GYM_ID + "='" + _GYM_ID.ToString() + "'" +
                " AND " + TBL_SCAN_BATCHTR.OPERATION_DATE + "='" + _OPERATION_DATE.ToString() + "'" +
                " AND " + TBL_SCAN_BATCHTR.SCANNER_ID + "='" + _SCANNER_ID + "'" +
                " AND " + TBL_SCAN_BATCHTR.BATCH_STATUS + "='0')";
            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_SCAN_BATCHTR.TABLE_NAME + " (" +
                TBL_SCAN_BATCHTR.GYM_ID + "," +
                TBL_SCAN_BATCHTR.OPERATION_DATE + "," +
                TBL_SCAN_BATCHTR.SCANNER_ID + "," +
                TBL_SCAN_BATCHTR.BATCH_NAME + "," +
                TBL_SCAN_BATCHTR.BATCH_STATUS + "," +
                TBL_SCAN_BATCHTR.ITEM_ID + "," +
                TBL_SCAN_BATCHTR.ITEM_NAME + "," +
                TBL_SCAN_BATCHTR.ITEM_VALUE +
                ") VALUES (" +
                "'" + m_GYM_ID + "'," +
                "'" + m_OPERATION_DATE + "'," +
                "'" + m_SCANNER_ID + "'," +
                "'" + m_BATCH_NAME + "'," +
                "'" + m_BATCH_STATUS + "'," +
                "'" + m_ITEM_ID + "'," +
                "'" + m_ITEM_NAME + "'," +
                "'" + m_ITEM_VALUE + "'"  +
                ")";

			return strSql;
		}

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_SCAN_BATCHTR.TABLE_NAME + " SET " +
                 TBL_SCAN_BATCHTR.BATCH_STATUS + " = " + "'" + m_BATCH_STATUS + "'," +
                TBL_SCAN_BATCHTR.ITEM_NAME + " = " + "'" + m_ITEM_NAME + "'," +
                TBL_SCAN_BATCHTR.ITEM_VALUE + " = " + "'" + m_ITEM_VALUE + "'" +
                " WHERE " +
                TBL_SCAN_BATCHTR.GYM_ID + " = " + "'" + m_GYM_ID + "'" +
                " AND " + TBL_SCAN_BATCHTR.OPERATION_DATE + " = " + "'" + m_OPERATION_DATE + "'" +
                " AND " + TBL_SCAN_BATCHTR.SCANNER_ID + " = " + "'" + m_SCANNER_ID + "'" +
                " AND " + TBL_SCAN_BATCHTR.BATCH_NAME + " = " + "'" + m_BATCH_NAME + "'" +
                " AND " + TBL_SCAN_BATCHTR.ITEM_ID + " = " + "'" + m_ITEM_ID + "'" ;
                
            return strSql;
        }
        public string GetUpdateStausQuery()
        {
            string strSql = "UPDATE " + TBL_SCAN_BATCHTR.TABLE_NAME + " SET " +
                 TBL_SCAN_BATCHTR.BATCH_STATUS + " = " + "'" + m_BATCH_STATUS + "'" +
                " WHERE " +
                TBL_SCAN_BATCHTR.BATCH_NAME + " = " + "'" + m_BATCH_NAME + "'";

            return strSql;
        }
        public string GetDeleteBatchQuery()
        {
            string strSql = "DELETE FROM " + TBL_SCAN_BATCHTR.TABLE_NAME +
                " WHERE " +
                TBL_SCAN_BATCHTR.BATCH_NAME + " = " + "'" + m_BATCH_NAME + "'";

            return strSql;
        }
        #endregion

    }
}
