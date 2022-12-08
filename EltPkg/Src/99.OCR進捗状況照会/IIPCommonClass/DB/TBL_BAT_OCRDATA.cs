using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// TBL_BAT_OCRDATA
	/// </summary>
	public class TBL_BAT_OCRDATA
	{
        public TBL_BAT_OCRDATA()
        {
        }

        public TBL_BAT_OCRDATA(int gym_id, int bat_id, int details_no, int dsp_no, int camera, int read_line, string field_name, string scanner_id, int operation_date)
        {
            m_GYM_ID = gym_id;
            m_BAT_ID = bat_id;
            m_DETAILS_NO = details_no;
            m_DSP_ID = dsp_no;
            m_CAMERA = camera;
            m_READ_LINE = read_line;
            m_FIELD_NAME = field_name;
            m_SCANNER_ID = scanner_id;
            m_OPERATION_DATE = operation_date;
        }

		public TBL_BAT_OCRDATA(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

  		public const string TABLE_NAME = "BAT_OCRDATA";

        public const string GYM_ID = "GYM_ID";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
		public const string DSP_ID = "DSP_ID";
		public const string CAMERA = "CAMERA";
		public const string READ_LINE = "READ_LINE";
		public const string FIELD_NAME = "FIELD_NAME";
		public const string FIELD_ORDER = "FIELD_ORDER";
		public const string OCR_READLINE = "OCR_READLINE";
        public const string SCANNER_ID = "SCANNER_ID";
		public const string OPERATION_DATE = "OPERATION_DATE";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_BAT_ID = 0;
		private int m_DETAILS_NO = 0;
		private int m_DSP_ID = 0;
		private int m_CAMERA = 0;
		private int m_READ_LINE = 0;
		private string m_FIELD_NAME = "";
		public int m_FIELD_ORDER = 0;
		public string m_OCR_READLINE = "";
		private string m_SCANNER_ID = "";
		private int m_OPERATION_DATE = 0;

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

        public int _DSP_ID
        {
            get { return m_DSP_ID; }
        }

        public int _CAMERA
        {
            get { return m_CAMERA; }
        }

        public int _READ_LINE
        {
            get { return m_READ_LINE; }
        }

        public string _FIELD_NAME
        {
            get { return m_FIELD_NAME; }
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
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BAT_OCRDATA.GYM_ID]);
			m_BAT_ID = DBConvert.ToIntNull(dr[TBL_BAT_OCRDATA.BAT_ID]);
			m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_BAT_OCRDATA.DETAILS_NO]);
			m_DSP_ID = DBConvert.ToIntNull(dr[TBL_BAT_OCRDATA.DSP_ID]);
			m_CAMERA = DBConvert.ToIntNull(dr[TBL_BAT_OCRDATA.CAMERA]);
			m_READ_LINE = DBConvert.ToIntNull(dr[TBL_BAT_OCRDATA.READ_LINE]);
			m_FIELD_NAME = DBConvert.ToStringNull(dr[TBL_BAT_OCRDATA.FIELD_NAME]);
			m_FIELD_ORDER = DBConvert.ToIntNull(dr[TBL_BAT_OCRDATA.FIELD_ORDER]);
			m_OCR_READLINE = DBConvert.ToStringNull(dr[TBL_BAT_OCRDATA.OCR_READLINE]);
			m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_BAT_OCRDATA.SCANNER_ID]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BAT_OCRDATA.OPERATION_DATE]);

        }

		#endregion

		#region クエリ取得

        /// <summary>
		/// キーを条件とするSELECT文を作成します
		/// </summary>
        /// <param name="gym_id">業務番号</param>
        /// <param name="bat_id">バッチ番号</param>
        /// <param name="details_no">明細番号</param>
        /// <param name="dsp_no">画面番号</param>
        /// <param name="camera">カメラ番号</param>
        /// <param name="read_line">読み取り行</param>
        /// <param name="field_name">項目名</param>
        /// <param name="scanner_id">スキャナー号機</param>
        /// <param name="operation_date">処理日</param>
        /// <returns></returns>
        public string GetSelectQuery(int gym_id, int bat_id, int details_no, int dsp_no, int camera, int read_line, string field_name, string scanner_id, int operation_date)
        {
            string strSql = "SELECT * FROM " + TBL_BAT_OCRDATA.TABLE_NAME +
                  " WHERE " + TBL_BAT_OCRDATA.GYM_ID + "=" + gym_id +
                  " AND " + TBL_BAT_OCRDATA.BAT_ID + "=" + bat_id +
                  " AND " + TBL_BAT_OCRDATA.DETAILS_NO + "=" + details_no +
                  " AND " + TBL_BAT_OCRDATA.DSP_ID + "=" + dsp_no +
                  " AND " + TBL_BAT_OCRDATA.CAMERA + "=" + camera +
                  " AND " + TBL_BAT_OCRDATA.READ_LINE + "=" + read_line +
                  " AND " + TBL_BAT_OCRDATA.FIELD_NAME + "='" + field_name + "'" +
                  " AND " + TBL_BAT_OCRDATA.SCANNER_ID + "='" + scanner_id + "'" +
                  " AND " + TBL_BAT_OCRDATA.OPERATION_DATE + "=" + operation_date;

            return strSql;
        }

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_BAT_OCRDATA.TABLE_NAME + " (" +
				TBL_BAT_OCRDATA.GYM_ID + "," + 
				TBL_BAT_OCRDATA.BAT_ID + "," + 
				TBL_BAT_OCRDATA.DSP_ID + "," +
				TBL_BAT_OCRDATA.CAMERA + "," +
				TBL_BAT_OCRDATA.READ_LINE + "," +
				TBL_BAT_OCRDATA.FIELD_NAME + "," +
				TBL_BAT_OCRDATA.FIELD_ORDER + "," +
				TBL_BAT_OCRDATA.OCR_READLINE + "," +
				TBL_BAT_OCRDATA.SCANNER_ID + "," + 
				TBL_BAT_OCRDATA.OPERATION_DATE + ") VALUES (" +
				m_GYM_ID + "," + 
				m_BAT_ID + "," + 
				m_DETAILS_NO + "," + 
				m_DSP_ID + "," + 
				m_CAMERA + "," + 
				m_READ_LINE + "," + 
				"'" + m_FIELD_NAME + "'," + 
				m_FIELD_ORDER + "," + 
				"'" + m_OCR_READLINE + "'," + 
				"'" + m_SCANNER_ID + "'," + 
				m_OPERATION_DATE + ")";

			return strSql;
		}

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BAT_OCRDATA.TABLE_NAME + " SET " +
				TBL_BAT_OCRDATA.FIELD_ORDER + "=" + m_FIELD_ORDER + ", " +
				TBL_BAT_OCRDATA.OCR_READLINE + "='" + m_OCR_READLINE + "' " +
				" WHERE " +
				TBL_BAT_OCRDATA.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_OCRDATA.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_OCRDATA.DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
                TBL_BAT_OCRDATA.DSP_ID + "=" + m_DSP_ID + " AND " +
                TBL_BAT_OCRDATA.CAMERA + "=" + m_CAMERA + " AND " +
                TBL_BAT_OCRDATA.READ_LINE + "=" + m_READ_LINE + " AND " +
                TBL_BAT_OCRDATA.FIELD_NAME + "='" + m_FIELD_NAME + "' AND " +
                TBL_BAT_OCRDATA.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_OCRDATA.OPERATION_DATE + "=" + m_OPERATION_DATE;

            return strSql;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_BAT_OCRDATA.TABLE_NAME + " WHERE " + TBL_BAT_OCRDATA.GYM_ID + "=" + m_GYM_ID;
            strSQL += " AND " + TBL_BAT_OCRDATA.BAT_ID + "=" + m_BAT_ID + " AND " + TBL_BAT_OCRDATA.DETAILS_NO + "=" + m_DETAILS_NO;
            strSQL += " AND " + TBL_BAT_OCRDATA.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " + TBL_BAT_OCRDATA.OPERATION_DATE + "=" + m_OPERATION_DATE;

            return strSQL;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQuery(TBL_BAT_TRDATA BTD)
        {
            string strSQL = "DELETE FROM " + TBL_BAT_OCRDATA.TABLE_NAME +
                " WHERE " + TBL_BAT_OCRDATA.GYM_ID + "=" + BTD._GYM_ID +
                " AND " + TBL_BAT_OCRDATA.BAT_ID + "=" + BTD._BAT_ID +
                " AND " + TBL_BAT_OCRDATA.DETAILS_NO + "=" + BTD._DETAILS_NO +
                " AND " + TBL_BAT_OCRDATA.SCANNER_ID + "='" + BTD._SCANNER_ID + "'" +
                " AND " + TBL_BAT_OCRDATA.OPERATION_DATE + "=" + BTD._OPERATION_DATE;
            return strSQL;
        }

        #endregion
    }
}
