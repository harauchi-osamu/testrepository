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

namespace CommonClass
{
    /// <summary>
    /// イメージ取込トランザクション操作共通
    /// </summary>
    public class ImportTRAccess
    {
        /// <summary>設定情報</summary>
        private ImportTRAccessCommon.OCRSettingData _SettingData = null;
        /// <summary>バッチ番号</summary>
        private int _BatchNumber = 0;
        /// <summary>バッチ票インプット表示データ</summary>
        private TBL_SCAN_BATCH_CTL _InputBatchData = null;
        /// <summary>業務日付</summary>
        private int _GymDate = 0;
        /// <summary>対象ルート</summary>
        private TBL_SCAN_BATCH_CTL.InputRoute _Route = TBL_SCAN_BATCH_CTL.InputRoute.Normal;
        /// <summary>対象バッチフォルダ名</summary>
        private string _BatchFolderName = string.Empty;
        /// <summary>端末番号</summary>
        private string _TerminalNumber = string.Empty;
        /// <summary>ユーザーID</summary>
        private string _UserID = string.Empty;
        /// <summary>OCR信頼度比較</summary>
        private int _OCRLevel = 0;
        /// <summary>画面ID変換マスタ</summary>
        private List<TBL_CHANGE_DSPIDMF> _ChgDspidMF = null;
        /// <summary>項目定義</summary>
        private List<TBL_ITEM_MASTER> _itemMF = null;
        /// <summary>読替処理クラス(銀行等スキーマ依存しない箇所は使用可能)</summary>
        private EntryReplacer _entryReplacer = null;
        /// <summary>画面項目定義</summary>
        private List<TBL_DSP_ITEM> _dspitemMF = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImportTRAccess(ImportTRAccessCommon.OCRSettingData SettingData, int GymDate, int BatchNumber, TBL_SCAN_BATCH_CTL.InputRoute Route, string BatchFolderName, 
                              TBL_SCAN_BATCH_CTL InputBatchData, string TerminalNumber, string UserID, int OCRLevel,
                              List<TBL_CHANGE_DSPIDMF> ChgDspidMF, List<TBL_ITEM_MASTER> ItemMF, List<TBL_DSP_ITEM> dspitemMF, EntryReplacer entryReplacer)
        {
            _SettingData = SettingData;
            _GymDate = GymDate;
            _BatchNumber = BatchNumber;
            _Route = Route;
            _BatchFolderName = BatchFolderName;
            _InputBatchData = InputBatchData;
            _TerminalNumber = TerminalNumber;
            _UserID = UserID;
            _OCRLevel = OCRLevel;
            _ChgDspidMF = ChgDspidMF;
            _itemMF = ItemMF;
            _entryReplacer = entryReplacer;
            _dspitemMF = dspitemMF;
        }

        #region データ登録処理

        /// <summary>
        /// バッチデータの登録処理
        /// </summary>
        public bool InsTRBatch(string TermIPAddress, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // バッチデータ登録データの作成
            TBL_TRBATCH InsBatchData = new TBL_TRBATCH(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, _InputBatchData.m_OC_BK_NO);
            InsBatchData.m_STS = 10;    // 状態
            InsBatchData.m_INPUT_ROUTE = (int)_Route;    // スキャン取込ルート
            InsBatchData.m_OC_BK_NO = _InputBatchData.m_OC_BK_NO;    // 持出銀行
            InsBatchData.m_OC_BR_NO = _InputBatchData.m_OC_BR_NO;    // 持出支店
            InsBatchData.m_SCAN_BR_NO = _InputBatchData.m_SCAN_BR_NO;    // スキャン支店
            InsBatchData.m_SCAN_DATE = _InputBatchData.m_SCAN_DATE;    // スキャン日
            InsBatchData.m_CLEARING_DATE = _InputBatchData.m_CLEARING_DATE;    // 交換希望日
            InsBatchData.m_SCAN_COUNT = _InputBatchData.m_SCAN_COUNT;    // スキャン枚数
            InsBatchData.m_TOTAL_COUNT = _InputBatchData.m_TOTAL_COUNT;    // 合計枚数
            InsBatchData.m_TOTAL_AMOUNT = _InputBatchData.m_TOTAL_AMOUNT;    // 合計金額
            InsBatchData.m_DELETE_DATE = 0;    // 削除日
            InsBatchData.m_DELETE_FLG = 0;    // 削除フラグ
            InsBatchData.m_E_TERM = _TerminalNumber.ToString();    // バッチ票入力端末番号
            InsBatchData.m_E_OPENO = _UserID;    // バッチ票入力オペレーター番号
            InsBatchData.m_E_YMD = _GymDate;    // バッチ票入力日付
            InsBatchData.m_E_TIME = int.Parse(DateTime.Now.ToString("HHmmss"));    // バッチ票入力時間

            return ImportTRAccessCommon.InsertTRBatchData(InsBatchData, dbp, Tran);
        }

        /// <summary>
        /// バッチデータの登録処理
        /// </summary>
        public bool InsTRBatchIMG(string TermIPAddress, ImportFileAccess.DetailCtl Ctl, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // イメージ登録データの作成
            foreach (ImportFileAccess.FileCtl Data in Ctl.FileList)
            {
                TBL_TRBATCHIMG IMGData = new TBL_TRBATCHIMG(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, (int)Data.IMG_KBNCD, _InputBatchData.m_OC_BK_NO);
                IMGData.m_IMG_FLNM = Data.NewFileName;  // 証券イメージファイル名
                IMGData.m_SCAN_BATCH_FOLDER_NAME = _BatchFolderName;  // スキャン連携時フォルダ名
                IMGData.m_IMG_FLNM_OLD = Data.OldFileName;  // スキャン連携時ファイル名
                if (!ImportTRAccessCommon.InsertTRBatchImageData(IMGData, dbp, Tran))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// TRMEIデータの登録処理
        /// </summary>
        public bool InsTRMei(string TermIPAddress, int DetailCnt, string FrontName, out int insdspID, List<TBL_OCR_DATA> OCRList, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string ocrvalue;
            int dspID = -1;

            // 明細トランザクション登録データの作成
            TBL_TRMEI InsMeiData = new TBL_TRMEI(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, _InputBatchData.m_OC_BK_NO);

            //画面番号 OCR認識値から算出した値
            // OCRデータ取得(交換券種)
            ocrvalue = GetMeiOCRData(_SettingData.BILL, OCRList, out TBL_OCR_DATA ocrdata);
            if (!string.IsNullOrEmpty(ocrvalue))
            {
                dspID = ImportTRAccessCommon.GetDSPID(int.Parse(ocrvalue), _ChgDspidMF);
            }
            if (dspID < 0) dspID = _SettingData.DspIDDefault;
            insdspID = dspID;

            // OCRデータ取得(メモ情報)
            string Memo =GetMeiOCRDataNonCheck(_SettingData.MEMO, OCRList);

            InsMeiData.m_DSP_ID = dspID;
            InsMeiData.m_MEMO = Memo;  // メモ情報
            InsMeiData.m_BUA_DATE = 0;  // 二重持出通知日(持出)
            InsMeiData.m_GMA_DATE = 0;  // 証券データ訂正通知日(持出)
            InsMeiData.m_GRA_DATE = 0;  // 不渡返還通知日(持出)
            InsMeiData.m_GXA_DATE = 0;  // 決済後訂正通知日(持出)
            InsMeiData.m_MRA_DATE = 0;  // 金融機関読替情報変更通知日(持出銀行コード変更・継承銀行向け)(持出)
            InsMeiData.m_MRC_DATE = 0;  // 金融機関読替情報変更通知日(持帰銀行コード変更・持出銀行向け)(持出)
            InsMeiData.m_EDIT_FLG = 0;  // 編集フラグ
            InsMeiData.m_BCA_STS = 0;  // 持出取消アップロード状態
            InsMeiData.m_DELETE_DATE = 0;  // 削除日
            InsMeiData.m_DELETE_FLG = 0;  // 削除フラグ

            return ImportTRAccessCommon.InsertTRMeiData(1, InsMeiData, dbp, Tran);
        }

        /// <summary>
        /// TRMEIIMGデータの登録処理
        /// </summary>
        public bool InsTRMeiImage(string TermIPAddress, int DetailCnt, ImportFileAccess.DetailCtl Ctl, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // イメージ登録データの作成
            foreach (ImportFileAccess.FileCtl Data in Ctl.FileList)
            {
                TBL_TRMEIIMG InsMeiIMGData = new TBL_TRMEIIMG(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, (int)Data.IMG_KBNCD, _InputBatchData.m_OC_BK_NO);
                InsMeiIMGData.m_IMG_FLNM = Data.NewFileName;  // 証券イメージファイル名
                InsMeiIMGData.m_IMG_FLNM_OLD = Data.OldFileName;  // スキャン連携時ファイル名
                InsMeiIMGData.m_OC_OC_BK_NO = Data.OC_BKNO;  // 持出銀行コード
                InsMeiIMGData.m_OC_OC_BR_NO = Data.OC_BRNO;  // 持出支店コード
                InsMeiIMGData.m_OC_IC_BK_NO = Data.IC_BKNO;  // 持帰銀行コード
                InsMeiIMGData.m_OC_OC_DATE = Data.OC_DATE;  // 持出日
                InsMeiIMGData.m_OC_CLEARING_DATE = Data.CLEARING_DATE;  // 交換希望日
                InsMeiIMGData.m_OC_AMOUNT = Data.AMOUNT;  // 金額
                InsMeiIMGData.m_PAY_KBN = Data.PAY_KBN;  // 決済対象区分
                InsMeiIMGData.m_UNIQUE_CODE = Data.UNIQUECODE;  // 一意コード
                InsMeiIMGData.m_FILE_EXTENSION = Data.EXTENSION;  // 拡張子
                InsMeiIMGData.m_BUA_STS = 0;  // 持出アップロード状態
                InsMeiIMGData.m_BUA_DATE = 0;  // 持出日(持出)
                InsMeiIMGData.m_BUA_TIME = 0;  // 持出時間(持出)
                InsMeiIMGData.m_DELETE_DATE = 0;  // 削除日
                InsMeiIMGData.m_DELETE_FLG = 0;  // 削除フラグ

                if (!ImportTRAccessCommon.InsertTRMeiImageData(1, InsMeiIMGData, dbp, Tran))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// HOSEISTATUSデータの登録処理
        /// </summary>
        public bool InsTRHoseiData(string TermIPAddress, int DetailCnt, bool BankComp, bool AmtComp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 持帰銀行コード
            TBL_HOSEI_STATUS Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 1, _InputBatchData.m_OC_BK_NO);
            Sts.m_INPT_STS = BankComp ? HoseiStatus.InputStatus.完了 : HoseiStatus.InputStatus.エントリ待;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 交換希望日
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 2, _InputBatchData.m_OC_BK_NO);
            Sts.m_INPT_STS = HoseiStatus.InputStatus.完了;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 金額
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 3, _InputBatchData.m_OC_BK_NO);
            Sts.m_INPT_STS = AmtComp ? HoseiStatus.InputStatus.完了 : HoseiStatus.InputStatus.エントリ待;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 交換尻
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 5, _InputBatchData.m_OC_BK_NO);
            Sts.m_INPT_STS = HoseiStatus.InputStatus.完了;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクションデータの登録処理
        /// </summary>
        public bool InsTRItemData(string TermIPAddress, int DetailCnt, string FrontName, int dspID, List<TBL_OCR_DATA> OCRList, out bool BankComp, out bool AmtComp, 
                                  AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 初期化
            BankComp = false;
            AmtComp = false;

            // 持帰銀行登録
            if (!InsTRItemDataBank(TermIPAddress, DetailCnt, FrontName, dspID, OCRList, ref BankComp, dbp, Tran))
            {
                return false;
            }

            // 交換希望日登録
            if (!InsTRItemDataClearingDate(TermIPAddress, DetailCnt, FrontName, dspID, OCRList, dbp, Tran))
            {
                return false;
            }

            // 金額登録
            if (!InsTRItemDataAmt(TermIPAddress, DetailCnt, FrontName, dspID, OCRList, ref AmtComp, dbp, Tran))
            {
                return false;
            }

            // 決済対象フラグ登録
            if (!InsTRItemDataPayKbn(TermIPAddress, DetailCnt, FrontName, dspID, OCRList, dbp, Tran))
            {
                return false;
            }

            // 最終項目登録
            if (!InsTRItemDataEnd(TermIPAddress, DetailCnt, FrontName, dspID, OCRList, dbp, Tran))
            {
                return false;
            }

            // その他項目登録
            if (!InsTRItemDataOther(TermIPAddress, DetailCnt, FrontName, dspID, OCRList, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(持帰銀行)
        /// </summary>
        private bool InsTRItemDataBank(string TermIPAddress, int DetailCnt, string FrontName, int dspID, List<TBL_OCR_DATA> OCRList, ref bool BankComp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            int InsertType = 1;
            string ocrvalue;
            TBL_OCR_DATA ocrdata;
            string Yomikaeicbank = string.Empty;
            string Yomikaebankname = string.Empty;
            BankComp = false;

            // 券面持帰銀行コード
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 19, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            // OCRデータ取得
            ocrvalue = GetMeiOCRData(_SettingData.IC_BK_NO, OCRList, out ocrdata, _OCRLevel);
            if (!string.IsNullOrEmpty(ocrvalue))
            {
                //読替・銀行名取得

                // 読替処理
                int BkNoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰銀行コード, Const.BANK_NO_LEN);
                Yomikaeicbank = _entryReplacer.GetYomikaeBkNo(int.Parse(ocrvalue), BkNoLength);
                // 銀行名取得
                Yomikaebankname = _entryReplacer.GetKameiBankName(int.Parse(Yomikaeicbank));
                //設定
                Item.m_OCR_ENT_DATA = int.Parse(ocrvalue).ToString(Const.BANK_NO_LEN_STR); // ＯＣＲ値（エントリ適用）

                if (ocrdata.m_CONFIDENCE >= _SettingData.OCRLevel && !string.IsNullOrEmpty(Yomikaebankname))
                {
                    //読替処理が問題なく行われて
                    //信頼度が閾値以上の場合、補正済設定
                    Item.m_ENT_DATA = Item.m_OCR_ENT_DATA;    // エントリー値
                    Item.m_END_DATA = Item.m_OCR_ENT_DATA;    // 最終確定値
                    InsertType = 3;
                    BankComp = true;
                }
            }
            if (ocrdata != null)
            {
                // OCR認識項目位置
                Item.m_ITEM_TOP = ocrdata.m_ITEM_TOP;
                Item.m_ITEM_LEFT = ocrdata.m_ITEM_LEFT;
                Item.m_ITEM_WIDTH = ocrdata.m_ITEM_WIDTH;
                Item.m_ITEM_HEIGHT = ocrdata.m_ITEM_HEIGHT;
            }
            if (!ImportTRAccessCommon.InsertTRItemData(InsertType, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰銀行コード
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 1, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            if (!string.IsNullOrEmpty(Yomikaebankname))
            {
                // 銀行マスタに存在すれば設定
                Item.m_OCR_ENT_DATA = Yomikaeicbank; // ＯＣＲ値（エントリ適用）
                if (BankComp)
                {
                    //補正済の場合
                    Item.m_ENT_DATA = Item.m_OCR_ENT_DATA;    // エントリー値
                    Item.m_END_DATA = Item.m_OCR_ENT_DATA;    // 最終確定値
                }
            }
            if (!ImportTRAccessCommon.InsertTRItemData(InsertType, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰銀行名
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 2, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_OCR_ENT_DATA = Yomikaebankname; // ＯＣＲ値（エントリ適用）
            if (BankComp)
            {
                //補正済の場合
                Item.m_ENT_DATA = Item.m_OCR_ENT_DATA;    // エントリー値
                Item.m_END_DATA = Item.m_OCR_ENT_DATA;    // 最終確定値
            }
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(InsertType, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(交換希望日)
        /// </summary>
        private bool InsTRItemDataClearingDate(string TermIPAddress, int DetailCnt, string FrontName, int dspID, List<TBL_OCR_DATA> OCRList, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string ocrvalue;
            TBL_OCR_DATA ocrdata;
            string inpClearingDate = string.Empty;
            string inpWarekiClearingDate = string.Empty;
            string ocrClearingDate = string.Empty;
            string ocrWarekiClearingDate = string.Empty;

            // 入力交換希望日
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 3, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // ＯＣＲ値（エントリ適用）
            Item.m_ENT_DATA = _InputBatchData.m_CLEARING_DATE.ToString();    // エントリー値
            Item.m_END_DATA = _InputBatchData.m_CLEARING_DATE.ToString();    // 最終確定値

            //交換日設定
            inpClearingDate = Calendar.GetSettleDay(_InputBatchData.m_CLEARING_DATE).ToString();
            //交換希望日和暦設定
            inpWarekiClearingDate = ImportTRAccessCommon.ConvWareki(_InputBatchData.m_CLEARING_DATE);

            // OCRデータ取得
            ocrvalue = GetMeiOCRData(_SettingData.CLEARING_DATE, OCRList, out ocrdata, _OCRLevel);
            if (!string.IsNullOrEmpty(ocrvalue))
            {
                // 日付チェック
                if (Calendar.IsDate(ocrvalue))
                {
                    //ocr交換日設定
                    ocrClearingDate = Calendar.GetSettleDay(int.Parse(ocrvalue)).ToString();
                    //ocr交換希望日和暦設定
                    ocrWarekiClearingDate = ImportTRAccessCommon.ConvWareki(int.Parse(ocrvalue));

                    Item.m_OCR_ENT_DATA = ocrvalue; // ＯＣＲ値（エントリ適用）
                }
            }
            if (ocrdata != null)
            {
                // OCR認識項目位置
                Item.m_ITEM_TOP = ocrdata.m_ITEM_TOP;
                Item.m_ITEM_LEFT = ocrdata.m_ITEM_LEFT;
                Item.m_ITEM_WIDTH = ocrdata.m_ITEM_WIDTH;
                Item.m_ITEM_HEIGHT = ocrdata.m_ITEM_HEIGHT;
            }
            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 和暦交換希望日
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 4, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            Item.m_ENT_DATA = inpWarekiClearingDate;    // エントリー値
            Item.m_END_DATA = inpWarekiClearingDate;    // 最終確定値
            Item.m_OCR_ENT_DATA = ocrWarekiClearingDate; // ＯＣＲ値（エントリ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 交換希望日
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 5, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            Item.m_ENT_DATA = inpClearingDate;    // エントリー値
            Item.m_END_DATA = inpClearingDate;    // 最終確定値
            Item.m_OCR_ENT_DATA = ocrClearingDate; // ＯＣＲ値（エントリ適用）
            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(金額)
        /// </summary>
        private bool InsTRItemDataAmt(string TermIPAddress, int DetailCnt, string FrontName, int dspID, List<TBL_OCR_DATA> OCRList, ref bool AmtComp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            int InsertType = 1;
            string ocrvalue;
            TBL_OCR_DATA ocrdata;
            AmtComp = false;

            // 金額
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 6, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            // OCRデータ取得
            ocrvalue = GetMeiOCRData(_SettingData.AMOUNT, OCRList, out ocrdata, _OCRLevel);
            if (!string.IsNullOrEmpty(ocrvalue))
            {
                // 空以外であれば設定
                Item.m_OCR_ENT_DATA = long.Parse(ocrvalue).ToString(ImportTRAccessCommon.AMT_LEN); // ＯＣＲ値（エントリ適用）

                if (ocrdata.m_CONFIDENCE >= _SettingData.OCRLevel && long.Parse(ocrvalue) > 0)
                {
                    //値が「0」より大きく
                    //信頼度が閾値以上の場合、補正済設定
                    Item.m_ENT_DATA = Item.m_OCR_ENT_DATA;    // エントリー値
                    Item.m_END_DATA = Item.m_OCR_ENT_DATA;    // 最終確定値
                    InsertType = 3;
                    AmtComp = true;
                }
            }
            if (ocrdata != null)
            {
                // OCR認識項目位置
                Item.m_ITEM_TOP = ocrdata.m_ITEM_TOP;
                Item.m_ITEM_LEFT = ocrdata.m_ITEM_LEFT;
                Item.m_ITEM_WIDTH = ocrdata.m_ITEM_WIDTH;
                Item.m_ITEM_HEIGHT = ocrdata.m_ITEM_HEIGHT;
            }
            if (!ImportTRAccessCommon.InsertTRItemData(InsertType, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(決済対象フラグ)
        /// </summary>
        private bool InsTRItemDataPayKbn(string TermIPAddress, int DetailCnt, string FrontName, int dspID, List<TBL_OCR_DATA> OCRList, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 決済対象フラグ
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 7, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // ＯＣＲ値（エントリ適用）
            Item.m_ENT_DATA = "0";    // エントリー値
            Item.m_END_DATA = "0";    // 最終確定値
            Item.m_OCR_ENT_DATA = "0"; // ＯＣＲ値（エントリ適用）
            // OCR認識項目位置
            Item.m_ITEM_TOP = 0;
            Item.m_ITEM_LEFT = 0;
            Item.m_ITEM_WIDTH = 0;
            Item.m_ITEM_HEIGHT = 0;
            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(最終項目)
        /// </summary>
        private bool InsTRItemDataEnd(string TermIPAddress, int DetailCnt, string FrontName, int dspID, List<TBL_OCR_DATA> OCRList, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 最終項目
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 999, _InputBatchData.m_OC_BK_NO);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(2, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(その他)
        /// </summary>
        private bool InsTRItemDataOther(string TermIPAddress, int DetailCnt, string FrontName, int dspID, List<TBL_OCR_DATA> OCRList, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            List<int> ex = new List<int> { DspItem.ItemId.持帰銀行コード, DspItem.ItemId.持帰銀行名, DspItem.ItemId.入力交換希望日,
                                           DspItem.ItemId.和暦交換希望日, DspItem.ItemId.交換日, DspItem.ItemId.金額, DspItem.ItemId.決済フラグ,
                                           DspItem.ItemId.券面持帰銀行コード, DspItem.ItemId.最終項目 };

            // 規定のデータ以外の定義があれば登録
            TBL_TRITEM Item;
            foreach (var ItemID in _dspitemMF.Where(x => x._GYM_ID == AppInfo.Setting.GymId && x._DSP_ID == dspID).Select(x => x._ITEM_ID).Except(ex))
            {
                Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, ItemID, _InputBatchData.m_OC_BK_NO);
                Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
                Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
                if (!ImportTRAccessCommon.InsertTRItemData(2, Item, dspID, _dspitemMF, dbp, Tran))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 共通

        /// <summary>
        /// 証券OCRデータ取得
        /// </summary>
        public static string GetMeiOCRData(string field_name, List<TBL_OCR_DATA> OcrList, out TBL_OCR_DATA ocrdata, int OCRLevel = -999)
        {
            ocrdata = null;

            IEnumerable<TBL_OCR_DATA> Data = OcrList.Where(x => x._FIELD_NAME == field_name);
            if (Data.Count() == 0) return string.Empty;

            ocrdata = Data.First();
            TBL_DSP_ITEM dsp = new TBL_DSP_ITEM(0);
            dsp.m_ITEM_TYPE = DspItem.ItemType.N;

            if (ocrdata.m_CONFIDENCE < OCRLevel)
            {
                //規定値未満の場合
                return string.Empty;
            }

            return CommonUtil.GetOcrValue(dsp, ocrdata.m_OCR);
        }
        /// <summary>
        /// 証券OCRデータ取得(TBL_DSP_ITEMチェックなし)
        /// </summary>
        public static string GetMeiOCRDataNonCheck(string field_name, List<TBL_OCR_DATA> OcrList)
        {
            TBL_OCR_DATA ocrdata = null;
            IEnumerable<TBL_OCR_DATA> Data = OcrList.Where(x => x._FIELD_NAME == field_name);
            if (Data.Count() == 0) return string.Empty;
            ocrdata = Data.First();
            return ocrdata.m_OCR;
        }
        #endregion

    }
}
