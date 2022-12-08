using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageController
{
    public class ImageEditor
    {
        /// <summary>濃淡変更：閾値</summary>
        public float BaseContrast { get; set; } = CONTRAST_DEFAULT;

        /// <summary>濃淡変更：増減間隔</summary>
        private int _addContrast = CONTRAST_RATE;

        private const int CONTRAST_DEFAULT = 210;
        private const int CONTRAST_MIN = 0;
        private const int CONTRAST_MAX = 255;
        private const int CONTRAST_RATE = 5;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageEditor(float baseContrast = 0)
        {
        }


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 描画領域をクローンする
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <returns></returns>
        public Bitmap CloneCanvas(Bitmap srcCanvas)
        {
            Rectangle rect = new Rectangle(0, 0, srcCanvas.Width, srcCanvas.Height);
            Bitmap cloneCanvas = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
            cloneCanvas.SetResolution(srcCanvas.HorizontalResolution, srcCanvas.VerticalResolution);
            cloneCanvas.Tag = srcCanvas.Tag;

            Graphics g = Graphics.FromImage(cloneCanvas);
            g.Clear(SystemColors.Control);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(srcCanvas, 0, 0, srcCanvas.Width, srcCanvas.Height);
            g.Dispose();

            return cloneCanvas;
        }

        /// <summary>
        /// 描画領域をクローンする
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="imgInfo"></param>
        /// <returns></returns>
        public Bitmap CloneCanvas(Bitmap srcCanvas, ImageInfo imgInfo)
        {
            Rectangle rect = new Rectangle(0, 0, imgInfo.Width, imgInfo.Height);
            Bitmap cloneCanvas = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
            cloneCanvas.SetResolution(imgInfo.HResolution, imgInfo.VResolution);
            cloneCanvas.Tag = srcCanvas.Tag;

            Graphics g = Graphics.FromImage(cloneCanvas);
            g.Clear(SystemColors.Control);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(srcCanvas, imgInfo.Start.X, imgInfo.Start.Y, imgInfo.Width, imgInfo.Height);
            g.Dispose();

            return cloneCanvas;
        }

        /// <summary>
        /// 描画領域をクローンする
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="imgInfo"></param>
        /// <returns></returns>
        public Bitmap CloneCanvas(Bitmap srcCanvas, ImageInfo imgInfo, int defaultSizeWidth, int defaultSizeHeight, 
                                  ImageCanvas.FitAlignType fitAlign, double pading, Color backcolor)
        {
            Bitmap cloneCanvas = null;
            if (fitAlign == ImageCanvas.FitAlignType.Center)
            {
                cloneCanvas = new Bitmap(defaultSizeWidth, defaultSizeHeight, PixelFormat.Format24bppRgb);
            }
            else
            {
                cloneCanvas = new Bitmap(imgInfo.Width, imgInfo.Height, PixelFormat.Format24bppRgb);
            }
            cloneCanvas.SetResolution(imgInfo.HResolution, imgInfo.VResolution);
            cloneCanvas.Tag = srcCanvas.Tag;

            Graphics g = Graphics.FromImage(cloneCanvas);
            g.Clear(SystemColors.Control);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(new SolidBrush(backcolor), 0, 0, cloneCanvas.Width, cloneCanvas.Height);
            g.DrawImage(srcCanvas, imgInfo.Start.X, imgInfo.Start.Y, imgInfo.Width, imgInfo.Height);
            g.Dispose();

            return cloneCanvas;
        }


        /// <summary>
        /// 背景色を指定の色にした空のイメージを作成する
        /// </summary>
        public Bitmap CreateSinmpleCanvas(int SizeWidth, int SizeHeight, Color backcolor, bool frame, int pading)
        {
            Bitmap Canvas = null;
            Canvas = new Bitmap(SizeWidth, SizeHeight, PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(Canvas);
            g.Clear(SystemColors.Control);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(new SolidBrush(backcolor), 0, 0, Canvas.Width, Canvas.Height);
            if (frame)
            {
                Pen p = new Pen(Color.Black, 3);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                g.DrawRectangle(p, pading, pading, SizeWidth - (pading * 2), SizeHeight - (pading * 2));
            }
            g.Dispose();

            return Canvas;
        }

        /// <summary>
        /// 描画領域をフィットさせる
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <returns></returns>
        public Bitmap FitCanvas(Bitmap srcCanvas, ImageInfo imgInfo, ImageCanvas.DirectionType direction, int defaultSize)
        {
            if (direction == ImageCanvas.DirectionType.Horizontal)
            {
                // 横幅に合わせる
                imgInfo.Width = defaultSize;
                imgInfo.Height = (int)(srcCanvas.Height * ((double)imgInfo.Width / (double)srcCanvas.Width));
            }
            else
            {
                // 高さに合わせる
                imgInfo.Height = defaultSize;
                imgInfo.Width = (int)(srcCanvas.Width * ((double)imgInfo.Height / (double)srcCanvas.Height));
            }
            return ConvertCanvas(srcCanvas, imgInfo);
        }


        /// <summary>
        /// 描画領域をフィットさせる
        /// アスペクト比で表示
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <returns></returns>
        public Bitmap FitCanvasAspect(Bitmap srcCanvas, ImageInfo imgInfo, int defaultSizeWidth, int defaultSizeHeight, 
                                      ImageCanvas.FitAlignType fitAlign, double pading, Color backcolor, out float ratio)
        {
            if ((srcCanvas.Width + (pading * 2)) <= defaultSizeWidth && (srcCanvas.Height + (pading * 2)) <= defaultSizeHeight)
            {
                ratio = 1;
                imgInfo.Width = srcCanvas.Width;
                imgInfo.Height = srcCanvas.Height;
            }
            else
            {
                float ratioW = (float)(defaultSizeWidth - (pading * 2)) / (float)srcCanvas.Width;
                float ratioH = (float)(defaultSizeHeight - (pading * 2)) / (float)srcCanvas.Height;
                ratio = ratioW < ratioH ? ratioW : ratioH;
                imgInfo.Width = (int)(srcCanvas.Width * ratio);
                imgInfo.Height = (int)(srcCanvas.Height * ratio);
            }
            if (fitAlign == ImageCanvas.FitAlignType.Center)
            {
                imgInfo.Start = new Point((defaultSizeWidth - imgInfo.Width) / 2, (defaultSizeHeight - imgInfo.Height) / 2);
            }

            return ConvertCanvas(srcCanvas, imgInfo, defaultSizeWidth, defaultSizeHeight, fitAlign, pading, backcolor);
        }

        /// <summary>
        /// 描画領域をサイズ変更する
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="imgInfo"></param>
        /// <returns></returns>
        public Bitmap ResizeCanvas(Bitmap srcCanvas, ImageEditor.ImageInfo imgInfo)
        {
            return ConvertCanvas(srcCanvas, imgInfo);
        }

        /// <summary>
        /// イメージをグレースケールにする
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <returns></returns>
        public Bitmap ConvToGrayScale(Bitmap srcCanvas)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(srcCanvas.Width, srcCanvas.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(srcCanvas, new Rectangle(0, 0, srcCanvas.Width, srcCanvas.Height),
               0, 0, srcCanvas.Width, srcCanvas.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        /// <summary>
        /// 画像を変更する
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="imgInfo"></param>
        /// <returns></returns>
        public Bitmap ConvertCanvas(Bitmap srcCanvas, ImageInfo imgInfo)
        {
            Bitmap cloneCanvas = null;
            if (imgInfo.IsBinaryColor)
            {
                // ２値イメージ（24bit）
                cloneCanvas = CloneCanvas(srcCanvas, imgInfo);
                CreateBinaryImage(cloneCanvas, imgInfo);
                RotateCanvas(cloneCanvas, imgInfo);
            }
            else
            {
                // カラーイメージ（24bit）
                cloneCanvas = CloneCanvas(srcCanvas, imgInfo);
                RotateCanvas(cloneCanvas, imgInfo);
            }
            return cloneCanvas;
        }

        /// <summary>
        /// 画像を変更する
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="imgInfo"></param>
        /// <returns></returns>
        public Bitmap ConvertCanvas(Bitmap srcCanvas, ImageInfo imgInfo, int defaultSizeWidth, int defaultSizeHeight, ImageCanvas.FitAlignType fitAlign, double pading, Color backcolor)
        {
            Bitmap cloneCanvas = null;
            if (imgInfo.IsBinaryColor)
            {
                // ２値イメージ（24bit）
                cloneCanvas = CloneCanvas(srcCanvas, imgInfo, defaultSizeWidth, defaultSizeHeight, fitAlign, pading, backcolor);
                CreateBinaryImage(cloneCanvas, imgInfo);
                RotateCanvas(cloneCanvas, imgInfo);
            }
            else
            {
                // カラーイメージ（24bit）
                cloneCanvas = CloneCanvas(srcCanvas, imgInfo, defaultSizeWidth, defaultSizeHeight, fitAlign, pading, backcolor);
                RotateCanvas(cloneCanvas, imgInfo);
            }
            return cloneCanvas;
        }

        /// <summary>
        /// 元の画像から切り出し画像を取得する
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="cutRect"></param>
        /// <returns></returns>
        public Bitmap CutCanvas(Bitmap srcCanvas, Rectangle cutRect)
        {
            // srcCanvasとcutRectの重なった領域を取得（画像をはみ出した領域を切り取る）

            // 画像の領域
            Rectangle imgRect = new Rectangle(0, 0, srcCanvas.Width, srcCanvas.Height);
            // はみ出した部分を切り取る(重なった領域を取得)
            Rectangle roiTrim = Rectangle.Intersect(imgRect, cutRect);
            if (roiTrim.IsEmpty == true) { return null; }

            // 画像の切り出し

            // 切り出す大きさと同じサイズのBitmapオブジェクトを作成
            Bitmap dstCanvas = new Bitmap(roiTrim.Width, roiTrim.Height, srcCanvas.PixelFormat);
            dstCanvas.SetResolution(srcCanvas.HorizontalResolution, srcCanvas.VerticalResolution);

            Graphics g = Graphics.FromImage(dstCanvas);
            Rectangle dstRect = new Rectangle(0, 0, roiTrim.Width, roiTrim.Height);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(srcCanvas, dstRect, roiTrim, GraphicsUnit.Pixel);
            g.Dispose();
            return dstCanvas;
        }

        /// <summary>
        /// 元の画像から切り出し画像を取得する
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="cutRect"></param>
        /// <param name="Resolution"></param>
        /// <returns></returns>
        public Bitmap CutCanvas(Bitmap srcCanvas, Rectangle cutRect, float Resolution)
        {
            // srcCanvasとcutRectの重なった領域を取得（画像をはみ出した領域を切り取る）

            // 画像の領域
            Rectangle imgRect = new Rectangle(0, 0, srcCanvas.Width, srcCanvas.Height);
            // はみ出した部分を切り取る(重なった領域を取得)
            Rectangle roiTrim = Rectangle.Intersect(imgRect, cutRect);
            if (roiTrim.IsEmpty == true) { return null; }

            // 画像の切り出し

            // 切り出す大きさと同じサイズのBitmapオブジェクトを作成
            Bitmap dstCanvas = new Bitmap(roiTrim.Width, roiTrim.Height, srcCanvas.PixelFormat);
            dstCanvas.SetResolution(Resolution, Resolution);

            Graphics g = Graphics.FromImage(dstCanvas);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            Rectangle dstRect = new Rectangle(0, 0, roiTrim.Width, roiTrim.Height);
            g.DrawImage(srcCanvas, dstRect, roiTrim, GraphicsUnit.Pixel);
            g.Dispose();
            return dstCanvas;
        }

        /// <summary>
        /// 四角枠を描画する
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void DrawRectangle(Bitmap srcCanvas, Bitmap editCanvas, Point start, Point end, Pen p)
        {
            if (editCanvas == null) { return; }

            // 領域を描画
            Graphics editBrush = Graphics.FromImage(editCanvas);
            editBrush.DrawImage(srcCanvas, 0, 0, srcCanvas.Width, srcCanvas.Height);
            editBrush.DrawRectangle(p, start.X, start.Y, GetLength(start.X, end.X), GetLength(start.Y, end.Y));
            editBrush.Dispose();
        }

        /// <summary>
        /// 四角枠を描画する
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void DrawRectangle(Bitmap srcCanvas, Bitmap editCanvas, Rectangle rect, Pen p)
        {
            if (editCanvas == null) { return; }

            // 領域を描画
            Graphics editBrush = Graphics.FromImage(editCanvas);
            editBrush.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            editBrush.DrawImage(srcCanvas, 0, 0, srcCanvas.Width, srcCanvas.Height);
            editBrush.DrawRectangle(p, rect);
            editBrush.Dispose();
        }

        /// <summary>
        /// 四角枠を描画する
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void DrawRectangle(Graphics editBrush, Bitmap srcCanvas, Rectangle rect, Pen p)
        {
            // 領域を描画
            editBrush.DrawRectangle(p, rect);
        }

        /// <summary>
        /// 四角枠の座標を算出する
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isSameRetio">縦横同一：true、縦横相違：false</param>
        public void GetRectanblePoint(Point p1, Point p2, ref Point start, ref Point end, bool isSameRetio = true)
        {
            // 小さい方の座標を開始起点とする
            if (p1.X < p2.X)
            {
                start.X = p1.X;
                end.X = p2.X;
            }
            else
            {
                start.X = p2.X;
                end.X = p1.X;
            }

            // 大きい方の座標を終了起点とする
            bool isReverseY = false;
            if (p1.Y < p2.Y)
            {
                start.Y = p1.Y;
                end.Y = p2.Y;
            }
            else
            {
                // Y軸反転
                start.Y = p2.Y;
                end.Y = p1.Y;
                isReverseY = true;
            }

            // 縦横比を合わせる場合は、終了点のY座標に横幅を加算する
            if (isSameRetio)
            {
                if (isReverseY)
                {
                    // 上下反転した場合
                    start.Y = end.Y - GetLength(end.X, start.X);
                }
                else
                {
                    // 通常時
                    end.Y = start.Y + GetLength(end.X, start.X);
                }
            }
        }

        /// <summary>
        /// 画像ファイル情報を取得する
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="imgInfo"></param>
        public ImageInfo GetImageInfo(Bitmap canvas)
        {
            ImageInfo imgInfo = new ImageInfo();

            // 解像度
            imgInfo.HResolution = canvas.HorizontalResolution;
            imgInfo.VResolution = canvas.VerticalResolution;

            // サイズ
            imgInfo.Width = canvas.Width;
            imgInfo.Height = canvas.Height;

            return imgInfo;
        }

        /// <summary>
        /// 画像を保存する
        /// </summary>
        /// <param name="saveCanvas"></param>
        /// <param name="filePath"></param>
        /// <param name="isBinarySave"></param>
        public void SaveCanvas(Bitmap saveCanvas, string filePath, bool isBinarySave)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            // フォーマット変換
            ImageFormat fmt = GetImageFormat(extension);

            if (isBinarySave)
            {
                // ２値で保存する場合は、本物の２値データを作成する
                Bitmap binCanvas = Create1bppIndexedImage(saveCanvas);
                binCanvas.Save(filePath, fmt);
            }
            else
            {
                saveCanvas.Save(filePath, fmt);
            }
        }

        /// <summary>
        /// 画像フォーマットを取得する
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public ImageFormat GetImageFormat(string extension)
        {
            ImageFormat fmt = ImageFormat.Jpeg;
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    fmt = ImageFormat.Jpeg;
                    break;
                case ".bmp":
                    fmt = ImageFormat.Bmp;
                    break;
                case ".tif":
                case ".tiff":
                    fmt = ImageFormat.Tiff;
                    break;
                case ".png":
                    fmt = ImageFormat.Png;
                    break;
                case ".gif":
                    fmt = ImageFormat.Gif;
                    break;
            }
            return fmt;
        }

        /// <summary>
        /// 長さを求める
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int GetLength(int start, int end)
        {
            return Math.Abs(start - end);
        }

        /// <summary>
        /// ファイルパスから描画オブジェクトを生成する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Bitmap CreateBitmap(string filePath)
        {
            // ファイルをロックしないように FileStream で読み込む
            Bitmap bmp = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bmp = new Bitmap(Image.FromStream(fs));
                fs.Close();
            }
            return bmp;
        }

        /// <summary>
        /// ファイルパスから描画オブジェクトを生成する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Bitmap CreateBitmapToClone(string filePath)
        {
            // FileStreamだと解像度等の情報が取得できないため、
            // Bitmapのコンストラクタで読み込んで、
            // ファイルロックさせないようにCloneを作成して返す
            Bitmap bmp = null;
            using (Bitmap DefImage = new Bitmap(filePath))
            {
                bmp = new ImageEditor().CloneCanvas(DefImage);
            }
            return bmp;
        }

        /// <summary>
        /// ファイルパスから描画オブジェクトを生成する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Image CreateImage(string filePath)
        {
            // ファイルをロックしないように FileStream で読み込む
            Image img = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                img = Image.FromStream(fs);
                fs.Close();
            }
            return img;
        }

        /// <summary>
        /// 帳票用データ作成
        /// </summary>
        /// <returns></returns>
        public static byte[] GetImageByte(Bitmap bmp)
        {
            byte[] retVal = null;
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                retVal = ms.ToArray();
            }
            return retVal;
        }

        /// <summary>
        /// 色を反転する
        /// </summary>
        /// <param name="srcFilePath"></param>
        public void ReverseBinaryColor(string srcFilePath, string dstFilePath)
        {
            if (!File.Exists(srcFilePath)) { return; }

            // 一時ファイル
            string srcDir = Path.GetDirectoryName(srcFilePath);
            string srcExtension = Path.GetExtension(srcFilePath);
            string srcFileName = Path.GetFileNameWithoutExtension(srcFilePath) + "_R." + srcExtension;
            string tmpFilePath = Path.Combine(srcDir, srcFileName);

            // MemoryStreamを利用した変換処理（Bitmap→BitmapSource 変換）
            Bitmap bmpTmp1 = CreateBitmap(srcFilePath);
            System.Windows.Media.Imaging.BitmapImage bmpImage = null;
            using (var ms = new System.IO.MemoryStream())
            {
                bmpTmp1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Position = 0;
                bmpImage = new System.Windows.Media.Imaging.BitmapImage();
                // MemoryStreamを書き込むために準備する
                bmpImage.BeginInit();
                bmpImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bmpImage.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.None;
                bmpImage.StreamSource = ms;
                bmpImage.EndInit();
                bmpImage.Freeze();
                System.Windows.Media.Imaging.BitmapSource bitmapSource = bmpImage;
            }

            // BitmapImageのPixelFormatをPbgra32に変換する
            System.Windows.Media.Imaging.FormatConvertedBitmap bitmap = new System.Windows.Media.Imaging.FormatConvertedBitmap(bmpImage, System.Windows.Media.PixelFormats.Pbgra32, null, 0);

            // 画像の大きさに従った配列を作る
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            byte[] originalPixcels = new byte[width * height * 4];
            byte[] inversedPixcels = new byte[width * height * 4];

            // BitmapSourceから配列にコピー
            int stride = (width * bitmap.Format.BitsPerPixel + 7) / 8;
            bitmap.CopyPixels(originalPixcels, stride, 0);

            // 色を反転する
            for (int x = 0; x < originalPixcels.Length; x = x + 4)
            {
                inversedPixcels[x] = (byte)(255 - originalPixcels[x]);
                inversedPixcels[x + 1] = (byte)(255 - originalPixcels[x + 1]);
                inversedPixcels[x + 2] = (byte)(255 - originalPixcels[x + 2]);
                inversedPixcels[x + 3] = originalPixcels[x + 3];
            }

            // 配列からBitmaopSourceを作る
            System.Windows.Media.Imaging.BitmapSource bmpSrc = System.Windows.Media.Imaging.BitmapSource.Create(width, height, 200, 200, System.Windows.Media.PixelFormats.Bgra32, null, inversedPixcels, stride);

            // BitmapSourceを保存する
            Bitmap bmpTmp2 = null;
            try
            {
                using (Stream stream = new FileStream(tmpFilePath, FileMode.Create))
                {
                    System.Windows.Media.Imaging.PngBitmapEncoder encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                    encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmpSrc));
                    encoder.Save(stream);
                }

                // ２値変換する
                bmpTmp2 = ImageEditor.CreateBitmap(tmpFilePath);
                ImageEditor edit = new ImageEditor();
                edit.SaveCanvas(bmpTmp2, dstFilePath, true);
            }
            catch (Exception)
            {
            }
            finally
            {
                DisposeBitmap(ref bmpTmp1);
                DisposeBitmap(ref bmpTmp2);
                File.Delete(tmpFilePath);
            }
        }

        /// <summary>
        /// リソース開放
        /// </summary>
        /// <param name="image"></param>
        public static void DisposeImage(ref Image image)
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
        }

        /// <summary>
        /// リソース開放
        /// </summary>
        /// <param name="image"></param>
        public static void DisposeBitmap(ref Bitmap image)
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
        }

        /// <summary>
        /// リソース開放
        /// </summary>
        /// <param name="image"></param>
        public static void DisposeGraphics(ref Graphics gp)
        {
            if (gp != null)
            {
                gp.Dispose();
                gp = null;
            }
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 濃淡変更
        /// </summary>
        /// <param name="isAdd"></param>
        public void ChangeBrightness(bool isAdd)
        {
            BaseContrast += isAdd ? _addContrast : (_addContrast * (-1));
            BaseContrast = (BaseContrast < CONTRAST_MIN) ? CONTRAST_MIN : BaseContrast;
            BaseContrast = (BaseContrast > CONTRAST_MAX) ? CONTRAST_MAX : BaseContrast;
        }

        /// <summary>
        /// 画像の見た目を２値化する（24ビットカラー）
        /// </summary>
        /// <param name="srcCanvas">変換する画像</param>
        private void CreateBinaryImage(Bitmap srcCanvas, ImageInfo imgInfo)
        {
            BitmapData srcData = null;
            try
            {
                //=====================================================================
                // 変換する画像の１ピクセルあたりのバイト数を取得
                //=====================================================================
                PixelFormat pixelFormat = srcCanvas.PixelFormat;
                int pixelSize = Image.GetPixelFormatSize(pixelFormat) / 8;

                //=====================================================================
                // 変換する画像データをアンマネージ配列にコピー
                //=====================================================================
                srcData = srcCanvas.LockBits(new Rectangle(0, 0, imgInfo.Width, imgInfo.Height), ImageLockMode.ReadWrite, pixelFormat);
                srcCanvas.SetResolution(imgInfo.HResolution, imgInfo.VResolution);
                byte[] buf = new byte[srcData.Stride * srcData.Height];
                Marshal.Copy(srcData.Scan0, buf, 0, buf.Length);

                //=====================================================================
                // ２値化
                //=====================================================================
                for (int y = 0; y < srcData.Height; y++)
                {
                    for (int x = 0; x < srcData.Width; x++)
                    {
                        // ピクセルで考えた場合の開始位置を計算する
                        int pos = y * srcData.Stride + x * pixelSize;

                        // ピクセルの輝度を算出
                        int gray = (int)(0.299 * buf[pos + 2] + 0.587 * buf[pos + 1] + 0.114 * buf[pos]);

                        if (gray > (int)BaseContrast)
                        {
                            // 閾値を超えた場合、白
                            buf[pos] = 0xFF;
                            buf[pos + 1] = 0xFF;
                            buf[pos + 2] = 0xFF;
                        }
                        else
                        {
                            // 閾値以下の場合、黒
                            buf[pos] = 0x0;
                            buf[pos + 1] = 0x0;
                            buf[pos + 2] = 0x0;
                        }
                    }
                }

                Marshal.Copy(buf, 0, srcData.Scan0, buf.Length);
            }
            finally
            {
                if (srcCanvas != null && srcData != null)
                {
                    srcCanvas.UnlockBits(srcData);
                }
            }
        }

        /// <summary>
        /// 2値化画像に変換(オーダー法)
        /// </summary>
        /// <param name="bmpBase">元となる画像</param>
        /// <returns>画像データ</returns>
        public Bitmap Create1bppIndexedImage(Bitmap bmpBase)
        {
            return ConvertBinary(bmpBase, 0, (rgb, th, sz, st) => BinaryOrderedConvert(rgb, sz, st));
        }

        /// <summary>
        /// 2値化用変換関数
        /// </summary>
        /// <param name="bmpBase">元となる画像</param>
        /// <param name="threshold">閾値</param>
        /// <param name="converter">変換式</param>
        /// <returns></returns>
        private Bitmap ConvertBinary(Bitmap bmpBase, Int32 threshold,
                                            Func<Byte[], Int32, Size, Int32, Byte[]> converter)
        {
            var rect = new Rectangle(0, 0, bmpBase.Width, bmpBase.Height);
            var result = new Bitmap(rect.Width, rect.Height, PixelFormat.Format1bppIndexed);
            result.SetResolution(bmpBase.HorizontalResolution, bmpBase.VerticalResolution);

            Byte[] rgbValues;
            // ベースのARGBデータ取得
            using (Bitmap bmp = bmpBase.Clone(rect, PixelFormat.Format32bppArgb))
            {
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                Int32 bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                rgbValues = new Byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                bmp.UnlockBits(bmpData);
            }

            // 変換後の2値データを作成
            BitmapData bmpResultData = result.LockBits(rect, ImageLockMode.WriteOnly, result.PixelFormat);
            // データをもとに計算
            var resultValues = converter(rgbValues, threshold, result.Size, bmpResultData.Stride);
            // 計算結果を画像に反映させる
            IntPtr ptrRet = bmpResultData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(resultValues, 0, ptrRet, resultValues.Length);

            result.UnlockBits(bmpResultData);
            return result;
        }

        /// <summary>
        /// オーダードディザリングの変換処理
        /// </summary>
        /// <param name="rgbValues">色データ</param>
        /// <param name="bmpSize">画像サイズ</param>
        /// <param name="stride">画像読み込み幅</param>
        /// <returns>変換色データ</returns>
        private Byte[] BinaryOrderedConvert(Byte[] rgbValues, Size bmpSize, Int32 stride)
        {
            var br = 0f;
            // 閾値マップを作成する
            var thMap = new Single[4][]
            {
        new Single[4] {1f/17f, 9f/17f, 3f/17f, 11f/17f},
        new Single[4] {13f/17f, 5f/17f, 15f/17f, 7f/17f},
        new Single[4] {4f/17f, 12f/17f, 2f/17f, 10f/17f},
        new Single[4] {16f/17f, 8f/17f, 14f/17f, 6f/17f },
            };
            var c = 0;
            var pos = 0;
            var result = new Byte[stride * bmpSize.Height];

            for (Int32 r = 0; r < bmpSize.Height; r++)
            {
                for (c = 0; c < bmpSize.Width; c++)
                {
                    br = GetBrightness(rgbValues[(r * bmpSize.Width * 4 + c * 4) + 2],  // r
                                       rgbValues[(r * bmpSize.Width * 4 + c * 4) + 1],  // g
                                       rgbValues[(r * bmpSize.Width * 4 + c * 4) + 0]); // b
                    if (thMap[r % 4][c % 4] <= br)
                    {
                        // 色設定
                        pos = (c >> 3) + stride * r;
                        result[pos] |= (Byte)(0x80 >> (c & 0x7));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 輝度計算
        /// </summary>
        /// <param name="r">赤</param>
        /// <param name="g">緑</param>
        /// <param name="b">青</param>
        /// <returns>輝度</returns>
        /// <remarks>
        /// 引数3つは、順不同でもよい。
        /// 単純にMaxとMinを取得しているだけなので。
        /// </remarks>
        private Single GetBrightness(Byte r, Byte g, Byte b)
        {
            Single max = r;
            Single min = r;

            if (max < g) { max = g; }
            if (max < b) { max = b; }

            if (g < min) { min = g; }
            if (b < min) { min = b; }

            return ((max / BaseContrast) + (min / BaseContrast)) / 2;
        }

        /// <summary>
        /// イメージを回転する
        /// </summary>
        /// <param name="srcCanvas"></param>
        /// <param name="imgInfo"></param>
        private void RotateCanvas(Bitmap srcCanvas, ImageInfo imgInfo)
        {
            switch (imgInfo.RotateState)
            {
                case ImageCanvas.RotateState.Default:
                    break;
                case ImageCanvas.RotateState.Right:
                    srcCanvas.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    break;
                case ImageCanvas.RotateState.UpDown:
                    srcCanvas.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    break;
                case ImageCanvas.RotateState.Left:
                    srcCanvas.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    break;
            }
        }


        /// <summary>
        /// イメージ情報
        /// </summary>
        public class ImageInfo
        {
            public bool IsBinaryColor { get; set; } = false;
            public float HResolution { get; set; } = 0.0F;
            public float VResolution { get; set; } = 0.0F;
            public int Width { get; set; } = 0;
            public int Height { get; set; } = 0;
            public ImageCanvas.RotateState RotateState { get; set; } = ImageCanvas.RotateState.Default;
            public Point Start { get; set; } = new Point(0, 0);
        }

        /// <summary>
        /// 四角情報
        /// </summary>
        public class RectangleInfo
        {
            public int X1 { get; set; } = 0;
            public int Y1 { get; set; } = 0;
            public int X2 { get; set; } = 0;
            public int Y2 { get; set; } = 0;
            public int Width { get; set; } = 0;
            public int Height { get; set; } = 0;
            public float BaseContrast { get; set; } = CONTRAST_DEFAULT;

            public RectangleInfo()
            {
            }

            public RectangleInfo(RectangleInfo rect)
            {
                X1 = rect.X1;
                Y1 = rect.Y1;
                X2 = rect.X2;
                Y2 = rect.Y2;
                Width = rect.Width;
                Height = rect.Height;
                BaseContrast = rect.BaseContrast;
            }
        }

    }
}
