using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR
{
	public class Operator
	{
		public static string IniPath { get { return @"C:\NCR\CtrOperator.ini"; } }

        static Operator()
		{
        }

		/// <summary>
		/// ユーザーID
		/// </summary>
		public static string UserID
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "User", "UserID"); }
            set { IniFileAccess.SetValue(IniPath, "User", "UserID", value); }
        }

        /// <summary>
        /// ユーザー名称
        /// </summary>
        public static string UserName
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "User", "UserName"); }
            set { IniFileAccess.SetValue(IniPath, "User", "UserName", value); }
        }

        /// <summary>
        /// ユーザー入力方法（ローマ入力：１,かな入力：０）
        /// </summary>
        public static int Roman
        {
            get { return IniFileAccess.GetKeyByInt(IniPath, "User", "Roman"); }
            set { IniFileAccess.SetValue(IniPath, "User", "Roman", value.ToString()); }
        }

		/// <summary>
		/// グループ権限
		/// </summary>
		public static int Priviledge
		{
			get { return IniFileAccess.GetKeyByInt(IniPath, "User", "Priviledge"); }
			set { IniFileAccess.SetValue(IniPath, "User", "Priviledge", value.ToString()); }
		}

        /// <summary>
        /// 処理対象の銀行コード
        /// </summary>
        public static int BankCD
        {
            get { return IniFileAccess.GetKeyByInt(IniPath, "Bank", "BankCD"); }
            set { IniFileAccess.SetValue(IniPath, "Bank", "BankCD", value.ToString()); }
        }

        /// <summary>
        /// 処理対象の銀行名
        /// </summary>
        public static string BankNM
        {
            get { return IniFileAccess.GetKeyByString(IniPath, "Bank", "BankNM"); }
            set { IniFileAccess.SetValue(IniPath, "Bank", "BankNM", value); }
        }

        /// <summary>
        /// 指定した処理グループの権限名を取得（補正エントリー用）
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static string PriviledgeName()
        {
            try
            {
                return priviledgeName(IniFileAccess.GetKeyByInt(IniPath, "User", "Priviledge"));
            }
            catch { return ""; }
        }

        /// <summary>
        /// 権限名称を取得
        /// </summary>
        /// <param name="priv"></param>
        /// <returns></returns>
        private static string priviledgeName(int priv)
        {
            switch (priv)
            {
                case 0:
                    return "NULL";
                case 1:
                    return "OPEL";
                case 4:
                    return "MANAGER";
                case 9:
                    return "SYSMANAGER";
                default:
                    return "";
            }
        }
    }
}
