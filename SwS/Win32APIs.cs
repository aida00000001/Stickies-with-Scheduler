using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;


namespace SwS
{
    /// <summary>
    /// Win32APIs 管理クラス
    /// </summary>
    class Win32APIs
    {
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern bool SetCapture(IntPtr hWnd);

        /// <summary>
        /// システム日時管理クラス
        /// </summary>
        public partial class SysDateTime
        {
            // Win32APIs外部宣言
            [DllImport("kernel32.dll")]
            private static extern bool SetLocalTime(ref SystemTime sysTime);

            // システム日付管理構造体
            private struct SystemTime
            {
                public ushort wYear;
                public ushort wMonth;
                public ushort wDayOfWeek;
                public ushort wDay;
                public ushort wHour;
                public ushort wMinute;
                public ushort wSecond;
                public ushort wMiliseconds;
            }

            /// <summary>
            /// 現在のシステム日時を設定する
            /// </summary>
            /// <param name="dt">設定する日時</param>
            public static void SetNowDateTime(DateTime dt)
            {
                //システム日時に設定する日時を指定する
                SystemTime sysTime = new SystemTime();
                sysTime.wYear = (ushort)dt.Year;
                sysTime.wMonth = (ushort)dt.Month;
                sysTime.wDay = (ushort)dt.Day;
                sysTime.wHour = (ushort)dt.Hour;
                sysTime.wMinute = (ushort)dt.Minute;
                sysTime.wSecond = (ushort)dt.Second;
                sysTime.wMiliseconds = (ushort)dt.Millisecond;
                //システム日時を設定する
                SetLocalTime(ref sysTime);
            }
        }

        /// <summary>
        /// リサイズ制御クラス
        /// </summary>
        public partial class Resize
        {
            public const int WM_SYSCOMMAND = 0x0112;
            public const int WM_SETCURSOR = 0x0020;
            public const int WM_NCHITTEST = 0x0084;
            public const int WM_NCLBUTTONDOWN = 0x00A1;
            public const int WM_MOUSEFIRST = 0x0200;

            public const int SC_MINIMIZE = 0xF020;
            public const int SC_MAXIMIZE = 0xF030;
            public const int SC_CLOSE = 0xF060;
            public const int SC_RESTORE = 0xF120;

            public const int HTCAPTION = 0x2;

            public const int HTLEFT = 0x0A;
            public const int HTRIGHT = 0x0B;
            public const int HTTOP = 0x0C;
            public const int HTTOPLEFT = 0x0D;
            public const int HTTOPRIGHT = 0x0E;
            public const int HTBOTTOM = 0x0F;
            public const int HTBOTTOMLEFT = 0x10;
            public const int HTBOTTOMRIGHT = 0x11;
            public const int HTBORDER = 0x12;

            public const int ODS_SELECTED = 0x0001;
            public const int ODS_DISABLED = 0x0004;
            public const int ODS_FOCUS = 0x0010;
            public const int ODS_DEFAULT = 0x0020;

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            private static extern bool ReleaseCapture();
            public static bool ReleaseCaptureAPI()
            {
                return ReleaseCapture();
            }

            [DllImport("user32.dll")]
            private static extern void SendMessage(IntPtr h, int m, int w, int l);
            public static void SendMessageAPI(IntPtr h, int m, int w, int l)
            {
                SendMessage(h, m, w, l);
            }
        }

        /// <summary>
        /// ウィンドウタイトル列挙クラス
        /// </summary>
        public partial class EnumWindow
        {
            // コールバックメソッドのデリゲート
            public delegate int EnumerateWindowsCallback(IntPtr hWnd, int lParam);

            // EnumWindows API関数の宣言
            [DllImport("user32.dll", EntryPoint = "EnumWindows")]
            public static extern int EnumWindows(EnumerateWindowsCallback lpEnumFunc, int lParam);

            // GetWindowText API関数の宣言
            [DllImport("user32.dll", EntryPoint = "GetWindowText", CharSet = CharSet.Auto)]
            private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

            // IsWindowVisible API関数の宣言
            [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
            private static extern int IsWindowVisible(IntPtr hWnd);

            // GetWindow API関数の宣言
            [DllImport("user32.dll", EntryPoint = "GetWindow")]
            private static extern IntPtr GetWindow(IntPtr hWnd, long wCmd);

            // 最前面ウィンドウ取得関数の宣言
            [DllImport("user32.dll")]
            private static extern IntPtr GetForegroundWindow();

            // GetWindowで使用する定数
            public const long GW_OWNER = 4;

            // リストに保持
            public static ArrayList windowList = new ArrayList();

            [StructLayout(LayoutKind.Sequential)]
            public struct WindowInfoStruct
            {
                public Boolean isChanged;
                public string windowTitle;
                public IntPtr wHnd;
                public int stickyId;
            }
            private static WindowInfoStruct wInfoBackup = new WindowInfoStruct();

            // リスト初期化
            public static void listClear()
            {
                windowList.Clear();
            }

            // ウィンドウを列挙するためのコールバックメソッド
            public static int EnumerateWindows(IntPtr hWnd, int lParam)
            {
                const int BUFFER_SIZE = 0x1000;
                StringBuilder sb = new StringBuilder(BUFFER_SIZE);

                System.Threading.Thread.Sleep(1);

                // ウィンドウが可視の場合
                if (IsWindowVisible(hWnd) != 0)
                {
                    // ウィンドウのキャプションを取得
                    if (GetWindowText(hWnd, sb, BUFFER_SIZE) != 0)
                    {
                        // トップレベルウィンドウか
                        if (GetWindow(hWnd, GW_OWNER) == IntPtr.Zero)
                        {
                            // シェルでないか
                            if (sb.ToString().Contains("Progman") == false && sb.ToString().Contains("Program Manager") == false)
                            {
                                WindowInfoStruct wInfo = new WindowInfoStruct();
                                wInfo.windowTitle = sb.ToString();
                                wInfo.wHnd = hWnd;
                                windowList.Add(wInfo);
                            }
                        }
                    }
                }
                // 列挙を継続するには0以外を返す必要がある
                return 1;
            }

            public static int GetForgroundWindowInfo()
            {
                const int BUFFER_SIZE = 0x1000;
                StringBuilder sb = new StringBuilder(BUFFER_SIZE);

                IntPtr hWnd = GetForegroundWindow();      // 最前面ウィンドウの hwnd を取得
                // ウィンドウが可視の場合
                if (IsWindowVisible(hWnd) != 0)
                {
                    // ウィンドウのキャプションを取得
                    if (GetWindowText(hWnd, sb, BUFFER_SIZE) != 0)
                    {
                        // トップレベルウィンドウか
                        if (GetWindow(hWnd, GW_OWNER) == IntPtr.Zero)
                        {
                            // シェルでないか
                            if (sb.ToString().Contains("Progman") == false && sb.ToString().Contains("Program Manager") == false)
                            {
                                WindowInfoStruct wInfo = new WindowInfoStruct();
                                wInfo.windowTitle = sb.ToString();
                                wInfo.wHnd = hWnd;
                                // 前回と今回で変更されたかどうか
                                if (wInfo.windowTitle.Equals(wInfoBackup.windowTitle) == false)
                                {
                                    wInfo.isChanged = true;
                                    wInfoBackup.windowTitle = wInfo.windowTitle;
                                }
                                else
                                {
                                    wInfo.isChanged = false;
                                }
                                windowList.Add(wInfo);
                            }
                        }
                    }
                }
                return 1;
            }


            // 最前面ウィンドウを取得するためのコールバックメソッド
            public static int EnumerateWindowsIsTop(IntPtr hWnd, int lParam)
            {
                const int BUFFER_SIZE = 0x1000;
                StringBuilder sb = new StringBuilder(BUFFER_SIZE);

                if (windowList.Count > 0)
                {
                    return 0;
                }

                System.Threading.Thread.Sleep(1);

                // ウィンドウが可視の場合
                if (IsWindowVisible(hWnd) != 0)
                {
                    // ウィンドウのキャプションを取得
                    if (GetWindowText(hWnd, sb, BUFFER_SIZE) != 0)
                    {
                        // トップレベルウィンドウか
                        if (GetWindow(hWnd, GW_OWNER) == IntPtr.Zero)
                        {
                            // シェルでないか
                            if (sb.ToString().Contains("Progman") == false && sb.ToString().Contains("Program Manager") == false)
                            {
                                WindowInfoStruct wInfo = new WindowInfoStruct();
                                wInfo.windowTitle = sb.ToString();
                                wInfo.wHnd = hWnd;
                                // 前回と今回で変更されたかどうか
                                if (wInfo.windowTitle.Equals(wInfoBackup.windowTitle) == false)
                                {
                                    wInfo.isChanged = true;
                                    wInfoBackup.windowTitle = wInfo.windowTitle;
                                }
                                else
                                {
                                    wInfo.isChanged = false;
                                }
                                windowList.Add(wInfo);
                            }
                        }
                    }
                }
                // 列挙を継続するには0以外を返す必要がある
                return 1;
            }
        }

        /// <summary>
        /// ホットキー制御クラス
        /// </summary>
        public class HotKey
        {
            [DllImport("user32.dll")]
            public extern static int RegisterHotKey(IntPtr HWnd, int ID, int MOD_KEY, int KEY);
            // 返り値:  成功 = 0以外,  失敗 = 0(既に他が登録済み)

            [DllImport("user32.dll")]
            public extern static int UnregisterHotKey(IntPtr HWnd, int ID);
            // 返り値:  成功 = 0以外,  失敗 = 0

            /// <summary>
            /// ホットキー押下通知
            /// </summary>
            public const int WM_HOTKEY = 0x0312;
            /// <summary>
            /// ALTキー判定値
            /// </summary>
            public const int MOD_ALT = 0x0001;
            /// <summary>
            /// CTRLキー判定値
            /// </summary>
            public const int MOD_CONTROL = 0x0002;
            /// <summary>
            /// SHIFTキー判定値
            /// </summary>
            public const int MOD_SHIFT = 0x0004;
            /// <summary>
            /// WINキー判定値
            /// </summary>
            public const int MOD_WIN = 0x0008;

            // 0x0000～0xbfff 内の適当な値でよい
            // 0xc000～0xffff は DLL 用なので 使用不可！
            public const int HOTKEY_ID_0000 = 0x0000;
            public const int HOTKEY_ID_0001 = 0x0001;
            public const int HOTKEY_ID_0002 = 0x0002;
            public const int HOTKEY_ID_0003 = 0x0003;
            public const int HOTKEY_ID_0004 = 0x0004;
            public const int HOTKEY_ID_0005 = 0x0005;
            public const int HOTKEY_ID_0006 = 0x0006;
            public const int HOTKEY_ID_0007 = 0x0007;
            public const int HOTKEY_ID_0008 = 0x0008;
            public const int HOTKEY_ID_0009 = 0x0009;

            /// <summary>
            /// ホットキーコードより送信用コントロールキー取得
            /// </summary>
            /// <param name="keyString"></param>
            /// <returns></returns>
            public static int GetControlKey(string keyString)
            {
                int ret = 0;

                string[] sep = { " + " };
                string[] keys = keyString.Trim().Split(sep, StringSplitOptions.None);

                foreach (string key in keys)
                {
                    ret |= getControlCode(key);
                }
                return ret;
            }

            private static Int32 getControlCode(string key)
            {
                if (key.Equals(System.Windows.Forms.Keys.Control.ToString()))
                {
                    return MOD_CONTROL;
                }
                else if (key.Equals(System.Windows.Forms.Keys.Shift.ToString()))
                {
                    return MOD_SHIFT;
                }
                else if (key.Equals(System.Windows.Forms.Keys.Alt.ToString()))
                {
                    return MOD_ALT;
                }
                return 0;
            }

            /// <summary>
            /// ホットキーコードより送信用ノーマルキー取得
            /// </summary>
            /// <param name="keyString"></param>
            /// <returns></returns>
            public static Int32 GetNormalKey(string keyString)
            {
                string[] sep = { " + " };
                string[] keys = keyString.Split(sep, StringSplitOptions.None);

                foreach (string key in keys)
                {
                    if (getControlCode(key) == 0 && !key.Equals(""))
                    {
                        return Int32.Parse(key);
                    }
                }
                return 0;
            }

            /// <summary>
            /// ホットキー再構築
            /// </summary>
            /// <param name="keyString"></param>
            /// <returns></returns>
            public static string BuildHotKey(string keyString)
            {
                string hotKey = "";

                KeysConverter kc = new KeysConverter();

                string[] sep = { " + " };
                string[] keys = keyString.Split(sep, StringSplitOptions.None);

                foreach (string key in keys)
                {
                    if (!key.Equals(""))
                    {
                        if (getControlCode(key.Trim()) != 0)
                        {
                            hotKey = hotKey + key + " + ";
                        }
                        else if (Int32.Parse(key) >= 0x30 && Int32.Parse(key) <= 0x39)
                        {
                            hotKey = hotKey + (char)Int32.Parse(key);
                        }
                        else
                        {
                            hotKey = hotKey + kc.ConvertFromString(key);
                        }
                    }
                }
                return hotKey;
            }
        }

        /// <summary>
        /// 電源制御クラス
        /// </summary>
        public class PowerManagement
        {
            /// <summary>
            /// スタンバイ時・復帰時、全てのウィンドウに送られる
            /// </summary>
            public const int WM_POWERBROADCAST = 0x0218;
            /// <summary>
            /// Windowsを終了しようとするとき、全てのウィンドウに送られる
            /// </summary>
            public const int WM_QUERYENDSESSION = 0x0011;

            // wParam
            /// <summary>
            /// バッテリ電力が低下した(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMBATTERLOW = 0x0009;
            /// <summary>
            /// OEM定義のイベントが発生した(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMOEMEVENT = 0x000B;
            /// <summary>
            /// パワー状態が変化した(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMPOWERSTATUSCHANGE = 0x000A;
            /// <summary>
            /// 待機要求をする(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMQUERYSUSPEND = 0x0000;
            /// <summary>
            /// 待機要求が拒否された(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMQUERYSUSPENDFAILED = 0x0002;
            /// <summary>
            /// システムが自動的に復帰しようとしている(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMRESUMEAUTOMATIC = 0x0012;
            /// <summary>
            /// 致命的な待機状態からシステムが復帰しようとしている(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMRESUMECRITICAL = 0x0006;
            /// <summary>
            /// 待機状態から復帰しようとしている(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMRESUMESUSPEND = 0x0007;
            /// <summary>
            /// システムが待機状態になろうとしている(WM_POWERBROADCAST)
            /// </summary>
            public const int PBT_APMSUSPEND = 0x0004;

            // lParam:常に0
        }

        /// <summary>
        /// ウィンドウ情報クラス
        /// </summary>
        public class WindowInfo
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int x;
                public int y;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct WINDOWPLACEMENT
            {
                public int Length;
                public int flags;
                public int showCmd;
                public POINT ptMinPosition;
                public POINT ptMaxPosition;
                public RECT rcNormalPosition;
            }

            [DllImport("user32.dll")]
            extern public static bool GetWindowPlacement(int hWnd, ref WINDOWPLACEMENT lpwndpl);

            /// <summary>
            /// 通常表示状態時のウィンドウロケーション情報をPointに包んで返却
            /// </summary>
            /// <param name="form"></param>
            /// <returns></returns>
            public static Point GetNormalWindowLocation(IntPtr hWnd)
            {
                WINDOWPLACEMENT wndpl = new WINDOWPLACEMENT();
                wndpl.Length = Marshal.SizeOf(wndpl);
                GetWindowPlacement(hWnd.ToInt32(), ref wndpl);
                return new Point(wndpl.rcNormalPosition.left, wndpl.rcNormalPosition.top);
            }

            public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
            public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

            const uint SWP_NOSIZE = 0x0001;
            const uint SWP_NOMOVE = 0x0002;
            const uint SWP_NOACTIVATE = 0x0010;
            const uint SWP_SHOWWINDOW = 0x0040;

            // (x, y), (cx, cy)を無視するようにする.
            public const uint TOPMOST_FLAGS = (SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE | SWP_SHOWWINDOW);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            extern public static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);
        }
    }
}
