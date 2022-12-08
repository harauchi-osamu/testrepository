using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;
using PrintOcBatchTotal;

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

			// 二重起動チェック
			if (System.Diagnostics.Process.GetProcessesByName(AplInfo.Aplname).Length > 1)
			{
				MessageBox.Show("すでに起動しています。", "二重起動", MessageBoxButtons.OK, MessageBoxIcon.Error);
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("二重起動により終了[{0}]", AplInfo.Aplname), 3);
				return;
			}

			// プログラム起動
			try
			{
				StartControl(args, new Controller());
			}
			finally
			{
				Close();
			}

			LogWriter.writeLog(MethodBase.GetCurrentMethod(), "プログラム終了", 1);
		}

		/// <summary>
		/// プログラム起動処理
		/// </summary>
		/// <param name="args"></param>
		/// <param name="ctl"></param>
		private static void StartControl(string[] args, Controller ctl)
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
		private static bool CheckArgs(string[] args, Controller ctl)
		{
			try
			{
				// 引数チェック
				if (args.Length < 1)
				{
					CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00002);
					LogWriter.writeLog(MethodBase.GetCurrentMethod(), "引数エラー ", 3);
					return false;
				}

				// 引数取得
				ctl.SetArgs(args);
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
		private static bool CheckConfigParams(Controller ctl)
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
			return true;
		}

		/// <summary>
		/// アプリケーションを初期化する
		/// </summary>
		/// <param name="args"></param>
		/// <param name="ctl"></param>
		/// <returns></returns>
		private static bool InitializeApp(string[] args, Controller ctl)
		{
			try
			{
				// 業務設定取得
				// 1:持出を設定(0:共通、1:持出、2:持帰)
				AppInfo.Setting.SetGymId(GymParam.GymId.持出);
				AppInfo.Setting.SetSchemaBankCD(NCR.Operator.BankCD);
				if (!AplInfo.GetGymSetting(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD))
				{
					CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E01002);
					LogWriter.writeLog(MethodBase.GetCurrentMethod(), "業務設定取得エラー ", 3);
					return false;
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
		private static void StartNavigate(Controller ctl)
		{
			try
			{
				// マスタテーブル取得
				MasterManager mst = new MasterManager();
				mst.FetchAllData();

				// トランザクションテーブル取得
				ItemManager item = new ItemManager(mst);
				item.FetchAllData();
				// コントローラー初期化
				ctl.SetManager(mst, item);

                // フォーム初期化
                PrintBatchTotal form = new PrintBatchTotal();
				form.InitializeForm(ctl);

				// フォーム起動
				form.ShowDialog();
				form = null;
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
