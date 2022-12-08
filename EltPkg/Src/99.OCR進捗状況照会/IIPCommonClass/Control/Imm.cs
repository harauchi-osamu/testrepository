using System.Runtime.InteropServices;
using System.Diagnostics;
using System;

namespace IIPCommonClass
{
	internal class Imm {
        [DllImport("User32.dll")]
        internal static extern bool GetKeyboardState(byte[] lpByteVar);

        [DllImport("User32.dll")]
        internal static extern bool SetKeyboardState(byte[] keystate);

        [DllImport("imm32.dll")]
        internal static extern int ImmSetConversionStatus(int hIMC, int fdwConversion, int fdwSentence);

        [DllImport("imm32.dll")]
        internal static extern int ImmGetContext(int hWnd);

        [DllImport("imm32.dll")]
        internal static extern int ImmReleaseContext(int hWnd, int hIMC);

        [DllImport("User32.dll")]
        internal static extern IntPtr GetMessageExtraInfo();

        [DllImport("User32.dll")]
        internal extern static int SendInput(int nInputs, ref INPUT pInputs, int cvSize);

        [DllImport("User32.dll", EntryPoint = "MapVirtualKeyA")]
        internal static extern short MapVirtualKey(int wCode, int wMapType);
    }

    public enum ENTRYMODE
    {
		IMEOFF_KANA = 0,
		IMEOFF_ALPHA = 1,
		IMEON_HANKAKU_KANA = 2,
		IMEON_ZENKAKU_HIRAGANA = 3,
		IMEON_ROMAN_ZENKAKU_HIRAGANA = 4,
		IMEON_ROMAN_HANKAKU_KANA = 5,
		IMEON = 9
	}

    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT
    {
        [FieldOffset(0)]
        public short Type;
        [FieldOffset(4)]
        public MOUSEINPUT mi;
        [FieldOffset(4)]
        public KEYBDINPUT ki;
        [FieldOffset(4)]
        public HARDWAREINPUT hi;
    }

    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

}
