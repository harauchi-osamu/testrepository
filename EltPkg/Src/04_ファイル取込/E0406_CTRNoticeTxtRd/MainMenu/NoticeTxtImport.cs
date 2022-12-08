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
    class NoticeTxtImport
    {
        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        #endregion

        // XMLPath
        private string XMLPath { get { return Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources/IF208Load.xml"); } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NoticeTxtImport(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
            iBicsCalendar cal = new iBicsCalendar();
            cal.SetHolidays();
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 通知テキスト取込処理
        /// </summary>
        public bool TextImport()
        {
            // 「IO集信フォルダ(銀行別)」フォルダにコピー
            File.Copy(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename), Path.Combine(_itemMgr.IOReceiveRoot(), _itemMgr._TargetFilename), true);

            // ファイル集配信管理テーブルに登録
            _itemMgr.SetFileCtlKey(_itemMgr._file_id, _itemMgr._file_divid, new string('Z', 32), _itemMgr._TargetFilename);
            if (!_itemMgr.FileCtlInsert()) return false;

            bool ImportFlg = false;
            try
            {
                // ファイル読み込み整合性確認
                IFFileDataLoad LoadFile = ChkFile();

                // 登録処理
                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
                {
                    NoticeTxtImportCommon TxtImp = new NoticeTxtImportCommon(_ctl, LoadFile);

                    //対象データの登録
                    if (!TxtImp.ImportNoticeTxtCtl(dbp, Tran)) return false;
                    if (!TxtImp.ImportNoticeTxt(dbp, Tran)) return false;


                    switch (_itemMgr._file_divid)
                    {
                        case "YCA":
                            // 判別不可
                            // 個別処理なし
                            ImportFlg = true;
                            break;
                        case "BUA":
                            // 二重持出通知(持出)
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportBUA(TxtImp, dbp, Tran);
                            break;
                        case "BUB":
                            // 二重持出通知(持帰)
                            AppInfo.Setting.SetGymId(GymParam.GymId.持帰);
                            ImportFlg = TextImportBUB(TxtImp, dbp, Tran);
                            break;
                        case "BCA":
                            // 持出取消通知
                            AppInfo.Setting.SetGymId(GymParam.GymId.持帰);
                            ImportFlg = TextImportBCA(TxtImp, dbp, Tran);
                            break;
                        case "GMA":
                            // 証券データ訂正通知(持出)
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportGMA(TxtImp, dbp, Tran);
                            break;
                        case "GMB":
                            // 証券データ訂正通知(持帰)
                            // 個別処理なし
                            ImportFlg = true;
                            break;
                        case "GRA":
                            // 不渡返還通知
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportGRA(TxtImp, dbp, Tran);
                            break;
                        case "GXA":
                            // 決済後訂正通知(持出)
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportGXA(TxtImp, dbp, Tran);
                            break;
                        case "GXB":
                            // 決済後訂正通知(持帰)
                            // 個別処理なし
                            ImportFlg = true;
                            break;
                        case "MRA":
                            // 金融機関読替情報変更通知(持出銀行コード変更・継承銀行向け)
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportMRA(TxtImp, dbp, Tran);
                            break;
                        case "MRB":
                            // 金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け)
                            AppInfo.Setting.SetGymId(GymParam.GymId.持帰);
                            ImportFlg = TextImportMRB(TxtImp, dbp, Tran);
                            break;
                        case "MRC":
                            // 金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け)
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportMRC(TxtImp, dbp, Tran);
                            break;
                        case "MRD":
                            // 金融機関読替情報変更通知(持帰銀行コード変更・継承銀行向け)
                            // 個別処理なし
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

            //// データレコード件数チェック
            //if (!(LoadFile.LoadData.First().LineData["ファイル識別区分"] == "YCA"))
            //{
            //    if (LoadFile.LoadData.Count(x => x.KBN == "2") == 0)
            //    {
            //        throw new Exception("データレコード件数が不正です");
            //    }
            //}

            return LoadFile;
        }

        #endregion

        #region 二重持出通知(持出)関連

        /// <summary>
        /// 二重持出通知(持出)取込処理
        /// </summary>
        private bool TextImportBUA(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(二重持出通知(持出))：{0}", _itemMgr._TargetFilename), 3);

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];
                string ImgNamedbl = Data.LineData["二重持出イメージファイル名"];
                int MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("二重持出通知(持出)取込：{0},{1}", ImgName, ImgNamedbl), 3);

                // 更新処理
                // 証券イメージファイル名
                if (_itemMgr.UpdateTRMeiSTS(ImgName, TBL_TRMEI.BUA_DATE, MKDate, dbp, Tran) <= 0)
                {
                    // エラーとせず、ログに出力して終了
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("二重持出通知(持出)に対する明細情報が存在しませんでした {0}", ImgName), 1);
                    //throw new Exception(string.Format("二重持出通知(持出)に対する明細情報が存在しませんでした {0}", ImgName));
                }

                // 二重持出イメージファイル名での更新はなし
                //// 更新処理
                //// 二重持出イメージファイル名
                //if (_itemMgr.UpdateTRMeiSTS(ImgNamedbl, TBL_TRMEI.BUA_DATE, MKDate, dbp, Tran) <= 0)
                //{
                //    throw new Exception(string.Format("二重持出通知(持出)に対する明細情報が存在しませんでした {0}", ImgNamedbl));
                //}
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理終了(二重持出通知(持出))：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 二重持出通知(持帰)関連

        /// <summary>
        /// 二重持出通知(持帰)取込処理
        /// </summary>
        /// <remarks>この箇所を修正する場合は持帰ダウンロードも修正が必要なため留意</remarks>
        private bool TextImportBUB(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(二重持出通知(持帰))：{0}", _itemMgr._TargetFilename), 3);

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];
                string ImgNamedbl = Data.LineData["二重持出イメージファイル名"];
                int MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("二重持出通知(持帰)取込：{0},{1}", ImgName, ImgNamedbl), 3);

                // 更新処理
                // 証券イメージファイル名
                if (_itemMgr.UpdateTRMeiSTS(ImgName, TBL_TRMEI.BUB_DATE, MKDate, dbp, Tran) <= 0)
                {
                    // エラーとせず、ログに出力して終了
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("二重持出通知(持帰)に対する明細情報が存在しませんでした {0}", ImgName), 1);
                    // throw new Exception(string.Format("二重持出通知(持帰)に対する明細情報が存在しませんでした {0}", ImgName));
                }

                // 二重持出イメージファイル名での更新はなし
                //// 更新処理
                //// 二重持出イメージファイル名
                //if (_itemMgr.UpdateTRMeiSTS(ImgNamedbl, TBL_TRMEI.BUB_DATE, MKDate, dbp, Tran) <= 0)
                //{
                //    throw new Exception(string.Format("二重持出通知(持帰)に対する明細情報が存在しませんでした {0}", ImgNamedbl));
                //}
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理終了(二重持出通知(持帰))：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 持出取消通知関連

        /// <summary>
        /// 持出取消通知取込処理
        /// </summary>
        /// <remarks>この箇所を修正する場合は持帰ダウンロードも修正が必要なため留意</remarks>
        private bool TextImportBCA(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(持出取消通知)：{0}", _itemMgr._TargetFilename), 3);

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];
                int MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持出取消通知取込：{0}", ImgName), 3);

                Dictionary<string, int> Field = new Dictionary<string, int>();
                Field.Add(TBL_TRMEI.BCA_DATE, MKDate);
                Field.Add(TBL_TRMEI.DELETE_DATE, AplInfo.OpDate());
                Field.Add(TBL_TRMEI.DELETE_FLG, 1);

                // 更新処理
                if (_itemMgr.UpdateTRMeiSTS(ImgName, Field, dbp, Tran) <= 0)
                {
                    // エラーとせず、ログに出力して終了
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持出取消通知に対する明細情報が存在しませんでした {0}", ImgName), 1);
                    // throw new Exception(string.Format("持出取消通知に対する明細情報が存在しませんでした {0}", ImgName));
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(持出取消通知)：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 証券データ訂正通知(持出)関連

        /// <summary>
        /// 証券データ訂正通知(持出)取込処理
        /// </summary>
        private bool TextImportGMA(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(証券データ訂正通知(持出))：{0}", _itemMgr._TargetFilename), 3);

            EntryReplacer Replacer = new EntryReplacer();

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];
                int MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("証券データ訂正通知(持出)取込：{0}", ImgName), 3);

                // 更新処理
                if (_itemMgr.UpdateTRMeiSTS(ImgName, TBL_TRMEI.GMA_DATE, MKDate, dbp, Tran) <= 0)
                {
                    // エラーとせず、ログに出力して終了
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("証券データ訂正通知(持出)に対する明細情報が存在しませんでした {0}", ImgName), 1);
                    //throw new Exception(string.Format("証券データ訂正通知(持出)に対する明細情報が存在しませんでした {0}", ImgName));
                }
                else
                {
                    // トランザクション更新処理
                    if (!TxtImp.UpdateTRData(Data, Replacer, dbp, Tran))
                    {
                        throw new Exception("トランザクション更新処理で失敗しました");
                    }
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(証券データ訂正通知(持出))：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 不渡返還通知関連

        /// <summary>
        /// 不渡返還通知取込処理
        /// </summary>
        private bool TextImportGRA(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(不渡返還通知)：{0}", _itemMgr._TargetFilename), 3);

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("不渡返還通知取込：{0}", ImgName), 3);

                int MKDate = 0;
                int DelFlg = 0;
                int DelDate = 0;
                switch (Data.LineData["不渡返還登録区分"])
                {
                    case "1":
                        MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);
                        DelFlg = 1;
                        DelDate = AplInfo.OpDate();
                        break;
                    case "9":
                        MKDate = 0;
                        DelFlg = 0;
                        DelDate = 0;
                        break;
                    default:
                        throw new Exception("不渡返還登録区分が不正です");
                }

                Dictionary<string, int> Field = new Dictionary<string, int>();
                Field.Add(TBL_TRMEI.GRA_DATE, MKDate);
                Field.Add(TBL_TRMEI.DELETE_DATE, DelDate);
                Field.Add(TBL_TRMEI.DELETE_FLG, DelFlg);

                // 更新処理
                if (_itemMgr.UpdateTRMeiSTS(ImgName, Field, dbp, Tran) <= 0)
                {
                    // エラーとせず、ログに出力して終了
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("不渡返還通知に対する明細情報が存在しませんでした {0}", ImgName), 1);
                    //throw new Exception(string.Format("不渡返還通知に対する明細情報が存在しませんでした {0}", ImgName));
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(不渡返還通知)：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 決済後訂正通知(持出)関連

        /// <summary>
        /// 決済後訂正通知(持出)取込処理
        /// </summary>
        private bool TextImportGXA(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(決済後訂正通知(持出))：{0}", _itemMgr._TargetFilename), 3);

            EntryReplacer Replacer = new EntryReplacer();

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];
                int MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("決済後訂正通知(持出)取込：{0}", ImgName), 3);

                // 更新処理
                if (_itemMgr.UpdateTRMeiSTS(ImgName, TBL_TRMEI.GXA_DATE, MKDate, dbp, Tran) <= 0)
                {
                    throw new Exception(string.Format("決済後訂正通知(持出)に対する明細情報が存在しませんでした {0}", ImgName));
                }
                // トランザクション更新処理
                if (!TxtImp.UpdateTRData(Data, Replacer, dbp, Tran))
                {
                    throw new Exception("トランザクション更新処理で失敗しました");
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(決済後訂正通知(持出))：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 金融機関読替情報変更通知(持出銀行コード変更・継承銀行向け)関連

        /// <summary>
        /// 金融機関読替情報変更通知(持出銀行コード変更・継承銀行向け)取込処理
        /// </summary>
        private bool TextImportMRA(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(金融機関読替情報変更通知(持出銀行コード変更・継承銀行向け))：{0}", _itemMgr._TargetFilename), 3);

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];
                int MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("金融機関読替情報変更通知(持出銀行コード変更・継承銀行向け)取込：{0}", ImgName), 3);

                // 更新処理
                if (_itemMgr.UpdateTRMeiSTS(ImgName, TBL_TRMEI.MRA_DATE, MKDate, dbp, Tran) <= 0)
                {
                    throw new Exception(string.Format("金融機関読替情報変更通知(持出銀行コード変更・継承銀行向け)に対する明細情報が存在しませんでした {0}", ImgName));
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(金融機関読替情報変更通知(持出銀行コード変更・継承銀行向け))：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け)関連

        /// <summary>
        /// 金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け)取込処理
        /// </summary>
        /// <remarks>この箇所を修正する場合は持帰ダウンロードも修正が必要なため留意</remarks>
        private bool TextImportMRB(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け))：{0}", _itemMgr._TargetFilename), 3);

            EntryReplacer Replacer = new EntryReplacer();

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];
                int MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け)取込：{0}", ImgName), 3);

                // 更新処理
                if (_itemMgr.UpdateTRMeiSTS(ImgName, TBL_TRMEI.MRB_DATE, MKDate, dbp, Tran) <= 0)
                {
                    // エラーとせず、ログに出力して終了
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け)に対する明細情報が存在しませんでした {0}", ImgName), 1);
                    // throw new Exception(string.Format("金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け)に対する明細情報が存在しませんでした {0}", ImgName));
                }
                else
                {
                    // トランザクション更新処理
                    if (!TxtImp.UpdateTRData(Data, Replacer, dbp, Tran))
                    {
                        throw new Exception("トランザクション更新処理で失敗しました");
                    }
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(金融機関読替情報変更通知(持出銀行コード変更・持帰銀行向け))：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

        #region 金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け)関連

        /// <summary>
        /// 金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け)取込処理
        /// </summary>
        private bool TextImportMRC(NoticeTxtImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け))：{0}", _itemMgr._TargetFilename), 3);

            EntryReplacer Replacer = new EntryReplacer();

            foreach (IFData Data in TxtImp._DataRecord)
            {
                string ImgName = Data.LineData["証券イメージファイル名"];
                int MKDate = int.Parse(TxtImp._HeaderData.LineData["作成日"]);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け)取込：{0}", ImgName), 3);

                // 更新処理
                if (_itemMgr.UpdateTRMeiSTS(ImgName, TBL_TRMEI.MRC_DATE, MKDate, dbp, Tran) <= 0)
                {
                    // エラーとせず、ログに出力して終了
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け)に対する明細情報が存在しませんでした {0}", ImgName), 1);
                    //throw new Exception(string.Format("金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け)に対する明細情報が存在しませんでした {0}", ImgName));
                }
                else
                {
                    // トランザクション更新処理
                    if (!TxtImp.UpdateTRData(Data, Replacer, dbp, Tran))
                    {
                        throw new Exception("トランザクション更新処理で失敗しました");
                    }
                }
            }

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("通知テキスト取込処理(金融機関読替情報変更通知(持帰銀行コード変更・持出銀行向け))：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

    }
}
