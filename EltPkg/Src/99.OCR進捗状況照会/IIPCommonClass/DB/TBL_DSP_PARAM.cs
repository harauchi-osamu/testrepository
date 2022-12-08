using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// 画面パラメーター
	/// </summary>
	public class TBL_DSP_PARAM
	{
        public TBL_DSP_PARAM()
        {
        }

        public TBL_DSP_PARAM(int gym_id, int dsp_id)
        {
            m_GYM_ID = gym_id;
			m_DSP_ID = dsp_id;
        }

		public TBL_DSP_PARAM(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

		public const string TABLE_NAME = "DSP_PARAM";
		public const string GYM_ID = "GYM_ID";
		public const string DSP_ID = "DSP_ID";
		public const string DSP_NAME = "DSP_NAME";
		public const string PRE_DSP_ID = "PRE_DSP_ID";
		public const string NEXT_DSP_ID = "NEXT_DSP_ID";
		public const string CNT_FLG1 = "CNT_FLG1";
		public const string CNT_FLG2 = "CNT_FLG2";
		public const string CNT_FLG3 = "CNT_FLG3";
		public const string CNT_FLG4 = "CNT_FLG4";
		public const string CNT_FLG5 = "CNT_FLG5";
		public const string CNT_FLG6 = "CNT_FLG6";
		public const string CNT_FLG7 = "CNT_FLG7";
		public const string CNT_FLG8 = "CNT_FLG8";
		public const string CNT_FLG9 = "CNT_FLG9";
		public const string CNT_FLG10 = "CNT_FLG10";
		public const string OUT_FLG = "OUT_FLG";
		public const string MENU_FLG = "MENU_FLG";
		public const string FONT_SIZE = "FONT_SIZE";
		public const string DSP_SUBRTN = "DSP_SUBRTN";
		public const string DSP_WIDTH = "DSP_WIDTH";
		public const string DSP_HEIGHT = "DSP_HEIGHT";
		public const string GROUP_NO = "GROUP_NO";
		public const string IMG_TYPE = "IMG_TYPE";
		public const string OCR_NAME = "OCR_NAME";
		public const string LINENO_START = "LINENO_START";
		public const string LINENO_END = "LINENO_END";
		public const string DELETE_FLG = "DELETE_FLG";
		public const string CREATE_USER = "CREATE_USER";
		public const string CREATE_TIME = "CREATE_TIME";
		public const string UPDATE_USER = "UPDATE_USER";
		public const string UPDATE_TIME = "UPDATE_TIME";
		public const string TP_FLG = "TP_FLG";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_DSP_ID = 0;
		public string m_DSP_NAME = "";
		public int m_PRE_DSP_ID = 0;
		public int m_NEXT_DSP_ID = 0;
		public string m_CNT_FLG1 = "N";
		public string m_CNT_FLG2 = "N";
		public string m_CNT_FLG3 = "N";
		public string m_CNT_FLG4 = "N";
		public string m_CNT_FLG5 = "N";
		public string m_CNT_FLG6 = "N";
		public string m_CNT_FLG7 = "N";
		public string m_CNT_FLG8 = "N";
		public string m_CNT_FLG9 = "N";
		public string m_CNT_FLG10 = "N";
		public string m_OUT_FLG = "0";
		public string m_MENU_FLG = "0";
		public int m_FONT_SIZE = 12;
		public string m_DSP_SUBRTN = "";
		public int m_DSP_WIDTH = 0;
		public int m_DSP_HEIGHT = 0;
		public int m_GROUP_NO = 0;
		public string m_IMG_TYPE = "";
		public string m_OCR_NAME = "";
		public int m_LINENO_START = 0;
		public int m_LINENO_END = 0;
		public string m_DELETE_FLG = "";
		public string m_CREATE_USER = "";
		public string m_CREATE_TIME = "";
		public string m_UPDATE_USER = "";
		public string m_UPDATE_TIME = "";
		public string m_TP_FLG = "0";

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
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.GYM_ID]);
			m_DSP_ID = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.DSP_ID]);
			m_DSP_NAME = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.DSP_NAME]);
			m_PRE_DSP_ID = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.PRE_DSP_ID]);
			m_NEXT_DSP_ID = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.NEXT_DSP_ID]);
			m_CNT_FLG1 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG1]);
			m_CNT_FLG2 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG2]);
			m_CNT_FLG3 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG3]);
			m_CNT_FLG4 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG4]);
			m_CNT_FLG5 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG5]);
			m_CNT_FLG6 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG6]);
			m_CNT_FLG7 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG7]);
			m_CNT_FLG8 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG8]);
			m_CNT_FLG9 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG9]);
			m_CNT_FLG10 = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CNT_FLG10]);
			m_OUT_FLG = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.OUT_FLG]);
			m_MENU_FLG = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.MENU_FLG]);
			m_FONT_SIZE = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.FONT_SIZE]);
			m_DSP_SUBRTN = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.DSP_SUBRTN]);
			m_DSP_WIDTH = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.DSP_WIDTH]);
			m_DSP_HEIGHT = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.DSP_HEIGHT]);
			m_GROUP_NO = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.GROUP_NO]);
			m_IMG_TYPE = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.IMG_TYPE]);
			m_OCR_NAME = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.OCR_NAME]);
			m_LINENO_START = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.LINENO_START]);
			m_LINENO_END = DBConvert.ToIntNull(dr[TBL_DSP_PARAM.LINENO_END]);
			m_DELETE_FLG = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.DELETE_FLG]);
			m_CREATE_USER = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CREATE_USER]);
			m_CREATE_TIME = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.CREATE_TIME]);
			m_UPDATE_USER = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.UPDATE_USER]);
			m_UPDATE_TIME = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.UPDATE_TIME]);
			m_TP_FLG = DBConvert.ToStringNull(dr[TBL_DSP_PARAM.TP_FLG]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// データを全て取得するSELECT文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetSelectAllQuery()
		{
			string strSql = "SELECT * FROM " + TBL_DSP_PARAM.TABLE_NAME +
				" ORDER BY " + TBL_DSP_PARAM.DSP_ID;

			return strSql;
		}

		/// <summary>
		/// 業務番号を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <returns></returns>
		public string GetSelectQuery(int gymid)
		{
			string strSql = "SELECT * FROM " + TBL_DSP_PARAM.TABLE_NAME +
				" WHERE " + TBL_DSP_PARAM.GYM_ID + "=" + gymid +
				" ORDER BY " + TBL_DSP_PARAM.DSP_ID;

			return strSql;
		}

		/// <summary>
		/// 業務番号、帳票番号を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <param name="dspid">帳票番号</param>
		/// <returns></returns>
		public string GetSelectQuery(int gymid, int dspid)
		{
			string strSql = "SELECT * FROM " + TBL_DSP_PARAM.TABLE_NAME +
				" WHERE " + TBL_DSP_PARAM.GYM_ID + "=" + gymid +
				" AND " + TBL_DSP_PARAM.DSP_ID + "=" + dspid;

			return strSql;
		}

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
			string strSql = "INSERT INTO " + TBL_DSP_PARAM.TABLE_NAME + " (" +
				TBL_DSP_PARAM.GYM_ID + "," +
				TBL_DSP_PARAM.DSP_ID + "," +
				TBL_DSP_PARAM.DSP_NAME + "," +
				TBL_DSP_PARAM.PRE_DSP_ID + "," +
				TBL_DSP_PARAM.NEXT_DSP_ID + "," +
				TBL_DSP_PARAM.CNT_FLG1 + "," +
				TBL_DSP_PARAM.CNT_FLG2 + "," +
				TBL_DSP_PARAM.CNT_FLG3 + "," +
				TBL_DSP_PARAM.CNT_FLG4 + "," +
				TBL_DSP_PARAM.CNT_FLG5 + "," +
				TBL_DSP_PARAM.CNT_FLG6 + "," +
				TBL_DSP_PARAM.CNT_FLG7 + "," +
				TBL_DSP_PARAM.CNT_FLG8 + "," +
				TBL_DSP_PARAM.CNT_FLG9 + "," +
				TBL_DSP_PARAM.CNT_FLG10 + "," +
				TBL_DSP_PARAM.OUT_FLG + "," +
				TBL_DSP_PARAM.MENU_FLG + "," +
				TBL_DSP_PARAM.FONT_SIZE + "," +
				TBL_DSP_PARAM.DSP_SUBRTN + "," +
				TBL_DSP_PARAM.DSP_WIDTH + "," +
				TBL_DSP_PARAM.DSP_HEIGHT + "," +
				TBL_DSP_PARAM.GROUP_NO + "," +
				TBL_DSP_PARAM.IMG_TYPE + "," +
				TBL_DSP_PARAM.OCR_NAME + "," +
				TBL_DSP_PARAM.LINENO_START + "," +
				TBL_DSP_PARAM.LINENO_END + "," +
				TBL_DSP_PARAM.DELETE_FLG + "," +
				TBL_DSP_PARAM.CREATE_USER + "," +
				TBL_DSP_PARAM.CREATE_TIME + "," +
				TBL_DSP_PARAM.UPDATE_USER + "," +
				TBL_DSP_PARAM.UPDATE_TIME + "," +
				TBL_DSP_PARAM.TP_FLG + ") VALUES (" +
				m_GYM_ID + "," +
				m_DSP_ID + "," +
				"'" + m_DSP_NAME + "'," +
				m_PRE_DSP_ID + "," +
				m_NEXT_DSP_ID + "," +
				"'" + m_CNT_FLG1 + "'," +
				"'" + m_CNT_FLG2 + "'," +
				"'" + m_CNT_FLG3 + "'," +
				"'" + m_CNT_FLG4 + "'," +
				"'" + m_CNT_FLG5 + "'," +
				"'" + m_CNT_FLG6 + "'," +
				"'" + m_CNT_FLG7 + "'," +
				"'" + m_CNT_FLG8 + "'," +
				"'" + m_CNT_FLG9 + "'," +
				"'" + m_CNT_FLG10 + "'," +
				"'" + m_OUT_FLG + "'," +
				"'" + m_MENU_FLG + "'," +
				m_FONT_SIZE + "," +
				"'" + m_DSP_SUBRTN + "'," +
				m_DSP_WIDTH + "," +
				m_DSP_HEIGHT + "," +
				m_GROUP_NO + "," +
				"'" + m_IMG_TYPE + "'," +
				"'" + m_OCR_NAME + "'," +
				m_LINENO_START + "," +
				m_LINENO_END + "," +
				"'" + m_DELETE_FLG + "'," +
				"'" + m_CREATE_USER + "'," +
				"'" + m_CREATE_TIME + "'," +
				"'" + m_UPDATE_USER + "'," +
				"'" + m_UPDATE_TIME + "'," +
				"'" + m_TP_FLG + "')";

			return strSql;
		}

		/// <summary>
		/// update文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetUpdateQuery()
		{
			string strSql = "UPDATE " + TBL_DSP_PARAM.TABLE_NAME + " SET " +
				TBL_DSP_PARAM.DSP_NAME + "='" + m_DSP_NAME + "', " +
				TBL_DSP_PARAM.PRE_DSP_ID + "=" + m_PRE_DSP_ID + ", " +
				TBL_DSP_PARAM.NEXT_DSP_ID + "=" + m_NEXT_DSP_ID + ", " +
				TBL_DSP_PARAM.CNT_FLG1 + "='" + m_CNT_FLG1 + "', " +
				TBL_DSP_PARAM.CNT_FLG2 + "='" + m_CNT_FLG2 + "', " +
				TBL_DSP_PARAM.CNT_FLG3 + "='" + m_CNT_FLG3 + "', " +
				TBL_DSP_PARAM.CNT_FLG4 + "='" + m_CNT_FLG4 + "', " +
				TBL_DSP_PARAM.CNT_FLG5 + "='" + m_CNT_FLG5 + "', " +
				TBL_DSP_PARAM.CNT_FLG6 + "='" + m_CNT_FLG6 + "', " +
				TBL_DSP_PARAM.CNT_FLG7 + "='" + m_CNT_FLG7 + "', " +
				TBL_DSP_PARAM.CNT_FLG8 + "='" + m_CNT_FLG8 + "', " +
				TBL_DSP_PARAM.CNT_FLG9 + "='" + m_CNT_FLG9 + "', " +
				TBL_DSP_PARAM.CNT_FLG10 + "='" + m_CNT_FLG10 + "', " +
				TBL_DSP_PARAM.OUT_FLG + "='" + m_OUT_FLG + "', " +
				TBL_DSP_PARAM.MENU_FLG + "='" + m_MENU_FLG + "', " +
				TBL_DSP_PARAM.FONT_SIZE + "=" + m_FONT_SIZE + ", " +
				TBL_DSP_PARAM.DSP_SUBRTN + "='" + m_DSP_SUBRTN + "', " +
				TBL_DSP_PARAM.DSP_WIDTH + "=" + m_DSP_WIDTH + ", " +
				TBL_DSP_PARAM.DSP_HEIGHT + "=" + m_DSP_HEIGHT + ", " +
				TBL_DSP_PARAM.GROUP_NO + "=" + m_GROUP_NO + ", " +
				TBL_DSP_PARAM.IMG_TYPE + "='" + m_IMG_TYPE + "', " +
				TBL_DSP_PARAM.OCR_NAME + "='" + m_OCR_NAME + "', " +
				TBL_DSP_PARAM.LINENO_START + "=" + m_LINENO_START + ", " +
				TBL_DSP_PARAM.LINENO_END + "=" + m_LINENO_END + ", " +
				TBL_DSP_PARAM.DELETE_FLG + "='" + m_DELETE_FLG + "', " +
				TBL_DSP_PARAM.UPDATE_USER + "='" + m_UPDATE_USER + "', " +
				TBL_DSP_PARAM.UPDATE_TIME + "='" + m_UPDATE_TIME + "'" +
				TBL_DSP_PARAM.TP_FLG + "='" + m_TP_FLG +
				" WHERE " +
				TBL_DSP_PARAM.GYM_ID + "=" + m_GYM_ID + ", " +
				TBL_DSP_PARAM.DSP_ID + "=" + m_DSP_ID;
			return strSql;
		}

		#endregion

		/// <summary>
		/// m_CNT_FLG1～m_CNT_FLG10を返す。添数範囲外は""が返る
		/// </summary>
		/// <param name="idx">添数(1-10)</param>
		public string GetCntFlg(int idx)
        {
            string res;
            switch (idx)
            {
                case 1:
                    res = m_CNT_FLG1;
                    break;
                case 2:
                    res = m_CNT_FLG2;
                    break;
                case 3:
                    res = m_CNT_FLG3;
                    break;
                case 4:
                    res = m_CNT_FLG4;
                    break;
                case 5:
                    res = m_CNT_FLG5;
                    break;
                case 6:
                    res = m_CNT_FLG6;
                    break;
                case 7:
                    res = m_CNT_FLG7;
                    break;
                case 8:
                    res = m_CNT_FLG8;
                    break;
                case 9:
                    res = m_CNT_FLG9;
                    break;
                case 10:
                    res = m_CNT_FLG10;
                    break;
                default:
                    res = "";
                    break;
            }
            return res;
        }
	}
}
