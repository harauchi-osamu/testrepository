using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonTable.DB
{
    public class DBCommon
    {

        /// <summary>
        /// 一括登録のSQL取得
        /// </summary>
        /// <returns></returns>
        public static string GetBatchInsertSQL(IEnumerable<string> list)
        {
            // 更新用テーブルに一発で入れる（マルチテーブルインサート：Oracle仕様）
            // マルチテーブルインサートでも遅いようなら、CSVファイル作成して SQL*Loader でバルクインサートするのが最速らしい（マルチインサートの６倍くらい）
            // SQLServer なら DataTable から直接バルクインサートできる

            string strSQL = string.Format("INSERT ALL {0} SELECT * FROM DUAL ", string.Join("", list));
            return strSQL;
        }

        /// <summary>
        /// 一括登録のSQL取得
        /// </summary>
        /// <returns></returns>
        public static string GetDropTmpTableSQL(string tableName)
        {
            string strSQL = string.Format("DROP TABLE {0} ", tableName);
            return strSQL;

        }

    }
}
