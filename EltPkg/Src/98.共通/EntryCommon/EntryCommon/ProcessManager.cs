using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace EntryCommon
{
    public class ProcessManager
    {
        public const int PROC_NOT_FOUND = -9999;
        public const int PROC_SUCCESS = 0;

        // 対象プロセスの前面表示
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private ProcessManager() { }

        /// <summary>
        /// プロセスを実行する
        /// </summary>
        /// <param name="procPath"></param>
        /// <param name="procWorkDir"></param>
        /// <param name="procArgument"></param>
        /// <param name="isSync"></param>
        /// <returns></returns>
        public static int RunProcess(string procPath, string procWorkDir, string procArgument, bool isSync = true, bool isUseShellExecute = true, bool ActiveWindow = true)
        {
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = procPath;
            pInfo.WorkingDirectory = procWorkDir;
            pInfo.Arguments = procArgument;
            pInfo.UseShellExecute = isUseShellExecute;

            if (!File.Exists(pInfo.FileName))
            {
                return PROC_NOT_FOUND;
            }
            return ExecProcess(pInfo, isSync, ActiveWindow);
        }

        /// <summary>
        /// プロセスを実行する
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="isSync"></param>
        /// <param name="ActiveWindow"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int RunProcess(ProcessStartInfo pInfo, bool isSync, bool ActiveWindow, ProcessWindowStyle style)
        {
            return ExecProcess(pInfo, isSync, ActiveWindow, style);
        }

        /// <summary>
        /// 指定したプロセスを起動する
        /// </summary>
        /// <param name="pInfo">起動プロセス情報</param>
        /// <param name="isSync">同期実行フラグ</param>
        /// <returns>isSyncがFalseの場合はエラーとなるので注意</returns>
        private static int ExecProcess(ProcessStartInfo pInfo, bool isSync, bool ActiveWindow, ProcessWindowStyle style = ProcessWindowStyle.Normal)
        {
            int retVal = 0;
            Process proc = new Process();
            try
            {
                // パラメータ設定
                pInfo.WindowStyle = style;
                pInfo.ErrorDialog = false;
                proc.StartInfo = pInfo;

                // プロセス実行
                proc.Start();

                // 終了するまで待機
                if (isSync)
                {
                    if (ActiveWindow)
                    {
                        // 画面の前面表示
                        // (起動プロセスの画面が呼び元の画面の裏に隠れるケースがあるため前面表示)
                        ProcessActiveWindow(proc);
                    }

                    // 待機
                    proc.WaitForExit();
                    retVal = proc.ExitCode;
                }
                else
                {
                    retVal = PROC_SUCCESS;
                }
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return retVal;
        }

        /// <summary>
        /// 指定プロセス画面を前面表示
        /// </summary>
        /// <param name="proc">起動プロセス</param>
        /// <returns>出力画面がないプロセスの場合は終了まで待つ</returns>
        private static void ProcessActiveWindow(Process proc)
        {
            // 実行タイミングによっては前面表示に成功しても
            // 裏に隠れる場合があるため2回実施
            for (int i = 0; i < 2; i++)
            {
                while (!proc.HasExited)
                {
                    // 少し待つ
                    proc.WaitForExit(1000);

                    // 対象プロセスの画面を前面表示
                    if (ProcessFromActive(proc))
                    {
                        // 成功時
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 画面の前面表示
        /// </summary>
        /// <param name="proc">起動プロセス</param>
        /// <returns></returns>
        private static bool ProcessFromActive(Process proc)
        {
            try
            {
                if (proc.HasExited || proc.MainWindowHandle == IntPtr.Zero)
                {
                    // プロセスが終了 または WindowHandleがなし(画面表示まで進んでいない OR 画面がない) 
                    return false;
                }

                // 指定プロセスの画面を前面表示(できなかった場合はfalse)
                return SetForegroundWindow(proc.MainWindowHandle);
            }
            catch
            {
                // MainWindowHandleを取得時にエラーとなる場合があるためエラーハンドリング
                return false;
            }
        }

        /// <summary>
        /// 指定したプロセスを終了する
        /// </summary>
        /// <param name="procPath"></param>
        public static void Kill(string procPath)
        {
            string procNmae = Path.GetFileNameWithoutExtension(procPath);
            Process[] ps = Process.GetProcessesByName(procNmae);
            foreach (Process p in ps)
            {
                // クローズメッセージを送信する
                p.CloseMainWindow();

                // プロセスが終了するまで最大500ミリ秒待機する
                p.WaitForExit(500);

                // 終了していない場合は強制終了する
                if (p.HasExited)
                {
                    ForceKill(procPath);
                }
            }
        }

        /// <summary>
        /// 指定したプロセスを強制終了する
        /// </summary>
        /// <param name="procPath"></param>
        public static void ForceKill(string procPath)
        {
            string procNmae = Path.GetFileNameWithoutExtension(procPath);
            Process[] ps = Process.GetProcessesByName(procNmae);
            foreach (Process p in ps)
            {
                // プロセスを強制的に終了させる
                p.Kill();
            }
        }

        /// <summary>
        /// 指定したプロセスが実行中かどうかチェックする
        /// </summary>
        /// <param name="procPath"></param>
        /// <returns></returns>
        public static bool IsLivingProcess(string procPath)
        {
            string procNmae = Path.GetFileNameWithoutExtension(procPath);
            Process[] ps = Process.GetProcessesByName(procNmae);
            return (ps.Length > 0);
        }
    }
}
