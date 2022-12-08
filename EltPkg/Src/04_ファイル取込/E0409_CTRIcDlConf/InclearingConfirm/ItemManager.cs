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

namespace InclearingConfirm
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        public List<TBL_BANKMF> _bankMF = null;
        public List<TBL_CHANGE_DSPIDMF> _chgdspidMF = null;
        public List<TBL_ITEM_MASTER> _itemMF = null;
        public List<TBL_HOSEIMODE_DSP_ITEM> _hoseidispitemMF = null;
        public List<TBL_DSP_ITEM> _dspitemMF = null;

        private const string APPID = "CTRIcDlConf";

        public List<ConfirmData> _ConfirmData = null;
        public long _NotImportCnt = -1;
        private List<TBL_IC_OCR_FINISH> _ICOcrFinish = null;

        /// <summary>
        /// 上書きタイプ
        /// </summary>
        public enum OverRideType
        {
            /// <summary>上書きなし</summary>
            None = 0,
            /// <summary>削除追加</summary>
            DeleteInsert = 1,
            /// <summary>削除解除</summary>
            UnDelete = 2,
        }

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
            FetchCHGDSPIDMF();
            FetchItemMF();
            Fetch_hoseidsp_items();
            Fetch_dsp_items();
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
        /// DSPID変換マスタ(画面番号)取得
        /// </summary>
        public void FetchCHGDSPIDMF()
        {
            // SELECT実行
            string strSQL = string.Empty;
            _chgdspidMF = new List<TBL_CHANGE_DSPIDMF>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // DSP_PARAM取得
                    List<TBL_DSP_PARAM> DspParams = new List<TBL_DSP_PARAM>(); 
                    strSQL = TBL_DSP_PARAM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            DspParams.Add(new TBL_DSP_PARAM(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                        }
                    }

                    // 変換マスタ取得
                    strSQL = TBL_CHANGE_DSPIDMF.GetSelectQuery();
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            // 変換マスタに登録されているDSP_IDがDSP_PARAMに定義されている場合のみ追加
                            TBL_CHANGE_DSPIDMF ChgDspID = new TBL_CHANGE_DSPIDMF(tbl.Rows[i]);
                            if (DspParams.Where(x => x._DSP_ID == ChgDspID.m_DSP_ID).Count() > 0)
                            {
                                _chgdspidMF.Add(new TBL_CHANGE_DSPIDMF(tbl.Rows[i]));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// 項目マスタ取得
        /// </summary>
        private void FetchItemMF()
        {
            string strSQL = TBL_ITEM_MASTER.GetSelectQuery(AppInfo.Setting.SchemaBankCD);
            _itemMF = new List<TBL_ITEM_MASTER>();
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _itemMF.Add(new TBL_ITEM_MASTER(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// 補正モード画面項目定義マスタ取得
        /// </summary>
        private void Fetch_hoseidsp_items()
        {
            string strSQL = TBL_HOSEIMODE_DSP_ITEM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);
            _hoseidispitemMF = new List<TBL_HOSEIMODE_DSP_ITEM>();
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _hoseidispitemMF.Add(new TBL_HOSEIMODE_DSP_ITEM(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
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
        /// 項目定義マスタ取得
        /// </summary>
        private void Fetch_dsp_items()
        {
            string strSQL = TBL_DSP_ITEM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);
            _dspitemMF = new List<TBL_DSP_ITEM>();
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _dspitemMF.Add(new TBL_DSP_ITEM(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
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
        /// ダウンロード処理対象データ取得
        /// </summary>
        public void GetConfirmData(FormBase form = null)
        {
            //初期化
            string strSQL = string.Empty;
            _ConfirmData = new List<ConfirmData>();
            _ICOcrFinish = new List<TBL_IC_OCR_FINISH>();

            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    //前営業日取得
                    iBicsCalendar cal = new iBicsCalendar();
                    cal.SetHolidays();
                    int BeforeDate = cal.getBusinessday(AplInfo.OpDate(), -1);

                    // 持帰OCR完了明細データ取得
                    strSQL = TBL_IC_OCR_FINISH.GetSelectQueryOpeDateMoreThen(BeforeDate);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        foreach (DataRow dr in tbl.Rows)
                        {
                            _ICOcrFinish.Add(new TBL_IC_OCR_FINISH(dr));
                        }
                    }

                    // ダウンロード処理対象データ取得
                    strSQL = SQLTextImport.GetInclearingConfirmData(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        foreach (DataRow dr in tbl.Rows)
                        {
                            Dictionary<string, string> LineData = new Dictionary<string, string>();
                            CreateConfirmDataList(dr, ref LineData);
                            ////持帰OCR完了明細チェック
                            //bool OcrFinish = true;
                            //if (ChkSkipConfirmData(LineData))
                            //{
                            //    // 登録済データの場合は常にOCR完了扱い
                            //    OcrFinish = true;
                            //}
                            //else if (LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == "1" || LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == "2")
                            //{
                            //    //表裏の場合完了明細チェック
                            //    OcrFinish = ChkICOcrFinish(LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]);
                            //}
                            _ConfirmData.Add(new ConfirmData(LineData, false));
                        }
                    }

                    foreach(var Data in _ConfirmData)
                    {
                        //持帰OCR完了明細チェック
                        bool OcrFinish = true;
                        if (ChkSkipConfirmData(Data.LineData, _ConfirmData))
                        {
                            // 登録済データの場合は常にOCR完了扱い
                            OcrFinish = true;
                        }
                        else if (Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == "1" || Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == "2")
                        {
                            //表裏の場合完了明細チェック
                            OcrFinish = ChkICOcrFinish(Data.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]);
                        }
                        Data.ICOcrFinish = OcrFinish;
                    }

                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                }
            }
        }

        /// <summary>
        /// 未取込データ件数取得
        /// </summary>
        public void GetNotImportCnt(FormBase form = null)
        {
            // SELECT実行
            string strSQL = SQLTextImport.GetInclearingNotImport(AppInfo.Setting.SchemaBankCD);
            _NotImportCnt = -1;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0) return;
                    _NotImportCnt = DBConvert.ToLongNull(tbl.Rows[0]["CNT"]);
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                }
            }
        }

        /// <summary>
        /// 持帰ダウンロードロックの取得
        /// </summary>
        /// <returns></returns>
        public bool GetLockProcessBank(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // 行ロック取得処理
                string strSQL = TBL_LOCK_BANK_PROCESS.GetSelectQuery(AppInfo.Setting.GymId, APPID, AppInfo.Setting.SchemaBankCD, true);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                if (tbl.Rows.Count == 0)
                {
                    DBManager.InitializeSql1(NCR.Server.DBDataSource, NCR.Server.DBUserID, NCR.Server.DBPassword);
                    DBManager.dbs1.Open();

                    // 無ければ登録してもう一度実施
                    using (AdoDatabaseProvider dbs = GenDbProviderFactory.CreateAdoProvider2())
                    using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbs))
                    {
                        // 無ければ登録してもう一度実施
                        strSQL = TBL_LOCK_BANK_PROCESS.GetInsertQuery(AppInfo.Setting.GymId, APPID, AppInfo.Setting.SchemaBankCD);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                    }
                    // 再ロック
                    strSQL = TBL_LOCK_BANK_PROCESS.GetSelectQuery(AppInfo.Setting.GymId, APPID, AppInfo.Setting.SchemaBankCD, true);
                    tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
            finally
            {
                if (DBManager.dbs1 != null)
                {
                    DBManager.dbs1.Close();
                    DBManager.dbs1 = null;
                }
            }
        }

        /// <summary>
        /// ファイル名から明細データ単位の登録チェック
        /// </summary>
        /// <returns></returns>
        public bool ChkItemDataFileName(string filename, out int DetailInput, out TBL_TRMEI TRMei, AdoDatabaseProvider dbp, FormBase form = null)
        {
            // 初期化
            DetailInput = 0;
            TRMei = new TBL_TRMEI(AppInfo.Setting.SchemaBankCD);
            try
            {
                // データ取得
                string strSQL = SQLTextImport.GetInclearingDetailImgFile(AppInfo.Setting.GymId, 1, filename, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                if (tbl.Rows.Count > 0)
                {
                    TRMei = new TBL_TRMEI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    // 削除状態によらず登録済設定
                    DetailInput = 1;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
        }

        /// <summary>
        /// 登録済の明細番号最大値取得
        /// </summary>
        /// <returns></returns>
        public bool GetItemDetailNo(string filename, out int ImgDetailNo, AdoDatabaseProvider dbp, FormBase form = null)
        {
            // 初期化
            ImgDetailNo = 0;
            try
            {
                // データ取得
                string strSQL = SQLTextImport.GetInclearingMaxDetailNo(AppInfo.Setting.GymId, AplInfo.OpDate(), 1, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                if (tbl.Rows.Count > 0) ImgDetailNo = DBConvert.ToIntNull(tbl.Rows[0][TBL_TRMEI.DETAILS_NO]);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
        }

        /// <summary>
        /// 対象の明細データ一式を削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteTRMEIAllData(TBL_TRMEI TRMei, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // TRITEM・HOSEI_STATUSデータ削除
                if (!DeleteTRITEMData(TRMei._OPERATION_DATE, TRMei._BAT_ID, TRMei._DETAILS_NO, dbp, Tran, form))
                {
                    return false;
                }

                // TRMEIIMGデータ削除
                string strSQL = TBL_TRMEIIMG.GetDeleteQueryBatIDDetailNo(AppInfo.Setting.GymId, TRMei._OPERATION_DATE, TRMei._BAT_ID, TRMei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                // TRFUWATARIデータ削除
                TBL_TRFUWATARI fuwa = new TBL_TRFUWATARI(AppInfo.Setting.GymId, TRMei._OPERATION_DATE, TRMei._SCAN_TERM, TRMei._BAT_ID, TRMei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                strSQL = fuwa.GetDeleteQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                // TRMEIデータ削除
                strSQL = TRMei.GetDeleteQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
        }

        /// <summary>
        /// 対象の明細データ一式を削除解除
        /// </summary>
        /// <returns></returns>
        public bool UnDeleteTRMEIAllData(TBL_TRMEI TRMei, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                UpdateData.Add(TBL_TRMEI.DELETE_DATE, 0);
                UpdateData.Add(TBL_TRMEI.DELETE_FLG, 0);

                // TRMEIIMGデータ削除解除
                string strSQL = SQLTextImport.GetUpdateDetailTRMEIIMG(AppInfo.Setting.GymId, TRMei._OPERATION_DATE, TRMei._SCAN_TERM, TRMei._BAT_ID, TRMei._DETAILS_NO, UpdateData, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                // TRMEIデータ削除解除
                strSQL = SQLTextImport.GetUpdateTRMEI(AppInfo.Setting.GymId, TRMei._OPERATION_DATE, TRMei._SCAN_TERM, TRMei._BAT_ID, TRMei._DETAILS_NO, UpdateData, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
        }

        /// <summary>
        /// 対象の項目データを削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteTRITEMData(int OpeDate, int Batid, int ImgDetailNo, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // TRITEMデータ削除
                string strSQL = TBL_TRITEM.GetDeleteQueryBatIDDetailNo(AppInfo.Setting.GymId, OpeDate, Batid, ImgDetailNo, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                // HOSEI_STATUSデータ削除
                strSQL = TBL_HOSEI_STATUS.GetDeleteQueryBatIDDetailNo(AppInfo.Setting.GymId, OpeDate, Batid, ImgDetailNo, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
        }

        /// <summary>
        /// 対象のイメージデータを削除
        /// </summary>
        /// <returns></returns>
        public bool DeleteTRMEIIMGData(int OpeDate, int Batid, int ImgDetailNo, string imgkbn, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                // TRMEIIMGデータ削除
                string strSQL = TBL_TRMEIIMG.GetDeleteQueryBatIDDetailNoKbn(AppInfo.Setting.GymId, OpeDate, Batid, ImgDetailNo, int.Parse(imgkbn), AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
        }

        /// <summary>
        /// 持出のOCR情報を取得
        /// </summary>
        /// <returns></returns>
        public bool GetOutclearingOCRInfo(string filename, out int InputRoute, out string FolderName, out string oldFileName, out int DspID, AdoDatabaseProvider dbp, FormBase form = null)
        {
            // 初期化
            InputRoute = 0;
            FolderName = string.Empty;
            oldFileName = string.Empty;
            DspID = 0;

            try
            {
                // データ取得
                string strSQL = SQLTextImport.GetOutclearingOCRInfo(GymParam.GymId.持出, filename, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                if (tbl.Rows.Count > 0)
                {
                    InputRoute = DBConvert.ToIntNull(tbl.Rows[0][TBL_TRBATCH.INPUT_ROUTE]);
                    FolderName = DBConvert.ToStringNull(tbl.Rows[0][TBL_TRBATCHIMG.SCAN_BATCH_FOLDER_NAME]);
                    oldFileName = DBConvert.ToStringNull(tbl.Rows[0][TBL_TRMEIIMG.IMG_FLNM_OLD]);
                    DspID = DBConvert.ToIntNull(tbl.Rows[0][TBL_TRMEI.DSP_ID]);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
        }

        /// <summary>
        /// 証券OCRデータ取得(持出)
        /// </summary>
        public List<TBL_OCR_DATA> GetMeiOCRData(int inputroute, string batchfolderName, string file_name, AdoDatabaseProvider dbp)
        {
            List<TBL_OCR_DATA> ocrlist = new List<TBL_OCR_DATA>();

            try
            {
                int Route = 0;
                switch (inputroute)
                {
                    case 1:
                        Route = (int)TBL_OCR_DATA.OCRInputRoute.Normal;
                        break;
                    case 2:
                        Route = (int)TBL_OCR_DATA.OCRInputRoute.Futai;
                        break;
                    case 3:
                        Route = (int)TBL_OCR_DATA.OCRInputRoute.Kijitu;
                        break;
                    default:
                        Route = (int)TBL_OCR_DATA.OCRInputRoute.Other;
                        break;
                }

                // 対象データの取得
                string strSQL = TBL_OCR_DATA.GetSelectQueryDirImgName(GymParam.GymId.持出, Route, batchfolderName, file_name);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    ocrlist.Add(new TBL_OCR_DATA(tbl.Rows[i]));
                }
                return ocrlist;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return new List<TBL_OCR_DATA>();
            }
        }

        /// <summary>
        /// 証券OCRデータ取得(持帰)
        /// ファイル名のみ
        /// </summary>
        public List<TBL_OCR_DATA> GetMeiOCRDataFileName(string file_name, AdoDatabaseProvider dbp)
        {
            List<TBL_OCR_DATA> ocrlist = new List<TBL_OCR_DATA>();

            try
            {
                // 対象データの取得
                string strSQL = TBL_OCR_DATA.GetSelectQueryImgName(GymParam.GymId.持帰, (int)TBL_OCR_DATA.OCRInputRoute.Other, file_name);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    ocrlist.Add(new TBL_OCR_DATA(tbl.Rows[i]));
                }
                return ocrlist;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return new List<TBL_OCR_DATA>();
            }
        }

        /// <summary>
        /// 持帰要求結果証券明細テキストの確定フラグ更新
        /// 持帰要求結果証券管理のステータス更新
        /// </summary>
        /// <returns></returns>
        public bool UpdateReqRetCtlSts(string retreqtxt, string meitxt, string capkbn, List<string> imglist, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            try
            {
                string strSQL;
                
                // 確定フラグ更新
                strSQL = SQLTextImport.GetUpdateBillMeiTxtKakuteiFlg(AplInfo.OpDate(), NCR.Operator.UserID, meitxt, capkbn, imglist, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                // 未確定件数取得
                strSQL = SQLTextImport.GetBillMeiTxtNotKautei(meitxt, capkbn, AppInfo.Setting.SchemaBankCD);
                if (DBConvert.ToIntNull(dbp.SelectTable(strSQL, new List<IDbDataParameter>()).Rows[0]["CNT"]) == 0)
                {
                    // 未確定がない場合、ステータス更新
                    strSQL = SQLTextImport.GetUpdateReqRetCtlSTS(retreqtxt, meitxt, capkbn, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
        }

        /// <summary>
        /// 表面イメージから対象証券の通知テキスト一覧を取得
        /// </summary>
        public bool GetInclearingTsuchiTxt(string filename, out List<TsuchiTxtData> txtDatas, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 初期化
            txtDatas = new List<TsuchiTxtData>();
            try
            {
                // 対象データの取得
                string strSQL = SQLTextImport.GetInclearingTsuchiTxt(filename, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    txtDatas.Add(new TsuchiTxtData(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        /// <summary>
        /// 表面イメージから持出の証券データを取得
        /// </summary>
        public TBL_TRMEI GetOutclearingMeiData(string filename, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            TBL_TRMEI trmei = new TBL_TRMEI(AppInfo.Setting.SchemaBankCD);
            trmei.m_DELETE_FLG = 0;
            try
            {
                // 対象データの取得
                string strSQL = SQLTextImport.GetOutclearingMeiData(GymParam.GymId.持出, filename, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                if (tbl.Rows.Count > 0)
                {
                    trmei = new TBL_TRMEI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                }
                return trmei;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return trmei;
            }
        }

        /// <summary>
        /// 対象データが登録対象外かどうか
        /// </summary>
        /// <param name="LineData"></param>
        public bool ChkSkipConfirmData(Dictionary<string, string> LineData, IEnumerable<ConfirmData> AllData)
        {
            switch (LineData["MEIFLG"])
            {
                case "0":
                    // 明細登録済

                    if (LineData["IMGFLG"] == "0")
                    {
                        // イメージも登録済の場合は登録対象外
                        return true;
                    }
                    else
                    {
                        // イメージ未登録の場合は登録対象
                        return false;
                    }
                case "1":
                    // 明細削除

                    switch (ChkOverRideData(LineData, AllData))
                    {
                        case OverRideType.DeleteInsert:
                        case OverRideType.UnDelete:
                            // 上書き対象・削除解除はすべて登録対象
                            return false;
                        default:
                            // 上書き対象外の場合（不渡での削除）
                            if (LineData["IMGFLG"] == "0")
                            {
                                // イメージ登録済の場合は登録対象外
                                return true;
                            }
                            else
                            {
                                // イメージ未登録の場合は登録対象
                                return false;
                            }
                    }
                default:
                    // 明細未登録
                    return false;
            }
        }

        /// <summary>
        /// 対象データの明細が上書き対象かどうか
        /// </summary>
        /// <param name="LineData"></param>
        public OverRideType ChkOverRideData(Dictionary<string, string> LineData, IEnumerable<ConfirmData> AllData)
        {
            switch (LineData["MEIFLG"])
            {
                case "0":
                    // 明細登録済の場合、上書き対象外
                    return OverRideType.None;
                case "1":
                    // 明細削除

                    if (DBConvert.ToIntNull(LineData["BCA_DATE"]) != 0)
                    {
                        // 持出取消での削除明細は上書き対象
                        return OverRideType.DeleteInsert;
                    }
                    else if (LineData["BKNO"] != AppInfo.Setting.SchemaBankCD.ToString(Const.BANK_NO_LEN_STR) && DBConvert.ToIntNull(LineData["GMA_STS"]) == TrMei.Sts.結果正常)
                    {
                        // 持帰訂正での削除明細は上書き対象(持帰銀行訂正)
                        return OverRideType.DeleteInsert;
                    }
                    else if (ChkDeleteCDate(LineData, AllData))
                    {
                        // 持帰訂正での削除明細は削除解除対象(交換日訂正)
                        return OverRideType.UnDelete;
                    }
                    else
                    {
                        // 上記以外の削除の場合、上書き対象外
                        return OverRideType.None;
                    }
                default:
                    // 明細未登録の場合、上書き対象外
                    return OverRideType.None;
            }
        }

        /// <summary>
        /// 対象データが交換日訂正での削除明細かどうか
        /// </summary>
        /// <param name="LineData"></param>
        private bool ChkDeleteCDate(Dictionary<string, string> BaseLineData, IEnumerable<ConfirmData> AllData)
        {
            // 表証券イメージファイル名が同じで区分が表のデータを取得
            IEnumerable<ConfirmData> FrontData = AllData.Where(x => x.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME] == BaseLineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME] &&
                                                                    x.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == "1");
            if (FrontData.LongCount() == 0)
            {
                // 対象データがない場合
                return false;
            }
            Dictionary<string, string> ChkLineData = FrontData.First().LineData;

            switch (ChkLineData["MEIFLG"])
            {
                case "1":
                    // 明細削除

                    if (DBConvert.ToIntNull(ChkLineData[TBL_ICREQRET_BILLMEITXT.IC_FLG]) != 0)
                    {
                        // 持帰状況フラグが0以外
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(ChkLineData[TBL_ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE]) ||
                        ChkLineData[TBL_ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE] == "ZZZZZZZZ")
                    {
                        // 証券データ訂正交換希望日が省略値
                        return false;
                    }

                    if (ChkLineData[TBL_ICREQRET_BILLMEITXT.OC_CLEARING_DATE] == ChkLineData[TBL_ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE])
                    {
                        // 持出時交換希望日と証券データ訂正交換希望日が同じ値
                        return false;
                    }

                    if (DBConvert.ToIntNull(ChkLineData["IMGFLG"]) == -1)
                    {
                        // 持出時交換希望日と証券データ訂正交換希望日が同じ値
                        return false;
                    }

                    // 上記以外は交換日訂正での削除明細
                    return true;
                case "0":
                default:
                    // 明細登録済・明細未登録の場合
                    return false;
            }
        }

        /// <summary>
        /// 持帰OCR完了明細チェック
        /// </summary>
        public bool ChkICOcrFinish(string FrontImgName)
        {
            return (_ICOcrFinish.Count(x => x._FRONT_IMG_NAME == FrontImgName) > 0);
        }

        /// <summary>
        /// 持帰ダウンロード確定前イメージルートフォルダパスを取得
        /// </summary>
        public string BankCheckImageRoot()
        {
            return string.Format(NCR.Server.BankCheckImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 持帰ダウンロード確定イメージルートフォルダパスを取得
        /// </summary>
        public string BankConfirmImageRoot()
        {
            return string.Format(NCR.Server.BankConfirmImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 処理対象データ作成
        /// </summary>
        private void CreateConfirmDataList(DataRow dr, ref Dictionary<string, string> LineData)
        {
            // ICREQRET_BILLMEITXT
            TBL_ICREQRET_BILLMEITXT meitxt = new TBL_ICREQRET_BILLMEITXT(dr, AppInfo.Setting.SchemaBankCD);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME, meitxt._MEI_TXT_NAME);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.CAP_KBN, meitxt._CAP_KBN.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.IMG_NAME, meitxt._IMG_NAME);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME, meitxt.m_IMG_ARCH_NAME);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME, meitxt.m_FRONT_IMG_NAME);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.IMG_KBN, meitxt.m_IMG_KBN.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FILE_OC_BK_NO, meitxt.m_FILE_OC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.CHG_OC_BK_NO, meitxt.m_CHG_OC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OC_BR_NO, meitxt.m_OC_BR_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OC_DATE, meitxt.m_OC_DATE.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OC_METHOD, meitxt.m_OC_METHOD);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OC_USERID, meitxt.m_OC_USERID);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.PAY_KBN, meitxt.m_PAY_KBN);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.BALANCE_FLG, meitxt.m_BALANCE_FLG);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OCR_IC_BK_NO, meitxt.m_OCR_IC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.QR_IC_BK_NO, meitxt.m_QR_IC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.MICR_IC_BK_NO, meitxt.m_MICR_IC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FILE_IC_BK_NO, meitxt.m_FILE_IC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.CHG_IC_BK_NO, meitxt.m_CHG_IC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.TEISEI_IC_BK_NO, meitxt.m_TEISEI_IC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.PAY_IC_BK_NO, meitxt.m_PAY_IC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.PAYAFT_REV_IC_BK_NO, meitxt.m_PAYAFT_REV_IC_BK_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OCR_IC_BK_NO_CONF, meitxt.m_OCR_IC_BK_NO_CONF);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OCR_AMOUNT, meitxt.m_OCR_AMOUNT);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.MICR_AMOUNT, meitxt.m_MICR_AMOUNT);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.QR_AMOUNT, meitxt.m_QR_AMOUNT);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FILE_AMOUNT, meitxt.m_FILE_AMOUNT);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.TEISEI_AMOUNT, meitxt.m_TEISEI_AMOUNT);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.PAY_AMOUNT, meitxt.m_PAY_AMOUNT);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.PAYAFT_REV_AMOUNT, meitxt.m_PAYAFT_REV_AMOUNT);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OCR_AMOUNT_CONF, meitxt.m_OCR_AMOUNT_CONF);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.OC_CLEARING_DATE, meitxt.m_OC_CLEARING_DATE);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE, meitxt.m_TEISEI_CLEARING_DATE);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.CLEARING_DATE, meitxt.m_CLEARING_DATE);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.QR_IC_BR_NO, meitxt.m_QR_IC_BR_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.KAMOKU, meitxt.m_KAMOKU);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.ACCOUNT, meitxt.m_ACCOUNT);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.BK_CTL_NO, meitxt.m_BK_CTL_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FREEFIELD, meitxt.m_FREEFIELD);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.BILL_CODE, meitxt.m_BILL_CODE);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.BILL_CODE_CONF, meitxt.m_BILL_CODE_CONF);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.QR, meitxt.m_QR);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.MICR, meitxt.m_MICR);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.MICR_CONF, meitxt.m_MICR_CONF);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.BILL_NO, meitxt.m_BILL_NO);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.BILL_NO_CONF, meitxt.m_BILL_NO_CONF);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FUBI_KBN_01, meitxt.m_FUBI_KBN_01);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.ZERO_FUBINO_01, meitxt.m_ZERO_FUBINO_01.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FUBI_KBN_02, meitxt.m_FUBI_KBN_02);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_02, meitxt.m_ZRO_FUBINO_02.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FUBI_KBN_03, meitxt.m_FUBI_KBN_03);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_03, meitxt.m_ZRO_FUBINO_03.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FUBI_KBN_04, meitxt.m_FUBI_KBN_04);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_04, meitxt.m_ZRO_FUBINO_04.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.FUBI_KBN_05, meitxt.m_FUBI_KBN_05);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_05, meitxt.m_ZRO_FUBINO_05.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.IC_FLG, meitxt.m_IC_FLG);
            LineData.Add(TBL_ICREQRET_BILLMEITXT.KAKUTEI_FLG, meitxt.m_KAKUTEI_FLG.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.KAKUTEI_DATE, meitxt.m_KAKUTEI_DATE.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.KAKUTEI_TIME, meitxt.m_KAKUTEI_TIME.ToString());
            LineData.Add(TBL_ICREQRET_BILLMEITXT.KAKUTEI_OPE, meitxt.m_KAKUTEI_OPE);

            // TBL_ICREQRET_CTL
            LineData.Add(TBL_ICREQRET_CTL.RET_REQ_TXT_NAME, DBConvert.ToStringNull(dr[TBL_ICREQRET_CTL.RET_REQ_TXT_NAME]));
            LineData.Add(TBL_ICREQRET_CTL.CAP_STS, DBConvert.ToIntNull(dr[TBL_ICREQRET_CTL.CAP_STS]).ToString());
            LineData.Add(TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS, DBConvert.ToIntNull(dr[TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS]).ToString());

            // その他
            LineData.Add("CTLFLG", DBConvert.ToIntNull(dr["CTLFLG"]).ToString());
            LineData.Add("IMGFLG", DBConvert.ToIntNull(dr["IMGFLG"]).ToString());
            LineData.Add("MEIFLG", DBConvert.ToIntNull(dr["MEIFLG"]).ToString());
            LineData.Add("BCA_DATE", DBConvert.ToIntNull(dr["BCA_DATE"]).ToString());
            LineData.Add("GMA_STS", DBConvert.ToIntNull(dr["GMA_STS"]).ToString());
            LineData.Add("BKNO", DBConvert.ToStringNull(dr["BKNO"]).ToString());
        }

        /// <summary>
        /// ダウンロード処理対象データ
        /// </summary>
        public class ConfirmData
        {
            //対象データ
            public Dictionary<string, string> LineData { get; set; }
            //明細イメージチェック結果
            public bool ImgExistChk { get; set; }
            //持帰OCR完了明細チェック結果
            public bool ICOcrFinish { get; set; }

            public ConfirmData(Dictionary<string, string> data, bool ocrfin)
            {
                LineData = data;
                ICOcrFinish = ocrfin;
                ImgExistChk = false;
            }
        }

        /// <summary>
        /// 通知テキストデータ
        /// </summary>
        public class TsuchiTxtData
        {
            public TBL_TSUCHITXT TsuchiTxt { get; set; }
            public string FILE_ID { get; set; }
            public string FILE_DIVID { get; set; }
            public int MAKE_DATE { get; set; }
            public int RECV_DATE { get; set; }
            public int RECV_TIME { get; set; }

            public TsuchiTxtData(DataRow dr, int Schemabankcd)
            {
                TsuchiTxt = new TBL_TSUCHITXT(dr, Schemabankcd);
                FILE_ID = DBConvert.ToStringNull(dr[TBL_TSUCHITXT_CTL.FILE_ID]);
                FILE_DIVID = DBConvert.ToStringNull(dr[TBL_TSUCHITXT_CTL.FILE_DIVID]);
                MAKE_DATE = DBConvert.ToIntNull(dr[TBL_TSUCHITXT_CTL.MAKE_DATE]);
                RECV_DATE = DBConvert.ToIntNull(dr[TBL_TSUCHITXT_CTL.RECV_DATE]);
                RECV_TIME = DBConvert.ToIntNull(dr[TBL_TSUCHITXT_CTL.RECV_TIME]);
            }
        }

        /// <summary>
        /// ConfirmData比較クラス
        /// 証券明細テキストファイル名,取込区分,証券イメージファイル名
        /// </summary>
        public class ConfirmDataKeyComparer : IEqualityComparer<ConfirmData>
        {
            public bool Equals(ConfirmData info1, ConfirmData info2)
            {
                // 証券明細テキストファイル名,取込区分,証券イメージファイル名が同じ場合、Equal
                return (info1.LineData[TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME] == info2.LineData[TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME]) &&
                       (info1.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN] == info2.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN]) &&
                       (info1.LineData[TBL_ICREQRET_BILLMEITXT.IMG_NAME] == info2.LineData[TBL_ICREQRET_BILLMEITXT.IMG_NAME]);
            }

            public int GetHashCode(ConfirmData info)
            {
                return (info.LineData[TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME], info.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN], info.LineData[TBL_ICREQRET_BILLMEITXT.IMG_NAME]).GetHashCode();
            }
        }

        /// <summary>
        /// ConfirmData比較クラス
        /// 表証券イメージファイル名,区分
        /// </summary>
        public class ConfirmDataImgKbnComparer : IEqualityComparer<ConfirmData>
        {
            public bool Equals(ConfirmData info1, ConfirmData info2)
            {
                // 表証券イメージファイル名,区分が同じ場合、Equal
                return (info1.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME] == info2.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]) && 
                       (info1.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == info2.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN]);
            }

            public int GetHashCode(ConfirmData info)
            {
                return (info.LineData[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME], info.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN]).GetHashCode();
            }
        }

        /// <summary>
        /// 取込結果データ
        /// </summary>
        public class ImportResult
        {
            //明細取込成功数
            public int DetailImportSuccess { get; set; } = 0;
            //明細取込失敗数
            public int DetailImportFail { get; set; } = 0;
            //イメージ取込成功数
            public int DetailImgImportSuccess { get; set; } = 0;
            //削除解除成功数
            public int UnDeleteDetailSuccess { get; set; } = 0;
        }

    }
}
