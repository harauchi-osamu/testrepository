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
using EntryClass;
using EntryCommon;

namespace ParamMaint
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ItemManagerBase
    {
        private Controller _ctl = null;

        public SortedDictionary<int, GymParamas> GymParam { get; set; }
        /// <summary>業務パラメーターマスタ</summary>
        public MasterDspParams MasterDspParam { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        private const string TMP_DSP_PARAM = "TMP_DSP_PARAM";
        private const string TMP_IMG_PARAM = "TMP_IMG_PARAM";
        private const string TMP_DSP_ITEM = "TMP_DSP_ITEM";
        private const string TMP_IMG_CURSOR_PARAM = "TMP_IMG_CURSOR_PARAM";
        private const string TMP_HOSEIMODE_PARAM = "TMP_HOSEIMODE_PARAM";
        private const string TMP_HOSEIMODE_DSP_ITEM = "TMP_HOSEIMODE_DSP_ITEM";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst) : base(mst)
        {
            this.DispParams = new DisplayParams();
			this.DispParams.ClearGym();
            GymParam = new SortedDictionary<int, GymParamas>();
            MasterDspParam = new MasterDspParams();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public override void FetchAllData()
        {
            Fetch_dsp_params();
            Fetch_img_params();
            Fetch_dsp_items();
            Fetch_img_cursor_params();
            Fetch_hosei_params();
            Fetch_hosei_items();
            Fetch_item_masters();
            Fetch_gym_param();

            // 画面パラメータ取得
            foreach (GymParamas gym in GymParam.Values)
            {
                SetDspDatas(gym);
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void FetchAllData(Controller ctl)
        {
            _ctl = ctl;
            FetchAllData();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_dsp_params()
        {
            string strSQL = TBL_DSP_PARAM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

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
            string strSQL = TBL_IMG_PARAM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

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
            string strSQL = TBL_DSP_ITEM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

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
            string strSQL = TBL_IMG_CURSOR_PARAM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

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
            string strSQL = TBL_HOSEIMODE_PARAM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

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
            string strSQL = TBL_HOSEIMODE_DSP_ITEM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

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
        private void Fetch_gym_param()
        {
            string strSQL = TBL_GYM_PARAM.GetSelectQuery(AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            GymParam = new SortedDictionary<int, GymParamas>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // TBL_GYM_PARAM
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    foreach (DataRow row in tbl.Rows)
                    {
                        TBL_GYM_PARAM data = new TBL_GYM_PARAM(row, AppInfo.Setting.SchemaBankCD);
                        GymParamas gym = new GymParamas(data._GYM_ID);
                        gym.gym_param = data;
                        GymParam.Add(gym._GYM_ID, gym);
                    }

                    // 画面パラメータ取得
                    foreach (GymParamas gym in GymParam.Values)
                    {
                        SetDspDatas(gym);
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
        /// 画面パラメーター取得
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        public void SetDspDatas(GymParamas gym)
        {
            DataRow[] filterRows = null;
            string filter = "";
            string sort = "";

            // 画面パラメータークリア
            gym.DspInfos.Clear();

            // 画面パラメータ
            filter = "";
            filter += string.Format("GYM_ID={0}{1}", gym._GYM_ID, "");
            sort = "DSP_ID";
            filterRows = MasterDspParam.dsp_params.Select(filter, sort);
            foreach (DataRow row in filterRows)
            {
                DspInfo dsp = new DspInfo(_ctl.HoseiItemMode, true);
                dsp.SetDspParam(row);
                // 0だといろいろ問題が起こるので1以上の値を設定する
                if (dsp.dsp_param.m_FONT_SIZE < 1)
                {
                    dsp.dsp_param.m_FONT_SIZE = 14;
                }

                // イメージパラメータ
                filter = "";
                filter += string.Format("GYM_ID={0}{1}", dsp._GYM_ID, " AND ");
                filter += string.Format("DSP_ID={0}{1}", dsp._DSP_ID, "");
                sort = "";
                filterRows = MasterDspParam.img_params.Select(filter, sort);
                if (filterRows.Length > 0)
                {
                    dsp.SetImgParam(filterRows[0]);
                    // 0だといろいろ問題が起こるので1以上の値を設定する
                    if (dsp.img_param.m_REDUCE_RATE < 1)
                    {
                        dsp.img_param.m_REDUCE_RATE = 1;
                    }
                }

                // 補正モードパラメータ
                filter = "";
                filter += string.Format("GYM_ID={0}{1}", dsp._GYM_ID, " AND ");
                filter += string.Format("DSP_ID={0}{1}", dsp._DSP_ID, " AND ");
                filter += string.Format("HOSEI_ITEMMODE={0}{1}", _ctl.HoseiItemMode, "");
                sort = "";
                filterRows = MasterDspParam.hosei_params.Select(filter, sort);
                if (filterRows.Length > 0)
                {
                    dsp.SetHoseiParam(filterRows[0]);
                }

                // 画面項目定義
                filter = "";
                filter += string.Format("GYM_ID={0}{1}", dsp._GYM_ID, " AND ");
                filter += string.Format("DSP_ID={0}{1}", dsp._DSP_ID, "");
                sort = "ITEM_ID";
                filterRows = MasterDspParam.dsp_items.Select(filter, sort);
                for (int j = 0; j < filterRows.Length; j++)
                {
                    dsp.AddDspItem(filterRows[j]);
                }

                // イメージカーソルパラメータ
                filter = "";
                filter += string.Format("GYM_ID={0}{1}", dsp._GYM_ID, " AND ");
                filter += string.Format("DSP_ID={0}{1}", dsp._DSP_ID, "");
                sort = "ITEM_ID";
                filterRows = MasterDspParam.img_cursor_params.Select(filter, sort);
                for (int j = 0; j < filterRows.Length; j++)
                {
                    dsp.AddImgCursor(filterRows[j]);
                }

                // 補正モード画面項目定義
                filter = "";
                filter += string.Format("GYM_ID={0}{1}", dsp._GYM_ID, " AND ");
                filter += string.Format("DSP_ID={0}{1}", dsp._DSP_ID, " AND ");
                filter += string.Format("HOSEI_ITEMMODE={0}{1}", _ctl.HoseiItemMode, "");
                sort = "ITEM_ID";
                filterRows = MasterDspParam.hosei_items.Select(filter, sort);
                for (int j = 0; j < filterRows.Length; j++)
                {
                    dsp.AddHoseiItem(filterRows[j]);
                }

                gym.DspInfos.Add(dsp._DSP_ID, dsp);
            }
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public int CheckDoneFlg()
        {
            int gymid = -1;

            string strSQL = "";
            strSQL += "SELECT MIN(" + TBL_GYM_PARAM.GYM_ID + ") AS GYM_ID FROM " + TBL_GYM_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
            strSQL += " WHERE   " + TBL_GYM_PARAM.DONE_FLG + " <> '1'";

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // TBL_GYM_PARAM
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        gymid = DBConvert.ToIntNull(tbl.Rows[0]["GYM_ID"]);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return gymid;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public bool CheckGymId(int gymid)
        {
            bool retVal = false;
            string strSQL = "";
            strSQL += "SELECT * FROM " + TBL_GYM_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
            strSQL += " WHERE " + TBL_GYM_PARAM.GYM_ID + "=" + gymid;

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // TBL_GYM_PARAM
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    retVal = (tbl.Rows.Count > 0);
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
        public bool CheckDspId(int gymid, int dspid)
        {
            bool retVal = false;
            string strSQL = TBL_DSP_PARAM.GetSelectQuery(gymid, dspid, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // TBL_DSP_PARAM
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    retVal = (tbl.Rows.Count > 0);
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
        /// 業務IDと関連レコード削除処理
        /// </summary>
        /// <param name="gymid"></param>
        /// <returns></returns>
        public bool DeleteGymID(int gymid)
        {
            // DELETE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    strSQL = "DELETE " + TBL_GYM_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE " + TBL_GYM_PARAM.GYM_ID + "=" + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_DSP_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_IMG_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_HOSEIMODE_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 画面IDと関連レコード削除処理
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="dspid"></param>
        /// <returns></returns>
        public bool DeleteDspID(int gymid, int dspid)
        {
            // DELETE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    strSQL = "DELETE " + TBL_DSP_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid + " AND DSP_ID=" + dspid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid + " AND DSP_ID=" + dspid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_IMG_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid + " AND DSP_ID=" + dspid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid + " AND DSP_ID=" + dspid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid + " AND DSP_ID=" + dspid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_HOSEIMODE_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid + " AND DSP_ID=" + dspid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    strSQL = "DELETE " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " WHERE GYM_ID=" + gymid + " AND DSP_ID=" + dspid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// GYM_PARAM のメンテフラグを更新する
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="isDone">false：未完了、true：完了</param>
        /// <returns></returns>
        public bool UpdateDoneFlg(int gymid, bool isDone)
        {
            // UPDATE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    string done = isDone ? "'1'" : "'0'";

                    strSQL = "";
                    strSQL += " UPDATE " + TBL_GYM_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                    strSQL += " SET ";
                    strSQL += "     " + TBL_GYM_PARAM.DONE_FLG + " = " + done + " ";
                    strSQL += " WHERE ";
                    strSQL += "     " + TBL_GYM_PARAM.GYM_ID + " = " + gymid;
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// GYM_PARAM 取得
        /// </summary>
        /// <param name="gymid"></param>
        public TBL_GYM_PARAM GetGymParam(int gymid)
        {
            string strSQL = TBL_GYM_PARAM.GetSelectQuery(gymid, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            TBL_GYM_PARAM gym_param = null;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    // TBL_GYM_PARAM
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
            return gym_param;
        }

        /// <summary>
        /// GYM_PARAM を登録する
        /// </summary>
        /// <param name="gym_param"></param>
        /// <returns></returns>
        public bool RegistGymParam(TBL_GYM_PARAM gym_param, bool isInsert)
        {
            string strSQL = "";
            if (isInsert)
            {
                strSQL = gym_param.GetInsertQuery();
            }
            else
            {
                strSQL = gym_param.GetUpdateQuery();
            }

            // UPDATE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// TRMEI が存在するかチェック
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="dspid"></param>
        /// <returns></returns>
        public bool CheckTrMeiExists(int gymid, int dspid)
        {
            bool retVal = false;
            string strSQL = "";
            strSQL += " SELECT * FROM " + TBL_TRMEI.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
            strSQL += " WHERE ";
            strSQL += "     " + TBL_TRMEI.GYM_ID + " = " + gymid;
            strSQL += " AND " + TBL_TRMEI.DSP_ID + " = " + dspid;

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    retVal = (tbl.Rows.Count > 0);
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
        /// GYM_PARAM と関連テーブルをコピーする
        /// </summary>
        /// <param name="form_gym_param"></param>
        /// <returns></returns>
        public bool CopyGymParam(TBL_GYM_PARAM form_gym_param)
        {
            string strSQL = "";
            string srcTableName = "";
            string dstTableName = "";

            // UPDATE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // 一時テーブル削除
                    DropTmpTable(dbp, auto);

                    // 一時テーブル作成
                    CreateTmpTable(dbp, auto);

                    // TBL_GYM_PARAM 登録
                    strSQL = form_gym_param.GetInsertQuery();
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // コピー元 から 一時テーブルに登録
                    {
                        // DSP_PARAM
                        srcTableName = TBL_DSP_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        dstTableName = TMP_DSP_PARAM;
                        strSQL = SQLEntry.GetInsertDstToTmp(srcTableName, dstTableName, TBL_DSP_PARAM.ALL_COLUMNS, DispParams.SrcGymId);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_PARAM
                        srcTableName = TBL_IMG_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        dstTableName = TMP_IMG_PARAM;
                        strSQL = SQLEntry.GetInsertDstToTmp(srcTableName, dstTableName, TBL_IMG_PARAM.ALL_COLUMNS, DispParams.SrcGymId);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // DSP_ITEM
                        srcTableName = TBL_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        dstTableName = TMP_DSP_ITEM;
                        strSQL = SQLEntry.GetInsertDstToTmp(srcTableName, dstTableName, TBL_DSP_ITEM.ALL_COLUMNS, DispParams.SrcGymId);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_CURSOR_PARAM
                        srcTableName = TBL_IMG_CURSOR_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        dstTableName = TMP_IMG_CURSOR_PARAM;
                        strSQL = SQLEntry.GetInsertDstToTmp(srcTableName, dstTableName, TBL_IMG_CURSOR_PARAM.ALL_COLUMNS, DispParams.SrcGymId);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // HOSEIMODE_PARAM
                        srcTableName = TBL_HOSEIMODE_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        dstTableName = TMP_HOSEIMODE_PARAM;
                        strSQL = SQLEntry.GetInsertDstToTmp(srcTableName, dstTableName, TBL_HOSEIMODE_PARAM.ALL_COLUMNS, DispParams.SrcGymId);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // HOSEIMODE_DSP_ITEM
                        srcTableName = TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        dstTableName = TMP_HOSEIMODE_DSP_ITEM;
                        strSQL = SQLEntry.GetInsertDstToTmp(srcTableName, dstTableName, TBL_HOSEIMODE_DSP_ITEM.ALL_COLUMNS, DispParams.SrcGymId);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }

                    // 一時テーブルのキー項目をコピー先のキーに更新
                    {
                        // TMP_DSP_PARAM
                        strSQL = "UPDATE " + TMP_DSP_PARAM + " SET GYM_ID = " + DispParams.GymId + " ";
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // TMP_IMG_PARAM
                        strSQL = "UPDATE " + TMP_IMG_PARAM + " SET GYM_ID = " + DispParams.GymId + " ";
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // TMP_DSP_ITEM
                        strSQL = "UPDATE " + TMP_DSP_ITEM + " SET GYM_ID = " + DispParams.GymId + " ";
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // TMP_IMG_CURSOR_PARAM
                        strSQL = "UPDATE " + TMP_IMG_CURSOR_PARAM + " SET GYM_ID = " + DispParams.GymId + " ";
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // TMP_HOSEIMODE_PARAM
                        strSQL = "UPDATE " + TMP_HOSEIMODE_PARAM + " SET GYM_ID = " + DispParams.GymId + " ";
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // TMP_HOSEIMODE_DSP_ITEM
                        strSQL = "UPDATE " + TMP_HOSEIMODE_DSP_ITEM + " SET GYM_ID = " + DispParams.GymId + " ";
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }

                    // 一時テーブルから コピー先に登録
                    {
                        // DSP_PARAM
                        srcTableName = TMP_DSP_PARAM;
                        dstTableName = TBL_DSP_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst1(srcTableName, dstTableName, TBL_DSP_PARAM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_PARAM
                        srcTableName = TMP_IMG_PARAM;
                        dstTableName = TBL_IMG_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst1(srcTableName, dstTableName, TBL_IMG_PARAM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // DSP_ITEM
                        srcTableName = TMP_DSP_ITEM;
                        dstTableName = TBL_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst1(srcTableName, dstTableName, TBL_DSP_ITEM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_CURSOR_PARAM
                        srcTableName = TMP_IMG_CURSOR_PARAM;
                        dstTableName = TBL_IMG_CURSOR_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst1(srcTableName, dstTableName, TBL_IMG_CURSOR_PARAM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // HOSEIMODE_PARAM
                        srcTableName = TMP_HOSEIMODE_PARAM;
                        dstTableName = TBL_HOSEIMODE_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst1(srcTableName, dstTableName, TBL_HOSEIMODE_PARAM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // HOSEIMODE_DSP_ITEM
                        srcTableName = TMP_HOSEIMODE_DSP_ITEM;
                        dstTableName = TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst1(srcTableName, dstTableName, TBL_HOSEIMODE_DSP_ITEM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }

                    // 一時テーブル削除
                    DropTmpTable(dbp, auto);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// DSP_PARAM と関連テーブルを登録する
        /// </summary>
        /// <param name="dsp"></param>
        /// <returns></returns>
        public bool RegistDspParam(DspParams dsp)
        {
            string strSQL = "";
            string srcTableName = "";
            string dstTableName = "";

            // UPDATE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // 一時テーブル削除
                    DropTmpTable(dbp, auto);

                    // 一時テーブル作成
                    CreateTmpTable(dbp, auto);

                    // 一時テーブルに登録
                    {
                        // DSP_PARAM
                        strSQL = dsp.dsp_param.GetInsertQuery(TMP_DSP_PARAM);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_PARAM
                        strSQL = dsp.img_param.GetInsertQuery(TMP_IMG_PARAM);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // DSP_ITEM
                        foreach (TBL_DSP_ITEM dsp_item in dsp.dsp_items.Values)
                        {
                            strSQL = dsp_item.GetInsertQuery(TMP_DSP_ITEM);
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }
                        // IMG_CURSOR_PARAM
                        foreach (TBL_IMG_CURSOR_PARAM img_cursor in dsp.img_coursors.Values)
                        {
                            strSQL = img_cursor.GetInsertQuery(TMP_IMG_CURSOR_PARAM);
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }
                        // HOSEIMODE_PARAM
                        strSQL = dsp.hosei_param.GetInsertQuery(TMP_HOSEIMODE_PARAM);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // HOSEIMODE_DSP_ITEM
                        foreach (TBL_HOSEIMODE_DSP_ITEM hosei_item in dsp.hosei_items.Values)
                        {
                            strSQL = hosei_item.GetInsertQuery(TMP_HOSEIMODE_DSP_ITEM);
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }
                    }

                    // レコード更新（UPDATE）
                    {
                        // DSP_PARAM
                        strSQL = dsp.dsp_param.GetUpdateQuery();
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_PARAM
                        strSQL = dsp.img_param.GetUpdateQuery();
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // DSP_ITEM
                        foreach (TBL_DSP_ITEM dsp_item in dsp.dsp_items.Values)
                        {
                            strSQL = dsp_item.GetUpdateQuery();
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }
                        // IMG_CURSOR_PARAM
                        foreach (TBL_IMG_CURSOR_PARAM img_cursor in dsp.img_coursors.Values)
                        {
                            strSQL = img_cursor.GetUpdateQuery();
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }
                        // HOSEIMODE_PARAM
                        strSQL = dsp.hosei_param.GetUpdateQuery();
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // HOSEIMODE_DSP_ITEM
                        foreach (TBL_HOSEIMODE_DSP_ITEM hosei_item in dsp.hosei_items.Values)
                        {
                            strSQL = hosei_item.GetUpdateQuery();
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }
                    }

                    // 登録テーブルにないレコードを登録（INSERT）
                    {
                        // DSP_PARAM
                        srcTableName = TMP_DSP_PARAM;
                        dstTableName = TBL_DSP_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst1(srcTableName, dstTableName, TBL_DSP_PARAM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_PARAM
                        srcTableName = TMP_IMG_PARAM;
                        dstTableName = TBL_IMG_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst1(srcTableName, dstTableName, TBL_IMG_PARAM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // HOSEIMODE_PARAM
                        srcTableName = TMP_HOSEIMODE_PARAM;
                        dstTableName = TBL_HOSEIMODE_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst5(srcTableName, dstTableName, TBL_HOSEIMODE_PARAM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // DSP_ITEM（GYM_ID、DSP_ID、ITEM_ID）
                        srcTableName = TMP_DSP_ITEM;
                        dstTableName = TBL_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst2(srcTableName, dstTableName, TBL_DSP_ITEM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_CURSOR_PARAM（GYM_ID、DSP_ID、ITEM_ID）
                        srcTableName = TMP_IMG_CURSOR_PARAM;
                        dstTableName = TBL_IMG_CURSOR_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst2(srcTableName, dstTableName, TBL_IMG_CURSOR_PARAM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // HOSEIMODE_DSP_ITEM（GYM_ID、DSP_ID、HOSEI_ITEMMODE、ITEM_ID）
                        srcTableName = TMP_HOSEIMODE_DSP_ITEM;
                        dstTableName = TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL = SQLEntry.GetInsertTmpToDst4(srcTableName, dstTableName, TBL_HOSEIMODE_DSP_ITEM.ALL_COLUMNS);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }

                    // 一時テーブルにないレコード削除（DELETE）
                    {
                        // HOSEIMODE_DSP_ITEM 削除
                        strSQL = SQLEntry.GetDeleteDspQuery4(TMP_HOSEIMODE_DSP_ITEM, TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD), dsp.hosei_param._GYM_ID, dsp.hosei_param._DSP_ID, dsp.hosei_param._HOSEI_ITEMMODE);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // IMG_CURSOR_PARAM 削除
                        strSQL = SQLEntry.GetDeleteDspQuery3(TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD), TBL_IMG_CURSOR_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD), dsp.dsp_param._GYM_ID, dsp.dsp_param._DSP_ID);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        // DSP_ITEM を削除
                        strSQL = SQLEntry.GetDeleteDspQuery3(TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD), TBL_DSP_ITEM.TABLE_NAME(AppInfo.Setting.SchemaBankCD), dsp.dsp_param._GYM_ID, dsp.dsp_param._DSP_ID);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }

                    // 一時テーブル削除
                    DropTmpTable(dbp, auto);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 一時テーブル作成
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void CreateTmpTable(AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            string strSQL = "";
            try
            {
                // TMP_DSP_PARAM
                strSQL = SQLEntry.GetCreateDSP_PARAM(TMP_DSP_PARAM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                // TMP_IMG_PARAM
                strSQL = SQLEntry.GetCreateIMG_PARAM(TMP_IMG_PARAM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                // TMP_DSP_ITEM
                strSQL = SQLEntry.GetCreateDSP_ITEM(TMP_DSP_ITEM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                // TMP_IMG_CURSOR_PARAM
                strSQL = SQLEntry.GetCreateIMG_CURSOR_PARAM(TMP_IMG_CURSOR_PARAM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                // TMP_HOSEIMODE_PARAM
                strSQL = SQLEntry.GetCreateHOSEIMODE_PARAM(TMP_HOSEIMODE_PARAM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                // TMP_HOSEIMODE_DSP_ITEM
                strSQL = SQLEntry.GetCreateHOSEIMODE_DSP_ITEM(TMP_HOSEIMODE_DSP_ITEM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch
            {
                // 何もしない
            }
        }

        /// <summary>
        /// 一時テーブル削除
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void DropTmpTable(AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            // 例外発生時に catch ブロックで ロールバックした後に、
            // finally ブロックで DROP TABLE を実行すると、
            // ロールバックされずに更新内容がコミットされてしまう。
            // そのため本メソッドは finally で実行せず、トランザクション開始前、終了後に呼んであげる
            string strSQL = "";
            try
            {
                strSQL = DBCommon.GetDropTmpTableSQL(TMP_DSP_PARAM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                strSQL = DBCommon.GetDropTmpTableSQL(TMP_IMG_PARAM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                strSQL = DBCommon.GetDropTmpTableSQL(TMP_DSP_ITEM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                strSQL = DBCommon.GetDropTmpTableSQL(TMP_IMG_CURSOR_PARAM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                strSQL = DBCommon.GetDropTmpTableSQL(TMP_HOSEIMODE_PARAM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                strSQL = DBCommon.GetDropTmpTableSQL(TMP_HOSEIMODE_DSP_ITEM);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch
            {
                // 何もしない
            }
        }



        public class GymParamas
        {
            public int _GYM_ID { get; private set; }
            public TBL_GYM_PARAM gym_param { get; set; }

            public SortedDictionary<int, DspInfo> DspInfos { get; set; }

            public GymParamas(int gymid)
            {
                _GYM_ID = gymid;
                gym_param = new TBL_GYM_PARAM(gymid, AppInfo.Setting.SchemaBankCD);
                DspInfos = new SortedDictionary<int, DspInfo>();
            }
        }

        public class DspParams
        {
            public TBL_DSP_PARAM dsp_param { get; set; }
            public TBL_HOSEIMODE_PARAM hosei_param { get; set; }
            public SortedDictionary<int, TBL_DSP_ITEM> dsp_items { get; set; }
            public SortedDictionary<int, TBL_HOSEIMODE_DSP_ITEM> hosei_items { get; set; }

            public TBL_IMG_PARAM img_param { get; set; }
            public SortedDictionary<int, TBL_IMG_CURSOR_PARAM> img_coursors { get; set; }
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
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            public bool IsDspUpdate { get; set; } = false;

            public string DspGymId { get { return GymId.ToString(Const.GYM_ID_LEN_STR); } }
            public int GymId { get; set; }
            public int PrevGymId { get; set; }
            public int NextGymId { get; set; }
            public int SrcGymId { get; set; }
            public int DstGymId { get; set; }
            public AplInfo.EditType ProcGymType { get; set; }

            public int DspId { get; set; }
            public int PrevDspId { get; set; }
            public int NextDspId { get; set; }
            public int SrcDspId { get; set; }
            public int DstDspId { get; set; }
            public AplInfo.EditType ProcDspType { get; set; }

            public void ClearGym()
			{
                GymId = 0;
                PrevGymId = 0;
                NextGymId = 0;
                SrcGymId = 0;
                DstGymId = 0;
                ProcGymType = AplInfo.EditType.NONE;
            }

            public void ClearDsp()
            {
                DspId = 0;
                PrevDspId = 0;
                NextDspId = 0;
                SrcDspId = 0;
                DstDspId = 0;
                ProcDspType = AplInfo.EditType.NONE;
            }
        }
    }
}
