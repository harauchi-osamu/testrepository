using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Common
{
    public class LogWriter
    {
        private static string _src;
        private const int _CATEGORY_ID = 150;
        private static string _LogDir;          //ログ出力フォルダ
        private static StreamWriter _swriter;

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="mb">メソッドベース(MethodBase.GetCurrentMethod())</param>
        /// <param name="msg">ログメッセージ</param>
        /// <param name="logtype">ログ種類(1:Info, 2:Warning, 3:Error)</param>
        public static void writeLog(MethodBase mb, string msg, int logtype)
        {
            _LogDir = LogConfInfo.LogFilePath;

            try
            {
                System.IO.Directory.CreateDirectory(_LogDir);
            }
            catch { }

            _src = mb.Module.Name + " " + mb.DeclaringType + "." + mb.Name;
            switch (logtype)
            {
                case 1:
                    if (LogConfInfo.DetailLog) { writeInformation(_src, msg); }
                    break;
                case 2:
                    writeWarning(_src, msg);
                    break;
                case 3:
                    writeError(_src, msg);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 情報ログ出力
        /// </summary>
        /// <param name="src">ログ出力ソース</param>
        /// <param name="msg">ログメッセージ</param>
        /// <param name="evtID">イベントＩＤ</param>
        private static void writeInformation(string src, string msg)
        {
            writeEventCommon(src, msg, EventLogEntryType.Information, 1, _CATEGORY_ID);
        }

        /// <summary>
        /// エラーログ出力
        /// </summary>
        /// <param name="src">ログ出力ソース</param>
        /// <param name="msg">ログメッセージ</param>
        private static void writeError(string src, string msg)
        {
            writeEventCommon(src, msg, EventLogEntryType.Error, 1000, _CATEGORY_ID);
        }

        /// <summary>
        /// 警告ログ出力
        /// </summary>
        /// <param name="src">ログ出力ソース</param>
        /// <param name="msg">ログメッセージ</param>
        private static void writeWarning(string src, string msg)
        {
            writeEventCommon(src, msg, EventLogEntryType.Warning, 10, _CATEGORY_ID);
        }

        /// <summary>
        /// ログ書き込み共通
        /// </summary>
        private static void writeEventCommon(string src, string msg, EventLogEntryType elet, int eventid, short category)
        {
            //所定回数ログ書き込みをリトライ
            int i = 0;
            do
            {
                if (writeEventvwr(src, msg, elet, eventid, category))
                {
                    // ログ書き込み成功
                    return;
                }

                //所定時間スリープ
                System.Threading.Thread.Sleep(LogConfInfo.WriteLogRetrySleepTime);

                i++;
            } while (i != LogConfInfo.WriteLogRetryCount);

            // すべて失敗(時分秒単位のファイル名で書き込み)
            try
            {
                using (_swriter = File.AppendText(System.IO.Path.Combine(_LogDir, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Environment.MachineName + ".log")))
                {
                    //改行Replaceなし
                    _swriter.AutoFlush = true;
                    _swriter.WriteLine("ログ出力エラー：" + DateTime.Now.ToString("HH:mm:ss.fff") + "," + NCR.Operator.UserID + "," + "[" + src + "]" + msg);
                }
                return;
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// ログ書き込みメソッド本体
        /// </summary>
        private static bool writeEventvwr(string src, string msg, EventLogEntryType elet, int eventid, short category)
        {
            try
            {
                using (_swriter = File.AppendText(System.IO.Path.Combine(_LogDir, DateTime.Now.ToString("yyyyMMdd") + "_" + Environment.MachineName + ".log")))
                {
                    //改行Replaceなし
                    _swriter.AutoFlush = true;
                    _swriter.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "," + NCR.Operator.UserID + "," + "[" + src + "]" + msg);
                    //_swriter.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "," + NCR.Operator.UserID + "," + "[" + src + "]" + msg.Replace("\\n", ""));
                }
                return true;
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show("ログに書き込めないため、ログの出力を停止します。\n" + ex.Message, "エラー", System.Windows.Forms.MessageBoxButtons.OK);
                //using (_swriter = File.AppendText(System.IO.Path.Combine(_LogDir, DateTime.Now.ToString("yyyyMMdd") + "_" + Environment.MachineName + ".log")))
                //{
                //    _swriter.AutoFlush = true;
                //    _swriter.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "," + "ログに書き込めないため、ログの出力を停止します。 " + ex.Message);
                //}
                return false;
            }
        }
    }
}
