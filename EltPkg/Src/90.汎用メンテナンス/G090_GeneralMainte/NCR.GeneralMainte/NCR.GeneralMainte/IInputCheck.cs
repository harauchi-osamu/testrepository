using System;
using System.Collections.Generic;
using System.Text;

namespace NCR.GeneralMainte
{
    abstract public class IInputCheck : ICheck
    {
        public virtual string SetColumnInfo(string ColumnName, string DefaultValue)
        {
            // 継承先で実装
            return "";
        }

        public virtual string IsOk(string ColumnName, string InputValue)
        {
            // 継承先で実装
            return "";
        }
    }
}
