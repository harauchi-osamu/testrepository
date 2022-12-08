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

namespace MainMenu
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        /// <summary>ファイル集配信管理（key=主キー連結文字, val=TBL_FILE_CTL）</summary>
        public SortedDictionary<int, TBL_FILE_CTL> file_ctls { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.DispParams = new DisplayParams();
            this.DispParams.Clear();

            file_ctls = new SortedDictionary<int, TBL_FILE_CTL>();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public bool Fetch_file_ctls()
        {
            file_ctls = new SortedDictionary<int, TBL_FILE_CTL>();
            string strSQL = SQLFileCreate.GetHulftMgrFileCtlSelect(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_FILE_CTL data = new TBL_FILE_CTL(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        file_ctls.Add(i, data);
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    throw ex;
                }
            }
            return (file_ctls.Count > 0);
        }

        /// <summary>
        /// FILE_CTLの更新処理
        /// </summary>
        public void UpdateFileCtlStatus(TBL_FILE_CTL fctl, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string strSQL = fctl.GetUpdateQuery();
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
        }

        /// <summary>
        /// TRMEIIMGの更新処理
        /// </summary>
        public int UpdateTRMeiImgOCStatus(TBL_FILE_CTL fctl, Dictionary<string, int> UpdateData, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string strSQL = SQLFileCreate.GetTRMeiImgArchNameUpdate(GymParam.GymId.持出, fctl._SEND_FILE_NAME, UpdateData, AppInfo.Setting.SchemaBankCD);
            return dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
        }

        /// <summary>
        /// TRMEIの更新処理
        /// </summary>
        /// <remarks>2022/03/28 銀行導入工程_不具合管理表No97 対応</remarks>
        public int UpdateTRMeiSendStatus(TBL_FILE_CTL fctl, Dictionary<string, int> UpdateData, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string strSQL = SQLFileCreate.GetTRMeiSendStsUpdate(fctl._SEND_FILE_NAME, UpdateData, AppInfo.Setting.SchemaBankCD);
            return dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            /// <summary>HULFT 処理結果</summary>
            public int Result { get; set; }
            /// <summary>HULFT エラーメッセージ</summary>
            public string ErrMsg { get; set; }

            public void Clear()
            {
                this.Result = -1;
                this.ErrMsg = "";
            }
        }
    }
}
