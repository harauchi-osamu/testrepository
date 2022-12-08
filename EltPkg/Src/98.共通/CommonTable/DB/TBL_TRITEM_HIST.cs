using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 汎用エントリオペレータ処理状況
    /// </summary>
    public class TBL_TRITEM_HIST
    {
        public TBL_TRITEM_HIST(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TRITEM_HIST(int gym_id, int operation_date, string scan_term, int details_no, int item_id, int bat_id, int seq, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_GYM_ID = gym_id;
            m_OPERATION_DATE = operation_date;
            m_SCAN_TERM = scan_term;
            m_BAT_ID = bat_id;
            m_DETAILS_NO = details_no;
            m_ITEM_ID = operation_date;
            m_SEQ = seq;
        }

        public TBL_TRITEM_HIST(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }      

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "TRITEM_HIST";
        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string ITEM_ID = "ITEM_ID";
        public const string SEQ = "SEQ";
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
        public const string UPDATE_DATE = "UPDATE_DATE";
        public const string UPDATE_TIME = "UPDATE_TIME";
        public const string UPDATE_KBN = "UPDATE_KBN";
        public const string FIX_TRIGGER = "FIX_TRIGGER";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        private int m_ITEM_ID = 0;
        private int m_SEQ = 0;
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
        public decimal m_ITEM_TOP = 0;
        public decimal m_ITEM_LEFT = 0;
        public decimal m_ITEM_WIDTH = 0;
        public decimal m_ITEM_HEIGHT = 0;
        public int m_UPDATE_DATE = 0;
        public int m_UPDATE_TIME = 0;
        public int m_UPDATE_KBN = 0;
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
        public int _BAT_ID
        {
            get { return m_BAT_ID; }
        }
        public string _SCAN_TERM
        {
            get { return m_SCAN_TERM; }
        }
        public int _DETAILS_NO
        {
            get { return m_DETAILS_NO; }
        }
        public int _ITEM_ID
        {
            get { return m_ITEM_ID; }
        }
        public int _SEQ
        {
            get { return m_SEQ; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.DETAILS_NO]);
            m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.ITEM_ID]);
            m_SEQ = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.SEQ]);
            m_ITEM_NAME = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.ITEM_NAME]);
            m_OCR_ENT_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.OCR_ENT_DATA]);
            m_OCR_VFY_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.OCR_VFY_DATA]);
            m_ENT_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.ENT_DATA]);
            m_VFY_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.VFY_DATA]);
            m_END_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.END_DATA]);
            m_BUA_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.BUA_DATA]);
            m_CTR_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.CTR_DATA]);
            m_ICTEISEI_DATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.ICTEISEI_DATA]);
            m_MRC_CHG_BEFDATA = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.MRC_CHG_BEFDATA]);
            m_E_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.E_TERM]);
            m_E_OPENO = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.E_OPENO]);
            m_E_STIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.E_STIME]);
            m_E_ETIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.E_ETIME]);
            m_E_YMD = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.E_YMD]);
            m_E_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.E_TIME]);
            m_V_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.V_TERM]);
            m_V_OPENO = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.V_OPENO]);
            m_V_STIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.V_STIME]);
            m_V_ETIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.V_ETIME]);
            m_V_YMD = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.V_YMD]);
            m_V_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.V_TIME]);
            m_C_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.C_TERM]);
            m_C_OPENO = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.C_OPENO]);
            m_C_STIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.C_STIME]);
            m_C_ETIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.C_ETIME]);
            m_C_YMD = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.C_YMD]);
            m_C_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.C_TIME]);
            m_O_TERM = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.O_TERM]);
            m_O_OPENO = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.O_OPENO]);
            m_O_STIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.O_STIME]);
            m_O_ETIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.O_ETIME]);
            m_O_YMD = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.O_YMD]);
            m_O_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.O_TIME]);
            m_ITEM_TOP = DBConvert.ToDecimalNull(dr[TBL_TRITEM_HIST.ITEM_TOP]);
            m_ITEM_LEFT = DBConvert.ToDecimalNull(dr[TBL_TRITEM_HIST.ITEM_LEFT]);
            m_ITEM_WIDTH = DBConvert.ToDecimalNull(dr[TBL_TRITEM_HIST.ITEM_WIDTH]);
            m_ITEM_HEIGHT = DBConvert.ToDecimalNull(dr[TBL_TRITEM_HIST.ITEM_HEIGHT]);
            m_UPDATE_DATE = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.UPDATE_DATE]);
            m_UPDATE_TIME = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.UPDATE_TIME]);
            m_UPDATE_KBN = DBConvert.ToIntNull(dr[TBL_TRITEM_HIST.UPDATE_KBN]);
            m_FIX_TRIGGER = DBConvert.ToStringNull(dr[TBL_TRITEM_HIST.FIX_TRIGGER]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRITEM_HIST.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRITEM_HIST.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_TRITEM_HIST.GYM_ID + ", " +
                TBL_TRITEM_HIST.OPERATION_DATE +
                TBL_TRITEM_HIST.SCAN_TERM + ", " +
                TBL_TRITEM_HIST.BAT_ID +
                TBL_TRITEM_HIST.DETAILS_NO + ", " +
                TBL_TRITEM_HIST.SEQ + ", " +
                TBL_TRITEM_HIST.ITEM_ID;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int operation_date, string scan_term, int bat_id, int details_no, int item_id, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRITEM_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                        TBL_TRITEM_HIST.GYM_ID + "=" + gym_id + " AND " +
                        TBL_TRITEM_HIST.OPERATION_DATE + "=" + operation_date + " AND " +
                        TBL_TRITEM_HIST.SCAN_TERM + "='" + scan_term + "' AND " +
                        TBL_TRITEM_HIST.BAT_ID + "=" + bat_id + " AND " +
                        TBL_TRITEM_HIST.DETAILS_NO + "=" + details_no + " AND " +
                        TBL_TRITEM_HIST.SEQ + "=" + seq + " AND " +
                        TBL_TRITEM_HIST.ITEM_ID + "=" + item_id;
            return strSql;
        }

        /// <summary>
        /// 明細単位でのDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryDetails(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_TRITEM_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRITEM_HIST.GYM_ID + "=" + gymid + " AND " +
                TBL_TRITEM_HIST.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRITEM_HIST.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRITEM_HIST.BAT_ID + "=" + batid + " AND " +
                TBL_TRITEM_HIST.DETAILS_NO + "=" + detailsno + " ";
            return strSQL;
        }

        #endregion
    }
}
