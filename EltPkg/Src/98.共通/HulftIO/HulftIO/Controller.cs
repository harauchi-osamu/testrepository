using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Common;
using EntryCommon;

namespace HulftIO
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private StringBuilder _output = null;


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 管理クラスを設定する
        /// </summary>
        /// <param name="mst"></param>
        /// <param name="item"></param>
        public override void SetManager(MasterManager mst, ManagerBase item)
		{
			base.SetManager(mst, item);
			_masterMgr = MasterMgr;
			_itemMgr = (ItemManager)ItemMgr;

            // Server.ini 初期化
            ServerIni.Initialize();
        }

        /// <summary>
        /// 引数を設定する
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool SetArgs(string[] args)
        {
            return true;
        }


		/// <summary>
		/// HULFTによる集信を行う
		/// </summary>
		/// <returns></returns>
		public int RecvHulft(string fileId)
		{
			int res = 0;

            // 伝送処理
            if (AppConfig.HulftRecvExec)
            {
                // HULFT伝送する
                res = HulftStartReceive(fileId);
                if (res != 0)
                {
                    _itemMgr.TransInfo.ErrMsg = string.Format("ファイルの集信に失敗しました。[fileId={0}, res={1}]", fileId, res);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), _itemMgr.TransInfo.ErrMsg, 2);
                    return res;
                }
            }
            else
            {
                // HULFT伝送しない
                //res = ItemManager.TransParams.RESULT_SKIP;
                //_itemMgr.TransInfo.ErrMsg = "未実施";
            }

            // 結果取得
            string rcvFilePath = Path.Combine(_itemMgr.TransInfo.RecvDirPath, _itemMgr.TransInfo.FileName);
            if (!File.Exists(rcvFilePath))
			{
				_itemMgr.TransInfo.ErrMsg = string.Format("ファイルの集信に失敗しました。[fileId={0}, res={1}]", fileId, "NOT_FOUND");
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), _itemMgr.TransInfo.ErrMsg, 2);
				return ItemManager.TransParams.RESULT_FILE_NOT;
			}

			return ItemManager.TransParams.RESULT_SUCCESS;
		}

		/// <summary>
		/// HULFTによる配信を行う
		/// </summary>
		/// <returns></returns>
		public int SendHulft(string fileId)
        {
            int res = 0;

            // 伝送処理
            if (AppConfig.HulftSendExec)
            {
                // HULFT伝送する
                string sendFilePath = Path.Combine(_itemMgr.TransInfo.SendDirPath, _itemMgr.TransInfo.FileName);
                res = HulftStartSend(fileId, sendFilePath);
                if (res != 0)
                {
                    _itemMgr.TransInfo.ErrMsg = string.Format("ファイルの配信に失敗しました。[fileId={0}, res={1}]", fileId, res);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), _itemMgr.TransInfo.ErrMsg, 2);
                    return res;
                }
            }
            else
            {
                // HULFT伝送しない
                res = ItemManager.TransParams.RESULT_SKIP;
                _itemMgr.TransInfo.ErrMsg = "未実施";
            }

            return ItemManager.TransParams.RESULT_SUCCESS;
        }

        /// <summary>
        /// 集信ログを出力する
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="logFilePath"></param>
        /// <returns></returns>
        public int WriteRecvLog(int dateFrom, int dateTo, string logFilePath)
        {
            int res = 0;
            _output = new StringBuilder();

            Process ps = new Process();
            ps.StartInfo.FileName = Path.Combine(ServerIni.Setting.HulftExePath, "bin", "utllist.exe");
            ps.StartInfo.Arguments = string.Format("-r -f {2} -from {0} -to {1} -v84", dateFrom, dateTo, ServerIni.Setting.RHulftID);
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.ErrorDialog = true;
            ps.StartInfo.RedirectStandardOutput = true;
            ps.OutputDataReceived += OutputHandler;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("HULFT集信ログ出力 exe=[{0}] args=[{1}]", ps.StartInfo.FileName, ps.StartInfo.Arguments), 1);
            ps.Start();
            ps.BeginOutputReadLine();
            ps.WaitForExit();
            res = ps.ExitCode;

            // 標準出力をファイルに書き出す
            string result = _output.ToString();
            if ((res == 109) || (res == 81))
            {
                // res=81　utllist:対象データがありません。
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("res=[{0}] msg=[{1}]", res, result.Replace(Const.CRLF, "")), 1);

                // ログファイルなし
                result = "";
            }
            else if (res != 0)
            {
                throw new Exception(string.Format("集信ログ取得失敗 res=[{0}] msg=[{1}]", res, result.Replace(Const.CRLF, "")));
            }
            CommonUtil.WriteAllTextStream(logFilePath, result);

            return res;
        }

        /// <summary>
        /// 配信ログを出力する
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="logFilePath"></param>
        /// <returns></returns>
        public int WriteSendLog(int dateFrom, int dateTo, string logFilePath)
        {
            int res = 0;
            _output = new StringBuilder();

            Process ps = new Process();
            ps.StartInfo.FileName = Path.Combine(ServerIni.Setting.HulftExePath, "bin", "utllist.exe");
            ps.StartInfo.Arguments = string.Format("-s -f {2} -from {0} -to {1} -v84", dateFrom, dateTo, ServerIni.Setting.SHulftID);
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.ErrorDialog = true;
            ps.StartInfo.RedirectStandardOutput = true;
            ps.OutputDataReceived += OutputHandler;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("HULFT配信ログ出力 exe=[{0}] args=[{1}]", ps.StartInfo.FileName, ps.StartInfo.Arguments), 1);
            ps.Start();
            ps.BeginOutputReadLine();
            ps.WaitForExit();
            res = ps.ExitCode;

            // 標準出力をファイルに書き出す
            string result = _output.ToString();
            if ((res == 109) || (res == 81))
            {
                // res=81　utllist:対象データがありません。
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("res=[{0}] msg=[{1}]", res, result.Replace(Const.CRLF, "")), 1);

                // ログファイルなし
                result = "";
            }
            else if (res != 0)
            {
                throw new Exception(string.Format("配信ログ取得失敗 res=[{0}] msg=[{1}]", res, result.Replace(Const.CRLF, "")));
            }
            CommonUtil.WriteAllTextStream(logFilePath, result);

            return res;
        }

        /// <summary>
        /// HULFT 集信
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public int HulftStartReceive(string fileId)
		{
			// 戻り値：99 → 転送失敗
			// 　　　　 0 → 成功
			int res = 0;
			Process ps = new Process();
            ps.StartInfo.FileName = Path.Combine(ServerIni.Setting.HulftExePath, "bin", "utlrecv.exe");
			ps.StartInfo.Arguments = string.Format("-f {0} -sync -w 300", fileId);
			ps.StartInfo.CreateNoWindow = true;
			ps.StartInfo.UseShellExecute = false;
			ps.StartInfo.ErrorDialog = true;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("HULFT集信 exe=[{0}] args=[{1}]", ps.StartInfo.FileName, ps.StartInfo.Arguments), 1);
            ps.Start();
			ps.WaitForExit();
			res = ps.ExitCode;

			return res;
		}

		/// <summary>
		/// HULFT 配信
		/// </summary>
		/// <param name="fileId"></param>
		/// <returns></returns>
		private int HulftStartSend(string fileId, string sendFilePath)
        {
            // 戻り値：99 → 転送失敗
            // 　　　　 0 → 成功
            int res = 0;
            Process ps = new Process();
            ps.StartInfo.FileName = Path.Combine(ServerIni.Setting.HulftExePath, "bin", "utlsend.exe");
            ps.StartInfo.Arguments = string.Format("-f {0} -file {1} -sync", fileId, sendFilePath);
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.ErrorDialog = true;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("HULFT配信 exe=[{0}] args=[{1}]", ps.StartInfo.FileName, ps.StartInfo.Arguments), 1);
            ps.Start();
            ps.WaitForExit();
            res = ps.ExitCode;

            return res;
        }

        /// <summary>
        /// 子プロセスが標準出力に出力したときに呼び出されるメソッド
        /// </summary>
        /// <param name="o"></param>
        /// <param name="args"></param>
        private void OutputHandler(object o, DataReceivedEventArgs args)
        {
            _output.AppendLine(args.Data); // 出力されたデータを保存
        }

    }
}
