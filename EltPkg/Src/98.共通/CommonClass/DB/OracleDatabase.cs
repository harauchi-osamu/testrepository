using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using Common;

namespace CommonClass.DB
{
    public class OracleDatabase : DataBase
    {
        #region Constructor

        public OracleDatabase(string name, string user, string pass) 
		{
            _dataset = new DataSet();

            _factory = DbProviderFactories.GetFactory("Oracle.DataAccess.Client");
            _name = name;
            _user = user;
            _password = pass;
        }

        #endregion

        #region Parameter

        public override DbParameter CreateSqlParameter(string name, object dbtype, ParameterDirection direction, object value)
        {
            return CreateSqlParameter(name, dbtype, direction, value, value.ToString().Length);
        }

        public override DbParameter CreateSqlParameter(string name, object dbtype, ParameterDirection direction, object value, int size)
        {
            DbParameter param = _factory.CreateParameter();
            param.ParameterName = name;
            param.DbType = (System.Data.DbType)dbtype;
            param.Direction = direction;
            param.Value = value;
            param.Size = size;

            return param;
        }

        public override void AddSqlParameter(string name, object dbtype, ParameterDirection direction, object value)
        {
            AddSqlParameter(name, dbtype, direction, value, value.ToString().Length);
        }

        public override void AddSqlParameter(string name, object dbtype, ParameterDirection direction, object value, int size)
        {
            DbParameter param = (DbParameter)CreateSqlParameter(name, dbtype, direction, value, size);
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
