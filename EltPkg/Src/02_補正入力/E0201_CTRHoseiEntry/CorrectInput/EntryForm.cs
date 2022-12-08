using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace CorrectInput
{
    /// <summary>
    /// エントリー画面
    /// </summary>
    public partial class EntryForm : EntryFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryForm()
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
            base.SetDispName1(string.Format("交換{0} 補正入力", GymParam.GymId.GetName(_ctl.GymId)));
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2(string.Format("{0} エントリー入力", HoseiStatus.HoseiInputMode.GetName(_ctl.HoseiInputMode)));
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            base.InitializeFunction();
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            base.SetFunctionState();
        }

        /// <summary>
        /// フォームを再描画する
        /// </summary>
        public override void ResetForm()
        {
            base.ResetForm();
        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        protected override void InitializeControl()
        {
            base.InitializeControl();
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected override void InitializeDisplayData()
        {
            base.InitializeDisplayData();
        }

        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        protected override void RefreshDisplayData()
        {
            base.RefreshDisplayData();
        }

        /// <summary>
        /// 画面表示状態更新
        /// </summary>
        protected override void RefreshDisplayState()
        {
            base.RefreshDisplayState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            base.SetDisplayParams();
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            base.GetDisplayParams();
            return true;
        }


        // *******************************************************************
        // イベント
        // *******************************************************************


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

    }
}
