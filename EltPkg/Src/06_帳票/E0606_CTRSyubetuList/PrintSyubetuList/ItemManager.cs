﻿using System;
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

namespace PrintSyubetuList
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        public List<TBL_BANKMF> _bankMF = null;
        public List<TBL_SYURUIMF> _syuruiMF = null;

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
            FetchSyuruiMF();
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
        /// 種類情報の取得
        /// </summary>
        public void FetchSyuruiMF()
        {
            // SELECT実行
            string strSQL = TBL_SYURUIMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);
            _syuruiMF = new List<TBL_SYURUIMF>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _syuruiMF.Add(new TBL_SYURUIMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
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
            string strSQL = SQLPrint.GetSyubetuPrintData(AppInfo.Setting.GymId, DispParams.Date, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        detail.Add(new PrintDetail(tbl.Rows[i], this));
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
        /// 種類名の取得
        /// </summary>
        public string GetSyurui(int syurui)
        {
            IEnumerable<TBL_SYURUIMF> Data = _syuruiMF.Where(x => x._SYURUI_CODE == syurui);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_SYURUI_NAME;
        }

        /// <summary>
        /// ReportPath取得
        /// </summary>
        public string ReportPath()
        {
            return System.IO.Path.Combine(string.Format(NCR.Server.ReportPath, AppInfo.Setting.SchemaBankCD), "CTRSyubetuList.rpt");
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
            public int SyuruiCode { get; set; } = 0;
            public string SyuruiName { get; set; } = "";
            public long Count { get; set; } = 0;
            public long Amount { get; set; } = 0;

            public PrintDetail(DataRow dr, ItemManager _itemMgr)
            {
                if (dr["SYURUI_CODE"].Equals(DBNull.Value))
                {
                    SyuruiCode = 999;
                    SyuruiName = _itemMgr.GetSyurui(999);
                }
                else
                {
                    SyuruiCode = DBConvert.ToIntNull(dr["SYURUI_CODE"]);
                    SyuruiName = DBConvert.ToStringNull(dr["SYURUI_NAME"]);
                }
                Count = DBConvert.ToLongNull(dr["CNT"]);
                Amount = DBConvert.ToLongNull(dr["AMT"]);
            }
        }

    }
}
