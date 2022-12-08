using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IIPCommonClass.DB
{
    public class TBL_BAT_TRJRNL
    {
        public TBL_BAT_TRJRNL()
        {
        }

        public TBL_BAT_TRJRNL(int gym_id, int bat_id, int details_no, int item_id, string scanner_id, int operation_date)
        {
            m_GYM_ID = gym_id;
            m_BAT_ID = bat_id;
            m_DETAILS_NO = details_no;
            m_JRNL_ID = item_id;
            m_SCANNER_ID = scanner_id;
            m_OPERATION_DATE = operation_date;
        }

        public TBL_BAT_TRJRNL(int gym_id, int bat_id, int details_no, int item_id, long count, long amount, string scanner_id, int operation_date)
        {
            m_GYM_ID = gym_id;
            m_BAT_ID = bat_id;
            m_DETAILS_NO = details_no;
            m_JRNL_ID = item_id;
            m_SCANNER_ID = scanner_id;
            m_OPERATION_DATE = operation_date;

            m_COUNT = count;
            m_AMOUNT = amount;
        }

        public TBL_BAT_TRJRNL(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_NAME = "BAT_TRJRNL";

        public const string ROW_GYM_ID = "GYM_ID";
        public const string ROW_BAT_ID = "BAT_ID";
        public const string ROW_DETAILS_NO = "DETAILS_NO";
        public const string ROW_SCANNER_ID = "SCANNER_ID";
        public const string ROW_OPERATION_DATE = "OPERATION_DATE";
        public const string ROW_JRNL_ID = "JRNL_ID";
        public const string ROW_COUNT = "COUNT";
        public const string ROW_AMOUNT = "AMOUNT";

        #endregion

        #region メンバ

        private int m_GYM_ID;
        private long m_BAT_ID;
        private int m_DETAILS_NO;
        private string m_SCANNER_ID;
        private int m_OPERATION_DATE;
        private int m_JRNL_ID;
        private long m_COUNT;
        private long m_AMOUNT;

        #endregion

        #region プロパティ

        public int GYM_ID
        {
            get { return m_GYM_ID; }
        }

        public long BAT_ID
        {
            get { return m_BAT_ID; }
        }

        public int DETAILS_NO
        {
            get { return m_DETAILS_NO; }
        }

        public string SCANNER_ID
        {
            get { return m_SCANNER_ID; }
        }

        public int OPERATION_DATE
        {
            get { return m_OPERATION_DATE; }
        }

        public int JRNL_ID
        {
            get { return m_JRNL_ID; }
        }

        public long COUNT
        {
            get { return m_COUNT; }
        }

        public long AMOUNTt
        {
            get { return m_AMOUNT; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRJRNL.ROW_GYM_ID]);
            m_BAT_ID = DBConvert.ToLongNull(dr[TBL_BAT_TRJRNL.ROW_BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRJRNL.ROW_DETAILS_NO]);
            m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_BAT_TRJRNL.ROW_SCANNER_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BAT_TRJRNL.ROW_OPERATION_DATE]);
            m_JRNL_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRJRNL.ROW_JRNL_ID]);
            m_COUNT = DBConvert.ToLongNull(dr[TBL_BAT_TRJRNL.ROW_COUNT]);
            m_AMOUNT = DBConvert.ToLongNull(dr[TBL_BAT_TRJRNL.ROW_AMOUNT]);
        }

        #endregion

        #region クエリ
        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gym_id">業務番号</param>
        /// <param name="bat_id">バッチ番号</param>
        /// <param name="details_no">明細番号</param>
        /// <param name="scanner_id">スキャナー号機</param>
        /// <param name="operation_date">処理日</param>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int bat_id, int details_no, string scanner_id, int operation_date)
        {
            string strSql = "SELECT * FROM " + TBL_BAT_TRJRNL.TABLE_NAME +
                  " WHERE " + TBL_BAT_TRJRNL.ROW_GYM_ID + "=" + gym_id +
                  " AND " + TBL_BAT_TRJRNL.ROW_BAT_ID + "=" + bat_id +
                  " AND " + TBL_BAT_TRJRNL.ROW_DETAILS_NO + "=" + details_no +
                  " AND " + TBL_BAT_TRJRNL.ROW_SCANNER_ID + "='" + scanner_id + "'" +
                  " AND " + TBL_BAT_TRJRNL.ROW_OPERATION_DATE + "=" + operation_date +
                  " ORDER BY " + TBL_BAT_TRJRNL.ROW_JRNL_ID;

            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_BAT_TRJRNL.TABLE_NAME + " (" +
                TBL_BAT_TRJRNL.ROW_GYM_ID + "," +
                TBL_BAT_TRJRNL.ROW_BAT_ID + "," +
                TBL_BAT_TRJRNL.ROW_DETAILS_NO + "," +
                TBL_BAT_TRJRNL.ROW_JRNL_ID + "," +
                TBL_BAT_TRJRNL.ROW_SCANNER_ID + "," +
                TBL_BAT_TRJRNL.ROW_OPERATION_DATE + "," +
                TBL_BAT_TRJRNL.ROW_COUNT + "," +
                TBL_BAT_TRJRNL.ROW_AMOUNT + ") VALUES (" +
                m_GYM_ID + "," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_JRNL_ID + "," +
                "'" + m_SCANNER_ID + "'," +
                "'" + m_OPERATION_DATE + "'," +
                m_COUNT + "," +
                m_AMOUNT + ")";

            return strSql;
        }

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BAT_TRJRNL.TABLE_NAME + " SET " +
                TBL_BAT_TRJRNL.ROW_COUNT + "=" + m_COUNT + ", " +
                TBL_BAT_TRJRNL.ROW_AMOUNT + "='" + m_AMOUNT + "', " +
                " WHERE " +
                TBL_BAT_TRJRNL.ROW_GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_BAT_TRJRNL.ROW_BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_BAT_TRJRNL.ROW_DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
                TBL_BAT_TRJRNL.ROW_JRNL_ID + "=" + m_JRNL_ID + " AND " +
                TBL_BAT_TRJRNL.ROW_SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
                TBL_BAT_TRJRNL.ROW_OPERATION_DATE + "=" + m_OPERATION_DATE;

            return strSql;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_BAT_TRJRNL.TABLE_NAME + " WHERE " + TBL_BAT_TRJRNL.ROW_GYM_ID + "=" + m_GYM_ID;
            strSQL += " AND " + TBL_BAT_TRJRNL.ROW_BAT_ID + "=" + m_BAT_ID + " AND " + TBL_BAT_TRJRNL.ROW_DETAILS_NO + "=" + m_DETAILS_NO;
            strSQL += " AND " + TBL_BAT_TRJRNL.ROW_SCANNER_ID + "='" + m_SCANNER_ID + "' AND " + TBL_BAT_TRJRNL.ROW_OPERATION_DATE + "=" + m_OPERATION_DATE;

            return strSQL;
        }

        /// <summary>
        /// バッチＩＤを条件とするDELETE文を作成します
        /// </summary>
        /// <param name="gym_id">業務番号</param>
        /// <param name="bat_id">バッチＩＤ</param>
        /// <param name="scanner_id">スキャナー号機</param>
        /// <param name="operation_date">処理日</param>
        /// <returns></returns>
        public static string GetDeleteQuery(int gym_id, int bat_id, string scanner_id, int operation_date)
        {
            string strSQL = "DELETE FROM " + TBL_BAT_TRJRNL.TABLE_NAME + " WHERE " + TBL_BAT_TRJRNL.ROW_GYM_ID + "=" + gym_id;
            strSQL += " AND " + TBL_BAT_TRJRNL.ROW_BAT_ID + "=" + bat_id;
            strSQL += " AND " + TBL_BAT_TRJRNL.ROW_SCANNER_ID + "='" + scanner_id + "' AND " + TBL_BAT_TRJRNL.ROW_OPERATION_DATE + "=" + operation_date;

            return strSQL;
        }

        /// <summary>
        /// 明細ＩＤを条件とするDELETE文を作成します
        /// </summary>
        /// <param name="gym_id">業務番号</param>
        /// <param name="bat_id">バッチＩＤ</param>
        /// <param name="details_no">明細ＩＤ</param>
        /// <param name="scanner_id">スキャナー号機</param>
        /// <param name="operation_date">処理日</param>
        /// <returns></returns>
        public static string GetDeleteQuery(int gym_id, int bat_id, int details_no, string scanner_id, int operation_date)
        {
            string strSQL = "DELETE FROM " + TBL_BAT_TRJRNL.TABLE_NAME + " WHERE " + TBL_BAT_TRJRNL.ROW_GYM_ID + "=" + gym_id;
            strSQL += " AND " + TBL_BAT_TRJRNL.ROW_BAT_ID + "=" + bat_id + " AND " + TBL_BAT_TRJRNL.ROW_DETAILS_NO + "=" + details_no;
            strSQL += " AND " + TBL_BAT_TRJRNL.ROW_SCANNER_ID + "='" + scanner_id + "' AND " + TBL_BAT_TRJRNL.ROW_OPERATION_DATE + "=" + operation_date;

            return strSQL;
        }

        #endregion

    }
}
