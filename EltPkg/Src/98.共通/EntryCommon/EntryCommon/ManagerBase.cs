using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryCommon
{
    abstract public class ManagerBase
    {
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ManagerBase()
        {
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public virtual void FetchAllData() {; }

        /// <summary>
        /// データセットのデータをＤＢに保存
        /// <param name="fetch">true：保存＆取得, false：保存</param>
        /// </summary>
        public virtual void SaveAllData(bool fetch) {; }

	}
}
