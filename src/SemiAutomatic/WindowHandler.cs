using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using HWND = System.IntPtr;

namespace Test
{
    class WindowHandler
    {

        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = NATIVE_User32_dll.GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            NATIVE_User32_dll.EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!NATIVE_User32_dll.IsWindowVisible(hWnd)) return true;

                int length = NATIVE_User32_dll.GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                NATIVE_User32_dll.GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }
    }
}