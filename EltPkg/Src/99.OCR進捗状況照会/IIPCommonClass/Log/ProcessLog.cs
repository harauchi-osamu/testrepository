using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace IIPCommonClass.Log
{
	/// <summary>
	/// 処理ログクラス
	/// </summary>
	public class ProcessLog : ProcessLogWriter
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ProcessLog()
		{
			Initialize();
		}

        /// <summary>
        /// ログ出力クラス
        /// </summary>
        private static ProcessLog _singleton = new ProcessLog();

        /// <summary>
        /// インスタンス取得
        /// </summary>
        /// <returns></returns>
        public static ProcessLog getInstance()
		{
			return _singleton;
		}

		/// <summary>
		/// 単独ログ出力
		/// </summary>
		/// <param name="message"></param>
		public void writeLog(string message)
		{
			base.Write(message);
		}

		/// <summary>
		/// トレースログ
		/// </summary>
		/// <param name="message"></param>
		public void Trace(string message)
		{
			_istrace = true;
			base.addTrace(message);
		}

		/// <summary>
		/// ログ追加
		/// </summary>
		/// <param name="message"></param>
		public void AddLog(string message)
		{
			base.Add(message);
		}


        public void DelOldLog()
        {
            base.DeleteOldLog();
        }

	}
}
