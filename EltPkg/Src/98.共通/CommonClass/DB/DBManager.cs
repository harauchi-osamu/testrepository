using Common;
using CommonTable.DB;
using System;
using System.Data;
using System.Reflection;

namespace CommonClass.DB
{
    public class DBManager
	{
		#region メンバ

		protected static DataBase _db;
        private static DataBase _dbs1;
        private static DataBase _dbs2;
        private static DataBase _db_stored;
        protected static DataRow[] _drs;

        #endregion

        #region プロパティ

        public static DataBase dbc
        {
            get { return _db; }
            set { _db = value; }
		}

        public static DataBase dbs1
        {
            get { return _dbs1; }
            set { _dbs1 = value; }
        }

        public static DataBase dbs2
        {
            get { return _dbs2; }
            set { _dbs2 = value; }
        }

        public static DataBase dbc_stored
        {
            get { return _db_stored; }
            set { _db_stored = value; }
        }

        #endregion

        #region メソッド

        public static void Initialize(string name, string user, string pass)
        {
            _db = new OracleDatabase(name, user, pass);
        }

        public static void InitializeSql1(string name, string user, string pass)
        {
            _dbs1 = new OracleDatabase(name, user, pass);
        }

        public static void InitializeSql2(string name, string user, string pass)
        {
            _dbs2 = new OracleDatabase(name, user, pass);
        }

        public static void Initialize_stored(string name, string user, string pass)
        {
            _db_stored = new OracleDatabase(name, user, pass);
        }

        // CommonTable.DB.DBConvert にも同じのがあるのでそっちを使うようにする↓

        //public static string ToStringNull(object obj)
        //{
        //    return (obj == null || obj.Equals(DBNull.Value)) ? "" : Convert.ToString(obj);
        //}

        //public static string[] ToStringNull(object[] objs)
        //{
        //    string[] res = new string[objs.Length];
        //    for (int i = 0; i < objs.Length; i++)
        //    {
        //        res[i] = ToStringNull(objs[i]);
        //    }
        //    return res;
        //}

        //public static int ToIntNull(object obj)
        //{
        //    int res = 0;
        //    if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
        //    if (Int32.TryParse(obj.ToString(), out res)) { return res; }
        //    try { res = Convert.ToInt32(obj); }
        //    catch { res = 0; }
        //    return res;
        //}

        //public static long ToLongNull(object obj)
        //{
        //    long res = 0;
        //    if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
        //    if (Int64.TryParse(obj.ToString(), out res)) { return res; }
        //    try { res = Convert.ToInt64(obj); }
        //    catch { res = 0; }
        //    return res;
        //}

        //public static decimal ToDecimalNull(object obj)
        //{
        //    decimal res = 0;
        //    if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
        //    if (Decimal.TryParse(obj.ToString(), out res)) { return res; }
        //    try { res = Convert.ToDecimal(obj); }
        //    catch { res = 0; }
        //    return res;
        //}

        //public static bool ToBoolNull(object obj)
        //{
        //    string str = ToStringNull(obj);
        //    if (string.IsNullOrEmpty(str) || str.Equals("0") || str.ToLower().Equals("false"))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        // CommonTable.DB.DBConvert にも同じのがあるのでそっちを使うようにする↑

        #endregion

        #region 共通業務設定取得

        /// <summary>
        /// <summary>
        /// 業務日付業務設定マスタデータ取得
        /// </summary>
        /// <param name="get_column"></param>
        /// <returns></returns>
        public static DataRow GetOperationDate()
        {
            DataRow dr = null;
            string strSQL = TBL_OPERATION_DATE.GetSelectQuery();
            string tableName = TBL_OPERATION_DATE.TABLE_NAME;
            _drs = ExecuteRows(strSQL, tableName);
            if (_drs.Length > 0)
            {
                dr = _drs[0];
            }
            return dr;
        }

        /// <summary>
        /// <summary>
        /// 業務設定マスタデータ取得
        /// </summary>
        /// <param name="get_column"></param>
        /// <returns></returns>
        public static DataRow GetGymSetMstData(int gymid, int Schemabankcd)
        {
            DataRow dr = null;
            string strSQL = TBL_GYM_PARAM.GetSelectQuery(gymid, Schemabankcd);
            string tableName = TBL_GYM_PARAM.TABLE_NAME(Schemabankcd);
            _drs = ExecuteRows(strSQL, tableName);
            if (_drs.Length > 0)
            {
                dr = _drs[0];
            }
            return dr;
        }

        #endregion

        #region クエリ実行

        /// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="_strQuery"></param>
		/// <returns></returns>
        public static int Execute(string _strQuery, string tableName)
		{
			try
			{
				DBManager.dbc.NewCommand(_strQuery, CommandType.Text);
				return DBManager.dbc.Execute();
			}
			catch (Exception ex)
			{
                ComMessageMgr.MessageError(ComMessageMgr.E00006, tableName);
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.Message, 3);
				throw ex;
			}
		}

        /// <summary>
        /// SQL実行                                                                                                     
        /// </summary>
        /// <param name="str_query"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataRow[] ExecuteRows(string str_query, string tableName)
        {
            try
            {
                DBManager.dbc.NewCommand(str_query, CommandType.Text);
                _drs = DBManager.dbc.ExecuteSelect();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00006, tableName);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.Message, 3);
                throw ex;
            }
            return _drs;
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="str_query"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool ExecuteBool(string str_query, string tableName)
        {
            try
            {
                DBManager.dbc.NewCommand(str_query, CommandType.Text);
                _drs = DBManager.dbc.ExecuteSelect();
                if (_drs.Length > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00006, tableName);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.Message, 3);
                throw ex;
            }
            return false;
        }

        #endregion

    }
}
