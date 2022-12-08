using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// バッチイメージトランザクション
    /// </summary>
    public class TBL_TRBATCHIMG
    {
        public TBL_TRBATCHIMG(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TRBATCHIMG(int gymid, int operationdate, string scanterm, int batid, int imgkbn, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
            m_IMG_KBN = imgkbn;
        }

        public TBL_TRBATCHIMG(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "TRBATCHIMG";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string IMG_KBN = "IMG_KBN";
        public const string IMG_FLNM = "IMG_FLNM";
        public const string SCAN_BATCH_FOLDER_NAME = "SCAN_BATCH_FOLDER_NAME";
        public const string IMG_FLNM_OLD = "IMG_FLNM_OLD";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_IMG_KBN = 0;
        public string m_IMG_FLNM = "";
        public string m_SCAN_BATCH_FOLDER_NAME = "";
        public string m_IMG_FLNM_OLD = "";

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
        public int _IMG_KBN
        {
            get { return m_IMG_KBN; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRBATCHIMG.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCHIMG.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRBATCHIMG.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRBATCHIMG.BAT_ID]);
            m_IMG_KBN = DBConvert.ToIntNull(dr[TBL_TRBATCHIMG.IMG_KBN]);
            m_IMG_FLNM = DBConvert.ToStringNull(dr[TBL_TRBATCHIMG.IMG_FLNM]);
            m_SCAN_BATCH_FOLDER_NAME = DBConvert.ToStringNull(dr[TBL_TRBATCHIMG.SCAN_BATCH_FOLDER_NAME]);
            m_IMG_FLNM_OLD = DBConvert.ToStringNull(dr[TBL_TRBATCHIMG.IMG_FLNM_OLD]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRBATCHIMG.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRBATCHIMG.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_TRBATCHIMG.GYM_ID + "," +
                TBL_TRBATCHIMG.OPERATION_DATE + "," +
                TBL_TRBATCHIMG.SCAN_TERM + "," +
                TBL_TRBATCHIMG.BAT_ID + "," +
                TBL_TRBATCHIMG.IMG_KBN;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRBATCHIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRBATCHIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRBATCHIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRBATCHIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRBATCHIMG.BAT_ID + "=" + batid;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int imgkbn, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRBATCHIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRBATCHIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRBATCHIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRBATCHIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRBATCHIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRBATCHIMG.IMG_KBN + "=" + imgkbn;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_TRBATCHIMG.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRBATCHIMG.GYM_ID + "," +
                TBL_TRBATCHIMG.OPERATION_DATE + "," +
                TBL_TRBATCHIMG.SCAN_TERM + "," +
                TBL_TRBATCHIMG.BAT_ID + "," +
                TBL_TRBATCHIMG.IMG_KBN + "," +
                TBL_TRBATCHIMG.IMG_FLNM + "," +
                TBL_TRBATCHIMG.SCAN_BATCH_FOLDER_NAME + "," +
                TBL_TRBATCHIMG.IMG_FLNM_OLD + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_IMG_KBN + "," +
                "'" + m_IMG_FLNM + "'," +
                "'" + m_SCAN_BATCH_FOLDER_NAME + "'," +
                "'" + m_IMG_FLNM_OLD + "' )";
            return strSQL;
        }

        #endregion
    }
}
