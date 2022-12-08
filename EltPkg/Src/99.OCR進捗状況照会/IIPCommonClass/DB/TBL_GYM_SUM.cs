using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// 業務パラメーター
	/// </summary>
	public class TBL_GYM_SUM
	{
        public TBL_GYM_SUM()
        {
        }

        public TBL_GYM_SUM(int gym_id, int sum_id)
        {
            m_GYM_ID = gym_id;
            m_SUM_ID = sum_id;
        }

		public TBL_GYM_SUM(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

		public const string TABLE_NAME = "GYM_SUM";

        public const string GYM_ID = "GYM_ID";
        public const string SUM_ID = "SUM_ID";
        public const string SUM_NAME = "SUM_NAME";
        public const string NAME_POS_TOP = "NAME_POS_TOP";
        public const string NAME_POS_LEFT = "NAME_POS_LEFT";
        public const string SUM_POS_TOP = "SUM_POS_TOP";
        public const string SUM_POS_LEFT = "SUM_POS_LEFT";
        public const string INPUT_WIDTH = "INPUT_WIDTH";
        public const string INPUT_HEIGHT = "INPUT_HEIGHT";
        public const string ITEM_LEN = "ITEM_LEN";
        public const string IF_CHK = "IF_CHK";
        public const string DELETE_FLG = "DELETE_FLG";
        public const string CREATE_USER = "CREATE_USER";
        public const string CREATE_TIME = "CREATE_TIME";
        public const string UPDATE_USER = "UPDATE_USER";
        public const string UPDATE_TIME = "UPDATE_TIME";

		#endregion

		#region メンバ

        private int m_GYM_ID = 0;
        private int m_SUM_ID = 0;
        public string m_SUM_NAME = "";
        public int m_NAME_POS_TOP = 0;
        public int m_NAME_POS_LEFT = 0;
        public int m_SUM_POS_TOP = 0;
        public int m_SUM_POS_LEFT = 0;
        public int m_INPUT_WIDTH = 0;
        public int m_INPUT_HEIGHT = 0;
        public int m_ITEM_LEN = 0;
        public string m_IF_CHK = "";
        public string m_DELETE_FLG = "0";
        public string m_CREATE_USER = "";
        public string m_CREATE_TIME = "";
        public string m_UPDATE_USER = "";
        public string m_UPDATE_TIME = "";


		#endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }

        public int _SUM_ID
        {
            get { return m_SUM_ID; }
        }

        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_GYM_SUM.GYM_ID]);
            m_SUM_ID = DBConvert.ToIntNull(dr[TBL_GYM_SUM.SUM_ID]);
            m_SUM_NAME = DBConvert.ToStringNull(dr[TBL_GYM_SUM.SUM_NAME]);
            m_NAME_POS_TOP = DBConvert.ToIntNull(dr[TBL_GYM_SUM.NAME_POS_TOP]);
            m_NAME_POS_LEFT = DBConvert.ToIntNull(dr[TBL_GYM_SUM.NAME_POS_LEFT]);
            m_SUM_POS_TOP = DBConvert.ToIntNull(dr[TBL_GYM_SUM.SUM_POS_TOP]);
            m_SUM_POS_LEFT = DBConvert.ToIntNull(dr[TBL_GYM_SUM.SUM_POS_LEFT]);
            m_INPUT_WIDTH = DBConvert.ToIntNull(dr[TBL_GYM_SUM.INPUT_WIDTH]);
            m_INPUT_HEIGHT = DBConvert.ToIntNull(dr[TBL_GYM_SUM.INPUT_HEIGHT]);
            m_ITEM_LEN = DBConvert.ToIntNull(dr[TBL_GYM_SUM.ITEM_LEN]);
            m_IF_CHK = DBConvert.ToStringNull(dr[TBL_GYM_SUM.IF_CHK]);
            m_DELETE_FLG = DBConvert.ToStringNull(dr[TBL_GYM_SUM.DELETE_FLG]);
            m_CREATE_USER = DBConvert.ToStringNull(dr[TBL_GYM_SUM.CREATE_USER]);
            m_CREATE_TIME = DBConvert.ToStringNull(dr[TBL_GYM_SUM.CREATE_TIME]);
            m_UPDATE_USER = DBConvert.ToStringNull(dr[TBL_GYM_SUM.UPDATE_USER]);
            m_UPDATE_TIME = DBConvert.ToStringNull(dr[TBL_GYM_SUM.UPDATE_TIME]);
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
            string strSql = "SELECT * FROM " + TBL_GYM_SUM.TABLE_NAME +
                  " WHERE " + TBL_GYM_SUM.GYM_ID + "=" + gymid +
                  " ORDER BY " + TBL_GYM_SUM.SUM_ID;

            return strSql;
        }

        /// <summary>
        /// 業務番号, 合計番号を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <param name="sumid">合計番号</param>
        /// <returns></returns>
        public string GetSelectQuery(int gymid, int sumid)
        {
            string strSql = "SELECT * FROM " + TBL_GYM_SUM.TABLE_NAME +
                  " WHERE " + TBL_GYM_SUM.GYM_ID + "=" + gymid +
                  " AND " + TBL_GYM_SUM.SUM_ID + "=" + sumid;

            return strSql;
        }

        /// <summary>
		/// ホストへ送信する為のインサート文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_GYM_SUM.TABLE_NAME + " (" +
                TBL_GYM_SUM.GYM_ID + "," +
                TBL_GYM_SUM.SUM_ID + "," +
                TBL_GYM_SUM.SUM_NAME + "," +
                TBL_GYM_SUM.NAME_POS_TOP + "," +
                TBL_GYM_SUM.NAME_POS_LEFT + "," +
                TBL_GYM_SUM.SUM_POS_TOP + "," +
                TBL_GYM_SUM.SUM_POS_LEFT + "," +
                TBL_GYM_SUM.INPUT_WIDTH + "," +
                TBL_GYM_SUM.INPUT_HEIGHT + "," +
                TBL_GYM_SUM.ITEM_LEN + "," +
                TBL_GYM_SUM.IF_CHK + "," +
                TBL_GYM_SUM.DELETE_FLG + "," +
                TBL_GYM_SUM.CREATE_USER + "," +
                TBL_GYM_SUM.CREATE_TIME + "," +
                TBL_GYM_SUM.UPDATE_USER + "," +
                TBL_GYM_SUM.UPDATE_TIME + ") VALUES (" +
                m_GYM_ID + "," +
                m_SUM_ID + "," +
                "'" + m_SUM_NAME + "'," +
                m_NAME_POS_TOP + "," +
                m_NAME_POS_LEFT + "," +
                m_SUM_POS_TOP + "," +
                m_SUM_POS_LEFT + "," +
                m_INPUT_WIDTH + "," +
                m_INPUT_HEIGHT + "," +
                m_ITEM_LEN + "," +
                "'" + m_IF_CHK + "'," +
                "'" + m_DELETE_FLG + "'," +
                "'" + m_CREATE_USER + "'," +
                "'" + m_CREATE_TIME + "'," +
                "'" + m_UPDATE_USER + "'," +
                "'" + m_UPDATE_TIME + "')";

			return strSql;
		}

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_GYM_SUM.TABLE_NAME + " SET " +

                TBL_GYM_SUM.SUM_NAME + "='" + m_SUM_NAME + "', " +
                TBL_GYM_SUM.NAME_POS_TOP + "=" + m_NAME_POS_TOP + ", " +
                TBL_GYM_SUM.NAME_POS_LEFT + "=" + m_NAME_POS_LEFT + ", " +
                TBL_GYM_SUM.SUM_POS_TOP + "=" + m_SUM_POS_TOP + ", " +
                TBL_GYM_SUM.SUM_POS_LEFT + "=" + m_SUM_POS_LEFT + ", " +
                TBL_GYM_SUM.INPUT_WIDTH + "=" + m_INPUT_WIDTH + ", " +
                TBL_GYM_SUM.INPUT_HEIGHT + "=" + m_INPUT_HEIGHT + ", " +
                TBL_GYM_SUM.ITEM_LEN + "=" + m_ITEM_LEN + ", " +
                TBL_GYM_SUM.IF_CHK + "='" + m_IF_CHK + "', " +
                TBL_GYM_SUM.DELETE_FLG + "='" + m_DELETE_FLG + "', " +
                TBL_GYM_SUM.UPDATE_USER + "='" + m_UPDATE_USER + "', " +
                TBL_GYM_SUM.UPDATE_TIME + "='" + m_UPDATE_TIME + "'" +
                " WHERE " +
                TBL_GYM_SUM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_GYM_SUM.SUM_ID + "=" + m_SUM_ID;

            return strSql;
        }

		#endregion
    }
}
