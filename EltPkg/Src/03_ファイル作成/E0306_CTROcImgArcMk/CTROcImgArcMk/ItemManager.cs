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

namespace CTROcImgArcMk
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;

        /// <summary>アーカイブ対象の明細リスト（他行）</summary>
        public SortedDictionary<string, MeisaiInfos> MeisaiList1 { get; set; }
        /// <summary>アーカイブ対象の明細リスト（自行）</summary>
        public SortedDictionary<string, MeisaiInfos> MeisaiList2 { get; set; }
        /// <summary>アーカイブファイルリスト</summary>
        public SortedDictionary<int, ArchiveInfos> ArchiveList { get; set; }
        /// <summary>自行情報</summary>
        public OwnBankInfos OwnBankInfo { get; set; }

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>一時テーブル補助項目</summary>
        private string _TMP_UNIQUEITEM = string.Empty;

        // *******************************************************************
        // 一時テーブル
        // *******************************************************************
        ///// <summary>一時テーブル：TRMEIIMG（他行：ロック用）</summary>
        //private const string TMP_TRIMG_LOCK1 = "TMP_TRIMG_LOCK1";
        ///// <summary>一時テーブル：TRMEIIMG（他行：更新用）</summary>
        //private const string TMP_TRIMG_UPD1 = "TMP_TRIMG_UPD1";

        ///// <summary>一時テーブル：TRMEIIMG（自行：ロック用）</summary>
        //private const string TMP_TRIMG_LOCK2 = "TMP_TRIMG_LOCK2";
        ///// <summary>一時テーブル：TRMEIIMG（自行：更新用）</summary>
        //private const string TMP_TRIMG_UPD2 = "TMP_TRIMG_UPD2";

        /// <summary>
        /// 一時テーブル：TMP_IMG_LK1_{IPアドレス}（他行：ロック用）
        /// </summary>
        private string TMP_TRIMG_LOCK1
        {
            get { return string.Format("TMP_IMG_LK1_{0}", _TMP_UNIQUEITEM); }
        }
        /// <summary>
        /// 一時テーブル：TMP_IMG_UPD1_{IPアドレス}（他行：更新用）
        /// </summary>
        private string TMP_TRIMG_UPD1
        {
            get { return string.Format("TMP_IMG_UPD1_{0}", _TMP_UNIQUEITEM); }
        }
        /// <summary>
        /// 一時テーブル：TMP_IMG_LK2_{IPアドレス}（自行：ロック用）
        /// </summary>
        private string TMP_TRIMG_LOCK2
        {
            get { return string.Format("TMP_IMG_LK2_{0}", _TMP_UNIQUEITEM); }
        }
        /// <summary>
        /// 一時テーブル：TMP_IMG_UPD2_{IPアドレス}（自行：更新用）
        /// </summary>
        private string TMP_TRIMG_UPD2
        {
            get { return string.Format("TMP_IMG_UPD2_{0}", _TMP_UNIQUEITEM); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            this.MeisaiList1 = new SortedDictionary<string, MeisaiInfos>();
            this.MeisaiList2 = new SortedDictionary<string, MeisaiInfos>();
            this.ArchiveList = new SortedDictionary<int, ArchiveInfos>();
            this.OwnBankInfo = new OwnBankInfos();
            this.DispParams = new DisplayParams();
			this.DispParams.Clear();
            // 一時テーブルの補助項目設定
            _TMP_UNIQUEITEM = ImportFileAccess.GetTermIPAddress().Replace(".", "_");
        }

        public enum ArchiveType
        {
            全データ,
            他行データ,
            自行データ
        }

        /// <summary>
        /// 作成対象データ取得
        /// </summary>
        /// <param name="isInternal">
        /// 　true：行内交換の明細のみ取得する
        /// 　false：イメージアーカイブ作成対象の明細をすべて取得する</param>
        /// <returns></returns>
        private string GetArchiveMeiListSelect(ArchiveType type)
        {
            // 業務
            SQLFileCreate.SearchType strInputRoute = SQLFileCreate.SearchType.Type0;
            if (DispParams.InputRoute == MeisaiListForm.GymType.交換持出)
            {
                strInputRoute = SQLFileCreate.SearchType.Type1;
            }
            else if (DispParams.InputRoute == MeisaiListForm.GymType.期日管理)
            {
                strInputRoute = SQLFileCreate.SearchType.Type2;
            }

            // 行内交換
            SQLFileCreate.SearchType strInternalExchange = SQLFileCreate.SearchType.Type0;
            if (type == ArchiveType.他行データ)
            {
                strInternalExchange = SQLFileCreate.SearchType.Type1;
            }
            else if (type == ArchiveType.自行データ)
            {
                strInternalExchange = SQLFileCreate.SearchType.Type2;
            }
            else // if (type == ArchiveType.全データ)
            {
                strInternalExchange = SQLFileCreate.SearchType.Type0;
            }

            // SQL取得
            string strFROM = SQLFileCreate.GetArchiveMeiListSelect(
                strInputRoute,
                strInternalExchange,
                NCR.Operator.BankCD,
                DispParams.ClearingDate,
                AppInfo.Setting.SchemaBankCD);
            return strFROM;
        }


        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public bool FetchTrMei(ArchiveType datatype, EntryCommonFormBase form)
        {
            string strFROM = GetArchiveMeiListSelect(datatype);
            SortedDictionary<string, MeisaiInfos> meisaiList = new SortedDictionary<string, MeisaiInfos>();

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                string strSQL = "";
                DataTable tblBat = null;
                DataTable tblMei = null;
                DataTable tblImg = null;
                DataTable tblItem = null;
                try
                {
                    strSQL = string.Format(strFROM, " BAT.* ");
                    tblBat = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    strSQL = string.Format(strFROM, " MEI.* ");
                    tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    strSQL = string.Format(strFROM, " IMG.* ");
                    tblImg = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    strSQL = string.Format(strFROM, " ITEM.* ");
                    tblItem = dbp.SelectTable(strSQL, new List<IDbDataParameter>());

                    // TBL_TRMEI
                    foreach (DataRow row in tblMei.Rows)
                    {
                        TBL_TRMEI data = new TBL_TRMEI(row, AppInfo.Setting.SchemaBankCD);
                        MeisaiInfos info = new MeisaiInfos(data, AppInfo.Setting.SchemaBankCD);

                        // TBL_TRBATCH
                        string filter = string.Format("GYM_ID={0} AND OPERATION_DATE={1} AND SCAN_TERM='{2}' AND BAT_ID={3}", data._GYM_ID, data._OPERATION_DATE, data._SCAN_TERM, data._BAT_ID);
                        DataRow[] rows = tblBat.Select(filter);
                        if (rows.Length > 0)
                        {
                            TBL_TRBATCH bat = new TBL_TRBATCH(rows[0], AppInfo.Setting.SchemaBankCD);
                            info.trbat = bat;
                            info.fileCtl.ImageDirPath = GetImgDirPath(bat);
                        }
                        meisaiList.Add(info.Key, info);
                    }

                    // TBL_TRMEIIMG
                    foreach (DataRow row in tblImg.Rows)
                    {
                        TBL_TRMEIIMG data = new TBL_TRMEIIMG(row, AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey(data._GYM_ID, data._OPERATION_DATE, data._SCAN_TERM, data._BAT_ID, data._DETAILS_NO);
                        if (meisaiList.ContainsKey(key))
                        {
                            meisaiList[key].images.Add(data._IMG_KBN, new ImageInfos(data));
                        }
                    }

                    // TBL_TRITEM
                    foreach (DataRow row in tblItem.Rows)
                    {
                        TBL_TRITEM data = new TBL_TRITEM(row, AppInfo.Setting.SchemaBankCD);
                        string key = CommonUtil.GenerateKey(data._GYM_ID, data._OPERATION_DATE, data._SCAN_TERM, data._BAT_ID, data._DETAILS_NO);
                        if (meisaiList.ContainsKey(key))
                        {
                            meisaiList[key].tritems.Add(data._ITEM_ID, data);
                        }
                    }

                    // 表面のイメージ設定
                    foreach(MeisaiInfos MeiInfo in meisaiList.Values)
                    {
                        var FrontImgs = MeiInfo.images.Where(x => x.Value.trimg._IMG_KBN == TrMeiImg.ImgKbn.表);
                        if (FrontImgs.Count() == 0)
                        {
                            // 送信一覧に表面が存在しない場合、別途データを取得して設定
                            MeiInfo.fronttrimg = GetFrontImgData(MeiInfo.trmei, dbp);
                        }
                        else
                        {
                            // 送信一覧に表面が存在する場合、表面情報を設定
                            // (オブジェクトは同じものを設定 MeiInfo.imagesを変更した内容はここにも反映したいため)
                            MeiInfo.fronttrimg = FrontImgs.First().Value.trimg;
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

            if (datatype != ArchiveType.自行データ)
            {
                // 他行 or 全件
                MeisaiList1 = meisaiList;
            }
            else
            {
                // 自行
                MeisaiList2 = meisaiList;
            }
            return true;
        }

        /// <summary>
        /// 処理対象テーブルをまとめてロックする
        /// </summary>
        /// <returns></returns>
        public bool LockTables(ArchiveType type, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            string strSQL = "";

            string tmpTableName = "";
            if (type != ArchiveType.自行データ)
            {
                // 他行 or 全件
                tmpTableName = TMP_TRIMG_LOCK1;
            }
            else
            {
                // 自行
                tmpTableName = TMP_TRIMG_LOCK2;
            }

            // 作成対象データ取得して一時テーブルに登録する
            string srcSELECT = GetArchiveMeiListSelect(type);
            srcSELECT = string.Format(srcSELECT, " IMG.* ");
            strSQL = SQLFileCreate.GetInsertTmpTable(srcSELECT, tmpTableName, TBL_TRMEIIMG.ALL_COLUMNS);
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

            // 一時テーブルに登録した処理対象レコードをロックする
            strSQL = SQLFileCreate.GetArchiveImgListSelect(tmpTableName, AppInfo.Setting.SchemaBankCD);
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
        /// 明細イメージを更新する
        /// ファイル集配信管理を登録する
        /// </summary>
        /// <returns></returns>
        public bool RegistImageArchives(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // DB更新処理はここでまとめて全部やる
            string strSQL = "";
            try
            {
                // 更新データを一時テーブルに登録
                CommonUtil.DBBatchInsert(GetTrImageInsertAllQuery(TMP_TRIMG_UPD1, MeisaiList1), dbp, non);

                // 一時テーブルのデータで明細イメージを更新する
                strSQL = SQLFileCreate.GetArchiveImgUpdateAll(TMP_TRIMG_UPD1, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                // アーカイブ件数分
                foreach (ItemManager.ArchiveInfos archive in ArchiveList.Values)
                {
                    // 結果テキスト
                    FileGenerator if206 = new FileGenerator(
                        DBConvert.ToIntNull(archive.if101.Seq),
                        FileParam.FileId.IF206,
                        FileParam.FileKbn.BUB,
                        NCR.Operator.BankCD,
                        ".txt");

                    // ファイル集配信管理
                    TBL_FILE_CTL fctl = new TBL_FILE_CTL(
                        archive.if101.FileId,
                        archive.if101.FileDivid,
                        archive.if101.FileName,
                        if206.FileName,
                        AppInfo.Setting.SchemaBankCD);
                    fctl.m_SEND_FILE_LENGTH = archive.if101.FileSize;
                    fctl.m_SEND_STS = 0;
                    fctl.m_MAKE_OPENO = AplInfo.OP_ID;
                    fctl.m_MAKE_DATE = AplInfo.OpDate();
                    fctl.m_MAKE_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));

                    // ファイル集配信管理を登録
                    strSQL = fctl.GetInsertQuery();
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
        /// 行内交換データを登録する
        /// </summary>
        /// <param name="if207">持帰要求結果テキストファイル</param>
        /// <param name="if201">証券明細テキストファイル</param>
        /// <param name="if101">アーカイブファイル</param>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        public bool RegistInternalExchanges(FileGenerator if207, FileGenerator if201, FileGenerator if101, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // DB更新処理はここでまとめて全部やる
            string strSQL = "";
            try
            {
                // 更新データを一時テーブルに登録
                CommonUtil.DBBatchInsert(GetTrImageInsertAllQuery(TMP_TRIMG_UPD2, MeisaiList2), dbp, non);

                // 一時テーブルのデータで明細イメージを更新する
                strSQL = SQLFileCreate.GetArchiveImgUpdateAll(TMP_TRIMG_UPD2, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                // 一時テーブルのデータで項目トランザクションを更新する
                strSQL = SQLFileCreate.GetArchiveItemUpdateAll(TMP_TRIMG_UPD2, AppInfo.Setting.SchemaBankCD);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                // 持帰要求結果管理を登録
                TBL_ICREQRET_CTL retctl = new TBL_ICREQRET_CTL(if207.FileName, if201.FileName, IcReqRetCtl.CapKbn.行内交換連携, AppInfo.Setting.SchemaBankCD);
                retctl.m_CAP_STS = IcReqRetCtl.Sts.ダウンロード確定待;
                retctl.m_IMG_ARCH_NAME = if101.FileName;
                retctl.m_IMG_ARCH_CAP_STS = IcReqRetCtl.Sts.ダウンロード確定待;
                strSQL = retctl.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

                // 持帰要求結果証券明細テキストを登録
                CommonUtil.DBBatchInsert(GetBillTextInsertAllQuery(if201, if101, dbp), dbp, non);
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
            // TRMEI
            CreateTmpTrMeiImgTable(TMP_TRIMG_LOCK1, dbp, non);
            CreateTmpTrMeiImgTable(TMP_TRIMG_UPD1, dbp, non);
            CreateTmpTrMeiImgTable(TMP_TRIMG_LOCK2, dbp, non);
            CreateTmpTrMeiImgTable(TMP_TRIMG_UPD2, dbp, non);
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
            DropTmpTable(TMP_TRIMG_LOCK1, dbp, non);
            DropTmpTable(TMP_TRIMG_UPD1, dbp, non);
            DropTmpTable(TMP_TRIMG_LOCK2, dbp, non);
            DropTmpTable(TMP_TRIMG_UPD2, dbp, non);
        }

        /// <summary>
        /// 一時テーブルを作成する（TRMEIIMG）
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        private void CreateTmpTrMeiImgTable(string tableName, AdoDatabaseProvider dbp, AdoNonCommitTransaction auto)
        {
            string strSQL = "";
            try
            {
                strSQL = SQLFileCreate.GetCreateTRMEIIMG(tableName);
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
        /// 一時テーブル登録クエリを取得する（明細イメージ）
        /// </summary>
        /// <returns></returns>
        private List<string> GetTrImageInsertAllQuery(string tableName, SortedDictionary<string, MeisaiInfos> meisaiList)
        {
            List<string> InsertList = new List<string>();

            // 明細件数
            foreach (MeisaiInfos meiinfo in meisaiList.Values)
            {
                // イメージ件数
                foreach (ImageInfos imginfo in meiinfo.images.Values)
                {
                    TBL_TRMEIIMG img = imginfo.trimg;

                    // キー項目と更新対象項目のみ
                    InsertList.Add(SQLFileCreate.GetArchiveImgInsertAll(tableName, img));
                }
            }

            return InsertList;
        }

        /// <summary>
        /// 一時テーブル登録クエリを取得する（証券明細テキスト）
        /// </summary>
        /// <returns></returns>
        private List<string> GetBillTextInsertAllQuery(FileGenerator if201, FileGenerator if101, AdoDatabaseProvider dbp)
        {
            List<string> InsertList = new List<string>();
            EntryReplacer er = new EntryReplacer();

            // 持帰要求結果証券明細テキストを明細単位でデータを登録する
            foreach (MeisaiInfos meisai in MeisaiList2.Values)
            {
                // イメージ表ファイル名取得
                string FrontImgFlnm = string.Empty;
                FrontImgFlnm = meisai.fronttrimg.m_IMG_FLNM;
                //IEnumerable<ImageInfos> imageInfos = meisai.images.Values.Where(p => p.trimg._IMG_KBN == TrMeiImg.ImgKbn.表);
                //if (imageInfos.Count() == 0)
                //{
                //    FrontImgFlnm = GetFrontImgName(meisai.trmei, dbp);
                //}
                //else
                //{
                //    FrontImgFlnm = imageInfos.First().trimg.m_IMG_FLNM;
                //}

                // DSPIDから交換証券種類を取得
                string BillCode = GetBillCode(meisai.trmei.m_DSP_ID, er);

                // イメージ件数分
                foreach (ImageInfos image in meisai.images.Values)
                {
                    // 持帰要求結果証券明細テキスト
                    TBL_ICREQRET_BILLMEITXT bill = new TBL_ICREQRET_BILLMEITXT(if201.FileName, IcReqRetCtl.CapKbn.行内交換連携, image.trimg.m_IMG_FLNM, AppInfo.Setting.SchemaBankCD);
                    bill.m_IMG_ARCH_NAME = if101.FileName;
                    bill.m_FRONT_IMG_NAME = FrontImgFlnm;
                    bill.m_IMG_KBN = image.trimg._IMG_KBN;
                    bill.m_FILE_OC_BK_NO = CommonUtil.PadLeft(DBConvert.ToStringNull(Operator.BankCD), 4, "0");
                    bill.m_CHG_OC_BK_NO = CommonUtil.PadLeft(DBConvert.ToStringNull(Operator.BankCD), 4, "0");
                    bill.m_OC_BR_NO = CommonUtil.PadLeft(DBConvert.ToStringNull(meisai.trbat.m_OC_BR_NO), 4, "0");
                    bill.m_OC_DATE = AplInfo.OpDate();
                    bill.m_OC_USERID = AplInfo.OP_ID;
                    bill.m_OCR_IC_BK_NO = meisai.tritems[DspItem.ItemId.持帰銀行コード].m_END_DATA;
                    bill.m_FILE_IC_BK_NO = CommonUtil.PadLeft(DBConvert.ToStringNull(Operator.BankCD), 4, "0");
                    bill.m_CHG_IC_BK_NO = CommonUtil.PadLeft(DBConvert.ToStringNull(Operator.BankCD), 4, "0");
                    bill.m_OCR_AMOUNT = meisai.tritems[DspItem.ItemId.金額].m_END_DATA;
                    bill.m_FILE_AMOUNT = meisai.tritems[DspItem.ItemId.金額].m_END_DATA;
                    bill.m_OC_CLEARING_DATE = meisai.tritems[DspItem.ItemId.入力交換希望日].m_END_DATA;
                    bill.m_BILL_CODE = BillCode;
                    bill.m_PAY_KBN = meisai.tritems[DspItem.ItemId.決済フラグ].m_END_DATA;

                    // 全部登録する
                    InsertList.Add(SQLFileCreate.GetArchiveBillInsertAll(TBL_ICREQRET_BILLMEITXT.TABLE_NAME(AppInfo.Setting.SchemaBankCD), bill));
                }
            }

            return InsertList;
        }

        /// <summary>
        /// 対象明細の表面イメージファイル名を取得
        /// </summary>
        /// <param name="meisai"></param>
        /// <param name="dbp"></param>
        /// <remarks>未使用</remarks>
        private string GetFrontImgName(TBL_TRMEI meisai, AdoDatabaseProvider dbp)
        {
            string strSQL = "";
            try
            {
                strSQL = TBL_TRMEIIMG.GetSelectQuery(meisai._GYM_ID, meisai._OPERATION_DATE, meisai._SCAN_TERM , meisai._BAT_ID, 
                                                     meisai._DETAILS_NO, TrMeiImg.ImgKbn.表, AppInfo.Setting.SchemaBankCD);
                DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                if (tbl.Rows.Count == 0) throw new Exception("表イメージの取得に失敗しました");
                TBL_TRMEIIMG trmeimg = new TBL_TRMEIIMG(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);

                return trmeimg.m_IMG_FLNM;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw ex;
            }
        }

        /// <summary>
        /// 対象明細の表面イメージデータを取得
        /// </summary>
        /// <param name="meisai"></param>
        /// <param name="dbp"></param>
        private TBL_TRMEIIMG GetFrontImgData(TBL_TRMEI meisai, AdoDatabaseProvider dbp)
        {
            // 表面のイメージ取得
            string strSQL = TBL_TRMEIIMG.GetSelectQuery(meisai._GYM_ID, meisai._OPERATION_DATE, meisai._SCAN_TERM, meisai._BAT_ID, 
                                                        meisai._DETAILS_NO, TrMeiImg.ImgKbn.表, AppInfo.Setting.SchemaBankCD);
            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            if (tbl.Rows.Count <= 0) throw new Exception("表イメージの取得に失敗しました");

            return new TBL_TRMEIIMG(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 交換証券種類を取得
        /// </summary>
        /// <param name="id"></param>
        private string GetBillCode(int id, EntryReplacer er)
        {
            // DSPIDから交換証券種類を取得
            int billCd = er.GetDspIDBillCode(id);
            if (billCd < 0)
            {
                // 交換証券種類取得エラー
                throw new Exception("交換証券種類コード取得エラー");
            }

            return billCd.ToString("D3");
        }

        /// <summary>
        /// イメージファイルフォルダパスを取得する
        /// </summary>
        private string GetImgDirPath(TBL_TRBATCH bat)
        {
            Dictionary<string, string> pathList = new Dictionary<string, string>();
            pathList.Add("BankNormalImageRoot", ServerIni.Setting.BankNormalImageRoot);
            pathList.Add("BankFutaiImageRoot", ServerIni.Setting.BankFutaiImageRoot);
            pathList.Add("BankKijituImageRoot", ServerIni.Setting.BankKijituImageRoot);
            pathList.Add("BankConfirmImageRoot", ServerIni.Setting.BankConfirmImageRoot);
            return CommonUtil.GetImgDirPath(bat, pathList);
        }

        /// <summary>
        /// 明細件数とイメージ件数を集計する
        /// </summary>
        /// <param name="meisaiList"></param>
        /// <returns></returns>
        public SortedDictionary<int, CreateData> CalcCreateDate(SortedDictionary<string, MeisaiInfos> meisaiList)
        {
            SortedDictionary<int, CreateData> dataList = new SortedDictionary<int, CreateData>();
            foreach (ItemManager.MeisaiInfos meisai in meisaiList.Values)
            {
                int clearingDate = DBConvert.ToIntNull(meisai.tritems[DspItem.ItemId.入力交換希望日].m_END_DATA);
                CreateData data = null;
                if (dataList.ContainsKey(clearingDate))
                {
                    data = dataList[clearingDate];
                    data.MeisaiCount++;
                    data.ImageCount += meisai.images.Count;
                }
                else
                {
                    data = new CreateData();
                    data.ClearingDate = clearingDate;
                    data.MeisaiCount++;
                    data.ImageCount += meisai.images.Count;
                    dataList.Add(data.ClearingDate, data);
                }
            }
            return dataList;
        }

        /// <summary>
        /// アーカイブ対象の明細
        /// </summary>
        public class MeisaiInfos
        {
            public string Key { get; private set; } = "";
            public TBL_TRBATCH trbat { get; set; } = null;
            public TBL_TRMEI trmei { get; set; } = null;
            /// <summary>Key=IMG_KBN, Value=ImageInfos</summary>
            public SortedDictionary<int, ImageInfos> images { get; set; }
            /// <summary>Key=ITEM_ID, Value=TBL_TRITEM</summary>
            public SortedDictionary<int, TBL_TRITEM> tritems { get; set; }
            /// <summary>表面のイメージ情報</summary>
            public TBL_TRMEIIMG fronttrimg { get; set; }

            /// <summary>明細データのイメージファイル情報</summary>
            public ImportFileAccess.DetailCtl fileCtl { get; set; }

            public MeisaiInfos(TBL_TRMEI trmei, int schemabankcd)
            {
                this.Key = CommonUtil.GenerateKey(trmei._GYM_ID, trmei._OPERATION_DATE, trmei._SCAN_TERM, trmei._BAT_ID, trmei._DETAILS_NO);
                this.trmei = trmei;
                this.images = new SortedDictionary<int, ImageInfos>();
                this.tritems = new SortedDictionary<int, TBL_TRITEM>();
                this.fileCtl = new ImportFileAccess.DetailCtl();
                this.fronttrimg = new TBL_TRMEIIMG(schemabankcd);
            }
        }

        /// <summary>
        /// アーカイブ対象の明細イメージ
        /// </summary>
        public class ImageInfos
        {
            public TBL_TRMEIIMG trimg { get; set; }
            public string OLD_IMG_FLNM { get; private set; }
            public string NEW_IMG_FLNM { get; set; }
            public ImageInfos(TBL_TRMEIIMG trimg)
            {
                this.trimg = trimg;
                this.OLD_IMG_FLNM = trimg.m_IMG_FLNM;
                this.NEW_IMG_FLNM = "";
            }
        }

        /// <summary>
        /// アーカイブファイル情報
        /// </summary>
        public class ArchiveInfos
        {
            /// <summary>アーカイブファイルに含める明細リスト</summary>
            public SortedDictionary<string, MeisaiInfos> MeisaiList { get; set; }
            /// <summary>アーカイブファイルサイズ</summary>
            public long TotalFileSize { get; set; }
            /// <summary>アーカイブフォルダがいっぱいかどうか（500MB以内）</summary>
            public bool IsFileFull { get; set; }
            /// <summary>電子交換所インターフェース情報：証券イメージ(イメージアーカイブ)</summary>
            public FileGenerator if101 { get; set; }

            public ArchiveInfos()
            {
                this.MeisaiList = new SortedDictionary<string, MeisaiInfos>();
                this.TotalFileSize = 0;
                this.IsFileFull = false;
                this.if101 = null;
            }
        }

        /// <summary>
        /// 自行情報
        /// </summary>
        public class OwnBankInfos
        {
            public string ArchiveFilePath { get; set; } = "";
        }

        /// <summary>
        /// 件数集計
        /// </summary>
        public class CreateData
        {
            public int ClearingDate { get; set; } = 0;
            public int MeisaiCount { get; set; } = 0;
            public int ImageCount { get; set; } = 0;
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
		{
            /// <summary>交換希望日</summary>
            public int ClearingDate { get; set; }
            /// <summary>業務</summary>
            public MeisaiListForm.GymType InputRoute { get; set; }

            /// <summary>予定：合計（明細）</summary>
            public int YoteiMeiCnt { get; set; } = 0;
            /// <summary>予定：合計（イメージ）</summary>
            public int YoteiImgCnt { get; set; } = 0;

            public void Clear()
			{
                this.ClearingDate = 0;
                this.InputRoute = MeisaiListForm.GymType.未指定;
                this.YoteiMeiCnt = 0;
                this.YoteiImgCnt = 0;
            }
        }
    }
}
