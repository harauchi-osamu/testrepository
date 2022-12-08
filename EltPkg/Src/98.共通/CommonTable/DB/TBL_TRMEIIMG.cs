using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 明細イメージトランザクション
    /// </summary>
    public class TBL_TRMEIIMG
    {
        public TBL_TRMEIIMG(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TRMEIIMG(int gymid, int operationdate, string scanterm, int batid, int detailsno, int imgkbn, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
            m_IMG_KBN = imgkbn;
        }

        public TBL_TRMEIIMG(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "TRMEIIMG";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string IMG_KBN = "IMG_KBN";
        public const string IMG_FLNM = "IMG_FLNM";
        public const string IMG_FLNM_OLD = "IMG_FLNM_OLD";
        public const string OC_OC_BK_NO = "OC_OC_BK_NO";
        public const string OC_OC_BR_NO = "OC_OC_BR_NO";
        public const string OC_IC_BK_NO = "OC_IC_BK_NO";
        public const string OC_OC_DATE = "OC_OC_DATE";
        public const string OC_CLEARING_DATE = "OC_CLEARING_DATE";
        public const string OC_AMOUNT = "OC_AMOUNT";
        public const string PAY_KBN = "PAY_KBN";
        public const string UNIQUE_CODE = "UNIQUE_CODE";
        public const string FILE_EXTENSION = "FILE_EXTENSION";
        public const string BUA_STS = "BUA_STS";
        public const string BUB_CONFIRMDATE = "BUB_CONFIRMDATE";
        public const string BUA_DATE = "BUA_DATE";
        public const string BUA_TIME = "BUA_TIME";
        public const string GDA_DATE = "GDA_DATE";
        public const string GDA_TIME = "GDA_TIME";
        public const string IMG_ARCH_NAME = "IMG_ARCH_NAME";
        public const string DELETE_DATE = "DELETE_DATE";
        public const string DELETE_FLG = "DELETE_FLG";

        public const string ALL_COLUMNS = " GYM_ID,OPERATION_DATE,SCAN_TERM,BAT_ID,DETAILS_NO,IMG_KBN,IMG_FLNM,IMG_FLNM_OLD,OC_OC_BK_NO,OC_OC_BR_NO,OC_IC_BK_NO,OC_OC_DATE,OC_CLEARING_DATE,OC_AMOUNT,PAY_KBN,UNIQUE_CODE,FILE_EXTENSION,BUA_STS,BUB_CONFIRMDATE,BUA_DATE,BUA_TIME,GDA_DATE,GDA_TIME,IMG_ARCH_NAME,DELETE_DATE,DELETE_FLG ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        private int m_IMG_KBN = 0;
        public string m_IMG_FLNM = "";
        public string m_IMG_FLNM_OLD = "";
        public string m_OC_OC_BK_NO = "";
        public string m_OC_OC_BR_NO = "";
        public string m_OC_IC_BK_NO = "";
        public string m_OC_OC_DATE = "";
        public string m_OC_CLEARING_DATE = "";
        public string m_OC_AMOUNT = "";
        public string m_PAY_KBN = "";
        public string m_UNIQUE_CODE = "";
        public string m_FILE_EXTENSION = "";
        public int m_BUA_STS = 0;
        public int m_BUB_CONFIRMDATE = 0;
        public int m_BUA_DATE = 0;
        public int m_BUA_TIME = 0;
        public int m_GDA_DATE = 0;
        public int m_GDA_TIME = 0;
        public string m_IMG_ARCH_NAME = "";
        public int m_DELETE_DATE = 0;
        public int m_DELETE_FLG = 0;

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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.DETAILS_NO]);
            m_IMG_KBN = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.IMG_KBN]);
            m_IMG_FLNM = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.IMG_FLNM]);
            m_IMG_FLNM_OLD = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.IMG_FLNM_OLD]);
            m_OC_OC_BK_NO = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.OC_OC_BK_NO]);
            m_OC_OC_BR_NO = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.OC_OC_BR_NO]);
            m_OC_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.OC_IC_BK_NO]);
            m_OC_OC_DATE = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.OC_OC_DATE]);
            m_OC_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.OC_CLEARING_DATE]);
            m_OC_AMOUNT = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.OC_AMOUNT]);
            m_PAY_KBN = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.PAY_KBN]);
            m_UNIQUE_CODE = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.UNIQUE_CODE]);
            m_FILE_EXTENSION = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.FILE_EXTENSION]);
            m_BUA_STS = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.BUA_STS]);
            m_BUB_CONFIRMDATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.BUB_CONFIRMDATE]);
            m_BUA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.BUA_DATE]);
            m_BUA_TIME = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.BUA_TIME]);
            m_GDA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.GDA_DATE]);
            m_GDA_TIME = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.GDA_TIME]);
            m_IMG_ARCH_NAME = DBConvert.ToStringNull(dr[TBL_TRMEIIMG.IMG_ARCH_NAME]);
            m_DELETE_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.DELETE_DATE]);
            m_DELETE_FLG = DBConvert.ToIntNull(dr[TBL_TRMEIIMG.DELETE_FLG]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRMEIIMG.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_TRMEIIMG.GYM_ID + "," +
                TBL_TRMEIIMG.OPERATION_DATE + "," +
                TBL_TRMEIIMG.SCAN_TERM + "," +
                TBL_TRMEIIMG.BAT_ID + "," +
                TBL_TRMEIIMG.DETAILS_NO + "," +
                TBL_TRMEIIMG.IMG_KBN;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryNotDel(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " AND " +
                TBL_TRMEIIMG.DELETE_FLG + "= 0";
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int imgkbn, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " AND " +
                TBL_TRMEIIMG.IMG_KBN + "=" + imgkbn;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryNotDel(int gymid, int operationdate, string scanterm, int batid, int detailsno, int imgkbn, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " AND " +
                TBL_TRMEIIMG.IMG_KBN + "=" + imgkbn + " AND " +
                TBL_TRMEIIMG.DELETE_FLG + "= 0";
            return strSQL;
        }

        /// <summary>
        /// 業務・バッチ番号・イメージファイル名を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryImgFile(int gymid, int batid, string imgflnm, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.IMG_FLNM + "='" + imgflnm + "' ";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_TRMEIIMG.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRMEIIMG.GYM_ID + "," +
                TBL_TRMEIIMG.OPERATION_DATE + "," +
                TBL_TRMEIIMG.SCAN_TERM + "," +
                TBL_TRMEIIMG.BAT_ID + "," +
                TBL_TRMEIIMG.DETAILS_NO + "," +
                TBL_TRMEIIMG.IMG_KBN + "," +
                TBL_TRMEIIMG.IMG_FLNM + "," +
                TBL_TRMEIIMG.IMG_FLNM_OLD + "," +
                TBL_TRMEIIMG.OC_OC_BK_NO + "," +
                TBL_TRMEIIMG.OC_OC_BR_NO + "," +
                TBL_TRMEIIMG.OC_IC_BK_NO + "," +
                TBL_TRMEIIMG.OC_OC_DATE + "," +
                TBL_TRMEIIMG.OC_CLEARING_DATE + "," +
                TBL_TRMEIIMG.OC_AMOUNT + "," +
                TBL_TRMEIIMG.PAY_KBN + "," +
                TBL_TRMEIIMG.UNIQUE_CODE + "," +
                TBL_TRMEIIMG.FILE_EXTENSION + "," +
                TBL_TRMEIIMG.BUA_STS + "," +
                TBL_TRMEIIMG.BUB_CONFIRMDATE + "," +
                TBL_TRMEIIMG.BUA_DATE + "," +
                TBL_TRMEIIMG.BUA_TIME + "," +
                TBL_TRMEIIMG.GDA_DATE + "," +
                TBL_TRMEIIMG.GDA_TIME + "," +
                TBL_TRMEIIMG.IMG_ARCH_NAME + "," +
                TBL_TRMEIIMG.DELETE_DATE + "," +
                TBL_TRMEIIMG.DELETE_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_IMG_KBN + "," +
                "'" + m_IMG_FLNM + "'," +
                "'" + m_IMG_FLNM_OLD + "'," +
                "'" + m_OC_OC_BK_NO + "'," +
                "'" + m_OC_OC_BR_NO + "'," +
                "'" + m_OC_IC_BK_NO + "'," +
                "'" + m_OC_OC_DATE + "'," +
                "'" + m_OC_CLEARING_DATE + "'," +
                "'" + m_OC_AMOUNT + "'," +
                "'" + m_PAY_KBN + "'," +
                "'" + m_UNIQUE_CODE + "'," +
                "'" + m_FILE_EXTENSION + "'," +
                m_BUA_STS + "," +
                m_BUB_CONFIRMDATE + "," +
                m_BUA_DATE + "," +
                m_BUA_TIME + "," +
                m_GDA_DATE + "," +
                m_GDA_TIME + "," +
                "'" + m_IMG_ARCH_NAME + "'," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + ")";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// (イメージ取込処理)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryImageImport()
        {
            string strSQL = "INSERT INTO " + TBL_TRMEIIMG.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRMEIIMG.GYM_ID + "," +
                TBL_TRMEIIMG.OPERATION_DATE + "," +
                TBL_TRMEIIMG.SCAN_TERM + "," +
                TBL_TRMEIIMG.BAT_ID + "," +
                TBL_TRMEIIMG.DETAILS_NO + "," +
                TBL_TRMEIIMG.IMG_KBN + "," +
                TBL_TRMEIIMG.IMG_FLNM + "," +
                TBL_TRMEIIMG.IMG_FLNM_OLD + "," +
                TBL_TRMEIIMG.OC_OC_BK_NO + "," +
                TBL_TRMEIIMG.OC_OC_BR_NO + "," +
                TBL_TRMEIIMG.OC_IC_BK_NO + "," +
                TBL_TRMEIIMG.OC_OC_DATE + "," +
                TBL_TRMEIIMG.OC_CLEARING_DATE + "," +
                TBL_TRMEIIMG.OC_AMOUNT + "," +
                TBL_TRMEIIMG.PAY_KBN + "," +
                TBL_TRMEIIMG.UNIQUE_CODE + "," +
                TBL_TRMEIIMG.FILE_EXTENSION + "," +
                TBL_TRMEIIMG.BUA_STS + "," +
                TBL_TRMEIIMG.BUB_CONFIRMDATE + "," +
                TBL_TRMEIIMG.BUA_DATE + "," +
                TBL_TRMEIIMG.BUA_TIME + "," +
                TBL_TRMEIIMG.DELETE_DATE + "," +
                TBL_TRMEIIMG.DELETE_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_IMG_KBN + "," +
                "'" + m_IMG_FLNM + "'," +
                "'" + m_IMG_FLNM_OLD + "'," +
                "'" + m_OC_OC_BK_NO + "'," +
                "'" + m_OC_OC_BR_NO + "'," +
                "'" + m_OC_IC_BK_NO + "'," +
                "'" + m_OC_OC_DATE + "'," +
                "'" + m_OC_CLEARING_DATE + "'," +
                "'" + m_OC_AMOUNT + "'," +
                "'" + m_PAY_KBN + "'," +
                "'" + m_UNIQUE_CODE + "'," +
                "'" + m_FILE_EXTENSION + "'," +
                m_BUA_STS + "," +
                m_BUB_CONFIRMDATE + "," +
                m_BUA_DATE + "," +
                m_BUA_TIME + "," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + ")";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// (持帰確定処理)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryFileImport()
        {
            string strSQL = "INSERT INTO " + TBL_TRMEIIMG.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRMEIIMG.GYM_ID + "," +
                TBL_TRMEIIMG.OPERATION_DATE + "," +
                TBL_TRMEIIMG.SCAN_TERM + "," +
                TBL_TRMEIIMG.BAT_ID + "," +
                TBL_TRMEIIMG.DETAILS_NO + "," +
                TBL_TRMEIIMG.IMG_KBN + "," +
                TBL_TRMEIIMG.IMG_FLNM + "," +
                TBL_TRMEIIMG.OC_OC_BK_NO + "," +
                TBL_TRMEIIMG.OC_OC_BR_NO + "," +
                TBL_TRMEIIMG.OC_IC_BK_NO + "," +
                TBL_TRMEIIMG.OC_OC_DATE + "," +
                TBL_TRMEIIMG.OC_CLEARING_DATE + "," +
                TBL_TRMEIIMG.OC_AMOUNT + "," +
                TBL_TRMEIIMG.PAY_KBN + "," +
                TBL_TRMEIIMG.UNIQUE_CODE + "," +
                TBL_TRMEIIMG.FILE_EXTENSION + "," +
                TBL_TRMEIIMG.GDA_DATE + "," +
                TBL_TRMEIIMG.GDA_TIME + "," +
                TBL_TRMEIIMG.IMG_ARCH_NAME + "," +
                TBL_TRMEIIMG.DELETE_DATE + "," +
                TBL_TRMEIIMG.DELETE_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_IMG_KBN + "," +
                "'" + m_IMG_FLNM + "'," +
                "'" + m_OC_OC_BK_NO + "'," +
                "'" + m_OC_OC_BR_NO + "'," +
                "'" + m_OC_IC_BK_NO + "'," +
                "'" + m_OC_OC_DATE + "'," +
                "'" + m_OC_CLEARING_DATE + "'," +
                "'" + m_OC_AMOUNT + "'," +
                "'" + m_PAY_KBN + "'," +
                "'" + m_UNIQUE_CODE + "'," +
                "'" + m_FILE_EXTENSION + "'," +
                m_GDA_DATE + "," +
                m_GDA_TIME + "," +
                "'" + m_IMG_ARCH_NAME + "'," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_TRMEIIMG.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRMEIIMG.IMG_FLNM + "='" + m_IMG_FLNM + "', " +
                TBL_TRMEIIMG.IMG_FLNM_OLD + "='" + m_IMG_FLNM_OLD + "', " +
                TBL_TRMEIIMG.OC_OC_BK_NO + "='" + m_OC_OC_BK_NO + "', " +
                TBL_TRMEIIMG.OC_OC_BR_NO + "='" + m_OC_OC_BR_NO + "', " +
                TBL_TRMEIIMG.OC_IC_BK_NO + "='" + m_OC_IC_BK_NO + "', " +
                TBL_TRMEIIMG.OC_OC_DATE + "='" + m_OC_OC_DATE + "', " +
                TBL_TRMEIIMG.OC_CLEARING_DATE + "='" + m_OC_CLEARING_DATE + "', " +
                TBL_TRMEIIMG.OC_AMOUNT + "='" + m_OC_AMOUNT + "', " +
                TBL_TRMEIIMG.PAY_KBN + "='" + m_PAY_KBN + "', " +
                TBL_TRMEIIMG.UNIQUE_CODE + "='" + m_UNIQUE_CODE + "', " +
                TBL_TRMEIIMG.FILE_EXTENSION + "='" + m_FILE_EXTENSION + "', " +
                TBL_TRMEIIMG.BUA_STS + "=" + m_BUA_STS + ", " +
                TBL_TRMEIIMG.BUB_CONFIRMDATE + "=" + m_BUB_CONFIRMDATE + ", " +
                TBL_TRMEIIMG.BUA_DATE + "=" + m_BUA_DATE + ", " +
                TBL_TRMEIIMG.BUA_TIME + "=" + m_BUA_TIME + ", " +
                TBL_TRMEIIMG.GDA_DATE + "=" + m_GDA_DATE + ", " +
                TBL_TRMEIIMG.GDA_TIME + "=" + m_GDA_TIME + ", " +
                TBL_TRMEIIMG.IMG_ARCH_NAME + "='" + m_IMG_ARCH_NAME + "', " +
                TBL_TRMEIIMG.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRMEIIMG.DELETE_FLG + "=" + m_DELETE_FLG + " " +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
                TBL_TRMEIIMG.IMG_KBN + "=" + m_IMG_KBN;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_TRMEIIMG.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
                TBL_TRMEIIMG.IMG_KBN + "=" + m_IMG_KBN;
            return strSQL;
        }

        /// <summary>
        /// 明細単位でのDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryDetails(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " ";
            return strSQL;
        }

        /// <summary>
        /// 業務ID・業務日付・バッチ番号・明細番号を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryBatIDDetailNo(int gymid, int operationdate, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " ";
            return strSQL;
        }

        /// <summary>
        /// 業務ID・業務日付・バッチ番号・明細番号・区分を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryBatIDDetailNoKbn(int gymid, int operationdate, int batid, int detailsno, int imgkbn, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " AND " +
                TBL_TRMEIIMG.IMG_KBN + "=" + imgkbn + " ";
            return strSQL;
        }

        #endregion
    }
}
