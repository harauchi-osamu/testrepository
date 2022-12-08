namespace EntryCommon
{
    abstract public class ControllerBase
    {
        /// <summary>マスタテーブル管理クラス</summary>
        public MasterManager MasterMgr { get; private set; }

        /// <summary>トランザクションテーブル管理クラス</summary>
        public ManagerBase ItemMgr { get; private set; }

        /// <summary>メニュー番号</summary>
        public string MenuNumber { get; set; } = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControllerBase()
        {
        }

        /// <summary>
        /// 管理クラスを設定する
        /// </summary>
        /// <param name="mst"></param>
        /// <param name="item"></param>
        public virtual void SetManager(MasterManager mst, ManagerBase item)
        {
            MasterMgr = mst;
            ItemMgr = item;
        }

        /// <summary>
        /// 引数を設定する
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual bool SetArgs(string[] args)
        {
            return true;
        }

        /// <summary>
        /// 設定ファイルを検証する
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckIniParams()
        {
            // Operator.ini
            if (!CheckOperatorIni())
            {
                return false;
            }

            // Term.ini
            if (!CheckTermIni())
            {
                return false;
            }

            // Server.ini
            if (!CheckServerIni())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Operator.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckOperatorIni()
        {
            return true;
        }

        /// <summary>
        /// Term.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckTermIni()
        {
            return true;
        }

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckServerIni()
        {
            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckAppConfig()
        {
            return true;
        }
    }
}
