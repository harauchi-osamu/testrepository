using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using Common;
using CommonClass.DB;
using CommonTable.DB;
using System.Linq;

namespace EntryCommon
{
	public class CommonUtil
	{
		private static Encoding _enc;

		private CommonUtil() { }

		static CommonUtil()
		{
			_enc = Encoding.GetEncoding("Shift_JIS");
		}


		/*******************************************************************
		 * 公開メソッド（日付操作）
		 *******************************************************************/

		/// <summary>
		/// ｎ日後(前)の日付を取得する
		/// </summary>
		/// <param name="yyyymmdd"></param>
		/// <param name="diffdays"></param>
		/// <param name="isAdd">true：日数加算、false：日数減算</param>
		/// <returns></returns>
		public static int GetAddDate(int yyyymmdd, int diffdays, bool isAdd)
		{
			string sYYYYMMDD = yyyymmdd.ToString();
			sYYYYMMDD = sYYYYMMDD.Substring(0, 4) + "/" + sYYYYMMDD.Substring(4, 2) + "/" + sYYYYMMDD.Substring(6, 2);
			string sDate = ConvToDateFormat(yyyymmdd);

			DateTime dYYYYMMDD;
			if (!DateTime.TryParse(sDate, out dYYYYMMDD))
			{
				throw new ApplicationException(string.Format("日付形式が不正です。[{0}]", sYYYYMMDD));
			}

			diffdays *= isAdd ? 1 : -1;
			return DBConvert.ToIntNull(dYYYYMMDD.AddDays(diffdays).ToString("yyyyMMdd"));
		}

		/// <summary>
		/// ｎヵ月後(前)の日付を取得する
		/// </summary>
		/// <param name="yyyymmdd"></param>
		/// <param name="diffMonth"></param>
		/// <param name="isAdd">true：日数加算、false：日数減算</param>
		/// <returns></returns>
		public static int GetAddMonth(int yyyymmdd, int diffMonth, bool isAdd)
		{
			string sYYYYMMDD = yyyymmdd.ToString();
			sYYYYMMDD = sYYYYMMDD.Substring(0, 4) + "/" + sYYYYMMDD.Substring(4, 2) + "/" + sYYYYMMDD.Substring(6, 2);
			string sDate = ConvToDateFormat(yyyymmdd);

			DateTime dYYYYMMDD;
			if (!DateTime.TryParse(sDate, out dYYYYMMDD))
			{
				throw new ApplicationException(string.Format("日付形式が不正です。[{0}]", sYYYYMMDD));
			}

			diffMonth *= isAdd ? 1 : -1;
			return DBConvert.ToIntNull(dYYYYMMDD.AddMonths(diffMonth).ToString("yyyyMMdd"));
		}

		/// <summary>
		/// 月初の日付を取得する
		/// </summary>
		/// <returns>yyyymmdd（引数の月初日付）</returns>
		/// <returns></returns>
		public static int GetFirstDay(int yyyymm)
		{
			string sYYYYMMDD = yyyymm.ToString() + "01";
			return DBConvert.ToIntNull(sYYYYMMDD);
		}

        // MODSTART 2022/07/08 銀行導入工程_不具合管理表No149 対応
        /// <summary>
        /// 月末の日付を取得する
        /// </summary>
        /// <param name="yyyymm"></param>
        /// <returns>yyyymmdd（引数の月末日付）</returns>
        public static int GetLastDay(int yyyymm)
        {
            if (yyyymm.ToString().Length < 6) { return yyyymm; }

            // 翌月にしてから－１日を当月末とする
            int iMM = DBConvert.ToIntNull(yyyymm.ToString().Substring(4, 2));
            string sYYYY = yyyymm.ToString().Substring(0, 4);
            int iYYYY = int.Parse(sYYYY);
            if (iMM == 12)
            {
                iMM = 1;
                iYYYY += 1;
            }
            else
            {
                iMM++;
            }

            string sYYYYMMDD = iYYYY.ToString("D4") + iMM.ToString("D2") + "01";
            return GetAddDate(DBConvert.ToIntNull(sYYYYMMDD), 1, false);
        }
        // MODEND

        /// <summary>
        /// 処理日を取得する（yyyyMMddHHmmss）
        /// </summary>
        /// <param name="opedate"></param>
        /// <returns></returns>
        public static string GetOpeDateTime(int opedate)
		{
			return opedate.ToString() + DateTime.Now.ToString("HHmmss");
		}


		/*******************************************************************
		 * 公開メソッド（フォーマット）
		 *******************************************************************/

		/// <summary>
		/// yyyy/MM/dd HH:mm:ss を yyyyMMddHHmmss に変換する
		/// </summary>
		/// <param name="datetime"></param>
		/// <returns></returns>
		public static int ConvDateToInt(string datetime)
		{
			if (string.IsNullOrEmpty(datetime)) { return 0; }
			string sDate = datetime.Replace("/", "").Replace(":", "");
			int nDate;
			if (!Int32.TryParse(sDate, out nDate)) { return 0; }
			return nDate;
		}

		/// <summary>
		/// yyyyMMdd を yyyy/MM/dd に変換する
		/// </summary>
		/// <param name="yyyyMMdd"></param>
		/// <returns></returns>
		public static string ConvToDateFormat(int yyyyMMdd, int type = 1)
		{
			string sYYYYMMDD = yyyyMMdd.ToString();
			return ConvToDateFormat(sYYYYMMDD, type);
		}

		/// <summary>
		/// yyyyMMdd を yyyy/MM/dd に変換する
		/// </summary>
		/// <param name="yyyyMMdd"></param>
		/// <returns></returns>
		public static string ConvToDateFormat(string yyyyMMdd, int type = 1)
		{
			if (string.IsNullOrEmpty(yyyyMMdd) || yyyyMMdd.Length < 8)
			{
				return yyyyMMdd;
			}
			string sYYYYMMDD = yyyyMMdd;
			switch (type)
			{
				case 1:
					sYYYYMMDD = sYYYYMMDD.Substring(0, 4) + "/" + sYYYYMMDD.Substring(4, 2) + "/" + sYYYYMMDD.Substring(6, 2);
					break;
				case 2:
					sYYYYMMDD = sYYYYMMDD.Substring(0, 4) + "年" + sYYYYMMDD.Substring(4, 2) + "月" + sYYYYMMDD.Substring(6, 2) + "日";
					break;
				case 3:
					sYYYYMMDD = sYYYYMMDD.Substring(0, 4) + "." + sYYYYMMDD.Substring(4, 2) + "." + sYYYYMMDD.Substring(6, 2);
					break;
                case 4:
                    sYYYYMMDD = sYYYYMMDD.Substring(0, 4) + "-" + sYYYYMMDD.Substring(4, 2) + "-" + sYYYYMMDD.Substring(6, 2);
                    break;
                default:
					break;
			}
			return sYYYYMMDD;
		}

        /// <summary>
        /// yyyyMMddHHmmss を yyyy/MM/dd HH:mm:ss に変換する
        /// </summary>
        /// <param name="yyyyMMddHHmmss"></param>
        /// <returns></returns>
        public static string ConvToDateTimeFormat(int yyyyMMdd, int HHmmss, int type = 1)
        {
            if ((yyyyMMdd == 0) || (HHmmss == 0)) { return ""; }
            string sYYYYMMDD = yyyyMMdd.ToString();
            string sHHMMSS = HHmmss.ToString();
            return ConvToDateFormat(yyyyMMdd) + " " + ConvToTimeFormat(HHmmss);
        }

        /// <summary>
        /// yyyyMMddHHmmss を yyyy/MM/dd HH:mm:ss に変換する
        /// </summary>
        /// <param name="yyyyMMddHHmmss"></param>
        /// <returns></returns>
        public static string ConvToDateTimeFormat(long yyyyMMddHHmmss, int type = 1)
		{
			string sYYYYMMDDHHMMSS = yyyyMMddHHmmss.ToString();
			return ConvToDateTimeFormat(sYYYYMMDDHHMMSS, type);
		}

		/// <summary>
		/// yyyyMMddHHmmss を yyyy/MM/dd HH:mm:ss に変換する
		/// </summary>
		/// <param name="yyyyMMddHHmmss"></param>
		/// <returns></returns>
		public static string ConvToDateTimeFormat(string yyyyMMddHHmmss, int type = 1)
		{
			if (string.IsNullOrEmpty(yyyyMMddHHmmss) || yyyyMMddHHmmss.Length < 14)
			{
				return yyyyMMddHHmmss;
			}
			string sYYYYMMDDHHMMSS = yyyyMMddHHmmss;
			switch (type)
			{
				case 1:
					sYYYYMMDDHHMMSS =
						sYYYYMMDDHHMMSS.Substring(0, 4) + "/" + sYYYYMMDDHHMMSS.Substring(4, 2) + "/" + sYYYYMMDDHHMMSS.Substring(6, 2) + " " +
						sYYYYMMDDHHMMSS.Substring(8, 2) + ":" + sYYYYMMDDHHMMSS.Substring(10, 2) + ":" + sYYYYMMDDHHMMSS.Substring(12, 2);
					break;
				default:
					break;
			}
			return sYYYYMMDDHHMMSS;
		}

        /// <summary>
        /// yyyyMMdd HHmmssfff を yyyy/MM/dd HH:mm:ss に変換する
        /// </summary>
        /// <param name="yyyyMMdd"></param>
        /// <param name="HHmmssfff"></param>
        /// <returns></returns>
        public static string ConvDateMiliTimeToDateTimeFormat(int yyyyMMdd, int HHmmssfff)
        {
            return ConvToDateTimeFormat(yyyyMMdd, HHmmssfff / 1000);
        }

        /// <summary>
        /// HHmmss を HH:mm:ss に変換する
        /// </summary>
        /// <param name="HHmmss"></param>
        /// <returns></returns>
        public static string ConvToTimeFormat(int HHmmss)
        {
            string sHHmmss = HHmmss.ToString("D6");
            return ConvToTimeFormat(sHHmmss);
        }

        /// <summary>
        /// HHmmss を HH:mm:ss に変換する
        /// </summary>
        /// <param name="HHmmss"></param>
        /// <returns></returns>
        public static string ConvToTimeFormat(string HHmmss)
		{
			if (string.IsNullOrEmpty(HHmmss) || HHmmss.Length < 6)
			{
				return HHmmss;
			}
			return HHmmss.Substring(0, 2) + ":" + HHmmss.Substring(2, 2) + ":" + HHmmss.Substring(4, 2);
		}

        /// <summary>
        /// HHmmssfff を HH:mm:ss に変換する
        /// </summary>
        /// <param name="HHmmssfff"></param>
        /// <returns></returns>
        public static string ConvMiliTimeToTimeFormat(int HHmmssfff)
        {
            string sHHmmssfff = HHmmssfff.ToString("D9");
            return ConvToTimeFormat(sHHmmssfff);
        }

        /// <summary>
        /// HHmmssfff を HH:mm:ss に変換する
        /// </summary>
        /// <param name="HHmmssfff"></param>
        /// <returns></returns>
        public static string ConvMiliTimeToTimeFormat(string HHmmssfff)
        {
            if (string.IsNullOrEmpty(HHmmssfff))
            {
                return HHmmssfff;
            }

            if (HHmmssfff.Length < 9)
            {
                HHmmssfff = HHmmssfff.ToString().PadLeft(9, '0');
            }
            return ConvToTimeFormat(HHmmssfff);
        }

        /// <summary>
        /// HHmmssfff を HH:mm:ss.fff に変換する
        /// </summary>
        /// <param name="HHmmssfff"></param>
        /// <returns></returns>
        public static string ConvToMiliTimeFormat(string HHmmssfff)
        {
            if (string.IsNullOrEmpty(HHmmssfff))
            {
                 return HHmmssfff;
            }

            if (HHmmssfff.Length < 9)
            {
                HHmmssfff = HHmmssfff.ToString().PadLeft(9, '0');
            }
            return HHmmssfff.Substring(0, 2) + ":" + HHmmssfff.Substring(2, 2) + ":" + HHmmssfff.Substring(4, 2) + "." + HHmmssfff.Substring(6, 3);
        }

        /// <summary>
        /// ミリ秒の値 を HH:mm:ss.fff 形式に変換する
        /// </summary>
        /// <param name="sss"></param>
        /// <returns></returns>
        public static string ConvSecondToMiliTimeFormat(string sss)
        {
            return ConvSecondToMiliTimeFormat(sss, "hh':'mm':'ss'.'fff");
        }

        /// <summary>
        /// ミリ秒の値 を 指定形式に変換する
        /// 出力フォーマット指定
        /// </summary>
        /// <param name="sss"></param>
        /// <param name="Format"></param>
        /// <returns></returns>
        public static string ConvSecondToMiliTimeFormat(string sss, string Format)
        {
            if (string.IsNullOrEmpty(sss) || !int.TryParse(sss, out int intsss))
            {
                return sss;
            }

            return new TimeSpan(0, 0, 0, intsss / 1000, intsss % 1000).ToString(Format);
        }

        /// <summary>
        /// yyyyMMddHHmmss を HH:mm:ss に変換する
        /// </summary>
        /// <param name="HHmmss"></param>
		/// <returns></returns>
		public static string GetTimeFormat(string yyyyMMddHHmmss)
		{
			if (string.IsNullOrEmpty(yyyyMMddHHmmss) || yyyyMMddHHmmss.Length < 14)
			{
				return yyyyMMddHHmmss;
			}
			string HHmmss = yyyyMMddHHmmss.Substring(8, 6);
			return ConvToTimeFormat(HHmmss);
		}

		/// <summary>
		/// yyyyMMdd（文字列）をyyyyMMdd（数値）に変換する
		/// </summary>
		/// <param name="yyyyMMdd"></param>
		/// <returns></returns>
		public static int ToIntDate(string yyyyMMdd)
		{
			if (string.IsNullOrEmpty(yyyyMMdd)) { return 0; }
			string date = yyyyMMdd.Replace("/", "");
			return DBConvert.ToIntNull(date);
		}

		/// <summary>
		/// HHMM 形式かどうかチェックする
		/// </summary>
		/// <param name="time"></param>
		/// <returns>OK：true, NG：false</returns>
		public static bool CheckTimeFormat(string time)
		{
			if(time.Trim().Length < 4)
			{
				return false;
			}
			int HH = DBConvert.ToIntNull(time.Substring(0, 2));
			if ((HH < 0) || (23 < HH))
			{
				return false;
			}
			int MM = DBConvert.ToIntNull(time.Substring(2, 2));
			if ((MM < 0) || (59 < MM))
			{
				return false;
			}
			return true;
		}

        /// <summary>
        /// 指定した日付に日数を加算する
        /// </summary>
        /// <param name="yyyyMMdd"></param>
        /// <param name="addday"></param>
        /// <returns></returns>
        public static int AddDate(int yyyyMMdd, int addday)
        {
            string sYYYYMMDD = ConvToDateFormat(yyyyMMdd);
            DateTime dt = DateTime.Parse(sYYYYMMDD).AddDays(addday);
            return DBConvert.ToIntNull(dt.ToString("yyyyMMdd"));
        }

        /// <summary>
        /// 指定した日付に月数を加算する
        /// </summary>
        /// <param name="yyyyMMdd"></param>
        /// <param name="addmonth"></param>
        /// <returns></returns>
        public static int AddMonth(int yyyyMMdd, int addmonth)
        {
            string sYYYYMMDD = ConvToDateFormat(yyyyMMdd);
            DateTime dt = DateTime.Parse(sYYYYMMDD).AddMonths(addmonth);
            return DBConvert.ToIntNull(dt.ToString("yyyyMMdd"));
        }


        /*******************************************************************
		 * 公開メソッド（文字列操作）
		 *******************************************************************/

        /// <summary>
        /// int を16進数文字列に変換する
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string ConvInt32ToHexString(int num)
		{
			return Convert.ToString(num, 16).ToUpper();
		}

		/// <summary>
		/// 16進数文字列を int に変換する
		/// </summary>
		/// <param name="hex"></param>
		/// <returns></returns>
		public static int ConvHexStringToInt32(string hex)
		{
			return Convert.ToInt32(hex, 16);
		}

		/// <summary>
		/// 文字列をバイナリに変換する
		/// </summary>
		/// <param name="enc">エンコーディング</param>
		/// <param name="str">文字列</param>
		/// <returns></returns>
		public static byte[] ConvStringToBinary(Encoding enc, string str)
		{
			return enc.GetBytes(str);
		}

		/// <summary>
		/// バイナリを文字列に変換する
		/// </summary>
		/// <param name="enc">エンコーディング</param>
		/// <param name="byteData">バイナリデータ</param>
		/// <returns></returns>
		public static string ConvBinaryToString(Encoding enc, byte[] byteData)
		{
			return enc.GetString(byteData);
		}

		/// <summary>
		/// 文字列をカンマ区切りの通貨形式に変換する
		/// </summary>
		/// <param name="str">数値文字列</param>
		/// <returns></returns>
		public static string ConvCurrencyFormat(string str)
		{
			if (string.IsNullOrEmpty(str)) { return ""; }
			return String.Format("{0:#,0}", Int64.Parse(str));
		}

		/// <summary>
		/// 文字列を \ 区切りで連結する
		/// </summary>
		/// <param name="path">連結文字列</param>
		/// <returns></returns>
		public static string ConcatPath(params string[] path)
		{
			StringBuilder sb = new StringBuilder();
			string span = "";
			for (int i = 0; i < path.Length; i++)
			{
				if (!sb.ToString().EndsWith(@"\"))
				{
					sb.Append(span);
				}
				sb.Append(path[i]);
				span = @"\";
			}
			return sb.ToString();
		}

		/// <summary>
		/// バイナリデータをスペース区切で連結した文字列を取得する
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string GetBinaryString(byte[] data)
		{
			StringBuilder sb = new StringBuilder();
			string sp = "";

			for (int i = 0; i < data.Length; i++)
			{
				string bin = CommonUtil.ConvInt32ToHexString((int)data[i]);
				string buf = CommonUtil.PadLeft(bin, 2, "0");
				sb.Append(sp);
				sb.Append(buf);
				sp = " ";
			}
			return sb.ToString();
		}

		/// <summary>
		/// 文字列の末尾から指定した長さの文字列を取得する
		/// </summary>
		/// <param name="str">文字列</param>
		/// <param name="len">文字列サイズ</param>
		/// <returns></returns>
		public static string Right(string str, int len)
		{
			return str.Substring(str.Length - len, len);
		}

		/// <summary>
		/// 文字列の先頭から指定した長さの文字列を取得する
		/// </summary>
		/// <param name="str">文字列</param>
		/// <param name="len">文字列サイズ</param>
		/// <returns></returns>
		public static string Left(string str, int len)
		{
			return str.Substring(0, len);
		}

		/// <summary>
		/// 文字列をパディングする（右）
		/// </summary>
		/// <param name="str">文字列</param>
		/// <param name="len">パディング桁数</param>
		/// <param name="padStr">パディング文字列</param>
		/// <returns></returns>
		public static string PadRight(string str, int len, string padStr)
		{
			return str.PadRight(len, Convert.ToChar(padStr));
		}

		/// <summary>
		/// 文字列をパディングする（左）
		/// </summary>
		/// <param name="str">文字列</param>
		/// <param name="len">パディング桁数</param>
		/// <param name="padStr">パディング文字列</param>
		/// <returns></returns>
		public static string PadLeft(string str, int len, string padStr)
		{
			return str.PadLeft(len, Convert.ToChar(padStr));
		}

        /// <summary>
        /// 文字列をバイト数でパディングする（右）
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="blen">パディングバイト数</param>
        /// <param name="padStr">パディング文字列</param>
        /// <returns></returns>
        public static string BPadRight(string str, int blen, string padStr)
        {
            return BPadRight(str, _enc, blen, padStr);
        }

        /// <summary>
        /// 文字列をバイト数でパディングする（右）
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="enc">文字エンコード</param>
        /// <param name="blen">パディングバイト数</param>
        /// <param name="padStr">パディング文字列</param>
        /// <returns></returns>
        public static string BPadRight(string str, Encoding enc, int blen, string padStr)
		{
			// パディングバイト数超
			byte[] srcByte = enc.GetBytes(str);
			if (srcByte.Length >= blen)
			{
				return str;
			}

			byte[] mergeByte = new byte[blen];
			byte[] padByte = enc.GetBytes(padStr);
			Array.Copy(srcByte, 0, mergeByte, 0, srcByte.Length);
			int mergeIdx = srcByte.Length;
			do
			{
				Array.Copy(padByte, 0, mergeByte, mergeIdx, padByte.Length);
				mergeIdx += padByte.Length;
			} while (mergeIdx < blen);

			return enc.GetString(mergeByte);
		}

        /// <summary>
        /// 文字列をバイト数でパディングする（左）
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="blen">パディングバイト数</param>
        /// <param name="padStr">パディング文字列</param>
        /// <returns></returns>
        public static string BPadLeft(string str, int blen, string padStr)
        {
            return BPadLeft(str, _enc, blen, padStr);
        }

        /// <summary>
        /// 文字列をバイト数でパディングする（左）
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="enc">文字エンコード</param>
        /// <param name="blen">パディングバイト数</param>
        /// <param name="padStr">パディング文字列</param>
        /// <returns></returns>
        public static string BPadLeft(string str, Encoding enc, int blen, string padStr)
		{
			// パディングバイト数超
			byte[] srcByte = enc.GetBytes(str);
			if (srcByte.Length >= blen)
			{
				return str;
			}

			byte[] mergeByte = new byte[blen];
			byte[] padByte = enc.GetBytes(padStr);
			int addLen = blen - srcByte.Length;
			int mergeIdx = 0;
			do
			{
				Array.Copy(padByte, 0, mergeByte, mergeIdx, padByte.Length);
				mergeIdx += padByte.Length;
			} while (mergeIdx < addLen);

			Array.Copy(srcByte, 0, mergeByte, mergeIdx, srcByte.Length);
			return enc.GetString(mergeByte);
		}

		/// <summary>
		/// 文字列のバイナリ桁数を取得する
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int BLen(string str)
		{
			return _enc.GetBytes(str).Length;
		}

		/// <summary>
		/// 文字列を分割する
		/// </summary>
		/// <param name="str">文字列</param>
		/// <param name="split">区切文字</param>
		/// <returns></returns>
		public static string[] Split(string str, string split)
		{
			return str.Split(new char[] { Convert.ToChar(split) });
		}

		/// <summary>
		/// シングルクォートを削除する
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string SanitizeSqlValue(string str)
		{
			return str.Replace("'", "''");
		}

		/*******************************************************************
		 * 公開メソッド（ファイル操作）
		 *******************************************************************/

		/// <summary>
		/// ディレクトリをコピーする
		/// </summary>
		/// <param name="srcDir">コピー元ディレクトリパス</param>
		/// <param name="destDir">コピー先ディレクトリパス</param>
		public static void CopyDirectory(string srcDir, string destDir)
		{
			// コピー先のディレクトリがないときは作る
			if (!Directory.Exists(destDir))
			{
				Directory.CreateDirectory(destDir);
				File.SetAttributes(destDir,
				File.GetAttributes(srcDir));
			}

			// コピー先のディレクトリ名の末尾に"\"をつける
			if (destDir[destDir.Length - 1] != Path.DirectorySeparatorChar)
			{
				destDir = destDir + Path.DirectorySeparatorChar;
			}

			// コピー元のディレクトリにあるファイルをコピー
			string[] files = Directory.GetFiles(srcDir);
			foreach (string file in files)
			{
				File.Copy(file, destDir + System.IO.Path.GetFileName(file), true);
			}

			// コピー元のディレクトリにあるディレクトリについて、再帰的に呼び出す
			string[] dirs = Directory.GetDirectories(srcDir);
			foreach (string dir in dirs)
			{
				CopyDirectory(dir, destDir + System.IO.Path.GetFileName(dir));
			}
		}

        /// <summary>
        /// ファイルを削除する
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath)) { return; }
            File.Delete(filePath);
        }

        /// <summary>
        /// 指定パス配下のファイルとフォルダを削除する
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="isOwnDelete">true:指定したパスのフォルダ自身も削除する</param>
        public static void DeleteDirectories(string dirPath, bool isOwnDelete)
        {
            if (!Directory.Exists(dirPath)) { return; }

            DirectoryInfo target = new DirectoryInfo(dirPath);
            foreach (FileInfo file in target.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in target.GetDirectories())
            {
                dir.Delete(true);
            }
            if (isOwnDelete)
            {
                Directory.Delete(dirPath);
            }
        }

        /// <summary>
        /// 指定フォルダが空の場合は削除する
        /// </summary>
        /// <param name="dirPath"></param>
        public static void DeleteEmptyDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath)) { return; }

            // 格納フォルダ・ファイルがなければ削除
            if (Directory.EnumerateFiles(dirPath, "*.*").Count() == 0 && Directory.EnumerateDirectories(dirPath).Count() == 0)
            {
                Directory.Delete(dirPath);
            }
        }

        /// <summary>
        /// バイナリの書き込みを行う（追加）
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void AppendBinaryStream(string filePath, byte[] data)
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
			{
				fs.Write(data, 0, data.Length);
				fs.Close();
			}
		}

		/// <summary>
		/// バイナリを読み込む
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static byte[] ReadBinaryStream(string filePath)
		{
			return File.ReadAllBytes(filePath);
		}

        /// <summary>
        /// テキストに書き込みを行う（上書き）
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="msg"></param>
        /// <param name="enc"></param>
        public static void WriteAllTextStream(string filePath, string msg)
        {
            WriteAllTextStream(filePath, msg, _enc);
        }

        /// <summary>
        /// テキストに書き込みを行う（上書き）
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="msg"></param>
        /// <param name="enc"></param>
        public static void WriteAllTextStream(string filePath, string msg, Encoding enc)
		{
			File.WriteAllText(filePath, msg, enc);
		}

		/// <summary>
		/// テキストに書き込みを行う（追加）
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="msg"></param>
		/// <param name="enc"></param>
		public static void AppendAllTextStream(string filePath, string msg, Encoding enc)
		{
			File.AppendAllText(filePath, msg, enc);
		}

        /// <summary>
        /// テキストを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string[] ReadTextStream(string filePath)
        {
            return ReadTextStream(filePath, _enc);
        }

        /// <summary>
        /// テキストを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string[] ReadTextStream(string filePath, Encoding enc)
		{
			string[] retVal = new string[0];
			int readIndex = 0;
			using (StreamReader sr = new StreamReader(filePath, enc))
			{
				while (sr.Peek() > -1)
				{
					retVal.CopyTo(retVal = new string[retVal.Length + 1], 0);
					retVal[readIndex] = sr.ReadLine();
					readIndex++;
				}
			}
			return retVal;
		}

		/// <summary>
		/// CSVを読み込む
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="hasHeader"></param>
		/// <param name="filePath"></param>
		/// <param name="sep"></param>
		/// <param name="hasQuote"></param>
		/// <returns></returns>
		public static DataTable ReadCsvStream(DataTable dt, bool hasHeader, string filePath, string sep, bool hasQuote)
		{
			TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
			parser.TextFieldType = FieldType.Delimited;
			parser.SetDelimiters(sep);
			parser.HasFieldsEnclosedInQuotes = hasQuote;
			string[] data;
			if (!parser.EndOfData)
			{
				data = parser.ReadFields();
				int cols = data.Length;
				if (hasHeader)
				{
					for (int i = 0; i < cols; i++)
					{
						dt.Columns.Add(new DataColumn(data[i]));
					}
				}
				else
				{
					for (int i = 0; i < cols; i++)
					{
						dt.Columns.Add(new DataColumn());
					}
					DataRow row = dt.NewRow();
					for (int i = 0; i < cols; i++)
					{
						row[i] = data[i];
					}
					dt.Rows.Add(row);
				}
			}
			while (!parser.EndOfData)
			{
				data = parser.ReadFields();
				DataRow row = dt.NewRow();
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					row[i] = data[i];
				}
				dt.Rows.Add(row);
			}
			return dt;
		}

        /// <summary>
        /// ファイルを移動する
        /// </summary>
        /// <param name="srcFilePath">移動元ファイルパス</param>
        /// <param name="dstFilePath">移動先ファイルパス</param>
        /// <param name="tryCount">試行回数</param>
        /// <param name="sleepTime">スリープ時間(ミリ秒)</param>
        public static bool TryMoveFile(string srcFilePath, string dstFilePath, int tryCount, int sleepTime)
        {
            for (int i = 0; i < tryCount; i++)
            {
                try
                {
                    // ファイル移動（成功したら終了）
                    File.Move(srcFilePath, dstFilePath);
                    return true;
                }
                catch (Exception ex)
                {
                    // スリープして再試行
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.Message, 2);
                    if (i + 1 < tryCount)
                    {
                        System.Threading.Thread.Sleep(sleepTime);
                        continue;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ファイルを削除する
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="tryCount">試行回数</param>
        /// <param name="sleepTime">スリープ時間(ミリ秒)</param>
        public static bool TryDeleteFile(string filePath, int tryCount, int sleepTime)
		{
			for (int i = 0; i < tryCount; i++)
			{
				try
				{
					// ファイル削除（成功したら終了）
					File.Delete(filePath);
					return true;
				}
				catch (Exception ex)
				{
					// スリープして再試行
					LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.Message, 2);
					if (i + 1 < tryCount)
					{
						System.Threading.Thread.Sleep(sleepTime);
						continue;
					}
				}
			}
			return false;
		}


		/*******************************************************************
		 * 公開メソッド（その他）
		 *******************************************************************/

		/// <summary>
		/// 指定したプロセスを起動する
		/// </summary>
		/// <param name="pInfo">起動プロセス情報</param>
		/// <param name="isSync">同期実行フラグ</param>
		/// <returns>未使用</returns>
		public static int ExecProcess(ProcessStartInfo pInfo, bool isSync = true)
		{
			int retVal = 0;
			Process proc = new Process();
			try
			{
				// パラメータ設定
				pInfo.WindowStyle = ProcessWindowStyle.Hidden;
				pInfo.ErrorDialog = false;
				proc.StartInfo = pInfo;

				// プロセス実行
				proc.Start();

				// 終了するまで待機
				if (isSync)
				{
					proc.WaitForExit();
				}
				retVal = proc.ExitCode;
			}
			finally
			{
				proc.Close();
				proc.Dispose();
			}
			return retVal;
		}

		public static void DisposeBitmap(Bitmap bmp)
		{
			if (bmp != null)
			{
				bmp.Dispose();
				bmp = null;
			}
		}

		public static void DisposeImage(Image img)
		{
			if (img != null)
			{
				img.Dispose();
				img = null;
			}
		}

		public static void DisposeGraphics(Graphics g)
		{
			if (g != null)
			{
				g.Dispose();
				g = null;
			}
		}

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /*******************************************************************
		 * 公開メソッド（業務）
		 *******************************************************************/

        /// <summary>
        /// OCR値を取得する
        /// </summary>
        /// <param name="di"></param>
        /// <param name="ocr"></param>
        /// <returns></returns>
        public static string GetOcrValue(TBL_DSP_ITEM di, string ocr)
        {
            string retVal = ocr;
            switch (di.m_ITEM_TYPE)
            {
                case DspItem.ItemType.N:
                    // OCR値が数値項目で判別できない場合は空文字返却
                    long num = 0;
                    if (!Int64.TryParse(ocr, out num))
                    {
                        return "";
                    }
                    break;
                case DspItem.ItemType.A:
                case DspItem.ItemType.K:
                case DspItem.ItemType.R:
                case DspItem.ItemType.S:
                case DspItem.ItemType.T:
                case DspItem.ItemType.C:
                case DspItem.ItemType.J:
                case DspItem.ItemType.AST:
                case DspItem.ItemType.D:
                case DspItem.ItemType.V:
                case DspItem.ItemType.W:
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// TRITEM を編集する
        /// DB格納値に変換
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public static string EditTrDataItem(string dspItemVal, TBL_DSP_ITEM di)
		{
			string retVal = dspItemVal;
            switch (di._ITEM_ID)
            {
                case DspItem.ItemId.金額:
                    retVal = dspItemVal.Replace(",", "");
                    break;
                case DspItem.ItemId.入力交換希望日:
                case DspItem.ItemId.交換日:
                    retVal = dspItemVal.Replace(".", "");
                    break;
            }

            // DB格納桁数取得
            int DBItemLength = GetDBItemLength(di);

            // 空白許可ではない数字項目の場合は、
            // 未入力の場合、0を設定する。
            switch (di.m_ITEM_TYPE)
			{
				case DspItem.ItemType.A:
					// 半角空白桁埋め(後方)
					retVal = BPadRight(retVal, _enc, DBItemLength, " ");
					break;

				case DspItem.ItemType.N:
					// 0桁埋め(前方)
					retVal = BPadLeft(retVal, _enc, DBItemLength, "0");
					break;

				case DspItem.ItemType.R:
					if (string.IsNullOrEmpty(retVal))
					{
						// 空打ち→半角空白桁埋め(後方)
						retVal = BPadRight(retVal, _enc, DBItemLength, " ");
					}
					else
					{
						// 入力済→0桁埋め(前方)
						retVal = BPadLeft(retVal, _enc, DBItemLength, "0");
					}
					break;
				case DspItem.ItemType.K: // カナ、英数字項目
				case DspItem.ItemType.J: // 漢字項目
					if (!string.IsNullOrEmpty(retVal))
					{
						retVal = retVal.Replace("ｧ", "ｱ");
						retVal = retVal.Replace("ｨ", "ｲ");
						retVal = retVal.Replace("ｩ", "ｳ");
						retVal = retVal.Replace("ｪ", "ｴ");
						retVal = retVal.Replace("ｫ", "ｵ");
						retVal = retVal.Replace("ｯ", "ﾂ");
						retVal = retVal.Replace("ｬ", "ﾔ");
						retVal = retVal.Replace("ｭ", "ﾕ");
						retVal = retVal.Replace("ｮ", "ﾖ");
						retVal = retVal.Replace("ｰ", "-");
					}
					// 半角空白桁埋め(後方)
					retVal = BPadRight(retVal, _enc, DBItemLength, " ");
					break;
				case DspItem.ItemType.S:   // 空白及びゼロでなければ、ゼロ及びカンマを取り除く
                    retVal = dspItemVal.Replace(",", "");
                    if (!DBConvert.ToLongNull(retVal).Equals(0))
					{
						retVal = retVal.TrimStart('0');
					}
					else
					{
						retVal = "0";
					}
					break;
				case DspItem.ItemType.T:   // ゼロでなければ、ゼロ及びカンマを取り除く、それ以外はそのまま
                    retVal = dspItemVal.Replace(",", "");
                    if (!DBConvert.ToLongNull(retVal).Equals(0) && !string.IsNullOrEmpty(retVal))
					{
						retVal = retVal.TrimStart('0');
					}
					else if (!string.IsNullOrEmpty(retVal))
					{
						retVal = "0";
					}
					break;
				case DspItem.ItemType.C:
					// 定数
					retVal = di.m_ITEM_DISPNAME;
					break;
                case DspItem.ItemType.V:
                case DspItem.ItemType.W:
                    // 読取項目
                    // 読取項目でも特殊項目は前ゼロを付ける
                    switch (di._ITEM_ID)
                    {
                        case DspItem.ItemId.持帰銀行コード:
                        case DspItem.ItemId.決済フラグ:
                        case DspItem.ItemId.交換証券種類コード:
                        case DspItem.ItemId.持帰支店コード:
                        case DspItem.ItemId.口座番号:
                            retVal = BPadLeft(retVal, _enc, DBItemLength, "0");
                            break;
                    }
                    break;
                default:
					break;
			}
			return retVal;
		}

        /// <summary>
        /// DB格納値の長さ取得
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public static int GetDBItemLength(TBL_DSP_ITEM di)
        {
            return GetDBItemLength(di._ITEM_ID, di.m_ITEM_LEN);
        }

        /// <summary>
        /// DB格納値の長さ取得
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="itemLen"></param>
        /// <returns></returns>
        public static int GetDBItemLength(int itemID, int itemLen)
        {
            int retVal = itemLen;
            switch (itemID)
            {
                case DspItem.ItemId.持帰銀行コード:
                case DspItem.ItemId.券面持帰銀行コード:
                    retVal = Const.BANK_NO_LEN;
                    break;
                case DspItem.ItemId.金額:
                    retVal = Const.AMOUNT_NO_LEN;
                    break;
                case DspItem.ItemId.決済フラグ:
                    retVal = Const.PAYKBN_LEN;
                    break;
                case DspItem.ItemId.交換証券種類コード:
                    retVal = Const.BILL_CD_LEN;
                    break;
                case DspItem.ItemId.手形種類コード:
                    retVal = Const.SYURUI_CD_LEN;
                    break;
                case DspItem.ItemId.券面持帰支店コード:
                case DspItem.ItemId.持帰支店コード:
                    retVal = Const.BR_NO_LEN;
                    break;
                case DspItem.ItemId.券面口座番号:
                case DspItem.ItemId.口座番号:
                    retVal = Const.KOZA_NO_LEN;
                    break;
                case DspItem.ItemId.手形番号:
                    retVal = Const.TEGATA_NO_LEN;
                    break;
            }

            return retVal;
        }

        /// <summary>
        /// TRITEM を編集する
        /// DB格納値から画面表示に変換
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        /// <remarks>カンマを付ける等は別途実施</remarks>
        public static string EditDspItem(string itemVal, TBL_DSP_ITEM di)
        {
            if (string.IsNullOrEmpty(itemVal))
            {
                // 空はそのまま返す
                return itemVal;
            }

            string retVal = itemVal;

            // 所定の文字埋めを削除して画面表示桁数に整形
            switch (di.m_ITEM_TYPE)
            {
                case DspItem.ItemType.A:
                    // 半角空白桁埋め(後方)
                    retVal = retVal.TrimEnd(' ');
                    retVal = BPadRight(retVal, _enc, di.m_ITEM_LEN, " ");
                    break;
                case DspItem.ItemType.N:
                    // 0桁埋め(前方)
                    retVal = retVal.TrimStart('0');
                    retVal = BPadLeft(retVal, _enc, di.m_ITEM_LEN, "0");
                    break;
                case DspItem.ItemType.R:
                    if (string.IsNullOrWhiteSpace(retVal))
                    {
                        // 半角空白のみ
                        retVal = BPadRight("", _enc, di.m_ITEM_LEN, " ");
                    }
                    else
                    {
                        // 0桁埋め(前方)
                        retVal = retVal.TrimStart('0');
                        retVal = BPadLeft(retVal, _enc, di.m_ITEM_LEN, "0");
                    }
                    break;
                case DspItem.ItemType.K: // カナ、英数字項目
                case DspItem.ItemType.J: // 漢字項目
                    // 半角空白桁埋め(後方)
                    retVal = retVal.TrimEnd(' ');
                    retVal = BPadRight(retVal, _enc, di.m_ITEM_LEN, " ");
                    break;
                case DspItem.ItemType.S:
                    // 空白及びゼロでなければ、ゼロ及びカンマを取り除く
                    if (!DBConvert.ToLongNull(retVal).Equals(0))
                    {
                        retVal = retVal.TrimStart('0');
                    }
                    break;
                case DspItem.ItemType.T:
                    // ゼロでなければ、ゼロ及びカンマを取り除く、それ以外はそのまま
                    if (!DBConvert.ToLongNull(retVal).Equals(0) && !string.IsNullOrEmpty(retVal))
                    {
                        retVal = retVal.TrimStart('0');
                    }
                    break;
                case DspItem.ItemType.V:
                case DspItem.ItemType.W:
                    // 読取項目
                    // 読取項目でも特殊項目は0桁埋め(前方)
                    switch (di._ITEM_ID)
                    {
                        case DspItem.ItemId.持帰銀行コード:
                        case DspItem.ItemId.決済フラグ:
                        case DspItem.ItemId.交換証券種類コード:
                        case DspItem.ItemId.持帰支店コード:
                        case DspItem.ItemId.口座番号:
                            // 0桁埋め(前方)
                            retVal = retVal.TrimStart('0');
                            retVal = BPadLeft(retVal, _enc, di.m_ITEM_LEN, "0");
                            break;
                    }
                    break;
                default:
                    break;
            }

            return retVal;
        }

        /// <summary>
        /// フォーカス対象外テキストボックス
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns>true：フォーカス対象外、false：フォーカス対象</returns>
        public static bool IsNotFocusItem(TBL_DSP_ITEM di)
		{
			if (di == null) { return false; }
			bool isNotFocus = false;
			isNotFocus |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.C);
			isNotFocus |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.AST);
			isNotFocus |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.V);
            isNotFocus |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.W);
            return isNotFocus;
		}

		/// <summary>
		/// ＤＢ登録対象外テキストボックス
		/// </summary>
		/// <param name="di"></param>
		/// <returns>true：ＤＢ登録対象外、false：ＤＢ登録対象</returns>
		public static bool IsNotRegistItem(TBL_DSP_ITEM di)
		{
			if (di == null) { return false; }
			bool isNotRegist = false;
			isNotRegist |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.D);
			isNotRegist |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.AST);
			return isNotRegist;
		}

		/// <summary>
		/// 入力登録対象外テキストボックス
		/// </summary>
		/// <param name="itemid"></param>
		/// <returns>true：手入力項目でない、false：手入力項目である</returns>
		public static bool IsNotInputItem(TBL_DSP_ITEM di)
		{
			if (di == null) { return false; }
			bool isNotInput = false;
			isNotInput |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.C);
			isNotInput |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.D);
			isNotInput |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.AST);
			isNotInput |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.V);
            isNotInput |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.W);
            return isNotInput;
		}

		/// <summary>
		/// 入力チェック対象外テキストボックス
		/// </summary>
		/// <param name="di"></param>
		/// <returns>true：入力チェック対象項目でない、false：入力チェック対象項目である</returns>
		public static bool IsNotInputCheckItem(TBL_DSP_ITEM di)
		{
			if (di == null) { return false; }
			bool isNotInput = false;
			isNotInput |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.C);
			isNotInput |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.D);
			isNotInput |= di.m_ITEM_TYPE.Equals(DspItem.ItemType.AST);
			return isNotInput;
		}

        /// <summary>
        /// イメージファイルフォルダパスを取得する
        /// </summary>
        public static string GetImgDirPath(TBL_TRBATCH bat, Dictionary<string, string> pathList)
        {
            string gymid = bat._GYM_ID.ToString("D3");
            string opedate = bat._OPERATION_DATE.ToString("D8");
            string batid = bat._BAT_ID.ToString("D8");
            string dirName = "";

            if (bat._GYM_ID == GymParam.GymId.持出)
            {
                if (bat.m_INPUT_ROUTE == TrBatch.InputRoute.通常)
                {
                    dirName = pathList["BankNormalImageRoot"];
                }
                else if (bat.m_INPUT_ROUTE == TrBatch.InputRoute.付帯)
                {
                    dirName = pathList["BankFutaiImageRoot"];
                }
                else if (bat.m_INPUT_ROUTE == TrBatch.InputRoute.期日管理)
                {
                    dirName = pathList["BankKijituImageRoot"];
                }
            }
            else // if (bat._GYM_ID == GymParam.GymId.持帰)
            {
                dirName = pathList["BankConfirmImageRoot"];
            }
            return Path.Combine(dirName, gymid + opedate + batid);
        }

        /// <summary>
        /// イメージファイルパスを取得する
        /// </summary>
        public static string GetImgFilePath(TBL_TRBATCH bat, TBL_TRMEIIMG img, Dictionary<string, string> pathList)
        {
            string dirName = GetImgDirPath(bat, pathList);
            return Path.Combine(dirName, img.m_IMG_FLNM);
        }

        /// <summary>
        /// イメージファイルパスを取得する
        /// </summary>
        public static string GetImgFilePath(TBL_TRBATCH bat, TBL_TRBATCHIMG img, Dictionary<string, string> pathList)
        {
            string dirName = GetImgDirPath(bat, pathList);
            return Path.Combine(dirName, img.m_IMG_FLNM);
        }

        /// <summary>
        /// キーを生成する
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GenerateKey(params object[] keys)
        {
            return GenerateKey("|", keys);
        }

        /// <summary>
        /// キーを生成する
        /// セパレータ指定
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GenerateKey(string sep, params object[] keys)
        {
            string retVal = "";
            string sepBuf = "";
            for (int i = 0; i < keys.Length; i++)
            {
                retVal += string.Format("{0}{1}", sepBuf, keys[i]);
                sepBuf = sep;
            }
            return retVal;
        }

        /// <summary>
        /// キーを取得する
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] DivideKeys(string key, string sep = "|")
        {
            if (string.IsNullOrEmpty(key)) { return new string[] { "" }; }
            return key.Split(new string[] { sep }, StringSplitOptions.None);
        }


        /*******************************************************************
		 * 公開メソッド（数値操作）
		 *******************************************************************/

        /// <summary>
        /// 除算して切り捨てる
        /// </summary>
        /// <param name="val"></param>
        /// <param name="div"></param>
        /// <returns></returns>
        public static int Divide(int val, int div)
		{
			int num = (int)Math.Floor((double)val / (double)div);
			return num;
		}

        /*******************************************************************
		 * 公開メソッド（ラベルフォント操作）
		 *******************************************************************/

        /// <summary>
        /// 文字列の幅に応じてラベルのフォントサイズを変更する
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="DefSize">規定のフォントサイズ</param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static void FitLabelFontSize(System.Windows.Forms.Label lbl, float DefSize, Graphics g)
        {
            // ラベルフォントを取得
            Font lblFont = lbl.Font;

            // 文字列からフォントサイズを取得
            float FontSize = 0;
            using (Font f = new Font(lblFont.FontFamily, 1, lblFont.Style, lblFont.Unit))
            {
                SizeF s = g.MeasureString(lbl.Text, f);
                float w = (float)Math.Truncate(lbl.Size.Width / s.Width);
                float h = (float)Math.Truncate(lbl.Size.Height / s.Height);
                FontSize = (w < h) ? w : h;
            }

            // 基準サイズより大きい場合は基準サイズ
            if (DefSize < FontSize)
            {
                FontSize = DefSize;
            }

            // 新しいフォントを設定
            Font newfont = new Font(lblFont.FontFamily, FontSize, lblFont.Style, lblFont.Unit);
            lbl.Font = newfont;
            // 置き換え前のフォントをDispose
            lblFont.Dispose();
        }

        /*******************************************************************
		 * 公開メソッド（DB操作）
		 *******************************************************************/

        /// <summary>
        /// 指定件数で分割して一括インサートを行う
        /// </summary>
        /// <param name="InsertList">インサートSQL</param>
        /// <param name="InsertCount">一回の登録件数</param>
        /// <returns></returns>
        public static int DBBatchInsert(List<string> InsertList, AdoDatabaseProvider dbp, AdoNonCommitTransaction non, int InsertCount = 500)
        {
            if (InsertList.Count == 0)
            {
                return 0;
            }

            /* ORACLEのINSERT ALLの場合
             * 登録するレコードの総項目数が「65533」を超えるとエラーになる
             * 例）1レコード17項目の登録の場合は
             *    17項目 * 3854レコード = 65,518 → OK
             *    17項目 * 3855レコード = 65,535 → NG
             * そのため、分割して登録する
             */

            int Count = 0;
            foreach (var ins in InsertList.Chunks(InsertCount))
            {
                string strSQL = DBCommon.GetBatchInsertSQL(ins);
                Count += dbp.CommandRun(strSQL, new List<IDbDataParameter>(), non.Trans);
            }

            return Count;
        }

    }

    /// <summary>
    /// Linq拡張
    /// </summary>
    public static class Linq_Extensions
    {
        /// <summary>
        /// 指定件数で分割する拡張メソッド
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> self, int chunkSize)
        {
            while (self.Any())
            {
                yield return self.Take(chunkSize);
                self = self.Skip(chunkSize);
            }
        }
    }

}
