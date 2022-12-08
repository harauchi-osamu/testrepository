using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 補正モード画面項目定義
    /// </summary>
    public class TBL_HOSEIMODE_DSP_ITEM
    {
        public TBL_HOSEIMODE_DSP_ITEM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_HOSEIMODE_DSP_ITEM(int gymid, int dspid, int hoseiitemmode, int itemid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_DSP_ID = dspid;
            m_HOSEI_ITEMMODE = hoseiitemmode;
            m_ITEM_ID = itemid;
        }

        public TBL_HOSEIMODE_DSP_ITEM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "HOSEIMODE_DSP_ITEM";

        public const string GYM_ID = "GYM_ID";
        public const string DSP_ID = "DSP_ID";
        public const string HOSEI_ITEMMODE = "HOSEI_ITEMMODE";
        public const string ITEM_ID = "ITEM_ID";
        public const string NAME_POS_TOP = "NAME_POS_TOP";
        public const string NAME_POS_LEFT = "NAME_POS_LEFT";
        public const string INPUT_POS_TOP = "INPUT_POS_TOP";
        public const string INPUT_POS_LEFT = "INPUT_POS_LEFT";
        public const string INPUT_WIDTH = "INPUT_WIDTH";
        public const string INPUT_HEIGHT = "INPUT_HEIGHT";
        public const string INPUT_SEQ = "INPUT_SEQ";

        public const string ALL_COLUMNS = " GYM_ID,DSP_ID,HOSEI_ITEMMODE,ITEM_ID,NAME_POS_TOP,NAME_POS_LEFT,INPUT_POS_TOP,INPUT_POS_LEFT,INPUT_WIDTH,INPUT_HEIGHT,INPUT_SEQ ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_DSP_ID = 0;
        private int m_HOSEI_ITEMMODE = 0;
        private int m_ITEM_ID = 0;
        public int m_NAME_POS_TOP = 0;
        public int m_NAME_POS_LEFT = 0;
        public int m_INPUT_POS_TOP = 0;
        public int m_INPUT_POS_LEFT = 0;
        public int m_INPUT_WIDTH = 0;
        public int m_INPUT_HEIGHT = 0;
        public int m_INPUT_SEQ = 0;

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
        public int _HOSEI_ITEMMODE
        {
            get { return m_HOSEI_ITEMMODE; }
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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.GYM_ID]);
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.DSP_ID]);
            m_HOSEI_ITEMMODE = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE]);
            m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.ITEM_ID]);
            m_NAME_POS_TOP = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.NAME_POS_TOP]);
            m_NAME_POS_LEFT = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.NAME_POS_LEFT]);
            m_INPUT_POS_TOP = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_TOP]);
            m_INPUT_POS_LEFT = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_LEFT]);
            m_INPUT_WIDTH = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.INPUT_WIDTH]);
            m_INPUT_HEIGHT = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.INPUT_HEIGHT]);
            m_INPUT_SEQ = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_DSP_ITEM.INPUT_SEQ]);
        }

        /// <summary>
        /// DataRow を取得する
        /// </summary>
        /// <returns></returns>
        public DataRow ToDataRow()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.GYM_ID);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.DSP_ID);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.ITEM_ID);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.NAME_POS_TOP);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.NAME_POS_LEFT);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_TOP);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_LEFT);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.INPUT_WIDTH);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.INPUT_HEIGHT);
            dt.Columns.Add(TBL_HOSEIMODE_DSP_ITEM.INPUT_SEQ);

            DataRow row = dt.NewRow();
            row[TBL_HOSEIMODE_DSP_ITEM.GYM_ID] = m_GYM_ID;
            row[TBL_HOSEIMODE_DSP_ITEM.DSP_ID] = m_DSP_ID;
            row[TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE] = m_HOSEI_ITEMMODE;
            row[TBL_HOSEIMODE_DSP_ITEM.ITEM_ID] = m_ITEM_ID;
            row[TBL_HOSEIMODE_DSP_ITEM.NAME_POS_TOP] = m_NAME_POS_TOP;
            row[TBL_HOSEIMODE_DSP_ITEM.NAME_POS_LEFT] = m_NAME_POS_LEFT;
            row[TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_TOP] = m_INPUT_POS_TOP;
            row[TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_LEFT] = m_INPUT_POS_LEFT;
            row[TBL_HOSEIMODE_DSP_ITEM.INPUT_WIDTH] = m_INPUT_WIDTH;
            row[TBL_HOSEIMODE_DSP_ITEM.INPUT_HEIGHT] = m_INPUT_HEIGHT;
            row[TBL_HOSEIMODE_DSP_ITEM.INPUT_SEQ] = m_INPUT_SEQ;
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_HOSEIMODE_DSP_ITEM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_HOSEIMODE_DSP_ITEM.GYM_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.DSP_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE + "," +
                TBL_HOSEIMODE_DSP_ITEM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEIMODE_DSP_ITEM.GYM_ID + "=" + gymid +
                " ORDER BY " +
                TBL_HOSEIMODE_DSP_ITEM.DSP_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE + "," +
                TBL_HOSEIMODE_DSP_ITEM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEIMODE_DSP_ITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.DSP_ID + "=" + dspid;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int hoseiitemmode, int itemid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEIMODE_DSP_ITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.DSP_ID + "=" + dspid + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE + "=" + hoseiitemmode + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.ITEM_ID + "=" + itemid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_HOSEIMODE_DSP_ITEM.GYM_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.DSP_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE + "," +
                TBL_HOSEIMODE_DSP_ITEM.ITEM_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.NAME_POS_TOP + "," +
                TBL_HOSEIMODE_DSP_ITEM.NAME_POS_LEFT + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_TOP + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_LEFT + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_WIDTH + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_HEIGHT + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_SEQ + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                m_HOSEI_ITEMMODE + "," +
                m_ITEM_ID + "," +
                m_NAME_POS_TOP + "," +
                m_NAME_POS_LEFT + "," +
                m_INPUT_POS_TOP + "," +
                m_INPUT_POS_LEFT + "," +
                m_INPUT_WIDTH + "," +
                m_INPUT_HEIGHT + "," +
                m_INPUT_SEQ + ")";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery(string tableName)
        {
            string strSQL = "INSERT INTO " + tableName + " (" +
                TBL_HOSEIMODE_DSP_ITEM.GYM_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.DSP_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE + "," +
                TBL_HOSEIMODE_DSP_ITEM.ITEM_ID + "," +
                TBL_HOSEIMODE_DSP_ITEM.NAME_POS_TOP + "," +
                TBL_HOSEIMODE_DSP_ITEM.NAME_POS_LEFT + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_TOP + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_LEFT + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_WIDTH + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_HEIGHT + "," +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_SEQ + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                m_HOSEI_ITEMMODE + "," +
                m_ITEM_ID + "," +
                m_NAME_POS_TOP + "," +
                m_NAME_POS_LEFT + "," +
                m_INPUT_POS_TOP + "," +
                m_INPUT_POS_LEFT + "," +
                m_INPUT_WIDTH + "," +
                m_INPUT_HEIGHT + "," +
                m_INPUT_SEQ + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_HOSEIMODE_DSP_ITEM.NAME_POS_TOP + "=" + m_NAME_POS_TOP + ", " +
                TBL_HOSEIMODE_DSP_ITEM.NAME_POS_LEFT + "=" + m_NAME_POS_LEFT + ", " +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_TOP + "=" + m_INPUT_POS_TOP + ", " +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_POS_LEFT + "=" + m_INPUT_POS_LEFT + ", " +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_WIDTH + "=" + m_INPUT_WIDTH + ", " +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_HEIGHT + "=" + m_INPUT_HEIGHT + ", " +
                TBL_HOSEIMODE_DSP_ITEM.INPUT_SEQ + "=" + m_INPUT_SEQ + " " +
                " WHERE " +
                TBL_HOSEIMODE_DSP_ITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE + "=" + m_HOSEI_ITEMMODE + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_HOSEIMODE_DSP_ITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.HOSEI_ITEMMODE + "=" + m_HOSEI_ITEMMODE + " AND " +
                TBL_HOSEIMODE_DSP_ITEM.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        #endregion
    }
}
