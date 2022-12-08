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

namespace StartDayOperation
{
    /// <summary>
    /// トランザクションテーブル管理クラス
    /// </summary>
    public class ItemManager : ManagerBase
    {
        private MasterManager _masterMgr = null;

        /// <summary>画面パラメーター</summary>
        public StartDispParams DispParams { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemManager(MasterManager mst)
        {
            _masterMgr = mst;
            this.DispParams = new StartDispParams();
        }

        /// <summary>
        /// ＤＢからデータ取得してデータセットに格納
        /// </summary>
        public override void FetchAllData() 
        {
        }

        /// <summary>
        /// 業務日付更新SQL実行
        /// </summary>
        public bool UpdateGymDate(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                TBL_OPERATION_DATE OpeDate = new TBL_OPERATION_DATE(DispParams.UpdateGymDate);
                string strSQL = OpeDate.GetUpdateQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                // 業務日付再取得
                DispParams.CurGymDate = DispParams.UpdateGymDate;
                if (!AplInfo.GetGymSetting(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD))
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "業務設定取得エラー ", 3);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        /// <summary>
        /// トランザクションデータ削除SQL実行
        /// </summary>
        public int DeleteTRData(string SQL, string Schema, int Date, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            int Result = -1;
            try
            {
                string strSQL = string.Format(SQL, Schema, Date);
                Result = dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
            return Result;
        }

        /// <summary>
        /// イメージ情報一覧を取得
        /// </summary>
        public bool GetImgList(int BankCd, out List<ImgFileData> ImgList, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // 初期化
            ImgList = new List<ImgFileData>();

            try
            {
                string strSQL = string.Empty;

                // TRMEIIMG一覧取得
                strSQL = TBL_TRMEIIMG.GetSelectQuery(BankCd);
                using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans))
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        // 追加
                        TBL_TRMEIIMG meiimg = new TBL_TRMEIIMG(tbl.Rows[i], BankCd);
                        if (!string.IsNullOrEmpty(meiimg.m_IMG_FLNM))
                        {
                            ImgList.Add(new ImgFileData(meiimg));
                        }
                    }
                }

                // TRBATCHIMG一覧取得
                strSQL = TBL_TRBATCHIMG.GetSelectQuery(BankCd);
                using (DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>(), Tran.Trans))
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        // 追加
                        TBL_TRBATCHIMG batimg = new TBL_TRBATCHIMG(tbl.Rows[i], BankCd);
                        if (!string.IsNullOrEmpty(batimg.m_IMG_FLNM))
                        {
                            ImgList.Add(new ImgFileData(batimg));
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        /// <summary>
        /// 通常バッチルートフォルダパスを取得
        /// </summary>
        public string BankNormalImageRoot(int BankCd)
        {
            return string.Format(NCR.Server.BankNormalImageRoot, BankCd);
        }

        /// <summary>
        /// 付帯バッチルートフォルダパスを取得
        /// </summary>
        public string BankFutaiImageRoot(int BankCd)
        {
            return string.Format(NCR.Server.BankFutaiImageRoot, BankCd);
        }

        /// <summary>
        /// 期日管理バッチルートフォルダパスを取得
        /// </summary>
        public string BankInventoryImageRoot(int BankCd)
        {
            return string.Format(NCR.Server.BankInventoryImageRoot, BankCd);
        }

        /// <summary>
        /// 持帰ダウンロード確定イメージルートフォルダパスを取得
        /// </summary>
        public string BankConfirmImageRoot(int BankCd)
        {
            return string.Format(NCR.Server.BankConfirmImageRoot, BankCd);
        }

        /// <summary>
        /// バッチフォルダからキー情報を取得
        /// </summary>
        public void GetBatFolderKey(string BatFolderPath, out int GymID, out int OpeDate, out int BatID)
        {
            string FullPath = BatFolderPath.TrimEnd(Path.DirectorySeparatorChar);
            string BatFolderName = Path.GetFileName(FullPath);

            if (BatFolderName.Length != 19)
            {
                // 規定の長さでない場合
                GymID = -99;
                OpeDate = -99;
                BatID = -99;

                return;
            }

            // 業務ID
            if (!int.TryParse(BatFolderName.Substring(0, 3), out GymID))
            {
                GymID = -99;
            }

            // 処理日付
            if (!int.TryParse(BatFolderName.Substring(3, 8), out OpeDate))
            {
                OpeDate = -99;
            }

            // バッチID
            if (!int.TryParse(BatFolderName.Substring(11, 8), out BatID))
            {
                BatID = -99;
            }

            return;
        }

        /// <summary>
        /// 画面パラメーター
        /// </summary>
        public class StartDispParams
        {
            public int CurGymDate { get; set; } = 0;
            public int UpdateGymDate { get; set; } = 0;
        }

        /// <summary>
        /// イメージデータ
        /// </summary>
        public class ImgFileData
        {
            public int GymID = 0;
            public int OpeDate = 0;
            public int BatID = 0;
            public string ImgFLNM = "";

            public ImgFileData(TBL_TRBATCHIMG batimg)
            {
                GymID = batimg._GYM_ID;
                OpeDate = batimg._OPERATION_DATE;
                BatID = batimg._BAT_ID;
                ImgFLNM = batimg.m_IMG_FLNM;
            }

            public ImgFileData(TBL_TRMEIIMG meiimg)
            {
                GymID = meiimg._GYM_ID;
                OpeDate = meiimg._OPERATION_DATE;
                BatID = meiimg._BAT_ID;
                ImgFLNM = meiimg.m_IMG_FLNM;
            }
        }

    }
}
