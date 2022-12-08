using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// 項目
	/// </summary>
	public class TBL_BAT_TRITEM
	{
        public TBL_BAT_TRITEM()
        {
        }

        public TBL_BAT_TRITEM(int gym_id, int bat_id, int details_no, int item_id, string scanner_id, int operation_date)
        {
            m_GYM_ID = gym_id;
            m_BAT_ID = bat_id;
            m_DETAILS_NO = details_no;
            m_ITEM_ID = item_id;
            m_SCANNER_ID = scanner_id;
            m_OPERATION_DATE = operation_date;
        }

		public TBL_BAT_TRITEM(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

  		public const string TABLE_NAME = "BAT_TRITEM";

		public const string GYM_ID = "GYM_ID";
		public const string OPERATION_DATE = "OPERATION_DATE";
		public const string SCANNER_ID = "SCANNER_ID";
		public const string BAT_ID = "BAT_ID";
		public const string IMAGE_NO = "IMAGE_NO";
		public const string DETAILS_NO = "DETAILS_NO";
		public const string ITEM_ID = "ITEM_ID";
		public const string ITEM_NAME = "ITEM_NAME";
		public const string OCRERR = "OCRERR";
		public const string ENTFLG = "ENTFLG";
		public const string VFYFLG = "VFYFLG";
		public const string ENDFLG = "ENDFLG";
		public const string OCR_DATA = "OCR_DATA";
		public const string ENT_DATA = "ENT_DATA";
		public const string VFY_DATA = "VFY_DATA";
		public const string END_DATA = "END_DATA";
		public const string CANNOT_KBN = "CANNOT_KBN";
		public const string VFY_UMU_FLG = "VFY_UMU_FLG";
		public const string ITEM_TOP = "ITEM_TOP";
		public const string ITEM_LEFT = "ITEM_LEFT";
		public const string ITEM_WIDTH = "ITEM_WIDTH";
		public const string ITEM_HEIGHT = "ITEM_HEIGHT";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_OPERATION_DATE = 0;
		private string m_SCANNER_ID = "";
		private int m_BAT_ID = 0;
		private int m_IMAGE_NO = 0;
		private int m_DETAILS_NO = 0;
		private int m_ITEM_ID = 0;
		public string m_ITEM_NAME = "";
		public string m_OCRERR = "";
		public string m_ENTFLG = "";
		public string m_VFYFLG = "";
		public string m_ENDFLG = "";
		public string m_OCR_DATA = "";
		public string m_ENT_DATA = "";
		public string m_VFY_DATA = "";
		public string m_END_DATA = "";
		public string m_CANNOT_KBN = "";
		public string m_VFY_UMU_FLG = "";
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

		public int _BAT_ID
		{
			get { return m_BAT_ID; }
		}

		public int _DETAILS_NO
		{
			get { return m_DETAILS_NO; }
		}

		public int _IMAGE_NO
		{
			get { return m_IMAGE_NO; }
		}

		public int _ITEM_ID
		{
			get { return m_ITEM_ID; }
		}

		public string _SCANNER_ID
		{
			get { return m_SCANNER_ID; }
		}

		public int _OPERATION_DATE
		{
			get { return m_OPERATION_DATE; }
		}

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.GYM_ID]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.OPERATION_DATE]);
			m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.SCANNER_ID]);
			m_BAT_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.BAT_ID]);
			m_IMAGE_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.IMAGE_NO]);
			m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.DETAILS_NO]);
			m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.ITEM_ID]);
			m_ITEM_NAME = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.ITEM_NAME]);
			m_OCRERR = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.OCRERR]);
			m_ENTFLG = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.ENTFLG]);
			m_VFYFLG = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.VFYFLG]);
			m_ENDFLG = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.ENDFLG]);
			m_OCR_DATA = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.OCR_DATA]);
			m_ENT_DATA = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.ENT_DATA]);
			m_VFY_DATA = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.VFY_DATA]);
			m_END_DATA = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.END_DATA]);
			m_CANNOT_KBN = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.CANNOT_KBN]);
			m_VFY_UMU_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRITEM.VFY_UMU_FLG]);
			m_ITEM_TOP = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.ITEM_TOP]);
			m_ITEM_LEFT = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.ITEM_LEFT]);
			m_ITEM_WIDTH = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.ITEM_WIDTH]);
			m_ITEM_HEIGHT = DBConvert.ToIntNull(dr[TBL_BAT_TRITEM.ITEM_HEIGHT]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// キーを条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <param name="bat_id">バッチ番号</param>
		/// <param name="details_no">明細番号</param>
		/// <param name="scanner_id">スキャナー号機</param>
		/// <param name="operation_date">処理日</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gym_id, long bat_id, int details_no, string scanner_id, int operation_date)
        {
			string strSql = "SELECT * FROM " + TBL_BAT_TRITEM.TABLE_NAME +
				" WHERE " + TBL_BAT_TRITEM.GYM_ID + "=" + gym_id +
				" AND " + TBL_BAT_TRITEM.OPERATION_DATE + "=" + operation_date +
				" AND " + TBL_BAT_TRITEM.SCANNER_ID + "='" + scanner_id + "'" +
				" AND " + TBL_BAT_TRITEM.BAT_ID + "=" + bat_id +
				" AND " + TBL_BAT_TRITEM.DETAILS_NO + "=" + details_no +
				" ORDER BY " + TBL_BAT_TRITEM.ITEM_ID;
			return strSql;
		}

		/// <summary>
		/// キーを条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <param name="bat_id">バッチ番号</param>
		/// <param name="details_no">明細番号</param>
		/// <param name="scanner_id">スキャナー号機</param>
		/// <param name="operation_date">処理日</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gym_id, int operation_date, string scanner_id, long bat_id, int imageno, int details_no)
		{
			string strSql = "SELECT * FROM " + TBL_BAT_TRITEM.TABLE_NAME +
				" WHERE " + TBL_BAT_TRITEM.GYM_ID + "=" + gym_id +
				" AND " + TBL_BAT_TRITEM.OPERATION_DATE + "=" + operation_date +
				" AND " + TBL_BAT_TRITEM.SCANNER_ID + "='" + scanner_id + "'" +
				" AND " + TBL_BAT_TRITEM.BAT_ID + "=" + bat_id +
				" AND " + TBL_BAT_TRITEM.IMAGE_NO + "=" + imageno +
				" AND " + TBL_BAT_TRITEM.DETAILS_NO + "=" + details_no +
				" ORDER BY " + TBL_BAT_TRITEM.ITEM_ID;
			return strSql;
		}

		/// <summary>
		/// キーを条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <param name="bat_id">バッチ番号</param>
		/// <param name="details_no">明細番号</param>
		/// <param name="item_id">項目番号</param>
		/// <param name="scanner_id">スキャナー号機</param>
		/// <param name="operation_date">処理日</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gym_id, long bat_id, int details_no, int item_id, string scanner_id, int operation_date)
        {
			string strSql = "SELECT * FROM " + TBL_BAT_TRITEM.TABLE_NAME +
				" WHERE " + TBL_BAT_TRITEM.GYM_ID + "=" + gym_id +
				" AND " + TBL_BAT_TRITEM.OPERATION_DATE + "=" + operation_date +
				" AND " + TBL_BAT_TRITEM.SCANNER_ID + "='" + scanner_id + "'" +
				" AND " + TBL_BAT_TRITEM.BAT_ID + "=" + bat_id +
				" AND " + TBL_BAT_TRITEM.DETAILS_NO + "=" + details_no +
				" AND " + TBL_BAT_TRITEM.ITEM_ID + "=" + item_id;
			return strSql;
		}

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
			string strSql = "INSERT INTO " + TBL_BAT_TRITEM.TABLE_NAME + " (" +
				TBL_BAT_TRITEM.GYM_ID + "," +
				TBL_BAT_TRITEM.OPERATION_DATE + "," +
				TBL_BAT_TRITEM.SCANNER_ID + "," +
				TBL_BAT_TRITEM.BAT_ID + "," +
				TBL_BAT_TRITEM.IMAGE_NO + "," +
				TBL_BAT_TRITEM.DETAILS_NO + "," +
				TBL_BAT_TRITEM.ITEM_ID + "," +
				TBL_BAT_TRITEM.ITEM_NAME + "," +
				TBL_BAT_TRITEM.OCRERR + "," +
				TBL_BAT_TRITEM.ENTFLG + "," +
				TBL_BAT_TRITEM.VFYFLG + "," +
				TBL_BAT_TRITEM.ENDFLG + "," +
				TBL_BAT_TRITEM.OCR_DATA + "," +
				TBL_BAT_TRITEM.ENT_DATA + "," +
				TBL_BAT_TRITEM.VFY_DATA + "," +
				TBL_BAT_TRITEM.END_DATA + "," +
				TBL_BAT_TRITEM.CANNOT_KBN + "," +
				TBL_BAT_TRITEM.VFY_UMU_FLG + "," +
				TBL_BAT_TRITEM.ITEM_TOP + "," +
				TBL_BAT_TRITEM.ITEM_LEFT + "," +
				TBL_BAT_TRITEM.ITEM_WIDTH + "," +
				TBL_BAT_TRITEM.ITEM_HEIGHT + ") VALUES (" +
				m_GYM_ID + "," +
				m_OPERATION_DATE + "," +
				"'" + m_SCANNER_ID + "'," +
				m_BAT_ID + "," +
				m_IMAGE_NO + "," +
				m_DETAILS_NO + "," +
				m_ITEM_ID + "," +
				"'" + m_ITEM_NAME + "'," +
				"'" + m_OCRERR + "'," +
				"'" + m_ENTFLG + "'," +
				"'" + m_VFYFLG + "'," +
				"'" + m_ENDFLG + "'," +
				"'" + m_OCR_DATA + "'," +
				"'" + m_ENT_DATA + "'," +
				"'" + m_VFY_DATA + "'," +
				"'" + m_END_DATA + "'," +
				"'" + m_CANNOT_KBN + "'," +
				"'" + m_VFY_UMU_FLG + "'," +
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
			string strSql = "UPDATE " + TBL_BAT_TRITEM.TABLE_NAME + " SET " +
				TBL_BAT_TRITEM.ITEM_NAME + "='" + m_ITEM_NAME + "', " +
				TBL_BAT_TRITEM.OCRERR + "='" + m_OCRERR + "', " +
				TBL_BAT_TRITEM.ENTFLG + "='" + m_ENTFLG + "', " +
				TBL_BAT_TRITEM.VFYFLG + "='" + m_VFYFLG + "', " +
				TBL_BAT_TRITEM.ENDFLG + "='" + m_ENDFLG + "', " +
				TBL_BAT_TRITEM.OCR_DATA + "='" + m_OCR_DATA + "', " +
				TBL_BAT_TRITEM.ENT_DATA + "='" + m_ENT_DATA + "', " +
				TBL_BAT_TRITEM.VFY_DATA + "='" + m_VFY_DATA + "', " +
				TBL_BAT_TRITEM.END_DATA + "='" + m_END_DATA + "', " +
				TBL_BAT_TRITEM.CANNOT_KBN + "='" + m_CANNOT_KBN + "', " +
				TBL_BAT_TRITEM.VFY_UMU_FLG + "='" + m_VFY_UMU_FLG + "', " +
				TBL_BAT_TRITEM.ITEM_TOP + "=" + m_ITEM_TOP + ", " +
				TBL_BAT_TRITEM.ITEM_LEFT + "=" + m_ITEM_LEFT + ", " +
				TBL_BAT_TRITEM.ITEM_WIDTH + "=" + m_ITEM_WIDTH + ", " +
				TBL_BAT_TRITEM.ITEM_HEIGHT + "=" + m_ITEM_HEIGHT +
				" WHERE " +
				TBL_BAT_TRITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_TRITEM.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
				TBL_BAT_TRITEM.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_TRITEM.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_TRITEM.IMAGE_NO + "=" + m_IMAGE_NO + " AND " +
				TBL_BAT_TRITEM.DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
				TBL_BAT_TRITEM.ITEM_ID + "=" + m_ITEM_ID;
			return strSql;
		}

		/// <summary>
		/// delete文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetDeleteQuery()
        {
			string strSql = "DELETE FROM " + TBL_BAT_TRITEM.TABLE_NAME +
				" WHERE " +
				TBL_BAT_TRITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_TRITEM.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
				TBL_BAT_TRITEM.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_TRITEM.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_TRITEM.IMAGE_NO + "=" + m_IMAGE_NO + " AND " +
				TBL_BAT_TRITEM.DETAILS_NO + "=" + m_DETAILS_NO;
			return strSql;
		}

		#endregion
	}
}
