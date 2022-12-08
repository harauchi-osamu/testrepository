using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;
using System.IO;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using ImageController;

namespace ImageImportFutai
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class ImageAssignForm : EntryCommonFormBase
    {

        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ImageList _imgList = null;
        private Dictionary<string, bool> _imgListFileName = null;
        private ImageEditor _editor = new ImageEditor();    // 回転等は行わないためEditorは一つを流用
        private Dictionary<int, AssignData> _AssignDataList = null;
        private int _CurCursor = 0;
        private bool _DisplayingImageList = false;
        CancellationTokenSource _tokenSource = null;

        /// <summary>イメージリストのイメージサイズ</summary>
        private Size imgListSize
        {
            get { return new Size(256, 150); }
        }

        /// <summary>基フォルダ表示中フラグ</summary>
        private bool DisplayingImageList
        {
            get { return _DisplayingImageList; }
        }

        /// <summary>
        /// Picture管理クラス
        /// </summary>
        public class AssignData
        {
            public Panel Panel { get; set; } = null;
            public PictureBox Picture { get; set; } = null;
            public ImageCanvas canvasImg { get; set; } = null;

            public AssignData(Panel pnl, PictureBox Pic, ImageCanvas Img)
            {
                Panel = pnl;
                Picture = Pic;
                canvasImg = Img;
            }
        }

        //イメージのPADING
        private const int LISTIMAGEPADING = 10;
        private const int ASSIGNIMAGEPADING = 5;

        private const int F1_ = 1;
        private const int F2_ = 2;
        private const int F3_ = 3;
        private const int F4_ = 4;
        private const int F5_ = 5;
        private const int F6_ = 6;
        private const int F7_ = 7;
        private const int F8_ = 8;
        private const int F9_ = 9;
        private const int F10_ = 10;
        private const int F11_ = 11;
        private const int F12_ = 12;

        private const int SF1_ = 1;
        private const int SF2_ = 2;
        private const int SF3_ = 3;
        private const int SF4_ = 4;
        private const int SF5_ = 5;
        private const int SF6_ = 6;
        private const int SF7_ = 7;
        private const int SF8_ = 8;
        private const int SF9_ = 9;
        private const int SF10_ = 10;
        private const int SF11_ = 11;
        private const int SF12_ = 12;

        private const int CF1_ = 1;
        private const int CF2_ = 2;
        private const int CF3_ = 3;
        private const int CF4_ = 4;
        private const int CF5_ = 5;
        private const int CF6_ = 6;
        private const int CF7_ = 7;
        private const int CF8_ = 8;
        private const int CF9_ = 9;
        private const int CF10_ = 10;
        private const int CF11_ = 11;
        private const int CF12_ = 12;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageAssignForm()
        {
            InitializeComponent();

            // 非同期処理をCancelするためのTokenを取得.
            _tokenSource = new CancellationTokenSource();

            _imgListFileName = new Dictionary<string, bool>(); 
            imgListCreate();

            _AssignDataList = new Dictionary<int, AssignData>();
            _AssignDataList.Add(1, new AssignData(pnlimg1, pbimg1, new ImageCanvas(_editor)));
            _AssignDataList.Add(2, new AssignData(pnlimg2, pbimg2, new ImageCanvas(_editor)));
            _AssignDataList.Add(3, new AssignData(pnlimg3, pbimg3, new ImageCanvas(_editor)));
            _AssignDataList.Add(4, new AssignData(pnlimg4, pbimg4, new ImageCanvas(_editor)));
            _AssignDataList.Add(5, new AssignData(pnlimg5, pbimg5, new ImageCanvas(_editor)));
        }

        /// <summary>
        /// Form_Load
        /// </summary>
        private void ImageAssignForm_Load(object sender, EventArgs e)
        {
            if (DisplayingImageList)
            {
                this.SetStatusMessage("基イメージフォルダ画像取得中です", Color.Transparent);
            }
        }

        /// <summary>
        /// Form_Close
        /// </summary>
        private void ImageAssignForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            imgListClear();
            
            foreach (ImageCanvas Canvas in _AssignDataList.Select(x => x.Value.canvasImg))
            {
                if (Canvas != null) Canvas.Dispose();
            }
            if (_tokenSource != null) _tokenSource.Dispose();
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

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        public override void InitializeForm(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            //明細紐づけデータの取得
            if (!_itemMgr.FetchScanMei())
            {
                //エラーを発生させる
                throw new Exception("明細紐づけデータの取得に失敗しました");
            }

            // 登録データの整合性チェック
            if (!ChkScanMeiData())
            {
                // 削除処理
                if (!ScanMeiManager.ScanMeiAllDelete(_itemMgr))
                {
                    //エラーを発生させる
                    throw new Exception("明細紐づけデータの取得に失敗しました");
                }
                if (!_itemMgr.FetchScanMei())
                {
                    //エラーを発生させる
                    throw new Exception("明細紐づけデータの取得に失敗しました");
                }
            }

            //初期表示明細の設定
            _itemMgr.AssignParams.CurrentDetail = GetMeiMaxData(1);

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
        /// 画面名を設定する
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
            base.SetDispName2("イメージ取込　明細紐づけ");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            bool EnableFlg = !DisplayingImageList;

            if (IsNotPressCSAKey)
            {
                // 通常状態
                SetFunctionName(F1_, "中断");
                SetFunctionName(F2_, string.Empty);
                SetFunctionName(F3_, string.Empty);
                SetFunctionName(F4_, "削除", EnableFlg);
                SetFunctionName(F5_, string.Empty);
                SetFunctionName(F6_, "イメージ\n  切出", EnableFlg, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F7_, string.Empty);
                SetFunctionName(F8_, "選択解除", EnableFlg, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F9_, "戻る", EnableFlg);
                SetFunctionName(F10_, "進む", EnableFlg);
                SetFunctionName(F11_, string.Empty);
                SetFunctionName(F12_, "確定", EnableFlg);
            }
            else if (IsPressShiftKey)
            {
                // Shiftキー押下
                SetFunctionName(SF1_, string.Empty);
                SetFunctionName(SF2_, string.Empty);
                SetFunctionName(SF3_, string.Empty);
                SetFunctionName(SF4_, string.Empty);
                SetFunctionName(SF5_, string.Empty);
                SetFunctionName(SF6_, string.Empty);
                SetFunctionName(SF7_, string.Empty);
                SetFunctionName(SF8_, string.Empty);
                SetFunctionName(SF9_, string.Empty);
                SetFunctionName(SF10_, string.Empty);
                SetFunctionName(SF11_, string.Empty);
                SetFunctionName(SF12_, string.Empty);
            }
            else if (IsPressCtrlKey)
            {
                // Ctrlキー押下
                SetFunctionName(CF1_, string.Empty);
                SetFunctionName(CF2_, string.Empty);
                SetFunctionName(CF3_, string.Empty);
                SetFunctionName(CF4_, string.Empty);
                SetFunctionName(CF5_, string.Empty);
                SetFunctionName(CF6_, string.Empty);
                SetFunctionName(CF7_, string.Empty);
                SetFunctionName(CF8_, string.Empty);
                SetFunctionName(CF9_, string.Empty);
                SetFunctionName(CF10_, string.Empty);
                SetFunctionName(CF11_, string.Empty);
                SetFunctionName(CF12_, string.Empty);
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            bool EnableFlg = !DisplayingImageList;

            if (IsNotPressCSAKey)
            {
                // 通常状態

                if (_itemMgr.AssignParams.CurrentDetail > GetMeiMaxData(0))
                {
                    // 未確定明細の場合、削除は無効
                    SetFunctionName(F4_, string.Empty);
                }
                else
                {
                    // 上記以外は有効
                    SetFunctionName(F4_, "削除", EnableFlg);
                }
                SetFunctionState(F6_, EnableFlg);
                SetFunctionState(F8_, EnableFlg);
                SetFunctionState(F9_, EnableFlg);
                SetFunctionState(F10_, EnableFlg);
                SetFunctionState(F12_, EnableFlg);
            }
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
            // 初期値設定

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
            // フォーカス初期位置設定や各項目のDisable設定を行う

            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>

        protected override void SetDisplayParams()
        {
            // 画面項目を設定する処理はまとめてここに実装してこのメソッドを呼ぶ

            // 情報欄の更新
            SetMeisaiIInfo();

            // カーソルをクリア
            DispFreame(0);

            // Picture初期化
            pbimg1.Image = null; pbimg1.Tag = string.Empty;
            pbimg2.Image = null; pbimg2.Tag = string.Empty;
            pbimg3.Image = null; pbimg3.Tag = string.Empty;
            pbimg4.Image = null; pbimg4.Tag = string.Empty;
            pbimg5.Image = null; pbimg5.Tag = string.Empty;

            // イメージ初期表示
            string FolderPath = _itemMgr.TargetFolderPath();
            foreach (TBL_SCAN_MEI mei in _itemMgr.scan_mei.Values.Where(m => m.m_BATCH_UCHI_RENBAN == _itemMgr.AssignParams.CurrentDetail).ToList())
            {
                if (!_AssignDataList.ContainsKey(mei.m_IMG_KBN))
                {
                    continue;
                }
                // 画面データ表示
                DispPictureBoxImage(FolderPath, mei._IMG_NAME, _AssignDataList[mei.m_IMG_KBN].Picture, _AssignDataList[mei.m_IMG_KBN].canvasImg);
            }
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
        /// 画像ドラッグ＆ドロップ
        /// </summary>
        private void pnlimg_DragDrop(object sender, DragEventArgs e)
        {
            string fileName = (string)e.Data.GetData(DataFormats.Text);
            if (string.IsNullOrEmpty(fileName) && !_imgListFileName.ContainsKey(fileName))
            {
                ComMessageMgr.MessageWarning("不正なドラッグ＆ドロップです");
                return;
            }

            Control Target = (Control)sender;
            if (Target == null || _AssignDataList.Where(x => x.Value.Panel.Name == Target.Name).Count() == 0)
            {
                ComMessageMgr.MessageWarning("不正なドラッグ＆ドロップです");
                return;
            }

            int i = _AssignDataList.First(x => x.Value.Panel.Name == Target.Name).Key; 
            if (!string.IsNullOrEmpty((string)_AssignDataList[i].Picture.Tag))
            {
                ComMessageMgr.MessageWarning("明細紐づけ済の箇所には設定できません");
                return;
            }

            lvImageList.BeginUpdate();
            string FolderPath = _itemMgr.TargetFolderPath();
            // 基イメージ置き換え
            ReplaceImageData(FolderPath, fileName, true);
            lvImageList.EndUpdate();

            // 画面データ表示
            DispPictureBoxImage(FolderPath, fileName, _AssignDataList[i].Picture, _AssignDataList[i].canvasImg);

            return;
        }

        /// <summary>
        /// 画像ドラッグEnter
        /// </summary>
        private void pnlimg_DragEnter(object sender, DragEventArgs e)
        {
            string fileName = (string)e.Data.GetData(DataFormats.Text);
            if (string.IsNullOrEmpty(fileName) && !_imgListFileName.ContainsKey(fileName))
            {
                return;
            }
            Control Target = (Control)sender;
            if (Target == null || _AssignDataList.Where(x => x.Value.Panel.Name == Target.Name).Count() == 0)
            {
                return;
            }
            int i = _AssignDataList.First(x => x.Value.Panel.Name == Target.Name).Key;
            if (!string.IsNullOrEmpty((string)_AssignDataList[i].Picture.Tag))
            {
                //イメージ割り当て済の場合不可
                e.Effect = DragDropEffects.None;
                return;
            }
            
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// 画像ドラッグ開始
        /// </summary>
        private void lvImageList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string filename = ((ListViewItem)e.Item).Text;
            if (!_imgListFileName.ContainsKey(filename)) return;
            if (filename == _itemMgr.InputParams.BatchImage.Front ||
                filename == _itemMgr.InputParams.BatchImage.Back)
            {
                return;
            }

            if (_imgListFileName[filename])
            {
                ComMessageMgr.MessageWarning("明細紐づけ済のイメージは設定できません");
                return;
            }

            // ドラッグ処理開始
            lvImageList.DoDragDrop(((ListViewItem)e.Item).Text, DragDropEffects.Copy | DragDropEffects.Move);
        }

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            //if (!DisplayingImageList)
            //{
            //    this.ClearStatusMessage();
            //}
            if (ChangeFunction(e)) SetFunctionState(); return;
        }

        /// <summary>
        /// [画面項目] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyUp(object sender, KeyEventArgs e)
        {
            if (ChangeFunction(e)) SetFunctionState(); return;
        }

        /// イメージ制御 KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numCur_KeyDown(object sender, KeyEventArgs e)
        {
            if (_CurCursor < 1)
            {
                return;
            }

            int i = _CurCursor;
            switch (e.KeyCode) 
            {
                case Keys.Left:
                    // １つ前に
                    i -= 1;
                    if (_AssignDataList.ContainsKey(i))
                    {
                        DispFreame(i);
                    }
                    e.Handled = true;
                    break;
                case Keys.Right:
                    // １つ先に
                    i += 1;
                    if (_AssignDataList.ContainsKey(i))
                    {
                        DispFreame(i);
                    }
                    e.Handled = true;
                    break;
                case Keys.Up:
                    // ２つ前に
                    i -= 2;
                    if (_AssignDataList.ContainsKey(i))
                    {
                        DispFreame(i);
                    }
                    e.Handled = true;
                    break;
                case Keys.Down:
                    // ２つ先に
                    i += 2;
                    if (_AssignDataList.ContainsKey(i))
                    {
                        DispFreame(i);
                    }
                    e.Handled = true;
                    break;
                case Keys.Tab:
                    i += 1;
                    if (e.Shift) i -= 2; ;
                    if (_AssignDataList.ContainsKey(i))
                    {
                        DispFreame(i);
                    }
                    e.Handled = true;
                    break;
                default:
                    if (ChangeFunction(e)) SetFunctionState();
                    break;
            }
        }

        private void pbimg_Click(object sender, EventArgs e)
        {
            Control Target = (Control)sender;
            if (Target == null || _AssignDataList.Where(x => x.Value.Picture.Name == Target.Name).Count() == 0)
            {
                return;
            }

            int i = _AssignDataList.First(x => x.Value.Picture.Name == Target.Name).Key;
            DispFreame(i);
            numimg.Focus();
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************
        /// <summary>
        /// F1：中断
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            try
            {
                //変更有の場合、確認メッセージ表示
                if (AssignChg() && (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "変更した内容は破棄されますよろしいですか？") == DialogResult.No))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "中断", 1);

                //画面表示終了
                _tokenSource.Cancel();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }
        /// <summary>
        /// F4：削除
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (GetMeiMaxData(0) <= 1)
                {
                    ComMessageMgr.MessageWarning("残明細が1枚なので削除できません");
                    return;
                }

                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "この明細を削除してもよろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("削除 明細番号:{0}", _itemMgr.AssignParams.CurrentDetail), 1);

                // 削除処理
                if (!ScanMeiManager.ScanMeiDelete(_itemMgr))
                {
                    return;
                }

                //明細紐づけデータの取得
                if (!_itemMgr.FetchScanMei())
                {
                    ComMessageMgr.MessageWarning("明細紐づけデータの取得に失敗しました");
                    return;
                }

                // 基フォルダ更新
                lvImageList.BeginUpdate();
                ClearImageData(_itemMgr.TargetFolderPath());
                lvImageList.EndUpdate();

                // 前明細表示
                if (_itemMgr.AssignParams.CurrentDetail > 1)
                {
                    _itemMgr.AssignParams.CurrentDetail--;
                }

                // 画面表示データ更新
                RefreshDisplayData();
                // 画面表示状態更新
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F6：イメージ切り出し
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                if (_CurCursor < 1 || string.IsNullOrEmpty((string)_AssignDataList[_CurCursor].Picture.Tag))
                {
                    ComMessageMgr.MessageWarning("イメージが選択されていません");
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ切り出し", 1);

                // イメージ切り出し画面表示
                string FolderPath = _itemMgr.TargetFolderPath();
                string FileName = (string)_AssignDataList[_CurCursor].Picture.Tag;
                ImageOperation.ImageCut form = new ImageOperation.ImageCut(ImageOperation.ImageCut.CutType.ImageImport, FolderPath, FileName, 
                                                                           NCR.Server.Tegata, NCR.Server.Kogitte, NCR.Server.DstDpi, NCR.Server.Quality, NCR.Server.ScanImageBackUpRoot);
                form.InitializeForm(_ctl);
                form.ResetForm();
                DialogResult result = form.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                lvImageList.BeginUpdate();
                // 基イメージ置き換え
                ReplaceImageData(FolderPath, FileName, true);
                lvImageList.EndUpdate();
                // 画面データ表示
                DispPictureBoxImage(FolderPath, FileName, _AssignDataList[_CurCursor].Picture, _AssignDataList[_CurCursor].canvasImg);
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }
        /// <summary>
        /// F8：選択解除
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (_CurCursor < 1 || string.IsNullOrEmpty((string)_AssignDataList[_CurCursor].Picture.Tag))
                {
                    ComMessageMgr.MessageWarning("イメージが選択されていません");
                    return;
                }

                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "選択解除してもよろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "選択解除", 1);

                string FolderPath = _itemMgr.TargetFolderPath();
                string fileName = (string)_AssignDataList[_CurCursor].Picture.Tag;
                // 基イメージ置き換え
                lvImageList.BeginUpdate();
                ReplaceImageData(FolderPath, fileName, false);
                lvImageList.EndUpdate();

                //画像クリア
                _AssignDataList[_CurCursor].Picture.Image = null;
                _AssignDataList[_CurCursor].Picture.Tag = string.Empty;
                _AssignDataList[_CurCursor].canvasImg.Dispose();

                //選択初期化
                DispFreame(0);
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F9：戻る
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (_itemMgr.AssignParams.CurrentDetail == 1)
                {
                    // 先頭明細の場合、戻るは無効
                    ComMessageMgr.MessageWarning("先頭明細のため戻ることはできません");
                    return;
                }

                //変更有の場合、確認メッセージ表示
                if (AssignChg() && (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "変更した内容は破棄されますよろしいですか？") == DialogResult.No))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "戻る", 1);

                // 基フォルダ更新
                lvImageList.BeginUpdate();
                RestoreImageData(_itemMgr.TargetFolderPath());
                lvImageList.EndUpdate();

                // 前明細表示
                _itemMgr.AssignParams.CurrentDetail--;
                // 画面表示データ更新
                RefreshDisplayData();
                // 画面表示状態更新
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }
        /// <summary>
        /// F10：進む
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (_itemMgr.AssignParams.CurrentDetail >= GetMeiMaxData(0))
                {
                    // 確定済最終明細以降の場合、進むは無効
                    ComMessageMgr.MessageWarning("最終明細のため進むことはできません");
                    return;
                }

                //変更有の場合、確認メッセージ表示
                if (AssignChg() && (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "変更した内容は破棄されますよろしいですか？") == DialogResult.No))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "進む", 1);

                // 基フォルダ更新
                lvImageList.BeginUpdate();
                RestoreImageData(_itemMgr.TargetFolderPath());
                lvImageList.EndUpdate();

                // 次明細表示
                _itemMgr.AssignParams.CurrentDetail++;
                // 画面表示データ更新
                RefreshDisplayData();
                // 画面表示状態更新
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
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
                if (string.IsNullOrEmpty((string)_AssignDataList[1].Picture.Tag) || 
                    string.IsNullOrEmpty((string)_AssignDataList[2].Picture.Tag))
                {
                    //表・裏は必須
                    ComMessageMgr.MessageWarning("表・裏イメージは設定する必要があります");
                    return;
                }

                if (_itemMgr.AssignParams.CurrentDetail < GetMeiMaxData(0) || 
                    _itemMgr.InputBatchData.m_TOTAL_COUNT - GetInputMeisaiI() != 0)
                {
                    //明細確定処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("明細確定 明細番号:{0}", _itemMgr.AssignParams.CurrentDetail), 1);

                    //明細データ登録
                    Dictionary<int, string> AssignList = new Dictionary<int, string>();
                    foreach (KeyValuePair<int, AssignData> Data in _AssignDataList)
                    {
                        if (!string.IsNullOrEmpty((string)Data.Value.Picture.Tag))
                        {
                            AssignList.Add(Data.Key, (string)Data.Value.Picture.Tag);
                        }
                    }
                    ScanMeiManager Maneger = new ScanMeiManager(_ctl, AssignList);
                    if (!Maneger.ScanMeiDataInput())
                    {
                        return;
                    }

                    // 次明細表示
                    //明細紐づけデータの取得
                    if (!_itemMgr.FetchScanMei())
                    {
                        ComMessageMgr.MessageWarning("明細紐づけデータの取得に失敗しました");
                        return;
                    }
                    _itemMgr.AssignParams.CurrentDetail++;
                    // 画面表示データ更新
                    RefreshDisplayData();
                    // 画面表示状態更新
                    RefreshDisplayState();
                }
                else
                {
                    //バッチ確定処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "バッチ確定", 1);

                    //確認メッセージ表示
                    if ((ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "バッチを確定しますか？") == DialogResult.No))
                    {
                        return;
                    }

                    //明細データ登録
                    Dictionary<int, string> AssignList = new Dictionary<int, string>();
                    foreach (KeyValuePair<int, AssignData> Data in _AssignDataList)
                    {
                        if (!string.IsNullOrEmpty((string)Data.Value.Picture.Tag))
                        {
                            AssignList.Add(Data.Key, (string)Data.Value.Picture.Tag);
                        }
                    }
                    ScanMeiManager Maneger = new ScanMeiManager(_ctl, AssignList);
                    if (!Maneger.ScanMeiDataInput())
                    {
                        return;
                    }

                    //明細紐づけデータの取得
                    if (!_itemMgr.FetchScanMei())
                    {
                        ComMessageMgr.MessageWarning("明細紐づけデータの取得に失敗しました");
                        return;
                    }

                    try
                    {
                        //メッセージ設定
                        Processing(CommonClass.ComMessageMgr.I00002);

                        //確定処理取得
                        if (!BatchInput())
                        {
                            return;
                        }
                    }
                    finally
                    {
                        //メッセージ初期化
                        EndProcessing(CommonClass.ComMessageMgr.I00002);
                    }

                    //画面表示終了
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message));
            }
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 確定処理
        /// </summary>
        private bool BatchInput()
        {
            // 対象データのステータス確認
            if (!_itemMgr.ChkBatchDataStatus(TBL_SCAN_BATCH_CTL.enumStatus.Processing, this))
            {
                //エラーを表示して画面に戻る。
                CommonClass.ComMessageMgr.MessageWarning("他オペレータにより状態が変更された可能性があります。最新状態を確認してください。");
                return false;
            }

            // 対象バッチフォルダの存在チェック
            string ImgFolderPath = _itemMgr.TargetFolderPath();
            if (!Directory.Exists(ImgFolderPath))
            {
                //エラーを表示して画面に戻る。
                CommonClass.ComMessageMgr.MessageWarning("イメージファイルの格納フォルダが確認できません");
                return false;
            }

            // バッチ番号の取得
            int BatchNumber = 0;
            for (int i = 1; i <= _ctl.SettingData.BatchSeqRetryCount; i++)
            {
                if (_itemMgr.GetBatchNumber(_itemMgr.AssignParams.GymDate, _itemMgr.InputBatchData.m_OC_BK_NO, out BatchNumber))
                {
                    break;
                }
            }
            if (BatchNumber <= 0)
            {
                //バッチ番号が取得できない場合エラーを表示して画面に戻る。
                CommonClass.ComMessageMgr.MessageWarning("他端末で確定処理中です。しばらくお待ちください");
                return false;
            }

            // 登録処理
            TRManager insTR = new TRManager(_ctl, BatchNumber, ImgFolderPath);
            if (!insTR.TRDataInput())
            {
                return false;
            }

            // イメージ退避処理
            if (!ImportFileAccess.ImageBackUp(ImgFolderPath, NCR.Server.ScanImageBackUpRoot))
            {
                //退避処理エラーの場合
                // DBの登録処理は完了しているため、エラーは表示するが次の処理を行う
                CommonClass.ComMessageMgr.MessageWarning("イメージ退避処理でエラーが発生しました");
            }

            //フォルダ削除処理
            ImportFileAccess.DeleteImportFolder(ImgFolderPath);

            return true;
        }

        /// <summary>
        /// 設定内容変更有無
        /// </summary>
        private bool AssignChg()
        {
            foreach(KeyValuePair<int, AssignData> Data in _AssignDataList)
            {
                if (string.IsNullOrEmpty((string)Data.Value.Picture.Tag))
                {
                    // 画面上未設定の場合
                    if (_itemMgr.scan_mei.Where(
                                    m => m.Value.m_BATCH_UCHI_RENBAN == _itemMgr.AssignParams.CurrentDetail && m.Value.m_IMG_KBN == Data.Key).Count() > 0)
                    {
                        // DB上設定があれば変更あり
                        return true;
                    }
                }
                else
                {
                    // 画面上設定ありの場合
                    if (_itemMgr.scan_mei.Where(
                                    m => m.Value.m_BATCH_UCHI_RENBAN == _itemMgr.AssignParams.CurrentDetail && m.Value.m_IMG_KBN == Data.Key).Count() == 0)
                    {
                        // DB上設定がない場合は変更あり
                        return true;
                    }

                    if ((string)Data.Value.Picture.Tag != 
                                    _itemMgr.scan_mei.First(
                                          m => m.Value.m_BATCH_UCHI_RENBAN == _itemMgr.AssignParams.CurrentDetail && m.Value.m_IMG_KBN == Data.Key).Value._IMG_NAME)
                    {
                        // 設定ファイル名が異なる場合は変更あり
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 明細番号の最大値取得
        /// </summary>

        private int GetMeiMaxData(int def)
        {
            if (_itemMgr.scan_mei.Count(m => m.Value.m_BATCH_UCHI_RENBAN > 0) == 0)
            {
                return def;
            }
            else
            {
                return _itemMgr.scan_mei.Max(m => m.Value.m_BATCH_UCHI_RENBAN);
            }
        }

        /// <summary>
        /// 明細分母の取得
        /// </summary>
        private int GetInputMeisaiI()
        {
            int meisaiTotal = GetMeiMaxData(0);
            if (meisaiTotal < _itemMgr.AssignParams.CurrentDetail) meisaiTotal++;

            return meisaiTotal;
        }

        /// <summary>
        /// 情報欄の更新
        /// </summary>

        private void SetMeisaiIInfo()
        {
            //明細位置
            int meisaiTotal = GetInputMeisaiI();
            lblmeisaiPositon.Text = string.Format("{0:#,##0}/{1:#,##0}", _itemMgr.AssignParams.CurrentDetail, meisaiTotal);
            //バッチ票合計
            lblBatchTotal.Text = string.Format("{0:#,##0}", _itemMgr.InputBatchData.m_TOTAL_COUNT);
            //明細枚数
            lblMeisaiCount.Text = string.Format("{0:#,##0}", meisaiTotal);
            //差 引枚数
            lblDiffCount.Text = string.Format("{0:#,##0}", _itemMgr.InputBatchData.m_TOTAL_COUNT - meisaiTotal);
        }


        /// <summary>
        /// 基フォルダ初期位置設定
        /// </summary>

        private void SetImageListDefPositon()
        {
            // 初期位置設定
            // 設定済のイメージ+1を先頭表示
            int i = 0;
            for (i = lvImageList.Items.Count - 1; i >= 0; i--)
            {
                ListViewItem item = lvImageList.Items[i];
                if (_imgListFileName.ContainsKey(item.Text) && _imgListFileName[item.Text])
                {
                    i = i + 1;
                    break;
                }
            }

            i += (int)Math.Floor((double)lvImageList.Height / imgListSize.Height) - 1;
            if (lvImageList.Items.Count - 1 < i)
            {
                i = lvImageList.Items.Count - 1;
            }
            lvImageList.Items[i].EnsureVisible();
        }

        /// <summary>
        /// イメージの枠出力
        /// </summary>

        private void DispFreame(int Disp)
        {
            if (_CurCursor >= 1)
            {
                // 出力されている枠を元に戻す
                _AssignDataList[_CurCursor].Panel.BackColor = SystemColors.Control;
            }
            if (Disp < 1)
            {
                _CurCursor = Disp;
                return;
            }

            // 対象に青枠を出力
            _AssignDataList[Disp].Panel.BackColor = Color.DarkBlue;
            _CurCursor = Disp;
        }

        /// <summary>
        /// スキャン明細データの整合性チェック
        /// </summary>
        private bool ChkScanMeiData()
        {
            //連番チェック
            int i = 1;
            foreach ( int renban in _itemMgr.scanmei_renban)
            {
                if  (renban != i)
                {
                    // 連番ではない場合
                    return false;
                }
                i++;
            }

            // ファイル存在チェック
            string FolderPath = _itemMgr.TargetFolderPath();
            foreach (TBL_SCAN_MEI mei in _itemMgr.scan_mei.Values)
            {
                if (!File.Exists(Path.Combine(FolderPath, mei._IMG_NAME)))
                {
                    // ファイルが存在していない場合
                    return false;
                }
            }

            return true;
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
            lvImageList.View = View.Details;
            lvImageList.Columns.Add("", _imgList.ImageSize.Width + 5);
            lvImageList.HeaderStyle = ColumnHeaderStyle.None;

            // 最初に設定するイメージ
            // バッチ票イメージの設定（表・裏）
            lvImageList.Items.Add(_itemMgr.InputParams.BatchImage.Front, _itemMgr.InputParams.BatchImage.Front);
            lvImageList.Items.Add(_itemMgr.InputParams.BatchImage.Back, _itemMgr.InputParams.BatchImage.Back);

            foreach (string imgname in _imgListFileName.Keys)
            {
                if (imgname.ToString() == _itemMgr.InputParams.BatchImage.Front ||
                    imgname.ToString() == _itemMgr.InputParams.BatchImage.Back)
                {
                    continue;
                }
                lvImageList.Items.Add(imgname, imgname);
            }
            // 初期位置設定
            SetImageListDefPositon();
            lvImageList.EndUpdate();
        }

        /// <summary>
        /// 基フォルダ画像データ一式取得
        /// </summary>

        private async void GetImageData(CancellationToken cancelToken)
        {
            // 初期処理
            lvImageList.Enabled = false;
            _DisplayingImageList = true;

            // 画像データの取得に時間が掛かるため別スレッド化
            bool RumRtn = await Task.Run(() =>
            {
                ImageCanvas canvas = null;

                // イメージ一覧取得
                string FolderPath = _itemMgr.TargetFolderPath();
                IEnumerable<string> FileList = Directory.EnumerateFiles(FolderPath, "*.jpg").Select(name => Path.GetFileName(name)).OrderBy(name => name);

                lock (_imgList)
                {
                    //初期化
                    imgListCreate();
                    _imgListFileName.Clear();

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

                            bool DragFlg = false;
                            // 画像読込
                            canvas.InitializeCanvas(Path.Combine(FolderPath, name));
                            canvas.SetDefaultReSize(_imgList.ImageSize.Width, _imgList.ImageSize.Height);

                            //背景色設定
                            Color color = Color.White;
                            if (_itemMgr.scan_mei.Where(m => m.Value._IMG_NAME == name).Count() > 0 ||
                                _itemMgr.InputParams.BatchImage.Front == name ||
                                _itemMgr.InputParams.BatchImage.Back == name)
                            {
                                // バッチイメージ　または　スキャン明細に登録済の場合背景グレー
                                color = Color.Gray;
                                DragFlg = true;
                            }

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

                    return true;
                }
            }, cancelToken);

            if (!RumRtn)
            {
                this.SetStatusMessage("基イメージフォルダ画像の取得で失敗しました");
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
        /// 基フォルダ画像データ置き換え
        /// </summary>
        private bool ReplaceImageData(string FolderPath, string fileName, bool DragFlg)
        {
            ImageCanvas canvas = null;

            try
            {
                canvas = new ImageCanvas(_editor);

                // 画像読込
                canvas.InitializeCanvas(Path.Combine(FolderPath, fileName));
                canvas.SetDefaultReSize(_imgList.ImageSize.Width, _imgList.ImageSize.Height);
                //背景色設定
                Color color = Color.White;
                if (DragFlg)
                {
                    //割り当て済の場合
                    color = Color.Gray;
                }
                // 全体表示 ・設定
                canvas.ToFitCanvasAspect(ImageCanvas.FitAlignType.Center, LISTIMAGEPADING, color);

                // イメージ情報更新
                _imgList.Images.RemoveByKey(fileName);
                _imgList.Images.Add(fileName, _editor.CloneCanvas(canvas.ResizeCanvas));
                _imgListFileName[fileName] = DragFlg;
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

            return true;
        }

        /// <summary>
        /// 割り当て済イメージ表示
        /// </summary>
        private bool DispPictureBoxImage(string FolderPath, string fileName, PictureBox Dispimg, ImageCanvas canvasimg)
        {
            try
            {
                if (canvasimg == null)
                {
                    canvasimg = new ImageCanvas(_editor);
                }
                // 画像読込
                canvasimg.InitializeCanvas(Path.Combine(FolderPath, fileName));
                canvasimg.SetDefaultReSize(Dispimg.Width, Dispimg.Height);
                // 全体表示
                canvasimg.ToFitCanvasAspect(ImageCanvas.FitAlignType.Center, ASSIGNIMAGEPADING, Color.White);
                Dispimg.Image = canvasimg.ResizeCanvas;
                Dispimg.Tag = fileName;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 現在表示の基イメージ状態初期化
        /// </summary>
        private void ClearImageData(string FolderPath)
        {
            // 現在画面上に設定されているイメージの背景グレーを初期化
            foreach (KeyValuePair<int, AssignData> Data in _AssignDataList)
            {
                if (!string.IsNullOrEmpty((string)Data.Value.Picture.Tag))
                {
                    ReplaceImageData(FolderPath, (string)Data.Value.Picture.Tag, false);
                }
            }
        }

        /// <summary>
        /// 基イメージ復元
        /// </summary>
        private void RestoreImageData(string FolderPath)
        {
            // 現在表示の基イメージ状態初期化
            ClearImageData(FolderPath);

            // DBの内容から基イメージ状態を復元
            foreach (TBL_SCAN_MEI mei in _itemMgr.scan_mei.Values.Where(m => m.m_BATCH_UCHI_RENBAN == _itemMgr.AssignParams.CurrentDetail)  )
            {
                ReplaceImageData(FolderPath, mei._IMG_NAME, true);
            }
        }

        #region 処理中設定

        /// <summary>
        /// 処理中状態に設定
        /// </summary>
        private void Processing(string msg)
        {
            // ファンクションDisable
            DisableAllFunctionState(false);

            SetWaitCursor();
            this.SetStatusMessage(msg, System.Drawing.Color.Transparent);
            this.Refresh();
        }

        /// <summary>
        /// 処理中状態を解除する
        /// </summary>
        private void EndProcessing(string msg)
        {
            // Disableにしたファンクションを元に戻す
            InitializeFunction();
            SetFunctionState();

            if (this.GetStatusMessage() == msg)
            {
                //メッセージが同じ場合クリア
                this.ClearStatusMessage();
            }
            ResetCursor();
        }

        #endregion

    }
}

