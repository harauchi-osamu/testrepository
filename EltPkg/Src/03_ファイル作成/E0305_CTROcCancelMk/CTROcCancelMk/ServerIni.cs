using CommonTable.DB;
using NCR;

namespace CTROcCancelMk
{
    public class ServerIni
    {
        public static ServerIni Setting { get; private set; }

        public int BankCd { get; private set; }

        public bool InternalExchange { get { return DBConvert.ToBoolNull(Server.InternalExchange); } }
        public string IOSendRoot { get { return string.Format(Server.IOSendRoot, BankCd); } }

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
