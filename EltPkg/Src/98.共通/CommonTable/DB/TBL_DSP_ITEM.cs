using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 画面項目定義
    /// </summary>
    public class TBL_DSP_ITEM
    {
        public TBL_DSP_ITEM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_DSP_ITEM(int gymid, int dspid, int itemid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_DSP_ID = dspid;
            m_ITEM_ID = itemid;
        }

        public TBL_DSP_ITEM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "DSP_ITEM";

        public const string GYM_ID = "GYM_ID";
        public const string DSP_ID = "DSP_ID";
        public const string ITEM_ID = "ITEM_ID";
        public const string ITEM_DISPNAME = "ITEM_DISPNAME";
        public const string ITEM_TYPE = "ITEM_TYPE";
        public const string ITEM_LEN = "ITEM_LEN";
        public const string POS = "POS";
        public const string DUP = "DUP";
        public const string AUTO_INPUT = "AUTO_INPUT";
        public const string ITEM_SUBRTN = "ITEM_SUBRTN";
        public const string BLANK_FLG = "BLANK_FLG";

        public const string ALL_COLUMNS = " GYM_ID,DSP_ID,ITEM_ID,ITEM_DISPNAME,ITEM_TYPE,ITEM_LEN,POS,DUP,AUTO_INPUT,ITEM_SUBRTN,BLANK_FLG ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_DSP_ID = 0;
        private int m_ITEM_ID = 0;
        public string m_ITEM_DISPNAME = "";
        public string m_ITEM_TYPE = "";
        public int m_ITEM_LEN = 0;
        public int m_POS = 0;
        public string m_DUP = "";
        public string m_AUTO_INPUT = "";
        public string m_ITEM_SUBRTN = "";
        public string m_BLANK_FLG = "0";

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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.GYM_ID]);
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.DSP_ID]);
            m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.ITEM_ID]);
            m_ITEM_DISPNAME = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.ITEM_DISPNAME]);
            m_ITEM_TYPE = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.ITEM_TYPE]);
            m_ITEM_LEN = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.ITEM_LEN]);
            m_POS = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.POS]);
            m_DUP = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.DUP]);
            m_AUTO_INPUT = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.AUTO_INPUT]);
            m_ITEM_SUBRTN = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.ITEM_SUBRTN]);
            m_BLANK_FLG = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.BLANK_FLG]);
        }

        /// <summary>
        /// DataRow を取得する
        /// </summary>
        /// <returns></returns>
        public DataRow ToDataRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TBL_DSP_ITEM.GYM_ID);
            dt.Columns.Add(TBL_DSP_ITEM.DSP_ID);
            dt.Columns.Add(TBL_DSP_ITEM.ITEM_ID);
            dt.Columns.Add(TBL_DSP_ITEM.ITEM_DISPNAME);
            dt.Columns.Add(TBL_DSP_ITEM.ITEM_TYPE);
            dt.Columns.Add(TBL_DSP_ITEM.ITEM_LEN);
            dt.Columns.Add(TBL_DSP_ITEM.POS);
            dt.Columns.Add(TBL_DSP_ITEM.DUP);
            dt.Columns.Add(TBL_DSP_ITEM.AUTO_INPUT);
            dt.Columns.Add(TBL_DSP_ITEM.ITEM_SUBRTN);
            dt.Columns.Add(TBL_DSP_ITEM.BLANK_FLG);

            DataRow row = dt.NewRow();
            row[TBL_DSP_ITEM.GYM_ID] = m_GYM_ID;
            row[TBL_DSP_ITEM.DSP_ID] = m_DSP_ID;
            row[TBL_DSP_ITEM.ITEM_ID] = m_ITEM_ID;
            row[TBL_DSP_ITEM.ITEM_DISPNAME] = m_ITEM_DISPNAME;
            row[TBL_DSP_ITEM.ITEM_TYPE] = m_ITEM_TYPE;
            row[TBL_DSP_ITEM.ITEM_LEN] = m_ITEM_LEN;
            row[TBL_DSP_ITEM.POS] = m_POS;
            row[TBL_DSP_ITEM.DUP] = m_DUP;
            row[TBL_DSP_ITEM.AUTO_INPUT] = m_AUTO_INPUT;
            row[TBL_DSP_ITEM.ITEM_SUBRTN] = m_ITEM_SUBRTN;
            row[TBL_DSP_ITEM.BLANK_FLG] = m_BLANK_FLG;
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_DSP_ITEM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_DSP_ITEM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_DSP_ITEM.GYM_ID + "," +
                TBL_DSP_ITEM.DSP_ID + "," +
                TBL_DSP_ITEM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_DSP_ITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_DSP_ITEM.GYM_ID + "=" + gymid +
                " ORDER BY " +
                TBL_DSP_ITEM.DSP_ID + "," +
                TBL_DSP_ITEM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_DSP_ITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_DSP_ITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_DSP_ITEM.DSP_ID + "=" + dspid;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int itemid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_DSP_ITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_DSP_ITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_DSP_ITEM.DSP_ID + "=" + dspid + " AND " +
                TBL_DSP_ITEM.ITEM_ID + "=" + itemid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_DSP_ITEM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_DSP_ITEM.GYM_ID + "," +
                TBL_DSP_ITEM.DSP_ID + "," +
                TBL_DSP_ITEM.ITEM_ID + "," +
                TBL_DSP_ITEM.ITEM_DISPNAME + "," +
                TBL_DSP_ITEM.ITEM_TYPE + "," +
                TBL_DSP_ITEM.ITEM_LEN + "," +
                TBL_DSP_ITEM.POS + "," +
                TBL_DSP_ITEM.DUP + "," +
                TBL_DSP_ITEM.AUTO_INPUT + "," +
                TBL_DSP_ITEM.ITEM_SUBRTN + "," +
                TBL_DSP_ITEM.BLANK_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                m_ITEM_ID + "," +
                "'" + m_ITEM_DISPNAME + "'," +
                "'" + m_ITEM_TYPE + "'," +
                m_ITEM_LEN + "," +
                m_POS + "," +
                "'" + m_DUP + "'," +
                "'" + m_AUTO_INPUT + "'," +
                "'" + m_ITEM_SUBRTN + "'," +
                "'" + m_BLANK_FLG + "')";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery(string tableName)
        {
            string strSQL = "INSERT INTO " + tableName + " (" +
                TBL_DSP_ITEM.GYM_ID + "," +
                TBL_DSP_ITEM.DSP_ID + "," +
                TBL_DSP_ITEM.ITEM_ID + "," +
                TBL_DSP_ITEM.ITEM_DISPNAME + "," +
                TBL_DSP_ITEM.ITEM_TYPE + "," +
                TBL_DSP_ITEM.ITEM_LEN + "," +
                TBL_DSP_ITEM.POS + "," +
                TBL_DSP_ITEM.DUP + "," +
                TBL_DSP_ITEM.AUTO_INPUT + "," +
                TBL_DSP_ITEM.ITEM_SUBRTN + "," +
                TBL_DSP_ITEM.BLANK_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                m_ITEM_ID + "," +
                "'" + m_ITEM_DISPNAME + "'," +
                "'" + m_ITEM_TYPE + "'," +
                m_ITEM_LEN + "," +
                m_POS + "," +
                "'" + m_DUP + "'," +
                "'" + m_AUTO_INPUT + "'," +
                "'" + m_ITEM_SUBRTN + "'," +
                "'" + m_BLANK_FLG + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_DSP_ITEM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_DSP_ITEM.ITEM_DISPNAME + "='" + m_ITEM_DISPNAME + "', " +
                TBL_DSP_ITEM.ITEM_TYPE + "='" + m_ITEM_TYPE + "', " +
                TBL_DSP_ITEM.ITEM_LEN + "=" + m_ITEM_LEN + ", " +
                TBL_DSP_ITEM.POS + "=" + m_POS + ", " +
                TBL_DSP_ITEM.DUP + "='" + m_DUP + "', " +
                TBL_DSP_ITEM.AUTO_INPUT + "='" + m_AUTO_INPUT + "', " +
                TBL_DSP_ITEM.ITEM_SUBRTN + "='" + m_ITEM_SUBRTN + "', " +
                TBL_DSP_ITEM.BLANK_FLG + "='" + m_BLANK_FLG + "' " +
                " WHERE " +
                TBL_DSP_ITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_DSP_ITEM.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_DSP_ITEM.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_DSP_ITEM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_DSP_ITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_DSP_ITEM.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_DSP_ITEM.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        #endregion
    }
}
