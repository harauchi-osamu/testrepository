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

namespace OcHoseiForce
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        public List<TBL_HOSEI_STATUS> hoseists { get; set; }
        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }
        /// <summary>更新パラメーター</summary>
        public UpdateParams UpdParams { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.DispParams = new DisplayParams();
            this.UpdParams = new UpdateParams();

            hoseists = new List<TBL_HOSEI_STATUS>();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public bool Fetch_HoseiSts(EntryCommonFormBase form)
        {
            //初期化
            hoseists = new List<TBL_HOSEI_STATUS>();

            List<int> InptMode = new List<int>();
            List<int> InptSts = new List<int>();

            // 取得対象のステータス設定
            InptSts.Add(HoseiStatus.InputStatus.エントリ待);
            InptSts.Add(HoseiStatus.InputStatus.エントリ保留);
            InptSts.Add(HoseiStatus.InputStatus.エントリ中);

            // 取得対象のモード設定
            if (DispParams.ICBk)
            {
                InptMode.Add(HoseiStatus.HoseiInputMode.持帰銀行);
            }
            if (DispParams.Amt)
            {
                InptMode.Add(HoseiStatus.HoseiInputMode.金額);
            }
            if (InptMode.Count() == 0) return true;

            // SELECT実行
            string strSQL = SQLEntry.GetOcHoseiForceSelect(AppInfo.Setting.GymId, InptMode, InptSts, AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        hoseists.Add(new TBL_HOSEI_STATUS(tbl.Rows[i], AppInfo.Setting.SchemaBankCD));
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }
        }
 
        /// <summary>
        /// 補正ステータスの強制完了
        /// </summary>
        public int UpdateHoseiSts(int Mode, AdoDatabaseProvider dbp, AdoNonCommitTransaction non, EntryCommonFormBase form)
        {
            List<int> InptMode = new List<int>();
            List<int> InptSts = new List<int>();

            // 取得対象のステータス設定
            InptSts.Add(HoseiStatus.InputStatus.エントリ待);
            InptSts.Add(HoseiStatus.InputStatus.エントリ保留);

            // 取得対象のモード設定
            InptMode.Add(Mode);

            try
            {
                // UPDATE実行
                string strSQL = SQLEntry.GetOcHoseiForceUpdate(AppInfo.Setting.GymId, HoseiStatus.InputStatus.完了, InptMode, InptSts, AppInfo.Setting.SchemaBankCD);
                return dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return -1;
            }
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public bool ICBk { get; set; } = true;
            public bool Amt { get; set; } = true;
        }

        /// <summary>
        /// 更新パラメーター
        /// </summary>
        public class UpdateParams
        {
            public int ICBkWait { get; set; } = 0;
            public int ICBkHoryu { get; set; } = 0;
            public int ICBkEnt { get; set; } = 0;
            public int AmtWait { get; set; } = 0;
            public int AmtHoryu { get; set; } = 0;
            public int AmtEnt { get; set; } = 0;
            public int ICBkUpdate { get; set; } = 0;
            public int AmtUpdate { get; set; } = 0;
        }

    }
}
