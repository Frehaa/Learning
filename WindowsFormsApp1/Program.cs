using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}
    }

    class Intercept
    {                                               // Installs a hook procedure:
        private const int WH_KEYBOARD = 2;          //      that monitors keystroke messages. For more information, see the KeyboardProc hook procedure.
        private const int WH_KEYBOARD_LL = 13;      //      that monitors low-level keyboard input events. For more information, see the LowLevelKeyboardProc hook procedure.
        private const int WH_MOUSE = 7;             //      that monitors mouse messages. For more information, see the MouseProc hook procedure.
        private const int WH_MOUSE_LL = 14;         //      that monitors low-level mouse input events. For more information, see the LowLevelMouseProc hook procedure.
        private const int WH_CALLWNDPROC = 4;       //      that monitors messages before the system sends them to the destination window procedure.For more information, see the CallWndProc hook procedure.
        private const int WH_CALLWNDPROCRET = 12;   //      that monitors messages after they have been processed by the destination window procedure.For more information, see the CallWndRetProc hook procedure.

        private const int WH_CBT = 5;               //      that receives notifications useful to a CBT application.For more information, see the CBTProc hook procedure.
        private const int WH_DEBUG = 9;             //      useful for debugging other hook procedures. For more information, see the DebugProc hook procedure.
        private const int WH_FOREGROUNDIDLE = 11;   //      that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time. For more information, see the ForegroundIdleProc hook procedure.
        private const int WH_GETMESSAGE = 3;        //      that monitors messages posted to a message queue. For more information, see the GetMsgProc hook procedure.
        private const int WH_JOURNALPLAYBACK = 1;   //      that posts messages previously recorded by a WH_JOURNALRECORD hook procedure. For more information, see the JournalPlaybackProc hook procedure.
        private const int WH_JOURNALRECORD = 0;     //      that records input messages posted to the system message queue. This hook is useful for recording macros. For more information, see the JournalRecordProc hook procedure.

        private const int WH_MSGFILTER = -1;        //      that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. For more information, see the MessageProc hook procedure.
        private const int WH_SHELL = 10;            //      that receives notifications useful to shell applications. For more information, see the ShellProc hook procedure.
        private const int WH_SYSMSGFILTER = 6;      //      that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread. For more information, see the SysMsgProc hook procedure.

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP= 0x0100;

        public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
        private static HookProc hookProcedure;

        [STAThread]
        public static void Main()
        {
            hookProcedure = MyCallbackFunction;

            IntPtr hookId = SetWindowsHookEx(WH_KEYBOARD, hookProcedure, IntPtr.Zero, GetCurrentThreadId());

            Application.Run(new Form1());

            UnhookWindowsHookEx(hookId);
        }

        private static IntPtr MyCallbackFunction(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                //you need to call CallNextHookEx without further processing
                //and return the value returned by CallNextHookEx
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }
            // we can convert the 2nd parameter (the key code) to a System.Windows.Forms.Keys enum constant
            Keys keyPressed = (Keys)wParam.ToInt32();
            //Debug.WriteLine(keyPressed);

            int param = lParam.ToInt32();
            int keyRepeatCount = param & 0x0000FFFF;
            Console.WriteLine(keyRepeatCount);
            //return the value returned by CallNextHookEx
            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        //LRESULT CALLBACK KeyboardProc(
        //  _In_ int code,
        //  _In_ WPARAM wParam,
        //  _In_ LPARAM lParam
        //);

        //private void KeyboardProcedure(int code)

        //private static IntPtr SetHook(LowLevelKeyboardProc proc)
        //{
        //    using (Process currentProcess = Process.GetCurrentProcess())
        //    using (ProcessModule currentModule = currentProcess.MainModule)
        //    {
        //        return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(currentModule.ModuleName), 0);
        //    }
        //}

        //private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        //private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        //{
        //    if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        //    {
        //        int vkCode = Marshal.ReadInt32(lParam);
        //        Console.WriteLine((Keys)vkCode);
        //    }
        //    return CallNextHookEx(_hookID, nCode, wParam, lParam);
        //}

        /**
         * <param name="idHook">Id of the type of hook to start</param>
         * <param name="lpfn">The callback. Long Pointer to FuNction</param>
         * <param name="hMod">Hook module???</param>
         * <param name="dwThreadId"></param>
         */
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetCurrentThreadId();
    }

}
