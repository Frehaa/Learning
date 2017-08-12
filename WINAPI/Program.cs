using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Learning.WINAPI
{
    class Program
    {
        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        private static HookProc keyboardHookProcedure = LowLevelKeyboardHookCallback;
        private static HookProc mouseHookProcedure = LowLevelMouseHookCallback;

        private static IntPtr keyboardHookId;
        private static IntPtr mouseHookId;

        public enum HookType
        {
            WH_MIN = (-1),                          
            WH_MSGFILTER = (-1),                    //      that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. For more information, see the MessageProc hook procedure.
            WH_JOURNALRECORD = 0,                   //      that records input messages posted to the system message queue. This hook is useful for recording macros. For more information, see the JournalRecordProc hook procedure.
            WH_JOURNALPLAYBACK = 1,                 //      that posts messages previously recorded by a WH_JOURNALRECORD hook procedure. For more information, see the JournalPlaybackProc hook procedure.
            WH_KEYBOARD = 2,                        //      that monitors keystroke messages. For more information, see the KeyboardProc hook procedure.
            WH_GETMESSAGE = 3,                      //      that monitors messages posted to a message queue. For more information, see the GetMsgProc hook procedure.
            WH_CALLWNDPROC = 4,                     //      that monitors messages before the system sends them to the destination window procedure.For more information, see the CallWndProc hook procedure.
            WH_CBT = 5,                             //      that receives notifications useful to a CBT application.For more information, see the CBTProc hook procedure.
            WH_SYSMSGFILTER = 6,                    //      that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread. For more information, see the SysMsgProc hook procedure.
            WH_MOUSE = 7,                           //      that monitors mouse messages. For more information, see the MouseProc hook procedure.
            WH_HARDWARE = 8,                        //  ???
            WH_DEBUG = 9,                           //      useful for debugging other hook procedures. For more information, see the DebugProc hook procedure.
            WH_SHELL = 10,                          //      that receives notifications useful to shell applications. For more information, see the ShellProc hook procedure.
            WH_FOREGROUNDIDLE = 11,                 //      that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time. For more information, see the ForegroundIdleProc hook procedure.
            WH_CALLWNDPROCRET = 12,                 //      that monitors messages after they have been processed by the destination window procedure.For more information, see the CallWndRetProc hook procedure.
            WH_KEYBOARD_LL = 13,                    //      that monitors low-level keyboard input events. For more information, see the LowLevelKeyboardProc hook procedure.
            WH_MOUSE_LL = 14                        //      that monitors low-level mouse input events. For more information, see the LowLevelMouseProc hook procedure.
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardStruct
        {
            public uint vkCode;
            public uint scanCode;
            public KeyboardStructFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        public enum KeyboardStructFlags : uint
        {
            LLKHF_EXTENDED = 0x01,
            LLKHF_INJECTED = 0x10,
            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }

        [Flags]
        public enum KeyFlags
        {
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseStruct
        {
            public POINT point;
            public uint mouseData;
            public uint flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        public enum MouseFlags
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_MOUSEHWHEEL = 0x020E,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_XBUTTONDOWN = 0x020B,
            WM_XBUTTONUP = 0x020C,
            WM_XBUTTONDBLCLK = 0x020D,
            WM_NCXBUTTONDOWN = 0x00AB,
            WM_NCXBUTTONUP = 0x00AC,
            WM_NCXBUTTONDBLCLK = 0x00AD,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
        }


        static void Main(string[] args)
        {
            using (Process process = Process.GetCurrentProcess())
            using (ProcessModule module = process.MainModule)
            {
                IntPtr hModule = GetModuleHandle(module.ModuleName);

                keyboardHookId = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, keyboardHookProcedure, hModule, 0);
                mouseHookId = SetWindowsHookEx(HookType.WH_MOUSE_LL, mouseHookProcedure, hModule, 0);

                Application.Run(new Form());

                if (UnhookWindowsHookEx(keyboardHookId))
                {
                    Debug.WriteLine("Failed to unhook");
                    Debug.WriteLine("Error code: " + Marshal.GetLastWin32Error());
                }

                if (UnhookWindowsHookEx(mouseHookId))
                {
                    Debug.WriteLine("Failed to unhook");
                    Debug.WriteLine("Error code: " + Marshal.GetLastWin32Error());
                }

            }

        }
        
        private static int LowLevelKeyboardHookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }

            KeyFlags keyboardMessageIdentifier = (KeyFlags)wParam.ToInt32();
            
            switch (keyboardMessageIdentifier)
            {
                case KeyFlags.WM_KEYDOWN:
                    Debug.WriteLine("WM_KEYDOWN");
                    break;
                case KeyFlags.WM_KEYUP:
                    Debug.WriteLine("WM_KEYUP");
                    break;
                case KeyFlags.WM_SYSKEYDOWN:
                    Debug.WriteLine("WM_SYSKEYDOWN");
                    break;
                case KeyFlags.WM_SYSKEYUP:
                    Debug.WriteLine("WM_SYSKEYUP");
                    break;
                default:
                    Debug.Fail("Unknown flag: " + keyboardMessageIdentifier);
                    break;
            }
            
            KeyboardStruct kbs = Marshal.PtrToStructure<KeyboardStruct>(lParam);
            
            Keys key = (Keys)kbs.vkCode;
            Debug.WriteLine(key);
            Debug.WriteLine(kbs.scanCode);
            Debug.WriteLine(kbs.flags);
            Debug.WriteLine(kbs.time);
            Debug.WriteLine(kbs.dwExtraInfo.ToUInt64());

            Debug.WriteLine("");

            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private static int LowLevelMouseHookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }

            MouseFlags flag = (MouseFlags)wParam;
            switch (flag)
            {
                case MouseFlags.WM_LBUTTONDOWN:
                    Debug.WriteLine(MouseFlags.WM_LBUTTONDOWN);
                    break;
                case MouseFlags.WM_LBUTTONUP:
                    Debug.WriteLine(MouseFlags.WM_LBUTTONUP);
                    break;
                case MouseFlags.WM_MOUSEMOVE:
                    Debug.WriteLine(MouseFlags.WM_MOUSEMOVE);
                    break;
                case MouseFlags.WM_MOUSEWHEEL:
                    Debug.WriteLine(MouseFlags.WM_MOUSEWHEEL);
                    break;
                case MouseFlags.WM_MOUSEHWHEEL:
                    Debug.WriteLine(MouseFlags.WM_MOUSEHWHEEL);
                    break;
                case MouseFlags.WM_RBUTTONDOWN:
                    Debug.WriteLine(MouseFlags.WM_RBUTTONDOWN);
                    break;
                case MouseFlags.WM_RBUTTONUP:
                    Debug.WriteLine(MouseFlags.WM_RBUTTONUP);
                    break;
                case MouseFlags.WM_XBUTTONDOWN:
                    Debug.WriteLine(MouseFlags.WM_XBUTTONDOWN);
                    break;
                case MouseFlags.WM_XBUTTONUP:
                    Debug.WriteLine(MouseFlags.WM_XBUTTONUP);
                    break;
                case MouseFlags.WM_XBUTTONDBLCLK:
                    Debug.WriteLine(MouseFlags.WM_XBUTTONDBLCLK);
                    break;
                case MouseFlags.WM_NCXBUTTONDOWN:
                    Debug.WriteLine(MouseFlags.WM_NCXBUTTONDOWN);
                    break;
                case MouseFlags.WM_NCXBUTTONUP:
                    Debug.WriteLine(MouseFlags.WM_NCXBUTTONUP);
                    break;
                case MouseFlags.WM_NCXBUTTONDBLCLK:
                    Debug.WriteLine(MouseFlags.WM_NCXBUTTONDBLCLK);
                    break;
                case MouseFlags.WM_MBUTTONDOWN:
                    Debug.WriteLine(MouseFlags.WM_NCXBUTTONDBLCLK);
                    break;
                case MouseFlags.WM_MBUTTONUP:
                    Debug.WriteLine(MouseFlags.WM_MBUTTONUP);
                    break;
                default:
                    Debug.WriteLine("Unknown Flag: {0:X}", flag);
                    break;
            }

            MouseStruct mouseStruct = Marshal.PtrToStructure<MouseStruct>(lParam);
            Debug.WriteLine(mouseStruct.point.x + ", " + mouseStruct.point.y);
            Debug.WriteLine(mouseStruct.mouseData);
            Debug.WriteLine(mouseStruct.flags);            
            //Debug.WriteLine(mouseStruct.time);
            if (mouseStruct.dwExtraInfo.ToUInt32() != 0)
                Debug.WriteLine(mouseStruct.dwExtraInfo);
            Debug.WriteLine("");

            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc hookProc, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int CallNextHookEx(IntPtr hhk, int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint GetCurrentThreadId();
                
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetLastError();
    }
}
