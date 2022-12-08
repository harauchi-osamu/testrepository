using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 不渡明細トランザクション
    /// </summary>
    public class TBL_TRFUWATARI
    {
        public TBL_TRFUWATARI(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TRFUWATARI(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
        }

        public TBL_TRFUWATARI(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "TRFUWATARI";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string FUBI_KBN_01 = "FUBI_KBN_01";
        public const string ZERO_FUBINO_01 = "ZERO_FUBINO_01";
        public const string FUBI_KBN_02 = "FUBI_KBN_02";
        public const string ZERO_FUBINO_02 = "ZERO_FUBINO_02";
        public const string FUBI_KBN_03 = "FUBI_KBN_03";
        public const string ZERO_FUBINO_03 = "ZERO_FUBINO_03";
        public const string FUBI_KBN_04 = "FUBI_KBN_04";
        public const string ZERO_FUBINO_04 = "ZERO_FUBINO_04";
        public const string FUBI_KBN_05 = "FUBI_KBN_05";
        public const string ZERO_FUBINO_05 = "ZERO_FUBINO_05";
        public const string DELETE_DATE = "DELETE_DATE";
        public const string DELETE_FLG = "DELETE_FLG";
        public const string E_TERM = "E_TERM";
        public const string E_OPENO = "E_OPENO";
        public const string E_YMD = "E_YMD";
        public const string E_TIME = "E_TIME";

        public const string ALL_COLUMNS = " GYM_ID,OPERATION_DATE,SCAN_TERM,BAT_ID,DETAILS_NO,FUBI_KBN_01,ZERO_FUBINO_01,FUBI_KBN_02,ZERO_FUBINO_02,FUBI_KBN_03,ZERO_FUBINO_03,FUBI_KBN_04,ZERO_FUBINO_04,FUBI_KBN_05,ZERO_FUBINO_05,DELETE_DATE,DELETE_FLG,E_TERM,E_OPENO,E_YMD,E_TIME ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        public int m_FUBI_KBN_01 = -1;
        public int m_ZERO_FUBINO_01 = -1;
        public int m_FUBI_KBN_02 = -1;
        public int m_ZERO_FUBINO_02 = -1;
        public int m_FUBI_KBN_03 = -1;
        public int m_ZERO_FUBINO_03 = -1;
        public int m_FUBI_KBN_04 = -1;
        public int m_ZERO_FUBINO_04 = -1;
        public int m_FUBI_KBN_05 = -1;
        public int m_ZERO_FUBINO_05 = -1;
        public int m_DELETE_DATE = 0;
        public int m_DELETE_FLG = 0;
        public string m_E_TERM = "";
        public string m_E_OPENO = "";
        public int m_E_YMD = 0;
        public long m_E_TIME = 0;

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
        public string _SCAN_TERM
        {
            get { return m_SCAN_TERM; }
        }
        public int _BAT_ID
        {
            get { return m_BAT_ID; }
        }
        public int _DETAILS_NO
        {
            get { return m_DETAILS_NO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRFUWATARI.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.DETAILS_NO]);
            m_FUBI_KBN_01 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.FUBI_KBN_01]);
            m_ZERO_FUBINO_01 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.ZERO_FUBINO_01]);
            m_FUBI_KBN_02 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.FUBI_KBN_02]);
            m_ZERO_FUBINO_02 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.ZERO_FUBINO_02]);
            m_FUBI_KBN_03 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.FUBI_KBN_03]);
            m_ZERO_FUBINO_03 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.ZERO_FUBINO_03]);
            m_FUBI_KBN_04 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.FUBI_KBN_04]);
            m_ZERO_FUBINO_04 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.ZERO_FUBINO_04]);
            m_FUBI_KBN_05 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.FUBI_KBN_05]);
            m_ZERO_FUBINO_05 = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.ZERO_FUBINO_05]);
            m_DELETE_DATE = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.DELETE_DATE]);
            m_DELETE_FLG = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.DELETE_FLG]);
            m_E_TERM = DBConvert.ToStringNull(dr[TBL_TRFUWATARI.E_TERM]);
            m_E_OPENO = DBConvert.ToStringNull(dr[TBL_TRFUWATARI.E_OPENO]);
            m_E_YMD = DBConvert.ToIntNull(dr[TBL_TRFUWATARI.E_YMD]);
            m_E_TIME = DBConvert.ToLongNull(dr[TBL_TRFUWATARI.E_TIME]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRFUWATARI.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_TRFUWATARI.GYM_ID + "," +
                TBL_TRFUWATARI.OPERATION_DATE + "," +
                TBL_TRFUWATARI.SCAN_TERM + "," +
                TBL_TRFUWATARI.BAT_ID + "," +
                TBL_TRFUWATARI.DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRFUWATARI.GYM_ID + "=" + gymid + " AND " +
                TBL_TRFUWATARI.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRFUWATARI.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRFUWATARI.BAT_ID + "=" + batid + " AND " +
                TBL_TRFUWATARI.DETAILS_NO + "=" + detailsno;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_TRFUWATARI.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRFUWATARI.GYM_ID + "," +
                TBL_TRFUWATARI.OPERATION_DATE + "," +
                TBL_TRFUWATARI.SCAN_TERM + "," +
                TBL_TRFUWATARI.BAT_ID + "," +
                TBL_TRFUWATARI.DETAILS_NO + "," +
                TBL_TRFUWATARI.FUBI_KBN_01 + "," +
                TBL_TRFUWATARI.ZERO_FUBINO_01 + "," +
                TBL_TRFUWATARI.FUBI_KBN_02 + "," +
                TBL_TRFUWATARI.ZERO_FUBINO_02 + "," +
                TBL_TRFUWATARI.FUBI_KBN_03 + "," +
                TBL_TRFUWATARI.ZERO_FUBINO_03 + "," +
                TBL_TRFUWATARI.FUBI_KBN_04 + "," +
                TBL_TRFUWATARI.ZERO_FUBINO_04 + "," +
                TBL_TRFUWATARI.FUBI_KBN_05 + "," +
                TBL_TRFUWATARI.ZERO_FUBINO_05 + "," +
                TBL_TRFUWATARI.DELETE_DATE + "," +
                TBL_TRFUWATARI.DELETE_FLG + "," +
                TBL_TRFUWATARI.E_TERM + "," +
                TBL_TRFUWATARI.E_OPENO + "," +
                TBL_TRFUWATARI.E_YMD + "," +
                TBL_TRFUWATARI.E_TIME + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_FUBI_KBN_01 + "," +
                m_ZERO_FUBINO_01 + "," +
                m_FUBI_KBN_02 + "," +
                m_ZERO_FUBINO_02 + "," +
                m_FUBI_KBN_03 + "," +
                m_ZERO_FUBINO_03 + "," +
                m_FUBI_KBN_04 + "," +
                m_ZERO_FUBINO_04 + "," +
                m_FUBI_KBN_05 + "," +
                m_ZERO_FUBINO_05 + "," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + "," +
                "'" + m_E_TERM + "'," +
                "'" + m_E_OPENO + "'," +
                m_E_YMD + "," +
                m_E_TIME + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_TRFUWATARI.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRFUWATARI.FUBI_KBN_01 + "=" + m_FUBI_KBN_01 + ", " +
                TBL_TRFUWATARI.ZERO_FUBINO_01 + "=" + m_ZERO_FUBINO_01 + ", " +
                TBL_TRFUWATARI.FUBI_KBN_02 + "=" + m_FUBI_KBN_02 + ", " +
                TBL_TRFUWATARI.ZERO_FUBINO_02 + "=" + m_ZERO_FUBINO_02 + ", " +
                TBL_TRFUWATARI.FUBI_KBN_03 + "=" + m_FUBI_KBN_03 + ", " +
                TBL_TRFUWATARI.ZERO_FUBINO_03 + "=" + m_ZERO_FUBINO_03 + ", " +
                TBL_TRFUWATARI.FUBI_KBN_04 + "=" + m_FUBI_KBN_04 + ", " +
                TBL_TRFUWATARI.ZERO_FUBINO_04 + "=" + m_ZERO_FUBINO_04 + ", " +
                TBL_TRFUWATARI.FUBI_KBN_05 + "=" + m_FUBI_KBN_05 + ", " +
                TBL_TRFUWATARI.ZERO_FUBINO_05 + "=" + m_ZERO_FUBINO_05 + ", " +
                TBL_TRFUWATARI.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRFUWATARI.DELETE_FLG + "=" + m_DELETE_FLG + ", " +
                TBL_TRFUWATARI.E_TERM + "='" + m_E_TERM + "', " +
                TBL_TRFUWATARI.E_OPENO + "='" + m_E_OPENO + "', " +
                TBL_TRFUWATARI.E_YMD + "=" + m_E_YMD + ", " +
                TBL_TRFUWATARI.E_TIME + "=" + m_E_TIME + " " +
                " WHERE " +
                TBL_TRFUWATARI.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRFUWATARI.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRFUWATARI.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRFUWATARI.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRFUWATARI.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_TRFUWATARI.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_TRFUWATARI.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRFUWATARI.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRFUWATARI.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRFUWATARI.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRFUWATARI.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSQL;
        }

        #endregion
    }
}
