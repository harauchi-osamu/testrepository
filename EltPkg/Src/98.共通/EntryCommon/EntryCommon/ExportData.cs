using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommonTable.DB;
using Common;
using EntryCommon;

namespace EntryCommon
{
	/// <summary>
	/// 外部出力データ
	/// </summary>
	public class ExportData
	{
		/// <summary>ファイルパス</summary>
		public string FilePath { get; private set; }

		/// <summary>ファイルのバイナリデータ</summary>
		public byte[] Bin { get; private set; }

		/// <summary>文字エンコーディング（SJIS）</summary>
		private Encoding _encSJIS;

        /// <summary>文字エンコーディング（IBM）</summary>
        private Encoding _encIBM;

        /// <summary>
        /// 画面項目定義リスト（ｎ画面分 dspitems）（key=DSP_ID, val=dspitems）
        ///  + dspitems（１画面分 DSP_ITEM）（key=ITEM_ID, val=TBL_DSP_ITEM）
        ///  + dspitems（１画面分 DSP_ITEM）（key=ITEM_ID, val=TBL_DSP_ITEM）
        /// </summary>
        public SortedList<int, SortedList<int, TBL_DSP_ITEM>> dspdatas { get; set; }

        /// <summary>外部出力データ（全レコード分の出力データ）（key=行番号, val=ExpDataRecord）</summary>
        public SortedList<int, ExpDataRecord> expdatas { get; private set; }

		/// <summary>IBMコード変換するかどうか</summary>
		public bool IsCONV { get; set; } = false;

		/// <summary>行の区切りに改行コードを出力するかどうか</summary>
		public bool IsCRLF { get; set; } = false;

		/// <summary>定数：改行コード</summary>
		private const string CRLF = "\r\n";


		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ExportData(string filePath)
		{
			FilePath = filePath;
            Bin = new byte[0];
            dspdatas = new SortedList<int, SortedList<int, TBL_DSP_ITEM>>();
			expdatas = new SortedList<int, ExpDataRecord>();
			_encSJIS = Encoding.GetEncoding("Shift_JIS");
            _encIBM = Encoding.GetEncoding("IBM290");
        }

        /// <summary>
        /// 画面項目定義リストを生成する
        /// （key=POS, val=TBL_DSP_ITEM）
        /// </summary>
        /// <returns></returns>
        public SortedList<int, TBL_DSP_ITEM> GetNewDspItems()
		{
			return new SortedList<int, TBL_DSP_ITEM>();
		}

        /// <summary>
        /// DSP_ID と ITEM_ID に合致する DSP_ITEM を取得する
        /// </summary>
        /// <param name="dspid"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public TBL_DSP_ITEM GetDspItem(int dspid, int itemid)
        {
            if (!dspdatas.ContainsKey(dspid)) { return null; }

            // ITEM_ID と合致するものを取得（注：キーは POS なので地道に探す）
            SortedList<int, TBL_DSP_ITEM> dspitems = dspdatas[dspid];
            foreach (TBL_DSP_ITEM di in dspitems.Values)
            {
                if(di._ITEM_ID == itemid) { return di; }
            }
            return null;
        }

        /// <summary>
        /// 画面項目定義リストを登録する
        /// </summary>
        /// <param name="dspid"></param>
        /// <param name="dspitems"></param>
        public void AddDspItems(int dspid, SortedList<int, TBL_DSP_ITEM> dspitems)
		{
			if (dspdatas.ContainsKey(dspid))
			{
				return;
			}
			dspdatas.Add(dspid, dspitems);
		}

		/// <summary>
		/// 定義レコードを取得する
		/// </summary>
		/// <returns></returns>
		public ExpDataRecord GetNewExpDataRecord(int dspid)
		{
			if (!dspdatas.ContainsKey(dspid))
			{
				return null;
			}
			SortedList<int, TBL_DSP_ITEM> dspitems = dspdatas[dspid];
			return new ExpDataRecord(dspitems, _encSJIS);
		}

        /// <summary>
        /// 定義レコードを取得する
        /// </summary>
        /// <returns></returns>
        public ExpDataRecord GetNewExpDataRecord(int dspid, int recSize)
        {
            if (!dspdatas.ContainsKey(dspid))
            {
                return null;
            }

            // 改行コードはレコードサイズに含まれていないので加算する
            recSize += IsCRLF ? CRLF.Length : 0;

            SortedList<int, TBL_DSP_ITEM> dspitems = dspdatas[dspid];
            return new ExpDataRecord(dspitems, _encSJIS, recSize);
        }

        /// <summary>
        /// 外部出力データを登録する
        /// </summary>
        /// <param name="rec"></param>
        public void AddExpDataRecord(ExpDataRecord rec)
		{
			int seq = expdatas.Count + 1;
			expdatas.Add(seq, rec);
		}

        /// <summary>
        /// ファイルを読み込んでデータに流し込む
        /// </summary>
        public bool ReadBinaryStream(int dspid)
        {
            int cnt = 0;
            int pos = 0;
            try
            {
                // ファイルを読み込んでバイナリ化
                Bin = CommonUtil.ReadBinaryStream(FilePath);

                // バイナリデータを型に流し込む
                while (pos < Bin.Length)
                {
                    ExpDataRecord rec = GetNewExpDataRecord(dspid);
                    foreach (ExpDataRecord.DataItem item in rec.Items.Values)
                    {
                        for (int i = 0; i < item.Bin.Length; i++)
                        {
                            item.Bin[i] = this.Bin[pos];
                            pos++;
                        }
                        item.SetStringValue();
                    }
                    expdatas.Add(cnt++, rec);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("ファイル形式が不正です。[rec={0},pos={1},path='{2}']", cnt, pos, FilePath);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), msg, 3);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ファイルを読み込んでデータに流し込む
        /// </summary>
        public bool ReadBinaryStream(int dspid, int endId, string endVal)
		{
            int cnt = 0;
            int pos = 0;
            try
            {
                // ファイルを読み込んでバイナリ化
                Bin = CommonUtil.ReadBinaryStream(FilePath);

                // バイナリデータを型に流し込む
                while (pos < Bin.Length)
                {
                    ExpDataRecord rec = GetNewExpDataRecord(dspid);
                    foreach (ExpDataRecord.DataItem item in rec.Items.Values)
                    {
                        for (int i = 0; i < item.Bin.Length; i++)
                        {
                            item.Bin[i] = this.Bin[pos];
                            pos++;
                        }
                        item.SetStringValue();
                     }
                    expdatas.Add(cnt++, rec);

                    // 指定したアイテムが指定した値になったら読込終了
                    if (rec.Items[endId].Value.Equals(endVal))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("ファイル形式が不正です。[rec={0},pos={1},path='{2}']", cnt, pos, FilePath);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), msg, 3);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
		}

        /// <summary>
        /// ファイルを1行ずつ（CRLF）読み込んでデータに流し込む
        /// </summary>
        public bool ReadBinaryStreamLine(int dspid, int recSize)
        {
            int cnt = 0;
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, _encSJIS))
                {
                    while (!sr.EndOfStream)
                    {
                        // 1行ずつ読み込む
                        int pos = 0;
                        string line = sr.ReadLine();
                        byte[] bin = CommonUtil.ConvStringToBinary(_encSJIS, line);

                        // 必要桁数に満たないレコードは切り捨て
                        if (bin.Length < recSize) { continue; }

                        ExpDataRecord rec = GetNewExpDataRecord(dspid, recSize);
                        foreach (ExpDataRecord.DataItem item in rec.Items.Values)
                        {
                            for (int i = 0; i < item.Bin.Length; i++)
                            {
                                item.Bin[i] = bin[pos];
                                pos++;
                            }
                            item.SetStringValue();
                        }
                        expdatas.Add(cnt++, rec);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("ファイル形式が不正です。[rec={0},path='{1}']", cnt, FilePath);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), msg, 3);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 外部連携データをファイル出力する
        /// </summary>
        public void WriteBinaryStream()
		{
			//SortedList<int, byte[]> binList = new SortedList<int, byte[]>();
			//int recCnt = 0;
			//int seq = 1;
			//try
			//{
			//	// 行数分繰り返し
			//	foreach (ExpDataRecord rec in expdatas.Values)
			//	{
			//		recCnt++;

   //                 // レコードサイズ半角空白パディング
   //                 string space = CommonUtil.BPadLeft("", _encSJIS, rec.RecSize, " ");
   //                 rec.RecBin = CommonUtil.ConvStringToBinary(_encSJIS, space);

   //                 // 項目数分繰り返し（並び順は ITEM_ID でソート済み）
   //                 foreach (ExpDataRecord.DataItem item in rec.Items.Values)
   //                 {
   //                     // バイナリに変換
   //                     if (item.ITEM_SUBRTN.Equals(SubRoutine.PCK))
   //                     {
   //                         // PCK 優先
   //                         item.Bin = ConvToPck(item.Value);
   //                     }
   //                     else
   //                     {
   //                         // SJIS
   //                         item.Bin = CommonUtil.ConvStringToBinary(_encSJIS, item.Value);
   //                     }

   //                     // POS 位置にデータを差し込む
   //                     int bidx = 0;
   //                     int edPos = item.POS + item.Bin.Length;
   //                     for (int i = item.POS; i < edPos; i++)
   //                     {
   //                         rec.RecBin[i] = item.Bin[bidx++];
   //                     }
   //                 }

			//		// 改行
			//		if (IsCRLF)
   //                 {
   //                     int pos = rec.RecSize - CRLF.Length;
   //                     byte[] bCrlf = CommonUtil.ConvStringToBinary(_encSJIS, CRLF);
   //                     int bidx = 0;
   //                     int edPos = pos + CRLF.Length;
   //                     for (int i = pos; i < edPos; i++)
   //                     {
   //                         rec.RecBin[i] = bCrlf[bidx++];
   //                     }
   //                 }
   //             }
			//}
			//catch (Exception ex)
			//{
			//	string msg = string.Format("ファイル作成中にエラーが発生しました。[cnt={0},seq={1},path='{2}']", recCnt, seq, FilePath);
			//	LogWriter.writeLog(MethodBase.GetCurrentMethod(), msg, 3);
			//	throw ex;
			//}

			//// バイナリをファイル出力する
			//File.Delete(FilePath);
   //         foreach (ExpDataRecord rec in expdatas.Values)
   //         {
   //             CommonUtil.AppendBinaryStream(FilePath, rec.RecBin);
   //         }
		}

		/// <summary>
		/// パックデータに変換する
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		private byte[] ConvToPck(string val)
		{
            int len = ((val.Length + 1) / 2);
			byte[] retBin = new byte[len];
			byte[] bufBin = new byte[val.Length + 1];
			for (int i = 0; i < val.Length; i++)
			{
				if ((i % 2) == 1)
				{
                    // 下位ビット
					bufBin[i] = (byte)(val[i] & 0xF);
				}
				else if ((i % 2) == 0)
				{
                    // 上位ビット
					bufBin[i] = (byte)((val[i] & 0xF) << 4);
				}
			}

			// 符号ビット
			bufBin[val.Length] = 0xC;

			int i_Pac = 0;
			for (int i = 0; i < val.Length; i++)
			{
				retBin[i_Pac] = (byte)(bufBin[i] | bufBin[i + 1]);
				i_Pac++;
				i++;
			}
			return retBin;
		}

        /// <summary>
        /// 外部出力データ（１レコード分）
        /// </summary>
        public class ExpDataRecord
		{
			/// <summary>データ項目リスト（key=ITEM_ID, val=DataItem）</summary>
			public SortedList<int, DataItem> Items { get; set; }

            /// <summary>文字エンコーディング</summary>
            private Encoding _enc;

            /// <summary>レコードサイズ</summary>
            public int RecSize { get; private set; }

            public byte[] RecBin { get; set; } = new byte[0];

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="dspitems"></param>
            public ExpDataRecord(SortedList<int, TBL_DSP_ITEM> dspitems, Encoding enc, int recSize = 0)
			{
				Items = new SortedList<int, DataItem>();
				_enc = enc;
                RecSize = recSize;
                foreach (TBL_DSP_ITEM item in dspitems.Values)
				{
					Items.Add(item._ITEM_ID, 
						new DataItem(
							item._ITEM_ID,
							item.m_ITEM_DISPNAME,
							item.m_ITEM_TYPE,
							item.m_ITEM_LEN,
							item.m_POS,
							item.m_ITEM_SUBRTN,
							enc));
				}

                RecBin = new byte[RecSize];
            }

			/// <summary>
			/// データ項目
			/// </summary>
			public class DataItem
			{
				public int ITEM_ID { get; set; } = -1;
				public string ITEM_NAME { get; set; } = "";
				public string ITEM_TYPE { get; set; } = "";
				public int ITEM_LEN { get; set; } = 0;
                public int POS { get; set; } = 0;
                public string ITEM_SUBRTN { get; set; } = "";
				public string Value { get; set; } = "";
				public byte[] Bin { get; set; } = new byte[0];

				/// <summary>文字エンコーディング</summary>
				private Encoding _enc;

				/// <summary>
				/// コンストラクタ
				/// </summary>
				/// <param name="itemId"></param>
				/// <param name="name"></param>
				/// <param name="type"></param>
				/// <param name="len"></param>
				/// <param name="pos"></param>
				/// <param name="sub"></param>
				public DataItem(int itemId, string name, string type, int len, int pos, string sub, Encoding enc)
				{
					this.ITEM_ID = itemId;
					this.ITEM_NAME = name;
					this.ITEM_TYPE = type;
					this.ITEM_LEN = len;
					this.POS = pos;
                    this.ITEM_SUBRTN = sub;
					_enc = enc;
					this.Bin = new byte[len];
				}

				/// <summary>
				/// データを設定する
				/// </summary>
				public void SetStringValue()
				{
					this.Value = CommonUtil.ConvBinaryToString(_enc, Bin);
				}

			}
		}

	}
}
