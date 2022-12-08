using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;
using NCR;

namespace CTRIcFuwatariMk
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;

        /// <summary>ファイル対象の明細リスト（登録データ：他行：持帰）</summary>
        public SortedDictionary<string, MeisaiInfos> ICMeisaiList1 { get; set; }
        /// <summary>ファイル対象の明細リスト（取消データ：他行：持帰）</summary>
        public SortedDictionary<string, MeisaiInfos> ICMeisaiList2 { get; set; }
        /// <summary>ファイル対象の明細リスト（登録データ：自行：持帰）</summary>
        public SortedDictionary<string, MeisaiInfos> ICMeisaiOwnList1 { get; set; }
        /// <summary>ファイル対象の明細リスト（取消データ：自行：持帰）</summary>
        public SortedDictionary<string, MeisaiInfos> ICMeisaiOwnList2 { get; set; }

        /// <summary>ファイル対象の明細リスト（登録データ：自行：持出）</summary>
        public SortedDictionary<string, MeisaiInfos> OCMeisaiOwnList1 { get; set; }
        /// <summary>ファイル対象の明細リスト（取消データ：自行：持出）</summary>
        public SortedDictionary<string, MeisaiInfos> OCMeisaiOwnList2 { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>一時テーブル補助項目</summary>
        private string _TMP_UNIQUEITEM = string.Empty;

        // *******************************************************************
        // ロック用テーブル（持帰）
        // *******************************************************************
        ///// <summary>一時テーブル：TMP_TRFUWA_IC_LOCK（持帰：他行：ロック用 明細）</summary>
        //private const string TMP_TRFUWA_IC_LOCK = "TMP_TRFUWA_IC_LOCK";
        ///// <summary>一時テーブル：TMP_TRFUWA_OWN_IC_LOCK（持帰：自行：ロック用 明細）</summary>
        //private const string TMP_TRFUWA_OWN_IC_LOCK = "TMP_TRFUWA_OWN_IC_LOCK";

        /// <summary>
        /// 一時テーブル：TMP_FUWA_LK_{IPアドレス}（持帰：他行：ロック用 明細）
        /// </summary>
        private string TMP_TRFUWA_IC_LOCK
        {
            get { return string.Format("TMP_FUWA_LK_{0}", _TMP_UNIQUEITEM); }
        }
        /// <summary>
        /// 一時テーブル：TMP_FUWA_O_LK_{IPアドレス}（持帰：自行：ロック用 明細）
        /// </summary>
        private string TMP_TRFUWA_OWN_IC_LOCK
        {
            get { return string.Format("TMP_FUWA_O_LK_{0}", _TMP_UNIQUEITEM); }
        }

        // *******************************************************************
        // 更新用テーブル（持帰）
        // *******************************************************************
        ///// <summary>一時テーブル：TMP_TRMEI_IC_UPD（持帰：他行：更新用 明細）</summary>
        //private const string TMP_TRMEI_IC_UPD = "TMP_TRFUWA_IC_UPD";
        ///// <summary>一時テーブル：TMP_TRMEI_OWN_IC_UPD（持帰：自行：更新用 明細）</summary>
        //private const string TMP_TRMEI_OWN_IC_UPD = "TMP_TRFUWA_OWN_IC_UPD";

        /// <summary>
        /// 一時テーブル：TMP_FUWA_ICU_{IPアドレス}（持帰：他行：更新用 明細）
        /// </summary>
        private string TMP_TRMEI_IC_UPD
        {
            get { return string.Format("TMP_FUWA_ICU_{0}", _TMP_UNIQUEITEM); }
        }
        /// <summary>
        /// 一時テーブル：TMP_FUWA_O_ICU_{IPアドレス}（持帰：自行：更新用 明細）
        /// </summary>
        private string TMP_TRMEI_OWN_IC_UPD
        {
            get { return string.Format("TMP_FUWA_O_ICU_{0}", _TMP_UNIQUEITEM); }
        }

        // *******************************************************************
        // 更新用テーブル（持出）
        // *******************************************************************
        ///// <summary>一時テーブル：TMP_TRMEI_OWN_OC_UPD（持出：自行：更新用 明細）</summary>
        //private const string TMP_TRMEI_OWN_OC_UPD = "TMP_TRMEI_OWN_OC_UPD";

        /// <summary>
        /// 一時テーブル：TMP_MEI_O_OCU_{IPアドレス}（持出：自行：更新用 明細）
        /// </summary>
        private string TMP_TRMEI_OWN_OC_UPD
        {
            get { return string.Format("TMP_MEI_O_OCU_{0}", _TMP_UNIQUEITEM); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            this.ICMeisaiList1 = new SortedDictionary<string, MeisaiInfos>();
            this.ICMeisaiList2 = new SortedDictionary<string, MeisaiInfos>();
            this.ICMeisaiOwnList1 = new SortedDictionary<string, MeisaiInfos>();
            this.ICMeisaiOwnList2 = new SortedDictionary<string, MeisaiInfos>();
            this.OCMeisaiOwnList1 = new SortedDictionary<string, MeisaiInfos>();
            this.OCMeisaiOwnList2 = new SortedDictionary<string, MeisaiInfos>();
            this.DispParams = new DisplayParams();
			this.DispParams.Clear();
            // 一時テーブルの補助項目設定
            _TMP_UNIQUEITEM = ImportFileAccess.GetTermIPAddress().Replace(".", "_");
        }

        public enum DataType
        {
            登録データ,
            取消データ,
        }

        public enum CreateType
        {
            全データ,
            他行データ,
            自行データ
        }


        /// <summary>
        /// 作成対象データ取得
        /// </summary>
        /// <param name="ctype"></param>
        /// <returns></returns>
        private string GetFuwatariTextFuwaListSelect(DataType dtype, CreateType ctype)
        {
            // データ種別
            SQLFileCreate.SearchType strDataType = SQLFileCreate.SearchType.Type0;
            if (dtype == DataType.登録データ)
            {
                strDataType = SQLFileCreate.SearchType.Type1;
            }
            else // if (dtype == DataType.取消データ)
            {
                strDataType = SQLFileCreate.SearchType.Type2;
            }

            // 行内交換
            SQLFileCreate.SearchType strInternalExchange = SQLFileCreate.SearchType.Type0;
            if (ctype == CreateType.他行データ)
            {
                strInternalExchange = SQLFileCreate.SearchType.Type1;
            }
            else if (ctype == CreateType.自行データ)
            {
                strInternalExchange = SQLFileCreate.SearchType.Type2;
            }
            else // if (type == ArchiveType.全データ)
            {
                strInternalExchange = SQLFileCreate.SearchType.Type0;
            }

            // SQL取得
            string strFROM = SQLFileCreate.GetFuwatariTextICMeiListSelect(
                strDataType,
                strInternalExchange,
                NCR.Operator.BankCD,
                AppInfo.Setting.SchemaBankCD);
            return strFROM;
        }


        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納（持帰）
        /// </summary>
        public bool FetchTrMeiIC(DataType dtype, CreateType ctype, EntryCommonFormBase form)
        {
            string strFROM = GetFuwatariTextFuwaListSelect(dtype, ctype);
            SortedDictionary<string, MeisaiInfos> meisaiList = new SortedDictionary<string, MeisaiInfos>();

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                string strSQL = "";
                DataTable tblMei = null;
                DataTable tblFuwa = null;
                DataTable tblImg = null;
                try
                {
                    strSQL = string.Format(strFROM, " MEI.* ");
                    tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    strSQL = string.Format(strFROM, " FUWA.* ");
                    tblFuwa = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    strSQL = string.Format(strFROM, " IMG.* ");
                    tblImg = dbp.SelectTable(strSQL, new List<IDbDataParameter>());

                    // TBL_TRMEI
                    foreach (DataRow row in tblMei.Rows)
                    {
                        TBL_TRMEI data = new TBL_TRMEI(row, AppInfo.Setting.SchemaBankCD);
                        MeisaiInfos info = new MeisaiInfos(data, dtype);
                        meisaiList.Add(info.Key, info);
                    }

                    // TBL_TRFUWATARI
                    foreach (DataRow row in tblFuwa.Rows)
                    {
                        TBL_TRFUWATARI data = new TBL_TRFUWATARI(row, AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey(data._GYM_ID, data._OPERATION_DATE, data._SCAN_TERM, data._BAT_ID, data._DETAILS_NO);
                        if (meisaiList.ContainsKey(key))
                        {
                            meisaiList[key].trfuwa = data;
                        }
                    }

                    // TBL_TRMEIIMG
                    foreach (DataRow row in tblImg.Rows)
                    {
                        TBL_TRMEIIMG data = new TBL_TRMEIIMG(row, AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey(data._GYM_ID, data._OPERATION_DATE, data._SCAN_TERM, data._BAT_ID, data._DETAILS_NO);
                        if (meisaiList.ContainsKey(key))
                        {
                            meisaiList[key].trimg = data;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }

            if (ctype != CreateType.自行データ)
            {
                // 他行 or 全件
                if (dtype == DataType.登録データ)
                {
                    ICMeisaiList1 = meisaiList;
                }
                else if (dtype == DataType.取消データ)
                {
                    ICMeisaiList2 = meisaiList;
                }
            }
            else
            {
                // 自行
                if (dtype == DataType.登録データ)
                {
                    ICMeisaiOwnList1 = meisaiList;
                }
                else if (dtype == DataType.取消データ)
                {
                    ICMeisaiOwnList2 = meisaiList;
                }
            }
            return true;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納（持出）
        /// </summary>
        public bool FetchTrMeiOC(DataType dtype, EntryCommonFormBase form)
        {
            // 持帰データのイメージファイル名をもとに持出データを抽出する
            List<string> fileNameList = new List<string>();
            if (dtype == DataType.登録データ)
            {
                foreach (MeisaiInfos mei in ICMeisaiOwnList1.Values)
                {
                    fileNameList.Add(mei.trimg.m_IMG_FLNM);
                }
            }
            else if (dtype == DataType.取消データ)
            {
                foreach (MeisaiInfos mei in ICMeisaiOwnList2.Values)
                {
                    fileNameList.Add(mei.trimg.m_IMG_FLNM);
                }
            }

            // データなし
            if (fileNameList.Count() == 0) { return true; }

            // SQL生成
            string strFROM = SQLFileCreate.GetFuwatariTextOCMeiListSelect(fileNameList, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            SortedDictionary<string, MeisaiInfos> meisaiList = new SortedDictionary<string, MeisaiInfos>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                DataTable tblMei = null;
                DataTable tblImg = null;
                string strSQL = "";
                try
                {
                    strSQL = string.Format(strFROM, " OCMEI.* ");
                    tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    strSQL = string.Format(strFROM, " OCIMG.* ");
                    tblImg = dbp.SelectTable(strSQL, new List<IDbDataParameter>());

                    // TBL_TRMEI
                    foreach (DataRow row in tblMei.Rows)
                    {
                        TBL_TRMEI data = new TBL_TRMEI(row, AppInfo.Setting.SchemaBankCD);
                        MeisaiInfos info = new MeisaiInfos(data, dtype);
                        meisaiList.Add(info.Key, info);
                    }
                    // TBL_TRMEIIMG
                    foreach (DataRow row in tblImg.Rows)
                    {
                        TBL_TRMEIIMG data = new TBL_TRMEIIMG(row, AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey(data._GYM_ID, data._OPERATION_DATE, data._SCAN_TERM, data._BAT_ID, data._DETAILS_NO);
                        if (meisaiList.ContainsKey(key))
                        {
                            meisaiList[key].trimg = data;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }

            if (dtype == DataType.登録データ)
            {
                OCMeisaiOwnList1 = meisaiList;
            }
            else if (dtype == DataType.取消データ)
            {
                OCMeisaiOwnList2 = meisaiList;
            }
            return true;
        }

        /// <summary>
        /// 処理対象テーブルをまとめてロックする
        /// </summary>
        /// <returns></returns>
        public bool LockTables(CreateType ctype, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string strSQL = "";

            string tmpTableName1 = "";
            if (ctype != CreateType.自行データ)
            {
                // 他行 or 全件
                tmpTableName1 = TMP_TRFUWA_IC_LOCK;
            }
            else
            {
                // 自行
                tmpTableName1 = TMP_TRFUWA_OWN_IC_LOCK;
            }

            // 作成対象データ取得して一時テーブルに登録する
            string srcSELECT = "";
            {
                // 登録データ
                srcSELECT = GetFuwatariTextFuwaListSelect(DataType.登録データ, ctype);
                srcSELECT = string.Format(srcSELECT, " FUWA.* ");
                strSQL = SQLFileCreate.GetInsertTmpTable(srcSELECT, tmpTableName1, TBL_TRFUWATARI.ALL_COLUMNS);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                // 取消データ
                srcSELECT = GetFuwatariTextFuwaListSelect(DataType.取消データ, ctype);
                srcSELECT = string.Format(srcSELECT, " FUWA.* ");
                strSQL = SQLFileCreate.GetInsertTmpTable(srcSELECT, tmpTableName1, TBL_TRFUWATARI.ALL_COLUMNS);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
            }

            // 一時テーブルに登録した処理対象レコードをロックする
            strSQL = SQLFileCreate.GetTmpFuwaListSelect(tmpTableName1, AppInfo.Setting.SchemaBankCD);
            strSQL += DBConvert.QUERY_LOCK;

            // SELECT 実行（ロック）
            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), non.Trans);
            if (tbl.Rows.Count < 1)
            {
                // データなし
                return false;
            }
            return true;
        }

        /// <summary>
        /// 不渡トランザクションを更新する
        /// ファイル集配信管理を登録する
        /// </summary>
        /// <returns></returns>
        public bool RegistMeisaiItems1(FileGenerator if205, FileGenerator if206, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // DB更新処理はここでまとめて全部やる
            string strSQL = "";
            try
            {
                // 更新データを一時テーブルに登録
                {
                    // 登録データ
                    CommonUtil.DBBatchInsert(GetTrMeiInsertAllQuery(TMP_TRMEI_IC_UPD, ICMeisaiList1), dbp, non);
                    // 取消データ
                    CommonUtil.DBBatchInsert(GetTrMeiInsertAllQuery(TMP_TRMEI_IC_UPD, ICMeisaiList2), dbp, non);
                }

                // 一時テーブルのデータで明細トランザクションを更新する
                strSQL = SQLFileCreate.GetFuwatariTextMeiUpdateAll(TMP_TRMEI_IC_UPD, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                // ファイル集配信管理
                TBL_FILE_CTL fctl = new TBL_FILE_CTL(
                    if205.FileId,
                    if205.FileDivid,
                    if205.FileName,
                    if206.FileName,
                    AppInfo.Setting.SchemaBankCD);
                fctl.m_SEND_FILE_LENGTH = if205.FileSize;
                fctl.m_SEND_STS = 0;
                fctl.m_MAKE_OPENO = AplInfo.OP_ID;
                fctl.m_MAKE_DATE = AplInfo.OpDate();
                fctl.m_MAKE_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
                strSQL = fctl.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                // 2022/03/28 銀行導入工程_不具合管理表No97 対応
                // 配信ファイル明細内訳に登録
                RegistSendFileTRMei(if205.FileName, TMP_TRMEI_IC_UPD, dbp, non);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("ＤＢ更新に失敗しました。");
            }
            return true;
        }

        /// <summary>
        /// 配信ファイル明細内訳に登録する
        /// </summary>
        /// <returns></returns>
        /// <remarks>2022/03/28 銀行導入工程_不具合管理表No97 対応</remarks>
        private void RegistSendFileTRMei(string SendFileName, string tmpTableName, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string strSQL = "";

            // 指定ファイル名のデータを削除
            strSQL = TBL_SEND_FILE_TRMEI.GetDeleteQueryFileName(SendFileName, AppInfo.Setting.SchemaBankCD);
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

            // 一時テーブルの内容で配信ファイル明細内訳に登録
            strSQL = SQLFileCreate.GetSendFileTRMeiInsert(SendFileName, tmpTableName, AppInfo.Setting.SchemaBankCD);
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
        }

        /// <summary>
        /// 不渡トランザクションを更新する
        /// 明細トランザクションを更新する
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        public bool RegistMeisaiItems2(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // DB更新処理はここでまとめて全部やる
            string strSQL = "";
            try
            {
                // 更新データを一時テーブルに登録
                {
                    // 明細トランザクション（持帰）
                    // 登録データ
                    CommonUtil.DBBatchInsert(GetTrMeiInsertAllQuery(TMP_TRMEI_OWN_IC_UPD, ICMeisaiOwnList1), dbp, non);
                    // 取消データ
                    CommonUtil.DBBatchInsert(GetTrMeiInsertAllQuery(TMP_TRMEI_OWN_IC_UPD, ICMeisaiOwnList2), dbp, non);

                    // 明細トランザクション（持出）
                    // 登録データ
                    CommonUtil.DBBatchInsert(GetTrMeiInsertAllQuery(TMP_TRMEI_OWN_OC_UPD, OCMeisaiOwnList1), dbp, non);
                    // 取消データ
                    CommonUtil.DBBatchInsert(GetTrMeiInsertAllQuery(TMP_TRMEI_OWN_OC_UPD, OCMeisaiOwnList2), dbp, non);
                }

                // 一時テーブルのデータで明細トランザクションを更新する
                {
                    // 明細トランザクション更新（持帰）
                    strSQL = SQLFileCreate.GetFuwatariTextMeiUpdateAll(TMP_TRMEI_OWN_IC_UPD, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                    // 明細トランザクション更新（持出）
                    strSQL = SQLFileCreate.GetFuwatariTextMeiUpdateAll(TMP_TRMEI_OWN_OC_UPD, AppInfo.Setting.SchemaBankCD);
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("ＤＢ更新に失敗しました。");
            }
            return true;
        }

        /// <summary>
        /// 一時テーブル作成
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        public void CreateTmpTable(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // TRFUWATARI
            CreateTmpTrFuwaTable(TMP_TRFUWA_IC_LOCK, dbp, non);
            CreateTmpTrFuwaTable(TMP_TRFUWA_OWN_IC_LOCK, dbp, non);
            // TRMEI
            CreateTmpTrMeiTable(TMP_TRMEI_IC_UPD, dbp, non);
            CreateTmpTrMeiTable(TMP_TRMEI_OWN_IC_UPD, dbp, non);
            CreateTmpTrMeiTable(TMP_TRMEI_OWN_OC_UPD, dbp, non);
        }

        /// <summary>
        /// 一時テーブル削除
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        public void DropTmpTable(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // 例外発生時に catch ブロックで ロールバック前に DROP TABLE を実行すると、
            // ロールバックされずに更新内容がコミットされてしまうので注意!!
            // TRFUWATARI
            DropTmpTable(TMP_TRFUWA_IC_LOCK, dbp, non);
            DropTmpTable(TMP_TRFUWA_OWN_IC_LOCK, dbp, non);
            // TRMEI
            DropTmpTable(TMP_TRMEI_IC_UPD, dbp, non);
            DropTmpTable(TMP_TRMEI_OWN_IC_UPD, dbp, non);
            DropTmpTable(TMP_TRMEI_OWN_OC_UPD, dbp, non);
        }

        /// <summary>
        /// 一時テーブルを作成する（TRMEI）
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void CreateTmpTrFuwaTable(string tableName, AdoDatabaseProvider dbp, AdoNonCommitTransaction auto)
        {
            string strSQL = "";
            try
            {
                strSQL = SQLFileCreate.GetCreateTRFUWATARI(tableName);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// 一時テーブルを作成する（TRMEI）
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void CreateTmpTrMeiTable(string tableName, AdoDatabaseProvider dbp, AdoNonCommitTransaction auto)
        {
            string strSQL = "";
            try
            {
                strSQL = SQLFileCreate.GetCreateTRMEI(tableName);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// 一時テーブルを削除する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void DropTmpTable(string tableName, AdoDatabaseProvider dbp, AdoNonCommitTransaction auto)
        {
            string strSQL = "";
            try
            {
                strSQL = DBCommon.GetDropTmpTableSQL(tableName);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception)
            {
                // 初回はテーブルないので例外になる
                //LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.Message, 2);
            }
        }

        /// <summary>
        /// 一時テーブル登録クエリを取得する（明細トランザクション）
        /// </summary>
        /// <returns></returns>
        private List<string> GetTrMeiInsertAllQuery(string tableName, SortedDictionary<string, MeisaiInfos> meisaiList)
        {
            List<string> InsertList = new List<string>();

            foreach (MeisaiInfos meiinfo in meisaiList.Values)
            {
                // キー項目と更新対象項目のみ
                InsertList.Add(SQLFileCreate.GetFuwatariTextTrMeiInsertAll(tableName, meiinfo.trmei));
            }

            return InsertList;
        }

        /// <summary>
        /// 作成対象の明細
        /// </summary>
        public class MeisaiInfos
        {
            public DataType Type { get; private set; }
            public string Key { get; private set; } = "";
            public TBL_TRMEI trmei { get; set; } = null;
            public TBL_TRMEIIMG trimg { get; set; } = null;
            public TBL_TRFUWATARI trfuwa { get; set; } = null;

            public MeisaiInfos(TBL_TRMEI trmei, DataType dtype)
            {
                this.Type = dtype;
                this.Key = CommonUtil.GenerateKey(trmei._GYM_ID, trmei._OPERATION_DATE, trmei._SCAN_TERM, trmei._BAT_ID, trmei._DETAILS_NO);
                this.trmei = trmei;
                this.trimg = null;
                this.trfuwa = null;
            }
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            /// <summary>予定：登録件数</summary>
            public int YoteiRegistCnt { get; set; } = 0;
            /// <summary>予定：取消件数</summary>
            public int YoteiCancelCnt { get; set; } = 0;
            /// <summary>予定：合計件数</summary>
            public int YoteiTotalCnt { get; set; } = 0;
            /// <summary>作成ファイル</summary>
            public string CreateFileName { get; set; } = "";
            public FileGenerator if205 { get; set; } = null;

            public void Clear()
			{
                this.YoteiRegistCnt = 0;
                this.YoteiCancelCnt = 0;
                this.YoteiTotalCnt = 0;
                this.CreateFileName = "";
                this.if205 = null;
            }
        }
    }
}
