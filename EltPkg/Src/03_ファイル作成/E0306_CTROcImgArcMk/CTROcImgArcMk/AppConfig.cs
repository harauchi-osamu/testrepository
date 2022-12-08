using System.Configuration;
using CommonTable.DB;

namespace CTROcImgArcMk
{
	public class AppConfig
	{
		private AppConfig() { }

        /// <summary>UniqueCodeSleepTime</summary>
        public static int UniqueCodeSleepTime { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["UniqueCodeSleepTime"]); } }

        /// <summary>アーカイブファイルサイズ（MB）</summary>
        public static int MaxArchiveFileSize { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["MaxArchiveFileSize"]); } }
    }
}
