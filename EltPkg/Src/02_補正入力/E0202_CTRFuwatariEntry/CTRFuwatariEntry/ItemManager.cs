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

namespace CTRFuwatariEntryForm
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;
        private Controller _ctl = null;

        public TBL_TRBATCH trbat { get; set; }
        public TBL_TRMEI trmei { get; set; }
        public SortedDictionary<int, TBL_TRMEIIMG> trimges { get; set; }
        public TBL_TRFUWATARI fuwatari { get; set; }
        public TBL_TRFUWATARI fuwatari_bef { get; set; }
        public TBL_TRMEI trmei_bef { get; set; }
        public SortedDictionary<int, TBL_TRITEM> tritems { get; set; }
        public TBL_IMG_PARAM imgparam { get; set; }
        public SortedDictionary<int, ImageInfo> ImageInfos { get; set; }

        /// <summary>交換証券種類マスタ（key=BILL_CODE, val=TBL_BILLMF）</summary>
        public SortedDictionary<int, TBL_BILLMF> mst_bills { get; set; }
        /// <summary>支店マスタ（key=BR_NO, val=TBL_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_BRANCHMF> mst_branches { get; set; }
        /// <summary>不渡事由コードマスタ</summary>
        public DataTable tblFuwatari { get; set; }
        /// <summary>項目定義マスタ（key=ITEM_ID, val=TBL_BRANCHMF）</summary>
        public SortedDictionary<int, TBL_DSP_ITEM> mst_dspitems { get; set; }

        public bool IsMeisaiNull { get { return (trmei == null); } }

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

            trbat = null;
            trmei = null;
            trmei_bef = null;
            trimges = new SortedDictionary<int, TBL_TRMEIIMG>();
            fuwatari = null;
            fuwatari_bef = null;
            tritems = new SortedDictionary<int, TBL_TRITEM>();
            imgparam = null;
            ImageInfos = new SortedDictionary<int, ImageInfo>();
            mst_bills = new SortedDictionary<int, TBL_BILLMF>();
            mst_branches = new SortedDictionary<int, TBL_BRANCHMF>();
            tblFuwatari = new DataTable();
            mst_dspitems = new SortedDictionary<int, TBL_DSP_ITEM>();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public void FetchAllData(Controller ctl)
        {
            _ctl = ctl;
            if (_ctl.IsIniErr) { return; }

            Fetch_trbat(_ctl.GymId, _ctl.OpeDate, _ctl.ScanTerm, _ctl.BatId);
            trmei = Fetch_trmei(_ctl.GymId, _ctl.OpeDate, _ctl.ScanTerm, _ctl.BatId, _ctl.DetailsNo);
            trmei_bef = Fetch_trmei(_ctl.GymId, _ctl.OpeDate, _ctl.ScanTerm, _ctl.BatId, _ctl.DetailsNo);
            Fetch_trimges(_ctl.GymId, _ctl.OpeDate, _ctl.ScanTerm, _ctl.BatId, _ctl.DetailsNo);
            fuwatari = Fetch_fuwatari(_ctl.GymId, _ctl.OpeDate, _ctl.ScanTerm, _ctl.BatId, _ctl.DetailsNo);
            fuwatari_bef = Fetch_fuwatari(_ctl.GymId, _ctl.OpeDate, _ctl.ScanTerm, _ctl.BatId, _ctl.DetailsNo);
            Fetch_tritems(_ctl.GymId, _ctl.OpeDate, _ctl.ScanTerm, _ctl.BatId, _ctl.DetailsNo);
            Fetch_imgparam(_ctl.GymId, trmei?.m_DSP_ID);
            Fetch_mst_bills();
            Fetch_mst_branches();
            Fetch_tblFuwatari();
            Fetch_mst_dspitems(_ctl.GymId, trmei?.m_DSP_ID);
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_trbat(int gymid, int opedate, string scanterm, int batid)
        {
            // 後続処理で使用するため空のデータを作成
            trbat = new TBL_TRBATCH(gymid, opedate, scanterm, batid, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private TBL_TRMEI Fetch_trmei(int gymid, int opedate, string scanterm, int batid, int detailsno)
        {
            string strSQL = TBL_TRMEI.GetSelectQuery(gymid, opedate, scanterm, batid, detailsno, AppInfo.Setting.SchemaBankCD);

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
                }
            }
            return trmei;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_trimges(int gymid, int opedate, string scanterm, int batid, int detailsno)
        {
            // イメージ情報
            ImageInfos = new SortedDictionary<int, ImageInfo>();
            ImageInfos.Add(TrMeiImg.ImgKbn.表, new ImageInfo(TrMeiImg.ImgKbn.表));
            ImageInfos.Add(TrMeiImg.ImgKbn.裏, new ImageInfo(TrMeiImg.ImgKbn.裏));
            ImageInfos.Add(TrMeiImg.ImgKbn.補箋, new ImageInfo(TrMeiImg.ImgKbn.補箋));
            ImageInfos.Add(TrMeiImg.ImgKbn.付箋, new ImageInfo(TrMeiImg.ImgKbn.付箋));
            ImageInfos.Add(TrMeiImg.ImgKbn.入金証明, new ImageInfo(TrMeiImg.ImgKbn.入金証明));
            ImageInfos.Add(TrMeiImg.ImgKbn.表再送分, new ImageInfo(TrMeiImg.ImgKbn.表再送分));
            ImageInfos.Add(TrMeiImg.ImgKbn.裏再送分, new ImageInfo(TrMeiImg.ImgKbn.裏再送分));
            ImageInfos.Add(TrMeiImg.ImgKbn.その他1, new ImageInfo(TrMeiImg.ImgKbn.その他1));
            ImageInfos.Add(TrMeiImg.ImgKbn.その他2, new ImageInfo(TrMeiImg.ImgKbn.その他2));
            ImageInfos.Add(TrMeiImg.ImgKbn.その他3, new ImageInfo(TrMeiImg.ImgKbn.その他3));
            ImageInfos.Add(TrMeiImg.ImgKbn.予備1, new ImageInfo(TrMeiImg.ImgKbn.予備1));
            ImageInfos.Add(TrMeiImg.ImgKbn.予備2, new ImageInfo(TrMeiImg.ImgKbn.予備2));
            ImageInfos.Add(TrMeiImg.ImgKbn.予備3, new ImageInfo(TrMeiImg.ImgKbn.予備3));

            string strSQL = TBL_TRMEIIMG.GetSelectQuery(gymid, opedate, scanterm, batid, detailsno, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            trimges = new SortedDictionary<int, TBL_TRMEIIMG>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_TRMEIIMG data = new TBL_TRMEIIMG(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        trimges.Add(data._IMG_KBN, data);

                        // イメージ情報
                        if (ImageInfos.ContainsKey(data._IMG_KBN))
                        {
                            ImageInfos[data._IMG_KBN].TrImage = data;
                            ImageInfos[data._IMG_KBN].HasImage = true;
                        }
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
        private TBL_TRFUWATARI Fetch_fuwatari(int gymid, int opedate, string scanterm, int batid, int detailsno)
        {
            string strSQL = TBL_TRFUWATARI.GetSelectQuery(gymid, opedate, scanterm, batid, detailsno, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            TBL_TRFUWATARI fuwa = null;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        fuwa = new TBL_TRFUWATARI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return fuwa;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private void Fetch_tritems(int gymid, int opedate, string scanterm, int batid, int detailsno)
        {
            string strSQL = TBL_TRITEM.GetSelectQuery(gymid, opedate, scanterm, batid, detailsno, AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            tritems = new SortedDictionary<int, TBL_TRITEM>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_TRITEM data = new TBL_TRITEM(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        tritems.Add(data._ITEM_ID, data);
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
        private void Fetch_imgparam(int gymid, int? dspid)
        {
            if (dspid == null) { return; }

            string strSQL = TBL_IMG_PARAM.GetSelectQuery(gymid, DBConvert.ToIntNull(dspid), AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            imgparam = null;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        imgparam = new TBL_IMG_PARAM(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
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
        private void Fetch_tblFuwatari()
        {
            string strSQL = TBL_FUWATARIMF.GetSelectQuery();

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    tblFuwatari = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
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
        private void Fetch_mst_dspitems(int gymid, int? dspid)
        {
            if (dspid == null) { return; }

            string strSQL = TBL_DSP_ITEM.GetSelectQuery(gymid, DBConvert.ToIntNull(dspid), AppInfo.Setting.SchemaBankCD);

            // SELECT実行
            mst_dspitems = new SortedDictionary<int, TBL_DSP_ITEM>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        TBL_DSP_ITEM dspitem = new TBL_DSP_ITEM(tbl.Rows[i], AppInfo.Setting.SchemaBankCD);
                        mst_dspitems.Add(dspitem._ITEM_ID, dspitem);
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
        public bool RegistTrFuwatari(TBL_TRFUWATARI fuwa, EntryForm.MethodType mType)
        {
            string strSQL = "";

            // INSERT or UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // 更新前の最終チェック
                    bool isUpdate = (mType != EntryForm.MethodType.登録);
                    if (isUpdate)
                    {
                        // 排他制御（TRFUWATARI）
                        {
                            strSQL = TBL_TRFUWATARI.GetSelectQuery(fuwa._GYM_ID, fuwa._OPERATION_DATE, fuwa._SCAN_TERM, fuwa._BAT_ID, fuwa._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                            strSQL += DBConvert.QUERY_LOCK;

                            // SELECT 実行（ロック）
                            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                            if (tbl.Rows.Count < 1)
                            {
                                // 取得した行ロックを解除するためロールバック
                                // メッセージボックス表示前に実施
                                auto.isCommitEnd = false;
                                auto.Trans.Rollback();
                                // メッセージ表示
                                ComMessageMgr.MessageWarning(ComMessageMgr.E01004);
                                return false;
                            }

                            // 最新状態を取得する
                            bool isEdited = false;
                            TBL_TRFUWATARI fuwatari_cur = new TBL_TRFUWATARI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                            isEdited |= (fuwatari_cur.m_FUBI_KBN_01 != fuwatari_bef.m_FUBI_KBN_01);
                            isEdited |= (fuwatari_cur.m_FUBI_KBN_02 != fuwatari_bef.m_FUBI_KBN_02);
                            isEdited |= (fuwatari_cur.m_FUBI_KBN_03 != fuwatari_bef.m_FUBI_KBN_03);
                            isEdited |= (fuwatari_cur.m_FUBI_KBN_04 != fuwatari_bef.m_FUBI_KBN_04);
                            isEdited |= (fuwatari_cur.m_FUBI_KBN_05 != fuwatari_bef.m_FUBI_KBN_05);
                            isEdited |= (fuwatari_cur.m_ZERO_FUBINO_01 != fuwatari_bef.m_ZERO_FUBINO_01);
                            isEdited |= (fuwatari_cur.m_ZERO_FUBINO_02 != fuwatari_bef.m_ZERO_FUBINO_02);
                            isEdited |= (fuwatari_cur.m_ZERO_FUBINO_03 != fuwatari_bef.m_ZERO_FUBINO_03);
                            isEdited |= (fuwatari_cur.m_ZERO_FUBINO_04 != fuwatari_bef.m_ZERO_FUBINO_04);
                            isEdited |= (fuwatari_cur.m_ZERO_FUBINO_05 != fuwatari_bef.m_ZERO_FUBINO_05);
                            isEdited |= (fuwatari_cur.m_DELETE_DATE != fuwatari_bef.m_DELETE_DATE);
                            isEdited |= (fuwatari_cur.m_DELETE_FLG != fuwatari_bef.m_DELETE_FLG);
                            if (isEdited)
                            {
                                // 取得した行ロックを解除するためロールバック
                                // メッセージボックス表示前に実施
                                auto.isCommitEnd = false;
                                auto.Trans.Rollback();
                                // メッセージ表示
                                ComMessageMgr.MessageWarning(ComMessageMgr.E01004);
                                return false;
                            }
                        }

                        // 排他制御（TRMEI）
                        {
                            strSQL = TBL_TRMEI.GetSelectQuery(fuwa._GYM_ID, fuwa._OPERATION_DATE, fuwa._SCAN_TERM, fuwa._BAT_ID, fuwa._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                            strSQL += DBConvert.QUERY_LOCK;

                            // SELECT 実行（ロック）
                            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                            if (tbl.Rows.Count < 1)
                            {
                                // 取得した行ロックを解除するためロールバック
                                // メッセージボックス表示前に実施
                                auto.isCommitEnd = false;
                                auto.Trans.Rollback();
                                // メッセージ表示
                                ComMessageMgr.MessageWarning(ComMessageMgr.E01004);
                                return false;
                            }

                            // 最新状態を取得する
                            bool isEdited = false;
                            TBL_TRMEI trmei_cur = new TBL_TRMEI(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);
                            isEdited |= (trmei_cur.m_GRA_STS != trmei_bef.m_GRA_STS);
                            isEdited |= (trmei_cur.m_GRA_CONFIRMDATE != trmei_bef.m_GRA_CONFIRMDATE);
                            if (isEdited)
                            {
                                // 取得した行ロックを解除するためロールバック
                                // メッセージボックス表示前に実施
                                auto.isCommitEnd = false;
                                auto.Trans.Rollback();
                                // メッセージ表示
                                ComMessageMgr.MessageWarning(ComMessageMgr.E01004);
                                return false;
                            }
                        }
                    }

                    // TRFUWATARI 更新
                    switch (mType)
                    {
                        case EntryForm.MethodType.登録:
                            strSQL = fuwa.GetInsertQuery();
                            break;
                        case EntryForm.MethodType.更新:
                            strSQL = fuwa.GetUpdateQuery();
                            break;
                        case EntryForm.MethodType.削除:
                            strSQL = fuwa.GetDeleteQuery();
                            break;
                    }
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // TRMEI 更新
                    if (isUpdate)
                    {
                        strSQL = trmei.GetUpdateQuery();
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }
                }
                catch (Exception ex)
                {
                    // 取得した行ロックを解除するためロールバック
                    // メッセージボックス表示前に実施
                    auto.isCommitEnd = false;
                    auto.Trans.Rollback();
                    // メッセージ表示
                    if (ex.Message.IndexOf(Const.ORACLE_ERR_LOCK) != -1)
                    {
                        // ロック中
                        ComMessageMgr.MessageWarning(ComMessageMgr.E01003);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    }
                    else if (ex.Message.IndexOf(Const.ORACLE_ERR_UNIQUE) != -1)
                    {
                        // 一意制約
                        ComMessageMgr.MessageWarning(ComMessageMgr.E01004);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    }
                    else
                    {
                        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public List<string> Fuwa0JiyuCdList { get; set; } = null;
            public bool Fuwa0 { get; set; } = false;
            public bool Fuwa1 { get; set; } = false;
            public bool Fuwa2 { get; set; } = false;

            public void Clear()
            {
                Fuwa0JiyuCdList = new List<string>();
                Fuwa0 = false;
                Fuwa1 = false;
                Fuwa2 = false;
            }
        }

        public class ImageInfo
        {
            public int ImgKbn { get; private set; }
            public bool HasImage { get; set; } = false;
            public TBL_TRMEIIMG TrImage { get; set; } = null;

            public ImageInfo(int imgKbm)
            {
                ImgKbn = imgKbm;
            }
        }

    }
}
