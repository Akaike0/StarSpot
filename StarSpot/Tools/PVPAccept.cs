using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Threading;


namespace StarSpot
{
    class PVPAccept
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam);
        static Int32 WM_SYSCOMMAND = 0x0112;
        static Int32 SC_RESTORE = 0xF120;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private static RECT pRect;
        private static Size cSize;
        private static Point po;
        private static Size screensize;

        public static bool timer_activated = false;
        DispatcherTimer timer = new DispatcherTimer();

        public PVPAccept()
        {
            //// Timer
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            //timer.Tick += timer_Tick;
            //timer.Start();
        }

        // Stats class
        Stats stats = new Stats();

        public static void accept()
        {
            if (Properties.Settings.Default.pvp_cursor_x_position != 0)
            {
                // Screen size
                try
                {
                    //screensize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                    //// Get the window's size
                    //GetWindowRect(MainWindow.hwnd, ref pRect);

                    //cSize.Width = pRect.Right - pRect.Left;
                    //cSize.Height = pRect.Bottom - pRect.Top;

                    //// Create a panel for pointtoscreen
                    //Panel panel = new Panel();
                    //panel.Location = new System.Drawing.Point(pRect.Left, pRect.Top);
                    //panel.Size = new System.Drawing.Size(cSize.Width, cSize.Height);

                    //try
                    //{
                    //    // Set the app to foreground
                    //    SetForegroundWindow(MainWindow.hwnd);
                    //    SendMessage(MainWindow.hwnd, WM_SYSCOMMAND, SC_RESTORE, 0); // Restore the window
                    //}
                    //catch { }

                    //// Click
                    //po = panel.PointToScreen(new Point((int)Properties.Settings.Default.pvp_cursor_x_position, (int)Properties.Settings.Default.pvp_cursor_y_position)); // Set the new cursor position
                    //Cursor.Position = po;

                    // Click
                    Keysimulation.SimulateKeys.MouseClick();
                }
                catch { }
            }
        }

        // Timer
        public void timer_Tick(object sender, EventArgs e)
        {
            if (!timer_activated)
            {
                // Set the cursor position
                //stats.cursor_x((uint)Properties.Settings.Default.pvp_cursor_x_position);
                //stats.cursor_y((uint)Properties.Settings.Default.pvp_cursor_y_position);
            }
        }
    }
}
