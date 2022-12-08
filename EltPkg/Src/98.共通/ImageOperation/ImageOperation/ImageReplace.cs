using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using System.Linq;
using ImageController;
using System.Drawing.Imaging;

namespace ImageOperation
{
    /// <summary>
    /// イメージ差替画面
    /// </summary>
    public partial class ImageReplace : EntryCommonFormBase
    {
        private ControllerBase _ctl = null;
        private ImageEditor _editor = null;
        private ImageCanvas _canvas = null;

        private int _gymid;
        private int _operationdate;
        private string _scanterm;
        private int _batid;
        private int _detailsno;
        private int _imgkbn;
        private int _SchemaBankCD;
        private string _ImageBackUpRoot;
        private string _BankNormalImageRoot;
        private string _BankFutaiImageRoot;
        private string _BankInventoryImageRoot;
        private string _Tegata;
        private string _Kogitte;
        private string _DstDpi;
        private string _Quality;
        private string _ScanImageBackUpRoot;
        private string _imageDirPath;
        private SortedDictionary<int, string> _fileNameList = null;
        private int _fileIdx;

        private TBL_TRBATCH _trbat = null;
        private TBL_TRMEIIMG _trimg = null;
        private ImageCanvas _cvsImage1 = null;
        private ImageCanvas _cvsImage2 = null;
        private Bitmap _cvsFit1 = null;
        private Bitmap _cvsFit2 = null;

        private const int REDUCE_RATE = 100;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private ImageReplace()
        {
            InitializeComponent();

            _editor = new ImageEditor();
            _canvas = new ImageCanvas(_editor);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gymid">差替前イメージファイルの GYM_ID</param>
        /// <param name="operationdate">差替前イメージファイルの OPERATION_DATE</param>
        /// <param name="scanterm">差替前イメージファイルの SCAN_TERM</param>
        /// <param name="batid">差替前イメージファイルの BAT_ID</param>
        /// <param name="detailsno">差替前イメージファイルの DETAILS_NO</param>
        /// <param name="imgkbn">差替前イメージファイルの IMG_KBN</param>
        /// <param name="SchemaBankCD">差替前イメージファイルの銀行スキーマ</param>
        /// <param name="ImageBackUpRoot">CtrServer.iniの ImageBackUpRoot</param>
        /// <param name="BankNormalImageRoot">CtrServer.iniの BankNormalImageRoot</param>
        /// <param name="BankFutaiImageRoot">CtrServer.iniの BankFutaiImageRoot</param>
        /// <param name="BankInventoryImageRoot">CtrServer.iniの BankInventoryImageRoot</param>
        /// <param name="Tegata">CtrServer.iniの Tegata</param>
        /// <param name="Kogitte">CtrServer.iniの Kogitte</param>
        /// <param name="DstDpi">CtrServer.iniの DstDpi</param>
        /// <param name="Quality">CtrServer.iniの Quality</param>
        /// <param name="ScanImageBackUpRoot">CtrServer.iniの ScanImageBackUpRoot</param>
        /// <param name="imageDirPath">差替イメージファイルフォルダ</param>
        /// <param name="fileNameList">差替イメージファイルリスト</param>
        /// <param name="fileIdx">差替イメージファイルリストのインデックス</param>
        public ImageReplace(int gymid, int operationdate, string scanterm, int batid, int detailsno, int imgkbn, int SchemaBankCD,
            string ImageBackUpRoot, string BankNormalImageRoot, string BankFutaiImageRoot, string BankInventoryImageRoot,
            string Tegata, string Kogitte, string DstDpi, string Quality, string ScanImageBackUpRoot,
            string imageDirPath, SortedDictionary<int, string> fileNameList, int fileIdx)
        {
            InitializeComponent();

            _gymid = gymid;
            _operationdate = operationdate;
            _scanterm = scanterm;
            _batid = batid;
            _detailsno = detailsno;
            _imgkbn = imgkbn;
            _SchemaBankCD = SchemaBankCD;
            _ImageBackUpRoot = ImageBackUpRoot;
            _BankNormalImageRoot = BankNormalImageRoot;
            _BankFutaiImageRoot = BankFutaiImageRoot;
            _BankInventoryImageRoot = BankInventoryImageRoot;
            _Tegata = Tegata;
            _Kogitte = Kogitte;
            _DstDpi = DstDpi;
            _Quality = Quality;
            _ScanImageBackUpRoot = ScanImageBackUpRoot;
            _imageDirPath = imageDirPath;
            _fileNameList = fileNameList;
            _fileIdx = fileIdx;
        }


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        public override void InitializeForm(ControllerBase ctl)
        {
            _ctl = ctl;

            // コントロール初期化
            InitializeControl();

            base.InitializeForm(ctl);
        }

        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

        /// <summary>
        /// 業務名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName1(string dispName)
        {
            base.SetDispName1("交換持出");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("イメージ差替");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // 通常状態
            SetFunctionName(1, "キャンセル", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(2, string.Empty);
            SetFunctionName(3, string.Empty);
            SetFunctionName(4, "選択");
            SetFunctionName(5, string.Empty);
            SetFunctionName(6, "イメージ\n切出", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(7, string.Empty);
            SetFunctionName(8, string.Empty);
            SetFunctionName(9, string.Empty);
            SetFunctionName(10, "前\nイメージ", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(11, "次\nイメージ", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(12, "確定");
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            bool isFirstImage = (_fileIdx < 1);
            bool isLastImage = (_fileNameList.Count <= (_fileIdx + 1));

            SetFunctionState(10, !isFirstImage);
            SetFunctionState(11, !isLastImage);
        }

        /// <summary>
        /// フォームを再描画する
        /// </summary>
        public override void ResetForm()
        {
            // 画面表示データ更新
            InitializeDisplayData();

            // 画面表示データ更新
            RefreshDisplayData();

            // 画面表示状態更新
            RefreshDisplayState();
        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        protected void InitializeControl()
        {
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
            // バッチ取得
            _trbat = GetTrBatch();

            // 項目トランザクション取得
            SortedDictionary<int, TBL_TRITEM> tritems = GetTrItems();
            if (tritems != null)
            {
                int clearingDate = DBConvert.ToIntNull(tritems[DspItem.ItemId.入力交換希望日].m_END_DATA);
                long amount = DBConvert.ToIntNull(tritems[DspItem.ItemId.金額].m_END_DATA);
                int bankCd = DBConvert.ToIntNull(tritems[DspItem.ItemId.持帰銀行コード].m_END_DATA);
                string bankName = DBConvert.ToStringNull(tritems[DspItem.ItemId.持帰銀行名].m_END_DATA);

                // 画面表示項目
                lblClearingDate.Text = CommonUtil.ConvToDateFormat(clearingDate, 3);
                lblKingaku.Text = string.Format("{0:###,###,###,###,##0}", amount);
                lblIcBankCd.Text = bankCd.ToString(Const.BANK_NO_LEN_STR);
                lblIcBankName.Text = bankName;
                lblImgKbn.Text = _imgkbn.ToString("00")+":"+TrMeiImg.ImgKbn.GetName(_imgkbn);              
            }

            // 差替前イメージファイル取得
            _trimg = GetTrImage(_imgkbn);
            if (_trimg != null)
            {
                string imgFilePath = GetImgFilePath(_trbat, _trimg);

                // イメージ描画（差替前）
                MakeView1(ref _cvsImage1, ref _cvsFit1, pictureBox1, imgFilePath);
            }

            // イメージ描画（差替後）
            MakeView2(ref _cvsImage2, ref _cvsFit2, pictureBox2, _fileIdx);
        }

        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        protected override void RefreshDisplayData()
        {
            // 画面項目設定
            SetDisplayParams();
        }

        /// <summary>
        /// 画面表示状態更新
        /// </summary>
        protected override void RefreshDisplayState()
        {
            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 入力データの取得

            return true;
        }

        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
        }


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F1：キャンセル
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "キャンセル", 1);

                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F4：選択
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "選択", 1);

                this.DialogResult = DialogResult.No;
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F6：イメージ切出
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ切出", 1);

                string dirPath = lblReplaceDirName.Text;
                string fileName = lblReplaceFileName.Text;

                // イメージ切出画面                
                ImageCut form = new ImageCut(ImageCut.CutType.ImageImport, dirPath, fileName,
                    _Tegata, _Kogitte, _DstDpi, _Quality, _ScanImageBackUpRoot);
                form.InitializeForm(_ctl);
                DialogResult res = form.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }

                // イメージ描画（差替後）
                MakeView2(ref _cvsImage2, ref _cvsFit2, pictureBox2, _fileIdx);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F10：前イメージ
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "前イメージ", 1);

                _fileIdx--;
                if (_fileIdx < 0)
                {
                    _fileIdx = 0;
                }

                // イメージ描画（差替後）
                MakeView2(ref _cvsImage2, ref _cvsFit2, pictureBox2, _fileIdx);

                // ファンクション状態更新
                SetFunctionState();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F1：次イメージ
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "次イメージ", 1);

                _fileIdx++;
                if (_fileNameList.Count <= _fileIdx)
                {
                    _fileIdx = _fileNameList.Count - 1;
                }

                // イメージ描画（差替後）
                MakeView2(ref _cvsImage2, ref _cvsFit2, pictureBox2, _fileIdx);

                // ファンクション状態更新
                SetFunctionState();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：確定
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 確認メッセージ
                DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "イメージの差替を行いますがよろしいですか？");
                if (res != DialogResult.OK)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);

                // 差替処理
                if (!ExecReplace())
                {
                    return;
                }

                // 完了メッセージ
                ComMessageMgr.MessageInformation("イメージ差替が完了しました。");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 差替処理
        /// </summary>
        /// <returns></returns>
        private bool ExecReplace()
        {
            // 差替後ファイル
            string repFilePath = Path.Combine(lblReplaceDirName.Text, lblReplaceFileName.Text);

            // SELECT実行
            bool res = false;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // イメージトランザクション更新
                    if (_trimg == null)
                    {
                        // イメージ追加
                        res = AddImage(repFilePath, lblReplaceFileName.Text, dbp, non);
                    }
                    else
                    {
                        // イメージ差替
                        res = ReplaceImage(repFilePath, dbp, non);
                    }

                    // コミット
                    non.Trans.Commit();

                    // コミット後に差替元ファイルを削除
                    File.Delete(repFilePath);
                }
                catch (Exception ex)
                {
                    // ロールバック
                    non.Trans.Rollback();
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }
            return res;
        }

        /// <summary>
        /// イメージ追加
        /// </summary>
        /// <returns></returns>
        private bool AddImage(string srcFilePath, string FileName, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // 表イメージ取得
            TBL_TRMEIIMG trimgF = GetTrImage(TrMeiImg.ImgKbn.表);

            // 追加イメージ情報設定
            _trimg = new TBL_TRMEIIMG(_gymid, _operationdate, _scanterm, _batid, _detailsno, _imgkbn, _SchemaBankCD);
            _trimg.m_OC_OC_BK_NO = trimgF.m_OC_OC_BK_NO;
            _trimg.m_OC_OC_BR_NO = trimgF.m_OC_OC_BR_NO;
            _trimg.m_OC_IC_BK_NO = trimgF.m_OC_IC_BK_NO;
            _trimg.m_OC_OC_DATE = trimgF.m_OC_OC_DATE;
            _trimg.m_OC_CLEARING_DATE = trimgF.m_OC_CLEARING_DATE;
            _trimg.m_OC_AMOUNT = trimgF.m_OC_AMOUNT;
            _trimg.m_PAY_KBN = trimgF.m_PAY_KBN;
            _trimg.m_UNIQUE_CODE = trimgF.m_UNIQUE_CODE;
            _trimg.m_FILE_EXTENSION = trimgF.m_FILE_EXTENSION;
            _trimg.m_IMG_FLNM = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                _trimg.m_OC_OC_BK_NO,
                _trimg.m_OC_OC_BR_NO,
                _trimg.m_OC_IC_BK_NO,
                _trimg.m_OC_OC_DATE,
                _trimg.m_OC_CLEARING_DATE,
                _trimg.m_OC_AMOUNT,
                _trimg.m_PAY_KBN,
                _trimg.m_UNIQUE_CODE,
                _trimg._IMG_KBN.ToString("D2"), 
                _trimg.m_FILE_EXTENSION);
            _trimg.m_IMG_FLNM_OLD = FileName;

            // DELETE
            string strSQL = "";
            strSQL = _trimg.GetDeleteQuery();
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

            // INSERT
            strSQL = _trimg.GetInsertQuery();
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

            // ファイル上書きコピー
            FileCopy(srcFilePath, _trbat, _trimg);

            return true;
        }

        /// <summary>
        /// イメージ差替
        /// </summary>
        /// <returns></returns>
        private bool ReplaceImage(string srcFilePath, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // 差替前ファイル
            string orgFilePath = GetImgFilePath(_trbat, _trimg);

            // 退避処理
            if(!ImportFileAccess.FileBackUp(Path.GetDirectoryName(orgFilePath), _trimg.m_IMG_FLNM, _ImageBackUpRoot))
            {
                ComMessageMgr.MessageWarning("ファイルの退避に失敗しました。しばらくしてから再度実行してください。");
                return false;
            }

            // 持出アップロード状態設定
            if (_trimg.m_BUA_STS != TrMei.Sts.未作成)
            {
                _trimg.m_BUA_STS = TrMei.Sts.再作成対象;
            }

            // UPDATE
            string strSQL = "";
            strSQL = _trimg.GetUpdateQuery();
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);

            // ファイル上書きコピー
            FileCopy(srcFilePath, _trbat, _trimg);

            return true;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private TBL_TRBATCH GetTrBatch()
        {
            string strSQL = TBL_TRBATCH.GetSelectQuery(_gymid, _operationdate, _scanterm, _batid, _SchemaBankCD);

            // SELECT実行
            TBL_TRBATCH trbat = null;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        trbat = new TBL_TRBATCH(tbl.Rows[0], _SchemaBankCD);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return null;
                }
            }
            return trbat;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private TBL_TRMEIIMG GetTrImage(int imgKbn)
        {
            string strSQL = TBL_TRMEIIMG.GetSelectQueryNotDel(_gymid, _operationdate, _scanterm, _batid, _detailsno, imgKbn, _SchemaBankCD);

            // SELECT実行
            TBL_TRMEIIMG trimg = null;
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0)
                    {
                        trimg = new TBL_TRMEIIMG(tbl.Rows[0], _SchemaBankCD);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return null;
                }
            }
            return trimg;
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        private SortedDictionary<int, TBL_TRITEM> GetTrItems()
        {
            string strSQL = TBL_TRITEM.GetSelectQuery(_gymid, _operationdate, _scanterm, _batid, _detailsno, _SchemaBankCD);

            // SELECT実行
            SortedDictionary<int, TBL_TRITEM> tritems = new SortedDictionary<int, TBL_TRITEM>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    foreach (DataRow row in tbl.Rows)
                    {
                        TBL_TRITEM data = new TBL_TRITEM(row, _SchemaBankCD);
                        tritems.Add(data._ITEM_ID, data);
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return null;
                }
            }
            if (tritems.Count < 1)
            {
                tritems = null;
            }
            return tritems;
        }


        // *******************************************************************
        // 内部メソッド（イメージ関連）
        // *******************************************************************
        /// <summary>
        /// ファイルの上書きコピー
        /// </summary>
        private void FileCopy(string srcFilePath, TBL_TRBATCH bat, TBL_TRMEIIMG img)
        {
            string dstFilePath = GetImgFilePath(_trbat, _trimg);

            // 対象バッチフォルダの存在チェック
            if (!Directory.Exists(Path.GetDirectoryName(dstFilePath)))
            {
                // なければ作る
                Directory.CreateDirectory(Path.GetDirectoryName(dstFilePath));
            }

            // ファイル上書きコピー
            File.Copy(srcFilePath, dstFilePath, true);
        }

        /// <summary>
        /// イメージファイルパスを取得する
        /// </summary>
        private string GetImgFilePath(TBL_TRBATCH bat, TBL_TRMEIIMG img)
        {
            Dictionary<string, string> pathList = new Dictionary<string, string>();
            pathList.Add("BankNormalImageRoot", _BankNormalImageRoot);
            pathList.Add("BankFutaiImageRoot", _BankFutaiImageRoot);
            pathList.Add("BankKijituImageRoot", _BankInventoryImageRoot);
            return CommonUtil.GetImgFilePath(bat, img, pathList);
        }

        /// <summary>
        /// 指定した描画領域にイメージ画像を読み込む
        /// </summary>
        private void MakeView1(ref ImageCanvas canvas, ref Bitmap cvsFit, PictureBox picBox, string filePath)
        {
            if (canvas != null)
            {
                canvas.Dispose();
                canvas = null;
            }
            if (cvsFit != null)
            {
                cvsFit.Dispose();
                cvsFit = null;
            }

            if (!File.Exists(filePath))
            {
                picBox.Image = null;
                return;
            }

            ImageEditor editor = new ImageEditor();
            canvas = new ImageCanvas(editor, REDUCE_RATE);
            canvas.InitializeCanvas(filePath);
            canvas.SetDefaultReSize(canvas.ResizeCanvas.Width, canvas.ResizeCanvas.Height);

            // 縮小率の小さい方にフィットさせる
            ImageEditor.ImageInfo imgInfo = editor.GetImageInfo(canvas.ResizeCanvas);
            float hReduce = (float)picBox.Width / (float)canvas.ResizeCanvas.Width;
            float vReduce = (float)picBox.Height / (float)canvas.ResizeCanvas.Height;
            if (hReduce > vReduce)
            {
                // 縦に収まらない場合のみ縮小する
                if (vReduce < 1)
                {
                    imgInfo.Width = (int)Math.Round(imgInfo.Width * vReduce);
                    imgInfo.Height = (int)Math.Round(imgInfo.Height * vReduce);
                }
            }
            else
            {
                // 横に収まらない場合のみ縮小する
                if (hReduce < 1)
                {
                    imgInfo.Width = (int)Math.Round(imgInfo.Width * hReduce);
                    imgInfo.Height = (int)Math.Round(imgInfo.Height * hReduce);
                }
            }
            cvsFit = editor.CloneCanvas(canvas.ResizeCanvas, imgInfo);
            picBox.Image = cvsFit;
        }

        /// <summary>
        /// 指定した描画領域にイメージ画像を読み込む
        /// </summary>
        private void MakeView2(ref ImageCanvas canvas, ref Bitmap cvsFit, PictureBox picBox, int fileIdx)
        {
            if (canvas != null)
            {
                canvas.Dispose();
                canvas = null;
            }
            if (cvsFit != null)
            {
                cvsFit.Dispose();
                cvsFit = null;
            }

            if (!_fileNameList.ContainsKey(fileIdx))
            {
                return;
            }

            string fileName = _fileNameList[fileIdx];
            string filePath = Path.Combine(_imageDirPath, fileName);
            if (!File.Exists(filePath))
            {
                picBox.Image = null;
                return;
            }

            ImageEditor editor = new ImageEditor();
            canvas = new ImageCanvas(editor, REDUCE_RATE);
            canvas.InitializeCanvas(filePath);
            canvas.SetDefaultReSize(canvas.ResizeCanvas.Width, canvas.ResizeCanvas.Height);

            // 縮小率の小さい方にフィットさせる
            ImageEditor.ImageInfo imgInfo = editor.GetImageInfo(canvas.ResizeCanvas);
            float hReduce = (float)picBox.Width / (float)canvas.ResizeCanvas.Width;
            float vReduce = (float)picBox.Height / (float)canvas.ResizeCanvas.Height;
            if (hReduce > vReduce)
            {
                // 縦に収まらない場合のみ縮小する
                if (vReduce < 1)
                {
                    imgInfo.Width = (int)Math.Round(imgInfo.Width * vReduce);
                    imgInfo.Height = (int)Math.Round(imgInfo.Height * vReduce);
                }
            }
            else
            {
                // 横に収まらない場合のみ縮小する
                if (hReduce < 1)
                {
                    imgInfo.Width = (int)Math.Round(imgInfo.Width * hReduce);
                    imgInfo.Height = (int)Math.Round(imgInfo.Height * hReduce);
                }
            }
            cvsFit = editor.CloneCanvas(canvas.ResizeCanvas, imgInfo);
            picBox.Image = cvsFit;

            lblReplaceDirName.Text = _imageDirPath;
            lblReplaceFileName.Text = fileName;
        }
    }
}
