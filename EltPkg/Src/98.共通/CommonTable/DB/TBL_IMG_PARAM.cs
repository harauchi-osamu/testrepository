using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// イメージパラメータ
    /// </summary>
    public class TBL_IMG_PARAM
    {
        public TBL_IMG_PARAM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_IMG_PARAM(int gymid, int dspid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_DSP_ID = dspid;
        }

        public TBL_IMG_PARAM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "IMG_PARAM";

        public const string GYM_ID = "GYM_ID";
        public const string DSP_ID = "DSP_ID";
        public const string IMG_FILE = "IMG_FILE";
        public const string REDUCE_RATE = "REDUCE_RATE";
        public const string IMG_TOP = "IMG_TOP";
        public const string IMG_LEFT = "IMG_LEFT";
        public const string IMG_WIDTH = "IMG_WIDTH";
        public const string IMG_HEIGHT = "IMG_HEIGHT";
        public const string IMG_BASE_POINT = "IMG_BASE_POINT";
        public const string XSCROLL_LEFT = "XSCROLL_LEFT";
        public const string XSCROLL_VALUE = "XSCROLL_VALUE";
        public const string XSCROLL_RIGHT = "XSCROLL_RIGHT";

        public const string ALL_COLUMNS = " GYM_ID,DSP_ID,IMG_FILE,REDUCE_RATE,IMG_TOP,IMG_LEFT,IMG_WIDTH,IMG_HEIGHT,IMG_BASE_POINT,XSCROLL_LEFT,XSCROLL_VALUE,XSCROLL_RIGHT ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_DSP_ID = 0;
        public string m_IMG_FILE = "";
        public int m_REDUCE_RATE = 0;
        public int m_IMG_TOP = 0;
        public int m_IMG_LEFT = 0;
        public int m_IMG_WIDTH = 0;
        public int m_IMG_HEIGHT = 0;
        public int m_IMG_BASE_POINT = 0;
        public int m_XSCROLL_LEFT = 0;
        public int m_XSCROLL_VALUE = 0;
        public int m_XSCROLL_RIGHT = 0;

        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }
        public int _DSP_ID
        {
            get { return m_DSP_ID; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.GYM_ID]);
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.DSP_ID]);
            m_IMG_FILE = DBConvert.ToStringNull(dr[TBL_IMG_PARAM.IMG_FILE]);
            m_REDUCE_RATE = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.REDUCE_RATE]);
            m_IMG_TOP = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.IMG_TOP]);
            m_IMG_LEFT = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.IMG_LEFT]);
            m_IMG_WIDTH = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.IMG_WIDTH]);
            m_IMG_HEIGHT = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.IMG_HEIGHT]);
            m_IMG_BASE_POINT = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.IMG_BASE_POINT]);
            m_XSCROLL_LEFT = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.XSCROLL_LEFT]);
            m_XSCROLL_VALUE = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.XSCROLL_VALUE]);
            m_XSCROLL_RIGHT = DBConvert.ToIntNull(dr[TBL_IMG_PARAM.XSCROLL_RIGHT]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_IMG_PARAM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_IMG_PARAM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_IMG_PARAM.GYM_ID + "," +
                TBL_IMG_PARAM.DSP_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_IMG_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_IMG_PARAM.GYM_ID + "=" + gymid +
                " ORDER BY " +
                TBL_IMG_PARAM.DSP_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_IMG_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_IMG_PARAM.GYM_ID + "=" + gymid + " AND " +
                TBL_IMG_PARAM.DSP_ID + "=" + dspid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_IMG_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_IMG_PARAM.GYM_ID + "," +
                TBL_IMG_PARAM.DSP_ID + "," +
                TBL_IMG_PARAM.IMG_FILE + "," +
                TBL_IMG_PARAM.REDUCE_RATE + "," +
                TBL_IMG_PARAM.IMG_TOP + "," +
                TBL_IMG_PARAM.IMG_LEFT + "," +
                TBL_IMG_PARAM.IMG_WIDTH + "," +
                TBL_IMG_PARAM.IMG_HEIGHT + "," +
                TBL_IMG_PARAM.IMG_BASE_POINT + "," +
                TBL_IMG_PARAM.XSCROLL_LEFT + "," +
                TBL_IMG_PARAM.XSCROLL_VALUE + "," +
                TBL_IMG_PARAM.XSCROLL_RIGHT + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                "'" + m_IMG_FILE + "'," +
                m_REDUCE_RATE + "," +
                m_IMG_TOP + "," +
                m_IMG_LEFT + "," +
                m_IMG_WIDTH + "," +
                m_IMG_HEIGHT + "," +
                m_IMG_BASE_POINT + "," +
                m_XSCROLL_LEFT + "," +
                m_XSCROLL_VALUE + "," +
                m_XSCROLL_RIGHT + ")";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery(string tableName)
        {
            string strSQL = "INSERT INTO " + tableName + " (" +
                TBL_IMG_PARAM.GYM_ID + "," +
                TBL_IMG_PARAM.DSP_ID + "," +
                TBL_IMG_PARAM.IMG_FILE + "," +
                TBL_IMG_PARAM.REDUCE_RATE + "," +
                TBL_IMG_PARAM.IMG_TOP + "," +
                TBL_IMG_PARAM.IMG_LEFT + "," +
                TBL_IMG_PARAM.IMG_WIDTH + "," +
                TBL_IMG_PARAM.IMG_HEIGHT + "," +
                TBL_IMG_PARAM.IMG_BASE_POINT + "," +
                TBL_IMG_PARAM.XSCROLL_LEFT + "," +
                TBL_IMG_PARAM.XSCROLL_VALUE + "," +
                TBL_IMG_PARAM.XSCROLL_RIGHT + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                "'" + m_IMG_FILE + "'," +
                m_REDUCE_RATE + "," +
                m_IMG_TOP + "," +
                m_IMG_LEFT + "," +
                m_IMG_WIDTH + "," +
                m_IMG_HEIGHT + "," +
                m_IMG_BASE_POINT + "," +
                m_XSCROLL_LEFT + "," +
                m_XSCROLL_VALUE + "," +
                m_XSCROLL_RIGHT + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_IMG_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_IMG_PARAM.IMG_FILE + "='" + m_IMG_FILE + "', " +
                TBL_IMG_PARAM.REDUCE_RATE + "=" + m_REDUCE_RATE + ", " +
                TBL_IMG_PARAM.IMG_TOP + "=" + m_IMG_TOP + ", " +
                TBL_IMG_PARAM.IMG_LEFT + "=" + m_IMG_LEFT + ", " +
                TBL_IMG_PARAM.IMG_WIDTH + "=" + m_IMG_WIDTH + ", " +
                TBL_IMG_PARAM.IMG_HEIGHT + "=" + m_IMG_HEIGHT + ", " +
                TBL_IMG_PARAM.IMG_BASE_POINT + "=" + m_IMG_BASE_POINT + ", " +
                TBL_IMG_PARAM.XSCROLL_LEFT + "=" + m_XSCROLL_LEFT + ", " +
                TBL_IMG_PARAM.XSCROLL_VALUE + "=" + m_XSCROLL_VALUE + ", " +
                TBL_IMG_PARAM.XSCROLL_RIGHT + "=" + m_XSCROLL_RIGHT + " " +
                " WHERE " +
                TBL_IMG_PARAM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_IMG_PARAM.DSP_ID + "=" + m_DSP_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_IMG_PARAM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_IMG_PARAM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_IMG_PARAM.DSP_ID + "=" + m_DSP_ID;
            return strSQL;
        }

        #endregion
    }
}
