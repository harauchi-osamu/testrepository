using IIPCommonClass.Log;
using System;
using System.Data;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;

namespace IIPCommonClass.DB
{
    public class DBManager
	{
		#region メンバ

		protected  DataBase _db;
        protected  DataRow[] _drs;

        #endregion

        public DBManager(string name, string user, string pass)
        {

            _db = new OdpDatabase(name, user, pass);
        }

        #region プロパティ

        public  DataBase dbc
        {
            get { return _db; }
            set { _db = value; }
		}

        #endregion

        #region メソッド
        public  string ToStringNull(object obj)
        {
            return (obj == null || obj.Equals(DBNull.Value)) ? "" : Convert.ToString(obj).Trim();
        }

        public  string[] ToStringNull(object[] objs)
        {
            string[] res = new string[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                res[i] = ToStringNull(objs[i]);
            }
            return res;
        }

        public  int ToIntNull(object obj)
        {
            int res = 0;
            if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
			if (Int32.TryParse(obj.ToString(), out res)) { return res; }
			try { res = Convert.ToInt32(obj); }
			catch { res = 0; }
			return res;
        }

        public  long ToLongNull(object obj)
        {
			long res = 0;
			if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
			if (Int64.TryParse(obj.ToString(), out res)) { return res; }
			try { res = Convert.ToInt64(obj); }
			catch { res = 0; }
			return res;
        }

        public  decimal ToDecimalNull(object obj)
        {
			decimal res = 0;
			if ((obj == null) || string.IsNullOrEmpty(obj.ToString())) { return res; }
			if (Decimal.TryParse(obj.ToString(), out res)) { return res; }
			try { res = Convert.ToDecimal(obj); }
			catch { res = 0; }
			return res;
		}
        #endregion

        #region 共通業務設定取得

        /// <summary>
        /// <summary>
        /// 業務設定マスタデータ取得（View）
        /// </summary>
        /// <param name="get_column"></param>
        /// <returns></returns>
        public  DataRow GetGymSetMstData(int gymno)
        {
            DataRow dr = null;

            _drs = ExecuteRows(TBL_GYM_SETTING.GetSelectQuery(gymno), TBL_GYM_SETTING.TABLE_NAME);

            if (_drs.Length == 1)
            {
                dr = _drs[0];
            }

            return dr;
        }

        /// <summary>
        /// <summary>
        /// 業務設定マスタデータ取得（View）
        /// </summary>
        /// <param name="get_column"></param>
        /// <returns></returns>
        public  DataRow GetGymSetMstDataView(int gymno)
        {
            DataRow dr = null;

            _drs = ExecuteRows(V_GYM_SETTING.GetSelectQuery(gymno), V_GYM_SETTING.TABLE_NAME);

            if (_drs.Length == 1)
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
        public  int Execute(string _strQuery, string tableName)
		{
			try
			{
				dbc.NewCommand(_strQuery, CommandType.Text, ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]));
				return dbc.Execute();
			}
			catch (Exception ex)
			{
				IniFileAccess.ShowMsgBox("E0002", tableName);
                ProcessLog.getInstance().writeLog(ex.Message);
				throw ex;
			}
		}

        /// <summary>
        /// SQL実行                                                                                                     
        /// </summary>
        /// <param name="str_query"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public  DataRow[] ExecuteRows(string str_query, string tableName)
        {
            try
            {
                dbc.NewCommand(str_query, CommandType.Text, ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]));
                _drs = dbc.ExecuteSelect(ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]));
            }
            catch (Exception ex)
            {
                IniFileAccess.ShowMsgBox("E0002", tableName);
                ProcessLog.getInstance().writeLog(ex.Message);
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
        public  bool ExecuteBool(string str_query, string tableName)
        {
            try
            {
                dbc.NewCommand(str_query, CommandType.Text, ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]));
                _drs = dbc.ExecuteSelect(ToIntNull(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]));
                if (_drs.Length > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                IniFileAccess.ShowMsgBox("E0002", tableName);
                ProcessLog.getInstance().writeLog(ex.Message);
                throw ex;
            }
            return false;
        }

        #endregion

    }
}
