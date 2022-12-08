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

namespace SearchProc
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        //2022.11.08 SP.harauchi No.152 表示速度改善対応
        /// <summary>イメージ取込データ</summary>
        //public List<ImgImport> imgImport { get; set; } = new List<ImgImport>();
        public class imgImp
        {
            public static int UnPro = 0;
            public static int Today_Batch = 0;
            public static int Today_Mei = 0;
            public static int Nextday_Batch = 0;
            public static int Nextday_Mei = 0;
            public static int All_Batch = 0;
            public static int All_Mei = 0;
        }
        public imgImp ImageImport;
        /// <summary>持出補正データ</summary>
        public List<OCInput> OCInputData { get; set; } = new List<OCInput>();
        /// <summary>持出UPLOADデータ</summary>
        public List<OCUpload> OCUploadData { get; set; } = new List<OCUpload>();
        /// <summary>持出取消UPLOADデータ</summary>
        public List<OCDeleteUpload> OCDeleteUploadData { get; set; } = new List<OCDeleteUpload>();
        /// <summary>持帰ダウンロード</summary>
        public List<ICDownLoad> ICDownLoadData { get; set; } = new List<ICDownLoad>();
        /// <summary>持帰補正データ</summary>
        public List<ICInput> ICInputData { get; set; } = new List<ICInput>();
        /// <summary>持帰訂正データ</summary>
        public List<ICTeisei> ICTeiseiData { get; set; } = new List<ICTeisei>();
        /// <summary>持帰訂正データ(処理待)</summary>
        public List<ICTeisei> ICWaitTeiseiData { get; set; } = new List<ICTeisei>();
        /// <summary>持帰削除データ</summary>
        public List<ICDelete> ICDeleteData { get; set; } = new List<ICDelete>();
        /// <summary>持帰不渡データ</summary>
        public List<ICFuwatari> ICFuwatariData { get; set; } = new List<ICFuwatari>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
        }

        /// <summary>
        /// イメージ取込情報取得
        /// </summary>
        public bool FetchImgImport(FormBase form = null)
        {
            ImageImport = new imgImp();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    //2022.11.08 SP.harauchi No.152 表示速度改善対応
                    string strSQL = string.Empty;

                    iBicsCalendar cal = new iBicsCalendar();
                    int AfterDate = cal.getBusinessday(AplInfo.OpDate(), 1);

                    // 未処理集計
                    strSQL = SQLSystemCommon.GetProcViewImgImportBatCtl(AplInfo.OpDate());
                    DataTable tbl_UnPro = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl_UnPro.Rows.Count > 0)
                    {
                        imgImp.UnPro = Convert.ToInt32(tbl_UnPro.Rows[0]["未処理件数"]);
                    }

                    // 処理済み（バッチ）集計
                    strSQL = SQLSystemCommon.GetProcViewImgImportBat(AppInfo.Setting.SchemaBankCD, AplInfo.OpDate(), AfterDate);
                    DataTable tbl_Batch = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl_Batch.Rows.Count > 0)
                    {
                        imgImp.Today_Batch = Convert.ToInt32(tbl_Batch.Rows[0]["Today_Batch"]);
                        imgImp.Nextday_Batch = Convert.ToInt32(tbl_Batch.Rows[0]["Nextday_Batch"]);
                        imgImp.All_Batch = Convert.ToInt32(tbl_Batch.Rows[0]["Otherday_Batch"]) + imgImp.Today_Batch + imgImp.Nextday_Batch;
                    }

                    // 処理済み（明細）集計
                    strSQL = SQLSystemCommon.GetProcViewImgImportMei(AppInfo.Setting.SchemaBankCD, AplInfo.OpDate(), AfterDate);
                    DataTable tbl_Mei = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl_Mei.Rows.Count > 0)
                    {
                        imgImp.Today_Mei = Convert.ToInt32(tbl_Mei.Rows[0]["Today_Mei"]);
                        imgImp.Nextday_Mei = Convert.ToInt32(tbl_Mei.Rows[0]["Nextday_Mei"]);
                        imgImp.All_Mei = Convert.ToInt32(tbl_Mei.Rows[0]["Otherday_Mei"]) + imgImp.Today_Mei + imgImp.Nextday_Mei;
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
        /// 持出補正情報取得
        /// </summary>
        public bool FetchOCInputData(FormBase form = null)
        {
            string strSQL = string.Empty;
            OCInputData = new List<OCInput>();
            OCUploadData = new List<OCUpload>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // SELECT実行(補正)
                    strSQL = SQLSystemCommon.GetProcViewOCInputData(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            OCInputData.Add(new OCInput(tbl.Rows[i]));
                        }
                    }
                    // SELECT実行(UPLOAD)
                    strSQL = SQLSystemCommon.GetProcViewOCUpLoadData(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            OCUploadData.Add(new OCUpload(tbl.Rows[i]));
                        }
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
        /// 持出取消情報取得
        /// </summary>
        public bool FetchOCDeleteData(FormBase form = null)
        {
            string strSQL = string.Empty;
            OCDeleteUploadData = new List<OCDeleteUpload>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // SELECT実行(Upload)
                    strSQL = SQLSystemCommon.GetProcViewOCDeleteUpLoadData(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            OCDeleteUploadData.Add(new OCDeleteUpload(tbl.Rows[i]));
                        }
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
        /// 持帰ダウンロード
        /// </summary>
        public bool FetchICDownLoadData(FormBase form = null)
        {
            string strSQL = string.Empty;
            ICDownLoadData = new List<ICDownLoad>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // SELECT実行
                    strSQL = SQLSystemCommon.GetProcViewICDownLoadData(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            ICDownLoadData.Add(new ICDownLoad(tbl.Rows[i]));
                        }
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
        /// 持帰補正情報取得
        /// </summary>
        public bool FetchICInputData(FormBase form = null)
        {
            string strSQL = string.Empty;
            ICInputData = new List<ICInput>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // SELECT実行(補正)
                    strSQL = SQLSystemCommon.GetProcViewICInputData(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            ICInputData.Add(new ICInput(tbl.Rows[i]));
                        }
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
        /// 持帰訂正情報取得
        /// </summary>
        public bool FetchICTeiseiiData(FormBase form = null)
        {
            string strSQL = string.Empty;
            ICTeiseiData = new List<ICTeisei>();
            ICWaitTeiseiData = new List<ICTeisei>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // SELECT実行
                    strSQL = SQLSystemCommon.GetProcViewICTeiseiUpLoadData(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            ICTeiseiData.Add(new ICTeisei(tbl.Rows[i]));
                        }
                    }
                    // SELECT実行
                    strSQL = SQLSystemCommon.GetProcViewICTeiseiUpLoadDataWait(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            ICWaitTeiseiData.Add(new ICTeisei(tbl.Rows[i]));
                        }
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
        /// 持帰削除情報取得
        /// </summary>
        public bool FetchICDeleteData(FormBase form = null)
        {
            string strSQL = string.Empty;
            ICDeleteData = new List<ICDelete>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // SELECT実行
                    strSQL = SQLSystemCommon.GetProcViewICDeleteData(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            ICDeleteData.Add(new ICDelete(tbl.Rows[i]));
                        }
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
        /// 持帰不渡情報取得
        /// </summary>
        public bool FetchICFuwatariData(FormBase form = null)
        {
            string strSQL = string.Empty;
            ICFuwatariData = new List<ICFuwatari>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // SELECT実行
                    strSQL = SQLSystemCommon.GetProcViewICFuwatariData(AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            ICFuwatariData.Add(new ICFuwatari(tbl.Rows[i]));
                        }
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
        /// イメージ取込データ
        /// </summary>
        public class ImgImport
        {
            public TBL_SCAN_BATCH_CTL cTL { get; set; } = null;
            public ImgImportData BatData { get; set; } = null;
            public List<ImgImportData> MeiData { get; set; } = null;

            public ImgImport(TBL_SCAN_BATCH_CTL ctl, ImgImportData batdata, List<ImgImportData> meidata)
            {
                cTL = ctl;
                BatData = batdata;
                MeiData = meidata;
            }
        }

        /// <summary>
        /// イメージ取込データ
        /// </summary>
        public class ImgImportData
        {
            public int ClearingDate { get; set; } = 0;
            public int Count { get; set; } = 0;

            public ImgImportData()
            {
                ClearingDate = 0;
                Count = 0;
            }

            public ImgImportData(DataRow dr)
            {
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }

        /// <summary>
        /// 持出補正データ
        /// </summary>
        public class OCInput
        {
            public int GymID { get; set; } = 0;
            public int InputMode { get; set; } = 0;
            public int InputSts { get; set; } = 0;
            public int ClearingDate { get; set; } = 0;
            public int Count { get; set; } = 0;

            public OCInput(DataRow dr)
            {
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                InputMode = DBConvert.ToIntNull(dr["HOSEI_INPTMODE"]);
                InputSts = DBConvert.ToIntNull(dr["INPT_STS"]);
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }

        /// <summary>
        /// 持出UPLOADデータ
        /// </summary>
        public class OCUpload
        {
            public int GymID { get; set; } = 0;
            public int ClearingDate { get; set; } = 0;
            public int BUASts { get; set; } = 0;
            //public int TeiseiInpuSts { get; set; } = 0;
            public int Count { get; set; } = 0;

            public OCUpload(DataRow dr)
            {
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                BUASts = DBConvert.ToIntNull(dr["BUA_STS"]);
                //TeiseiInpuSts = DBConvert.ToIntNull(dr["TEISEIINPTSTS"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }

        /// <summary>
        /// 持出取消UPLOADデータ
        /// </summary>
        public class OCDeleteUpload
        {
            public int GymID { get; set; } = 0;
            public int BCASts { get; set; } = 0;
            public int ClearingDate { get; set; } = 0;
            public int Count { get; set; } = 0;

            public OCDeleteUpload(DataRow dr)
            {
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                BCASts = DBConvert.ToIntNull(dr["BCA_STS"]);
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }

        /// <summary>
        /// 持帰ダウンロード
        /// </summary>
        public class ICDownLoad
        {
            public int ClearingDate { get; set; } = 0;
            //public string ImgName { get; set; } = "";
            public int KakuteiFlg { get; set; } = 0;
            public int Count { get; set; } = 0;
            
            public ICDownLoad(DataRow dr)
            {
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                //ImgName = DBConvert.ToStringNull(dr["IMG_NAME"]);
                KakuteiFlg = DBConvert.ToIntNull(dr["KAKUTEI_FLG"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }

        /// <summary>
        /// 持帰補正データ
        /// </summary>
        public class ICInput
        {
            public int GymID { get; set; } = 0;
            public int InputMode { get; set; } = 0;
            public int InputSts { get; set; } = 0;
            public int ClearingDate { get; set; } = 0;
            public int Count { get; set; } = 0;

            public ICInput(DataRow dr)
            {
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                InputMode = DBConvert.ToIntNull(dr["HOSEI_INPTMODE"]);
                InputSts = DBConvert.ToIntNull(dr["INPT_STS"]);
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }

        /// <summary>
        /// 持帰訂正データ
        /// </summary>
        public class ICTeisei
        {
            public int GymID { get; set; } = 0;
            public int GMASts { get; set; } = 0;
            public int ClearingDate { get; set; } = 0;
            public int Count { get; set; } = 0;

            public ICTeisei(DataRow dr)
            {
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                GMASts = DBConvert.ToIntNull(dr["GMA_STS"]);
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }

        /// <summary>
        /// 持帰削除データ
        /// </summary>
        public class ICDelete
        {
            public int GymID { get; set; } = 0;
            public int ClearingDate { get; set; } = 0;
            public int Count { get; set; } = 0;

            public ICDelete(DataRow dr)
            {
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }

        /// <summary>
        /// 持帰不渡データ
        /// </summary>
        public class ICFuwatari
        {
            public int GymID { get; set; } = 0;
            public int GRASts { get; set; } = 0;
            public int ClearingDate { get; set; } = 0;
            public int FuwatariEYMD { get; set; } = 0;
            public int FuwatariDelDate { get; set; } = 0;
            public int FuwatariDelFlg { get; set; } = 0;
            public int Count { get; set; } = 0;

            public ICFuwatari(DataRow dr)
            {
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                GRASts = DBConvert.ToIntNull(dr["GRA_STS"]);
                ClearingDate = DBConvert.ToIntNull(dr["CLEARING_DATE"]);
                FuwatariEYMD = DBConvert.ToIntNull(dr["E_YMD"]);
                FuwatariDelDate = DBConvert.ToIntNull(dr["DELETE_DATE"]);
                FuwatariDelFlg = DBConvert.ToIntNull(dr["DELETE_FLG"]);
                Count = DBConvert.ToIntNull(dr["CNT"]);
            }
        }
    }
}
