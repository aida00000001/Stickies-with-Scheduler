using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SwS
{
    class CrossProcessMemory
    {
        const int LVM_GETITEM = 0x1005;
        const int LVM_SETITEM = 0x1006;
        const int LVIF_TEXT = 0x0001;
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
          ref LV_ITEM buffer, int dwSize, IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
          IntPtr lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32")]
        static extern bool CloseHandle(IntPtr hObject);

        [StructLayout(LayoutKind.Sequential)]
        public struct LV_ITEM
        {
            public uint mask;
            public int iItem;
            public int iSubItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
        }

        public static string ReadListViewItem(IntPtr hWnd, int item)
        {
            const int dwBufferSize = 1024;

            int dwProcessID;
            LV_ITEM lvItem;
            string retval;
            bool bSuccess;
            IntPtr hProcess = IntPtr.Zero;
            IntPtr lpRemoteBuffer = IntPtr.Zero;
            IntPtr lpLocalBuffer = IntPtr.Zero;
            IntPtr threadId = IntPtr.Zero;

            try
            {
                lvItem = new LV_ITEM();
                lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize);
                // Get the process id owning the window
                threadId = GetWindowThreadProcessId(hWnd, out dwProcessID);
                if ((threadId == IntPtr.Zero) || (dwProcessID == 0))
                    throw new ArgumentException("hWnd");

                // Open the process with all access
                hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, dwProcessID);
                if (hProcess == IntPtr.Zero)
                    throw new ApplicationException("Failed to access process");

                // Allocate a buffer in the remote process
                lpRemoteBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, MEM_COMMIT,
                  PAGE_READWRITE);
                if (lpRemoteBuffer == IntPtr.Zero)
                    throw new SystemException("Failed to allocate memory in remote process");

                // Fill in the LVITEM struct, this is in your own process
                // Set the pszText member to somewhere in the remote buffer,
                // For the example I used the address imediately following the LVITEM stuct
                lvItem.mask = LVIF_TEXT;
                lvItem.iItem = 0;
                lvItem.pszText = (IntPtr)(lpRemoteBuffer.ToInt32() + Marshal.SizeOf(typeof(LV_ITEM)));
                lvItem.cchTextMax = 50;

                // Copy the local LVITEM to the remote buffer
                bSuccess = WriteProcessMemory(hProcess, lpRemoteBuffer, ref lvItem,
                  Marshal.SizeOf(typeof(LV_ITEM)), IntPtr.Zero);
                if (!bSuccess)
                    throw new SystemException("Failed to write to process memory");

                // Send the message to the remote window with the address of the remote buffer
                SendMessage(hWnd, LVM_GETITEM, 0, lpRemoteBuffer);

                // Read the struct back from the remote process into local buffer
                bSuccess = ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize,
                  IntPtr.Zero);
                if (!bSuccess)
                    throw new SystemException("Failed to read from process memory");

                // At this point the lpLocalBuffer contains the returned LV_ITEM structure
                // the next line extracts the text from the buffer into a managed string
                retval = Marshal.PtrToStringAnsi((IntPtr)(lpLocalBuffer.ToInt32() +
                  Marshal.SizeOf(typeof(LV_ITEM))));
            }
            finally
            {
                if (lpLocalBuffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpLocalBuffer);
                if (lpRemoteBuffer != IntPtr.Zero)
                    VirtualFreeEx(hProcess, lpRemoteBuffer, 0, MEM_RELEASE);
                if (hProcess != IntPtr.Zero)
                    CloseHandle(hProcess);
            }
            return retval;
        }
    }
}
