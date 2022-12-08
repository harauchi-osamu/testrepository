using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Common;
using CommonClass;
using EntryCommon;
using System.Linq;
using ImageController;
using System.Drawing.Imaging;

namespace ImageOperation
{
    /// <summary>
    /// 切り出し画面
    /// </summary>
    public partial class ImageCut : EntryCommonFormBase
    {
        private ControllerBase _ctl = null;
        private ImageEditor _editor = null;
        private ImageCanvas _canvas = null;
        private bool _imgDisp = false;
        private bool _CutFlg = false;
        private CutType _cuttype = CutType.ImageImport;
        private string _FolderPath = string.Empty;
        private string _fileName = string.Empty;
        private string _TegataSize = string.Empty;
        private string _KogitteSize = string.Empty;
        private string _CutDstDpi = string.Empty;
        private string _CutQuality = string.Empty;
        private string _ImageBackUpRoot = string.Empty;

        private ImgCut _imgCut = ImgCut.Free;
        private Size _MoveSize = new Size(0, 0);

        // *******************************************************************
        // マウス関連
        // *******************************************************************
        private bool _isMouseDown = false;

        private ImageEditor.RectangleInfo _cutRect = null;
        private Point _posMouseDown = new Point();
        private Point _posMouseUp = new Point();
        private Pen _cvsPen = null;

        public enum CutType
        {
            ImageImport = 1,
        }

        public enum ImgCut
        {
            Free = 0,
            Tegata = 1,
            Kogitte = 2,
        }

        /// <summary>
        /// 変更フラグ
        /// </summary>
        public bool ChgFlg
        {
            get
            {
                if (_canvas == null) return false;
                return _CutFlg || _canvas.ResizeInfo.RotateState != ImageCanvas.RotateState.Default;
            }
        }

        /// <summary>
        /// 赤枠設定済フラグ
        /// </summary>
        public bool SetRectangle
        {
            get
            {
                if (_canvas == null) return false;
                return !(_cutRect.Height + _cutRect.Width == 0);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private ImageCut()
        {
            InitializeComponent();

            _editor = new ImageEditor();
            _canvas = new ImageCanvas(_editor);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageCut(CutType cuttype, string FolderPath, string fileName, 
                        string tegata, string kogitte, string cutdstdpi, string quality, string imagebackuproot)
        {
            _cuttype = cuttype;
            _FolderPath = FolderPath;
            _fileName = fileName;
            _TegataSize = tegata;
            _KogitteSize = kogitte;
            _CutDstDpi = cutdstdpi;
            _CutQuality = quality;
            _ImageBackUpRoot = imagebackuproot;

            InitializeComponent();

            _editor = new ImageEditor();
            _canvas = new ImageCanvas(_editor);

            _cvsPen = new Pen(Color.Red, 4);
            _cvsPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            _cutRect = new ImageEditor.RectangleInfo( );

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("切り出し実行 ファイル名:{0}", fileName), 1);
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
            switch (_cuttype)
            {
                case CutType.ImageImport:
                    base.SetDispName1("交換持出");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("イメージ切り出し");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (IsNotPressCSAKey)
            {
                // 通常状態
                SetFunctionName(1, "終了");
                SetFunctionName(2, string.Empty);
                SetFunctionName(3, "左回転", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(4, "右回転", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(5, "手形枠", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(6, "小切手枠", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(7, "フリー枠", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(8, "切出実行", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(9, string.Empty);
                SetFunctionName(10, "初期状態\n  に戻す", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(11, string.Empty);
                SetFunctionName(12, "確定");
            }
            else
            {
                // Shiftキー押下
                SetFunctionName(1, string.Empty);
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
                SetFunctionName(12, string.Empty);
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            if (IsNotPressCSAKey)
            {
                //初期状態に戻す
                if (ChgFlg)
                {
                    SetFunctionState(10, true);
                }
                else
                {
                    SetFunctionState(10, false);
                }
            }

            // イメージ表示時のエラー発生時はF1以外Disable
            if (!_imgDisp)
            {
                DisableAllFunctionState(true);
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
            // 活性・非活性処理

            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            // 画面項目を設定する処理はまとめてここに実装してこのメソッドを呼ぶ

            // イメージ表示
            if (!LoadImage(_FolderPath, _fileName, pbCutImage))
            {
                // イメージ表示でエラーの場合、
                this.SetStatusMessage("イメージの表示でエラーが発生しました");
                return;
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
            //this.ClearStatusMessage();

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

        // *******************************************************************
        // イベント（マウス）
        // *******************************************************************

        /// <summary>
        /// [描画領域] MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCut_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (!_imgDisp) return;

                _isMouseDown = true;

                if (_imgCut == ImgCut.Free)
                {
                    // マウスドラッグ範囲の赤線描画を開始する
                    RectangleDraw_MouseDown(e);
                }
                else
                {
                    // マウスドラッグで赤枠を移動する
                    RectangleMove_MouseMove(pbCutImage, e);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                ComMessageMgr.MessageWarning("切り出し処理でエラーが発生しました");
            }
        }

        /// <summary>
        /// [描画領域] MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCut_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!_imgDisp) return;

                if (!_isMouseDown) { return; }

                if (_imgCut == ImgCut.Free)
                {
                    // マウスドラッグ範囲を赤線描画する
                    RectangleDraw_MouseMove(pbCutImage, e);
                }
                else
                {
                    // マウスドラッグで赤枠を移動する
                    RectangleMove_MouseMove(pbCutImage, e);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                ComMessageMgr.MessageWarning("切り出し処理でエラーが発生しました");
            }
        }

        /// <summary>
        /// [描画領域] MouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCut_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!_imgDisp) return;

                if (!_isMouseDown) { return; }

                _isMouseDown = false;

                if (_imgCut == ImgCut.Free)
                {
                    // マウスドラッグ範囲の赤線描画を終了する
                    RectangleDraw_MouseUp(pbCutImage, e);
                }
                else
                {
                    // マウスドラッグで赤枠を移動する
                    RectangleMove_MouseUp(e);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                ComMessageMgr.MessageWarning("切り出し処理でエラーが発生しました");
            }
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        #region ファンクション

        /// <summary>
        /// F1：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            //確認メッセージ表示
            if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "切り出し画面を終了しますが、よろしいですか？") == DialogResult.No)
            {
                return;
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(),"終了", 1);

            //画面表示終了
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// F3：左回転
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (!_imgDisp) return;

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "左回転", 1);

                ImageCanvas.RotateState rsts = GetRotateState(ImageCanvas.RotateType.TurnLeft);
                _canvas.RotateToFitCanvasAspect(rsts);
                // イメージ表示
                DispImage(pbCutImage);
                //State箇所更新
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// F4：右回転
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (!_imgDisp) return;

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "右回転", 1);

                ImageCanvas.RotateState rsts = GetRotateState(ImageCanvas.RotateType.TurnRight);
                _canvas.RotateToFitCanvasAspect(rsts);
                // イメージ表示
                DispImage(pbCutImage);
                //State箇所更新
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// F5：手形枠
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (!_imgDisp) return;
                if (SetRectangle)
                {
                    // 赤枠設定済でTypeが同じ場合は処理なし
                    if (_imgCut == ImgCut.Tegata) return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "手形枠", 1);

                // 手形設定
                List<string> WkSize = _TegataSize.Split(',').ToList();
                if (WkSize.Count == 2 && int.TryParse(WkSize[0], out int Width) && int.TryParse(WkSize[1], out int Height))
                {
                    _imgCut = ImgCut.Tegata;
                    _MoveSize.Width = Width;
                    _MoveSize.Height = Height;

                    // 画面リフレッシュ
                    _canvas.Refresh(pbCutImage, ImageCanvas.CanvasType.Current);

                    //右下に赤枠表示
                    Rectangle_RightBottom(pbCutImage);
                }
                else 
                { 
                    CommonClass.ComMessageMgr.MessageWarning("手形設定に不備があります");
                }
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// F6：小切手枠
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (!_imgDisp) return;
                if (SetRectangle)
                {
                    // 赤枠設定済でTypeが同じ場合は処理なし
                    if (_imgCut == ImgCut.Kogitte) return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "小切手枠", 1);

                // 小切手設定
                List<string> WkSize = _KogitteSize.Split(',').ToList();
                if (WkSize.Count == 2 && int.TryParse(WkSize[0], out int Width) && int.TryParse(WkSize[1], out int Height))
                {
                    _imgCut = ImgCut.Kogitte;
                    _MoveSize.Width = Width;
                    _MoveSize.Height = Height;

                    // 画面リフレッシュ
                    _canvas.Refresh(pbCutImage, ImageCanvas.CanvasType.Current);

                    //右下に赤枠表示
                    Rectangle_RightBottom(pbCutImage);
                }
                else
                {
                    CommonClass.ComMessageMgr.MessageWarning("小切手設定に不備があります");
                }
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// F7：フリー枠
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (!_imgDisp) return;
                if (SetRectangle)
                {
                    // 赤枠設定済でTypeが同じ場合は処理なし
                    if (_imgCut == ImgCut.Free) return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "フリー枠", 1);

                // フリー設定
                _imgCut = ImgCut.Free;
                _MoveSize.Width = 0;
                _MoveSize.Height = 0;

                // 画面リフレッシュ
                _canvas.Refresh(pbCutImage, ImageCanvas.CanvasType.Current);

                //切り出し枠初期化
                _cutRect = new ImageEditor.RectangleInfo();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// F8：切出実行
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (!_imgDisp) return;

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "切出実行", 1);

                // 切出処理
                _canvas.CutToOrg(_cutRect, Int32.Parse(_CutDstDpi));
                if (_canvas.CutCanvas == null)
                {
                    CommonClass.ComMessageMgr.MessageInformation("切り出し枠が設定されていません");
                    return;
                }

                using (Bitmap wk = _editor.CloneCanvas(_canvas.CutCanvas))
                {
                    _canvas.Dispose();
                    // 切出画像で新しいcanvasクラスを作成
                    _canvas.InitializeCanvasToCopy(wk);
                    // 全体表示
                    _canvas.ToFitCanvasAspect(ImageCanvas.FitAlignType.None, 0, Color.Transparent);
                    // 回転状態更新
                    _canvas.ResizeInfo.RotateState = ImageCanvas.RotateState.Default;
                }
                //切り出し枠初期化
                _cutRect = new ImageEditor.RectangleInfo();
                //切り出しフラグ設定
                _CutFlg = true;
                // イメージ表示
                DispImage(pbCutImage);

                //State箇所更新
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// F10：初期状態に戻す
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();

            try
            {
                if (!_imgDisp) return;
                if (!ChgFlg) return;

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "初期状態に戻す", 1);

                //画面初期化
                ResetForm();
            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
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
                if (!_imgDisp) return;

                if (!ChgFlg)
                {
                    CommonClass.ComMessageMgr.MessageInformation("イメージの変更が行われていません");
                    return;
                }

                //確認メッセージ表示
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "このイメージで確定してよろしいですか？") == DialogResult.No)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);

                // ファイル退避処理
                if (!ImportFileAccess.FileBackUp(_FolderPath, _fileName, _ImageBackUpRoot, true))
                {
                    //退避処理エラーの場合
                    CommonClass.ComMessageMgr.MessageWarning("イメージ退避処理でエラーが発生しました");
                    return;
                }

                // ファイル保存
                _canvas.CutToOrg(null, Int32.Parse(_CutDstDpi));
                FileSave(_canvas.CutCanvas);

                //画面表示終了
                this.DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        #endregion

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 表示イメージ取得
        /// </summary>
        private bool LoadImage(string FolderPath, string fileName, PictureBox DispPicture)
        {
            try
            {
                // 初期化
                _CutFlg = false;
                _imgDisp = false;
                _cutRect = new ImageEditor.RectangleInfo();
                _canvas.Dispose();
                _editor = new ImageEditor();
                _canvas = new ImageCanvas(_editor);

                // 画像読込
                _canvas.InitializeCanvasToClone(Path.Combine(FolderPath, fileName));
                _canvas.SetDefaultReSize(DispPicture.Parent.Width, DispPicture.Parent.Height);
                // 全体表示
                _canvas.ToFitCanvasAspect(ImageCanvas.FitAlignType.None, 0, Color.Transparent);
                // 画像表示
                if (!DispImage(DispPicture))
                {
                    return false;
                }

                //表示成功
                _imgDisp = true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// イメージ表示
        /// </summary>
        private bool DispImage(PictureBox DispPicture)
        {
            try
            {
                //イメージ表示
                _canvas.Refresh(DispPicture, ImageCanvas.CanvasType.Current);

                // DispPictureの中央表示
                int x = (DispPicture.Parent.Width - DispPicture.Width) / 2;
                int y = (DispPicture.Parent.Height - DispPicture.Height) / 2;
                DispPicture.Location = new Point(x, y);

                if (SetRectangle)
                {
                    //赤枠が設定済の場合、赤枠初期表示
                    switch (_imgCut)
                    {
                        case ImgCut.Kogitte:
                        case ImgCut.Tegata:
                            //右下に赤枠表示
                            Rectangle_RightBottom(DispPicture);
                            break;
                        case ImgCut.Free:
                            //切り出し枠初期化
                            _cutRect = new ImageEditor.RectangleInfo();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 切出画像の保存処理
        /// </summary>
        private void FileSave(Bitmap Canvas)
        {
            // JPEG用のエンコーダの取得
            ImageCodecInfo jpgEncoder = null;
            foreach (ImageCodecInfo ici
                  in ImageCodecInfo.GetImageEncoders())
            {
                if (ici.FormatID == ImageFormat.Jpeg.Guid)
                {
                    jpgEncoder = ici;
                    break;
                }
            }

            // Quality設定
            System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter QualityEncoderParameter = new EncoderParameter(QualityEncoder, Int32.Parse(_CutQuality));

            // EncoderParameters設定
            EncoderParameters EncoderParameters = new EncoderParameters(1);
            EncoderParameters.Param[0] = QualityEncoderParameter;

            // 保存処理
            Canvas.Save(Path.Combine(_FolderPath, _fileName), jpgEncoder, EncoderParameters);
        }

        // *******************************************************************
        // 内部メソッド（赤枠移動）
        // *******************************************************************

        /// <summary>
        /// 赤枠移動：MouseMove
        /// </summary>
        private void RectangleMove_MouseMove(PictureBox picture, MouseEventArgs e)
        {
            // マウスドラッグで赤枠を移動する
            Point start = new Point();
            Point end = new Point();

            // 中心座標から枠の四隅を算出（表示レートも考慮）
            int rectHalfWidth = ((int)(_MoveSize.Width * _canvas.ResizeInfo.DSP_REDUCE_RATE) / 2);
            int rectHalfHeight = ((int)(_MoveSize.Height * _canvas.ResizeInfo.DSP_REDUCE_RATE) / 2);
            start.Y = e.Y - rectHalfHeight;
            start.X = e.X - rectHalfWidth;
            end.Y = e.Y + rectHalfHeight;
            end.X = e.X + rectHalfWidth;

            // 赤枠描画
            _canvas.DrawRectangleOut(ImageCanvas.CanvasType.Resize, ImageCanvas.CanvasType.Edit, start, end, _cvsPen);

            picture.Image = _canvas.EditCanvas;
            picture.Refresh();

            // 選択中の座標表示
            SetRectanblePoint(start, end);
        }

        /// <summary>
        /// 赤枠移動：MouseUp
        /// </summary>
        private void RectangleMove_MouseUp(MouseEventArgs e)
        {
            // マウスドラッグで赤枠を移動終了する
            Point start = new Point();
            Point end = new Point();

            // 中心座標から枠の四隅を算出（表示レートも考慮）
            int rectHalfWidth = ((int)(_MoveSize.Width * _canvas.ResizeInfo.DSP_REDUCE_RATE) / 2);
            int rectHalfHeight = ((int)(_MoveSize.Height * _canvas.ResizeInfo.DSP_REDUCE_RATE) / 2);
            start.Y = e.Y - rectHalfHeight;
            start.X = e.X - rectHalfWidth;
            end.Y = e.Y + rectHalfHeight;
            end.X = e.X + rectHalfWidth;

            // 選択中の座標表示
            SetRectanblePoint(start, end);
        }

        /// <summary>
        /// 赤枠表示：右下初期表示
        /// </summary>
        private void Rectangle_RightBottom(PictureBox picture)
        {
            Point start = new Point();
            Point end = new Point();

            // pictureの右下座標から枠の四隅を算出（表示レートも考慮）
            int rectWidth = ((int)(_MoveSize.Width * _canvas.ResizeInfo.DSP_REDUCE_RATE));
            int rectHeight = ((int)(_MoveSize.Height * _canvas.ResizeInfo.DSP_REDUCE_RATE));
            start.Y = picture.Height - rectHeight;
            start.X = picture.Width - rectWidth;
            end.Y = picture.Height;
            end.X = picture.Width;

            // 赤枠描画
            _canvas.DrawRectangleOut(ImageCanvas.CanvasType.Resize, ImageCanvas.CanvasType.Edit, start, end, _cvsPen);
            picture.Image = _canvas.EditCanvas;
            picture.Refresh();

            // 選択中の座標表示
            SetRectanblePoint(start, end);
        }

        // *******************************************************************
        // 内部メソッド（赤枠描画）
        // *******************************************************************

        /// <summary>
        /// 切出座標を設定する
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void SetRectanblePoint(Point start, Point end)
        {
            // 表示レートを考慮して設定
            _cutRect.X1 = (int)(start.X / _canvas.ResizeInfo.DSP_REDUCE_RATE);
            _cutRect.Y1 = (int)(start.Y / _canvas.ResizeInfo.DSP_REDUCE_RATE);
            if (_imgCut == ImgCut.Free)
            {
                _cutRect.X2 = (int)(end.X / _canvas.ResizeInfo.DSP_REDUCE_RATE);
                _cutRect.Y2 = (int)(end.Y / _canvas.ResizeInfo.DSP_REDUCE_RATE);
                _cutRect.Height = ImageEditor.GetLength(_cutRect.Y1, _cutRect.Y2);
                _cutRect.Width = ImageEditor.GetLength(_cutRect.X1, _cutRect.X2);
            }
            else
            {
                _cutRect.Height = _MoveSize.Height;
                _cutRect.Width = _MoveSize.Width;
                _cutRect.Y2 = _cutRect.Y1 + _MoveSize.Height;
                _cutRect.X2 = _cutRect.X1 + _MoveSize.Width;
            }
        }

        /// <summary>
        /// 赤枠描画：MouseDown
        /// </summary>
        private void RectangleDraw_MouseDown(MouseEventArgs e)
        {
            // マウスドラッグ範囲の赤線描画を開始する
            // Mouseを押した座標を記録
            _posMouseDown.X = e.X;
            _posMouseDown.Y = e.Y;
        }

        /// <summary>
        /// 赤枠描画：MouseMove
        /// </summary>
        private void RectangleDraw_MouseMove(PictureBox picture, MouseEventArgs e)
        {
            // マウスドラッグ範囲を赤線描画する
            Point p = new Point();
            Point start = new Point();
            Point end = new Point();

            // カーソルが示している場所の座標を取得
            p.X = e.X;
            p.Y = e.Y;

            // 座標から(X,Y)座標を計算
            _editor.GetRectanblePoint(_posMouseDown, p, ref start, ref end, false);

            // 赤枠描画
            _canvas.DrawRectangleOut(ImageCanvas.CanvasType.Current, ImageCanvas.CanvasType.Edit, start, end, _cvsPen);

            picture.Image = _canvas.EditCanvas;
            picture.Refresh();

            // 選択中の座標表示
            SetRectanblePoint(start, end);
        }

        /// <summary>
        /// 赤枠描画：MouseUp
        /// </summary>
        private void RectangleDraw_MouseUp(PictureBox picture, MouseEventArgs e)
        {
            // マウスドラッグ範囲の赤線描画を終了する
            Point start = new Point();
            Point end = new Point();

            // Mouseを離した座標を記録
            _posMouseUp.X = e.X;
            _posMouseUp.Y = e.Y;

            // 座標から(X,Y)座標を計算
            _editor.GetRectanblePoint(_posMouseDown, _posMouseUp, ref start, ref end, false);

            // 赤枠描画
            _canvas.DrawRectangleOut(ImageCanvas.CanvasType.Current, ImageCanvas.CanvasType.Edit, start, end, _cvsPen);

            picture.Image = _canvas.EditCanvas;
            picture.Refresh();

            // 選択中の座標表示
            SetRectanblePoint(start, end);
        }

        /// <summary>
        /// 回転状態を取得する
        /// </summary>
        /// <param name="rtype"></param>
        /// <returns></returns>
        private ImageCanvas.RotateState GetRotateState(ImageCanvas.RotateType rtype)
        {
            ImageCanvas.RotateState rst = ImageCanvas.RotateState.Default;
            switch (_canvas.ResizeInfo.RotateState)
            {
                case ImageCanvas.RotateState.Default:
                    if (rtype == ImageCanvas.RotateType.TurnRight) { rst = ImageCanvas.RotateState.Right; }
                    else if (rtype == ImageCanvas.RotateType.TurnLeft) { rst = ImageCanvas.RotateState.Left; }
                    break;
                case ImageCanvas.RotateState.Right:
                    if (rtype == ImageCanvas.RotateType.TurnRight) { rst = ImageCanvas.RotateState.UpDown; }
                    else if (rtype == ImageCanvas.RotateType.TurnLeft) { rst = ImageCanvas.RotateState.Default; }
                    break;
                case ImageCanvas.RotateState.UpDown:
                    if (rtype == ImageCanvas.RotateType.TurnRight) { rst = ImageCanvas.RotateState.Left; }
                    else if (rtype == ImageCanvas.RotateType.TurnLeft) { rst = ImageCanvas.RotateState.Right; }
                    break;
                case ImageCanvas.RotateState.Left:
                    if (rtype == ImageCanvas.RotateType.TurnRight) { rst = ImageCanvas.RotateState.Default; }
                    else if (rtype == ImageCanvas.RotateType.TurnLeft) { rst = ImageCanvas.RotateState.UpDown; }
                    break;
            }
            return rst;
        }
    }
}
