using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IIPCommonClass;
using IIPCommonClass.DB;
using IIPCommonClass.Log;


namespace IIPReference
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // 二重起動チェック
            if (System.Diagnostics.Process.GetProcessesByName(AplInfo.Aplname).Length > 1)
            {
                MessageBox.Show("すでに起動しています。", "二重起動", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcessLog.getInstance().writeLog("二重起動により終了");
                return;
            }


            List<DBManager> dBs = new List<DBManager>();
            var ServerInfo = new string[][] {

                //new string[]{Properties.Settings.Default.DataSource1, Properties.Settings.Default.InitialCatalog1,
                //       Properties.Settings.Default.UserId1, Properties.Settings.Default.Password1 },

                //new string[]{Properties.Settings.Default.DataSource2, Properties.Settings.Default.InitialCatalog2,
                // Properties.Settings.Default.UserId2, Properties.Settings.Default.Password2}

                new string[]{Properties.Settings.Default.DataSource1, Properties.Settings.Default.InitialCatalog1,
                       Properties.Settings.Default.UserId1, Properties.Settings.Default.Password1 }
                };
            int ServerCount = ServerInfo.Length;

            for (int i = 0; i < ServerInfo.Length; i++)
            {
                try
                {
                    DBManager dBManager1 = new DBManager(ServerInfo[i][0],  ServerInfo[i][2], ServerInfo[i][3]);
                    dBManager1.dbc.Open();
                    dBs.Add(dBManager1);
                }
                catch (Exception ex)
                {
                    ServerCount--;
                    ProcessLog.getInstance().writeLog("ＤＢ接続失敗: " + ex.Message
                        + " DataSourse:" + ServerInfo[i][0] + " InitialCatalog:" + ServerInfo[i][1] + " UserId:" + ServerInfo[i][2]);
                }
            }

            if (ServerCount == 0)
            {
                IniFileAccess.ShowMsgBox("E0001");
                return;
            }


            AppMainForm thefrm = null;
            try
            {
                thefrm = new AppMainForm(dBs, ServerCount);
                if (thefrm.InitializeForm())
                {
                    thefrm.ShowDialog();
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally
            {
                if (thefrm != null) { thefrm.Dispose(); }
            }

            for (int i = 0; i < dBs.Count; i++)
            {
                dBs[i].dbc.Close();
            }

            ProcessLog.getInstance().writeLog("プログラム終了");
            ProcessLog.getInstance().DelOldLog();
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
            ProcessLog.getInstance().writeLog("実行時エラー発生: " + errMessage + Environment.NewLine + errStack);

            // 異常終了
            MessageBox.Show("強制終了します", "実行時エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Environment.Exit(9);
        }
    }
}
