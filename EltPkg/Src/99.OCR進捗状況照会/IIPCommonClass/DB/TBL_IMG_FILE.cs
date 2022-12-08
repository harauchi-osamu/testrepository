using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// イメージファイル
	/// </summary>
	public class TBL_IMG_FILE
	{
        public TBL_IMG_FILE()
        {
        }
        public TBL_IMG_FILE(int gym_id, int dsp_id)
        {
            m_GYM_ID = gym_id;
			m_DSP_ID = dsp_id;
		}

		public TBL_IMG_FILE(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

  		public const string TABLE_NAME = "IMG_FILE";

		public const string GYM_ID = "GYM_ID";
		public const string DSP_ID = "DSP_ID";
		public const string IMG_FILE = "IMG_FILE";
		public const string REDUCE_RATE = "REDUCE_RATE";
		public const string IMG_TOP = "IMG_TOP";
		public const string IMG_LEFT = "IMG_LEFT";
		public const string PIC_WIDTH = "PIC_WIDTH";
		public const string PIC_HEIGHT = "PIC_HEIGHT";
		public const string GROUP_NO = "GROUP_NO";
		public const string IMG_BASE_POINT = "IMG_BASE_POINT";
		public const string IMG_INC_START = "IMG_INC_START";
		public const string IMG_INC_END = "IMG_INC_END";
		public const string XSCROLL_LEFT = "XSCROLL_LEFT";
		public const string XSCROLL_VALUE = "XSCROLL_VALUE";
		public const string XSCROLL_RIGHT = "XSCROLL_RIGHT";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_DSP_ID = 0;
		public string m_IMG_FILE = "";
		public int m_REDUCE_RATE = 100;
		public int m_IMG_TOP = 5;
		public int m_IMG_LEFT = 5;
		public int m_PIC_WIDTH = 600;
		public int m_PIC_HEIGHT = 775;
		public int m_GROUP_NO = 0;
		public int m_IMG_BASE_POINT = 0;
		public int m_IMG_INC_START = 0;
		public int m_IMG_INC_END = 0;
		public int m_XSCROLL_LEFT = 0;
		public int m_XSCROLL_VALUE = 0;
		public int m_XSCROLL_RIGHT = 0;

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

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_IMG_FILE.GYM_ID]);
			m_DSP_ID = DBConvert.ToIntNull(dr[TBL_IMG_FILE.DSP_ID]);
			m_IMG_FILE = DBConvert.ToStringNull(dr[TBL_IMG_FILE.IMG_FILE]);
			m_REDUCE_RATE = DBConvert.ToIntNull(dr[TBL_IMG_FILE.REDUCE_RATE]);
			m_IMG_TOP = DBConvert.ToIntNull(dr[TBL_IMG_FILE.IMG_TOP]);
			m_IMG_LEFT = DBConvert.ToIntNull(dr[TBL_IMG_FILE.IMG_LEFT]);
			m_PIC_WIDTH = DBConvert.ToIntNull(dr[TBL_IMG_FILE.PIC_WIDTH]);
			m_PIC_HEIGHT = DBConvert.ToIntNull(dr[TBL_IMG_FILE.PIC_HEIGHT]);
			m_GROUP_NO = DBConvert.ToIntNull(dr[TBL_IMG_FILE.GROUP_NO]);
			m_IMG_BASE_POINT = DBConvert.ToIntNull(dr[TBL_IMG_FILE.IMG_BASE_POINT]);
			m_IMG_INC_START = DBConvert.ToIntNull(dr[TBL_IMG_FILE.IMG_INC_START]);
			m_IMG_INC_END = DBConvert.ToIntNull(dr[TBL_IMG_FILE.IMG_INC_END]);
			m_XSCROLL_LEFT = DBConvert.ToIntNull(dr[TBL_IMG_FILE.XSCROLL_LEFT]);
			m_XSCROLL_VALUE = DBConvert.ToIntNull(dr[TBL_IMG_FILE.XSCROLL_VALUE]);
			m_XSCROLL_RIGHT = DBConvert.ToIntNull(dr[TBL_IMG_FILE.XSCROLL_RIGHT]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// データを全て取得するSELECT文を作成します
		/// </summary>
		/// <returns></returns>
		public static string GetSelectAllQuery()
		{
			string strSql = "SELECT * FROM " + TBL_IMG_FILE.TABLE_NAME +
				" ORDER BY " + TBL_IMG_FILE.DSP_ID;

			return strSql;
		}

		/// <summary>
		/// 業務番号、市町村・水道事業コードを条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <param name="gymid">市町村・水道事業</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gymid)
		{
			string strSql = "SELECT * FROM " + TBL_IMG_FILE.TABLE_NAME +
				  " WHERE " + TBL_IMG_FILE.GYM_ID + "=" + gymid +
				  " ORDER BY " + TBL_IMG_FILE.DSP_ID;

			return strSql;
		}

		/// <summary>
		/// 業務番号を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <param name="dspid">画面番号</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gymid, int dspid)
		{
			string strSql = "SELECT * FROM " + TBL_IMG_FILE.TABLE_NAME +
				  " WHERE " + TBL_IMG_FILE.GYM_ID + "=" + gymid +
				  " AND " + TBL_IMG_FILE.DSP_ID + "=" + dspid;

			return strSql;
		}

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
			string strSql = "INSERT INTO " + TBL_IMG_FILE.TABLE_NAME + " (" +
				TBL_IMG_FILE.GYM_ID + "," +
				TBL_IMG_FILE.DSP_ID + "," +
				TBL_IMG_FILE.IMG_FILE + "," +
				TBL_IMG_FILE.REDUCE_RATE + "," +
				TBL_IMG_FILE.IMG_TOP + "," +
				TBL_IMG_FILE.IMG_LEFT + "," +
				TBL_IMG_FILE.PIC_WIDTH + "," +
				TBL_IMG_FILE.PIC_HEIGHT + "," +
				TBL_IMG_FILE.GROUP_NO + "," +
				TBL_IMG_FILE.IMG_BASE_POINT + "," +
				TBL_IMG_FILE.IMG_INC_START + "," +
				TBL_IMG_FILE.IMG_INC_END + "," +
				TBL_IMG_FILE.XSCROLL_LEFT + "," +
				TBL_IMG_FILE.XSCROLL_VALUE + "," +
				TBL_IMG_FILE.XSCROLL_RIGHT + ") VALUES (" +
				m_GYM_ID + "," +
				m_DSP_ID + "," +
				"'" + m_IMG_FILE + "'," +
				m_REDUCE_RATE + "," +
				m_IMG_TOP + "," +
				m_IMG_LEFT + "," +
				m_PIC_WIDTH + "," +
				m_PIC_HEIGHT + "," +
				m_GROUP_NO + "," +
				m_IMG_BASE_POINT + "," +
				m_IMG_INC_START + "," +
				m_IMG_INC_END + "," +
				m_XSCROLL_LEFT + "," +
				m_XSCROLL_VALUE + "," +
				m_XSCROLL_RIGHT + ")";

			return strSql;
		}

		/// <summary>
		/// update文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetUpdateQuery()
		{
			string strSql = "UPDATE " + TBL_IMG_FILE.TABLE_NAME + " SET " +
				TBL_IMG_FILE.IMG_FILE + "='" + m_IMG_FILE + "', " +
				TBL_IMG_FILE.REDUCE_RATE + "=" + m_REDUCE_RATE + ", " +
				TBL_IMG_FILE.IMG_TOP + "=" + m_IMG_TOP + ", " +
				TBL_IMG_FILE.IMG_LEFT + "=" + m_IMG_LEFT + ", " +
				TBL_IMG_FILE.PIC_WIDTH + "=" + m_PIC_WIDTH + ", " +
				TBL_IMG_FILE.PIC_HEIGHT + "=" + m_PIC_HEIGHT + ", " +
				TBL_IMG_FILE.GROUP_NO + "=" + m_GROUP_NO + ", " +
				TBL_IMG_FILE.IMG_BASE_POINT + "=" + m_IMG_BASE_POINT + ", " +
				TBL_IMG_FILE.IMG_INC_START + "=" + m_IMG_INC_START + ", " +
				TBL_IMG_FILE.IMG_INC_END + "=" + m_IMG_INC_END + ", " +
				TBL_IMG_FILE.XSCROLL_LEFT + "=" + m_XSCROLL_LEFT + ", " +
				TBL_IMG_FILE.XSCROLL_VALUE + "=" + m_XSCROLL_VALUE + ", " +
				TBL_IMG_FILE.XSCROLL_RIGHT + "=" + m_XSCROLL_RIGHT +
				" WHERE " +
				TBL_IMG_FILE.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_IMG_FILE.DSP_ID + "=" + m_DSP_ID;

			return strSql;
		}

		#endregion

	}
}
