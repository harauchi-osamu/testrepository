using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;
using EntryClass;

namespace CorrectInput
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ItemManagerBase
    {
        private Controller _ctl = null;

        public Controller Controller { get; set; }
        public EntryController EntController { get; set; }
        public EntryDspControl DspControl { get; set; }
        public EntryImageHandler ImageHandler { get; set; }
        public EntryInputChecker Checker { get; set; }
        public EntryDataUpdater Updater { get; set; }

        /// <summary>自動配信対象外明細リスト</summary>
        public List<string> IgnoreMeisaiList { get; set; }

        /// <summary>検索結果バッチ</summary>
        public SortedDictionary<string, BatchInfo> bat_manages { get; set; }
        /// <summary>エントリ中バッチ</summary>
        public BatchInfo CurBat { get; set; }
        /// <summary>業務パラメーター</summary>
        public TBL_GYM_PARAM gym_param { get; set; }
        /// <summary>銀行マスタ（key=BK_NO, val=TBL_BANKMF）</summary>
        public SortedDictionary<int, TBL_BANKMF> mst_banks { get; set; }
        /// <summary>支店マスタ（key=BR_NO, val=TBL_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_BRANCHMF> mst_branches { get; set; }
        /// <summary>交換証券種類マスタ（key=BILL_CODE, val=TBL_BILLMF）</summary>
        public SortedDictionary<int, TBL_BILLMF> mst_bills { get; set; }
        /// <summary>種類マスタ（key=SYURUI_CODE, val=TBL_BILLMF）</summary>
        public SortedDictionary<int, TBL_SYURUIMF> mst_syuruimfs { get; set; }
        /// <summary>支払人マスタ（key=BR_NO,ACCOUNT_NO, val=TBL_PAYERMF）</summary>
        public SortedDictionary<string, TBL_PAYERMF> mst_payermf { get; set; }
        /// <summary>項目定義（key=ITEM_ID, val=TBL_ITEM_MASTER）</summary>
        public SortedDictionary<int, TBL_ITEM_MASTER> mst_items { get; set; }
        /// <summary>サブルーチン（val=TBL_SUB_RTN）</summary>
        public List<TBL_SUB_RTN> sub_rtns { get; set; }
        /// <summary>読替マスタ（key=主キー, val=TBL_CHANGEMF）</summary>
        public SortedDictionary<string, TBL_CHANGEMF> mst_changes { get; set; }
        /// <summary>銀行読替マスタ（key=OLD_BK_NO, val=TBL_BKCHANGEMF）</summary>
        public SortedDictionary<int, TBL_BKCHANGEMF> mst_bk_changes { get; set; }
        /// <summary>交換証券種類変換マスタ（key=DSP_ID, val=TBL_CHANGE_BILLMF）</summary>
        public SortedDictionary<int, TBL_CHANGE_BILLMF> mst_chgbillmf { get; set; }

        /// <summary>業務パラメーターマスタ</summary>
        public MasterDspParams MasterDspParam { get; set; }

        /// <summary>バッチ画面パラメータ</summary>
        public DisplayParams DspParams { get; set; }
		/// <summary>エントリーパラメータ</summary>
		public EntryParams EntParams { get; set; }

        /// <summary>最後に選択した明細行</summary>
        public SelectedInfos SelectedInfo { get; set; }

        /// <summary>連続訂正対象一覧</summary>
        public List<RenzokuTeiseiInfo> wk_Renzokulist { get; set; }

        /// <summary>最終工程かどうか</summary>
        public bool IsLastProcess
        {
            get
            {
                bool isLast = false;
                isLast |= (DspParams.IsEntryExec && !DBConvert.ToBoolNull(CurBat.CurMei.CurDsp.hosei_param.m_VERY_MODE));
                // ベリファイ工程は常に最終工程
                //isLast |= (DspParams.IsVerifyExec && DBConvert.ToBoolNull(CurBat.CurMei.CurDsp.hosei_param.m_VERY_MODE));
                isLast |= (DspParams.IsVerifyExec);
                return isLast;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst) : base(mst)
        {
            DspParams = new DisplayParams();
			EntParams = new EntryParams();
			CurBat = null;
            bat_manages = new SortedDictionary<string, BatchInfo>();
            gym_param = null;
            mst_banks = new SortedDictionary<int, TBL_BANKMF>();
            mst_branches = new SortedDictionary<int, TBL_BRANCHMF>();
            mst_bills = new SortedDictionary<int, TBL_BILLMF>();
            mst_syuruimfs = new SortedDictionary<int, TBL_SYURUIMF>();
            mst_payermf = new SortedDictionary<string, TBL_PAYERMF>();
            mst_items = new SortedDictionary<int, TBL_ITEM_MASTER>();
            sub_rtns = new List<TBL_SUB_RTN>();
            mst_changes = new SortedDictionary<string, TBL_CHANGEMF>();
            mst_bk_changes = new SortedDictionary<int, TBL_BKCHANGEMF>();
            mst_chgbillmf = new SortedDictionary<int, TBL_CHANGE_BILLMF>();

            IgnoreMeisaiList = new List<string>();
            Controller = null;
            EntController = null;
            MasterDspParam = new MasterDspParams();
            SelectedInfo = new SelectedInfos();
            wk_Renzokulist = new List<RenzokuTeiseiInfo>();
        }

		/// <summary>
		/// ＤＢからデータ取得してデータセットに格納
		/// </summary>
		public override void FetchAllData()
        {
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void FetchAllData(Controller ctl)
        {
            _ctl = ctl;

            // マスタ読み込みを最初に実施
            Fetch_gym_param();
            Fetch_mst_banks();
            Fetch_mst_branches();
            Fetch_mst_bills();
            Fetch_mst_syuruimfs();
            Fetch_mst_payermfs();
            Fetch_mst_items();
            Fetch_sub_rtns();
            Fetch_mst_changes();
            Fetch_dsp_params();
            Fetch_img_params();
            Fetch_dsp_items();
            Fetch_img_cursor_params();
            Fetch_hosei_params();
            Fetch_hosei_items();
            Fetch_item_masters();
            Fetch_mst_bk_changes();
            Fetch_mst_chgbillmf();

            DspControl = new EntryDspControl(ctl);
            ImageHandler = new EntryImageHandler(ctl);
            Checker = new EntryInputChecker(ctl);
            Updater = new EntryDataUpdater(ctl);

        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_gym_param()
        {
            gym_param = new TBL_GYM_PARAM(AppInfo.Setting.SchemaBankCD);
            string strSQL = TBL_GYM_PARAM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        gym_param = new TBL_GYM_PARAM(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }
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
        private void Fetch_mst_banks()
        {
            mst_banks = new SortedDictionary<int, TBL_BANKMF>();
            string strSQL = TBL_BANKMF.GetSelectQuery();

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BANKMF data = new TBL_BANKMF(tbl.Rows[i]);
                        mst_banks.Add(data._BK_NO, data);
                    }
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
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_mst_syuruimfs()
        {
            mst_syuruimfs = new SortedDictionary<int, TBL_SYURUIMF>();
            string strSQL = TBL_SYURUIMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_SYURUIMF data = new TBL_SYURUIMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        mst_syuruimfs.Add(data._SYURUI_CODE, data);
                    }
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
        private void Fetch_mst_payermfs()
        {
            mst_payermf = new SortedDictionary<string, TBL_PAYERMF>();
            string strSQL = TBL_PAYERMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_PAYERMF data = new TBL_PAYERMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey(data._BR_NO, data._ACCOUNT_NO);
                        mst_payermf.Add(key, data);
                    }
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
        private void Fetch_mst_items()
        {
            mst_items = new SortedDictionary<int, TBL_ITEM_MASTER>();
            string strSQL = TBL_ITEM_MASTER.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_ITEM_MASTER data = new TBL_ITEM_MASTER(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        mst_items.Add(data._ITEM_ID, data);
                    }
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
        private void Fetch_mst_branches()
        {
            mst_branches= new SortedDictionary<int, TBL_BRANCHMF>();
            string strSQL = TBL_BRANCHMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BRANCHMF data = new TBL_BRANCHMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        mst_branches.Add(data._BR_NO, data);
                    }
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
        private void Fetch_sub_rtns()
        {
            sub_rtns = new List<TBL_SUB_RTN>();
            string strSQL = TBL_SUB_RTN.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_SUB_RTN data = new TBL_SUB_RTN(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        sub_rtns.Add(data);
                    }
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
        private void Fetch_mst_changes()
        {
            mst_changes = new SortedDictionary<string, TBL_CHANGEMF>();
            string strSQL = TBL_CHANGEMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_CHANGEMF data = new TBL_CHANGEMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey(data._OLD_BR_NO, data._OLD_ACCOUNT_NO);
                        mst_changes.Add(key, data);
                    }
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
        private void Fetch_dsp_params()
        {
            string strSQL = TBL_DSP_PARAM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    MasterDspParam.dsp_params = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
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
        private void Fetch_img_params()
        {
            string strSQL = TBL_IMG_PARAM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    MasterDspParam.img_params = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
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
        private void Fetch_dsp_items()
        {
            string strSQL = TBL_DSP_ITEM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    MasterDspParam.dsp_items = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
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
        private void Fetch_img_cursor_params()
        {
            string strSQL = TBL_IMG_CURSOR_PARAM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    MasterDspParam.img_cursor_params = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
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
        private void Fetch_hosei_params()
        {
            string strSQL = TBL_HOSEIMODE_PARAM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    MasterDspParam.hosei_params = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
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
        private void Fetch_hosei_items()
        {
            string strSQL = TBL_HOSEIMODE_DSP_ITEM.GetSelectQuery(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    MasterDspParam.hosei_items = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
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
        private void Fetch_item_masters()
        {
            string strSQL = TBL_ITEM_MASTER.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    MasterDspParam.item_masters = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
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
        private void Fetch_mst_bk_changes()
        {
            mst_bk_changes = new SortedDictionary<int, TBL_BKCHANGEMF>();
            string strSQL = TBL_BKCHANGEMF.GetSelectQuery();

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BKCHANGEMF data = new TBL_BKCHANGEMF(tbl.Rows[i]);
                        mst_bk_changes.Add(data._OLD_BK_NO, data);
                    }
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
        private void Fetch_mst_chgbillmf()
        {
            mst_chgbillmf = new SortedDictionary<int, TBL_CHANGE_BILLMF>();
            string strSQL = TBL_CHANGE_BILLMF.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_CHANGE_BILLMF data = new TBL_CHANGE_BILLMF(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        mst_chgbillmf.Add(data._DSP_ID, data);
                    }
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
        /// 連続訂正一覧
        /// </summary>
        public bool Fetch_renzokulist()
        {
            //端末IPアドレスの取得
            string TermIPAddress = ImportFileAccess.GetTermIPAddress().Replace(".", "_");

            string strSQL = TBL_WK_IMGELIST.GetSelectQuery(TermIPAddress, _ctl.GymId);
            List<TBL_WK_IMGELIST> wkList = new List<TBL_WK_IMGELIST>();
            wk_Renzokulist = new List<RenzokuTeiseiInfo>();

            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        wkList.Add(new TBL_WK_IMGELIST(tbl.Rows[i]));
                    }

                    int y = 1;
                    foreach (TBL_WK_IMGELIST wk in wkList.OrderBy(x => x.m_SORT_NO))
                    {

                        string key = CommonUtil.GenerateKey(wk._GYM_ID, wk._OPERATION_DATE, wk._SCAN_TERM, wk._BAT_ID, wk._DETAILS_NO);
                        wk_Renzokulist.Add(new RenzokuTeiseiInfo(y, key, wk));
                        y++;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
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

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public TBL_TRMEIIMG GetTrImg(TBL_TRMEI mei, int imgKbn)
        {
            string strSQL = TBL_TRMEIIMG.GetSelectQuery(
                mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, imgKbn, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            TBL_TRMEIIMG trimg = null;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        trimg = new TBL_TRMEIIMG(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return null;
                }
            }
            return trimg;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納（バッチデータ）
        /// </summary>
        public DataTable GetTrMei(BatchListForm.SearchType mode)
        {
            DataTable tbl = new DataTable();
            SQLEntry.SearchType type = SQLEntry.SearchType.Type1;
            if (mode == BatchListForm.SearchType.RemainData)
            {
                // 残表示
                type = SQLEntry.SearchType.Type1;
            }
            else if (mode == BatchListForm.SearchType.AllData)
            {
                // 全表示
                type = SQLEntry.SearchType.Type2;
            }
            string strSQL = SQLEntry.GetBatchListSelect(type, _ctl.GymId, _ctl.HoseiInputMode, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp1 = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    tbl = dbp1.SelectTable(strSQL, new List<IDbDataParameter>());
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return tbl;
                }
            }
            return tbl;
        }

        /// <summary>
        /// 自動配信バッチを取得
        /// </summary>
        /// <param name="dspMode"></param>
        /// <param name="gymid"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public TBL_HOSEI_STATUS GetAutoReceiveBatch(BatchListForm.ExecMode execMode, int gymid, List<string> ignoreMeisaiList)
        {
            if (execMode == BatchListForm.ExecMode.Teisei) { return null; }

            TBL_HOSEI_STATUS hosei_sts = null;
            SQLEntry.SearchType type = SQLEntry.SearchType.Type1;
            if (execMode == BatchListForm.ExecMode.FuncEnt)
            {
                // エントリ入力
                type = SQLEntry.SearchType.Type1;
            }
            else if (execMode == BatchListForm.ExecMode.FuncVfy)
            {
                // ベリファイ入力
                type = SQLEntry.SearchType.Type2;
            }

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
					// 2022.05.25 排他制御の競合抑止対応
					Random rnd = new Random();
					int mdSTEP = rnd.Next(1, 21);
					int cnt = 0;
					int chk = mdSTEP;
					
					// HOSEI_STATUSテーブルからSELECTする行数 = 取得する画面上で選択している行番号 + 1～20のランダムな値
					int nHoseiStatusRecordCnt = ignoreMeisaiList.Count + 1 + chk;
					string strSQL = SQLEntry.GetBatchListAutoReceiveSelect(type,
																		_ctl.GymId,
																		_ctl.HoseiInputMode,
																		AplInfo.OP_ID,
																		nHoseiStatusRecordCnt,
																		AppInfo.Setting.SchemaBankCD);

                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    foreach (DataRow row in tbl.Rows)
                    {
						if (cnt++ == chk)
						{
							chk += mdSTEP;
							// 自動配信対象外リストの明細は無視する
							TBL_HOSEI_STATUS sts = new TBL_HOSEI_STATUS(row, AppInfo.Setting.SchemaBankCD);
							string key = CommonUtil.GenerateKey(sts._GYM_ID, sts._OPERATION_DATE, sts._SCAN_TERM, sts._BAT_ID, sts._DETAILS_NO);
							if (ignoreMeisaiList.Contains(key))
							{
								continue;
							}
							hosei_sts = sts;
							break;
						}
                    }

					if (hosei_sts == null)
					{
						// HOSEI_STATUSテーブルからSELECTする行数 = 取得する画面上で選択している行番号
						strSQL = SQLEntry.GetBatchListAutoReceiveSelect(type,
																		_ctl.GymId,
																		_ctl.HoseiInputMode,
																		AplInfo.OP_ID,
																		ignoreMeisaiList.Count + 1,
																		AppInfo.Setting.SchemaBankCD);

						tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());

                    foreach (DataRow row in tbl.Rows)
                    {
                        // 自動配信対象外リストの明細は無視する
                        TBL_HOSEI_STATUS sts = new TBL_HOSEI_STATUS(row, AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey(sts._GYM_ID, sts._OPERATION_DATE, sts._SCAN_TERM, sts._BAT_ID, sts._DETAILS_NO);
                        if (ignoreMeisaiList.Contains(key))
                        {
                            continue;
                        }
                        hosei_sts = sts;
                        break;
                    }
					}
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return hosei_sts;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public TBL_HOSEI_STATUS GetHoseiStatus(int gymid, int operationdate, string scanterm, int batid, int detailsno, int hoseiinptmode)
        {
            string strSQL = TBL_HOSEI_STATUS.GetSelectQuery(gymid, operationdate, scanterm, batid, detailsno, hoseiinptmode, AppInfo.Setting.SchemaBankCD);
            TBL_HOSEI_STATUS sts = null;

            // SELECT実行
            using (AdoDatabaseProvider dbp1 = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp1.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0) { sts = new TBL_HOSEI_STATUS(tbl.Rows[0], AppInfo.Setting.SchemaBankCD); }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return null;
                }
            }
            return sts;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public SortedDictionary<int, TBL_HOSEI_STATUS> GetHoseiStatuses(int gymid, int operationdate, string scanterm, int batid, int detailsno)
        {
            string strSQL = TBL_HOSEI_STATUS.GetSelectQuery(gymid, operationdate, scanterm, batid, detailsno, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            SortedDictionary<int, TBL_HOSEI_STATUS> hosei_statuses = new SortedDictionary<int, TBL_HOSEI_STATUS>();
            using (AdoDatabaseProvider dbp1 = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp1.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_HOSEI_STATUS data = new TBL_HOSEI_STATUS(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        hosei_statuses.Add(data._HOSEI_INPTMODE, data);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return null;
                }
            }
            return hosei_statuses;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public SortedDictionary<int, TBL_HOSEI_STATUS> GetHoseiStatusesByKingaku(int gymid, int operationdate, string scanterm, int batid, int hoseiinputmode)
        {
            string strSQL = TBL_HOSEI_STATUS.GetSelectQueryHosei(
                gymid, operationdate, scanterm, batid, hoseiinputmode, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            SortedDictionary<int, TBL_HOSEI_STATUS> hosei_statuses = new SortedDictionary<int, TBL_HOSEI_STATUS>();
            using (AdoDatabaseProvider dbp1 = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp1.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_HOSEI_STATUS data = new TBL_HOSEI_STATUS(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        hosei_statuses.Add(data._DETAILS_NO, data);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return null;
                }
            }
            return hosei_statuses;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納（バッチデータ）
        /// </summary>
        public bool SetCurrentBat(int gymid, int opedate, string scannerid, int batid, int DetailsNo, AdoDatabaseProvider dbp2 = null, AdoAutoCommitTransaction auto2 = null, bool isDel = false)
        {
            // 持帰は TRBATCH がないので生成する
            if (gymid == GymParam.GymId.持帰)
            {
                TBL_TRBATCH trbatch = new TBL_TRBATCH(gymid, opedate, scannerid, batid, AppInfo.Setting.SchemaBankCD);
                BatchInfo bat = new BatchInfo(1, trbatch, DetailsNo);
                CurBat = bat;
                return true;
            }

            string strSQL = TBL_TRBATCH.GetSelectQuery(gymid, opedate, scannerid, batid, AppInfo.Setting.SchemaBankCD);

            // トランザクションありの場合は呼出元で例外補足
            if (dbp2 != null)
            {
                DataTable tbl = dbp2.SelectTable(strSQL, new List<IDbDataParameter>(), auto2.Trans);
                if (tbl.Rows.Count < 1)
                {
                    return true;
                }
                BatchInfo bat = new BatchInfo(1, tbl.Rows[0], DetailsNo);
                CurBat = bat;
                return true;
            }

            // トランザクションなし
            using (AdoDatabaseProvider dbp1 = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp1.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count < 1)
                    {
                        return true;
                    }
                    BatchInfo bat = new BatchInfo(1, tbl.Rows[0], DetailsNo);
                    CurBat = bat;
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
        /// ＤＢからデータ取得してデータセットに格納（明細データ）
        /// </summary>
        public MeisaiInfo SetCurrentMeisai(int gymid, int opedate, string scannerid, int batid, int detailsno)
        {
            if (CurBat == null) { return null; }
            if (CurBat.CurMei == null) { return null; }
            return CurBat.CurMei;
            //var meiInfos = CurBat.MeisaiInfos.Values.Where(p => p.trmei._DETAILS_NO == detailsno);
            //if (meiInfos.Count() < 1) { return null; }
            //MeisaiInfo mei = meiInfos.First();
            //CurBat.CurMeiSeq = mei.Seq;
            //return mei;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納（明細データ）
        /// </summary>
        public bool FetchTrDatas(BatchInfo bat, TBL_HOSEI_STATUS cursts, AdoDatabaseProvider dbp2 = null, AdoAutoCommitTransaction auto2 = null, bool isDel = false)
        {
            // トランザクションありの場合は呼出元で例外補足
            if (dbp2 != null)
            {
                SetTrdatas(bat, cursts, dbp2, auto2, isDel);
                return true;
            }

            // トランザクションなし
            using (AdoDatabaseProvider dbp1 = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    SetTrdatas(bat, cursts, dbp1, null, isDel);
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
        /// ＤＢからデータ取得してデータセットに格納（明細データ）
        /// </summary>
        public void FetchTrData(MeisaiInfo mei, AdoDatabaseProvider dbp2 = null, AdoAutoCommitTransaction auto2 = null, bool isDel = false)
        {
            // トランザクションありの場合は呼出元で例外補足
            if (dbp2 != null)
            {
                SetTrdata(mei, dbp2, auto2, isDel);
            }

            // トランザクションなし
            using (AdoDatabaseProvider dbp1 = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    SetTrdata(mei, dbp1, null, isDel);
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納（アイテム）
        /// </summary>
        public bool FetchTrItems(MeisaiInfo mei, AdoDatabaseProvider dbp2 = null, AdoAutoCommitTransaction auto2 = null)
        {
            // トランザクションありの場合は呼出元で例外補足
            if (dbp2 != null)
            {
                SetTrItems(mei, dbp2, auto2);
                return true;
            }

            // トランザクションなし
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    SetTrItems(mei, dbp, null);
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
        /// 明細取得（関連）
        /// 対象明細のみ取得
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void SetTrdatas(BatchInfo bat, TBL_HOSEI_STATUS cursts, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto, bool isDel = false)
        {
            DataTable tblMei = null;
            //DataTable tblHosSts = null;
            DataTable tblimg = null;
            string strSQL = "";

            // 明細クリア
            bat.Reset();

            // 明細取得
            strSQL = TBL_TRMEI.GetSelectQuery(
                bat._GYM_ID, bat._OPERATION_DATE, bat._SCAN_TERM, bat._BAT_ID, bat._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
            if (auto == null)
            {
                tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            }
            else
            {
                tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }

            //// 補正ステータス
            //strSQL = TBL_HOSEI_STATUS.GetSelectQuery(
            //    bat._GYM_ID, bat._OPERATION_DATE, bat._SCAN_TERM, bat._BAT_ID, bat._DETAILS_NO, _ctl.HoseiInputMode, AppInfo.Setting.SchemaBankCD);
            //if (auto == null)
            //{
            //    tblHosSts = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            //}
            //else
            //{
            //    tblHosSts = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
            //}

            // 明細イメージ取得
            strSQL = TBL_TRMEIIMG.GetSelectQuery(
                bat._GYM_ID, bat._OPERATION_DATE, bat._SCAN_TERM, bat._BAT_ID, bat._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
            if (auto == null)
            {
                tblimg = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            }
            else
            {
                tblimg = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }

            // 明細データ(対象明細のみ取得)
            if (tblMei.Rows.Count > 0)
            {
                // 明細情報生成
                DataRow trdataRow = tblMei.Rows[0];
                MeisaiInfo newMei = new MeisaiInfo(1, trdataRow, bat, _ctl.HoseiItemMode);

                // 補正ステータス
                // データ取得時のオリジナルデータを設定
                // (他端末との同時実行でステータス等が更新されるケースがあるため最新ではなく初期取得のデータを保持)
                newMei.SetHoseiStatus(cursts);
                //AddHoseiStatus(newMei, tblHosSts);

                // 明細イメージ
                AddImageInfo(newMei, tblimg);

                // 画面パラメーター取得
                SetDspDatas(newMei);

                // バッチに明細を追加
                bat.AddMeisaiInfo(newMei);
            }
            //for (int i = 0; i < tblMei.Rows.Count; i++)
            //{
            //    // 明細情報生成
            //    DataRow trdataRow = tblMei.Rows[i];
            //    MeisaiInfo newMei = new MeisaiInfo(i + 1, trdataRow, bat, _ctl.HoseiItemMode);

            //    // 補正ステータス
            //    AddHoseiStatus(newMei, tblHosSts);

            //    // 明細イメージ
            //    AddImageInfo(newMei, tblimg);

            //    // 画面パラメーター取得
            //    SetDspDatas(newMei);

            //    // ここではアイテムを取得しない
            //    //SetTrItems(mei, dbp, auto);

            //    // バッチに明細を追加
            //    bat.AddMeisaiInfo(newMei);
            //}

            tblMei.Clear();
            tblMei = null;
            //tblHosSts.Clear();
            //tblHosSts = null;
            tblimg.Clear();
            tblimg = null;
        }

        /// <summary>
        /// 明細取得（関連）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void SetTrdata(MeisaiInfo curMei, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto, bool isDel = false)
        {
            DataTable tblMei = null;
            //DataTable tblHosSts = null;
            DataTable tblimg = null;
            string strSQL = "";
            BatchInfo bat = curMei.ParentBat;
            TBL_HOSEI_STATUS orgsts = curMei.hosei_status;

            // アイテムクリア
            curMei.Reset();

            // 明細取得
            strSQL = TBL_TRMEI.GetSelectQuery(
                curMei._GYM_ID, curMei._OPERATION_DATE, curMei._SCAN_TERM, curMei._BAT_ID, curMei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
            if (auto == null)
            {
                tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            }
            else
            {
                tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }

            //// 補正ステータス
            //strSQL = TBL_HOSEI_STATUS.GetSelectQuery(
            //    curMei._GYM_ID, curMei._OPERATION_DATE, curMei._SCAN_TERM, curMei._BAT_ID, curMei._DETAILS_NO, _ctl.HoseiInputMode, AppInfo.Setting.SchemaBankCD);
            //if (auto == null)
            //{
            //    tblHosSts = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            //}
            //else
            //{
            //    tblHosSts = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
            //}

            // 明細イメージ取得
            strSQL = TBL_TRMEIIMG.GetSelectQuery(
                curMei._GYM_ID, curMei._OPERATION_DATE, curMei._SCAN_TERM, curMei._BAT_ID, curMei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
            if (auto == null)
            {
                tblimg = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            }
            else
            {
                tblimg = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }

            // 明細データ
            if (tblMei.Rows.Count > 0)
            {
                // 明細情報生成
                DataRow trdataRow = tblMei.Rows[0];
                curMei.UpdateTrMei(trdataRow);

                // 補正ステータス
                // オリジナルのデータを設定
                curMei.SetHoseiStatus(orgsts);
                //AddHoseiStatus(curMei, tblHosSts);

                // 明細イメージ
                AddImageInfo(curMei, tblimg);

                // 画面パラメーター取得
                SetDspDatas(curMei);
            }

            tblMei.Clear();
            tblMei = null;
            //tblHosSts.Clear();
            //tblHosSts = null;
            tblimg.Clear();
            tblimg = null;
        }

        /// <summary>
        /// 補正ステータスを明細情報に追加する
        /// 未使用
        /// </summary>
        /// <param name="newMei"></param>
        /// <param name="tblHosSts"></param>
        private void AddHoseiStatus(MeisaiInfo newMei, DataTable tblHosSts)
        {
            string filter = "";
            string sort = "";
            DataRow[] filterRows = null;

            // 不要な条件削除
            //filter += string.Format("GYM_ID={0}{1}", newMei._GYM_ID, " AND ");
            //filter += string.Format("OPERATION_DATE={0}{1}", newMei._OPERATION_DATE, " AND ");
            //filter += string.Format("SCAN_TERM='{0}'{1}", newMei._SCAN_TERM, " AND ");
            //filter += string.Format("BAT_ID={0}{1}", newMei._BAT_ID, " AND ");
            //filter += string.Format("DETAILS_NO={0}{1}", newMei._DETAILS_NO, " AND ");
            //filter += string.Format("HOSEI_INPTMODE={0}{1}", _ctl.HoseiInputMode, "");
            filter += string.Format("DETAILS_NO={0}{1}", newMei._DETAILS_NO, "");

            sort = "";
            filterRows = tblHosSts.Select(filter, sort);
            if (filterRows.Length > 0)
            {
                newMei.SetHoseiStatus(filterRows[0]);
            }
        }

        /// <summary>
        /// イメージ情報を明細情報に追加する
        /// </summary>
        /// <param name="newMei"></param>
        /// <param name="tblimg"></param>
        private void AddImageInfo(MeisaiInfo newMei, DataTable tblimg)
        {
            string filter = "";
            string sort = "";
            DataRow[] filterRows = null;

            // 不要な条件削除
            filter = "";
            //filter += string.Format("GYM_ID={0}{1}", newMei._GYM_ID, " AND ");
            //filter += string.Format("OPERATION_DATE={0}{1}", newMei._OPERATION_DATE, " AND ");
            //filter += string.Format("SCAN_TERM='{0}'{1}", newMei._SCAN_TERM, " AND ");
            //filter += string.Format("BAT_ID={0}{1}", newMei._BAT_ID, " AND ");
            filter += string.Format("DETAILS_NO={0}{1}", newMei._DETAILS_NO, "");
            sort = "IMG_KBN";
            filterRows = tblimg.Select(filter, sort);
            for (int j = 0; j < filterRows.Length; j++)
            {
                // イメージ情報生成
                DataRow trimgRow = filterRows[j];
                ImageInfo img = new ImageInfo(j + 1, trimgRow, newMei.ParentBat, newMei);
                newMei.AddImageInfo(img);
            }

            // 表再送分のイメージがある場合は、表イメージよりも優先的に表示する
            var imgs = newMei.ImageInfos.Values.Where(p => (p._IMG_KBN == TrMeiImg.ImgKbn.表再送分));
            if (imgs.Count() > 0)
            {
                newMei.CurImgSeq = imgs.First().Seq;
            }
        }

        /// <summary>
        /// 画面パラメーター取得
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        public void SetDspDatas(MeisaiInfo mei)
        {
            DataRow[] filterRows = null;
            string filter = "";
            string sort = "";

            // 画面パラメータークリア
            mei.ClearDsp();

            // 画面パラメータ
            // 不要な条件削除
            filter = "";
            //filter += string.Format("GYM_ID={0}{1}", mei._GYM_ID, " AND ");
            filter += string.Format("DSP_ID={0}{1}", mei._DSP_ID, "");
            sort = "";
            filterRows = MasterDspParam.dsp_params.Select(filter, sort);
            if (filterRows.Length > 0)
            {
                mei.CurDsp.SetDspParam(filterRows[0]);
                // 0だといろいろ問題が起こるので1以上の値を設定する
                if (mei.CurDsp.dsp_param.m_FONT_SIZE < 1)
                {
                    mei.CurDsp.dsp_param.m_FONT_SIZE = 14;
                }
            }

            // イメージパラメータ
            // 不要な条件削除
            filter = "";
            //filter += string.Format("GYM_ID={0}{1}", mei._GYM_ID, " AND ");
            filter += string.Format("DSP_ID={0}{1}", mei._DSP_ID, "");
            sort = "";
            filterRows = MasterDspParam.img_params.Select(filter, sort);
            if (filterRows.Length > 0)
            {
                mei.CurDsp.SetImgParam(filterRows[0]);
                // 0だといろいろ問題が起こるので1以上の値を設定する
                if (mei.CurDsp.img_param.m_REDUCE_RATE < 1)
                {
                    mei.CurDsp.img_param.m_REDUCE_RATE = 1;
                }
            }

            // 補正モードパラメータ
            // 不要な条件削除
            filter = "";
            //filter += string.Format("GYM_ID={0}{1}", mei._GYM_ID, " AND ");
            filter += string.Format("DSP_ID={0}{1}", mei._DSP_ID, " AND ");
            filter += string.Format("HOSEI_ITEMMODE={0}{1}", _ctl.HoseiItemMode, "");
            sort = "";
            filterRows = MasterDspParam.hosei_params.Select(filter, sort);
            if (filterRows.Length > 0)
            {
                mei.CurDsp.SetHoseiParam(filterRows[0]);
            }

            // 画面項目定義
            // 不要な条件削除
            filter = "";
            //filter += string.Format("GYM_ID={0}{1}", mei._GYM_ID, " AND ");
            filter += string.Format("DSP_ID={0}{1}", mei._DSP_ID, "");
            sort = "ITEM_ID";
            filterRows = MasterDspParam.dsp_items.Select(filter, sort);
            for (int j = 0; j < filterRows.Length; j++)
            {
                mei.CurDsp.AddDspItem(filterRows[j]);
            }

            // イメージカーソルパラメータ
            // 不要な条件削除
            filter = "";
            //filter += string.Format("GYM_ID={0}{1}", mei._GYM_ID, " AND ");
            filter += string.Format("DSP_ID={0}{1}", mei._DSP_ID, "");
            sort = "ITEM_ID";
            filterRows = MasterDspParam.img_cursor_params.Select(filter, sort);
            for (int j = 0; j < filterRows.Length; j++)
            {
                mei.CurDsp.AddImgCursor(filterRows[j]);
            }

            // 補正モード画面項目定義
            // 不要な条件削除
            filter = "";
            //filter += string.Format("GYM_ID={0}{1}", mei._GYM_ID, " AND ");
            filter += string.Format("DSP_ID={0}{1}", mei._DSP_ID, " AND ");
            filter += string.Format("HOSEI_ITEMMODE={0}{1}", _ctl.HoseiItemMode, "");
            sort = "ITEM_ID";
            filterRows = MasterDspParam.hosei_items.Select(filter, sort);
            for (int j = 0; j < filterRows.Length; j++)
            {
                mei.CurDsp.AddHoseiItem(filterRows[j]);
            }
        }

        /// <summary>
        /// アイテム取得
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void SetTrItems(MeisaiInfo mei, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            DataTable tblItem = null;
            string strSQL = "";

            // アイテムクリア
            mei.ClearItem();

            // アイテム取得
            strSQL = SQLEntry.GetEntryTrItemsSelect(mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, mei._DSP_ID, _ctl.HoseiItemMode, AppInfo.Setting.SchemaBankCD);
            if (auto == null)
            {
                tblItem = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            }
            else
            {
                tblItem = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            for (int j = 0; j < tblItem.Rows.Count; j++)
            {
                mei.AddTrItem(tblItem.Rows[j]);
            }
        }

        /// <summary>
        /// 明細と項目のみ取得する
        /// </summary>
        public void SetCurrentTrdataItem()
        {
            DataTable tblData = null;
            DataTable tblItem = null;
            string strSQL = "";

            MeisaiInfo mei = this.CurBat.CurMei;

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // 明細取得
                    strSQL = TBL_TRMEI.GetSelectQuery(
                        mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                    tblData = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tblData.Rows.Count > 0)
                    {
                        mei.trmei = null;
                        mei.trmei = new TBL_TRMEI(tblData.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }

                    // アイテム取得
                    strSQL = SQLEntry.GetEntryTrItemsSelect(mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, mei._DSP_ID, _ctl.HoseiItemMode, AppInfo.Setting.SchemaBankCD);
                    tblItem = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    mei.ClearItem();
                    for (int k = 0; k < tblItem.Rows.Count; k++)
                    {
                        mei.AddTrItem(tblItem.Rows[k]);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return;
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public long GetProofKingaku(MeisaiInfo mei)
        {
            long retVal = 0;

            string strSQL = "";
            string itemCol = "";
            if (DspParams.IsEntryExec)
            {
                itemCol = "ENT_DATA";
            }
            else
            {
                itemCol = "VFY_DATA";
            }
            strSQL = SQLEntry.GetEntryProofKingaku(mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, itemCol, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count < 1) { return 0; }
                    retVal = DBConvert.ToLongNull(tbl.Rows[0]["AMOUNT"]);
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return retVal;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public ProofInfo GetProofData(MeisaiInfo mei)
        {
            ProofInfo pi = new ProofInfo();
            string strSQL = "";
            string itemCol = "";
            if (DspParams.IsEntryExec)
            {
                itemCol = "ENT_DATA";
            }
            else if (_ctl.IsKanryouTeisei)
            {
                //完了訂正はEND_DATA
                itemCol = "END_DATA";
            }
            else
            {
                itemCol = "VFY_DATA";
            }
            strSQL = SQLEntry.GetEntryProofKingaku(mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, itemCol, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count < 1) { return pi; }
                    pi.BatId = CurBat._BAT_ID;
                    pi.BatCount = CurBat.trbatch.m_TOTAL_COUNT;
                    pi.BatAmount = CurBat.trbatch.m_TOTAL_AMOUNT;
                    pi.MeiCount = DBConvert.ToLongNull(tbl.Rows[0]["TOTAL_COUNT"]);
                    pi.MeiAmount = DBConvert.ToLongNull(tbl.Rows[0]["TOTAL_AMOUNT"]);
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return pi;
        }

        /// <summary>
        /// 状態復旧のためのステータス変更
        /// </summary>
        /// <param name="tbm"></param>
        /// <returns></returns>
        public bool UpdateRecoveryStatus(TBL_HOSEI_STATUS sts, int rcvSts)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // 排他制御
                    TBL_HOSEI_STATUS updSts = null;
                    {
                        // TBL_HOSEI_STATUS
                        strSQL = TBL_HOSEI_STATUS.GetSelectQuery(sts._GYM_ID, sts._OPERATION_DATE, sts._SCAN_TERM, sts._BAT_ID, sts._DETAILS_NO, _ctl.HoseiInputMode, AppInfo.Setting.SchemaBankCD);
                        strSQL += DBConvert.QUERY_LOCK;
                        DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        if (tbl.Rows.Count < 1)
                        {
                            return false;
                        }
                        updSts = new TBL_HOSEI_STATUS(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }

                    updSts.m_INPT_STS = rcvSts;
                    strSQL = updSts.GetUpdateQuery();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("状況復旧 [{0}]", strSQL), 1);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    // 取得した行ロックを解除するためロールバック
                    // メッセージボックス表示前に実施
                    auto.isCommitEnd = false;
                    auto.Trans.Rollback();
                    // メッセージ表示
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 読取専用にする ITEM_ID を取得する
        /// </summary>
        public List<int> GetReadOnlyItemIdList(int gymid, int opedate, string scanterm, int batid, int detailsno)
        {
            List<int> itemIdList = new List<int>();
            string strSQL = SQLEntry.GetReadOnlyItemId(gymid, opedate, scanterm, batid, detailsno, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    foreach (DataRow row in tbl.Rows)
                    {
                        TBL_ITEM_MASTER im = new TBL_ITEM_MASTER(row, AppInfo.Setting.SchemaBankCD);
                        itemIdList.Add(im._ITEM_ID);
                    }                    
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return itemIdList;
        }

        /// <summary>
        /// ベリファイあり業務かどうか判別する
        /// </summary>
        public bool IsVerifyGym()
        {
            bool isVfy = false;
            string strSQL = SQLEntry.GetVfyHoseiParam(_ctl.GymId, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    isVfy = (tbl.Rows.Count > 0);
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return isVfy;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public TBL_TRITEM GetTritem(int gymid, int opedate, string scanterm, int batid, int detailsno, int itemid)
        {
            TBL_TRITEM tritem = null;
            string strSQL = TBL_TRITEM.GetSelectQuery(gymid, opedate, scanterm, batid, detailsno, itemid, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        tritem = new TBL_TRITEM(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return tritem;
        }

        /// <summary>
        /// 複数バッチ・複数明細のエントリ処理で共通のフラグ
        /// </summary>
        public class DisplayParams
        {
            /// <summary>実行モード</summary>
            public BatchListForm.ExecMode ExecMode { get; set; }

            /// <summary>自動配信するかどうか</summary>
            public bool IsAutoReceiveBatch { get; set; }

            /// <summary>エントリー実行かどうか</summary>
            public bool IsEntryExec { get { return (ExecMode == BatchListForm.ExecMode.FuncEnt); } }
            /// <summary>ベリファイ実行かどうか</summary>
            public bool IsVerifyExec { get { return (ExecMode == BatchListForm.ExecMode.FuncVfy); } }

            /// <summary>オートチェックを停止するかどうか</summary>
            public bool IsAutoCheckStop { get; set; } = false;
            /// <summary>１つ前のバッチキー</summary>
            public string PrevBatKey { get; set; } = "";
            /// <summary>１つ前の明細キー</summary>
            public string PrevMeiKey { get; set; } = "";

            /// <summary>
            /// 初回のフラグクリア
            /// </summary>
            public void InitializeParams()
            {
                this.IsAutoCheckStop = false;
                this.PrevBatKey = "";
                this.PrevMeiKey = "";
            }
        }

        /// <summary>
        /// １バッチ・１明細のエントリ処理毎に初期化されるフラグ
        /// </summary>
        public class EntryParams
		{
            /// <summary>画面コントロール初期化フラグ</summary>
            public bool IsInitControl { get; set; } = false;
            /// <summary>画面イメージ初期化フラグ</summary>
            public bool IsInitImage { get; set; } = false;
            /// <summary>自動エンベリするか</summary>
            public bool IsAutoExec { get { return (IsAutoEntryExec || IsAutoVerifyExec); } }
            /// <summary>自動エントリするか</summary>
            public bool IsAutoEntryExec { get; set; }
            /// <summary>自動ベリファイするか</summary>
            public bool IsAutoVerifyExec { get; set; }
            /// <summary>バッチ内の明細が1つ以上更新されたか</summary>
            public bool IsBatchUpdate { get; set; } = false;
            /// <summary>明細の項目が1つ以上更新されたか</summary>
            public bool IsMeisaiUpdate { get { return (this.UpdateCount > 0); } }
            /// <summary>明細画面で変更した項目数</summary>
            public int UpdateCount { get; set; } = 0;
            /// <summary>イメージ回転情報</summary>
            public int ImageRotate { get; set; } = 0;
            /// <summary>終了ボタン押下による終了か</summary>
            public bool IsMeisaiSyuryo { get { return (ExecFunc == EntryFormBase.FuncType.終了); } }
            /// <summary>確定ボタン押下による終了か</summary>
            public bool IsMeisaiKakutei { get { return (ExecFunc == EntryFormBase.FuncType.確定); } }
            /// <summary>保留ボタン押下による終了か</summary>
            public bool IsMeisaiHoryu { get { return (ExecFunc == EntryFormBase.FuncType.保留); } }
            /// <summary>バッチ終了するか</summary>
            public bool IsBatchEnd { get; set; } = false;
            /// <summary>イメージを削除する</summary>
            public bool IsDeleteImage{ get; set; } = true;
            /// <summary>保存情報：補正状態（明細切替時にクリアしない）</summary>
            public int SaveInputSts { get; set; } = 0;
            ///// <summary>保存情報：端末番号（明細切替時にクリアしない）</summary>
            //public string SaveTerm { get; set; } = "";
            /// <summary>保存情報：オペレーターＩＤ（明細切替時にクリアしない）</summary>
            public string SaveOpeId { get; set; } = "";
            /// <summary>証券種類を変更したかどうか</summary>
            public bool IsChangeDsp { get; set; } = false;

            /// <summary>押下ファンクション</summary>
            public EntryFormBase.FuncType ExecFunc { get; set; }

            /// <summary>
            /// 初回のフラグクリア
            /// </summary>
            public void InitializeParams()
            {
                this.IsInitControl = false;
                SetNewBatch();
            }

            /// <summary>
            /// バッチ切替時のフラグクリア
            /// </summary>
            public void SetNewBatch()
            {
                this.IsBatchUpdate = false;
                this.IsBatchEnd = false;
                SetNewMeisai();
            }

            /// <summary>
            /// 明細切替時のフラグクリア
            /// </summary>
            public void SetNewMeisai()
            {
                this.IsInitImage = false;
                this.ImageRotate = 0;
                this.IsAutoEntryExec = false;
                this.IsAutoVerifyExec = false;
                this.UpdateCount = 0;
                this.ExecFunc = EntryFormBase.FuncType.未実行;
                this.IsDeleteImage = true;
                this.IsChangeDsp = false;
            }

            /// <summary>
            /// 回転情報を更新（時計回りに90°回転）
            /// ※補正エントリー画面で回転操作した場合にメモリ内の値を更新する為に使用
            /// </summary>
            public void ChangeRotate()
            {
                // インクリメント（90°回転）
                ImageRotate++;
                // 270°より大きくなった場合は0°に変更
                if (ImageRotate > 270)
                {
                    ImageRotate = 0;
                }
            }
        }

        public class ProofInfo
        {
            public int BatId { get; set; } = 0;
            public long BatCount { get; set; } = 0;
            public long BatAmount { get; set; } = 0;
            public long MeiCount { get; set; } = 0;
            public long MeiAmount { get; set; } = 0;
        }

        public class MasterDspParams
        {
            /// <summary>画面パラメータ（key=DSP_ID, val=TBL_DSP_PARAM）</summary>
            public DataTable dsp_params { get; set; } = null;

            /// <summary>イメージパラメータ（key=DSP_ID, val=TBL_IMG_PARAM）</summary>
            public DataTable img_params { get; set; } = null;

            /// <summary>画面項目定義（key=DSP_ID,ITEM_ID, val=TBL_DSP_ITEM）</summary>
            public DataTable dsp_items { get; set; } = null;

            /// <summary>イメージカーソルパラメータ（key=DSP_ID,ITEM_ID, val=TBL_IMG_CURSOR_PARAM）</summary>
            public DataTable img_cursor_params { get; set; } = null;

            /// <summary>補正モードパラメータ（key=DSP_ID,HOSEI_ITEMMODE, val=TBL_HOSEIMODE_PARAM）</summary>
            public DataTable hosei_params { get; set; } = null;

            /// <summary>補正モード画面項目定義（key=DSP_ID,HOSEI_ITEMMODE,ITEM_ID, val=TBL_HOSEIMODE_DSP_ITEM）</summary>
            public DataTable hosei_items { get; set; } = null;

            /// <summary>項目定義（key=ITEM_ID, val=TBL_ITEM_MASTER）</summary>
            public DataTable item_masters { get; set; } = null;
        }

        /// <summary>
        /// 選択行情報
        /// </summary>
        public class SelectedInfos
        {
            public string Key { get; set; } = "";
            public int RowIdx { get; set; } = -1;
        }

        public class RenzokuTeiseiInfo
        {
            public int Seq { get; set; }
            public string Key { get; set; }
            public TBL_WK_IMGELIST wk_list { get; set; }

            public RenzokuTeiseiInfo(int seq, string key, TBL_WK_IMGELIST list)
            {
                Seq = seq;
                Key = key;
                wk_list = list;
            }
        }

    }
}
