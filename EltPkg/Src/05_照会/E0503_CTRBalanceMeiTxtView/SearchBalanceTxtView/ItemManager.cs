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
using IFImportCommon;

namespace SearchBalanceTxtView
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        public const float MESSEGELBL_DEFSIZE = 14.25F;

		private MasterManager _masterMgr = null;
        private CTRTxtRd _txtRd = null;

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }
        /// <summary>交換尻照会一覧データ</summary>
        public Dictionary<string, DetailData> BalanceTxt { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            this.DispParams = new DisplayParams();

            // 読取クラス作成
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                _txtRd = new CTRTxtRd(AppInfo.Setting.SchemaBankCD, dbp, 2);
            }
        }

        /// <summary>
        /// テキスト一覧取得
        /// </summary>
        public bool FetchBalanceTextControl(int ListDispLimit, out bool LimitOver, out bool PkgOnly, FormBase form = null)
        {
            // 初期化
            LimitOver = false;
            PkgOnly = false;

            // SELECT実行
            string FileDivid = AppInfo.Setting.GymId == GymParam.GymId.持出 ? "'SPA','SFA'" : "'SPB','SFB'";
            string strSQL = SQLSearch.GetSearchBalanceTxtData(AppInfo.Setting.GymId, FileDivid, 
                                                              DispParams.CtlDate, DispParams.OCBKNo, DispParams.Date, DispParams.ICBKNo,
                                                              DispParams.Amount, DispParams.PayKbn, DispParams.ICFlg, DispParams.Diff, DispParams.Fuwatari, 
                                                              DispParams.ImgFLNm, DispParams.ImgFLNmOpt, 
                                                              AppInfo.Setting.SchemaBankCD, ListDispLimit);
            BalanceTxt = new Dictionary<string, DetailData>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        if (i + 1 > ListDispLimit)
                        {
                            LimitOver = true;
                            break;
                        }

                        DetailData data = new DetailData(tbl.Rows[i], _txtRd, AppInfo.Setting.GymId);
                        string key = CommonUtil.GenerateKey("|", data.TxtName, data.ImgName);
                        BalanceTxt.Add(key, data);
                    }

                    // パッケージのみデータの検索
                    strSQL = SQLSearch.GetSearchBalanceTxtPkgOnlyCount(AppInfo.Setting.GymId, FileDivid,
                                                                       DispParams.CtlDate, NCR.Server.InternalExchange,
                                                                       DispParams.ImgFLNm, DispParams.ImgFLNmOpt,
                                                                       AppInfo.Setting.SchemaBankCD);
                    DataTable tblonly = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tblonly.Rows.Count > 0)
                    {
                        // パッケージのみデータがあり
                        if (DBConvert.ToIntNull(tblonly.Rows[0]["CNT"]) > 0) PkgOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    //if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    if (form != null) { form.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message), System.Drawing.Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE); }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// テキスト一覧取得
        /// パッケージのみ
        /// </summary>
        public bool FetchBalanceTextControlPkgOnly(int ListDispLimit, out bool LimitOver, FormBase form = null)
        {
            // 初期化
            LimitOver = false;

            // パッケージのみデータの検索
            string FileDivid = AppInfo.Setting.GymId == GymParam.GymId.持出 ? "'SPA','SFA'" : "'SPB','SFB'";
            string strSQL = SQLSearch.GetSearchBalanceTxtPkgOnlyData(AppInfo.Setting.GymId, FileDivid,
                                                                     DispParams.CtlDate, NCR.Server.InternalExchange,
                                                                     DispParams.ImgFLNm, DispParams.ImgFLNmOpt,
                                                                     AppInfo.Setting.SchemaBankCD, ListDispLimit);
            BalanceTxt = new Dictionary<string, DetailData>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        if (i + 1 > ListDispLimit)
                        {
                            LimitOver = true;
                            break;
                        }

                        // パッケージデータのみ向けインスタンス
                        DetailData data = new DetailData(tbl.Rows[i], AppInfo.Setting.GymId);
                        string key = CommonUtil.GenerateKey("|", data.TxtName, data.ImgName);
                        BalanceTxt.Add(key, data);
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    //if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                    if (form != null) { form.SetStatusMessageFontSizeAuto(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message), System.Drawing.Color.Salmon, ItemManager.MESSEGELBL_DEFSIZE); }
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
            public int CtlDate { get; set; } = -1;
            public int OCBKNo { get; set; } = -1;
            public int Date { get; set; } = -1;
            public int ICBKNo { get; set; } = -1;
            public long Amount { get; set; } = -1;
            public int PayKbn { get; set; } = -1;
            public int ICFlg { get; set; } = -1;
            public int Diff { get; set; } = -1;
            public int Fuwatari { get; set; } = 0;
            public int PkgOnly { get; set; } = 0;
            public string ImgFLNm { get; set; } = string.Empty;
            public int ImgFLNmOpt { get; set; } = 0;

            public void Clear()
            {
                this.CtlDate = -1;
                this.OCBKNo = -1;
                this.Date = -1;
                this.ICBKNo = -1;
                this.Amount = -1;
                this.PayKbn = -1;
                this.ICFlg = -1;
                this.Diff = -1;
                this.Fuwatari = 0;
                this.PkgOnly = 0;
                this.ImgFLNm = string.Empty;
                this.ImgFLNmOpt = 0;
            }
        }

        /// <summary>
        /// 取得明細データ
        /// </summary>
        public class DetailData
        {
            public int GymId { get; set; } = 0;
            public string TxtName { get; set; } = "";
            public string ImgName { get; set; } = "";
            public string OCMethod { get; set; } = "";
            public string PayKbn { get; set; } = "";
            public string ICFlg { get; set; } = "";
            public string PayICBKNo { get; set; } = "";
            public string PayAmount { get; set; } = "";
            public string ClearingDate { get; set; } = "";
            public string EndICBKNo { get; set; } = "";
            public string EndAmount { get; set; } = "";
            public string EndClearingDate { get; set; } = "";
            public int MeiDelFlg { get; set; } = -1;
            public int MeiGRAConfirmDate { get; set; } = 0;
            public int MeiGRADate { get; set; } = 0;
            public string OCBKNo { get; set; } = "";
            public bool PackageExists { get; set; } = false;
            public bool TxtExists { get; set; } = false;

            /// <summary>
            /// 通常データ向けインスタンス
            /// </summary>
            public DetailData(DataRow dr, CTRTxtRd txtRd, int gymid)
            {
                GymId = gymid;

                // 証券明細はあり固定
                TxtExists = true;

                // 証券明細テキスト一覧
                TBL_BILLMEITXT meitxt = new TBL_BILLMEITXT(dr, AppInfo.Setting.SchemaBankCD);
                Dictionary<string, string> LineData = GetMeilData(meitxt);

                // 個別取得
                TxtName = meitxt._TXTNAME;
                ImgName = meitxt._IMG_NAME;
                OCMethod = meitxt.m_OC_METHOD;
                PayKbn = meitxt.m_PAY_KBN;
                ICFlg = meitxt.m_IC_FLG;

                // パッケージDBデータ
                EndICBKNo = DBConvert.ToStringNull(dr["ITEMICBKNO"]);
                EndAmount = DBConvert.ToStringNull(dr["ITEMAMT"]);
                EndClearingDate = DBConvert.ToStringNull(dr["ITEMCLEARING_DATE"]);

                // 証券明細データ
                OCBKNo = DBConvert.ToStringNull(dr["OCBKNO"]);
                PayICBKNo = txtRd.GetText(LineData, "持帰銀行コード", CommonUtil.PadLeft(string.Empty, 4, "Z"));
                PayAmount = txtRd.GetText(LineData, "金額", CommonUtil.PadLeft(string.Empty, 12, "Z"));
                ClearingDate = txtRd.GetText(LineData, "交換日", CommonUtil.PadLeft(string.Empty, 8, "Z"));

                if (dr["GYM_ID"].Equals(DBNull.Value))
                {
                    PackageExists = false;
                }
                else
                {
                    PackageExists = true;

                    // パッケージデータがある場合のみ設定
                    // 不渡関連のデータ
                    MeiDelFlg = DBConvert.ToIntNull(dr["DELETE_FLG"]);
                    MeiGRAConfirmDate = DBConvert.ToIntNull(dr["GRA_CONFIRMDATE"]);
                    MeiGRADate = DBConvert.ToIntNull(dr["GRA_DATE"]);
                }

                // クリア
                LineData.Clear();
            }

            /// <summary>
            /// パッケージデータのみ向けインスタンス
            /// </summary>
            public DetailData(DataRow dr, int gymid)
            {
                GymId = gymid;

                // 証券明細はなし固定
                TxtExists = false;
                // パッケージはあり固定
                PackageExists = true;

                // 個別取得
                ImgName = DBConvert.ToStringNull(dr["IMG_FLNM"]);
                EndICBKNo = DBConvert.ToStringNull(dr["ITEMICBKNO"]);
                EndAmount = DBConvert.ToStringNull(dr["ITEMAMT"]);
                EndClearingDate = DBConvert.ToStringNull(dr["ITEMCLEARING_DATE"]);
                // 不渡関連のデータ
                MeiDelFlg = DBConvert.ToIntNull(dr["DELETE_FLG"]);
                MeiGRAConfirmDate = DBConvert.ToIntNull(dr["GRA_CONFIRMDATE"]);
                MeiGRADate = DBConvert.ToIntNull(dr["GRA_DATE"]);
            }

            /// <summary>
            /// 証券明細テキスト一覧取得
            /// </summary>
            private Dictionary<string, string> GetMeilData(TBL_BILLMEITXT meitxt)
            {
                Dictionary<string, string> LineData = new Dictionary<string, string>();
                LineData.Add(TBL_BILLMEITXT.TXTNAME, meitxt._TXTNAME);
                LineData.Add(TBL_BILLMEITXT.IMG_NAME, meitxt._IMG_NAME);
                LineData.Add(TBL_BILLMEITXT.FRONT_IMG_NAME, meitxt.m_FRONT_IMG_NAME);
                LineData.Add(TBL_BILLMEITXT.IMG_KBN, meitxt.m_IMG_KBN.ToString());
                LineData.Add(TBL_BILLMEITXT.FILE_OC_BK_NO, meitxt.m_FILE_OC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.CHG_OC_BK_NO, meitxt.m_CHG_OC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.OC_BR_NO, meitxt.m_OC_BR_NO);
                LineData.Add(TBL_BILLMEITXT.OC_DATE, meitxt.m_OC_DATE.ToString());
                LineData.Add(TBL_BILLMEITXT.OC_METHOD, meitxt.m_OC_METHOD);
                LineData.Add(TBL_BILLMEITXT.OC_USERID, meitxt.m_OC_USERID);
                LineData.Add(TBL_BILLMEITXT.PAY_KBN, meitxt.m_PAY_KBN);
                LineData.Add(TBL_BILLMEITXT.BALANCE_FLG, meitxt.m_BALANCE_FLG);
                LineData.Add(TBL_BILLMEITXT.OCR_IC_BK_NO, meitxt.m_OCR_IC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.QR_IC_BK_NO, meitxt.m_QR_IC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.MICR_IC_BK_NO, meitxt.m_MICR_IC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.FILE_IC_BK_NO, meitxt.m_FILE_IC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.CHG_IC_BK_NO, meitxt.m_CHG_IC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.TEISEI_IC_BK_NO, meitxt.m_TEISEI_IC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.PAY_IC_BK_NO, meitxt.m_PAY_IC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.PAYAFT_REV_IC_BK_NO, meitxt.m_PAYAFT_REV_IC_BK_NO);
                LineData.Add(TBL_BILLMEITXT.OCR_IC_BK_NO_CONF, meitxt.m_OCR_IC_BK_NO_CONF);
                LineData.Add(TBL_BILLMEITXT.OCR_AMOUNT, meitxt.m_OCR_AMOUNT);
                LineData.Add(TBL_BILLMEITXT.MICR_AMOUNT, meitxt.m_MICR_AMOUNT);
                LineData.Add(TBL_BILLMEITXT.QR_AMOUNT, meitxt.m_QR_AMOUNT);
                LineData.Add(TBL_BILLMEITXT.FILE_AMOUNT, meitxt.m_FILE_AMOUNT);
                LineData.Add(TBL_BILLMEITXT.TEISEI_AMOUNT, meitxt.m_TEISEI_AMOUNT);
                LineData.Add(TBL_BILLMEITXT.PAY_AMOUNT, meitxt.m_PAY_AMOUNT);
                LineData.Add(TBL_BILLMEITXT.PAYAFT_REV_AMOUNT, meitxt.m_PAYAFT_REV_AMOUNT);
                LineData.Add(TBL_BILLMEITXT.OCR_AMOUNT_CONF, meitxt.m_OCR_AMOUNT_CONF);
                LineData.Add(TBL_BILLMEITXT.OC_CLEARING_DATE, meitxt.m_OC_CLEARING_DATE);
                LineData.Add(TBL_BILLMEITXT.TEISEI_CLEARING_DATE, meitxt.m_TEISEI_CLEARING_DATE);
                LineData.Add(TBL_BILLMEITXT.CLEARING_DATE, meitxt.m_CLEARING_DATE);
                LineData.Add(TBL_BILLMEITXT.QR_IC_BR_NO, meitxt.m_QR_IC_BR_NO);
                LineData.Add(TBL_BILLMEITXT.KAMOKU, meitxt.m_KAMOKU);
                LineData.Add(TBL_BILLMEITXT.ACCOUNT, meitxt.m_ACCOUNT);
                LineData.Add(TBL_BILLMEITXT.BK_CTL_NO, meitxt.m_BK_CTL_NO);
                LineData.Add(TBL_BILLMEITXT.FREEFIELD, meitxt.m_FREEFIELD);
                LineData.Add(TBL_BILLMEITXT.BILL_CODE, meitxt.m_BILL_CODE);
                LineData.Add(TBL_BILLMEITXT.BILL_CODE_CONF, meitxt.m_BILL_CODE_CONF);
                LineData.Add(TBL_BILLMEITXT.QR, meitxt.m_QR);
                LineData.Add(TBL_BILLMEITXT.MICR, meitxt.m_MICR);
                LineData.Add(TBL_BILLMEITXT.MICR_CONF, meitxt.m_MICR_CONF);
                LineData.Add(TBL_BILLMEITXT.BILL_NO, meitxt.m_BILL_NO);
                LineData.Add(TBL_BILLMEITXT.BILL_NO_CONF, meitxt.m_BILL_NO_CONF);
                LineData.Add(TBL_BILLMEITXT.FUBI_KBN_01, meitxt.m_FUBI_KBN_01);
                LineData.Add(TBL_BILLMEITXT.ZERO_FUBINO_01, meitxt.m_ZERO_FUBINO_01.ToString());
                LineData.Add(TBL_BILLMEITXT.FUBI_KBN_02, meitxt.m_FUBI_KBN_02);
                LineData.Add(TBL_BILLMEITXT.ZRO_FUBINO_02, meitxt.m_ZRO_FUBINO_02.ToString());
                LineData.Add(TBL_BILLMEITXT.FUBI_KBN_03, meitxt.m_FUBI_KBN_03);
                LineData.Add(TBL_BILLMEITXT.ZRO_FUBINO_03, meitxt.m_ZRO_FUBINO_03.ToString());
                LineData.Add(TBL_BILLMEITXT.FUBI_KBN_04, meitxt.m_FUBI_KBN_04);
                LineData.Add(TBL_BILLMEITXT.ZRO_FUBINO_04, meitxt.m_ZRO_FUBINO_04.ToString());
                LineData.Add(TBL_BILLMEITXT.FUBI_KBN_05, meitxt.m_FUBI_KBN_05);
                LineData.Add(TBL_BILLMEITXT.ZRO_FUBINO_05, meitxt.m_ZRO_FUBINO_05.ToString());
                LineData.Add(TBL_BILLMEITXT.IC_FLG, meitxt.m_IC_FLG);

                return LineData;
            }

        }
    }
}
