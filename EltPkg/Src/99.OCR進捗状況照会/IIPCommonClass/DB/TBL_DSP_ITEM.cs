using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
    /// 画面項目定義
	/// </summary>
	public class TBL_DSP_ITEM
	{
        public TBL_DSP_ITEM()
        {
        }
        public TBL_DSP_ITEM(int gym_id, int dsp_id, int item_id)
        {
            m_GYM_ID = gym_id;
            m_DSP_ID = dsp_id;
            m_ITEM_ID = item_id;
        }

		public TBL_DSP_ITEM(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

  		public const string TABLE_NAME = "DSP_ITEM";

		public const string GYM_ID = "GYM_ID";
		public const string DSP_ID = "DSP_ID";
		public const string ITEM_ID = "ITEM_ID";
		public const string ITEM_NAME = "ITEM_NAME";
		public const string ITEM_TYPE = "ITEM_TYPE";
		public const string ITEM_LEN = "ITEM_LEN";
		public const string POS = "POS";
		public const string VFY = "VFY";
		public const string REVFY = "REVFY";
		public const string AUTO_DUP = "AUTO_DUP";
		public const string DUP = "DUP";
		public const string AUTO_INPUT = "AUTO_INPUT";
		public const string NAME_POS_TOP = "NAME_POS_TOP";
		public const string NAME_POS_LEFT = "NAME_POS_LEFT";
		public const string INPUT_POS_TOP = "INPUT_POS_TOP";
		public const string INPUT_POS_LEFT = "INPUT_POS_LEFT";
		public const string INPUT_WIDTH = "INPUT_WIDTH";
		public const string INPUT_HEIGHT = "INPUT_HEIGHT";
		public const string SUM_PRF1 = "SUM_PRF1";
		public const string SUM_PRF2 = "SUM_PRF2";
		public const string SUM_PRF3 = "SUM_PRF3";
		public const string SUM_PRF4 = "SUM_PRF4";
		public const string SUM_PRF5 = "SUM_PRF5";
		public const string SUM_PRF6 = "SUM_PRF6";
		public const string SUM_PRF7 = "SUM_PRF7";
		public const string SUM_PRF8 = "SUM_PRF8";
		public const string SUM_PRF9 = "SUM_PRF9";
		public const string SUM_PRF10 = "SUM_PRF10";
		public const string ITEM_SUBRTN = "ITEM_SUBRTN";
		public const string DELETE_FLG = "DELETE_FLG";
		public const string BLANK_FLG = "BLANK_FLG";
		public const string DATA_FLG = "DATA_FLG";
		public const string CREATE_USER = "CREATE_USER";
		public const string CREATE_TIME = "CREATE_TIME";
		public const string UPDATE_USER = "UPDATE_USER";
		public const string UPDATE_TIME = "UPDATE_TIME";
		public const string ITEM_SET_POS = "ITEM_SET_POS";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_DSP_ID = 0;
		private int m_ITEM_ID = 0;
		public string m_ITEM_NAME = "";
		public string m_ITEM_TYPE = "";
		public int m_ITEM_LEN = 0;
		public int m_POS = 0;
		public string m_VFY = "";
		public string m_REVFY = "";
		public string m_AUTO_DUP = "";
		public string m_DUP = "";
		public string m_AUTO_INPUT = "";
		public int m_NAME_POS_TOP = 0;
		public int m_NAME_POS_LEFT = 0;
		public int m_INPUT_POS_TOP = 0;
		public int m_INPUT_POS_LEFT = 0;
		public int m_INPUT_WIDTH = 0;
		public int m_INPUT_HEIGHT = 0;
		public string m_SUM_PRF1 = "";
		public string m_SUM_PRF2 = "";
		public string m_SUM_PRF3 = "";
		public string m_SUM_PRF4 = "";
		public string m_SUM_PRF5 = "";
		public string m_SUM_PRF6 = "";
		public string m_SUM_PRF7 = "";
		public string m_SUM_PRF8 = "";
		public string m_SUM_PRF9 = "";
		public string m_SUM_PRF10 = "";
		public string m_ITEM_SUBRTN = "";
		public string m_DELETE_FLG = "";
		public string m_BLANK_FLG = "";
		public string m_DATA_FLG = "";
		public string m_CREATE_USER = "";
		public string m_CREATE_TIME = "";
		public string m_UPDATE_USER = "";
		public string m_UPDATE_TIME = "";
		public int m_ITEM_SET_POS = 0;

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

		public bool IsLabelItem
		{
			get { return m_ITEM_TYPE.Equals("*"); }
		}

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.GYM_ID]);
			m_DSP_ID = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.DSP_ID]);
			m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.ITEM_ID]);
			m_ITEM_NAME = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.ITEM_NAME]);
			m_ITEM_TYPE = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.ITEM_TYPE]);
			m_ITEM_LEN = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.ITEM_LEN]);
			m_POS = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.POS]);
			m_VFY = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.VFY]);
			m_REVFY = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.REVFY]);
			m_AUTO_DUP = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.AUTO_DUP]);
			m_DUP = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.DUP]);
			m_AUTO_INPUT = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.AUTO_INPUT]);
			m_NAME_POS_TOP = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.NAME_POS_TOP]);
			m_NAME_POS_LEFT = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.NAME_POS_LEFT]);
			m_INPUT_POS_TOP = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.INPUT_POS_TOP]);
			m_INPUT_POS_LEFT = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.INPUT_POS_LEFT]);
			m_INPUT_WIDTH = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.INPUT_WIDTH]);
			m_INPUT_HEIGHT = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.INPUT_HEIGHT]);
			m_SUM_PRF1 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF1]);
			m_SUM_PRF2 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF2]);
			m_SUM_PRF3 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF3]);
			m_SUM_PRF4 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF4]);
			m_SUM_PRF5 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF5]);
			m_SUM_PRF6 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF6]);
			m_SUM_PRF7 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF7]);
			m_SUM_PRF8 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF8]);
			m_SUM_PRF9 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF9]);
			m_SUM_PRF10 = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.SUM_PRF10]);
			m_ITEM_SUBRTN = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.ITEM_SUBRTN]);
			m_DELETE_FLG = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.DELETE_FLG]);
			m_BLANK_FLG = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.BLANK_FLG]);
			m_DATA_FLG = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.DATA_FLG]);
			m_CREATE_USER = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.CREATE_USER]);
			m_CREATE_TIME = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.CREATE_TIME]);
			m_UPDATE_USER = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.UPDATE_USER]);
			m_UPDATE_TIME = DBConvert.ToStringNull(dr[TBL_DSP_ITEM.UPDATE_TIME]);
			m_ITEM_SET_POS = DBConvert.ToIntNull(dr[TBL_DSP_ITEM.ITEM_SET_POS]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// データを全て取得するSELECT文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetSelectAllQuery()
		{
			string strSql = "SELECT * FROM " + TBL_DSP_ITEM.TABLE_NAME +
				" ORDER BY " + TBL_DSP_ITEM.ITEM_ID;

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
			string strSql = "SELECT * FROM " + TBL_DSP_ITEM.TABLE_NAME +
				" WHERE " + TBL_DSP_ITEM.GYM_ID + "=" + gymid +
				"   AND " + TBL_DSP_ITEM.DSP_ID + "=" + dspid +
				  " ORDER BY " + TBL_DSP_ITEM.ITEM_ID;

			return strSql;
		}

		/// <summary>
		/// 業務番号, 画面番号, 明細番号を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <param name="dspid">画面番号</param>
		/// <param name="itemid">明細番号</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gymid, int dspid, int itemid)
		{
			string strSql = "SELECT * FROM " + TBL_DSP_ITEM.TABLE_NAME +
				" WHERE " + TBL_DSP_ITEM.GYM_ID + "=" + gymid +
				"   AND " + TBL_DSP_ITEM.DSP_ID + "=" + dspid +
				  " AND " + TBL_DSP_ITEM.ITEM_ID + "=" + itemid;

			return strSql;
		}

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
			string strSql = "INSERT INTO " + TBL_DSP_ITEM.TABLE_NAME + " (" +
				TBL_DSP_ITEM.GYM_ID + "," +
				TBL_DSP_ITEM.DSP_ID + "," +
				TBL_DSP_ITEM.ITEM_ID + "," +
				TBL_DSP_ITEM.ITEM_NAME + "," +
				TBL_DSP_ITEM.ITEM_TYPE + "," +
				TBL_DSP_ITEM.ITEM_LEN + "," +
				TBL_DSP_ITEM.POS + "," +
				TBL_DSP_ITEM.VFY + "," +
				TBL_DSP_ITEM.REVFY + "," +
				TBL_DSP_ITEM.AUTO_DUP + "," +
				TBL_DSP_ITEM.DUP + "," +
				TBL_DSP_ITEM.AUTO_INPUT + "," +
				TBL_DSP_ITEM.NAME_POS_TOP + "," +
				TBL_DSP_ITEM.NAME_POS_LEFT + "," +
				TBL_DSP_ITEM.INPUT_POS_TOP + "," +
				TBL_DSP_ITEM.INPUT_POS_LEFT + "," +
				TBL_DSP_ITEM.INPUT_WIDTH + "," +
				TBL_DSP_ITEM.INPUT_HEIGHT + "," +
				TBL_DSP_ITEM.SUM_PRF1 + "," +
				TBL_DSP_ITEM.SUM_PRF2 + "," +
				TBL_DSP_ITEM.SUM_PRF3 + "," +
				TBL_DSP_ITEM.SUM_PRF4 + "," +
				TBL_DSP_ITEM.SUM_PRF5 + "," +
				TBL_DSP_ITEM.SUM_PRF6 + "," +
				TBL_DSP_ITEM.SUM_PRF7 + "," +
				TBL_DSP_ITEM.SUM_PRF8 + "," +
				TBL_DSP_ITEM.SUM_PRF9 + "," +
				TBL_DSP_ITEM.SUM_PRF10 + "," +
				TBL_DSP_ITEM.ITEM_SUBRTN + "," +
				TBL_DSP_ITEM.DELETE_FLG + "," +
				TBL_DSP_ITEM.BLANK_FLG + "," +
				TBL_DSP_ITEM.DATA_FLG + "," +
				TBL_DSP_ITEM.CREATE_USER + "," +
				TBL_DSP_ITEM.CREATE_TIME + "," +
				TBL_DSP_ITEM.UPDATE_USER + "," +
				TBL_DSP_ITEM.UPDATE_TIME + "," +
				TBL_DSP_ITEM.ITEM_SET_POS + ") VALUES (" +
				m_GYM_ID + "," +
				m_DSP_ID + "," +
				m_ITEM_ID + "," +
				"'" + m_ITEM_NAME + "'," +
				"'" + m_ITEM_TYPE + "'," +
				m_ITEM_LEN + "," +
				m_POS + "," +
				"'" + m_VFY + "'," +
				"'" + m_REVFY + "'," +
				"'" + m_AUTO_DUP + "'," +
				"'" + m_DUP + "'," +
				"'" + m_AUTO_INPUT + "'," +
				m_NAME_POS_TOP + "," +
				m_NAME_POS_LEFT + "," +
				m_INPUT_POS_TOP + "," +
				m_INPUT_POS_LEFT + "," +
				m_INPUT_WIDTH + "," +
				m_INPUT_HEIGHT + "," +
				"'" + m_SUM_PRF1 + "'," +
				"'" + m_SUM_PRF2 + "'," +
				"'" + m_SUM_PRF3 + "'," +
				"'" + m_SUM_PRF4 + "'," +
				"'" + m_SUM_PRF5 + "'," +
				"'" + m_SUM_PRF6 + "'," +
				"'" + m_SUM_PRF7 + "'," +
				"'" + m_SUM_PRF8 + "'," +
				"'" + m_SUM_PRF9 + "'," +
				"'" + m_SUM_PRF10 + "'," +
				"'" + m_ITEM_SUBRTN + "'," +
				"'" + m_DELETE_FLG + "'," +
				"'" + m_BLANK_FLG + "'," +
				"'" + m_DATA_FLG + "'," +
				"'" + m_CREATE_USER + "'," +
				"'" + m_CREATE_TIME + "'," +
				"'" + m_UPDATE_USER + "'," +
				"'" + m_UPDATE_TIME + "'," +
				m_ITEM_SET_POS + ")";
			return strSql;
		}

		/// <summary>
		/// update文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetUpdateQuery()
		{
			string strSql = "UPDATE " + TBL_DSP_ITEM.TABLE_NAME + " SET " +
				TBL_DSP_ITEM.ITEM_NAME + "='" + m_ITEM_NAME + "', " +
				TBL_DSP_ITEM.ITEM_TYPE + "='" + m_ITEM_TYPE + "', " +
				TBL_DSP_ITEM.ITEM_LEN + "=" + m_ITEM_LEN + ", " +
				TBL_DSP_ITEM.POS + "=" + m_POS + ", " +
				TBL_DSP_ITEM.VFY + "='" + m_VFY + "', " +
				TBL_DSP_ITEM.REVFY + "='" + m_REVFY + "', " +
				TBL_DSP_ITEM.AUTO_DUP + "='" + m_AUTO_DUP + "', " +
				TBL_DSP_ITEM.DUP + "='" + m_DUP + "', " +
				TBL_DSP_ITEM.AUTO_INPUT + "='" + m_AUTO_INPUT + "', " +
				TBL_DSP_ITEM.NAME_POS_TOP + "=" + m_NAME_POS_TOP + ", " +
				TBL_DSP_ITEM.NAME_POS_LEFT + "=" + m_NAME_POS_LEFT + ", " +
				TBL_DSP_ITEM.INPUT_POS_TOP + "=" + m_INPUT_POS_TOP + ", " +
				TBL_DSP_ITEM.INPUT_POS_LEFT + "=" + m_INPUT_POS_LEFT + ", " +
				TBL_DSP_ITEM.INPUT_WIDTH + "=" + m_INPUT_WIDTH + ", " +
				TBL_DSP_ITEM.INPUT_HEIGHT + "=" + m_INPUT_HEIGHT + ", " +
				TBL_DSP_ITEM.SUM_PRF1 + "='" + m_SUM_PRF1 + "', " +
				TBL_DSP_ITEM.SUM_PRF2 + "='" + m_SUM_PRF2 + "', " +
				TBL_DSP_ITEM.SUM_PRF3 + "='" + m_SUM_PRF3 + "', " +
				TBL_DSP_ITEM.SUM_PRF4 + "='" + m_SUM_PRF4 + "', " +
				TBL_DSP_ITEM.SUM_PRF5 + "='" + m_SUM_PRF5 + "', " +
				TBL_DSP_ITEM.SUM_PRF6 + "='" + m_SUM_PRF6 + "', " +
				TBL_DSP_ITEM.SUM_PRF7 + "='" + m_SUM_PRF7 + "', " +
				TBL_DSP_ITEM.SUM_PRF8 + "='" + m_SUM_PRF8 + "', " +
				TBL_DSP_ITEM.SUM_PRF9 + "='" + m_SUM_PRF9 + "', " +
				TBL_DSP_ITEM.SUM_PRF10 + "='" + m_SUM_PRF10 + "', " +
				TBL_DSP_ITEM.ITEM_SUBRTN + "='" + m_ITEM_SUBRTN + "', " +
				TBL_DSP_ITEM.DELETE_FLG + "='" + m_DELETE_FLG + "', " +
				TBL_DSP_ITEM.BLANK_FLG + "='" + m_BLANK_FLG + "', " +
				TBL_DSP_ITEM.DATA_FLG + "='" + m_DATA_FLG + "', " +
				TBL_DSP_ITEM.UPDATE_USER + "='" + m_UPDATE_USER + "', " +
				TBL_DSP_ITEM.UPDATE_TIME + "='" + m_UPDATE_TIME + "', " +
				TBL_DSP_ITEM.ITEM_SET_POS + "=" + m_ITEM_SET_POS +
				" WHERE " +
				TBL_DSP_ITEM.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_DSP_ITEM.DSP_ID + "=" + m_DSP_ID + " AND " +
				TBL_DSP_ITEM.ITEM_ID + "=" + m_ITEM_ID;
			return strSql;
		}

		#endregion

		/// <summary>
		/// m_SUM_PRF1～m_SUM_PRF10を返す。添数範囲外は""が返る
		/// </summary>
		/// <param name="idx">添数(1-10)</param>
		public string GetSumPrf(int idx)
        {
            string res;
            switch (idx)
            {
                case 1:
                    res = m_SUM_PRF1;
                    break;
                case 2:
                    res = m_SUM_PRF2;
                    break;
                case 3:
                    res = m_SUM_PRF3;
                    break;
                case 4:
                    res = m_SUM_PRF4;
                    break;
                case 5:
                    res = m_SUM_PRF5;
                    break;
                case 6:
                    res = m_SUM_PRF6;
                    break;
                case 7:
                    res = m_SUM_PRF7;
                    break;
                case 8:
                    res = m_SUM_PRF8;
                    break;
                case 9:
                    res = m_SUM_PRF9;
                    break;
                case 10:
                    res = m_SUM_PRF10;
                    break;
                default:
                    res = "";
                    break;
            }
            return res;
        }
    }
}
