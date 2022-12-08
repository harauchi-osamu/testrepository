using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Common;
using CommonClass;
using CommonTable.DB;

namespace EntryCommon
{
    /// <summary>
    /// マスタテーブル管理クラス
    /// </summary>
    public class MasterManager : ManagerBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MasterManager()
        {
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
        public TBL_GYM_PARAM GetGymParam(int gymid)
        {
            TBL_GYM_PARAM gym_param = new TBL_GYM_PARAM(AppInfo.Setting.SchemaBankCD);
            string strSQL = TBL_GYM_PARAM.GetSelectQuery(gymid, AppInfo.Setting.SchemaBankCD);

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
            return gym_param;
        }

        /// <summary>
        /// 電子交換ユーザー情報マスタを取得する
        /// </summary>
        /// <returns></returns>
        public TBL_CTRUSERINFO GetCtrUserInfo(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            TBL_CTRUSERINFO ctrinfo = new TBL_CTRUSERINFO(AppInfo.Setting.SchemaBankCD);
            string strSQL = TBL_CTRUSERINFO.GetSelectQueryDate(AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), non.Trans);
            if (tbl.Rows.Count > 0)
            {
                TBL_CTRUSERINFO baseinfo = new TBL_CTRUSERINFO(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);

                // 銀行コードを付加したユーザーを作成
                string newUserID = string.Format("{0}{1}", AppInfo.Setting.SchemaBankCD.ToString(Const.BANK_NO_LEN_STR), baseinfo._USERID);
                ctrinfo = new TBL_CTRUSERINFO(newUserID, baseinfo._S_DATE, baseinfo, AppInfo.Setting.SchemaBankCD);
            }
            return ctrinfo;
        }

    }
}
