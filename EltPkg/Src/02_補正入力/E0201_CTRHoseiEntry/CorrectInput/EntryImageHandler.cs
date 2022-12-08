using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Common;
using EntryClass;
using EntryCommon;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using ImageController;

namespace CorrectInput
{
    public class EntryImageHandler
    {
        protected Controller _ctl;
        protected ItemManager _itemMgr = null;
        public EntryCommonFormBase form { get; set; } = null;

        protected EntryController _econ { get { return _itemMgr.EntController; } }
        protected EntryDspControl _dcon { get { return _itemMgr.DspControl; } }
        protected EntryImageHandler eiHandler { get { return _itemMgr.ImageHandler; } }
        protected EntryInputChecker eiChecker { get { return _itemMgr.Checker; } }
        protected EntryDataUpdater edUpdater { get { return _itemMgr.Updater; } }
        protected MeisaiInfo _curMei { get { return _itemMgr.CurBat.CurMei; } }

        private ImageEditor _editor = null;
        private ImageCanvas _canvas = null;
        public ImageCanvas Canvas { get { return _canvas; } }
        public bool HasImage { get; private set; } = false;

        private bool _isInit = false;
        private SortedList<int, Rectangle> _recRegions; // ＯＣＲ認識位置
        private SizeF imgSize;
        public Bitmap ColorCanvas { get; private set; } = null;
        public Bitmap CurCanvas { get; set; } = null;
        private Graphics gp = null; // PictureBoxの描画領域

        /// <summary>矢印キーでの画像移動距離（横）</summary>
        private int xpos = 0;
        /// <summary>矢印キーでの画像移動距離（縦）</summary>
        private int ypos = 0;
        private string _dspFilePath;
        private float _reduceRate = 1.0F;

        private const int DEFAULT_SIZE = 100;
        private const int CONST_ADD = 5;

        public PictureBox pcBox { get; private set; }
        public Panel pcPanel { get; private set; }
        public Panel pcPanelLine { get; private set; }

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
        public EntryImageHandler(Controller ctl)
        {
            _ctl = ctl;
            _itemMgr = (ItemManager)_ctl.ItemMgr;
        }


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// イメージコントロールの作成
        /// </summary>
        public bool CreateImageControl(MeisaiInfo mei, int imgKbn)
        {
            TBL_IMG_PARAM img_param = mei.CurDsp.img_param;

            _editor = new ImageEditor();
            _canvas = new ImageCanvas(_editor, img_param.m_REDUCE_RATE);
            _canvas.EnableCanvasGray = true;
            _canvas.EnableCanvasBin = true;

            // 描画領域のサイズ設定
            pcPanel.Top = img_param.m_IMG_TOP;
            pcPanel.Left = img_param.m_IMG_LEFT;
            pcPanel.Height = (img_param.m_IMG_HEIGHT == 0) ? _canvas.ResizeCanvas.Height : img_param.m_IMG_HEIGHT;
            pcPanel.Width = (img_param.m_IMG_WIDTH == 0) ? _canvas.ResizeCanvas.Width : img_param.m_IMG_WIDTH;
            pcPanelLine.Width = pcPanel.Width;

            // スクロールバーの位置設定
            pcPanel.HorizontalScroll.Value = Math.Min(0, pcPanel.HorizontalScroll.Maximum);
            pcPanel.VerticalScroll.Value = Math.Min(img_param.m_XSCROLL_VALUE, pcPanel.VerticalScroll.Maximum);
            _isInit = false;

            pcBox.SizeMode = PictureBoxSizeMode.Normal;
            pcPanel.AutoScroll = true;
            pcPanel.Controls.Add(this.pcBox);

            // 画像ファイル読み込み
            this.HasImage = false;
            ImageInfo img = mei.GetImageInfo(imgKbn);
            string _orgFilePath = _econ.GetImgFilePath(img);
            if (File.Exists(_orgFilePath))
            {
                // ファイルあり
                _dspFilePath = _orgFilePath;
            }
            else
            {
                // ファイルがない場合はサンプル画像を表示
                _dspFilePath = CommonUtil.ConcatPath(ServerIni.Setting.BankSampleImagePath, AppConfig.DefaultImage);
                if (!File.Exists(_dspFilePath)) { return true; }
            }
            this.HasImage = true;

            // 描画領域初期化
            _canvas.InitializeCanvas(_dspFilePath);

            // 縮小率に合わせてサイズ調整
            _canvas.Resize(_reduceRate);
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Color);

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
            return true;
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

            // フォーカス枠描画
            DrawRectangle(itemid);

            // コントロール描画再開
            pcPanel.ResumeLayout();
        }

        /// <summary>
        /// 帳票拡大／縮小
        /// </summary>
        /// <param name="ed"></param>
        /// <param name="mode">0:拡大, 1:縮小</param>
        public void SizeChangeImage(int mode, int itemid)
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
                    EnableScroll(ScrollType.Both);
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
                    return;
            }
            // 拡大・縮小・回転・移動したら CurCanvas を再表示する（メソッド内で EditCanvas を表示）
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Current);

            // スクロールバー調整
            int defW = (int)Math.Round(_canvas.ResizeCanvas.Width / _reduceRate);
            int defH = (int)Math.Round(_canvas.ResizeCanvas.Height / _reduceRate);
            if (defH > pcPanel.Height)
            {
                EnableScroll(ScrollType.Vertical);
                if (defW > pcPanel.Width)
                {
                    EnableScroll(ScrollType.Both);
                }
            }
            else
            {
                EnableScroll(ScrollType.None);
                if (defW > pcPanel.Width)
                {
                    EnableScroll(ScrollType.Horizontal);
                }
            }

            // フォーカス枠描画
            DrawRectangle(itemid);

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

            // フォーカス枠描画
            DrawRectangle(itemid);

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

            //// エントリー取込の場合のみ帳票回転で画像ファイルを保存する
            //if (_itemMgr.DspParams.IsImgEntryExec)
            //{
            //    using (Bitmap img = ImageEditor.CreateBitmap(_dspFilePath))
            //    {
            //        img.SetResolution(300.0F, 300.0F);
            //        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //        img.Save(_dspFilePath, ImageFormat.Jpeg);
            //    }
            //}

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
            RefreshRectangleInfo();
            pcBox = new PictureBox();
            pcPanel = new Panel();
            pcPanel.BackColor = SystemColors.ScrollBar;
            pcPanel.BorderStyle = BorderStyle.FixedSingle;
            pcPanelLine = new Panel();
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
        /// フォーカス枠描画
        /// </summary>
        private void DrawRectangle(int itemid)
        {
            // イメージが「表」以外は描画不要
            if (!_curMei.CurImg.IsOmote) { return; }

            // 読取専用の場合は描画不良
            if (_ctl.IsDspReadOnly) { return; }

            // 編集描画オブジェクト初期化
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Current);
            // 切出枠を描画する
            _canvas.InitilizeEditBrush();

            // フォーカスすべきpicturePanelの開始左上位置
            int recRegionPX = pcPanel.HorizontalScroll.Value;
            int recRegionPY = pcPanel.VerticalScroll.Value;
            bool isDrawRectangle = false;

            // 認識領域描画
            // 枠を全て表示し、フォーカスの在る項目の枠のみ色を変更する。
            if (itemid >= 0)
            {
                Rectangle focusRect = new Rectangle();
                if (_recRegions.ContainsKey(itemid))
                {
                    // 拡大・縮小
                    Rectangle rec = _recRegions[itemid];
                    int recLeft = (int)Math.Ceiling((float)rec.Left * _canvas.ResizeInfo.DSP_REDUCE_RATE * _canvas.ResizeInfo.IMG_REDUCE_RATE) + xpos;
                    int recTop = (int)Math.Ceiling((float)rec.Top * _canvas.ResizeInfo.DSP_REDUCE_RATE * _canvas.ResizeInfo.IMG_REDUCE_RATE) + ypos;
                    int recWidth = (int)Math.Ceiling((float)rec.Width * _canvas.ResizeInfo.DSP_REDUCE_RATE * _canvas.ResizeInfo.IMG_REDUCE_RATE);
                    int recHeight = (int)Math.Ceiling((float)rec.Height * _canvas.ResizeInfo.DSP_REDUCE_RATE * _canvas.ResizeInfo.IMG_REDUCE_RATE);
                    rec = new Rectangle(recLeft, recTop, recWidth, recHeight);

                    // 赤枠
                    if (recWidth > 0 || recHeight > 0)
                    {
                        Pen p = new Pen(Color.Red, 2);
                        _canvas.DrawEditRectangle(rec, p);
                        focusRect = rec;
                        isDrawRectangle = true;
                    }
                }

                // フォーカスすべきpicturePanelの開始左上位置
                if (isDrawRectangle)
                {
                    recRegionPX = (int)((float)(focusRect.Left) - Const.IMAGE_RECTANGLE_X);
                    recRegionPY = (int)((float)(focusRect.Top) - Const.IMAGE_RECTANGLE_Y);
                    this.pcPanel.AutoScrollPosition = new Point(recRegionPX, recRegionPY);
                    this.pcPanel.Refresh();
                }

                // スクロール初期位置
                if (!_isInit && !isDrawRectangle)
                {
                    int scX = 0;
                    int scY = 0;
                    if (_curMei.ImageScrollPosX != 0)
                    {
                        scX = Math.Min(_curMei.ImageScrollPosX, pcPanel.HorizontalScroll.Maximum);
                    }
                    if (_curMei.ImageScrollPosY != 0)
                    {
                        scY = Math.Min(_curMei.ImageScrollPosY, pcPanel.VerticalScroll.Maximum);
                    }
                    this.pcPanel.AutoScrollPosition = new Point(scX, scY);
                    this.pcPanel.Refresh();
                    _isInit = true;
                }

                _curMei.ImageScrollPosX = pcPanel.HorizontalScroll.Value;
                _curMei.ImageScrollPosY = pcPanel.VerticalScroll.Value;
            }

            // 画面表示する描画領域は常に EditCanvas を指定する
            _canvas.Refresh(pcBox, ImageCanvas.CanvasType.Edit);
        }

        /// <summary>
        /// イメージ再描画
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RefreshImage()
        {
            RefreshImage(xpos, ypos);
        }

        /// <summary>
        /// イメージ再描画
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void RefreshImage(int x, int y)
        {
            if (gp == null) { return; }
            gp.Clear(Color.Transparent);
            gp.DrawImage(_canvas.OrgCanvas, x, y, imgSize.Width, imgSize.Height);
            this.pcPanel.Refresh();
        }

        /// <summary>
        /// TBL_IMG_CURSOR_PARAM を取得する
        /// </summary>
        /// <returns></returns>
        public void RefreshRectangleInfo()
        {
            _recRegions = new SortedList<int, Rectangle>();

            // TBL_IMG_CURSOR_PARAM から座標を取得する
            foreach (TBL_IMG_CURSOR_PARAM imgcur in _curMei.CurDsp.img_cursor_params.Values)
            {
                _recRegions.Add(imgcur._ITEM_ID, new Rectangle(imgcur.m_ITEM_LEFT, imgcur.m_ITEM_TOP, imgcur.m_ITEM_WIDTH, imgcur.m_ITEM_HEIGHT));
            }

            // TBL_TRITEM の認識座標からも取得する
            foreach (TBL_TRITEM item in _curMei.tritems.Values)
            {
                if (!_recRegions.ContainsKey(item._ITEM_ID))
                {
                    // IMG_CURSOR_PARAMから取得なしであれば追加
                    _recRegions.Add(item._ITEM_ID, new Rectangle(
                        DBConvert.ToIntNull(item.m_ITEM_LEFT),
                        DBConvert.ToIntNull(item.m_ITEM_TOP),
                        DBConvert.ToIntNull(item.m_ITEM_WIDTH),
                        DBConvert.ToIntNull(item.m_ITEM_HEIGHT)));
                    continue;
                }

                // TRITEM座標が未定義の場合次へ
                long itempos = item.m_ITEM_LEFT + item.m_ITEM_TOP + item.m_ITEM_WIDTH + item.m_ITEM_HEIGHT;
                if (itempos < 1) { continue; }

                // TBL_IMG_CURSOR_PARAMが定義済の場合次へ
                Rectangle rect = _recRegions[item._ITEM_ID];
                long rectpos = rect.X + rect.Y + rect.Width + rect.Height;
                if (rectpos > 0) { continue; }

                // IMG_CURSOR_PARAMが未定義でTRITEM座標が定義済の場合、TRITEM座標を設定
                _recRegions.Remove(item._ITEM_ID);
                _recRegions.Add(item._ITEM_ID, new Rectangle(
                    DBConvert.ToIntNull(item.m_ITEM_LEFT),
                    DBConvert.ToIntNull(item.m_ITEM_TOP),
                    DBConvert.ToIntNull(item.m_ITEM_WIDTH),
                    DBConvert.ToIntNull(item.m_ITEM_HEIGHT)));
            }
        }

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
                    pcPanel.AutoScroll = true;
                    break;
            }
        }
    }
}
