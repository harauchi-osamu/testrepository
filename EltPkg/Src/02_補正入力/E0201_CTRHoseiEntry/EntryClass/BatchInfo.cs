using System.Collections.Generic;
using System.Data;
using System.Linq;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace EntryClass
{
	/// <summary>
	/// バッチ情報
	/// </summary>
	public class BatchInfo
	{
		private int _seq = 0;

        // *******************************************************************
        // キー項目
        // *******************************************************************

        public string Key
		{
			get
			{
				string key =
					DBConvert.ToStringNull(trbatch._GYM_ID) + "_" +
                    DBConvert.ToStringNull(trbatch._OPERATION_DATE) + "_" +
                    DBConvert.ToStringNull(trbatch._SCAN_TERM) + "_" +
                    DBConvert.ToStringNull(trbatch._BAT_ID);
				return key;
			}
		}

        /// <summary>シーケンス番号（1から）</summary>
        public int Seq { get { return _seq; } }
        public int _GYM_ID { get { return trbatch._GYM_ID; } }
		public int _OPERATION_DATE { get { return trbatch._OPERATION_DATE; } }
		public string _SCAN_TERM { get { return trbatch._SCAN_TERM; } }
		public int _BAT_ID { get { return trbatch._BAT_ID; } }


        // *******************************************************************
        // トランザクションデータ
        // *******************************************************************

        /// <summary>バッチ（自分）</summary>
        public TBL_TRBATCH trbatch { get; private set; }

        /// <summary>
        /// 明細情報（key=Seq, val=MeisaiInfo）
        /// </summary>
        /// <remarks>
        /// 対象明細のみ保持
        /// バッチ配下の明細すべてを保持していたが使用しないため対象明細のみ保持
        /// </remarks>
        public KeyValuePair<int, MeisaiInfo> CurMeisaiInfo { get; set; }
        ///// <summary>イメージ（key=Seq, val=MeisaiInfo）</summary>
        //public SortedList<int, MeisaiInfo> MeisaiInfos { get; set; }

        // *******************************************************************
        // プロパティ
        // *******************************************************************

        /// <summary>対象の明細番号</summary>
        private int _curDetailsNo = 0;
        public int _DETAILS_NO
        {
            get { return _curDetailsNo; }
        }

        // *******************************************************************
        // 制御用データ
        // *******************************************************************

        ///// <summary>表示中のイメージ番号（デフォルト：先頭イメージ）</summary>
        //private int _curSeq = 1;
        //public int CurMeiSeq
        //{
        //	get { return _curSeq; }
        //	set { _curSeq = value; }
        //}

        /// <summary>表示中の明細情報</summary>
        public MeisaiInfo CurMei
        {
            get
            {
                return CurMeisaiInfo.Value;
                //if (MeisaiInfos.ContainsKey(CurMeiSeq))
                //{
                //    return MeisaiInfos[CurMeiSeq];
                //}
                //return null;
            }
        }

        ///// <summary>表示中の最後のイメージ番号</summary>
        //public int LastMeiSeq
        //{
        //    get
        //    {
        //        int max = 0;
        //        foreach (int seq in MeisaiInfos.Keys) { if (max < seq) { max = seq; } }
        //        return max;
        //    }
        //}

        ///// <summary>最初の明細かどうか</summary>
        //public bool IsFirstMeisai { get { return (CurMei.Seq == 1); } }

        ///// <summary>最後の明細かどうか</summary>
        //public bool IsLastMeisai { get { return (CurMei.Seq == LastMeiSeq); } }


        // *******************************************************************
        // メソッド
        // *******************************************************************

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="row"></param>
        public BatchInfo(int seq, DataRow row, int DetailsNo)
		{
			_seq = seq;
            _curDetailsNo = DetailsNo;
            trbatch = new TBL_TRBATCH(row, AppInfo.Setting.SchemaBankCD);
            CurMeisaiInfo = new KeyValuePair<int, MeisaiInfo>();
            //MeisaiInfos = new SortedList<int, MeisaiInfo>();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="row"></param>
        public BatchInfo(int seq, TBL_TRBATCH bat, int DetailsNo)
        {
            _seq = seq;
            _curDetailsNo = DetailsNo;
            trbatch = bat;
            CurMeisaiInfo = new KeyValuePair<int, MeisaiInfo>();
            //MeisaiInfos = new SortedList<int, MeisaiInfo>();
        }

        /// <summary>
        /// 明細をクリアする
        /// </summary>
        public void Reset()
        {
            if(CurMeisaiInfo.Value == null)
            {
                return;
            }
            CurMeisaiInfo.Value.Reset();

            ////trbatch = null;
            //foreach (MeisaiInfo mei in MeisaiInfos.Values)
            //{
            //    mei.Reset();
            //}
            //MeisaiInfos.Clear();
        }

        /// <summary>
        /// 明細を追加する
        /// </summary>
        /// <param name="row"></param>
        public void AddMeisaiInfo(MeisaiInfo mei)
        {
            CurMeisaiInfo = new KeyValuePair<int, MeisaiInfo>(mei.Seq, mei);
            //MeisaiInfos.Add(mei.Seq, mei);
        }
    }
}
