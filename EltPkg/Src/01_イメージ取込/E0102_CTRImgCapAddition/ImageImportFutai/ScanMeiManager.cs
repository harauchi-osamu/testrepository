using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;
using System.IO;

namespace ImageImportFutai
{
    /// <summary>
    /// スキャン明細データ登録処理管理クラス
    /// </summary>
    public class ScanMeiManager
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private Dictionary<int, string> _AssignList;

        /// <summary>バッチ票インプット表示データ</summary>
        private TBL_SCAN_BATCH_CTL InputBatchData 
        { 
            get { return _itemMgr.InputBatchData; }
        }

        /// <summary>バッチ票インプット画面パラメータ</summary>
        private ItemManager.BatchInputParams InputParams
        {
            get { return _itemMgr.InputParams; }
        }

        /// <summary>バッチ票インプット画面パラメータ</summary>
        private ItemManager.ImageAssignParams AssignParams
        {
            get { return _itemMgr.AssignParams; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScanMeiManager(Controller ctl, Dictionary<int, string> AssignList)
        {
            _ctl = ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            _AssignList = AssignList;
        }

        /// <summary>
        /// スキャン明細データ登録処理
        /// </summary>
        public bool ScanMeiDataInput()
        {
            // 登録処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // 対象スキャン明細の削除
                    if (!_itemMgr.DeleteScan_MeiData(dbp, Tran))
                    {
                        //エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("スキャン明細の削除に失敗しました");
                        return false;
                    }
                    // 登録処理
                    foreach (KeyValuePair<int, string> Data in _AssignList)
                    {
                        if (!InsScanMei(Data.Value, Data.Key, dbp, Tran))
                        {
                            //エラーを表示して画面に戻る。
                            CommonClass.ComMessageMgr.MessageWarning("スキャン明細の登録に失敗しました");
                            return false;
                        }
                    }

                    // コミット
                    Tran.Trans.Commit();
                }
                catch (Exception ex)
                {
                    // ロールバック
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    //エラーを表示して画面に戻る。
                    CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// スキャン明細
        /// </summary>
        private bool InsScanMei(string fileName, int imgkbn, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // スキャン明細登録データの作成
            TBL_SCAN_MEI InsScnaMei = new TBL_SCAN_MEI(fileName, AssignParams.GymDate);
            InsScnaMei.m_INPUT_ROUTE = (int)InputParams.TargetRoute;    // 取込ルート
            InsScnaMei.m_BATCH_FOLDER_NAME = InputParams.TargetBatchFolderName; // バッチフォルダ名
            InsScnaMei.m_BATCH_UCHI_RENBAN = AssignParams.CurrentDetail;    // バッチ内連番
            InsScnaMei.m_IMG_KBN = imgkbn;    // 表裏等の別
            InsScnaMei.m_I_TERM = NCR.Terminal.Number.ToString();    // 入力端末
            InsScnaMei.m_I_OPENO = NCR.Operator.UserID;    // 入力オペレーター番号
            InsScnaMei.m_I_YMD = int.Parse(DateTime.Now.ToString("yyyyMMdd"));    // 入力日付
            InsScnaMei.m_I_TIME = int.Parse(DateTime.Now.ToString("HHmmss"));    // 入力時間

            return _itemMgr.InsertScan_MeiData(InsScnaMei, dbp, Tran);
        }

        /// <summary>
        /// 指定スキャン明細の削除処理
        /// </summary>
        public static bool ScanMeiDelete(ItemManager itemMgr)
        {
            // 登録処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // 対象スキャン明細の削除
                    if (!itemMgr.DeleteScan_MeiData(dbp, Tran))
                    {
                        //エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("スキャン明細の削除に失敗しました");
                        return false;
                    }

                    // 対象スキャン明細以降の連番を更新
                    if (!itemMgr.UpdateScan_MeiData(dbp, Tran))
                    {
                        //エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("スキャン明細の登録に失敗しました");
                        return false;
                    }

                    // コミット
                    Tran.Trans.Commit();
                }
                catch (Exception ex)
                {
                    // ロールバック
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    //エラーを表示して画面に戻る。
                    CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// スキャン明細を全削除処理
        /// </summary>
        public static bool ScanMeiAllDelete(ItemManager itemMgr)
        {
            // 登録処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    foreach (int renban in itemMgr.scanmei_renban)
                    {
                        itemMgr.AssignParams.CurrentDetail = renban;
                        // 対象スキャン明細の削除
                        if (!itemMgr.DeleteScan_MeiData(dbp, Tran))
                        {
                            //画面に戻る。
                            return false;
                        }
                    }

                    // コミット
                    Tran.Trans.Commit();
                    itemMgr.AssignParams.CurrentDetail = 0;
                }
                catch (Exception ex)
                {
                    // ロールバック
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

    }
}
