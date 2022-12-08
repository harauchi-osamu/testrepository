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

namespace SearchICReqResult
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        /// <summary>汎用テキストデータICREQ</summary>
        public List<TBL_GENERALTEXTMF> ICREQGeneralTextMF { get; set; }
        /// <summary>汎用テキストデータICREQRET</summary>
        public List<TBL_GENERALTEXTMF> ICREQRETGeneralTextMF { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }
        /// <summary>結果照会一覧データ</summary>
        public Dictionary<string, ListData> SearchData { get; set; }
        /// <summary>詳細照会データ</summary>
        public TBL_ICREQ_CTL ICReqData { get; private set; }

        #region GeneralText定義

        public enum DispType
        {
            /// <summary>説明</summary>
            DESCRIPTION = 1,
            /// <summary>略称</summary>
            ABBREVIATE = 2,
            /// <summary>略称(値)</summary>
            ABBREVIATECODE = 3,
        }

        public enum ICREQDataNo
        {
            /// <summary>交換証券種類コード</summary>
            BILL_CODE = 5,
            /// <summary>持帰対象区分</summary>
            IC_TYPE = 6,
            /// <summary>証券イメージ要否区分</summary>
            IMG_NEED = 7,
        }

        public enum ICREQRETHeaderNo
        {
            /// <summary>ファイルチェック結果コード</summary>
            FILE_CHK_CODE = 10,
            /// <summary>処理結果コード</summary>
            PROC_RETCODE = 17,
        }

        #endregion 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.DispParams = new DisplayParams();
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public string ReqTxtName { get; set; } = "";
            public int reqdate { get; set; } = -1;
            public void Clear()
            {
                this.reqdate = -1;
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public override void FetchAllData()
        {
            FetchGeneralTextMF();
        }

        /// <summary>
        /// 汎用テキスト一覧取得
        /// </summary>
        public bool FetchGeneralTextMF(FormBase form = null)
        {
            // 初期化
            ICREQGeneralTextMF = new List<TBL_GENERALTEXTMF>();
            ICREQRETGeneralTextMF = new List<TBL_GENERALTEXTMF>();

            // SELECT実行
            string strSQL = string.Empty;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    strSQL = TBL_GENERALTEXTMF.GetSelectQueryTextKbn((int)TBL_GENERALTEXTMF.TextKbn.ICREQ);
                    DataTable tblReq = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tblReq.Rows.Count; i++)
                    {
                        TBL_GENERALTEXTMF ctl = new TBL_GENERALTEXTMF(tblReq.Rows[i]);
                        ICREQGeneralTextMF.Add(ctl);
                    }
                    strSQL = TBL_GENERALTEXTMF.GetSelectQueryTextKbn((int)TBL_GENERALTEXTMF.TextKbn.ICREQRET);
                    DataTable tblRet = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tblRet.Rows.Count; i++)
                    {
                        TBL_GENERALTEXTMF ctl = new TBL_GENERALTEXTMF(tblRet.Rows[i]);
                        ICREQRETGeneralTextMF.Add(ctl);
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
        /// 持帰要求結果一覧取得
        /// </summary>
        public bool FetchIcreqCtlControl(int ListDispLimit, FormBase form = null)
        {
            // SELECT実行
            string strSQL = SQLSearch.Get_SearchIcreqCtl(DispParams.reqdate, AppInfo.Setting.SchemaBankCD, ListDispLimit);
            SearchData = new Dictionary<string, ListData>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                       TBL_ICREQ_CTL ctl = new TBL_ICREQ_CTL(tbl.Rows[i]);
                        string key = ctl._REQ_TXT_NAME;
                        SearchData.Add(key, new ListData(ctl, i));
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
        /// 汎用テキスト変換(ICREQデータ)
        /// </summary>
        public string GetGeneralTextICREQData(ICREQDataNo no, string value, DispType Type)
        {
            return GetGeneralText(TBL_GENERALTEXTMF.TextKbn.ICREQ, (int)TBL_GENERALTEXTMF.RecordKbn.DATA, (int)no, value, (int)Type);
        }

        /// <summary>
        /// 汎用テキスト変換(ICREQRETヘッダー)
        /// </summary>
        public string GetGeneralTextICREQRETHeader(ICREQRETHeaderNo no, string value, DispType Type)
        {
            return GetGeneralText(TBL_GENERALTEXTMF.TextKbn.ICREQRET, (int)TBL_GENERALTEXTMF.RecordKbn.HEADER, (int)no, value, (int)Type);
        }

        /// <summary>
        /// 汎用テキスト変換
        /// </summary>
        private string GetGeneralText(TBL_GENERALTEXTMF.TextKbn TextKbn, int recordkbn, int no, string value, int Type)
        {
            //対象データ取得
            List<TBL_GENERALTEXTMF> Target;
            if (TextKbn == TBL_GENERALTEXTMF.TextKbn.ICREQ)
            {
                Target = ICREQGeneralTextMF;
            }
            else
            {
                Target = ICREQRETGeneralTextMF;
            }
            IEnumerable<TBL_GENERALTEXTMF> list = Target.Where(x => x._RECORDKBN == recordkbn && x._NO == no && x._VALUE.Trim() == value.Trim());

            if (list.Count() == 0) return value;

            if (Type == 1)
            {
                return list.First().m_DESCRIPTION;
            }
            else if (Type == 2)
            {
                return list.First().m_ABBREVIATE;
            }
            else
            {
                return string.Format("{0}({1})", list.First().m_ABBREVIATE, value);
            }
        }

        /// <summary>
        /// 持帰要求結果詳細取得
        /// </summary>
        public bool GetICReqCtlDeatil(string reqtxtname)
        {
            // 初期化
            ICReqData = new TBL_ICREQ_CTL(AppInfo.Setting.SchemaBankCD);
            if (!SearchData.ContainsKey(reqtxtname)) return false;
            ICReqData = SearchData[reqtxtname].ICReqCtl;
            return true;
        }

        /// <summary>
        /// 現在より一つ古い持帰要求結果詳細取得
        /// </summary>
        public bool GetOldICReqCtlDeatil()
        {
            if (!SearchData.ContainsKey(ICReqData._REQ_TXT_NAME)) return false;
            int Order = SearchData[ICReqData._REQ_TXT_NAME].Order;
            if (SearchData.Count <= Order) return false;

            //一つ大きい項目を設定
            var NextData = SearchData.Where(x => x.Value.Order == Order + 1);
            if (NextData.Count() == 0) return false;
            ICReqData = NextData.First().Value.ICReqCtl;
            // 選択状況の反映
            DispParams.ReqTxtName = ICReqData._REQ_TXT_NAME;

            return true;
        }

        /// <summary>
        /// 現在より一つ新しい持帰要求結果詳細取得
        /// </summary>
        public bool GetNewICReqCtlDeatil()
        {
            if (!SearchData.ContainsKey(ICReqData._REQ_TXT_NAME)) return false;
            int Order = SearchData[ICReqData._REQ_TXT_NAME].Order;
            if (0 >= Order) return false;

            //一つ小さい項目を設定
            var PreData = SearchData.Where(x => x.Value.Order == Order - 1);
            if (PreData.Count() == 0) return false;
            ICReqData = PreData.First().Value.ICReqCtl;
            // 選択状況の反映
            DispParams.ReqTxtName = ICReqData._REQ_TXT_NAME;

            return true;
        }

        /// <summary>
        /// 一覧表示データクラス
        /// </summary>
        public class ListData
        {
            public TBL_ICREQ_CTL ICReqCtl { get; set; }
            public int Order { get; set; }

            public ListData(TBL_ICREQ_CTL icreq, int order)
            {
                ICReqCtl = icreq;
                Order = order;
            }
        }

    }
}
