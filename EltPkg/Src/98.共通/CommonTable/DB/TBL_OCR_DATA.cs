using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace CommonTable.DB
{
	/// <summary>
	/// TBL_BAT_OCRDATA
	/// </summary>
	public class TBL_OCR_DATA
    {
        public TBL_OCR_DATA()
        {
        }

        public TBL_OCR_DATA(int gymid, int inputroute, int operation_date, string img_dirname, string img_name, string field_name)
        {
            m_GYM_ID = gymid;
            m_INPUT_ROUTE = inputroute;
            m_OPERATION_DATE = operation_date;
            m_IMG_DIRNAME = img_dirname;
            m_IMG_NAME = img_name;
            m_FIELD_NAME = field_name;
        }

		public TBL_OCR_DATA(DataRow dr)
		{
			initializeByDataRow(dr);
		}


        #region Route定義

        public enum OCRInputRoute
        {
            Normal = 1,
            Futai = 2,
            Kijitu = 3,
            Other = 0,
        }

        #endregion

        #region テーブル定義

        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
		public const string TABLE_PHYSICAL_NAME = "OCR_DATA";

        public const string GYM_ID = "GYM_ID";
        public const string INPUT_ROUTE = "INPUT_ROUTE";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string IMG_DIRNAME = "IMG_DIRNAME";
        public const string IMG_NAME = "IMG_NAME";
		public const string FIELD_NAME = "FIELD_NAME";
        public const string CONFIDENCE = "CONFIDENCE";
        public const string OCR = "OCR";
        public const string ITEM_TOP = "ITEM_TOP";
        public const string ITEM_LEFT = "ITEM_LEFT";
        public const string ITEM_WIDTH = "ITEM_WIDTH";
        public const string ITEM_HEIGHT = "ITEM_HEIGHT";

        #endregion

        #region メンバ

        private int m_GYM_ID = 0;
        private int m_INPUT_ROUTE = 0;
        private int m_OPERATION_DATE = 0;
		private string m_IMG_DIRNAME = "";
        private string m_IMG_NAME = "";
        private string m_FIELD_NAME = "";
        public int m_CONFIDENCE = 0;
        public string m_OCR = "";
        public long m_ITEM_TOP = 0;
        public long m_ITEM_LEFT = 0;
        public long m_ITEM_WIDTH = 0;
        public long m_ITEM_HEIGHT = 0;

        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }
        public int _INPUT_ROUTE
        {
            get { return m_INPUT_ROUTE; }
        }
        public int _OPERATION_DATE
		{
            get { return m_OPERATION_DATE; }
        }
        public string _IMG_DIRNAME
        {
            get { return m_IMG_DIRNAME; }
        }

        public string _IMG_NAME
        {
            get { return m_IMG_NAME; }
        }

        public String _FIELD_NAME
        {
            get { return m_FIELD_NAME; }
        }

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_OCR_DATA.GYM_ID]);
            m_INPUT_ROUTE = DBConvert.ToIntNull(dr[TBL_OCR_DATA.INPUT_ROUTE]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_OCR_DATA.OPERATION_DATE]);
            m_IMG_DIRNAME = DBConvert.ToStringNull(dr[TBL_OCR_DATA.IMG_DIRNAME]);
            m_IMG_NAME = DBConvert.ToStringNull(dr[TBL_OCR_DATA.IMG_NAME]);
            m_FIELD_NAME = DBConvert.ToStringNull(dr[TBL_OCR_DATA.FIELD_NAME]);
            m_CONFIDENCE = DBConvert.ToIntNull(dr[TBL_OCR_DATA.CONFIDENCE]);
            m_OCR = DBConvert.ToStringNull(dr[TBL_OCR_DATA.OCR]);
            m_ITEM_TOP = DBConvert.ToLongNull(dr[TBL_OCR_DATA.ITEM_TOP]);
            m_ITEM_LEFT = DBConvert.ToLongNull(dr[TBL_OCR_DATA.ITEM_LEFT]);
            m_ITEM_WIDTH = DBConvert.ToLongNull(dr[TBL_OCR_DATA.ITEM_WIDTH]);
            m_ITEM_HEIGHT = DBConvert.ToLongNull(dr[TBL_OCR_DATA.ITEM_HEIGHT]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public  static string GetSelectQuery(int gymid, int inputroute, int operation_date, string img_dirname, string img_name, string field_name)
        {
            string strSql = "SELECT * FROM " + TBL_OCR_DATA.TABLE_NAME +
                  " WHERE " + TBL_OCR_DATA.GYM_ID + "=" + gymid +
                  " AND " + TBL_OCR_DATA.INPUT_ROUTE + "=" + inputroute +
                  " AND " + TBL_OCR_DATA.OPERATION_DATE + "=" + operation_date +
                  " AND " + TBL_OCR_DATA.IMG_DIRNAME + "='" + img_dirname + "' " + 
                  " AND " + TBL_OCR_DATA.IMG_NAME + "='" + img_name + "' " +
                  " AND " + TBL_OCR_DATA.FIELD_NAME + "='" + field_name + "' ";

            return strSql;
        }

        /// <summary>
        /// 処理日・ディレクトリ・イメージファイルを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryDirImgName(int gymid, int inputroute, int operation_date, string img_dirname, string img_name)
        {
            string strSql = "SELECT * FROM " + TBL_OCR_DATA.TABLE_NAME +
                  " WHERE " + TBL_OCR_DATA.GYM_ID + "=" + gymid +
                  " AND " + TBL_OCR_DATA.INPUT_ROUTE + "=" + inputroute +
                  " AND " + TBL_OCR_DATA.OPERATION_DATE + "=" + operation_date +
                  " AND " + TBL_OCR_DATA.IMG_DIRNAME + "='" + img_dirname + "' " +
                  " AND " + TBL_OCR_DATA.IMG_NAME + "='" + img_name + "' ";
            return strSql;
        }

        /// <summary>
        /// ディレクトリ・イメージファイルを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryDirImgName(int gymid, int inputroute, string img_dirname, string img_name)
        {
            string strSql = "SELECT * FROM " + TBL_OCR_DATA.TABLE_NAME +
                  " WHERE " + TBL_OCR_DATA.GYM_ID + "=" + gymid +
                  " AND " + TBL_OCR_DATA.INPUT_ROUTE + "=" + inputroute +
                  " AND " + TBL_OCR_DATA.IMG_DIRNAME + "='" + img_dirname + "' " +
                  " AND " + TBL_OCR_DATA.IMG_NAME + "='" + img_name + "' ";
            return strSql;
        }

        /// <summary>
        /// イメージファイルを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryImgName(int gymid, int inputroute, string img_name)
        {
            string strSql = "SELECT * FROM " + TBL_OCR_DATA.TABLE_NAME +
                  " WHERE " + TBL_OCR_DATA.GYM_ID + "=" + gymid +
                  " AND " + TBL_OCR_DATA.INPUT_ROUTE + "=" + inputroute +
                  " AND " + TBL_OCR_DATA.IMG_NAME + "='" + img_name + "' ";
            return strSql;
        }

        /// <summary>
        /// ファイル名・フィールド名を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryImgName(int gymid, int inputroute, int operation_date, string img_name, string field_name)
        {
            string strSql = "SELECT * FROM " + TBL_OCR_DATA.TABLE_NAME +
                  " WHERE " + TBL_OCR_DATA.GYM_ID + "=" + gymid +
                  " AND " + TBL_OCR_DATA.INPUT_ROUTE + "=" + inputroute +
                  " AND " + TBL_OCR_DATA.OPERATION_DATE + "=" + operation_date +
                  " AND " + TBL_OCR_DATA.IMG_NAME + "='" + img_name + "' " +
                  " AND " + TBL_OCR_DATA.FIELD_NAME + "='" + field_name + "' ";

            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_OCR_DATA.TABLE_NAME + " (" +
                TBL_OCR_DATA.GYM_ID + "," +
                TBL_OCR_DATA.INPUT_ROUTE + "," +
                TBL_OCR_DATA.OPERATION_DATE + "," +
                TBL_OCR_DATA.IMG_DIRNAME + "," +
                TBL_OCR_DATA.IMG_NAME + "," +
                TBL_OCR_DATA.FIELD_NAME + "," +
                TBL_OCR_DATA.CONFIDENCE + "," +
                TBL_OCR_DATA.OCR + "," +
                TBL_OCR_DATA.ITEM_TOP + "," +
                TBL_OCR_DATA.ITEM_LEFT + "," +
                TBL_OCR_DATA.ITEM_WIDTH + "," +
                TBL_OCR_DATA.ITEM_HEIGHT + ") VALUES (" +
                m_GYM_ID + "," +
                m_INPUT_ROUTE + "," +
                m_OPERATION_DATE + "," +
                "'" + m_IMG_DIRNAME + "'," +
                "'" + m_IMG_NAME + "'," +
                "'" + m_FIELD_NAME + "'," +
                m_CONFIDENCE + "," +
                "'" + m_OCR + "'," +
                m_ITEM_TOP + "," +
                m_ITEM_LEFT + "," +
                m_ITEM_WIDTH + "," +
                m_ITEM_HEIGHT + ")";

			return strSql;
		}

        /// <summary>
        /// 期日管理時のinsert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryKijitu()
        {
            string strSql = "INSERT INTO " + TBL_OCR_DATA.TABLE_NAME + " (" +
                TBL_OCR_DATA.GYM_ID + "," +
                TBL_OCR_DATA.INPUT_ROUTE + "," +
                TBL_OCR_DATA.OPERATION_DATE + "," +
                TBL_OCR_DATA.IMG_DIRNAME + "," +
                TBL_OCR_DATA.IMG_NAME + "," +
                TBL_OCR_DATA.FIELD_NAME + "," +
                TBL_OCR_DATA.CONFIDENCE + "," +
                TBL_OCR_DATA.OCR + ") VALUES (" +
                m_GYM_ID + "," +
                m_INPUT_ROUTE + "," +
                m_OPERATION_DATE + "," +
                "'" + m_IMG_DIRNAME + "'," +
                "'" + m_IMG_NAME + "'," +
                "'" + m_FIELD_NAME + "'," +
                m_CONFIDENCE + "," +
                "'" + m_OCR + "'" + ")";

            return strSql;
        }

        /// <summary>
        /// 期日管理時のdelete文を作成します
        /// 取込ルート・フォルダー・ファイル単位でのDELETE
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryKijitu(int gymid, int inputroute, int operation_date, string img_dirname, string img_name)
        {
            string strSql = "DELETE FROM " + TBL_OCR_DATA.TABLE_NAME +
                  " WHERE " + TBL_OCR_DATA.GYM_ID + "=" + gymid +
                  " AND " + TBL_OCR_DATA.INPUT_ROUTE + "=" + inputroute +
                  " AND " + TBL_OCR_DATA.OPERATION_DATE + "=" + operation_date +
                  " AND " + TBL_OCR_DATA.IMG_DIRNAME + "='" + img_dirname + "' " +
                  " AND " + TBL_OCR_DATA.IMG_NAME + "='" + img_name + "' ";
            return strSql;
        }

        #endregion
    }
}
