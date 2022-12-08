using System;
using System.Data;
using System.Data.Common;

namespace EntryCommon
{
	public sealed class AdoAutoCommitTransaction : IDisposable
	{
		private AdoDatabaseProvider _dbp = null;
        public DbTransaction Trans { get; set; } = null;

        /// <summary>コミットするかどうか（コミットしない場合は false を指定[デフォルト：true]）</summary>
		public bool isCommitEnd { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbp"></param>
		public AdoAutoCommitTransaction(AdoDatabaseProvider dbp, IsolationLevel level = IsolationLevel.ReadCommitted)
		{
			_dbp = dbp;
			Trans = _dbp.Db.Conn.BeginTransaction(level);
			isCommitEnd = true;
		}

        /// <summary>
        /// リソース破棄
        /// </summary>
		public void Dispose()
		{
			if (Trans != null && Trans.Connection != null)
			{

				if (Trans.Connection.State != System.Data.ConnectionState.Closed)
				{
                    if (isCommitEnd)
                    {
                        Trans.Commit();
                    }
                    else
                    {
                        Trans.Rollback();
                    }
                }
                Trans.Dispose();
				Trans = null;
			}
		}
	}
}
