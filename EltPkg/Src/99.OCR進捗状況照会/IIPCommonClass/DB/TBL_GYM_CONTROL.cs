using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// 業務日付コントロール
	/// </summary>
	public class TBL_GYM_CONTROL
    {
        public TBL_GYM_CONTROL()
        {
        }

        public TBL_GYM_CONTROL(int gym_id)
        {
            m_GYM_ID = gym_id;
        }

		public TBL_GYM_CONTROL(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "GYM_CONTROL";
        //カラム
		public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string OPERATION_PRE_DATE = "OPERATION_PRE_DATE";
        public const string OPERATION_AFT_DATE = "OPERATION_AFT_DATE";
        public const string BUSINESS_DATE = "BUSINESS_DATE";
        public const string BUSINESS_PRE_DATE = "BUSINESS_PRE_DATE";
        public const string BUSINESS_AFT_DATE = "BUSINESS_AFT_DATE";
		#endregion

		#region メンバ

		public int m_GYM_ID = 0;
        public int m_OPERATION_DATE = 0;
        public int m_OPERATION_PRE_DATE = 0;
        public int m_OPERATION_AFT_DATE = 0;
        public int m_BUSINESS_DATE = 0;
        public int m_BUSINESS_PRE_DATE = 0;
        public int m_BUSINESS_AFT_DATE = 0;
		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_GYM_CONTROL.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_GYM_CONTROL.OPERATION_DATE]);
            m_OPERATION_PRE_DATE = DBConvert.ToIntNull(dr[TBL_GYM_CONTROL.OPERATION_PRE_DATE]);
            m_OPERATION_AFT_DATE = DBConvert.ToIntNull(dr[TBL_GYM_CONTROL.OPERATION_AFT_DATE]);
            m_BUSINESS_DATE = DBConvert.ToIntNull(dr[TBL_GYM_CONTROL.BUSINESS_DATE]);
            m_BUSINESS_PRE_DATE = DBConvert.ToIntNull(dr[TBL_GYM_CONTROL.BUSINESS_PRE_DATE]);
            m_BUSINESS_AFT_DATE = DBConvert.ToIntNull(dr[TBL_GYM_CONTROL.BUSINESS_AFT_DATE]);
        }

		#endregion

		#region クエリ取得

        /// <summary>
		/// 業務番号を条件とするSELECT文を作成します
		/// </summary>
        /// <param name="gymid">業務番号</param>
		/// <returns></returns>
        public string GetSelectQuery(int gymid)
        {
            return GetSelectQuery(gymid, true);
        }

        /// <summary>
        /// 業務番号を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <param name="locked">編集ロックされているもの(DONE_FLG=0)は含めるかどうか</param>
        /// <returns></returns>
        public string GetSelectQuery(int gymid, bool locked)
        {
            string strSql = "SELECT * FROM " + TBL_GYM_CONTROL.TABLE_NAME +
                " WHERE " + TBL_GYM_CONTROL.GYM_ID + "=" + gymid;
            return strSql;
        }

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_GYM_CONTROL.TABLE_NAME + " (" +
                TBL_GYM_CONTROL.GYM_ID + "," +
                TBL_GYM_CONTROL.OPERATION_DATE + "," +
                TBL_GYM_CONTROL.OPERATION_PRE_DATE + "," +
                TBL_GYM_CONTROL.OPERATION_AFT_DATE + "," +
                TBL_GYM_CONTROL.BUSINESS_DATE + "," +
                TBL_GYM_CONTROL.BUSINESS_PRE_DATE + "," +
                TBL_GYM_CONTROL.BUSINESS_AFT_DATE + ") VALUES (" +
                m_GYM_ID + "," +
                "'" + m_OPERATION_DATE + "'," +
                "'" + m_OPERATION_PRE_DATE + "'," +
                "'" + m_OPERATION_AFT_DATE + "'," +
                "'" + m_BUSINESS_DATE + "'," +
                "'" + m_BUSINESS_PRE_DATE + "'," +
                "'" + m_BUSINESS_AFT_DATE + "')";

			return strSql;
		}

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_GYM_CONTROL.TABLE_NAME + " SET " +
                TBL_GYM_CONTROL.OPERATION_DATE + "='" + m_OPERATION_DATE + "', " +
                TBL_GYM_CONTROL.OPERATION_PRE_DATE + "='" + m_OPERATION_PRE_DATE + "', " +
                TBL_GYM_CONTROL.OPERATION_AFT_DATE + "='" + m_OPERATION_AFT_DATE + "', " +
                TBL_GYM_CONTROL.BUSINESS_DATE + "='" + m_BUSINESS_DATE + "', " +
                TBL_GYM_CONTROL.BUSINESS_PRE_DATE + "='" + m_BUSINESS_PRE_DATE + "', " +
                TBL_GYM_CONTROL.BUSINESS_AFT_DATE + "='" + m_BUSINESS_AFT_DATE + "' " +
                "WHERE " + TBL_GYM_PARAM.GYM_ID + "=" + m_GYM_ID;

            return strSql;
        }


        #endregion

	}
}
