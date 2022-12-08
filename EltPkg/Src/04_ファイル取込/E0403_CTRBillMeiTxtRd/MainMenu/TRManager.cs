using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;
using IFFileOperation;
using IFImportCommon;

namespace MainMenu
{
    /// <summary>
    /// トランザクションデータ登録処理管理クラス
    /// </summary>
    public class TRManager
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private int _GymDate = 0;
        private int _BankCode = 0;
        private int _BatchNumber = 0;
        private int _BRNo = 0;
        private int _OCDate = 0;
        private IEnumerable<IFData> _impData = null;
        private ImportTRAccessComp _TRAccess = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TRManager(Controller ctl, int GymDate, int BankCode, int BatchNumber, int BRNo, int OCDate,  IEnumerable<IFData> impData)
        {
            _ctl = ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
            _GymDate = GymDate;
            _BankCode = BankCode;
            _BatchNumber = BatchNumber;
            _BRNo = BRNo;
            _OCDate = OCDate;
            _impData = impData;

            // DB登録クラス作成
            _TRAccess = new ImportTRAccessComp(GymDate, BatchNumber, "", BankCode, NCR.Terminal.Number, NCR.Operator.UserID, 
                                               _itemMgr._chgdspidMF, GetItemMF(BankCode), GetDspItemMF(BankCode), new EntryReplacer());
        }

        /// <summary>
        /// データ登録処理
        /// </summary>
        public bool TRDataInput(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            //端末IPアドレスの取得
            string TermIPAddress = ImportFileAccess.GetTermIPAddress().Replace(".", "_");

            // 登録処理
            try
            {
                // バッチ単位での処理（持出支店コード・持出日）
                int DetailCnt = 0;
                int ImgCnt = 0;
                long DetailImportTotalAmt = 0;

                CTRTxtRd txtRd = new CTRTxtRd(_BankCode, dbp);

                //証券単位でのループ
                foreach (IGrouping<string, IFData> FrontNameGrp in _impData.GroupBy(x => x.LineData["表証券イメージファイル名"]))
                {
                    if (ChkFileInput(FrontNameGrp.Key, _BankCode, dbp, Tran))
                    {
                        // 表面がすでに登録済の場合次の処理
                        continue;
                    }

                    ImportFileAccess.DetailCtl Detail = new ImportFileAccess.DetailCtl();
                    string Amt = new string('Z', 12);
                    string IcBKNo = new string('Z', 4);
                    string ClearingDate = new string('Z', 8);
                    string PayKbn = new string('Z', 1);

                    // イメージ単位でのループ
                    foreach (IFData item in FrontNameGrp)
                    {
                        //DetailCtl設定
                        setDetailCtl(item, txtRd, Detail, ref Amt, ref IcBKNo, ref ClearingDate, ref PayKbn);
                    }

                    if (Amt == new string('Z', 12) || IcBKNo == new string('Z', 4) || ClearingDate == new string('Z', 8) || PayKbn == new string('Z', 1))
                    {
                        throw new Exception("当日持出持出明細の電子交換所認識値が不正です");
                    }

                    //ファイル各項目設定
                    foreach (ImportFileAccess.FileCtl filectl in Detail.FileList)
                    {
                        string FileName = filectl.OldFileName;
                        filectl.OldFileName = string.Empty;
                        filectl.NewFileName = FileName;

                        filectl.OC_BKNO = FileName.Substring(0, 4);  // 持出銀行コード
                        filectl.OC_BRNO = FileName.Substring(4, 4);  // 持出支店コード
                        filectl.IC_BKNO = FileName.Substring(8, 4);  // 持帰銀行コード
                        filectl.OC_DATE = FileName.Substring(12, 8);  // 持出日
                        filectl.CLEARING_DATE = FileName.Substring(20, 8);  // 交換希望日
                        filectl.AMOUNT = FileName.Substring(28, 12);  // 金額
                        filectl.PAY_KBN = FileName.Substring(40, 1);  // 決済対象区分
                        filectl.UNIQUECODE = FileName.Substring(41, 15);  // 持出銀行で一意となるコード
                        filectl.IMG_KBN = ((int)filectl.IMG_KBNCD).ToString("D2");  // 表・裏等の別
                        filectl.EXTENSION = ".jpg";  // 拡張子
                    }

                    // 通知テキストでの更新値を取得
                    GetTsuchiTxtChgData(FrontNameGrp.Key,
                                        out int BUADate, out int GRADate, out int DelDate, out int DelFlg,
                                        dbp, Tran);

                    //明細トランザクションの登録
                    if (!_TRAccess.InsTRMeiBQA(TermIPAddress, DetailCnt + 1, 999, string.Empty, 999, out int DspID, BUADate, GRADate, DelDate, DelFlg, dbp, Tran))
                    {
                        //catch でリネームの戻し作業等を行う
                        throw new Exception("明細トランザクションの登録に失敗しました");
                    }

                    //明細イメージトランザクションの登録
                    if (!_TRAccess.InsTRMeiImageBQA(TermIPAddress, DetailCnt + 1, Detail, TrMei.Sts.結果正常, DelDate, DelFlg, dbp, Tran))
                    {
                        //catch でリネームの戻し作業等を行う
                        throw new Exception("明細イメージトランザクションの登録に失敗しました");
                    }

                    //補正ステータスの登録
                    if (!_TRAccess.InsTRHoseiData(TermIPAddress, DetailCnt + 1, dbp, Tran))
                    {
                        //catch でリネームの戻し作業等を行う
                        throw new Exception("補正ステータスの登録に失敗しました");
                    }

                    //項目トランザクションの登録
                    if (!_TRAccess.InsTRItemDataBQA(TermIPAddress, DetailCnt + 1, int.Parse(IcBKNo), int.Parse(ClearingDate), long.Parse(Amt), PayKbn, DspID, dbp, Tran))
                    {
                        //catch でリネームの戻し作業等を行う
                        throw new Exception("項目トランザクションの登録に失敗しました");
                    }

                    if (DelFlg != 0)
                    {
                        // 削除で初期登録
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル転送外明細データ登録(削除) バッチ番号:{0} 明細番号:{1}", _BatchNumber.ToString("D8"), (DetailCnt + 1).ToString("D8")), 1);
                    }

                    DetailCnt++;
                    ImgCnt += Detail.FileList.Count();
                    DetailImportTotalAmt += long.Parse(Amt);
                }

                if (DetailCnt == 0) return true;

                // バッチデータの作成
                TBL_TRBATCH InsBatchData = new TBL_TRBATCH(AppInfo.Setting.GymId, _GymDate, TermIPAddress, _BatchNumber, _BankCode);
                InsBatchData.m_STS = 10;    // 状態
                InsBatchData.m_OC_BK_NO = _BankCode;    // 持出銀行
                InsBatchData.m_OC_BR_NO = _BRNo;    // 持出支店
                InsBatchData.m_SCAN_BR_NO = _BRNo;    // スキャン支店
                InsBatchData.m_SCAN_DATE = _OCDate;    // スキャン日
                InsBatchData.m_SCAN_COUNT = ImgCnt;    // スキャン枚数
                InsBatchData.m_TOTAL_COUNT = DetailCnt;    // 合計枚数
                InsBatchData.m_TOTAL_AMOUNT = DetailImportTotalAmt;    // 合計金額
                InsBatchData.m_DELETE_DATE = 0;    // 削除日
                InsBatchData.m_DELETE_FLG = 0;    // 削除フラグ
                InsBatchData.m_INPUT_ROUTE = TrBatch.InputRoute.通常; // 取込ルート
                InsBatchData.m_E_TERM = NCR.Terminal.Number;    // バッチ票入力端末番号
                InsBatchData.m_E_OPENO = NCR.Operator.UserID;    // バッチ票入力オペレーター番号
                InsBatchData.m_E_YMD = _GymDate;    // バッチ票入力日付
                InsBatchData.m_E_TIME = int.Parse(DateTime.Now.ToString("HHmmss"));    // バッチ票入力時間

                //バッチデータの登録
                if (!_TRAccess.InsTRBatch(InsBatchData, dbp, Tran))
                {
                    //catch でリネームの戻し作業等を行う
                    throw new Exception("バッチデータの登録に失敗しました");
                }

                //バッチイメージの作成
                //表・裏のデータ設定枠を作成
                ImportFileAccess.DetailCtl BatchCtl = new ImportFileAccess.DetailCtl();
                BatchCtl.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Omote, string.Empty));
                BatchCtl.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Ura, string.Empty));

                //バッチイメージの登録
                if (!_TRAccess.InsTRBatchIMG(TermIPAddress, BatchCtl, dbp, Tran))
                {
                    //catch でリネームの戻し作業等を行う
                    throw new Exception("バッチイメージの登録に失敗しました");
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル転送外明細データ登録処理終了 バッチ番号:{0}", _BatchNumber.ToString("D8")), 1);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _ctl._TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _ctl._TargetFilename, 3);
                return false;
            }

            return true;
        }

        /// <summary>
        /// DetailCtl設定
        /// </summary>
        private void setDetailCtl(IFData item, CTRTxtRd txtRd, ImportFileAccess.DetailCtl Detail, ref string Amt, ref string IcBKNo, ref string ClearingDate, ref string PayKbn)
        {
            switch (item.LineData["表・裏等の別"])
            {
                case "01":
                    //表証券イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Omote, item.LineData["証券イメージファイル名"]));

                    // 電子交換所認識値取得
                    Amt = txtRd.GetText(item.LineData, "金額", new string('Z', 12));
                    IcBKNo = txtRd.GetText(item.LineData, "持帰銀行コード", new string('Z', 4));
                    ClearingDate = txtRd.GetText(item.LineData, "交換日", new string('Z', 8));
                    PayKbn = txtRd.GetText(item.LineData, "決済対象フラグ", new string('Z', 1));

                    break;
                case "02":
                    //裏証券イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Ura, item.LineData["証券イメージファイル名"]));

                    break;
                case "03":
                    //補箋イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Hosen, item.LineData["証券イメージファイル名"]));

                    break;
                case "04":
                    //付箋イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Fusen, item.LineData["証券イメージファイル名"]));

                    break;
                case "05":
                    //入金証明イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Nyukin, item.LineData["証券イメージファイル名"]));

                    break;
                case "06":
                    //表再送分イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.SaiOmote, item.LineData["証券イメージファイル名"]));

                    break;
                case "07":
                    //裏再送分イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.SaiUra, item.LineData["証券イメージファイル名"]));

                    break;
                case "08":
                    //その他1イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Other1, item.LineData["証券イメージファイル名"]));

                    break;
                case "09":
                    //その他2イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Other2, item.LineData["証券イメージファイル名"]));

                    break;
                case "10":
                    //その他3イメージデータ設定箇所を作成
                    Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Other3, item.LineData["証券イメージファイル名"]));

                    break;
                default:
                    throw new Exception("表・裏等の別の値が不正です");
            }
        }

        /// <summary>
        /// 項目マスタ取得
        /// </summary>
        private List<TBL_ITEM_MASTER> GetItemMF(int SchemaBankCD)
        {
            List<TBL_ITEM_MASTER> ItemMF = new List<TBL_ITEM_MASTER>();

            string strSQL = TBL_ITEM_MASTER.GetSelectQuery(SchemaBankCD);
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        ItemMF.Add(new TBL_ITEM_MASTER(tbl.Rows[i], SchemaBankCD));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return ItemMF;
        }

        /// <summary>
        /// 項目定義マスタ取得
        /// </summary>
        private List<TBL_DSP_ITEM> GetDspItemMF(int SchemaBankCD)
        {
            List<TBL_DSP_ITEM> DspItemMF = new List<TBL_DSP_ITEM>();
            string strSQL = TBL_DSP_ITEM.GetSelectQuery(AppInfo.Setting.GymId, SchemaBankCD);
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DspItemMF.Add(new TBL_DSP_ITEM(tbl.Rows[i], SchemaBankCD));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return DspItemMF;
        }

        /// <summary>
        /// 対象ファイルの登録チェック
        /// </summary>
        private bool ChkFileInput(string fileName, int SchemaBankCD, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            string strSQL = SQLTextImport.GetTRMeiDatafileName(AppInfo.Setting.GymId, fileName, SchemaBankCD);
            DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            if (tbl.Rows.Count == 0) return false;
            return true;
        }

        /// <summary>
        /// 通知テキストでの更新値を取得
        /// </summary>
        private void GetTsuchiTxtChgData(string FrontFileName, 
                                         out int BUADate, out int GRADate, out int DelDate, out int DelFlg,
                                         AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 対象証券の通知テキストを取得
            if (!_itemMgr.GetIcTxtTsuchiTxt(FrontFileName, out List<ItemManager.TsuchiTxtData> tsuchiTxtDatas, dbp, Tran))
            {
                // 処理失敗
                throw new Exception(string.Format("対象証券の通知テキスト取得エラー{0}", FrontFileName));
            }

            ItemManager.TsuchiTxtData TxtData = null;

            // 二重持出処理
            BUADate = 0;
            if (GetFileDividTsuchiTxt(FileParam.FileKbn.BUA, tsuchiTxtDatas, ref TxtData))
            {
                // データがある場合は二重持出通知日(持出)を設定
                BUADate = TxtData.MAKE_DATE;
            }

            // 不渡返還通知(持出)
            GRADate = 0;
            DelDate = 0;
            DelFlg = 0;
            if (GetFileDividTsuchiTxt(FileParam.FileKbn.GRA, tsuchiTxtDatas, ref TxtData))
            {
                switch (TxtData.TsuchiTxt.m_FUBI_REG_KBN)
                {
                    case "1":
                        // 通知テキストの最後の不渡返還登録区分が「1」
                        // 不渡返還通知日(持出)を設定
                        GRADate = TxtData.MAKE_DATE;
                        DelDate = AplInfo.OpDate();
                        DelFlg = 1;
                        break;
                    case "9":
                        // 通知テキストの最後の不渡返還登録区分が「9」
                        GRADate = 0;
                        DelDate = 0;
                        DelFlg = 0;
                        break;
                }
            }

        }

        /// <summary>
        /// 指定ファイル識別区分の通知テキストを取得
        /// </summary>
        /// <remarks>戻り値TrueでTxtData設定なしは不可</remarks>
        private bool GetFileDividTsuchiTxt(string FileDivid, List<ItemManager.TsuchiTxtData> tsuchiTxtDatas,
                                           ref ItemManager.TsuchiTxtData TxtData)
        {
            var Datas = tsuchiTxtDatas.Where(x => x.FILE_DIVID == FileDivid);
            if (Datas.Count() == 0) return false;
            TxtData = Datas.OrderByDescending(x => x.RECV_DATE).ThenByDescending(x => x.RECV_TIME).ThenByDescending(x => x.TsuchiTxt._RECORD_SEQ).First();
            return true;
        }

    }
}
