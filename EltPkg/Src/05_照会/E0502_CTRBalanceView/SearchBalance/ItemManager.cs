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

namespace SearchBalance
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;

        public List<TBL_BANKMF> BankMF { get; set; }
        public Dictionary<string, TBL_BALANCETXT> Balance { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public override void FetchAllData()
        {
            FetchBankMF();
        }

        /// <summary>
        /// 銀行マスタ一覧取得
        /// </summary>
        public bool FetchBankMF(FormBase form = null)
        {
            // 初期化
            BankMF = new List<TBL_BANKMF>();

            // SELECT実行
            string strSQL = TBL_BANKMF.GetSelectQuery();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {

                        TBL_BANKMF ctl = new TBL_BANKMF(tbl.Rows[i]);
                        BankMF.Add(ctl);
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
        /// 銀行マスタ一覧取得
        /// </summary>
        public string GetBankName(int bkno, FormBase form = null)
        {
            return (BankMF.FirstOrDefault(x => x._BK_NO == bkno) ?? new TBL_BANKMF()).m_BK_NAME_KANJI;
        }

        /// <summary>
        /// 交換尻テキスト一覧取得
        /// </summary>
        public bool FetchBalanceText(string FileID, FormBase form = null)
        {
            // SELECT実行
            string strSQL = SQLSearch.Get_SearchBalanceCtl(FileID, NCR.Operator.BankCD.ToString("D4"), AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD);
            Balance = new Dictionary<string, TBL_BALANCETXT>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_BALANCETXT ctl = new TBL_BALANCETXT(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        string key = ctl._FILE_NAME + "_" + CommonUtil.PadLeft(ctl._BK_NO, Const.BANK_NO_LEN, "0");
                        Balance.Add(key, ctl);
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
    }
}
