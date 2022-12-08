using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Linq;

namespace CommonTable.DB
{
    /// <summary>
    /// TBL_GENERALTEXTMF
    /// </summary>
    public class TBL_GENERALTEXTMF
    {
        public TBL_GENERALTEXTMF()
        {
        }

        public TBL_GENERALTEXTMF(int textkbn, int recordkbn, int no, string value)
        {
            m_TEXTKBN = textkbn;
            m_RECORDKBN = recordkbn;
            m_NO = no;
            m_VALUE = value;
        }

        public TBL_GENERALTEXTMF(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        #region TextKbn定義
        public enum TextKbn
        {
            /// <summary>通知テキスト</summary>
            TSUCHITXT = 1,
            /// <summary>結果テキスト</summary>
            RESULTTXT = 2,
            /// <summary>持帰要求</summary>
            ICREQ = 3,
            /// <summary>持帰要求結果</summary>
            ICREQRET = 4,
        }
        #endregion 

        #region RecordKbn定義
        public enum RecordKbn
        {
            /// <summary>ヘッダー</summary>
            HEADER = 1,
            /// <summary>データ</summary>
            DATA = 2,
            /// <summary>トレーラ</summary>
            TRAILER = 8,
            /// <summary>エンド</summary>
            END = 9,
        }
        #endregion 

        #region テーブル定義

        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
		public const string TABLE_PHYSICAL_NAME = "GENERALTEXTMF";

        public const string TEXTKBN = "TEXTKBN";
        public const string RECORDKBN = "RECORDKBN";
        public const string NO = "NO";
        public const string NAME = "NAME";
        public const string VALUE = "VALUE";
        public const string DESCRIPTION = "DESCRIPTION";
        public const string ABBREVIATE = "ABBREVIATE";

        #endregion

        #region メンバ

        private int m_TEXTKBN = 0;
        private int m_RECORDKBN = 0;
        private int m_NO = 0;
        private string m_VALUE = "";
        public string m_NAME = "";
        public string m_DESCRIPTION = "";
        public string m_ABBREVIATE = "";

        #endregion

        #region プロパティ

        public int _TEXTKBN
        {
            get { return m_TEXTKBN; }
        }
        public int _RECORDKBN
        {
            get { return m_RECORDKBN; }
        }

        public int _NO
        {
            get { return m_NO; }
        }

        public string _VALUE
        {
            get { return m_VALUE; }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
		{
            m_TEXTKBN = DBConvert.ToIntNull(dr[TBL_GENERALTEXTMF.TEXTKBN]);
            m_RECORDKBN = DBConvert.ToIntNull(dr[TBL_GENERALTEXTMF.RECORDKBN]);
            m_NO = DBConvert.ToIntNull(dr[TBL_GENERALTEXTMF.NO]);
            m_VALUE = DBConvert.ToStringNull(dr[TBL_GENERALTEXTMF.VALUE]);
            m_NAME = DBConvert.ToStringNull(dr[TBL_GENERALTEXTMF.NAME]);
            m_DESCRIPTION = DBConvert.ToStringNull(dr[TBL_GENERALTEXTMF.DESCRIPTION]);
            m_ABBREVIATE = DBConvert.ToStringNull(dr[TBL_GENERALTEXTMF.ABBREVIATE]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int textkbn, int recordkbn, int no, string value)
        {
            string strSql = "SELECT * FROM " + TBL_GENERALTEXTMF.TABLE_NAME +
                " WHERE " +
                TBL_GENERALTEXTMF.TEXTKBN + "=" + textkbn + " AND " +
                TBL_GENERALTEXTMF.RECORDKBN + "=" + recordkbn + " AND " +
                TBL_GENERALTEXTMF.NO + "=" + no + " AND " +
                TBL_GENERALTEXTMF.VALUE + "='" + value + "' ";
            return strSql;
        }

        /// <summary>
        /// TEXTKBNを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryTextKbn(int textkbn)
        {
            string strSql = "SELECT * FROM " + TBL_GENERALTEXTMF.TABLE_NAME +
                " WHERE " +
                TBL_GENERALTEXTMF.TEXTKBN + "=" + textkbn + " ";
            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_GENERALTEXTMF.TABLE_NAME + " (" +
                TBL_GENERALTEXTMF.TEXTKBN + "," +
                TBL_GENERALTEXTMF.RECORDKBN + "," +
                TBL_GENERALTEXTMF.NO + "," +
                TBL_GENERALTEXTMF.NAME + "," +
                TBL_GENERALTEXTMF.VALUE + "," +
                TBL_GENERALTEXTMF.DESCRIPTION + "," +
                TBL_GENERALTEXTMF.ABBREVIATE + ") VALUES (" +
                m_TEXTKBN + "," +
                m_RECORDKBN + "," +
                m_NO + "," +
                m_NAME + "," +
                "'" + m_VALUE + "'," +
                "'" + m_DESCRIPTION + "'," +
                "'" + m_ABBREVIATE + "'" + ")";
			return strSql;
		}

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_GENERALTEXTMF.TABLE_NAME + " SET " +
                TBL_GENERALTEXTMF.NAME + "='" + m_NAME + "', " +
                TBL_GENERALTEXTMF.DESCRIPTION + "='" + m_DESCRIPTION + "', " +
                TBL_GENERALTEXTMF.ABBREVIATE + "='" + m_ABBREVIATE + "' " +
                " WHERE " +
                TBL_GENERALTEXTMF.TEXTKBN + "=" + m_TEXTKBN + " AND " +
                TBL_GENERALTEXTMF.RECORDKBN + "=" + m_RECORDKBN + " AND " +
                TBL_GENERALTEXTMF.NO + "=" + m_NO + " AND " +
                TBL_GENERALTEXTMF.VALUE + "='" + m_VALUE + "' ";

            return strSql;
        }

        #endregion
    }
}
