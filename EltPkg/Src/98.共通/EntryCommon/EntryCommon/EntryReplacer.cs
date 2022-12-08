using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;

namespace EntryCommon
{
    public class EntryReplacer
    {
        /// <summary>銀行マスタ（key=BK_NO, val=TBL_BANKMF）</summary>
        public SortedDictionary<int, TBL_BANKMF> mst_banks { get; set; }

        /// <summary>銀行読替マスタ（key=OLD_BK_NO, val=TBL_BKCHANGEMF）</summary>
        public SortedDictionary<int, TBL_BKCHANGEMF> mst_bk_changes { get; set; }

        /// <summary>支店マスタ（key=BR_NO, val=TBL_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_BRANCHMF> mst_branches { get; set; }

        /// <summary>交換証券種類マスタ（key=BILL_CODE, val=TBL_BILLMF）</summary>
        public SortedDictionary<int, TBL_BILLMF> mst_bills { get; set; }

        /// <summary>種類マスタ（key=SYURUI_CODE, val=TBL_BILLMF）</summary>
        public SortedDictionary<int, TBL_SYURUIMF> mst_syuruimfs { get; set; }

        /// <summary>読替マスタ（key=主キー, val=TBL_CHANGEMF）</summary>
        public SortedDictionary<string, TBL_CHANGEMF> mst_br_changes { get; set; }

        /// <summary>交換証券種類変換マスタ（key=DSP_ID, val=TBL_CHANGE_BILLMF）</summary>
        public SortedDictionary<int, TBL_CHANGE_BILLMF> mst_chgbillmf { get; set; }

        /// <summary>支払人マスタ（key=主キー, val=PAYERMF）</summary>
        public SortedDictionary<string, TBL_PAYERMF> mst_payermf { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryReplacer()
        {
            // 休日カレンダー取得
            iBicsCalendar cal = new iBicsCalendar();
            cal.SetHolidays();

            // 各種マスタ取得
            FetchAllData();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>マスタ定義を別箇所から取得</remarks>
        public EntryReplacer(SortedDictionary<int, TBL_BANKMF> banks, SortedDictionary<int, TBL_BKCHANGEMF> bk_changes, 
                             SortedDictionary<int, TBL_BRANCHMF> branches, SortedDictionary<int, TBL_BILLMF> bills, 
                             SortedDictionary<int, TBL_SYURUIMF> syuruimfs, SortedDictionary<string, TBL_CHANGEMF> br_changes, 
                             SortedDictionary<int, TBL_CHANGE_BILLMF> chgbillmf, SortedDictionary<string, TBL_PAYERMF> payermf,
                             bool GetHolidays)
        {
            if (GetHolidays)
            {
                // 休日カレンダー取得
                iBicsCalendar cal = new iBicsCalendar();
                cal.SetHolidays();
            }

            // 各種マスタ設定

            // 銀行マスタ
            mst_banks = banks;
            // 銀行読替マスタ
            mst_bk_changes = bk_changes;
            // 支店マスタ
            mst_branches = branches;
            // 交換証券種類マスタ
            mst_bills = bills;
            // 種類マスタ
            mst_syuruimfs = syuruimfs;
            // 読替マスタ
            mst_br_changes = br_changes;
            // 交換証券種類変換マスタ
            mst_chgbillmf = chgbillmf;
            // 支払人マスタ
            mst_payermf = payermf;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 読み替え処理を行う
        /// 　IN ：券面持帰銀行コード
        /// 　OUT：持帰銀行コード
        /// 　OUT：持帰銀行名
        /// </summary>
        /// <param name="bkno"></param>
        /// <param name="bknolen">銀行番号の長さ</param>
        /// <param name="txtBkCd"></param>
        /// <param name="txtBkName"></param>
        public void ReplaceBankCd(string bkno, int bknolen, ref string sBkCd, ref string sBkName)
        {
            sBkCd = GetYomikaeBkNo(DBConvert.ToIntNull(bkno), bknolen);
            sBkName = GetBankName(DBConvert.ToIntNull(sBkCd));
        }

        /// <summary>
        /// 読み替え処理を行う
        /// 　IN ：券面持帰銀行コード
        /// 　OUT：持帰銀行コード
        /// 　OUT：持帰銀行名
        /// </summary>
        /// <param name="bkno"></param>
        /// <param name="bknolen">銀行番号の長さ</param>
        /// <param name="txtBkCd"></param>
        /// <param name="txtBkName"></param>
        public void ReplaceBankCd(string bkno, int bknolen, TextBox txtBkCd, TextBox txtBkName)
        {
            if (bkno.Length == 0)
            {
                txtBkCd.Text = "";
                txtBkName.Text = "";
            }
            else
            {
                txtBkCd.Text = GetYomikaeBkNo(DBConvert.ToIntNull(bkno), bknolen);
                txtBkName.Text = GetBankName(DBConvert.ToIntNull(txtBkCd.Text));
            }
        }

        /// <summary>
        /// 読み替え処理を行う
        /// 　IN ：交換証券種類コード
        /// 　OUT：交換証券種類名
        /// </summary>
        /// <param name="billCd"></param>
        /// <param name="txtBillName"></param>
        public void ReplaceBillName(string billCd, TextBox txtBillName)
        {
            txtBillName.Text = GetBillName(DBConvert.ToIntNull(billCd));
        }

        /// <summary>
        /// 読み替え処理を行う
        /// 　IN ：手形種類コード
        /// 　OUT：手形種類名
        /// </summary>
        /// <param name="tegataCd"></param>
        /// <param name="txtTegataName"></param>
        public void ReplaceTegataName(string tegataCd, TextBox txtTegataName)
        {
            txtTegataName.Text = GetTegataName(DBConvert.ToIntNull(tegataCd));
        }

        /// <summary>
        /// 読み替え処理を行う
        /// 　IN ：入力交換希望日
        /// 　OUT：和暦交換希望日
        /// 　OUT：交換日
        /// </summary>
        /// <param name="clearingDate"></param>
        /// <param name="sWarekiDate"></param>
        /// <param name="sBusinessDate"></param>
        public void ReplaceClearingDate(string clearingDate, ref string sWarekiDate, ref string sBusinessDate)
        {
            int nClearingDate = DBConvert.ToIntNull(clearingDate);

            // 和暦算出
            iBicsCalendar cal = new iBicsCalendar();
            string gengo = cal.getGengo(nClearingDate);
            if (DBConvert.ToIntNull(gengo) >= 0)
            {
                string wareki = iBicsCalendar.datePlanetoDisp3(DBConvert.ToStringNull(cal.getWareki(nClearingDate)));
                sWarekiDate = string.Format("{0} {1}", gengo, wareki);
            }

            // 営業日算出
            int bizDate = Calendar.GetSettleDay(nClearingDate);
            if (bizDate >= 0)
            {
                sBusinessDate = CommonUtil.ConvToDateFormat(bizDate, 3);
            }
            else
            {
                sWarekiDate = "";
            }
        }

        /// <summary>
        /// 読み替え処理を行う(営業日の書式変更なし)
        /// 　IN ：入力交換希望日
        /// 　OUT：和暦交換希望日
        /// 　OUT：交換日
        /// </summary>
        /// <param name="clearingDate"></param>
        /// <param name="sWarekiDate"></param>
        /// <param name="sBusinessDate"></param>
        public void ReplaceClearingDateOrg(string clearingDate, ref string sWarekiDate, ref string sBusinessDate)
        {
            ReplaceClearingDate(clearingDate, ref sWarekiDate, ref sBusinessDate);
            sBusinessDate = sBusinessDate.Replace(".", "");
        }

        /// <summary>
        /// 読み替え処理を行う
        /// 　IN ：入力交換希望日
        /// 　OUT：和暦交換希望日
        /// 　OUT：交換日
        /// </summary>
        /// <param name="clearingDate"></param>
        /// <param name="txtWarekiDate"></param>
        /// <param name="txtBusinessDate"></param>
        public void ReplaceClearingDate(string clearingDate, TextBox txtWarekiDate, TextBox txtBusinessDate)
        {
            string sWarekiDate = "";
            string sBusinessDate = "";
            ReplaceClearingDate(clearingDate, ref sWarekiDate, ref sBusinessDate);
            txtWarekiDate.Text = sWarekiDate;
            txtBusinessDate.Text = sBusinessDate;
        }

        /// <summary>
        /// 読み替え処理を行う
        /// 　IN ：持帰支店コード
        /// 　OUT：持帰支店名
        /// </summary>
        /// <param name="brno"></param>
        /// <param name="txtBrName"></param>
        public void ReplaceBrName(string brno, TextBox txtBrName)
        {
            txtBrName.Text = GetBranchName(DBConvert.ToIntNull(brno));
        }

        /// <summary>
        /// 読み替え処理を行う（口座番号変更時）
        /// 　IN ：券面持帰支店コード
        /// 　IN ：券面口座番号
        /// 　OUT：持帰支店コード
        /// 　OUT：持帰支店名
        /// 　OUT：口座番号
        /// 　OUT：支払人
        /// </summary>
        /// <param name="brno"></param>
        /// <param name="kozaNo"></param>
        /// <param name="kozalen">口座番号の長さ</param>
        /// <param name="brnolen">支店番号の長さ</param>
        /// <param name="sBrCd"></param>
        /// <param name="sBrName"></param>
        /// <param name="sKozaNo"></param>
        /// <param name="sPayer"></param>
        public void ReplaceBrName(string brno, string kozaNo, int kozalen, int brnolen, ref string sBrCd, ref string sBrName, ref string sKozaNo, ref string sPayer)
        {
            sBrCd = GetYomikaeBrNo(DBConvert.ToIntNull(brno), DBConvert.ToLongNull(kozaNo), brnolen);
            sBrName = GetBranchName(DBConvert.ToIntNull(sBrCd));
            sKozaNo = GetYomikaeKozaName(DBConvert.ToIntNull(brno), DBConvert.ToLongNull(kozaNo), kozalen);
            sPayer = GetPayerName(DBConvert.ToIntNull(sBrCd), DBConvert.ToLongNull(sKozaNo));
        }

        /// <summary>
        /// 読み替え処理を行う（支店番号・口座番号変更時）
        /// 　IN ：券面持帰支店コード
        /// 　IN ：券面口座番号
        /// 　OUT：持帰支店コード
        /// 　OUT：持帰支店名
        /// 　OUT：口座番号
        /// 　OUT：支払人
        /// </summary>
        /// <param name="brno"></param>
        /// <param name="kozaNo"></param>
        /// <param name="kozalen">口座番号の長さ</param>
        /// <param name="brnolen">支店番号の長さ</param>
        /// <param name="txtBrCd"></param>
        /// <param name="txtBrName"></param>
        /// <param name="txtKozaNo"></param>
        /// <param name="txtPayer"></param>
        public void ReplaceBrName(string brno, string kozaNo, int kozalen, int brnolen, TextBox txtBrCd, TextBox txtBrName, TextBox txtKozaNo, TextBox txtPayer)
        {
            if (string.IsNullOrEmpty(kozaNo))
            {
                txtBrCd.Text = GetYomikaeBrNo(DBConvert.ToIntNull(brno), DBConvert.ToLongNull(kozaNo), brnolen);
            }
            else
            {
                txtBrCd.Text = GetYomikaeBrNo(DBConvert.ToIntNull(brno), DBConvert.ToLongNull(kozaNo), brnolen);
            }
            txtBrName.Text = GetBranchName(DBConvert.ToIntNull(txtBrCd.Text));
            if (txtKozaNo != null)
            {
                txtKozaNo.Text = GetYomikaeKozaName(DBConvert.ToIntNull(brno), DBConvert.ToLongNull(kozaNo), kozalen);
            }

            if (txtKozaNo != null && txtPayer != null)
            {
                txtPayer.Text = GetPayerName(DBConvert.ToIntNull(txtBrCd.Text), DBConvert.ToLongNull(txtKozaNo.Text));
            }
        }

        /// <summary>
        /// 銀行名を取得する
        /// </summary>
        /// <param name="bkno"></param>
        /// <returns></returns>
        public string GetBankName(int bkno)
        {
            if (!mst_banks.ContainsKey(bkno)) { return ""; }
            return mst_banks[bkno].m_BK_NAME_KANJI;
        }

        /// <summary>
        /// 電子交換加盟銀行名を取得する
        /// </summary>
        /// <param name="bkno"></param>
        /// <returns></returns>
        public string GetKameiBankName(int bkno)
        {
            if (!mst_banks.ContainsKey(bkno)) { return ""; }
            if (mst_banks[bkno].m_KAMEI_FLG == 0) { return ""; }
            return mst_banks[bkno].m_BK_NAME_KANJI;
        }

        /// <summary>
        /// 読替銀行マスタから読替銀行番号を取得する
        /// </summary>
        /// <param name="bkno"></param>
        /// <param name="bknolen">銀行番号の長さ</param>
        /// <returns></returns>
        public string GetYomikaeBkNo(int bkno, int bknolen)
        {
            if (!mst_bk_changes.ContainsKey(bkno)) { return CommonUtil.PadLeft(bkno.ToString(), bknolen, "0"); }
            return CommonUtil.PadLeft(mst_bk_changes[bkno].m_NEW_BK_NO.ToString(), bknolen, "0");
        }

        /// <summary>
        /// 支店名を取得する
        /// </summary>
        /// <param name="brno"></param>
        /// <returns></returns>
        public string GetBranchName(int brno)
        {
            if (!mst_branches.ContainsKey(brno)) { return ""; }
            return mst_branches[brno].m_BR_NAME_KANJI;
        }

        /// <summary>
        /// 証券種類名を取得する
        /// </summary>
        /// <param name="billcd"></param>
        /// <returns></returns>
        public string GetBillName(int billcd)
        {
            if (!mst_bills.ContainsKey(billcd)) { return ""; }
            return mst_bills[billcd].m_STOCK_NAME;
        }

        /// <summary>
        /// 手形種類名を取得する
        /// </summary>
        /// <param name="shuruicd"></param>
        /// <returns></returns>
        public string GetTegataName(int shuruicd)
        {
            if (!mst_syuruimfs.ContainsKey(shuruicd)) { return ""; }
            return mst_syuruimfs[shuruicd].m_SYURUI_NAME;
        }

        /// <summary>
        /// 読替マスタから新支店番号を取得する
        /// </summary>
        /// <param name="brno"></param>
        /// <param name="kozaNo"></param>
        /// <param name="brnolen">支店番号の長さ</param>
        /// <returns></returns>
        public string GetYomikaeBrNo(int brno, long kozaNo, int brnolen)
        {
            string key = CommonUtil.GenerateKey(brno, kozaNo);
            if (!mst_br_changes.ContainsKey(key)) { return CommonUtil.PadLeft(brno.ToString(), brnolen, "0"); }
            return CommonUtil.PadLeft(mst_br_changes[key].m_NEW_BR_NO.ToString(), brnolen, "0");
        }

        /// <summary>
        /// 読替マスタから新口座番号を取得する
        /// </summary>
        /// <param name="brno"></param>
        /// <param name="kozaNo"></param>
        /// <param name="kozalen">口座番号の長さ</param>
        /// <returns></returns>
        public string GetYomikaeKozaName(int brno, long kozaNo, int kozalen)
        {
            string key = CommonUtil.GenerateKey(brno, kozaNo);
            if (!mst_br_changes.ContainsKey(key)) { return CommonUtil.PadLeft(kozaNo.ToString(), kozalen, "0"); }
            return CommonUtil.PadLeft(mst_br_changes[key].m_NEW_ACCOUNT_NO.ToString(), kozalen, "0");
        }

        /// <summary>
        /// 交換証券種類変換マスタから交換証券種類を取得する
        /// </summary>
        /// <param name="dspid"></param>
        /// <returns></returns>
        public int GetDspIDBillCode(int dspid)
        {
            if (!mst_chgbillmf.ContainsKey(dspid)) { return -1; }
            return mst_chgbillmf[dspid].m_BILL_CODE;
        }

        /// <summary>
        /// 支払人マスタから支払人名を取得する
        /// </summary>
        /// <param name="brno"></param>
        /// <param name="kozaNo"></param>
        /// <returns></returns>
        public string GetPayerName(int brno, long kozaNo)
        {
            string key = CommonUtil.GenerateKey(brno, kozaNo);
            if (!mst_payermf.ContainsKey(key)) { return string.Empty; }

            if (string.IsNullOrEmpty(mst_payermf[key].m_NAME_KANA))
            {
                return mst_payermf[key].m_NAME_KANJI;
            }
            return mst_payermf[key].m_NAME_KANA;
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void FetchAllData()
        {
            Fetch_mst_banks();
            Fetch_mst_bk_changes();
            Fetch_mst_branches();
            Fetch_mst_bills();
            Fetch_mst_syuruimfs();
            Fetch_mst_br_changes();
            Fetch_mst_chgbillmf();
            Fetch_mst_payermf();
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
        private void Fetch_mst_branches()
        {
            mst_branches = new SortedDictionary<int, TBL_BRANCHMF>();
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
        private void Fetch_mst_br_changes()
        {
            mst_br_changes = new SortedDictionary<string, TBL_CHANGEMF>();
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
                        mst_br_changes.Add(key, data);
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
        /// </summary>
        private void Fetch_mst_payermf()
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

    }
}
