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
using System.IO;

namespace SearchIcBkView
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;
        private List<TBL_BANKMF> _bankMF = null;

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
		}

        /// <summary>
        /// データ読み込み
        /// </summary>
        public override void FetchAllData()
        {
            FetchBankMF();
        }

        /// <summary>
        /// 銀行情報の取得
        /// </summary>
        public void FetchBankMF(FormBase form = null)
        {
            // SELECT実行
            string strSQL = TBL_BANKMF.GetSelectQuery();
            _bankMF = new List<TBL_BANKMF>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _bankMF.Add(new TBL_BANKMF(tbl.Rows[i]));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message)); }
                }
            }
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
            string strSQL = SQLSearch.GetSearchIcBkView(AppInfo.Setting.GymId, DispParams.InputDate, DispParams.ClearingDate, DispParams.OCBkCode, AppInfo.Setting.SchemaBankCD, ListDispLimit);
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
        /// 銀行名の取得
        /// </summary>
        public string GetBank(string bkno)
        {
            if (!int.TryParse(bkno, out int intbkno))
            {
                return string.Empty;
            }
            return GetBank(intbkno);
        }

        /// <summary>
        /// 銀行名の取得
        /// </summary>
        public string GetBank(int bkno)
        {
            IEnumerable<TBL_BANKMF> Data = _bankMF.Where(x => x._BK_NO == bkno);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_BK_NAME_KANJI;
        }

        /// <summary>
        /// 別プロセスの実行
        /// </summary>
        public bool RunProcess(string ExeName, string Argument, FormBase form = null)
        {
            string WorkDir = string.Format(NCR.Server.ExePath, AppInfo.Setting.SchemaBankCD);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("プログラム起動：{0} 引数：{1}", ExeName, Argument), 3);
            int Rtn = ProcessManager.RunProcess(Path.Combine(WorkDir, ExeName), WorkDir, Argument, true, false);
            if (Rtn != 0)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("実行処理でエラーが発生しました{0} 戻り値：{1}", ExeName, Rtn), 3);
                return false;
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
            public int OCBkCode { get; set; }
            public string Key { get; set; }
            

            public void Clear()
            {
                this.InputDate = -1;
                this.ClearingDate = -1;
                this.OCBkCode = -1;
                this.Key = "";
            }
        }
    }
}
