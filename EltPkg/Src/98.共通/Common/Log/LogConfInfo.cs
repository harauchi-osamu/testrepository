using System;
using System.Configuration;

namespace Common
{
    public class LogConfInfo
    {
        /// <summary>共通コンフィグ</summary>
        //private static Configuration _config = null;

        public static bool _dtail_log = true;

        static LogConfInfo()
        {
            //LogConfInfo ai = new LogConfInfo();
            //string configPath = ai.GetType().Assembly.Location;
            //_config = ConfigurationManager.OpenExeConfiguration(configPath);
        }
        
        /// <summary>
        /// ログ書き込みリトライ回数
        /// </summary>
        public static int WriteLogRetryCount
        {
            get { return 5; }
        }
        /// <summary>
        /// ログ書き込みリトライSleepTime
        /// </summary>
        public static int WriteLogRetrySleepTime
        {
            get { return 200; }
        }

        /// <summary>
        /// 端末名
        /// </summary>
        public static string TermName
        {
            get { return Environment.MachineName; }
        }
        /// <summary>
        /// Informationレベルのログを出力するか
        /// </summary>
        public static bool DetailLog
        {
            get { return _dtail_log; }
            set { _dtail_log = value; }
        }
        /// <summary>
        /// ログファイルフォルダのパス
        /// </summary>
        public static string LogFilePath
        {
            get 
            {
#if (DEBUG)
                // デバック実行時
                return "C:/NCR/LOG/" + DateTime.Now.ToString("yyyyMMdd");
#else
                return System.Windows.Forms.Application.StartupPath + NCR.Server.EXELogRoot + DateTime.Now.ToString("yyyyMMdd"); 
#endif
            }
        }
    }
}
