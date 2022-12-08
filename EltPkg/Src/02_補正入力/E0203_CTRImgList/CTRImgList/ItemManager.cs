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

namespace CTRImgListForm
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;
        private Controller _ctl = null;

        private const int MEI_ROW_MAX = 10;

        public PageInfos PageInfo { get; set; }

        /// <summary>イメージカーソルパラメータ（key=DSP_ID,ITEM_ID, val=TBL_IMG_CURSOR_PARAM）</summary>
        private DataTable ImgCursorParams { get; set; } = null;

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            this.DispParams = new DisplayParams();
			this.DispParams.Clear();
            this.PageInfo = new PageInfos(1);
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void FetchAllData(Controller ctl)
        {
            _ctl = ctl;
            Fetch_img_cursor_params();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_img_cursor_params()
        {
            string strSQL = TBL_IMG_CURSOR_PARAM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    ImgCursorParams = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public bool Fetch_wk_trmei(int pageno)
        {
            int rowFrom = (MEI_ROW_MAX * (pageno - 1)) + 1;
            int rowTo = (MEI_ROW_MAX * pageno) + 1;

            //端末IPアドレスの取得
            string TermIPAddress = ImportFileAccess.GetTermIPAddress().Replace(".", "_");

            string strSQL = SQLEntry.GetMeisaiPageList(TermIPAddress, _ctl.GymId, rowFrom, rowTo);

            // SELECT実行
            DataTable tbl = null;
            PageInfo = new PageInfos(pageno);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // 取得するのは10件まで
                    DataTable tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    int max = Math.Min(tblMei.Rows.Count, MEI_ROW_MAX);
                    for (int i = 0; i < max; i++)
                    {
                        int no = i + 1;
                        DataRow row = tblMei.Rows[i];
                        MeisaiInfos info = new MeisaiInfos(DBConvert.ToIntNull(row["ROW_ID"]), no);
                        TBL_WK_IMGELIST wkmei = new TBL_WK_IMGELIST(row);
                        info.wk_trmei = wkmei;

                        // TBL_TRITEM
                        strSQL = TBL_TRITEM.GetSelectQuery(
                            wkmei._GYM_ID, wkmei._OPERATION_DATE, wkmei._SCAN_TERM, wkmei._BAT_ID, wkmei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                        DataTable tblItem = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                        foreach (DataRow rowItem in tblItem.Rows)
                        {
                            TBL_TRITEM item = new TBL_TRITEM(rowItem, AppInfo.Setting.SchemaBankCD);
                            info.tritems.Add(item._ITEM_ID, item);
                        }

                        // TBL_TRBATCH
                        strSQL = TBL_TRBATCH.GetSelectQuery(wkmei._GYM_ID, wkmei._OPERATION_DATE, wkmei._SCAN_TERM, wkmei._BAT_ID, AppInfo.Setting.SchemaBankCD);
                        tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                        if (tbl.Rows.Count > 0)
                        {
                            info.trbat = new TBL_TRBATCH(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                        }

                        // TBL_TRMEI
                        strSQL = TBL_TRMEI.GetSelectQuery(
                            wkmei._GYM_ID, wkmei._OPERATION_DATE, wkmei._SCAN_TERM, wkmei._BAT_ID, wkmei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                        tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                        if (tbl.Rows.Count > 0)
                        {
                            info.trmei = new TBL_TRMEI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                            if (info.trbat == null)
                            {
                                //後続のためtrmeiから作成
                                info.trbat = new TBL_TRBATCH(info.trmei._GYM_ID, info.trmei._OPERATION_DATE, info.trmei._SCAN_TERM, info.trmei._BAT_ID, AppInfo.Setting.SchemaBankCD);
                            }
                        }

                        // TBL_TRMEIIMG（表再送）
                        strSQL = TBL_TRMEIIMG.GetSelectQuery(
                            wkmei._GYM_ID, wkmei._OPERATION_DATE, wkmei._SCAN_TERM, wkmei._BAT_ID, wkmei._DETAILS_NO, TrMeiImg.ImgKbn.表再送分, AppInfo.Setting.SchemaBankCD);
                        tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                        if (tbl.Rows.Count > 0)
                        {
                            // 表再送イメージを優先表示する
                            info.trimg = new TBL_TRMEIIMG(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                        }
                        // TBL_TRMEIIMG（表）
                        if (info.trimg == null)
                        {
                            strSQL = TBL_TRMEIIMG.GetSelectQuery(
                                wkmei._GYM_ID, wkmei._OPERATION_DATE, wkmei._SCAN_TERM, wkmei._BAT_ID, wkmei._DETAILS_NO, TrMeiImg.ImgKbn.表, AppInfo.Setting.SchemaBankCD);
                            tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                            if (tbl.Rows.Count > 0)
                            {
                                info.trimg = new TBL_TRMEIIMG(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                            }
                        }

                        // TBL_IMG_PARAM
                        if (info.trmei != null)
                        {
                            strSQL = TBL_IMG_PARAM.GetSelectQuery(info.trmei._GYM_ID, info.trmei.m_DSP_ID, AppInfo.Setting.SchemaBankCD);
                            tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                            if (tbl.Rows.Count > 0)
                            {
                                info.imgparam = new TBL_IMG_PARAM(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                            }
                        }

                        PageInfo.wk_trmeis.Add(info.No, info);
                    }

                    // 1件多く取得して次のページがあるか判定する
                    PageInfo.IsNextPage = (tblMei.Rows.Count > MEI_ROW_MAX);

                    // 最大件数を取得する
                    strSQL = TBL_WK_IMGELIST.GetSelectQuery(TermIPAddress, _ctl.GymId);
                    tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    PageInfo.MaxPageNo = (int)Math.Ceiling(((double)tbl.Rows.Count/MEI_ROW_MAX));
                    PageInfo.MaxPageNo = (PageInfo.MaxPageNo < 1) ? 1 : PageInfo.MaxPageNo;
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public TBL_HOSEI_STATUS GetHoseiStatus(TBL_TRMEI mei, int hoseiinptmode)
        {
            string strSQL = TBL_HOSEI_STATUS.GetSelectQuery(
                mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, hoseiinptmode, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            TBL_HOSEI_STATUS hosei_status = null;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        hosei_status = new TBL_HOSEI_STATUS(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return null;
                }
            }
            return hosei_status;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public TBL_TRMEI GetTrMei(TBL_TRMEI mei)
        {
            string strSQL = TBL_TRMEI.GetSelectQuery(
                mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            TBL_TRMEI trmei = null;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        trmei = new TBL_TRMEI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return null;
                }
            }
            return trmei;
        }

        public TBL_IMG_CURSOR_PARAM GetImgCursorParams(int DspID, int ItemID)
        {
            DataRow[] filterRows = null;
            string filter = "";
            string sort = "";

            // イメージカーソルパラメータ取得
            filter += string.Format("GYM_ID={0}{1}", AppInfo.Setting.GymId, " AND ");
            filter += string.Format("DSP_ID={0}{1}", DspID, " AND ");
            filter += string.Format("ITEM_ID={0}{1}", ItemID, "");
            sort = "ITEM_ID";
            filterRows = ImgCursorParams.Select(filter, sort);
            if (filterRows.Length == 0) return new TBL_IMG_CURSOR_PARAM(AppInfo.Setting.SchemaBankCD);

            return new TBL_IMG_CURSOR_PARAM(filterRows[0], AppInfo.Setting.SchemaBankCD);
        }

        public class PageInfos
        {
            public int PageNo { get; private set; }
            public bool IsNextPage { get; set; }
            public int MaxPageNo { get; set; }
            public SortedDictionary<int, MeisaiInfos> wk_trmeis { get; set; }

            public PageInfos(int pageno)
            {
                PageNo = pageno;
                IsNextPage = false;
                MaxPageNo = PageNo;
                wk_trmeis = new SortedDictionary<int, MeisaiInfos>();
            }
        }

        public class MeisaiInfos
        {
            public int No { get; set; }
            public int Seq { get; set; }
            public TBL_WK_IMGELIST wk_trmei { get; set; }
            public SortedDictionary<int, TBL_TRITEM> tritems { get; set; }

            public TBL_TRBATCH trbat { get; set; }
            public TBL_TRMEI trmei { get; set; }
            public TBL_TRMEIIMG trimg { get; set; }
            public TBL_IMG_PARAM imgparam { get; set; }

            public MeisaiInfos(int seq, int no)
            {
                Seq = seq;
                No = no;
                tritems = new SortedDictionary<int, TBL_TRITEM>();
            }

            /// <summary>イメージファイルパス</summary>
            public string ImgFilePath
            {
                get
                {
                    if ((trbat == null) || (trimg == null)) { return ""; }
                    Dictionary<string, string> pathList = new Dictionary<string, string>();
                    pathList.Add("BankNormalImageRoot", ServerIni.Setting.BankNormalImageRoot);
                    pathList.Add("BankFutaiImageRoot", ServerIni.Setting.BankFutaiImageRoot);
                    pathList.Add("BankKijituImageRoot", ServerIni.Setting.BankKijituImageRoot);
                    pathList.Add("BankConfirmImageRoot", ServerIni.Setting.BankConfirmImageRoot);
                    return CommonUtil.GetImgFilePath(trbat, trimg, pathList);
                }
            }
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            public int Seq { get; set; }
            public void Clear()
			{
                this.Seq = 0;
            }
		}
    }
}
