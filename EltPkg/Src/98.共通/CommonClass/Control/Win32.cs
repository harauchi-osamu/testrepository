using System;
using System.Runtime.InteropServices;

namespace CommonClass
{
	/// <summary>
	/// clsWin32 の概要の説明です。
	/// </summary>
	internal class Win32 {

		[DllImport("Kernel32.dll")]
		internal static extern bool Beep( uint dwFreq, uint dwDuration );

		internal static bool iBicsBeep() {

            bool ret = false;
            //ret = Beep(1000, 150);
            return ret;
		}

		internal static bool iBicsBeep( uint dwFreq, uint dwDuration ) 
		{
            bool ret = false;
            //ret = Beep(dwFreq, dwDuration);
            return ret;
		}
	}
}
