using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CommonTable.DB;
using CommonClass;
using EntryCommon;
using IFFileOperation;
using IFImportCommon;

namespace MainMenu
{
    class ResultTextImport
    {
        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        #endregion

        // XMLPath
        private string XMLPath { get { return Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources/IF206Load.xml"); } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ResultTextImport(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 結果テキスト取込処理
        /// </summary>
        public bool TextImport()
        {
            // 「IO集信フォルダ(銀行別)」フォルダにコピー
            File.Copy(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename), Path.Combine(_itemMgr.IOReceiveRoot(), _itemMgr._TargetFilename), true);

            // ファイル集配信管理テーブル登録更新
            if (!FileCtlStart()) return false;

            bool ImportFlg = false;
            try
            {
                // ファイル読み込み整合性確認
                IFFileDataLoad LoadFile = ChkFile();

                // 登録処理
                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
                {
                    ResultTextImportCommon TxtImp = new ResultTextImportCommon(_ctl, LoadFile);

                    //対象データの登録
                    if (!TxtImp.ImportResultTxtCtl(dbp, Tran)) return false;
                    if (!TxtImp.ImportResultTxt(dbp, Tran)) return false;

                    switch (_itemMgr._file_divid)
                    {
                        case "BUB":
                            // 持出アップロード
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportBUB(TxtImp, dbp, Tran);
                            break;
                        case "BCA":
                            // 持出取消
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportBCA(TxtImp, dbp, Tran);
                            break;
                        case "GMA":
                            // 証券データ訂正
                            AppInfo.Setting.SetGymId(GymParam.GymId.持帰);
                            ImportFlg = TextImportGMA(TxtImp, dbp, Tran);
                            break;
                        case "GRA":
                            // 不渡返還
                            AppInfo.Setting.SetGymId(GymParam.GymId.持帰);
                            ImportFlg = TextImportGRA(TxtImp, dbp, Tran);
                            break;
                        case "YCA":
                            // 判別不可
                            ImportFlg = true;
                            break;
                        default:
                            break;
                    }

                    if (ImportFlg)
                    {
                        //コミット
                        Tran.Trans.Commit();
                        // HULFT集信フォルダのファイル削除
                        File.Delete(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename));
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _itemMgr._TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _itemMgr._TargetFilename, 3);
                ImportFlg = false;
            }
            finally
            {
                // ファイル集配信管理テーブル更新
                _itemMgr.FileCtlUpdate((ImportFlg == true) ? 10 : 9);
                AppInfo.Setting.SetGymId(GymParam.GymId.共通);
            }

            return ImportFlg;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        #region ファイル集配信管理テーブル

        /// <summary>
        /// 処理開始時のファイル集配信管理登録・更新
        /// </summary>
        private bool FileCtlStart()
        {
            switch (_itemMgr._file_divid)
            {
                case "YCA":
                    // 判別不可

                    // ファイル集配信管理テーブルに登録
                    _itemMgr.SetFileCtlKey(_itemMgr._file_id, _itemMgr._file_divid, new string('Z', 32), _itemMgr._TargetFilename);
                    if (!_itemMgr.FileCtlInsert()) return false;

                    return true;
                default:
                    // その他
                    return FileCtlStartUpdate();
            }
        }

        /// <summary>
        /// 処理開始時のファイル集配信管理更新
        /// YCA以外
        /// </summary>
        private bool FileCtlStartUpdate()
        {
            string fileID = string.Empty;
            string fileext = string.Empty;

            switch (_itemMgr._file_divid)
            {
                case "BUB":
                    // 持出アップロード
                    fileID = "IF101";
                    fileext = ".tar";

                    break;
                case "BCA":
                    // 持出取消
                    fileID = "IF202";
                    fileext = ".txt";

                    break;
                case "GMA":
                    // 証券データ訂正
                    fileID = "IF204";
                    fileext = ".txt";

                    break;
                case "GRA":
                    // 不渡返還
                    fileID = "IF205";
                    fileext = ".txt";

                    break;
                default:
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ファイル識別区分が不正です" + "(" + _itemMgr._TargetFilename + ")", 3);
                    LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), "ファイル識別区分が不正です", _itemMgr._TargetFilename, 3);
                    return false;
            }

            string SearchFileName = _itemMgr._TargetFilename;
            // 拡張子置き換え
            SearchFileName = SearchFileName.Remove(SearchFileName.Length - 4, 4).Insert(SearchFileName.Length - 4, fileext);
            // FileID置き換え
            SearchFileName = SearchFileName.Remove(0, 5).Insert(0, fileID);

            // ファイル集配信管理テーブル更新
            _itemMgr.SetFileCtlKey(fileID, _itemMgr._file_divid, SearchFileName, _itemMgr._TargetFilename);
            if (_itemMgr.FileCtlStartUpdate() <= 0)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "対象ファイルに対する配信情報を取得できませんでした" + "(" + _itemMgr._TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), "対象ファイルに対する配信情報を取得できませんでした", _itemMgr._TargetFilename, 3);
                return false;
            }

            return true;
        }

        #endregion

        #region ファイル整合性確認

        /// <summary>
        /// ファイル整合性確認
        /// </summary>
        private IFFileDataLoad ChkFile()
        {
            // ファイル識別区分チェック
            if (!_itemMgr.GetfileParam(out int FileSize))
            {
                throw new Exception("ファイル識別区分が不正です");
            }

            // データ読み込み準備
            IFFileDataLoad LoadFile = new IFFileDataLoad(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename), XMLPath);

            // ファイルサイズチェック
            if (!LoadFile.ChkFileSize(FileSize))
            {
                throw new Exception("ファイルサイズが不正です");
            }

            // データ読み込み
            if (!LoadFile.IFDataLoad())
            {
                string ErrMsg = string.Empty;
                switch (LoadFile.LoadError)
                {
                    case IFFileDataLoad.LoadErrorType.KBNIllegal:
                        ErrMsg = "レコード区分が不正です";
                        break;
                    case IFFileDataLoad.LoadErrorType.DataIllegal:
                        ErrMsg = "データが不正です";
                        break;
                    default:
                        ErrMsg = "データ読み込みに失敗しました";
                        break;
                }
                throw new Exception(ErrMsg);
            }

            // レコード区分順序チェック

            // 最低3レコード数チェック
            if (LoadFile.LoadData.Count < 3)
            {
                throw new Exception("データが不正です");
            }

            // ヘッダ・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN == "1") == 1 && LoadFile.LoadData.First().KBN == "1"))
            {
                throw new Exception("ヘッダ・レコードが不正です");
            }

            // ファイル識別区分チェック
            if (!CodeListChk(LoadFile.LoadData.First().LineData["ファイル識別区分"], "BUB", "BCA", "GMA", "GRA", "YCA"))
            {
                throw new Exception("ファイル識別区分が不正です");
            }

            // エンド・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN == "9") == 1 && LoadFile.LoadData.Last().KBN == "9"))
            {
                throw new Exception("エンド・レコードが不正です");
            }

            // トレーラ・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN == "8") == 1 && LoadFile.LoadData.Skip(LoadFile.LoadData.Count - 2).Take(1).Count(x => x.KBN == "8") == 1))
            {
                throw new Exception("トレーラ・レコードが不正です");
            }

            // トレーラ・データレコード件数チェック
            IEnumerable<IFData> Data = LoadFile.LoadData.Where(x => x.KBN == "8");
            if (!(Data.Count() == 1 &&
                  long.Parse(Data.First().LineData["レコード件数"]) == LoadFile.LoadData.Count(x => x.KBN == "2")))
            {
                throw new Exception("データレコード件数が不正です");
            }

            // データレコード件数チェック
            if (!(LoadFile.LoadData.First().LineData["ファイル識別区分"] == "YCA" ||
                  LoadFile.LoadData.First().LineData["ファイルチェック結果コード"].Trim().Last() == 'E'))
            {
                if (LoadFile.LoadData.Count(x => x.KBN == "2") == 0)
                {
                    throw new Exception("データレコード件数が不正です");
                }
            }

            return LoadFile;
        }

        #endregion

        #region 持出アップロード関連

        /// <summary>
        /// 結果テキスト取込処理
        /// 持出アップロード
        /// </summary>
        private bool TextImportBUB(ResultTextImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果テキスト取込処理(持出アップロード)：{0}", _itemMgr._TargetFilename), 3);

            CTRTxtRd txtRd = new CTRTxtRd(AppInfo.Setting.SchemaBankCD, dbp);
            EntryReplacer entryReplacer = new EntryReplacer();
　
            foreach (IFData data in TxtImp._DataRecord)
            {
                // 対象ファイル名の取得
                string filename = data.LineData["受付内容"].Trim();

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持出アップロード結果取込：{0}", filename), 3);

                // 処理結果コードチェック
                bool CodeChk = CodeListChk(data.LineData["処理結果コード"], "M1012000-I");

                // 更新処理
                Dictionary<string, int> Field = new Dictionary<string, int>() { { TBL_TRMEIIMG.BUA_STS, (CodeChk ? 20 : 19) } };
                if (Field[TBL_TRMEIIMG.BUA_STS] == 20)
                {
                    Field.Add(TBL_TRMEIIMG.BUB_CONFIRMDATE, AplInfo.OpDate());
                }

                if (_itemMgr.UpdateTRMeiImgSTS(filename, Field, dbp, Tran) <= 0)
                {
                    if (CodeChk && data.LineData["持出時接続方式"] == "2")
                    {
                        throw new Exception(string.Format("対象データに対する明細情報が存在しませんでした {0}", filename));
                    }
                }
                
                if (CodeChk && data.LineData["表・裏等の別"] == "01" && data.LineData["持出時接続方式"] == "2")
                {
                    // 電子交換所認識値取得
                    string Amt = txtRd.GetText(data.LineData, "金額");
                    string IcBKNo = txtRd.GetText(data.LineData, "持帰銀行コード");
                    string ClearingDate = txtRd.GetText(data.LineData, "交換日");
                    string PayKbn = txtRd.GetText(data.LineData, "決済対象フラグ");

                    // 金額欄処理
                    if (!_itemMgr.UpdateTRItemBUB(filename, 6, Amt, dbp, Tran))
                    {
                        throw new Exception("金額更新に失敗しました");
                    }

                    // 持帰銀行処理
                    string BankName = string.Empty;
                    if (int.TryParse(IcBKNo, out int iIcBKNo))
                    {
                        // 銀行名取得
                        BankName = entryReplacer.GetBankName(int.Parse(IcBKNo));
                    }
                    // 券面持帰銀行コード欄処理
                    if (!_itemMgr.UpdateTRItemBUB(filename, 19, IcBKNo, dbp, Tran))
                    {
                        throw new Exception("券面持帰銀行コード更新に失敗しました");
                    }
                    // 持帰銀行コード欄処理
                    if (!_itemMgr.UpdateTRItemBUB(filename, 1, IcBKNo, dbp, Tran))
                    {
                        throw new Exception("持帰銀行コード更新に失敗しました");
                    }
                    // 持帰銀行コード欄処理
                    if (!_itemMgr.UpdateTRItemBUB(filename, 2, BankName, dbp, Tran))
                    {
                        throw new Exception("持帰銀行名更新に失敗しました");
                    }

                    // 交換日処理
                    string sarekiDate = string.Empty;
                    string BusinessDate = string.Empty;
                    entryReplacer.ReplaceClearingDateOrg(ClearingDate, ref sarekiDate, ref BusinessDate);
                    // 入力交換希望日欄処理
                    if (!_itemMgr.UpdateTRItemBUB(filename, 3, ClearingDate, dbp, Tran))
                    {
                        throw new Exception("入力交換希望日更新に失敗しました");
                    }
                    // 和暦交換希望日欄処理
                    if (!_itemMgr.UpdateTRItemBUB(filename, 4, sarekiDate, dbp, Tran))
                    {
                        throw new Exception("和暦交換希望日更新に失敗しました");
                    }
                    // 交換日欄処理
                    if (!_itemMgr.UpdateTRItemBUB(filename, 5, BusinessDate.Replace(".", ""), dbp, Tran))
                    {
                        throw new Exception("交換日更新に失敗しました");
                    }

                    // 決済対象フラグ欄処理
                    if (!_itemMgr.UpdateTRItemBUB(filename, 7, PayKbn, dbp, Tran))
                    {
                        throw new Exception("決済対象フラグ更新に失敗しました");
                    }
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果テキスト取込処理終了(持出アップロード)：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 持出取消関連

        /// <summary>
        /// 結果テキスト取込処理
        /// 持出取消
        /// </summary>
        private bool TextImportBCA(ResultTextImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果テキスト取込処理(持出取消)：{0}", _itemMgr._TargetFilename), 3);

            foreach (IFData data in TxtImp._DataRecord)
            {
                // 対象ファイル名の取得(2桁目から62Byte)
                string filename = data.LineData["受付内容"].Substring(1, 62).Trim();

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持出取消結果取込：{0}", filename), 3);

                // 処理結果コードチェック
                bool CodeChk = CodeListChk(data.LineData["処理結果コード"], "M1052000-I");

                // 更新処理
                Dictionary<string, int> Field = new Dictionary<string, int>() { { TBL_TRMEI.BCA_STS, (CodeChk ? 20 : 19) } };
                if (CodeChk)
                {
                    // 削除成功
                    Field.Add(TBL_TRMEI.DELETE_FLG, 1);
                    Field.Add(TBL_TRMEI.DELETE_DATE, AplInfo.OpDate());
                }
                // 更新処理(TRMEI)
                if (_itemMgr.UpdateTRMeiSTS(filename, Field, dbp, Tran) <= 0)
                {
                    throw new Exception(string.Format("対象データに対する明細情報が存在しませんでした {0}", filename));
                }

                if (CodeChk)
                {
                    // 削除成功時はイメージも削除
                    Field.Clear();
                    Field.Add(TBL_TRMEIIMG.DELETE_FLG, 1);
                    Field.Add(TBL_TRMEIIMG.DELETE_DATE, AplInfo.OpDate());

                    // 更新処理(TRMEIIMG)
                    if (_itemMgr.UpdateTRMeiImgSTSFrontImg(filename, Field, dbp, Tran) <= 0)
                    {
                        throw new Exception(string.Format("対象データに対する明細情報が存在しませんでした {0}", filename));
                    }
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果テキスト取込処理終了(持出取消)：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 証券データ訂正関連

        /// <summary>
        /// 結果テキスト取込処理
        /// 証券データ訂正
        /// </summary>
        private bool TextImportGMA(ResultTextImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果テキスト取込処理(証券データ訂正)：{0}", _itemMgr._TargetFilename), 3);

            List<string> OCRDeleteFiles = new List<string>();

            foreach (IFData data in TxtImp._DataRecord)
            {
                // 対象ファイル名の取得(2桁目から62Byte)
                string filename = data.LineData["受付内容"].Substring(1, 62).Trim();

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("証券データ訂正結果取込：{0}", filename), 3);

                // 処理結果コードチェック
                bool CodeChk = CodeListChk(data.LineData["処理結果コード"], "M2042000-I");

                // 更新処理
                if (_itemMgr.UpdateTRMeiSTS(filename, TBL_TRMEI.GMA_STS, (CodeChk ? 20 : 19), dbp, Tran) <= 0)
                {
                    throw new Exception(string.Format("対象データに対する明細情報が存在しませんでした {0}", filename));
                }

                // 持帰訂正確定値更新処理
                //if (CodeChk && !_itemMgr.UpdateTRItemTeiseiData(filename, dbp, Tran))
                //{
                //    throw new Exception("持帰訂正確定値の更新に失敗しました");
                //}
                if (CodeChk)
                {
                    // 結果正常

                    // 持帰銀行
                    if (!UpdTeiseiICBK(filename, data, dbp, Tran))
                    {
                        throw new Exception("持帰訂正確定値の更新に失敗しました(持帰銀行)");
                    }

                    // 交換日
                    if (!UpdTeiseiCDATE(filename, data, dbp, Tran))
                    {
                        throw new Exception("持帰訂正確定値の更新に失敗しました(交換日)");
                    }

                    // 金額
                    if (!UpdTeiseiAmt(filename, data, dbp, Tran))
                    {
                        throw new Exception("持帰訂正確定値の更新に失敗しました(金額)");
                    }
                }

                // 結果正常で持帰状況フラグが未持帰の場合、削除フラグを設定
                if (CodeChk)
                {
                    if ( data.LineData["持帰状況フラグ"] == "0")
                    {
                        List<TBL_TRITEM> ItemData;
                        if (!_itemMgr.GetItemDataFileName(filename, out ItemData, dbp))
                        {
                            throw new Exception("項目の取得に失敗しました");
                        }

                        //削除処理
                        if (!_itemMgr.DeleteTrMei(ItemData.First(), dbp, Tran))
                        {
                            throw new Exception("削除処理に失敗しました");
                        }

                        // OCRデータの削除ファイルを設定(表面と裏面を設定)
                        string Backfilename = filename.Substring(0, 56) + TrMeiImg.ImgKbn.裏.ToString("D2") + filename.Substring(58, 4);
                        OCRDeleteFiles.Add(filename);
                        OCRDeleteFiles.Add(Backfilename);
                    }

                    //List<TBL_TRITEM> ItemData;
                    //if (!_itemMgr.GetItemDataFileName(filename, out ItemData, dbp))
                    //{
                    //    throw new Exception("項目の取得に失敗しました");
                    //}

                    //if (!_itemMgr.ChkJikouBankCd(ItemData))
                    //{
                    //    //自行ではない場合は削除
                    //    if (!_itemMgr.DeleteTrMei(ItemData.First(), dbp, Tran))
                    //    {
                    //        throw new Exception("削除処理に失敗しました");
                    //    }
                    //}
                }
            }

            // OCRデータの削除処理
            if (!_itemMgr.DeleteOCRDATA(OCRDeleteFiles, dbp, Tran))
            {
                throw new Exception("OCRデータの削除処理に失敗しました");
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果テキスト取込処理終了(証券データ訂正)：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 不渡返還関連

        /// <summary>
        /// 結果テキスト取込処理
        /// 不渡返還
        /// </summary>
        private bool TextImportGRA(ResultTextImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果テキスト取込処理(不渡返還)：{0}", _itemMgr._TargetFilename), 3);

            foreach (IFData data in TxtImp._DataRecord)
            {
                // 対象ファイル名の取得(3桁目から62Byte)
                string filename = data.LineData["受付内容"].Substring(2, 62).Trim();
                // 対象登録区分の取得(2桁目から1Byte)
                string Kbn = data.LineData["受付内容"].Substring(1, 1).Trim();

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("不渡返還結果取込：{0}", filename), 3);

                // 処理結果コードチェック
                bool CodeChk = CodeListChk(data.LineData["処理結果コード"], "M2052001-I", "M2052002-I");

                // 更新処理
                Dictionary<string, int> Field = new Dictionary<string, int>() { { TBL_TRMEI.GRA_STS, (CodeChk ? 20 : 19) } };
                if (Field[TBL_TRMEI.GRA_STS] == 20)
                {
                    Field.Add(TBL_TRMEI.GRA_CONFIRMDATE, AplInfo.OpDate());
                }

                if (CodeChk)
                {
                    // 正常の場合
                    switch (Kbn)
                    {
                        case "1":
                            // 新規登録の場合は削除フラグを設定
                            Field.Add(TBL_TRMEI.DELETE_FLG, 1);
                            Field.Add(TBL_TRMEI.DELETE_DATE, AplInfo.OpDate());

                            break;
                        case "9":
                            // 取り消しの場合

                            // 不渡返還で削除されている場合は削除解除
                            // (不渡返還以外で削除されている場合はそのまま)

                            // 明細データ取得
                            if (!_itemMgr.GetMeiDataFileName(filename, out TBL_TRMEI MeiData, dbp))
                            {
                                throw new Exception("明細の取得に失敗しました");
                            }
                            
                            // 項目データ取得
                            if (!_itemMgr.GetItemDataFileName(filename, out List<TBL_TRITEM> ItemData, dbp))
                            {
                                throw new Exception("項目の取得に失敗しました");
                            }

                            if (MeiData.m_DELETE_FLG == 1)
                            {
                                if (MeiData.m_BCA_DATE != 0)
                                {
                                    // 持出取消での削除ケース
                                }
                                else if (!_itemMgr.ChkJikouBankCd(ItemData) && MeiData.m_GMA_STS == TrMei.Sts.結果正常)
                                {
                                    // 持帰訂正での削除ケース(持帰銀行訂正)
                                }
                                else
                                {
                                    // 上記以外は削除解除
                                    Field.Add(TBL_TRMEI.DELETE_FLG, 0);
                                    Field.Add(TBL_TRMEI.DELETE_DATE, 0);
                                }
                            }

                            break;
                    }
                }

                if (_itemMgr.UpdateTRMeiSTS(filename, Field, dbp, Tran) <= 0)
                {
                    throw new Exception(string.Format("対象データに対する明細情報が存在しませんでした {0}", filename));
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("結果テキスト取込処理終了(不渡返還)：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 共通

        /// <summary>
        /// 持帰訂正更新処理
        /// 持帰銀行
        /// </summary>
        private bool UpdTeiseiICBK(string filename, IFData data, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            List<int> UpdItemID = new List<int>() { DspItem.ItemId.券面持帰銀行コード, DspItem.ItemId.持帰銀行コード, DspItem.ItemId.持帰銀行名 };

            if (data.LineData["証券データ訂正持帰銀行コード"] == new string('Z', 4))
            {
                // 持帰銀行訂正なし
                return _itemMgr.UpdateTRItemTeiseiData(filename, UpdItemID, true, dbp, Tran);
            }
            else
            {
                // 持帰銀行訂正あり
                return _itemMgr.UpdateTRItemTeiseiData(filename, UpdItemID, false, dbp, Tran);
            }
        }

        /// <summary>
        /// 持帰訂正更新処理
        /// 持帰銀行
        /// </summary>
        private bool UpdTeiseiCDATE(string filename, IFData data, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            List<int> UpdItemID = new List<int>() { DspItem.ItemId.入力交換希望日, DspItem.ItemId.和暦交換希望日, DspItem.ItemId.交換日 };

            if (data.LineData["証券データ訂正交換希望日"] == new string('Z', 8))
            {
                // 交換日訂正なし
                return _itemMgr.UpdateTRItemTeiseiData(filename, UpdItemID, true, dbp, Tran);
            }
            else
            {
                // 交換日訂正あり
                return _itemMgr.UpdateTRItemTeiseiData(filename, UpdItemID, false, dbp, Tran);
            }
        }

        /// <summary>
        /// 持帰訂正更新処理
        /// 金額
        /// </summary>
        private bool UpdTeiseiAmt(string filename, IFData data, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            List<int> UpdItemID = new List<int>() { DspItem.ItemId.金額 };

            if (data.LineData["証券データ訂正金額"] == new string('Z', 12))
            {
                // 金額訂正なし
                return _itemMgr.UpdateTRItemTeiseiData(filename, UpdItemID, true, dbp, Tran);
            }
            else
            {
                // 金額訂正あり
                return _itemMgr.UpdateTRItemTeiseiData(filename, UpdItemID, false, dbp, Tran);
            }
        }

        private bool CodeListChk(string Chk, params string[] Chkkeys)
        {
            if (Chkkeys.Count() == 0) return false;
            return Chkkeys.Contains(Chk.Trim());
        }

        #endregion

    }
}
