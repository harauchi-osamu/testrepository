using System;
using System.Runtime.InteropServices;

namespace IIPCommonClass
{
	/// <summary>
	/// clsWin32 の概要の説明です。
	/// </summary>
	internal class Win32 {

		[DllImport("Kernel32.dll")]
		internal static extern bool Beep( uint dwFreq, uint dwDuration );

		internal static bool iBicsBeep() {

			return Beep(1000, 150);
		}

		internal static bool iBicsBeep( uint dwFreq, uint dwDuration ) 
		{

			return Beep(dwFreq, dwDuration);
		}
	}
}
