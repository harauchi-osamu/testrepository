using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 画面パラメータ
    /// </summary>
    public class TBL_DSP_PARAM
    {
        public TBL_DSP_PARAM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_DSP_PARAM(int gymid, int dspid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_DSP_ID = dspid;
        }

        public TBL_DSP_PARAM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "DSP_PARAM";

        public const string GYM_ID = "GYM_ID";
        public const string DSP_ID = "DSP_ID";
        public const string DSP_NAME = "DSP_NAME";
        public const string FONT_SIZE = "FONT_SIZE";
        public const string DSP_WIDTH = "DSP_WIDTH";
        public const string DSP_HEIGHT = "DSP_HEIGHT";
        public const string OCR_NAME = "OCR_NAME";

        public const string ALL_COLUMNS = " GYM_ID,DSP_ID,DSP_NAME,FONT_SIZE,DSP_WIDTH,DSP_HEIGHT,OCR_NAME ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_DSP_ID = 0;
        public string m_DSP_NAME = "";
        public int m_FONT_SIZE = 14;
        public int m_DSP_WIDTH = 0;
        public int m_DSP_HEIGHT = 0;
        public string m_OCR_NAME = "";

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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.GYM_ID]);
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.DSP_ID]);
            m_DSP_NAME = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.DSP_NAME]);
            m_FONT_SIZE = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.FONT_SIZE]);
            m_DSP_WIDTH = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.DSP_WIDTH]);
            m_DSP_HEIGHT = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.DSP_HEIGHT]);
            m_OCR_NAME = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.OCR_NAME]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_DSP_PARAM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_DSP_PARAM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_DSP_PARAM.GYM_ID + "," +
                TBL_DSP_PARAM.DSP_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_DSP_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_DSP_PARAM.GYM_ID + "=" + gymid +
                " ORDER BY " +
                TBL_DSP_PARAM.DSP_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_DSP_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_DSP_PARAM.GYM_ID + "=" + gymid + " AND " +
                TBL_DSP_PARAM.DSP_ID + "=" + dspid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_DSP_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_DSP_PARAM.GYM_ID + "," +
                TBL_DSP_PARAM.DSP_ID + "," +
                TBL_DSP_PARAM.DSP_NAME + "," +
                TBL_DSP_PARAM.FONT_SIZE + "," +
                TBL_DSP_PARAM.DSP_WIDTH + "," +
                TBL_DSP_PARAM.DSP_HEIGHT + "," +
                TBL_DSP_PARAM.OCR_NAME + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                "'" + m_DSP_NAME + "'," +
                m_FONT_SIZE + "," +
                m_DSP_WIDTH + "," +
                m_DSP_HEIGHT + "," +
                "'" + m_OCR_NAME + "')";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery(string tableName)
        {
            string strSQL = "INSERT INTO " + tableName + " (" +
                TBL_DSP_PARAM.GYM_ID + "," +
                TBL_DSP_PARAM.DSP_ID + "," +
                TBL_DSP_PARAM.DSP_NAME + "," +
                TBL_DSP_PARAM.FONT_SIZE + "," +
                TBL_DSP_PARAM.DSP_WIDTH + "," +
                TBL_DSP_PARAM.DSP_HEIGHT + "," +
                TBL_DSP_PARAM.OCR_NAME + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                "'" + m_DSP_NAME + "'," +
                m_FONT_SIZE + "," +
                m_DSP_WIDTH + "," +
                m_DSP_HEIGHT + "," +
                "'" + m_OCR_NAME + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_DSP_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_DSP_PARAM.DSP_NAME + "='" + m_DSP_NAME + "', " +
                TBL_DSP_PARAM.FONT_SIZE + "=" + m_FONT_SIZE + ", " +
                TBL_DSP_PARAM.DSP_WIDTH + "=" + m_DSP_WIDTH + ", " +
                TBL_DSP_PARAM.DSP_HEIGHT + "=" + m_DSP_HEIGHT + ", " +
                TBL_DSP_PARAM.OCR_NAME + "='" + m_OCR_NAME + "' " +
                " WHERE " +
                TBL_DSP_PARAM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_DSP_PARAM.DSP_ID + "=" + m_DSP_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_DSP_PARAM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_DSP_PARAM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_DSP_PARAM.DSP_ID + "=" + m_DSP_ID;
            return strSQL;
        }

        #endregion
    }
}
