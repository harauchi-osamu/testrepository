using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// イメージ
	/// </summary>
	public class TBL_BAT_TRIMAGE
    {
        #region 定数定義
        public enum ROTATE_STATUS
        {
            /// <summary>0°回転（回転なし）</summary>
            ROTATE_0 = 0,
            /// <summary>90°回転</summary>
            ROTATE_90 = 1,
            /// <summary>180°回転</summary>
            ROTATE_180 = 2,
            /// <summary>270°回転</summary>
            ROTATE_270 = 3
        }
        #endregion

        #region テーブル定義
        public const string TABLE_NAME = "BAT_TRIMAGE";

		public const string GYM_ID = "GYM_ID";
		public const string OPERATION_DATE = "OPERATION_DATE";
		public const string SCANNER_ID = "SCANNER_ID";
		public const string BAT_ID = "BAT_ID";
		public const string IMAGE_NO = "IMAGE_NO";
		public const string IMG_FLG = "IMG_FLG";
		public const string IMG_FLNM = "IMG_FLNM";
		public const string IMG_FLNM2 = "IMG_FLNM2";
		public const string IMG_FLNM3 = "IMG_FLNM3";
		public const string IMG_FLNM4 = "IMG_FLNM4";
		public const string IMG_FLNM5 = "IMG_FLNM5";
		public const string IMG_FLNM6 = "IMG_FLNM6";
		public const string NUMBERING = "NUMBERING";
		public const string BARCODE_ID = "BARCODE_ID";
		public const string PKT_NO = "PKT_NO";
		public const string ROTATE = "ROTATE";
		public const string DSP_ID = "DSP_ID";
		public const string SEAl_RATE = "SEAl_RATE";
		public const string NUM01 = "NUM01";
		public const string NUM02 = "NUM02";
		public const string NUM03 = "NUM03";
		public const string CHAR01 = "CHAR01";
		public const string CHAR02 = "CHAR02";
		public const string CHAR03 = "CHAR03";
		public const string TODOKEDE_FLG = "TODOKEDE_FLG";
		public const string INNKAN_SHOGO_FLG = "INNKAN_SHOGO_FLG";
		public const string HOST_TOROKU_FLG = "HOST_TOROKU_FLG";
		public const string SAISATSU_FLG = "SAISATSU_FLG";
		public const string DEL_FLG = "DEL_FLG";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_OPERATION_DATE = 0;
		private string m_SCANNER_ID = "";
		private int m_BAT_ID = 0;
		private int m_IMAGE_NO = 0;
		public string m_IMG_FLG = "";
		public string m_IMG_FLNM = "";
		public string m_IMG_FLNM2 = "";
		public string m_IMG_FLNM3 = "";
		public string m_IMG_FLNM4 = "";
		public string m_IMG_FLNM5 = "";
		public string m_IMG_FLNM6 = "";
		public string m_NUMBERING = "";
		public string m_BARCODE_ID = "";
		public int m_PKT_NO = 0;
		public int m_ROTATE = 0;
		public int m_DSP_ID = 0;
		public int m_SEAl_RATE = 0;
		public int m_NUM01 = 0;
		public int m_NUM02 = 0;
		public int m_NUM03 = 0;
		public string m_CHAR01 = "";
		public string m_CHAR02 = "";
		public string m_CHAR03 = "";
		public string m_TODOKEDE_FLG = "";
		public string m_INNKAN_SHOGO_FLG = "";
		public string m_HOST_TOROKU_FLG = "";
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

		public long _BAT_ID
		{
			get { return m_BAT_ID; }
		}

		public int _IMAGE_NO
		{
			get { return m_IMAGE_NO; }
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TBL_BAT_TRIMAGE()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <param name="batid">バッチ番号</param>
        /// <param name="detailsno">明細番号</param>
        /// <param name="scannerid">スキャナ名</param>
        /// <param name="oparationdate">処理日(OPARATION_DATE)</param>
        /// <param name="imgfile">イメージファイル</param>
        /// <param name="rotateinfo">回転情報</param>
		/// <param name="dspId">画面ID</param>
        public TBL_BAT_TRIMAGE(int gymid, int batid, int detailsno, string scannerid, int oparationdate, string imgfile, int rotateinfo, int dspId)
        {
			m_GYM_ID = gymid;
            m_BAT_ID = batid;
            m_SCANNER_ID = scannerid;
            m_OPERATION_DATE = oparationdate;
            m_IMG_FLNM = imgfile;
            m_ROTATE = rotateinfo;
			m_DSP_ID = dspId;
        }

        /// <summary>
        /// コンストラクタ（DataRow指定）
        /// 更新前の再確認など、既存のレコードを取得して格納する場合に使用
        /// </summary>
        /// <param name="dr">DataRow(Table:BAT_IMGROTATE)</param>
        public TBL_BAT_TRIMAGE(DataRow dr)
        {
            initializeByDataRow(dr);
        }
        #endregion

        #region 初期化
        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.GYM_ID]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.OPERATION_DATE]);
			m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.SCANNER_ID]);
			m_BAT_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.BAT_ID]);
			m_IMAGE_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.IMAGE_NO]);
			m_IMG_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.IMG_FLG]);
			m_IMG_FLNM = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.IMG_FLNM]);
			m_IMG_FLNM2 = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.IMG_FLNM2]);
			m_IMG_FLNM3 = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.IMG_FLNM3]);
			m_IMG_FLNM4 = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.IMG_FLNM4]);
			m_IMG_FLNM5 = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.IMG_FLNM5]);
			m_IMG_FLNM6 = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.IMG_FLNM6]);
			m_NUMBERING = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.NUMBERING]);
			m_BARCODE_ID = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.BARCODE_ID]);
			m_PKT_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.PKT_NO]);
			m_ROTATE = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.ROTATE]);
			m_DSP_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.DSP_ID]);
			m_SEAl_RATE = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.SEAl_RATE]);
			m_NUM01 = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.NUM01]);
			m_NUM02 = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.NUM02]);
			m_NUM03 = DBConvert.ToIntNull(dr[TBL_BAT_TRIMAGE.NUM03]);
			m_CHAR01 = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.CHAR01]);
			m_CHAR02 = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.CHAR02]);
			m_CHAR03 = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.CHAR03]);
			m_TODOKEDE_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.TODOKEDE_FLG]);
			m_INNKAN_SHOGO_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.INNKAN_SHOGO_FLG]);
			m_HOST_TOROKU_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.HOST_TOROKU_FLG]);
			m_SAISATSU_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.SAISATSU_FLG]);
			m_DEL_FLG = DBConvert.ToStringNull(dr[TBL_BAT_TRIMAGE.DEL_FLG]);
		}
		#endregion

		#region クエリ取得

		/// <summary>
		/// データを全て取得するSELECT文を作成します
		/// </summary>
		/// <returns>SQL文(SELECT *)</returns>
		public string GetSelectAllQuery()
		{
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT * FROM {0} ", TBL_BAT_TRIMAGE.TABLE_NAME);
			return sql.ToString();
		}

        /// <summary>
        /// 引数で指定したキー値を条件とするSELECT文を作成します
        /// ※明細番号指定なし
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <param name="batid">バッチ番号</param>
        /// <param name="scannerid">スキャナ名</param>
        /// <param name="oparationdate">処理日(OPARATION_DATE)</param>
        /// <returns>SQL文(SELECT *)</returns>
        public string GetSelectQuery(int gymid, int batid, string scannerid, int oparationdate)
        {
			string strSql = "SELECT * FROM " + TBL_BAT_TRIMAGE.TABLE_NAME +
				" WHERE " +
				TBL_BAT_TRIMAGE.GYM_ID + "=" + gymid + " AND " +
				TBL_BAT_TRIMAGE.OPERATION_DATE + "=" + oparationdate + " AND " +
				TBL_BAT_TRIMAGE.SCANNER_ID + "='" + scannerid + "' AND " +
				TBL_BAT_TRIMAGE.BAT_ID + "=" + batid +
				" ORDER BY " +
				TBL_BAT_TRIMAGE.GYM_ID + ", " +
				TBL_BAT_TRIMAGE.OPERATION_DATE + ", " +
				TBL_BAT_TRIMAGE.SCANNER_ID + ", " +
				TBL_BAT_TRIMAGE.BAT_ID + ", " +
				TBL_BAT_TRIMAGE.IMAGE_NO;
			return strSql;
		}

		/// <summary>
		/// 引数で指定したキー値を条件とするSELECT文を作成します
		/// ※画面ＩＤ指定あり
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <param name="batid">バッチ番号</param>
		/// <param name="dspid">画面ＩＤ</param>
		/// <param name="scannerid">スキャナ名</param>
		/// <param name="oparationdate">処理日(OPARATION_DATE)</param>
		/// <returns>SQL文(SELECT *)</returns>
		public string GetSelectQuery(int gymid, int batid, string dspid, string scannerid, int oparationdate)
		{
			StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT * FROM {0} ", TBL_BAT_TRIMAGE.TABLE_NAME);
            sql.AppendFormat("WHERE {0}={1} ", TBL_BAT_TRIMAGE.GYM_ID, gymid);
            sql.AppendFormat("AND {0}={1} ", TBL_BAT_TRIMAGE.BAT_ID, batid);
            sql.AppendFormat("AND {0} IN ({1}) ", TBL_BAT_TRIMAGE.DSP_ID, dspid);
            sql.AppendFormat("AND {0}='{1}' ", TBL_BAT_TRIMAGE.SCANNER_ID, scannerid);
            sql.AppendFormat("AND {0}={1} ", TBL_BAT_TRIMAGE.OPERATION_DATE, oparationdate);

			return sql.ToString();
		}

		/// <summary>
		/// 引数で指定したキー値を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid"></param>
		/// <param name="oparationdate"></param>
		/// <param name="scannerid"></param>
		/// <param name="batid"></param>
		/// <param name="imageno"></param>
		/// <returns></returns>
		public static string GetSelectQuery(int gymid, int oparationdate, string scannerid, int batid, int imageno)
		{
			string strSql = "SELECT * FROM " + TBL_BAT_TRIMAGE.TABLE_NAME +
				" WHERE " +
				TBL_BAT_TRIMAGE.GYM_ID + "=" + gymid + " AND " +
				TBL_BAT_TRIMAGE.OPERATION_DATE + "=" + oparationdate + " AND " +
				TBL_BAT_TRIMAGE.SCANNER_ID + "='" + scannerid + "' AND " +
				TBL_BAT_TRIMAGE.BAT_ID + "=" + batid + " AND " +
				TBL_BAT_TRIMAGE.IMAGE_NO + "=" + imageno +
				" ORDER BY " +
				TBL_BAT_TRIMAGE.GYM_ID + ", " +
				TBL_BAT_TRIMAGE.OPERATION_DATE + ", " +
				TBL_BAT_TRIMAGE.SCANNER_ID + ", " +
				TBL_BAT_TRIMAGE.BAT_ID + ", " +
				TBL_BAT_TRIMAGE.IMAGE_NO;
			return strSql;
		}

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns>SQL文(INSERT INTO VALUES)</returns>
		public string GetInsertQuery()
		{
			string strSql = "INSERT INTO " + TBL_BAT_TRIMAGE.TABLE_NAME + " (" +
				TBL_BAT_TRIMAGE.GYM_ID + "," +
				TBL_BAT_TRIMAGE.OPERATION_DATE + "," +
				TBL_BAT_TRIMAGE.SCANNER_ID + "," +
				TBL_BAT_TRIMAGE.BAT_ID + "," +
				TBL_BAT_TRIMAGE.IMAGE_NO + "," +
				TBL_BAT_TRIMAGE.IMG_FLG + "," +
				TBL_BAT_TRIMAGE.IMG_FLNM + "," +
				TBL_BAT_TRIMAGE.IMG_FLNM2 + "," +
				TBL_BAT_TRIMAGE.IMG_FLNM3 + "," +
				TBL_BAT_TRIMAGE.IMG_FLNM4 + "," +
				TBL_BAT_TRIMAGE.IMG_FLNM5 + "," +
				TBL_BAT_TRIMAGE.IMG_FLNM6 + "," +
				TBL_BAT_TRIMAGE.NUMBERING + "," +
				TBL_BAT_TRIMAGE.BARCODE_ID + "," +
				TBL_BAT_TRIMAGE.PKT_NO + "," +
				TBL_BAT_TRIMAGE.ROTATE + "," +
				TBL_BAT_TRIMAGE.DSP_ID + "," +
				TBL_BAT_TRIMAGE.SEAl_RATE + "," +
				TBL_BAT_TRIMAGE.NUM01 + "," +
				TBL_BAT_TRIMAGE.NUM02 + "," +
				TBL_BAT_TRIMAGE.NUM03 + "," +
				TBL_BAT_TRIMAGE.CHAR01 + "," +
				TBL_BAT_TRIMAGE.CHAR02 + "," +
				TBL_BAT_TRIMAGE.CHAR03 + "," +
				TBL_BAT_TRIMAGE.TODOKEDE_FLG + "," +
				TBL_BAT_TRIMAGE.INNKAN_SHOGO_FLG + "," +
				TBL_BAT_TRIMAGE.HOST_TOROKU_FLG + "," +
				TBL_BAT_TRIMAGE.SAISATSU_FLG + "," +
				TBL_BAT_TRIMAGE.DEL_FLG + ") VALUES (" +
				m_GYM_ID + "," +
				m_OPERATION_DATE + "," +
				"'" + m_SCANNER_ID + "'," +
				m_BAT_ID + "," +
				m_IMAGE_NO + "," +
				"'" + m_IMG_FLG + "'," +
				"'" + m_IMG_FLNM + "'," +
				"'" + m_IMG_FLNM2 + "'," +
				"'" + m_IMG_FLNM3 + "'," +
				"'" + m_IMG_FLNM4 + "'," +
				"'" + m_IMG_FLNM5 + "'," +
				"'" + m_IMG_FLNM6 + "'," +
				"'" + m_NUMBERING + "'," +
				"'" + m_BARCODE_ID + "'," +
				m_PKT_NO + "," +
				m_ROTATE + "," +
				m_DSP_ID + "," +
				m_SEAl_RATE + "," +
				m_NUM01 + "," +
				m_NUM02 + "," +
				m_NUM03 + "," +
				"'" + m_CHAR01 + "'," +
				"'" + m_CHAR02 + "'," +
				"'" + m_CHAR03 + "'," +
				"'" + m_TODOKEDE_FLG + "'," +
				"'" + m_INNKAN_SHOGO_FLG + "'," +
				"'" + m_HOST_TOROKU_FLG + "'," +
				"'" + m_SAISATSU_FLG + "'," +
				"'" + m_DEL_FLG + "')";
			return strSql;
		}

		/// <summary>
		/// update文を作成します
		/// </summary>
		/// <returns>SQL文(UPDATE SET)</returns>
		public string GetUpdateQuery()
		{
			string strSql = "UPDATE " + TBL_BAT_TRIMAGE.TABLE_NAME + " SET " +
				TBL_BAT_TRIMAGE.IMG_FLG + "='" + m_IMG_FLG + "', " +
				TBL_BAT_TRIMAGE.IMG_FLNM + "='" + m_IMG_FLNM + "', " +
				TBL_BAT_TRIMAGE.IMG_FLNM2 + "='" + m_IMG_FLNM2 + "', " +
				TBL_BAT_TRIMAGE.IMG_FLNM3 + "='" + m_IMG_FLNM3 + "', " +
				TBL_BAT_TRIMAGE.IMG_FLNM4 + "='" + m_IMG_FLNM4 + "', " +
				TBL_BAT_TRIMAGE.IMG_FLNM5 + "='" + m_IMG_FLNM5 + "', " +
				TBL_BAT_TRIMAGE.IMG_FLNM6 + "='" + m_IMG_FLNM6 + "', " +
				TBL_BAT_TRIMAGE.NUMBERING + "='" + m_NUMBERING + "', " +
				TBL_BAT_TRIMAGE.BARCODE_ID + "='" + m_BARCODE_ID + "', " +
				TBL_BAT_TRIMAGE.PKT_NO + "=" + m_PKT_NO + ", " +
				TBL_BAT_TRIMAGE.ROTATE + "=" + m_ROTATE + ", " +
				TBL_BAT_TRIMAGE.DSP_ID + "=" + m_DSP_ID + ", " +
				TBL_BAT_TRIMAGE.SEAl_RATE + "=" + m_SEAl_RATE + ", " +
				TBL_BAT_TRIMAGE.NUM01 + "=" + m_NUM01 + ", " +
				TBL_BAT_TRIMAGE.NUM02 + "=" + m_NUM02 + ", " +
				TBL_BAT_TRIMAGE.NUM03 + "=" + m_NUM03 + ", " +
				TBL_BAT_TRIMAGE.CHAR01 + "='" + m_CHAR01 + "', " +
				TBL_BAT_TRIMAGE.CHAR02 + "='" + m_CHAR02 + "', " +
				TBL_BAT_TRIMAGE.CHAR03 + "='" + m_CHAR03 + "', " +
				TBL_BAT_TRIMAGE.TODOKEDE_FLG + "='" + m_TODOKEDE_FLG + "', " +
				TBL_BAT_TRIMAGE.INNKAN_SHOGO_FLG + "='" + m_INNKAN_SHOGO_FLG + "', " +
				TBL_BAT_TRIMAGE.HOST_TOROKU_FLG + "='" + m_HOST_TOROKU_FLG + "', " +
				TBL_BAT_TRIMAGE.SAISATSU_FLG + "='" + m_SAISATSU_FLG + "', " +
				TBL_BAT_TRIMAGE.DEL_FLG + "='" + m_DEL_FLG + "'" +
				" WHERE " +
				TBL_BAT_TRIMAGE.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_TRIMAGE.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
				TBL_BAT_TRIMAGE.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_TRIMAGE.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_TRIMAGE.IMAGE_NO + "=" + m_IMAGE_NO;
			return strSql;
		}

		/// <summary>
		/// delete文を作成します
		/// </summary>
		/// <returns>SQL文(DELETE)</returns>
		public string GetDeleteQuery()
        {
			string strSql = "DELETE FROM " + TBL_BAT_TRIMAGE.TABLE_NAME +
				" WHERE " +
				TBL_BAT_TRIMAGE.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_TRIMAGE.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
				TBL_BAT_TRIMAGE.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_TRIMAGE.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_TRIMAGE.IMAGE_NO + "=" + m_IMAGE_NO;
			return strSql;
		}

		/// <summary>
		/// delete文を作成します
		/// </summary>
		/// <returns></returns>
		public static string GetDeleteQuery(TBL_BAT_TRDATA BTD)
        {
			string strSql = "DELETE FROM " + TBL_BAT_TRIMAGE.TABLE_NAME +
				" WHERE " +
				TBL_BAT_TRIMAGE.GYM_ID + "=" + BTD._GYM_ID + " AND " +
				TBL_BAT_TRIMAGE.OPERATION_DATE + "=" + BTD._OPERATION_DATE + " AND " +
				TBL_BAT_TRIMAGE.SCANNER_ID + "='" + BTD._SCANNER_ID + "' AND " +
				TBL_BAT_TRIMAGE.BAT_ID + "=" + BTD._BAT_ID + " AND " +
				TBL_BAT_TRIMAGE.IMAGE_NO + "=" + BTD._IMAGE_NO;
			return strSql;
        }

        #endregion

        #region 回転状態変更
        /// <summary>
        /// 回転情報を更新（時計回りに90°回転）
        /// ※補正エントリー画面で回転操作した場合にメモリ内の値を更新する為に使用
        /// </summary>
        public void ChangeRotate()
        {
            // インクリメント（90°回転）
            m_ROTATE++;
            // 270°より大きくなった場合は0°に変更
            if (m_ROTATE > (int)ROTATE_STATUS.ROTATE_270)
            {
                m_ROTATE = (int)ROTATE_STATUS.ROTATE_0;
            }
        }
        /// <summary>
        /// 反時計回りに90°回転
        /// ※補正エントリー画面で逆回転操作した場合にメモリ内の値を更新する為に使用
        /// </summary>
        public void ChangeReverseRotate()
        {
            // デクリメント（90°逆回転）
            m_ROTATE--;
            // マイナス値になった場合は270°に変更
            if (m_ROTATE < (int)ROTATE_STATUS.ROTATE_0)
            {
                m_ROTATE = (int)ROTATE_STATUS.ROTATE_270;
            }
        }
        #endregion
    }
}
