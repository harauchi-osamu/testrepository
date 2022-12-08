using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using HulftIO;

namespace MainMenu
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private const int RETRY_COUNT = 5;
        private const int RETRY_INTERVAL = 500;

        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        public PType ProcType { get; set; }
        public PMode ProcMode { get; set; }
        public AppMain.Type IsSuccess { get; private set; }

        /// <summary>
        /// 設定ファイルチェックの結果メッセージ
        /// 空の場合チェックOK
        /// </summary>
        public string CheckParamMsg { get; private set; } = "";

        /// <summary>
        /// ServerIniファイル存在チェック結果
        /// True：OK/ False：NG
        /// </summary>
        public bool ChkServerIni { get; private set; } = true;


        public enum PType
        {
            正常ジョブ,
            異常ジョブ
        }

        public enum PMode
        {
            配信,
            集信
        }

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
		}

		/// <summary>
		/// 引数を設定する
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public override bool SetArgs(string[] args)
        {
            string IniPath = args[0];
            NCR.Server.IniPath = IniPath;

            string ptype = args[1];
            if (!int.TryParse(ptype, out int nProcType))
            {
                throw new Exception("ジョブ種別が不正です");
            }
            string pmode = args[2];
            if (!int.TryParse(pmode, out int nProcMode))
            {
                throw new Exception("集配信区分が不正です");
            }

            // セクション名の設定
            string LogSectionName = args[3];
            NCR.Server.HULFTLogSectionName = LogSectionName;

            this.IsSuccess = AppMain.Type.Success;

            // ジョブ種別
            switch (nProcType)
            {
                case 1:
                    this.ProcType = PType.正常ジョブ;
                    break;
                case 2:
                    this.ProcType = PType.異常ジョブ;
                    break;
                default:
                    throw new Exception("ジョブ種別が不正です");
            }

            // 集配信区分
            switch (nProcMode)
            {
                case 1:
                    this.ProcMode = PMode.配信;
                    break;
                case 2:
                    this.ProcMode = PMode.集信;
                    break;
                default:
                    throw new Exception("集配信区分が不正です");
            }
            return true;
        }

        #region 設定チェック

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckServerIni()
        {
            // ServerIniファイル存在チェック
            if (!ServerIniExists())
            {
                return false;
            }

            ChkParam(NCR.Server.BankNM, "銀行名");
            ChkParam(NCR.Server.Environment, "環境");
            ChkParam(NCR.Server.HulftExePath, "HALFTプログラム");
            //ChkParam(NCR.Server.LogRoot, "HULFT集配信ログフォルダ");
            //ChkParam(NCR.Server.LogErrRoot, "HULFT集配信エラーログフォルダ");
            ChkParam(NCR.Server.SHulftID, "配信HULFTID");
            ChkParam(NCR.Server.RHulftID, "集信HULFTID");

            // HULFTセクション別設定
            ChkParam(NCR.Server.LogRoot, "HULFT集配信ログフォルダ");
            ChkParam(NCR.Server.LogErrRoot, "HULFT集配信エラーログフォルダ");
            ChkParam(NCR.Server.LogRecvFileName, "HULFT集信ログファイル名");
            ChkParam(NCR.Server.LogErrRecvFileName, "HULFT集信エラーログファイル名");
            ChkParam(NCR.Server.LogSendFileName, "HULFT配信ログファイル名");
            ChkParam(NCR.Server.LogErrSendFileName, "HULFT配信エラーログファイル名");

            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            return true;
        }

        #endregion

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// Server.ini 存在チェック
        /// </summary>
        /// <returns></returns>
        private bool ServerIniExists()
        {
            // ServerIniファイル存在チェック
            if (!System.IO.File.Exists(NCR.Server.IniPath))
            {
                ChkServerIni = false;
            }

            return true;
        }

        /// <summary>
        /// 設定内容チェック
        /// </summary>
        private bool ChkParam(string Item, string ItemName)
        {
            if (string.IsNullOrEmpty(Item))
            {
                SetCheckParamMsg(ItemName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 結果メッセージへの設定
        /// </summary>
        private void SetCheckParamMsg(string Msg)
        {
            if (string.IsNullOrEmpty(CheckParamMsg))
            {
                CheckParamMsg = Msg;
            }
            else
            {
                CheckParamMsg += "," + Msg;
            }
        }

        /// <summary>
        /// 集配信ログ出力処理を行う
        /// </summary>
        /// <returns></returns>
        public bool ExecHulftLogWrite()
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "集配信ログ出力処理 開始", 1);
            _itemMgr.DispParams.Clear();

            try
            {
                // ログフォルダ作成
                //int opedate = AplInfo.OpDate();
                //string sOpedate = opedate.ToString();
                //string logDirPath = Path.Combine(ServerIni.Setting.LogRoot, sOpedate);
                //string recvLogFilePath = Path.Combine(logDirPath, ServerIni.Setting.LogRecvFileName);
                //string sendLogFilePath = Path.Combine(logDirPath, ServerIni.Setting.LogSendFileName);
                string logDirPath = ServerIni.Setting.LogRoot;
                string recvLogFilePath = Path.Combine(logDirPath, ServerIni.Setting.LogRecvFileName);
                string sendLogFilePath = Path.Combine(logDirPath, ServerIni.Setting.LogSendFileName);
                if (!Directory.Exists(logDirPath))
                {
                    Directory.CreateDirectory(logDirPath);
                }

                // ログ出力
                switch (this.ProcMode)
                {
                    case PMode.配信:
                        WriteLogCommon(RETRY_COUNT, RETRY_INTERVAL, PType.正常ジョブ, PMode.配信, sendLogFilePath);
                        break;
                    case PMode.集信:
                        WriteLogCommon(RETRY_COUNT, RETRY_INTERVAL, PType.正常ジョブ, PMode.集信, recvLogFilePath);
                        break;
                }

                //// HULFT パラメーター
                //HulftIO.ItemManager hulftItem = new HulftIO.ItemManager(_masterMgr);

                //// HULFT コントローラー
                //HulftIO.Controller hulftCtl = new HulftIO.Controller();
                //hulftCtl.SetManager(_masterMgr, hulftItem);

                //// HULFT集配信ログ取得
                //int sysdate = DBConvert.ToIntNull(DateTime.Now.ToString("yyyyMMdd"));
                //switch (this.ProcMode)
                //{
                //    case PMode.配信:
                //        hulftCtl.WriteSendLog(sysdate, sysdate, sendLogFilePath);
                //        break;
                //    case PMode.集信:
                //        hulftCtl.WriteRecvLog(sysdate, sysdate, recvLogFilePath);
                //        break;
                //}
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "集配信ログ出力処理エラー：" + ex.ToString(), 3);
                this.IsSuccess = AppMain.Type.Error;
                return false;
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "集配信ログ出力処理 終了", 1);
            return true;
        }

        /// <summary>
        /// 集配信ログ出力処理を行う
        /// </summary>
        /// <returns></returns>
        public bool ExecHulftErrLogWrite()
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "集配信エラーログ出力処理 開始", 1);

            try
            {
                // ログフォルダ作成
                //int opedate = AplInfo.OpDate();
                //string sOpedate = opedate.ToString();
                //string errLogDirPath = Path.Combine(ServerIni.Setting.LogErrRoot, sOpedate);
                //string recvErrLogFilePath = Path.Combine(errLogDirPath, ServerIni.Setting.LogErrRecvFileName);
                //string sendErrLogFilePath = Path.Combine(errLogDirPath, ServerIni.Setting.LogErrSendFileName);
                string errLogDirPath = ServerIni.Setting.LogErrRoot;
                string recvErrLogFilePath = Path.Combine(errLogDirPath, ServerIni.Setting.LogErrRecvFileName);
                string sendErrLogFilePath = Path.Combine(errLogDirPath, ServerIni.Setting.LogErrSendFileName);
                if (!Directory.Exists(errLogDirPath))
                {
                    Directory.CreateDirectory(errLogDirPath);
                }

                // エラーログの空ファイル作成
                switch (this.ProcMode)
                {
                    case PMode.配信:
                        //CommonUtil.WriteAllTextStream(sendErrLogFilePath, string.Empty);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("配信エラー [{0}]", sendErrLogFilePath), 1);
                        WriteLogCommon(RETRY_COUNT, RETRY_INTERVAL, PType.異常ジョブ, PMode.配信, sendErrLogFilePath);
                        break;
                    case PMode.集信:
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("集信エラー [{0}]", recvErrLogFilePath), 1);
                        WriteLogCommon(RETRY_COUNT, RETRY_INTERVAL, PType.異常ジョブ, PMode.集信, recvErrLogFilePath);
                        //CommonUtil.WriteAllTextStream(recvErrLogFilePath, string.Empty);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "集配信エラーログ出力処理エラー：" + ex.ToString(), 3);
                this.IsSuccess = AppMain.Type.Error;
                return false;
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "集配信ログ出力処理 終了", 1);
            return true;
        }

        /// <summary>
        /// ログ出力共通処理
        /// </summary>
        /// <param name="retryCount">最大リトライ回数</param>
        /// <param name="retryInterval">リトライ間隔</param>
        /// <returns></returns>
        private void WriteLogCommon(int retryCount, int retryInterval, PType ptype, PMode pmode, string logFilePath)
        {
            // 指定回数リトライ
            int i = 0;
            do
            {
                try
                {
                    // 処理実行
                    switch (ptype)
                    {
                        case PType.正常ジョブ:

                            // HULFT パラメーター
                            HulftIO.ItemManager hulftItem = new HulftIO.ItemManager(_masterMgr);

                            // HULFT コントローラー
                            HulftIO.Controller hulftCtl = new HulftIO.Controller();
                            hulftCtl.SetManager(_masterMgr, hulftItem);
                            int sysdate = DBConvert.ToIntNull(DateTime.Now.ToString("yyyyMMdd"));

                            // HULFT集配信ログ取得
                            if (pmode == PMode.配信)
                            {
                                // 正常 & 配信
                                hulftCtl.WriteSendLog(sysdate, sysdate, logFilePath);
                            }
                            else //if (pmode == PMode.集信)
                            {
                                // 正常 & 集信
                                hulftCtl.WriteRecvLog(sysdate, sysdate, logFilePath);
                            }

                            // 処理成功時は終了
                            return;
                        case PType.異常ジョブ:
                            // 異常 & (配信/集信)
                            CommonUtil.WriteAllTextStream(logFilePath, string.Empty);

                            // 処理成功時は終了
                            return;
                    }
                }
                catch(Exception ex)
                {
                    // ログ出力してリトライ
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ログ出力処理エラー：" + ex.ToString(), 3);
                }

                //指定期間スリープ
                System.Threading.Thread.Sleep(retryInterval);

                i++;
            } while (i < retryCount);

            // 指定回数リトライして書込エラーの場合
            throw new Exception(string.Format("{0}回リトライしてログ出力に失敗しました。", retryCount));
        }

    }
}
