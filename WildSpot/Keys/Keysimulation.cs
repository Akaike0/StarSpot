using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StarSpot
{
    class Keysimulation
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, uint lParam);

        // HWND
        public static IntPtr hwnd;

        // RN
        public static RandomNR randomNr = new RandomNR();

        // Press Keys
        internal class SimulateKeys
        {
            public static void KeySwitch(int Switcher)
            {
                try
                {
                    switch (Switcher)
                    {
                        case 0:
                            PostMessage(hwnd, 0x100, 0x31, 0x20001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x31, 0x20001);
                            break;

                        case 1:
                            PostMessage(hwnd, 0x100, 50, 0x30001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 50, 0x30001);
                            break;

                        case 2:
                            PostMessage(hwnd, 0x100, 0x33, 0x40001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x33, 0x40001);
                            break;

                        case 3:
                            PostMessage(hwnd, 0x100, 0x34, 0x50001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x34, 0x50001);
                            break;

                        case 4:
                            PostMessage(hwnd, 0x100, 0x35, 0x60001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x35, 0x60001);
                            break;

                        case 5:
                            PostMessage(hwnd, 0x100, 0x36, 0x70001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x36, 0x70001);
                            break;

                        case 6:
                            PostMessage(hwnd, 0x100, 0x37, 0x80001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x37, 0x80001);
                            break;

                        case 7:
                            PostMessage(hwnd, 0x100, 0x38, 0x90001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x38, 0x90001);
                            break;

                        case 8:
                            PostMessage(hwnd, 0x100, 0x39, 0xa0001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x39, 0xa0001);
                            break;

                        case 9:
                            PostMessage(hwnd, 0x100, 0x30, 0xb0001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x30, 0xb0001);
                            break;
                    }
                }
                catch
                {

                }
            }

            public static void KeyUsing(string Switcher)
            {
                try
                {
                    switch (Switcher)
                    {
                        case "1":
                            PostMessage(hwnd, 0x100, 0x31, 0x20001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x31, 0x20001);
                            break;

                        case "2":
                            PostMessage(hwnd, 0x100, 50, 0x30001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 50, 0x30001);
                            break;

                        case "3":
                            PostMessage(hwnd, 0x100, 0x33, 0x40001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x33, 0x40001);
                            break;

                        case "4":
                            PostMessage(hwnd, 0x100, 0x34, 0x50001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x34, 0x50001);
                            break;

                        case "5":
                            PostMessage(hwnd, 0x100, 0x35, 0x60001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x35, 0x60001);
                            break;

                        case "6":
                            PostMessage(hwnd, 0x100, 0x36, 0x70001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x36, 0x70001);
                            break;

                        case "7":
                            PostMessage(hwnd, 0x100, 0x37, 0x80001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x37, 0x80001);
                            break;

                        case "8":
                            PostMessage(hwnd, 0x100, 0x38, 0x90001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x38, 0x90001);
                            break;

                        case "9":
                            PostMessage(hwnd, 0x100, 0x39, 0xa0001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x39, 0xa0001);
                            break;

                        case "Y":
                            PostMessage(hwnd, 0x100, 0x31, 0x2c0001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x31, 0x2c0001);
                            break;

                        case "G":
                            PostMessage(hwnd, 0x100, 0x47, 0x00220001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x47, 0xC0220001);
                            break;

                        case "X":
                            PostMessage(hwnd, 0x100, 50, 0x2d0001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 50, 0x2d0001);
                            break;

                        case "C":
                            PostMessage(hwnd, 0x100, 0x43, 0x2e0001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x43, 0x2e0001);
                            break;

                        case "V":
                            PostMessage(hwnd, 0x100, 0x34, 0x2f0001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x34, 0x2f0001);
                            break;

                        case "B":
                            PostMessage(hwnd, 0x100, 0x35, 0x300001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x35, 0x300001);
                            break;

                        case "N":
                            PostMessage(hwnd, 0x100, 0x36, 0x310001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x36, 0x310001);
                            break;

                        case "M":
                            PostMessage(hwnd, 0x100, 0x37, 0x320001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x37, 0x320001);
                            break;

                        case ",":
                            PostMessage(hwnd, 0x100, 0x38, 0x330001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x38, 0x330001);
                            break;

                        case ".":
                            PostMessage(hwnd, 0x100, 0x39, 0x340001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x39, 0x340001);
                            break;

                        case "-":
                            PostMessage(hwnd, 0x100, 0x30, 0xb0001);
                            System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                            PostMessage(hwnd, 0x101, 0x30, 0xb0001);
                            break;
                    }
                }
                catch
                {

                }
            }

            // Move Character with automatic forward
            public static void Move()
            {
                PostMessage(hwnd, 0x100, 0x2E, 0x1530001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x2E, 0xc1530001);
            }

            // Press on TAB
            public static void Tab()
            {
                PostMessage(hwnd, 0x100, 0x09, 0x000F0001);
                PostMessage(hwnd, 0x102, 0x09, 0x000F0001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x70, 0xC00F0001);
            }

            public static void ESC()
            {
                PostMessage(hwnd, 0x100, 0x1B, 0x10000);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x1B, 0x10000);
            }
            // Stats class
            private static Stats stats = new Stats();
            public static void F1()
            {
                PostMessage(hwnd, 0x100, 0x70, 0x003B0001);
                System.Threading.Thread.Sleep(100);
                PostMessage(hwnd, 0x101, 0x70, 0xC03B0001);
            }

            public static bool mouse_moved = false;

            public static void MouseClick()
            {
                if (!mouse_moved)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        // Set the cursor position
                        stats.cursor_login_x((uint)Properties.Settings.Default.pvp_cursor_x_position);
                        stats.cursor_login_y((uint)Properties.Settings.Default.pvp_cursor_y_position);

                        PostMessage(hwnd, 0x200, 0x0, 0x01FC0006);
                        System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                        PostMessage(hwnd, 0x201, 0x0, 0x01FC0006);

                        // Set the cursor position
                        stats.cursor_login_x((uint)Properties.Settings.Default.pvp_cursor_x_position);
                        stats.cursor_login_y((uint)Properties.Settings.Default.pvp_cursor_y_position);

                        System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));

                        mouse_moved = true;
                    }
                }


                for (int i = 0; i < 10; i++)
                {
                    // Click
                    PostMessage(hwnd, 0x201, 0x1, 0);
                    System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                    PostMessage(hwnd, 0x202, 0x1, 0);

                    // Set the cursor position
                    stats.cursor_login_x((uint)Properties.Settings.Default.pvp_cursor_x_position);
                    stats.cursor_login_y((uint)Properties.Settings.Default.pvp_cursor_y_position);

                    // Click
                    PostMessage(hwnd, 0x201, 0x1, 0);
                    System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                    PostMessage(hwnd, 0x202, 0x1, 0);
                }

                mouse_moved = false;
            }
            public static void MouseClickLogin()
            {
                String[] position = Properties.Settings.Default.loginclick_x_y.Split(',');

                if (!mouse_moved)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        // Set the cursor position
                        stats.cursor_login_x(Convert.ToUInt32(position[0]));
                        stats.cursor_login_y(Convert.ToUInt32(position[1]));

                        PostMessage(hwnd, 0x200, 0x0, 0x01FC0006);
                        System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                        PostMessage(hwnd, 0x201, 0x0, 0x01FC0006);

                        // Set the cursor position
                        stats.cursor_login_x(Convert.ToUInt32(position[0]));
                        stats.cursor_login_y(Convert.ToUInt32(position[1]));

                        System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));

                        mouse_moved = true;
                    }
                }


                for (int i = 0; i < 10; i++)
                {
                    // Click
                    PostMessage(hwnd, 0x201, 0x1, 0);
                    System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                    PostMessage(hwnd, 0x202, 0x1, 0);

                    // Set the cursor position
                    stats.cursor_login_x(Convert.ToUInt32(position[0]));
                    stats.cursor_login_y(Convert.ToUInt32(position[1]));

                    // Click
                    PostMessage(hwnd, 0x201, 0x1, 0);
                    System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                    PostMessage(hwnd, 0x202, 0x1, 0);
                }

                mouse_moved = false;
            }

            public static void Space()
            {
                PostMessage(hwnd, 0x100, 0x70, 0x390000);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x70, 0x390000);
            }

            public static void G()
            {
                PostMessage(hwnd, 0x100, 0x47, 0x00220001);
                PostMessage(hwnd, 0x102, 0x67, 0x00220001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x47, 0xC0220001);
            }

            public static void text(string text)
            {
                // Convert the text to chars
                string pw = text;
                char[] chars = pw.ToCharArray();

                // Send it!
                for (int i = 0; i < pw.Length; i++)
                {
                    PostMessage(hwnd, 0x102, chars[i], 0x001C0001);
                }
            }

            public static void Enter()
            {
                PostMessage(hwnd, 0x100, 0x0000000D, 0x011C0001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x0000000D, 0xC11C0001);
            }

            public static void F()
            {
                PostMessage(hwnd, 0x100, 0x46, 0x00210001);
                PostMessage(hwnd, 0x102, 0x66, 0x00210001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x46, 0xC0210001);
            }

            public static void Y()
            {
                PostMessage(hwnd, 0x100, 0x59, 0x2C0001);
                PostMessage(hwnd, 0x100, 0x79, 0x2C0001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x59, 0xC02C0001);
            }

            public static void V()
            {
                PostMessage(hwnd, 0x100, 0x70, 0x2f0001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x70, 0x2f0001);
            }

            public static void R()
            {
                PostMessage(hwnd, 0x100, 0x70, 0x130001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x70, 0x130001);
            }

            public static void S()
            {
                PostMessage(hwnd, 0x100, 0x53, 0x001f0001);
                PostMessage(hwnd, 0x102, 0x73, 0x001f0001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x53, 0xC01f0001);
            }

            public static void Q()
            {
                PostMessage(hwnd, 0x100, 0x51, 0x00100001);
                PostMessage(hwnd, 0x102, 0x71, 0x00100001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x51, 0xC0100001);
            }
            public static void Q(bool state)
            {
                switch(state)
                {
                    case true:
                        PostMessage(hwnd, 0x100, 0x51, 0x00100001);
                        break;
                    case false:
                        PostMessage(hwnd, 0x101, 0x51, 0xC0100001);
                        break;
                }
            }

            public static void E()
            {
                PostMessage(hwnd, 0x100, 0x45, 0x00120001);
                PostMessage(hwnd, 0x102, 0x65, 0x00120001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x45, 0xC0120001);
            }
            public static void E(bool state)
            {
                switch (state)
                {
                    case true:
                        PostMessage(hwnd, 0x100, 0x45, 0x00120001);
                        break;
                    case false:
                        PostMessage(hwnd, 0x101, 0x45, 0xC0120001);
                        break;
                }
            }

            public static void H()
            {
                PostMessage(hwnd, 0x100, 0x48, 0x00230001);
                PostMessage(hwnd, 0x102, 0x68, 0x00230001);
                System.Threading.Thread.Sleep(35 + randomNr.create(2, 13));
                PostMessage(hwnd, 0x101, 0x70, 0x00230001);
            }
        }
    }
}
