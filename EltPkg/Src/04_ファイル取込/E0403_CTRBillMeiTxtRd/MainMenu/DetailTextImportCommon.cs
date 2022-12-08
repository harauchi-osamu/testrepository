using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Common;
using CommonTable.DB;
using EntryCommon;
using IFFileOperation;
using CommonClass;

namespace MainMenu
{
    class DetailTextImportCommon
    {

        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private IFFileDataLoad _LoadFile = null;
        #endregion

        public IFData _HeaderData
        {
            get { return _LoadFile.LoadData.Where(x => x.KBN == "1").First(); }
        }
        public IFData _PayData
        {
            get { return _LoadFile.LoadData.Where(x => x.KBN == "8" && x.LineData["集計区分"] == "0").First(); }
        }
        public IFData _NonPayData
        {
            get { return _LoadFile.LoadData.Where(x => x.KBN == "8" && x.LineData["集計区分"] == "1").First(); }
        }
        public IFData _OtherData
        {
            get { return _LoadFile.LoadData.Where(x => x.KBN == "8" && x.LineData["集計区分"] == "Z").First(); }
        }
        public IEnumerable<IFData> _DataRecord
        {
            get { return _LoadFile.LoadData.Where(x => x.KBN == "2"); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DetailTextImportCommon(ControllerBase ctl, IFFileDataLoad LoadFile)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
            _LoadFile = LoadFile;

            // 休日マスタ取得
            iBicsCalendar cal = new iBicsCalendar();
            cal.SetHolidays();
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 証券明細テキスト管理登録
        /// </summary>
        public bool ImportBillMeiTxtCtl(int CreateDateDiff, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 作成日処理
            int MKCreateDate = int.Parse(_HeaderData.LineData["作成日"]);
            if (CreateDateDiff != 0)
            {
                // Zero以外の場合は作成日から指定営業日加減算した値を設定
                iBicsCalendar cal = new iBicsCalendar();
                MKCreateDate = cal.getBusinessday(MKCreateDate, CreateDateDiff);
            }

            TBL_BILLMEITXT_CTL TxtCtl = new TBL_BILLMEITXT_CTL(_itemMgr._TargetFilename, AppInfo.Setting.SchemaBankCD);
            TxtCtl.m_FILE_ID = _HeaderData.LineData["ファイルID"];
            TxtCtl.m_FILE_DIVID = _HeaderData.LineData["ファイル識別区分"];
            TxtCtl.m_BK_NO = _HeaderData.LineData["銀行コード"];
            TxtCtl.m_CREATE_DATE = MKCreateDate;
            TxtCtl.m_SEND_SEQ = _HeaderData.LineData["送信一連番号"];
            TxtCtl.m_PAY_RECORD_COUNT = int.Parse(_PayData.LineData["レコード件数"]);
            TxtCtl.m_PAY_TOTAL_AMOUNT = long.Parse(_PayData.LineData["合計金額"]);
            TxtCtl.m_PAY_TOTAL_COUNT = int.Parse(_PayData.LineData["合計枚数"]);
            TxtCtl.m_NONEPAY_RECORD_COUNT = int.Parse(_NonPayData.LineData["レコード件数"]);
            TxtCtl.m_NONEPAY_TOTAL_AMOUNT = long.Parse(_NonPayData.LineData["合計金額"]);
            TxtCtl.m_NONEPAY_TOTAL_COUNT = int.Parse(_NonPayData.LineData["合計枚数"]);
            TxtCtl.m_OTHER_FRONT_COUNT = int.Parse(_OtherData.LineData["レコード件数"]);

            return _itemMgr.InsertBillMeiTxtCtl(TxtCtl, dbp, Tran);
        }

        /// <summary>
        /// 証券明細テキスト登録
        /// </summary>
        public bool ImportBillMeiTxt(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            foreach (IFData Data in _DataRecord)
            {
                TBL_BILLMEITXT Txt = new TBL_BILLMEITXT(_itemMgr._TargetFilename, Data.LineData["証券イメージファイル名"], AppInfo.Setting.SchemaBankCD);
                Txt.m_FRONT_IMG_NAME = Data.LineData["表証券イメージファイル名"];
                Txt.m_IMG_KBN = int.Parse(Data.LineData["表・裏等の別"]);
                Txt.m_FILE_OC_BK_NO = Data.LineData["ファイル名持出銀行コード"];
                Txt.m_CHG_OC_BK_NO = Data.LineData["読替持出銀行コード"];
                Txt.m_OC_BR_NO = Data.LineData["持出支店コード"];
                Txt.m_OC_DATE = int.Parse(Data.LineData["持出日"]);
                Txt.m_OC_METHOD = Data.LineData["持出時接続方式"];
                Txt.m_OC_USERID = Data.LineData["ユーザID(持出者)"];
                Txt.m_PAY_KBN = Data.LineData["決済対象区分"];
                Txt.m_BALANCE_FLG = Data.LineData["交換尻確定フラグ"];
                Txt.m_OCR_IC_BK_NO = Data.LineData["OCR持帰銀行コード"];
                Txt.m_QR_IC_BK_NO = Data.LineData["QRコード持帰銀行コード"];
                Txt.m_MICR_IC_BK_NO = Data.LineData["MICR持帰銀行コード"];
                Txt.m_FILE_IC_BK_NO = Data.LineData["ファイル名持帰銀行コード"];
                Txt.m_CHG_IC_BK_NO = Data.LineData["読替持帰銀行コード"];
                Txt.m_TEISEI_IC_BK_NO = Data.LineData["証券データ訂正持帰銀行コード"];
                Txt.m_PAY_IC_BK_NO = Data.LineData["決済持帰銀行コード"];
                Txt.m_PAYAFT_REV_IC_BK_NO = Data.LineData["決済後訂正持帰銀行コード"];
                Txt.m_OCR_IC_BK_NO_CONF = Data.LineData["OCR持帰銀行コード確信度"];
                Txt.m_OCR_AMOUNT = Data.LineData["OCR金額"];
                Txt.m_MICR_AMOUNT = Data.LineData["MICR金額"];
                Txt.m_QR_AMOUNT = Data.LineData["QRコード金額"];
                Txt.m_FILE_AMOUNT = Data.LineData["ファイル名金額"];
                Txt.m_TEISEI_AMOUNT = Data.LineData["証券データ訂正金額"];
                Txt.m_PAY_AMOUNT = Data.LineData["決済金額"];
                Txt.m_PAYAFT_REV_AMOUNT = Data.LineData["決済後訂正金額"];
                Txt.m_OCR_AMOUNT_CONF = Data.LineData["OCR金額確信度"];
                Txt.m_OC_CLEARING_DATE = Data.LineData["持出時交換希望日"];
                Txt.m_TEISEI_CLEARING_DATE = Data.LineData["証券データ訂正交換希望日"];
                Txt.m_CLEARING_DATE = Data.LineData["交換日"];
                Txt.m_QR_IC_BR_NO = Data.LineData["QRコード持帰支店コード"];
                Txt.m_KAMOKU = Data.LineData["科目コード"];
                Txt.m_ACCOUNT = Data.LineData["口座番号"];
                Txt.m_BK_CTL_NO = Data.LineData["銀行管理番号"];
                Txt.m_FREEFIELD = Data.LineData["自由記述欄"];
                Txt.m_BILL_CODE = Data.LineData["交換証券種類コード"];
                Txt.m_BILL_CODE_CONF = Data.LineData["交換証券種類コード確信度"];
                Txt.m_QR = Data.LineData["QRコード情報"];
                Txt.m_MICR = Data.LineData["MICR情報"];
                Txt.m_MICR_CONF = Data.LineData["MICR情報確信度"];
                Txt.m_BILL_NO = Data.LineData["手形・小切手番号"];
                Txt.m_BILL_NO_CONF = Data.LineData["手形・小切手番号確信度"];
                Txt.m_FUBI_KBN_01 = Data.LineData["不渡返還区分１"];
                Txt.m_ZERO_FUBINO_01 = ChgIntData(Data.LineData["0号不渡事由コード１"]);
                Txt.m_FUBI_KBN_02 = Data.LineData["不渡返還区分２"];
                Txt.m_ZRO_FUBINO_02 = ChgIntData(Data.LineData["0号不渡事由コード２"]);
                Txt.m_FUBI_KBN_03 = Data.LineData["不渡返還区分３"];
                Txt.m_ZRO_FUBINO_03 = ChgIntData(Data.LineData["0号不渡事由コード３"]);
                Txt.m_FUBI_KBN_04 = Data.LineData["不渡返還区分４"];
                Txt.m_ZRO_FUBINO_04 = ChgIntData(Data.LineData["0号不渡事由コード４"]);
                Txt.m_FUBI_KBN_05 = Data.LineData["不渡返還区分５"];
                Txt.m_ZRO_FUBINO_05 = ChgIntData(Data.LineData["0号不渡事由コード５"]);
                Txt.m_IC_FLG = Data.LineData["持帰状況フラグ"];

                if (!_itemMgr.InsertBillMeiTxt(Txt, dbp, Tran)) return false;
            }

            return true;
        }

        /// <summary>
        /// 証券明細テキスト登録
        /// 交換日算出処理あり
        /// </summary>
        /// <remarks>ImportBillMeiTxtと共通化できるタイミングがあれば実施</remarks>
        public bool ImportBillMeiTxtChgDate(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            foreach (IFData Data in _DataRecord)
            {
                TBL_BILLMEITXT Txt = new TBL_BILLMEITXT(_itemMgr._TargetFilename, Data.LineData["証券イメージファイル名"], AppInfo.Setting.SchemaBankCD);
                Txt.m_FRONT_IMG_NAME = Data.LineData["表証券イメージファイル名"];
                Txt.m_IMG_KBN = int.Parse(Data.LineData["表・裏等の別"]);
                Txt.m_FILE_OC_BK_NO = Data.LineData["ファイル名持出銀行コード"];
                Txt.m_CHG_OC_BK_NO = Data.LineData["読替持出銀行コード"];
                Txt.m_OC_BR_NO = Data.LineData["持出支店コード"];
                Txt.m_OC_DATE = int.Parse(Data.LineData["持出日"]);
                Txt.m_OC_METHOD = Data.LineData["持出時接続方式"];
                Txt.m_OC_USERID = Data.LineData["ユーザID(持出者)"];
                Txt.m_PAY_KBN = Data.LineData["決済対象区分"];
                Txt.m_BALANCE_FLG = Data.LineData["交換尻確定フラグ"];
                Txt.m_OCR_IC_BK_NO = Data.LineData["OCR持帰銀行コード"];
                Txt.m_QR_IC_BK_NO = Data.LineData["QRコード持帰銀行コード"];
                Txt.m_MICR_IC_BK_NO = Data.LineData["MICR持帰銀行コード"];
                Txt.m_FILE_IC_BK_NO = Data.LineData["ファイル名持帰銀行コード"];
                Txt.m_CHG_IC_BK_NO = Data.LineData["読替持帰銀行コード"];
                Txt.m_TEISEI_IC_BK_NO = Data.LineData["証券データ訂正持帰銀行コード"];
                Txt.m_PAY_IC_BK_NO = Data.LineData["決済持帰銀行コード"];
                Txt.m_PAYAFT_REV_IC_BK_NO = Data.LineData["決済後訂正持帰銀行コード"];
                Txt.m_OCR_IC_BK_NO_CONF = Data.LineData["OCR持帰銀行コード確信度"];
                Txt.m_OCR_AMOUNT = Data.LineData["OCR金額"];
                Txt.m_MICR_AMOUNT = Data.LineData["MICR金額"];
                Txt.m_QR_AMOUNT = Data.LineData["QRコード金額"];
                Txt.m_FILE_AMOUNT = Data.LineData["ファイル名金額"];
                Txt.m_TEISEI_AMOUNT = Data.LineData["証券データ訂正金額"];
                Txt.m_PAY_AMOUNT = Data.LineData["決済金額"];
                Txt.m_PAYAFT_REV_AMOUNT = Data.LineData["決済後訂正金額"];
                Txt.m_OCR_AMOUNT_CONF = Data.LineData["OCR金額確信度"];
                Txt.m_OC_CLEARING_DATE = Data.LineData["持出時交換希望日"];
                Txt.m_TEISEI_CLEARING_DATE = Data.LineData["証券データ訂正交換希望日"];
                Txt.m_CLEARING_DATE = Data.LineData["交換日"];
                Txt.m_QR_IC_BR_NO = Data.LineData["QRコード持帰支店コード"];
                Txt.m_KAMOKU = Data.LineData["科目コード"];
                Txt.m_ACCOUNT = Data.LineData["口座番号"];
                Txt.m_BK_CTL_NO = Data.LineData["銀行管理番号"];
                Txt.m_FREEFIELD = Data.LineData["自由記述欄"];
                Txt.m_BILL_CODE = Data.LineData["交換証券種類コード"];
                Txt.m_BILL_CODE_CONF = Data.LineData["交換証券種類コード確信度"];
                Txt.m_QR = Data.LineData["QRコード情報"];
                Txt.m_MICR = Data.LineData["MICR情報"];
                Txt.m_MICR_CONF = Data.LineData["MICR情報確信度"];
                Txt.m_BILL_NO = Data.LineData["手形・小切手番号"];
                Txt.m_BILL_NO_CONF = Data.LineData["手形・小切手番号確信度"];
                Txt.m_FUBI_KBN_01 = Data.LineData["不渡返還区分１"];
                Txt.m_ZERO_FUBINO_01 = ChgIntData(Data.LineData["0号不渡事由コード１"]);
                Txt.m_FUBI_KBN_02 = Data.LineData["不渡返還区分２"];
                Txt.m_ZRO_FUBINO_02 = ChgIntData(Data.LineData["0号不渡事由コード２"]);
                Txt.m_FUBI_KBN_03 = Data.LineData["不渡返還区分３"];
                Txt.m_ZRO_FUBINO_03 = ChgIntData(Data.LineData["0号不渡事由コード３"]);
                Txt.m_FUBI_KBN_04 = Data.LineData["不渡返還区分４"];
                Txt.m_ZRO_FUBINO_04 = ChgIntData(Data.LineData["0号不渡事由コード４"]);
                Txt.m_FUBI_KBN_05 = Data.LineData["不渡返還区分５"];
                Txt.m_ZRO_FUBINO_05 = ChgIntData(Data.LineData["0号不渡事由コード５"]);
                Txt.m_IC_FLG = Data.LineData["持帰状況フラグ"];

                // 交換日算出設定
                ChgClearingDate(ref Txt);

                if (!_itemMgr.InsertBillMeiTxt(Txt, dbp, Tran)) return false;
            }

            return true;
        }

        /// <summary>
        /// 処理対象ファイル名で証券明細テキストを削除
        /// </summary>
        public bool DeleteBillMeiTxt(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            return DeleteBillMeiTxtFilename(_itemMgr._TargetFilename, dbp, Tran);
        }

        /// <summary>
        /// 交換尻関連の証券明細テキストを削除
        /// </summary>
        public bool DeleteBillMeiTxtBalance(int Type, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            //削除対象のデータ一式取得
            //int CreateDate = int.Parse(_HeaderData.LineData["作成日"]);
            if(!_itemMgr.GetDeleteBillMeiTxtBalance(Type, out List<TBL_BILLMEITXT_CTL> CtlData, dbp))
            {
                return false;
            }

            foreach (TBL_BILLMEITXT_CTL Data in CtlData)
            {
                //データ削除
                if (!DeleteBillMeiTxtFilename(Data._TXTNAME, dbp, Tran)) return false;
            }

            return true;
        }

        /// <summary>
        /// 持帰要求結果証券明細テキスト登録
        /// </summary>
        public bool ImportICReqRetBillMeiTxt(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // イメージアーカイブ名を取得
            if (!_itemMgr.GetImgARCHName(out string ImgARCHName, dbp)) return false;

            foreach (IFData Data in _DataRecord)
            {
                TBL_ICREQRET_BILLMEITXT Txt = new TBL_ICREQRET_BILLMEITXT(_itemMgr._TargetFilename, 0, Data.LineData["証券イメージファイル名"], AppInfo.Setting.SchemaBankCD);
                Txt.m_IMG_ARCH_NAME = ImgARCHName;
                Txt.m_FRONT_IMG_NAME = Data.LineData["表証券イメージファイル名"];
                Txt.m_IMG_KBN = int.Parse(Data.LineData["表・裏等の別"]);
                Txt.m_FILE_OC_BK_NO = Data.LineData["ファイル名持出銀行コード"];
                Txt.m_CHG_OC_BK_NO = Data.LineData["読替持出銀行コード"];
                Txt.m_OC_BR_NO = Data.LineData["持出支店コード"];
                Txt.m_OC_DATE = int.Parse(Data.LineData["持出日"]);
                Txt.m_OC_METHOD = Data.LineData["持出時接続方式"];
                Txt.m_OC_USERID = Data.LineData["ユーザID(持出者)"];
                Txt.m_PAY_KBN = Data.LineData["決済対象区分"];
                Txt.m_BALANCE_FLG = Data.LineData["交換尻確定フラグ"];
                Txt.m_OCR_IC_BK_NO = Data.LineData["OCR持帰銀行コード"];
                Txt.m_QR_IC_BK_NO = Data.LineData["QRコード持帰銀行コード"];
                Txt.m_MICR_IC_BK_NO = Data.LineData["MICR持帰銀行コード"];
                Txt.m_FILE_IC_BK_NO = Data.LineData["ファイル名持帰銀行コード"];
                Txt.m_CHG_IC_BK_NO = Data.LineData["読替持帰銀行コード"];
                Txt.m_TEISEI_IC_BK_NO = Data.LineData["証券データ訂正持帰銀行コード"];
                Txt.m_PAY_IC_BK_NO = Data.LineData["決済持帰銀行コード"];
                Txt.m_PAYAFT_REV_IC_BK_NO = Data.LineData["決済後訂正持帰銀行コード"];
                Txt.m_OCR_IC_BK_NO_CONF = Data.LineData["OCR持帰銀行コード確信度"];
                Txt.m_OCR_AMOUNT = Data.LineData["OCR金額"];
                Txt.m_MICR_AMOUNT = Data.LineData["MICR金額"];
                Txt.m_QR_AMOUNT = Data.LineData["QRコード金額"];
                Txt.m_FILE_AMOUNT = Data.LineData["ファイル名金額"];
                Txt.m_TEISEI_AMOUNT = Data.LineData["証券データ訂正金額"];
                Txt.m_PAY_AMOUNT = Data.LineData["決済金額"];
                Txt.m_PAYAFT_REV_AMOUNT = Data.LineData["決済後訂正金額"];
                Txt.m_OCR_AMOUNT_CONF = Data.LineData["OCR金額確信度"];
                Txt.m_OC_CLEARING_DATE = Data.LineData["持出時交換希望日"];
                Txt.m_TEISEI_CLEARING_DATE = Data.LineData["証券データ訂正交換希望日"];
                Txt.m_CLEARING_DATE = Data.LineData["交換日"];
                Txt.m_QR_IC_BR_NO = Data.LineData["QRコード持帰支店コード"];
                Txt.m_KAMOKU = Data.LineData["科目コード"];
                Txt.m_ACCOUNT = Data.LineData["口座番号"];
                Txt.m_BK_CTL_NO = Data.LineData["銀行管理番号"];
                Txt.m_FREEFIELD = Data.LineData["自由記述欄"];
                Txt.m_BILL_CODE = Data.LineData["交換証券種類コード"];
                Txt.m_BILL_CODE_CONF = Data.LineData["交換証券種類コード確信度"];
                Txt.m_QR = Data.LineData["QRコード情報"];
                Txt.m_MICR = Data.LineData["MICR情報"];
                Txt.m_MICR_CONF = Data.LineData["MICR情報確信度"];
                Txt.m_BILL_NO = Data.LineData["手形・小切手番号"];
                Txt.m_BILL_NO_CONF = Data.LineData["手形・小切手番号確信度"];
                Txt.m_FUBI_KBN_01 = Data.LineData["不渡返還区分１"];
                Txt.m_ZERO_FUBINO_01 = ChgIntData(Data.LineData["0号不渡事由コード１"]);
                Txt.m_FUBI_KBN_02 = Data.LineData["不渡返還区分２"];
                Txt.m_ZRO_FUBINO_02 = ChgIntData(Data.LineData["0号不渡事由コード２"]);
                Txt.m_FUBI_KBN_03 = Data.LineData["不渡返還区分３"];
                Txt.m_ZRO_FUBINO_03 = ChgIntData(Data.LineData["0号不渡事由コード３"]);
                Txt.m_FUBI_KBN_04 = Data.LineData["不渡返還区分４"];
                Txt.m_ZRO_FUBINO_04 = ChgIntData(Data.LineData["0号不渡事由コード４"]);
                Txt.m_FUBI_KBN_05 = Data.LineData["不渡返還区分５"];
                Txt.m_ZRO_FUBINO_05 = ChgIntData(Data.LineData["0号不渡事由コード５"]);
                Txt.m_IC_FLG = Data.LineData["持帰状況フラグ"];
                Txt.m_KAKUTEI_FLG = 0;

                if (!_itemMgr.InsertICReqRetBillMeiTxt(Txt, dbp, Tran)) return false;
            }

            return true;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// 指定ファイル名で証券明細テキストを削除
        /// </summary>
        private bool DeleteBillMeiTxtFilename(string Filename, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            if (!_itemMgr.DeleteBillMeiTxtCtl(Filename, dbp, Tran)) return false;
            if (!_itemMgr.DeleteBillMeiTxt(Filename, dbp, Tran)) return false;

            return true;
        }

        /// <summary>
        /// データ変更
        /// </summary>
        /// <returns></returns>
        public static int ChgIntData(string Data)
        {
            if (!int.TryParse(Data, out int intData)) return 0;

            return intData;
        }

        /// <summary>
        /// 交換日変更
        /// </summary>
        /// <returns></returns>
        private static void ChgClearingDate(ref TBL_BILLMEITXT Txt)
        {
            string Wk = string.Empty;
            if (Txt.m_CLEARING_DATE != "ZZZZZZZZ")
            {
                // 交換日が省略値以外の場合
                Wk = Txt.m_CLEARING_DATE;
            }
            else if (Txt.m_TEISEI_CLEARING_DATE != "ZZZZZZZZ")
            {
                // 証券データ訂正交換希望日が省略値以外の場合
                Wk = Txt.m_TEISEI_CLEARING_DATE;
            }
            else
            {
                // 上記以外
                Wk = Txt.m_OC_CLEARING_DATE;
            }

            // 営業日換算
            if (int.TryParse(Wk, out int intWk))
            {
                Wk = Calendar.GetSettleDay(intWk).ToString();
            }

            // 算出値設定
            Txt.m_CLEARING_DATE = Wk;
        }

    }
}
