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

namespace PrintMeiList
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;
        public List<TBL_BANKMF> _bankMF = null;
        public List<TBL_BRANCHMF> _branchMF = null;
        public List<TBL_BILLMF> _billMF = null;
        public List<TBL_SYURUIMF> _syuruiMF = null;
        public List<TBL_WK_IMGELIST> _printList = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
		}

        /// <summary>
        /// データ読み込み
        /// </summary>
        public override void FetchAllData()
        {
            FetchBankMF();
            FetchBranchMF();
            FetchBillMF();
            FetchSyuruiMF();
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
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message)); }
                }
            }
        }

        /// <summary>
        /// 交換券種情報の取得
        /// </summary>
        public void FetchBillMF(FormBase form = null)
        {
            // SELECT実行
            string strSQL = TBL_BILLMF.GetSelectQuery();
            _billMF = new List<TBL_BILLMF>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _billMF.Add(new TBL_BILLMF(tbl.Rows[i]));
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
        /// 種類情報の取得
        /// </summary>
        public void FetchSyuruiMF(FormBase form = null)
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
                    if (form != null) { form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message)); }
                }
            }
        }

        /// <summary>
        /// 印刷データを取得する
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool GetPrintData(FormBase form = null)
        {
            // SELECT実行
            string strSQL = TBL_WK_IMGELIST.GetSelectQuery(GetTermIPAddress(), AppInfo.Setting.GymId);
            _printList = new List<TBL_WK_IMGELIST>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _printList.Add(new TBL_WK_IMGELIST(tbl.Rows[i]));
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
        }

        /// <summary>
        /// 印刷明細データを取得する
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool GetMeiPrintData(TBL_WK_IMGELIST data, out List<PrintDetail> detail, out int ImgSizeKbn, FormBase form = null)
        {
            //初期化
            detail = new List<PrintDetail>();
            ImgSizeKbn = TrMeiImg.ImgKbn.表;

            // SELECT実行
            string strSQL = SQLPrint.GetMeiListPrintData(data._OPERATION_DATE, data._GYM_ID, data._SCAN_TERM, data._BAT_ID, data._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        detail.Add(new PrintDetail(tbl.Rows[i]));
                    }

                    // イメージサイズの基準区分設定
                    if (detail.Where(x => x.ImgKbn == TrMeiImg.ImgKbn.表再送分).Count() > 0)
                    {
                        // 表再送があれば設定
                        ImgSizeKbn = TrMeiImg.ImgKbn.表再送分;
                    }

                    // 表示順設定
                    int SortType = ImgSizeKbn;
                    if (ImgSizeKbn == TrMeiImg.ImgKbn.表 && 
                        detail.Where(x => x.ImgKbn == TrMeiImg.ImgKbn.裏再送分).Count() > 0)
                    {
                        // 裏再送があって表再送がない場合
                        SortType = TrMeiImg.ImgKbn.裏再送分;
                    }
                    foreach(PrintDetail pData in detail)
                    {
                        int SortCoef = 1000; // ソート順係数
                        switch (pData.ImgKbn)
                        {
                            case TrMeiImg.ImgKbn.表再送分:
                            case TrMeiImg.ImgKbn.裏再送分:
                                // 表・裏再送は優先表示
                                SortCoef = 1;
                                break;
                            case TrMeiImg.ImgKbn.表:
                                if (SortType == TrMeiImg.ImgKbn.裏再送分)
                                {
                                    // 裏再送があって表再送がない場合は優先表示
                                    SortCoef = 1;
                                }
                                else
                                {
                                    // 上記以外は後ろ表示
                                    SortCoef = 1000;
                                }
                                break;
                            default:
                                // 上記以外は後ろ表示
                                SortCoef = 1000;
                                break;
                        }

                        pData.SortNo = pData.ImgKbn + SortCoef;
                    }

                    return true;
                }
                catch (Exception ex)
                {
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
        /// 交換券種名の取得
        /// </summary>
        public string GeBill(int billcode)
        {
            IEnumerable<TBL_BILLMF> Data = _billMF.Where(x => x._BILL_CODE == billcode);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_STOCK_NAME;
        }

        /// <summary>
        /// 種類名の取得
        /// </summary>
        public string GeSyurui(int Syuruicode)
        {
            IEnumerable<TBL_SYURUIMF> Data = _syuruiMF.Where(x => x._SYURUI_CODE == Syuruicode);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_SYURUI_NAME;
        }

        /// <summary>
        /// ファイル出力パスを取得
        /// </summary>
        public string ReportFileOutPutPath()
        {
            return NCR.Server.ReportFileOutPutPath;
        }

        /// <summary>
        /// 端末IPアドレスの取得
        /// </summary>
        public string GetTermIPAddress()
        {
            return ImportFileAccess.GetTermIPAddress().Replace(".", "_");
        }

        /// <summary>
        /// 端末IPアドレスの取得（前ゼロ）
        /// </summary>
        public string GetTermIPAddressZeroPad()
        {
            string RtnValue = string.Empty;
            foreach(string Data in ImportFileAccess.GetTermIPAddress().Split('.'))
            {
                RtnValue += CommonUtil.PadLeft(Data, 3, "0");
            }
            return RtnValue;
        }

        /// <summary>
        /// バッチフォルダパスを取得
        /// </summary>
        public string GetBankBacthFolder(int opeDate, int BatID, int InputRoute)
        {
            string FolderPath = string.Empty;
            if (AppInfo.Setting.GymId == GymParam.GymId.持帰)
            {
                FolderPath = BankConfirmImageRoot();
            }
            else
            {
                switch (InputRoute)
                {
                    case TrBatch.InputRoute.通常:
                        FolderPath = BankNormalImageRoot();
                        break;
                    case TrBatch.InputRoute.付帯:
                        FolderPath = BankFutaiImageRoot();
                        break;
                    case TrBatch.InputRoute.期日管理:
                        FolderPath = BankInventoryImageRoot();
                        break;
                }
            }

            return System.IO.Path.Combine(FolderPath, AppInfo.Setting.GymId.ToString("D3") + opeDate.ToString("D8") + BatID.ToString("D8"));
        }

        /// <summary>
        /// 通常バッチルートフォルダパスを取得
        /// </summary>
        private string BankNormalImageRoot()
        {
            return string.Format(NCR.Server.BankNormalImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 付帯バッチルートフォルダパスを取得
        /// </summary>
        private string BankFutaiImageRoot()
        {
            return string.Format(NCR.Server.BankFutaiImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 期日管理バッチルートフォルダパスを取得
        /// </summary>
        private string BankInventoryImageRoot()
        {
            return string.Format(NCR.Server.BankInventoryImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 持帰ダウンロード確定イメージルートフォルダパスを取得
        /// </summary>
        private string BankConfirmImageRoot()
        {
            return string.Format(NCR.Server.BankConfirmImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// ReportPath取得
        /// </summary>
        public string ReportPath()
        {
            return System.IO.Path.Combine(string.Format(NCR.Server.ReportPath, AppInfo.Setting.SchemaBankCD), "CTRMeiList.rpt");
        }

        /// <summary>
        /// 印刷データ
        /// </summary>
        public class PrintDetail
        {
            public int GymId { get; set; } = 0;
            public int OpeDate { get; set; } = 0;
            public string ScanTerm { get; set; } = "";
            public int BatID { get; set; } = 0;
            public int DetailNo { get; set; } = 0;
            public int ImgKbn { get; set; } = 0;
            public string ImgFlnm { get; set; } = "";
            public int InputRoute { get; set; } = 0;
            public int OCBKNo { get; set; } = 0;
            public int OCBRNo { get; set; } = 0;
            public int ICOCBKNo { get; set; } = 0;
            public string ICBKKNo { get; set; } = "";
            public string ClearingDate{ get; set; } = "";
            public string Amt { get; set; } = "";
            public string BillCD { get; set; } = "";
            public string SyuruiCD { get; set; } = "";
            public string ICBRNo { get; set; } = "";
            public string Account { get; set; } = "";
            public string BillNo { get; set; } = "";
            /// <summary>表示順ソート番号</summary>
            public int SortNo { get; set; } = 0;

            public PrintDetail(DataRow dr)
            {
                GymId = DBConvert.ToIntNull(dr["GYM_ID"]);
                OpeDate = DBConvert.ToIntNull(dr["OPERATION_DATE"]);
                ScanTerm = DBConvert.ToStringNull(dr["SCAN_TERM"]);
                BatID = DBConvert.ToIntNull(dr["BAT_ID"]);
                DetailNo = DBConvert.ToIntNull(dr["DETAILS_NO"]);
                ImgKbn = DBConvert.ToIntNull(dr["IMG_KBN"]);
                ImgFlnm = DBConvert.ToStringNull(dr["IMG_FLNM"]);
                InputRoute = DBConvert.ToIntNull(dr["INPUT_ROUTE"]);
                OCBKNo = DBConvert.ToIntNull(dr["OC_BK_NO"]);
                OCBRNo = DBConvert.ToIntNull(dr["OC_BR_NO"]);
                ICOCBKNo = DBConvert.ToIntNull(dr["IC_OC_BK_NO"]);
                ICBKKNo = DBConvert.ToStringNull(dr["ICBKKNO"]);
                ClearingDate = DBConvert.ToStringNull(dr["CLEARING_DATE"]);
                Amt = DBConvert.ToStringNull(dr["AMT"]);
                BillCD = DBConvert.ToStringNull(dr["BILLCD"]);
                SyuruiCD = DBConvert.ToStringNull(dr["SYURUICD"]);
                ICBRNo = DBConvert.ToStringNull(dr["ICBRNO"]);
                Account = DBConvert.ToStringNull(dr["ACCOUNT"]);
                BillNo = DBConvert.ToStringNull(dr["BILLNO"]);
            }
        }

        /// <summary>
        /// 印刷データ一式
        /// </summary>
        public class PrintData
        {
            public TBL_WK_IMGELIST WkImgList = null;
            public List<PrintDetail> Details = null;
            /// <summary>イメージサイズの基準区分</summary>
            public int ImgSizeKbn { get; } = 0;

            public PrintData(TBL_WK_IMGELIST Wk, List<PrintDetail> detail, int imgsizekbn)
            {
                WkImgList = Wk;
                Details = detail;
                ImgSizeKbn = imgsizekbn;
            }
        }

    }
}
