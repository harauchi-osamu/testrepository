using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Oracle.DataAccess.Client;


namespace CommonClass.DB
{
    public class OraDatabase : DataBase
    {
        public OraDatabase(string name, string user, string pass)
        {
            _factory = DbProviderFactories.GetFactory("Oracle.DataAccess.Client");
            _name = name;
            _user = user;
            _password = pass;
        }

        #region Parameter

        public override void AddSqlParameter(string name, DBTYPE dbtype, ParameterDirection direction, object value)
        {
            AddSqlParameter(name, dbtype, direction, value, value.ToString().Length);
        }

        public override void AddSqlParameter(string name, DBTYPE dbtype, ParameterDirection direction, object value, int size)
        {
            OracleDbType oraType;
            switch (dbtype)
            {
                case DBTYPE.NUMBER:
                    oraType = OracleDbType.Decimal;
                    break;
                case DBTYPE.VARCHAR2:
                    oraType = OracleDbType.Varchar2;
                    break;
                default:
                    oraType = OracleDbType.Varchar2;
                    break;
            }
            OracleParameter param = new OracleParameter();
            param.ParameterName = name;
            param.OracleDbType =oraType;
            param.Direction = direction;
            param.Value = value;
            if (oraType == OracleDbType.Varchar2 && direction != ParameterDirection.Output)
            {
                param.Size = size;
            }
            _cmd.Parameters.Add(param);
        }

        public override Object GetSqlParameterValue(string name)
        {
            _cmd.Cancel();
            return _cmd.Parameters[name].Value.ToString();
        }

        public override void ClearSqlParameters()
        {
            _cmd.Parameters.Clear();
        }

        #endregion
    }
}
