using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR
{
    public class Terminal
    {
		public static string IniPath { get { return @"C:\NCR\CtrTerm.ini"; } }

        static Terminal()
        {
        }

		/// <summary>
		/// ルートディレクトリパス
		/// </summary>
		public static string RootDir
		{
			get { return ConfigurationManager.AppSettings["RootDir"]; }
		}

		/// <summary>
		/// ログインEXEパス
		/// </summary>
		public static string LoginEXE
		{
			get { return ConfigurationManager.AppSettings["LoginEXE"]; }
		}

		/// <summary>
		/// 端末番号
		/// </summary>
		public static string Number
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "Number"); }
        }

        /// <summary>
        /// Server.iniパス
        /// </summary>
        public static string ServeriniPath
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "ServerIniPath"); }
        }

        public static string Executable
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "Executable"); }
            //set { IniFileAccess.SetValue(_iniPath, "Common", "Executable", value); }
        }

        /// <summary>
        /// 端末メニュー種類
        /// 例：管理端末、イメトラ、エントリー
        /// </summary>
        public static string MenuType
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Common", "Type"); }
            //set { IniFileAccess.SetValue(_iniPath, "Common",, "Type", value); }
        }

        /// <summary>
        /// レーザープリンター名称
        /// </summary>
        public static string LaserPrinterName
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Printer", "Laser"); }
        }

        /// <summary>
        /// ドットプリンター名称
        /// </summary>
        public static string DotPrinterName
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Printer", "Dot"); }
        }

    }
}
