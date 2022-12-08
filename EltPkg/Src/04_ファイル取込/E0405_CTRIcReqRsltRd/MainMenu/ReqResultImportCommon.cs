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
    class ReqResultImportCommon
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
        public ReqResultImportCommon(ControllerBase ctl, IFFileDataLoad LoadFile)
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
        /// 持帰要求管理存在チェック・更新処理
        /// </summary>
        public bool UpdateReqCtl()
        {
            //存在チェック
            if (!_itemMgr.ChkReqCtl(_HeaderData.LineData["持帰要求テキストファイル名"], out TBL_ICREQ_CTL ctl))
            {
                return false;
            }

            //更新処理
            ctl.m_RET_TXT_NAME = _itemMgr._TargetFilename;
            ctl.m_RET_COUNT = int.Parse(_TrailerData.LineData["レコード件数"]);
            ctl.m_RET_STS = int.Parse(_HeaderData.LineData["銀行コード"]);
            ctl.m_RET_MAKE_DATE = int.Parse(_HeaderData.LineData["作成日"]);
            ctl.m_RET_REQ_TXT_NAME = _HeaderData.LineData["持帰要求テキストファイル名"];
            ctl.m_RET_FILE_CHK_CODE = _HeaderData.LineData["ファイルチェック結果コード"];
            ctl.m_RET_CLEARING_DATE_S = _HeaderData.LineData["交換希望日１"];
            ctl.m_RET_CLEARING_DATE_E = _HeaderData.LineData["交換希望日２"];
            ctl.m_RET_BILL_CODE = _HeaderData.LineData["交換証券種類コード"];
            ctl.m_RET_IC_TYPE = _HeaderData.LineData["持帰対象区分"];
            ctl.m_RET_IMG_NEED = _HeaderData.LineData["証券イメージ要否区分"];
            ctl.m_RET_PROC_RETCODE = _HeaderData.LineData["処理結果コード"];
            ctl.m_RET_DATE = AplInfo.OpDate();
            ctl.m_RET_TIME = int.Parse(System.DateTime.Now.ToString("HHmmssfff"));

            return _itemMgr.UpdateReqCtl(ctl);
        }

        /// <summary>
        /// 持帰要求結果管理登録
        /// </summary>
        public bool ImportReqRetCtl(IFData ifdata, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            TBL_ICREQRET_CTL ReqRetCtl = new TBL_ICREQRET_CTL(_itemMgr._TargetFilename, ifdata.LineData["証券明細テキストファイル名"], 0, AppInfo.Setting.SchemaBankCD);
            ReqRetCtl.m_CAP_STS = 0;

            string archname = ifdata.LineData["イメージアーカイブ名"];
            int archcapsts = 0;
            if (archname == new string('Z', 32))
            {
                archname = string.Empty;
                archcapsts = -1;
            }
            ReqRetCtl.m_IMG_ARCH_NAME = archname;
            ReqRetCtl.m_IMG_ARCH_CAP_STS = archcapsts;
            return _itemMgr.InsertReqRetCtl(ReqRetCtl, dbp, Tran);
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
