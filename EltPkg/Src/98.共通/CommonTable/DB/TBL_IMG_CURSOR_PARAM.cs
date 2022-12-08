using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// イメージカーソルパラメータ
    /// </summary>
    public class TBL_IMG_CURSOR_PARAM
    {
        public TBL_IMG_CURSOR_PARAM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_IMG_CURSOR_PARAM(int gymid, int dspid, int itemid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_DSP_ID = dspid;
            m_ITEM_ID = itemid;
        }

        public TBL_IMG_CURSOR_PARAM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "IMG_CURSOR_PARAM";

        public const string GYM_ID = "GYM_ID";
        public const string DSP_ID = "DSP_ID";
        public const string ITEM_ID = "ITEM_ID";
        public const string ITEM_TOP = "ITEM_TOP";
        public const string ITEM_LEFT = "ITEM_LEFT";
        public const string ITEM_WIDTH = "ITEM_WIDTH";
        public const string ITEM_HEIGHT = "ITEM_HEIGHT";
        public const string LINE_WEIGHT = "LINE_WEIGHT";
        public const string LINE_COLOR = "LINE_COLOR";

        public const string ALL_COLUMNS = " GYM_ID,DSP_ID,ITEM_ID,ITEM_TOP,ITEM_LEFT,ITEM_WIDTH,ITEM_HEIGHT,LINE_WEIGHT,LINE_COLOR ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_DSP_ID = 0;
        private int m_ITEM_ID = 0;
        public int m_ITEM_TOP = 0;
        public int m_ITEM_LEFT = 0;
        public int m_ITEM_WIDTH = 0;
        public int m_ITEM_HEIGHT = 0;
        public int m_LINE_WEIGHT = 0;
        public int m_LINE_COLOR = 0;

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
        public int _ITEM_ID
        {
            get { return m_ITEM_ID; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.GYM_ID]);
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.DSP_ID]);
            m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.ITEM_ID]);
            m_ITEM_TOP = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.ITEM_TOP]);
            m_ITEM_LEFT = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.ITEM_LEFT]);
            m_ITEM_WIDTH = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.ITEM_WIDTH]);
            m_ITEM_HEIGHT = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.ITEM_HEIGHT]);
            m_LINE_WEIGHT = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.LINE_WEIGHT]);
            m_LINE_COLOR = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR_PARAM.LINE_COLOR]);
        }

        /// <summary>
        /// DataRow を取得する
        /// </summary>
        /// <returns></returns>
        public DataRow ToDataRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.GYM_ID);
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.DSP_ID);
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.ITEM_ID);
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.ITEM_TOP);
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.ITEM_LEFT);
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.ITEM_WIDTH);
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.ITEM_HEIGHT);
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.LINE_WEIGHT);
            dt.Columns.Add(TBL_IMG_CURSOR_PARAM.LINE_COLOR);

            DataRow row = dt.NewRow();
            row[TBL_IMG_CURSOR_PARAM.GYM_ID] = m_GYM_ID;
            row[TBL_IMG_CURSOR_PARAM.DSP_ID] = m_DSP_ID;
            row[TBL_IMG_CURSOR_PARAM.ITEM_ID] = m_ITEM_ID;
            row[TBL_IMG_CURSOR_PARAM.ITEM_TOP] = m_ITEM_TOP;
            row[TBL_IMG_CURSOR_PARAM.ITEM_LEFT] = m_ITEM_LEFT;
            row[TBL_IMG_CURSOR_PARAM.ITEM_WIDTH] = m_ITEM_WIDTH;
            row[TBL_IMG_CURSOR_PARAM.ITEM_HEIGHT] = m_ITEM_HEIGHT;
            row[TBL_IMG_CURSOR_PARAM.LINE_WEIGHT] = m_LINE_WEIGHT;
            row[TBL_IMG_CURSOR_PARAM.LINE_COLOR] = m_LINE_COLOR;
            return row;
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_IMG_CURSOR_PARAM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_IMG_CURSOR_PARAM.GYM_ID + "," +
                TBL_IMG_CURSOR_PARAM.DSP_ID + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_IMG_CURSOR_PARAM.GYM_ID + "=" + gymid +
                " ORDER BY " +
                TBL_IMG_CURSOR_PARAM.DSP_ID + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_IMG_CURSOR_PARAM.GYM_ID + "=" + gymid + " AND " +
                TBL_IMG_CURSOR_PARAM.DSP_ID + "=" + dspid;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int itemid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_IMG_CURSOR_PARAM.GYM_ID + "=" + gymid + " AND " +
                TBL_IMG_CURSOR_PARAM.DSP_ID + "=" + dspid + " AND " +
                TBL_IMG_CURSOR_PARAM.ITEM_ID + "=" + itemid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_IMG_CURSOR_PARAM.GYM_ID + "," +
                TBL_IMG_CURSOR_PARAM.DSP_ID + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_ID + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_TOP + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_LEFT + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_WIDTH + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_HEIGHT + "," +
                TBL_IMG_CURSOR_PARAM.LINE_WEIGHT + "," +
                TBL_IMG_CURSOR_PARAM.LINE_COLOR + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                m_ITEM_ID + "," +
                m_ITEM_TOP + "," +
                m_ITEM_LEFT + "," +
                m_ITEM_WIDTH + "," +
                m_ITEM_HEIGHT + "," +
                m_LINE_WEIGHT + "," +
                m_LINE_COLOR + ")";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery(string tableName)
        {
            string strSQL = "INSERT INTO " + tableName + " (" +
                TBL_IMG_CURSOR_PARAM.GYM_ID + "," +
                TBL_IMG_CURSOR_PARAM.DSP_ID + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_ID + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_TOP + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_LEFT + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_WIDTH + "," +
                TBL_IMG_CURSOR_PARAM.ITEM_HEIGHT + "," +
                TBL_IMG_CURSOR_PARAM.LINE_WEIGHT + "," +
                TBL_IMG_CURSOR_PARAM.LINE_COLOR + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                m_ITEM_ID + "," +
                m_ITEM_TOP + "," +
                m_ITEM_LEFT + "," +
                m_ITEM_WIDTH + "," +
                m_ITEM_HEIGHT + "," +
                m_LINE_WEIGHT + "," +
                m_LINE_COLOR + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_IMG_CURSOR_PARAM.ITEM_TOP + "=" + m_ITEM_TOP + ", " +
                TBL_IMG_CURSOR_PARAM.ITEM_LEFT + "=" + m_ITEM_LEFT + ", " +
                TBL_IMG_CURSOR_PARAM.ITEM_WIDTH + "=" + m_ITEM_WIDTH + ", " +
                TBL_IMG_CURSOR_PARAM.ITEM_HEIGHT + "=" + m_ITEM_HEIGHT + ", " +
                TBL_IMG_CURSOR_PARAM.LINE_WEIGHT + "=" + m_LINE_WEIGHT + ", " +
                TBL_IMG_CURSOR_PARAM.LINE_COLOR + "=" + m_LINE_COLOR + " " +
                " WHERE " +
                TBL_IMG_CURSOR_PARAM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_IMG_CURSOR_PARAM.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_IMG_CURSOR_PARAM.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_IMG_CURSOR_PARAM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_IMG_CURSOR_PARAM.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_IMG_CURSOR_PARAM.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        #endregion
    }
}
