using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CommonTable.DB
{
	/// <summary>
	/// V_GYM_SETTING の概要の説明です。
	/// </summary>
	public class V_BANKMF
	{
        /// <summary>
        /// データを渡された場合
        /// </summary>
        /// <param name="dr"></param>
        public V_BANKMF()
        {
        }

		/// <summary>
		/// データを渡された場合
		/// </summary>
		/// <param name="dr"></param>
		public V_BANKMF(DataRow dr)
		{
			this.setDataRow(dr);
		}

		#region テーブル定義
		public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
		public const string TABLE_PHYSICAL_NAME = "BANKMF";
		//カラム
		public const string BK_NO = "BK_NO";
		public const string BK_NAME_KANJI = "BK_NAME_KANJI";
		public const string BK_NAME_KANA = "BK_NAME_KANA";
		public const string KAMEI_FLG = "KAMEI_FLG";

		#endregion

		#region メンバ
		private int m_BK_NO = 0;
		public string m_BK_NAME_KANJI = "";
		public string m_BK_NAME_KANA = "";
		public int m_KAMEI_FLG = 0;

		#endregion

		#region プロパティ

		/// <summary>
		/// 業務番号
		/// </summary>
		public int _BK_NO
		{
			get{ return m_BK_NO; }
		}

		#endregion

		#region データセット

		private void setDataRow(DataRow dr)
		{
			m_BK_NO = DBConvert.ToIntNull(dr[V_BANKMF.BK_NO]);
			m_BK_NAME_KANJI = DBConvert.ToStringNull(dr[V_BANKMF.BK_NAME_KANJI]);
			m_BK_NAME_KANA = DBConvert.ToStringNull(dr[V_BANKMF.BK_NAME_KANA]);
			m_KAMEI_FLG = DBConvert.ToIntNull(dr[V_BANKMF.KAMEI_FLG]);
		}

		/// <summary>
		/// SELECT_QUERY取得
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <returns></returns>
		public static string GetSelectQuery(int bk_nm)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT *");
            sb.Append(" FROM " + TABLE_NAME);
            sb.Append(" WHERE " + V_BANKMF.BK_NO + " = " + bk_nm);
            return sb.ToString();
        }

        #endregion
    }
}
