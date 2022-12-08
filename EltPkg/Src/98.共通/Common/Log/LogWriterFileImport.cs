using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Common
{
    /// <summary>
    /// ファイル取込処理向けのLogWriter
    /// </summary>
    public class LogWriterFileImport
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
        /// <param name="ErrFile">エラーファイル</param>
        /// <param name="logtype">ログ種類(1:Info, 2:Warning, 3:Error)</param>
        public static void writeLog(MethodBase mb, string msg, string ErrFile, int logtype)
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
                    if (LogConfInfo.DetailLog) { writeInformation(_src, msg, ErrFile); }
                    break;
                case 2:
                    writeWarning(_src, msg, ErrFile);
                    break;
                case 3:
                    writeError(_src, msg, ErrFile);
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
        private static void writeInformation(string src, string msg, string ErrFile)
        {
            writeEventCommon(src, msg, EventLogEntryType.Information, 1, _CATEGORY_ID, ErrFile);
        }

        /// <summary>
        /// エラーログ出力
        /// </summary>
        /// <param name="src">ログ出力ソース</param>
        /// <param name="msg">ログメッセージ</param>
        private static void writeError(string src, string msg, string ErrFile)
        {
            writeEventCommon(src, msg, EventLogEntryType.Error, 1000, _CATEGORY_ID, ErrFile);
        }
        /// <summary>
        /// 警告ログ出力
        /// </summary>
        /// <param name="src">ログ出力ソース</param>
        /// <param name="msg">ログメッセージ</param>
        private static void writeWarning(string src, string msg, string ErrFile)
        {
            writeEventCommon(src, msg, EventLogEntryType.Warning, 10, _CATEGORY_ID, ErrFile);
        }

        /// <summary>
        /// ログ書き込み共通
        /// </summary>
        private static void writeEventCommon(string src, string msg, EventLogEntryType elet, int eventid, short category, string ErrFile)
        {
            //所定回数ログ書き込みをリトライ
            int i = 0;
            do
            {
                if (writeEventvwr(src, msg, elet, eventid, category, ErrFile))
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
                using (_swriter = File.AppendText(System.IO.Path.Combine(_LogDir, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + GetfileKey(ErrFile, elet) + ".log")))
                {
                    //改行Replaceなし
                    _swriter.AutoFlush = true;
                    _swriter.WriteLine("ログ出力エラー：" + DateTime.Now.ToString("HH:mm:ss.fff") + "," + NCR.Operator.UserID + "," + ErrFile + "," + "[" + src + "]" + msg);
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
        private static bool writeEventvwr(string src, string msg, EventLogEntryType elet, int eventid, short category, string ErrFile)
        {
            try
            {
                string file_Key = ErrFile.Substring(0, 8);

                using (_swriter = File.AppendText(System.IO.Path.Combine(_LogDir, DateTime.Now.ToString("yyyyMMdd") + "_" + GetfileKey(ErrFile, elet) + ".log")))
                {
                    //改行Replaceなし
                    _swriter.AutoFlush = true;
                    _swriter.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "," + NCR.Operator.UserID + "," + ErrFile + "," + "[" + src + "]" + msg);
                    //_swriter.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "," + NCR.Operator.UserID + "," + ErrFile + "," + "[" + src + "]" + msg.Replace("\\n", ""));
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

        /// <summary>
        /// ファイル名のキー情報取得
        /// </summary>
        private static string GetfileKey(string ErrFile, EventLogEntryType elet)
        {
            string Errtype = string.Empty;

            switch (elet)
            {
                case EventLogEntryType.Error:
                    Errtype = "_E";
                    break;
                case EventLogEntryType.Warning:
                    Errtype = "_W";
                    break;
                case EventLogEntryType.Information:
                    Errtype = "_I";
                    break;
            }

            try
            {
                return ErrFile.Substring(0, 8) + Errtype;
            }
            catch (Exception)
            {
                return "None";
            }
        }

    }
}
