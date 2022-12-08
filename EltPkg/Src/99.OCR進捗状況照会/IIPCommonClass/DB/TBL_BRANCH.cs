using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// BRANCH
	/// </summary>
	public class TBL_BRANCH
	{
        public TBL_BRANCH()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		public TBL_BRANCH(DataRow dr)
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
			initializeByDataRow(dr);
		}

		#region テーブル定義

		public const string TABLE_NAME = "BRANCH";

        public const string ROW_BR_NO = "BR_NO";
		public const string ROW_BR_NAME_KANJI = "BR_NAME_KANJI";
		public const string ROW_BR_NAME_KANA = "BR_NAME_KANA";
		public const string ROW_STARTDATE = "STARTDATE";
		public const string ROW_ENDDATE = "ENDDATE";

		#endregion

		#region メンバ

		protected int m_BR_NO = 0;
		protected string m_BR_NAME_KANJI = "";
		protected string m_BR_NAME_KANA = "";
		protected int m_STARTDATE = 0;
		protected int m_ENDDATE = 0;
		
		#endregion

		#region プロパティ

		/// <summary>
        /// 支店番号
		/// </summary>
        public virtual int BR_NO
		{
            get { return m_BR_NO; }
		}

		/// <summary>
		/// 支店名称漢字
		/// </summary>
		public virtual string BR_NAME_KANJI
		{
			get{ return m_BR_NAME_KANJI; }
		}

		/// <summary>
		/// 支店名称カナ
		/// </summary>
		public virtual string BR_NAME_KANA
		{
			get{ return m_BR_NAME_KANA; }
		}

		/// <summary>
		/// 開始年月日
		/// </summary>
		public virtual int STARTDATE
		{
			get{ return m_STARTDATE; }
		}

		/// <summary>
		/// 終了年月日
		/// </summary>
		public virtual int ENDDATE
		{
			get{ return m_ENDDATE; }
		}

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_BR_NO = Convert.ToInt32(dr[TBL_BRANCH.ROW_BR_NO]);
            m_BR_NAME_KANJI = Convert.ToString(dr[TBL_BRANCH.ROW_BR_NAME_KANJI]);
            m_BR_NAME_KANA = Convert.ToString(dr[TBL_BRANCH.ROW_BR_NAME_KANA]);
            m_STARTDATE = Convert.ToInt32(dr[TBL_BRANCH.ROW_STARTDATE]);
            m_ENDDATE = Convert.ToInt32(dr[TBL_BRANCH.ROW_ENDDATE]);
		}

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		public void InitializeByDataRow(DataRow dr)
		{
			this.initializeByDataRow(dr);
		}

		#endregion

		#region クエリ取得（static）

		/// <summary>
		/// タッチパネル取得
		/// </summary>
		/// <returns></returns>
		public static string GetSelectQuery(int brno, int baseDate)
		{
            string strSql = "SELECT * FROM " + TBL_BRANCH.TABLE_NAME +
                " WHERE " + TBL_BRANCH.ROW_BR_NO + " = " + brno +
                " AND " + TBL_BRANCH.ROW_STARTDATE + " <= " + baseDate +
                " AND " + TBL_BRANCH.ROW_ENDDATE + " >= " + baseDate;
			return strSql;
		}

		#endregion
	}
}
