namespace EntryCommon
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManagerBase : ManagerBase
    {
        public MasterManager MasterMgr { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManagerBase(MasterManager mst = null)
        {
            MasterMgr = mst;
        }
    }
}
