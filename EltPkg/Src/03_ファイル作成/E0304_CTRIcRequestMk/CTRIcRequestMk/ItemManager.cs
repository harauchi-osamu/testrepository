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

namespace CTRIcRequestMk
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        private Controller _ctl = null;

        /// <summary>交換証券種類マスタ（key=BILL_CODE, val=TBL_BILLMF）</summary>
        public SortedDictionary<int, TBL_BILLMF> mst_bills { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            mst_bills = new SortedDictionary<int, TBL_BILLMF>();
            this.DispParams = new DisplayParams();
            this.DispParams.Clear();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void FetchAllData(Controller ctl)
        {
            _ctl = ctl;
            if (_ctl.IsIniErr) { return; }

            Fetch_mst_bills();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_mst_bills()
        {
            mst_bills = new SortedDictionary<int, TBL_BILLMF>();
            string strSQL = TBL_BILLMF.GetSelectQuery();

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BILLMF data = new TBL_BILLMF(tbl.Rows[i]);
                        mst_bills.Add(data._BILL_CODE, data);
                    }
                }
                catch (Exception ex)
                {
                    if (!_ctl.IsSilent)
                    {
                        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    }
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// 明細トランザクションを更新する
        /// ファイル集配信管理を登録する
        /// </summary>
        /// <returns></returns>
        public bool RegistMeisaiItems(FileGenerator if203, FileGenerator if207, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // DB更新処理はここでまとめて全部やる
            string strSQL = "";
            try
            {
                // ファイル集配信管理
                TBL_FILE_CTL fctl = new TBL_FILE_CTL(
                    if203.FileId,
                    if203.FileDivid,
                    if203.FileName,
                    if207.FileName,
                    AppInfo.Setting.SchemaBankCD);
                fctl.m_SEND_FILE_LENGTH = if203.FileSize;
                fctl.m_SEND_STS = 0;
                fctl.m_MAKE_OPENO = AplInfo.OP_ID;
                fctl.m_MAKE_DATE = AplInfo.OpDate();
                fctl.m_MAKE_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
                strSQL = fctl.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                // 持帰要求管理
                TBL_ICREQ_CTL req = new TBL_ICREQ_CTL(if203.FileName, AppInfo.Setting.SchemaBankCD);
                req.m_REQ_DATE = AplInfo.OpDate();
                req.m_REQ_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
                req.m_CLEARING_DATE_S = DispParams.ClearingDateFrom;
                req.m_CLEARING_DATE_E = DispParams.ClearingDateTo;
                req.m_BILL_CODE = DispParams.BillCode;
                req.m_IC_TYPE = DBConvert.ToIntNull(DispParams.IcType);
                req.m_IMG_NEED = DBConvert.ToIntNull(DispParams.ImageNeed);
                strSQL = req.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("ＤＢ更新に失敗しました。");
            }
            return true;
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public int ClearingDateFrom { get; set; }
            public int ClearingDateTo { get; set; }
            public string BillCode { get; set; }
            public string IcType { get; set; }
            public string ImageNeed { get; set; }
            public string CreateFileName { get; set; } = "";
            public FileGenerator if203 { get; set; } = null;

            public void Clear()
            {
                this.ClearingDateFrom = 0;
                this.ClearingDateTo = 0;
                this.BillCode = "";
                this.IcType = "";
                this.ImageNeed = "";
                this.CreateFileName = "";
                this.if203 = null;
            }
        }
    }
}
