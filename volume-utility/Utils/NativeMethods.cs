using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace volume_utility.Utils
{
    internal class NativeMethods
    {
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);
        /// <summary>
        /// ウィンドウの端を丸めるスタイルを有効にする
        /// </summary>
        /// <param name="handle"></param>
        internal static void EnableRoundWindowStyle(nint handle)
        {
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(handle, attribute, ref preference, sizeof(uint));
        }

        /// <summary>
        /// カスタムフックイベント
        /// </summary>
        internal const uint WM_HOOK_ACTIVATE = 0x8001;

        [DllImport(@".\EventHookModule.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool StartHook(IntPtr hWnd);

        [DllImport(@".\EventHookModule.dll", CharSet = CharSet.Unicode, CallingConvention =CallingConvention.StdCall)]
        internal static extern bool EndHook();

        [DllImport("user32.dll")]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        /// ウィンドウハンドルからプロセス名を取得する
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        internal static Process? getProcess(IntPtr hWnd)
        {
            try
            {
                GetWindowThreadProcessId(hWnd, out int processId);
                if (processId != 0)
                {
                    return Process.GetProcessById(processId);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
    }
}
