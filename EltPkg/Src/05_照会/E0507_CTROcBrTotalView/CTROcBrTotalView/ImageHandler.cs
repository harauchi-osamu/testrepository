using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EntryCommon;
using CommonTable.DB;
using ImageController;

namespace CTROcBrTotalView
{
    public class ImageHandler
    {
        protected Controller _ctl;
        protected ItemManager _itemMgr = null;
        private ImageEditor _editor = null;
        private ImageCanvas _canvas = null;

        private Graphics gp = null; // PictureBoxの描画領域
        private string _dspFilePath;
        private int _panelWidth = 0;
        private int _panelHeight = 0;
        private float _reduceRate = 1.0F;

        public PictureBox pcBox { get; private set; }
        public Panel pcPanel { get; private set; }
        public bool HasImage { get; private set; } = false;
        private int oriW = 0;
        private int oriH = 0;

        public enum ScrollType
        {
            Horizontal,
            Vertical,
            Both,
            None,
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="itemMgr"></param>
        public ImageHandler(Controller ctl)
        {
            _ctl = ctl;
            _itemMgr = (ItemManager)_ctl.ItemMgr;

            pcBox = new PictureBox();
            pcPanel = new Panel();
            pcPanel.BackColor = SystemColors.ScrollBar;
            pcPanel.BorderStyle = BorderStyle.FixedSingle;
        }


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// パネルサイズを初期化する
        /// </summary>
        /// <param name="panelWidth"></param>
        /// <param name="panelHeight"></param>
        public void InitializePanelSize(int panelWidth, int panelHeight)
        {
            _panelWidth = panelWidth;
            _panelHeight = panelHeight;
        }

        /// <summary>
        /// イメージコントロールの作成
        /// </summary>
        public bool CreateImageControl(TBL_BR_TOTAL brt, TBL_IMG_PARAM imgparam, bool isFit = false)
        {
            _editor = new ImageEditor();
            _canvas = new ImageCanvas(_editor, imgparam.m_REDUCE_RATE);
            _canvas.EnableCanvasGray = true;
            _canvas.EnableCanvasBin = true;
            _canvas.IsFit = isFit;

            // 描画領域のサイズ設定
            pcPanel.Top = imgparam.m_IMG_TOP;
            pcPanel.Left = imgparam.m_IMG_LEFT;
            pcPanel.Height = (imgparam.m_IMG_HEIGHT == 0) ? _canvas.ResizeCanvas.Height : imgparam.m_IMG_HEIGHT;
            pcPanel.Width = (imgparam.m_IMG_WIDTH == 0) ? _canvas.ResizeCanvas.Width : imgparam.m_IMG_WIDTH;
            pcPanel.Height = (_panelHeight > 0) ? _panelHeight : pcPanel.Height;
            pcPanel.Width = (_panelWidth > 0) ? _panelWidth : pcPanel.Width;

            // スクロールバーの位置設定
            pcPanel.HorizontalScroll.Value = Math.Min(0, pcPanel.HorizontalScroll.Maximum);
            pcPanel.VerticalScroll.Value = Math.Min(imgparam.m_XSCROLL_VALUE, pcPanel.VerticalScroll.Maximum);

            pcBox.SizeMode = PictureBoxSizeMode.Normal;
            pcPanel.AutoScroll = true;
            pcPanel.Controls.Add(this.pcBox);

            // 画像ファイル読み込み
            this.HasImage = false;
            string _orgFilePath = GetImgFilePath(brt);
            if (File.Exists(_orgFilePath))
            {
                // ファイルあり
                _dspFilePath = _orgFilePath;
            }
            else
            {
                return true;
            }
            this.HasImage = true;

            // 描画領域初期化
            _canvas.InitializeCanvas(_dspFilePath);

            // 縮小率に合わせてサイズ調整
            _canvas.Resize(_reduceRate);
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Color);

            // 全体サイズ
            if (_canvas.IsFit)
            {
                // デフォルトサイズを保持
                _canvas.SetDefaultReSize(_canvas.ResizeCanvas.Width, _canvas.ResizeCanvas.Height);

                // 縮小率の小さい方にフィットさせる
                float hReduce = (float)pcPanel.Width / (float)_canvas.ResizeCanvas.Width;
                float vReduce = (float)pcPanel.Height / (float)_canvas.ResizeCanvas.Height;
                if (hReduce > vReduce)
                {
                    EnableScroll(ScrollType.None);
                    pcPanel.AutoScroll = false;
                    _canvas.ToFitCanvas(ImageCanvas.DirectionType.Vertical, pcPanel.Width, pcPanel.Height);
                }
                else
                {
                    EnableScroll(ScrollType.None);
                    pcPanel.AutoScroll = false;
                    _canvas.ToFitCanvas(ImageCanvas.DirectionType.Horizontal, pcPanel.Width, pcPanel.Height);
                }

                // 拡大・縮小・回転・移動したら CurCanvas を再表示する（メソッド内で EditCanvas を表示）
                _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Current);

                // コントロール描画再開
                pcPanel.ResumeLayout();

                // デフォルトサイズを保持（全体表示）
                _canvas.SetDefaultFitSize(_canvas.ResizeCanvas.Width, _canvas.ResizeCanvas.Height);
            }
            else
            {
                // デフォルトサイズを保持
                int defW = (int)Math.Round(_canvas.ResizeCanvas.Width / _reduceRate);
                int defH = (int)Math.Round(_canvas.ResizeCanvas.Height / _reduceRate);
                if (_canvas.ResizeInfo.IsFront)
                {
                    _canvas.SetDefaultReSize(defW, defH);
                }
                else
                {
                    _canvas.SetDefaultReSize(defH, defW);
                }
            }
            return true;
        }

        /// <summary>
        /// イメージファイルパスを取得する
        /// </summary>
        private string GetImgFilePath(TBL_BR_TOTAL brt)
        {
            return Path.Combine(ServerIni.Setting.BankTotalImageRoot, brt.m_IMPORT_IMG_FLNM);
        }

        /// <summary>
        /// イメージ表示位置を設定する
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public void SetImagePosition(int top, int left = -1)
        {
            pcPanel.Top = top;
            if (left > -1)
            {
                pcPanel.Left = left;
            }
        }

        /// <summary>
        /// イメージ描画領域の設定
        /// </summary>
        /// <param name="itemid"></param>
        public void SetImageRegion(int itemid)
        {
            if (!HasImage) { return; }

            // コントロール描画中断
            pcPanel.SuspendLayout();

            // コントロール描画再開
            pcPanel.ResumeLayout();
        }

        /// <summary>
        /// 帳票拡大／縮小
        /// </summary>
        /// <param name="ed"></param>
        /// <param name="mode">0:拡大, 1:縮小</param>
        public void SizeChangeImage(int mode, int panelW, int panelH)
        {
            // コントロール描画中断
            pcPanel.SuspendLayout();
            pcPanel.AutoScroll = true;
            bool isZoomIn = (mode == Const.IMAGE_ZOOM_IN);

            // イメージ拡大縮小
            ImageCanvas.ZoomState state = _canvas.SetZoomLevel(isZoomIn);            
            switch (state)
            {
                case ImageCanvas.ZoomState.ZoomIn:
                case ImageCanvas.ZoomState.ZoomOut:

                    // 拡大・縮小                                       
                    _canvas.Zoom(isZoomIn, Const.IMAGE_ZOOM_PER);

                    break;
                case ImageCanvas.ZoomState.Initilize:
                case ImageCanvas.ZoomState.ZoomOver:
                    // 全体サイズ
                    // 縮小率の小さい方にフィットさせる
                    float hReduce = (float)pcPanel.Width / (float)_canvas.ResizeCanvas.Width;
                    float vReduce = (float)pcPanel.Height / (float)_canvas.ResizeCanvas.Height;
                    if (hReduce > vReduce)
                    {
                        EnableScroll(ScrollType.None);
                        _canvas.ToFitCanvas(ImageCanvas.DirectionType.Vertical, pcPanel.Width, pcPanel.Height - Const.SCROLLBAR_WIDTH);
                        _canvas.UpdateReduceRate(ImageCanvas.DirectionType.Vertical);
                    }
                    else
                    {
                        EnableScroll(ScrollType.None);
                        _canvas.ToFitCanvas(ImageCanvas.DirectionType.Horizontal, pcPanel.Width - Const.SCROLLBAR_WIDTH, pcPanel.Height);
                        _canvas.UpdateReduceRate(ImageCanvas.DirectionType.Horizontal);
                    }
                    break;
                case ImageCanvas.ZoomState.None:
                    _canvas.Zoom(isZoomIn, Const.IMAGE_ZOOM_PER);
                    break;
            }
            // 拡大・縮小・回転・移動したら CurCanvas を再表示する（メソッド内で EditCanvas を表示）
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Current);
            oriW = panelW;
            oriH = panelH;
            int defW = (int)Math.Round(_canvas.ResizeCanvas.Width / _reduceRate);
            int defH = (int)Math.Round(_canvas.ResizeCanvas.Height / _reduceRate);

            if (defH > oriH)
            {
                EnableScroll(ScrollType.Vertical);
                if (defW > oriW)
                {

                    EnableScroll(ScrollType.Both);
                }
            }
            else
            {
                EnableScroll(ScrollType.None);
                if (defW > oriW)
                {

                    EnableScroll(ScrollType.Horizontal);
                }
            }
            // コントロール描画再開
            pcPanel.ResumeLayout();
        }

        /// <summary>
        /// 全体表示する
        /// </summary>
        /// <param name="itemid"></param>
        public void SizeChangeImageFit(int itemid)
        {
            // コントロール描画中断
            pcPanel.SuspendLayout();
            pcPanel.AutoScroll = false;

            // イメージ拡大縮小
            _canvas.ZoomLevel = ImageCanvas.ZOOM_CNT_MAX1 - 1;
            _canvas.SetZoomLevel(true);

            // 全体サイズ
            // 縮小率の小さい方にフィットさせる
            float hReduce = (float)pcPanel.Width / (float)_canvas.ResizeCanvas.Width;
            float vReduce = (float)pcPanel.Height / (float)_canvas.ResizeCanvas.Height;
            if (hReduce > vReduce)
            {
                EnableScroll(ScrollType.None);
                _canvas.ToFitCanvas(ImageCanvas.DirectionType.Vertical, pcPanel.Width, pcPanel.Height - Const.SCROLLBAR_WIDTH);
                _canvas.UpdateReduceRate(ImageCanvas.DirectionType.Vertical);
            }
            else
            {
                EnableScroll(ScrollType.None);
                _canvas.ToFitCanvas(ImageCanvas.DirectionType.Horizontal, pcPanel.Width - Const.SCROLLBAR_WIDTH, pcPanel.Height);
                _canvas.UpdateReduceRate(ImageCanvas.DirectionType.Horizontal);
            }

            // 拡大・縮小・回転・移動したら CurCanvas を再表示する（メソッド内で EditCanvas を表示）
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Current);

            // コントロール描画再開
            pcPanel.ResumeLayout();
        }

        /// <summary>
        /// 帳票回転
        /// </summary>
        /// <param name="ed"></param>
        public void RotateImage(int mode)
        {
            // コントロール描画中断
            pcPanel.SuspendLayout();
            pcPanel.AutoScroll = true;

            // スクロール状態変更
            EnableScroll(ScrollType.Both);

            // 回転方向
            ImageCanvas.RotateType rtype = (mode == 0) ? ImageCanvas.RotateType.TurnRight : ImageCanvas.RotateType.TurnLeft;
            ImageCanvas.RotateState rst = GetRotateState(rtype);

            // イメージ回転
            _canvas.Rotate(rst);

            // 拡大・縮小・回転・移動したら CurCanvas を再表示する（メソッド内で EditCanvas を表示）
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Current);

            // コントロール描画再開
            pcPanel.ResumeLayout();
        }

        /// <summary>
        /// イメージをスクロールする
        /// </summary>
        /// <param name="mode"></param>
        public void MoveImage(ImageCanvas.MoveType mode)
        {
            const int MOVE_POINT = 100;
            int mvX = 0;
            int mvY = 0;
            switch (mode)
            {
                case ImageCanvas.MoveType.Up:
                    mvY -= MOVE_POINT;
                    break;
                case ImageCanvas.MoveType.Down:
                    mvY += MOVE_POINT;
                    break;
                case ImageCanvas.MoveType.Left:
                    mvX -= MOVE_POINT;
                    break;
                case ImageCanvas.MoveType.Right:
                    mvX += MOVE_POINT;
                    break;
            }
            pcPanel.AutoScrollPosition = new Point(
                    -pcPanel.AutoScrollPosition.X + mvX,
                    -pcPanel.AutoScrollPosition.Y + mvY);
            pcPanel.Refresh();
        }

        /// <summary>
        /// 画像イメージをクリアする
        /// </summary>
        public void ClearImage()
        {
            if (gp == null) { return; }
            gp.Clear(Color.Transparent);
        }

        /// <summary>
        /// イメージパネルを初期化する
        /// </summary>
        public void InitializeImagePanel()
        {
            pcBox = new PictureBox();
            pcPanel = new Panel();
            pcPanel.BackColor = SystemColors.ScrollBar;
            pcPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// カラー表示する
        /// </summary>
        public void ToColor()
        {
            _canvas.ToColor();
            // 描画領域を更新したので読み込み
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Color);

            // 切出枠を描画する
            _canvas.InitilizeEditBrush();
            // 画面表示する描画領域は常に EditCanvas を指定する
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Edit);
        }

        /// <summary>
        /// グレー表示する
        /// </summary>
        public void ToGray()
        {
            _canvas.ToGray();
            // 描画領域を更新したので読み込み
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Gray);

            // 切出枠を描画する
            _canvas.InitilizeEditBrush();
            // 画面表示する描画領域は常に EditCanvas を指定する
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Edit);
        }

        /// <summary>
        /// ２値表示する
        /// </summary>
        public void ToBinary()
        {
            _canvas.ToBinary();
            // 描画領域を更新したので読み込み
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Binary);

            // 切出枠を描画する
            _canvas.InitilizeEditBrush();
            // 画面表示する描画領域は常に EditCanvas を指定する
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Edit);
        }

        /// <summary>
        /// 濃淡変更
        /// </summary>
        /// <param name="isAdd"></param>
        public void ChangeBinaryCanvas(bool isAdd)
        {
            _canvas.ChangeBinaryContrast(isAdd);
            _canvas.ToBinary();
            // 描画領域を更新したので読み込み
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Current);

            // 切出枠を描画する
            _canvas.InitilizeEditBrush();
            // 画面表示する描画領域は常に EditCanvas を指定する
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Edit);
        }

        /// <summary>
        /// 倍率指定してイメージを初期表示する
        /// </summary>
        /// <param name="reduceRate"></param>
        public void ApplyReduceRate(float reduceRate)
        {
            if (reduceRate == 0) { return; }
            _reduceRate = reduceRate;
        }

        /// <summary>
        /// リソース解放
        /// </summary>
        public void Dispose()
        {
            _canvas.Dispose();
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
        private int GetDisplaySize(int size)
        {
            return (int)Math.Round(size * _canvas.ResizeInfo.DSP_REDUCE_RATE);
        }

        /// <summary>
        /// 拡大縮小率サイズを基に原寸サイズを取得する
        /// </summary>
        /// <param name="size"></param>
        /// <param name="reduceRate"></param>
        /// <returns></returns>
        private int GetRealSize(int size)
        {
            return (int)Math.Round(size / _canvas.ResizeInfo.DSP_REDUCE_RATE);
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

        /// <summary>
        /// スクロール状態変更
        /// </summary>
        /// <param name="mode"></param>
        public void EnableScroll(ScrollType mode)
        {
            switch (mode)
            {
                case ScrollType.Vertical:
                    // 縦スクロールのみ有効
                    pcPanel.AutoScroll = false;
                    pcPanel.HorizontalScroll.Visible = false;
                    pcPanel.HorizontalScroll.Enabled = false;
                    pcPanel.VerticalScroll.Visible = true;
                    pcPanel.VerticalScroll.Enabled = true;
                    pcPanel.AutoScroll = true;
                    break;

                case ScrollType.Horizontal:
                    // 横スクロールのみ有効
                    pcPanel.AutoScroll = false;
                    pcPanel.HorizontalScroll.Visible = true;
                    pcPanel.HorizontalScroll.Enabled = true;
                    pcPanel.VerticalScroll.Visible = false;
                    pcPanel.VerticalScroll.Enabled = false;
                    pcPanel.AutoScroll = true;
                    break;

                case ScrollType.Both:
                    // 両方有効
                    pcPanel.AutoScroll = false;
                    pcPanel.HorizontalScroll.Visible = true;
                    pcPanel.HorizontalScroll.Enabled = true;
                    pcPanel.VerticalScroll.Visible = true;
                    pcPanel.VerticalScroll.Enabled = true;
                    pcPanel.AutoScroll = true;
                    break;

                case ScrollType.None:
                    // 両方無効
                    pcPanel.AutoScroll = false;
                    pcPanel.HorizontalScroll.Visible = false;
                    pcPanel.HorizontalScroll.Enabled = false;
                    pcPanel.VerticalScroll.Visible = false;
                    pcPanel.VerticalScroll.Enabled = false;
                    //pcPanel.AutoScroll = true;
                    break;
            }
        }
    }
}
