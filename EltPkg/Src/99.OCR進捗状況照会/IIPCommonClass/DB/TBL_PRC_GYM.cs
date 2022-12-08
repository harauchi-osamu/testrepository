using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// オペレーター
	/// </summary>
	public class TBL_PRC_GYM
	{
        public TBL_PRC_GYM()
        {
        }

        public TBL_PRC_GYM(int gym_id)
        {
            m_GYM_ID = gym_id;
        }

        public TBL_PRC_GYM(DataRow dr)
		{
			initializeByDataRow(dr);
		}


        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "PRC_GYM";

        public const string GYM_ID = "GYM_ID";
        public const string GYM_SEQ = "GYM_SEQ";
        public const string GYM_KANJI = "GYM_KANJI";
        public const string STAGE1 = "STAGE1";
        public const string STAGE2 = "STAGE2";
        public const string STAGE3 = "STAGE3";
        public const string STAGE4 = "STAGE4";
        public const string STAGE5 = "STAGE5";
        public const string LIMIT_TIME = "LIMIT_TIME";
        public const string EXEC_TIME = "EXEC_TIME";
        public const string JIMU_TALLY = "JIMU_TALLY";
        public const string JIMU_PERIOD = "JIMU_PERIOD";
        public const string OPE_TALLY = "OPE_TALLY";
        public const string OPE_PERIOD = "OPE_PERIOD";
        public const string CREATE_USER = "CREATE_USER";
        public const string CREATE_TIME = "CREATE_TIME";
        public const string UPDATE_USER = "UPDATE_USER";
        public const string UPDATE_TIME = "UPDATE_TIME";

        #endregion

        #region メンバ

        public int m_GYM_ID = 0;
        public int m_GYM_SEQ = 0;
        public string m_GYM_KANJI = "";
        public int m_STAGE1 = 0;
        public int m_STAGE2 = 0;
        public int m_STAGE3 = 0;
        public int m_STAGE4 = 0;
        public int m_STAGE5 = 0;
        public int m_LIMIT_TIME = 0;
        public int m_EXEC_TIME = 0;
        public int m_JIMU_TALLY = 0;
        public int m_JIMU_PERIOD = 0;
        public int m_OPE_TALLY = 0;
        public int m_OPE_PERIOD = 0;
        public string m_CREATE_USER = "";
        public string m_CREATE_TIME = "";
        public string m_UPDATE_USER = "";
        public string m_UPDATE_TIME = "";


        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }

        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_PRC_GYM.GYM_ID]);
            m_GYM_SEQ = DBConvert.ToIntNull(dr[TBL_PRC_GYM.GYM_SEQ]);
            m_GYM_KANJI = DBConvert.ToStringNull(dr[TBL_PRC_GYM.GYM_KANJI]);
            m_STAGE1 = DBConvert.ToIntNull(dr[TBL_PRC_GYM.STAGE1]);
            m_STAGE2 = DBConvert.ToIntNull(dr[TBL_PRC_GYM.STAGE2]);
            m_STAGE3 = DBConvert.ToIntNull(dr[TBL_PRC_GYM.STAGE3]);
            m_STAGE4 = DBConvert.ToIntNull(dr[TBL_PRC_GYM.STAGE4]);
            m_STAGE5 = DBConvert.ToIntNull(dr[TBL_PRC_GYM.STAGE5]);
            m_LIMIT_TIME = DBConvert.ToIntNull(dr[TBL_PRC_GYM.LIMIT_TIME]);
            m_EXEC_TIME = DBConvert.ToIntNull(dr[TBL_PRC_GYM.EXEC_TIME]);
            m_JIMU_TALLY = DBConvert.ToIntNull(dr[TBL_PRC_GYM.JIMU_TALLY]);
            m_JIMU_PERIOD = DBConvert.ToIntNull(dr[TBL_PRC_GYM.JIMU_PERIOD]);
            m_OPE_TALLY = DBConvert.ToIntNull(dr[TBL_PRC_GYM.OPE_TALLY]);
            m_OPE_PERIOD = DBConvert.ToIntNull(dr[TBL_PRC_GYM.OPE_PERIOD]);
            m_CREATE_USER = DBConvert.ToStringNull(dr[TBL_PRC_GYM.CREATE_USER]);
            m_CREATE_TIME = DBConvert.ToStringNull(dr[TBL_PRC_GYM.CREATE_TIME]);
            m_UPDATE_USER = DBConvert.ToStringNull(dr[TBL_PRC_GYM.UPDATE_USER]);
            m_UPDATE_TIME = DBConvert.ToStringNull(dr[TBL_PRC_GYM.UPDATE_TIME]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetSelectQuery()
		{
            string strSql = "SELECT * FROM " + TBL_PRC_GYM.TABLE_NAME +
                " ORDER BY " +
                TBL_PRC_GYM.GYM_ID;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_GYM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_GYM.GYM_ID + "=" + gymid;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <param name="gymseq">業務連番</param>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int gymseq)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_GYM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_GYM.GYM_ID + "=" + gymid + " And " +
                TBL_PRC_GYM.GYM_SEQ + "=" + gymseq;
            return strSql;
        }

        #endregion
    }
}
