using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;

namespace CorrectInput
{
    public class EntryDataUpdater
    {
        protected Controller _ctl;
        protected ItemManager _itemMgr = null;
        public EntryCommonFormBase form { get; set; } = null;

        protected EntryController _econ { get { return _itemMgr.EntController; } }
        protected EntryDspControl _dcon { get { return _itemMgr.DspControl; } }
        protected EntryImageHandler eiHandler { get { return _itemMgr.ImageHandler; } }
        protected EntryInputChecker eiChecker { get { return _itemMgr.Checker; } }
        protected EntryDataUpdater edUpdater { get { return _itemMgr.Updater; } }
        protected MeisaiInfo _curMei { get { return _itemMgr.CurBat.CurMei; } }

        protected DateTime _startTime;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryDataUpdater(Controller ctl)
        {
            _ctl = ctl;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        /// <summary>
        /// 明細開始処理
        /// </summary>
        public void UpdateMeisaiToStart()
        {
            // 処理開始時刻
            _startTime = DateTime.Now;
        }

        /// <summary>
        /// 明細終了処理
        /// </summary>
        public void UpdateMeisaiToEnd(MeisaiInfo mei)
        {
            // オートチェックONの場合はオペレーター情報を更新しない
            // ベリファイアンマッチの場合は明細画面を開くのでオペレーター情報を更新する

            // 入力開始時からの経過時間を計算
            TimeSpan ts = DateTime.Now - _startTime;
            int addMilSecond = DBConvert.ToIntNull(Math.Round(ts.TotalSeconds * 1000));
            if (_itemMgr.EntParams.IsAutoExec)
            {
                addMilSecond = 0;
            }

            // オペレーター情報更新
            if (_ctl.IsKanryouTeisei)
            {
                // ********************
                // 完了訂正
                // ********************
                // フラグが立っていない場合 かつ オートチェックでない場合にオペレータ実績を更新する
                foreach (TBL_TRITEM item in mei.tritems.Values)
                {
                    // 手入力項目のみオペレーター実績を更新
                    if (_econ.IsNotInputItem(item._ITEM_ID)) { continue; }

                    // 完了訂正の未完了データはＤＢ登録しない
                    if (_ctl.IsReadOnlyItemId(item._ITEM_ID)) { continue; }

                    if (item.m_C_STIME == 0)
                    {
                        // 初回のみ初期化
                        item.m_C_STIME = DBConvert.ToIntNull(_startTime.ToString("HHmmssfff"));
                        item.m_C_YMD = AplInfo.OpDate();
                    }
                    if (!AplInfo.OP_ID.Equals(item.m_C_OPENO))
                    {
                        // オペレーターが変わったら初期化
                        item.m_C_OPENO = AplInfo.OP_ID;
                        item.m_C_TIME = addMilSecond;
                    }
                    else
                    {
                        // 同一オペレーターは時間加算
                        item.m_C_TIME += addMilSecond;
                    }
                    // 終了時刻は必ず更新する
                    item.m_C_TERM = AplInfo.TermNo;
                    item.m_C_ETIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
                }
            }
            else if (_itemMgr.DspParams.IsEntryExec)
            {
                // ********************
                // エントリー
                // ********************
                // フラグが立っていない場合 かつ オートチェックでない場合にオペレータ実績を更新する
                foreach (TBL_TRITEM item in mei.tritems.Values)
                {
                    // 手入力項目のみオペレーター実績を更新
                    if (_econ.IsNotInputItem(item._ITEM_ID)) { continue; }

                    if (item.m_E_STIME == 0)
                    {
                        // 初回のみ初期化
                        item.m_E_STIME = DBConvert.ToIntNull(_startTime.ToString("HHmmssfff"));
                        item.m_E_YMD = AplInfo.OpDate();
                    }

                    // 自動エントリはオペレーターを設定しない
                    if (!_itemMgr.EntParams.IsAutoEntryExec)
                    {
                        if (!AplInfo.OP_ID.Equals(item.m_E_OPENO))
                        {
                            // オペレーターが変わったら初期化
                            item.m_E_OPENO = AplInfo.OP_ID;
                            item.m_E_TIME = addMilSecond;
                        }
                        else
                        {
                            // 同一オペレーターは時間加算
                            item.m_E_TIME += addMilSecond;
                        }
                    }
                    // 終了時刻は必ず更新する
                    item.m_E_TERM = AplInfo.TermNo;
                    item.m_E_ETIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
                }
            }
            else if (_itemMgr.DspParams.IsVerifyExec)
            {
                // ********************
                // ベリファイ
                // ********************
                // フラグが立っていない場合 かつ オートチェックでない場合にオペレータ実績を更新する
                foreach (TBL_TRITEM item in mei.tritems.Values)
                {
                    // 手入力項目のみオペレーター実績を更新
                    if (_econ.IsNotInputItem(item._ITEM_ID)) { continue; }

                    if (item.m_V_STIME == 0)
                    {
                        // 初回のみ初期化
                        item.m_V_STIME = DBConvert.ToIntNull(_startTime.ToString("HHmmssfff"));
                        item.m_V_YMD = AplInfo.OpDate();
                    }

                    // 自動ベリファイはオペレーターを設定しない
                    if (!_itemMgr.EntParams.IsAutoVerifyExec)
                    {
                        if (!AplInfo.OP_ID.Equals(item.m_V_OPENO))
                        {
                            // オペレーターが変わったら初期化
                            item.m_V_OPENO = AplInfo.OP_ID;
                            item.m_V_TIME = addMilSecond;
                        }
                        else
                        {
                            // 同一オペレーターは時間加算
                            item.m_V_TIME += addMilSecond;
                        }
                    }
                    // 終了時刻は必ず更新する
                    item.m_V_TERM = AplInfo.TermNo;
                    item.m_V_ETIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmssfff"));
                }
            }

            // TBL_TRITEM 更新
            // TBL_TRMEIIMG 更新
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("明細終了"), 1);
            UpdateTrItemOperator(mei);
            UpdateTrImage(mei);
        }

        /// <summary>
        /// TBL_TRITEM 更新
        /// </summary>
        /// <param name="ed"></param>
        /// <returns></returns>
        private bool UpdateTrItemOperator(MeisaiInfo mei)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    foreach (TBL_TRITEM item in mei.tritems.Values)
                    {
                        // ダミーはＤＢ登録しない
                        if (_econ.IsNotRegistItem(item._ITEM_ID)) { continue; }

                        // 完了訂正の未完了データはＤＢ登録しない
                        if (_ctl.IsReadOnlyItemId(item._ITEM_ID)) { continue; }

                        item.m_FIX_TRIGGER = TrItem.FixTrigger.補正エントリ;

                        // SQL生成
                        strSQL = item.GetUpdateQuery();

                        // 実行
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// TBL_TRMEIIMG 更新
        /// </summary>
        /// <param name="ed"></param>
        /// <returns></returns>
        private bool UpdateTrImage(MeisaiInfo mei)
        {
            // 完了訂正以外は処理不要
            if (!_ctl.IsKanryouTeisei) { return true; }

            // 持帰は処理不要
            if (_ctl.GymId == GymParam.GymId.持帰) { return true; }

            // 確定以外（保留）は処理不要
            if (!_itemMgr.EntParams.IsMeisaiKakutei) { return true; }

            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    // 持出アップロード状態が「０：未作成」以外なら「１：再作成対象」に状態を更新する
                    strSQL = SQLEntry.GetEntryBuaStsUpdate(mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);

                    // 実行
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// DB更新
        /// </summary>
        /// <returns></returns>
        public bool UpdateTransation(MeisaiInfo mei)
        {
            //別の箇所に移動
            //// すべての入力項目検証（サブルーチン）
            //bool isSuccess = true;
            //foreach (int itemid in _dcon.tbDspItems.Keys)
            //{
            //    if (!eiChecker.CheckTextBoxInput(itemid, _dcon.tbDspItems, true))
            //    {
            //        isSuccess = false;
            //        break;
            //    }
            //}

            //// サブルーチンエラー有無
            //if (!isSuccess)
            //{
            //    if (!string.IsNullOrEmpty(eiChecker.ErrMsg))
            //    {
            //        form.SetStatusMessage(eiChecker.ErrMsg);
            //        if (eiChecker.ErrItemId != EntryInputChecker.DEF_ERRID)
            //        {
            //            _econ.ChangeFocusedControl(eiChecker.ErrItemId);
            //        }
            //    }
            //    return false;
            //}

            // 各種DB更新
            return edUpdater.UpdateTrItems(mei);
        }

        /// <summary>
        /// アイテムを更新する
        /// </summary>
        /// <returns></returns>
        public bool UpdateTrItems(MeisaiInfo mei)
        {
            // ＤＢ更新処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // 訂正回数カウント
                    foreach (int itemid in _dcon.tbDspItems.Keys)
                    {
                        UpdateEntryCount(mei, itemid);
                    }

                    // TRITEM 更新
                    if (!UpdateData(mei, dbp, auto))
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 訂正回数カウント
        /// </summary>
        /// <param name="mei"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public bool UpdateEntryCount(MeisaiInfo mei, int itemid)
        {
            TextBox tb = _econ.GetTextBox(itemid);
            TBL_DSP_ITEM di = _econ.GetDispItem(itemid);
            if (tb == null) { return false; }
            if (di == null) { return false; }
            if (_econ.IsNotInputItem(itemid)) { return false; }

            // BAT_TRITEM が存在しない場合は新規なので変更あり
            if (!mei.tritems_org.ContainsKey(itemid))
            {
                // 進捗ツール用の統計
                _itemMgr.EntParams.UpdateCount++;
                _itemMgr.EntParams.IsBatchUpdate = true;
                return true;
            }

            // オリジナルデータと比較
            string orgdata = _econ.GetOriginalData(mei, itemid);
            string indata = tb.Text;
            orgdata = CommonUtil.EditTrDataItem(orgdata, di);
            indata = CommonUtil.EditTrDataItem(indata, di);

            // 何か変更があるなら訂正回数を増やす
            if (!orgdata.Equals(indata))
            {
                // 進捗ツール用の統計
                _itemMgr.EntParams.UpdateCount++;
                _itemMgr.EntParams.IsBatchUpdate = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// BAT_TRDATA, BAT_TRITEMを更新
        /// </summary>
        /// <returns></returns>
        private bool UpdateData(MeisaiInfo mei, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            SortedList<int, TBL_TRITEM> bat_tritems = mei.tritems;
            try
            {
                // BAT_TRITEM 更新
                foreach (KeyValuePair<int, TextBox> keyVal in _dcon.tbDspItems)
                {
                    int itemid = keyVal.Key;
                    string itemname = keyVal.Value.Name;
                    //string tag = DBConvert.ToStringNull(keyVal.Value.Tag);

                    // ダミーはＤＢ登録しない
                    if (_econ.IsNotRegistItem(itemid)) { continue; }

                    // 完了訂正の未完了データはＤＢ登録しない
                    if (_ctl.IsReadOnlyItemId(itemid)) { continue; }
                    
                    if (bat_tritems.ContainsKey(itemid))
                    {
                        // 更新
                        if (!UpdateTrItem(mei, itemid, bat_tritems[itemid], dbp, auto)) { return false; }
                    }
                    else
                    {
                        // 登録
                        if (!InsertTrItem(mei, itemid, itemname, dbp, auto)) { return false; }
                    }
                }

                // BAT_TRITEM 更新（定数を全て登録）
                foreach (TBL_DSP_ITEM di in mei.CurDsp.dsp_items.Values)
                {
                    if (di.m_ITEM_TYPE != DspItem.ItemType.C) { continue; }
                    TBL_TRITEM bat_tritem = null;
                    if (bat_tritems.ContainsKey(di._ITEM_ID))
                    {
                        // 更新（定数）
                        bat_tritem = bat_tritems[di._ITEM_ID];
                        if (!UpdateTrItemConstValue(bat_tritem, di, dbp, auto)) { return false; ; }
                    }
                    else
                    {
                        // 登録（定数）
                        if (!InsertTrItem(mei, di._ITEM_ID, di.m_ITEM_DISPNAME, dbp, auto)) { return false; }
                    }
                }
            }
            catch (Exception ex)
            {
                auto.isCommitEnd = false;
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
            return true;
        }

        /// <summary>
        /// BAT_TRITEM 更新
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="trdata"></param>
        /// <param name="tritem"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        /// <returns></returns>
        private bool UpdateTrItem(MeisaiInfo mei, int itemid, TBL_TRITEM tritem, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            // 入力データ取得
            string tbdata = EditTextBoxValue(itemid);

            // 入力データ設定
            SetBatTrItem(mei, itemid, tbdata, tritem);

            //string strSQL = "";
            try
            {
                // TBL_TRITEM
                tritem.m_FIX_TRIGGER = TrItem.FixTrigger.補正エントリ;

                // 更新トリガーが２回実行されてしまうのでここでは UPDATE しない
                //strSQL = tritem.GetUpdateQuery();
                //dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception ex)
            {
                auto.isCommitEnd = false;
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
            return true;
        }

        /// <summary>
        /// BAT_TRITEM 登録
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="itemname"></param>
        /// <param name="trdata"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        /// <returns></returns>
        private bool InsertTrItem(MeisaiInfo mei, int itemid, string itemname, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            TBL_TRITEM tritem = new TBL_TRITEM(mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, itemid, AppInfo.Setting.SchemaBankCD);
            tritem.m_ITEM_NAME = itemname;

            // 入力データ取得
            string tbdata = EditTextBoxValue(itemid);

            // 入力データ設定
            SetBatTrItem(mei, itemid, tbdata, tritem);

            //string strSQL = "";
            try
            {
                // TBL_TRITEM
                tritem.m_FIX_TRIGGER = TrItem.FixTrigger.補正エントリ;

                // 更新トリガーが２回実行されてしまうのでここでは UPDATE しない
                //strSQL = tritem.GetInsertQuery();
                //dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception ex)
            {
                auto.isCommitEnd = false;
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                if (form != null) form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 入力データをＤＢ登録用に編集する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string EditTextBoxValue(int itemid)
        {
            string dspItemVal = "";
            TextBox tb = _econ.GetTextBox(itemid);
            if (tb != null)
            {
                dspItemVal = tb.Text.Replace(",", "");
            }

            // 入力データ編集
            TBL_DSP_ITEM di = _econ.GetDispItem(itemid);
            if (di == null) { return dspItemVal; }
            dspItemVal = CommonUtil.EditTrDataItem(dspItemVal, di);

            return dspItemVal;
        }

        /// <summary>
        /// 入力データを設定する
        /// </summary>
        /// <param name="mei"></param>
        /// <param name="itemid"></param>
        /// <param name="tbdata"></param>
        /// <param name="tritem"></param>
        private void SetBatTrItem(MeisaiInfo mei, int itemid, string tbdata, TBL_TRITEM tritem)
        {
            if (_itemMgr.DspParams.IsEntryExec)
            {
                // エントリ
                if (_itemMgr.EntParams.IsAutoEntryExec)
                {
                    // 自動エントリの場合は設定済みのため何もしない
                    return;
                }
                else
                {
                    tritem.m_ENT_DATA = tbdata;
                }
            }
            else if (_itemMgr.DspParams.IsVerifyExec)
            {
                // ベリファイ
                if (_itemMgr.EntParams.IsAutoVerifyExec)
                {
                    // 自動ベリファイの場合は設定済みのため何もしない
                    return;
                }
                else
                {
                    tritem.m_VFY_DATA = tbdata;
                }
            }

            // 最終工程
            if (_itemMgr.IsLastProcess || _ctl.IsKanryouTeisei)
            {
                // 確定値
                tritem.m_END_DATA = tbdata;
            }
        }

        /// <summary>
        /// BAT_TRITEM 更新（定数）
        /// </summary>
        /// <param name="tritem"></param>
        /// <param name="di"></param>
        /// <param name="dbp"></param>
        /// <param name="auto"></param>
        /// <returns></returns>
        private bool UpdateTrItemConstValue(TBL_TRITEM tritem, TBL_DSP_ITEM di, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            if (_itemMgr.DspParams.IsEntryExec)
            {
                // エントリ
                tritem.m_ENT_DATA = di.m_ITEM_DISPNAME;
            }
            else if(_itemMgr.DspParams.IsVerifyExec)
            {
                // ベリファイ
                tritem.m_VFY_DATA = di.m_ITEM_DISPNAME;
            }

            // 最終工程
            if (_itemMgr.IsLastProcess || _ctl.IsKanryouTeisei)
            {
                tritem.m_END_DATA = di.m_ITEM_DISPNAME;
            }

            // 更新トリガーが２回実行されてしまうのでここでは UPDATE しない
            //string strSQL = "";
            //try
            //{
            //    // TBL_TRITEM
            //    strSQL = tritem.GetUpdateQuery();
            //    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
            //}
            //catch (Exception ex)
            //{
            //    auto.isCommitEnd = false;
            //    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
            //    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            //    form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// 画面ＩＤを決定した後の TRITEM 再生成（削除、追加）
        /// </summary>
        /// <returns></returns>
        public bool DspUpdate(MeisaiInfo mei, int dspid, int BillCode, string BillName)
        {
            string msg = "他のユーザーがエントリ中のため画面種類を変更できません。\n";
                   msg += "しばらくしてから再度実行してください。";

            string strSQL = SQLEntry.GetEntryHoseiStatusSelect(
                mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, _ctl.HoseiInputMode, AppInfo.Setting.SchemaBankCD);

            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                DataTable tblHosei = null;
                DataTable tblMei = null;
                DataTable tblItem = null;
                DataTable tblDspItem = null;
                try
                {
                    SortedDictionary<int, TBL_TRITEM> curItems = new SortedDictionary<int, TBL_TRITEM>();
                    SortedDictionary<int, TBL_DSP_ITEM> newItems = new SortedDictionary<int, TBL_DSP_ITEM>();

                    // レコード取得できたら他のユーザーが入力中
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    if (tbl.Rows.Count > 0)
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        auto.isCommitEnd = false;
                        auto.Trans.Rollback();
                        // メッセージ表示
                        ComMessageMgr.MessageWarning(msg);
                        return false;
                    }

                    // 排他制御
                    {
                        // TBL_HOSEI_STATUS
                        strSQL = TBL_HOSEI_STATUS.GetSelectQuery(
                            mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, mei.hosei_status._HOSEI_INPTMODE, AppInfo.Setting.SchemaBankCD);
                        strSQL += DBConvert.QUERY_LOCK;
                        tblHosei = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);

                        // TBL_TRMEI
                        strSQL = TBL_TRMEI.GetSelectQuery(
                            mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                        strSQL += DBConvert.QUERY_LOCK;
                        tblMei = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);

                        // TBL_TRITEM
                        strSQL = TBL_TRITEM.GetSelectQuery(
                            mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                        strSQL += DBConvert.QUERY_LOCK;
                        tblItem = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                        for (int i = 0; i < tblItem.Rows.Count; i++)
                        {
                            TBL_TRITEM data = new TBL_TRITEM(tblItem.Rows[i], AppInfo.Setting.SchemaBankCD);
                            curItems.Add(data._ITEM_ID, data);
                        }

                        // TBL_DSP_ITEM（ロックしない）
                        strSQL = TBL_DSP_ITEM.GetSelectQuery(mei._GYM_ID, dspid, AppInfo.Setting.SchemaBankCD);
                        tblDspItem = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                        for (int i = 0; i < tblDspItem.Rows.Count; i++)
                        {
                            TBL_DSP_ITEM data = new TBL_DSP_ITEM(tblDspItem.Rows[i], AppInfo.Setting.SchemaBankCD);
                            newItems.Add(data._ITEM_ID, data);
                        }
                    }

                    // UPDATE・DELETE・INSERT
                    {
                        // TRITEM にない ITEM_ID を追加する
                        foreach (TBL_DSP_ITEM newItem in newItems.Values)
                        {
                            if (curItems.ContainsKey(newItem._ITEM_ID)) { continue; }
                            if (_econ.IsNotRegistItem(newItem._ITEM_ID)) { continue; }

                            // TRITEM に INSERT する
                            TBL_TRITEM addItem = new TBL_TRITEM(
                                mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, newItem._ITEM_ID, AppInfo.Setting.SchemaBankCD);
                            addItem.m_ITEM_NAME = newItem.m_ITEM_DISPNAME;
                            addItem.m_FIX_TRIGGER = TrItem.FixTrigger.補正エントリ;
                            strSQL = addItem.GetInsertQuery();
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }

                        // DSP_ITEM にない ITEM_ID を削除する
                        foreach (TBL_TRITEM curItem in curItems.Values)
                        {
                            if (newItems.ContainsKey(curItem._ITEM_ID)) { continue; }

                            // TRITEM から DELETE する
                            curItem.m_FIX_TRIGGER = TrItem.FixTrigger.補正エントリ;
                            strSQL = curItem.GetDeleteQuery();
                            dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                        }

                        // 交換証券種類があれば更新
                        foreach (TBL_TRITEM Item in curItems.Values)
                        {
                            switch (Item._ITEM_ID)
                            {
                                case DspItem.ItemId.交換証券種類コード:
                                    if (_ctl.IsKanryouTeisei)
                                    {
                                        //完了訂正時はEND_DATAを更新
                                        Item.m_END_DATA = BillCode.ToString("D3");
                                    }
                                    else
                                    {
                                        //完了訂正以外はOCR_DATAを更新
                                        Item.m_OCR_ENT_DATA = BillCode.ToString("D3");
                                        Item.m_OCR_VFY_DATA = BillCode.ToString("D3");
                                    }
                                    strSQL = Item.GetUpdateQuery();
                                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                                    break;
                                case DspItem.ItemId.交換証券種類名:
                                    if (_ctl.IsKanryouTeisei)
                                    {
                                        //完了訂正時はEND_DATAを更新
                                        Item.m_END_DATA = BillName;
                                    }
                                    else
                                    {
                                        //完了訂正以外はOCR_DATAを更新
                                        Item.m_OCR_ENT_DATA = BillName;
                                        Item.m_OCR_VFY_DATA = BillName;
                                    }
                                    strSQL = Item.GetUpdateQuery();
                                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                                    break;
                            }
                        }

                        // TRMEI 更新（画面ID）
                        strSQL = SQLEntry.GetEntryDspIdUpdate(
                            mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, dspid, AppInfo.Setting.SchemaBankCD);
                        dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                    }
                }
                catch (Exception ex)
                {
                    // 取得した行ロックを解除するためロールバック
                    // メッセージボックス表示前に実施
                    auto.isCommitEnd = false;
                    auto.Trans.Rollback();
                    // メッセージ表示
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    if (form != null) form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    return false;
                }
            }
            return true;
        }

    }
}
