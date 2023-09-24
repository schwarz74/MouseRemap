using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseRemap
{
    internal static class Program
    {
#if DEBUG
        [DllImport("kernel32.dll")]
        public static extern Int32 AllocConsole();
#endif
        public const int WH_MOUSE_LL = 14;
        private static LowLevelMouseProc _proc_mouse = HookCallback_mouse;
        private static IntPtr _hookID_mouse = IntPtr.Zero;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc_keyboard = HookCallback_keyboard;
        private static IntPtr _hookID_keyboard = IntPtr.Zero;
        private static Keys prevKeyCode = Keys.None;

        private static Button button;
        private static CheckBox cbLeft;
        private static CheckBox cbRight;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            AllocConsole(); Console.WriteLine("init");
#endif
            _hookID_mouse = SetHook_mouse(_proc_mouse);
            _hookID_keyboard = SetHook_keyboard(_proc_keyboard);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Form1 form = new Form1();
            button = form.button1;
            cbLeft = form.mousePressToClickLeft;
            cbRight = form.mousePressToClickRight;

            Application.Run(form);
            UnhookWindowsHookEx(_hookID_mouse);
            UnhookWindowsHookEx(_hookID_keyboard);

            //form.button1;
            //form.mousePressToClickLeft
            //form.mousePressToClickRight
        }
        private static IntPtr SetHook_mouse(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback_mouse(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                //Console.WriteLine(hookStruct.pt.x + ", " + hookStruct.pt.y);
                if (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
                {
                    Console.WriteLine("L button down");
                    if (cbLeft.Checked) 
                    {
                        doLeftUp();
                    }
                    
                }
                if (MouseMessages.WM_LBUTTONUP == (MouseMessages)wParam)
                    Console.WriteLine("L button up");
                if (MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam)
                {
                    Console.WriteLine("R button down");
                    if(cbRight.Checked) 
                    {
                        doRightUp();
                    }
                }
                if (MouseMessages.WM_RBUTTONUP == (MouseMessages)wParam)
                    Console.WriteLine("R button up");
            }

            return CallNextHookEx(_hookID_mouse, nCode, wParam, lParam);
        }

        private static IntPtr SetHook_keyboard(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback_keyboard(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    Console.WriteLine((Keys)vkCode);
                    Console.WriteLine("previous key: " + prevKeyCode);
                    //ctrl+0
                    if ((prevKeyCode == Keys.LControlKey) && ((Keys)vkCode == Keys.D0))
                    {
                        Console.WriteLine("CTRL+0 pressed!");
                        button.PerformClick();
                    }
                    prevKeyCode = (Keys)vkCode;
                }
                else
                {
                    prevKeyCode = Keys.None;
                }


            }
            return CallNextHookEx(_hookID_keyboard, nCode, wParam, lParam);
        }

        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public HardwareInput hi;
        }
        public struct Input
        {
            public int type;
            public InputUnion u;
        }
        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }
        [Flags]
        public enum MouseEventF
        {
            Absolute = 0x8000,
            HWheel = 0x01000,
            Move = 0x0001,
            MoveNoCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            VirtualDesk = 0x4000,
            Wheel = 0x0800,
            XDown = 0x0080,
            XUp = 0x0100
        }

        public static void doLeftUp()
        {
            Input[] inputs = new Input[]
            {
                new Input
                {
                    type = (int) InputType.Mouse,
                    u = new InputUnion
                    {
                        mi = new MouseInput
                        {
                            dwFlags = (uint)MouseEventF.LeftUp,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                }
            };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public static void doRightUp()
        {
            Input[] inputs = new Input[]
            {
                new Input
                {
                    type = (int) InputType.Mouse,
                    u = new InputUnion
                    {
                        mi = new MouseInput
                        {
                            dwFlags = (uint)MouseEventF.RightUp,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                }
            };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
    }
}