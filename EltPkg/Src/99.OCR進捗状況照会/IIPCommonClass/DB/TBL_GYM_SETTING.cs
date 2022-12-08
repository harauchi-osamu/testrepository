using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// TBL_GYM_SETTING の概要の説明です。
	/// </summary>
	public class TBL_GYM_SETTING
	{
        /// <summary>
        /// データを渡された場合
        /// </summary>
        /// <param name="dr"></param>
        public TBL_GYM_SETTING()
        {
        }

		/// <summary>
		/// データを渡された場合
		/// </summary>
		/// <param name="dr"></param>
		public TBL_GYM_SETTING(DataRow dr)
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
			this.setDataRow(dr);
		}

        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "GYM_SETTING";
		//カラム
		public const string GYM_KBN = "GYM_KBN";
		public const string GYM_NAME = "GYM_NAME";
		public const string OPERATION_DATE = "OPERATION_DATE";
		public const string OPERATION_PRE_DATE = "OPERATION_PRE_DATE";
		public const string OPERATION_AFT_DATE = "OPERATION_AFT_DATE";
		public const string SORT_NO = "SORT_NO";
		public const string R_DATA_CLEAR_EXECFLG = "R_DATA_CLEAR_EXECFLG";
		public const string HOST_SEND_EXECFLG = "HOST_SEND_EXECFLG";
		public const string FILING_SEND_EXECFLG = "FILING_SEND_EXECFLG";
		public const string WEB_SEND_EXECFLG = "WEB_SEND_EXECFLG";
        public const string DATA_UPDATE_EXECFLG = "DATA_UPDATE_EXECFLG";

		private int m_GYM_KBN = 0;
		private string m_GYM_NAME = "";
		private int m_OPERATION_DATE = 0;
		public int m_OPERATION_PRE_DATE = 0;
		public int m_OPERATION_AFT_DATE = 0;
		public int m_SORT_NO = 0;
		public int m_R_DATA_CLEAR_EXECFLG = 0;
		public int m_HOST_SEND_EXECFLG = 0;
		public int m_FILING_SEND_EXECFLG = 0;
		public int m_WEB_SEND_EXECFLG = 0;
        public int m_DATA_UPDATE_EXECFLG = 0;


        public int _GYM_KBN
		{
			get { return m_GYM_KBN; }
		}

        public int _OPERATION_DATE
		{
			get { return m_OPERATION_DATE; }
		}

        public int _DATA_UPDATE_EXECFLG
        {
            get { return m_DATA_UPDATE_EXECFLG; }
        }

        private void setDataRow(DataRow dr)
		{
			m_GYM_KBN = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.GYM_KBN]);
			m_GYM_NAME = DBConvert.ToStringNull(dr[TBL_GYM_SETTING.GYM_NAME]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.OPERATION_DATE]);
			m_OPERATION_PRE_DATE = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.OPERATION_PRE_DATE]);
			m_OPERATION_AFT_DATE = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.OPERATION_AFT_DATE]);
			m_SORT_NO = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.SORT_NO]);
			m_R_DATA_CLEAR_EXECFLG = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.R_DATA_CLEAR_EXECFLG]);
			m_HOST_SEND_EXECFLG = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.HOST_SEND_EXECFLG]);
			m_FILING_SEND_EXECFLG = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.FILING_SEND_EXECFLG]);
			m_WEB_SEND_EXECFLG = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.WEB_SEND_EXECFLG]);
            m_DATA_UPDATE_EXECFLG = DBConvert.ToIntNull(dr[TBL_GYM_SETTING.DATA_UPDATE_EXECFLG]);
        }

		/// <summary>
		/// SELECT_QUERY取得
		/// </summary>
		/// <param name="gymkbn"></param>
		/// <returns></returns>
		public static string GetSelectQuery()
		{
			string strSql = "SELECT * FROM " + TBL_GYM_SETTING.TABLE_NAME +
				" ORDER BY " +
				TBL_GYM_SETTING.SORT_NO;
			return strSql;
		}

		/// <summary>
		/// SELECT_QUERY取得
		/// </summary>
		/// <param name="gymkbn"></param>
		/// <returns></returns>
		public static string GetSelectQuery(int gymkbn)
		{
			string strSql = "SELECT * FROM " + TBL_GYM_SETTING.TABLE_NAME +
				" WHERE " +
				TBL_GYM_SETTING.GYM_KBN + "=" + gymkbn;
			return strSql;
		}

    }
}
