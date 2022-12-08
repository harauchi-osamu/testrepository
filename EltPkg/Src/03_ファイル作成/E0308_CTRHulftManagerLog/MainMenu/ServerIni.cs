using CommonTable.DB;
using NCR;

namespace MainMenu
{
    public class ServerIni
    {
        public static ServerIni Setting { get; private set; }

        public string LogRoot { get { return Server.LogRoot; } }
        public string LogErrRoot { get { return Server.LogErrRoot; } }
        public string LogRecvFileName { get { return Server.LogRecvFileName; } }
        public string LogErrRecvFileName { get { return Server.LogErrRecvFileName; } }
        public string LogSendFileName { get { return Server.LogSendFileName; } }
        public string LogErrSendFileName { get { return Server.LogErrSendFileName; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private ServerIni()
        {
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize()
        {
            Setting = new ServerIni();
        }
    }
}
