using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Reflection;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;
using IFImportCommon;

namespace InclearingConfirm
{
    /// <summary>
    /// 持帰ダウンロード確定トランザクション操作共通
    /// </summary>
    public class ImportTRAccessDownLoad
    {
        /// <summary>証券種類デフォルト</summary>
        private const int BILLCD_DEF = 999;

        /// <summary>設定ファイル情報</summary>
        public SettingData _SettingData { get; private set; } = null;
        /// <summary>バッチ番号</summary>
        private int _BatchNumber = 0;
        /// <summary>画面ID変換マスタ</summary>
        private List<TBL_CHANGE_DSPIDMF> _ChgDspidMF = null;
        /// <summary>項目定義</summary>
        private List<TBL_ITEM_MASTER> _itemMF = null;
        /// <summary>読替処理クラス</summary>
        private EntryReplacer _entryReplacer = null;
        /// <summary>補正モード画面項目定義</summary>
        private List<TBL_HOSEIMODE_DSP_ITEM> _hoseidispitemMF = null;
        /// <summary>画面項目定義</summary>
        private List<TBL_DSP_ITEM> _dspitemMF = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImportTRAccessDownLoad(SettingData SettingData, int BatchNumber, 
                                      List<TBL_CHANGE_DSPIDMF> ChgDspidMF, List<TBL_ITEM_MASTER> ItemMF, EntryReplacer entryReplacer, List<TBL_HOSEIMODE_DSP_ITEM> hoseidispitemMF, 
                                      List<TBL_DSP_ITEM> dspitemMF)
        {
            _SettingData = SettingData;
            _BatchNumber = BatchNumber;
            _ChgDspidMF = ChgDspidMF;
            _itemMF = ItemMF;
            _entryReplacer = entryReplacer;
            _hoseidispitemMF = hoseidispitemMF;
            _dspitemMF = dspitemMF;
        }

        #region データ登録処理

        /// <summary>
        /// TRMEIデータの登録処理
        /// </summary>
        public bool InsTRMei(int OpeDate, string TermIPAddress, int DetailCnt, string BillCode, 
                             string OCBKNo, string OldOCBKNo, int MRBDate, int BUBDate, int BCADate, int DelDate, int DelFlg,
                             out int insdspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 明細トランザクション登録データの作成
            TBL_TRMEI InsMeiData = new TBL_TRMEI(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, AppInfo.Setting.SchemaBankCD);

            // 交換証券種類からDSPID取得
            ChgBillCodeToDspID(BillCode, out insdspID);

            // 二重持出・持出取消・金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け)関連は引数から設定
            InsMeiData.m_DSP_ID = insdspID; //画面番号
            int.TryParse(OCBKNo, out int intOCBKNo);
            InsMeiData.m_IC_OC_BK_NO = intOCBKNo;  // 持出銀行(持帰)
            int.TryParse(OldOCBKNo, out int intOldOCBKNo);
            InsMeiData.m_IC_OLD_OC_BK_NO = intOldOCBKNo;  // 旧持出銀行(持帰)
            InsMeiData.m_BUB_DATE = BUBDate;  // 二重持出通知日(持帰)
            InsMeiData.m_BCA_DATE = BCADate;  // 持出取消通知日(持帰)
            InsMeiData.m_GMB_DATE = 0;  // 証券データ訂正通知日(持帰)
            InsMeiData.m_GXB_DATE = 0;  // 決済後訂正通知日(持帰)
            InsMeiData.m_MRB_DATE = MRBDate;  // 金融機関読替情報変更通知日(持出銀行コード変更・持帰銀行向け)(持帰)
            InsMeiData.m_MRD_DATE = 0;  // 金融機関読替情報変更通知日(持帰銀行コード変更・継承銀行向け)(持帰)
            InsMeiData.m_YCA_MARK = 0;  // 判定不可(持帰)
            InsMeiData.m_EDIT_FLG = 0;  // 編集フラグ
            InsMeiData.m_GMA_STS = 0;  // 持帰訂正データアップロード状態
            InsMeiData.m_GRA_STS = 0;  // 不渡返還登録アップロード状態
            InsMeiData.m_DELETE_DATE = DelDate;  // 削除日
            InsMeiData.m_DELETE_FLG = DelFlg;  // 削除フラグ

            return ImportTRAccessCommon.InsertTRMeiData(2, InsMeiData, dbp, Tran);
        }

        /// <summary>
        /// TRMEIデータの更新処理
        /// </summary>
        /// <remarks>未使用</remarks>
        public bool UpdTRMei(int OpeDate, string TermIPAddress, int DetailCnt, string BillCode, out int upddspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 明細トランザクション更新データの作成
            TBL_TRMEI UpdMeiData = new TBL_TRMEI(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, AppInfo.Setting.SchemaBankCD);

            // 交換証券種類からDSPID取得
            ChgBillCodeToDspID(BillCode, out upddspID);

            UpdMeiData.m_DSP_ID = upddspID; //画面番号

            // DSPIDの更新
            return ImportTRAccessCommon.UpdateTRMeiData(UpdMeiData, AppInfo.Setting.SchemaBankCD, dbp, Tran);
        }

        /// <summary>
        /// TRMEIIMGデータの登録処理
        /// </summary>
        public bool InsTRMeiImage(int OpeDate, string TermIPAddress, int DetailCnt, int ImgKbnCD, string ImgFileName, string ArchName, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            TBL_TRMEIIMG InsMeiIMGData = new TBL_TRMEIIMG(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, ImgKbnCD, AppInfo.Setting.SchemaBankCD);
            InsMeiIMGData.m_IMG_FLNM = ImgFileName;  // 証券イメージファイル名
            InsMeiIMGData.m_OC_OC_BK_NO = ImgFileName.Substring(0, 4);  // 持出銀行コード
            InsMeiIMGData.m_OC_OC_BR_NO = ImgFileName.Substring(4, 4);  // 持出支店コード
            InsMeiIMGData.m_OC_IC_BK_NO = ImgFileName.Substring(8, 4);  // 持帰銀行コード
            InsMeiIMGData.m_OC_OC_DATE = ImgFileName.Substring(12, 8);   // 持出日
            InsMeiIMGData.m_OC_CLEARING_DATE = ImgFileName.Substring(20, 8);   // 交換希望日
            InsMeiIMGData.m_OC_AMOUNT = ImgFileName.Substring(28, 12);   // 金額
            InsMeiIMGData.m_PAY_KBN = ImgFileName.Substring(40, 1);   // 決済対象区分
            InsMeiIMGData.m_UNIQUE_CODE = ImgFileName.Substring(41, 15);   // 一意となるコード
            InsMeiIMGData.m_FILE_EXTENSION = ".jpg";   // 拡張子
            InsMeiIMGData.m_GDA_DATE = AplInfo.OpDate();   // 持帰取込日
            InsMeiIMGData.m_GDA_TIME = int.Parse(DateTime.Now.ToString("HHmmss"));   // 持帰取込時間
            InsMeiIMGData.m_IMG_ARCH_NAME = ArchName;   // イメージアーカイブファイル名
            InsMeiIMGData.m_DELETE_DATE = 0;  // 削除日
            InsMeiIMGData.m_DELETE_FLG = 0;  // 削除フラグ
            if (!ImportTRAccessCommon.InsertTRMeiImageData(2, InsMeiIMGData, dbp, Tran))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// HOSEISTATUSデータの登録処理
        /// </summary>
        public bool InsTRHoseiData(int OpeDate, string TermIPAddress, int DetailCnt, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 持帰銀行コード
            TBL_HOSEI_STATUS Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, 1, AppInfo.Setting.SchemaBankCD);
            Sts.m_INPT_STS = 1000;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 交換希望日
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, 2, AppInfo.Setting.SchemaBankCD);
            Sts.m_INPT_STS = 1000;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 金額
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, 3, AppInfo.Setting.SchemaBankCD);
            Sts.m_INPT_STS = 1000;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 自行情報
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, 4, AppInfo.Setting.SchemaBankCD);
            Sts.m_INPT_STS = 1000;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 交換尻
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, 5, AppInfo.Setting.SchemaBankCD);
            Sts.m_INPT_STS = 3000;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクションデータの登録処理
        /// </summary>
        public bool InsTRItemData(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                  List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                  AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {

            // 持帰銀行登録
            if(!InsTRItemDataBank(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // 交換希望日登録
            if (!InsTRItemDataClearingDate(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // 金額登録
            if (!InsTRItemDataAmt(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // 決済対象フラグ
            if (!InsTRItemDataPayKbn(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // 交換証券種類登録
            if (!InsTRItemDataBillCode(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // 手形種類登録
            if (!InsTRItemDataSyuruiCode(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // 持帰支店・口座番号登録
            if (!InsTRItemDataBrNoAccount(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // 手形番号登録
            if (!InsTRItemDataBillNo(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // 最終項目登録
            if (!InsTRItemDataEnd(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            // その他項目登録
            if (!InsTRItemDataOther(OpeDate, TermIPAddress, DetailCnt, Data, dspID, OutOcrList, InOcrList, txtRd, pkgRd, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 個別登録処理

        /// <summary>
        /// 項目トランザクション登録(持帰銀行)
        /// </summary>
        private bool InsTRItemDataBank(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                       List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                       AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string CTRICBKNo = string.Empty;
            string ENTICBKNo = string.Empty;
            string VFYICBKNo = string.Empty;

            string ENTYomikaeICBKNo = string.Empty;
            string VFYYomikaeICBKNo = string.Empty;
            string ENTYomikaeBankName = string.Empty;
            string VFYYomikaeBankName = string.Empty;

            // 交換尻(持帰銀行コード)
            GetDataKoukanjiri(Data, "持帰銀行コード", "", _SettingData.OCRSettingData.IC_BK_NO,
                              out CTRICBKNo, out ENTICBKNo, out VFYICBKNo,
                              OutOcrList, InOcrList, txtRd, pkgRd);

            // 電子交換所値整形
            if (int.TryParse(CTRICBKNo, out int intCTRICBKNo))
            {
                CTRICBKNo = intCTRICBKNo.ToString(Const.BANK_NO_LEN_STR);
            }

            // 券面持帰銀行コード
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.券面持帰銀行コード, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRICBKNo;   //電子交換所結果値

            //読替・銀行名取得
            if (int.TryParse(ENTICBKNo, out int intENTICBKNo))
            {
                // 読替処理
                ENTYomikaeICBKNo = _entryReplacer.GetYomikaeBkNo(intENTICBKNo, Const.BANK_NO_LEN);
                // 銀行名取得
                ENTYomikaeBankName = _entryReplacer.GetBankName(int.Parse(ENTYomikaeICBKNo));
                // 項目設定
                Item.m_OCR_ENT_DATA = intENTICBKNo.ToString(Const.BANK_NO_LEN_STR); // ＯＣＲ値（エントリ適用）
            }
            if (int.TryParse(VFYICBKNo, out int intVFYICBKNo))
            {
                // 読替処理
                VFYYomikaeICBKNo = _entryReplacer.GetYomikaeBkNo(intVFYICBKNo, Const.BANK_NO_LEN);
                // 銀行名取得
                VFYYomikaeBankName = _entryReplacer.GetBankName(int.Parse(VFYYomikaeICBKNo));
                // 項目設定
                Item.m_OCR_VFY_DATA = intVFYICBKNo.ToString(Const.BANK_NO_LEN_STR); // ＯＣＲ値（ベリファイ適用）
            }
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰銀行コード
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.持帰銀行コード, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_CTR_DATA = CTRICBKNo;   //電子交換所結果値
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            if (!string.IsNullOrEmpty(ENTYomikaeBankName))
            {
                // 銀行マスタに存在すれば設定
                Item.m_OCR_ENT_DATA = ENTYomikaeICBKNo; // ＯＣＲ値（エントリ適用）
            }
            if (!string.IsNullOrEmpty(VFYYomikaeBankName))
            {
                // 銀行マスタに存在すれば設定
                Item.m_OCR_VFY_DATA = VFYYomikaeICBKNo; // ＯＣＲ値（ベリファイ適用）
            }
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰銀行名
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.持帰銀行名, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_CTR_DATA = "";   //電子交換所結果値
            Item.m_OCR_ENT_DATA = ENTYomikaeBankName; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = VFYYomikaeBankName; // ＯＣＲ値（ベリファイ適用）
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(交換希望日)
        /// </summary>
        private bool InsTRItemDataClearingDate(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                               List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                               AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string CTRClearingDate = string.Empty;
            string ENTClearingDate = string.Empty;
            string VFYClearingDate = string.Empty;
            string CTRDate = string.Empty;
            string CTRWarekiDate = string.Empty;
            string ENTDate = string.Empty;
            string ENTWarekiDate = string.Empty;
            string VFYDate = string.Empty;
            string VFYWarekiDate = string.Empty;

            // 交換尻(交換日)
            GetDataKoukanjiri(Data, "交換日", "", _SettingData.OCRSettingData.CLEARING_DATE,
                              out CTRClearingDate, out ENTClearingDate, out VFYClearingDate,
                              OutOcrList, InOcrList, txtRd, pkgRd);

            // 入力交換希望日
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.入力交換希望日, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRClearingDate;   //電子交換所結果値

            //交換日・交換希望日和暦取得
            if (Calendar.IsDate(CTRClearingDate) && int.TryParse(CTRClearingDate, out int intCTRClearingDate))
            {
                //交換日設定
                CTRDate = Calendar.GetSettleDay(intCTRClearingDate).ToString();
                //交換希望日和暦設定
                CTRWarekiDate = ImportTRAccessCommon.ConvWareki(intCTRClearingDate);
            }
            if (Calendar.IsDate(ENTClearingDate) && int.TryParse(ENTClearingDate, out int intENTClearingDate))
            {
                //交換日設定
                ENTDate = Calendar.GetSettleDay(intENTClearingDate).ToString();
                //交換希望日和暦設定
                ENTWarekiDate = ImportTRAccessCommon.ConvWareki(intENTClearingDate);

                Item.m_OCR_ENT_DATA = intENTClearingDate.ToString(); // ＯＣＲ値（エントリ適用）
            }
            if (Calendar.IsDate(VFYClearingDate) && int.TryParse(VFYClearingDate, out int intVFYClearingDate))
            {
                //交換日設定
                VFYDate = Calendar.GetSettleDay(intVFYClearingDate).ToString();
                //交換希望日和暦設定
                VFYWarekiDate = ImportTRAccessCommon.ConvWareki(intVFYClearingDate);

                Item.m_OCR_VFY_DATA = intVFYClearingDate.ToString(); // ＯＣＲ値（ベリファイ適用）
            }
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 和暦交換希望日
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.和暦交換希望日, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRWarekiDate;   //電子交換所結果値
            Item.m_OCR_ENT_DATA = ENTWarekiDate; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = VFYWarekiDate; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 交換日
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.交換日, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRDate;   //電子交換所結果値
            Item.m_OCR_ENT_DATA = ENTDate; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = VFYDate; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(金額)
        /// </summary>
        private bool InsTRItemDataAmt(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                      List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                      AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string CTRAmt = string.Empty;
            string ENTAmt = string.Empty;
            string VFYAmt = string.Empty;

            // 交換尻(金額)
            GetDataKoukanjiri(Data, "金額", "", _SettingData.OCRSettingData.AMOUNT,
                              out CTRAmt, out ENTAmt, out VFYAmt,
                              OutOcrList, InOcrList, txtRd, pkgRd);

            // 電子交換所値整形
            if (long.TryParse(CTRAmt, out long intCTRAmt))
            {
                CTRAmt = intCTRAmt.ToString(ImportTRAccessCommon.AMT_LEN);
            }

            // 金額
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.金額, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRAmt;   //電子交換所結果値
            if (long.TryParse(ENTAmt, out long intENTAmt))
            {
                Item.m_OCR_ENT_DATA = intENTAmt.ToString(ImportTRAccessCommon.AMT_LEN); // ＯＣＲ値（エントリ適用）
            }
            if (long.TryParse(VFYAmt, out long intVFYAmt))
            {
                Item.m_OCR_VFY_DATA = intVFYAmt.ToString(ImportTRAccessCommon.AMT_LEN); // ＯＣＲ値（ベリファイ適用）
            }
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(決済対象フラグ)
        /// </summary>
        private bool InsTRItemDataPayKbn(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                         List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                         AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string PayKbn = string.Empty;

            // 自行情報(決済対象フラグ)
            PayKbn = txtRd.GetText(Data.LineData, "決済対象フラグ");

            // 決済対象フラグ(取得データ無しの場合は空設定)
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.決済フラグ, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称

            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = PayKbn;   //電子交換所結果値
            Item.m_OCR_ENT_DATA = PayKbn; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = PayKbn; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(交換証券種類)
        /// </summary>
        private bool InsTRItemDataBillCode(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                           List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                           AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string CTRBillCode = string.Empty;
            string IDToBillCode = string.Empty;
            string IDToBillName = string.Empty;

            // 自行情報(交換証券種類コード)
            CTRBillCode = txtRd.GetText(Data.LineData, "交換証券種類コード");

            // 電子交換所値整形
            if (int.TryParse(CTRBillCode, out int intCTRBillCode))
            {
                CTRBillCode = intCTRBillCode.ToString(Const.BILL_CD_LEN_STR);
            }

            // DSPIDから交換証券種類を取得
            ChgDspIDToBillCode(dspID, out IDToBillCode, out IDToBillName);

            // 交換証券種類コード
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.交換証券種類コード, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRBillCode;   //電子交換所結果値
            Item.m_OCR_ENT_DATA = IDToBillCode; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = IDToBillCode; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 交換証券種類名
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.交換証券種類名, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = "";   //電子交換所結果値
            Item.m_OCR_ENT_DATA = IDToBillName; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = IDToBillName; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(種類コード)
        /// </summary>
        private bool InsTRItemDataSyuruiCode(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                             List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                             AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string CTRSyuruiCode = string.Empty;
            string ENTSyuruiCode = string.Empty;
            string VFYSyuruiCode = string.Empty;
            string ENTSyuruiName = string.Empty;
            string VFYSyuruiName = string.Empty;

            // 自行情報(種類コード)
            GetDataJikou(Data, "種類コード", _SettingData.QRSyuruiCode, "", _SettingData.OCRSettingData.BILL_CODE,
                         out CTRSyuruiCode, out ENTSyuruiCode, out VFYSyuruiCode,
                         OutOcrList, InOcrList, txtRd, pkgRd);

            // 電子交換所値整形
            if (int.TryParse(CTRSyuruiCode, out int intCTRSyuruiCode))
            {
                CTRSyuruiCode = intCTRSyuruiCode.ToString(Const.SYURUI_CD_LEN_STR);
            }

            // 手形種類コード
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.手形種類コード, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRSyuruiCode;   //電子交換所結果値

            //種類名取得
            if (int.TryParse(ENTSyuruiCode, out int intENTSyuruiCode))
            {
                ENTSyuruiName = _entryReplacer.GetTegataName(intENTSyuruiCode);
                Item.m_OCR_ENT_DATA = CommonUtil.PadLeft(intENTSyuruiCode.ToString(), Const.SYURUI_CD_LEN, "0"); // ＯＣＲ値（エントリ適用）
            }
            if (int.TryParse(VFYSyuruiCode, out int intVFYSyuruiCode))
            {
                VFYSyuruiName = _entryReplacer.GetTegataName(intVFYSyuruiCode);
                Item.m_OCR_VFY_DATA = CommonUtil.PadLeft(intVFYSyuruiCode.ToString(), Const.SYURUI_CD_LEN, "0"); // ＯＣＲ値（ベリファイ適用）
            }
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 手形種類名
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.手形種類名, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = "";   //電子交換所結果値
            Item.m_OCR_ENT_DATA = ENTSyuruiName; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = VFYSyuruiName; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(持帰支店・口座番号)
        /// </summary>
        private bool InsTRItemDataBrNoAccount(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                              List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                              AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string CTRICBrNo = string.Empty;
            string ENTICBrNo = string.Empty;
            string VFYICBrNo = string.Empty;
            string CTRAccount = string.Empty;
            string ENTAccount = string.Empty;
            string VFYAccount = string.Empty;
            string ENTYomikaeICBrNo = string.Empty;
            string ENTYomikaeICBrName = string.Empty;
            string VFYYomikaeICBrNo = string.Empty;
            string VFYYomikaeICBrName = string.Empty;

            // 自行情報(持帰支店コード)
            GetDataJikou(Data, "持帰支店コード", "", "", _SettingData.OCRSettingData.IC_BR_NO,
                         out CTRICBrNo, out ENTICBrNo, out VFYICBrNo,
                         OutOcrList, InOcrList, txtRd, pkgRd);

            // 自行情報(口座番号)
            GetDataJikou(Data, "口座番号", "", "", _SettingData.OCRSettingData.ACCOUNT,
                         out CTRAccount, out ENTAccount, out VFYAccount,
                         OutOcrList, InOcrList, txtRd, pkgRd);

            // 電子交換所値整形(持帰支店)
            if (int.TryParse(CTRICBrNo, out int intCTRICBrNo))
            {
                CTRICBrNo = intCTRICBrNo.ToString(Const.BR_NO_LEN_STR);
            }

            // 券面持帰支店コード
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.券面持帰支店コード, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRICBrNo;   //電子交換所結果値

            //持帰支店取得
            if (int.TryParse(ENTICBrNo, out int intENTICBrNo))
            {
                // 読替マスタから新支店番号を取得
                ENTYomikaeICBrNo = _entryReplacer.GetYomikaeBrNo(intENTICBrNo, DBConvert.ToLongNull(ENTAccount), Const.BR_NO_LEN);
                // 新支店名を取得
                ENTYomikaeICBrName = _entryReplacer.GetBranchName(DBConvert.ToIntNull(ENTYomikaeICBrNo));

                Item.m_OCR_ENT_DATA = intENTICBrNo.ToString(Const.BR_NO_LEN_STR); // ＯＣＲ値（エントリ適用）
            }
            if (int.TryParse(VFYICBrNo, out int intVFYICBrNo))
            {
                // 読替マスタから新支店番号を取得
                VFYYomikaeICBrNo = _entryReplacer.GetYomikaeBrNo(intVFYICBrNo, DBConvert.ToLongNull(VFYAccount), Const.BR_NO_LEN);
                // 新支店名を取得
                VFYYomikaeICBrName = _entryReplacer.GetBranchName(DBConvert.ToIntNull(VFYYomikaeICBrNo));

                Item.m_OCR_VFY_DATA = intVFYICBrNo.ToString(Const.BR_NO_LEN_STR); // ＯＣＲ値（ベリファイ適用）
            }
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰支店コード
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.持帰支店コード, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRICBrNo;   //電子交換所結果値
            Item.m_OCR_ENT_DATA = ENTYomikaeICBrNo; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = VFYYomikaeICBrNo; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰支店名
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.持帰支店名, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = "";   //電子交換所結果値
            Item.m_OCR_ENT_DATA = ENTYomikaeICBrName; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = VFYYomikaeICBrName; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            string ENTYomikaeAccount = string.Empty;
            string ENTYomikaePayer = string.Empty;
            string VFYYomikaeAccount = string.Empty;
            string VFYYomikaePayer = string.Empty;

            // 電子交換所値整形(口座番号)
            if (long.TryParse(CTRAccount, out long intCTRAccount))
            {
                CTRAccount = intCTRAccount.ToString(Const.KOZA_NO_LEN_STR);
            }

            // 券面口座番号
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.券面口座番号, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRAccount;   //電子交換所結果値

            //口座番号・支払人名取得
            if (long.TryParse(ENTAccount, out long intENTAccount))
            {
                // 読替マスタから新口座番号を取得
                ENTYomikaeAccount = _entryReplacer.GetYomikaeKozaName(intENTICBrNo, intENTAccount, Const.KOZA_NO_LEN);
                // 支払人を取得
                ENTYomikaePayer = _entryReplacer.GetPayerName(DBConvert.ToIntNull(ENTYomikaeICBrNo), DBConvert.ToLongNull(ENTYomikaeAccount));
                Item.m_OCR_ENT_DATA = CommonUtil.PadLeft(intENTAccount.ToString(), Const.KOZA_NO_LEN, "0"); // ＯＣＲ値（エントリ適用）
            }
            if (long.TryParse(VFYAccount, out long intVFYAccount))
            {
                // 読替マスタから新口座番号を取得
                VFYYomikaeAccount = _entryReplacer.GetYomikaeKozaName(intVFYICBrNo, intVFYAccount, Const.KOZA_NO_LEN);
                // 支払人を取得
                VFYYomikaePayer = _entryReplacer.GetPayerName(DBConvert.ToIntNull(VFYYomikaeICBrNo), DBConvert.ToLongNull(VFYYomikaeAccount));
                Item.m_OCR_VFY_DATA = CommonUtil.PadLeft(intVFYAccount.ToString(), Const.KOZA_NO_LEN, "0"); // ＯＣＲ値（ベリファイ適用）
            }
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 口座番号
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.口座番号, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRAccount;   //電子交換所結果値
            Item.m_OCR_ENT_DATA = ENTYomikaeAccount; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = VFYYomikaeAccount; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 支払人名
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.支払人名, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = "";   //電子交換所結果値
            Item.m_OCR_ENT_DATA = ENTYomikaePayer; // ＯＣＲ値（エントリ適用）
            Item.m_OCR_VFY_DATA = VFYYomikaePayer; // ＯＣＲ値（ベリファイ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(手形番号)
        /// </summary>
        private bool InsTRItemDataBillNo(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID, 
                                         List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                         AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string CTRBillNo = string.Empty;
            string ENTBillNo = string.Empty;
            string VFYBillNo = string.Empty;

            // 自行情報(手形番号)
            GetDataJikou(Data, "手形番号", _SettingData.QRBillNo, _SettingData.BillNo_RegexPattern, _SettingData.OCRSettingData.BILL_NO,
                         out CTRBillNo, out ENTBillNo, out VFYBillNo,
                         OutOcrList, InOcrList, txtRd, pkgRd);

            // 電子交換所値整形(手形番号)
            if (long.TryParse(CTRBillNo, out long intCTRBillNo))
            {
                CTRBillNo = intCTRBillNo.ToString(Const.TEGATA_NO_LEN_STR);
            }

            // 手形番号
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.手形番号, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            Item.m_CTR_DATA = CTRBillNo;   //電子交換所結果値

            if (long.TryParse(ENTBillNo, out long intENTBillNo))
            {
                Item.m_OCR_ENT_DATA = CommonUtil.PadLeft(intENTBillNo.ToString(), Const.TEGATA_NO_LEN, "0"); // ＯＣＲ値（エントリ適用）
            }
            if (long.TryParse(VFYBillNo, out long intVFYBillNo))
            {
                Item.m_OCR_VFY_DATA = CommonUtil.PadLeft(intVFYBillNo.ToString(), Const.TEGATA_NO_LEN, "0"); // ＯＣＲ値（ベリファイ適用）
            }
            if (!ImportTRAccessCommon.InsertTRItemData(5, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(最終項目)
        /// </summary>
        private bool InsTRItemDataEnd(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                      List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                      AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
           // 最終項目
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, DspItem.ItemId.最終項目, AppInfo.Setting.SchemaBankCD);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(2, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(その他)
        /// </summary>
        private bool InsTRItemDataOther(int OpeDate, string TermIPAddress, int DetailCnt, ItemManager.ConfirmData Data, int dspID,
                                        List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd,
                                        AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            List<int> ex = new List<int> { DspItem.ItemId.持帰銀行コード, DspItem.ItemId.持帰銀行名, DspItem.ItemId.入力交換希望日,
                                           DspItem.ItemId.和暦交換希望日, DspItem.ItemId.交換日, DspItem.ItemId.金額, DspItem.ItemId.決済フラグ,
                                           DspItem.ItemId.交換証券種類コード, DspItem.ItemId.交換証券種類名, DspItem.ItemId.手形種類コード,
                                           DspItem.ItemId.手形種類名, DspItem.ItemId.券面持帰支店コード, DspItem.ItemId.持帰支店コード,
                                           DspItem.ItemId.持帰支店名, DspItem.ItemId.券面口座番号, DspItem.ItemId.口座番号, DspItem.ItemId.支払人名,
                                           DspItem.ItemId.手形番号, DspItem.ItemId.券面持帰銀行コード, DspItem.ItemId.最終項目 };

            // 規定のデータ以外の定義があれば登録
            TBL_TRITEM Item;
            foreach (var ItemID in _dspitemMF.Where(x => x._GYM_ID == AppInfo.Setting.GymId && x._DSP_ID == dspID).Select(x => x._ITEM_ID).Except(ex))
            {
                Item = new TBL_TRITEM(AppInfo.Setting.GymId, OpeDate, TermIPAddress, _BatchNumber, DetailCnt, ItemID, AppInfo.Setting.SchemaBankCD);
                Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
                Item.m_FIX_TRIGGER = "持帰ダウンロード確定"; // 修正トリガー
                if (!ImportTRAccessCommon.InsertTRItemData(2, Item, dspID, _dspitemMF, dbp, Tran))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 対象データ取得処理

        /// <summary>
        /// 対象データ取得(交換尻)
        /// </summary>
        private void GetDataKoukanjiri(ItemManager.ConfirmData FrontData, string ItemName, string CutPosQR, string ocrFieldName,
                                       out string CTRData, out string ENTData, out string VFYData,
                                       List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd)
        {
            Dictionary<CTRRdCommon.CutDataType, string> CutPos = new Dictionary<CTRRdCommon.CutDataType, string>();
            CutPos.Add(CTRRdCommon.CutDataType.QR, CutPosQR);
            CutPos.Add(CTRRdCommon.CutDataType.TXTBILLNO, ""); // 交換尻では使用しないため空を設定

            //ファイル名の持出銀行取得
            string FILE_OCBKNo = FrontData.LineData[TBL_ICREQRET_BILLMEITXT.FILE_OC_BK_NO];
            //取込区分
            string CAPKBN = FrontData.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN];

            // 電子交換所値取得
            CTRData = txtRd.GetTextCut(FrontData.LineData, ItemName, CutPos);

            // ＯＣＲ値（エントリ）
            ENTData = string.Empty;
            if (CAPKBN == "0")
            {
                //電子交換所
                ENTData = pkgRd.GetTextCut(FrontData.LineData, ItemName, CTRData, CutPos);
            }
            else
            {
                //行内連携
                ENTData = CTRData;
            }

            // ＯＣＲ値（ベリファイ）
            VFYData = string.Empty;
            if (CAPKBN == "0")
            {
                //電子交換所

                //持帰のOCR情報から取得
                VFYData = GetMeiOCRData(ocrFieldName, InOcrList, out TBL_OCR_DATA ocrdata);
                if (ocrdata == null || ocrdata.m_CONFIDENCE < NCR.Server.IC_OCRLevel)
                {
                    //規定値未満の場合
                    VFYData = string.Empty;
                }
            }
            else
            {
                //行内連携

                //持出のOCR情報から取得
                VFYData = GetMeiOCRData(ocrFieldName, OutOcrList, out TBL_OCR_DATA ocrdata);
                if (ocrdata == null || ocrdata.m_CONFIDENCE < _SettingData.OCOCRLevel)
                {
                    //持出の信頼度が規定値未満の場合
                    VFYData = string.Empty;
                }
                if (string.IsNullOrEmpty(VFYData))
                {
                    // 持出OCR情報から取得不可(持出のデータなし OR 信頼度が規定未満)

                    //持帰のOCR情報から取得
                    VFYData = GetMeiOCRData(ocrFieldName, InOcrList, out ocrdata);
                    if (ocrdata == null || ocrdata.m_CONFIDENCE < NCR.Server.IC_OCRLevel)
                    {
                        //規定値未満の場合
                        VFYData = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// 対象データ取得(自行)
        /// </summary>
        private void GetDataJikou(ItemManager.ConfirmData FrontData, string ItemName, string CutPosQR, string BillNoPattern, string ocrFieldName,
                                  out string CTRData, out string ENTData, out string VFYData,
                                  List<TBL_OCR_DATA> OutOcrList, List<TBL_OCR_DATA> InOcrList, CTRTxtRd txtRd, CTRPkgRd pkgRd)
        {
            Dictionary<CTRRdCommon.CutDataType, string> CutPos = new Dictionary<CTRRdCommon.CutDataType, string>();
            CutPos.Add(CTRRdCommon.CutDataType.QR, CutPosQR);
            CutPos.Add(CTRRdCommon.CutDataType.TXTBILLNO, BillNoPattern);

            //ファイル名の持出銀行取得
            string FILE_OCBKNo = FrontData.LineData[TBL_ICREQRET_BILLMEITXT.FILE_OC_BK_NO];
            //取込区分
            string CAPKBN = FrontData.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN];

            // 電子交換所値取得
            CTRData = txtRd.GetTextCut(FrontData.LineData, ItemName, CutPos);

            // ＯＣＲ値（エントリ）
            ENTData = string.Empty;
            if (CAPKBN == "0")
            {
                //電子交換所
                ENTData = pkgRd.GetTextCut(FrontData.LineData, ItemName, CTRData, CutPos);
            }
            else
            {
                //行内連携

                //持出のOCR情報から取得
                ENTData = GetMeiOCRData(ocrFieldName, OutOcrList, out TBL_OCR_DATA ocrdata);
                if (ocrdata == null || ocrdata.m_CONFIDENCE < _SettingData.OCOCRLevel)
                {
                    //持出の信頼度が規定値未満の場合
                    ENTData = string.Empty;
                }
            }

            // ＯＣＲ値（ベリファイ）
            VFYData = string.Empty;
            if (CAPKBN == "0")
            {
                //電子交換所

                //持帰のOCR情報から取得
                VFYData = GetMeiOCRData(ocrFieldName, InOcrList, out TBL_OCR_DATA ocrdata);
                if (ocrdata == null || ocrdata.m_CONFIDENCE < NCR.Server.IC_OCRLevel)
                {
                    //規定値未満の場合
                    VFYData = string.Empty;
                }
            }
            else
            {
                //行内連携

                //持出のOCR情報から取得
                VFYData = GetMeiOCRData(ocrFieldName, OutOcrList, out TBL_OCR_DATA ocrdata);
                if (ocrdata == null || ocrdata.m_CONFIDENCE < _SettingData.OCOCRLevel)
                {
                    //持出の信頼度が規定値未満の場合
                    VFYData = string.Empty;
                }
                if (string.IsNullOrEmpty(VFYData))
                {
                    // 持出OCR情報から取得不可(持出のデータなし OR 信頼度が規定未満)

                    //持帰のOCR情報から取得
                    VFYData = GetMeiOCRData(ocrFieldName, InOcrList, out ocrdata);
                    if (ocrdata == null || ocrdata.m_CONFIDENCE < NCR.Server.IC_OCRLevel)
                    {
                        //規定値未満の場合
                        VFYData = string.Empty;
                    }
                }
            }
        }

        #endregion

        #region 共通

        /// <summary>
        /// 証券OCRデータ取得
        /// </summary>
        public static string GetMeiOCRData(string field_name, List<TBL_OCR_DATA> OcrList, out TBL_OCR_DATA ocrdata)
        {
            ocrdata = null;

            IEnumerable<TBL_OCR_DATA> Data = OcrList.Where(x => x._FIELD_NAME == field_name);
            if (Data.Count() == 0) return string.Empty;

            ocrdata = Data.First();
            TBL_DSP_ITEM dsp = new TBL_DSP_ITEM(0);
            dsp.m_ITEM_TYPE = DspItem.ItemType.N;

            return CommonUtil.GetOcrValue(dsp, ocrdata.m_OCR);
        }

        /////// <summary>
        /////// 口座番号等の登録が必要か確認
        /////// </summary>
        ////public bool ChkTegataBillCode(int gymid, int dspid)
        ////{
        ////    IEnumerable<TBL_HOSEIMODE_DSP_ITEM> Data = _hoseidispitemMF.Where(x => x._GYM_ID == gymid && x._DSP_ID == dspid && x._HOSEI_ITEMMODE  == HoseiStatus.HoseiInputMode.自行情報);
        ////    // 券面口座番号があるかで確認
        ////    return (Data.Where(x => x._ITEM_ID == DspItem.ItemId.券面口座番号).Count() > 0);
        ////}

        /// <summary>
        /// 証券種類コードの存在チェック
        /// </summary>
        /// <param name="billcd"></param>
        /// <returns></returns>
        public bool ExistsBillCode(int billcd)
        {
            return _entryReplacer.mst_bills.ContainsKey(billcd);
        }

        /// <summary>
        /// 交換証券種類の整形
        /// </summary>
        private int FormatBillCode(string BillCode)
        {
            if (!(int.TryParse(BillCode, out int intBillCode) && ExistsBillCode(intBillCode)))
            {
                // BILLMFに存在しないコードの場合、初期値を設定
                intBillCode = BILLCD_DEF;
            }

            return intBillCode;
        }

        /// <summary>
        /// 交換証券種類からDSPIDへの変換処理
        /// </summary>
        private void ChgBillCodeToDspID(string BillCode, out int dspID)
        {
            // 交換証券種類の整形
            int intBillCode = FormatBillCode(BillCode);
            //DSPID変換処理
            dspID = ImportTRAccessCommon.GetDSPID(intBillCode, _ChgDspidMF);
            if (dspID < 0)
            {
                //変換マスタに存在しない場合は[999]
                dspID = BILLCD_DEF;
            }
        }

        /// <summary>
        /// DSPIDから交換証券種類への変換処理
        /// </summary>
        private void ChgDspIDToBillCode(int dspID, out string BillCode, out string BillName)
        {
            // DSPIDから交換証券種類を取得
            int intBillCd = _entryReplacer.GetDspIDBillCode(dspID);
            if (intBillCd < 0)
            {
                // 変換できない場合、[999]を設定
                intBillCd = BILLCD_DEF;
            }
            intBillCd = FormatBillCode(intBillCd.ToString());
            BillCode = intBillCd.ToString(Const.BILL_CD_LEN_STR);

            // 交換証券種類名
            BillName = _entryReplacer.GetBillName(intBillCd);
        }

        #endregion

    }
}
