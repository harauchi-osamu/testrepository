using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
    /// スキャナ画面入力バッチ項目
	/// </summary>
	public class TBL_SCAN_BATCHITEM
    {
        public TBL_SCAN_BATCHITEM()
        {
        }
        public TBL_SCAN_BATCHITEM(int _GYM_ID
            , string _SCAN_ITEMNAME
            , int _ITEM_ID
            , string _ITEM_NAME)
        {
            m_GYM_ID = _GYM_ID;
            m_SCAN_ITEMNAME = _SCAN_ITEMNAME;
            m_ITEM_ID = _ITEM_ID;
            m_ITEM_NAME = _ITEM_NAME;
        }

		public TBL_SCAN_BATCHITEM(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "."+ TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA  = "hen";
        public const string TABLE_PHYSICAL_NAME = "SCAN_BATCHITEM";
        public const string GYM_ID = "GYM_ID";
        public const string SCAN_ITEMNAME = "SCAN_ITEMNAME";
		public const string ITEM_ID = "ITEM_ID";
		public const string ITEM_NAME = "ITEM_NAME";
		#endregion

		#region メンバ
        private int m_GYM_ID = 0;
        private string m_SCAN_ITEMNAME = "";
        private int m_ITEM_ID = 0;
        private string m_ITEM_NAME = "";
		#endregion

        #region プロパティ
        
        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[GYM_ID]);
            m_SCAN_ITEMNAME = DBConvert.ToStringNull(dr[SCAN_ITEMNAME]);
            m_ITEM_ID = DBConvert.ToIntNull(dr[ITEM_ID]);
            m_ITEM_NAME = DBConvert.ToStringNull(dr[ITEM_NAME]);

        }

		#endregion

		#region クエリ取得

		/// <summary>
		/// データを全て取得するSELECT文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetSelectAllQuery()
		{
			string strSql = "SELECT * FROM " + TABLE_NAME +
				" ORDER BY " + GYM_ID +
                " , " + SCAN_ITEMNAME +
                " , " + ITEM_ID;

            return strSql;
		}

        /// <summary>
        /// 業務番号キーにするSELECT文
        /// </summary>
        /// <param name="_GYM_ID"></param>
        /// <param name="_OPERATION_DATE"></param>
        /// <param name="_SCANNER_ID"></param>
        /// <param name="_BATCH_NAME"></param>
        /// <returns></returns>
        public static string GetSelectQuery(int _GYM_ID)
        {
            string strSql = "SELECT * FROM " + TABLE_NAME +
                " WHERE " + GYM_ID + "='" + _GYM_ID.ToString() + "'" +
                " ORDER BY " + GYM_ID +
                " , " + SCAN_ITEMNAME +
                " , " + ITEM_ID;
            return strSql;
        }
        #endregion

    }
}
