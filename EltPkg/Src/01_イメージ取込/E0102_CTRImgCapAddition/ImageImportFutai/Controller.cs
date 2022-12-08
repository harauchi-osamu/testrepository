﻿using System;
using EntryCommon;
using System.Configuration;
using System.Collections.Generic;

namespace ImageImportFutai
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        /// <summary>設定ファイル情報</summary>
        public SettingData SettingData { get; private set; } = new SettingData();


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// コントローラー初期化
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
        /// 引数を設定する
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool SetArgs(string[] args)
        {
            string MenuNumber = args[0];
            base.MenuNumber = MenuNumber;

            return true;
        }

        #region 設定チェック

        /// <summary>
        /// Operator.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckOperatorIni()
        {
            SettingData.ChkParam(NCR.Operator.UserID, "ユーザーID");
            SettingData.ChkParam(NCR.Operator.UserName, "ユーザー名");
            // 特権レベルは設定がなければ「0」扱い

            return true;
        }

        /// <summary>
        /// Term.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckTermIni()
        {
            SettingData.ChkParam(NCR.Terminal.Number, "端末番号");
            SettingData.ChkParam(NCR.Terminal.ServeriniPath, "CtrServer.iniパス");

            return true;
        }

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckServerIni()
        {
            // ServerIniファイル存在チェック
            
            if (!SettingData.ServerIniExists())
            {
                return false;
            }

            SettingData.ChkParam(NCR.Server.BankNM, "銀行名");
            SettingData.ChkParam(NCR.Server.Environment, "環境");
            SettingData.ChkParam(NCR.Server.ScanFutaiImageRoot, "付帯バッチルート情報(スキャン)");
            SettingData.ChkParam(NCR.Server.BankFutaiImageRoot, "付帯バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.ScanImageBackUpRoot, "スキャン退避フォルダ情報");
            SettingData.ChkParam(NCR.Server.ContractBankCd, "受託元銀行番号");
            SettingData.ChkParam(NCR.Server.HandlingBankCdList, "設定可能な銀行一覧");
            SettingData.ChkParam(NCR.Server.Tegata, "イメージ切取手形枠");
            SettingData.ChkParam(NCR.Server.Kogitte, "イメージ切取小切手枠");
            SettingData.ChkParam(NCR.Server.DstDpi, "イメージ切取解像度");
            SettingData.ChkParam(NCR.Server.Quality, "イメージ切取クオリティ");
            SettingData.ChkParam(NCR.Server.OC_OCRLevel, "OCR信頼度比較(持出)");

            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            return SettingData.CheckAppData();
        }

        #endregion 

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

    }
}
