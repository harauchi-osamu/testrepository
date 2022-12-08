using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
    public class OdpDatabase　: DataBase
    {
        #region Constructor

        public DbConnection Conn { get { return _conn; } }
        public DbProviderFactory Factory { get { return _factory; } }

        public OdpDatabase(string name, string user, string pass) 
		{
            _dataset = new DataSet();

            _factory = DbProviderFactories.GetFactory("Oracle.DataAccess.Client");
            _name = name;
            _user = user;
            _password = pass;
        }

        #endregion

        #region Parameter
        public override void AddSqlParameter(string name, object dbtype, ParameterDirection direction, object value)
        {
            AddSqlParameter(name, dbtype, direction, value, value.ToString().Length);
        }

        public override void AddSqlParameter(string name, object dbtype, ParameterDirection direction, object value, int size)
        {
            DbParameter param = _cmd.CreateParameter();
            param.ParameterName = name;
            ((System.Data.SqlClient.SqlParameter)param).SqlDbType = (System.Data.SqlDbType)dbtype;
            param.Direction = direction;
            param.Value = value;
            param.Size = size;

            _cmd.Parameters.Add(param);
        }

        public override object GetSqlParameterValue(string name)
        {
            _cmd.Cancel();
            return _cmd.Parameters[name].Value;
        }

        public override void ClearSqlParameters()
        {
            _cmd.Parameters.Clear();
        }
        #endregion
    }
}
