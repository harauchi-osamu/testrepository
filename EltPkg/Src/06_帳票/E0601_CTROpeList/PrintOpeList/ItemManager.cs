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

namespace PrintOpeList
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        public List<TBL_BANKMF> _bankMF = null;
        private List<TBL_OPERATOR> _operatorMF = null;

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
            FetchOperatorMF();
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
        /// オペレータマスタ情報の取得
        /// </summary>
        public void FetchOperatorMF(FormBase form = null)
        {
            // SELECT実行
            _operatorMF = new List<TBL_OPERATOR>();
            string strSQL = TBL_OPERATOR.GetSelectQuery();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _operatorMF.Add(new TBL_OPERATOR(tbl.Rows[i]));
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
        /// 印刷データを取得する
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool GetPrintData(out List<PrintDetail> detail, FormBase form = null)
        {
            //初期化
            detail = new List<PrintDetail>();

            // SELECT実行
            string strSQL = SQLPrint.GetCTROpeListPrintData(DispParams.DateFrom, DispParams.DateTo, AppInfo.Setting.SchemaBankCD);
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
        /// オペレータ名の取得
        /// </summary>
        public string GeOperator(string openo)
        {
            IEnumerable<TBL_OPERATOR> Data = _operatorMF.Where(x => x._OPENO == openo);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_OPENAME;
        }

        /// <summary>
        /// ReportPath取得
        /// </summary>
        public string ReportPath()
        {
            return System.IO.Path.Combine(string.Format(NCR.Server.ReportPath, AppInfo.Setting.SchemaBankCD), "CTROpeList.rpt");
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public int DateFrom { get; set; } = 0;
            public int DateTo { get; set; } = 0;
        }

        /// <summary>
        /// 印刷データ
        /// </summary>
        public class PrintDetail
        {
            public int GymID { get; set; } = 0;
            public int ItemID { get; set; } = 0;
            public string ItemName { get; set; } = "";
            public string OpeNo { get; set; } = "";
            public long E_Count { get; set; } = 0;
            public long E_Time { get; set; } = 0;
            public long V_Count { get; set; } = 0;
            public long V_Time { get; set; } = 0;
            public long TeiseiCount { get; set; } = 0;
            public int SortNo { get; set; } = 0;

            public PrintDetail(DataRow dr)
            {
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                ItemID = DBConvert.ToIntNull(dr["ITEM_ID"]);
                ItemName = DBConvert.ToStringNull(dr["ITEM_NAME"]);
                OpeNo = DBConvert.ToStringNull(dr["OPENO"]);
                E_Count = DBConvert.ToLongNull(dr["E_CNT"]);
                E_Time = DBConvert.ToLongNull(dr["E_TIME"]);
                V_Count = DBConvert.ToLongNull(dr["V_CNT"]);
                V_Time = DBConvert.ToLongNull(dr["V_TIME"]);
                TeiseiCount = DBConvert.ToLongNull(dr["TEISEICNT"]);
                SortNo = GetSortNo(ItemID);
            }

            private int GetSortNo(int ItemID)
            {
                switch (ItemID)
                {
                    case DspItem.ItemId.券面持帰銀行コード:
                        return 1;
                    case DspItem.ItemId.金額:
                        return 2;
                    case DspItem.ItemId.入力交換希望日:
                        return 3;
                    case DspItem.ItemId.手形種類コード:
                        return 4;
                    case DspItem.ItemId.券面持帰支店コード:
                        return 5;
                    case DspItem.ItemId.券面口座番号:
                        return 6;
                    case DspItem.ItemId.手形番号:
                        return 7;
                    default:
                        //上記以外は後ろに表示
                        return ItemID * 1000;
                }
            }
        }

    }
}
