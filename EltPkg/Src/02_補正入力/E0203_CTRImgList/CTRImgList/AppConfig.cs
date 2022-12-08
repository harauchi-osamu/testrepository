using System.Configuration;
using CommonTable.DB;

namespace CTRImgListForm
{
	public class AppConfig
	{
		private AppConfig() { }

        /// <summary>AAAAAAA</summary>
        public static string AAAAAAA { get { return ConfigurationManager.AppSettings["AAAAAAA"]; } }

        /// <summary>BBBBBBB</summary>
        public static int BBBBBBB { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["BBBBBBB"]); } }
    }
}
