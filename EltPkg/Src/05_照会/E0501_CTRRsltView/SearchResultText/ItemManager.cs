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

namespace SearchResultText
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        /// <summary>汎用テキストデータ</summary>
        public List<TBL_GENERALTEXTMF> GeneralTextMF { get; set; }
        /// <summary>ファイルパラメータデータ</summary>
        public List<TBL_FILE_PARAM> FileParamMF { get; set; }

        /// <summary>結果照会画面パラメーター</summary>
        public SearchResultDispParams ResultDispParams { get; set; }
        /// <summary>結果照会一覧データ</summary>
        public Dictionary<string, TBL_RESULTTXT_CTL> Result_Ctl { get; set; }
        /// <summary>レコード一覧照会画面パラメーター</summary>
        public SearchRecordListDispParams RecordListDispParams { get; set; }
        /// <summary>レコード一覧照会データ</summary>
        public Dictionary<string, TBL_RESULTTXT> ResultTxtList { get; set; }
        /// <summary>レコード明細照会データ</summary>
        public TBL_RESULTTXT ResultTxt { get; set; }

        #region GeneralText定義

        public enum DispType
        {
            /// <summary>説明</summary>
            DESCRIPTION = 1,
            /// <summary>略称</summary>
            ABBREVIATE = 2,
        }

        public enum HeaderNo
        {
            /// <summary>ファイルチェック結果コード</summary>
            FILE_CHK_CODE = 10,
        }

        public enum DataNo
        {
            /// <summary>受付内容</summary>
            RECEPTION = 3,
            /// <summary>処理結果コード</summary>
            RET_CODE = 5,
            /// <summary>証券イメージファイル名</summary>
            IMG_NAME = 6,
            /// <summary>表証券イメージファイル名</summary>
            FRONT_IMG_NAME = 7,
            /// <summary>表裏等の別</summary>
            IMG_KBN = 8,
            /// <summary>ファイル名持出銀行コード</summary>
            FILE_OC_BK_NO = 9,
            /// <summary>読替持出銀行コード</summary>
            CHG_OC_BK_NO = 10,
            /// <summary>持出支店コード</summary>
            OC_BR_NO = 11,
            /// <summary>持出日</summary>
            OC_DATE = 12,
            /// <summary>持出時接続方式</summary>
            OC_METHOD = 13,
            /// <summary>ユーザID(持出者)</summary>
            OC_USERID = 14,
            /// <summary>決済対象区分</summary>
            PAY_KBN = 15,
            /// <summary>交換尻確定フラグ</summary>
            BALANCE_FLG = 16,
            /// <summary>MICR持帰銀行コード</summary>
            MICR_IC_BK_NO = 17,
            /// <summary>OCR持帰銀行コード</summary>
            OCR_IC_BK_NO = 18,
            /// <summary>QRコード持帰銀行コード</summary>
            QR_IC_BK_NO = 19,
            /// <summary>ファイル名持帰銀行コード</summary>
            FILE_IC_BK_NO = 20,
            /// <summary>読替持帰銀行コード</summary>
            CHG_IC_BK_NO = 21,
            /// <summary>証券データ訂正持帰銀行コード</summary>
            TEISEI_IC_BK_NO = 22,
            /// <summary>決済持帰銀行コード</summary>
            PAY_IC_BK_NO = 23,
            /// <summary>決済後訂正持帰銀行コード</summary>
            PAYAFT_REV_IC_BK_NO = 24,
            /// <summary>OCR持帰銀行コード確信度</summary>
            OCR_IC_BK_NO_CONF = 25,
            /// <summary>OCR金額</summary>
            OCR_AMOUNT = 26,
            /// <summary>MICR金額</summary>
            MICR_AMOUNT = 27,
            /// <summary>QRコード金額</summary>
            QR_AMOUNT = 28,
            /// <summary>ファイル名金額</summary>
            FILE_AMOUNT = 29,
            /// <summary>証券データ訂正金額</summary>
            TEISEI_AMOUNT = 30,
            /// <summary>決済金額</summary>
            PAY_AMOUNT = 31,
            /// <summary>決済後訂正金額</summary>
            PAYAFT_REV_AMOUNT = 32,
            /// <summary>OCR金額確信度</summary>
            OCR_AMOUNT_CONF = 33,
            /// <summary>持出時交換希望日</summary>
            OC_CLEARING_DATE = 34,
            /// <summary>証券データ訂正交換希望日</summary>
            TEISEI_CLEARING_DATE = 35,
            /// <summary>交換日</summary>
            CLEARING_DATE = 36,
            /// <summary>QRコード持帰支店コード</summary>
            QR_IC_BR_NO = 37,
            /// <summary>科目コード</summary>
            KAMOKU = 38,
            /// <summary>口座番号</summary>
            ACCOUNT = 39,
            /// <summary>銀行管理番号</summary>
            BK_CTL_NO = 40,
            /// <summary>自由記述欄</summary>
            FREEFIELD = 41,
            /// <summary>交換証券種類コード</summary>
            BILL_CODE = 42,
            /// <summary>交換証券種類コード確信度</summary>
            BILL_CODE_CONF = 43,
            /// <summary>QRコード情報</summary>
            QR = 44,
            /// <summary>MICR情報</summary>
            MICR = 45,
            /// <summary>MICR情報確信度</summary>
            MICR_CONF = 46,
            /// <summary>手形・小切手番号</summary>
            BILL_NO = 47,
            /// <summary>手形・小切手番号確信度</summary>
            BILL_NO_CONF = 48,
            /// <summary>不渡返還区分１</summary>
            FUBI_KBN_01 = 49,
            /// <summary>0号不渡事由コード１</summary>
            ZERO_FUBINO_01 = 50,
            /// <summary>不渡返還区分２</summary>
            FUBI_KBN_02 = 51,
            /// <summary>0号不渡事由コード２</summary>
            ZRO_FUBINO_02 = 52,
            /// <summary>不渡返還区分３</summary>
            FUBI_KBN_03 = 53,
            /// <summary>0号不渡事由コード３</summary>
            ZRO_FUBINO_03 = 54,
            /// <summary>不渡返還区分４</summary>
            FUBI_KBN_04 = 55,
            /// <summary>0号不渡事由コード４</summary>
            ZRO_FUBINO_04 = 56,
            /// <summary>不渡返還区分５</summary>
            FUBI_KBN_05 = 57,
            /// <summary>0号不渡事由コード５</summary>
            ZRO_FUBINO_05 = 58,
            /// <summary>持帰状況フラグ</summary>
            IC_FLG = 59,
        }

        #endregion 


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.ResultDispParams = new SearchResultDispParams();
            this.ResultDispParams.Clear();
            this.RecordListDispParams = new SearchRecordListDispParams();
            this.RecordListDispParams.Clear();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public override void FetchAllData() 
        {
            FetchGeneralTextMF();
            FetchFileParamMF();
        }

        /// <summary>
        /// 汎用テキスト一覧取得
        /// </summary>
        public bool FetchGeneralTextMF(FormBase form = null)
        {
            // 初期化
            GeneralTextMF = new List<TBL_GENERALTEXTMF>();

            // SELECT実行
            string strSQL = TBL_GENERALTEXTMF.GetSelectQueryTextKbn((int)TBL_GENERALTEXTMF.TextKbn.RESULTTXT);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {

                        TBL_GENERALTEXTMF ctl = new TBL_GENERALTEXTMF(tbl.Rows[i]);
                        GeneralTextMF.Add(ctl);
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
        /// ファイルパラメータ一覧取得
        /// </summary>
        public bool FetchFileParamMF(FormBase form = null)
        {
            // 初期化
            FileParamMF = new List<TBL_FILE_PARAM>();

            // SELECT実行
            string strSQL = TBL_FILE_PARAM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_FILE_PARAM ctl = new TBL_FILE_PARAM(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        if (ctl._FILE_ID == FileParam.FileId.IF206)
                        {
                            FileParamMF.Add(ctl);
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
        /// 結果テキスト管理一覧取得
        /// </summary>
        public bool FetchResultTextControl(int ListDispLimit, out bool LimitOver, FormBase form = null)
        {
            // 初期化
            LimitOver = false;

            // SELECT実行
            string strSQL = SQLSearch.Get_SearchResultData(ResultDispParams.RecvDate, ResultDispParams.FileDivid, AppInfo.Setting.SchemaBankCD, ResultDispParams.ErrFlg, ListDispLimit);
            Result_Ctl = new Dictionary<string, TBL_RESULTTXT_CTL>();
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

                        TBL_RESULTTXT_CTL ctl = new TBL_RESULTTXT_CTL(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        string key = ctl._FILE_NAME;
                        Result_Ctl.Add(key, ctl);
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
        /// 結果テキスト一覧取得
        /// </summary>
        public bool FetchResultTextList(FormBase form = null)
        {
            // SELECT実行
            string strSQL = SQLSearch.Get_SearchResultTxtData(RecordListDispParams.TargetFileName, RecordListDispParams.ListErrFlg, AppInfo.Setting.SchemaBankCD);
            ResultTxtList = new Dictionary<string, TBL_RESULTTXT>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_RESULTTXT ctl = new TBL_RESULTTXT(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        string key = ctl._SEQ.ToString();
                        ResultTxtList.Add(key, ctl);
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
        /// 結果テキスト取得
        /// </summary>
        public bool FetchResultText(FormBase form = null)
        {
            ResultTxt = new TBL_RESULTTXT(AppInfo.Setting.SchemaBankCD);

            if (!ResultTxtList.ContainsKey(RecordListDispParams.SEQ.ToString()))
            {
                return false;
            }
            ResultTxt = ResultTxtList[RecordListDispParams.SEQ.ToString()];
            return true;
        }

        /// <summary>
        /// 汎用テキスト変換(ヘッダー)
        /// </summary>
        public string GetGeneralTextHeader(HeaderNo no, string value, DispType Type)
        {
            return GetGeneralText((int)TBL_GENERALTEXTMF.RecordKbn.HEADER, (int)no, value, (int)Type);
        }

        /// <summary>
        /// 汎用テキスト変換(データ)
        /// </summary>
        public string GetGeneralTextData(DataNo no, string value, DispType Type)
        {
            return GetGeneralText((int)TBL_GENERALTEXTMF.RecordKbn.DATA, (int)no, value, (int)Type);
        }

        /// <summary>
        /// 汎用テキスト変換
        /// </summary>
        private string GetGeneralText(int recordkbn, int no, string value, int Type)
        {
            //対象データ取得
            IEnumerable<TBL_GENERALTEXTMF> list = GeneralTextMF.Where(x => x._RECORDKBN == recordkbn && x._NO == no && x._VALUE.Trim() == value.Trim());

            if (list.Count() == 0) return value;

            if (Type == 1)
            {
                return string.Format("{0}：{1}", value, list.First().m_DESCRIPTION);
            }
            else
            {
                return string.Format("{0}：{1}", value, list.First().m_ABBREVIATE);
            }
        }

        /// <summary>
        /// ファイルパラメータ名取得
        /// </summary>
        public string GetFileParamName(string fileDivid)
        {
            //対象データ取得
            IEnumerable<TBL_FILE_PARAM> list = FileParamMF.Where(x => x._FILE_DIVID == fileDivid);

            if (list.Count() == 0) return fileDivid;
            return list.First().m_FILE_NAME;
        }

        /// <summary>
        /// 表示対象の結果テキスト管理取得
        /// </summary>
        public TBL_RESULTTXT_CTL GetResultTextControl()
        {
            //対象データ取得
            IEnumerable<TBL_RESULTTXT_CTL> list = Result_Ctl.Values.Where(x => x._FILE_NAME == RecordListDispParams.TargetFileName);
            if (list.Count() == 0) return new TBL_RESULTTXT_CTL(AppInfo.Setting.SchemaBankCD);

            return list.First();
        }

        /// <summary>
        /// 対象の結果テキスト管理取得(ファイル名指定)
        /// </summary>
        public TBL_RESULTTXT_CTL GetResultTextControl(string FileName)
        {
            //対象データ取得
            IEnumerable<TBL_RESULTTXT_CTL> list = Result_Ctl.Values.Where(x => x._FILE_NAME == FileName);

            if (list.Count() == 0) return new TBL_RESULTTXT_CTL(AppInfo.Setting.SchemaBankCD);

            return list.First();
        }

        /// <summary>
        /// 持出アップロードの対象データ取得
        /// </summary>
        public bool GetBUBData(string filename, out TBL_TRMEIIMG Data, bool route, out int InputRoute, FormBase form = null)
        {
            //初期化
            InputRoute = 0;
            Data = new TBL_TRMEIIMG(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            string strSQL = SQLSearch.Get_SearchResultDataBUB(GymParam.GymId.持出, filename, AppInfo.Setting.SchemaBankCD, route);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0)
                    {
                        return false;
                    }
                    if (route)
                    {
                        InputRoute = DBConvert.ToIntNull(tbl.Rows[0][TBL_TRBATCH.INPUT_ROUTE]);
                    }
                    Data = new TBL_TRMEIIMG(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
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
        /// 持出取消の対象データ取得
        /// </summary>
        public bool GetBCAData(string filename, out TBL_TRMEI Data, bool route, out int InputRoute, FormBase form = null)
        {
            return GetTRMEIData(GymParam.GymId.持出, filename, out Data, route, out InputRoute, form);
        }

        /// <summary>
        /// 証券データ訂正の対象データ取得
        /// </summary>
        public bool GetGMAData(string filename, out TBL_TRMEI Data, FormBase form = null)
        {
            return GetTRMEIData(GymParam.GymId.持帰, filename, out Data, false, out int InputRoute, form);
        }

        /// <summary>
        /// 不渡返還の対象データ取得
        /// </summary>
        public bool GetGRAData(string filename, out TBL_TRMEI Data, FormBase form = null)
        {
            return GetTRMEIData(GymParam.GymId.持帰, filename, out Data, false, out int InputRoute, form);
        }

        /// <summary>
        /// TBL_TRMEIの対象データ取得
        /// </summary>
        private bool GetTRMEIData(int gymid, string filename, out TBL_TRMEI Data, bool route, out int InputRoute, FormBase form = null)
        {
            //初期化
            InputRoute = 0;
            Data = new TBL_TRMEI(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            string strSQL = SQLSearch.Get_SearchResultDataOther(gymid, filename, AppInfo.Setting.SchemaBankCD, route);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0)
                    {
                        return false;
                    }
                    Data = new TBL_TRMEI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    if (route)
                    {
                        InputRoute = DBConvert.ToIntNull(tbl.Rows[0][TBL_TRBATCH.INPUT_ROUTE]);
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
        /// 持出アップロードの再送登録
        /// </summary>
        public bool UpdateBUBData(string filename, FormBase form = null)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = SQLSearch.Get_UpdateSearchResultDataBUB(GymParam.GymId.持出, filename, AppInfo.Setting.SchemaBankCD);
                    if (dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans) == 0)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
        }

        /// <summary>
        /// 持出取消の再送登録
        /// </summary>
        public bool UpdateBCAData(string filename, FormBase form = null)
        {
            return UpdateTRMEIData(GymParam.GymId.持出, filename, TBL_TRMEI.BCA_STS, form);
        }

        /// <summary>
        /// 証券データ訂正の再送登録
        /// </summary>
        public bool UpdateGMAData(string filename, FormBase form = null)
        {
            return UpdateTRMEIData(GymParam.GymId.持帰, filename, TBL_TRMEI.GMA_STS, form);
        }

        /// <summary>
        /// 不渡返還の再送登録
        /// </summary>
        public bool UpdateGRAData(string filename, FormBase form = null)
        {
            return UpdateTRMEIData(GymParam.GymId.持帰, filename, TBL_TRMEI.GRA_STS, form);
        }

        /// <summary>
        /// 対象データの再送登録
        /// </summary>
        private bool UpdateTRMEIData(int gymid, string filename, string UpdateField, FormBase form = null)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = SQLSearch.Get_UpdateSearchResultDataOther(gymid, filename, AppInfo.Setting.SchemaBankCD, UpdateField);
                    if (dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans) == 0)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
        }

        /// <summary>
        /// 結果ファイル単位での持出アップロードの再送登録
        /// </summary>
        public bool UpdateBUBFile(string procfilename, out int CommitCount, FormBase form = null)
        {
            // 初期化
            CommitCount = 0;
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = SQLSearch.Get_UpdateSearchResultFileBUB(GymParam.GymId.持出, procfilename, AppInfo.Setting.SchemaBankCD);
                    CommitCount = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    return true;
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
        }

        /// <summary>
        /// 結果ファイル単位での持出取消の再送登録
        /// </summary>
        public bool UpdateBCAFile(string procfilename, out int CommitCount, FormBase form = null)
        {
            return UpdateTRMEIFile(GymParam.GymId.持出, procfilename, TBL_TRMEI.BCA_STS, out CommitCount, form);
        }

        /// <summary>
        /// 結果ファイル単位での証券データ訂正の再送登録
        /// </summary>
        public bool UpdateGMAFile(string procfilename, out int CommitCount, FormBase form = null)
        {
            return UpdateTRMEIFile(GymParam.GymId.持帰, procfilename, TBL_TRMEI.GMA_STS, out CommitCount, form);
        }

        /// <summary>
        /// 結果ファイル単位での不渡返還の再送登録
        /// </summary>
        public bool UpdateGRAFile(string procfilename, out int CommitCount, FormBase form = null)
        {
            return UpdateTRMEIFile(GymParam.GymId.持帰, procfilename, TBL_TRMEI.GRA_STS, out CommitCount, form);
        }

        /// <summary>
        /// 結果ファイル単位での対象データの再送登録
        /// </summary>
        private bool UpdateTRMEIFile(int gymid, string procfilename, string UpdateField, out int CommitCount, FormBase form = null)
        {
            // 初期化
            CommitCount = 0;
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    string strSQL = SQLSearch.Get_UpdateSearchResultFileOther(gymid, procfilename, AppInfo.Setting.SchemaBankCD, UpdateField);
                    CommitCount = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    return true;
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    return false;
                }
            }
        }

        /// <summary>
        /// バッチフォルダパスを取得
        /// </summary>
        public string GetBankBacthFolder(int GymID, int opeDate, int BatID, int InputRoute)
        {
            string FolderPath = string.Empty;
            if (GymID == GymParam.GymId.持帰)
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

            return System.IO.Path.Combine(FolderPath, GymID.ToString("D3") + opeDate.ToString("D8") + BatID.ToString("D8"));
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
        /// 結果照会画面パラメーター
        /// </summary>
        public class SearchResultDispParams
        {
            public string FileDivid { get; set; } = string.Empty;
            public int RecvDate { get; set; } = -1;
            public int ErrFlg { get; set; } = -1;
            public string FileName { get; set; } = string.Empty;

            public void Clear()
            {
                this.FileDivid = string.Empty;
                this.RecvDate = -1;
                this.ErrFlg = -1;
                this.FileName = string.Empty;
            }
        }

        /// <summary>
        /// レコード一覧照会画面パラメーター
        /// </summary>
        public class SearchRecordListDispParams
        {
            public string TargetFileDicid { get; set; } = string.Empty;
            public string TargetFileName { get; set; } = string.Empty;

            public int SEQ { get; set; } = 0;
            public int ListErrFlg { get; set; } = -1;

            public void Clear()
            {
                this.TargetFileDicid = string.Empty;
                this.TargetFileName = string.Empty;
                this.SEQ = -1;
                this.ListErrFlg = -1;
            }
        }

    }
}
