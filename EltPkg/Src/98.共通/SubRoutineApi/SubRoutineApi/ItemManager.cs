using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using System.Linq;

namespace SubRoutineApi
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ItemManagerBase
    {
        /// <summary>銀行マスタ（key=BK_NO, val=TBL_BANKMF）</summary>
        public SortedDictionary<int, TBL_BANKMF> mst_banks { get; set; }
        /// <summary>支店マスタ（key=BR_NO, val=TBL_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_BRANCHMF> mst_branches { get; set; }
        /// <summary>種類マスタ（key=SYURUI_CODE, val=TBL_SYURUIMF）</summary>
        public SortedDictionary<int, TBL_SYURUIMF> mst_syuruimfs { get; set; }
        /// <summary>入力チェック用種類マスタ（key=SYURUI_CODE|DSP_ID, val=TBL_SYURUIMF）</summary>
        public Dictionary<string, TBL_SYURUIMF> mst_chksyuruimfs { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst) : base(mst)
        {
            mst_banks = new SortedDictionary<int, TBL_BANKMF>();
            mst_branches = new SortedDictionary<int, TBL_BRANCHMF>();
            mst_syuruimfs = new SortedDictionary<int, TBL_SYURUIMF>();
            mst_chksyuruimfs = new Dictionary<string, TBL_SYURUIMF>();
        }

		/// <summary>
		/// ＤＢからデータ取得してデータセットに格納
		/// </summary>
		public override void FetchAllData()
        {
        }

        /// <summary>
        /// データセットに設定
        /// </summary>
        public void FetchAllData(SortedDictionary<int, TBL_BANKMF> banks, SortedDictionary<int, TBL_BRANCHMF> branches, SortedDictionary<int, TBL_SYURUIMF> syuruimfs)
        {
            mst_banks = banks;
            mst_branches = branches;
            mst_syuruimfs = syuruimfs;
            // 入力チェック用の種類マスタ設定
            Fetch_mst_chksyuruimfs();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void Fetch_mst_banks()
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
        public void Fetch_mst_syuruimfs()
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
                    Fetch_mst_chksyuruimfs();
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
        }

        /// <summary>
        /// 入力チェック用の種類マスタ設定
        /// </summary>
        public void Fetch_mst_chksyuruimfs()
        {
            // TBL_SYURUIMFの展開
            foreach (TBL_SYURUIMF data in mst_syuruimfs.Values)
            {
                // 入力チェック用種類マスタ設定
                if (string.IsNullOrEmpty(data.m_INPUT_DSP_ID))
                {
                    // 入力可能画面番号が空の場合はDSPID箇所を空で登録
                    string Key = CommonUtil.GenerateKey("|", data._SYURUI_CODE, string.Empty);
                    mst_chksyuruimfs.Add(Key, data);
                }
                else
                {
                    // 入力可能画面番号が空以外は設定のDSPID毎に登録
                    foreach (string id in CommonUtil.Split(data.m_INPUT_DSP_ID, ",").Select(x => x.Trim()).Distinct().OrderBy(x => DBConvert.ToIntNull(x)))
                    {
                        if (int.TryParse(id, out int nid))
                        {
                            // 数値変換できる場合のみ登録
                            string Key = CommonUtil.GenerateKey("|", data._SYURUI_CODE, nid);
                            mst_chksyuruimfs.Add(Key, data);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void Fetch_mst_branches()
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
    }
}
