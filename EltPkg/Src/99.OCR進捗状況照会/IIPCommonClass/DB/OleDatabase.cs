using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace IIPCommonClass.DB
{
    internal class OleDatabase : DataBase
    {
        internal OleDatabase()
        {
            _factory = DbProviderFactories.GetFactory("System.Data.OleDb");
        }

        internal OleDatabase(string provider, string name, string user, string pass)
        {
            _factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            _name = name;
            _user = user;
            _password = pass;
            _provider = provider;
        }

        #region Parameter
        public override void AddSqlParameter(string name, object dbtype, ParameterDirection direction, object value)
        {
            AddSqlParameter(name, dbtype, direction, value, value.ToString().Length);
        }

        public override void AddSqlParameter(string name, object dbtype, ParameterDirection direction, object value, int size)
        {
            DbParameter param = _cmd.CreateParameter();
            param.ParameterName = name;
            ((System.Data.OleDb.OleDbParameter)param).OleDbType = (System.Data.OleDb.OleDbType)dbtype;
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
