using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;
using System.IO;

namespace SearchOcMeiView
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;
        private List<TBL_BANKMF> _bankMF = null;
        private List<TBL_BRANCHMF> _branchMF = null;
        private List<TBL_BRANCHMF> _scanbranchMF = null;
        private List<TBL_OPERATOR> _operatorMF = null;

        public Dictionary<string, DetailData> MeiDetails = null;
        public Dictionary<string, List<TBL_TRMEIIMG>> MeiImgList = null;
        public Dictionary<int, ImageInfo> ImageInfos { get; set; } = null;

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }
        /// <summary>明細パラメータ</summary>
        public DetailDispParams DetailParams { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            DispParams = new DisplayParams();
            DetailParams = new DetailDispParams();
            MeiDetails = new Dictionary<string, DetailData>();
            MeiImgList = new Dictionary<string, List<TBL_TRMEIIMG>>();
            ImageInfos = new Dictionary<int, ImageInfo>();
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        public override void FetchAllData()
        {
            FetchBankMF();
            FetchBranchMF();
            FetchScanBranchMF();
            FetchOperatorMF();
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
        /// スキャン支店情報の取得
        /// </summary>
        public void FetchScanBranchMF(FormBase form = null)
        {
            // SELECT実行
            _scanbranchMF = new List<TBL_BRANCHMF>();
            if (!int.TryParse(NCR.Server.ContractBankCd, out int intBank))
            {
                return;
            }
            string strSQL = TBL_BRANCHMF.GetSelectQuery(intBank);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        _scanbranchMF.Add(new TBL_BRANCHMF(tbl.Rows[i], intBank));
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
        /// 明細一覧取得
        /// </summary>
        public bool FetchMeiList(int ListDispLimit, out bool LimitOver, FormBase form = null)
        {
            // 初期化
            LimitOver = false;
            MeiDetails = new Dictionary<string, DetailData>();
            MeiImgList = new Dictionary<string, List<TBL_TRMEIIMG>>();

            // SELECT実行
            string strSQL = SQLSearch.GetOcMeiList(GymParam.GymId.持出, DispParams.Rdate, DispParams.ScanBrNo, DispParams.ScanDate, DispParams.OCBRNoFrom, DispParams.OCBRNoTo, DispParams.BatNo, DispParams.ICBKNo,
                                                   DispParams.ClearingDataFrom, DispParams.ClearingDataTo, DispParams.AmountFrom, DispParams.AmountTo, DispParams.Status,
                                                   DispParams.Delete, DispParams.ntOpeNumer, DispParams.Memo, DispParams.MemoOpt, DispParams.ImgFLNm, DispParams.ImgFLNmOpt,
                                                   DispParams.BUASts, DispParams.BCASts, DispParams.BUA, DispParams.GMB, DispParams.GRASts, DispParams.GXA, DispParams.Def, DispParams.EditFlg, 
                                                   AppInfo.Setting.SchemaBankCD, ListDispLimit);
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
                        DetailData Detail = new DetailData(tbl.Rows[i], i);
                        List<TBL_TRMEIIMG> ImgList = new List<TBL_TRMEIIMG>();
                        string key = CommonUtil.GenerateKey("|", Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo);
                        if (!FetchMeiImgList(ref Detail, ref ImgList, dbp, form)) return false;
                        MeiDetails.Add(key, Detail);
                        MeiImgList.Add(key, ImgList);
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
        /// 対象明細のイメージ一覧取得
        /// </summary>
        public bool FetchMeiImgList(ref DetailData Detail, ref List<TBL_TRMEIIMG> ImgList, AdoDatabaseProvider dbp, FormBase form = null)
        {
            // 初期化
            ImgList = new List<TBL_TRMEIIMG>();

            // SELECT実行
            string strSQL = TBL_TRMEIIMG.GetSelectQuery(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AppInfo.Setting.SchemaBankCD);
            try
            {
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    TBL_TRMEIIMG img = new TBL_TRMEIIMG(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                    if (Detail.MeiDelete == 0 && img.m_DELETE_FLG == 1)
                    {
                        // 明細が未削除でイメージが削除されている場合は対象外
                        continue;
                    }
                    ImgList.Add(img);
                }
                if (ImgList.Count() > 0)
                {
                    Detail.ImgUploadSts = ImgList.Min(x => x.m_BUA_STS);
                }
                else
                {
                    Detail.ImgUploadSts = -1;
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 対象明細のイメージ情報を設定
        /// </summary>
        public void Fetch_meiimges()
        {
            // イメージ情報
            ImageInfos = new Dictionary<int, ImageInfo>();
            ImageInfos.Add(TrMeiImg.ImgKbn.表, new ImageInfo(TrMeiImg.ImgKbn.表));
            ImageInfos.Add(TrMeiImg.ImgKbn.裏, new ImageInfo(TrMeiImg.ImgKbn.裏));
            ImageInfos.Add(TrMeiImg.ImgKbn.補箋, new ImageInfo(TrMeiImg.ImgKbn.補箋));
            ImageInfos.Add(TrMeiImg.ImgKbn.付箋, new ImageInfo(TrMeiImg.ImgKbn.付箋));
            ImageInfos.Add(TrMeiImg.ImgKbn.入金証明, new ImageInfo(TrMeiImg.ImgKbn.入金証明));
            ImageInfos.Add(TrMeiImg.ImgKbn.表再送分, new ImageInfo(TrMeiImg.ImgKbn.表再送分));
            ImageInfos.Add(TrMeiImg.ImgKbn.裏再送分, new ImageInfo(TrMeiImg.ImgKbn.裏再送分));
            ImageInfos.Add(TrMeiImg.ImgKbn.その他1, new ImageInfo(TrMeiImg.ImgKbn.その他1));
            ImageInfos.Add(TrMeiImg.ImgKbn.その他2, new ImageInfo(TrMeiImg.ImgKbn.その他2));
            ImageInfos.Add(TrMeiImg.ImgKbn.その他3, new ImageInfo(TrMeiImg.ImgKbn.その他3));
            ImageInfos.Add(TrMeiImg.ImgKbn.予備1, new ImageInfo(TrMeiImg.ImgKbn.予備1));
            ImageInfos.Add(TrMeiImg.ImgKbn.予備2, new ImageInfo(TrMeiImg.ImgKbn.予備2));
            ImageInfos.Add(TrMeiImg.ImgKbn.予備3, new ImageInfo(TrMeiImg.ImgKbn.予備3));

            if (!(MeiImgList.ContainsKey(DetailParams.Key) && MeiDetails.ContainsKey(DetailParams.Key)))
            {
                // 取得データない場合
                return;
            }

            //対象明細のイメージ一覧を設定
            int InputRoute = MeiDetails[DetailParams.Key].InputRoute;
            foreach(TBL_TRMEIIMG data in MeiImgList[DetailParams.Key])
            {
                if (ImageInfos.ContainsKey(data._IMG_KBN))
                {
                    ImageInfos[data._IMG_KBN].MeiImage = data;
                    ImageInfos[data._IMG_KBN].HasImage = true;
                    ImageInfos[data._IMG_KBN].ImgFolderPath = GetBankBacthFolder(data._GYM_ID, data._OPERATION_DATE, data._BAT_ID, InputRoute);
                }
            }
        }

        /// <summary>
        /// ワークテーブルへの登録処理
        /// </summary>
        public bool InsertWKImgList(string Key, DetailData Detail, FormBase form = null)
        {
            Dictionary<string, DetailData> Data = new Dictionary<string, DetailData>();
            Data.Add(Key, Detail);
            return InsertWKImgList(Data, form);
        }

        /// <summary>
        /// ワークテーブルへの登録処理
        /// </summary>
        public bool InsertWKImgList(Dictionary<string, DetailData> Details, FormBase form = null)
        {
            //端末IPアドレスの取得
            string TermIPAddress = ImportFileAccess.GetTermIPAddress().Replace(".", "_");

            // 削除・登録
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = "";
                    // 削除
                    strSQL = TBL_WK_IMGELIST.GetDeleteQuerySerchTermID(TermIPAddress);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // 登録
                    int Cnt = 1;
                    TBL_WK_IMGELIST insData = null;
                    foreach (DetailData Data in Details.Values.Where(x => x.MeiDelete == 0).OrderBy(x => x.SortNo))
                    {
                        insData = new TBL_WK_IMGELIST(TermIPAddress, Data.GymID, Data.OpeDate, Data.ScanTerm, Data.BatID, Data.DetailNo);
                        insData.m_SORT_NO = Cnt;
                        strSQL = insData.GetInsertQuery();
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                        Cnt++;
                    }
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// TRMEI取得
        /// </summary>
        public bool getTRMei(DetailData Detail, out TBL_TRMEI mei, FormBase form = null)
        {
            //初期化
            mei = new TBL_TRMEI(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            string strSQL = TBL_TRMEI.GetSelectQuery(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0) return false;
                    mei = new TBL_TRMEI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);

                    return true;
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
        }

        /// <summary>
        /// 持出アップロードステータス確認
        /// </summary>
        public bool ChkTRMeiImgUploadSts(ref ImageInfo img, int[] ErrSts, FormBase form = null)
        {
            // SELECT実行
            string strSQL = TBL_TRMEIIMG.GetSelectQuery(img.MeiImage._GYM_ID, img.MeiImage._OPERATION_DATE, img.MeiImage._SCAN_TERM, img.MeiImage._BAT_ID, 
                                                        img.MeiImage._DETAILS_NO, img.MeiImage._IMG_KBN, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0) return false;

                    //最新データに置き換え
                    img.MeiImage = new TBL_TRMEIIMG(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    if (ErrSts.Contains(img.MeiImage.m_BUA_STS))
                    {
                        return false;
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
        }

        /// <summary>
        /// TRMEI編集中更新
        /// </summary>
        public bool UpdateTRMEIEditFlg(DetailData Detail, int EditFlg, FormBase form = null)
        {
            // 更新
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = SQLSearch.GetUpdateTRMEIEditFlg(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, EditFlg, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// TRMEIIMGステータス更新
        /// </summary>
        public bool UpdateTRMEIIMGSts(TBL_TRMEIIMG meiimg, Dictionary<string, int> UpdateData,
                                      AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran, FormBase form = null)
        {
            // 更新
            try
            {
                string strSQL = SQLSearch.GetUpdateTRMEIIMG(meiimg._GYM_ID, meiimg._OPERATION_DATE, meiimg._SCAN_TERM, meiimg._BAT_ID, meiimg._DETAILS_NO, meiimg._IMG_KBN,
                                                            UpdateData, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 明細イメージアップロードステータスチェック
        /// </summary>
        public bool ChkTRMEIIMGUploadSts(DetailData Detail, out List<TBL_TRMEIIMG> ImgList, FormBase form = null)
        {
            // 初期化
            ImgList = new List<TBL_TRMEIIMG>();

            // SELECT実行
            string strSQL = TBL_TRMEIIMG.GetSelectQueryNotDel(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        ImgList.Add(new TBL_TRMEIIMG(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                    }

                    if (ImgList.Count(x => x.m_BUA_STS == TrMei.Sts.ファイル作成 || x.m_BUA_STS == TrMei.Sts.アップロード) > 0)
                    {
                        return false;
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
        }

        /// <summary>
        /// 明細データの削除処理
        /// </summary>
        public bool DeleteTrMei(DetailData Detail, string Key, FormBase form = null)
        {
            // 更新
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = string.Empty;

                    // TRMEIの削除
                    strSQL = SQLSearch.GetUpdateTRMEIDelete(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // TRMEIMGの削除
                    strSQL = SQLSearch.GetUpdateTRMEIIMGDetailDelete(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    //取得済データの削除処理
                    foreach (TBL_TRMEIIMG data in MeiImgList[Key])
                    {
                        data.m_DELETE_FLG = 1;
                    }
                    MeiDetails[Key].MeiDelete = 1;

                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 明細データの削除解除処理
        /// </summary>
        public bool UnDeleteTrMei(DetailData Detail, string Key, FormBase form = null)
        {
            // 更新
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = string.Empty;

                    // TRMEIの削除解除
                    strSQL = SQLSearch.GetUpdateTRMEIUnDelete(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // TRMEIMGの削除解除
                    strSQL = SQLSearch.GetUpdateTRMEIIMGDetailUnDelete(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    //取得済データの削除解除処理
                    foreach (TBL_TRMEIIMG data in MeiImgList[Key])
                    {
                        data.m_DELETE_FLG = 0;
                    }
                    MeiDetails[Key].MeiDelete = 0;
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 明細データの持出取消予約処理
        /// 持出取消アップロード状態を作成対象に更新
        /// </summary>
        public bool OCCancelTrMei(DetailData Detail, string Key, FormBase form = null)
        {
            // 更新
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // 持出取消アップロード状態を作成対象に更新
                    Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                    UpdateData.Add(TBL_TRMEI.BCA_STS, TrMei.Sts.作成対象);

                    string strSQL = string.Empty;
                    // TRMEIの削除
                    strSQL = SQLSearch.GetUpdateTRMEI(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, UpdateData, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // データの更新
                    MeiDetails[Key].BCASts = TrMei.Sts.作成対象;
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 明細データの持出取消予約キャンセル処理
        /// 持出取消アップロード状態を未作成に更新
        /// </summary>
        public bool OCCancelDisableTrMei(DetailData Detail, string Key, FormBase form = null)
        {
            // 更新
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // 持出取消アップロード状態を未作成に更新
                    Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                    UpdateData.Add(TBL_TRMEI.BCA_STS, TrMei.Sts.未作成);

                    string strSQL = string.Empty;
                    // TRMEIの削除
                    strSQL = SQLSearch.GetUpdateTRMEI(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, UpdateData, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // データの更新
                    MeiDetails[Key].BCASts = TrMei.Sts.未作成;
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 補正中ステータスチェック
        /// </summary>
        public bool ChkEntryHoseiSts(DetailData Detail, FormBase form = null)
        {
            int[] OKSts = { HoseiStatus.InputStatus.エントリ待, HoseiStatus.InputStatus.エントリ保留, HoseiStatus.InputStatus.完了, HoseiStatus.InputStatus.完了訂正保留 };

            // SELECT実行
            string strSQL = TBL_HOSEI_STATUS.GetSelectQuery(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0) return false;
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        if (!OKSts.Contains((new TBL_HOSEI_STATUS(tbl.Rows[i], AppInfo.Setting.SchemaBankCD)).m_INPT_STS))
                        {
                            return false;
                        }
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
        }

        /// <summary>
        /// 訂正後の取得データ部分更新
        /// </summary>
        public bool UpdateTeiseiData(DetailData Detail, FormBase form = null)
        {
            string strSQL = SQLSearch.GetOcMeiListRow(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        List<TBL_TRMEIIMG> ImgList = new List<TBL_TRMEIIMG>();

                        //取得データの置き換え
                        DetailData data = new DetailData(tbl.Rows[0], Detail.SortNo);
                        string key = CommonUtil.GenerateKey("|", Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo);
                        if (!FetchMeiImgList(ref data, ref ImgList, dbp, form)) return false;
                        MeiDetails[key] = data;
                        MeiImgList[key] = ImgList;
                    }

                    //対象明細のイメージ情報を設定
                    Fetch_meiimges();

                    return true;
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
        }

        /// <summary>
        /// ロック解除処理(交換尻)
        /// </summary>
        public bool UnLockKoukanJiri(FormBase form = null)
        {
            // 更新
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    DetailData Detail = MeiDetails[DetailParams.Key];
                    string strSQL = SQLSearch.GetUpdateUnlockIcMei(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo,
                                                                   HoseiStatus.HoseiInputMode.交換尻, HoseiStatus.InputStatus.完了訂正中, HoseiStatus.InputStatus.完了,
                                                                   AppInfo.Setting.SchemaBankCD);
                    int rtn = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    if (rtn > 0)
                    {
                        //更新データがある場合、表示データを置き換え
                        MeiDetails[DetailParams.Key].TeiseiInptSts = HoseiStatus.InputStatus.完了;
                        MeiDetails[DetailParams.Key].TeiseiTMNO = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ロック解除処理(編集フラグ)
        /// </summary>
        public bool UnLockEditFlg(FormBase form = null)
        {
            // 更新
            try
            {
                DetailData Detail = MeiDetails[DetailParams.Key];
                bool Rtn = UpdateTRMEIEditFlg(Detail, 0, form);
                if (Rtn)
                {
                    //処理成功時、データを置き換え
                    MeiDetails[DetailParams.Key].EditFlg = 0;
                }

                return Rtn;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
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
        /// カレントの明細データ移動
        /// </summary>
        public bool CurrentDetailMove(bool Prev)
        {
            string[] Keys = MeiDetails.Keys.ToArray();
            int curIdx = Array.IndexOf(Keys, DetailParams.Key);
            if (curIdx == -1) return false;
            if (Prev) { curIdx--; }
            else { curIdx++; }

            if (curIdx < 0 || Keys.Count() <= curIdx)
            {
                // 範囲外の場合はfalse
                return false;
            }

            //画面表示キーを変更
            DetailParams.Key = Keys[curIdx];

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
        /// 支店名の取得
        /// </summary>
        public string GeBranch(int brno)
        {
            IEnumerable<TBL_BRANCHMF> Data = _branchMF.Where(x => x._BR_NO == brno);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_BR_NAME_KANJI;
        }

        /// <summary>
        /// スキャン支店名の取得
        /// </summary>
        public string GeScanBranch(int brno)
        {
            IEnumerable<TBL_BRANCHMF> Data = _scanbranchMF.Where(x => x._BR_NO == brno);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_BR_NAME_KANJI;
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
        /// バッチフォルダパスを取得
        /// </summary>
        public string GetBankBacthFolder(int GymID, int opeDate, int BatID, int InputRoute)
        {
            string FolderPath = string.Empty;
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

            return Path.Combine(FolderPath, GymID.ToString("D3") + opeDate.ToString("D8") + BatID.ToString("D8"));
        }

        /// <summary>
        /// 通常バッチルートフォルダパスを取得
        /// </summary>
        public string BankNormalImageRoot()
        {
            return string.Format(NCR.Server.BankNormalImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 付帯バッチルートフォルダパスを取得
        /// </summary>
        public string BankFutaiImageRoot()
        {
            return string.Format(NCR.Server.BankFutaiImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 期日管理バッチルートフォルダパスを取得
        /// </summary>
        public string BankInventoryImageRoot()
        {
            return string.Format(NCR.Server.BankInventoryImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// ファイル出力パスを取得
        /// </summary>
        public string ReportFileOutPutPath()
        {
            return NCR.Server.ReportFileOutPutPath;
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            public int Rdate { get; set; } = -1;
            public int ScanBrNo { get; set; } = -1;
            public int ScanDate { get; set; } = -1;
            public int OCBRNoFrom { get; set; } = -1;
            public int OCBRNoTo { get; set; } = -1;
            public int BatNo { get; set; } = -1;
            public int ICBKNo { get; set; } = -1;
            public int ClearingDataFrom { get; set; } = -1;
            public int ClearingDataTo { get; set; } = -1;
            public long AmountFrom { get; set; } = -1;
            public long AmountTo { get; set; } = -1;
            public int Status { get; set; } = -1;
            public int Delete { get; set; } = -1;
            public string ntOpeNumer { get; set; } = string.Empty;
            public string Memo { get; set; } = string.Empty;
            public int MemoOpt { get; set; } = 0;
            public string ImgFLNm { get; set; } = string.Empty;
            public int ImgFLNmOpt { get; set; } = 0;
            public int BUASts { get; set; } = -1;
            public int BCASts { get; set; } = -1;
            public int BUA { get; set; } = -1;
            public int GMB { get; set; } = -1;
            public int GRASts { get; set; } = -1;
            public int GXA { get; set; } = -1;
            public int Def { get; set; } = -1;
            public int EditFlg { get; set; } = -1;

            public void Clear()
            {
                this.Rdate = -1;
                this.ScanBrNo = -1;
                this.ScanDate = -1;
                this.OCBRNoFrom = -1;
                this.OCBRNoTo = -1;
                this.BatNo = -1;
                this.ICBKNo = -1;
                this.ClearingDataFrom = -1;
                this.ClearingDataTo = -1;
                this.AmountFrom = -1;
                this.AmountTo = -1;
                this.Status = -1;
                this.Delete = -1;
                this.ntOpeNumer = string.Empty;
                this.Memo = string.Empty;
                this.MemoOpt = 0;
                this.ImgFLNm = string.Empty;
                this.ImgFLNmOpt = 0;
                this.BUASts = -1;
                this.BCASts = -1;
                this.BUA = -1;
                this.GMB = -1;
                this.GRASts = -1;
                this.GXA = -1;
                this.Def = -1;
                this.EditFlg = -1;
            }
        }

        /// <summary>
        /// 明細画面パラメーター
        /// </summary>
        public class DetailDispParams
        {
            public string Key { get; set; } = string.Empty;

            // 以下はロック解除で使用
            public int TeiseiInptSts { get; set; } = -1;
            public int DeleteFlg { get; set; } = -1;
            public int EditFlg { get; set; } = -1;
        }

        /// <summary>
        /// イメージ情報
        /// </summary>
        public class ImageInfo
        {
            public int ImgKbn { get; private set; }
            public bool HasImage { get; set; } = false;
            public TBL_TRMEIIMG MeiImage { get; set; } = null;
            public string ImgFolderPath { get; set; } = "";

            public ImageInfo(int imgKbm)
            {
                ImgKbn = imgKbm;
            }
        }

        /// <summary>
        /// 取得明細データ
        /// </summary>
        public class DetailData
        {
            public int SortNo { get; set; } = 0;
            public int GymID { get; set; } = 0;
            public int OpeDate { get; set; } = 0;
            public string ScanTerm { get; set; } = "";
            public int BatID { get; set; } = 0;
            public int DetailNo { get; set; } = 0;
            public int OCBKNo { get; set; } = 0;
            public int OCBRNo { get; set; } = 0;
            public int ScanBRNo { get; set; } = 0;
            public int ScanDate { get; set; } = 0;
            public int InputRoute { get; set; } = 0;
            public string ICBKNo { get; set; } = "";
            public string ClearingDate { get; set; } = "";
            public string Amount { get; set; } = "";
            public string PayKbn { get; set; } = "";
            public string ICBKOpeNo { get; set; } = "";
            public string AmountOpeNo { get; set; } = "";
            public int BCASts { get; set; } = 0;
            public int BUADate { get; set; } = 0;
            public int GMADate { get; set; } = 0;
            public int GRADate { get; set; } = 0;
            public int GXADate { get; set; } = 0;
            public string Memo { get; set; } = "";
            public int MeiDelete { get; set; } = 0;
            public int ICBKInptSts { get; set; } = -1;
            public int CDateInptSts { get; set; } = -1;
            public int AmountInptSts { get; set; } = -1;
            public int TeiseiInptSts { get; set; } = -1;
            public string TeiseiTMNO { get; set; } = "";
            public int ImgUploadSts { get; set; } = -1;
            public int EditFlg { get; set; } = -1;

            public DetailData(DataRow dr, int sortno)
            {
                SortNo = sortno;
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                OpeDate = DBConvert.ToIntNull(dr["OPERATION_DATE"]);
                ScanTerm = DBConvert.ToStringNull(dr["SCAN_TERM"]);
                BatID = DBConvert.ToIntNull(dr["BAT_ID"]);
                DetailNo = DBConvert.ToIntNull(dr["DETAILS_NO"]);
                OCBKNo = DBConvert.ToIntNull(dr["OC_BK_NO"]);
                OCBRNo = DBConvert.ToIntNull(dr["OC_BR_NO"]);
                ScanBRNo = DBConvert.ToIntNull(dr["SCAN_BR_NO"]);
                ScanDate = DBConvert.ToIntNull(dr["SCAN_DATE"]);
                InputRoute = DBConvert.ToIntNull(dr["INPUT_ROUTE"]);
                ICBKNo = DBConvert.ToStringNull(dr["ICBK_NO"]);
                ClearingDate = DBConvert.ToStringNull(dr["CLEARING_DATE"]);
                Amount = DBConvert.ToStringNull(dr["AMT"]);
                PayKbn = DBConvert.ToStringNull(dr["PAY_KBN"]);
                ICBKOpeNo = DBConvert.ToStringNull(dr["ICBK_OPENO"]);
                AmountOpeNo = DBConvert.ToStringNull(dr["AMT_OPENO"]);
                BCASts = DBConvert.ToIntNull(dr["BCA_STS"]);
                BUADate = DBConvert.ToIntNull(dr["BUA_DATE"]);
                GMADate = DBConvert.ToIntNull(dr["GMA_DATE"]);
                GRADate = DBConvert.ToIntNull(dr["GRA_DATE"]);
                GXADate = DBConvert.ToIntNull(dr["GXA_DATE"]);
                Memo = DBConvert.ToStringNull(dr["MEMO"]);
                MeiDelete = DBConvert.ToIntNull(dr["DELETE_FLG"]);
                ICBKInptSts = DBConvert.ToIntNull(dr["ICBKINPTSTS"]);
                CDateInptSts = DBConvert.ToIntNull(dr["CDATEINPTSTS"]);
                AmountInptSts = DBConvert.ToIntNull(dr["AMTINPTSTS"]);
                TeiseiInptSts = DBConvert.ToIntNull(dr["TEISEIINPTSTS"]);
                TeiseiTMNO = DBConvert.ToStringNull(dr["TEISEITMNO"]);
                EditFlg = DBConvert.ToIntNull(dr["EDIT_FLG"]);
            }
        }

    }
}
