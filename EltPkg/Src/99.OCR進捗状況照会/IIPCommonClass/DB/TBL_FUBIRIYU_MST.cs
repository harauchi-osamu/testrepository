using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// TBL_FUBIRIYU_MSTテーブルクラス
	/// 当該テーブルの情報格納
	/// 参照・更新SQL発行
	/// 回転情報の更新メソッドなど
	/// </summary>
	public class TBL_FUBIRIYU_MST
	{
        #region テーブル定義
        public const string TABLE_NAME = "FUBIRIYU_MST";       // テーブル名称
															   // 項目名
		public const string FUBI_NO = "FUBI_NO";
		public const string FUBI_REASON = "FUBI_REASON";
		public const string SORT_NO = "SORT_NO";
		public const string DEL_FLG = "DEL_FLG";
		#endregion

		#region メンバ
		public int m_FUBI_NO = 0;
		public string m_FUBI_REASON = "";
		public int m_SORT_NO = 0;
		public string m_DEL_FLG = "";
		#endregion

		#region プロパティ

		public int _FUBI_NO
		{
			get { return m_FUBI_NO; }
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TBL_FUBIRIYU_MST()
        {
        }

        /// <summary>
        /// コンストラクタ（DataRow指定）
        /// 更新前の再確認など、既存のレコードを取得して格納する場合に使用
        /// </summary>
        /// <param name="dr">DataRow(Table:BAT_IMGROTATE)</param>
        public TBL_FUBIRIYU_MST(DataRow dr)
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
			m_FUBI_NO = DBConvert.ToIntNull(dr[TBL_FUBIRIYU_MST.FUBI_NO]);
			m_FUBI_REASON = DBConvert.ToStringNull(dr[TBL_FUBIRIYU_MST.FUBI_REASON]);
			m_SORT_NO = DBConvert.ToIntNull(dr[TBL_FUBIRIYU_MST.SORT_NO]);
			m_DEL_FLG = DBConvert.ToStringNull(dr[TBL_FUBIRIYU_MST.DEL_FLG]);
		}
		#endregion

		#region クエリ取得

		/// <summary>
		/// データを全て取得するSELECT文を作成します
		/// </summary>
		/// <returns>SQL文(SELECT *)</returns>
		public static string GetSelectAllQuery()
		{
			string strSql = "SELECT * FROM " + TBL_FUBIRIYU_MST.TABLE_NAME +
				" ORDER BY " +
					TBL_FUBIRIYU_MST.SORT_NO;
			return strSql;
		}

        #endregion
    }
}
