using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using NCR.GeneralMainte;
using Common;
using CommonClass;
using CommonClass.DB;

namespace MainMenu
{
    public class AppMain
    {
        /// <summary>	
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>		
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "プログラム起動", 1);

            // 二重起動チェック
            //if (System.Diagnostics.Process.GetProcessesByName(AplInfo.Aplname).Length > 1)
            //{
            //    MessageBox.Show("すでに起動しています。", "二重起動", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("二重起動により終了[{0}]", AplInfo.Aplname), 3);
            //    return;
            //}

            // DB初期化
            try
            {
                // hen
                DBManager.Initialize(NCR.Server.DBDataSource, NCR.Server.DBUserID, NCR.Server.DBPassword);
                DBManager.dbc.Open();
            }
            catch (Exception ex)
			{
				ComMessageMgr.MessageError(ComMessageMgr.E00005, ex.Message);
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ＤＢ接続失敗: " + ex.Message, 3);
				return;
			}

			// プログラム起動
			try
			{
				// アプリケーションの初期化
				AppController theAppController = new AppController();
				InputCheck ic = new InputCheck();
				InitialParameter ip = new InitialParameter();
				if (!theAppController.Start(args))
				{
					MessageBox.Show("アプリケーションの初期化中にエラーが発生しました。\n処理を中断します。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				// 汎用メンテナンス画面
				mainForm form = new mainForm(args, theAppController, ic, ip);
				form.ShowDialog();
                form = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("画面表示処理でエラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面表示処理失敗: " + ex.ToString(), 3);
            }
            finally
            {
				Close();
			}

			LogWriter.writeLog(MethodBase.GetCurrentMethod(), "プログラム終了", 1);
		}

		/// <summary>
		/// クローズ処理
		/// </summary>
		private static void Close()
		{
			if (DBManager.dbc != null)
			{
				DBManager.dbc.Close();
			}
			if (DBManager.dbs1 != null)
			{
				DBManager.dbs1.Close();
			}
			if (DBManager.dbs2 != null)
			{
				DBManager.dbs2.Close();
			}
		}

        /// <summary>
        /// モジュールのどこかでエラーが起こったものを最後に拾うエラーハンドリング
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object source, System.Threading.ThreadExceptionEventArgs e)
        {
            string errMessage = "";
            string errStack = "";
            for (Exception tempException = e.Exception; tempException != null; tempException = tempException.InnerException)
            {
                errMessage += tempException.Message + Environment.NewLine;
                errStack += tempException.StackTrace + Environment.NewLine;
            }
            MessageBox.Show(string.Format("想定されていないアプリケーションエラーが発生しました。" + Environment.NewLine + "以下エラーメッセージ内容を控えてください。"
                + Environment.NewLine + Environment.NewLine + "{0}", errMessage), "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "実行時エラー発生: " + errMessage + Environment.NewLine + errStack, 3);

            // 異常終了
            MessageBox.Show("強制終了します", "実行時エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();

            Environment.Exit(9);
        }
    }
}
