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

namespace PrintOcBatchTotal
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        public List<TBL_BANKMF> _bankMF = null;
        public List<TBL_BRANCHMF> _branchMF = null;

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.DispParams = new DisplayParams();
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        public override void FetchAllData()
        {
            FetchBankMF();
            FetchBranchMF();
        }

        /// <summary>
        /// 銀行情報の取得
        /// </summary>
        public void FetchBankMF()
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
                }
            }
        }

        /// <summary>
        /// 支店情報の取得
        /// </summary>
        public void FetchBranchMF(FormBase form = null)
        {
            // SELECT実行
            string strSQL = TBL_BRANCHMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);
            _branchMF = new List<TBL_BRANCHMF>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _branchMF.Add(new TBL_BRANCHMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message)); }
                }
            }
        }

        /// <summary>
        /// 印刷データを取得する
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool GetPrintData(out List<PrintDetail> detail, FormBase form = null)
        {
            //初期化
            detail = new List<PrintDetail>();

            // SELECT実行
            string strSQL = SQLPrint.GetOcBatchTotalPrintData(AppInfo.Setting.GymId, DispParams.Date, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        detail.Add(new PrintDetail(tbl.Rows[i]));
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
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
        /// 支店名の取得
        /// </summary>
        public string GeBranch(int brno)
        {
            IEnumerable<TBL_BRANCHMF> Data = _branchMF.Where(x => x._BR_NO == brno);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_BR_NAME_KANJI;
        }

        /// <summary>
        /// ReportPath取得
        /// </summary>
        public string ReportPath()
        {
            return System.IO.Path.Combine(string.Format(NCR.Server.ReportPath, AppInfo.Setting.SchemaBankCD), "CTROcBatchTotaList.rpt");
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public int Date { get; set; } = 0;
        }

        /// <summary>
        /// 印刷データ
        /// </summary>
        public class PrintDetail
        {
            public int OpeDate { get; set; } = 0;
            public int BatID { get; set; } = 0;
            public int OCBKNo { get; set; } = 0;
            public int OCBRNo { get; set; } = 0;
            public int ClearingDate { get; set; } = 0;
            public long TotalCount { get; set; } = 0;
            public long TotalAmount { get; set; } = 0;
            public long Count { get; set; } = 0;
            public long Amount { get; set; } = 0;

            public PrintDetail(DataRow dr)
            {
                OpeDate = DBConvert.ToIntNull(dr["OPERATION_DATE"]);
                BatID = DBConvert.ToIntNull(dr["BAT_ID"]);
                OCBKNo = DBConvert.ToIntNull(dr["OC_BK_NO"]);
                OCBRNo = DBConvert.ToIntNull(dr["OC_BR_NO"]);
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                TotalCount = DBConvert.ToLongNull(dr["TOTAL_COUNT"]);
                TotalAmount = DBConvert.ToLongNull(dr["TOTAL_AMOUNT"]);
                Count = DBConvert.ToLongNull(dr["CNT"]);
                Amount = DBConvert.ToLongNull(dr["AMT"]);
            }
        }

    }
}
