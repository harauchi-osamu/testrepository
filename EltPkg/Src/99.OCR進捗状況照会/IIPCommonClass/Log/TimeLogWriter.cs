using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using IIPCommonClass.DB;

namespace IIPCommonClass.Log
{
    public class TimeLogWriter
    {
        private static TimeLogWriter _log = new TimeLogWriter();
        private static List<TimeSpanMessage> _list;
        private static string _LogDir;
        private const int _LogSaveDays = -31;
        private static StreamWriter _swriter;
        private static Stopwatch _swatch;

        private TimeLogWriter()
        {
            _LogDir = Path.Combine(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.LastIndexOf("\\")), "Log");
            _list = new List<TimeSpanMessage>();
            _swatch = new Stopwatch();

            clearLogFile();
        }

        public static TimeLogWriter GetInstance() { return _log; }

        public void AddMessage(string msg)
        {
            if (_swatch.IsRunning)
            {
                _swatch.Stop();
            }

            _list.Add(new TimeSpanMessage(DateTime.Now, _swatch.Elapsed, msg));
            _swatch.Start();
        }

        private void clearLogFile()
        {
            try
            {
                int target = 9999;

                foreach (string path in Directory.GetFiles(_LogDir, @"????????_*.txt"))
                {
                    if (target <= 9999)
                    //dBManager.ToIntNull(Path.GetFileNameWithoutExtension(path).Substring(0, 8))
                    {
                        continue;
                    }

                    File.Delete(path);
                }
            }
            catch
            {
            }
            finally
            {
            }
        }

        public void WriteFlush(DBManager listDb)
        {
            TimeSpanMessage before = null;
            TimeSpan ts;

            try
            {
                if (listDb.ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]) < 0) return;

                if (!Directory.Exists(_LogDir))
                {
                    Directory.CreateDirectory(_LogDir);
                }

                using (_swriter = File.AppendText(System.IO.Path.Combine(_LogDir, DateTime.Now.ToString("yyyyMMdd") + "_" + Environment.MachineName + ".txt")))
                {
                    _swriter.AutoFlush = true;

                    foreach (TimeSpanMessage tsm in _list)
                    {
                        if (before != null)
                        {
                            ts = tsm.TimeSpan.Subtract(before.TimeSpan);
                            _swriter.WriteLine(
                                tsm.DateTime.ToString("yyyy/MM/dd HH:mm:ss:ffff") + ", " +
                                ts.TotalMilliseconds.ToString("0000.0000") + ", " +
                                tsm.Message);
                        }
                        else
                        {
                            _swriter.WriteLine(tsm.Message);
                        }
                        before = tsm;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessLog.getInstance().writeLog(ex.Message);
                ProcessLog.getInstance().writeLog(_LogDir);
            }
            finally
            {
                _list.Clear();
            }
        }
    }
}
