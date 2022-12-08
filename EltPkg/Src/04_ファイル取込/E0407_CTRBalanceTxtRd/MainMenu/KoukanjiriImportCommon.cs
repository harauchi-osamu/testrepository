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
    class KoukanjiriImportCommon
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
        public KoukanjiriImportCommon(ControllerBase ctl, IFFileDataLoad LoadFile)
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
        /// 交換尻管理登録
        /// </summary>
        public bool ImportBalanceTxtCtl(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            TBL_BALANCETXT_CTL TxtCtl = new TBL_BALANCETXT_CTL(_itemMgr._TargetFilename, AppInfo.Setting.SchemaBankCD);
            TxtCtl.m_FILE_ID = _HeaderData.LineData["ファイルID"];
            TxtCtl.m_FILE_DIVID = _HeaderData.LineData["ファイル識別区分"];
            TxtCtl.m_BK_NO = _HeaderData.LineData["銀行コード"];
            TxtCtl.m_MAKE_DATE = int.Parse(_HeaderData.LineData["作成日"]);
            TxtCtl.m_SEND_SEQ = _HeaderData.LineData["送信一連番号"];
            TxtCtl.m_CLEARING_DATE = int.Parse(_HeaderData.LineData["交換日"]);
            TxtCtl.m_RECORD_COUNT = int.Parse(_TrailerData.LineData["レコード件数"]);
            TxtCtl.m_RECV_DATE = AplInfo.OpDate();
            TxtCtl.m_RECV_TIME = int.Parse(System.DateTime.Now.ToString("HHmmssfff"));

            return _itemMgr.InsertBalanceTxtCtl(TxtCtl, dbp, Tran);
        }

        /// <summary>
        /// 交換尻登録
        /// </summary>
        public bool ImportBalanceTxt(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            foreach (IFData Data in _DataRecord)
            {
                TBL_BALANCETXT Txt = new TBL_BALANCETXT(_itemMgr._TargetFilename, Data.LineData["銀行コード"], AppInfo.Setting.SchemaBankCD);
                Txt.m_CONFIRM_FLG = Data.LineData["確定フラグ"];
                Txt.m_LOAN_KBN = int.Parse(Data.LineData["借貸区分コード"]);
                Txt.m_PAY_AMOUNT = long.Parse(Data.LineData["決済金額"]).ToString();

                if (!_itemMgr.InsertBalanceTxt(Txt, dbp, Tran)) return false;
            }

            return true;
        }

        /// <summary>
        /// 交換尻テキストを削除
        /// </summary>
        public bool DeleteBalanceTxt(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            //削除対象のデータ一式取得
            //int CreateDate = int.Parse(_HeaderData.LineData["交換日"]);
            if(!_itemMgr.GetDeleteBalanceTxt(out List<TBL_BALANCETXT_CTL> CtlData, dbp))
            {
                return false;
            }

            foreach (TBL_BALANCETXT_CTL Data in CtlData)
            {
                //データ削除
                if (!DeleteBalanceTxtFilename(Data._FILE_NAME, dbp, Tran)) return false;
            }

            return true;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// 指定ファイル名で証券明細テキストを削除
        /// </summary>
        private bool DeleteBalanceTxtFilename(string Filename, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            if (!_itemMgr.DeleteBalanceTxtCtl(Filename, dbp, Tran)) return false;
            if (!_itemMgr.DeleteBalanceTxt(Filename, dbp, Tran)) return false;

            return true;
        }
    }
}
