using System;
using System.Data;
using System.Data.Common;

namespace EntryCommon
{
	public sealed class AdoNonCommitTransaction : IDisposable
	{
		private AdoDatabaseProvider _dbp = null;
        public DbTransaction Trans { get; set; } = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbp"></param>
		public AdoNonCommitTransaction(AdoDatabaseProvider dbp, IsolationLevel level = IsolationLevel.ReadCommitted)
		{
			_dbp = dbp;
			Trans = _dbp.Db.Conn.BeginTransaction(level);
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
                    Trans.Rollback();
                }
                Trans.Dispose();
				Trans = null;
			}
		}
	}
}
