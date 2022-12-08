using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageController
{
    public class ImageCanvas : IDisposable
    {
        /// <summary>最大率</summary>
        public const float ZOOM_MAX_RATE = 3.0F;
        /// <summary>最小率</summary>
        public const float ZOOM_MIN_RATE = 0.2F;
        /// <summary>最大縮小率</summary>
        public const float ZOOM_PER = 0.25F;

        /// <summary>描画領域：デフォルト幅</summary>
        private int _defaultResizeWidth = 0;
        /// <summary>描画領域：デフォルト高さ</summary>
        private int _defaultResizeHeight = 0;
        /// <summary>描画領域：最大幅</summary>
        private int _maxWidth = 0;
        /// <summary>描画領域：最小幅</summary>
        private int _minWidth = 0;
        /// <summary>ToFitCanvasAspect パラメータ</summary>
        private FitAlignType _AspectfitAlign =  FitAlignType.None;
        /// <summary>ToFitCanvasAspect パラメータ</summary>
        private double _AspectPading = 0;
        /// <summary>ToFitCanvasAspect パラメータ</summary>
        private Color _AspectBackColor = Color.Transparent;
        /// <summary>全体表示：初期サイズ幅</summary>
        private int _defaultFitWidth = 0;
        /// <summary>全体表示：初期サイズ高さ</summary>
        private int _defaultFitHeight = 0;

        /// <summary>描画領域：フィット基準方向</summary>
        public DirectionType FitDirection { get; private set; } = DirectionType.Horizontal;

        private Bitmap _orgCanvas = null;
        private Bitmap _resizeCanvas = null;
        private Bitmap _colorCanvas = null;
        private Bitmap _grayCanvas = null;
        private Bitmap _binCanvas = null;
        private Bitmap _saveCanvas = null;
        private Bitmap _editCanvas = null;
        private Bitmap _cutCanvas = null;
        private Graphics _gpEdit = null;
        private ImageEditor _editor = null;

        /// <summary>現在の拡大回数</summary>
        public int ZoomLevel { get; set; } = 0;

        /// <summary>最大拡大数：４回まで拡大、５回目で初期サイズ</summary>
        public const int ZOOM_CNT_MAX1 = 5;
        /// <summary>最大拡大数：４回まで拡大、５回目で初期サイズ</summary>
        public const int ZOOM_CNT_MAX2 = ZOOM_CNT_MAX1;

        public ColorType CurColType { get; private set; } = ColorType.Color;

        public SizeInfo ResizeInfo { get; set; } = null;

        /// <summary>描画オブジェクト：原本</summary>
        public Bitmap OrgCanvas { get { return _orgCanvas; } }
        /// <summary>描画オブジェクト：原本＋サイズ調整（画面表示せず保持のみ）</summary>
        public Bitmap ResizeCanvas { get { return _resizeCanvas; } }
        /// <summary>描画オブジェクト：サイズ調整＋カラー（画面表示せず保持のみ）</summary>
        public Bitmap ColorCanvas { get { return _colorCanvas; } }
        /// <summary>描画オブジェクト：サイズ調整＋グレー（画面表示せず保持のみ）</summary>
        public Bitmap GrayCanvas { get { return _grayCanvas; } }
        /// <summary>描画オブジェクト：サイズ調整＋白黒（画面表示せず保持のみ）</summary>
        public Bitmap BinCanvas { get { return _binCanvas; } }
        /// <summary>描画オブジェクト：サイズ調整＋ファイル保存用</summary>
        public Bitmap SaveCanvas { get { return _saveCanvas; } }
        /// <summary>描画オブジェクト：切出イメージ</summary>
        public Bitmap CutCanvas { get { return _cutCanvas; } }
        /// <summary>描画オブジェクト：サイズ調整＋編集用（カラー、グレー、白黒をクローンして、赤枠描画、画面表示として使う）</summary>
        public Bitmap EditCanvas { get { return _editCanvas; } }

        /// <summary>描画オブジェクト有効状態：カラー</summary>
        public bool EnableCanvasColor { get; set; } = true;
        /// <summary>描画オブジェクト有効状態：グレー</summary>
        public bool EnableCanvasGray { get; set; } = false;
        /// <summary>描画オブジェクト有効状態：白黒</summary>
        public bool EnableCanvasBin { get; set; } = false;
        /// <summary>描画オブジェクト有効状態：保存</summary>
        public bool EnableCanvasSave { get; set; } = false;
        /// <summary>描画オブジェクト有効状態：切出</summary>
        public bool EnableCanvasCut { get; set; } = false;

        /// <summary>イメージ情報：サイズ調整</summary>
        public ImageEditor.ImageInfo ResizeImageInfo { get { return _editor.GetImageInfo(_resizeCanvas); } }
        /// <summary>イメージ情報：ファイル保存用</summary>
        public ImageEditor.ImageInfo SaveImageInfo { get { return _editor.GetImageInfo(_saveCanvas); } }

        /// <summary>描画オブジェクト：表示中の色のキャンバスを保持（カラー、グレー、白黒のいずれか）画面表示せず実体でもない。EditCanvasにクローンするだけ）</summary>
        public Bitmap CurCanvas { get; private set; }
        public ColorType CurColorType { get; private set; }
        /// <summary>全体表示かどうか</summary>
        public bool IsFit { get; set; } = false;

        public enum ColorType
        {
            Color = 1,
            Gray,
            Binary
        }

        public enum CanvasType
        {
            Original = 1,
            Resize,
            Color,
            Gray,
            Binary,
            Save,
            Edit,
            Cut,
            Current,
        }

        public enum DirectionType
        {
            Horizontal,
            Vertical
        }

        public enum FitAlignType
        {
            None,
            Center
        }

        public enum ZoomState
        {
            None,
            ZoomIn,
            ZoomOut,
            ZoomOver,
            Initilize,
        }

        public enum RotateType
        {
            Default,
            TurnRight,
            TurnLeft,
        }

        public enum RotateState
        {
            Default,
            Right,
            Left,
            UpDown,
        }

        public enum MoveType
        {
            Up,
            Down,
            Left,
            Right,
        }

        public class SizeInfo
        {
            /// <summary>PictureBox幅</summary>
            public int PicBoxWidth { get; set; } = 0;
            /// <summary>PictureBox高さ</summary>
            public int PicBoxHeight { get; set; } = 0;
            /// <summary>画像ファイル縮小率（IMG_FILE.REDUCE_RATE）</summary>
            public float IMG_REDUCE_RATE { get; set; } = 1.0F;
            /// <summary>画面表示倍率（拡大縮小・全体表示）</summary>
            public float DSP_REDUCE_RATE { get; set; } = 1.0F;
            /// <summary>回転状態</summary>
            public ImageCanvas.RotateState RotateState { get; set; } = ImageCanvas.RotateState.Default;
            /// <summary>イメージ描画開始座標</summary>
            public Point Start { get; set; } = new Point(0, 0);

            public bool IsFront { get { return ((RotateState == ImageCanvas.RotateState.Default) || (RotateState == ImageCanvas.RotateState.UpDown)); } }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageCanvas(ImageEditor editor, float baseReduceRate = 100.0F, ColorType colType = ColorType.Color)
        {
            _editor = editor;            
            CurColType = colType;
            ResizeInfo = new SizeInfo();
            ResizeInfo.IMG_REDUCE_RATE = baseReduceRate / 100.0F;
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~ImageCanvas()
        {
            Dispose();
        }


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 描画オブジェクトを初期化する
        /// </summary>
        /// <param name="filePath"></param>
        public void InitializeCanvas(string filePath)
        {
            Dispose();
            _orgCanvas = ImageEditor.CreateBitmap(filePath);
            _orgCanvas.Tag = "org";

            // 描画オブジェクト複製
            CloneFromOrgCanvas();

            // 描画オブジェクト切替
            SetCurCanvas(CurColType);
            ResizeInfo.PicBoxWidth = CurCanvas.Width;
            ResizeInfo.PicBoxHeight = CurCanvas.Height;
            ResizeInfo.DSP_REDUCE_RATE = 1.0F;
        }

        /// <summary>
        /// 描画オブジェクトを初期化する
        /// FileStream形式ではなくClone形式で読み込み
        /// </summary>
        /// <param name="filePath"></param>
        public void InitializeCanvasToClone(string filePath)
        {
            Dispose();
            _orgCanvas = ImageEditor.CreateBitmapToClone(filePath);
            _orgCanvas.Tag = "org";

            // 描画オブジェクト複製
            CloneFromOrgCanvas();

            // 描画オブジェクト切替
            SetCurCanvas(CurColType);
            ResizeInfo.PicBoxWidth = CurCanvas.Width;
            ResizeInfo.PicBoxHeight = CurCanvas.Height;
            ResizeInfo.DSP_REDUCE_RATE = 1.0F;
        }

        /// <summary>
        /// 描画オブジェクトを初期化する
        /// 引数のイメージから作成
        /// </summary>
        public void InitializeCanvasToCopy(Bitmap image)
        {
            Dispose();
            _orgCanvas = _editor.CloneCanvas(image);
            _orgCanvas.Tag = "org";

            // 描画オブジェクト複製
            CloneFromOrgCanvas();

            // 描画オブジェクト切替
            SetCurCanvas(CurColType);
            ResizeInfo.PicBoxWidth = CurCanvas.Width;
            ResizeInfo.PicBoxHeight = CurCanvas.Height;
            ResizeInfo.DSP_REDUCE_RATE = 1.0F;
        }

        /// <summary>
        /// 描画領域の初期サイズを設定する
        /// </summary>
        /// <param name="defaultResizeWidth"></param>
        /// <param name="defaultResizeHeight"></param>
        public void SetDefaultReSize(int defaultResizeWidth, int defaultResizeHeight)
        {
            _defaultResizeWidth = defaultResizeWidth;
            _defaultResizeHeight = defaultResizeHeight;

            _maxWidth = (int)Math.Round(_defaultResizeWidth * ZOOM_MAX_RATE);
            _minWidth = (int)Math.Round(_defaultResizeWidth * ZOOM_MIN_RATE);
        }

        /// <summary>
        /// 描画領域の初期サイズを設定する（全体表示）
        /// </summary>
        /// <param name="defaultFitWidth"></param>
        /// <param name="defaultFitHeight"></param>
        public void SetDefaultFitSize(int defaultFitWidth, int defaultFitHeight)
        {
            _defaultFitWidth = defaultFitWidth;
            _defaultFitHeight = defaultFitHeight;
        }

        /// <summary>
        /// イメージを全体表示する
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="defaultWidth"></param>
        /// <param name="defaultHeight"></param>
        public void ToFitCanvas(DirectionType direction, int panelWidth, int panelHeight)
        {
            FitDirection = direction;
            int defaultSize = (direction == DirectionType.Horizontal) ? panelWidth : panelHeight;

            // 画像変換情報
            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = false;
            imgInfo.HResolution = _orgCanvas.HorizontalResolution;
            imgInfo.VResolution = _orgCanvas.VerticalResolution;

            // 全体表示
            ImageEditor.DisposeBitmap(ref _resizeCanvas);
            _resizeCanvas = _editor.FitCanvas(ColorCanvas, imgInfo, direction, defaultSize);

            // 描画オブジェクト複製
            CloneFromResizeCanvas();

            // 描画オブジェクト切替
            SetCurCanvas(CurColType);
            if (direction == DirectionType.Horizontal)
            {
                // 横幅に合わせる
                ResizeInfo.PicBoxWidth = panelWidth;
                ResizeInfo.PicBoxHeight = Math.Max(panelHeight, _resizeCanvas.Height);
            }
            else
            {
                // 高さに合わせる
                ResizeInfo.PicBoxWidth = Math.Max(panelWidth, _resizeCanvas.Width);
                ResizeInfo.PicBoxHeight = panelHeight;
            }

            // 縮小率
            if (ResizeInfo.IsFront)
            {
                // 縦表示
                if (direction == DirectionType.Horizontal)
                {
                    ResizeInfo.DSP_REDUCE_RATE = ((float)_resizeCanvas.Height / (float)_defaultResizeHeight);
                }
                else
                {
                    ResizeInfo.DSP_REDUCE_RATE = ((float)_resizeCanvas.Width / (float)_defaultResizeWidth);
                }
            }
            else
            {
                // 横表示
                if (direction == DirectionType.Horizontal)
                {
                    ResizeInfo.DSP_REDUCE_RATE = ((float)_resizeCanvas.Height / (float)_defaultResizeWidth);
                }
                else
                {
                    ResizeInfo.DSP_REDUCE_RATE = ((float)_resizeCanvas.Width / (float)_defaultResizeHeight);
                }
            }

            // サイズ情報適用
            ApplyResizeCanvas();
        }

        /// <summary>
        /// イメージを全体表示する
        /// アスペクト比で全体表示
        /// </summary>
        public void ToFitCanvasAspect(FitAlignType fitAlign, double pading, Color backcolor)
        {
            _AspectfitAlign = fitAlign;
            _AspectPading = pading;
            _AspectBackColor = backcolor;

            ToFitAspect(ColorCanvas);
        }

        /// <summary>
        /// アスペクト比で全体表示
        /// </summary>
        private void ToFitAspect(Bitmap Canvas)
        {
            // 画像変換情報
            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = false;
            imgInfo.HResolution = _orgCanvas.HorizontalResolution;
            imgInfo.VResolution = _orgCanvas.VerticalResolution;

            // 全体表示
            ImageEditor.DisposeBitmap(ref _resizeCanvas);
            _resizeCanvas = _editor.FitCanvasAspect(Canvas, imgInfo, _defaultResizeWidth, _defaultResizeHeight, _AspectfitAlign, _AspectPading, _AspectBackColor, out float ratio);

            // 描画オブジェクト複製
            CloneFromResizeCanvas();

            // 描画オブジェクト切替
            SetCurCanvas(CurColType);

            if (_AspectfitAlign == FitAlignType.Center)
            {
                ResizeInfo.PicBoxWidth = _defaultResizeWidth;
                ResizeInfo.PicBoxHeight = _defaultResizeHeight;
            }
            else
            {
                ResizeInfo.PicBoxWidth = _resizeCanvas.Width;
                ResizeInfo.PicBoxHeight = _resizeCanvas.Height;
            }
            // 縮小率
            if (ResizeInfo.IsFront)
            {
                ResizeInfo.DSP_REDUCE_RATE = ratio;
            }
            else
            {
                ResizeInfo.DSP_REDUCE_RATE = ratio;
            }

            // サイズ情報適用
            ApplyResizeCanvas();
        }

        /// <summary>
        /// 拡大縮小率を更新する
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="isFront"></param>
        public void UpdateReduceRate(DirectionType direction, bool isFront = false)
        {
            // 縮小率
            if (ResizeInfo.IsFront || isFront)
            {
                // 縦表示
                if (direction == DirectionType.Horizontal)
                {
                    ResizeInfo.DSP_REDUCE_RATE = ((float)_resizeCanvas.Height / (float)_defaultResizeHeight);
                }
                else
                {
                    ResizeInfo.DSP_REDUCE_RATE = ((float)_resizeCanvas.Width / (float)_defaultResizeWidth);
                }
            }
            else
            {
                // 横表示
                if (direction == DirectionType.Horizontal)
                {
                    ResizeInfo.DSP_REDUCE_RATE = ((float)_resizeCanvas.Height / (float)_defaultResizeWidth);
                }
                else
                {
                    ResizeInfo.DSP_REDUCE_RATE = ((float)_resizeCanvas.Width / (float)_defaultResizeHeight);
                }
            }
        }

        /// <summary>
        /// 縮小率を指定してサイズ変更する
        /// </summary>
        /// <param name="isZoomIn"></param>
        /// <param name="zoomRetio"></param>
        public void Resize(float reduceRate, bool isChange = true)
        {
            // サイズ算出
            ResizeInfo.DSP_REDUCE_RATE = reduceRate;

            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = false;
            imgInfo.Width = GetDisplaySize(_orgCanvas.Width);
            imgInfo.Height = GetDisplaySize(_orgCanvas.Height);
            imgInfo.HResolution = _orgCanvas.HorizontalResolution;
            imgInfo.VResolution = _orgCanvas.VerticalResolution;
            imgInfo.RotateState = ResizeInfo.RotateState;
            imgInfo.Start = ResizeInfo.Start;

            // 画像情報適用
            ApplyImageInfo(imgInfo, isChange);
        }

        /// <summary>
        /// 拡大・縮小を算出する
        /// </summary>
        /// <param name="isZoomIn"></param>
        /// <returns></returns>
        public ZoomState SetZoomLevel(bool isZoomIn)
        {
            int nextLevel = ZoomLevel;
            nextLevel += isZoomIn ? 1 : -1;
            float nextRate = 1 + (nextLevel * ZOOM_PER);
            int nextWidth = (int)Math.Round(_orgCanvas.Width * ResizeInfo.IMG_REDUCE_RATE * nextRate);

            // 拡大縮小率
            if (isZoomIn)
            {
                if (_maxWidth < nextWidth)
                {
                    // 何もしない
                    return ZoomState.None;
                }
                else
                {
                    // 拡大
                    ZoomLevel = nextLevel;
                    return ZoomState.ZoomIn;
                }
            }
            else
            {
                if (nextWidth < _minWidth)
                {
                    // 何もしない
                    return ZoomState.None;
                }
                else
                {
                    // 縮小
                    ZoomLevel = nextLevel;
                    return ZoomState.ZoomOut;
                }
            }
        }

        /// <summary>
        /// イメージをサイズ変更する
        /// </summary>
        /// <param name="isZoomIn"></param>
        /// <param name="zoomRetio"></param>
        public void Zoom(bool isZoomIn, float zoomRetio)
        {
            ResizeInfo.DSP_REDUCE_RATE = 1 + (zoomRetio * ZoomLevel);

            // 画像変換情報
            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = false;
            imgInfo.HResolution = _orgCanvas.HorizontalResolution;
            imgInfo.VResolution = _orgCanvas.VerticalResolution;

            // サイズ算出（長さ）
            if (IsFit)
            {
                imgInfo.Width = GetDisplaySize((int)Math.Round(_defaultFitWidth / ResizeInfo.IMG_REDUCE_RATE));
                imgInfo.Height = GetDisplaySize((int)Math.Round(_defaultFitHeight / ResizeInfo.IMG_REDUCE_RATE));
            }
            else
            {
                imgInfo.Width = GetDisplaySize(_orgCanvas.Width);
                imgInfo.Height = GetDisplaySize(_orgCanvas.Height);
            }
            imgInfo.RotateState = ResizeInfo.RotateState;
            imgInfo.Width = (imgInfo.Width < 1) ? 1 : imgInfo.Width;
            imgInfo.Height = (imgInfo.Height < 1) ? 1 : imgInfo.Height;

            // サイズ算出（座標）
            imgInfo.Start = ResizeInfo.Start;
            int x = GetDisplaySize(ResizeInfo.Start.X);
            int y = GetDisplaySize(ResizeInfo.Start.Y);
            ResizeInfo.Start = new Point(x, y);

            // 画像情報適用
            ApplyImageInfo(imgInfo);
        }

        /// <summary>
        /// イメージを回転する
        /// </summary>
        /// <param name="cvs"></param>
        public void RotateOriginal(int rotate, bool isForceUpdate = false)
        {
            if ((rotate == 0) && !isForceUpdate) { return; }

            ImageCanvas.RotateState rst = ImageCanvas.RotateState.Default;
            switch (rotate)
            {
                case 0:
                    rst = ImageCanvas.RotateState.Default;
                    break;
                case 1:
                    rst = ImageCanvas.RotateState.Right;
                    break;
                case 2:
                    rst = ImageCanvas.RotateState.UpDown;
                    break;
                case 3:
                    rst = ImageCanvas.RotateState.Left;
                    break;
            }

            Rotate(rst);

            // 画像情報適用
            ResizeImageInfo.RotateState = ResizeInfo.RotateState;
        }

        /// <summary>
        /// イメージを回転する
        /// </summary>
        /// <param name="ctype"></param>
        /// <param name="rtype"></param>
        public void Rotate(ImageCanvas.RotateState rst)
        {
            if (ResizeInfo.RotateState == RotateState.Default)
            {
                // 回転前：正面
                switch (rst)
                {
                    case RotateState.Default:
                        // 回転後：正面
                        // 変更なし
                        return;
                    case RotateState.Right:
                        // 回転後：右向き
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case RotateState.UpDown:
                        // 回転後：上下逆
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case RotateState.Left:
                        // 回転後：左向き
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
            }
            else if (ResizeInfo.RotateState == RotateState.Right)
            {
                // 回転前：右向き
                switch (rst)
                {
                    case RotateState.Default:
                        // 回転後：正面
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case RotateState.Right:
                        // 回転後：右向き
                        // 何もしない
                        return;
                    case RotateState.UpDown:
                        // 回転後：上下逆
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case RotateState.Left:
                        // 回転後：左向き
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                }
            }
            else if (ResizeInfo.RotateState == RotateState.UpDown)
            {
                // 回転前：上下逆
                switch (rst)
                {
                    case RotateState.Default:
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        // 回転後：正面
                        break;
                    case RotateState.Right:
                        // 回転後：右向き
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case RotateState.UpDown:
                        // 回転後：上下逆
                        // 何もしない
                        return;
                    case RotateState.Left:
                        // 回転後：左向き
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                }
            }
            else if (ResizeInfo.RotateState == RotateState.Left)
            {
                // 回転前：左向き
                switch (rst)
                {
                    case RotateState.Default:
                        // 回転後：正面
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case RotateState.Right:
                        // 回転後：右向き
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case RotateState.UpDown:
                        // 回転後：上下逆
                        _resizeCanvas.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case RotateState.Left:
                        // 回転後：左向き
                        // 何もしない
                        return;
                }
            }

            ResizeInfo.PicBoxWidth = _resizeCanvas.Width;
            ResizeInfo.PicBoxHeight = _resizeCanvas.Height;
            ResizeInfo.RotateState = rst;

            // サイズ情報適用
            ApplyResizeCanvas();
        }

        /// <summary>
        /// イメージを回転する
        /// アスペクト比で全体表示
        /// </summary>
        /// <param name="ctype"></param>
        /// <param name="rtype"></param>
        public void RotateToFitCanvasAspect(ImageCanvas.RotateState rst)
        {
            // オリジナルのクローンに対して回転してからFitAspect処理を実施
            using (Bitmap WkCanvas = _editor.CloneCanvas(OrgCanvas))
            {
                RotateBitmap(rst, WkCanvas);
                ToFitAspect(WkCanvas);
            }

            // 回転情報設定
            ResizeInfo.RotateState = rst;
        }

        /// <summary>
        /// イメージを移動する
        /// </summary>
        public void Move(MoveType mtype)
        {
            // 画像変換情報
            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = (CurColType == ColorType.Binary);
            imgInfo.HResolution = _resizeCanvas.HorizontalResolution;
            imgInfo.VResolution = _resizeCanvas.VerticalResolution;
            imgInfo.Width = _resizeCanvas.Width;
            imgInfo.Height = _resizeCanvas.Height;
            imgInfo.RotateState = ResizeInfo.RotateState;
            imgInfo.Start = ResizeInfo.Start;

            // 移動座標算出
            const int MOVE_POINT = 20;
            switch (mtype)
            {
                case MoveType.Up:
                    ResizeInfo.Start = new Point(ResizeInfo.Start.X, (ResizeInfo.Start.Y - MOVE_POINT));
                    break;
                case MoveType.Down:
                    ResizeInfo.Start = new Point(ResizeInfo.Start.X, (ResizeInfo.Start.Y + MOVE_POINT));
                    break;
                case MoveType.Left:
                    ResizeInfo.Start = new Point(ResizeInfo.Start.X - MOVE_POINT, (ResizeInfo.Start.Y));
                    break;
                case MoveType.Right:
                    ResizeInfo.Start = new Point(ResizeInfo.Start.X + MOVE_POINT, (ResizeInfo.Start.Y));
                    break;
            }
            imgInfo.Start = ResizeInfo.Start;

            // 画像情報適用
            ApplyImageInfo(imgInfo);
        }

        /// <summary>
        /// イメージを移動する
        /// </summary>
        public void Move(Point movePoint)
        {
            // 画像変換情報
            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = (CurColType == ColorType.Binary);
            imgInfo.HResolution = _resizeCanvas.HorizontalResolution;
            imgInfo.VResolution = _resizeCanvas.VerticalResolution;

            // 縦横入れ替え
            if ((ResizeInfo.RotateState == RotateState.Right) ||
                (ResizeInfo.RotateState == RotateState.Left))
            {
                imgInfo.Width = _resizeCanvas.Height;
                imgInfo.Height = _resizeCanvas.Width;
            }
            else
            {
                imgInfo.Width = _resizeCanvas.Width;
                imgInfo.Height = _resizeCanvas.Height;
            }
            imgInfo.RotateState = ResizeInfo.RotateState;

            // 回転状態によって調整する
            Point updPoint = movePoint;
            switch (ResizeInfo.RotateState)
            {
                case RotateState.Default:
                    break;
                case RotateState.Right:
                    updPoint = new Point(movePoint.Y, -updPoint.X);
                    break;
                case RotateState.UpDown:
                    updPoint = new Point(-movePoint.X, -updPoint.Y);
                    break;
                case RotateState.Left:
                    updPoint = new Point(-movePoint.Y, updPoint.X);
                    break;
            }

            imgInfo.Start = updPoint;
            ResizeInfo.Start = updPoint;

            // 画像情報適用
            ApplyImageInfo(imgInfo);
        }

        /// <summary>
        /// カラー表示する
        /// </summary>
        public void ToColor()
        {
            if (_resizeCanvas == null) { return; }

            ImageEditor.DisposeBitmap(ref _colorCanvas);
            _colorCanvas = _editor.CloneCanvas(_resizeCanvas);
            _colorCanvas.Tag = "color";

            // 画像変換情報
            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = false;
            imgInfo.HResolution = _resizeCanvas.HorizontalResolution;
            imgInfo.VResolution = _resizeCanvas.VerticalResolution;
            imgInfo.Width = _resizeCanvas.Width;
            imgInfo.Height = _resizeCanvas.Height;
            imgInfo.RotateState = RotateState.Default;  // ここでは回転させないこと
            imgInfo.Start = new Point(0, 0);    // ここで座標指定すると移動してしまうので何もしない

            // 画像変換
            _colorCanvas = _editor.ConvertCanvas(_colorCanvas, imgInfo);

            // 描画オブジェクト切替
            SetCurCanvas(ColorType.Color);
        }

        /// <summary>
        /// グレー表示する
        /// </summary>
        public void ToGray()
        {
            if (_resizeCanvas == null) { return; }

            ImageEditor.DisposeBitmap(ref _grayCanvas);
            _grayCanvas = _editor.ConvToGrayScale(_resizeCanvas);
            _grayCanvas.Tag = "gray";

            // 画像変換情報
            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = false;
            imgInfo.HResolution = _resizeCanvas.HorizontalResolution;
            imgInfo.VResolution = _resizeCanvas.VerticalResolution;
            imgInfo.Width = _resizeCanvas.Width;
            imgInfo.Height = _resizeCanvas.Height;
            imgInfo.RotateState = RotateState.Default;  // ここでは回転させないこと
            imgInfo.Start = new Point(0, 0);    // ここで座標指定すると移動してしまうので何もしない

            // 画像変換
            _grayCanvas = _editor.ConvertCanvas(_grayCanvas, imgInfo);

            // 描画オブジェクト切替
            SetCurCanvas(ColorType.Gray);
        }

        /// <summary>
        /// ２値表示する
        /// </summary>
        public void ToBinary()
        {
            if (_resizeCanvas == null) { return; }

            // 描画領域クローン
            ImageEditor.DisposeBitmap(ref _binCanvas);
            _binCanvas = _editor.CloneCanvas(_resizeCanvas);
            _binCanvas.Tag = "bin";

            // 画像変換情報
            ImageEditor.ImageInfo imgInfo = new ImageEditor.ImageInfo();
            imgInfo.IsBinaryColor = true;
            imgInfo.HResolution = _resizeCanvas.HorizontalResolution;
            imgInfo.VResolution = _resizeCanvas.VerticalResolution;
            imgInfo.Width = _resizeCanvas.Width;
            imgInfo.Height = _resizeCanvas.Height;
            imgInfo.RotateState = RotateState.Default;  // ここでは回転させないこと
            imgInfo.Start = new Point(0, 0);    // ここで座標指定すると移動してしまうので何もしない

            // 画像変換
            _binCanvas = _editor.ConvertCanvas(_binCanvas, imgInfo);

            // 描画オブジェクト切替
            SetCurCanvas(ColorType.Binary);
        }

        /// <summary>
        /// コントロール領域を更新する
        /// </summary>
        /// <param name="pbox"></param>
        /// <param name="srcCanvas"></param>
        public void Refresh(PictureBox pbox, CanvasType ctype)
        {
            if (ctype != CanvasType.Edit)
            {
                Bitmap srcCanvas = null;
                GetCanvas(ref srcCanvas, ctype);
                ImageEditor.DisposeBitmap(ref _editCanvas);
                _editCanvas = _editor.CloneCanvas(srcCanvas);
            }

            // コントロールには常に EditCanvas を表示する
            pbox.Width = ResizeInfo.PicBoxWidth;
            pbox.Height = ResizeInfo.PicBoxHeight;
            pbox.Image = _editCanvas;
            pbox.Refresh();
        }

        /// <summary>
        /// 濃淡変更
        /// </summary>
        /// <param name="isAdd"></param>
        public void ChangeBinaryContrast(bool isAdd)
        {
            _editor.ChangeBrightness(isAdd);
        }

        /// <summary>
        /// 描画オブジェクトを変換する
        /// </summary>
        /// <param name="dstTYpe"></param>
        /// <param name="imgInfo"></param>
        public void ConvertCanvas(CanvasType srcTYpe, CanvasType dstTYpe, ImageEditor.ImageInfo imgInfo)
        {
            Bitmap srcCanvas = null;
            GetCanvas(ref srcCanvas, srcTYpe);

            // 画像変換
            switch (dstTYpe)
            {
                case CanvasType.Resize:
                    ImageEditor.DisposeBitmap(ref _resizeCanvas);
                    _resizeCanvas = _editor.ConvertCanvas(srcCanvas, imgInfo);
                    break;
                case CanvasType.Color:
                    ImageEditor.DisposeBitmap(ref _colorCanvas);
                    _colorCanvas = _editor.ConvertCanvas(srcCanvas, imgInfo);
                    break;
                case CanvasType.Gray:
                    ImageEditor.DisposeBitmap(ref _grayCanvas);
                    _grayCanvas = _editor.ConvertCanvas(srcCanvas, imgInfo);
                    break;
                case CanvasType.Binary:
                    ImageEditor.DisposeBitmap(ref _binCanvas);
                    _binCanvas = _editor.ConvertCanvas(srcCanvas, imgInfo);
                    break;
                case CanvasType.Save:
                    ImageEditor.DisposeBitmap(ref _saveCanvas);
                    _saveCanvas = _editor.ConvertCanvas(srcCanvas, imgInfo);
                    break;
                case CanvasType.Edit:
                    ImageEditor.DisposeBitmap(ref _editCanvas);
                    _editCanvas = _editor.ConvertCanvas(srcCanvas, imgInfo);
                    break;
                case CanvasType.Cut:
                    ImageEditor.DisposeBitmap(ref _cutCanvas);
                    _cutCanvas = _editor.ConvertCanvas(srcCanvas, imgInfo);
                    break;
                case CanvasType.Current:
                    break;
            }
        }

        /// <summary>
        /// 画像をトリミングする
        /// </summary>
        /// <param name="srcType"></param>
        /// <param name="cutRect"></param>
        public void Cut(CanvasType srcType, ImageEditor.RectangleInfo cutRect)
        {
            if ((cutRect.Width == 0) || (cutRect.Height == 0)) { return; }

            Rectangle rect = new Rectangle(new Point(cutRect.X1, cutRect.Y1), new Size(cutRect.Width, cutRect.Height));
            Bitmap srcCanvas = null;
            GetCanvas(ref srcCanvas, srcType);
            if (srcCanvas == null) { return; }

            ImageEditor.DisposeBitmap(ref _cutCanvas);
            _cutCanvas = _editor.CutCanvas(srcCanvas, rect);
        }

        /// <summary>
        /// Original画像からトリミングする
        /// </summary>
        /// <param name="cutRect"></param>
        /// <param name="Resolution"></param>
        public void CutToOrg(ImageEditor.RectangleInfo cutRect, float Resolution)
        {
            ImageEditor.DisposeBitmap(ref _cutCanvas);

            Bitmap srcCanvas = null;
            GetCanvas(ref srcCanvas, CanvasType.Original);
            if (srcCanvas == null) { return; }

            if (cutRect == null)
            {
                // cutRectがnullの場合、回転処理のみ実施
                // オリジナルクローンに対して回転してからCutを行う
                using (Bitmap WkCanvas = _editor.CloneCanvas(srcCanvas))
                {
                    // 回転処理
                    RotateBitmap(ResizeInfo.RotateState, WkCanvas);
                    _cutCanvas = _editor.CloneCanvas(WkCanvas);
                }

                return;
            }

            if ((cutRect.Width == 0) || (cutRect.Height == 0)) { return; }
            Rectangle rect = new Rectangle(new Point(cutRect.X1, cutRect.Y1), new Size(cutRect.Width, cutRect.Height));

            // オリジナルクローンに対して回転してからCutを行う
            using (Bitmap WkCanvas = _editor.CloneCanvas(srcCanvas))
            {
                // 回転処理
                RotateBitmap(ResizeInfo.RotateState, WkCanvas);
                // Cut処理
                _cutCanvas = _editor.CutCanvas(WkCanvas, rect, Resolution);
            }
        }

        /// <summary>
        /// 赤枠を描画する
        /// 指定位置から赤枠を外側表示
        /// </summary>
        /// <param name="srcType"></param>
        /// <param name="dstTYpe"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void DrawRectangleOut(CanvasType srcType, CanvasType dstType, Point start, Point end, Pen p)
        {
            Point newStart = new Point(start.X, start.Y);
            Point newEnd = new Point(end.X, end.Y);

            if (ImageEditor.GetLength(start.X, end.X) > 0)
            {
                // Pen幅分Offset
                int PenHalfSize = (int)Math.Floor(p.Width / 2);
                newStart.Offset(PenHalfSize, 0);
                newEnd.Offset(PenHalfSize * -1, 0);
            }
            if (ImageEditor.GetLength(start.Y, end.Y) > 0)
            {
                // Pen幅分Offset
                int PenHalfSize = (int)Math.Ceiling(p.Width / 2);
                newStart.Offset(0, PenHalfSize);
                newEnd.Offset(0, PenHalfSize * -1);
            }

            // 赤枠描画
            DrawRectangle(srcType, dstType, newStart, newEnd, p);
        }

        /// <summary>
        /// 赤枠を描画する
        /// </summary>
        /// <param name="srcType"></param>
        /// <param name="dstTYpe"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void DrawRectangle(CanvasType srcType, CanvasType dstType, Point start, Point end, Pen p)
        {
            Bitmap srcCanvas = null;
            GetCanvas(ref srcCanvas, srcType);

            switch (dstType)
            {
                case CanvasType.Resize:
                    _editor.DrawRectangle(srcCanvas, _resizeCanvas, start, end, p);
                    break;
                case CanvasType.Color:
                    _editor.DrawRectangle(srcCanvas, _colorCanvas, start, end, p);
                    break;
                case CanvasType.Gray:
                    _editor.DrawRectangle(srcCanvas, _grayCanvas, start, end, p);
                    break;
                case CanvasType.Binary:
                    _editor.DrawRectangle(srcCanvas, _binCanvas, start, end, p);
                    break;
                case CanvasType.Save:
                    _editor.DrawRectangle(srcCanvas, _saveCanvas, start, end, p);
                    break;
                case CanvasType.Edit:
                    _editor.DrawRectangle(srcCanvas, _editCanvas, start, end, p);
                    break;
                case CanvasType.Cut:
                    _editor.DrawRectangle(srcCanvas, _cutCanvas, start, end, p);
                    break;
                case CanvasType.Current:
                    break;
            }
        }

        /// <summary>
        /// 赤枠を描画する
        /// </summary>
        /// <param name="srcType"></param>
        /// <param name="dstTYpe"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void DrawRectangle(CanvasType srcType, CanvasType dstType, Rectangle rect, Pen p)
        {
            Bitmap srcCanvas = null;
            GetCanvas(ref srcCanvas, srcType);

            switch (dstType)
            {
                case CanvasType.Resize:
                    _editor.DrawRectangle(srcCanvas, _resizeCanvas, rect, p);
                    break;
                case CanvasType.Color:
                    _editor.DrawRectangle(srcCanvas, _colorCanvas, rect, p);
                    break;
                case CanvasType.Gray:
                    _editor.DrawRectangle(srcCanvas, _grayCanvas, rect, p);
                    break;
                case CanvasType.Binary:
                    _editor.DrawRectangle(srcCanvas, _binCanvas, rect, p);
                    break;
                case CanvasType.Save:
                    _editor.DrawRectangle(srcCanvas, _saveCanvas, rect, p);
                    break;
                case CanvasType.Edit:
                    _editor.DrawRectangle(srcCanvas, _editCanvas, rect, p);
                    break;
                case CanvasType.Cut:
                    _editor.DrawRectangle(srcCanvas, _cutCanvas, rect, p);
                    break;
                case CanvasType.Current:
                    break;
            }
        }

        /// <summary>
        /// 回転状態に応じた移動座標を設定する
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="xpos"></param>
        /// <param name="ypos"></param>
        /// <param name="movex"></param>
        /// <param name="movey"></param>
        public void SetMovePoint(ImageCanvas.MoveType mode, ref int xpos, ref int ypos, int movex, int movey)
        {
            switch (ResizeInfo.RotateState)
            {
                case ImageCanvas.RotateState.Default:
                    // 正面
                    switch (mode)
                    {
                        case ImageCanvas.MoveType.Right:
                            // 右移動
                            xpos += movex;
                            break;
                        case ImageCanvas.MoveType.Down:
                            // 下移動
                            ypos += movey;
                            break;
                        case ImageCanvas.MoveType.Left:
                            // 左移動
                            xpos -= movex;
                            break;
                        case ImageCanvas.MoveType.Up:
                            // 上移動
                            ypos -= movey;
                            break;
                    }
                    break;
                case ImageCanvas.RotateState.Right:
                    // 右向き
                    switch (mode)
                    {
                        case ImageCanvas.MoveType.Right:
                            // 右移動
                            xpos += movey;
                            break;
                        case ImageCanvas.MoveType.Down:
                            // 下移動
                            ypos += movex;
                            break;
                        case ImageCanvas.MoveType.Left:
                            // 左移動
                            xpos -= movey;
                            break;
                        case ImageCanvas.MoveType.Up:
                            // 上移動
                            ypos -= movex;
                            break;
                    }
                    break;
                case ImageCanvas.RotateState.UpDown:
                    // 上下逆
                    switch (mode)
                    {
                        case ImageCanvas.MoveType.Right:
                            // 右移動
                            xpos += movex;
                            break;
                        case ImageCanvas.MoveType.Down:
                            // 下移動
                            ypos += movey;
                            break;
                        case ImageCanvas.MoveType.Left:
                            // 左移動
                            xpos -= movex;
                            break;
                        case ImageCanvas.MoveType.Up:
                            // 上移動
                            ypos -= movey;
                            break;
                    }
                    break;
                case ImageCanvas.RotateState.Left:
                    // 左向き
                    switch (mode)
                    {
                        case ImageCanvas.MoveType.Right:
                            // 右移動
                            xpos += movey;
                            break;
                        case ImageCanvas.MoveType.Down:
                            // 下移動
                            ypos += movex;
                            break;
                        case ImageCanvas.MoveType.Left:
                            // 左移動
                            xpos -= movey;
                            break;
                        case ImageCanvas.MoveType.Up:
                            // 上移動
                            ypos -= movex;
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 編集用描画オブジェクト初期化
        /// </summary>
        public void InitilizeEditBrush()
        {
            ImageEditor.DisposeGraphics(ref _gpEdit);
            _gpEdit = Graphics.FromImage(_editCanvas);
            _gpEdit.DrawImage(_editCanvas, 0, 0, _editCanvas.Width, _editCanvas.Height);
        }

        /// <summary>
        /// 四角枠を描画する
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="editCanvas"></param>
        /// <param name="rect"></param>
        /// <param name="p"></param>
        public void DrawEditRectangle(Rectangle rect, Pen p)
        {
            _editor.DrawRectangle(_gpEdit, CurCanvas, rect, p);
        }

        /// <summary>
        /// 描画オブジェクトを保存する
        /// </summary>
        /// <param name="srcTYpe"></param>
        /// <param name="filePath"></param>
        /// <param name="isBinarySave"></param>
        public void Save(CanvasType srcTYpe, string filePath, bool isBinarySave)
        {
            Bitmap saveCanvas = null;
            GetCanvas(ref saveCanvas, srcTYpe);
            _editor.SaveCanvas(saveCanvas, filePath, isBinarySave);
        }

        /// <summary>
        /// リソース開放
        /// </summary>
        public void Dispose()
        {
            ImageEditor.DisposeBitmap(ref _orgCanvas);
            ImageEditor.DisposeBitmap(ref _resizeCanvas);
            ImageEditor.DisposeBitmap(ref _colorCanvas);
            ImageEditor.DisposeBitmap(ref _grayCanvas);
            ImageEditor.DisposeBitmap(ref _binCanvas);
            ImageEditor.DisposeBitmap(ref _saveCanvas);
            ImageEditor.DisposeBitmap(ref _editCanvas);
            ImageEditor.DisposeBitmap(ref _cutCanvas);
            ImageEditor.DisposeGraphics(ref _gpEdit);
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 原寸サイズを基に拡大縮小率を加味したサイズを取得する
        /// </summary>
        /// <param name="size"></param>
        /// <param name="reduceRate"></param>
        /// <returns></returns>
        public int GetDisplaySize(int size, bool isImageReduce = true)
        {
            if (isImageReduce)
            {
                return (int)Math.Round(size * ResizeInfo.DSP_REDUCE_RATE * ResizeInfo.IMG_REDUCE_RATE);
            }
            else
            {
                return (int)Math.Round(size * ResizeInfo.DSP_REDUCE_RATE);
            }
        }

        /// <summary>
        /// 描画オブジェクトを取得する
        /// </summary>
        /// <param name="ctype"></param>
        /// <returns></returns>
        private void GetCanvas(ref Bitmap canvas, CanvasType ctype)
        {
            canvas = null;
            switch (ctype)
            {
                case CanvasType.Original:
                    canvas = _orgCanvas;
                    break;
                case CanvasType.Resize:
                    canvas = _resizeCanvas;
                    break;
                case CanvasType.Color:
                    canvas = _colorCanvas;
                    break;
                case CanvasType.Gray:
                    canvas = _grayCanvas;
                    break;
                case CanvasType.Binary:
                    canvas = _binCanvas;
                    break;
                case CanvasType.Save:
                    canvas = _saveCanvas;
                    break;
                case CanvasType.Edit:
                    canvas = _editCanvas;
                    break;
                case CanvasType.Cut:
                    canvas = _cutCanvas;
                    break;
                case CanvasType.Current:

                    switch (CurColType)
                    {
                        case ColorType.Color:
                            canvas = _colorCanvas;
                            break;
                        case ColorType.Gray:
                            canvas = _grayCanvas;
                            break;
                        case ColorType.Binary:
                            canvas = _binCanvas;
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 描画オブジェクト切替
        /// </summary>
        /// <param name="colType"></param>
        private void SetCurCanvas(ColorType colType)
        {
            CurColType = colType;
            switch (colType)
            {
                case ColorType.Color:
                    CurCanvas = _colorCanvas;
                    break;
                case ColorType.Gray:
                    CurCanvas = _grayCanvas;
                    break;
                case ColorType.Binary:
                    CurCanvas = _binCanvas;
                    break;
            }
        }

        /// <summary>
        /// 描画オブジェクト複製
        /// </summary>
        private void CloneFromOrgCanvas()
        {
            // 使用する描画オブジェクトのみクローンする（処理時間短縮）

            // サイズ調整用（必須）
            ImageEditor.DisposeBitmap(ref _resizeCanvas);
            _resizeCanvas = _editor.CloneCanvas(_orgCanvas);

            // 初回はカラーのみ初期化
            ImageEditor.DisposeBitmap(ref _colorCanvas);
            _colorCanvas = _editor.CloneCanvas(_orgCanvas);
            _colorCanvas.Tag = "color";
        }

        /// <summary>
        /// 描画オブジェクト複製
        /// </summary>
        private void CloneFromResizeCanvas()
        {
            // 使用する描画オブジェクトのみクローンする（処理時間短縮）

            // カラー（任意）
            if (EnableCanvasColor)
            {
                ImageEditor.DisposeBitmap(ref _colorCanvas);
                _colorCanvas = _editor.CloneCanvas(_resizeCanvas);
                _colorCanvas.Tag = "color";
            }
            // グレー（任意）
            if (EnableCanvasGray)
            {
                ImageEditor.DisposeBitmap(ref _grayCanvas);
                _grayCanvas = _editor.CloneCanvas(_resizeCanvas);
                _grayCanvas.Tag = "gray";
            }
            // ２値（任意）
            if (EnableCanvasBin)
            {
                ImageEditor.DisposeBitmap(ref _binCanvas);
                _binCanvas = _editor.CloneCanvas(_resizeCanvas);
                _binCanvas.Tag = "bin";
            }
            // 保存用（保存するときに複製するので初期化不要）
            //if (EnableCanvasSave)
            //{
            //    ImageEditor.DisposeBitmap(ref _saveCanvas);
            //    _saveCanvas = _editor.CloneCanvas(_resizeCanvas);
            //}

            // 編集用（必須）
            ImageEditor.DisposeBitmap(ref _editCanvas);
            _editCanvas = _editor.CloneCanvas(_resizeCanvas);
            _editCanvas.Tag = "edit";

            // 切出用（任意）
            if (EnableCanvasCut)
            {
                ImageEditor.DisposeBitmap(ref _cutCanvas);
                _cutCanvas = _editor.CloneCanvas(_resizeCanvas);
                _cutCanvas.Tag = "cut";
            }
        }

        /// <summary>
        /// サイズ変更を適用する
        /// </summary>
        /// <param name="imgInfo"></param>
        private void ApplyImageInfo(ImageEditor.ImageInfo imgInfo, bool isChange = true)
        {
            // サイズ変更
            ImageEditor.DisposeBitmap(ref _resizeCanvas);
            _resizeCanvas = _editor.ResizeCanvas(_orgCanvas, imgInfo);

            if (ResizeInfo.IsFront)
            {
                ResizeInfo.PicBoxWidth = imgInfo.Width;
                ResizeInfo.PicBoxHeight = imgInfo.Height;
            }
            else
            {
                ResizeInfo.PicBoxWidth = imgInfo.Height;
                ResizeInfo.PicBoxHeight = imgInfo.Width;
            }

            // サイズ情報適用
            ApplyResizeCanvas(isChange);
        }

        /// <summary>
        /// サイズ情報を適用して元の状態に復帰する
        /// </summary>
        private void ApplyResizeCanvas(bool isChange = true)
        {
            // すべての描画オブジェクトにサイズ情報を適用する

            // 描画オブジェクト複製
            CloneFromResizeCanvas();

            // 描画オブジェクト切替
            if (isChange)
            {
                switch (CurColType)
                {
                    case ColorType.Color:
                        ToColor();
                        break;
                    case ColorType.Gray:
                        ToGray();
                        break;
                    case ColorType.Binary:
                        ToBinary();
                        break;
                }
            }
        }

        /// <summary>
        /// 回転状態を取得する
        /// </summary>
        /// <param name="rtype"></param>
        /// <returns></returns>
        private RotateState GetRotateState(RotateType rtype)
        {
            RotateState rstate = ResizeInfo.RotateState;
            if (rtype == RotateType.TurnRight)
            {
                switch (rstate)
                {
                    case RotateState.Default:
                        rstate = RotateState.Right;
                        break;
                    case RotateState.Right:
                        rstate = RotateState.UpDown;
                        break;
                    case RotateState.UpDown:
                        rstate = RotateState.Left;
                        break;
                    case RotateState.Left:
                        rstate = RotateState.Default;
                        break;
                }
            }
            return rstate;
        }

        /// <summary>
        /// イメージを回転する(Bitmap指定)
        /// </summary>
        /// <param name="ctype"></param>
        /// <param name="rtype"></param>
        private void RotateBitmap(ImageCanvas.RotateState rst, Bitmap Canvas)
        {
            // 回転前：正面
            switch (rst)
            {
                case RotateState.Default:
                    // 回転後：正面
                    // 変更なし
                    break;
                case RotateState.Right:
                    // 回転後：右向き
                    Canvas.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case RotateState.UpDown:
                    // 回転後：上下逆
                    Canvas.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case RotateState.Left:
                    // 回転後：左向き
                    Canvas.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
        }

    }
}
