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

namespace SearchOutclearingBranch
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;

		/// <summary>画面パラメータ</summary>
		public DisplayParams DispParams { get; set; }

        /// <summary>リスト表示データ</summary>
        public List<DataRow> ListData { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            this.DispParams = new DisplayParams();
			this.DispParams.Clear();
            this.ListData = new List<DataRow>();
        }

        /// <summary>
        /// リスト表示一覧取得
        /// </summary>
        public bool FetchListData(int ListDispLimit, out bool LimitOver, FormBase form = null)
        {
            // 初期化
            LimitOver = false;
            ListData = new List<DataRow>();

            // SELECT実行
            string strSQL = SQLSearch.Get_SearchOutclearingBranch(AppInfo.Setting.GymId, DispParams.InputDate, DispParams.ClearingDate, DispParams.BrCode, AppInfo.Setting.SchemaBankCD, ListDispLimit);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        if (i + 1 > ListDispLimit)
                        {
                            LimitOver = true;
                            break;
                        }
                        ListData.Add(tbl.Rows[i]);
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            public int InputDate { get; set; }
            public int ClearingDate { get; set; }
            public int BrCode { get; set; }

            public void Clear()
			{
                this.InputDate = -1;
                this.ClearingDate = -1;
                this.BrCode = -1;
			}
		}
    }
}
