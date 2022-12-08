using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// データ定義
	/// </summary>
	public class TBL_DATA_DEFINE
	{
        public TBL_DATA_DEFINE()
        {
        }

        public TBL_DATA_DEFINE(int gymid, string imgid)
        {
            m_GYM_ID = gymid;
            m_IMG_ID = imgid;
        }

		public TBL_DATA_DEFINE(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

        public const string TABLE_NAME = "DATA_DEFINE";             // データ定義

        public const string GYM_ID = "GYM_ID";                      // 業務番号
        public const string IMG_ID = "IMG_ID";                      // データ番号
        public const string IMG_NM = "IMG_NM";                      // データ名称
        public const string IMG_FLNM = "IMG_FLNM";                  // レポートファイル
        public const string LIST_FLG = "LIST_FLG";                  // 一覧表示フラグ 0：一覧表示対象外 1：一覧表示対象
		public const string CREATE_TIME = "CREATE_TIME";            // 作成日時
        public const string STATUS = "STATUS";                      // 状態 0：空白 1：印刷済
        public const string DEL_FLG = "DEL_FLG";                    // 削除フラグ
        public const string PRINTER_KBN = "PRINTER_KBN";           // 使用するプリンタ 0：ドットプリンタ 1：レーザープリンタ

		#endregion

        #region メンバ

        private int m_GYM_ID;
        private string m_IMG_ID;
        public string m_IMG_NM;
        public string m_IMG_FLNM;
        public string m_LIST_FLG;
		public string m_CREATE_TIME;
		public string m_STATUS;
        public string m_DEL_FLG;
        public string m_PRINTER_KBN;

		#endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }

        public string _IMG_ID
        {
            get { return m_IMG_ID; }
        }

        public string Key
        {
            get { return DBConvert.ToStringNull(_GYM_ID) + _IMG_ID; }
        }
        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_DATA_DEFINE.GYM_ID]);
            m_IMG_ID = DBConvert.ToStringNull(dr[TBL_DATA_DEFINE.IMG_ID]);
            m_IMG_NM = DBConvert.ToStringNull(dr[TBL_DATA_DEFINE.IMG_NM]);
            m_IMG_FLNM = DBConvert.ToStringNull(dr[TBL_DATA_DEFINE.IMG_FLNM]);
            m_LIST_FLG = DBConvert.ToStringNull(dr[TBL_DATA_DEFINE.LIST_FLG]);
			m_CREATE_TIME = DBConvert.ToStringNull(dr[TBL_DATA_DEFINE.CREATE_TIME]);
			m_STATUS = DBConvert.ToStringNull(dr[TBL_DATA_DEFINE.STATUS]);
            m_DEL_FLG = DBConvert.ToStringNull(dr[TBL_DATA_DEFINE.DEL_FLG]);
            m_PRINTER_KBN = DBConvert.ToStringNull(dr[TBL_DATA_DEFINE.PRINTER_KBN]);
        }

		#endregion

		#region クエリ取得

        /// <summary>
		/// SELECT文を作成します
		/// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            return GetSelectQuery(false);
        }

        /// <summary>
		/// SELECT文を作成します
		/// </summary>
        /// <param name="forupdate">FOR UPDATE文を付けるかどうか</param>
        /// <returns></returns>
        public static string GetSelectQuery(bool forupdate)
        {
            string strSql = "SELECT * FROM " + TBL_DATA_DEFINE.TABLE_NAME +
                            " ORDER BY " + TBL_DATA_DEFINE.GYM_ID +
                                     "," + TBL_DATA_DEFINE.IMG_ID;
            if (forupdate)
            {
                strSql += " FOR UPDATE";
            }

            return strSql;
        }

        /// <summary>
        /// SELECT文を作成します
        /// </summary>
        /// <param name="forupdate">FOR UPDATE文を付けるかどうか</param>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid,string imgid, bool forupdate)
        {
            string strSql = "SELECT * FROM " + TBL_DATA_DEFINE.TABLE_NAME +
                            " WHERE " + TBL_DATA_DEFINE.GYM_ID + "=" + gymid +
                              " AND " + TBL_DATA_DEFINE.IMG_ID + "=" + imgid +
                            " ORDER BY " + TBL_DATA_DEFINE.GYM_ID +
                                     "," + TBL_DATA_DEFINE.IMG_ID;
            if (forupdate)
            {
                strSql += " FOR UPDATE";
            }

            return strSql;
        }

        /// <summary>
        /// 業務更新QUERY
        /// </summary>
        /// <param name="gym_id"></param>
        /// <returns></returns>
        public static string GetUpdataQuery(int gym_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + TBL_DATA_DEFINE.TABLE_NAME + " SET ");
            sb.Append(TBL_DATA_DEFINE.CREATE_TIME + " = '" + DBNull.Value + "', ");
            sb.Append(TBL_DATA_DEFINE.STATUS + " = '0'");
            sb.Append(" WHERE " + TBL_DATA_DEFINE.GYM_ID + " = " + gym_id);

            return sb.ToString();
        }

        /// <summary>
        /// Delete文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_DATA_DEFINE.TABLE_NAME +
                                 " WHERE " + TBL_DATA_DEFINE.GYM_ID + "=" + m_GYM_ID +
                                   " AND " + TBL_DATA_DEFINE.IMG_ID + "=" + m_IMG_ID;
            return strSql;
        }

        #endregion

        /// <summary>
        /// STATUSに対応するステータスの文言を返します
        /// </summary>
        /// <returns></returns>
        public string GetStatusMessage()
        {
            string res = "";
			string status = m_STATUS;

            switch (status)
            {
                case "0":
                    res = "";
                    break;
                case "1":
                    res = "作成済";
                    break;
                default:
                    break;
            }

            return res;
        }        
    }
}
