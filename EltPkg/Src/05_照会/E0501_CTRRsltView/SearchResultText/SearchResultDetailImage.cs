using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;
using ImageController;
using System.IO;

namespace SearchResultText
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchResultDetailImage : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private ImageEditor _editor = null;
        private ImageCanvas _canvas = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchResultDetailImage()
        {
            InitializeComponent();

            _editor = new ImageEditor();
            _canvas = new ImageCanvas(_editor);
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
            base.SetDispName2("結果照会");
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
                SetFunctionName(5, string.Empty);
                SetFunctionName(6, string.Empty);
                SetFunctionName(7, string.Empty);
                SetFunctionName(8, string.Empty);
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
            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            // ファイル情報欄
            SearchResultCommon.UpdatepnlInfo(_itemMgr, pnlInfo);

            // イメージ表示
            string imgPath = string.Empty;
            string filename = string.Empty;
            switch (_itemMgr.RecordListDispParams.TargetFileDicid)
            {
                case "BUB":
                    // 持出アップロード
                    imgPath = GetBUBPath(out filename);
                    break;
                case "BCA":
                    // 持出取消
                    imgPath = GetBCAPath(out filename);
                    break;
                case "GMA":
                    // 証券データ訂正
                    imgPath = GetGMAPath(out filename);
                    break;
                case "GRA":
                    // 不渡返還
                    imgPath = GetGRAPath(out filename);
                    break;
                default:
                    break;
            }
            DispImage(imgPath, pbimg1, this);

            //証券イメージ欄表示
            lblimgfile.Text = filename;
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
        // イベント（ファンクションキー）
        // *******************************************************************

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// イメージ表示
        /// </summary>
        private void DispImage(string imgpath, PictureBox DispPicture, FormBase form = null)
        {
            if (!File.Exists(imgpath)) return;

            try
            {
                // 画像読込
                _canvas.InitializeCanvas(imgpath);
                ImageCanvas.DirectionType defaulttype = (_canvas.ResizeInfo.PicBoxHeight >= _canvas.ResizeInfo.PicBoxWidth) ? ImageCanvas.DirectionType.Vertical : ImageCanvas.DirectionType.Horizontal;
                _canvas.SetDefaultReSize(DispPicture.Width, DispPicture.Height);
                // 全体表示
                _canvas.ToFitCanvas(defaulttype, DispPicture.Width, DispPicture.Height);
                DispPicture.Width = _canvas.ResizeInfo.PicBoxWidth;
                DispPicture.Height = _canvas.ResizeInfo.PicBoxHeight;
                DispPicture.Image = _canvas.ResizeCanvas;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) { form.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00004, ex.Message)); }
                return ;
            }
        }

        /// <summary>
        ///BUBイメージパス取得
        /// </summary>
        private string GetBUBPath(out string filename)
        {
            //初期化
            filename = string.Empty;

            try
            {
                filename = _itemMgr.ResultTxt.m_RECEPTION.Trim();

                // 対象データの取得
                if (!_itemMgr.GetBUBData(filename, out TBL_TRMEIIMG Data, true, out int InputRoute, this))
                {
                    return string.Empty;
                }

                // バッチフォルダ名の算出
                string BachtFolderPath = _itemMgr.GetBankBacthFolder(Data._GYM_ID, Data._OPERATION_DATE, Data._BAT_ID, InputRoute);
                return Path.Combine(BachtFolderPath, filename);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return string.Empty;
            }
        }

        /// <summary>
        ///BCAイメージパス取得
        /// </summary>
        private string GetBCAPath(out string filename)
        {
            //初期化
            filename = string.Empty;

            try
            {
                filename = _itemMgr.ResultTxt.m_RECEPTION.Substring(1, 62).Trim();

                // 対象データの取得
                if (!_itemMgr.GetBCAData(filename, out TBL_TRMEI Data, true, out int InputRoute, this))
                {
                    return string.Empty;
                }

                // バッチフォルダ名の算出
                string BachtFolderPath = _itemMgr.GetBankBacthFolder(Data._GYM_ID, Data._OPERATION_DATE, Data._BAT_ID, InputRoute);
                return Path.Combine(BachtFolderPath, filename);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return string.Empty;
            }
        }

        /// <summary>
        ///GMAイメージパス取得
        /// </summary>
        private string GetGMAPath(out string filename)
        {
            //初期化
            filename = string.Empty;

            try
            {
                filename = _itemMgr.ResultTxt.m_RECEPTION.Substring(1, 62).Trim();

                // 対象データの取得
                if (!_itemMgr.GetGMAData(filename, out TBL_TRMEI Data, this))
                {
                    return string.Empty;
                }

                // バッチフォルダ名の算出
                string BachtFolderPath = _itemMgr.GetBankBacthFolder(Data._GYM_ID, Data._OPERATION_DATE, Data._BAT_ID, 0);
                return Path.Combine(BachtFolderPath, filename);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return string.Empty;
            }
        }

        /// <summary>
        ///GRAイメージパス取得
        /// </summary>
        private string GetGRAPath(out string filename)
        {
            //初期化
            filename = string.Empty;

            try
            {
                filename = _itemMgr.ResultTxt.m_RECEPTION.Substring(2, 62).Trim();

                // 対象データの取得
                if (!_itemMgr.GetGRAData(filename, out TBL_TRMEI Data, this))
                {
                    return string.Empty;
                }

                // バッチフォルダ名の算出
                string BachtFolderPath = _itemMgr.GetBankBacthFolder(Data._GYM_ID, Data._OPERATION_DATE, Data._BAT_ID, 0);
                return Path.Combine(BachtFolderPath, filename);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return string.Empty;
            }
        }

    }
}

