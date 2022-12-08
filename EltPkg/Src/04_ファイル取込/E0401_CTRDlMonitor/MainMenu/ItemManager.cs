using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonClass;
using EntryCommon;

namespace MainMenu
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
        }

        /// <summary>
        /// 対象のファイルID/ファイル識別区分/銀行番号を取得
        /// </summary>
        public void GetFileIDDivIDBankCD(string Filename, out string file_id, out string file_divid, out string bankcd)
        {
            file_id = Filename.Substring(0, 5);
            file_divid = Filename.Substring(5, 3);
            bankcd = Filename.Substring(8, 4);
        }

        /// <summary>
        /// HULFT集信フォルダパスを取得
        /// </summary>
        public string HULFTReceiveRoot()
        {
            return NCR.Server.ReceiveRoot;
        }

    }
}
