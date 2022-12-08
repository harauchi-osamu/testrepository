using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// スキャンバッチ管理
    /// </summary>
    public class TBL_SCAN_BATCH_CTL
    {
        public TBL_SCAN_BATCH_CTL()
        {
        }

        public TBL_SCAN_BATCH_CTL(int input_route, string batch_folder_name)
        {
            m_INPUT_ROUTE = input_route;
            m_BATCH_FOLDER_NAME = batch_folder_name;
        }

        public TBL_SCAN_BATCH_CTL(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region Route定義

        public enum InputRoute
        {
            Normal = 1,
            Futai = 2,
        }

        #endregion 

        #region Status定義

        /// <summary>
        /// Statusに対応する出力テキスト取得
        /// </summary>

        public static string GetStatusText(enumStatus Status)
        {
            string rtnValue = string.Empty;
            if (Status.Equals(enumStatus.Wait)) rtnValue = "処理待";
            if (Status.Equals(enumStatus.Processing)) rtnValue = "処理中";
            if (Status.Equals(enumStatus.Complete)) rtnValue = "処理済";
            if (Status.Equals(enumStatus.Hold)) rtnValue = "保留";
            if (Status.Equals(enumStatus.Delete)) rtnValue = "削除";

            return rtnValue;
        }

        public enum enumStatus
        {
            ///<summary>処理待</summary>
            Wait = 1,
            ///<summary>保留</summary>
            Hold = 2,
            ///<summary>処理中</summary>
            Processing = 3,
            ///<summary>処理済</summary>
            Complete = 4, 
            ///<summary>削除</summary>
            Delete = 9,
        }
        
        #endregion 

        #region テーブル定義

        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "SCAN_BATCH_CTL";

        public const string INPUT_ROUTE = "INPUT_ROUTE";
        public const string BATCH_FOLDER_NAME = "BATCH_FOLDER_NAME";
        public const string OC_BK_NO = "OC_BK_NO";
        public const string OC_BR_NO = "OC_BR_NO";
        public const string SCAN_BR_NO = "SCAN_BR_NO";
        public const string SCAN_DATE = "SCAN_DATE";
        public const string CLEARING_DATE = "CLEARING_DATE";
        public const string SCAN_COUNT = "SCAN_COUNT";
        public const string TOTAL_COUNT = "TOTAL_COUNT";
        public const string TOTAL_AMOUNT = "TOTAL_AMOUNT";
        public const string IMAGE_COUNT = "IMAGE_COUNT";
        public const string STATUS = "STATUS";
        public const string LOCK_TERM = "LOCK_TERM";
        #endregion

        #region メンバ

        private int m_INPUT_ROUTE = 0;
        private string m_BATCH_FOLDER_NAME = "";
        public int m_OC_BK_NO = 0;
        public int m_OC_BR_NO = 0;
        public int m_SCAN_BR_NO = 0;
        public int m_SCAN_DATE = 0;
        public int m_CLEARING_DATE = 0;
        public int m_SCAN_COUNT = 0;
        public int m_TOTAL_COUNT = 0;
        public long m_TOTAL_AMOUNT = 0;
        public int m_IMAGE_COUNT = 0;
        public int m_STATUS = 0;
        public string m_LOCK_TERM = "";

        #endregion

        #region プロパティ

        public int _INPUT_ROUTE
        {
            get { return m_INPUT_ROUTE; }
        }
        public string _BATCH_FOLDER_NAME
        {
            get { return m_BATCH_FOLDER_NAME; }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_INPUT_ROUTE = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.INPUT_ROUTE]);
            m_BATCH_FOLDER_NAME = DBConvert.ToStringNull(dr[TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME]);
            m_OC_BK_NO = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.OC_BK_NO]);
            m_OC_BR_NO = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.OC_BR_NO]);
            m_SCAN_BR_NO = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.SCAN_BR_NO]);
            m_SCAN_DATE = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.SCAN_DATE]);
            m_CLEARING_DATE = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.CLEARING_DATE]);
            m_SCAN_COUNT = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.SCAN_COUNT]);
            m_TOTAL_COUNT = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.TOTAL_COUNT]);
            m_TOTAL_AMOUNT = DBConvert.ToLongNull(dr[TBL_SCAN_BATCH_CTL.TOTAL_AMOUNT]);
            m_IMAGE_COUNT = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.IMAGE_COUNT]);
            m_STATUS = DBConvert.ToIntNull(dr[TBL_SCAN_BATCH_CTL.STATUS]);
            m_LOCK_TERM = DBConvert.ToStringNull(dr[TBL_SCAN_BATCH_CTL.LOCK_TERM]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSql = "SELECT * FROM " + TBL_SCAN_BATCH_CTL.TABLE_NAME +
                " ORDER BY " +
                TBL_SCAN_BATCH_CTL.INPUT_ROUTE + "," +
                TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(InputRoute inputroute, string batch_folder_name)
        {
            string strSql = "SELECT * FROM " + TBL_SCAN_BATCH_CTL.TABLE_NAME +
                " WHERE " +
                TBL_SCAN_BATCH_CTL.INPUT_ROUTE + " = " + inputroute.ToString("d") + " AND " +
                TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME + " = '" + batch_folder_name + "'";
            return strSql;
        }

        /// <summary>
        /// INPUT_ROUTE/SCAN_DATE 基準のSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryListData(int scandate , InputRoute inputroute)
        {
            string strSql = "SELECT * FROM " + TBL_SCAN_BATCH_CTL.TABLE_NAME +
            " WHERE " + "(" + TBL_SCAN_BATCH_CTL.SCAN_DATE +  "= 0 " + " OR " 
                            + TBL_SCAN_BATCH_CTL.SCAN_DATE + "= " + scandate + " ) " + " AND "
                      + TBL_SCAN_BATCH_CTL.INPUT_ROUTE + "=" + inputroute.ToString("d") + 
            " ORDER BY " +
            TBL_SCAN_BATCH_CTL.STATUS + "," +
            TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME;
            ;

            return strSql;
        }

        /// <summary>
        /// キー項目・ステータス指定のSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryStatus(InputRoute inputroute, string batch_folder_name, enumStatus GetStatus)
        {
            string strSql = "SELECT * FROM " + TBL_SCAN_BATCH_CTL.TABLE_NAME +
                " WHERE " +
                TBL_SCAN_BATCH_CTL.INPUT_ROUTE + " = " + inputroute.ToString("d") + " AND " +
                TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME + " = '" + batch_folder_name + "' AND " +
                TBL_SCAN_BATCH_CTL.STATUS + " = " + GetStatus.ToString("d") + "";
            return strSql;
        }

        /// <summary>
        /// ステータス変更時のUPDATE文を作成します
        /// 更新元のステータス指定あり
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateQueryStatusChg(InputRoute inputroute, string BatchFolderName, enumStatus GetStatus,
                                                     enumStatus SetStatus, string SetLOCK)
        {
            string strSql = "UPDATE " + TBL_SCAN_BATCH_CTL.TABLE_NAME + " SET " +
                              TBL_SCAN_BATCH_CTL.STATUS + " = " + SetStatus.ToString("d") + " , " +
                              TBL_SCAN_BATCH_CTL.LOCK_TERM + " = '" + SetLOCK + "'" +
                            " WHERE " + TBL_SCAN_BATCH_CTL.INPUT_ROUTE + " = " + inputroute.ToString("d") + " AND "
                                  + TBL_SCAN_BATCH_CTL.STATUS + " = " + GetStatus.ToString("d") + " AND "
                                  + TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME + " = '" + BatchFolderName + "' "
            ;
            return strSql;
        }

        /// <summary>
        /// 保留用のUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryHold(enumStatus GetStatus)
        {
            string strSql = "UPDATE " + TBL_SCAN_BATCH_CTL.TABLE_NAME + " SET " +
                TBL_SCAN_BATCH_CTL.OC_BK_NO + "=" + m_OC_BK_NO + ", " +
                TBL_SCAN_BATCH_CTL.OC_BR_NO + "=" + m_OC_BR_NO + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_BR_NO + "=" + m_SCAN_BR_NO + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_DATE + "=" + m_SCAN_DATE + ", " +
                TBL_SCAN_BATCH_CTL.CLEARING_DATE + "=" + m_CLEARING_DATE + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_COUNT + "=" + m_SCAN_COUNT + ", " +
                TBL_SCAN_BATCH_CTL.TOTAL_COUNT + "=" + m_TOTAL_COUNT + ", " +
                TBL_SCAN_BATCH_CTL.TOTAL_AMOUNT + "=" + m_TOTAL_AMOUNT + ", " +
                TBL_SCAN_BATCH_CTL.STATUS + "=" + m_STATUS + ", " +
                TBL_SCAN_BATCH_CTL.LOCK_TERM + "='" + m_LOCK_TERM + "' " + 
                " WHERE " +
                TBL_SCAN_BATCH_CTL.INPUT_ROUTE + " = " + m_INPUT_ROUTE + " AND " +
                TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME + " = '" + m_BATCH_FOLDER_NAME + "' AND " +
                TBL_SCAN_BATCH_CTL.STATUS + " = " + GetStatus.ToString("d");
            return strSql;
        }

        /// <summary>
        /// 処理済用のUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryComplete(enumStatus GetStatus)
        {
            string strSql = "UPDATE " + TBL_SCAN_BATCH_CTL.TABLE_NAME + " SET " +
                TBL_SCAN_BATCH_CTL.OC_BK_NO + "=" + m_OC_BK_NO + ", " +
                TBL_SCAN_BATCH_CTL.OC_BR_NO + "=" + m_OC_BR_NO + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_BR_NO + "=" + m_SCAN_BR_NO + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_DATE + "=" + m_SCAN_DATE + ", " +
                TBL_SCAN_BATCH_CTL.CLEARING_DATE + "=" + m_CLEARING_DATE + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_COUNT + "=" + m_SCAN_COUNT + ", " +
                TBL_SCAN_BATCH_CTL.TOTAL_COUNT + "=" + m_TOTAL_COUNT + ", " +
                TBL_SCAN_BATCH_CTL.TOTAL_AMOUNT + "=" + m_TOTAL_AMOUNT + ", " +
                TBL_SCAN_BATCH_CTL.IMAGE_COUNT + "=" + m_IMAGE_COUNT + ", " +
                TBL_SCAN_BATCH_CTL.STATUS + "=" + m_STATUS + ", " +
                TBL_SCAN_BATCH_CTL.LOCK_TERM + "='" + m_LOCK_TERM + "' " +
                " WHERE " +
                TBL_SCAN_BATCH_CTL.INPUT_ROUTE + " = " + m_INPUT_ROUTE + " AND " +
                TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME + " = '" + m_BATCH_FOLDER_NAME + "' AND " +
                TBL_SCAN_BATCH_CTL.STATUS + " = " + GetStatus.ToString("d");
            return strSql;
        }


        /// <summary>
        /// 確定処理（付帯バッチ票入力）用のUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryInputComplete(enumStatus GetStatus)
        {
            string strSql = "UPDATE " + TBL_SCAN_BATCH_CTL.TABLE_NAME + " SET " +
                TBL_SCAN_BATCH_CTL.OC_BK_NO + "=" + m_OC_BK_NO + ", " +
                TBL_SCAN_BATCH_CTL.OC_BR_NO + "=" + m_OC_BR_NO + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_BR_NO + "=" + m_SCAN_BR_NO + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_DATE + "=" + m_SCAN_DATE + ", " +
                TBL_SCAN_BATCH_CTL.CLEARING_DATE + "=" + m_CLEARING_DATE + ", " +
                TBL_SCAN_BATCH_CTL.SCAN_COUNT + "=" + m_SCAN_COUNT + ", " +
                TBL_SCAN_BATCH_CTL.TOTAL_COUNT + "=" + m_TOTAL_COUNT + ", " +
                TBL_SCAN_BATCH_CTL.TOTAL_AMOUNT + "=" + m_TOTAL_AMOUNT + ", " +
                TBL_SCAN_BATCH_CTL.IMAGE_COUNT + "=" + m_IMAGE_COUNT + 
                " WHERE " +
                TBL_SCAN_BATCH_CTL.INPUT_ROUTE + " = " + m_INPUT_ROUTE + " AND " +
                TBL_SCAN_BATCH_CTL.BATCH_FOLDER_NAME + " = '" + m_BATCH_FOLDER_NAME + "' AND " +
                TBL_SCAN_BATCH_CTL.STATUS + " = " + GetStatus.ToString("d");
            return strSql;
        }

        #endregion
    }
}
