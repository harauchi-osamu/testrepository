using System;
using System.IO;
using Microsoft.Win32;

namespace CommonClass
{
    public class RegistryAccess
	{
        private const string RegOperator = @"Operator\";
		private const string RegPath =  @"Software\NCR\BICS\Local";

		private const string RemoteRegPath =  @"Software\NCR\BICS";
        internal static string SERVER = ""; 
        internal static string ALIAS = "";

        public RegistryAccess()
        {
        }

        #region レジストリ取得・設定メソッド

        public string getLMKeyValue(string ValueName)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey(RegPath);
			string Ret = Convert.ToString(key.GetValue(ValueName));
			return Ret;
		}

        public string getLMKeyValue(string Subkey, string ValueName)
        {
			string sk = this.getSubKey(Subkey);
			RegistryKey key = Registry.LocalMachine.OpenSubKey(sk);
			string Ret = Convert.ToString(key.GetValue(ValueName));
			return Ret;
		}

        public string getLMKeyValueDWORD(string Subkey, string ValueName)
        {
			string sk = this.getSubKey(Subkey);
			RegistryKey key = Registry.LocalMachine.OpenSubKey(sk);
			string Ret = Convert.ToInt32(key.GetValue(ValueName)).ToString();
			return Ret;
		}

		internal string getRemoteLMKeyValue( string Subkey,string ValueName )
        {
			RegistryKey key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine,SERVER).OpenSubKey(this.getRemoteSubKey(Subkey));
			string Ret = Convert.ToString(key.GetValue(ValueName));
			return Ret;
        }

        internal string getRemoteLMKeyValueDWORD(string Subkey, string ValueName)
        {
            RegistryKey key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, SERVER).OpenSubKey(this.getRemoteSubKey(Subkey));
            string Ret = Convert.ToInt32(key.GetValue(ValueName)).ToString();
            return Ret;
        }

		internal void setLMKeyValue(string Subkey,string ValueName,object Value)
		{
			RegistryKey key = Registry.LocalMachine.OpenSubKey( this.getSubKey(Subkey), true );
			if( key == null)
			{
				key = Registry.LocalMachine.CreateSubKey( this.getSubKey(Subkey) );
			}
			key.SetValue(ValueName,Value);
			key.Close();
        }

        private string getPathNcr()
        {
            return RegPath;
        }

        private string getRemotePathNcr()
        {
            return RemoteRegPath + @"\" + ALIAS;
        }

        private string getSubKey(string SubKey)
        {
            SubKey = Path.Combine(this.getPathNcr(), SubKey);
            return SubKey;
        }

        private string getRemoteSubKey(string SubKey)
        {
            SubKey = Path.Combine(this.getRemotePathNcr(), SubKey);
            return SubKey;
        }

        #endregion
    }
}
