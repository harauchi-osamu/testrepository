using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace NCR.GeneralMainte
{
    public interface IInitialParameter
    {
        /// <summary>
        /// マスタの抽出条件
        /// </summary>
        /// <returns></returns>
        string GetMasterQueryCondition();

        /// <summary>
        /// 処理日の抽出クエリ
        /// </summary>
        /// <returns></returns>
        string GetOperationDateQuery();

        /// <summary>
        /// 処理日の列名
        /// </summary>
        /// <returns></returns>
        string GetOperationDateColumnName();

        /// <summary>
        /// レポート出力パス
        /// </summary>
        /// <returns></returns>
        string GetOutReportPath();
    }
}
