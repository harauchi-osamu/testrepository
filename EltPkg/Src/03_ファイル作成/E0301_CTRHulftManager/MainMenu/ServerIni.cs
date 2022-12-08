using CommonTable.DB;
using NCR;

namespace MainMenu
{
    public class ServerIni
    {
        public static ServerIni Setting { get; private set; }

        public int BankCd { get; private set; }

        public string SendRoot { get { return string.Format(Server.SendRoot, BankCd); } }
        public string IOSendRoot { get { return string.Format(Server.IOSendRoot, BankCd); } }
        public string IOSendRootBk { get { return string.Format(Server.IOSendRootBk, BankCd); } }

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
