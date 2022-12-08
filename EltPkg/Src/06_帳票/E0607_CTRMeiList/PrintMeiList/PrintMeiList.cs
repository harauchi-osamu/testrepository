using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using ImageController;

namespace PrintMeiList
{
    public class PrintMeiList
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private CTRMeiListDataSet _ds = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrintMeiList(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
            _ds = new CTRMeiListDataSet();
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 印刷処理
        /// </summary>
        public bool PrintMei()
        {
            try
            {
                // 印刷処理
                return PrintList();
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 印刷を行う
        /// </summary>
        private bool PrintList()
        {
            // 対象データ取得
            if (!_itemMgr.GetPrintData())
            {
                return false;
            }

            // PDF出力のフォルダ作成
            string PDFOutputPath = string.Empty;
            switch (_ctl.PrintType)
            {
                case 2:
                    // PDF
                    PDFOutputPath = GetPDFOutputPath();
                    Directory.CreateDirectory(PDFOutputPath);
                    break;
            }

            // 対象データ取得
            List<ItemManager.PrintData> OutputData = new List<ItemManager.PrintData>();
            foreach (TBL_WK_IMGELIST wk in _itemMgr._printList.OrderBy(x => x.m_SORT_NO))
            {
                //明細データの取得
                if (!_itemMgr.GetMeiPrintData(wk, out List<ItemManager.PrintDetail> details, out int ImgSizeKbn))
                {
                    return false;
                }

                // 出力データを設定
                OutputData.Add(new ItemManager.PrintData(wk, details, ImgSizeKbn));
            }

            // 対象データ
            long TotalCount = OutputData.Sum(x => x.Details.LongCount());
            long PageCount = 1;
            long DetailCount = 1;
            foreach (ItemManager.PrintData wk in OutputData.OrderBy(x => x.WkImgList.m_SORT_NO))
            {
                // 明細単位で出力処理を実施

                // レポート設定
                CrystalDecisions.CrystalReports.Engine.ReportClass reportClass = new CrystalDecisions.CrystalReports.Engine.ReportClass();
                reportClass.FileName = _itemMgr.ReportPath();
                // 用紙を横に設定
                reportClass.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                // イメージサイズ初期設定
                ImgSizeInfo sizeInfo = GetDefImgSizeInfo(reportClass);

                // 初期化
                _ds.Clear();

                // ヘッダー
                CTRMeiListDataSet.HeaderDataTable hTable = _ds.Header;
                CTRMeiListDataSet.HeaderRow hRow = hTable.NewHeaderRow();
                hTable.AddHeaderRow(hRow);

                hTable.Dispose();

                // フッター
                CTRMeiListDataSet.FooterDataTable fTable = _ds.Footer;
                CTRMeiListDataSet.FooterRow fRow = fTable.NewFooterRow();

                // フッダー固定箇所設定
                fRow.フッター固定 = string.Format("＜{0}【{1}】＞ [{2}] {3}",
                                        _itemMgr.GetBank(AppInfo.Setting.SchemaBankCD),
                                        NCR.Server.Environment,
                                        NCR.Operator.UserName,
                                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                fTable.AddFooterRow(fRow);

                // 明細
                CTRMeiListDataSet.DetailDataTable dTable = _ds.Detail;

                foreach (ItemManager.PrintDetail detail in wk.Details.OrderBy(x => x.SortNo))
                {
                    CTRMeiListDataSet.DetailRow dRow = dTable.NewDetailRow();

                    // ページ情報
                    dRow.ページ合計 = TotalCount;
                    dRow.ページ番号 = PageCount;

                    // 業務ID
                    dRow.業務ID = detail.GymId;

                    // 業務
                    switch (detail.GymId)
                    {
                        case GymParam.GymId.持出:
                            dRow.業務 = "業務：交換持出";
                            break;
                        case GymParam.GymId.持帰:
                            dRow.業務 = "業務：交換持帰";
                            break;
                    }
                    // 取込日
                    dRow.取込日 = CommonUtil.ConvToDateFormat(detail.OpeDate, 3);
                    // ファイル名
                    dRow.ファイル名 = detail.ImgFlnm;
                    // バッチ番号
                    dRow.バッチ番号 = detail.BatID.ToString("D6");
                    // 明細番号
                    dRow.明細番号 = detail.DetailNo.ToString("D6");
                    // 表裏等の別
                    dRow.表裏等の別 = GetImgKbnOutput(detail.ImgKbn);
                    // 持出銀行・持出支店
                    switch (detail.GymId)
                    {
                        case GymParam.GymId.持出:
                            dRow.持出銀行 = GetBankOutput(detail.OCBKNo);
                            dRow.持出支店 = GetBranchOutput(detail.OCBRNo);
                            break;
                        case GymParam.GymId.持帰:
                            dRow.持出銀行 = GetBankOutput(detail.ICOCBKNo);
                            break;
                    }
                    // 持帰銀行
                    dRow.持帰銀行 = GetBankOutput(detail.ICBKKNo);
                    //交換日項目名
                    switch (detail.GymId)
                    {
                        case GymParam.GymId.持出:
                            dRow.交換日項目名 = "交換希望日：";
                            break;
                        case GymParam.GymId.持帰:
                            dRow.交換日項目名 = "交換日：";
                            break;
                    }
                    //交換希望日
                    if (int.TryParse(detail.ClearingDate, out int intClearingDate))
                    {
                        dRow.交換希望日 = CommonUtil.ConvToDateFormat(intClearingDate, 3);
                    }
                    //金額
                    if (long.TryParse(detail.Amt, out long longAmt))
                    {
                        dRow.金額 = longAmt;
                    }
                    //持帰のみ
                    switch (detail.GymId)
                    {
                        case GymParam.GymId.持帰:
                            // 証券種類
                            dRow.証券種類 = GetBillCDOutput(detail.BillCD);
                            // 持帰支店
                            dRow.持帰支店 = GetBranchOutput(detail.ICBRNo);
                            // 口座番号
                            dRow.口座番号 = detail.Account;
                            // 手形番号
                            dRow.手形番号 = detail.BillNo;
                            // 手形種類
                            dRow.手形種類 = GetSyuruiCDOutput(detail.SyuruiCD);

                            break;
                    }
                    // 明細イメージ
                    dRow.明細イメージ = GetImageData(detail, wk.ImgSizeKbn, ref sizeInfo);

                    // 追加
                    dTable.Rows.Add(dRow);
                    
                    // ページ件数カウントアップ
                    PageCount++;
                }

                // イメージサイズ情報設定
                SetImgSizeInfo(ref reportClass, sizeInfo);

                //データセット設定
                reportClass.SetDataSource(_ds);
                reportClass.Refresh();

                // レポート出力
                switch (_ctl.PrintType)
                {
                    case 1:
                        // 印刷
                        ReportPrint rptnormal = new ReportPrint("");
                        rptnormal.Print(reportClass, ReportPrint.PrintType.Print, 1);
                        break;
                    case 2:
                        // PDF
                        ReportPrint rptpdf = new ReportPrint(GetPDFFilePath(PDFOutputPath, wk.Details, DetailCount));
                        rptpdf.Print(reportClass, ReportPrint.PrintType.PDF, 1);
                        break;
                }

                // 解放
                reportClass.Close();
                reportClass.Dispose();
                _ds.Clear();

                // 明細件数カウントアップ
                DetailCount++;
            }

            return true;
        }

        /// <summary>
        /// PDF出力フォルダ取得
        /// </summary>
        private string GetPDFOutputPath()
        {
            string Directory = string.Format("{0:D8}{1}_{2}", AplInfo.OpDate(), DateTime.Now.ToString("HHmmss"), _itemMgr.GetTermIPAddressZeroPad());
            return Path.Combine(_itemMgr.ReportFileOutPutPath(), Directory);
        }

        /// <summary>
        /// PDFファイルパス取得
        /// </summary>
        private string GetPDFFilePath(string PDFOutputPath, List<ItemManager.PrintDetail> Details, long Count)
        {
            string ImgFlnm = string.Empty;
            IEnumerable<ItemManager.PrintDetail> ie = Details.Where(x => x.ImgKbn == TrMeiImg.ImgKbn.表);
            if (ie.Count() > 0)
            {
                // 拡張子を除いたファイル名取得
                ImgFlnm = Path.GetFileNameWithoutExtension(ie.First().ImgFlnm);
            }

            return Path.Combine(PDFOutputPath, string.Format("{0:D5}_{1}.pdf", Count, ImgFlnm));
        }

        #region 個別出力制御

        /// <summary>
        /// 銀行出力データ取得
        /// </summary>
        private string GetBankOutput(string bkno)
        {
            if (int.TryParse(bkno, out int intbkno))
            {
                return GetBankOutput(intbkno);
            }
            return bkno;
        }

        /// <summary>
        /// 銀行出力データ取得
        /// </summary>
        private string GetBankOutput(int bkno)
        {
            return string.Format("{0:0000}  {1}", bkno, _itemMgr.GetBank(bkno));
        }

        /// <summary>
        /// 支店出力データ取得
        /// </summary>
        private string GetBranchOutput(string brno)
        {
            if (int.TryParse(brno, out int intbrno))
            {
                return GetBranchOutput(intbrno);
            }
            return brno;
        }

        /// <summary>
        /// 支店出力データ取得
        /// </summary>
        private string GetBranchOutput(int brno)
        {
            return string.Format("{0:0000}  {1}", brno, _itemMgr.GeBranch(brno));
        }

        /// <summary>
        /// 表裏等の別出力データ取得
        /// </summary>
        private string GetImgKbnOutput(int ImgKbn)
        {
            return string.Format("{0:00}  {1}", ImgKbn, TrMeiImg.ImgKbn.GetName(ImgKbn));
        }

        /// <summary>
        /// 証券種類出力データ取得
        /// </summary>
        private string GetBillCDOutput(string billcd)
        {
            if (int.TryParse(billcd, out int intbillcd))
            {
                return GetBillCDOutput(intbillcd);
            }
            return billcd;
        }

        /// <summary>
        /// 証券種類出力データ取得
        /// </summary>
        private string GetBillCDOutput(int billcd)
        {
            return string.Format("{0:000}  {1}", billcd, _itemMgr.GeBill(billcd));
        }

        /// <summary>
        /// 種類出力データ取得
        /// </summary>
        private string GetSyuruiCDOutput(string syuruicd)
        {
            if (int.TryParse(syuruicd, out int intsyuruicd))
            {
                return GetSyuruiCDOutput(intsyuruicd);
            }
            return syuruicd;
        }

        /// <summary>
        /// 種類出力データ取得
        /// </summary>
        private string GetSyuruiCDOutput(int syuruicd)
        {
            return string.Format("{0:000}  {1}", syuruicd, _itemMgr.GeSyurui(syuruicd));
        }

        #endregion

        #region イメージ出力制御

        /// <summary>
        /// 対象イメージのイメージデータを取得
        /// </summary>
        private byte[] GetImageData(ItemManager.PrintDetail detail, int ImgSizeKbn, ref ImgSizeInfo sizeInfo)
        {
            // イメージパス取得
            string ImgPath = Path.Combine(_itemMgr.GetBankBacthFolder(detail.OpeDate, detail.BatID, detail.InputRoute), detail.ImgFlnm);

            ImageEditor editor = new ImageEditor();
            ImageCanvas canvas = new ImageCanvas(editor);

            try
            {
                // 画像読込
                canvas.InitializeCanvasToClone(ImgPath);

                if (detail.ImgKbn == ImgSizeKbn)
                {
                    //基準イメージの場合、イメージサイズ情報更新
                    GetReSizeInfo(canvas, ref sizeInfo);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    canvas.OrgCanvas.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.GetBuffer();
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return null;
            }
            finally
            {
                if (canvas != null) canvas.Dispose();
            }
        }

        /// <summary>
        /// 対象イメージのReSize情報を取得
        /// </summary>
        private void GetReSizeInfo(ImageCanvas canvas, ref ImgSizeInfo sizeInfo)
        {
            float imgTwipW = 0;
            float imgTwipH = 0;

            // イメージサイズをPixcelからTwipに変換 (1インチ = 1440TWIPS)
            imgTwipW = (canvas.OrgCanvas.Width / canvas.OrgCanvas.HorizontalResolution) * 1440;
            imgTwipH = (canvas.OrgCanvas.Height / canvas.OrgCanvas.VerticalResolution) * 1440;

            if (sizeInfo.orgWidth >= imgTwipW && sizeInfo.orgHeight >= imgTwipH)
            {
                // 縦横が初期サイズ以下の場合はイメージサイズそのまま
                ChgImgSizeInfo((int)Math.Floor(imgTwipW), (int)Math.Floor(imgTwipH), ref sizeInfo);

                return;
            }

            // 幅と高さの各拡大縮小率を算出
            float perxA4 = 0;
            float peryA4 = 0;
            perxA4 = sizeInfo.orgWidth / imgTwipW;
            peryA4 = sizeInfo.orgHeight / imgTwipH;

            // 縦と横で拡大率が小さい方を適用する
            if (perxA4 > peryA4)
            {
                // 縦の拡大率を適用
                ChgImgSizeInfo((int)(imgTwipW * peryA4), (int)(imgTwipH * peryA4), ref sizeInfo);
            }
            else
            {
                // 横の拡大率を適用
                ChgImgSizeInfo((int)(imgTwipW * perxA4), (int)(imgTwipH * perxA4), ref sizeInfo);
            }
        }

        /// <summary>
        /// 画像情報の変更
        /// </summary>
        private void ChgImgSizeInfo(int Width, int Height, ref ImgSizeInfo sizeInfo)
        {
            sizeInfo.Width = Width;
            sizeInfo.Height = Height;
            // 表示位置調整
            sizeInfo.Left += (sizeInfo.orgWidth - sizeInfo.Width) / 2;
            sizeInfo.Top += (sizeInfo.orgHeight - sizeInfo.Height) / 2;
        }

        /// <summary>
        /// 画像情報の初期設定取得
        /// </summary>
        private ImgSizeInfo GetDefImgSizeInfo(CrystalDecisions.CrystalReports.Engine.ReportClass reportClass)
        {
            ImgSizeInfo sizeInfo = new ImgSizeInfo();

            sizeInfo.orgHeight = reportClass.ReportDefinition.ReportObjects["明細1"].Height;
            sizeInfo.orgWidth = reportClass.ReportDefinition.ReportObjects["明細1"].Width;
            sizeInfo.orgTop = reportClass.ReportDefinition.ReportObjects["明細1"].Top;
            sizeInfo.orgLeft = reportClass.ReportDefinition.ReportObjects["明細1"].Left;
            sizeInfo.Height = sizeInfo.orgHeight;
            sizeInfo.Width = sizeInfo.orgWidth;
            sizeInfo.Top = sizeInfo.orgTop;
            sizeInfo.Left = sizeInfo.orgLeft;

            return sizeInfo;
        }

        /// <summary>
        /// 画像情報の設定
        /// </summary>
        private void SetImgSizeInfo(ref CrystalDecisions.CrystalReports.Engine.ReportClass reportClass, ImgSizeInfo sizeInfo)
        {
            reportClass.ReportDefinition.ReportObjects["明細1"].Height = sizeInfo.Height;
            reportClass.ReportDefinition.ReportObjects["明細1"].Width = sizeInfo.Width;
            reportClass.ReportDefinition.ReportObjects["明細1"].Top = sizeInfo.Top;
            reportClass.ReportDefinition.ReportObjects["明細1"].Left = sizeInfo.Left;
        }

        /// <summary>
        /// 画像情報
        /// </summary>
        private class ImgSizeInfo
        {
            public int Width { get; set; } = 0;
            public int Height { get; set; } = 0;
            public int Top { get; set; } = 0;
            public int Left { get; set; } = 0;
            public int orgWidth { get; set; } = 0;
            public int orgHeight { get; set; } = 0;
            public int orgTop { get; set; } = 0;
            public int orgLeft { get; set; } = 0;
        }

        #endregion

    }
}
