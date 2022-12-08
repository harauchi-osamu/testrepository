using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;
using HulftIO;

namespace CTRHulftRireki
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;
        private Controller _ctl = null;

        public Dictionary<string, string> SendNameList { get; private set; } = null;
        public Dictionary<string, string> RecvNameList { get; private set; } = null;

        /// <summary>ファイル集配信管理</summary>
        public DataTable tbl_file_ctls { get; set; }
        /// <summary>ファイルパラメーター</summary>
        public DataTable tbl_file_params { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>ヘッダー情報</summary>
        public HeaderInfos HeaderInfo { get; set; }

        public enum ProcTypes
        {
            配信,
            集信
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            this.DispParams = new DisplayParams();
			this.DispParams.Clear();
            this.HeaderInfo = new HeaderInfos();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void FetchAllData(Controller ctl)
        {
            _ctl = ctl;
            if (_ctl.IsIniErr) { return; }

            Fetch_file_ctls();
            Fetch_file_params();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void Fetch_file_ctls()
        {
            string strSQL = TBL_FILE_CTL.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            tbl_file_ctls = new DataTable();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    tbl_file_ctls = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void Fetch_file_params()
        {
            string strSQL = TBL_FILE_PARAM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            SendNameList = new Dictionary<string, string>();
            RecvNameList = new Dictionary<string, string>();
            tbl_file_params = new DataTable();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    tbl_file_params = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    foreach (DataRow row in tbl_file_params.Rows)
                    {
                        TBL_FILE_PARAM data = new TBL_FILE_PARAM(row, AppInfo.Setting.SchemaBankCD);
                        string key = string.Format("{0}{1}", data._FILE_ID, data._FILE_DIVID);
                        switch (data.m_FILE_COURSE)
                        {
                            case 0:
                                SendNameList.Add(string.Format("{0}{1}", data._FILE_ID, data._FILE_DIVID), data.m_FILE_NAME);
                                break;
                            case 1:
                                RecvNameList.Add(string.Format("{0}{1}", data._FILE_ID, data._FILE_DIVID), data.m_FILE_NAME);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ファイル退避でのFILE_CTL更新処理
        /// </summary>
        public void UpdateFileCtlFileMove(HulftLog.RecordInfo rec, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // 最新データの取得
            TBL_FILE_CTL filectl = null;
            string strSQL = TBL_FILE_CTL.GetSelectQuery(rec.fctl._FILE_ID, rec.fctl._FILE_DIVID, rec.fctl._SEND_FILE_NAME, rec.fctl._CAP_FILE_NAME, AppInfo.Setting.SchemaBankCD);
            using (DataTable dt = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), non.Trans))
            {
                if (dt.Rows.Count == 0)
                {
                    // ないとは思うが取得データがない場合
                    throw new Exception("FILE_CTLの取得に失敗しました");
                }
                filectl = new TBL_FILE_CTL(dt.Rows[0], AppInfo.Setting.SchemaBankCD);
            }

            // 最新データのステータス確認
            if (filectl.m_CAP_STS != FileCtl.CapSts.取込エラー)
            {
                // 取込エラー以外の場合
                throw new Exception("FILE_CTLのステータスが不正です");
            }

            // ステータス更新
            filectl.m_CAP_STS = FileCtl.CapSts.取込保留;
            UpdateFileCtlStatus(filectl, dbp, non);

            // RecordInfoに反映
            rec.fctl = filectl;
        }

        /// <summary>
        /// FILE_CTLの更新処理
        /// </summary>
        private void UpdateFileCtlStatus(TBL_FILE_CTL fctl, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string strSQL = fctl.GetUpdateQuery();
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            public ProcTypes ProcType { get; set; }
            public HulftLog HulftLogs { get; set; }
            public string SendItemValue { get; set; }
            public string RecvItemValue { get; set; }
            public string SendSelectItemKey { get; set; }
            public string SendSelectItemFileKey { get; set; }
            public string RecvSelectItemKey { get; set; }
            public string RecvSelectItemFileKey { get; set; }

            public HulftLog.RecordInfo LogRecord { get; set; }

            public void Clear()
			{
                this.ProcType = ProcTypes.集信;
                this.HulftLogs = null;
                this.SendItemValue = "";
                this.RecvItemValue = "";
                this.SendSelectItemKey = "";
                this.SendSelectItemFileKey = "";
                this.RecvSelectItemKey = "";
                this.RecvSelectItemFileKey = "";
                this.LogRecord = null;
            }
        }

        /// <summary>
        /// ヘッダー情報
        /// </summary>
        public class HeaderInfos
        {
            public int MiHaishinCnt { get; set; } = 0;
            public int MiTorikomiCnt { get; set; } = 0;
            public bool IsAutoRefresh { get; set; } = false;
        }

    }
}
