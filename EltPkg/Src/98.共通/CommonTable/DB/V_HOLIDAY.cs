using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CommonTable.DB
{
	/// <summary>
	/// V_HOLIDAY の概要の説明です。
	/// </summary>
	public class V_HOLIDAY
	{
        /// <summary>
        /// データを渡された場合
        /// </summary>
        /// <param name="dr"></param>
        public V_HOLIDAY()
        {
        }

		/// <summary>
		/// データを渡された場合
		/// </summary>
		/// <param name="dr"></param>
		public V_HOLIDAY(DataRow dr)
		{
			this.setDataRow(dr);
		}

		#region テーブル定義
		public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
		public const string TABLE_PHYSICAL_NAME = "HOLIDAY";
		//カラム
		public const string DAY = "DAY";
		public const string DESCRIPTION = "DESCRIPTION";

		#endregion

		#region メンバ
		private int m_DAY = 0;
		public string m_DESCRIPTION = "";

		#endregion

		#region プロパティ

		/// <summary>
		/// 休日
		/// </summary>
		public int _DAY
		{
			get{ return m_DAY; }
		}

		#endregion

		#region データセット

		private void setDataRow(DataRow dr)
		{
			m_DAY = DBConvert.ToIntNull(dr[V_HOLIDAY.DAY]);
			m_DESCRIPTION = DBConvert.ToStringNull(dr[V_HOLIDAY.DESCRIPTION]);
		}

		/// <summary>
		/// SELECT_QUERY取得
		/// </summary>
		/// <returns></returns>
		public static string GetSelectQuery()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT *");
			sb.Append(" FROM " + TABLE_NAME);
			return sb.ToString();
		}

		/// <summary>
		/// SELECT_QUERY取得
		/// </summary>
		/// <returns></returns>
		public static string GetSelectQuery(int day)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT *");
            sb.Append(" FROM " + TABLE_NAME);
            sb.Append(" WHERE " + V_HOLIDAY.DAY + " = " + day);
            return sb.ToString();
        }

        #endregion
    }
}
