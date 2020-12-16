using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using HWND = System.IntPtr;

namespace Test
{
    public class NATIVE_User32_dll
    {
        public delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("USER32.DLL")]
        public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        public static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        public static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        public static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        public static extern IntPtr GetShellWindow();
    }
}
