using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
    public class TBL_COL_INFO
    {
        /// <summary>
        /// カラム名
        /// </summary>
        public string colName = "";
        /// <summary>
        /// データタイプ
        /// </summary>
        public string data_type = "";
        /// <summary>
        /// 値
        /// </summary>
        public string val = "";
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dr"></param>
        public TBL_COL_INFO(DataRow dr)
        {
            colName = DBConvert.ToStringNull(dr["COLUMN_NAME"].ToString());
            data_type =  DBConvert.ToStringNull(dr["DATA_TYPE"].ToString());
        }
    }
}
