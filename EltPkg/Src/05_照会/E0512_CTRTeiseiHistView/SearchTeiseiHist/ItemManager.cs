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

namespace SearchTeiseiHist
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }
        public List<DataRow> ListData { get; set; }
        /// <summary>一覧画面表示データ</summary>
        public Dictionary<string, TBL_TRITEM_HIST> tritem_hist { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.DispParams = new DisplayParams();
            this.DispParams.Clear();
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public int operationdate { get; set; }
            public int batid { get; set; }
            public int detailsno { get; set; }
            public int updatedate { get; set; }
            public int updatetime { get; set; }
            public int updatetime2 { get; set; }
            public string itemname { get; set; }
            public string itemvalue { get; set; }
            public string imgflnm { get; set; }
            public int radioselect { get; set; }
            public void Clear()
            {
                this.operationdate = -1;
                this.batid = -1;
                this.detailsno = -1;
                this.updatedate = -1;
                this.updatetime = -1;
                this.updatetime2 = -1;
                this.radioselect = 0;
                this.itemname = "";
                this.itemvalue = "";
                this.imgflnm = "";
            }
        }

        /// <summary>
        /// 項目名所取得
        /// </summary>
        public bool FetchItemName(FormBase form = null)
        {
            ListData = new List<DataRow>();
            string strSQL = SQLSearch.Get_SearchTeiseiHistItemName(AppInfo.Setting.SchemaBankCD);

            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
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
        /// 検索結果の取得
        /// </summary>
        public bool FetchListData(int ListDispLimit, out bool LimitOver, FormBase form = null)
        {
            // 初期化
            LimitOver = false;

            // SELECT実行
            string strSQL = SQLSearch.Get_SearchTeiseiHistDataList(AppInfo.Setting.GymId, DispParams.operationdate, DispParams.batid, DispParams.detailsno, 
                                                                   DispParams.updatedate, DispParams.updatetime, DispParams.updatetime2, DispParams.itemname, 
                                                                   DispParams.itemvalue, DispParams.imgflnm, DispParams.radioselect, 
                                                                   AppInfo.Setting.SchemaBankCD, ListDispLimit);
            tritem_hist = new Dictionary<string, TBL_TRITEM_HIST>();
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
                        TBL_TRITEM_HIST ctl = new TBL_TRITEM_HIST(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        string key = ctl._GYM_ID + "_" + ctl._OPERATION_DATE + "_" + ctl._SCAN_TERM + "_" + ctl._BAT_ID + "_" + ctl._DETAILS_NO + "_" + ctl._ITEM_ID + "_" + ctl._SEQ;
                        tritem_hist.Add(key, ctl);

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
    }
}
