using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Util
{
    public static class WindowMessage
    {
        public const int WM_DRAWCLIPBOARD = 0x0308;

        public const int WM_CHANGECBCHAIN = 0x030D;
    }

    public static class WinApi
    {
        [DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
        internal static extern IntPtr SetClipboardViewer( IntPtr hWndNewViewer );

        [DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
        internal static extern bool ChangeClipboardChain( IntPtr hWndRemove, IntPtr hWndNewNext );

        [DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
        internal static extern IntPtr SendMessage( IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam );
    }
}
