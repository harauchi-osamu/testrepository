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
    class DetailTextImport
    {
        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        #endregion

        // XMLPath
        private string XMLPath { get { return Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources/IF201Load.xml"); } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DetailTextImport(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 証券明細テキスト取込処理
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
                    DetailTextImportCommon TxtImp = new DetailTextImportCommon(_ctl, LoadFile);

                    switch (_itemMgr._file_divid)
                    {
                        case "BQA":
                            // 当日持出持出明細
                            iBicsCalendar cal = new iBicsCalendar();
                            cal.SetHolidays();
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportBQA(TxtImp, dbp, Tran);
                            
                            break;
                        case "GDA":
                            // 持帰ダウンロード
                            AppInfo.Setting.SetGymId(GymParam.GymId.持帰);
                            ImportFlg = TextImportGDA(TxtImp, dbp, Tran);

                            break;
                        case "SPA":
                        case "SFA":
                            // 交換尻データ持出証券
                            AppInfo.Setting.SetGymId(GymParam.GymId.持出);
                            ImportFlg = TextImportBalance(0, TxtImp, dbp, Tran);

                            break;
                        case "SPB":
                        case "SFB":
                            // 交換尻データ持帰証券
                            AppInfo.Setting.SetGymId(GymParam.GymId.持帰);
                            ImportFlg = TextImportBalance(1, TxtImp, dbp, Tran);

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

            // 最低5レコード数チェック
            if (LoadFile.LoadData.Count < 5)
            {
                throw new Exception("データが不正です");
            }

            // ヘッダ・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN =="1") == 1 && LoadFile.LoadData.First().KBN == "1"))
            {
                throw new Exception("ヘッダ・レコードが不正です");
            }

            // エンド・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN == "9") == 1 && LoadFile.LoadData.Last().KBN == "9"))
            {
                throw new Exception("エンド・レコードが不正です");
            }

            // トレーラ・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN == "8") == 3 && LoadFile.LoadData.Skip(LoadFile.LoadData.Count - 4).Take(3).Count(x => x.KBN == "8") == 3))
            {
                throw new Exception("トレーラ・レコードが不正です");
            }

            // データレコード件数チェック
            IEnumerable<IFData> Data = LoadFile.LoadData.Where(x => x.KBN == "8" && x.LineData["集計区分"] == "0");
            if (!(Data.Count() == 1 &&
                  long.Parse(Data.First().LineData["レコード件数"]) == LoadFile.LoadData.Count(x => x.KBN == "2" && x.LineData["決済対象区分"] == "0")))
            {
                throw new Exception("データレコード件数が不正です");
            }
            Data = LoadFile.LoadData.Where(x => x.KBN == "8" && x.LineData["集計区分"] == "1");
            if (!(Data.Count() == 1 &&
                  long.Parse(Data.First().LineData["レコード件数"]) == LoadFile.LoadData.Count(x => x.KBN == "2" && x.LineData["決済対象区分"] != "0" && x.LineData["決済対象区分"] != "Z")))
            {
                throw new Exception("データレコード件数が不正です");
            }
            Data = LoadFile.LoadData.Where(x => x.KBN == "8" && x.LineData["集計区分"] == "Z");
            if (!(Data.Count() == 1 &&
                  long.Parse(Data.First().LineData["レコード件数"]) == LoadFile.LoadData.Count(x => x.KBN == "2" && x.LineData["決済対象区分"] == "Z")))
            {
                throw new Exception("データレコード件数が不正です");
            }

            return LoadFile;
        }

        #endregion

        #region 交換尻関連

        /// <summary>
        /// 証券明細テキスト取込処理
        /// 交換尻関連
        /// </summary>
        private bool TextImportBalance(int Type, DetailTextImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("証券明細テキスト取込処理(交換尻)：{0}", _itemMgr._TargetFilename), 3);

            int CreateDateDiff = 0;
            switch (_itemMgr._file_divid)
            {
                case "SFA":
                case "SFB":
                    // 交換尻データ確定版

                    // 確定版の場合は登録済の速報版データ削除
                    if (!TxtImp.DeleteBillMeiTxtBalance(Type, dbp, Tran)) return false;

                    // 登録する作成日はそのまま
                    CreateDateDiff = 0;
                    break;
                case "SPA":
                case "SPB":
                    // 交換尻データ速報版

                    // 登録する作成日は作成日の翌営業日
                    CreateDateDiff = 1;
                    break;
                default:
                    break;
            }

            //対象データの登録
            if (!TxtImp.ImportBillMeiTxtCtl(CreateDateDiff, dbp, Tran)) return false;
            if (!TxtImp.ImportBillMeiTxtChgDate(dbp, Tran)) return false;

            return true;
        }

        #endregion

        #region 持帰ダウンロード関連

        /// <summary>
        /// 証券明細テキスト取込処理
        /// 持帰ダウンロード関連
        /// </summary>
        public bool TextImportGDA(DetailTextImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("証券明細テキスト取込処理(持帰ダウンロード)：{0}", _itemMgr._TargetFilename), 3);

            // 「持帰要求結果管理」テーブルの証券明細テキスト取込状態を更新
            if (_itemMgr.UpdateICREQRETCtl(5, dbp, Tran) == 0)
            {
                throw new Exception("持帰要求結果管理テーブルに対象の証券明細テキストファイルの定義がありませんでした");
            }

            // 持帰要求結果証券明細テキスト登録
            if (!TxtImp.ImportICReqRetBillMeiTxt(dbp, Tran)) return false;

            return true;
        }

        #endregion

        #region 当日持出持出明細関連

        /// <summary>
        /// 証券明細テキスト取込処理
        /// 当日持出明細関連
        /// </summary>
        public bool TextImportBQA(DetailTextImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("証券明細テキスト取込処理(当日持出明細)：{0}", _itemMgr._TargetFilename), 3);

            //登録済のデータ削除
            if (!TxtImp.DeleteBillMeiTxt(dbp, Tran)) return false;

            //対象データの登録(登録する作成日はそのまま)
            if (!TxtImp.ImportBillMeiTxtCtl(0, dbp, Tran)) return false;
            if (!TxtImp.ImportBillMeiTxt(dbp, Tran)) return false;

            IEnumerable<IFData> TRData = TxtImp._DataRecord.Where(x => x.LineData["持出時接続方式"] == "1" || x.LineData["持出時接続方式"] == "3");
            foreach (var data in TRData.Where(x => x.LineData["表・裏等の別"] == "01")
                                       .GroupBy(x => new { BRNo = x.LineData["持出支店コード"], OCDate = x.LineData["持出日"] }))
            {
                // 持出時接続方式「2」以外の処理

                int BRNo = 0;
                int OCDate = 0;
                int.TryParse(data.Key.BRNo, out BRNo);
                int.TryParse(data.Key.OCDate, out OCDate);

                //バッチ番号取得
                int BatchNumber = 0;
                for (int i = 1; i <= _ctl.BatchSeqRetryCount; i++)
                {
                    if (_itemMgr.GetBatchNumber(AplInfo.OpDate(), out BatchNumber))
                    {
                        break;
                    }
                }
                if (BatchNumber <= 0)
                {
                    //バッチ番号が取得できない場合
                    throw new Exception("他端末で処理中のためバッチ番号を取得できませんでした");
                }

                // データ登録
                IEnumerable<string> FrontList = data.Select(x => x.LineData["表証券イメージファイル名"]);
                TRManager manager = new TRManager(_ctl, AplInfo.OpDate(), AppInfo.Setting.SchemaBankCD, BatchNumber, BRNo, OCDate,
                                                  TRData.Where(x => FrontList.Contains(x.LineData["表証券イメージファイル名"])));
                if (!manager.TRDataInput(dbp, Tran))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

    }
}
