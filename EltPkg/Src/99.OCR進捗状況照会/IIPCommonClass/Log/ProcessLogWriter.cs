using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using IIPCommonClass;
using System.Configuration;

namespace IIPCommonClass.Log
{
	/// <summary>
	/// 業務ログ出力クラス
	/// </summary>
    public abstract class ProcessLogWriter
    {
		#region メンバ

		protected string _logbasepath = "";
		protected string _basedate = "";
		protected string _filepath = "";
		protected string _filename = "";
		protected string _procname = "";
		protected bool _istrace = false;

		/// <summary>
		/// エラーログリスト
		/// </summary>
		private ArrayList _arr;

        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected ProcessLogWriter()
        {
            _logbasepath = AplInfo.LogFolderPath;
            _basedate = DateTime.Now.ToString("yyyyMMdd");
			_filename = DateTime.Now.ToString("yyyyMMdd") + "_" + Process.GetCurrentProcess().ProcessName + "_" + Environment.MachineName + ".log";
			_arr = new ArrayList();
        }

        #region メソッド

		/// <summary>
		/// 初期化
		/// </summary>
        protected void Initialize()
        {
            this.CreateDirectory(_logbasepath);

            //日付フォルダ（yyyymmdd）は不要
            string path = Path.Combine(_logbasepath, _basedate);
            this.CreateDirectory(path);
            //string path = _logbasepath;

            _filepath = Path.Combine(path, _filename);
        }

		/// <summary>
		/// ログ出力先ディレクトリ作成
		/// </summary>
		/// <param name="path"></param>
		protected void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

		/// <summary>
		/// エラーログ
		/// </summary>
		/// <param name="e"></param>
		protected void ErrorLog(Exception e)
		{
			this.Write("Source = " + e.Source + " : Message = " + e.Message + " : StackTrace = " + e.StackTrace);
		}

		/// <summary>
		/// ログ出力
		/// </summary>
		protected void Add(string message) 
		{
            this.Write(message);
		}

		/// <summary>
		/// ログ出力
		/// </summary>
		/// <param name="message"></param>
		protected void Write(string message)
        {
            using (StreamWriter sw = new StreamWriter(_filepath, true, Encoding.GetEncoding("Shift-JIS")))
            {
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "," + AplInfo.Aplname + "," + message);
            }
        }

		/// <summary>
		/// トレースログ追加
		/// </summary>
		/// <param name="message"></param>
		protected void addTrace(string message)
		{
			if (_istrace)
			{
				this.Add("Trace *** " + message);
			}
		}

        /// <summary>
        /// 過去ログ削除処理
        /// </summary>
        protected void DeleteOldLog()
        {
            Calendar theDay = CultureInfo.InvariantCulture.Calendar;
            string delbasedate = theDay.AddWeeks(DateTime.Now, -5).ToString("yyyyMMdd");   //5週間保存

            try
            {
                string[] folders = Directory.GetDirectories(_logbasepath);

                foreach (string folder in folders)
                {

                    string[] target = folder.Split('\\');
                    string targetDate = target[target.Length - 1];

                    if (Convert.ToInt32(targetDate) < Convert.ToInt32(delbasedate))
                    {
                        Directory.Delete(folder, true);
                        this.Write("過去ログ：" + folder + "を削除しました");
                    }
                }
            }
            catch (Exception e)
            {
                this.ErrorLog(e);
            }
        }
        #endregion
    }
}
