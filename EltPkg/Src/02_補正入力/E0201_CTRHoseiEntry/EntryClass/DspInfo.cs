using System.Collections.Generic;
using System.Data;
using System.Linq;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

namespace EntryClass
{
	/// <summary>
	/// 画面情報
	/// </summary>
	public class DspInfo
	{
        // *******************************************************************
        // キー項目
        // *******************************************************************

        public string Key
		{
			get
			{
				string key =
					DBConvert.ToStringNull(dsp_param._GYM_ID) + "_" +
                    DBConvert.ToStringNull(dsp_param._DSP_ID);
				return key;
			}
		}

        public int _GYM_ID { get { return DBConvert.ToIntNull(dsp_param._GYM_ID); } }
        public int _DSP_ID { get { return DBConvert.ToIntNull(dsp_param._DSP_ID); } }
        public int _HOSEI_ITEMMODE { get; private set; }


        // *******************************************************************
        // トランザクションデータ
        // *******************************************************************

        /// <summary>画面パラメータ（自分）</summary>
        public TBL_DSP_PARAM dsp_param { get; set; }

        /// <summary>イメージパラメータ</summary>
        public TBL_IMG_PARAM img_param { get; set; }

        /// <summary>補正モードパラメータ</summary>
        public TBL_HOSEIMODE_PARAM hosei_param { get; set; }

        /// <summary>画面項目定義（key=ITEM_ID, val=TBL_DSP_ITEM）</summary>
        public SortedList<int, TBL_DSP_ITEM> dsp_items { get; set; }

        /// <summary>補正モード画面項目定義（key=ITEM_ID, val=TBL_HOSEIMODE_DSP_ITEM）</summary>
        public SortedList<int, TBL_HOSEIMODE_DSP_ITEM> hosei_items { get; set; }

        /// <summary>イメージカーソルパラメータ（key=ITEM_ID, val=TBL_IMG_CURSOR_PARAM）</summary>
        public SortedList<int, TBL_IMG_CURSOR_PARAM> img_cursor_params { get; set; }

        // *******************************************************************
        // トランザクションデータ（オリジナル）
        // *******************************************************************

        /// <summary>画面パラメータ（自分）</summary>
        public TBL_DSP_PARAM org_dsp_param { get; set; }

        /// <summary>イメージパラメータ</summary>
        public TBL_IMG_PARAM org_img_param { get; set; }

        /// <summary>補正モードパラメータ</summary>
        public TBL_HOSEIMODE_PARAM org_hosei_param { get; set; }

        /// <summary>画面項目定義（key=ITEM_ID, val=TBL_DSP_ITEM）</summary>
        public SortedList<int, TBL_DSP_ITEM> org_dsp_items { get; set; }

        /// <summary>補正モード画面項目定義（key=ITEM_ID, val=TBL_HOSEIMODE_DSP_ITEM）</summary>
        public SortedList<int, TBL_HOSEIMODE_DSP_ITEM> org_hosei_items { get; set; }

        /// <summary>イメージカーソルパラメータ（key=ITEM_ID, val=TBL_IMG_CURSOR_PARAM）</summary>
        public SortedList<int, TBL_IMG_CURSOR_PARAM> org_img_cursor_params { get; set; }


        // *******************************************************************
        // プロパティ
        // *******************************************************************

        public bool HasOriginal { get; private set; }


        // *******************************************************************
        // メソッド
        // *******************************************************************

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="row"></param>
        public DspInfo(int hosei_itemmode, bool hasOriginal = false)
		{
            _HOSEI_ITEMMODE = hosei_itemmode;

            dsp_param = new TBL_DSP_PARAM(AppInfo.Setting.SchemaBankCD);
            img_param = new TBL_IMG_PARAM(AppInfo.Setting.SchemaBankCD);
            hosei_param = new TBL_HOSEIMODE_PARAM(AppInfo.Setting.SchemaBankCD);
            dsp_items = new SortedList<int, TBL_DSP_ITEM>();
            hosei_items = new SortedList<int, TBL_HOSEIMODE_DSP_ITEM>();
            img_cursor_params = new SortedList<int, TBL_IMG_CURSOR_PARAM>();

            HasOriginal = hasOriginal;
            if (HasOriginal)
            {
                org_dsp_param = new TBL_DSP_PARAM(AppInfo.Setting.SchemaBankCD);
                org_img_param = new TBL_IMG_PARAM(AppInfo.Setting.SchemaBankCD);
                org_hosei_param = new TBL_HOSEIMODE_PARAM(AppInfo.Setting.SchemaBankCD);
                org_dsp_items = new SortedList<int, TBL_DSP_ITEM>();
                org_hosei_items = new SortedList<int, TBL_HOSEIMODE_DSP_ITEM>();
                org_img_cursor_params = new SortedList<int, TBL_IMG_CURSOR_PARAM>();
            }
        }

        /// <summary>
        /// クリアする
        /// </summary>
        public void Dispose()
        {
            dsp_param = null;
            img_param = null;
            hosei_param = null;
            dsp_items.Clear();
            hosei_items.Clear();
            img_cursor_params.Clear();

            if (HasOriginal)
            {
                org_dsp_param = null;
                org_img_param = null;
                org_hosei_param = null;
                org_dsp_items.Clear();
                org_hosei_items.Clear();
                org_img_cursor_params.Clear();
            }
        }

        /// <summary>
        /// 画面パラメータを設定する
        /// </summary>
        /// <param name="row"></param>
        public void SetDspParam(DataRow row)
        {
            dsp_param = new TBL_DSP_PARAM(row, AppInfo.Setting.SchemaBankCD);
            if (HasOriginal)
            {
                org_dsp_param = new TBL_DSP_PARAM(row, AppInfo.Setting.SchemaBankCD);
            }
        }

        /// <summary>
        /// イメージパラメータを設定する
        /// </summary>
        /// <param name="row"></param>
        public void SetImgParam(DataRow row)
        {
            img_param = new TBL_IMG_PARAM(row, AppInfo.Setting.SchemaBankCD);
            if (HasOriginal)
            {
                org_img_param = new TBL_IMG_PARAM(row, AppInfo.Setting.SchemaBankCD);
            }
        }

        /// <summary>
        /// 補正モードパラメータを設定する
        /// </summary>
        /// <param name="row"></param>
        public void SetHoseiParam(DataRow row)
        {
            hosei_param = new TBL_HOSEIMODE_PARAM(row, AppInfo.Setting.SchemaBankCD);
            if (HasOriginal)
            {
                org_hosei_param = new TBL_HOSEIMODE_PARAM(row, AppInfo.Setting.SchemaBankCD);
            }
        }

        /// <summary>
        /// 画面項目定義を追加する
        /// </summary>
        /// <param name="row"></param>
        public void AddDspItem(DataRow row, bool addOrg = true)
        {
            TBL_DSP_ITEM data = new TBL_DSP_ITEM(row, AppInfo.Setting.SchemaBankCD);
            if (!dsp_items.ContainsKey(data._ITEM_ID))
            {
                dsp_items.Add(data._ITEM_ID, data);
            }
            if (HasOriginal && addOrg)
            {
                TBL_DSP_ITEM org = new TBL_DSP_ITEM(row, AppInfo.Setting.SchemaBankCD);
                if (!org_dsp_items.ContainsKey(data._ITEM_ID))
                {
                    org_dsp_items.Add(org._ITEM_ID, org);
                }
            }
        }

        /// <summary>
        /// 補正モード画面項目定義を追加する
        /// </summary>
        /// <param name="row"></param>
        public void AddHoseiItem(DataRow row, bool addOrg = true)
        {
            TBL_HOSEIMODE_DSP_ITEM data = new TBL_HOSEIMODE_DSP_ITEM(row, AppInfo.Setting.SchemaBankCD);
            if (!hosei_items.ContainsKey(data._ITEM_ID))
            {
                hosei_items.Add(data._ITEM_ID, data);
            }
            if (HasOriginal && addOrg)
            {
                TBL_HOSEIMODE_DSP_ITEM org = new TBL_HOSEIMODE_DSP_ITEM(row, AppInfo.Setting.SchemaBankCD);
                if (!org_hosei_items.ContainsKey(data._ITEM_ID))
                {
                    org_hosei_items.Add(org._ITEM_ID, org);
                }
            }
        }

        /// <summary>
        /// イメージカーソルパラメータを追加する
        /// </summary>
        /// <param name="row"></param>
        public void AddImgCursor(DataRow row, bool addOrg = true)
        {
            TBL_IMG_CURSOR_PARAM data = new TBL_IMG_CURSOR_PARAM(row, AppInfo.Setting.SchemaBankCD);
            if (!img_cursor_params.ContainsKey(data._ITEM_ID))
            {
                img_cursor_params.Add(data._ITEM_ID, data);
            }
            if (HasOriginal && addOrg)
            {
                TBL_IMG_CURSOR_PARAM org = new TBL_IMG_CURSOR_PARAM(row, AppInfo.Setting.SchemaBankCD);
                if (!org_img_cursor_params.ContainsKey(data._ITEM_ID))
                {
                    org_img_cursor_params.Add(org._ITEM_ID, org);
                }
            }
        }

        /// <summary>
        /// 画面項目定義を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public TBL_DSP_ITEM GetDspItem(int itemid)
        {
            if (dsp_items.ContainsKey(itemid)) { return dsp_items[itemid]; }
            return null;
        }

        /// <summary>
        /// 補正モード画面項目定義を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public TBL_HOSEIMODE_DSP_ITEM GetHoseiDspItem(int itemid)
        {
            if (hosei_items.ContainsKey(itemid)) { return hosei_items[itemid]; }
            return null;
        }

        /// <summary>
        /// イメージカーソルパラメータを取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public TBL_IMG_CURSOR_PARAM GetImageCursor(int itemid)
        {
            if (img_cursor_params.ContainsKey(itemid)) { return img_cursor_params[itemid]; }
            return null;
        }
    }
}
