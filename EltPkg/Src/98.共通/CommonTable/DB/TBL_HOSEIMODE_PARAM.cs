using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 補正モードパラメータ
    /// </summary>
    public class TBL_HOSEIMODE_PARAM
    {
        public TBL_HOSEIMODE_PARAM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_HOSEIMODE_PARAM(int gymid, int dspid, int hoseiitemmode, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_DSP_ID = dspid;
            m_HOSEI_ITEMMODE = hoseiitemmode;
        }

        public TBL_HOSEIMODE_PARAM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "HOSEIMODE_PARAM";

        public const string GYM_ID = "GYM_ID";
        public const string DSP_ID = "DSP_ID";
        public const string HOSEI_ITEMMODE = "HOSEI_ITEMMODE";
        public const string AUTO_SKIP_MODE_ENT = "AUTO_SKIP_MODE_ENT";
        public const string AUTO_SKIP_MODE_VFY = "AUTO_SKIP_MODE_VFY";
        public const string VERY_MODE = "VERY_MODE";

        public const string ALL_COLUMNS = " GYM_ID,DSP_ID,HOSEI_ITEMMODE,AUTO_SKIP_MODE_ENT,AUTO_SKIP_MODE_VFY,VERY_MODE ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_DSP_ID = 0;
        private int m_HOSEI_ITEMMODE = 0;
        public int m_AUTO_SKIP_MODE_ENT = 0;
        public int m_AUTO_SKIP_MODE_VFY = 0;
        public int m_VERY_MODE = 0;

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
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_PARAM.GYM_ID]);
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_PARAM.DSP_ID]);
            m_HOSEI_ITEMMODE = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_PARAM.HOSEI_ITEMMODE]);
            m_AUTO_SKIP_MODE_ENT = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_PARAM.AUTO_SKIP_MODE_ENT]);
            m_AUTO_SKIP_MODE_VFY = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_PARAM.AUTO_SKIP_MODE_VFY]);
            m_VERY_MODE = DBConvert.ToIntNull(dr[TBL_HOSEIMODE_PARAM.VERY_MODE]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_HOSEIMODE_PARAM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEIMODE_PARAM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_HOSEIMODE_PARAM.GYM_ID + "," +
                TBL_HOSEIMODE_PARAM.DSP_ID + "," +
                TBL_HOSEIMODE_PARAM.HOSEI_ITEMMODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEIMODE_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEIMODE_PARAM.GYM_ID + "=" + gymid +
                " ORDER BY " +
                TBL_HOSEIMODE_PARAM.DSP_ID + "," +
                TBL_HOSEIMODE_PARAM.HOSEI_ITEMMODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int dspid, int hoseiitemmode, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEIMODE_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEIMODE_PARAM.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEIMODE_PARAM.DSP_ID + "=" + dspid + " AND " +
                TBL_HOSEIMODE_PARAM.HOSEI_ITEMMODE + "=" + hoseiitemmode;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_HOSEIMODE_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_HOSEIMODE_PARAM.GYM_ID + "," +
                TBL_HOSEIMODE_PARAM.DSP_ID + "," +
                TBL_HOSEIMODE_PARAM.HOSEI_ITEMMODE + "," +
                TBL_HOSEIMODE_PARAM.AUTO_SKIP_MODE_ENT + "," +
                TBL_HOSEIMODE_PARAM.AUTO_SKIP_MODE_VFY + "," +
                TBL_HOSEIMODE_PARAM.VERY_MODE + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                m_HOSEI_ITEMMODE + "," +
                m_AUTO_SKIP_MODE_ENT + "," +
                m_AUTO_SKIP_MODE_VFY + "," +
                m_VERY_MODE + ")";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery(string tableName)
        {
            string strSQL = "INSERT INTO " + tableName + " (" +
                TBL_HOSEIMODE_PARAM.GYM_ID + "," +
                TBL_HOSEIMODE_PARAM.DSP_ID + "," +
                TBL_HOSEIMODE_PARAM.HOSEI_ITEMMODE + "," +
                TBL_HOSEIMODE_PARAM.AUTO_SKIP_MODE_ENT + "," +
                TBL_HOSEIMODE_PARAM.AUTO_SKIP_MODE_VFY + "," +
                TBL_HOSEIMODE_PARAM.VERY_MODE + ") VALUES (" +
                m_GYM_ID + "," +
                m_DSP_ID + "," +
                m_HOSEI_ITEMMODE + "," +
                m_AUTO_SKIP_MODE_ENT + "," +
                m_AUTO_SKIP_MODE_VFY + "," +
                m_VERY_MODE + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_HOSEIMODE_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_HOSEIMODE_PARAM.AUTO_SKIP_MODE_ENT + "=" + m_AUTO_SKIP_MODE_ENT + ", " +
                TBL_HOSEIMODE_PARAM.AUTO_SKIP_MODE_VFY + "=" + m_AUTO_SKIP_MODE_VFY + ", " +
                TBL_HOSEIMODE_PARAM.VERY_MODE + "=" + m_VERY_MODE + " " +
                " WHERE " +
                TBL_HOSEIMODE_PARAM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_HOSEIMODE_PARAM.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_HOSEIMODE_PARAM.HOSEI_ITEMMODE + "=" + m_HOSEI_ITEMMODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_HOSEIMODE_PARAM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_HOSEIMODE_PARAM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_HOSEIMODE_PARAM.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_HOSEIMODE_PARAM.HOSEI_ITEMMODE + "=" + m_HOSEI_ITEMMODE;
            return strSQL;
        }

        #endregion
    }
}
