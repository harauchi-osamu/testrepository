using System.Collections.Generic;
using System.Data;
using System.Linq;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;


namespace EntryClass
{
	/// <summary>
	/// イメージ情報
	/// </summary>
	public class ImageInfo
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
					DBConvert.ToStringNull(trimg._GYM_ID) + "_" +
                    DBConvert.ToStringNull(trimg._OPERATION_DATE) + "_" +
                    DBConvert.ToStringNull(trimg._SCAN_TERM) + "_" +
                    DBConvert.ToStringNull(trimg._BAT_ID) + "_" +
                    DBConvert.ToStringNull(trimg._DETAILS_NO) + "_" +
                    DBConvert.ToStringNull(trimg._IMG_KBN);
				return key;
			}
		}

        /// <summary>シーケンス番号（1から）</summary>
        public int Seq { get { return _seq; } }
        public int _GYM_ID { get { return trimg._GYM_ID; } }
		public int _OPERATION_DATE { get { return trimg._OPERATION_DATE; } }
		public string _SCAN_TERM { get { return trimg._SCAN_TERM; } }
        public int _BAT_ID { get { return trimg._BAT_ID; } }
        public int _DETAILS_NO { get { return trimg._DETAILS_NO; } }
        public int _IMG_KBN { get { return trimg._IMG_KBN; } }


        // *******************************************************************
        // トランザクションデータ
        // *******************************************************************

        /// <summary>イメージ（自分）</summary>
        public TBL_TRMEIIMG trimg { get; set; }


        // *******************************************************************
        // トランザクションデータ（親データ）
        // *******************************************************************

        /// <summary>親バッチ</summary>
        public BatchInfo ParentBat { get; private set; }

        /// <summary>親明細</summary>
        public MeisaiInfo ParentMei { get; private set; }


        // *******************************************************************
        // プロパティ
        // *******************************************************************

        public bool IsOmote { get { return ((trimg._IMG_KBN == TrMeiImg.ImgKbn.表) || (trimg._IMG_KBN == TrMeiImg.ImgKbn.表再送分)); } }


        // *******************************************************************
        // メソッド
        // *******************************************************************

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="row"></param>
        public ImageInfo(int seq, DataRow row, BatchInfo parentbat, MeisaiInfo parentmei)
		{
			_seq = seq;
            trimg = new TBL_TRMEIIMG(row, AppInfo.Setting.SchemaBankCD);
            ParentBat = parentbat;
            ParentMei = parentmei;
        }

        /// <summary>
        /// クリアする
        /// </summary>
        public void Dispose()
        {
            trimg = null;
            ParentBat = null;
            ParentMei = null;
        }
    }
}
