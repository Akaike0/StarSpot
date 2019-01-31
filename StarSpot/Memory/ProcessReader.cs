using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StarSpot
{
    class ProcessReader
    {
        // Constans //
        public static IntPtr pHandle;
        public static Int64 base_adress;
        public static Int32 base_adress_x86;
        public static Process memoryProcess = null;

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel64.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, UIntPtr dwSize, out UIntPtr lpNumberOfBytesRead);

        public static IntPtr MakeLParam(int wLow, int wHigh)
        {
            return (IntPtr)(((short)wHigh << 16) | (wLow & 0xffff));
        }

        public static bool OpenProcess()
        {
            memoryProcess = Process.GetCurrentProcess();
            if (memoryProcess.Handle != IntPtr.Zero)
            {
                pHandle = memoryProcess.Handle;
                return true;
            }
            return false;
        }

        public static bool OpenProcxss(int GameID)
        {
            memoryProcess = Process.GetProcessById(GameID);
            if (memoryProcess.Handle != IntPtr.Zero)
            {
                pHandle = memoryProcess.Handle;
                return true;
            }
            return false;
        }

        public uint FindBaseAddress(string modulname)
        {
            try
            {
                return (uint)this.FindModule(modulname).BaseAddress.ToInt32();
            }
            catch { return 0; }
        }

        private ProcessModule FindModule(string modulname)
        {
            for (int i = 0; i < memoryProcess.Modules.Count; i++)
            {
                if (memoryProcess.Modules[i].ModuleName == modulname)
                {
                    return memoryProcess.Modules[i];
                }
            }
            return null;
        }

        // Read Functions //
        public static int readInt(long Address)
        {
            byte[] buffer = new byte[sizeof(Int64)];
            ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static IntPtr readIntptr(long Address)
        {
            byte[] buffer = new byte[sizeof(Int64)];
            ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
            return (IntPtr)BitConverter.ToInt32(buffer, 0);
        }

        public static string readString(long Address)
        {
            byte[] buffer = new byte[60];

            ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)60, IntPtr.Zero);

            string ret = Encoding.Unicode.GetString(buffer);

            if (ret.IndexOf('\0') != -1)
                ret = ret.Remove(ret.IndexOf('\0'));
            return ret;
        }

        public static float readFloat(long Address)
        {
            byte[] buffer = new byte[sizeof(float)];
            ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static byte readByte(long Address)
        {
            byte[] buffer = new byte[1];
            ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)1, IntPtr.Zero);
            return buffer[0];
        }

        public static UInt64 readUInt64(long Address)
        {

            byte[] buffer = new byte[sizeof(Int64)];
            ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)8, IntPtr.Zero);
            return (UInt64)BitConverter.ToUInt64(buffer, 0);
        }

        public static uint readUInt(long Address)
        {
            byte[] buffer = new byte[sizeof(Int32)];
            ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
            return (UInt32)BitConverter.ToUInt32(buffer, 0);
        }

        // Write Functions //
        public static void WriteInt(long Address, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)buffer.Length, IntPtr.Zero);
        }

        public static void WriteString(long Address, string value)
        {
            byte[] buffer = new byte[value.Length * sizeof(char)];
            WriteProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)buffer.Length, IntPtr.Zero);
        }

        public static void WriteUInt(UInt64 Address, uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)buffer.Length, IntPtr.Zero);
        }

        public static void writeFloat(long Address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)buffer.Length, IntPtr.Zero);
        }

        public static ulong FindPattern(ulong start, int size, string pattern, string mask, char delimiter = ' ')
        {
            string[] strArray = pattern.Split(delimiter);
            byte[] pattern1 = new byte[strArray.Length];
            for (int index = 0; index < pattern1.Length; ++index)
                pattern1[index] = Convert.ToByte(strArray[index], 16);
            return FindPattern(start, size, pattern1, mask);
        }

        public static ulong FindPattern(ProcessModule module, string pattern, string mask)
        {
            return FindPattern((ulong)(long)module.BaseAddress, module.ModuleMemorySize, pattern, mask, ' ');
        }

        private static uint FindPattern(byte[] data, byte[] pattern, string mask)
        {
            int length = pattern.Length;
            int num = data.Length - length;
            for (int index1 = 0; index1 < num; ++index1)
            {
                bool flag = true;
                for (int index2 = 0; index2 < length; ++index2)
                {
                    if ((int)mask[index2] == 120 && (int)pattern[index2] != (int)data[index1 + index2] || (int)mask[index2] == 33 && (int)pattern[index2] == (int)data[index1 + index2])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    return (uint)index1;
            }
            return 0U;
        }

        private static ulong FindPattern(ulong start, int size, byte[] pattern, string mask)
        {
            byte[] numArray = new byte[size];
            uint pattern1 = FindPattern(numArray, pattern, mask);
            if ((int)pattern1 != 0)
                return start + (ulong)pattern1;
            else
                return 0UL;
        }

        public static Process memoryProcxss { get; set; }
    }
}

