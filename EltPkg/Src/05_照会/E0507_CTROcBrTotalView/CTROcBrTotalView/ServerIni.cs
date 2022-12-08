﻿using CommonTable.DB;
using NCR;

namespace CTROcBrTotalView
{
    public class ServerIni
    {
        public static ServerIni Setting { get; private set; }

        public int BankCd { get; private set; }

        public string ExePath { get { return string.Format(Server.ExePath, BankCd); } }
        public int ContractBankCd { get { return DBConvert.ToIntNull(string.Format(Server.ContractBankCd, BankCd)); } }
        public string BankTotalImageRoot { get { return string.Format(Server.BankTotalImageRoot, BankCd); } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="bankcd"></param>
        private ServerIni(int bankcd)
        {
            BankCd = bankcd;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="bankcd"></param>
        public static void Initialize(int bankcd)
        {
            Setting = new ServerIni(bankcd);
        }
    }
}
