using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 支店マスタ
    /// </summary>
    public class V_BRANCHMF
    {
        public V_BRANCHMF(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public V_BRANCHMF(int brno, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_BR_NO = brno;
        }

        public V_BRANCHMF(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "V_BRANCHMF";

        public const string BR_NO = "BR_NO";
        public const string BR_NAME_KANJI = "BR_NAME_KANJI";
        public const string BR_NAME_KANA = "BR_NAME_KANA";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_BR_NO = 0;
        public string m_BR_NAME_KANJI = "";
        public string m_BR_NAME_KANA = "";

        #endregion

        #region プロパティ

        public int _BR_NO
        {
            get { return m_BR_NO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_BR_NO = DBConvert.ToIntNull(dr[V_BRANCHMF.BR_NO]);
            m_BR_NAME_KANJI = DBConvert.ToStringNull(dr[V_BRANCHMF.BR_NAME_KANJI]);
            m_BR_NAME_KANA = DBConvert.ToStringNull(dr[V_BRANCHMF.BR_NAME_KANA]);
        }

        #endregion

        #region テーブル名取得

        /// <summary>
        /// テーブル名取得
        /// 引数によりスキーマ変更
        /// </summary>
        /// <returns></returns>
        public static string TABLE_NAME(int Schemabankcd)
        {
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + V_BRANCHMF.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + V_BRANCHMF.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                V_BRANCHMF.BR_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int brno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + V_BRANCHMF.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                V_BRANCHMF.BR_NO + "=" + brno;
            return strSQL;
        }
        #endregion
    }
}
