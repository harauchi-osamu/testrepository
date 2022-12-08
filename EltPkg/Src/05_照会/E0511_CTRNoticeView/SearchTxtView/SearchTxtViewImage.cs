using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace SearchTxtView
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchTxtViewImage : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private ImageHandler _imgHandler = null;
        private ItemManager.ImageInfo _curImage = null;

        #region ListViewNo定義

        private enum ListViewNo
        {
            /// <summary>Key</summary>
            clKey = 0,
            /// <summary>証券イメージファイル名</summary>
            clIMG_NAME = 1,
            /// <summary>銀行コード訂正フラグ</summary>
            clBK_NO_TEISEI_FLG = 2,
            /// <summary>訂正前銀行コード</summary>
            clTEISEI_BEF_BK_NO = 3,
            /// <summary>訂正後銀行コード</summary>
            clTEISEI_AFT_BK_NO = 4,
            /// <summary>交換希望日訂正フラグ</summary>
            clCLEARING_TEISEI_FLG = 5,
            /// <summary>訂正前交換希望日</summary>
            clTEISEI_BEF_CLEARING_DATE = 6,
            /// <summary>訂正後交換希望日</summary>
            clTEISEI_CLEARING_DATE = 7,
            /// <summary>金額訂正フラグ</summary>
            clAMOUNT_TEISEI_FLG = 8,
            /// <summary>訂正前金額</summary>
            clTEISEI_BEF_AMOUNT = 9,
            /// <summary>訂正後金額</summary>
            clTEISEI_AMOUNT = 10,
            /// <summary>二重持出イメージファイル名</summary>
            clDUPLICATE_IMG_NAME = 11,
            /// <summary>不渡返還登録区分</summary>
            clFUBI_REG_KBN = 12,
            /// <summary>不渡返還区分１</summary>
            clFUBI_KBN_01 = 13,
            /// <summary>0号不渡事由コード１</summary>
            clZERO_FUBINO_01 = 14,
            /// <summary>不渡返還区分２</summary>
            clFUBI_KBN_02 = 15,
            /// <summary>0号不渡事由コード２</summary>
            clZRO_FUBINO_02 = 16,
            /// <summary>不渡返還区分３</summary>
            clFUBI_KBN_03 = 17,
            /// <summary>0号不渡事由コード３</summary>
            clZRO_FUBINO_03 = 18,
            /// <summary>不渡返還区分４</summary>
            clFUBI_KBN_04 = 19,
            /// <summary>0号不渡事由コード４</summary>
            clZRO_FUBINO_04 = 20,
            /// <summary>不渡返還区分５</summary>
            clFUBI_KBN_05 = 21,
            /// <summary>0号不渡事由コード５</summary>
            clZRO_FUBINO_05 = 22,
            /// <summary>逆交換対象フラグ</summary>
            clREV_CLEARING_FLG = 23,
        }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchTxtViewImage()
        {
            InitializeComponent();
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
            base.SetDispName1("業務共通");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("通知照会");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (!IsPressShiftKey && !IsPressCtrlKey)
            {
                // 通常状態
                SetFunctionName(1, "戻る");
                SetFunctionName(2, string.Empty);
                SetFunctionName(3, string.Empty);
                SetFunctionName(4, string.Empty);
                SetFunctionName(5, "拡大");
                SetFunctionName(6, "縮小");
                SetFunctionName(7, "回転\n （右回り）", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(8, "回転\n （左回り）", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(9, string.Empty);
                SetFunctionName(10, string.Empty);
                SetFunctionName(11, string.Empty);
                SetFunctionName(12, string.Empty);
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
            SetTabControl(lblImg1, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表]);
            SetTabControl(lblImg2, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏]);
            SetTabControl(lblImg3, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.補箋]);
            SetTabControl(lblImg4, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.付箋]);
            SetTabControl(lblImg5, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.入金証明]);
            SetTabControl(lblImg6, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分]);
            SetTabControl(lblImg7, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏再送分]);
            SetTabControl(lblImg8, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他1]);
            SetTabControl(lblImg9, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他2]);
            SetTabControl(lblImg10, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他3]);
            SetTabControl(lblImg11, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備1]);
            SetTabControl(lblImg12, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備2]);
            SetTabControl(lblImg13, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備3]);
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            SetDisplayParams(true);
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected void SetDisplayParams(bool isImageRefresh)
        {
            // ファイル情報欄
            SearchResultCommon.UpdatepnlInfo(_itemMgr, pnlInfo);

            //詳細表示
            TBL_TSUCHITXT_CTL ctlTsuchi = _itemMgr.GetTsuchiTextControl();
            TBL_TSUCHITXT TsuchiTxt = _itemMgr.GetTsuchiText();
            DispTsuchiData(ctlTsuchi, TsuchiTxt);

            // イメージ取得
            _itemMgr.FetchImgList();
            _itemMgr.Fetch_meiimges();

            // イメージ描画
            if (isImageRefresh)
            {
                if (_curImage == null)
                {
                    //初期表示の場合
                    _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表];
                    if (_itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分].HasImage)
                    {
                        //表再送分があれば優先
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分];
                    }
                }
                else
                {
                    // 以前の設定優先
                    int kbn = _curImage.ImgKbn;
                    _curImage = _itemMgr.ImageInfos[kbn];
                }
                MakeView(_curImage);
            }
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
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            //KeyDownClearStatusMessage(e);
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

        /// <summary>
        /// [タブ]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab_MouseClick(object sender, MouseEventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                switch (((Control)sender).Name)
                {
                    case "lblImg1":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表];
                        break;
                    case "lblImg2":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏];
                        break;
                    case "lblImg3":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.補箋];
                        break;
                    case "lblImg4":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.付箋];
                        break;
                    case "lblImg5":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.入金証明];
                        break;
                    case "lblImg6":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分];
                        break;
                    case "lblImg7":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏再送分];
                        break;
                    case "lblImg8":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他1];
                        break;
                    case "lblImg9":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他2];
                        break;
                    case "lblImg10":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他3];
                        break;
                    case "lblImg11":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備1];
                        break;
                    case "lblImg12":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備2];
                        break;
                    case "lblImg13":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備3];
                        break;


                }
                if (!_curImage.HasImage)
                {
                    return;
                }

                // 画面コントロール描画
                MakeView(_curImage);

                // 画面表示状態更新
                RefreshDisplayState();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvDetailData.Columns[e.ColumnIndex].Width;
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************  

        /// <summary>
        /// F05：拡大
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "拡大", 1);

                // 拡大
                _imgHandler.SizeChangeImage(Const.IMAGE_ZOOM_IN, pnlImage.Width, pnlImage.Height);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F06：縮小
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "縮小", 1);

                // 縮小
                _imgHandler.SizeChangeImage(Const.IMAGE_ZOOM_OUT, pnlImage.Width, pnlImage.Height);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F07：回転（左回り）
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    return;
                }

                // 回転（右回り）
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "回転（右回り）", 1);

                // 右回転
                _imgHandler.RotateImage(0);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F08：回転（右回り）
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "回転（左回り）", 1);

                // 左回転
                _imgHandler.RotateImage(1);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
                RefreshDisplayState();
            }
        }

        // *******************************************************************
        // 内部メソッド（イメージ関連）
        // *******************************************************************

        /// <summary>
        /// 画面コントロール描画
        /// </summary>
        private void MakeView(ItemManager.ImageInfo imgInfo)
        {
            // コントロール描画中断
            this.SuspendLayout();

            // 最初にコントロールを削除する
            this.RemoveImgControl(_imgHandler);

            // TBL_IMG_PARAM は 1 以上の値を入れておく
            TBL_IMG_PARAM imgparam = new TBL_IMG_PARAM(AppInfo.Setting.GymId, 1, AppInfo.Setting.SchemaBankCD);
            imgparam.m_REDUCE_RATE = _ctl.SettingData.ImageReduceRate;
            imgparam.m_IMG_TOP = 1;
            imgparam.m_IMG_LEFT = 1;
            imgparam.m_IMG_HEIGHT = 1;
            imgparam.m_IMG_WIDTH = 1;
            imgparam.m_XSCROLL_VALUE = 0;

            // イメージコントロール作成
            _imgHandler = new ImageHandler(_ctl);
            _imgHandler.InitializePanelSize(pnlImage.Width, pnlImage.Height);
            if (imgInfo.HasImage)
            {
                // 対象イメージがある場合のみ表示処理実施
                _imgHandler.CreateImageControl(imgInfo.MeiImage, imgparam, imgInfo.ImgFolderPath, true);
                // コントロール、コントロールのイベント設定
                this.PutImgControl(_imgHandler);
            }

            // コントロール描画再開
            this.ResumeLayout();
        }

        /// <summary>
        /// 画面コントロールに画像イメージを追加する
        /// </summary>
        /// <param name="eiHandler"></param>
        internal void PutImgControl(ImageHandler eiHandler)
        {
            if (eiHandler == null) { return; }
            contentsPanel.Controls.Add(eiHandler.pcPanel);
            eiHandler.SetImagePosition(pnlImage.Top, pnlImage.Left);
            pnlImage.Visible = false;
        }

        /// <summary>
        /// 画面コントロールから画像イメージを削除する
        /// </summary>
        /// <param name="eiHandler"></param>
        internal void RemoveImgControl(ImageHandler eiHandler)
        {
            if (eiHandler == null) { return; }
            eiHandler.ClearImage();
            contentsPanel.Controls.Remove(eiHandler.pcPanel);
            pnlImage.Visible = true;
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        #region タブ制御

        /// <summary>
        /// タブボタンの表示を切り替える
        /// </summary>
        /// <param name="lblTab"></param>
        /// <param name="imgInfo"></param>
        private void SetTabControl(Label lblTab, ItemManager.ImageInfo imgInfo)
        {
            if (imgInfo.HasImage)
            {
                if (_curImage.ImgKbn == imgInfo.ImgKbn)
                {
                    // 選択済み
                    lblTab.BackColor = Color.LightGreen;
                    lblTab.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    // 未選択
                    lblTab.BackColor = Color.Ivory;
                    lblTab.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            else
            {
                // 使用不可
                lblTab.BackColor = SystemColors.ScrollBar;
                lblTab.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        #endregion

        #region 詳細表示

        /// <summary>
        /// 詳細表示
        /// </summary>
        private void DispTsuchiData(TBL_TSUCHITXT_CTL ctlTsuchi, TBL_TSUCHITXT TsuchiTxt)
        {
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[1];

            listItem = SearchResultCommon.GetSearchDetailListData(_itemMgr, _itemMgr.ImgParams.Key, TsuchiTxt);
            listView[0] = new ListViewItem(listItem.ToArray());

            this.lvDetailData.Items.Clear();
            this.lvDetailData.Items.AddRange(listView);
            this.lvDetailData.Enabled = true;
            this.lvDetailData.Refresh();
            this.lvDetailData.Select();

            // 表示順設定
            DispListOrder(this.lvDetailData, ctlTsuchi);
            //列幅自動調整
            List<int> HdWidth = new List<int>();
            this.lvDetailData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            for (int i = 0; i < this.lvDetailData.Columns.Count; i++)
            {
                HdWidth.Add(this.lvDetailData.Columns[i].Width);
            }
            this.lvDetailData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            for (int i = 0; i < this.lvDetailData.Columns.Count; i++)
            {
                this.lvDetailData.Columns[i].Width = Math.Max(this.lvDetailData.Columns[i].Width, HdWidth[i]);
            }
            this.lvDetailData.Columns[0].Width = 0;

            if (this.lvDetailData.Items.Count > 0)
            {
                this.lvDetailData.Items[0].Selected = true;
                this.lvDetailData.Items[0].Focused = true;
            }
        }


        /// <summary>
        /// ListView 表示順設定
        /// </summary>
        private void DispListOrder(ListView view, TBL_TSUCHITXT_CTL ctlTsuchi)
        {
            // 表示順設定
            switch (ctlTsuchi.m_FILE_DIVID)
            {
                case "BUA":
                case "BUB":
                    view.Columns[(int)ListViewNo.clIMG_NAME].DisplayIndex = 1;
                    view.Columns[(int)ListViewNo.clDUPLICATE_IMG_NAME].DisplayIndex = 2;
                    break;
                case "GMA":
                case "GMB":
                    view.Columns[(int)ListViewNo.clIMG_NAME].DisplayIndex = 1;
                    view.Columns[(int)ListViewNo.clTEISEI_BEF_BK_NO].DisplayIndex = 2;
                    view.Columns[(int)ListViewNo.clTEISEI_AFT_BK_NO].DisplayIndex = 3;
                    view.Columns[(int)ListViewNo.clTEISEI_BEF_CLEARING_DATE].DisplayIndex = 4;
                    view.Columns[(int)ListViewNo.clTEISEI_CLEARING_DATE].DisplayIndex = 5;
                    view.Columns[(int)ListViewNo.clTEISEI_BEF_AMOUNT].DisplayIndex = 6;
                    view.Columns[(int)ListViewNo.clTEISEI_AMOUNT].DisplayIndex = 7;

                    break;
                case "GXA":
                case "GXB":
                    view.Columns[(int)ListViewNo.clIMG_NAME].DisplayIndex = 1;
                    view.Columns[(int)ListViewNo.clTEISEI_BEF_BK_NO].DisplayIndex = 2;
                    view.Columns[(int)ListViewNo.clTEISEI_AFT_BK_NO].DisplayIndex = 3;
                    view.Columns[(int)ListViewNo.clTEISEI_BEF_AMOUNT].DisplayIndex = 4;
                    view.Columns[(int)ListViewNo.clTEISEI_AMOUNT].DisplayIndex = 5;

                    break;
                case "MRA":
                case "MRB":
                case "MRC":
                case "MRD":
                    view.Columns[(int)ListViewNo.clIMG_NAME].DisplayIndex = 1;
                    view.Columns[(int)ListViewNo.clTEISEI_BEF_BK_NO].DisplayIndex = 2;
                    view.Columns[(int)ListViewNo.clTEISEI_AFT_BK_NO].DisplayIndex = 3;

                    break;
                case "GRA":
                    view.Columns[(int)ListViewNo.clIMG_NAME].DisplayIndex = 1;
                    view.Columns[(int)ListViewNo.clFUBI_REG_KBN].DisplayIndex = 2;
                    view.Columns[(int)ListViewNo.clFUBI_KBN_01].DisplayIndex = 3;
                    view.Columns[(int)ListViewNo.clZERO_FUBINO_01].DisplayIndex = 4;
                    view.Columns[(int)ListViewNo.clFUBI_KBN_02].DisplayIndex = 5;
                    view.Columns[(int)ListViewNo.clZRO_FUBINO_02].DisplayIndex = 6;
                    view.Columns[(int)ListViewNo.clFUBI_KBN_03].DisplayIndex = 7;
                    view.Columns[(int)ListViewNo.clZRO_FUBINO_03].DisplayIndex = 8;
                    view.Columns[(int)ListViewNo.clFUBI_KBN_04].DisplayIndex = 9;
                    view.Columns[(int)ListViewNo.clZRO_FUBINO_04].DisplayIndex = 10;
                    view.Columns[(int)ListViewNo.clFUBI_KBN_05].DisplayIndex = 11;
                    view.Columns[(int)ListViewNo.clZRO_FUBINO_05].DisplayIndex = 12;
                    view.Columns[(int)ListViewNo.clREV_CLEARING_FLG].DisplayIndex = 13;

                    break;
                case "BCA":
                case "YCA":
                default:
                    break;
            }
        }

        #endregion

        #region 入力チェック

        /// <summary>
        /// KeyDown時のクリアメッセージ
        /// </summary>
        private void KeyDownClearStatusMessage(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                    break;
                default:
                    this.ClearStatusMessage();
                    break;
            }
        }

        /// <summary>
        /// 全コントロール取得
        /// </summary>
        private static List<Control> AllSubControls(Control control)
        {
            var list = control.Controls.OfType<Control>().ToList();
            var deep = list.Where(o => o.Controls.Count > 0).SelectMany(AllSubControls).ToList();
            list.AddRange(deep);
            return list;
        }

        /// <summary>
        /// 全入力項目チェック
        /// </summary>
        private bool CheckInputAll()
        {
            foreach (Control con in AllSubControls(this).OrderBy(c => c.TabIndex))
            {
                if (con is BaseTextBox)
                {
                    // BaseTextBoxを継承している場合は、チェックイベント強制発生
                    if (((BaseTextBox)con).RaiseI_Validating())
                    {
                        // 項目遷移時、遷移元のValidatingは発生させない
                        this.AutoValidate = AutoValidate.Disable;
                        ((BaseTextBox)con).Select();
                        this.AutoValidate = AutoValidate.EnablePreventFocusChange;

                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

    }
}
