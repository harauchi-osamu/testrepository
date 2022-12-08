using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// イメージカーソル
	/// </summary>
	public class TBL_IMG_CURSOR
	{
        public TBL_IMG_CURSOR()
        {
        }

        public TBL_IMG_CURSOR(int gym_id, int dsp_id, int item_id)
        {
            m_GYM_ID = gym_id;
			m_DSP_ID = dsp_id;
			m_ITEM_ID = item_id;
        }

		public TBL_IMG_CURSOR(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

  		public const string TABLE_NAME = "IMG_CURSOR";

		public const string GYM_ID = "GYM_ID";
		public const string DSP_ID = "DSP_ID";
		public const string ITEM_ID = "ITEM_ID";
		public const string ITEM_TOP = "ITEM_TOP";
		public const string ITEM_LEFT = "ITEM_LEFT";
		public const string ITEM_WIDTH = "ITEM_WIDTH";
		public const string ITEM_HEIGHT = "ITEM_HEIGHT";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_DSP_ID = 0;
		private int m_ITEM_ID = 0;
		public int m_ITEM_TOP = 0;
		public int m_ITEM_LEFT = 0;
		public int m_ITEM_WIDTH = 0;
		public int m_ITEM_HEIGHT = 0;

		#endregion

		#region プロパティ

		public int _GYM_ID
		{
			get { return m_GYM_ID; }
		}

		public int _DSP_ID
		{
			get { return m_DSP_ID; }
		}

		public int _ITEM_ID
		{
			get { return m_ITEM_ID; }
			set { m_ITEM_ID = value; }
		}

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR.GYM_ID]);
			m_DSP_ID = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR.DSP_ID]);
			m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR.ITEM_ID]);
			m_ITEM_TOP = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR.ITEM_TOP]);
			m_ITEM_LEFT = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR.ITEM_LEFT]);
			m_ITEM_WIDTH = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR.ITEM_WIDTH]);
			m_ITEM_HEIGHT = DBConvert.ToIntNull(dr[TBL_IMG_CURSOR.ITEM_HEIGHT]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// データを全て取得するSELECT文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetSelectAllQuery()
		{
			string strSql = "SELECT * FROM " + TBL_IMG_CURSOR.TABLE_NAME +
				" ORDER BY " + TBL_IMG_CURSOR.ITEM_ID;

			return strSql;
		}

		/// <summary>
		/// 業務番号, 画面番号を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <param name="dspid">画面番号</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gymid, int dspid)
		{
			string strSql = "SELECT * FROM " + TBL_IMG_CURSOR.TABLE_NAME +
				" WHERE " + TBL_IMG_CURSOR.GYM_ID + "=" + gymid +
				"   AND " + TBL_IMG_CURSOR.DSP_ID + "=" + dspid +
				  " ORDER BY " + TBL_IMG_CURSOR.ITEM_ID;

			return strSql;
		}

		/// <summary>
		/// 業務番号, 画面番号, 項目番号を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <param name="dspid">画面番号</param>
		/// <param name="itemid">項目番号</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gymid, int dspid, int itemid)
		{
			string strSql = "SELECT * FROM " + TBL_IMG_CURSOR.TABLE_NAME +
				  " WHERE " + TBL_IMG_CURSOR.GYM_ID + "=" + gymid +
				  " AND " + TBL_IMG_CURSOR.DSP_ID + "=" + dspid +
				  " AND " + TBL_IMG_CURSOR.ITEM_ID + "=" + itemid;

			return strSql;
		}

		// 市町村・水道事業単位でのコピー／反映
		/// <summary>
		/// 業務番号を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gymid)
		{
			string strSql = "SELECT * FROM " + TBL_IMG_CURSOR.TABLE_NAME +
				  " WHERE " + TBL_IMG_CURSOR.GYM_ID + "=" + gymid +
				  " ORDER BY " + TBL_IMG_CURSOR.DSP_ID + "," + TBL_IMG_CURSOR.ITEM_ID;

			return strSql;
		}

		// 市町村・水道事業単位でのコピー／反映


		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
			string strSql = "INSERT INTO " + TBL_IMG_CURSOR.TABLE_NAME + " (" +
				TBL_IMG_CURSOR.GYM_ID + "," +
				TBL_IMG_CURSOR.DSP_ID + "," +
				TBL_IMG_CURSOR.ITEM_ID + "," +
				TBL_IMG_CURSOR.ITEM_TOP + "," +
				TBL_IMG_CURSOR.ITEM_LEFT + "," +
				TBL_IMG_CURSOR.ITEM_WIDTH + "," +
				TBL_IMG_CURSOR.ITEM_HEIGHT + ") VALUES (" +
				m_GYM_ID + "," +
				m_DSP_ID + "," +
				m_ITEM_ID + "," +
				m_ITEM_TOP + "," +
				m_ITEM_LEFT + "," +
				m_ITEM_WIDTH + "," +
				m_ITEM_HEIGHT + ")";

			return strSql;
		}

		/// <summary>
		/// update文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetUpdateQuery()
		{
			string strSql = "UPDATE " + TBL_IMG_CURSOR.TABLE_NAME + " SET " +
				TBL_IMG_CURSOR.ITEM_TOP + "=" + m_ITEM_TOP + ", " +
				TBL_IMG_CURSOR.ITEM_LEFT + "=" + m_ITEM_LEFT + ", " +
				TBL_IMG_CURSOR.ITEM_WIDTH + "=" + m_ITEM_WIDTH + ", " +
				TBL_IMG_CURSOR.ITEM_HEIGHT + "=" + m_ITEM_HEIGHT +
				" WHERE " +
				TBL_IMG_CURSOR.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_IMG_CURSOR.DSP_ID + "=" + m_DSP_ID + " AND " +
				TBL_IMG_CURSOR.ITEM_ID + "=" + m_ITEM_ID;

			return strSql;
		}

		#endregion

	}
}
