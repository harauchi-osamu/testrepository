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

namespace HulftIO
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
		private MasterManager _masterMgr = null;

		/// <summary>画面パラメータ</summary>
		public DisplayParams DispParams { get; set; }

        /// <summary>伝送情報</summary>
        public TransParams TransInfo { get; set; }


		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ItemManager(MasterManager mst)
        {
			_masterMgr = mst;
            this.DispParams = new DisplayParams();
			this.DispParams.Clear();
            this.TransInfo = new TransParams();
		}


        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class DisplayParams
        {
            public bool IsAutoExec { get; set; }
            public int TransType { get; set; }
            public string SendDirPath { get; set; }
            public string RecvDirPath { get; set; }
            public string FileId { get; set; }
            public string FileName { get; set; }
            public string SHulftID { get; set; }
            public string RHulftID { get; set; }
            public long RecordSize { get; set; }
            public string CreateDate { get; set; }
            public string CreateTime { get; set; }
            public int RecordCount { get; set; }
            public string ResultMsg { get; set; }

            public void Clear()
			{
                this.TransType = 0;
                this.IsAutoExec = false;
                this.SendDirPath = "";
                this.RecvDirPath = "";
                this.FileId = "";
                this.FileName = "";
                this.SHulftID = "";
                this.RHulftID = "";
                this.RecordSize = 0;
                this.CreateDate = "";
                this.CreateTime = "";
                this.RecordCount = 0;
                this.ResultMsg = "";
            }
        }

        /// <summary>
        /// 伝送情報
        /// </summary>
        public class TransParams
        {
            public const int 集信 = 0;
            public const int 配信 = 1;
            public const int RESULT_SUCCESS = 0;
			public const int RESULT_SKIP = 1;
			public const int RESULT_FILE_NOT = -1;
            public const int RESULT_HULFT_ERR = -2;

            public string SHulftID { get; set; } = "";
            public string RHulftID { get; set; } = "";
            public string FileName { get; set; } = "";
            public string SendDirPath { get; set; }
            public string RecvDirPath { get; set; }
            public string ErrMsg { get; set; } = "";

            public void Clear()
            {
                this.SHulftID = "";
                this.RHulftID = "";
                this.FileName = "";
                this.SendDirPath = "";
                this.RecvDirPath = "";
                this.ErrMsg = "";
            }
        }

	}
}
