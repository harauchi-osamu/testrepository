using CommonClass.DB;

namespace EntryCommon
{
	public sealed class GenDbProviderFactory
	{
        /// <summary>
        /// ADO.NET接続プロバイダを生成する
        /// </summary>
        /// <returns></returns>
        public static AdoDatabaseProvider CreateAdoProvider(DataBase db)
        {
            return new AdoDatabaseProvider(db);
        }

        /// <summary>
        /// ADO.NET接続プロバイダを生成する
        /// </summary>
        /// <returns></returns>
        public static AdoDatabaseProvider CreateAdoProvider1()
        {
            return new AdoDatabaseProvider(DBManager.dbc);
        }

        /// <summary>
        /// ADO.NET接続プロバイダを生成する
        /// </summary>
        /// <returns></returns>
        public static AdoDatabaseProvider CreateAdoProvider2()
        {
            return new AdoDatabaseProvider(DBManager.dbs1);
        }

        /// <summary>
        /// ADO.NET接続プロバイダを生成する
        /// </summary>
        /// <returns></returns>
        public static AdoDatabaseProvider CreateAdoProvider3()
        {
            return new AdoDatabaseProvider(DBManager.dbs2);
        }

    }
}
