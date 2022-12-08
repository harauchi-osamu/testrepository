using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using IIPCommonClass.Log;
using Oracle.ManagedDataAccess.Client;

namespace IIPCommonClass.DB
{
    public partial class DataBase
    {
        #region Member Value

        protected DbProviderFactory _factory;
        protected DbConnection _conn;
        //protected OracleConnection _conn;
        protected DbCommand _cmd;
        //protected OracleCommand _cmd;

        protected DbCommandBuilder _cmdb;
        protected DbTransaction _trans;
        //protected OracleTransaction _trans;

        protected DbDataAdapter _adapter;
        protected DbDataReader _reader;
        protected DataSet _dataset = new DataSet();

        protected string _provider;
        protected string _catalog;
        protected string _name;
        protected string _user;
        protected string _password;

        #endregion

        #region Open/Close

        public bool Open()
        {
            DbConnectionStringBuilder csbuilder = _factory.CreateConnectionStringBuilder();

            csbuilder["Data Source"] = _name;
            csbuilder["Initial Catalog"] = _catalog;
            csbuilder["User ID"] = _user;
            csbuilder["Password"] = _password;
            csbuilder["Provider"] = _provider;

            return Open(csbuilder.ConnectionString);
        }
        public bool Open(string name, string user, string pass, string provider)
        {
            DbConnectionStringBuilder csbuilder = _factory.CreateConnectionStringBuilder();
            csbuilder["Data Source"] = name;
            csbuilder["User ID"] = user;
            csbuilder["Password"] = pass;
            csbuilder["Provider"] = _provider;

            return Open(csbuilder.ConnectionString);
        }

        public bool Open(string name, string user, string pass)
        {
            DbConnectionStringBuilder csbuilder = _factory.CreateConnectionStringBuilder();
            csbuilder["Data Source"] = name;
            csbuilder["User ID"] = user;
            csbuilder["Password"] = pass;

            return Open(csbuilder.ConnectionString);
        }

        private bool Open(string connstring)
        {
            //LogWriter.writeLog(MethodBase.GetCurrentMethod(), "OpenConnection = " + connstring, 1);

            _conn = _factory.CreateConnection();
            //_conn = new OracleConnection();
            _conn.ConnectionString = connstring;
            _conn.Open();

            return true;
        }

        public bool Close()
        {
            //LogWriter.writeLog(MethodBase.GetCurrentMethod(), "CloseConnection", 1);
            if(_cmd != null) _cmd.Dispose();
            if (_conn.State != ConnectionState.Closed)
                _conn.Close();

            return true;
        }
        #endregion

        /// <summary>
        /// 新しいコマンド(SQL)を作成
        /// </summary>
        /// <param name="QueryOrProcedure">SQL</param>
        /// <param name="Commandtype">CommandType.Text,Procedure</param>
        public void NewCommand(string QueryOrProcedure, CommandType Commandtype, int LogLevel)
        {
            if (LogLevel > 75)
            {
                //LogWriter.writeLog(MethodBase.GetCurrentMethod(), "NewCommand = " + QueryOrProcedure, 1);
            }

            if(_cmd!= null) _cmd.Dispose();
            _cmd = _factory.CreateCommand();
            //_cmd = new OracleCommand();

            _cmd.Connection = _conn;
            _cmd.CommandText = QueryOrProcedure;
            _cmd.CommandType = Commandtype;
            _cmd.Transaction = _trans;
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <returns></returns>
        public int Execute()
        {
            return this.Execute(false);
        }

        /// <summary>
        /// Insert Update Deleteを実行する。NewCommandでSqlをセットする。
        /// </summary>
        /// <param name="LogWrite">EventLog出力</param>
        /// <returns>件数</returns>
        public int Execute(bool LogWrite)
        {
            if (LogWrite)
            {
                //LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Execute: " + _cmd.CommandText, 1);
            }

            int ret = _cmd.ExecuteNonQuery();
            if(_cmd.CommandType != CommandType.StoredProcedure)
            {
                _cmd.Dispose();
            }

            return ret;
        }

        /// <summary>
        /// DBReaderでSelectSQLを実行し値を取得する。
        /// </summary>
        /// <returns>DataRow[]</returns>
        public DataRow[] ExecuteSelect(int LogLevel)
        {
            DataColumn dataColumn;
            DataRow dataRow;
            string strColumnName;

            if (LogLevel > 75)
            {
                //LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ExecuteSelect: " + _cmd.CommandText, 1);
            }

            DataTable dataTable = new DataTable();

            _reader = _cmd.ExecuteReader();

            //ReaderからDataTableを作成する。
            for (int i = 0; i < _reader.FieldCount; i++)
            {
                dataColumn = new DataColumn(_reader.GetName(i), _reader.GetFieldType(i));
                dataTable.Columns.Add(dataColumn);
            }

            //ReaderからDataRowを作成する
            while (_reader.Read())
            {
                dataRow = dataTable.NewRow();
                for (int i = 0; i < _reader.FieldCount; i++)
                {
                    strColumnName = _reader.GetName(i);
                    dataRow[strColumnName] = _reader[strColumnName];
                }
                dataTable.Rows.Add(dataRow);
            }

            _reader.Close();
            _cmd.Dispose();

            return dataTable.Select();
        }

		/// <summary>
		/// DBReaderでSelectSQLを実行し値を取得する。(Export用)
		/// ※Export処理以外で使用しないで下さい。
		/// </summary>
		/// <returns>DataTable</returns>
		public DataTable ExportExecuteSelect()
		{
			DataColumn myDataColumn;
			DataRow myRow;
			string strColumnName;

			ProcessLog.getInstance().AddLog("ExecuteSelect");

			DataTable myDataTable = new DataTable();

			_reader = _cmd.ExecuteReader();

			//ReaderからDataTableを作成する。
			for (int i = 0; i < _reader.FieldCount; i++)
			{
				myDataColumn = new DataColumn(_reader.GetName(i), _reader.GetFieldType(i));
				myDataTable.Columns.Add(myDataColumn);
			}

			//ReaderからDaraRowを作成する
			while (_reader.Read())
			{
				myRow = myDataTable.NewRow();
				for (int i = 0; i < _reader.FieldCount; i++)
				{
					strColumnName = _reader.GetName(i);
					myRow[strColumnName] = _reader[strColumnName];
				}
				myDataTable.Rows.Add(myRow);
			}

			_reader.Close();
            _cmd.Dispose();

			return myDataTable;
		}
        #region Transaction

        //public void StartTrans()
        //{
        //    if (DBManager.ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]) > 75)
        //    {
        //        //LogWriter.writeLog(MethodBase.GetCurrentMethod(), "BeginTrans", 1);
        //    }

        //    _trans = _conn.BeginTransaction();
        //}

        //public void CommitTrans()
        //{
        //    if (DBManager.ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]) > 75)
        //    {
        //        //LogWriter.writeLog(MethodBase.GetCurrentMethod(), "CommitTrans", 1);
        //    }

        //    if (_trans.Connection.State != ConnectionState.Closed)
        //    {
        //        _trans.Commit();
        //        _trans.Dispose();
        //        _cmd.Dispose();
        //    }
        //}

        //public void RollbackTrans()
        //{
        //    if (DBManager.ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]) > 75)
        //    {
        //        //LogWriter.writeLog(MethodBase.GetCurrentMethod(), "CommitTrans", 1);
        //    }

        //    if (_trans.Connection.State != ConnectionState.Closed)
        //    {
        //        _trans.Rollback();
        //        _trans.Dispose();
        //        _cmd.Dispose();
        //    }
        //}
        #endregion

        #region Parameters
        public virtual void AddSqlParameter(string name, object dbtype, ParameterDirection direction, object value)
        {
            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + " is not implementened.");
        }

        public virtual void AddSqlParameter(string name, object dbtype, ParameterDirection direction, object value, int size)
        {
            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + " is not implementened.");
        }

        public virtual void AddSqlParameter(string name, DBTYPE dbtype, ParameterDirection direction, object value)
        {
            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + " is not implementened.");
        }

        public virtual void AddSqlParameter(string name, DBTYPE dbtype, ParameterDirection direction, object value, int size)
        {
            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + " is not implementened.");
        }

        public virtual Object GetSqlParameterValue(string name)
        {
            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + " is not implementened.");
        }

        public virtual void ClearSqlParameters()
        {
            throw new Exception(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + " is not implementened.");
        }
        #endregion
    }
    public enum DBTYPE
    {
        NUMBER = 1,
        VARCHAR2 = 2
    }
}
