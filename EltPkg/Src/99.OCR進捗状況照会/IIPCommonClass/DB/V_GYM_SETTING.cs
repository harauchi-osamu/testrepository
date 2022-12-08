using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// V_GYM_SETTING の概要の説明です。
	/// </summary>
	public class V_GYM_SETTING
	{
        /// <summary>
        /// データを渡された場合
        /// </summary>
        /// <param name="dr"></param>
        public V_GYM_SETTING()
        {
        }

		/// <summary>
		/// データを渡された場合
		/// </summary>
		/// <param name="dr"></param>
		public V_GYM_SETTING(DataRow dr)
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
			this.setDataRow(dr);
		}

		#region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "V_GYM_SETTING";
		//カラム
		public const string GYM_ID = "GYM_ID";
		public const string GYM_KANA = "GYM_KANA";
		public const string GYM_KANJI = "GYM_KANJI";
		public const string GROUP_NO = "GROUP_NO";
		public const string OPERATIONDATE_DIFFDAYS = "OPERATIONDATE_DIFFDAYS";
		public const string BUSINESSDATE_DIFFDAYS = "BUSINESSDATE_DIFFDAYS";
		public const string IMAGE_FLG = "IMAGE_FLG";
		public const string METHOD = "METHOD";
		public const string PAPER_TYPE = "PAPER_TYPE";
		public const string ENTRY_MODE = "ENTRY_MODE";
		public const string FST_DSP_ID = "FST_DSP_ID";
		public const string HST_FLG = "HST_FLG";
		public const string FILING_FLG = "FILING_FLG";
		public const string WEB_FLG = "WEB_FLG";
		public const string SAVE_DAYS = "SAVE_DAYS";
		public const string WEB_SAVE_MONTH = "WEB_SAVE_MONTH";
		public const string OUT_FLG = "OUT_FLG";
		public const string OUT_GYM_ID = "OUT_GYM_ID";
		public const string OUT_GYM_KANA = "OUT_GYM_KANA";
		public const string OUT_GYM_KANJI = "OUT_GYM_KANJI";
		public const string OUT_LEN1 = "OUT_LEN1";
		public const string OUT_FILE = "OUT_FILE";
		public const string OUT_SUBRTN = "OUT_SUBRTN";
		public const string BACK_COLOR = "BACK_COLOR";
		public const string BAT_AUTO_SET = "BAT_AUTO_SET";
		public const string DONE_FLG = "DONE_FLG";
		public const string CREATE_USER = "CREATE_USER";
		public const string CREATE_TIME = "CREATE_TIME";
		public const string UPDATE_USER = "UPDATE_USER";
		public const string UPDATE_TIME = "UPDATE_TIME";
		public const string DATE_UPDATE_EXECFLG = "DATE_UPDATE_EXECFLG";
		public const string DATA_CLEAR_EXECFLG = "DATA_CLEAR_EXECFLG";
		public const string OPERATION_DATE = "OPERATION_DATE";
		public const string OPERATION_PRE_DATE = "OPERATION_PRE_DATE";
		public const string OPERATION_AFT_DATE = "OPERATION_AFT_DATE";
		public const string BUSINESS_DATE = "BUSINESS_DATE";
		public const string BUSINESS_PRE_DATE = "BUSINESS_PRE_DATE";
		public const string BUSINESS_AFT_DATE = "BUSINESS_AFT_DATE";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		public string m_GYM_KANA = "";
		public string m_GYM_KANJI = "";
		public int m_GROUP_NO = 0;
		public int m_OPERATIONDATE_DIFFDAYS = 0;
		public int m_BUSINESSDATE_DIFFDAYS = 0;
		public int m_IMAGE_FLG = 0;
		public int m_METHOD = 0;
		public int m_PAPER_TYPE = 0;
		public int m_ENTRY_MODE = 0;
		public int m_FST_DSP_ID = 0;
		public int m_HST_FLG = 0;
		public int m_FILING_FLG = 0;
		public int m_WEB_FLG = 0;
		public int m_SAVE_DAYS = 0;
		public int m_WEB_SAVE_MONTH = 0;
		public string m_OUT_FLG = "";
		public int m_OUT_GYM_ID = 0;
		public string m_OUT_GYM_KANA = "";
		public string m_OUT_GYM_KANJI = "";
		public int m_OUT_LEN1 = 0;
		public string m_OUT_FILE = "";
		public string m_OUT_SUBRTN = "";
		public string m_BACK_COLOR = "";
		public string m_BAT_AUTO_SET = "";
		public string m_DONE_FLG = "";
		public string m_CREATE_USER = "";
		public string m_CREATE_TIME = "";
		public string m_UPDATE_USER = "";
		public string m_UPDATE_TIME = "";
		public int m_DATE_UPDATE_EXECFLG = 0;
		public int m_DATA_CLEAR_EXECFLG = 0;
		public int m_OPERATION_DATE = 0;
		public int m_OPERATION_PRE_DATE = 0;
		public int m_OPERATION_AFT_DATE = 0;
		public int m_BUSINESS_DATE = 0;
		public int m_BUSINESS_PRE_DATE = 0;
		public int m_BUSINESS_AFT_DATE = 0;

		#endregion

		#region プロパティ

		/// <summary>
		/// 業務番号
		/// </summary>
		public int _GYM_ID
        {
			get{ return m_GYM_ID; }
		}


		/// <summary>
		/// 業務名称
		/// </summary>
		public string _GYM_KANJI
        {
			get{ return m_GYM_KANJI; }
		}
        public int SODATE
        {
            get { return m_OPERATION_DATE; }
        }
        public int SODATE_PRE
        {
            get { return m_OPERATION_PRE_DATE; }
        }
        public int SODATE_AFT
        {
            get { return m_OPERATION_AFT_DATE; }
        }
        public int BUSINESSDATE
        {
            get { return m_BUSINESS_DATE; }
        }
        public int BUSINESSDATE_PRE
        {
            get { return m_BUSINESS_PRE_DATE; }
        }
        public int BUSINESSDATE_AFT
        {
            get { return m_BUSINESS_AFT_DATE; }
        }
        public int AUTH_KIND
        {
            set { m_GYM_ID =value; }
            get { return m_GYM_ID; }
        }
        /// <summary>
        /// 背景色
        /// </summary>
        public string _BACK_COLOR
        {
            get { return m_BACK_COLOR; }
            set { m_BACK_COLOR = value; }
        }
        #endregion



        #region データセット

        private void setDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[V_GYM_SETTING.GYM_ID]);
			m_GYM_KANA = DBConvert.ToStringNull(dr[V_GYM_SETTING.GYM_KANA]);
			m_GYM_KANJI = DBConvert.ToStringNull(dr[V_GYM_SETTING.GYM_KANJI]);
			m_GROUP_NO = DBConvert.ToIntNull(dr[V_GYM_SETTING.GROUP_NO]);
			m_OPERATIONDATE_DIFFDAYS = DBConvert.ToIntNull(dr[V_GYM_SETTING.OPERATIONDATE_DIFFDAYS]);
			m_BUSINESSDATE_DIFFDAYS = DBConvert.ToIntNull(dr[V_GYM_SETTING.BUSINESSDATE_DIFFDAYS]);
			m_IMAGE_FLG = DBConvert.ToIntNull(dr[V_GYM_SETTING.IMAGE_FLG]);
			m_METHOD = DBConvert.ToIntNull(dr[V_GYM_SETTING.METHOD]);
			m_PAPER_TYPE = DBConvert.ToIntNull(dr[V_GYM_SETTING.PAPER_TYPE]);
			m_ENTRY_MODE = DBConvert.ToIntNull(dr[V_GYM_SETTING.ENTRY_MODE]);
			m_FST_DSP_ID = DBConvert.ToIntNull(dr[V_GYM_SETTING.FST_DSP_ID]);
			m_HST_FLG = DBConvert.ToIntNull(dr[V_GYM_SETTING.HST_FLG]);
			m_FILING_FLG = DBConvert.ToIntNull(dr[V_GYM_SETTING.FILING_FLG]);
			m_WEB_FLG = DBConvert.ToIntNull(dr[V_GYM_SETTING.WEB_FLG]);
			m_SAVE_DAYS = DBConvert.ToIntNull(dr[V_GYM_SETTING.SAVE_DAYS]);
			m_WEB_SAVE_MONTH = DBConvert.ToIntNull(dr[V_GYM_SETTING.WEB_SAVE_MONTH]);
			m_OUT_FLG = DBConvert.ToStringNull(dr[V_GYM_SETTING.OUT_FLG]);
			m_OUT_GYM_ID = DBConvert.ToIntNull(dr[V_GYM_SETTING.OUT_GYM_ID]);
			m_OUT_GYM_KANA = DBConvert.ToStringNull(dr[V_GYM_SETTING.OUT_GYM_KANA]);
			m_OUT_GYM_KANJI = DBConvert.ToStringNull(dr[V_GYM_SETTING.OUT_GYM_KANJI]);
			m_OUT_LEN1 = DBConvert.ToIntNull(dr[V_GYM_SETTING.OUT_LEN1]);
			m_OUT_FILE = DBConvert.ToStringNull(dr[V_GYM_SETTING.OUT_FILE]);
			m_OUT_SUBRTN = DBConvert.ToStringNull(dr[V_GYM_SETTING.OUT_SUBRTN]);
			m_BACK_COLOR = DBConvert.ToStringNull(dr[V_GYM_SETTING.BACK_COLOR]);
			m_BAT_AUTO_SET = DBConvert.ToStringNull(dr[V_GYM_SETTING.BAT_AUTO_SET]);
			m_DONE_FLG = DBConvert.ToStringNull(dr[V_GYM_SETTING.DONE_FLG]);
			m_CREATE_USER = DBConvert.ToStringNull(dr[V_GYM_SETTING.CREATE_USER]);
			m_CREATE_TIME = DBConvert.ToStringNull(dr[V_GYM_SETTING.CREATE_TIME]);
			m_UPDATE_USER = DBConvert.ToStringNull(dr[V_GYM_SETTING.UPDATE_USER]);
			m_UPDATE_TIME = DBConvert.ToStringNull(dr[V_GYM_SETTING.UPDATE_TIME]);
			m_DATE_UPDATE_EXECFLG = DBConvert.ToIntNull(dr[V_GYM_SETTING.DATE_UPDATE_EXECFLG]);
			m_DATA_CLEAR_EXECFLG = DBConvert.ToIntNull(dr[V_GYM_SETTING.DATA_CLEAR_EXECFLG]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[V_GYM_SETTING.OPERATION_DATE]);
			m_OPERATION_PRE_DATE = DBConvert.ToIntNull(dr[V_GYM_SETTING.OPERATION_PRE_DATE]);
			m_OPERATION_AFT_DATE = DBConvert.ToIntNull(dr[V_GYM_SETTING.OPERATION_AFT_DATE]);
			m_BUSINESS_DATE = DBConvert.ToIntNull(dr[V_GYM_SETTING.BUSINESS_DATE]);
			m_BUSINESS_PRE_DATE = DBConvert.ToIntNull(dr[V_GYM_SETTING.BUSINESS_PRE_DATE]);
			m_BUSINESS_AFT_DATE = DBConvert.ToIntNull(dr[V_GYM_SETTING.BUSINESS_AFT_DATE]);
		}

		/// <summary>
		/// SELECT_QUERY取得
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gym_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT *");
            sb.Append(" FROM " + TABLE_NAME);
            sb.Append(" WHERE " + GYM_ID + " = " + gym_id);
            return sb.ToString();
        }

        public static string GetSelectGymQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT *");
            sb.Append(" FROM " + TABLE_NAME);
            sb.Append(" WHERE " + GYM_ID + " <>0 ");
            return sb.ToString();
        }
        public static string GetSelectScanGymQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT *");
            sb.Append(" FROM " + TABLE_NAME);
            sb.Append(" WHERE " + GYM_ID + " <>0 ");
            sb.Append(" AND " + GROUP_NO + " =1 ");
            
            return sb.ToString();
        }
        #endregion
    }
}
