using CommonTable.DB;
using NCR;

namespace HulftIO
{
    public class ServerIni
    {
        public static ServerIni Setting { get; private set; }

        public string HulftExePath { get { return Server.HulftExePath; } }
        public string SHulftID { get { return Server.SHulftID; } }
        public string RHulftID { get { return Server.RHulftID; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="bankcd"></param>
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
