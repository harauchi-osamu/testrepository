using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace CommonTable.DB
{
    /// <summary>
    /// 項目
    /// </summary>
    public class TBL_TRMEIIMG_HIST
    {
        public TBL_TRMEIIMG_HIST(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="batid"></param>
        /// <param name="detailsno"></param>
        /// <param name="IMG_FLNM"></param>
        /// <param name="SCAN_TERM"></param>
        /// <param name="operationdate"></param>
        /// <param name="imageno"></param>
        public TBL_TRMEIIMG_HIST(int gymid, int operationdate, string scannerterm, int batid, int detailsno, int img_kbn, int seq, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scannerterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
            m_IMG_KBN = img_kbn;
            m_SEQ = seq;
        }

        public TBL_TRMEIIMG_HIST(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "TRMEIIMG_HIST";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string IMG_KBN = "IMG_KBN";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string SEQ = "SEQ";
        public const string IMG_FLNM = "IMG_FLNM";
        public const string IMG_FLNM_OLD = "IMG_FLNM_OLD";
        public const string OC_OC_BK_NO = "OC_OC_BK_NO";
        public const string OC_OC_BR_NO = "OC_OC_BR_NO";
        public const string OC_IC_BK_NO = "OC_IC_BK_NO";
        public const string OC_OC_DATE = "OC_OC_DATE";
        public const string OC_CLEARING_DATE = "OC_CLEARING_DATE";

        public const string PAY_KBN = "PAY_KBN";
        public const string UNIQUE_CODE = "UNIQUE_CODE";
        public const string OC_IMG_KBN = "OC_IMG_KBN";
        public const string FILE_EXTENSION = "FILE_EXTENSION";
        public const string BUA_STS = "BUA_STS";
        public const string BUA_DATE = "BUA_DATE";
        public const string BUA_TIME = "BUA_TIME";
        public const string GDA_DATE = "GDA_DATE";
        public const string GDA_TIME = "GDA_TIME";
        public const string IMG_ARCH_NAME = "IMG_ARCH_NAME";
        public const string DELETE_DATE = "DELETE_DATE";
        public const string DELETE_FLG = "DELETE_FLG";
        public const string UPDATE_DATE = "UPDATE_DATE";
        public const string UPDATE_TIME = "UPDATE_TIME";
        public const string UPDATE_KBN = "UPDATE_KBN";

        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_IMG_KBN = 0;
        private int m_DETAILS_NO = 0;
        private int m_SEQ = 0;
        public string m_IMG_FLNM = "";
        public string m_IMG_FLNM_OLD = "";
        public string m_OC_OC_BK_NO = "";
        public string m_OC_OC_BR_NO = "";
        public string m_OC_IC_BK_NO = "";
        public string m_OC_OC_DATE = "";
        public string m_OC_CLEARING_DATE = "";
        public string m_PAY_KBN = "";
        public string m_UNIQUE_CODE = "";
        public string m_OC_IMG_KBN = "";
        public string m_FILE_EXTENSION = "";
        public int m_BUA_STS = 0;
        public int m_BUA_DATE = 0;
        public int m_BUA_TIME = 0;
        public int m_GDA_DATE = 0;
        public int m_GDA_TIME = 0;
        public string m_IMG_ARCH_NAME = "";
        public int m_DELETE_DATE = 0;
        public string m_DELETE_FLG = "";
        public int m_UPDATE_DATE = 0;
        public int m_UPDATE_TIME = 0;
        public int m_UPDATE_KBN = 0;


        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
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
        public string _SCAN_TERM
        {
            get { return m_SCAN_TERM; }
        }

        public int _OPERATION_DATE
        {
            get { return m_OPERATION_DATE; }
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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.BAT_ID]);
            m_IMG_KBN = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.IMG_KBN]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.DETAILS_NO]);
            m_SEQ = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.SEQ]);
            m_IMG_FLNM = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.IMG_FLNM]);
            m_IMG_FLNM_OLD = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.IMG_FLNM_OLD]);
            m_OC_OC_BK_NO = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.OC_OC_BK_NO]);
            m_OC_OC_BR_NO = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.OC_OC_BR_NO]);
            m_OC_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.OC_IC_BK_NO]);
            m_OC_OC_DATE = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.OC_OC_DATE]);
            m_OC_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.OC_CLEARING_DATE]);
            m_PAY_KBN = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.PAY_KBN]);
            m_UNIQUE_CODE = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.UNIQUE_CODE]);
            m_OC_IMG_KBN = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.OC_IMG_KBN]);
            m_FILE_EXTENSION = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.FILE_EXTENSION]);
            m_BUA_STS = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.BUA_STS]);
            m_BUA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.BUA_DATE]);
            m_BUA_TIME = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.BUA_TIME]);
            m_GDA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.GDA_DATE]);
            m_GDA_TIME = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.GDA_TIME]);
            m_IMG_ARCH_NAME = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.IMG_ARCH_NAME]);
            m_DELETE_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.DELETE_DATE]);
            m_DELETE_FLG = DBConvert.ToStringNull(dr[TBL_TRMEIIMG_HIST.DELETE_FLG]);
            m_UPDATE_DATE = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.UPDATE_DATE]);
            m_UPDATE_TIME = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.UPDATE_TIME]);
            m_UPDATE_KBN = DBConvert.ToIntNull(dr[TBL_TRMEIIMG_HIST.UPDATE_KBN]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRMEIIMG_HIST.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int bat_id, string scan_term, int operation_date, int img_kbn, int details_no, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEIIMG_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG_HIST.GYM_ID + "=" + gym_id + " AND " +
                TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + operation_date + " AND " +
                TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + scan_term + "' AND " +
                TBL_TRMEIIMG_HIST.IMG_KBN + "=" + img_kbn + " AND " +
                 TBL_TRMEIIMG_HIST.SEQ + "=" + seq + " AND " +
                TBL_TRMEIIMG_HIST.DETAILS_NO + "='" + details_no + "' AND " +
                TBL_TRMEIIMG_HIST.BAT_ID + "=" + bat_id +
                " ORDER BY " +
                TBL_TRMEIIMG_HIST.GYM_ID + ", " +
                TBL_TRMEIIMG_HIST.OPERATION_DATE + ", " +
                TBL_TRMEIIMG_HIST.SCAN_TERM + ", " +
                TBL_TRMEIIMG_HIST.BAT_ID + ", " +
                TBL_TRMEIIMG_HIST.IMG_KBN + ", " +
                TBL_TRMEIIMG_HIST.DETAILS_NO + ", " +
                TBL_TRMEIIMG_HIST.SEQ;
            return strSql;
        }

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int bat_id, string scan_term, int operation_date, int details_no, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEIIMG_HIST.TABLE_NAME(Schemabankcd) +
                 " WHERE " + TBL_TRMEIIMG_HIST.GYM_ID + "=" + gym_id +
                 " AND " + TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + operation_date +
                 " AND " + TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + SCAN_TERM + "'" +
                 " AND " + TBL_TRMEIIMG_HIST.BAT_ID + "=" + bat_id +
                " AND " + TBL_TRMEIIMG_HIST.SEQ + "=" + seq +
                 " AND " + TBL_TRMEIIMG_HIST.DETAILS_NO + "=" + details_no +
                 " ORDER BY " + TBL_TRMEIIMG_HIST.GYM_ID;
            return strSql;
        }

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int bat_id, string scan_term, int operation_date, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEIIMG_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " + TBL_TRMEIIMG_HIST.GYM_ID + "=" + gym_id +
                " AND " + TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + operation_date +
                " AND " + TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + scan_term + "'" +
                " AND " + TBL_TRMEIIMG_HIST.SEQ + "='" + seq + "'" +
                " AND " + TBL_TRMEIIMG_HIST.BAT_ID + "=" + bat_id +
                " ORDER BY " + TBL_TRMEIIMG_HIST.GYM_ID;
            return strSql;
        }

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int bat_id, int details_no, string scan_term, int operation_date, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEIIMG_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " + TBL_TRMEIIMG_HIST.GYM_ID + "=" + gym_id +
                " AND " + TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + operation_date +
                " AND " + TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + scan_term + "'" +
                " AND " + TBL_TRMEIIMG_HIST.BAT_ID + "=" + bat_id +
                 " AND " + TBL_TRMEIIMG_HIST.SEQ + "=" + seq +
                " AND " + TBL_TRMEIIMG_HIST.DETAILS_NO + "=" + details_no;
            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_TRMEIIMG_HIST.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRMEIIMG_HIST.GYM_ID + "," +
                TBL_TRMEIIMG_HIST.OPERATION_DATE + "," +
                TBL_TRMEIIMG_HIST.SCAN_TERM + "," +
                TBL_TRMEIIMG_HIST.BAT_ID + "," +
                TBL_TRMEIIMG_HIST.IMG_KBN + "," +
                TBL_TRMEIIMG_HIST.DETAILS_NO + "," +
                TBL_TRMEIIMG_HIST.SEQ + "," +
                TBL_TRMEIIMG_HIST.IMG_FLNM + "," +
                TBL_TRMEIIMG_HIST.IMG_FLNM_OLD + "," +
                TBL_TRMEIIMG_HIST.OC_OC_BK_NO + "," +
                TBL_TRMEIIMG_HIST.OC_OC_BR_NO + "," +
                TBL_TRMEIIMG_HIST.OC_IC_BK_NO + "," +
                TBL_TRMEIIMG_HIST.OC_OC_DATE + "," +
                TBL_TRMEIIMG_HIST.OC_CLEARING_DATE + "," +
                TBL_TRMEIIMG_HIST.PAY_KBN + "," +
                TBL_TRMEIIMG_HIST.UNIQUE_CODE + "," +
                TBL_TRMEIIMG_HIST.OC_IMG_KBN + "," +
                TBL_TRMEIIMG_HIST.FILE_EXTENSION + "," +
                TBL_TRMEIIMG_HIST.BUA_STS + "," +
                TBL_TRMEIIMG_HIST.BUA_DATE + "," +
                TBL_TRMEIIMG_HIST.BUA_TIME + "," +
                TBL_TRMEIIMG_HIST.GDA_DATE + "," +
                TBL_TRMEIIMG_HIST.GDA_TIME + "," +
                TBL_TRMEIIMG_HIST.IMG_ARCH_NAME + "," +
                TBL_TRMEIIMG_HIST.DELETE_DATE + "," +
                TBL_TRMEIIMG_HIST.DELETE_FLG + "," +
                TBL_TRMEIIMG_HIST.UPDATE_DATE + "," +
                TBL_TRMEIIMG_HIST.UPDATE_TIME + "," +
                TBL_TRMEIIMG_HIST.UPDATE_KBN + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_IMG_KBN + "," +
                m_DETAILS_NO + "," +
                 m_SEQ + "," +
                m_IMG_FLNM + "," +
                "'" + m_IMG_FLNM_OLD + "'," +
                "'" + m_OC_OC_BK_NO + "'," +
                "'" + m_OC_OC_BR_NO + "'," +
                "'" + m_OC_IC_BK_NO + "'," +
                "'" + m_OC_OC_DATE + "'," +
                "'" + m_OC_CLEARING_DATE + "'," +
                "'" + m_PAY_KBN + "'," +
                "'" + m_UNIQUE_CODE + "'," +
                "'" + m_OC_IMG_KBN + "'," +
                "'" + m_FILE_EXTENSION + "'," +
                m_BUA_STS + "," +
                m_BUA_DATE + "," +
                m_BUA_TIME + "," +
                m_GDA_DATE + "," +
                m_GDA_TIME + "," +
                m_IMG_ARCH_NAME + "," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + "," +
                m_UPDATE_DATE + "," +
                m_UPDATE_TIME + "," +
                m_UPDATE_KBN + ")";
            return strSql;
        }

        /// <summary>
        /// update文を作成します（エントリ用）（★OC_CLEARING_DATEは更新不可）
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryEnt()
        {
            string strSql = "UPDATE " + TBL_TRMEIIMG_HIST.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRMEIIMG_HIST.IMG_FLNM + "=" + m_IMG_FLNM + "', " +
                TBL_TRMEIIMG_HIST.IMG_FLNM_OLD + "='" + m_IMG_FLNM_OLD + "', " +
                TBL_TRMEIIMG_HIST.OC_OC_BK_NO + "='" + m_OC_OC_BK_NO + "', " +
                TBL_TRMEIIMG_HIST.OC_OC_BR_NO + "='" + m_OC_OC_BR_NO + "', " +
                TBL_TRMEIIMG_HIST.OC_IC_BK_NO + "='" + m_OC_IC_BK_NO + "', " +
                TBL_TRMEIIMG_HIST.OC_OC_DATE + "='" + m_OC_OC_DATE + "', " +
                //  TBL_TRMEIIMG_HIST.OC_CLEARING_DATE + "='" + m_OC_CLEARING_DATE + "', " +
                TBL_TRMEIIMG_HIST.PAY_KBN + "='" + m_PAY_KBN + "', " +
                TBL_TRMEIIMG_HIST.UNIQUE_CODE + "='" + m_UNIQUE_CODE + "', " +
                TBL_TRMEIIMG_HIST.OC_IMG_KBN + "='" + m_OC_IMG_KBN + "', " +
                TBL_TRMEIIMG_HIST.FILE_EXTENSION + "='" + m_FILE_EXTENSION + "', " +
                TBL_TRMEIIMG_HIST.BUA_STS + "=" + m_BUA_STS + ", " +
                TBL_TRMEIIMG_HIST.BUA_DATE + "=" + m_BUA_DATE + ", " +
                TBL_TRMEIIMG_HIST.BUA_TIME + "=" + m_BUA_TIME + ", " +
                TBL_TRMEIIMG_HIST.GDA_DATE + "=" + m_GDA_DATE +
                TBL_TRMEIIMG_HIST.GDA_TIME + "=" + m_GDA_TIME + ", " +
                TBL_TRMEIIMG_HIST.IMG_ARCH_NAME + "=" + m_IMG_ARCH_NAME + ", " +
                TBL_TRMEIIMG_HIST.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRMEIIMG_HIST.DELETE_FLG + "=" + m_DELETE_FLG + ", " +
                TBL_TRMEIIMG_HIST.UPDATE_DATE + "=" + m_UPDATE_DATE + ", " +
                TBL_TRMEIIMG_HIST.UPDATE_TIME + "=" + m_UPDATE_TIME + ", " +
                TBL_TRMEIIMG_HIST.UPDATE_KBN + "=" + m_UPDATE_KBN +
                " WHERE " +
                TBL_TRMEIIMG_HIST.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEIIMG_HIST.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEIIMG_HIST.IMG_KBN + "=" + m_IMG_KBN + " AND " +
                TBL_TRMEIIMG_HIST.SEQ + "=" + m_SEQ + " AND " +
                TBL_TRMEIIMG_HIST.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// update文を作成します（ベリファイ用）（★OC_CLEARING_DATEは更新不可）
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryVfy()
        {
            string strSql = "UPDATE " + TBL_TRMEIIMG_HIST.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRMEIIMG_HIST.IMG_FLNM_OLD + "='" + m_IMG_FLNM_OLD + "', " +
                //TBL_TRMEIIMG_HIST.OC_OC_BK_NO + "='" + m_OC_OC_BK_NO + "', " +
                //TBL_TRMEIIMG_HIST.OC_OC_BR_NO + "='" + m_OC_OC_BR_NO + "', " +
                TBL_TRMEIIMG_HIST.OC_IC_BK_NO + "='" + m_OC_IC_BK_NO + "', " +
                TBL_TRMEIIMG_HIST.OC_OC_DATE + "='" + m_OC_OC_DATE + "', " +
                //TBL_TRMEIIMG_HIST.OC_CLEARING_DATE + "='" + m_OC_CLEARING_DATE + "', " +
                TBL_TRMEIIMG_HIST.PAY_KBN + "='" + m_PAY_KBN + "', " +
                TBL_TRMEIIMG_HIST.UNIQUE_CODE + "='" + m_UNIQUE_CODE + "', " +
                TBL_TRMEIIMG_HIST.OC_IMG_KBN + "='" + m_OC_IMG_KBN + "', " +
                TBL_TRMEIIMG_HIST.FILE_EXTENSION + "='" + m_FILE_EXTENSION + "', " +
                TBL_TRMEIIMG_HIST.BUA_STS + "=" + m_BUA_STS + ", " +
                TBL_TRMEIIMG_HIST.BUA_DATE + "=" + m_BUA_DATE + ", " +
                TBL_TRMEIIMG_HIST.BUA_TIME + "=" + m_BUA_TIME + ", " +
                TBL_TRMEIIMG_HIST.GDA_DATE + "=" + m_GDA_DATE + ", " +
                TBL_TRMEIIMG_HIST.GDA_TIME + "=" + m_GDA_TIME + ", " +
                TBL_TRMEIIMG_HIST.IMG_ARCH_NAME + "=" + m_IMG_ARCH_NAME + ", " +
                TBL_TRMEIIMG_HIST.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRMEIIMG_HIST.DELETE_FLG + "=" + m_DELETE_FLG + ", " +
                TBL_TRMEIIMG_HIST.UPDATE_DATE + "=" + m_UPDATE_DATE + ", " +
                TBL_TRMEIIMG_HIST.UPDATE_TIME + "=" + m_UPDATE_TIME + ", " +
                TBL_TRMEIIMG_HIST.UPDATE_KBN + "=" + m_UPDATE_KBN +
                " WHERE " +
                TBL_TRMEIIMG_HIST.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEIIMG_HIST.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEIIMG_HIST.IMG_KBN + "=" + m_IMG_KBN + " AND " +
                TBL_TRMEIIMG_HIST.SEQ + "=" + m_SEQ + " AND " +
                TBL_TRMEIIMG_HIST.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_TRMEIIMG_HIST.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_TRMEIIMG_HIST.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEIIMG_HIST.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEIIMG_HIST.IMG_KBN + "=" + m_IMG_KBN + " AND " +
                TBL_TRMEIIMG_HIST.SEQ + "=" + m_SEQ + " AND " +
                TBL_TRMEIIMG_HIST.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// 明細単位でのDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryDetails(int gymid, int operationdate, string scannerid, long batid, int detailsno, int Schemabankcd)
        {
            string strSql = "DELETE FROM " + TBL_TRMEIIMG_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG_HIST.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + scannerid + "' AND " +
                TBL_TRMEIIMG_HIST.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG_HIST.DETAILS_NO + "=" + detailsno;
            return strSql;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQuery(int gymid, int operationdate, string scannerid, long batid, int imgkbn, int detailsno, int seq, int Schemabankcd)
        {
            string strSql = "DELETE FROM " + TBL_TRMEIIMG_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEIIMG_HIST.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG_HIST.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG_HIST.SCAN_TERM + "='" + scannerid + "' AND " +
                TBL_TRMEIIMG_HIST.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG_HIST.IMG_KBN + "=" + imgkbn + " AND " +
                TBL_TRMEIIMG_HIST.SEQ + "=" + seq + " AND " +
                TBL_TRMEIIMG_HIST.DETAILS_NO + "=" + detailsno;
            return strSql;
        }

        #endregion
    }
}
