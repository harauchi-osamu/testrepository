using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 項目トランザクション
    /// </summary>
    public class TBL_TRITEM
    {
        public TBL_TRITEM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TRITEM(int gymid, int operationdate, string scanterm, int batid, int detailsno, int itemid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
            m_ITEM_ID = itemid;
        }

        public TBL_TRITEM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "TRITEM";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string ITEM_ID = "ITEM_ID";
        public const string ITEM_NAME = "ITEM_NAME";
        public const string OCR_ENT_DATA = "OCR_ENT_DATA";
        public const string OCR_VFY_DATA = "OCR_VFY_DATA";
        public const string ENT_DATA = "ENT_DATA";
        public const string VFY_DATA = "VFY_DATA";
        public const string END_DATA = "END_DATA";
        public const string BUA_DATA = "BUA_DATA";
        public const string CTR_DATA = "CTR_DATA";
        public const string ICTEISEI_DATA = "ICTEISEI_DATA";
        public const string MRC_CHG_BEFDATA = "MRC_CHG_BEFDATA";
        public const string E_TERM = "E_TERM";
        public const string E_OPENO = "E_OPENO";
        public const string E_STIME = "E_STIME";
        public const string E_ETIME = "E_ETIME";
        public const string E_YMD = "E_YMD";
        public const string E_TIME = "E_TIME";
        public const string V_TERM = "V_TERM";
        public const string V_OPENO = "V_OPENO";
        public const string V_STIME = "V_STIME";
        public const string V_ETIME = "V_ETIME";
        public const string V_YMD = "V_YMD";
        public const string V_TIME = "V_TIME";
        public const string C_TERM = "C_TERM";
        public const string C_OPENO = "C_OPENO";
        public const string C_STIME = "C_STIME";
        public const string C_ETIME = "C_ETIME";
        public const string C_YMD = "C_YMD";
        public const string C_TIME = "C_TIME";
        public const string O_TERM = "O_TERM";
        public const string O_OPENO = "O_OPENO";
        public const string O_STIME = "O_STIME";
        public const string O_ETIME = "O_ETIME";
        public const string O_YMD = "O_YMD";
        public const string O_TIME = "O_TIME";
        public const string ITEM_TOP = "ITEM_TOP";
        public const string ITEM_LEFT = "ITEM_LEFT";
        public const string ITEM_WIDTH = "ITEM_WIDTH";
        public const string ITEM_HEIGHT = "ITEM_HEIGHT";
        public const string FIX_TRIGGER = "FIX_TRIGGER";

        public const string ALL_COLUMNS = " GYM_ID,OPERATION_DATE,SCAN_TERM,BAT_ID,DETAILS_NO,ITEM_ID,ITEM_NAME,OCR_ENT_DATA,OCR_VFY_DATA,ENT_DATA,VFY_DATA,END_DATA,BUA_DATA,CTR_DATA,ICTEISEI_DATA,MRC_CHG_BEFDATA,E_TERM,E_OPENO,E_STIME,E_ETIME,E_YMD,E_TIME,V_TERM,V_OPENO,V_STIME,V_ETIME,V_YMD,V_TIME,C_TERM,C_OPENO,C_STIME,C_ETIME,C_YMD,C_TIME,O_TERM,O_OPENO,O_STIME,O_ETIME,O_YMD,O_TIME,ITEM_TOP,ITEM_LEFT,ITEM_WIDTH,ITEM_HEIGHT,FIX_TRIGGER ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        private int m_ITEM_ID = 0;
        public string m_ITEM_NAME = "";
        public string m_OCR_ENT_DATA = "";
        public string m_OCR_VFY_DATA = "";
        public string m_ENT_DATA = "";
        public string m_VFY_DATA = "";
        public string m_END_DATA = "";
        public string m_BUA_DATA = "";
        public string m_CTR_DATA = "";
        public string m_ICTEISEI_DATA = "";
        public string m_MRC_CHG_BEFDATA = "";
        public string m_E_TERM = "";
        public string m_E_OPENO = "";
        public int m_E_STIME = 0;
        public int m_E_ETIME = 0;
        public int m_E_YMD = 0;
        public int m_E_TIME = 0;
        public string m_V_TERM = "";
        public string m_V_OPENO = "";
        public int m_V_STIME = 0;
        public int m_V_ETIME = 0;
        public int m_V_YMD = 0;
        public int m_V_TIME = 0;
        public string m_C_TERM = "";
        public string m_C_OPENO = "";
        public int m_C_STIME = 0;
        public int m_C_ETIME = 0;
        public int m_C_YMD = 0;
        public int m_C_TIME = 0;
        public string m_O_TERM = "";
        public string m_O_OPENO = "";
        public int m_O_STIME = 0;
        public int m_O_ETIME = 0;
        public int m_O_YMD = 0;
        public int m_O_TIME = 0;
        public long m_ITEM_TOP = -1;
        public long m_ITEM_LEFT = -1;
        public long m_ITEM_WIDTH = -1;
        public long m_ITEM_HEIGHT = -1;
        public string m_FIX_TRIGGER = "";

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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRITEM.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRITEM.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRITEM.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_TRITEM.DETAILS_NO]);
            m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_TRITEM.ITEM_ID]);
            m_ITEM_NAME = DBConvert.ToStringNull(dr[TBL_TRITEM.ITEM_NAME]);
            m_OCR_ENT_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM.OCR_ENT_DATA]);
            m_OCR_VFY_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM.OCR_VFY_DATA]);
            m_ENT_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM.ENT_DATA]);
            m_VFY_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM.VFY_DATA]);
            m_END_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM.END_DATA]);
            m_BUA_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM.BUA_DATA]);
            m_CTR_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM.CTR_DATA]);
            m_ICTEISEI_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM.ICTEISEI_DATA]);
            m_MRC_CHG_BEFDATA = DBConvert.ToStringNull(dr[TBL_TRITEM.MRC_CHG_BEFDATA]);
            m_E_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM.E_TERM]);
            m_E_OPENO = DBConvert.ToStringNull(dr[TBL_TRITEM.E_OPENO]);
            m_E_STIME = DBConvert.ToIntNull(dr[TBL_TRITEM.E_STIME]);
            m_E_ETIME = DBConvert.ToIntNull(dr[TBL_TRITEM.E_ETIME]);
            m_E_YMD = DBConvert.ToIntNull(dr[TBL_TRITEM.E_YMD]);
            m_E_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM.E_TIME]);
            m_V_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM.V_TERM]);
            m_V_OPENO = DBConvert.ToStringNull(dr[TBL_TRITEM.V_OPENO]);
            m_V_STIME = DBConvert.ToIntNull(dr[TBL_TRITEM.V_STIME]);
            m_V_ETIME = DBConvert.ToIntNull(dr[TBL_TRITEM.V_ETIME]);
            m_V_YMD = DBConvert.ToIntNull(dr[TBL_TRITEM.V_YMD]);
            m_V_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM.V_TIME]);
            m_C_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM.C_TERM]);
            m_C_OPENO = DBConvert.ToStringNull(dr[TBL_TRITEM.C_OPENO]);
            m_C_STIME = DBConvert.ToIntNull(dr[TBL_TRITEM.C_STIME]);
            m_C_ETIME = DBConvert.ToIntNull(dr[TBL_TRITEM.C_ETIME]);
            m_C_YMD = DBConvert.ToIntNull(dr[TBL_TRITEM.C_YMD]);
            m_C_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM.C_TIME]);
            m_O_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM.O_TERM]);
            m_O_OPENO = DBConvert.ToStringNull(dr[TBL_TRITEM.O_OPENO]);
            m_O_STIME = DBConvert.ToIntNull(dr[TBL_TRITEM.O_STIME]);
            m_O_ETIME = DBConvert.ToIntNull(dr[TBL_TRITEM.O_ETIME]);
            m_O_YMD = DBConvert.ToIntNull(dr[TBL_TRITEM.O_YMD]);
            m_O_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM.O_TIME]);
            m_ITEM_TOP = DBConvert.ToLongNull(dr[TBL_TRITEM.ITEM_TOP]);
            m_ITEM_LEFT = DBConvert.ToLongNull(dr[TBL_TRITEM.ITEM_LEFT]);
            m_ITEM_WIDTH = DBConvert.ToLongNull(dr[TBL_TRITEM.ITEM_WIDTH]);
            m_ITEM_HEIGHT = DBConvert.ToLongNull(dr[TBL_TRITEM.ITEM_HEIGHT]);
            m_FIX_TRIGGER = DBConvert.ToStringNull(dr[TBL_TRITEM.FIX_TRIGGER]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRITEM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_TRITEM.GYM_ID + "," +
                TBL_TRITEM.OPERATION_DATE + "," +
                TBL_TRITEM.SCAN_TERM + "," +
                TBL_TRITEM.BAT_ID + "," +
                TBL_TRITEM.DETAILS_NO + "," +
                TBL_TRITEM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_TRITEM.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRITEM.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRITEM.BAT_ID + "=" + batid + " " +
                " ORDER BY " +
                TBL_TRITEM.DETAILS_NO + "," +
                TBL_TRITEM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_TRITEM.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRITEM.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRITEM.BAT_ID + "=" + batid + " AND " +
                TBL_TRITEM.DETAILS_NO + "=" + detailsno + " " +
                " ORDER BY " +
                TBL_TRITEM.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int itemid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_TRITEM.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRITEM.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRITEM.BAT_ID + "=" + batid + " AND " +
                TBL_TRITEM.DETAILS_NO + "=" + detailsno + " AND " +
                TBL_TRITEM.ITEM_ID + "=" + itemid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_TRITEM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRITEM.GYM_ID + "," +
                TBL_TRITEM.OPERATION_DATE + "," +
                TBL_TRITEM.SCAN_TERM + "," +
                TBL_TRITEM.BAT_ID + "," +
                TBL_TRITEM.DETAILS_NO + "," +
                TBL_TRITEM.ITEM_ID + "," +
                TBL_TRITEM.ITEM_NAME + "," +
                TBL_TRITEM.OCR_ENT_DATA + "," +
                TBL_TRITEM.OCR_VFY_DATA + "," +
                TBL_TRITEM.ENT_DATA + "," +
                TBL_TRITEM.VFY_DATA + "," +
                TBL_TRITEM.END_DATA + "," +
                TBL_TRITEM.BUA_DATA + "," +
                TBL_TRITEM.CTR_DATA + "," +
                TBL_TRITEM.ICTEISEI_DATA + "," +
                TBL_TRITEM.MRC_CHG_BEFDATA + "," +
                TBL_TRITEM.E_TERM + "," +
                TBL_TRITEM.E_OPENO + "," +
                TBL_TRITEM.E_STIME + "," +
                TBL_TRITEM.E_ETIME + "," +
                TBL_TRITEM.E_YMD + "," +
                TBL_TRITEM.E_TIME + "," +
                TBL_TRITEM.V_TERM + "," +
                TBL_TRITEM.V_OPENO + "," +
                TBL_TRITEM.V_STIME + "," +
                TBL_TRITEM.V_ETIME + "," +
                TBL_TRITEM.V_YMD + "," +
                TBL_TRITEM.V_TIME + "," +
                TBL_TRITEM.C_TERM + "," +
                TBL_TRITEM.C_OPENO + "," +
                TBL_TRITEM.C_STIME + "," +
                TBL_TRITEM.C_ETIME + "," +
                TBL_TRITEM.C_YMD + "," +
                TBL_TRITEM.C_TIME + "," +
                TBL_TRITEM.O_TERM + "," +
                TBL_TRITEM.O_OPENO + "," +
                TBL_TRITEM.O_STIME + "," +
                TBL_TRITEM.O_ETIME + "," +
                TBL_TRITEM.O_YMD + "," +
                TBL_TRITEM.O_TIME + "," +
                TBL_TRITEM.ITEM_TOP + "," +
                TBL_TRITEM.ITEM_LEFT + "," +
                TBL_TRITEM.ITEM_WIDTH + "," +
                TBL_TRITEM.ITEM_HEIGHT + "," +
                TBL_TRITEM.FIX_TRIGGER + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_ITEM_ID + "," +
                "'" + m_ITEM_NAME + "'," +
                "'" + m_OCR_ENT_DATA + "'," +
                "'" + m_OCR_VFY_DATA + "'," +
                "'" + m_ENT_DATA + "'," +
                "'" + m_VFY_DATA + "'," +
                "'" + m_END_DATA + "'," +
                "'" + m_BUA_DATA + "'," +
                "'" + m_CTR_DATA + "'," +
                "'" + m_ICTEISEI_DATA + "'," +
                "'" + m_MRC_CHG_BEFDATA + "'," +
                "'" + m_E_TERM + "'," +
                "'" + m_E_OPENO + "'," +
                m_E_STIME + "," +
                m_E_ETIME + "," +
                m_E_YMD + "," +
                m_E_TIME + "," +
                "'" + m_V_TERM + "'," +
                "'" + m_V_OPENO + "'," +
                m_V_STIME + "," +
                m_V_ETIME + "," +
                m_V_YMD + "," +
                m_V_TIME + "," +
                "'" + m_C_TERM + "'," +
                "'" + m_C_OPENO + "'," +
                m_C_STIME + "," +
                m_C_ETIME + "," +
                m_C_YMD + "," +
                m_C_TIME + "," +
                "'" + m_O_TERM + "'," +
                "'" + m_O_OPENO + "'," +
                m_O_STIME + "," +
                m_O_ETIME + "," +
                m_O_YMD + "," +
                m_O_TIME + "," +
                m_ITEM_TOP + "," +
                m_ITEM_LEFT + "," +
                m_ITEM_WIDTH + "," +
                m_ITEM_HEIGHT + "," +
                "'" + m_FIX_TRIGGER + "')";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// (イメージ取込)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryImageImport1()
        {
            string strSQL = "INSERT INTO " + TBL_TRITEM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRITEM.GYM_ID + "," +
                TBL_TRITEM.OPERATION_DATE + "," +
                TBL_TRITEM.SCAN_TERM + "," +
                TBL_TRITEM.BAT_ID + "," +
                TBL_TRITEM.DETAILS_NO + "," +
                TBL_TRITEM.ITEM_ID + "," +
                TBL_TRITEM.ITEM_NAME + "," +
                TBL_TRITEM.OCR_ENT_DATA + "," +
                TBL_TRITEM.ITEM_TOP + "," +
                TBL_TRITEM.ITEM_LEFT + "," +
                TBL_TRITEM.ITEM_WIDTH + "," +
                TBL_TRITEM.ITEM_HEIGHT + "," +
                TBL_TRITEM.FIX_TRIGGER + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_ITEM_ID + "," +
                "'" + m_ITEM_NAME + "'," +
                "'" + m_OCR_ENT_DATA + "'," +
                m_ITEM_TOP + "," +
                m_ITEM_LEFT + "," +
                m_ITEM_WIDTH + "," +
                m_ITEM_HEIGHT + "," +
                "'" + m_FIX_TRIGGER + "')";
                return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// (イメージ取込)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryImageImport2()
        {
            string strSQL = "INSERT INTO " + TBL_TRITEM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRITEM.GYM_ID + "," +
                TBL_TRITEM.OPERATION_DATE + "," +
                TBL_TRITEM.SCAN_TERM + "," +
                TBL_TRITEM.BAT_ID + "," +
                TBL_TRITEM.DETAILS_NO + "," +
                TBL_TRITEM.ITEM_ID + "," +
                TBL_TRITEM.ITEM_NAME + "," +
                TBL_TRITEM.OCR_ENT_DATA + "," +
                TBL_TRITEM.FIX_TRIGGER + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_ITEM_ID + "," +
                "'" + m_ITEM_NAME + "'," +
                "'" + m_OCR_ENT_DATA + "'," +
                "'" + m_FIX_TRIGGER + "')";

            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// (イメージ取込)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryImageImport3()
        {
            string strSQL = "INSERT INTO " + TBL_TRITEM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRITEM.GYM_ID + "," +
                TBL_TRITEM.OPERATION_DATE + "," +
                TBL_TRITEM.SCAN_TERM + "," +
                TBL_TRITEM.BAT_ID + "," +
                TBL_TRITEM.DETAILS_NO + "," +
                TBL_TRITEM.ITEM_ID + "," +
                TBL_TRITEM.ITEM_NAME + "," +
                TBL_TRITEM.OCR_ENT_DATA + "," +
                TBL_TRITEM.ENT_DATA + "," +
                TBL_TRITEM.END_DATA + "," +
                TBL_TRITEM.ITEM_TOP + "," +
                TBL_TRITEM.ITEM_LEFT + "," +
                TBL_TRITEM.ITEM_WIDTH + "," +
                TBL_TRITEM.ITEM_HEIGHT + "," +
                TBL_TRITEM.FIX_TRIGGER + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_ITEM_ID + "," +
                "'" + m_ITEM_NAME + "'," +
                "'" + m_OCR_ENT_DATA + "'," +
                "'" + m_ENT_DATA + "'," +
                "'" + m_END_DATA + "'," +
                m_ITEM_TOP + "," +
                m_ITEM_LEFT + "," +
                m_ITEM_WIDTH + "," +
                m_ITEM_HEIGHT + "," +
                "'" + m_FIX_TRIGGER + "')";
            return strSQL;

        }

        /// <summary>
        /// INSERT文を作成します
        /// (持出明細取込)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryImageImport4()
        {
            string strSQL = "INSERT INTO " + TBL_TRITEM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRITEM.GYM_ID + "," +
                TBL_TRITEM.OPERATION_DATE + "," +
                TBL_TRITEM.SCAN_TERM + "," +
                TBL_TRITEM.BAT_ID + "," +
                TBL_TRITEM.DETAILS_NO + "," +
                TBL_TRITEM.ITEM_ID + "," +
                TBL_TRITEM.ITEM_NAME + "," +
                TBL_TRITEM.ENT_DATA + "," +
                TBL_TRITEM.END_DATA + "," +
                TBL_TRITEM.BUA_DATA + "," +
                TBL_TRITEM.CTR_DATA + "," +
                TBL_TRITEM.FIX_TRIGGER + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_ITEM_ID + "," +
                "'" + m_ITEM_NAME + "'," +
                "'" + m_ENT_DATA + "'," +
                "'" + m_END_DATA + "'," +
                "'" + m_BUA_DATA + "'," +
                "'" + m_CTR_DATA + "'," +
                "'" + m_FIX_TRIGGER + "')";
            return strSQL;

        }

        /// <summary>
        /// INSERT文を作成します
        /// (持帰ダウンロード確定)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryImageImport5()
        {
            string strSQL = "INSERT INTO " + TBL_TRITEM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRITEM.GYM_ID + "," +
                TBL_TRITEM.OPERATION_DATE + "," +
                TBL_TRITEM.SCAN_TERM + "," +
                TBL_TRITEM.BAT_ID + "," +
                TBL_TRITEM.DETAILS_NO + "," +
                TBL_TRITEM.ITEM_ID + "," +
                TBL_TRITEM.ITEM_NAME + "," +
                TBL_TRITEM.OCR_ENT_DATA + "," +
                TBL_TRITEM.OCR_VFY_DATA + "," +
                TBL_TRITEM.CTR_DATA + "," +
                TBL_TRITEM.FIX_TRIGGER + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_ITEM_ID + "," +
                "'" + m_ITEM_NAME + "'," +
                "'" + m_OCR_ENT_DATA + "'," +
                "'" + m_OCR_VFY_DATA + "'," +
                "'" + m_CTR_DATA + "'," +
                "'" + m_FIX_TRIGGER + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_TRITEM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRITEM.ITEM_NAME + "='" + m_ITEM_NAME + "', " +
                TBL_TRITEM.OCR_ENT_DATA + "='" + m_OCR_ENT_DATA + "', " +
                TBL_TRITEM.OCR_VFY_DATA + "='" + m_OCR_VFY_DATA + "', " +
                TBL_TRITEM.BUA_DATA + "='" + m_BUA_DATA + "', " +
                TBL_TRITEM.ENT_DATA + "='" + m_ENT_DATA + "', " +
                TBL_TRITEM.VFY_DATA + "='" + m_VFY_DATA + "', " +
                TBL_TRITEM.END_DATA + "='" + m_END_DATA + "', " +
                TBL_TRITEM.ICTEISEI_DATA + "='" + m_ICTEISEI_DATA + "', " +
                TBL_TRITEM.E_TERM + "='" + m_E_TERM + "', " +
                TBL_TRITEM.E_OPENO + "='" + m_E_OPENO + "', " +
                TBL_TRITEM.E_STIME + "=" + m_E_STIME + ", " +
                TBL_TRITEM.E_ETIME + "=" + m_E_ETIME + ", " +
                TBL_TRITEM.E_YMD + "=" + m_E_YMD + ", " +
                TBL_TRITEM.E_TIME + "=" + m_E_TIME + ", " +
                TBL_TRITEM.V_TERM + "='" + m_V_TERM + "', " +
                TBL_TRITEM.V_OPENO + "='" + m_V_OPENO + "', " +
                TBL_TRITEM.V_STIME + "=" + m_V_STIME + ", " +
                TBL_TRITEM.V_ETIME + "=" + m_V_ETIME + ", " +
                TBL_TRITEM.V_YMD + "=" + m_V_YMD + ", " +
                TBL_TRITEM.V_TIME + "=" + m_V_TIME + ", " +
                TBL_TRITEM.C_TERM + "='" + m_C_TERM + "', " +
                TBL_TRITEM.C_OPENO + "='" + m_C_OPENO + "', " +
                TBL_TRITEM.C_STIME + "=" + m_C_STIME + ", " +
                TBL_TRITEM.C_ETIME + "=" + m_C_ETIME + ", " +
                TBL_TRITEM.C_YMD + "=" + m_C_YMD + ", " +
                TBL_TRITEM.C_TIME + "=" + m_C_TIME + ", " +
                TBL_TRITEM.O_TERM + "='" + m_O_TERM + "', " +
                TBL_TRITEM.O_OPENO + "='" + m_O_OPENO + "', " +
                TBL_TRITEM.O_STIME + "=" + m_O_STIME + ", " +
                TBL_TRITEM.O_ETIME + "=" + m_O_ETIME + ", " +
                TBL_TRITEM.O_YMD + "=" + m_O_YMD + ", " +
                TBL_TRITEM.O_TIME + "=" + m_O_TIME + ", " +
                TBL_TRITEM.ITEM_TOP + "=" + m_ITEM_TOP + ", " +
                TBL_TRITEM.ITEM_LEFT + "=" + m_ITEM_LEFT + ", " +
                TBL_TRITEM.ITEM_WIDTH + "=" + m_ITEM_WIDTH + ", " +
                TBL_TRITEM.ITEM_HEIGHT + "=" + m_ITEM_HEIGHT + ", " +
                TBL_TRITEM.FIX_TRIGGER + "='" + m_FIX_TRIGGER + "' " +
                " WHERE " +
                TBL_TRITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRITEM.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRITEM.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRITEM.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRITEM.DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
                TBL_TRITEM.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_TRITEM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_TRITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRITEM.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRITEM.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRITEM.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRITEM.DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
                TBL_TRITEM.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// 明細単位でのDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryDetails(int gymid, int operationdate, string scanterm, long batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_TRITEM.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRITEM.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRITEM.BAT_ID + "=" + batid + " AND " +
                TBL_TRITEM.DETAILS_NO + "=" + detailsno + " ";
            return strSQL;
        }

        /// <summary>
        /// 業務ID・業務日付・バッチ番号・明細番号を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryBatIDDetailNo(int gymid, int operationdate, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRITEM.GYM_ID + "=" + gymid + " AND " +
                TBL_TRITEM.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRITEM.BAT_ID + "=" + batid + " AND " +
                TBL_TRITEM.DETAILS_NO + "=" + detailsno + " ";
            return strSQL;
        }

        #endregion
    }
}
