using System.Collections.Generic;
using System.Data;
using System.Linq;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace EntryClass
{
	/// <summary>
	/// 明細情報
	/// </summary>
	public class MeisaiInfo
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
					DBConvert.ToStringNull(trmei._GYM_ID) + "_" +
					DBConvert.ToStringNull(trmei._OPERATION_DATE) + "_" +
					DBConvert.ToStringNull(trmei._SCAN_TERM) + "_" +
					DBConvert.ToStringNull(trmei._BAT_ID) + "_" +
					DBConvert.ToStringNull(trmei._DETAILS_NO);
				return key;
			}
		}

        /// <summary>シーケンス番号（1から）</summary>
        public int Seq { get { return _seq; } }
        public int _GYM_ID { get { return trmei._GYM_ID; } }
		public int _OPERATION_DATE { get { return trmei._OPERATION_DATE; } }
		public string _SCAN_TERM { get { return trmei._SCAN_TERM; } }
		public int _BAT_ID { get { return trmei._BAT_ID; } }
        public int _DETAILS_NO { get { return trmei._DETAILS_NO; } }
        public int _DSP_ID { get { return trmei.m_DSP_ID; } }
        public int _HOSEI_ITEMMODE { get; private set; }


        // *******************************************************************
        // トランザクションデータ
        // *******************************************************************

        /// <summary>明細（自分）</summary>
        public TBL_TRMEI trmei { get; set; }

        /// <summary>補正ステータス（HOSEI_STATUS）</summary>
        public TBL_HOSEI_STATUS hosei_status { get; set; }

        /// <summary>明細イメージ（key=Seq, val=ImageInfo）</summary>
        public SortedList<int, ImageInfo> ImageInfos { get; set; }

        /// <summary>項目トランザクション（key=ITEM_ID, val=TBL_TRITEM）</summary>
        public SortedList<int, TBL_TRITEM> tritems { get; set; } = null;
        /// <summary>項目トランザクション（key=ITEM_ID, val=TBL_TRITEM）</summary>
        public SortedList<int, TBL_TRITEM> tritems_org { get; set; } = null;


        // *******************************************************************
        // パラメーター
        // *******************************************************************

        /// <summary>画面パラメータ</summary>
        public DspInfo CurDsp { get; set; }


        // *******************************************************************
        // トランザクションデータ（親データ）
        // *******************************************************************

        /// <summary>親バッチ</summary>
        public BatchInfo ParentBat { get; private set; }


        // *******************************************************************
        // プロパティ
        // *******************************************************************

        /// <summary>イメージ：表示中イメージ区分</summary>
        public int CurImgSeq { get; set; } = 1;
        /// <summary>イメージ：表示中イメージ情報</summary>
        public ImageInfo CurImg
        {
            get
            {
                if (!ImageInfos.ContainsKey(CurImgSeq)) { return null; }
                return ImageInfos[CurImgSeq];
            }
        }

        /// <summary>イメージ：スクロールバー位置X</summary>
        public int ImageScrollPosX { get; set; }
        /// <summary>イメージ：スクロールバー位置Y</summary>
        public int ImageScrollPosY { get; set; }


        // *******************************************************************
        // メソッド
        // *******************************************************************

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="row"></param>
        public MeisaiInfo(int seq, DataRow row, BatchInfo parentbat, int hosei_itemmode)
		{
			_seq = seq;
            _HOSEI_ITEMMODE = hosei_itemmode;
            trmei = new TBL_TRMEI(row, AppInfo.Setting.SchemaBankCD);
            hosei_status = new TBL_HOSEI_STATUS(AppInfo.Setting.SchemaBankCD);
            ImageInfos = new SortedList<int, ImageInfo>();
            CurDsp = new DspInfo(_HOSEI_ITEMMODE);
            tritems = new SortedList<int, TBL_TRITEM>();
            tritems_org = new SortedList<int, TBL_TRITEM>();

            ParentBat = parentbat;
        }

        /// <summary>
        /// クリアする
        /// </summary>
        public void Reset()
        {
            //trmei = null;
            hosei_status = new TBL_HOSEI_STATUS(AppInfo.Setting.SchemaBankCD);
            ImageInfos.Clear();
            ClearItem();
            ClearDsp();
            CurImgSeq = 1;
        }

        /// <summary>
        /// アイテムをクリアする
        /// </summary>
        public void ClearItem()
        {
            tritems.Clear();
            tritems_org.Clear();
        }

        /// <summary>
        /// 画面パラメーターをクリアする
        /// </summary>
        public void ClearDsp()
        {
            CurDsp.Dispose();
            CurDsp = new DspInfo(_HOSEI_ITEMMODE, CurDsp.HasOriginal);
        }

        /// <summary>
        /// 補正ステータスを設定する
        /// </summary>
        /// <param name="row"></param>
        public void SetHoseiStatus(DataRow row)
        {
            hosei_status = new TBL_HOSEI_STATUS(row, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 補正ステータスを設定する
        /// </summary>
        /// <param name="row"></param>
        public void SetHoseiStatus(TBL_HOSEI_STATUS sts)
        {
            // コピーを設定
            hosei_status = sts.ShallowCopy();
        }

        /// <summary>
		/// 明細イメージを追加する
		/// </summary>
		/// <param name="row"></param>
		public void AddImageInfo(ImageInfo img)
        {
            ImageInfos.Add(img.Seq, img);
        }

        /// <summary>
        /// アイテムを追加する
        /// </summary>
        /// <param name="row"></param>
        public void AddTrItem(DataRow row)
        {
            TBL_TRITEM item1 = new TBL_TRITEM(row, AppInfo.Setting.SchemaBankCD);
            TBL_TRITEM item2 = new TBL_TRITEM(row, AppInfo.Setting.SchemaBankCD);
            tritems.Add(item1._ITEM_ID, item1);
            tritems_org.Add(item2._ITEM_ID, item2);
        }

        /// <summary>
        /// 明細イメージを取得する
        /// </summary>
        /// <param name="imgKbn"></param>
        /// <returns></returns>
        public ImageInfo GetImageInfo(int imgKbn)
        {
            if (ImageInfos.Count < 1) { return null; }
            var imgs = ImageInfos.Values.Where(p => p._IMG_KBN == imgKbn);
            if (imgs.Count() < 1) { return null; }
            return imgs.First();
        }

        /// <summary>
        /// 明細を更新する
        /// </summary>
        /// <param name="row"></param>
        public void UpdateTrMei(DataRow row)
        {
            trmei = null;
            trmei = new TBL_TRMEI(row, AppInfo.Setting.SchemaBankCD);
        }

        /// <summary>
        /// 次のイメージを表示する
        /// </summary>
        public void SetNextImageSeq()
        {
            int mexSeq = ImageInfos.Keys.Max();
            if ((CurImgSeq + 1) > mexSeq)
            {
                CurImgSeq = 1;
            }
            else
            {
                CurImgSeq++;
            }
        }
    }
}
