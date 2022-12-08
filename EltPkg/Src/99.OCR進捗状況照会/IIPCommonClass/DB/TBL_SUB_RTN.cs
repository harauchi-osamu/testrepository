using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// 業務パラメーター
	/// </summary>
	public class TBL_SUB_RTN
	{
        public TBL_SUB_RTN()
        {
        }

		public TBL_SUB_RTN(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

		public const string TABLE_NAME = "SUB_RTN";

        public const string SUB_KBN = "SUB_KBN";
        public const string SUB_SUB = "SUB_SUB";

		#endregion

		#region メンバ

        public int m_SUB_KBN = 0;
        public string m_SUB_SUB = "";

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
        }

		#endregion

		#region クエリ取得

        /// <summary>
		/// 区分、ルーチン名を条件とするSELECT文を作成します
		/// </summary>
        /// <param name="subkbn">サブルーチン区分</param>
        /// <param name="subrtn">サブルーチン名</param>
		/// <returns></returns>
        public string GetSelectQuery(int subkbn, string subrtn)
        {
            string strSql = "SELECT * FROM " + TBL_SUB_RTN.TABLE_NAME +
                  " WHERE " + TBL_SUB_RTN.SUB_KBN + "=" + subkbn
                  + " AND TRIM(" + TBL_SUB_RTN.SUB_SUB + ")='" + subrtn + "'";

            return strSql;
        }

        /// <summary>
        /// SELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetSelectQuery()
        {
            return "SELECT * FROM " + TBL_SUB_RTN.TABLE_NAME;
        }

		/// <summary>
		/// ホストへ送信する為のインサート文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_SUB_RTN.TABLE_NAME + " (" +
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
