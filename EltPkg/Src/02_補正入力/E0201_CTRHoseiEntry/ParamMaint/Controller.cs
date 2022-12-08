using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using EntryClass;
using EntryCommon;

namespace ParamMaint
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerEntBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ctl"></param>
        public Controller(ControllerEntBase ctl)
        {
            this.SettingData = ctl.SettingData;
            this.MenuNumber = ctl.MenuNumber;
            this.GymId = -1;
            this.HoseiItemMode = ctl.HoseiItemMode;
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

        /// <summary>
        /// Operator.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckOperatorIni()
        {
            return true;
        }

        /// <summary>
        /// Term.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckTermIni()
        {
            return true;
        }

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckServerIni()
        {
            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            return true;
        }

        /// <summary>
        /// 業務名を取得する
        /// </summary>
        /// <param name="gymid"></param>
        /// <returns></returns>
        public string GetGymName(int gymid)
        {
            if (!_itemMgr.GymParam.ContainsKey(gymid)) { return ""; }
            return _itemMgr.GymParam[gymid].gym_param.m_GYM_KANJI;
        }

        /// <summary>
        /// 画面名を取得する
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="dspid"></param>
        /// <returns></returns>
        public string GetDspName(int gymid, int dspid)
        {
            if (!_itemMgr.GymParam.ContainsKey(gymid)) { return ""; }
            if (!_itemMgr.GymParam[gymid].DspInfos.ContainsKey(dspid)) { return ""; }
            return _itemMgr.GymParam[gymid].DspInfos[dspid].dsp_param.m_DSP_NAME;
        }

        /// <summary>
        /// 遷移先画面パラメーターを設定する
        /// </summary>
        /// <param name="type"></param>
        public void SetDispPramsGym(AplInfo.EditType type, int srcGymId, int dstGymId)
        {
            _itemMgr.DispParams.ClearGym();
            _itemMgr.DispParams.GymId = dstGymId;
            _itemMgr.DispParams.SrcGymId = srcGymId;
            _itemMgr.DispParams.DstGymId = dstGymId;
            _itemMgr.DispParams.ProcGymType = type;

            var prevGyms = _itemMgr.GymParam.Values.Where(p => p._GYM_ID < dstGymId);
            if (prevGyms.Count() > 0)
            {
                _itemMgr.DispParams.PrevGymId = prevGyms.Last()._GYM_ID;
            }
            var nextGyms = _itemMgr.GymParam.Values.Where(p => p._GYM_ID > dstGymId);
            if (nextGyms.Count() > 0)
            {
                _itemMgr.DispParams.NextGymId = nextGyms.First()._GYM_ID;
            }
        }

        /// <summary>
        /// 遷移先画面パラメーターを設定する
        /// </summary>
        /// <param name="type"></param>
        public void SetDispPramsDsp(AplInfo.EditType type, int srcDspId, int dstDspId)
        {
            _itemMgr.DispParams.ClearDsp();
            _itemMgr.DispParams.DspId = dstDspId;
            _itemMgr.DispParams.SrcDspId = srcDspId;
            _itemMgr.DispParams.DstDspId = dstDspId;
            _itemMgr.DispParams.ProcDspType = type;

            var prevDsps = _itemMgr.GymParam[_itemMgr.DispParams.GymId].DspInfos.Values.Where(p => p._DSP_ID < dstDspId);
            if (prevDsps.Count() > 0)
            {
                _itemMgr.DispParams.PrevDspId = prevDsps.Last()._DSP_ID;
            }
            var nextDsps = _itemMgr.GymParam[_itemMgr.DispParams.GymId].DspInfos.Values.Where(p => p._DSP_ID > dstDspId);
            if (nextDsps.Count() > 0)
            {
                _itemMgr.DispParams.NextDspId = nextDsps.First()._DSP_ID;
            }
        }

    }
}
