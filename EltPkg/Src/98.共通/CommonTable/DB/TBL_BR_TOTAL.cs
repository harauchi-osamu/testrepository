using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Linq;

namespace CommonTable.DB
{
    /// <summary>
    /// TBL_BR_TOTAL
    /// </summary>
    public class TBL_BR_TOTAL
    {
        public TBL_BR_TOTAL()
        {
        }

        public TBL_BR_TOTAL(int gymid, int operationdate, string scanimgname)
        {
            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_IMG_FLNM = scanimgname;
        }

        public TBL_BR_TOTAL(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        #region Status定義

        /// <summary>
        /// Statusに対応する出力テキスト取得
        /// </summary>
        public static string GetStatusText(enumStatus Status)
        {
            string rtnValue = string.Empty;
            if (Status.Equals(enumStatus.Wait)) rtnValue = "処理待";
            if (Status.Equals(enumStatus.Processing)) rtnValue = "処理中";
            if (Status.Equals(enumStatus.Complete)) rtnValue = "処理済";
            if (Status.Equals(enumStatus.Hold)) rtnValue = "保留";
            if (Status.Equals(enumStatus.Delete)) rtnValue = "削除";

            return rtnValue;
        }

        public enum enumStatus
        {
            ///<summary>処理待</summary>
            Wait = 1,
            ///<summary>保留</summary>
            Hold = 2,
            ///<summary>処理中</summary>
            Processing = 3,
            ///<summary>処理済</summary>
            Complete = 4,
            ///<summary>削除</summary>
            Delete = 9,
        }

        #endregion

        #region テーブル定義

        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
		public const string TABLE_PHYSICAL_NAME = "BR_TOTAL";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_IMG_FLNM = "SCAN_IMG_FLNM";
        public const string IMPORT_IMG_FLNM = "IMPORT_IMG_FLNM";
        public const string BK_NO = "BK_NO";
        public const string BR_NO = "BR_NO";
        public const string SCAN_DATE = "SCAN_DATE";
        public const string SCAN_BR_NO = "SCAN_BR_NO";
        public const string TOTAL_COUNT = "TOTAL_COUNT";
        public const string TOTAL_AMOUNT = "TOTAL_AMOUNT";
        public const string STATUS = "STATUS";
        public const string LOCK_TERM = "LOCK_TERM";

        public const string ALL_COLUMNS = " GYM_ID,OPERATION_DATE,SCAN_IMG_FLNM,IMPORT_IMG_FLNM,BK_NO,BR_NO,SCAN_DATE,SCAN_BR_NO,TOTAL_COUNT,TOTAL_AMOUNT,STATUS,LOCK_TERM ";
        #endregion

        #region メンバ

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_IMG_FLNM = "";
        public string m_IMPORT_IMG_FLNM = "";
        public int m_BK_NO = 0;
        public int m_BR_NO = 0;
        public int m_SCAN_DATE = 0;
        public int m_SCAN_BR_NO = 0;
        public int m_TOTAL_COUNT = 0;
        public long m_TOTAL_AMOUNT = 0;
        public int m_STATUS = 0;
        public string m_LOCK_TERM = "";
        
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

        public string _SCAN_IMG_FLNM
        {
            get { return m_SCAN_IMG_FLNM; }
        }

        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BR_TOTAL.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BR_TOTAL.OPERATION_DATE]);
            m_SCAN_IMG_FLNM = DBConvert.ToStringNull(dr[TBL_BR_TOTAL.SCAN_IMG_FLNM]);
            m_IMPORT_IMG_FLNM = DBConvert.ToStringNull(dr[TBL_BR_TOTAL.IMPORT_IMG_FLNM]);
            m_BK_NO = DBConvert.ToIntNull(dr[TBL_BR_TOTAL.BK_NO]);
            m_BR_NO = DBConvert.ToIntNull(dr[TBL_BR_TOTAL.BR_NO]);
            m_SCAN_DATE = DBConvert.ToIntNull(dr[TBL_BR_TOTAL.SCAN_DATE]);
            m_SCAN_BR_NO = DBConvert.ToIntNull(dr[TBL_BR_TOTAL.SCAN_BR_NO]);
            m_TOTAL_COUNT = DBConvert.ToIntNull(dr[TBL_BR_TOTAL.TOTAL_COUNT]);
            m_TOTAL_AMOUNT = DBConvert.ToLongNull(dr[TBL_BR_TOTAL.TOTAL_AMOUNT]);
            m_STATUS = DBConvert.ToIntNull(dr[TBL_BR_TOTAL.STATUS]);
            m_LOCK_TERM = DBConvert.ToStringNull(dr[TBL_BR_TOTAL.LOCK_TERM]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(bool Lock)
        {
            string strSql = "SELECT * FROM " + TBL_BR_TOTAL.TABLE_NAME +
                " ORDER BY " +
                TBL_BR_TOTAL.GYM_ID + "," +
                TBL_BR_TOTAL.OPERATION_DATE + "," +
                TBL_BR_TOTAL.SCAN_IMG_FLNM;
            if (Lock)
            {
                strSql += DBConvert.QUERY_LOCK + " ";
            }

            return strSql;
        }

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanimgname)
        {
            string strSql = "SELECT * FROM " + TBL_BR_TOTAL.TABLE_NAME +
                " WHERE " +
                TBL_BR_TOTAL.GYM_ID + "=" + gymid + " AND " +
                TBL_BR_TOTAL.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_BR_TOTAL.SCAN_IMG_FLNM + "='" + scanimgname + "' ";
            return strSql;
        }

        /// <summary>
        /// SCAN_DATE 基準のSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryListData(int gymid, int scandate)
        {
            string strSql = "SELECT * FROM " + TBL_BR_TOTAL.TABLE_NAME +
                " WHERE " + "(" + TBL_BR_TOTAL.SCAN_DATE + "= 0 " + " OR "
                                + TBL_BR_TOTAL.SCAN_DATE + "= " + scandate + " ) AND " + 
                    TBL_BR_TOTAL.GYM_ID + "=" + gymid + " " +
                " ORDER BY " +
                    TBL_BR_TOTAL.STATUS + "," +
                    TBL_BR_TOTAL.OPERATION_DATE + "," +
                    TBL_BR_TOTAL.SCAN_IMG_FLNM;
            ;

            return strSql;
        }

        /// <summary>
        /// SCAN_DATE、STATUS 基準のSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryScanDateStatus(int gymid, int scandate, enumStatus GetStatus)
        {
            string strSql = "SELECT * FROM " + TBL_BR_TOTAL.TABLE_NAME +
                " WHERE " + "(" + TBL_BR_TOTAL.SCAN_DATE + "= 0 " + " OR "
                                + TBL_BR_TOTAL.SCAN_DATE + "= " + scandate + " ) AND " +
                    TBL_BR_TOTAL.GYM_ID + "=" + gymid + " AND " +
                    TBL_BR_TOTAL.STATUS + "=" + GetStatus.ToString("d") + "" +
                " ORDER BY " +
                    TBL_BR_TOTAL.OPERATION_DATE + "," +
                    TBL_BR_TOTAL.SCAN_IMG_FLNM;
            ;

            return strSql;
        }

        /// <summary>
        /// キー項目、SCAN_DATE、STATUS 基準のSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// opedateとscanimgflnmの組み合わせ以上のデータを対象に抽出
        /// </remarks>
        public static string GetSelectQueryScanDateStatus(int gymid, int scandate, int opedate, string scanimgflnm, enumStatus GetStatus)
        {
            string strSql = "SELECT * FROM " + TBL_BR_TOTAL.TABLE_NAME +
                " WHERE " + "(" + TBL_BR_TOTAL.SCAN_DATE + "= 0 " + " OR "
                                + TBL_BR_TOTAL.SCAN_DATE + "= " + scandate + " ) AND " +
                    TBL_BR_TOTAL.GYM_ID + "=" + gymid + " AND " +
                    "(" + "( " + TBL_BR_TOTAL.OPERATION_DATE + "= " + opedate + " AND " + TBL_BR_TOTAL.SCAN_IMG_FLNM + ">= '" + scanimgflnm + "' ) " + " OR " +
                          "( " + TBL_BR_TOTAL.OPERATION_DATE + "> " + opedate + " ) " + 
                    " ) AND " +
                    TBL_BR_TOTAL.STATUS + "=" + GetStatus.ToString("d") + " " +
                " ORDER BY " +
                    TBL_BR_TOTAL.OPERATION_DATE + "," +
                    TBL_BR_TOTAL.SCAN_IMG_FLNM;
            ;

            return strSql;
        }

        /// <summary>
        /// キー項目・ステータス指定のSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryStatus(int gymid, int operationdate, string filename, enumStatus GetStatus)
        {
            return GetSelectQueryStatus(gymid, operationdate, filename, GetStatus, false);
        }

        /// <summary>
        /// キー項目・ステータス指定のSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryStatus(int gymid, int operationdate, string filename, enumStatus GetStatus, bool Lock)
        {
            string strSql = "SELECT * FROM " + TBL_BR_TOTAL.TABLE_NAME +
                " WHERE " +
                TBL_BR_TOTAL.GYM_ID + "=" + gymid + " AND " +
                TBL_BR_TOTAL.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_BR_TOTAL.SCAN_IMG_FLNM + " = '" + filename + "' AND " +
                TBL_BR_TOTAL.STATUS + " = " + GetStatus.ToString("d") + " ";
            if (Lock)
            {
                strSql += DBConvert.QUERY_LOCK + " ";
            }
            return strSql;
        }

        /// <summary>
        /// キー項目・ステータス指定のSELECT文を作成します
        /// ステータス複数指定
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryStatus(int gymid, int operationdate, string filename, System.Collections.Generic.List<enumStatus> GetStatus, bool Lock)
        {
            string strSql = "SELECT * FROM " + TBL_BR_TOTAL.TABLE_NAME +
                " WHERE " +
                TBL_BR_TOTAL.GYM_ID + "=" + gymid + " AND " +
                TBL_BR_TOTAL.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_BR_TOTAL.SCAN_IMG_FLNM + " = '" + filename + "' AND " +
                TBL_BR_TOTAL.STATUS + " IN(" + string.Join(",", GetStatus.Select(x => x.ToString("d"))) + ") ";
            if (Lock)
            {
                strSql += DBConvert.QUERY_LOCK + " ";
            }
            return strSql;
        }

        /// <summary>
        /// ステータス変更時のUPDATE文を作成します
        /// 更新元のステータス指定なし
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateQueryStatusChg(int gymid, int operationdate, string filename, 
                                                     enumStatus SetStatus, string SetLOCK)
        {
            string strSql = "UPDATE " + TBL_BR_TOTAL.TABLE_NAME + " SET " +
                              TBL_BR_TOTAL.STATUS + " = " + SetStatus.ToString("d") + " , " +
                              TBL_BR_TOTAL.LOCK_TERM + " = '" + SetLOCK + "'" +
                            " WHERE " +
                            TBL_BR_TOTAL.GYM_ID + "=" + gymid + " AND " +
                            TBL_BR_TOTAL.OPERATION_DATE + "=" + operationdate + " AND " +
                            TBL_BR_TOTAL.SCAN_IMG_FLNM + " = '" + filename + "' ";
            ;
            return strSql;
        }

        /// <summary>
        /// ステータス変更時のUPDATE文を作成します
        /// 更新元のステータス指定あり
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateQueryStatusChg(int gymid, int operationdate, string filename, enumStatus GetStatus,
                                                     enumStatus SetStatus, string SetLOCK)
        {
            string strSql = "UPDATE " + TBL_BR_TOTAL.TABLE_NAME + " SET " +
                              TBL_BR_TOTAL.STATUS + " = " + SetStatus.ToString("d") + " , " +
                              TBL_BR_TOTAL.LOCK_TERM + " = '" + SetLOCK + "'" +
                            " WHERE " +
                            TBL_BR_TOTAL.GYM_ID + "=" + gymid + " AND " +
                            TBL_BR_TOTAL.OPERATION_DATE + "=" + operationdate + " AND " +
                            TBL_BR_TOTAL.SCAN_IMG_FLNM + " = '" + filename + "' AND " +
                            TBL_BR_TOTAL.STATUS + " = " + GetStatus.ToString("d") + "";
            ;
            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_BR_TOTAL.TABLE_NAME + " (" +
                TBL_BR_TOTAL.GYM_ID + "," +
                TBL_BR_TOTAL.OPERATION_DATE + "," +
                TBL_BR_TOTAL.SCAN_IMG_FLNM + "," +
                TBL_BR_TOTAL.IMPORT_IMG_FLNM + "," +
                TBL_BR_TOTAL.BK_NO + "," +
                TBL_BR_TOTAL.BR_NO + "," +
                TBL_BR_TOTAL.SCAN_DATE + "," +
                TBL_BR_TOTAL.SCAN_BR_NO + "," +
                TBL_BR_TOTAL.TOTAL_COUNT + "," +
                TBL_BR_TOTAL.TOTAL_AMOUNT + "," +
                TBL_BR_TOTAL.STATUS + "," +
                TBL_BR_TOTAL.LOCK_TERM + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_IMG_FLNM + "'," +
                "'" + m_IMPORT_IMG_FLNM + "'," +
                m_BK_NO + "," +
                m_BR_NO + "," +
                m_SCAN_DATE + "," +
                m_SCAN_BR_NO + "," +
                m_TOTAL_COUNT + "," +
                m_TOTAL_AMOUNT + "," +
                m_STATUS + "," +
                "'" + m_LOCK_TERM + "'" + ")";
			return strSql;
		}

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BR_TOTAL.TABLE_NAME + " SET " +
                TBL_BR_TOTAL.IMPORT_IMG_FLNM + "='" + m_IMPORT_IMG_FLNM + "', " +
                TBL_BR_TOTAL.BK_NO + "=" + m_BK_NO + ", " +
                TBL_BR_TOTAL.BR_NO + "=" + m_BR_NO + ", " +
                TBL_BR_TOTAL.SCAN_DATE + "=" + m_SCAN_DATE + ", " +
                TBL_BR_TOTAL.SCAN_BR_NO + "=" + m_SCAN_BR_NO + ", " +
                TBL_BR_TOTAL.TOTAL_COUNT + "=" + m_TOTAL_COUNT + ", " +
                TBL_BR_TOTAL.TOTAL_AMOUNT + "=" + m_TOTAL_AMOUNT + ", " +
                TBL_BR_TOTAL.STATUS + "=" + m_STATUS + ", " +
                TBL_BR_TOTAL.LOCK_TERM + "='" + m_LOCK_TERM + "' " +
                " WHERE " +
                TBL_BR_TOTAL.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_BR_TOTAL.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_BR_TOTAL.SCAN_IMG_FLNM + "='" + m_SCAN_IMG_FLNM + "' ";

            return strSql;
        }

        /// <summary>
        /// ステータス指定のUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryStatus(enumStatus GetStatus)
        {
            string strSql = "UPDATE " + TBL_BR_TOTAL.TABLE_NAME + " SET " +
                TBL_BR_TOTAL.BK_NO + "=" + m_BK_NO + ", " +
                TBL_BR_TOTAL.BR_NO + "=" + m_BR_NO + ", " +
                TBL_BR_TOTAL.SCAN_DATE + "=" + m_SCAN_DATE + ", " +
                TBL_BR_TOTAL.SCAN_BR_NO + "=" + m_SCAN_BR_NO + ", " +
                TBL_BR_TOTAL.TOTAL_COUNT + "=" + m_TOTAL_COUNT + ", " +
                TBL_BR_TOTAL.TOTAL_AMOUNT + "=" + m_TOTAL_AMOUNT + ", " +
                TBL_BR_TOTAL.STATUS + "=" + m_STATUS + ", " +
                TBL_BR_TOTAL.LOCK_TERM + "='" + m_LOCK_TERM + "' " +
                " WHERE " +
                TBL_BR_TOTAL.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_BR_TOTAL.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_BR_TOTAL.SCAN_IMG_FLNM + "='" + m_SCAN_IMG_FLNM + "' AND " +
                TBL_BR_TOTAL.STATUS + " = " + GetStatus.ToString("d");

            return strSql;
        }

        #endregion
    }
}
