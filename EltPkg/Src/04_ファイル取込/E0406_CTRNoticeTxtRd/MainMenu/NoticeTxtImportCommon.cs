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
    class NoticeTxtImportCommon
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
        public NoticeTxtImportCommon(ControllerBase ctl, IFFileDataLoad LoadFile)
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
        /// 通知テキスト管理登録
        /// </summary>
        public bool ImportNoticeTxtCtl(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            TBL_TSUCHITXT_CTL TxtCtl = new TBL_TSUCHITXT_CTL(_itemMgr._TargetFilename, AppInfo.Setting.SchemaBankCD);
            TxtCtl.m_FILE_ID = _HeaderData.LineData["ファイルID"];
            TxtCtl.m_FILE_DIVID = _HeaderData.LineData["ファイル識別区分"];
            TxtCtl.m_BK_NO = _HeaderData.LineData["銀行コード"];
            TxtCtl.m_MAKE_DATE = int.Parse(_HeaderData.LineData["作成日"]);
            TxtCtl.m_SEND_SEQ = _HeaderData.LineData["送信一連番号"];
            TxtCtl.m_RECORD_COUNT = int.Parse(_TrailerData.LineData["レコード件数"]);
            TxtCtl.m_RECV_DATE = AplInfo.OpDate();
            TxtCtl.m_RECV_TIME = int.Parse(System.DateTime.Now.ToString("HHmmssfff"));

            return _itemMgr.InsertNoticeTxtCtl(TxtCtl, dbp, Tran);
        }

        /// <summary>
        /// 通知テキスト登録
        /// </summary>
        public bool ImportNoticeTxt(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            int i = 1;
            foreach (IFData Data in _DataRecord)
            {
                TBL_TSUCHITXT Txt = new TBL_TSUCHITXT(_itemMgr._TargetFilename, i, AppInfo.Setting.SchemaBankCD);
                Txt.m_IMG_NAME = Data.LineData["証券イメージファイル名"];
                Txt.m_BK_NO_TEISEI_FLG = Data.LineData["銀行コード訂正フラグ"];
                Txt.m_TEISEI_BEF_BK_NO = Data.LineData["訂正前銀行コード"];
                Txt.m_TEISEI_AFT_BK_NO = Data.LineData["訂正後銀行コード"];
                Txt.m_CLEARING_TEISEI_FLG = Data.LineData["交換希望日訂正フラグ"];
                Txt.m_TEISEI_BEF_CLEARING_DATE = Data.LineData["訂正前交換希望日"];
                Txt.m_TEISEI_CLEARING_DATE = Data.LineData["訂正後交換希望日"];
                Txt.m_AMOUNT_TEISEI_FLG = Data.LineData["金額訂正フラグ"];
                Txt.m_TEISEI_BEF_AMOUNT = Data.LineData["訂正前金額"];
                Txt.m_TEISEI_AMOUNT = Data.LineData["訂正後金額"];
                Txt.m_DUPLICATE_IMG_NAME = Data.LineData["二重持出イメージファイル名"];
                Txt.m_FUBI_REG_KBN = Data.LineData["不渡返還登録区分"];
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
                Txt.m_REV_CLEARING_FLG = Data.LineData["逆交換対象フラグ"];

                if (!_itemMgr.InsertNoticeTxt(Txt, dbp, Tran)) return false;
                i++;
            }

            return true;
        }

        /// <summary>
        /// トランザクション項目更新処理
        /// </summary>
        /// <returns></returns>
        public bool UpdateTRData(IFData ifData, EntryReplacer Replacer, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            return UpdateTRITEMBKNo(ifData, Replacer, dbp, Tran) && UpdateTRITEMClearingDate(ifData, dbp, Tran) && UpdateTRITEMAmount(ifData, dbp, Tran);
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// データ変更
        /// </summary>
        /// <returns></returns>
        private static int ChgIntData(string Data)
        {
            if (!int.TryParse(Data, out int intData)) return 0;

            return intData;
        }

        /// <summary>
        /// 持帰銀行更新処理
        /// </summary>
        /// <returns></returns>
        private bool UpdateTRITEMICBK(IFData ifData, EntryReplacer Replacer, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string ImgName = ifData.LineData["証券イメージファイル名"];
            string UpdateData = ifData.LineData["訂正後銀行コード"];
            string SetYomikaeBKCode = UpdateData;
            string SetYomikaeBKName = string.Empty;
            if (int.TryParse(UpdateData, out int intdata))
            {
                // 読替処理
                int BkNoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰銀行コード, Const.BANK_NO_LEN);
                SetYomikaeBKCode = Replacer.GetYomikaeBkNo(intdata, BkNoLength);
                // 銀行名取得
                SetYomikaeBKName = Replacer.GetBankName(int.Parse(SetYomikaeBKCode));
            }

            // 券面持帰銀行・持帰銀行・持帰銀行名更新
            return !(_itemMgr.UpdateTRItemTeisei(1, ImgName, SetYomikaeBKCode, dbp, Tran) <= 0 ||
                     _itemMgr.UpdateTRItemTeisei(2, ImgName, SetYomikaeBKName, dbp, Tran) <= 0 ||
                     _itemMgr.UpdateTRItemTeisei(19, ImgName, UpdateData, dbp, Tran) <= 0);
        }

        /// <summary>
        /// 持帰銀行更新処理(MRC読替)
        /// </summary>
        /// <returns></returns>
        private bool UpdateTRITEMICBKMRC(IFData ifData, EntryReplacer Replacer, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string ImgName = ifData.LineData["証券イメージファイル名"];
            string UpdateData = ifData.LineData["訂正後銀行コード"];
            string SetYomikaeBKCode = UpdateData;
            string SetYomikaeBKName = string.Empty;
            if (int.TryParse(UpdateData, out int intdata))
            {
                // 読替処理
                int BkNoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰銀行コード, Const.BANK_NO_LEN);
                SetYomikaeBKCode = Replacer.GetYomikaeBkNo(intdata, BkNoLength);
                // 銀行名取得
                SetYomikaeBKName = Replacer.GetBankName(int.Parse(SetYomikaeBKCode));
            }

            // 券面持帰銀行・持帰銀行・持帰銀行名更新
            return !(_itemMgr.UpdateTRItemYomikae(1, ImgName, SetYomikaeBKCode, dbp, Tran) <= 0 ||
                     _itemMgr.UpdateTRItemYomikae(2, ImgName, SetYomikaeBKName, dbp, Tran) <= 0 ||
                     _itemMgr.UpdateTRItemYomikae(19, ImgName, UpdateData, dbp, Tran) <= 0);
        }

        /// <summary>
        /// 持出銀行更新処理
        /// </summary>
        /// <returns></returns>
        private bool UpdateTRMEIOCBK(IFData ifData, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string ImgName = ifData.LineData["証券イメージファイル名"];
            string UpdateData = ifData.LineData["訂正後銀行コード"];
            if (!int.TryParse(UpdateData, out int intdata))
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "訂正後銀行コードが不正です", 3);
                return false;
            }

            // 持出銀行更新
            return !(_itemMgr.UpdateTRMeiTeisei(ImgName, intdata, dbp, Tran) <= 0);
        }

        /// <summary>
        /// 銀行データ更新処理
        /// </summary>
        /// <returns></returns>
        private bool UpdateTRITEMBKNo(IFData ifData, EntryReplacer Replacer, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            switch (ifData.LineData["銀行コード訂正フラグ"])
            {
                case "0":
                case "Z":
                    //更新無し
                    break;
                case "1":
                    // 訂正あり

                    switch (_itemMgr._file_divid)
                    {
                        case "GMA":
                        case "GXA":

                            if (!UpdateTRITEMICBK(ifData, Replacer, dbp, Tran))
                            {
                                return false;
                            }
                            break;
                    }

                    break;
                case "2":
                    // 変更あり

                    switch (_itemMgr._file_divid)
                    {
                        case "MRB":
                            if (!UpdateTRMEIOCBK(ifData, dbp, Tran))
                            {
                                return false;
                            }
                            break;
                    }

                    break;
                case "3":
                    // 変更あり

                    switch (_itemMgr._file_divid)
                    {
                        case "MRC":
                            if (!UpdateTRITEMICBKMRC(ifData, Replacer, dbp, Tran))
                            {
                                return false;
                            }
                            break;
                    }

                    break;
                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 交換希望日データ更新処理
        /// </summary>
        /// <returns></returns>
        private bool UpdateTRITEMClearingDate(IFData ifData, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            switch (ifData.LineData["交換希望日訂正フラグ"])
            {
                case "0":
                case "Z":
                    //更新無し
                    break;
                case "1":
                    // 訂正あり
                    if (AppInfo.Setting.GymId == GymParam.GymId.持出)
                    {
                        // 持出のみ処理
                        string ImgName = ifData.LineData["証券イメージファイル名"];
                        string UpdateData = ifData.LineData["訂正後交換希望日"];
                        string SetClearingDate = string.Empty;
                        string SetWarekiClearingDate = string.Empty;
                        if (int.TryParse(UpdateData, out int intdata))
                        {
                            UpdateData = intdata.ToString();
                            //交換日算出
                            SetClearingDate = Calendar.GetSettleDay(intdata).ToString();
                            //交換希望日和暦算出
                            SetWarekiClearingDate = ImportTRAccessCommon.ConvWareki(intdata);
                        }

                        // 交換希望日・和暦交換希望日・交換日更新
                        if (_itemMgr.UpdateTRItemTeisei(3, ImgName, UpdateData, dbp, Tran) <= 0 ||
                            _itemMgr.UpdateTRItemTeisei(4, ImgName, SetWarekiClearingDate, dbp, Tran) <= 0 ||
                            _itemMgr.UpdateTRItemTeisei(5, ImgName, SetClearingDate, dbp, Tran) <= 0)
                        {
                            return false;
                        }
                    }

                    break;
                case "9":
                    // 訂正取消
                    if (AppInfo.Setting.GymId == GymParam.GymId.持出)
                    {
                        // 持出のみ処理
                        string ImgName = ifData.LineData["証券イメージファイル名"];
                        // 交換希望日・和暦交換希望日・交換日更新
                        if (_itemMgr.UpdateTRItemCancel(3, ImgName, dbp, Tran) <= 0 ||
                            _itemMgr.UpdateTRItemCancel(4, ImgName, dbp, Tran) <= 0 ||
                            _itemMgr.UpdateTRItemCancel(5, ImgName, dbp, Tran) <= 0)
                        {
                            return false;
                        }
                    }

                    break;
                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 金額データ更新処理
        /// </summary>
        /// <returns></returns>
        private bool UpdateTRITEMAmount(IFData ifData, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            switch (ifData.LineData["金額訂正フラグ"])
            {
                case "0":
                case "Z":
                    //更新無し
                    break;
                case "1":
                    // 訂正あり
                    if (AppInfo.Setting.GymId == GymParam.GymId.持出)
                    {
                        // 持出のみ処理
                        string ImgName = ifData.LineData["証券イメージファイル名"];
                        string UpdateData = ifData.LineData["訂正後金額"];
                        // 更新
                        if (_itemMgr.UpdateTRItemTeisei(6, ImgName, UpdateData, dbp, Tran) <= 0)
                        {
                            return false;
                        }
                    }

                    break;
                case "9":
                    // 訂正取消
                    if (AppInfo.Setting.GymId == GymParam.GymId.持出)
                    {
                        // 持出のみ処理
                        string ImgName = ifData.LineData["証券イメージファイル名"];
                        // 更新
                        if (_itemMgr.UpdateTRItemCancel(6, ImgName, dbp, Tran) <= 0)
                        {
                            return false;
                        }
                    }

                    break;
                default:
                    return false;
            }

            return true;
        }

    }
}
