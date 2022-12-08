using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MainMenu
{
    class InputCheck : NCR.GeneralMainte.IInputCheck, NCR.GeneralMainte.ICheck
    {
        /// <summary>
        /// 入力補助設定
        /// </summary>
        public override string SetColumnInfo(string ColumnName, string DefaultValue)
        {
             return DefaultValue;
        }
    }
}
