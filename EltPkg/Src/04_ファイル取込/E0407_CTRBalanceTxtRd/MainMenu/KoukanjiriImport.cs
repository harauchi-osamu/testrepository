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

namespace MainMenu
{
    class KoukanjiriImport
    {
        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        #endregion

        // XMLPath
        private string XMLPath { 
            get 
            {
                return Path.Combine(System.Windows.Forms.Application.StartupPath, string.Format("Resources/{0}Load.xml", _itemMgr._file_id));
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KoukanjiriImport(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 交換尻テキスト取込処理
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
                    KoukanjiriImportCommon TxtImp = new KoukanjiriImportCommon(_ctl, LoadFile);
                    ImportFlg = TextImportBalance(TxtImp, dbp, Tran);

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
            if (!(LoadFile.LoadData.Count(x => x.KBN == "8") == 1 && LoadFile.LoadData.Skip(LoadFile.LoadData.Count - 2).Take(1).Count(x => x.KBN == "8") == 1))
            {
                throw new Exception("トレーラ・レコードが不正です");
            }

            // データレコード件数チェック
            IEnumerable<IFData> Data = LoadFile.LoadData.Where(x => x.KBN == "8");
            if (!(Data.Count() == 1  &&
                  long.Parse(Data.First().LineData["レコード件数"]) == LoadFile.LoadData.Count(x => x.KBN == "2")))
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
        private bool TextImportBalance(KoukanjiriImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("証券明細テキスト取込処理(交換尻)：{0}", _itemMgr._TargetFilename), 3);

            switch (_itemMgr._file_divid)
            {
                case "SFA":
                    // 交換尻データ確定版

                    // 確定版の場合は登録済の速報版データ削除
                    if (!TxtImp.DeleteBalanceTxt(dbp, Tran)) return false;

                    break;
                default:
                    break;
            }

            //対象データの登録
            if (!TxtImp.ImportBalanceTxtCtl(dbp, Tran)) return false;
            if (!TxtImp.ImportBalanceTxt(dbp, Tran)) return false;

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("証券明細テキスト取込処理(交換尻)：{0},件数：{1}", _itemMgr._TargetFilename, TxtImp._DataRecord.LongCount()), 3);

            return true;
        }

        #endregion

    }
}
