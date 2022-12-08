using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CommonClass
{
    class TarArchiveHandler
    {

        [DllImport("tar32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        internal static extern uint Tar(
            int _hwnd,
            string _szCmdLine,
            string _szOutput,
            int _dwSize);
    }
}
