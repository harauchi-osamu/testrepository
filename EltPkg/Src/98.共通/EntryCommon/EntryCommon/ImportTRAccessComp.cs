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
    public class ImportTRAccessComp
    {
        /// <summary>バッチ番号</summary>
        private int _BatchNumber = 0;
        /// <summary>業務日付</summary>
        private int _GymDate = 0;
        /// <summary>対象バッチフォルダ名</summary>
        private string _BatchFolderName = string.Empty;
        /// <summary>銀行コード</summary>
        private int _BankCode = 0;
        /// <summary>端末番号</summary>
        private string _TerminalNumber = string.Empty;
        /// <summary>ユーザーID</summary>
        private string _UserID = string.Empty;
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
        public ImportTRAccessComp(int GymDate, int BatchNumber, string BatchFolderName, int BankCode, string TerminalNumber, string UserID, 
                                  List<TBL_CHANGE_DSPIDMF> ChgDspidMF, List<TBL_ITEM_MASTER> ItemMF, List<TBL_DSP_ITEM> dspitemMF, EntryReplacer entryReplacer)
        {
            _GymDate = GymDate;
            _BatchNumber = BatchNumber;
            _BatchFolderName = BatchFolderName;
            _BankCode = BankCode;
            _TerminalNumber = TerminalNumber;
            _UserID = UserID;
            _ChgDspidMF = ChgDspidMF;
            _itemMF = ItemMF;
            _entryReplacer = entryReplacer;
            _dspitemMF = dspitemMF;
        }

        #region データ登録処理

        /// <summary>
        /// バッチデータの登録処理
        /// </summary>
        public bool InsTRBatch(TBL_TRBATCH InsBatchData, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            return ImportTRAccessCommon.InsertTRBatchData(InsBatchData, dbp, Tran);
        }

        /// <summary>
        /// バッチデータの更新処理
        /// </summary>
        public bool UpdTRBatch(TBL_TRBATCH InsBatchData, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            return ImportTRAccessCommon.UpdateTRBatchData(InsBatchData, dbp, Tran);
        }

        /// <summary>
        /// バッチイメージデータの登録処理
        /// </summary>
        public bool InsTRBatchIMG(string TermIPAddress, ImportFileAccess.DetailCtl Ctl, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // イメージ登録データの作成
            foreach (ImportFileAccess.FileCtl Data in Ctl.FileList)
            {
                TBL_TRBATCHIMG IMGData = new TBL_TRBATCHIMG(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, (int)Data.IMG_KBNCD, _BankCode);
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
        public bool InsTRMei(string TermIPAddress, int DetailCnt, int Tegatashurui, string Memo, int DspIDDefault, out int insdspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 明細トランザクション登録データの作成
            TBL_TRMEI InsMeiData = new TBL_TRMEI(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, _BankCode);

            // 券種からdspID取得
            int dspID = ImportTRAccessCommon.GetDSPID(Tegatashurui, _ChgDspidMF);
            if (dspID < 0) dspID = DspIDDefault;
            insdspID = dspID;

            InsMeiData.m_DSP_ID = dspID;
            InsMeiData.m_BUA_DATE = 0;  // 二重持出通知日(持出)
            InsMeiData.m_GMA_DATE = 0;  // 証券データ訂正通知日(持出)
            InsMeiData.m_GRA_DATE = 0;  // 不渡返還通知日(持出)
            InsMeiData.m_GXA_DATE = 0;  // 決済後訂正通知日(持出)
            InsMeiData.m_MRA_DATE = 0;  // 金融機関読替情報変更通知日(持出銀行コード変更・継承銀行向け)(持出)
            InsMeiData.m_MRC_DATE = 0;  // 金融機関読替情報変更通知日(持帰銀行コード変更・持出銀行向け)(持出)
            InsMeiData.m_EDIT_FLG = 0;  // 編集フラグ
            InsMeiData.m_BCA_STS = 0;  // 持出取消アップロード状態
            InsMeiData.m_MEMO = Memo;  // メモ
            InsMeiData.m_DELETE_DATE = 0;  // 削除日
            InsMeiData.m_DELETE_FLG = 0;  // 削除フラグ

            return ImportTRAccessCommon.InsertTRMeiData(1, InsMeiData, dbp, Tran);
        }

        /// <summary>
        /// TRMEIIMGデータの登録処理
        /// </summary>
        public bool InsTRMeiImage(string TermIPAddress, int DetailCnt, ImportFileAccess.DetailCtl Ctl, int BUASts, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // イメージ登録データの作成
            foreach (ImportFileAccess.FileCtl Data in Ctl.FileList)
            {
                TBL_TRMEIIMG InsMeiIMGData = new TBL_TRMEIIMG(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, (int)Data.IMG_KBNCD, _BankCode);
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
                InsMeiIMGData.m_BUA_STS = BUASts;  // 持出アップロード状態
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
        /// TRMEIデータの登録処理
        /// 持出明細取込向け
        /// </summary>
        public bool InsTRMeiBQA(string TermIPAddress, int DetailCnt, int Tegatashurui, string Memo, int DspIDDefault, out int insdspID,
                                int BUADate, int GRADate, int DelDate, int DelFlg,
                                AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 明細トランザクション登録データの作成
            TBL_TRMEI InsMeiData = new TBL_TRMEI(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, _BankCode);

            // 券種からdspID取得
            int dspID = ImportTRAccessCommon.GetDSPID(Tegatashurui, _ChgDspidMF);
            if (dspID < 0) dspID = DspIDDefault;
            insdspID = dspID;

            InsMeiData.m_DSP_ID = dspID;
            InsMeiData.m_BUA_DATE = BUADate;  // 二重持出通知日(持出)
            InsMeiData.m_GMA_DATE = 0;  // 証券データ訂正通知日(持出)
            InsMeiData.m_GRA_DATE = GRADate;  // 不渡返還通知日(持出)
            InsMeiData.m_GXA_DATE = 0;  // 決済後訂正通知日(持出)
            InsMeiData.m_MRA_DATE = 0;  // 金融機関読替情報変更通知日(持出銀行コード変更・継承銀行向け)(持出)
            InsMeiData.m_MRC_DATE = 0;  // 金融機関読替情報変更通知日(持帰銀行コード変更・持出銀行向け)(持出)
            InsMeiData.m_EDIT_FLG = 0;  // 編集フラグ
            InsMeiData.m_BCA_STS = 0;  // 持出取消アップロード状態
            InsMeiData.m_MEMO = Memo;  // メモ
            InsMeiData.m_DELETE_DATE = DelDate;  // 削除日
            InsMeiData.m_DELETE_FLG = DelFlg;  // 削除フラグ

            return ImportTRAccessCommon.InsertTRMeiData(1, InsMeiData, dbp, Tran);
        }

        /// <summary>
        /// TRMEIIMGデータの登録処理
        /// 持出明細取込向け
        /// </summary>
        public bool InsTRMeiImageBQA(string TermIPAddress, int DetailCnt, ImportFileAccess.DetailCtl Ctl, 
                                     int BUASts, int DelDate, int DelFlg, 
                                     AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // イメージ登録データの作成
            foreach (ImportFileAccess.FileCtl Data in Ctl.FileList)
            {
                TBL_TRMEIIMG InsMeiIMGData = new TBL_TRMEIIMG(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, (int)Data.IMG_KBNCD, _BankCode);
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
                InsMeiIMGData.m_BUA_STS = BUASts;  // 持出アップロード状態
                InsMeiIMGData.m_BUA_DATE = 0;  // 持出日(持出)
                InsMeiIMGData.m_BUA_TIME = 0;  // 持出時間(持出)
                InsMeiIMGData.m_DELETE_DATE = DelDate;  // 削除日
                InsMeiIMGData.m_DELETE_FLG = DelFlg;  // 削除フラグ

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
        public bool InsTRHoseiData(string TermIPAddress, int DetailCnt, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 持帰銀行コード
            TBL_HOSEI_STATUS Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 1, _BankCode);
            Sts.m_INPT_STS = HoseiStatus.InputStatus.完了;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 交換希望日
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 2, _BankCode);
            Sts.m_INPT_STS = HoseiStatus.InputStatus.完了;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 金額
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 3, _BankCode);
            Sts.m_INPT_STS = HoseiStatus.InputStatus.完了;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            // 交換尻
            Sts = new TBL_HOSEI_STATUS(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 5, _BankCode);
            Sts.m_INPT_STS = HoseiStatus.InputStatus.完了;
            if (!ImportTRAccessCommon.InsertTRHoseiData(Sts, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクションデータの登録処理
        /// 期日管理向け
        /// </summary>
        public bool InsTRItemDataKijitu(string TermIPAddress, int DetailCnt, int ocr_icbank, int ocr_clearingdate, long ocr_amount, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 持帰銀行登録
            if (!InsTRItemDataBankKijitu(TermIPAddress, DetailCnt, ocr_icbank, dspID, dbp, Tran))
            {
                return false;
            }

            // 交換希望日登録
            if (!InsTRItemDataClearingDateKijitu(TermIPAddress, DetailCnt, ocr_clearingdate, dspID, dbp, Tran))
            {
                return false;
            }

            // 金額登録
            if (!InsTRItemDataAmtKijitu(TermIPAddress, DetailCnt, ocr_amount, dspID, dbp, Tran))
            {
                return false;
            }

            // 決済対象フラグ登録
            if (!InsTRItemDataPayKbnKijitu(TermIPAddress, DetailCnt, dspID, dbp, Tran))
            {
                return false;
            }

            // 最終項目登録
            if (!InsTRItemDataEnd(TermIPAddress, DetailCnt, "イメージ取込", dspID, dbp, Tran))
            {
                return false;
            }

            // その他項目登録
            if (!InsTRItemDataOther(TermIPAddress, DetailCnt, "イメージ取込", dspID, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクションデータの登録処理
        /// 持出明細取込向け
        /// </summary>
        public bool InsTRItemDataBQA(string TermIPAddress, int DetailCnt, int icbank, int clearingdate, long amount, string Paykbn, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 持帰銀行登録
            if (!InsTRItemDataBankBQA(TermIPAddress, DetailCnt, icbank, dspID, dbp, Tran))
            {
                return false;
            }

            // 交換希望日登録
            if (!InsTRItemDataClearingDateBQA(TermIPAddress, DetailCnt, clearingdate, dspID, dbp, Tran))
            {
                return false;
            }

            // 金額登録
            if (!InsTRItemDataAmtBQA(TermIPAddress, DetailCnt, amount, dspID, dbp, Tran))
            {
                return false;
            }

            // 決済対象フラグ登録
            if (!InsTRItemDataPayKbnBQA(TermIPAddress, DetailCnt, Paykbn, dspID, dbp, Tran))
            {
                return false;
            }

            // 最終項目登録
            if (!InsTRItemDataEnd(TermIPAddress, DetailCnt, "証券明細テキスト取込", dspID, dbp, Tran))
            {
                return false;
            }

            // その他項目登録
            if (!InsTRItemDataOther(TermIPAddress, DetailCnt, "証券明細テキスト取込", dspID, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 個別登録処理(持帰銀行)

        /// <summary>
        /// 期日管理
        /// 項目トランザクション登録(持帰銀行)
        /// </summary>
        private bool InsTRItemDataBankKijitu(string TermIPAddress, int DetailCnt, int ocr_icbank, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string Yomikaeicbank = string.Empty;
            string Yomikaebankname = string.Empty;

            // 券面持帰銀行コード
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 19, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            // 読替処理
            int BkNoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰銀行コード, Const.BANK_NO_LEN);
            Yomikaeicbank = _entryReplacer.GetYomikaeBkNo(ocr_icbank, BkNoLength);
            // 銀行名取得
            Yomikaebankname = _entryReplacer.GetBankName(int.Parse(Yomikaeicbank));
            // 設定
            Item.m_OCR_ENT_DATA = ocr_icbank.ToString(Const.BANK_NO_LEN_STR); // ＯＣＲ値（エントリ適用）
            Item.m_ENT_DATA = ocr_icbank.ToString(Const.BANK_NO_LEN_STR);     // エントリー値
            Item.m_END_DATA = ocr_icbank.ToString(Const.BANK_NO_LEN_STR);     // 最終確定値
            // OCR認識項目位置
            Item.m_ITEM_TOP = 0;
            Item.m_ITEM_LEFT = 0;
            Item.m_ITEM_WIDTH = 0;
            Item.m_ITEM_HEIGHT = 0;
            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰銀行コード
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 1, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            if (!string.IsNullOrEmpty(Yomikaebankname))
            {
                // 銀行マスタに存在すれば設定
                Item.m_OCR_ENT_DATA = Yomikaeicbank; // ＯＣＲ値（エントリ適用）
                Item.m_ENT_DATA = Yomikaeicbank;     // エントリー値
                Item.m_END_DATA = Yomikaeicbank;     // 最終確定値
            }
            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰銀行名
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 2, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_OCR_ENT_DATA = Yomikaebankname; // ＯＣＲ値（エントリ適用）
            Item.m_ENT_DATA = Yomikaebankname;    // エントリー値
            Item.m_END_DATA = Yomikaebankname;    // 最終確定値
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 証券明細テキスト取込
        /// 項目トランザクション登録(持帰銀行)
        /// </summary>
        private bool InsTRItemDataBankBQA(string TermIPAddress, int DetailCnt, int icbank, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string Yomikaeicbank = string.Empty;
            string Yomikaebankname = string.Empty;

            // 券面持帰銀行コード
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 19, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_ENT_DATA = icbank.ToString(Const.BANK_NO_LEN_STR);    // エントリー値
            Item.m_END_DATA = icbank.ToString(Const.BANK_NO_LEN_STR);    // 最終確定値
            Item.m_BUA_DATA = icbank.ToString(Const.BANK_NO_LEN_STR);    // 持出アップロード値
            Item.m_CTR_DATA = icbank.ToString(Const.BANK_NO_LEN_STR);    // 電子交換所結果値
            Item.m_FIX_TRIGGER = "証券明細テキスト取込"; // 修正トリガー
            // 読替処理
            int BkNoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰銀行コード, Const.BANK_NO_LEN);
            Yomikaeicbank = _entryReplacer.GetYomikaeBkNo(icbank, BkNoLength);
            // 銀行名取得
            Yomikaebankname = _entryReplacer.GetBankName(int.Parse(Yomikaeicbank));
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰銀行コード
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 1, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_ENT_DATA = Yomikaeicbank;    // エントリー値
            Item.m_END_DATA = Yomikaeicbank;    // 最終確定値
            Item.m_BUA_DATA = Yomikaeicbank;    // 持出アップロード値
            Item.m_CTR_DATA = Yomikaeicbank;    // 電子交換所結果値
            Item.m_FIX_TRIGGER = "証券明細テキスト取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 持帰銀行名
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 2, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_ENT_DATA = Yomikaebankname;    // エントリー値
            Item.m_END_DATA = Yomikaebankname;    // 最終確定値
            Item.m_BUA_DATA = Yomikaebankname;    // 持出アップロード値
            Item.m_CTR_DATA = Yomikaebankname;    // 電子交換所結果値
            Item.m_FIX_TRIGGER = "証券明細テキスト取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 個別登録処理(交換希望日)

        /// <summary>
        /// 期日管理
        /// 項目トランザクション登録(交換希望日)
        /// </summary>
        private bool InsTRItemDataClearingDateKijitu(string TermIPAddress, int DetailCnt, int ocr_clearingdate, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string inpClearingDate = string.Empty;
            string inpWarekiClearingDate = string.Empty;

            // 入力交換希望日
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 3, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー

            //交換日設定
            inpClearingDate = Calendar.GetSettleDay(ocr_clearingdate).ToString();
            //交換希望日和暦設定
            inpWarekiClearingDate = ImportTRAccessCommon.ConvWareki(ocr_clearingdate);
            // 日付チェック
            if (Calendar.IsDate(ocr_clearingdate.ToString()))
            {
                Item.m_OCR_ENT_DATA = ocr_clearingdate.ToString(); // ＯＣＲ値（エントリ適用）
                Item.m_ENT_DATA = ocr_clearingdate.ToString();    // エントリー値
                Item.m_END_DATA = ocr_clearingdate.ToString();    // 最終確定値
                                                                  // OCR認識項目位置
                Item.m_ITEM_TOP = 0;
                Item.m_ITEM_LEFT = 0;
                Item.m_ITEM_WIDTH = 0;
                Item.m_ITEM_HEIGHT = 0;
            }

            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 和暦交換希望日
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 4, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            Item.m_OCR_ENT_DATA = inpWarekiClearingDate; // ＯＣＲ値（エントリ適用）
            Item.m_ENT_DATA = inpWarekiClearingDate;    // エントリー値
            Item.m_END_DATA = inpWarekiClearingDate;    // 最終確定値
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 交換希望日
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 5, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_OCR_ENT_DATA = inpClearingDate; // ＯＣＲ値（エントリ適用）
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            Item.m_ENT_DATA = inpClearingDate;    // エントリー値
            Item.m_END_DATA = inpClearingDate;    // 最終確定値
            if (!ImportTRAccessCommon.InsertTRItemData(3, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 証券明細テキスト取込
        /// 項目トランザクション登録(交換希望日)
        /// </summary>
        private bool InsTRItemDataClearingDateBQA(string TermIPAddress, int DetailCnt, int clearingdate, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string inpClearingDate = string.Empty;
            string inpWarekiClearingDate = string.Empty;

            // 入力交換希望日
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 3, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_ENT_DATA = clearingdate.ToString();    // エントリー値
            Item.m_END_DATA = clearingdate.ToString();    // 最終確定値
            Item.m_BUA_DATA = clearingdate.ToString();    // 持出アップロード値
            Item.m_CTR_DATA = clearingdate.ToString();    // 電子交換所結果値
            Item.m_FIX_TRIGGER = "証券明細テキスト取込"; // 修正トリガー
            //交換日設定
            inpClearingDate = Calendar.GetSettleDay(clearingdate).ToString();
            //交換希望日和暦設定
            inpWarekiClearingDate = ImportTRAccessCommon.ConvWareki(clearingdate);
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 和暦交換希望日
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 4, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_ENT_DATA = inpWarekiClearingDate;    // エントリー値
            Item.m_END_DATA = inpWarekiClearingDate;    // 最終確定値
            Item.m_BUA_DATA = inpWarekiClearingDate;    // 持出アップロード値
            Item.m_CTR_DATA = inpWarekiClearingDate;    // 電子交換所結果値
            Item.m_FIX_TRIGGER = "証券明細テキスト取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            // 交換希望日
            Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 5, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_ENT_DATA = inpClearingDate;    // エントリー値
            Item.m_END_DATA = inpClearingDate;    // 最終確定値
            Item.m_BUA_DATA = inpClearingDate;    // 持出アップロード値
            Item.m_CTR_DATA = inpClearingDate;    // 電子交換所結果値
            Item.m_FIX_TRIGGER = "証券明細テキスト取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 個別登録処理(金額)

        /// <summary>
        /// 期日管理
        /// 項目トランザクション登録(金額)
        /// </summary>
        private bool InsTRItemDataAmtKijitu(string TermIPAddress, int DetailCnt, long ocr_amount, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {

            // 金額
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 6, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
            Item.m_OCR_ENT_DATA = ocr_amount.ToString(ImportTRAccessCommon.AMT_LEN); // ＯＣＲ値（エントリ適用）
            Item.m_ENT_DATA = ocr_amount.ToString(ImportTRAccessCommon.AMT_LEN);    // エントリー値
            Item.m_END_DATA = ocr_amount.ToString(ImportTRAccessCommon.AMT_LEN);    // 最終確定値
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
        /// 証券明細テキスト取込
        /// 項目トランザクション登録(金額)
        /// </summary>
        private bool InsTRItemDataAmtBQA(string TermIPAddress, int DetailCnt, long amount, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {

            // 金額
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 6, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_ENT_DATA = amount.ToString(ImportTRAccessCommon.AMT_LEN);    // エントリー値
            Item.m_END_DATA = amount.ToString(ImportTRAccessCommon.AMT_LEN);    // 最終確定値
            Item.m_BUA_DATA = amount.ToString(ImportTRAccessCommon.AMT_LEN);    // 持出アップロード値
            Item.m_CTR_DATA = amount.ToString(ImportTRAccessCommon.AMT_LEN);    // 電子交換所結果値
            Item.m_FIX_TRIGGER = "証券明細テキスト取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 個別登録処理(決済対象フラグ)

        /// <summary>
        /// 期日管理
        /// 項目トランザクション登録(決済対象フラグ)
        /// </summary>
        private bool InsTRItemDataPayKbnKijitu(string TermIPAddress, int DetailCnt, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 決済対象フラグ
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 7, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = "イメージ取込"; // 修正トリガー
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
        /// 証券明細テキスト取込
        /// 項目トランザクション登録(決済対象フラグ)
        /// </summary>
        private bool InsTRItemDataPayKbnBQA(string TermIPAddress, int DetailCnt, string Paykbn, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {

            // 決済対象フラグ
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 7, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_ENT_DATA = Paykbn;    // エントリー値
            Item.m_END_DATA = Paykbn;    // 最終確定値
            Item.m_BUA_DATA = Paykbn;    // 持出アップロード値
            Item.m_CTR_DATA = Paykbn;    // 電子交換所結果値
            Item.m_FIX_TRIGGER = "証券明細テキスト取込"; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(4, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 個別登録処理(その他)

        /// <summary>
        /// 項目トランザクション登録(最終項目)
        /// </summary>
        private bool InsTRItemDataEnd(string TermIPAddress, int DetailCnt, string TRIGGER, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 最終項目
            TBL_TRITEM Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, 999, _BankCode);
            Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
            Item.m_FIX_TRIGGER = TRIGGER; // 修正トリガー
            if (!ImportTRAccessCommon.InsertTRItemData(2, Item, dspID, _dspitemMF, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 項目トランザクション登録(その他)
        /// </summary>
        private bool InsTRItemDataOther(string TermIPAddress, int DetailCnt, string TRIGGER, int dspID, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            List<int> ex = new List<int> { DspItem.ItemId.持帰銀行コード, DspItem.ItemId.持帰銀行名, DspItem.ItemId.入力交換希望日,
                                           DspItem.ItemId.和暦交換希望日, DspItem.ItemId.交換日, DspItem.ItemId.金額, DspItem.ItemId.決済フラグ,
                                           DspItem.ItemId.券面持帰銀行コード, DspItem.ItemId.最終項目 };

            // 規定のデータ以外の定義があれば登録
            TBL_TRITEM Item;
            foreach (var ItemID in _dspitemMF.Where(x => x._GYM_ID == AppInfo.Setting.GymId && x._DSP_ID == dspID).Select(x => x._ITEM_ID).Except(ex))
            {
                Item = new TBL_TRITEM(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, DetailCnt, ItemID, _BankCode);
                Item.m_ITEM_NAME = ImportTRAccessCommon.GetTRItemName(Item._ITEM_ID, _itemMF);   // 項目名称
                Item.m_FIX_TRIGGER = TRIGGER; // 修正トリガー
                if (!ImportTRAccessCommon.InsertTRItemData(2, Item, dspID, _dspitemMF, dbp, Tran))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
