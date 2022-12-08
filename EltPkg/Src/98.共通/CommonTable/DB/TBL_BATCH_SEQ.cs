using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// バッチ番号採番
    /// </summary>
    public class TBL_BATCH_SEQ
    {
        public TBL_BATCH_SEQ(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_BATCH_SEQ(int gym_id, int operation_date, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_GYM_ID = gym_id;
            m_OPERATION_DATE = operation_date;
        }

        public TBL_BATCH_SEQ(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "BATCH_SEQ";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string LAST_SEQ = "LAST_SEQ";
        public const string LAST_SEQ_DATE = "LAST_SEQ_DATE";
        public const string LAST_SEQ_TIME = "LAST_SEQ_TIME";
        public const string LAST_FILE_ID = "LAST_FILE_ID";
        public const string LAST_FILE_DIVID = "LAST_FILE_DIVID";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        public int m_LAST_SEQ = 0;
        public int m_LAST_SEQ_DATE = 0;
        public int m_LAST_SEQ_TIME = 0;
        public string m_LAST_FILE_ID = "";
        public string m_LAST_FILE_DIVID = "";
        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BATCH_SEQ.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BATCH_SEQ.OPERATION_DATE]);
            m_LAST_SEQ = DBConvert.ToIntNull(dr[TBL_BATCH_SEQ.LAST_SEQ]);
            m_LAST_SEQ_DATE = DBConvert.ToIntNull(dr[TBL_BATCH_SEQ.LAST_SEQ_DATE]);
            m_LAST_SEQ_TIME = DBConvert.ToIntNull(dr[TBL_BATCH_SEQ.LAST_SEQ_TIME]);
            m_LAST_FILE_ID = DBConvert.ToStringNull(dr[TBL_BATCH_SEQ.LAST_FILE_ID]);
            m_LAST_FILE_DIVID = DBConvert.ToStringNull(dr[TBL_BATCH_SEQ.LAST_FILE_DIVID]);
        }

        #endregion

        #region テーブル名取得

        /// <summary>
        /// テーブル名取得
        /// 引数によりスキーマ変更
        /// </summary>
        /// <returns></returns>
        public static string TABLE_NAME(int Schemabankcd)
        {
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_BATCH_SEQ.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BATCH_SEQ.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_BATCH_SEQ.GYM_ID + "," +
                TBL_BATCH_SEQ.OPERATION_DATE;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operation_date, int Schemabankcd)
        {
            return GetSelectQuery(gymid, operation_date, Schemabankcd, false);
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operation_date, int Schemabankcd, bool Lock)
        {
            string strSql = "SELECT * FROM " + TBL_BATCH_SEQ.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_BATCH_SEQ.GYM_ID + "=" + gymid + " " +
                " AND " + TBL_BATCH_SEQ.OPERATION_DATE + "=" + operation_date + " ";
            if (Lock)
            {
                strSql += string.Format(DBConvert.QUERY_LOCK_WAIT, 5) + " ";
            }
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetInsertQuery(int gymid, int operation_date, int SEQ, int Schemabankcd)
        {
            string strSql = "INSERT INTO " + TBL_BATCH_SEQ.TABLE_NAME(Schemabankcd) + " (" +
                TBL_BATCH_SEQ.GYM_ID + "," +
                TBL_BATCH_SEQ.OPERATION_DATE + "," +
                TBL_BATCH_SEQ.LAST_SEQ + ") VALUES (" +
                gymid + "," +
                operation_date + "," +
                SEQ  + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateQuery(int gymid, int operation_date, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_BATCH_SEQ.TABLE_NAME(Schemabankcd) + " SET " +
                TBL_BATCH_SEQ.LAST_SEQ + "= LAST_SEQ + 1" + ", " +
                TBL_BATCH_SEQ.LAST_SEQ_DATE + "=" + System.DateTime.Now.ToString("yyyyMMdd") + ", " +
                TBL_BATCH_SEQ.LAST_SEQ_TIME + "=" + System.DateTime.Now.ToString("HHmmssfff") + 
                " WHERE " +
                TBL_BATCH_SEQ.GYM_ID + "=" + gymid +
                 " AND " + TBL_BATCH_SEQ.OPERATION_DATE + " = " + operation_date;
            return strSql;
        }

        #endregion
    }
}
