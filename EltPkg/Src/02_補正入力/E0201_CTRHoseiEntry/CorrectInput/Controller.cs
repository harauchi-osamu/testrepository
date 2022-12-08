using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;

namespace CorrectInput
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerEntBase
    {
		private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        /// <summary>
        /// 完了訂正の入力可否チェック結果
        /// </summary>
        public enum ChkTeiseiType
        {
            OK,
            DeleteDataError,
            TeiseiError,
            IFError,
            ClearingDateError,
            DspReadOnly
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ctl"></param>
        public Controller(ControllerEntBase ctl)
        {
            this.SettingData = ctl.SettingData;
            this.MenuNumber = ctl.MenuNumber;
            this.GymId = ctl.GymId;
            this.HoseiItemMode = ctl.HoseiItemMode;
            this.HoseiInputMode = ctl.HoseiInputMode;
            this.IsKanryouTeisei = ctl.IsKanryouTeisei;
            this.IsRenzokuTeisei = ctl.IsRenzokuTeisei;
            this.IsDspReadOnly = ctl.IsDspReadOnly;
            this.ReadOnlyItemIdList = ctl.ReadOnlyItemIdList;

            this.OpeDate = ctl.OpeDate;
            this.ScanTerm = ctl.ScanTerm;
            this.BatId = ctl.BatId;
            this.DetailsNo = ctl.DetailsNo;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            StringBuilder param = new StringBuilder();
            string sep = "";

            // LabelFontName
            if (string.IsNullOrEmpty(AppConfig.LabelFontName))
            {
                param.Append(sep);
                param.Append("フォント名（ラベル）");
                sep = COMMA;
            }
            // TextFontName
            if (string.IsNullOrEmpty(AppConfig.TextFontName))
            {
                param.Append(sep);
                param.Append("フォント名（テキストボックス）");
                sep = COMMA;
            }

            // KingakuProofCheck
            // キー未定義でもエラーにならない
            bool bln1 = AppConfig.KingakuProofCheck;

            // DefaultImage
            if (string.IsNullOrEmpty(AppConfig.DefaultImage))
            {
                param.Append(sep);
                param.Append("デフォルトイメージ画像");
                sep = COMMA;
            }
            //// MeiListBackColor
            //if (string.IsNullOrEmpty(AppConfig.MeiListBackColor))
            //{
            //    param.Append(sep);
            //    param.Append("明細一覧の背景色設定");
            //    sep = COMMA;
            //}

            if (!string.IsNullOrEmpty(param.ToString()))
            {
                ComMessageMgr.MessageError(ComMessageMgr.E01001, param.ToString());
                return false;
            }
            return true;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 管理クラスを設定する
        /// </summary>
        /// <param name="mst"></param>
        /// <param name="item"></param>
        public override void SetManager(MasterManager mst, ManagerBase item)
		{
			base.SetManager(mst, item);
			_masterMgr = MasterMgr;
			_itemMgr = (ItemManager)ItemMgr;
		}

		/// <summary>
		/// フォームを設定する
		/// </summary>
		/// <param name="form"></param>
		public void SetControlForm(Controller ctl, EntryCommonFormBase form)
		{
            _itemMgr.Controller = ctl;
            _itemMgr.DspControl.form = form;
            _itemMgr.ImageHandler.form = form;
            _itemMgr.Checker.form = form;
            _itemMgr.Updater.form = form;
        }

        /// <summary>
        /// 銀行名を取得する
        /// </summary>
        /// <param name="bkno"></param>
        /// <returns></returns>
        public string GetBankName(int bkno)
        {
            if (!_itemMgr.mst_banks.ContainsKey(bkno)) { return ""; }
            return _itemMgr.mst_banks[bkno].m_BK_NAME_KANJI;
        }

        /// <summary>
        /// 支店名を取得する
        /// </summary>
        /// <param name="brno"></param>
        /// <returns></returns>
        public string GetBranchName(int brno)
        {
            if (!_itemMgr.mst_branches.ContainsKey(brno)) { return ""; }
            return _itemMgr.mst_branches[brno].m_BR_NAME_KANJI;
        }

        /// <summary>
        /// 証券種類名を取得する
        /// </summary>
        /// <param name="billcd"></param>
        /// <returns></returns>
        public string GetBillName(int billcd)
        {
            if (!_itemMgr.mst_bills.ContainsKey(billcd)) { return ""; }
            return _itemMgr.mst_bills[billcd].m_STOCK_NAME;
        }

        /// <summary>
        /// 手形種類名を取得する
        /// </summary>
        /// <param name="shuruicd"></param>
        /// <returns></returns>
        public string GetShuruiName(int shuruicd)
        {
            if (!_itemMgr.mst_syuruimfs.ContainsKey(shuruicd)) { return ""; }
            return _itemMgr.mst_syuruimfs[shuruicd].m_SYURUI_NAME;
        }

        /// <summary>
        /// 読替マスタから新支店番号を取得する
        /// </summary>
        /// <param name="shuruicd"></param>
        /// <returns></returns>
        /// <remarks>未使用</remarks>
        public string GetYomikaeBrNo(int brno, int kozaNo)
        {
            string key = CommonUtil.GenerateKey(brno, kozaNo);
            if (!_itemMgr.mst_changes.ContainsKey(key)) { return brno.ToString(Const.BR_NO_LEN_STR); }
            return _itemMgr.mst_changes[key].m_NEW_BR_NO.ToString(Const.BR_NO_LEN_STR);
        }

        /// <summary>
        /// 読替マスタから新口座番号を取得する
        /// </summary>
        /// <param name="brno"></param>
        /// <param name="kozaNo"></param>
        /// <param name="kozalen">口座番号の長さ</param>
        /// <returns></returns>
        /// <remarks>未使用</remarks>
        public string GetYomikaeKozaName(int brno, int kozaNo, int kozalen)
        {
            string key = CommonUtil.GenerateKey(brno, kozaNo);
            if (!_itemMgr.mst_changes.ContainsKey(key)) { return CommonUtil.PadLeft(kozaNo.ToString(), kozalen, "0"); }
            return CommonUtil.PadLeft(_itemMgr.mst_changes[key].m_NEW_ACCOUNT_NO.ToString(), kozalen, "0");
        }

        /// <summary>
        /// エントリー前にバッチステータスを変更する
        /// </summary>
        /// <param name="tbm"></param>
        /// <returns></returns>
        public bool PrepareEntryStatus(TBL_HOSEI_STATUS curSts, BatchListForm.ExecMode execMode)
        {
            // 参照モードは処理不要
            if (this.IsDspReadOnly) { return true; }

            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                try
                {
                    // バッチ管理の更新
                    if (!UpdateBatManage(curSts, execMode, out string ErrMsg, dbp, auto))
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        auto.isCommitEnd = false;
                        auto.Trans.Rollback();
                        // 自動配信以外の場合はエラーメッセージ表示
                        if (!_itemMgr.DspParams.IsAutoReceiveBatch) { ComMessageMgr.MessageWarning(ErrMsg); }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // 取得した行ロックを解除するためロールバック
                    // メッセージボックス表示前に実施
                    auto.isCommitEnd = false;
                    auto.Trans.Rollback();
                    // 自動配信以外の場合はエラーメッセージ表示
                    if (!_itemMgr.DspParams.IsAutoReceiveBatch) { ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message); }
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ステータスを処理中に更新する
        /// </summary>
        /// <returns></returns>
        private bool UpdateBatManage(TBL_HOSEI_STATUS curSts, BatchListForm.ExecMode execMode, out string ErrMsg, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            // 初期化
            ErrMsg = string.Empty;

            TBL_HOSEI_STATUS newSts = null;
            try
            {
                {
                    // 排他制御で対象データ取得
                    DataTable tbl = GetUpdateManageData(curSts, dbp, auto);

                    // データ取得できなかったら他のユーザーが処理中（行ロックエラー）
                    if (tbl == null)
                    {
                        auto.isCommitEnd = false;
                        ErrMsg = EntMessageMgr.ENT10002;
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), EntMessageMgr.ENT10002.Replace("\n", "1"), 1); // この箇所のログでは改行なし
                        return false;
                    }

                    // レコード取得できなかったら他のユーザーに更新された
                    if (tbl.Rows.Count < 1)
                    {
                        auto.isCommitEnd = false;
                        ErrMsg = EntMessageMgr.ENT10002;
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), EntMessageMgr.ENT10002.Replace("\n", "2"), 1); // この箇所のログでは改行なし
                        return false;
                    }

                    // 最新データ
                    newSts = new TBL_HOSEI_STATUS(tbl.Rows[0], AppInfo.Setting.SchemaBankCD);

                    // 最新の処理状態チェック
                    if (!ChkBatchStatusToStart(newSts))
                    {
                        auto.isCommitEnd = false;
                        ErrMsg = EntMessageMgr.ENT10002;
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), EntMessageMgr.ENT10002.Replace("\n", "3"), 1); // この箇所のログでは改行なし
                        return false;
                    }

                    // 処理状態が変わっていたら他のユーザーに更新された
                    if (curSts.m_INPT_STS != newSts.m_INPT_STS)
                    {
                        auto.isCommitEnd = false;
                        ErrMsg = EntMessageMgr.ENT10002;
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), EntMessageMgr.ENT10002.Replace("\n", "4"), 1); // この箇所のログでは改行なし
                        return false;
                    }
                }

                // 取得データ検証
                if (execMode == BatchListForm.ExecMode.FuncVfy)
                {
                    // エントリーと同じオペレーターの場合はベリファイは行えない
                    // ベリファイ保留は同じオペレーターでも可能
                    if ((newSts.m_INPT_STS == HoseiStatus.InputStatus.ベリファイ待) && (newSts.m_E_OPENO == AplInfo.OP_ID))
                    {
                        auto.isCommitEnd = false;
                        ErrMsg = "ベリファイは、エントリーを行ったユーザー以外で行ってください。";
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ベリファイは、エントリーを行ったユーザー以外で行ってください。", 1);
                        return false;
                    }
                }

                // UPDATE
                string strSQL = "";
                {
                    // 端末番号・オペIDを設定
                    newSts.m_TMNO = AplInfo.TermNo;
                    newSts.m_OPENO = AplInfo.OP_ID;

                    // 状態の更新を行う
                    newSts.m_INPT_STS = GetBatchStatusToStart(newSts);

                    // SQL生成
                    strSQL = newSts.GetUpdateQuery();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("エントリ開始 [{0}]", strSQL), 1);

                    // SQL実行
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);

                    // 呼び出し元の BAt_MANAGE にも反映する
                    curSts.m_INPT_STS = newSts.m_INPT_STS;
                }
            }
            catch (Exception ex)
            {
                // 呼び元で処理させる
                throw ex;
                //auto.isCommitEnd = false;
                //ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                //LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                //return false;
            }
            return true;
        }

        /// <summary>
        /// ステータスを更新するデータ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetUpdateManageData(TBL_HOSEI_STATUS curSts, AdoDatabaseProvider dbp, AdoAutoCommitTransaction auto)
        {
            // 排他制御
            string strSQL = "";
            strSQL = TBL_HOSEI_STATUS.GetSelectQuery(
                curSts._GYM_ID, curSts._OPERATION_DATE, curSts._SCAN_TERM, curSts._BAT_ID, curSts._DETAILS_NO, curSts._HOSEI_INPTMODE, AppInfo.Setting.SchemaBankCD);
            strSQL += DBConvert.QUERY_LOCK;

            try
            {
                // データ取得
                return dbp.SelectTable(strSQL, new List<IDbDataParameter>(), auto.Trans);
            }
            catch (Exception ex)
            {
                // 行ロックの取得失敗等
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return null;
            }
        }

        /// <summary>
        /// 明細終了
        /// </summary>
        public bool TerminateEntryStatus(MeisaiInfo mei, bool isMeisaiCancel = false)
        {
            // 参照モードは処理不要
            if (this.IsDspReadOnly) { return true; }

            // ステータス更新
            mei.hosei_status.m_INPT_STS = GetBatchStatusToEnd(_itemMgr.EntParams.ExecFunc, mei);
            if (isMeisaiCancel)
            {
                // 明細終了(端末番号はクリア)
                //mei.hosei_status.m_TMNO = _itemMgr.EntParams.SaveTerm;
                mei.hosei_status.m_OPENO = _itemMgr.EntParams.SaveOpeId;
                mei.hosei_status.m_TMNO = string.Empty;
            }
            else
            {
                // 明細確定(端末番号はクリア)
                //mei.hosei_status.m_TMNO = AplInfo.TermNo;
                mei.hosei_status.m_OPENO = AplInfo.OP_ID;
                mei.hosei_status.m_TMNO = string.Empty;
                if (_itemMgr.DspParams.IsEntryExec)
                {
                    mei.hosei_status.m_E_OPENO = AplInfo.OP_ID;
                }
            }

            // TBL_HOSEI_STATUS 更新
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("バッチ終了"), 1);
            return UpdateHoseiStatus(mei.hosei_status);
        }

        /// <summary>
        /// エントリー開始のためのステータスチェック
        /// </summary>
        /// <param name="sts"></param>
        /// <returns></returns>
        public bool ChkBatchStatusToStart(TBL_HOSEI_STATUS sts)
        {
            bool ChkStatus = false;
            switch (sts.m_INPT_STS)
            {
                case HoseiStatus.InputStatus.エントリ待:
                case HoseiStatus.InputStatus.エントリ保留:
                case HoseiStatus.InputStatus.ベリファイ待:
                case HoseiStatus.InputStatus.ベリファイ保留:
                case HoseiStatus.InputStatus.完了:
                case HoseiStatus.InputStatus.完了訂正保留:
                    // 処理待の場合はOK
                    ChkStatus = true;
                    break;
                case HoseiStatus.InputStatus.エントリ中:
                case HoseiStatus.InputStatus.ベリファイ中:
                case HoseiStatus.InputStatus.完了訂正中:
                default:
                    // 処理中の場合はNG
                    ChkStatus = false;
                    break;
            }

            return ChkStatus;
        }

        /// <summary>
        /// エントリー開始のための区分・ステータス変更
        /// </summary>
        /// <param name="sts"></param>
        /// <returns></returns>
        public int GetBatchStatusToStart(TBL_HOSEI_STATUS sts)
        {
            // ステータスを保存する
            _itemMgr.EntParams.SaveInputSts = sts.m_INPT_STS;
            _itemMgr.EntParams.SaveOpeId = sts.m_OPENO;
            //_itemMgr.EntParams.SaveTerm = sts.m_TMNO;

            int inputsts = sts.m_INPT_STS;
            switch (inputsts)
            {
                case HoseiStatus.InputStatus.エントリ待:
                case HoseiStatus.InputStatus.エントリ保留:
                    inputsts = HoseiStatus.InputStatus.エントリ中;
                    break;
                case HoseiStatus.InputStatus.ベリファイ待:
                case HoseiStatus.InputStatus.ベリファイ保留:
                    inputsts = HoseiStatus.InputStatus.ベリファイ中;
                    break;
                case HoseiStatus.InputStatus.完了:
                case HoseiStatus.InputStatus.完了訂正保留:
                    inputsts = HoseiStatus.InputStatus.完了訂正中;
                    break;
                default:
                    break;
            }
            return inputsts;
        }

        /// <summary>
        /// エントリー終了のための区分・ステータス変更
        /// </summary>
        /// <param name="sts"></param>
        /// <returns></returns>
        public int GetBatchStatusToEnd(EntryFormBase.FuncType ftype, MeisaiInfo mei)
        {
            int inputsts = mei.hosei_status.m_INPT_STS;
            switch (inputsts)
            {
                case HoseiStatus.InputStatus.エントリ中:
                    switch (ftype)
                    {
                        case EntryFormBase.FuncType.終了:
                            inputsts = _itemMgr.EntParams.SaveInputSts;
                            break;
                        case EntryFormBase.FuncType.保留:
                            inputsts = HoseiStatus.InputStatus.エントリ保留;
                            break;
                        case EntryFormBase.FuncType.確定:
                            if (DBConvert.ToBoolNull(mei.CurDsp.hosei_param.m_VERY_MODE))
                            {
                                inputsts = HoseiStatus.InputStatus.ベリファイ待;
                            }
                            else
                            {
                                inputsts = HoseiStatus.InputStatus.完了;
                            }
                            break;
                    }
                    break;

                case HoseiStatus.InputStatus.ベリファイ中:
                    switch (ftype)
                    {
                        case EntryFormBase.FuncType.終了:
                            inputsts = _itemMgr.EntParams.SaveInputSts;
                            break;
                        case EntryFormBase.FuncType.保留:
                            inputsts = HoseiStatus.InputStatus.ベリファイ保留;
                            break;
                        case EntryFormBase.FuncType.確定:
                            inputsts = HoseiStatus.InputStatus.完了;
                            break;
                    }
                    break;

                case HoseiStatus.InputStatus.完了訂正中:
                    switch (ftype)
                    {
                        case EntryFormBase.FuncType.終了:
                            inputsts = _itemMgr.EntParams.SaveInputSts;
                            break;
                        case EntryFormBase.FuncType.保留:
                            inputsts = HoseiStatus.InputStatus.完了訂正保留;
                            break;
                        case EntryFormBase.FuncType.確定:
                            inputsts = HoseiStatus.InputStatus.完了;
                            break;
                    }
                    break;

                default:
                    break;
            }
            return inputsts;
        }

        /// <summary>
        /// TBL_HOSEI_STATUS 更新
        /// </summary>
        /// <param name="ed"></param>
        /// <returns></returns>
        private bool UpdateHoseiStatus(TBL_HOSEI_STATUS hsts)
        {
            // UPDATE実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoAutoCommitTransaction auto = new AdoAutoCommitTransaction(dbp))
            {
                string strSQL = "";
                try
                {
                    strSQL = hsts.GetUpdateQuery();
                    dbp.CommandRun(strSQL, new List<IDbDataParameter>(), auto.Trans);
                }
                catch (Exception ex)
                {
                    auto.isCommitEnd = false;
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 自動配信バッチを取得する
        /// </summary>
        /// <param name="dspMode"></param>
        /// <returns></returns>
        public TBL_HOSEI_STATUS GetAutoReceiveBatch(BatchListForm.ExecMode execMode, int gymid)
        {
            TBL_HOSEI_STATUS sts = null;
            if (this.IsRenzokuTeisei)
            {
                // 連続訂正
                sts = GetRenzokuTeiseiReceiveBatch(execMode, gymid);
            }
            else
            {
                // 通常自動配信
                sts = _itemMgr.GetAutoReceiveBatch(execMode, gymid, _itemMgr.IgnoreMeisaiList);
            }

            if (sts == null)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("{0} 自動配信データなし", this.IsRenzokuTeisei ? "連続訂正" : "通常"), 1);
                return null;
            }
            return sts;
        }

        /// <summary>
        /// 連続完了訂正の次情報を取得する
        /// </summary>
        /// <param name="dspMode"></param>
        /// <returns></returns>
        private TBL_HOSEI_STATUS GetRenzokuTeiseiReceiveBatch(BatchListForm.ExecMode execMode, int gymid)
        {
            // 現在のキー情報取得
            string key = CommonUtil.GenerateKey(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo);
            var curwk = _itemMgr.wk_Renzokulist.Where(x => x.Key == key);
            int seq = 0;
            if (curwk.Count() > 0)
            {
                // 現在のシーケンス設定(なければ先頭から)
                seq = curwk.First().Seq;
            }

            foreach(ItemManager.RenzokuTeiseiInfo teiseiInfo in _itemMgr.wk_Renzokulist.Where(x => x.Seq > seq).OrderBy(x => x.Seq))
            {
                // 現在のシーケンス以降のデータを順に取得

                // 各キー項目を設定
                this.OpeDate = teiseiInfo.wk_list._OPERATION_DATE;
                this.ScanTerm = teiseiInfo.wk_list._SCAN_TERM;
                this.BatId = teiseiInfo.wk_list._BAT_ID;
                this.DetailsNo = teiseiInfo.wk_list._DETAILS_NO;

                // 入力可能か確認
                SortedDictionary<int, TBL_HOSEI_STATUS> all_statuses = _itemMgr.GetHoseiStatuses(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo);
                if (all_statuses == null) { continue; }
                var comp_status = all_statuses.Values.Where(p => p._HOSEI_INPTMODE == this.HoseiInputMode);
                if (comp_status.Count() < 1) { continue; }
                TBL_HOSEI_STATUS hosei_status = comp_status.First();
                TBL_TRMEI trmei = _itemMgr.GetTrMei(new TBL_TRMEI(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo, AppInfo.Setting.SchemaBankCD));
                TBL_TRMEIIMG trimgF = _itemMgr.GetTrImg(trmei, TrMeiImg.ImgKbn.表);
                // 交換日取得
                TBL_TRITEM clearingDateItem = _itemMgr.GetTritem(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo, DspItem.ItemId.交換日);

                // 訂正可否チェック
                ChkTeiseiType chkTeisei = ChkEntryTeisei(this.GymId, this.HoseiInputMode, all_statuses, hosei_status, trmei, trimgF, clearingDateItem, false);
                switch (chkTeisei)
                {
                    case ChkTeiseiType.DeleteDataError:
                        // エラーの場合、次明細に移動
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("連続訂正不可(削除エラー) GYM={0}, OPE={1}, SCAN={2}, BAT={3}, DET={4}", this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo), 1);
                        continue;
                    case ChkTeiseiType.TeiseiError:
                        // エラーの場合、次明細に移動
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("連続訂正不可(訂正中) GYM={0}, OPE={1}, SCAN={2}, BAT={3}, DET={4}", this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo), 1);
                        continue;
                    case ChkTeiseiType.IFError:
                        // エラーの場合、次明細に移動
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("連続訂正不可(IF中) GYM={0}, OPE={1}, SCAN={2}, BAT={3}, DET={4}", this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo), 1);
                        continue;
                    case ChkTeiseiType.ClearingDateError:
                        // エラーの場合、次明細に移動
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("連続訂正不可(交換日過去日付) GYM={0}, OPE={1}, SCAN={2}, BAT={3}, DET={4}", this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo), 1);
                        continue;
                    case ChkTeiseiType.DspReadOnly:
                        // 読取専用の場合、次明細に移動
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("連続訂正不可(読取専用) GYM={0}, OPE={1}, SCAN={2}, BAT={3}, DET={4}", this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo), 1);
                        continue;
                    case ChkTeiseiType.OK:
                        this.IsDspReadOnly = false;
                        break;
                }

                switch (this.HoseiInputMode)
                {
                    case HoseiStatus.HoseiInputMode.交換尻:
                        // 補正未完了の ITEM_ID を取得
                        this.ReadOnlyItemIdList = _itemMgr.GetReadOnlyItemIdList(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo);
                        break;
                }

                // 初期化
                _itemMgr.EntParams.IsInitControl = false;
                _itemMgr.EntParams.IsInitImage = false;
                _itemMgr.EntParams.IsDeleteImage = true;

                // 訂正可能の場合は対象データを返して終了
                return hosei_status;
            }

            // 訂正可能データがない場合はnull
            return null;
        }

        /// <summary>
        /// エントリーデータを取得する
        /// </summary>
        /// <param name="sts"></param>
        /// <returns></returns>
        public MeisaiInfo GetNextEntryData(TBL_HOSEI_STATUS sts)
        {
            _itemMgr.SetCurrentBat(sts._GYM_ID, sts._OPERATION_DATE, sts._SCAN_TERM, sts._BAT_ID, sts._DETAILS_NO);
            _itemMgr.FetchTrDatas(_itemMgr.CurBat, sts);
            MeisaiInfo mei = _itemMgr.SetCurrentMeisai(sts._GYM_ID, sts._OPERATION_DATE, sts._SCAN_TERM, sts._BAT_ID, sts._DETAILS_NO);
            return mei;
        }

        /// <summary>
        /// 完了訂正を行う
        /// </summary>
        public void DoEntryTeisei()
        {
            SortedDictionary<int, TBL_HOSEI_STATUS> all_statuses = _itemMgr.GetHoseiStatuses(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo);
            if (all_statuses == null) { return; }
            var comp_status = all_statuses.Values.Where(p => p._HOSEI_INPTMODE == this.HoseiInputMode);
            if (comp_status.Count() < 1)
            {
                ComMessageMgr.MessageWarning(ComMessageMgr.W00001);
                return;
            }
            TBL_HOSEI_STATUS hosei_status = comp_status.First();
            TBL_TRMEI trmei = _itemMgr.GetTrMei(new TBL_TRMEI(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo, AppInfo.Setting.SchemaBankCD));
            TBL_TRMEIIMG trimgF = _itemMgr.GetTrImg(trmei, TrMeiImg.ImgKbn.表);
            // 交換日取得
            TBL_TRITEM clearingDateItem = _itemMgr.GetTritem(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo, DspItem.ItemId.交換日);

            // 画面パラメータ
            _itemMgr.DspParams.ExecMode = BatchListForm.ExecMode.Teisei;

            // 訂正可否チェック
            ChkTeiseiType chkTeisei = ChkEntryTeisei(this.GymId, this.HoseiInputMode, all_statuses, hosei_status, trmei, trimgF, clearingDateItem, true);
            switch (chkTeisei)
            {
                case ChkTeiseiType.DeleteDataError:
                case ChkTeiseiType.TeiseiError:
                case ChkTeiseiType.IFError:
                case ChkTeiseiType.ClearingDateError:
                    // エラーの場合は終了
                    return;
                case ChkTeiseiType.DspReadOnly:
                    // 読取専用の場合、ReadOnlyにtrueを設定
                    this.IsDspReadOnly = true;
                    break;
                case ChkTeiseiType.OK:
                    this.IsDspReadOnly = false;
                    break;
            }

            switch (this.HoseiInputMode)
            {
                case HoseiStatus.HoseiInputMode.交換尻:
                    // 補正未完了の ITEM_ID を取得
                    this.ReadOnlyItemIdList = _itemMgr.GetReadOnlyItemIdList(this.GymId, this.OpeDate, this.ScanTerm, this.BatId, this.DetailsNo);

                    break;
            }

            // エントリデータ取得
            MeisaiInfo mei = this.GetNextEntryData(hosei_status);

            // エントリ開始
            using (EntryController econ = new EntryController(this, null))
            {
                _itemMgr.DspParams.InitializeParams();
                _itemMgr.EntParams.InitializeParams();
                econ.SetEntryForm(_itemMgr.DspParams.ExecMode);
                econ.StartControl(mei);
            }
        }

        /// <summary>
        /// 連続完了訂正を行う
        /// </summary>
        public void DoRenzokuTeisei()
        {
            // 自動配信設定をありとする
            _itemMgr.DspParams.IsAutoReceiveBatch = true;

            // 対象データの取得
            if (!_itemMgr.Fetch_renzokulist())
            {
                return;
            }

            // 画面パラメータ
            _itemMgr.DspParams.ExecMode = BatchListForm.ExecMode.Teisei;

            // 訂正データを取得
            TBL_HOSEI_STATUS hosei_status = GetAutoReceiveBatch(_itemMgr.DspParams.ExecMode, this.GymId);

            if (hosei_status == null)
            {
                //初回時に訂正データがない場合
                ComMessageMgr.MessageWarning("訂正可能な明細がありません。");
                return;
            }

            // エントリデータ取得
            MeisaiInfo mei = this.GetNextEntryData(hosei_status);

            // エントリ開始
            using (EntryController econ = new EntryController(this, null))
            {
                _itemMgr.DspParams.InitializeParams();
                _itemMgr.EntParams.InitializeParams();
                econ.SetEntryForm(_itemMgr.DspParams.ExecMode);
                econ.StartControl(mei);
            }
        }

        /// <summary>
        /// 完了訂正の入力可否確認
        /// </summary>
        public ChkTeiseiType ChkEntryTeisei(int gymId, int InputMode, SortedDictionary<int, TBL_HOSEI_STATUS> all_statuses, TBL_HOSEI_STATUS hosei_status, 
                                            TBL_TRMEI trmei, TBL_TRMEIIMG trimgF, TBL_TRITEM clearingDateItem, bool ShowMsg)
        {
            ChkTeiseiType rtnValue = ChkTeiseiType.OK;

            // 自身以外のステータスがすべて「完了」になっていなければ「参照モード」で開く
            string procname = "";
            switch (InputMode)
            {
                case HoseiStatus.HoseiInputMode.自行情報:
                    procname = "自行情報";

                    // 自行情報で未補正の場合は読取専用とする
                    if (hosei_status.m_INPT_STS < HoseiStatus.InputStatus.完了)
                    {
                        rtnValue = ChkTeiseiType.DspReadOnly;
                    }
                    break;
                case HoseiStatus.HoseiInputMode.交換尻:
                    procname = "交換尻";

                    // すべて未完了だったら画面全体を読取専用にする
                    bool isReadOnly = true;
                    foreach (TBL_HOSEI_STATUS hsts in all_statuses.Values)
                    {
                        // 補正状態が完了の場合は入力可能
                        if (hsts._HOSEI_INPTMODE == HoseiStatus.HoseiInputMode.持帰銀行 && hsts.m_INPT_STS >= HoseiStatus.InputStatus.完了)
                        {
                            isReadOnly = false;
                            break;
                        }
                        else if (hsts._HOSEI_INPTMODE == HoseiStatus.HoseiInputMode.交換希望日 && hsts.m_INPT_STS >= HoseiStatus.InputStatus.完了)
                        {
                            isReadOnly = false;
                            break;
                        }
                        else if (hsts._HOSEI_INPTMODE == HoseiStatus.HoseiInputMode.金額 && hsts.m_INPT_STS >= HoseiStatus.InputStatus.完了)
                        {
                            isReadOnly = false;
                            break;
                        }
                    }

                    if (isReadOnly) rtnValue = ChkTeiseiType.DspReadOnly;
                    break;
            }

            // 削除済み明細
            if (DBConvert.ToBoolNull(trmei.m_DELETE_FLG))
            {
                if (ShowMsg) ComMessageMgr.MessageWarning("削除済みデータのため補正できません。", procname);
                return ChkTeiseiType.DeleteDataError;
            }

            // 完了訂正可能かチェックする(ステータス)
            if (InputMode == HoseiStatus.HoseiInputMode.自行情報 && hosei_status.m_INPT_STS < HoseiStatus.InputStatus.完了)
            {
                // 完了以下の場合は読取専用のため他端末チェック除外
            }
            else
            {
                if ((hosei_status.m_INPT_STS != HoseiStatus.InputStatus.完了) &&
                    (hosei_status.m_INPT_STS != HoseiStatus.InputStatus.完了訂正保留))
                {
                    if (ShowMsg) ComMessageMgr.MessageWarning("他端末で訂正中のため、{0}訂正は行えません。", procname);
                    return ChkTeiseiType.TeiseiError;
                }
            }

            if (gymId == GymParam.GymId.持出)
            {
                // 持出
                if ((trimgF.m_BUA_STS == TrMei.Sts.ファイル作成) ||
                    (trimgF.m_BUA_STS == TrMei.Sts.アップロード))
                {
                    // 表イメージでチェック
                    // 表イメージが未作成でそれ以外がアップロード中のケースは存在しないため
                    // 表イメージがアップロード済の場合は参照表示
                    if (ShowMsg) ComMessageMgr.MessageWarning("IF処理中のため、{0}訂正は行えません。", procname);
                    return ChkTeiseiType.IFError;
                }

                if (trimgF.m_BUA_STS == TrMei.Sts.結果正常)
                {
                    // 表イメージの持出アップロード状態が「２０：結果正常」だった場合は入力不可
                    rtnValue = ChkTeiseiType.DspReadOnly;
                }
            }
            else // if (gymId == GymParam.GymId.持帰)
            {
                // 持帰
                if ((trmei.m_GMA_STS == TrMei.Sts.ファイル作成) ||
                    (trmei.m_GMA_STS == TrMei.Sts.アップロード))
                {
                    if (ShowMsg) ComMessageMgr.MessageWarning("IF処理中のため、{0}訂正は行えません。", procname);
                    return ChkTeiseiType.IFError;
                }
            }

            // 完了訂正可能かチェックする(交換日)
            if (gymId == GymParam.GymId.持出)
            {
                // 持出
                // 交換尻がすべて入力完了で交換日が処理日より過去の場合は訂正不可
                if (GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.持帰銀行) == HoseiStatus.InputStatus.完了 &&
                    GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.交換希望日) == HoseiStatus.InputStatus.完了 &&
                    GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.金額) == HoseiStatus.InputStatus.完了 &&
                    GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.交換尻) == HoseiStatus.InputStatus.完了 &&
                    (!string.IsNullOrEmpty(clearingDateItem.m_END_DATA) && DBConvert.ToIntNull(clearingDateItem.m_END_DATA) < AplInfo.OpDate()))
                {
                    if (ShowMsg) ComMessageMgr.MessageWarning("交換日が過去のため、{0}訂正は行えません。", procname);
                    return ChkTeiseiType.ClearingDateError;
                }
            }
            else // if (gymId == GymParam.GymId.持帰)
            {
                // 持帰
                // 交換尻・自行情報がすべて入力完了で交換日が処理日より過去の場合は訂正不可
                if (GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.持帰銀行) == HoseiStatus.InputStatus.完了 &&
                    GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.交換希望日) == HoseiStatus.InputStatus.完了 &&
                    GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.金額) == HoseiStatus.InputStatus.完了 &&
                    GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.交換尻) == HoseiStatus.InputStatus.完了 &&
                    GetHoseiSTS(all_statuses, HoseiStatus.HoseiInputMode.自行情報) == HoseiStatus.InputStatus.完了 &&
                    (!string.IsNullOrEmpty(clearingDateItem.m_END_DATA) && DBConvert.ToIntNull(clearingDateItem.m_END_DATA) < AplInfo.OpDate()))
                {
                    if (ShowMsg) ComMessageMgr.MessageWarning("交換日が過去のため、{0}訂正は行えません。", procname);
                    return ChkTeiseiType.ClearingDateError;
                }
            }

            return rtnValue;
        }

        /// <summary>
        /// 指定した ITEM_ID が読取専用かどうか判別する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public bool IsReadOnlyItemId(int itemid)
        {
            return this.ReadOnlyItemIdList.Contains(itemid);
        }

        /// <summary>
        /// 対象の補正ステータスを取得
        /// </summary>
        private int GetHoseiSTS(SortedDictionary<int, TBL_HOSEI_STATUS> all_statuses, int InputMode, int DefSts = HoseiStatus.InputStatus.エントリ待)
        {
            int rtnSts = DefSts;
            if (all_statuses.ContainsKey(InputMode))
            {
                rtnSts = all_statuses[InputMode].m_INPT_STS; 
            }

            return rtnSts;
        }

        /// <summary>
        /// EntryReplacer取得
        /// </summary>
        /// <remarks>ItemManagerのマスタを使用して作成</remarks>
        public EntryReplacer GetEntReplacer()
        {
            return new EntryReplacer(_itemMgr.mst_banks, _itemMgr.mst_bk_changes, _itemMgr.mst_branches, _itemMgr.mst_bills, 
                                     _itemMgr.mst_syuruimfs, _itemMgr.mst_changes, _itemMgr.mst_chgbillmf, _itemMgr.mst_payermf, false);
        }

    }
}
