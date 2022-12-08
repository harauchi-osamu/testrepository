using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using EntryClass;
using EntryCommon;
using CorrectInput;
using ParamMaint;

namespace MainMenu
{
	static class AppMain
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

            //二重起動チェック
            if (System.Diagnostics.Process.GetProcessesByName(AplInfo.Aplname).Length > 1)
            {
                MessageBox.Show("すでに起動しています。", "二重起動", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("二重起動により終了[{0}]", AplInfo.Aplname), 3);
                return;
            }

            // プログラム起動
            try
            {
				StartControl(args, new EntryClass.ControllerEntBase());
			}
			finally
			{
				Close();
			}

			LogWriter.writeLog(MethodBase.GetCurrentMethod(), "プログラム終了", 1);
            Environment.Exit(0);
        }

        /// <summary>
        /// プログラム起動処理
        /// </summary>
        /// <param name="args"></param>
        /// <param name="ctl"></param>
        private static void StartControl(string[] args, EntryClass.ControllerEntBase ctl)
		{
			// 引数チェック
			if (!CheckArgs(args, ctl))
			{
				return;
			}

            // コンフィグ検証
            if (!CheckConfigParams(ctl))
            {
                return;
            }

            // DB初期化
            try
            {
                DBManager.Initialize(NCR.Server.DBDataSource, NCR.Server.DBUserID, NCR.Server.DBPassword);
                DBManager.dbc.Open();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00005, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ＤＢ接続失敗: " + ex.ToString(), 3);
                return;
            }

            // アプリケーション初期化
            if (!InitializeApp(args, ctl))
            {
                return;
            }

            // 画面表示
            StartNavigate(ctl);
		}

		/// <summary>
		/// 引数をチェックする
		/// </summary>
		/// <param name="args"></param>
		/// <param name="ctl"></param>
		/// <returns></returns>
		private static bool CheckArgs(string[] args, EntryClass.ControllerEntBase ctl)
		{
			try
			{
				// 引数チェック
				if (args.Length < 4)
				{
                    ComMessageMgr.MessageError(ComMessageMgr.E00002);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "引数エラー ", 3);
					return false;
				}

                // 引数取得
                if (!ctl.SetArgs(args))
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00002);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "引数エラー ", 3);
                    return false;
                }
            }
			catch (Exception ex)
			{
				MessageBox.Show("引数取得処理でエラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), "引数取得失敗: " + ex.ToString(), 3);
				return false;
			}
			return true;
		}

        /// <summary>
        /// コンフィグ検証
        /// </summary>
        /// <returns></returns>
        private static bool CheckConfigParams(EntryClass.ControllerEntBase ctl)
        {
            // INIファイルチェック
            if (!ctl.CheckIniParams())
            {
                return false;
            }

            // exe.config チェック
            if (!ctl.CheckAppConfig())
            {
                return false;
            }

            // Server.ini 初期化
            ServerIni.Initialize(NCR.Operator.BankCD);
            return true;
        }

		/// <summary>
		/// アプリケーションを初期化する
		/// </summary>
		/// <param name="args"></param>
		/// <param name="ctl"></param>
		/// <returns></returns>
		private static bool InitializeApp(string[] args, EntryClass.ControllerEntBase ctl)
		{
			try
			{
				// 業務設定取得
				AppInfo.Setting.SetGymId(ctl.GymId);
				AppInfo.Setting.SetSchemaBankCD(NCR.Operator.BankCD);

                if (!AplInfo.GetGymSetting(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD))
                {
                    if (!ctl.IsMaint)
                    {
                        //メンテの場合はエラーなし
                        ComMessageMgr.MessageError(ComMessageMgr.E01002);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "業務設定取得エラー ", 3);
                        return false;
                    }
                }
            }
			catch (Exception ex)
			{
				MessageBox.Show("アプリケーション初期化処理でエラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), "アプリケーション初期化失敗: " + ex.ToString(), 3);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 画面表示
		/// </summary>
		/// <param name="ctl"></param>
		private static void StartNavigate(EntryClass.ControllerEntBase ctl)
		{
			try
			{
                // マスタテーブル取得
                MasterManager mst = new MasterManager();

                // 休日カレンダー取得
                iBicsCalendar cal = new iBicsCalendar();
                cal.SetHolidays();

                if (ctl.IsMaint)
                {
                    // 業務メンテナンス
                    ParamMaint.ItemManager pItem = new ParamMaint.ItemManager(mst);

                    // コントローラー初期化
                    ParamMaint.Controller pCtl = new ParamMaint.Controller(ctl);
                    pCtl.SetManager(mst, pItem);
                    pItem.FetchAllData(pCtl);

                    // メンテナンス画面
                    MainteForm frm = new MainteForm();
                    frm.InitializeForm(pCtl);

                    // フォーム起動
                    frm.ShowDialog();
                    frm = null;
                }
                else if (ctl.IsKanryouTeisei)
                {
                    // 完了訂正
                    CorrectInput.ItemManager cItem = new CorrectInput.ItemManager(mst);

                    // コントローラー初期化
                    CorrectInput.Controller cCtl = new CorrectInput.Controller(ctl);

                    // exe.config チェック
                    if (!cCtl.CheckAppConfig()) { return; }

                    cCtl.SetManager(mst, cItem);
                    cItem.FetchAllData(cCtl);

                    if (ctl.IsRenzokuTeisei)
                    {
                        //連続完了訂正実行
                        cCtl.DoRenzokuTeisei();
                    }
                    else
                    {
                        // 完了訂正実行
                        cCtl.DoEntryTeisei();
                    }
                }
                else
                {
                    // 明細一覧
                    CorrectInput.ItemManager cItem = new CorrectInput.ItemManager(mst);

                    // コントローラー初期化
                    CorrectInput.Controller cCtl = new CorrectInput.Controller(ctl);

                    // exe.config チェック
                    if (!cCtl.CheckAppConfig()) { return; }

                    cCtl.SetManager(mst, cItem);
                    cItem.FetchAllData(cCtl);

                    // 明細一覧画面
                    BatchListForm frm = new BatchListForm();
                    frm.InitializeForm(cCtl);

                    // フォーム起動
                    frm.ShowDialog();
                    frm = null;
                }
            }
            catch (Exception ex)
			{
				MessageBox.Show("画面表示処理でエラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面表示処理失敗: " + ex.ToString(), 3);
			}
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
