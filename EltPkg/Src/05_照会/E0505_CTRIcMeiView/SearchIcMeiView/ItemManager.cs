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

namespace SearchIcMeiView
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
            FetchBillMF();
            FetchSyuruiMF();
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
        public bool FetchMeiList(int ListDispLimit, List<int> BillNoList, out bool LimitOver, FormBase form = null)
        {
            // 初期化
            LimitOver = false;
            MeiDetails = new Dictionary<string, DetailData>();
            MeiImgList = new Dictionary<string, List<TBL_TRMEIIMG>>();

            // SELECT実行
            string strSQL = SQLSearch.GetIcMeiList(GymParam.GymId.持帰, DispParams.Rdate, DispParams.OcBkNo, DispParams.ClearingDate, DispParams.AmountFrom, DispParams.AmountTo,
                                                   DispParams.BillNoFrom, DispParams.BillNoTo, DispParams.SyuriNo, DispParams.BrNo, DispParams.AccountNo, DispParams.TegataNo, DispParams.Status,
                                                   DispParams.entOpeNumer, DispParams.veriOpeNumer, DispParams.ImgFLNm, DispParams.ImgFLNmOpt, 
                                                   DispParams.TeiseiFlg, DispParams.FuwatariFlg, DispParams.GMASts, DispParams.GRASts, DispParams.Delete, DispParams.PayKbn, DispParams.BUB, 
                                                   AppInfo.Setting.SchemaBankCD, ListDispLimit, DispParams.DispSortType, BillNoList);
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
                    ImgList.Add(new TBL_TRMEIIMG(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
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
            foreach (TBL_TRMEIIMG data in MeiImgList[DetailParams.Key])
            {
                if (ImageInfos.ContainsKey(data._IMG_KBN))
                {
                    ImageInfos[data._IMG_KBN].MeiImage = data;
                    ImageInfos[data._IMG_KBN].HasImage = true;
                    ImageInfos[data._IMG_KBN].ImgFolderPath = GetBankBacthFolder(data._GYM_ID, data._OPERATION_DATE, data._BAT_ID);
                }
            }
        }

        /// <summary>
        /// 対象明細のファイル出力情報取得
        /// </summary>
        public bool FetchFileOutputData(Dictionary<string, DetailData> Details, int FileOutPutType, out List<FileOutputData> fileOutputs, FormBase form = null)
        {
            // 初期化
            fileOutputs = new List<FileOutputData>();

            // 取得
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    string strSQL = "";

                    // ファイル出力情報取得
                    List<FileOutputList> wkPosList = new List<FileOutputList>();
                    strSQL = SQLSearch.GetICMeiFileOutputInfo(GymParam.GymId.持帰, AppInfo.Setting.SchemaBankCD);
                    using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                    {
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            wkPosList.Add(new FileOutputList(tbl.Rows[i]));
                        }
                    }
                    if (wkPosList.Count() == 0) return true;

                    // 設定されているPosの最大値を取得
                    int PosMax = wkPosList.Max(x => x.ItemPos);

                    // 取得(削除データは除外)
                    foreach (DetailData Data in Details.Values.Where(x => x.MeiDelete == 0).OrderBy(x => x.SortNo))
                    {
                        List<FileOutputData> datas = new List<FileOutputData>();
                        strSQL = SQLSearch.GetICMeiFileOutputData(Data.GymID, Data.OpeDate, Data.ScanTerm, Data.BatID, Data.DetailNo, AppInfo.Setting.SchemaBankCD);
                        using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>()))
                        {
                            for (int i = 0; i < tbl.Rows.Count; i++)
                            {
                                datas.Add(new FileOutputData(tbl.Rows[i], Data.SortNo));
                            }
                        }

                        // 設定されているPosの最大値分出力情報を設定
                        for (int wkPos = 1; wkPos <= PosMax; wkPos++)
                        {
                            var wkdatas = datas.Where(x => x.ItemPos == wkPos);

                            switch (wkdatas.Count())
                            {
                                case 0:
                                    // 対象の出力位置のデータがない場合

                                    int Len = 0;
                                    if (FileOutPutType == (int)SettingData.FileOutType.Txt)
                                    {
                                        // 固定出力の場合、対象位置の長さを取得
                                        var wk = wkPosList.Where(x => x.ItemPos == wkPos);

                                        if (wk.Count() == 1)
                                        {
                                            // 対象の出力位置の長さを設定
                                            Len = wk.First().ItemLen;
                                        }
                                        else if(wk.Count() > 1)
                                        {
                                            // 同じ出力位置の情報が複数ある場合
                                            throw new Exception("出力設定に不備あります");
                                        }
                                    }

                                    // 空情報のデータを設定
                                    FileOutputData file = new FileOutputData(Data.GymID, Data.OpeDate, Data.ScanTerm, Data.BatID, Data.DetailNo,
                                                                             Len, wkPos, Data.SortNo);
                                    fileOutputs.Add(file);
                                    break;
                                case 1:
                                    // 対象の出力位置のデータがある場合
                                    fileOutputs.Add(wkdatas.First());
                                    break;
                                default:
                                    // 対象の出力位置のデータが複数ある場合はエラー
                                    throw new Exception("出力設定に不備あります");
                            }
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
                        MeiDetails[DetailParams.Key].KoukanjiriInptSts = HoseiStatus.InputStatus.完了;
                        MeiDetails[DetailParams.Key].KoukanjiriTMNO = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ロック解除処理(自行)
        /// </summary>
        public bool UnLockJikou(FormBase form = null)
        {
            // 更新
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    DetailData Detail = MeiDetails[DetailParams.Key];
                    string strSQL = SQLSearch.GetUpdateUnlockIcMei(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo,
                                                                   HoseiStatus.HoseiInputMode.自行情報, HoseiStatus.InputStatus.完了訂正中, HoseiStatus.InputStatus.完了,
                                                                   AppInfo.Setting.SchemaBankCD);
                    int rtn = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    if (rtn > 0)
                    {
                        //更新データがある場合、表示データを置き換え
                        MeiDetails[DetailParams.Key].JikouInptSts = HoseiStatus.InputStatus.完了;
                        MeiDetails[DetailParams.Key].JikouTMNO = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
            return true;
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
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 訂正後の取得データ部分更新
        /// </summary>
        public bool UpdateTeiseiData(DetailData Detail, FormBase form = null)
        {
            string strSQL = SQLSearch.GetIcMeiListRow(Detail.GymID, Detail.OpeDate, Detail.ScanTerm, Detail.BatID, Detail.DetailNo, AppInfo.Setting.SchemaBankCD);
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
        public string GetBank(int bkno)
        {
            IEnumerable<TBL_BANKMF> Data = _bankMF.Where(x => x._BK_NO == bkno);
            if (Data.Count() == 0) return string.Empty;
            return Data.First().m_BK_NAME_KANJI;
        }

        /// <summary>
        /// 支店名の取得
        /// </summary>
        public string GeBranch(string brno)
        {
            if (!int.TryParse(brno, out int intbrno))
            {
                return string.Empty;
            }

            return GeBranch(intbrno);
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
        public string GeBill(string billcode)
        {
            if (!int.TryParse(billcode, out int intbillcode))
            {
                return string.Empty;
            }

            return GeBill(intbillcode);
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
        public string GetSyurui(string syurui)
        {
            if (!int.TryParse(syurui, out int intsyurui))
            {
                return string.Empty;
            }

            return GetSyurui(intsyurui);
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
        public string GetBankBacthFolder(int GymID, int opeDate, int BatID)
        {
            string FolderPath = BankConfirmImageRoot();
            return Path.Combine(FolderPath, GymID.ToString("D3") + opeDate.ToString("D8") + BatID.ToString("D8"));
        }

        /// <summary>
        /// 持帰ダウンロード確定イメージルートフォルダパスを取得
        /// </summary>
        public string BankConfirmImageRoot()
        {
            return string.Format(NCR.Server.BankConfirmImageRoot, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// ファイル出力パスを取得
        /// </summary>
        public string ReportFileOutPutPath()
        {
            return NCR.Server.ReportFileOutPutPath;
        }

        /// <summary>
        /// 対象のENDDATA取得
        /// </summary>
        public string GetEndData(List<TBL_TRITEM> tRITEM, int id)
        {
            IEnumerable<TBL_TRITEM> ts = tRITEM.Where(x => x._ITEM_ID == id);
            if (ts.Count() == 0) return string.Empty;

            return ts.First().m_END_DATA;
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public int Rdate { get; set; } = -1;
            public int OcBkNo { get; set; } = -1;
            public int ClearingDate { get; set; } = -1;
            public long AmountFrom { get; set; } = -1;
            public long AmountTo { get; set; } = -1;
            public int BillNoFrom { get; set; } = -1;
            public int BillNoTo { get; set; } = -1;
            public int SyuriNo { get; set; } = -1;
            public int BrNo { get; set; } = -1;
            public long AccountNo { get; set; } = -1;
            public long TegataNo { get; set; } = -1;
            public int Status { get; set; } = -1;
            public string entOpeNumer { get; set; } = string.Empty;
            public string veriOpeNumer { get; set; } = string.Empty;
            public string ImgFLNm { get; set; } = string.Empty;
            public int ImgFLNmOpt { get; set; } = 0;
            public int TeiseiFlg { get; set; } = -1;
            public int FuwatariFlg { get; set; } = -1;
            public int GMASts { get; set; } = -1;
            public int GRASts { get; set; } = -1;
            public int Delete { get; set; } = -1;
            public int PayKbn { get; set; } = -1;
            public int BUB { get; set; } = -1;
            public bool DispSortType { get; set; } = false;

            public void Clear()
            {
                this.Rdate = -1;
                this.OcBkNo = -1;
                this.ClearingDate = -1;
                this.AmountFrom = -1;
                this.AmountTo = -1;
                this.BillNoFrom = -1;
                this.BillNoTo = -1;
                this.SyuriNo = -1;
                this.BrNo = -1;
                this.AccountNo = -1;
                this.TegataNo = -1;
                this.Status = -1;
                this.entOpeNumer = string.Empty;
                this.veriOpeNumer = string.Empty;
                this.ImgFLNm = string.Empty;
                this.ImgFLNmOpt = 0;
                this.TeiseiFlg = -1;
                this.FuwatariFlg = -1;
                this.GMASts = -1;
                this.GRASts = -1;
                this.Delete = -1;
                this.PayKbn = -1;
                this.BUB = -1;
                //this.DispSortType = false;
            }
        }

        /// <summary>
        /// 明細画面パラメーター
        /// </summary>
        public class DetailDispParams
        {
            public string Key { get; set; } = string.Empty;
            
            // 以下はロック解除で使用
            public int JikouInptSts { get; set; } = -1;
            public int KoukanjiriInptSts { get; set; } = -1;
            public int DeleteFlg { get; set; } = -1;
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
            public int BMASts { get; set; } = 0;
            public int BRASts { get; set; } = 0;
            public int BUBDate { get; set; } = 0;
            public string InptCTRICBKNo { get; set; } = "";
            public string InptICBKNo { get; set; } = "";
            public string CTRICBKNo { get; set; } = "";
            public string ICBKNo { get; set; } = "";
            public string InptCTRClearingDate { get; set; } = "";
            public string InptClearingDate { get; set; } = "";
            public string CTRClearingDate { get; set; } = "";
            public string ClearingDate { get; set; } = "";
            public string CTRAmount { get; set; } = "";
            public string Amount { get; set; } = "";
            public string PayKbn { get; set; } = "";
            public string CTRBillCD { get; set; } = "";
            public string BillCD { get; set; } = "";
            public string CTRSyuruiCD { get; set; } = "";
            public string SyuruiCD { get; set; } = "";
            public string InptCTRICBrCD { get; set; } = "";
            public string InptICBrCD { get; set; } = "";
            public string CTRICBrCD { get; set; } = "";
            public string ICBrCD { get; set; } = "";
            public string InptCTRAccount { get; set; } = "";
            public string InptAccount { get; set; } = "";
            public string CTRAccount { get; set; } = "";
            public string Account { get; set; } = "";
            public string CTRTegata { get; set; } = "";
            public string Tegata { get; set; } = "";
            public string ICBKEOpe { get; set; } = "";
            public string ICBKVOpe { get; set; } = "";
            public string CDateEOpe { get; set; } = "";
            public string CDateVOpe { get; set; } = "";
            public string AmountEOpe { get; set; } = "";
            public string AmountVOpe { get; set; } = "";
            public string JikouEOpe { get; set; } = "";
            public string JikouVOpe { get; set; } = "";
            public int ICBKInptSts { get; set; } = 0;
            public int CDATEInptSts { get; set; } = 0;
            public int AMTInptSts { get; set; } = 0;
            public int JikouInptSts { get; set; } = 0;
            public string JikouTMNO { get; set; } = "";
            public int KoukanjiriInptSts { get; set; } = 0;
            public string KoukanjiriTMNO { get; set; } = "";
            public int Fuwatari { get; set; } = 0;
            public int FuwatariDelete { get; set; } = 0;
            public int MeiDelete { get; set; } = 0;

            public DetailData(DataRow dr, int sortno)
            {
                SortNo = sortno;
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                OpeDate = DBConvert.ToIntNull(dr["OPERATION_DATE"]);
                ScanTerm = DBConvert.ToStringNull(dr["SCAN_TERM"]);
                BatID = DBConvert.ToIntNull(dr["BAT_ID"]);
                DetailNo = DBConvert.ToIntNull(dr["DETAILS_NO"]);
                OCBKNo = DBConvert.ToIntNull(dr["IC_OC_BK_NO"]);
                BMASts = DBConvert.ToIntNull(dr["GMA_STS"]);
                BRASts = DBConvert.ToIntNull(dr["GRA_STS"]);
                BUBDate = DBConvert.ToIntNull(dr["BUB_DATE"]);
                InptCTRICBKNo = DBConvert.ToStringNull(dr["INPT_CTRICBKNO"]);
                InptICBKNo = DBConvert.ToStringNull(dr["INPT_ICBKNO"]);
                CTRICBKNo = DBConvert.ToStringNull(dr["CTRICBKNO"]);
                ICBKNo = DBConvert.ToStringNull(dr["ICBKNO"]);
                InptCTRClearingDate = DBConvert.ToStringNull(dr["INPT_CTRCLEARING_DATE"]);
                InptClearingDate = DBConvert.ToStringNull(dr["INPT_CLEARING_DATE"]);
                CTRClearingDate = DBConvert.ToStringNull(dr["CTRCLEARING_DATE"]);
                ClearingDate = DBConvert.ToStringNull(dr["CLEARING_DATE"]);
                CTRAmount = DBConvert.ToStringNull(dr["CTRAMT"]);
                Amount = DBConvert.ToStringNull(dr["AMT"]);
                PayKbn = DBConvert.ToStringNull(dr["PAYKBN"]);
                CTRBillCD = DBConvert.ToStringNull(dr["CTRBILLCD"]);
                BillCD = DBConvert.ToStringNull(dr["BILLCD"]);
                CTRSyuruiCD = DBConvert.ToStringNull(dr["CTRSYURUICD"]);
                SyuruiCD = DBConvert.ToStringNull(dr["SYURUICD"]);
                InptCTRICBrCD = DBConvert.ToStringNull(dr["INPT_CTRICBRNO"]);
                InptICBrCD = DBConvert.ToStringNull(dr["INPT_ICBRNO"]);
                CTRICBrCD = DBConvert.ToStringNull(dr["CTRICBRNO"]);
                ICBrCD = DBConvert.ToStringNull(dr["ICBRNO"]);
                InptCTRAccount = DBConvert.ToStringNull(dr["INPT_CTRACCOUNT"]);
                InptAccount = DBConvert.ToStringNull(dr["INPT_ACCOUNT"]);
                CTRAccount = DBConvert.ToStringNull(dr["CTRACCOUNT"]);
                Account = DBConvert.ToStringNull(dr["ACCOUNT"]);
                CTRTegata = DBConvert.ToStringNull(dr["CTRTEGATA"]);
                Tegata = DBConvert.ToStringNull(dr["TEGATA"]);
                ICBKEOpe = DBConvert.ToStringNull(dr["ICBK_EOPENO"]);
                ICBKVOpe = DBConvert.ToStringNull(dr["ICBK_VOPENO"]);
                CDateEOpe = DBConvert.ToStringNull(dr["CDATE_EOPENO"]);
                CDateVOpe = DBConvert.ToStringNull(dr["CDATE_VOPENO"]);
                AmountEOpe = DBConvert.ToStringNull(dr["AMT_EOPENO"]);
                AmountVOpe = DBConvert.ToStringNull(dr["AMT_VOPENO"]);
                JikouEOpe = DBConvert.ToStringNull(dr["JIKOU_EOPENO"]);
                JikouVOpe = DBConvert.ToStringNull(dr["JIKOU_VOPENO"]);
                ICBKInptSts = DBConvert.ToIntNull(dr["ICBKINPTSTS"]);
                CDATEInptSts = DBConvert.ToIntNull(dr["CDATEINPTSTS"]);
                AMTInptSts = DBConvert.ToIntNull(dr["AMTINPTSTS"]);
                JikouInptSts = DBConvert.ToIntNull(dr["JIKOUINPTSTS"]);
                JikouTMNO = DBConvert.ToStringNull(dr["JIKOUTMNO"]);
                KoukanjiriInptSts = DBConvert.ToIntNull(dr["KOUKANJIRIINPTSTS"]);
                KoukanjiriTMNO = DBConvert.ToStringNull(dr["KOUKANJIRITMNO"]);
                Fuwatari = DBConvert.ToIntNull(dr["FUWATARI"]);
                FuwatariDelete = DBConvert.ToIntNull(dr["FUWATARI_DELETE_FLG"]);
                MeiDelete = DBConvert.ToIntNull(dr["DELETE_FLG"]);
            }
        }

        /// <summary>
        /// ファイル出力データ
        /// </summary>
        public class FileOutputData
        {
            public int SortNo { get; set; } = 0;
            public string GrpKey { get; set; } = "";
            public int GymID { get; set; } = 0;
            public int OpeDate { get; set; } = 0;
            public string ScanTerm { get; set; } = "";
            public int BatID { get; set; } = 0;
            public int DetailNo { get; set; } = 0;
            public int ItemID { get; set; } = 0;
            public string DispName { get; set; } = "";
            public string ItemType { get; set; } = "";
            public int ItemLen { get; set; } = 0;
            public int ItemPos { get; set; } = 0;
            public string EndData { get; set; } = "";

            public FileOutputData(int gymid, int opedate, string scanterm, int batid, int detailno, 
                                  int itemlen, int itempos, int sortno)
            {
                SortNo = sortno;
                GymID = gymid;
                OpeDate = opedate;
                ScanTerm = scanterm;
                BatID = batid;
                DetailNo = detailno;
                ItemID = DspItem.ItemId.最終項目;
                DispName = "";
                ItemType = "";
                ItemLen = itemlen;
                ItemPos = itempos;
                EndData = "";
                //グループキー設定
                GrpKey = CommonUtil.GenerateKey("|", GymID, OpeDate, ScanTerm, BatID, DetailNo);
            }

            public FileOutputData(DataRow dr, int sortno)
            {
                SortNo = sortno;
                GymID = DBConvert.ToIntNull(dr["GYM_ID"]);
                OpeDate = DBConvert.ToIntNull(dr["OPERATION_DATE"]);
                ScanTerm = DBConvert.ToStringNull(dr["SCAN_TERM"]);
                BatID = DBConvert.ToIntNull(dr["BAT_ID"]);
                DetailNo = DBConvert.ToIntNull(dr["DETAILS_NO"]);
                ItemID = DBConvert.ToIntNull(dr["ITEM_ID"]);
                DispName = DBConvert.ToStringNull(dr["ITEM_DISPNAME"]);
                ItemType = DBConvert.ToStringNull(dr["ITEM_TYPE"]);
                ItemLen = DBConvert.ToIntNull(dr["ITEM_LEN"]);
                ItemPos = DBConvert.ToIntNull(dr["POS"]);
                EndData = DBConvert.ToStringNull(dr["END_DATA"]);
                //グループキー設定
                GrpKey = CommonUtil.GenerateKey("|", GymID, OpeDate, ScanTerm, BatID, DetailNo);
            }
        }

        /// <summary>
        /// ファイル出力情報
        /// </summary>
        public class FileOutputList
        {
            public int ItemID { get; set; } = 0;
            public int ItemLen { get; set; } = 0;
            public int ItemPos { get; set; } = 0;

            public FileOutputList(int id, int len, int pos)
            {
                ItemID = id;
                ItemLen = len;
                ItemPos = pos;
            }

            public FileOutputList(DataRow dr)
            {
                ItemID = DBConvert.ToIntNull(dr["ITEM_ID"]);
                ItemLen = DBConvert.ToIntNull(dr["ITEM_LEN"]);
                ItemPos = DBConvert.ToIntNull(dr["POS"]);
            }
        }

    }
}
