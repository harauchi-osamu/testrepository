using System.Configuration;

namespace SampleProcess
{
	public class ProcAplInfo
	{
		private ProcAplInfo() { }

		/// <summary>
		/// HULFTインストールフォルダ（ローカル）
		/// </summary>
		public static string HulftPath
		{
			get { return ConfigurationManager.AppSettings["HulftPath"]; }
		}
	}
}
