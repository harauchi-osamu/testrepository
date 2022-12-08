using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace NCR.GeneralMainte
{
    public abstract class InitialParameter
    {
        /// <summary>
        /// マスタの抽出条件
        /// </summary>
        /// <returns></returns>
        public virtual string GetMasterQueryCondition()
        {
            // 継承先で実装
            return "";
        }

        /// <summary>
        /// 処理日の抽出クエリ
        /// </summary>
        /// <returns></returns>
        public virtual string GetOperationDateQuery()
        {
            // 継承先で実装
            return "";
        }

        /// <summary>
        /// 処理日の列名
        /// </summary>
        /// <returns></returns>
        public virtual string GetOperationDateColumnName()
        {
            // 継承先で実装
            return "";
        }

        /// <summary>
        /// レポート出力パス
        /// </summary>
        /// <returns></returns>
        public virtual string GetOutReportPath()
        {
            // 継承先で実装
            return "";
        }
    }
}
