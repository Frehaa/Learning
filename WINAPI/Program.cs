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

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;


        static void Main(string[] args)
        {
            using (Process process = Process.GetCurrentProcess())
            using (ProcessModule module = process.MainModule)
            {
                IntPtr hModule = GetModuleHandle(module.ModuleName);

                hookId = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, hookProc, hModule, 0);
                Application.Run(new Form());

                if (UnhookWindowsHookEx(hookId))
                {
                    Debug.WriteLine("Failed to unhook");
                    Debug.WriteLine("Error code: " + Marshal.GetLastWin32Error());
                }
                
            }

        }

        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        private static HookProc hookProc = MyCallBack;
        private static IntPtr hookId;

        private static int MyCallBack(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }

            int keyboardMessageIdentifier = wParam.ToInt32();

            switch (keyboardMessageIdentifier)
            {
                case WM_KEYDOWN:
                    Debug.WriteLine("WM_KEYDOWN");
                    break;
                case WM_KEYUP:
                    Debug.WriteLine("WM_KEYUP");
                    break;
                case WM_SYSKEYDOWN:
                    Debug.WriteLine("WM_SYSKEYDOWN");
                    break;
                case WM_SYSKEYUP:
                    Debug.WriteLine("WM_SYSKEYUP");
                    break;
                default:
                    Debug.Fail("ERROR: UNKNOWN KEY");
                    break;
            }           
            
            KeyboardStruct kbs = (KeyboardStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardStruct));

            Keys key = (Keys)kbs.vkCode;
            Debug.WriteLine(key);
            Debug.WriteLine(kbs.scanCode);
            Debug.WriteLine(kbs.flags);
            Debug.WriteLine(kbs.time);
            Debug.WriteLine(kbs.dwExtraInfo.ToUInt64());

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
