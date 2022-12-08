using System;

namespace NCR.GeneralMainte
{
	/// <summary>
	/// ICheck の概要の説明です。
	/// </summary>
	public interface ICheck
	{
		string IsOk(string ColumnName,string InputValue);

        string SetColumnInfo(string ColumnName, string DefaultValue);
	}
}
