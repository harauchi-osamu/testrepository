using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// 明細
	/// </summary>
	public class TBL_BAT_TRDATA
	{
        public TBL_BAT_TRDATA()
        {
        }

        public TBL_BAT_TRDATA(int gym_id, int bat_id, int details_no, string scanner_id, int operation_date)
        {
            m_GYM_ID = gym_id;
            m_BAT_ID = bat_id;
            m_DETAILS_NO = details_no;
            m_SCANNER_ID = scanner_id;
            m_OPERATION_DATE = operation_date;
        }

		public TBL_BAT_TRDATA(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

  		public const string TABLE_NAME = "BAT_TRDATA";

		public const string GYM_ID = "GYM_ID";
		public const string OPERATION_DATE = "OPERATION_DATE";
		public const string SCANNER_ID = "SCANNER_ID";
		public const string BAT_ID = "BAT_ID";
		public const string IMAGE_NO = "IMAGE_NO";
		public const string DETAILS_NO = "DETAILS_NO";
		public const string PRE_DETAILS_NO = "PRE_DETAILS_NO";
		public const string DSP_ID = "DSP_ID";
		public const string ITN_NO = "ITN_NO";
		public const string LINE_NO = "LINE_NO";
		public const string OCR_ID = "OCR_ID";
		public const string UNREC_FLG = "UNREC_FLG";
		public const string CANNOT_KBN = "CANNOT_KBN";
		public const string DATA_STS = "DATA_STS";
		public const string COUNT = "COUNT";
		public const string AMOUNT = "AMOUNT";
		public const string PRFDATA = "PRFDATA";
		public const string VFY_UMU_FLG = "VFY_UMU_FLG";
		public const string BATCH_NO = "BATCH_NO";
		public const string SEQ_NO = "SEQ_NO";
		public const string GRP_NO = "GRP_NO";
		public const string BATCH_KBN = "BATCH_KBN";
		public const string GYOMU_KBN = "GYOMU_KBN";
		public const string KAIJI = "KAIJI";
		public const string FILE_NO = "FILE_NO";
		public const string PAGE_NO = "PAGE_NO";
		public const string E_FLG = "E_FLG";
		public const string E_TMNO = "E_TMNO";
		public const string E_OPEN = "E_OPEN";
		public const string E_STIME = "E_STIME";
		public const string E_ETIME = "E_ETIME";
		public const string E_YMD = "E_YMD";
		public const string E_INP_CNT = "E_INP_CNT";
		public const string E_UPD_CNT = "E_UPD_CNT";
		public const string E_DEL_CNT = "E_DEL_CNT";
		public const string E_TIME = "E_TIME";
		public const string E_TOUCH = "E_TOUCH";
		public const string V_FLG = "V_FLG";
		public const string V_TMNO = "V_TMNO";
		public const string V_OPEN = "V_OPEN";
		public const string V_STIME = "V_STIME";
		public const string V_ETIME = "V_ETIME";
		public const string V_YMD = "V_YMD";
		public const string V_CNT = "V_CNT";
		public const string V_UPD_CNT = "V_UPD_CNT";
		public const string V_TIME = "V_TIME";
		public const string V_TOUCH = "V_TOUCH";
		public const string C_FLG = "C_FLG";
		public const string C_TMNO = "C_TMNO";
		public const string C_OPEN = "C_OPEN";
		public const string C_STIME = "C_STIME";
		public const string C_ETIME = "C_ETIME";
		public const string C_YMD = "C_YMD";
		public const string C_CNT = "C_CNT";
		public const string C_UPD_CNT = "C_UPD_CNT";
		public const string C_TIME = "C_TIME";
		public const string C_TOUCH = "C_TOUCH";
		public const string VFY_CNT = "VFY_CNT";
		public const string SAISATSU_FLG = "SAISATSU_FLG";
		public const string DEL_FLG = "DEL_FLG";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_OPERATION_DATE = 0;
		private string m_SCANNER_ID = "";
		private int m_BAT_ID = 0;
		private int m_IMAGE_NO = 0;
		private int m_DETAILS_NO = 0;
		public int m_PRE_DETAILS_NO = 0;
		public int m_DSP_ID = 0;
		public int m_ITN_NO = 0;
		public int m_LINE_NO = 0;
		public string m_OCR_ID = "";
		public string m_UNREC_FLG = "";
		public string m_CANNOT_KBN = "";
		public int m_DATA_STS = 0;
		public int m_COUNT = 0;
		public int m_AMOUNT = 0;
		public string m_PRFDATA = "";
		public string m_VFY_UMU_FLG = "";
		public int m_BATCH_NO = 0;
		public int m_SEQ_NO = 0;
		public int m_GRP_NO = 0;
		public string m_BATCH_KBN = "";
		public int m_GYOMU_KBN = 0;
		public int m_KAIJI = 0;
		public int m_FILE_NO = 0;
		public int m_PAGE_NO = 0;
		public string m_E_FLG = "";
		public string m_E_TMNO = "";
		public string m_E_OPEN = "";
		public int m_E_STIME = 0;
		public int m_E_ETIME = 0;
		public int m_E_YMD = 0;
		public int m_E_INP_CNT = 0;
		public int m_E_UPD_CNT = 0;
		public int m_E_DEL_CNT = 0;
		public int m_E_TIME = 0;
		public int m_E_TOUCH = 0;
		public string m_V_FLG = "";
		public string m_V_TMNO = "";
		public string m_V_OPEN = "";
		public int m_V_STIME = 0;
		public int m_V_ETIME = 0;
		public int m_V_YMD = 0;
		public int m_V_CNT = 0;
		public int m_V_UPD_CNT = 0;
		public int m_V_TIME = 0;
		public int m_V_TOUCH = 0;
		public string m_C_FLG = "";
		public string m_C_TMNO = "";
		public string m_C_OPEN = "";
		public int m_C_STIME = 0;
		public int m_C_ETIME = 0;
		public int m_C_YMD = 0;
		public int m_C_CNT = 0;
		public int m_C_UPD_CNT = 0;
		public int m_C_TIME = 0;
		public int m_C_TOUCH = 0;
		public int m_VFY_CNT = 0;
		public string m_SAISATSU_FLG = "";
		public string m_DEL_FLG = "";

		#endregion

		#region プロパティ

		public int _GYM_ID
		{
            get { return m_GYM_ID; }
        }

		public int _OPERATION_DATE
		{
			get { return m_OPERATION_DATE; }
		}

		public string _SCANNER_ID
		{
			get { return m_SCANNER_ID; }
		}

		public int _BAT_ID
		{
			get { return m_BAT_ID; }
		}

		public int _IMAGE_NO
		{
			get { return m_IMAGE_NO; }
		}

		public int _DETAILS_NO
		{
			get { return m_DETAILS_NO; }
		}

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.GYM_ID]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.OPERATION_DATE]);
			m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.SCANNER_ID]);
			m_BAT_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.BAT_ID]);
			m_IMAGE_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.IMAGE_NO]);
			m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.DETAILS_NO]);
			m_PRE_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.PRE_DETAILS_NO]);
			m_DSP_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.DSP_ID]);
			m_ITN_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.ITN_NO]);
			m_LINE_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.LINE_NO]);
			m_OCR_ID = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.OCR_ID]);
			m_UNREC_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.UNREC_FLG]);
			m_CANNOT_KBN = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.CANNOT_KBN]);
			m_DATA_STS = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.DATA_STS]);
			m_COUNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.COUNT]);
			m_AMOUNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.AMOUNT]);
			m_PRFDATA = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.PRFDATA]);
			m_VFY_UMU_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.VFY_UMU_FLG]);
			m_BATCH_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.BATCH_NO]);
			m_SEQ_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.SEQ_NO]);
			m_GRP_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.GRP_NO]);
			m_BATCH_KBN = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.BATCH_KBN]);
			m_GYOMU_KBN = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.GYOMU_KBN]);
			m_KAIJI = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.KAIJI]);
			m_FILE_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.FILE_NO]);
			m_PAGE_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.PAGE_NO]);
			m_E_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.E_FLG]);
			m_E_TMNO = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.E_TMNO]);
			m_E_OPEN = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.E_OPEN]);
			m_E_STIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.E_STIME]);
			m_E_ETIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.E_ETIME]);
			m_E_YMD = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.E_YMD]);
			m_E_INP_CNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.E_INP_CNT]);
			m_E_UPD_CNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.E_UPD_CNT]);
			m_E_DEL_CNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.E_DEL_CNT]);
			m_E_TIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.E_TIME]);
			m_E_TOUCH = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.E_TOUCH]);
			m_V_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.V_FLG]);
			m_V_TMNO = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.V_TMNO]);
			m_V_OPEN = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.V_OPEN]);
			m_V_STIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.V_STIME]);
			m_V_ETIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.V_ETIME]);
			m_V_YMD = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.V_YMD]);
			m_V_CNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.V_CNT]);
			m_V_UPD_CNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.V_UPD_CNT]);
			m_V_TIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.V_TIME]);
			m_V_TOUCH = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.V_TOUCH]);
			m_C_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.C_FLG]);
			m_C_TMNO = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.C_TMNO]);
			m_C_OPEN = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.C_OPEN]);
			m_C_STIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.C_STIME]);
			m_C_ETIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.C_ETIME]);
			m_C_YMD = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.C_YMD]);
			m_C_CNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.C_CNT]);
			m_C_UPD_CNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.C_UPD_CNT]);
			m_C_TIME = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.C_TIME]);
			m_C_TOUCH = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.C_TOUCH]);
			m_VFY_CNT = DBConvert.ToIntNull(dr[TBL_BAT_TRDATA.VFY_CNT]);
			m_SAISATSU_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.SAISATSU_FLG]);
			m_DEL_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRDATA.DEL_FLG]);
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
		public static string GetSelectQuery(int gym_id, int bat_id, int details_no, string scanner_id, int operation_date)
        {
			string strSql = "SELECT * FROM " + TBL_BAT_TRDATA.TABLE_NAME +
				  " WHERE " +
					TBL_BAT_TRDATA.GYM_ID + "=" + gym_id + " AND " +
					TBL_BAT_TRDATA.OPERATION_DATE + "=" + operation_date + " AND " +
					TBL_BAT_TRDATA.SCANNER_ID + "='" + scanner_id + "' AND " +
					TBL_BAT_TRDATA.BAT_ID + "=" + bat_id + " AND " +
					TBL_BAT_TRDATA.DETAILS_NO + "=" + details_no;
			return strSql;
		}

		/// <summary>
		/// 明細番号とスキャナIDを除いたキーを条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <param name="bat_id">バッチ番号</param>
		/// <param name="operation_date">処理日</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gym_id, int bat_id, int operation_date)
        {
			string strSql = "SELECT * FROM " + TBL_BAT_TRDATA.TABLE_NAME +
				  " WHERE " +
					TBL_BAT_TRDATA.GYM_ID + "=" + gym_id + " AND " +
					TBL_BAT_TRDATA.OPERATION_DATE + "=" + operation_date + " AND " +
					TBL_BAT_TRDATA.BAT_ID + "=" + bat_id;
			return strSql;
		}

		/// <summary>
		/// 明細番号を除いたキーを条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <param name="bat_id">バッチ番号</param>
		/// <param name="scanner_id">スキャナー号機</param>
		/// <param name="operation_date">処理日</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gym_id, int bat_id, string scanner_id, int operation_date)
        {
			string strSql = "SELECT * FROM " + TBL_BAT_TRDATA.TABLE_NAME +
				  " WHERE " +
					TBL_BAT_TRDATA.GYM_ID + "=" + gym_id + " AND " +
					TBL_BAT_TRDATA.OPERATION_DATE + "=" + operation_date + " AND " +
					TBL_BAT_TRDATA.SCANNER_ID + "='" + scanner_id + "' AND " +
					TBL_BAT_TRDATA.BAT_ID + "=" + bat_id +
					" ORDER BY " + TBL_BAT_TRDATA.DETAILS_NO;
			return strSql;
		}

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
			string strSql = "INSERT INTO " + TBL_BAT_TRDATA.TABLE_NAME + " (" +
				TBL_BAT_TRDATA.GYM_ID + "," +
				TBL_BAT_TRDATA.OPERATION_DATE + "," +
				TBL_BAT_TRDATA.SCANNER_ID + "," +
				TBL_BAT_TRDATA.BAT_ID + "," +
				TBL_BAT_TRDATA.IMAGE_NO + "," +
				TBL_BAT_TRDATA.DETAILS_NO + "," +
				TBL_BAT_TRDATA.PRE_DETAILS_NO + "," +
				TBL_BAT_TRDATA.DSP_ID + "," +
				TBL_BAT_TRDATA.ITN_NO + "," +
				TBL_BAT_TRDATA.LINE_NO + "," +
				TBL_BAT_TRDATA.OCR_ID + "," +
				TBL_BAT_TRDATA.UNREC_FLG + "," +
				TBL_BAT_TRDATA.CANNOT_KBN + "," +
				TBL_BAT_TRDATA.DATA_STS + "," +
				TBL_BAT_TRDATA.COUNT + "," +
				TBL_BAT_TRDATA.AMOUNT + "," +
				TBL_BAT_TRDATA.PRFDATA + "," +
				TBL_BAT_TRDATA.VFY_UMU_FLG + "," +
				TBL_BAT_TRDATA.BATCH_NO + "," +
				TBL_BAT_TRDATA.SEQ_NO + "," +
				TBL_BAT_TRDATA.GRP_NO + "," +
				TBL_BAT_TRDATA.BATCH_KBN + "," +
				TBL_BAT_TRDATA.GYOMU_KBN + "," +
				TBL_BAT_TRDATA.KAIJI + "," +
				TBL_BAT_TRDATA.FILE_NO + "," +
				TBL_BAT_TRDATA.PAGE_NO + "," +
				TBL_BAT_TRDATA.E_FLG + "," +
				TBL_BAT_TRDATA.E_TMNO + "," +
				TBL_BAT_TRDATA.E_OPEN + "," +
				TBL_BAT_TRDATA.E_STIME + "," +
				TBL_BAT_TRDATA.E_ETIME + "," +
				TBL_BAT_TRDATA.E_YMD + "," +
				TBL_BAT_TRDATA.E_INP_CNT + "," +
				TBL_BAT_TRDATA.E_UPD_CNT + "," +
				TBL_BAT_TRDATA.E_DEL_CNT + "," +
				TBL_BAT_TRDATA.E_TIME + "," +
				TBL_BAT_TRDATA.E_TOUCH + "," +
				TBL_BAT_TRDATA.V_FLG + "," +
				TBL_BAT_TRDATA.V_TMNO + "," +
				TBL_BAT_TRDATA.V_OPEN + "," +
				TBL_BAT_TRDATA.V_STIME + "," +
				TBL_BAT_TRDATA.V_ETIME + "," +
				TBL_BAT_TRDATA.V_YMD + "," +
				TBL_BAT_TRDATA.V_CNT + "," +
				TBL_BAT_TRDATA.V_UPD_CNT + "," +
				TBL_BAT_TRDATA.V_TIME + "," +
				TBL_BAT_TRDATA.V_TOUCH + "," +
				TBL_BAT_TRDATA.C_FLG + "," +
				TBL_BAT_TRDATA.C_TMNO + "," +
				TBL_BAT_TRDATA.C_OPEN + "," +
				TBL_BAT_TRDATA.C_STIME + "," +
				TBL_BAT_TRDATA.C_ETIME + "," +
				TBL_BAT_TRDATA.C_YMD + "," +
				TBL_BAT_TRDATA.C_CNT + "," +
				TBL_BAT_TRDATA.C_UPD_CNT + "," +
				TBL_BAT_TRDATA.C_TIME + "," +
				TBL_BAT_TRDATA.C_TOUCH + "," +
				TBL_BAT_TRDATA.VFY_CNT + "," +
				TBL_BAT_TRDATA.SAISATSU_FLG + "," +
				TBL_BAT_TRDATA.DEL_FLG + ") VALUES (" +
				m_GYM_ID + "," +
				m_OPERATION_DATE + "," +
				"'" + m_SCANNER_ID + "'," +
				m_BAT_ID + "," +
				m_IMAGE_NO + "," +
				m_DETAILS_NO + "," +
				m_PRE_DETAILS_NO + "," +
				m_DSP_ID + "," +
				m_ITN_NO + "," +
				m_LINE_NO + "," +
				"'" + m_OCR_ID + "'," +
				"'" + m_UNREC_FLG + "'," +
				"'" + m_CANNOT_KBN + "'," +
				m_DATA_STS + "," +
				m_COUNT + "," +
				m_AMOUNT + "," +
				"'" + m_PRFDATA + "'," +
				"'" + m_VFY_UMU_FLG + "'," +
				m_BATCH_NO + "," +
				m_SEQ_NO + "," +
				m_GRP_NO + "," +
				"'" + m_BATCH_KBN + "'," +
				m_GYOMU_KBN + "," +
				m_KAIJI + "," +
				m_FILE_NO + "," +
				m_PAGE_NO + "," +
				"'" + m_E_FLG + "'," +
				"'" + m_E_TMNO + "'," +
				"'" + m_E_OPEN + "'," +
				m_E_STIME + "," +
				m_E_ETIME + "," +
				m_E_YMD + "," +
				m_E_INP_CNT + "," +
				m_E_UPD_CNT + "," +
				m_E_DEL_CNT + "," +
				m_E_TIME + "," +
				m_E_TOUCH + "," +
				"'" + m_V_FLG + "'," +
				"'" + m_V_TMNO + "'," +
				"'" + m_V_OPEN + "'," +
				m_V_STIME + "," +
				m_V_ETIME + "," +
				m_V_YMD + "," +
				m_V_CNT + "," +
				m_V_UPD_CNT + "," +
				m_V_TIME + "," +
				m_V_TOUCH + "," +
				"'" + m_C_FLG + "'," +
				"'" + m_C_TMNO + "'," +
				"'" + m_C_OPEN + "'," +
				m_C_STIME + "," +
				m_C_ETIME + "," +
				m_C_YMD + "," +
				m_C_CNT + "," +
				m_C_UPD_CNT + "," +
				m_C_TIME + "," +
				m_C_TOUCH + "," +
				m_VFY_CNT + "," +
				"'" + m_SAISATSU_FLG + "'," +
				"'" + m_DEL_FLG + ")";
			return strSql;
		}

		/// <summary>
		/// delete文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetDeleteQuery()
        {
			string strSql = "DELETE FROM " + TBL_BAT_TRDATA.TABLE_NAME +
				" WHERE " +
				TBL_BAT_TRDATA.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_TRDATA.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
				TBL_BAT_TRDATA.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_TRDATA.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_TRDATA.IMAGE_NO + "=" + m_IMAGE_NO + " AND " +
				TBL_BAT_TRDATA.DETAILS_NO + "=" + m_DETAILS_NO;
			return strSql;
		}

		#endregion
	}
}
