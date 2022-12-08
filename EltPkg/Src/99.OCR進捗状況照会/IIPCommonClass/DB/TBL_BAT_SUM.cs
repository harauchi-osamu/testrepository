using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// TBL_BAT_SUM
	/// </summary>
	public class TBL_BAT_SUM
	{
        public TBL_BAT_SUM()
        {
        }

        public TBL_BAT_SUM(int gym_id, int bat_id, string bat_details_kbn, int sum_id, string scanner_id, int operation_date)
        {
            m_GYM_ID = gym_id;
            m_BAT_ID = bat_id;
            m_BAT_DETAILS_KBN = bat_details_kbn;
            m_SUM_ID = sum_id;
            m_SCANNER_ID = scanner_id;
            m_OPERATION_DATE = operation_date;
        }

		public TBL_BAT_SUM(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

  		public const string TABLE_NAME = "BAT_SUM";

        public const string GYM_ID = "GYM_ID";
        public const string BAT_ID = "BAT_ID";
		public const string BAT_DETAILS_KBN = "BAT_DETAILS_KBN";
		public const string SUM_ID = "SUM_ID";
		public const string SUM_NAME = "SUM_NAME";
		public const string SUM_VALUE = "SUM_VALUE";
        public const string SCANNER_ID = "SCANNER_ID";
		public const string OPERATION_DATE = "OPERATION_DATE";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_BAT_ID = 0;
		private string m_BAT_DETAILS_KBN = "";
		private int m_SUM_ID = 0;
		public string m_SUM_NAME = "";
		public decimal m_SUM_VALUE = 0;
		private string m_SCANNER_ID;
		private int m_OPERATION_DATE;

		#endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }

        public int _BAT_ID
        {
            get { return m_BAT_ID; }
        }

        public string _BAT_DETAILS_KBN
        {
            get { return m_BAT_DETAILS_KBN; }
        }

        public int _SUM_ID
        {
            get { return m_SUM_ID; }
        }

        public string _SCANNER_ID
        {
            get { return m_SCANNER_ID; }
        }

        public int _OPERATION_DATE
        {
            get { return m_OPERATION_DATE; }
        }

        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BAT_SUM.GYM_ID]);
			m_BAT_ID = DBConvert.ToIntNull(dr[TBL_BAT_SUM.BAT_ID]);
			m_BAT_DETAILS_KBN = DBConvert.ToStringNull(dr[TBL_BAT_SUM.BAT_DETAILS_KBN]);
			m_SUM_ID = DBConvert.ToIntNull(dr[TBL_BAT_SUM.SUM_ID]);
			m_SUM_NAME = DBConvert.ToStringNull(dr[TBL_BAT_SUM.SUM_NAME]);
			m_SUM_VALUE = DBConvert.ToDecimalNull(dr[TBL_BAT_SUM.SUM_VALUE]);
			m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_BAT_SUM.SCANNER_ID]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BAT_SUM.OPERATION_DATE]);

        }

		#endregion

		#region クエリ取得

        /// <summary>
		/// キーを条件とするSELECT文を作成します
		/// </summary>
        /// <param name="gym_id">業務番号</param>
        /// <param name="bat_id">バッチ番号</param>
        /// <param name="bat_details_kbn">バッチ明細区分</param>
        /// <param name="sum_id">合計番号</param>
        /// <param name="scanner_id">スキャナー号機</param>
        /// <param name="operation_date">処理日</param>
        /// <returns></returns>
        public string GetSelectQuery(int gym_id, int bat_id, string bat_details_kbn, int sum_id, string scanner_id, int operation_date)
        {
            string strSql = "SELECT * FROM " + TBL_BAT_SUM.TABLE_NAME +
                  " WHERE " + TBL_BAT_SUM.GYM_ID + "=" + gym_id +
                  " AND " + TBL_BAT_SUM.BAT_ID + "=" + bat_id +
                  " AND " + TBL_BAT_SUM.BAT_DETAILS_KBN + "='" + bat_details_kbn + "'" +
                  " AND " + TBL_BAT_SUM.SUM_ID + "=" + sum_id +
                  " AND " + TBL_BAT_SUM.SCANNER_ID + "='" + scanner_id + "'" +
                  " AND " + TBL_BAT_SUM.OPERATION_DATE + "=" + operation_date;

            return strSql;
        }

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_BAT_SUM.TABLE_NAME + " (" +
				TBL_BAT_SUM.GYM_ID + "," + 
				TBL_BAT_SUM.BAT_ID + "," + 
				TBL_BAT_SUM.BAT_DETAILS_KBN + "," +
				TBL_BAT_SUM.SUM_ID + "," +
				TBL_BAT_SUM.SUM_NAME + "," +
				TBL_BAT_SUM.SUM_VALUE + "," +
				TBL_BAT_SUM.SCANNER_ID + "," + 
				TBL_BAT_SUM.OPERATION_DATE + ") VALUES (" +
				m_GYM_ID + "," + 
				m_BAT_ID + "," + 
				"'" + m_BAT_DETAILS_KBN + "," + 
				m_SUM_ID + "," + 
				"'" + m_SUM_NAME + "," + 
				m_SUM_VALUE + "," + 
				"'" + m_SCANNER_ID + "'," + 
				m_OPERATION_DATE + ")";

			return strSql;
		}

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BAT_SUM.TABLE_NAME + " SET " +
				TBL_BAT_SUM.SUM_NAME + "='" + m_SUM_NAME + "', " +
				TBL_BAT_SUM.SUM_VALUE + "=" + m_SUM_VALUE + ", " +
				" WHERE " +
				TBL_BAT_SUM.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_SUM.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_SUM.BAT_DETAILS_KBN + "='" + m_BAT_DETAILS_KBN + "' AND " +
				TBL_BAT_SUM.SUM_ID + "=" + m_SUM_ID + " AND " +
				TBL_BAT_SUM.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_SUM.OPERATION_DATE + "=" + m_OPERATION_DATE;

            return strSql;
        }


        #endregion
    }
}
