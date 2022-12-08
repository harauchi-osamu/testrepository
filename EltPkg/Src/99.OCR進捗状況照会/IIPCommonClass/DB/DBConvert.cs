using Microsoft.VisualBasic;
using System;

namespace IIPCommonClass.DB
{
    public class DBConvert
    {
		/// <summary>クエリ：行ロックしない</summary>
		public const string QUERY_NOLOCK = " WITH(NOLOCK) ";
		/// <summary>クエリ：行ロックする</summary>
		public const string QUERY_LOCK = " WITH(XLOCK,ROWLOCK,NOWAIT) ";

		public static string ToStringNull(object obj)
        {
            return (obj == null || obj.Equals(DBNull.Value)) ? "" : Convert.ToString(obj).Trim();
        }

		public static string[] ToStringNull(object[] objs)
		{
			string[] res = new string[objs.Length];
			for (int i = 0; i < objs.Length; i++)
			{
				res[i] = ToStringNull(objs[i]);
			}
			return res;
		}

		public static int ToIntNull(object obj)
		{
			int res = 0;
			if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
			if (Int32.TryParse(obj.ToString(), out res)) { return res; }
			try { res = Convert.ToInt32(obj); }
			catch { res = 0; }
			return res;
		}

		public static long ToLongNull(object obj)
		{
			long res = 0;
			if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
			if (Int64.TryParse(obj.ToString(), out res)) { return res; }
			try { res = Convert.ToInt64(obj); }
			catch { res = 0; }
			return res;
		}

		public static decimal ToDecimalNull(object obj)
		{
			decimal res = 0;
			if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
			if (Decimal.TryParse(obj.ToString(), out res)) { return res; }
			try { res = Convert.ToDecimal(obj); }
			catch { res = 0; }
			return res;
		}

		public static string GetWideString(string value)
        {
            return Strings.StrConv(value, VbStrConv.Wide, 0);
        }

        public static string GetNarrowString(string value)
        {
            return Strings.StrConv(value, VbStrConv.Narrow, 0);
        }

        public static string GetWideUpperString(string value)
        {
            return Strings.StrConv(Strings.StrConv(value, VbStrConv.Wide, 0), VbStrConv.Uppercase, 0);
        }

        public static string GetWideLowerString(string value)
        {
            return Strings.StrConv(Strings.StrConv(value, VbStrConv.Wide, 0), VbStrConv.Lowercase, 0);
        }

        public static string GetNarrowUpperString(string value)
        {
            return Strings.StrConv(Strings.StrConv(value, VbStrConv.Narrow, 0), VbStrConv.Uppercase, 0);
        }

        public static string GetNarrowLowerString(string value)
        {
            return Strings.StrConv(Strings.StrConv(value, VbStrConv.Narrow, 0), VbStrConv.Lowercase, 0);
        }
    }
}
