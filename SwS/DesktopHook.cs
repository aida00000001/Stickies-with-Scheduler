using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;

namespace SwS
{
    /// <summary>
    /// ZMemoソースから移植するDesktopHook機能の実現
    /// </summary>
    public static class DesktopHook
    {
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, ref ListViewAccessor.LV_ITEM lParam);

        /// <summary>
        /// C言語のポインタNULLチェック対処用
        /// </summary>
        private static IntPtr NULL = IntPtr.Zero;

        /// <summary>
        /// デスクトップリストビュー取得クラス
        /// </summary>
        public static class GetFindDesktopListViewWnd
        {
            private static IntPtr _hDefViewWnd = NULL;
            private static IntPtr _hProgmanWnd = NULL;
            private static IntPtr hDesktopListView = NULL;

            /// <summary>
            /// デスクトップリストビューアドレス返却
            /// </summary>
            /// <returns></returns>
            public static IntPtr getDesktopListViewAddress()
            {
                if (hDesktopListView == NULL)
                {
                    FindDesktopListViewWnd();
                }
                return hDesktopListView;
            }

            /// <summary>
            /// デスクトップリストビュー取得
            /// </summary>
            public static void FindDesktopListViewWnd()
            {
                if (_hDefViewWnd != NULL)
                    _hDefViewWnd = NULL;
                if (_hProgmanWnd != NULL)
                    _hProgmanWnd = NULL;
                IntPtr hProgramManWnd = FindWindowEx(NULL, NULL, "Progman", "Program Manager");
                if (hProgramManWnd != NULL)
                {
                    IntPtr hDefViewWnd = FindWindowEx(hProgramManWnd, NULL, "SHELLDLL_DefView", null);
                    if (hDefViewWnd != NULL)
                    {
                        hDesktopListView = FindWindowEx(hDefViewWnd, NULL, "SysListView32", null);
                        if (hDesktopListView != NULL)
                        {
                            if (_hDefViewWnd != NULL)
                                _hDefViewWnd = hDefViewWnd;
                            if (_hProgmanWnd != NULL)
                                _hProgmanWnd = hProgramManWnd;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 自身以外の共有リソースアクセス用クラス
        /// </summary>
        public static class SharedMemory
        {
            const int LVM_FIRST = 0x1000;                   // ListView messages
            const int LVM_GETITEMCOUNT = (LVM_FIRST + 4);
            const int LVM_GETITEM = (LVM_FIRST + 5);
            const int LVM_GETITEMRECT = (LVM_FIRST + 14);
            const int LVM_HITTEST = (LVM_FIRST + 18);
            const int LVM_GETITEMTEXTA = (LVM_FIRST + 45);
            const int LVM_GETITEMTEXTW = (LVM_FIRST + 115);
            const int LVM_GETITEMTEXT = LVM_GETITEMTEXTA;

            const int LVIF_TEXT = 0x00000001;
            const int LVIR_ICON = 1;
            const int LVIR_LABEL = 2;

            const int LVHT_ONITEMICON = 0x00000002;
            const int LVHT_ONITEMLABEL = 0x00000004;
            const int LVHT_ONITEMSTATEICON = 0x00000008;
            const int LVHT_ONITEM = (LVHT_ONITEMICON | LVHT_ONITEMLABEL | LVHT_ONITEMSTATEICON);

            const int LVM_SETITEM = 0x1006;
            const uint PROCESS_ALL_ACCESS = (uint)(0x000F0000L | 0x00100000L | 0xFFF);
            const uint MEM_COMMIT = 0x1000;
            const uint MEM_RELEASE = 0x8000;
            const uint PAGE_READWRITE = 0x04;

            [DllImport("user32.dll")]
            static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, IntPtr lParam);

            [DllImport("user32")]
            static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out int lpwdProcessID);

            [DllImport("kernel32")]
            static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle,
              int dwProcessId);

            [DllImport("kernel32")]
            static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
              int dwSize, uint flAllocationType, uint flProtect);

            [DllImport("kernel32")]
            static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize,
              uint dwFreeType);

            [DllImport("kernel32")]
            static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
              ref ListViewAccessor.LV_ITEM buffer, int dwSize, IntPtr lpNumberOfBytesWritten);

            [DllImport("kernel32")]
            static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
              ref ListViewAccessor.RECT buffer, int dwSize, IntPtr lpNumberOfBytesWritten);

            [DllImport("kernel32")]
            static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
              ref ListViewAccessor.LVHITTESTINFO buffer, int dwSize, IntPtr lpNumberOfBytesWritten);

            [DllImport("kernel32")]
            static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
              ref char[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

            [DllImport("kernel32")]
            static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
              ref ListViewAccessor.RECT lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

            [DllImport("kernel32")]
            static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
              IntPtr lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

            [DllImport("kernel32")]
            static extern bool CloseHandle(IntPtr hObject);

            private static int dwBufferSize = 1024;
            private static IntPtr hWnd = IntPtr.Zero;
            private static IntPtr lpLocalBuffer = IntPtr.Zero;
            private static IntPtr lpRemoteBuffer = IntPtr.Zero;
            private static IntPtr hProcess = IntPtr.Zero;

            /// <summary>
            /// デスクトップ共有アクセスを用意
            /// </summary>
            public static void GetSharedMemory(IntPtr hWnd)
            {
                int dwProcessID;
                IntPtr threadId = IntPtr.Zero;
                try
                {
                    // 確保済みなら開放
                    Parge();

                    // Get the process id owning the window
                    threadId = GetWindowThreadProcessId(hWnd, out dwProcessID);
                    if ((threadId == IntPtr.Zero) || (dwProcessID == 0))
                        throw new ArgumentException("hWnd");

                    // Open the process with all access
                    hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, dwProcessID);
                    if (hProcess == IntPtr.Zero)
                        throw new ApplicationException("Failed to access process");

                    // Allocate a buffer in the remote process
                    lpRemoteBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, MEM_COMMIT, PAGE_READWRITE);

                    /*
                    {
                        try
                        {
                            ListViewAccessor.ClearIconInfoList();
                            lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize);
                            int IconCount = ListViewAccessor.ListView_GetItemCount(hWnd);
                            for (int IconNo = 0; IconNo < IconCount; IconNo++)
                            {
                                IntPtr ipIconNo = (IntPtr)IconNo;

                                // Fill in the LVITEM struct, this is in your own process
                                // Set the pszText member to somewhere in the remote buffer,
                                // For the example I used the address imediately following the LVITEM stuct
                                lvItem.mask = LVIF_TEXT;
                                lvItem.iItem = 0;
                                lvItem.pszText = (IntPtr)(lpRemoteBuffer.ToInt32() + Marshal.SizeOf(typeof(ListViewAccessor.LV_ITEM)));
                                lvItem.cchTextMax = 50;

                                // Copy the local LVITEM to the remote buffer
                                bSuccess = WriteProcessMemory(hProcess, lpRemoteBuffer, ref lvItem, Marshal.SizeOf(typeof(ListViewAccessor.LV_ITEM)), IntPtr.Zero);
                                if (!bSuccess)
                                    throw new SystemException("Failed to write to process memory");

                                // Send the message to the remote window with the address of the remote buffer
                                SendMessage(hWnd, LVM_GETITEMTEXT, IconNo, lpRemoteBuffer);

                                // Read the struct back from the remote process into local buffer
                                bSuccess = ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize,
                                  IntPtr.Zero);
                                if (!bSuccess)
                                    throw new SystemException("Failed to read from process memory");

                                // At this point the lpLocalBuffer contains the returned LV_ITEM structure
                                // the next line extracts the text from the buffer into a managed string
                                string iconText = Marshal.PtrToStringAnsi((IntPtr)(lpLocalBuffer.ToInt32() + Marshal.SizeOf(typeof(ListViewAccessor.LV_ITEM))));

                                if (lpRemoteBuffer == IntPtr.Zero)
                                    throw new SystemException("Failed to allocate memory in remote process");

                                // アイコン矩形取得＆チェック
                                ListViewAccessor.RECT LocalRect = new ListViewAccessor.RECT();
                                LocalRect.left = LVIR_ICON;
                                SharedMemory.ProcessWrite(lpRemoteBuffer, ref LocalRect, Marshal.SizeOf(typeof(ListViewAccessor.RECT)));
                                SendMessage(hWnd, LVM_GETITEMRECT, IconNo, lpRemoteBuffer);
                                Marshal.StructureToPtr(LocalRect, lpLocalBuffer, true);
                                SharedMemory.ProcessRead(lpRemoteBuffer, lpLocalBuffer, Marshal.SizeOf(typeof(ListViewAccessor.RECT)));
                                LocalRect = (ListViewAccessor.RECT)Marshal.PtrToStructure(lpLocalBuffer, typeof(ListViewAccessor.RECT));
                                ListViewAccessor.setIconList(LocalRect, iconText);
                                // テキスト矩形取得＆チェック
                                LocalRect.left = LVIR_LABEL;
                                SharedMemory.ProcessWrite(lpRemoteBuffer, ref LocalRect, Marshal.SizeOf(typeof(ListViewAccessor.RECT)));
                                SendMessage(hWnd, LVM_GETITEMRECT, IconNo, lpRemoteBuffer);
                                Marshal.StructureToPtr(LocalRect, lpLocalBuffer, true);
                                SharedMemory.ProcessRead(lpRemoteBuffer, lpLocalBuffer, Marshal.SizeOf(typeof(ListViewAccessor.RECT)));
                                LocalRect = (ListViewAccessor.RECT)Marshal.PtrToStructure(lpLocalBuffer, typeof(ListViewAccessor.RECT));
                                ListViewAccessor.setIconList(LocalRect, iconText);
                            }
                        }
                        finally
                        {
                            if (lpLocalBuffer != IntPtr.Zero)
                                Marshal.FreeHGlobal(lpLocalBuffer);
                        }
                    }
                    */
                }
                finally
                {
                }
            }

            /// <summary>
            /// 確保領域開放
            /// </summary>
            public static void Parge()
            {
                if (lpRemoteBuffer != IntPtr.Zero)
                    VirtualFreeEx(hProcess, lpRemoteBuffer, 0, MEM_RELEASE);
                if (hProcess != IntPtr.Zero)
                    CloseHandle(hProcess);
            }

            public static void ProcessWrite(IntPtr Dst, ref ListViewAccessor.LV_ITEM Src, int Len)
            {
                IntPtr WriteBytes = NULL;
                WriteProcessMemory(hProcess, Dst, ref Src, Len, WriteBytes);
            }

            public static void ProcessWrite(IntPtr Dst, ref ListViewAccessor.RECT Src, int Len)
            {
                IntPtr WriteBytes = NULL;
                WriteProcessMemory(hProcess, Dst, ref Src, Len, WriteBytes);
            }

            public static void ProcessWrite(IntPtr Dst, ref ListViewAccessor.LVHITTESTINFO Src, int Len)
            {
                IntPtr WriteBytes = NULL;
                WriteProcessMemory(hProcess, Dst, ref Src, Len, WriteBytes);
            }

            public static void ProcessRead(IntPtr Dst, ref char[] Src, int Len)
            {
                IntPtr ReadBytes = NULL;
                ReadProcessMemory(hProcess, Dst, ref Src, Len, ReadBytes);
            }

            public static void ProcessRead(IntPtr Dst, ref ListViewAccessor.RECT Src, int Len)
            {
                IntPtr ReadBytes = NULL;
                ReadProcessMemory(hProcess, Dst, ref Src, Len, ReadBytes);
            }

            public static void ProcessRead(IntPtr Dst, IntPtr Src, int Len)
            {
                IntPtr ReadBytes = NULL;
                ReadProcessMemory(hProcess, Dst, Src, Len, ReadBytes);
            }

            public static IntPtr getRemoteBufferAddress() {
                return lpRemoteBuffer;
            }
        }

        /// <summary>
        /// リストビューアクセサ
        /// </summary>
        public static class ListViewAccessor
        {
            [DllImport("User32.Dll")]
            static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool SetForegroundWindow(IntPtr hWnd);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", SetLastError = true)]
            extern static bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            const int LVM_FIRST = 0x1000;                   // ListView messages
            const int LVM_GETITEMCOUNT = (LVM_FIRST + 4);
            const int LVM_GETITEM = (LVM_FIRST + 5);
            const int LVM_GETITEMRECT = (LVM_FIRST + 14);
            const int LVM_HITTEST = (LVM_FIRST + 18);
            const int LVM_GETITEMTEXTA = (LVM_FIRST + 45);
            const int LVM_GETITEMTEXTW = (LVM_FIRST + 115);
            const int LVM_GETITEMTEXT = LVM_GETITEMTEXTA;

            const int LVIF_TEXT = 0x00000001;
            const int LVIR_ICON = 1;
            const int LVIR_LABEL = 2;

            const int LVHT_ONITEMICON = 0x00000002;
            const int LVHT_ONITEMLABEL = 0x00000004;
            const int LVHT_ONITEMSTATEICON = 0x00000008;
            const int LVHT_ONITEM = (LVHT_ONITEMICON | LVHT_ONITEMLABEL | LVHT_ONITEMSTATEICON);

            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_LBUTTONUP = 0x0202;
            const int WM_LBUTTONDBLCLK = 0x0203;
            const int WM_RBUTTONDOWN = 0x0204;
            const int WM_RBUTTONUP = 0x0205;
            const int MK_LBUTTON = 0x0001;
            const int MK_RBUTTON = 0x0002;

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct LV_ITEM
            {
                public Int32 mask;
                public int iItem;
                public int iSubItem;
                public Int32 state;
                public Int32 stateMask;
                public IntPtr pszText;
                public int cchTextMax;
                public int iImage;
                public Int32 lParam;
                public int iIndent;
                public int iGroupId;
                public Int32 cColumns;      // tile view columns
                public IntPtr puColumns;
                public IntPtr piColFmt;
                public int iGroup;          // readonly. only valid for owner data.
            }

            public struct LVHITTESTINFO
            {
                public ListViewAccessor.POINT pt;
                public Int32 flags;
                public int iItem;
                public int iSubItem;        // this is was NOT in win95.  valid only for LVM_SUBITEMHITTEST
                public int iGroup;          // readonly. index of group. only valid for owner data.
            }

            public struct POINT
            {
                public Int32 x;
                public Int32 y;
            }

            /// <summary>
            /// アイコン(座標＋テキスト)リスト
            /// </summary>
            private static IconInfoList iconInfoList = new IconInfoList();

            private class IconInfoList : CollectionBase
            {
                public int Add(iconInfo d)
                {
                    return (List.Add(d));
                }

                public int IndexOf(iconInfo d)
                {
                    return (List.IndexOf(d));
                }

                public void Insert(int index, iconInfo d)
                {
                    List.Insert(index, d);
                }

                public void Remove(iconInfo d)
                {
                    List.Remove(d);
                }

                public bool Contains(iconInfo d)
                {
                    return (List.Contains(d));
                }

                public iconInfo this[int index]
                {
                    get
                    {
                        return ((iconInfo)List[index]);
                    }
                    set
                    {
                        List[index] = value;
                    }
                }

                /// <summary>
                /// 発見したらそのアイコン情報を返却する
                /// </summary>
                /// <param name="pos"></param>
                /// <returns></returns>
                public iconInfo HitTestIconInfo(Point pos)
                {
                    iconInfo select = new iconInfo();
                    lock (this)
                    {
                        foreach (iconInfo ii in this)
                        {
                            if (ii.HitTest(pos) == true)
                            {
                                select = ii;
                                break;
                            }
                        }
                    }
                    return select;
                }

                /// <summary>
                /// 発見したらそのアイコンテキストを返却する
                /// </summary>
                /// <param name="pos"></param>
                /// <returns></returns>
                public string HitTestIconText(Point pos)
                {
                    string select = "";
                    lock (this)
                    {
                        foreach (iconInfo ii in this)
                        {
                            if (ii.HitTest(pos) == true)
                            {
                                select = ii.text;
                                break;
                            }
                        }
                    }
                    return select;
                }
            }

            /// <summary>
            /// アイコン情報(アイコン領域とテキスト領域は別に保持されるが、同じテキスト内容で保持される)
            /// </summary>
            private class iconInfo
            {
                /// <summary>
                /// アイコン矩形領域
                /// </summary>
                public RECT rect;
                /// <summary>
                /// アイコンテキスト
                /// </summary>
                public string text;

                public bool HitTest(Point pos)
                {
                    if (pos.X >= rect.left && pos.X <= rect.right && pos.Y >= rect.top && pos.Y <= rect.bottom)
                    {
                        return true;
                    }
                    return false;
                }
            }

            /// <summary>
            /// リストビューの数を返却
            /// </summary>
            /// <param name="hwnd"></param>
            /// <returns></returns>
            public static int ListView_GetItemCount(IntPtr hwnd)
            {
                return (int)SendMessage(hwnd, LVM_GETITEMCOUNT, NULL, NULL);
            }

            /// <summary>
            /// ヒットテスト
            /// </summary>
            /// <param name="hwndLV"></param>
            /// <param name="pinfo"></param>
            /// <returns></returns>
            public static int ListView_HitTest(IntPtr hwndLV, IntPtr pinfo)
            {
                return (int)SendMessage(hwndLV, LVM_HITTEST, NULL, pinfo);
            }

            /// <summary>
            /// アイコン一覧の取得
            /// </summary>
            public static void ReadIconList()
            {
                int dwBufferSize = 1024;
                IntPtr lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize);

                lock (iconInfoList)
                {
                    iconInfoList.Clear();
                    IntPtr hDesktopListView = GetFindDesktopListViewWnd.getDesktopListViewAddress();
                    IntPtr baseAddress = SharedMemory.getRemoteBufferAddress();
                    int IconCount = ListView_GetItemCount(hDesktopListView);

                    try
                    {
                        for (int IconNo = 0; IconNo < IconCount; IconNo++)
                        {
                            IntPtr ipIconNo = (IntPtr)IconNo;
                            // テキスト取得
                            LV_ITEM LocalItem = new LV_ITEM();
                            LocalItem.mask = LVIF_TEXT;
                            LocalItem.iItem = 0;
                            LocalItem.pszText = (IntPtr)(baseAddress.ToInt32() + Marshal.SizeOf(typeof(LV_ITEM)));
                            LocalItem.cchTextMax = 256;
                            SharedMemory.ProcessWrite(baseAddress, ref LocalItem, Marshal.SizeOf(typeof(LV_ITEM)));
                            int Len = SendMessage(hDesktopListView, LVM_GETITEMTEXT, ipIconNo, baseAddress);
                            // char[] LocalText = new char[256];
                            // System.Runtime.InteropServices.Marshal.StructureToPtr(LocalText, lpLocalBuffer, true);
                            SharedMemory.ProcessRead(baseAddress, lpLocalBuffer, dwBufferSize);
                            string iconText = Marshal.PtrToStringAnsi((IntPtr)(lpLocalBuffer.ToInt32() + Marshal.SizeOf(typeof(LV_ITEM))));

                            // アイコン矩形取得＆チェック
                            RECT LocalRect = new RECT();
                            LocalRect.left = LVIR_ICON;
                            SharedMemory.ProcessWrite(baseAddress, ref LocalRect, Marshal.SizeOf(typeof(RECT)));
                            SendMessage(hDesktopListView, LVM_GETITEMRECT, (IntPtr)IconNo, baseAddress);
                            Marshal.StructureToPtr(LocalRect, lpLocalBuffer, true);
                            SharedMemory.ProcessRead(baseAddress, lpLocalBuffer, Marshal.SizeOf(typeof(RECT)));
                            LocalRect = (RECT)Marshal.PtrToStructure(lpLocalBuffer, typeof(RECT));
                            setIconList(LocalRect, iconText);
                            // テキスト矩形取得＆チェック
                            LocalRect.left = LVIR_LABEL;
                            SharedMemory.ProcessWrite(baseAddress, ref LocalRect, Marshal.SizeOf(typeof(RECT)));
                            SendMessage(hDesktopListView, LVM_GETITEMRECT, (IntPtr)IconNo, baseAddress);
                            Marshal.StructureToPtr(LocalRect, lpLocalBuffer, true);
                            SharedMemory.ProcessRead(baseAddress, lpLocalBuffer, Marshal.SizeOf(typeof(RECT)));
                            LocalRect = (RECT)Marshal.PtrToStructure(lpLocalBuffer, typeof(RECT));
                            setIconList(LocalRect, iconText);
                        }
                    }
                    finally
                    {
                        if (lpLocalBuffer != IntPtr.Zero)
                            Marshal.FreeHGlobal(lpLocalBuffer);
                    }
                }
            }

            /// <summary>
            /// アイコン情報作成及びリストへ保管
            /// </summary>
            /// <param name="rect"></param>
            /// <param name="text"></param>
            private static void setIconList(RECT rect, string text)
            {
                iconInfo iInfo = new iconInfo();

                iInfo.rect = rect;
                iInfo.text = text;
                iconInfoList.Add(iInfo);
            }

            /// <summary>
            /// アイコンリストクリア
            /// </summary>
            public static void ClearIconInfoList()
            {
                iconInfoList.Clear();
            }

            /// <summary>
            /// 付箋上のマウス位置にアイコンがあるかどうかを確認
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            /// <returns></returns>
            public static bool HitTest(Point cursorPos)
            {
                return !iconInfoList.HitTestIconText(cursorPos).Equals("");
            }

            /// <summary>
            /// 当該アイコンのテキスト取得
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            /// <returns></returns>
            public static string GetIconText(Point cursorPos)
            {
                return iconInfoList.HitTestIconText(cursorPos);
            }

            /// <summary>
            /// マウス左ボタンダブルクリック送信
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            public static void PostMessageMouseLDblClick(Point cursorPos)
            {
                PostMessageToDesktop(WM_LBUTTONDBLCLK, MK_LBUTTON, cursorPos);
            }

            /// <summary>
            /// マウス左ボタン押下送信
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            public static void PostMessageMouseLDown(Point cursorPos)
            {
                PostMessageToDesktop(WM_LBUTTONDOWN, MK_LBUTTON, cursorPos);
            }

            /// <summary>
            /// マウス左ボタン開放送信
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            public static void PostMessageMouseLUp(Point cursorPos)
            {
                PostMessageToDesktop(WM_LBUTTONUP, MK_LBUTTON, cursorPos);
            }

            /// <summary>
            /// マウス左ボタン押下送信
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            public static void PostMessageMouseRDown(Point cursorPos)
            {
                PostMessageToDesktop(WM_RBUTTONDOWN, MK_RBUTTON, cursorPos);
            }

            /// <summary>
            /// マウス左ボタン開放送信
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            public static void PostMessageMouseRUp(Point cursorPos)
            {
                PostMessageToDesktop(WM_RBUTTONUP, MK_RBUTTON, cursorPos);
            }

            /// <summary>
            /// DesktopへPostMessage
            /// </summary>
            /// <param name="postMessage"></param>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            private static void PostMessageToDesktop(uint postMessage, int wParam, Point cursorPos)
            {
                IntPtr hDesktopListView = DesktopHook.GetFindDesktopListViewWnd.getDesktopListViewAddress();

                PostMessage(new HandleRef(null, hDesktopListView), postMessage, (IntPtr)wParam, makeLParam((int)cursorPos.X, (int)cursorPos.Y));
                if (postMessage != WM_LBUTTONUP)
                {
                    SetForegroundWindow(DesktopHook.GetFindDesktopListViewWnd.getDesktopListViewAddress());
                }
            }

            /// <summary>
            /// PostMessage用LPARAM作成
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            /// <returns></returns>
            private static IntPtr makeLParam(int X, int Y)
            {
                return ((IntPtr)(((UInt16)(((UInt64)(X)) & 0xffff)) | ((UInt16)((UInt16)(((UInt64)(Y)) & 0xffff))) << 16));
            }
        }

    }
}
