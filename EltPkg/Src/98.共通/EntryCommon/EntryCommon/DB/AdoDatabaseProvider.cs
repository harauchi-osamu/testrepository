using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
//using System.Data.SqlClient;
using CommonClass.DB;

namespace EntryCommon
{
    public class AdoDatabaseProvider : IDisposable
    {
        /// <summary>Oracle接続情報</summary>
        public CommonClass.DB.DataBase Db { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="db"></param>
        public AdoDatabaseProvider(DataBase db)
        {
            Db = db;
        }

        /// <summary>
        /// パラメータを生成する
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(string paramName, DbType type, object value)
        {
            return CreateParameter(paramName, type, ParameterDirection.Input, value);
        }

        /// <summary>
        /// パラメータを生成する
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(string paramName, DbType type, ParameterDirection Direction, object value)
        {
            return Db.CreateSqlParameter(paramName, type, Direction, value);
        }

        /// <summary>
        /// パラメータを生成する
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(string paramName, DbType type, ParameterDirection Direction, object value, int size)
        {
            return Db.CreateSqlParameter(paramName, type, Direction, value, size);
        }

        /// <summary>
        /// INSERT/UPDATE
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns>影響を受けた行数</returns>
        public int CommandRun(string sql, List<IDbDataParameter> param, DbTransaction tran)
        {
            int retVal = 0;
            DbCommand cmd = null;
            try
            {
                cmd = Db.Factory.CreateCommand();
                cmd.Connection = Db.Conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Transaction = tran;
                foreach (var p in param)
                {
                    cmd.Parameters.Add(p);
                }
                retVal = cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
            return retVal;
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public DataTable SelectTable(string sql, List<IDbDataParameter> param, DbTransaction tran = null)
        {
            DataTable tbl = new DataTable();
            DbCommand cmd = null;
            DbDataReader reader = null;
            try
            {
                cmd = Db.Factory.CreateCommand();
                cmd.Connection = Db.Conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                if (tran != null)
                {
                    cmd.Transaction = tran;
                }
                foreach (var p in param)
                {
                    cmd.Parameters.Add(p);
                }
                reader = cmd.ExecuteReader();
                tbl.Load(reader);
            }
            finally
            {
                if ((reader != null) && !reader.IsClosed)
                {
                    reader.Close();
                    reader = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
            return tbl;
        }

        /// <summary>
        /// Procedure
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public void Procedure(string sql, List<IDbDataParameter> param)
        {
            DbCommand cmd = null;
            try
            {

                cmd = Db.Factory.CreateCommand();
                cmd.Connection = Db.Conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var p in param)
                {
                    cmd.Parameters.Add(p);
                }
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
        }

        /// <summary>
        /// リソース破棄
        /// </summary>
		public void Dispose()
        {
            // 本システム起動中はDB接続したままにするのでここではクローズしない
            //if (Dbinfo.Conn != null)
            //{
            //    if (Dbinfo.Conn.State != ConnectionState.Closed)
            //    {
            //        Dbinfo.Conn.Close();
            //    }
            //    Dbinfo.Conn.Dispose();
            //    Dbinfo.Conn = null;
            //}
        }
    }
}
