using System.Configuration;
using CommonTable.DB;

namespace CorrectInput
{
	public class AppConfig
	{
		private AppConfig() { }

        /// <summary>エントリー画面で使用するフォント名（ラベル）</summary>
        public static string LabelFontName { get { return DBConvert.ToStringNull(ConfigurationManager.AppSettings["LabelFontName"]); } }
        /// <summary>エントリー画面で使用するフォント名（テキストボックス）</summary>
        public static string TextFontName { get { return DBConvert.ToStringNull(ConfigurationManager.AppSettings["TextFontName"]); } }
        /// <summary>金額プルーフチェック</summary>
        public static bool KingakuProofCheck { get { return DBConvert.ToBoolNull(ConfigurationManager.AppSettings["KingakuProofCheck"]); } }
        /// <summary>デフォルトイメージ画像</summary>
        public static string DefaultImage { get { return DBConvert.ToStringNull(ConfigurationManager.AppSettings["DefaultImage"]); } }
        /// <summary>明細一覧の背景色設定</summary>
        public static string MeiListBackColor { get { return DBConvert.ToStringNull(ConfigurationManager.AppSettings["MeiListBackColor"]); } }
    }
}
