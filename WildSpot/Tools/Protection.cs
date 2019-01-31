using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace StarSpot
{
    class Protection
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetKernelObjectSecurity(IntPtr Handle, int securityInformation, [In()] byte[] pSecurityDescriptor);
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool GetKernelObjectSecurity(IntPtr Handle, int securityInformation, [Out()]byte[] pSecurityDescriptor, uint nLength, ref uint lpnLengthNeeded);
        [Flags()]
        public enum ProcessAccessRights
        {
            PROCESS_CREATE_PROCESS = 0x80,
            PROCESS_CREATE_THREAD = 0x2,
            PROCESS_DUP_HANDLE = 0x40,
            PROCESS_QUERY_INFORMATION = 0x400,
            PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,
            PROCESS_SET_INFORMATION = 0x200,
            PROCESS_SET_QUOTA = 0x100,
            PROCESS_SUSPEND_RESUME = 0x800,
            PROCESS_TERMINATE = 0x1,
            PROCESS_VM_OPERATION = 0x8,
            PROCESS_VM_READ = 0x10,
            PROCESS_VM_WRITE = 0x20,
            DELETE = 0x10000,
            READ_CONTROL = 0x20000,
            SYNCHRONIZE = 0x100,
            WRITE_DAC = 0x40000,
            ITE_OWNER = 0x80000,
            STANDARD_RIGHTS_REQUIRED = 0xf0000,
            PROCESS_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xfff)
        }

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);
        static bool isDebuggerPresent = false;

        public static void cProtect()
        {
            IntPtr hProcess = GetCurrentProcess();
            RawSecurityDescriptor dal = ParseProcDescriptor(hProcess);
            dal.DiscretionaryAcl.InsertAce(0, new CommonAce(AceFlags.None, AceQualifier.AccessDenied, 0xf0fff, new SecurityIdentifier(WellKnownSidType.WorldSid, null), false, null));
            EditProcDescriptor(hProcess, dal);
        }
        public static void dProtect()
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // P
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);

                if (isDebuggerPresent)
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        // IP Protection
        private static string address = "gplays.co";
        private static IPAddress[] addresslist;
        public static void pProtect()
        {
            try
            {
                //addresslist = Dns.GetHostAddresses(address);

                //if (addresslist[0].ToString() != "84.200.44.188")
                //{
                //    Process.GetCurrentProcess().Kill();
                //}
            }
            catch { }
        }

        // ID protection
        public static void idProtect()
        {
            if(Login.ID == 0)
            {
                string query_check = new WebClient().DownloadString("" + Properties.Settings.Default.serial_key);

                if (idResult(query_check.ToLower()) != Login.machine_id)
                {
                    Process.GetCurrentProcess().Kill();
                }

                string query = new WebClient().DownloadString("" + Properties.Settings.Default.serial_key + "&machine=" + Login.machine_id.ToString());
                switch (query.ToLower())
                {
                    case "":
                        {
                            Process.GetCurrentProcess().Kill();
                            break;
                        }
                }
            }
        }
        protected static int idResult(string result)
        {
            // Here we had some secret "math" stuff happening to verify the id.
            return 0;
        }

        public static RawSecurityDescriptor ParseProcDescriptor(IntPtr processHandle)
        {
            const int dal_SECURITY_INFORMATION = 0x4;
            byte[] buff = new byte[-1 + 1];
            uint setblock = 0;
            GetKernelObjectSecurity(processHandle, dal_SECURITY_INFORMATION, buff, 0, ref setblock);
            if (setblock < 0 || setblock > short.MaxValue)
            {
                throw new Win32Exception();
            }
            if (!GetKernelObjectSecurity(processHandle, dal_SECURITY_INFORMATION, InlineAssignHelper(ref buff, new byte[setblock]), setblock, ref setblock))
            {
                throw new Win32Exception();
            }
            return new RawSecurityDescriptor(buff, 0);
        }

        public static void EditProcDescriptor(IntPtr processHandle, RawSecurityDescriptor dal)
        {
            const int dal_SECURITY_INFORMATION = 0x4;
            byte[] rawsd = new byte[dal.BinaryLength];
            dal.GetBinaryForm(rawsd, 0);
            if (!SetKernelObjectSecurity(processHandle, dal_SECURITY_INFORMATION, rawsd))
            {
                throw new Win32Exception();
            }
        }

        private static T InlineAssignHelper<T>(ref T app, T ret)
        {
            app = ret;
            return ret;
        }
    }
}
