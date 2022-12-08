using CommonTable.DB;
using NCR;

namespace CTRHulftRireki
{
    public class ServerIni
    {
        public static ServerIni Setting { get; private set; }

        public int BankCd { get; private set; }

        public string HulftEXELogRoot { get { return string.Format(Server.HulftEXELogRoot, BankCd); } }
        public string SendRoot { get { return string.Format(Server.RemoteSendRoot, BankCd); } }
        public string RecvRoot { get { return string.Format(Server.RemoteReceiveRoot, BankCd); } }
        //public string BackupRoot { get { return string.Format(Server.BackupRoot, BankCd); } }
        //public string LogRoot { get { return string.Format(Server.LogRoot, BankCd); } }
        //public string LogErrRoot { get { return string.Format(Server.LogErrRoot, BankCd); } }
        public string IOSendRoot { get { return string.Format(Server.IOSendRoot, BankCd); } }

        public string BackupRoot { get { return Server.BackupRoot; } }
        public string LogRoot { get { return Server.LogRoot; } }
        public string LogErrRoot { get { return Server.LogErrRoot; } }
        public string LogRecvFileName { get { return Server.LogRecvFileName; } }
        public string LogErrRecvFileName { get { return Server.LogErrRecvFileName; } }
        public string LogSendFileName { get { return Server.LogSendFileName; } }
        public string LogErrSendFileName { get { return Server.LogErrSendFileName; } }

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
