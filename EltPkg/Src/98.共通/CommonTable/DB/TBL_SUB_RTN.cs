using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace CommonTable.DB
{
    /// <summary>
    /// サブルーチン
    /// </summary>
    public class TBL_SUB_RTN
	{
        public TBL_SUB_RTN(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

		public TBL_SUB_RTN(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
		}

		#region テーブル定義

		public const string TABLE_PHYSICAL_NAME = "SUB_RTN";

        public const string SUB_KBN = "SUB_KBN";
        public const string SUB_SUB = "SUB_SUB";
        public const string SUB_VALUE = "SUB_VALUE";

        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        public int m_SUB_KBN = 0;
        public string m_SUB_SUB = "";
        public string m_SUB_VALUE = "";

        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
		{
            m_SUB_KBN = DBConvert.ToIntNull(dr[TBL_SUB_RTN.SUB_KBN]);
            m_SUB_SUB = DBConvert.ToStringNull(dr[TBL_SUB_RTN.SUB_SUB]);
            m_SUB_VALUE = DBConvert.ToStringNull(dr[TBL_SUB_RTN.SUB_VALUE]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// 区分、ルーチン名を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="subkbn">サブルーチン区分</param>
        /// <param name="subrtn">サブルーチン名</param>
        /// <returns></returns>
        public string GetSelectQuery(int subkbn, string subrtn, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_SUB_RTN.TABLE_NAME(Schemabankcd) +
                  " WHERE " + TBL_SUB_RTN.SUB_KBN + "=" + subkbn
                  + " AND TRIM(" + TBL_SUB_RTN.SUB_SUB + ")='" + subrtn + "'";

            return strSql;
        }

        /// <summary>
        /// SELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            return "SELECT * FROM " + TBL_SUB_RTN.TABLE_NAME(Schemabankcd);
        }

		/// <summary>
		/// ホストへ送信する為のインサート文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_SUB_RTN.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_SUB_RTN.SUB_KBN + "," +
                TBL_SUB_RTN.SUB_SUB + ") VALUES (" +
                m_SUB_KBN + "," +
                "'" + m_SUB_SUB + "')";

			return strSql;
		}

        /// <summary>
        /// 区分、ルーチン名を条件として存在判定します
        /// </summary>
        /// <param name="subrtn">サブルーチン名</param>
        /// <returns></returns>
        public static bool CheckSubrtn(string subrtn)
        {
			return true;
		}

		#endregion

	}
}
