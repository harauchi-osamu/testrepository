using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using EntryCommon;
using System.IO;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using ImageController;

namespace ImageOperation
{
    /// <summary>
    /// イメージ選択画面
    /// </summary>
    public partial class ImageSelect : EntryCommonFormBase
    {
        private ControllerBase _ctl = null;

        private int _gymid;
        private int _operationdate;
        private string _scanterm;
        private int _batid;
        private int _detailsno;
        private int _imgkbn;
        private int _SchemaBankCD;
        private string _ImageReplaceRoot;
        private string _ImageBackUpRoot;
        private string _BankNormalImageRoot;
        private string _BankFutaiImageRoot;
        private string _BankInventoryImageRoot;
        private string _Tegata;
        private string _Kogitte;
        private string _DstDpi;
        private string _Quality;
        private string _ScanImageBackUpRoot;

        private List<ItemSet> _pathList = null;
        private bool _isImageLoaded = false;

        private ImageList _imgList = null;
        private Dictionary<string, bool> _imgListFileName = null;
        private ImageEditor _editor = new ImageEditor();    // 回転等は行わないためEditorは一つを流用
        private bool _DisplayingImageList = false;
        CancellationTokenSource _tokenSource = null;

        /// <summary>イメージリストのイメージサイズ</summary>
        private Size imgListSize
        {
            get { return new Size(256, 139); }
        }

        /// <summary>基フォルダ表示中フラグ</summary>
        private bool DisplayingImageList
        {
            get { return _DisplayingImageList; }
        }

        private const int LISTIMAGEPADING = 10;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private ImageSelect()
        {
            InitializeComponent();

            _editor = new ImageEditor();
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
        /// <param name="ImageReplaceRoot">CtrServer.iniの ImageReplaceRoot</param>
        /// <param name="ImageBackUpRoot">CtrServer.iniの ImageBackUpRoot</param>
        /// <param name="BankNormalImageRoot">CtrServer.iniの BankNormalImageRoot</param>
        /// <param name="BankFutaiImageRoot">CtrServer.iniの BankFutaiImageRoot</param>
        /// <param name="BankInventoryImageRoot">CtrServer.iniの BankInventoryImageRoot</param>
        /// <param name="Tegata">CtrServer.iniの Tegata</param>
        /// <param name="Kogitte">CtrServer.iniの Kogitte</param>
        /// <param name="DstDpi">CtrServer.iniの DstDpi</param>
        /// <param name="Quality">CtrServer.iniの Quality</param>
        /// <param name="ScanImageBackUpRoot">CtrServer.iniの ScanImageBackUpRoot</param>
        public ImageSelect(int gymid, int operationdate, string scanterm, int batid, int detailsno, int imgkbn, int SchemaBankCD,
            string ImageReplaceRoot, string ImageBackUpRoot, string BankNormalImageRoot, string BankFutaiImageRoot, string BankInventoryImageRoot,
            string Tegata, string Kogitte, string DstDpi, string Quality, string ScanImageBackUpRoot)
        {
            InitializeComponent();

            _gymid = gymid;
            _operationdate = operationdate;
            _scanterm = scanterm;
            _batid = batid;
            _detailsno = detailsno;
            _imgkbn = imgkbn;
            _SchemaBankCD = SchemaBankCD;
            _ImageReplaceRoot = ImageReplaceRoot;
            _ImageBackUpRoot = ImageBackUpRoot;
            _BankNormalImageRoot = BankNormalImageRoot;
            _BankFutaiImageRoot = BankFutaiImageRoot;
            _BankInventoryImageRoot = BankInventoryImageRoot;
            _Tegata = Tegata;
            _Kogitte = Kogitte;
            _DstDpi = DstDpi;
            _Quality = Quality;
            _ScanImageBackUpRoot = ScanImageBackUpRoot;
            SetFileList(ImageReplaceRoot, ImageBackUpRoot);

            // 非同期処理をCancelするためのTokenを取得.
            _tokenSource = new CancellationTokenSource();

            _imgListFileName = new Dictionary<string, bool>();
            imgListCreate();

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("イメージ選択 バッチ番号:{0} 明細番号:{1} 表裏等の別:{2}", batid, detailsno, imgkbn), 1);
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

            //イメージ情報の取得
            GetImageData(_tokenSource.Token);

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
            base.SetDispName2("イメージ選択");
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
            SetFunctionName(4, string.Empty);
            SetFunctionName(5, string.Empty);
            SetFunctionName(6, string.Empty);
            SetFunctionName(7, string.Empty);
            SetFunctionName(8, string.Empty);
            SetFunctionName(9, string.Empty);
            SetFunctionName(10, string.Empty);
            SetFunctionName(11, string.Empty);
            SetFunctionName(12, "決定");
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
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

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (lvImageList.Focused)
                    {
                        btnFunc12_Click(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// [バッチ一覧] マウスダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBatList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnFunc12_Click(sender, e);
        }

        /// <summary>
        /// [差替イメージ選択] SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isImageLoaded) { return; }
            try
            {
                //イメージ情報の取得
                GetImageData(_tokenSource.Token);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
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
        /// F12：決定
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            if (!_isImageLoaded) { return; }
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "決定", 1);

                // イメージ選択
                DialogResult res = ExecSelect();
                if (res == DialogResult.Cancel || res == DialogResult.OK)
                {
                    this.DialogResult = res;
                    this.Close();
                    return;
                }
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
        /// プルダウン
        /// </summary>
        /// <returns></returns>
        private void SetFileList(string ImageReplaceRoot, string ImageBackUpRoot)
        {
            _pathList = new List<ItemSet>();
            _pathList.Add(new ItemSet(ImageReplaceRoot, "スキャン差替フォルダ情報"));
            _pathList.Add(new ItemSet(ImageBackUpRoot, "スキャン退避フォルダ情報"));
            cmbFileList.DataSource = _pathList;
            cmbFileList.DisplayMember = "ItemDisp";
            cmbFileList.ValueMember = "ItemValue";
        }

        /// <summary>
        /// 決定処理
        /// </summary>
        /// <returns></returns>
        private DialogResult ExecSelect()
        {
            if (lvImageList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("画像を選択してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return DialogResult.No;
            }

            // 入力項目取得
            string dirPath = (cmbFileList.SelectedIndex < 0) ? "" : ((ItemSet)cmbFileList.SelectedItem).ItemValue;
            string selectFileName = lvImageList.SelectedItems[0].SubItems[0].Text;

            // ファイル一覧生成
            int idx = 0;
            SortedDictionary<int, string> fileNameList = new SortedDictionary<int, string>();
            foreach (ListViewItem item in lvImageList.Items)
            {
                fileNameList.Add(idx++, item.Text);
            }

            // 選択中ファイル取得
            var filesIdx = fileNameList.Where(p => p.Value.Equals(selectFileName)).Select(p => p.Key);
            if (filesIdx.Count() < 1)
            {
                return DialogResult.No;
            }
            int selectedIndex = filesIdx.First();

            // イメージ差替画面
            ImageReplace form = new ImageReplace(_gymid, _operationdate, _scanterm, _batid, _detailsno, _imgkbn, _SchemaBankCD,
                _ImageBackUpRoot, _BankNormalImageRoot, _BankFutaiImageRoot, _BankInventoryImageRoot,
                _Tegata, _Kogitte, _DstDpi, _Quality, _ScanImageBackUpRoot,
                dirPath, fileNameList, selectedIndex);
            form.InitializeForm(_ctl);
            return form.ShowDialog();
        }


        // *******************************************************************
        // 内部メソッド（イメージ関連）
        // *******************************************************************

        /// <summary>
        /// 基フォルダ画像データ一式取得
        /// </summary>

        private async void GetImageData(CancellationToken cancelToken)
        {
            _isImageLoaded = false;

            // プルダウン取得
            string dirPath = (cmbFileList.SelectedIndex < 0) ? "" : ((ItemSet)cmbFileList.SelectedItem).ItemValue;

            // 初期処理
            lvImageList.Enabled = false;
            _DisplayingImageList = true;

            //初期化
            imgListCreate();
            _imgListFileName.Clear();

            // 画像データの取得に時間が掛かるため別スレッド化
            SetWaitCursor();
            bool RumRtn = await Task.Run(() =>
            {
                ImageCanvas canvas = null;

                // イメージ一覧取得
                string FolderPath = dirPath;
                IEnumerable<string> FileList = Directory.EnumerateFiles(FolderPath, "*.jpg").Select(name => Path.GetFileName(name)).OrderBy(name => name);

                lock (_imgList)
                {
                    try
                    {
                        canvas = new ImageCanvas(_editor);

                        foreach (string name in FileList)
                        {
                            if (cancelToken.IsCancellationRequested)
                            {
                                // キャンセルされたらTaskを終了する.
                                return false;
                            }

                            // 画像読込
                            canvas.InitializeCanvas(Path.Combine(FolderPath, name));
                            canvas.SetDefaultReSize(_imgList.ImageSize.Width, _imgList.ImageSize.Height);

                            //背景色設定
                            bool DragFlg = false;
                            Color color = Color.White;

                            // 全体表示 ・設定
                            canvas.ToFitCanvasAspect(ImageCanvas.FitAlignType.Center, LISTIMAGEPADING, color);
                            _imgListFileName.Add(name, DragFlg);
                            _imgList.Images.Add(name, _editor.CloneCanvas(canvas.ResizeCanvas));
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        return false;
                    }
                    finally
                    {
                        if (canvas != null) canvas.Dispose();
                    }

                    _isImageLoaded = true;
                    return true;
                }
            }, cancelToken);
            ResetCursor();

            if (!RumRtn)
            {
                this.SetStatusMessage("差替基イメージフォルダ画像の取得に失敗しました。");
                return;
            }

            // 基フォルダ一覧設定
            ImageListUpdate();
            lvImageList.Enabled = true;
            _DisplayingImageList = false;
            this.ClearStatusMessage();
            RefreshDisplayState();

            return;
        }

        /// <summary>
        /// _imgList変数初期化
        /// </summary>
        private void imgListCreate()
        {
            if (_imgList != null)
            {
                imgListClear();
            }

            _imgList = new ImageList();
            _imgList.ColorDepth = ColorDepth.Depth24Bit;
            _imgList.ImageSize = imgListSize;
            _imgList.TransparentColor = Color.Transparent;
        }

        /// <summary>
        /// _imgList変数クリア
        /// </summary>
        private void imgListClear()
        {
            if (_imgList != null)
            {
                foreach (Image img in _imgList.Images)
                {
                    img.Dispose();
                }
                _imgList.Dispose();
                _imgList = null;
            }
        }

        /// <summary>
        /// 基フォルダ一覧設定
        /// </summary>
        private void ImageListUpdate()
        {
            // 基イメージ箇所設定
            lvImageList.BeginUpdate();
            lvImageList.Clear();
            lvImageList.LargeImageList = _imgList;
            lvImageList.SmallImageList = _imgList;
            lvImageList.View = View.LargeIcon;
            lvImageList.Columns.Add("", _imgList.ImageSize.Width);
            lvImageList.HeaderStyle = ColumnHeaderStyle.None;

            foreach (string imgname in _imgListFileName.Keys)
            {
                lvImageList.Items.Add(imgname, imgname);
            }
            // 初期位置設定
            SetImageListDefPositon();
            lvImageList.EndUpdate();
        }

        /// <summary>
        /// 基フォルダ初期位置設定
        /// </summary>

        private void SetImageListDefPositon()
        {
            if (lvImageList.Items.Count < 1) { return; }

            // 初期位置設定
            lvImageList.Items[0].EnsureVisible();
        }

        public class ItemSet
        {
            public string ItemValue { get; set; }
            public string ItemDisp { get; set; }
            public ItemSet(string value, string disp)
            {
                ItemValue = value;
                ItemDisp = disp;
            }
        }
    }
}
