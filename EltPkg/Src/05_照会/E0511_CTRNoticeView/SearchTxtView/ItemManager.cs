using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.IO;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;

namespace SearchTxtView
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
        
        /// <summary>照会一覧データ</summary>
        public Dictionary<string, TBL_TSUCHITXT_CTL> TsuchiTxtCtl = null;
        /// <summary>明細一覧データ</summary>
        public Dictionary<string, TBL_TSUCHITXT> TsuchiTxt = null;
        /// <summary>イメージデータ</summary>
        public List<ImgData> ImageData { get; set; } = null;
        /// <summary>イメージ情報</summary>
        public Dictionary<int, ImageInfo> ImageInfos { get; set; } = null;

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }
        /// <summary>明細一覧パラメータ</summary>
        public DetailDispParams DetailParams { get; set; }
        /// <summary>イメージ明細画面パラメータ</summary>
        public ImgDispParams ImgParams { get; set; }
        /// <summary>ヘッダー情報</summary>
        public HeaderInfos HeaderInfo { get; set; }

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
            /// <summary>証券イメージファイル名</summary>
            IMG_NAME = 3,
            /// <summary>銀行コード訂正フラグ</summary>
            BK_NO_TEISEI_FLG = 4,
            /// <summary>訂正前銀行コード</summary>
            TEISEI_BEF_BK_NO = 5,
            /// <summary>訂正後銀行コード</summary>
            TEISEI_AFT_BK_NO = 6,
            /// <summary>交換希望日訂正フラグ</summary>
            CLEARING_TEISEI_FLG = 7,
            /// <summary>訂正前交換希望日</summary>
            TEISEI_BEF_CLEARING_DATE = 8,
            /// <summary>訂正後交換希望日</summary>
            TEISEI_CLEARING_DATE = 9,
            /// <summary>金額訂正フラグ</summary>
            AMOUNT_TEISEI_FLG = 10,
            /// <summary>訂正前金額</summary>
            TEISEI_BEF_AMOUNT = 11,
            /// <summary>訂正後金額</summary>
            TEISEI_AMOUNT = 12,
            /// <summary>二重持出イメージファイル名</summary>
            DUPLICATE_IMG_NAME = 13,
            /// <summary>不渡返還登録区分</summary>
            FUBI_REG_KBN = 14,
            /// <summary>不渡返還区分１</summary>
            FUBI_KBN_01 = 15,
            /// <summary>0号不渡事由コード１</summary>
            ZERO_FUBINO_01 = 16,
            /// <summary>不渡返還区分２</summary>
            FUBI_KBN_02 = 17,
            /// <summary>0号不渡事由コード２</summary>
            ZRO_FUBINO_02 = 18,
            /// <summary>不渡返還区分３</summary>
            FUBI_KBN_03 = 19,
            /// <summary>0号不渡事由コード３</summary>
            ZRO_FUBINO_03 = 20,
            /// <summary>不渡返還区分４</summary>
            FUBI_KBN_04 = 21,
            /// <summary>0号不渡事由コード４</summary>
            ZRO_FUBINO_04 = 22,
            /// <summary>不渡返還区分５</summary>
            FUBI_KBN_05 = 23,
            /// <summary>0号不渡事由コード５</summary>
            ZRO_FUBINO_05 = 24,
            /// <summary>逆交換対象フラグ</summary>
            REV_CLEARING_FLG = 25,
        }

        #endregion 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            DispParams = new DisplayParams();
            DetailParams = new DetailDispParams();
            ImgParams = new ImgDispParams();
            TsuchiTxtCtl = new Dictionary<string, TBL_TSUCHITXT_CTL>();
            TsuchiTxt = new Dictionary<string, TBL_TSUCHITXT>();
            ImageData = new List<ImgData>();
            this.HeaderInfo = new HeaderInfos();
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
            string strSQL = TBL_GENERALTEXTMF.GetSelectQueryTextKbn((int)TBL_GENERALTEXTMF.TextKbn.TSUCHITXT);
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
                        if (ctl._FILE_ID == FileParam.FileId.IF208)
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
        /// 通知テキスト管理一覧取得
        /// </summary>
        public bool FetchTsuchiCtlList(int ListDispLimit, out bool LimitOver, FormBase form = null)
        {
            // 初期化
            LimitOver = false;
            TsuchiTxtCtl = new Dictionary<string, TBL_TSUCHITXT_CTL>();
            // SELECT実行
            string strSQL = SQLSearch.GetSearchTsuchiTxtCtl(DispParams.Rdate, DispParams.FileDivid, AppInfo.Setting.SchemaBankCD, ListDispLimit);
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
                        TBL_TSUCHITXT_CTL txtctl = new TBL_TSUCHITXT_CTL(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        string key = txtctl._FILE_NAME;
                        TsuchiTxtCtl.Add(key, txtctl);
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
        /// 通知テキスト一覧取得
        /// </summary>
        public bool FetchTsuchiList(FormBase form = null)
        {
            // 初期化
            TsuchiTxt = new Dictionary<string, TBL_TSUCHITXT>();

            // SELECT実行
            string strSQL = TBL_TSUCHITXT.GetSelectQueryFileName(DetailParams.TargetFileName, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_TSUCHITXT txt = new TBL_TSUCHITXT(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey("|", txt._FILE_NAME, txt._RECORD_SEQ);
                        TsuchiTxt.Add(key, txt);
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
        /// 通知テキストのイメージファイル名からイメージ情報一覧取得
        /// </summary>
        public bool FetchImgList(FormBase form = null)
        {
            // 初期化
            ImageData = new List<ImgData>();

            //処理対象データ取得
            TBL_TSUCHITXT_CTL ctlparam = GetTsuchiTextControl();
            TBL_TSUCHITXT txtparam = GetTsuchiText();
            int GymID = 0;
            switch (ctlparam.m_FILE_DIVID)
            {
                case "BUA":
                case "GMA":
                case "GRA":
                case "GXA":
                case "MRA":
                case "MRC":
                    GymID = GymParam.GymId.持出;
                    break;
                case "BUB":
                case "BCA":
                case "GMB":
                case "GXB":
                case "MRB":
                case "MRD":
                    GymID = GymParam.GymId.持帰;
                    break;
                default:
                    return true;
            }

            // SELECT実行
            string strSQL = SQLSearch.GetTsuchiTxtImgData(GymID, txtparam.m_IMG_NAME, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        ImageData.Add(new ImgData(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
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

            //対象明細のイメージ一覧を設定
            foreach (ImgData data in ImageData)
            {
                if (ImageInfos.ContainsKey(data.trimg._IMG_KBN))
                {
                    ImageInfos[data.trimg._IMG_KBN].MeiImage = data.trimg;
                    ImageInfos[data.trimg._IMG_KBN].HasImage = true;
                    ImageInfos[data.trimg._IMG_KBN].ImgFolderPath = GetBankBacthFolder(data.trimg._GYM_ID, data.trimg._OPERATION_DATE, data.trimg._BAT_ID, data.InputRoute);
                }
            }
        }

        /// <summary>
        /// バッチフォルダパスを取得
        /// </summary>
        public string GetBankBacthFolder(int GymID, int opeDate, int BatID, int InputRoute)
        {
            string FolderPath = string.Empty;
            if (GymID == GymParam.GymId.持出)
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
            else
            {
                FolderPath = BankConfirmImageRoot();
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
        /// 持帰ダウンロード確定イメージルートフォルダパスを取得
        /// </summary>
        public string BankConfirmImageRoot()
        {
            return string.Format(NCR.Server.BankConfirmImageRoot, AppInfo.Setting.SchemaBankCD);
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
        public string GetGeneralTextData(DataNo no, int value, DispType Type)
        {
            return GetGeneralTextData(no, value.ToString(), Type);
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
                return list.First().m_DESCRIPTION;
            }
            else
            {
                return list.First().m_ABBREVIATE;
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
        /// 表示対象の通知テキスト管理取得
        /// </summary>
        public TBL_TSUCHITXT_CTL GetTsuchiTextControl()
        {
            if (!TsuchiTxtCtl.ContainsKey(DetailParams.TargetFileName))
            {
                return new TBL_TSUCHITXT_CTL(AppInfo.Setting.SchemaBankCD);
            }

            //対象データ取得
            return TsuchiTxtCtl[DetailParams.TargetFileName];
        }

        /// <summary>
        /// 表示対象の通知テキスト取得
        /// </summary>
        public TBL_TSUCHITXT GetTsuchiText()
        {
            if (!TsuchiTxt.ContainsKey(ImgParams.Key))
            {
                return new TBL_TSUCHITXT(AppInfo.Setting.SchemaBankCD);
            }

            //対象データ取得
            return TsuchiTxt[ImgParams.Key];
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            public int Rdate { get; set; } = -1;
            public string FileDivid { get; set; } = "";

            public void Clear()
            {
                this.Rdate = -1;
                this.FileDivid = string.Empty;
            }
        }

        /// <summary>
        /// 明細一覧画面パラメーター
        /// </summary>
        public class DetailDispParams
        {
            public string TargetFileName { get; set; } = string.Empty;

            public void Clear()
            {
                this.TargetFileName = string.Empty;
            }
        }

        /// <summary>
        /// イメージ明細画面パラメーター
        /// </summary>
        public class ImgDispParams
        {
            public string Key { get; set; } = string.Empty;
            public string TargetFileName { get; set; } = string.Empty;
            public int TargetSeq { get; set; } = 0;

            public void Clear()
            {
                this.Key = string.Empty;
                this.TargetFileName = string.Empty;
                this.TargetSeq = 0;
            }
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
        /// イメージデータ
        /// </summary>
        public class ImgData
        {
            public TBL_TRMEIIMG trimg { get; set; } = null;
            public int InputRoute { get; set; } = 0;

            public ImgData(DataRow dr, int Schemabankcd)
            {
                trimg = new TBL_TRMEIIMG(dr, Schemabankcd);
                InputRoute = DBConvert.ToIntNull(dr["INPUT_ROUTE"]);
            }
        }

        /// <summary>
        /// ヘッダー情報
        /// </summary>
        public class HeaderInfos
        {
            public bool IsAutoRefresh { get; set; } = false;
        }

    }
}
