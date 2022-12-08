using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;

namespace ParamMaint
{
    /// <summary>
    /// 業務画面調整画面
    /// </summary>
    public partial class DspAdjustForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ItemManager.DisplayParams _dp { get { return _itemMgr.DispParams; } }
        private DspInfo _dsp { get { return _itemMgr.GymParam[_dp.GymId].DspInfos[_dp.DspId]; } }

        private Image img;
        private SizeF imgSize;
        private Graphics gp; // PictureBoxの描画領域
        private Bitmap _bmp; // PictureBoxの描画領域
        private SortedList<int, Rectangle> recRegion; // 認識領域の編集したものを退避しておく
        private int selectedRegion; // 認識編集用に選択されたitemid
        private Point mouseStartPos;
        private Point absContLoc;
        private SortedDictionary<int, Label> lblDspItems;
        private SortedDictionary<int, TextBox> tbDspItems;

        private bool _IsPicPanelMoving = false;
        private bool _IsPicPanelSeized = false;
        private bool _IsImgRegionEdited = false;
        private bool _IsImgRegionDefined = false;
        private bool _IsRecRegSeized = false;
        private bool _IsRecRegMoving = false;
        private bool _IsFontChanged = false;
        private bool _IsImgAdded = false;

        private TBL_DSP_PARAM _dsp_param { get { return _dsp.dsp_param; } }
        private TBL_IMG_PARAM _img_param { get { return _dsp.img_param; } }
        private TBL_HOSEIMODE_PARAM _hosei_param { get { return _dsp.hosei_param; } }
        private SortedList<int, TBL_DSP_ITEM> _dsp_items { get { return _dsp.dsp_items; } }
        private SortedList<int, TBL_HOSEIMODE_DSP_ITEM> _hosei_items { get { return _dsp.hosei_items; } }
        private SortedList<int, TBL_IMG_CURSOR_PARAM> _img_cursors { get { return _dsp.img_cursor_params; } }

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


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DspAdjustForm()
        {
            InitializeComponent();

            tbDspItems = new SortedDictionary<int, TextBox>();
            lblDspItems = new SortedDictionary<int, Label>();
            mouseStartPos = new Point();
            recRegion = new SortedList<int, Rectangle>();
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

            string imgFile = Path.Combine(ServerIni.Setting.BankSampleImagePath, _img_param.m_IMG_FILE);
            _IsImgAdded = File.Exists(imgFile);

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
            base.SetDispName1("業務メンテナンス");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("業務画面調整");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, string.Empty);
            SetFunctionName(F3_, string.Empty);
            SetFunctionName(F4_, string.Empty);
            SetFunctionName(F5_, string.Empty);
            SetFunctionName(F6_, "フォント", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F7_, string.Empty);
            SetFunctionName(F8_, "詳細");
            SetFunctionName(F9_, string.Empty);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "更新");
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
            // 画面コントロールをいったんきれいにする
            ClearDspControls();

            // 画面パラメータコントロール作成
            CreateDspControls();

            // イメージコントロール描画
            DrawImageControls();

            // StatusBar設定
            SetStatusMessage(2, _dsp._DSP_ID.ToString("D4") + " " + _dsp_param.m_DSP_NAME, SystemColors.ButtonFace);

            this.Refresh();
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
        /// [パネル] MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Panels_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                // フォーム全体に対する絶対座標を取得
                int x = ((Control)sender).PointToScreen(e.Location).X;
                int y = ((Control)sender).PointToScreen(e.Location).Y;
                Point absPicLoc = picturePanel.PointToScreen(new Point(0, 0));
                // 位置をステータスラベルに表示
                SetStatusMessage(3, "マウス位置 X,Y: " + x + "," + y, SystemColors.ButtonFace);

                if (_IsImgRegionEdited) { return; }

                if (e.Button.Equals(MouseButtons.Left) && _IsPicPanelMoving)
                {
                    // コントロールの位置をステータスラベルに表示
                    SetStatusMessage(4, "項目位置 TOP,LEFT: " + absPicLoc.X + "," + absPicLoc.Y, SystemColors.ButtonFace);

                    // contentsPanel内だけでpicturePanelを動かす
                    if (x > absContLoc.X && y > absContLoc.Y
                        && x < absContLoc.X + contentsPanel.Width - picturePanel.Width
                        && y < absContLoc.Y + contentsPanel.Height - picturePanel.Height)
                    {
                        picturePanel.Location = picturePanel.Parent.PointToClient(new Point(x, y));
                        return;
                    }
                }
                if (EntryCommonFunc.OnLeftTopCorner(this.picturePanel, x, y, 10))
                {
                    this.Cursor = Cursors.SizeAll;
                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        _IsPicPanelMoving = true;
                    }
                    return;
                }

                if (e.Button.Equals(MouseButtons.Left) && _IsPicPanelSeized)
                {
                    // コントロールの位置をステータスラベルに表示
                    SetStatusMessage(4, "項目位置 TOP,LEFT: " + absContLoc.X + "," + absContLoc.Y, SystemColors.ButtonFace);

                    // contentsPanel内だけでpicturePanelを拡大・縮小
                    if (x > absPicLoc.X && y > absPicLoc.Y
                        && x < absContLoc.X + contentsPanel.Width
                        && y < absContLoc.Y + contentsPanel.Height)
                    {
                        picturePanel.Width = x - absPicLoc.X;
                        picturePanel.Height = y - absPicLoc.Y;
                        return;
                    }
                }

                if (EntryCommonFunc.OnRightBottomCorner(this.picturePanel, x, y, 5))
                {
                    this.Cursor = Cursors.SizeNWSE;
                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        _IsPicPanelSeized = true;
                    }
                    return;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [パネル] MouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Panels_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button.Equals(MouseButtons.Left))
                {
                    _IsPicPanelMoving = false;
                    _IsPicPanelSeized = false;
                    // コントロールの位置をステータスラベルから消す
                    SetStatusMessage(4, "項目位置 TOP,LEFT:", SystemColors.ButtonFace);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [イメージ画像] MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                double approxRate;

                // 認識領域編集モードでない場合は、他のコントロール用のイベントと同じ
                if (!_IsImgRegionEdited)
                {
                    Panels_MouseMove(sender, e);
                    return;
                }

                if (_IsImgRegionDefined)
                {
                    this.Cursor = Cursors.Default;

                    if (e.Button.Equals(MouseButtons.Left) && _IsRecRegMoving)
                    {
                        // pbCtrl内だけで領域を拡大・縮小
                        if (e.X > Math.Abs(pbCtrl.Bounds.Left) && e.X + recRegion[selectedRegion].Width < Math.Abs(pbCtrl.Bounds.Right)
                            && e.Y > Math.Abs(pbCtrl.Bounds.Top) && e.Y + recRegion[selectedRegion].Height < Math.Abs(pbCtrl.Bounds.Bottom))
                        {
                            RefreshImage();
                            recRegion[selectedRegion] = new Rectangle(e.X, e.Y,
                                                Math.Min(recRegion[selectedRegion].Width, pbCtrl.Width - e.X),
                                                Math.Min(recRegion[selectedRegion].Height, pbCtrl.Height - e.Y));
                            gp.DrawRectangle(new Pen(Color.Red, 3), recRegion[selectedRegion]);
                            picturePanel.Refresh();
                            return;
                        }
                    }

                    // マウスイベントの位置はpbCtrl相対位置
                    // 左上の付近(縮小率*0.1)%に入ったら拡大縮小できる
                    approxRate = Math.Max(_dsp.org_img_param.m_REDUCE_RATE * 0.001, 0.001);
                    if (e.X > recRegion[selectedRegion].Left * (1 - approxRate) && e.X < recRegion[selectedRegion].Left * (1 + approxRate) &&
                        e.Y > recRegion[selectedRegion].Top * (1 - approxRate) && e.Y < recRegion[selectedRegion].Top * (1 + approxRate))
                    {
                        this.Cursor = Cursors.SizeAll;
                        if (e.Button.Equals(MouseButtons.Left))
                        {
                            _IsRecRegMoving = true;
                        }
                        return;
                    }

                    if (e.Button.Equals(MouseButtons.Left) && _IsRecRegSeized)
                    {
                        // pbCtrl内だけでpicturePanelを拡大・縮小
                        if (e.X > Math.Abs(pbCtrl.Bounds.Left) && e.X < Math.Abs(pbCtrl.Bounds.Right) + Math.Abs(pbCtrl.Bounds.Left)
                            && e.Y > Math.Abs(pbCtrl.Bounds.Top) && e.Y < Math.Abs(pbCtrl.Bounds.Bottom) + Math.Abs(pbCtrl.Bounds.Top))
                        {
                            RefreshImage();
                            recRegion[selectedRegion] = new Rectangle(recRegion[selectedRegion].Left, recRegion[selectedRegion].Top,
                                            e.X - recRegion[selectedRegion].Left, e.Y - recRegion[selectedRegion].Top);
                            gp.DrawRectangle(new Pen(Color.Red, 3), recRegion[selectedRegion]);
                            picturePanel.Refresh();

                            return;
                        }
                    }

                    // マウスイベントの位置はpicturePanel相対位置
                    approxRate = Math.Max(_dsp.org_img_param.m_REDUCE_RATE * 0.001, 0.001);
                    // 認識領域の付近(縮小率*0.1)%に入ったら拡大縮小できる            
                    if (e.X > recRegion[selectedRegion].Right * (1 - approxRate) && e.X < recRegion[selectedRegion].Right * (1 + approxRate) &&
                        e.Y > recRegion[selectedRegion].Bottom * (1 - approxRate) && e.Y < recRegion[selectedRegion].Bottom * (1 + approxRate))
                    {
                        this.Cursor = Cursors.SizeNWSE;
                        if (e.Button.Equals(MouseButtons.Left))
                        {
                            _IsRecRegSeized = true;
                        }
                        return;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Cross;

                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        if (mouseStartPos.IsEmpty)
                        {
                            mouseStartPos = new Point(e.X, e.Y);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [イメージ画像] MouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCtrl_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                // 認識領域編集モードでない場合は、他のコントロール用のイベントと同じ
                if (!_IsImgRegionEdited)
                {
                    Panels_MouseUp(sender, e);
                    return;
                }

                if (e.Button.Equals(MouseButtons.Left))
                {
                    // 領域がない場合はここで新しい四角を描画する
                    // 左クリックを離すまでは描画されないが、MouseMoveに書いたのではうまくいかないのでこっちにする
                    if (!_IsImgRegionDefined)
                    {
                        RefreshImage();
                        recRegion[selectedRegion] = new Rectangle(Math.Min(mouseStartPos.X, e.X), Math.Min(mouseStartPos.Y, e.Y),
                                                        Math.Min(Math.Abs(e.X - mouseStartPos.X), Math.Abs(pbCtrl.Bounds.Width - mouseStartPos.X)),
                                                        Math.Min(Math.Abs(e.Y - mouseStartPos.Y), Math.Abs(pbCtrl.Bounds.Height - mouseStartPos.Y)));
                        gp.DrawRectangle(new Pen(Color.Red, 3), recRegion[selectedRegion]);
                        picturePanel.Refresh();
                        _IsImgRegionDefined = true;
                    }

                    _IsRecRegSeized = false;
                    _IsRecRegMoving = false;
                    mouseStartPos = new Point();
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [テキストボックス] MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblTxt_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                // マウスの絶対座標
                int x = ((Control)sender).PointToScreen(e.Location).X;
                int y = ((Control)sender).PointToScreen(e.Location).Y;

                // 位置をステータスラベルに表示
                SetStatusMessage(3, "マウス位置 X,Y: " + x + "," + y, SystemColors.ButtonFace);

                this.Cursor = Cursors.SizeAll;

                // 左ボタンが押されていなければ用はない
                if (!e.Button.Equals(MouseButtons.Left)) { return; }

                // もし座標が退避されていないなら絶対座標を退避
                if (mouseStartPos.IsEmpty)
                {
                    mouseStartPos = new Point(x, y);
                }

                // 該当するControlを特定する
                // 各入力項目のテキストボックスか？
                foreach (KeyValuePair<int, TextBox> keyVal in tbDspItems)
                {
                    int itemid = keyVal.Key;
                    TextBox text = keyVal.Value;
                    if (sender == text)
                    {
                        MoveTextBoxControl("Dsp", itemid, x, y);
                        return;
                    }
                }

                // 各入力項目のラベルか？
                foreach (KeyValuePair<int, Label> keyVal in lblDspItems)
                {
                    int itemid = keyVal.Key;
                    Label label = keyVal.Value;
                    if (sender == label)
                    {
                        MoveLabelControl("Dsp", itemid, x, y);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            return;
        }

        /// <summary>
        /// [テキストボックス] MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // イメージ認識領域編集用
                if (!_IsImgAdded) { return; }
                // テキストボックス上の右クリックでイメージ領域編集をトグルする
                if (!e.Button.Equals(MouseButtons.Right)) { return; }

                if (_IsImgRegionEdited)
                {
                    // 右クリックしたコントロールと違うコントロールが編集状態の場合もあるので全部クリア
                    foreach (TextBox tb in tbDspItems.Values)
                    {
                        tb.BackColor = SystemColors.Window;
                    }
                    _IsImgRegionEdited = false;
                    _IsImgRegionDefined = false;
                    _IsRecRegSeized = false;
                    selectedRegion = 0;
                    // グラフィックスを再描画（認識範囲の四角を消す）
                    RefreshImage();
                }
                else
                {
                    // 背景を緑色にして編集モードON
                    ((TextBox)sender).BackColor = Color.Green;
                    _IsImgRegionEdited = true;

                    // 該当するControlを特定して領域描画
                    foreach (KeyValuePair<int, TextBox> keyVal in tbDspItems)
                    {
                        int itemid = keyVal.Key;
                        TextBox text = keyVal.Value;
                        if (sender == text)
                        {
                            selectedRegion = itemid;
                            SetImageRegion(selectedRegion);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [テキストボックス] MouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblTxt_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                // 左ボタンを離したら退避座標をクリア
                if (e.Button.Equals(MouseButtons.Left))
                {
                    mouseStartPos = new Point();
                    // コントロールの位置をステータスラベルから消す
                    SetStatusMessage(4, "項目位置 TOP,LEFT:", SystemColors.ButtonFace);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [イメージ画像] コンテキストメニュー表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbContextMenu_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                // 領域が定義されていない場合は削除メニューを無効に
                if (!_IsImgRegionDefined)
                {
                    toolStripDelRegion.Enabled = false;
                    return;
                }
                toolStripDelRegion.Enabled = true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [コンテキストメニュー：領域の削除] Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripDelRegion_Click(object sender, EventArgs e)
        {
            try
            {
                // 長方形のパラメータ４つをすべて-1にして消去の意味とする
                recRegion[selectedRegion] = new Rectangle(-1, -1, -1, -1);
                RefreshImage();
                _IsImgRegionDefined = false;
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
        /// F1：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 通常処理
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);

                if ((!ChangedDspItem() && !ChangedImage())
                    || ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, EntMessageMgr.Q0106).Equals(DialogResult.Yes))
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F6：フォント
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // フォント
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "フォント", 1);

                // フォント画面
                FontDialog form = new FontDialog();
                form.InitializeForm(_ctl);
                DialogResult res = form.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }

                _dsp_param.m_FONT_SIZE = form.FontSize;
                _IsFontChanged = true;

                // フォントサイズ変更
                CalcTextBoxWidth();

                // 画面表示データ更新
                RefreshDisplayData();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F8：詳細
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 詳細
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "詳細", 1);

                // イメージ詳細設定画面
                ImageParamForm form = new ImageParamForm();
                form.InitializeForm(_ctl);
                DialogResult res = form.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }

                // イメージコントロール描画
                DrawImageControls();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：更新
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 何も変更なかったら戻る
                if (!ChangedDspItem() && !ChangedImage()) { return; }

                // UPDATE実行
                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
                {
                    try
                    {
                        // フォントサイズ更新
                        if (_IsFontChanged && !UpdateDspParam(dbp, auto))
                        {
                            auto.isCommitEnd = false;
                            return;
                        }

                        // 入力項目の更新
                        if (ChangedDspItem() && !UpdateDspItem(dbp, auto))
                        {
                            auto.isCommitEnd = false;
                            return;
                        }

                        if (_IsImgAdded)
                        {
                            // 画像の更新
                            if (ChangedImage() && (!UpdateImgFile(dbp, auto) || !UpdateImgCursors(dbp, auto)))
                            {
                                auto.isCommitEnd = false;
                                return;
                            }
                        }

                        ComMessageMgr.MessageInformation(EntMessageMgr.I0122);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "情報更新成功, 画面ID: " + _dp.DspId, 1);
                    }
                    catch (Exception ex)
                    {
                        auto.isCommitEnd = false;
                        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                        return;
                    }
                }

                // 更新フラグ
                _itemMgr.DispParams.IsDspUpdate = true;
                _IsFontChanged = false;

                // 最新取得
                _itemMgr.FetchAllData();

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// コントロールクリア
        /// </summary>
        private void ClearDspControls()
        {
            foreach (TextBox tb in tbDspItems.Values)
            {
                this.contentsPanel.Controls.Remove(tb);
            }
            foreach (Label lb in lblDspItems.Values)
            {
                this.contentsPanel.Controls.Remove(lb);
            }
        }

        /// <summary>
        /// DSP_ITEM からコントロールを作成する
        /// </summary>
        private void CreateDspControls()
        {
            tbDspItems = new SortedDictionary<int, TextBox>();
            lblDspItems = new SortedDictionary<int, Label>();

            int i = 0;
            foreach (TBL_HOSEIMODE_DSP_ITEM hi in _hosei_items.Values)
            {
                if (!_dsp_items.ContainsKey(hi._ITEM_ID)) { continue; }
                TBL_DSP_ITEM di = _dsp_items[hi._ITEM_ID];
                TextBox text = CreateDefaultTextBox();
                Label label = CreateDefaultLabel();

                bool isC = di.m_ITEM_TYPE.Equals(DspItem.ItemType.C);
                bool isAST = di.m_ITEM_TYPE.Equals(DspItem.ItemType.AST);
                if (!isAST)
                {
                    text.Font = new Font(AplInfo.FontName, (float)_dsp_param.m_FONT_SIZE);
                    text.Top = hi.m_INPUT_POS_TOP;
                    text.Left = hi.m_INPUT_POS_LEFT;
                    text.Width = hi.m_INPUT_WIDTH;
                    text.Height = hi.m_INPUT_HEIGHT;
                    text.ReadOnly = true;
                    text.ShortcutsEnabled = false;
                    text.BackColor = SystemColors.Window;
                    text.Visible = !isC;
                    text.MouseMove += new System.Windows.Forms.MouseEventHandler(LblTxt_MouseMove);
                    text.MouseUp += new System.Windows.Forms.MouseEventHandler(LblTxt_MouseUp);
                    text.MouseDown += new System.Windows.Forms.MouseEventHandler(Txt_MouseDown);
                    contentsPanel.Controls.Add(text);
                    tbDspItems.Add(hi._ITEM_ID, text);
                }
                if (!isC)
                {
                    label.Text = di.m_ITEM_DISPNAME;
                    label.Top = hi.m_NAME_POS_TOP;
                    label.Left = hi.m_NAME_POS_LEFT;
                    label.Font = new Font(AplInfo.LabelFontName, (float)_dsp_param.m_FONT_SIZE);
                    label.AutoSize = true;
                    label.Visible = !isC;
                    label.MouseMove += new System.Windows.Forms.MouseEventHandler(LblTxt_MouseMove);
                    label.MouseUp += new System.Windows.Forms.MouseEventHandler(LblTxt_MouseUp);
                    contentsPanel.Controls.Add(label);
                    lblDspItems.Add(hi._ITEM_ID, label);
                }
                i++;
            }
        }

        /// <summary>
        /// テキストボックスを生成する
        /// </summary>
        /// <returns></returns>
        private TextBox CreateDefaultTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.Font = new Font(Const.FONT_NAME_DEF, Const.FONT_SIZE_ITEM_DEF);
            textBox.Text = string.Empty;
            textBox.Top = 0;
            textBox.Left = 0;
            textBox.Height = 0;
            textBox.Width = 0;
            textBox.ReadOnly = true;
            textBox.ShortcutsEnabled = false;
            textBox.BackColor = SystemColors.Window;
            textBox.Visible = true;
            return textBox;
        }

        /// <summary>
        /// ラベルを生成する
        /// </summary>
        /// <returns></returns>
        private Label CreateDefaultLabel()
        {
            Label label = new Label();
            label.Font = new Font(Const.FONT_NAME_DEF, Const.FONT_SIZE_ITEM_DEF);
            label.Text = string.Empty;
            label.Top = 0;
            label.Left = 0;
            label.AutoSize = true;
            label.Visible = true;
            return label;
        }

        /// <summary>
        /// イメージカーソルを取得する
        /// </summary>
        /// <returns></returns>
        private bool GetImgCursor()
        {
            recRegion = new SortedList<int, Rectangle>();
            foreach (TBL_HOSEIMODE_DSP_ITEM hi in _hosei_items.Values)
            {
                Rectangle rect = new Rectangle();
                if (_img_cursors.ContainsKey(hi._ITEM_ID))
                {
                    TBL_IMG_CURSOR_PARAM cursor = _img_cursors[hi._ITEM_ID];
                    rect = new Rectangle(ChgRectRate(cursor.m_ITEM_LEFT, _img_param.m_REDUCE_RATE),
                                         ChgRectRate(cursor.m_ITEM_TOP, _img_param.m_REDUCE_RATE), 
                                         ChgRectRate(cursor.m_ITEM_WIDTH, _img_param.m_REDUCE_RATE),
                                         ChgRectRate(cursor.m_ITEM_HEIGHT, _img_param.m_REDUCE_RATE));
                }
                recRegion.Add(hi._ITEM_ID, rect);
            }
            return true;
        }

        /// <summary>
        /// イメージコントロール描画
        /// </summary>
        private void DrawImageControls()
        {
            if (_IsImgAdded)
            {
                if (!GetImgCursor())
                {
                    return;
                }

                // イメージコントロール作成（パネル調整あり）
                if (!CreateImageControls())
                {
                    return;
                }
            }
            else
            {
                this.picturePanel.Top = 0;
                this.picturePanel.Left = 0;
                this.picturePanel.Height = 0;
                this.picturePanel.Width = 0;
                this.picturePanel.Visible = false;
                this.btnFunc[F8_].Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CreateImageControls()
        {
            string imgFile = Path.Combine(ServerIni.Setting.BankSampleImagePath, _img_param.m_IMG_FILE);

            // picturebox描画
            CommonUtil.DisposeImage(img);
            img = new Bitmap(imgFile);
            imgSize = new SizeF(img.Width * _img_param.m_REDUCE_RATE / 100f, img.Height * _img_param.m_REDUCE_RATE / 100f);
            int nImgWidth = DBConvert.ToIntNull(imgSize.Width);
            int nImgHeight = DBConvert.ToIntNull(imgSize.Height);
            CommonUtil.DisposeBitmap(_bmp);
            _bmp = new Bitmap(nImgWidth, nImgHeight);

            this.pbCtrl.Width = nImgWidth;
            this.pbCtrl.Height = nImgHeight;
            this.pbCtrl.Image = _bmp;

            CommonUtil.DisposeGraphics(gp);
            gp = Graphics.FromImage(pbCtrl.Image);
            gp.Clear(Color.Transparent);
            gp.DrawImage(img, 0, 0, imgSize.Width, imgSize.Height);

            // 描画領域のサイズ設定
            picturePanel.Top = _img_param.m_IMG_TOP;
            picturePanel.Left = _img_param.m_IMG_LEFT;
            picturePanel.Height = (_img_param.m_IMG_HEIGHT == 0) ? nImgHeight : _img_param.m_IMG_HEIGHT;
            picturePanel.Width = (_img_param.m_IMG_WIDTH == 0) ? nImgWidth : _img_param.m_IMG_WIDTH;

            // スクロールバーの位置設定
            picturePanel.HorizontalScroll.Value = 0;
            picturePanel.VerticalScroll.Value = Math.Min(_img_param.m_XSCROLL_VALUE, picturePanel.VerticalScroll.Maximum);

            return true;
        }

        /// <summary>
        /// 画像イメージを描画する
        /// </summary>
        private void RefreshImage()
        {
            gp.Clear(Color.Transparent);
            gp.DrawImage(img, 0, 0, imgSize.Width, imgSize.Height);
            picturePanel.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="itemid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void MoveTextBoxControl(string p, int itemid, int x, int y)
        {
            TextBox target;
            Label targetlbl;
            switch (p)
            {
                case "Dsp":
                    target = tbDspItems[itemid];
                    targetlbl = lblDspItems[itemid];
                    break;
                default:
                    return;
            }

            // 動かすべき分量を決定
            int movex = x - mouseStartPos.X;
            int movey = y - mouseStartPos.Y;
            // ターゲットの左上座標
            // なぜかTextBoxだけ自身の(0,0)座標取得ではうまくいかないので親コントロール上の座標を絶対座標に変換する
            //Point targetPos = target.PointToScreen(new Point(0, 0));
            Point targetPos = target.Parent.PointToScreen(new Point(target.Left, target.Top));
            Point targetlblPos = targetlbl.PointToScreen(new Point(0, 0));
            // コントロールの位置をステータスラベルに表示
            SetStatusMessage(4, "項目位置 TOP,LEFT: " + targetPos.X + "," + targetPos.Y, SystemColors.ButtonFace);

            // 動かしたときにtextboxかlabelがcontentsPanelより出てしまう場合は、動かさない
            // 四隅の最大値
            //int lx = Math.Min(targetPos.X, targetlblPos.X);
            //int ly = Math.Min(targetPos.Y, targetlblPos.Y);
            //int rx = Math.Max(targetPos.X + target.Width, targetlblPos.X + targetlbl.Width);
            //int ry = Math.Max(targetPos.Y + target.Height, targetlblPos.Y + targetlbl.Height);

            //if (absContLoc.X > lx + movex || absContLoc.Y > ly + movey ||
            //    absContLoc.X + contentsPanel.Width < rx + movex || absContLoc.Y + contentsPanel.Height < ry + movey)
            //{
            //    return;
            //}

            // TextBoxとLabelを移動する
            target.Location = target.Parent.PointToClient(new Point(targetPos.X + movex, targetPos.Y + movey));
            targetlbl.Location = targetlbl.Parent.PointToClient(new Point(targetlblPos.X + movex, targetlblPos.Y + movey));

            // いまのマウス位置が次の移動の初期位置となる
            mouseStartPos = new Point(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="idx"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void MoveLabelControl(string p, int idx, int x, int y)
        {
            // ラベルの場合はラベルのみ動かす
            Label targetlbl;
            switch (p)
            {
                case "Dsp":
                    targetlbl = lblDspItems[idx];
                    break;
                default:
                    return;
            }

            // 動かすべき分量を決定
            int movex = x - mouseStartPos.X;
            int movey = y - mouseStartPos.Y;
            // ターゲットの左上座標
            Point targetlblPos = targetlbl.PointToScreen(new Point(0, 0));
            // コントロールの位置をステータスラベルに表示
            SetStatusMessage(4, "項目位置 TOP,LEFT: " + targetlblPos.X + "," + targetlblPos.Y, SystemColors.ButtonFace);

            // 動かしたときにlabelがcontentsPanelより出てしまう場合は、動かさない
            // 四隅の値
            //int lx = targetlblPos.X;
            //int ly = targetlblPos.Y;
            //int rx = targetlblPos.X + targetlbl.Width;
            //int ry = targetlblPos.Y + targetlbl.Height;

            //if (absContLoc.X > lx + movex || absContLoc.Y > ly + movey ||
            //    absContLoc.X + contentsPanel.Width < rx + movex || absContLoc.Y + contentsPanel.Height < ry + movey)
            //{
            //    return;
            //}

            // Labelを移動する
            targetlbl.Location = targetlbl.Parent.PointToClient(new Point(targetlblPos.X + movex, targetlblPos.Y + movey));

            // Labelが対応するTextBoxのTop/Leftのある範囲内に来た場合、自動的にTop/LeftをTextBoxにあわせる機能がある

            // いまのマウス位置が次の移動の初期位置となる
            mouseStartPos = new Point(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemid"></param>
        private void SetImageRegion(int itemid)
        {
            // イメージカーソルの定義がない場合はイメージ認識位置が未設定
            if (!recRegion.ContainsKey(itemid) ||
                (recRegion[itemid].Top == 0 && recRegion[itemid].Left == 0 && recRegion[itemid].Width == 0 && recRegion[itemid].Height == 0))
            {
                _IsImgRegionDefined = false;
                return;
            }

            _IsImgRegionDefined = true;

            // 認識領域描画
            gp.DrawRectangle(new Pen(Color.Red, 3), recRegion[itemid]);

            // 描画した位置がpicturePanelの中心になるようにフォーカスする
            // 描画領域の中心点（画像上）
            Point recRegionCenter = new Point(recRegion[itemid].Left + recRegion[itemid].Width / 2, recRegion[itemid].Top + recRegion[itemid].Height / 2);
            // フォーカスすべきpicturePanelの開始左上位置
            int recRegionPX = Math.Max(recRegionCenter.X - picturePanel.ClientSize.Width / 2, 0);
            int recRegionPY = Math.Max(recRegionCenter.Y - picturePanel.ClientSize.Height / 2, 0);

            picturePanel.HorizontalScroll.Value = Math.Min(picturePanel.HorizontalScroll.Maximum, recRegionPX);
            picturePanel.VerticalScroll.Value = Math.Min(picturePanel.VerticalScroll.Maximum, recRegionPY);

            picturePanel.Refresh();
        }

        /// <summary>
        /// テキスト幅算出
        /// </summary>
        private void CalcTextBoxWidth()
        {
            int top = 0;
            int sub = 0;
            foreach (TBL_HOSEIMODE_DSP_ITEM hi in _hosei_items.Values)
            {
                TBL_DSP_ITEM di = _dsp_items[hi._ITEM_ID];

                EntryCommonFunc.AdjustSize(_dsp_param.m_FONT_SIZE, ref top, ref sub, ref hi.m_INPUT_HEIGHT);

                SizeF sf = EntryCommonFunc.GetMaxCharSize(_dsp_param.m_FONT_SIZE, di.m_ITEM_TYPE, ref top);
                int width = (int)Math.Floor(sf.Width);
                hi.m_INPUT_WIDTH = DBConvert.ToIntNull(di.m_ITEM_LEN * width + top);

                // 全角の場合は幅を調整する
                if (EntryCommonFunc.IsZenkaku(di.m_ITEM_TYPE))
                {
                    // バイト数から文字桁数を取得
                    int strLen = EntryCommonFunc.GetCharLength(di.m_ITEM_TYPE, di.m_ITEM_LEN);
                    // フォント/４
                    int widthOf4 = (int)Math.Floor(sf.Width / 4.0F);
                    // 文字数/４
                    int strLenOf4 = (int)Math.Floor((float)strLen / 4.0F);
                    // ４の倍数ごとに
                    int subLen = (strLen % 4 == 0) ? 1 : 0;
                    // 差を増やしていく
                    int subWidth = widthOf4 * (strLenOf4 - subLen);
                    // 全角なので２倍する
                    hi.m_INPUT_WIDTH -= (subWidth * 2);
                }
            }
        }

        /// <summary>
        /// 更新チェック
        /// </summary>
        /// <returns></returns>
        private bool ChangedDspItem()
        {
            // フォントの変更があるなら、DspItemの変更も必要のため
            if (_IsFontChanged) { return true; }

            foreach (KeyValuePair<int, TBL_HOSEIMODE_DSP_ITEM> keyVal in _hosei_items)
            {
                int itemid = keyVal.Key;
                TBL_HOSEIMODE_DSP_ITEM hi = keyVal.Value;

                // テキストボックス
                if (tbDspItems[itemid].Top != hi.m_INPUT_POS_TOP) { return true; }
                if (tbDspItems[itemid].Left != hi.m_INPUT_POS_LEFT) { return true; }
                // ラベル
                if (lblDspItems[itemid].Top != hi.m_NAME_POS_TOP) { return true; }
                if (lblDspItems[itemid].Left != hi.m_NAME_POS_LEFT) { return true; }

                // テキストボックスの Width/Height は自動計算されるのでＤＢの値と比較する（フォントサイズ変更時）
                if (!_dsp.org_hosei_items.ContainsKey(itemid)) { continue; }
                TBL_HOSEIMODE_DSP_ITEM org_hi = _dsp.org_hosei_items[itemid];
                if (org_hi.m_INPUT_WIDTH != hi.m_INPUT_WIDTH) { return true; }
                if (org_hi.m_INPUT_HEIGHT != hi.m_INPUT_HEIGHT) { return true; }
            }
            return false;
        }

        /// <summary>
        /// 更新チェック
        /// </summary>
        /// <returns></returns>
        private bool ChangedImage()
        {
            if (_img_param == null) return false;

            TBL_IMG_PARAM org_img_param = _dsp.org_img_param;
            if (org_img_param.m_REDUCE_RATE != _img_param.m_REDUCE_RATE) { return true; }
            if (org_img_param.m_IMG_TOP != _img_param.m_IMG_TOP) { return true; }
            if (org_img_param.m_IMG_LEFT != _img_param.m_IMG_LEFT) { return true; }
            if (org_img_param.m_IMG_HEIGHT != _img_param.m_IMG_HEIGHT) { return true; }
            if (org_img_param.m_IMG_WIDTH != _img_param.m_IMG_WIDTH) { return true; }
            if (org_img_param.m_IMG_BASE_POINT != _img_param.m_IMG_BASE_POINT) { return true; }
            if (org_img_param.m_XSCROLL_VALUE != _img_param.m_XSCROLL_VALUE) { return true; }

            foreach (KeyValuePair<int, TBL_HOSEIMODE_DSP_ITEM> keyVal in _hosei_items)
            {
                int itemid = keyVal.Key;
                TBL_HOSEIMODE_DSP_ITEM hi = keyVal.Value;
                if (!_img_cursors.ContainsKey(itemid)) { continue; }
                if (!recRegion.ContainsKey(itemid)) { continue; }

                TBL_IMG_CURSOR_PARAM cursor = _img_cursors[itemid];
                if (_img_cursors[itemid].m_ITEM_LEFT != recRegion[itemid].Left ||
                    _img_cursors[itemid].m_ITEM_TOP != recRegion[itemid].Top ||
                    _img_cursors[itemid].m_ITEM_WIDTH != recRegion[itemid].Width ||
                    _img_cursors[itemid].m_ITEM_HEIGHT != recRegion[itemid].Height)
                { return true; }
            }
            return false;
        }

        /// <summary>
        /// TBL_DSP_PARAM 更新
        /// </summary>
        /// <returns></returns>
        private bool UpdateDspParam(AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            // フォントサイズ設定画面で設定済みのため UPDATE のみ行う
            TBL_DSP_PARAM tdp = _dsp_param;

            // UPDATE
            string strSQL = tdp.GetUpdateQuery();
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            return true;
        }

        /// <summary>
        /// TBL_HOSEIMODE_DSP_ITEM 更新
        /// </summary>
        /// <returns></returns>
        private bool UpdateDspItem(AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            // スクロールバーの位置を原点に戻す（配置したコントロールの座標が変わるため）
            this.contentsPanel.AutoScrollPosition = new Point(0, 0);

            string strSQL = "";
            foreach (KeyValuePair<int, TBL_HOSEIMODE_DSP_ITEM> keyVal in _hosei_items)
            {
                int itemid = keyVal.Key;
                TBL_HOSEIMODE_DSP_ITEM hi = keyVal.Value;
                hi.m_NAME_POS_TOP = lblDspItems[itemid].Top;
                hi.m_NAME_POS_LEFT = lblDspItems[itemid].Left;
                hi.m_INPUT_POS_TOP = tbDspItems[itemid].Top;
                hi.m_INPUT_POS_LEFT = tbDspItems[itemid].Left;

                // UPDATE
                strSQL = hi.GetUpdateQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            return true;
        }

        /// <summary>
        /// TBL_IMG_PARAM 更新
        /// </summary>
        /// <returns></returns>
        private bool UpdateImgFile(AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            // イメージ詳細設定画面で設定済みのため UPDATE のみ行う
            TBL_IMG_PARAM tif = _img_param;

            // UPDATE
            string strSQL = tif.GetUpdateQuery();
            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            return true;
        }

        /// <summary>
        /// TBL_IMG_CURSOR_PARAM 更新
        /// </summary>
        /// <returns></returns>
        private bool UpdateImgCursors(AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            TBL_IMG_CURSOR_PARAM tic;

            // ここでの処理用にIMG_CURSORをSortedListにしておく
            SortedList<int, TBL_IMG_CURSOR_PARAM> listOrgImgCursors = new SortedList<int, TBL_IMG_CURSOR_PARAM>();
            foreach (TBL_IMG_CURSOR_PARAM orgtic in _img_cursors.Values)
            {
                listOrgImgCursors.Add(orgtic._ITEM_ID, orgtic);
            }

            string strSQL = "";
            foreach (int itemid in recRegion.Keys)
            {
                // すべて-1は削除したものとする
                if (recRegion[itemid].Left == -1 && recRegion[itemid].Top == -1 &&
                    recRegion[itemid].Width == -1 && recRegion[itemid].Height == -1)
                {
                    if (listOrgImgCursors.ContainsKey(itemid))
                    {
                        strSQL = "DELETE FROM " + TBL_IMG_CURSOR_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                        strSQL += " WHERE " + TBL_IMG_CURSOR_PARAM.GYM_ID + "=" + _dp.GymId + " AND ";
                        strSQL += TBL_IMG_CURSOR_PARAM.DSP_ID + "=" + _dp.DspId + " AND ";
                        strSQL += TBL_IMG_CURSOR_PARAM.ITEM_ID + "=" + itemid;
                    }
                }
                else if (listOrgImgCursors.ContainsKey(itemid))
                {
                    // 変更がある場合
                    // 普通の更新
                    tic = listOrgImgCursors[itemid];
                    tic.m_ITEM_LEFT = ChgCursorParamRate(recRegion[itemid].Left, _img_param.m_REDUCE_RATE);
                    tic.m_ITEM_TOP = ChgCursorParamRate(recRegion[itemid].Top, _img_param.m_REDUCE_RATE);
                    tic.m_ITEM_WIDTH = ChgCursorParamRate(recRegion[itemid].Width, _img_param.m_REDUCE_RATE);
                    tic.m_ITEM_HEIGHT = ChgCursorParamRate(recRegion[itemid].Height, _img_param.m_REDUCE_RATE);
                    strSQL = tic.GetUpdateQuery();
                }
                // IMG_CURSORにないので挿入
                else
                {
                    tic = new TBL_IMG_CURSOR_PARAM(_dp.GymId, _dp.DspId, itemid, AppInfo.Setting.SchemaBankCD);
                    tic.m_ITEM_LEFT = ChgCursorParamRate(recRegion[itemid].Left, _img_param.m_REDUCE_RATE);
                    tic.m_ITEM_TOP = ChgCursorParamRate(recRegion[itemid].Top, _img_param.m_REDUCE_RATE);
                    tic.m_ITEM_WIDTH = ChgCursorParamRate(recRegion[itemid].Width, _img_param.m_REDUCE_RATE);
                    tic.m_ITEM_HEIGHT = ChgCursorParamRate(recRegion[itemid].Height, _img_param.m_REDUCE_RATE);
                    strSQL = tic.GetInsertQuery();
                }

                // 登録・更新
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            return true;
        }

        /// <summary>
        /// イメージカーソル位置変更
        /// (実レート→縮小率換算レート)
        /// </summary>
        /// <returns></returns>
        private int ChgRectRate(int CursorLength, int ReduceRate)
        {
            if (ReduceRate <= 0)
            {
                //ないとは思うが、Zero以下の場合は100とする
                ReduceRate = 100;
            }

            return (int)(CursorLength * (_img_param.m_REDUCE_RATE / 100.0F));
        }

        /// <summary>
        /// イメージカーソル位置変更
        /// (縮小率換算レート→実レート)
        /// </summary>
        /// <returns></returns>
        private int ChgCursorParamRate(int CursorLength, int ReduceRate)
        {
            if (ReduceRate <= 0)
            {
                //ないとは思うが、Zero以下の場合は100とする
                ReduceRate = 100;
            }

            return (int)(CursorLength / (_img_param.m_REDUCE_RATE / 100.0F));
        }


    }
}
