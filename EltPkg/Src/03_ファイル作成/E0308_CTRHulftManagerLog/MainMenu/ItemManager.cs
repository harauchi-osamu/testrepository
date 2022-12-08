using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;

namespace MainMenu
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        /// <summary>画面パラメータ</summary>
        public DisplayParams DispParams { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.DispParams = new DisplayParams();
            this.DispParams.Clear();
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            /// <summary>HULFT 処理結果</summary>
            public int Result { get; set; }
            /// <summary>HULFT エラーメッセージ</summary>
            public string ErrMsg { get; set; }

            public void Clear()
            {
                this.Result = -1;
                this.ErrMsg = "";
            }
        }
    }
}
