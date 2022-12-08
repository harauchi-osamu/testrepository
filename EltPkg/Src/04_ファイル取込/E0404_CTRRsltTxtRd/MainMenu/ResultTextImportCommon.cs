using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using IFFileOperation;

namespace MainMenu
{
    class ResultTextImportCommon
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
        public IFData _TrailerData
        {
            get { return _LoadFile.LoadData.Where(x => x.KBN == "8").First(); }
        }
        public IEnumerable<IFData> _DataRecord
        {
            get { return _LoadFile.LoadData.Where(x => x.KBN == "2"); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ResultTextImportCommon(ControllerBase ctl, IFFileDataLoad LoadFile)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
            _LoadFile = LoadFile;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 結果テキスト管理登録
        /// </summary>
        public bool ImportResultTxtCtl(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            TBL_RESULTTXT_CTL TxtCtl = new TBL_RESULTTXT_CTL(_itemMgr._TargetFilename, AppInfo.Setting.SchemaBankCD);
            TxtCtl.m_FILE_ID = _HeaderData.LineData["ファイルID"];
            TxtCtl.m_FILE_DIVID = _HeaderData.LineData["ファイル識別区分"];
            TxtCtl.m_BK_NO = _HeaderData.LineData["銀行コード"];
            TxtCtl.m_MAKE_DATE = int.Parse(_HeaderData.LineData["作成日"]);
            TxtCtl.m_SEND_SEQ = _HeaderData.LineData["送信一連番号"];
            TxtCtl.m_RECEPTION_ID = _HeaderData.LineData["受付ファイルID"];
            TxtCtl.m_PROC_FILE_NAME = _HeaderData.LineData["処理対象ファイル名"];
            TxtCtl.m_FILE_CHK_CODE = _HeaderData.LineData["ファイルチェック結果コード"];
            TxtCtl.m_RECORD_COUNT = int.Parse(_TrailerData.LineData["レコード件数"]);
            TxtCtl.m_RECV_DATE = AplInfo.OpDate();
            TxtCtl.m_RECV_TIME = int.Parse(System.DateTime.Now.ToString("HHmmssfff"));

            return _itemMgr.InsertResultTxtCtl(TxtCtl, dbp, Tran);
        }

        /// <summary>
        /// 結果テキスト登録
        /// </summary>
        public bool ImportResultTxt(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            int i = 1;
            foreach (IFData Data in _DataRecord)
            {
                TBL_RESULTTXT Txt = new TBL_RESULTTXT(_itemMgr._TargetFilename, i, AppInfo.Setting.SchemaBankCD);
                Txt.m_RECEPTION = Data.LineData["受付内容"];
                Txt.m_RET_CODE = Data.LineData["処理結果コード"];
                Txt.m_IMG_NAME = Data.LineData["証券イメージファイル名"];
                Txt.m_FRONT_IMG_NAME = Data.LineData["表証券イメージファイル名"];
                Txt.m_IMG_KBN = ChgIntData(Data.LineData["表・裏等の別"]);
                Txt.m_FILE_OC_BK_NO = Data.LineData["ファイル名持出銀行コード"];
                Txt.m_CHG_OC_BK_NO = Data.LineData["読替持出銀行コード"];
                Txt.m_OC_BR_NO = Data.LineData["持出支店コード"];
                Txt.m_OC_DATE = ChgIntData(Data.LineData["持出日"]);
                Txt.m_OC_METHOD = Data.LineData["持出時接続方式"];
                Txt.m_OC_USERID = Data.LineData["ユーザID(持出者)"];
                Txt.m_PAY_KBN = Data.LineData["決済対象区分"];
                Txt.m_BALANCE_FLG = Data.LineData["交換尻確定フラグ"];
                Txt.m_OCR_IC_BK_NO = Data.LineData["MICR持帰銀行コード"];
                Txt.m_QR_IC_BK_NO = Data.LineData["OCR持帰銀行コード"];
                Txt.m_MICR_IC_BK_NO = Data.LineData["QRコード持帰銀行コード"];
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

                if (!_itemMgr.InsertResultTxt(Txt, dbp, Tran)) return false;
                i++;
            }

            return true;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// データ変更
        /// </summary>
        /// <returns></returns>
        public static int ChgIntData(string Data)
        {
            if (!int.TryParse(Data, out int intData)) return 0;

            return intData;
        }

    }
}
