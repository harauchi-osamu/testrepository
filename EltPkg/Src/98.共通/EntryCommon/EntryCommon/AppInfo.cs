using System;
using System.Collections.Generic;
using CommonClass;

namespace EntryCommon
{
	public class AppInfo
	{
		public static AppInfo Setting { get; private set; }

		/// <summary>権限有無</summary>
		public bool IsAuth { get { return (AplInfo.OP_PRIV() > 0); } }

		/// <summary>業務ID</summary>
		public int GymId { get; private set; } = 0;

		/// <summary>スキーマ銀行番号</summary>
		public int SchemaBankCD { get; private set; } = 0;

		/// <summary>権限リスト</summary>
		public SortedList<int, string> AuthList { get; private set; }

		/// <summary>熟練度リスト</summary>
		public SortedList<string, int> RankList { get; private set; }


		/// <summary>
		/// コンストラクタ
		/// </summary>
		private AppInfo()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		static AppInfo()
		{
			Setting = new AppInfo();
		}

        /// <summary>
        /// 業務IDを設定する
        /// </summary>
        /// <param name="gymid"></param>
        public void SetGymId(int gymid)
        {
            this.GymId = gymid;
        }

		/// <summary>
		/// スキーマ銀行番号を設定する
		/// </summary>
		/// <param name="bankcd">銀行番号</param>
		public void SetSchemaBankCD(int bankcd)
		{
			this.SchemaBankCD = bankcd;
		}

	}
}
