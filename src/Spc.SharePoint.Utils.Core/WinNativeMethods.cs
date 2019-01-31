namespace Spc.SharePoint.Utils.Core
{
    using Spc.SharePoint.Utils.Core.Helper;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Security;

    [EditorBrowsable(EditorBrowsableState.Never), SecurityCritical]
    public sealed class WinNativeMethods
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern WinServiceHandle OpenSCManager(string machineName, string db, ServiceControlAccessRights desiredAccess);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern WinServiceHandle OpenService(WinServiceHandle serviceControlManagerHandle, string serviceName, ServiceAccessRights desiredAccess);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int ChangeServiceConfig2(WinServiceHandle serviceHandle, ServiceConfig2InfoLevel dwInfoLevel, IntPtr lpInfo);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool CloseServiceHandle(IntPtr handle);

        [Flags]
        public enum ServiceAccessRights : uint
        {
            SERVICE_ALL_ACCESS = 0xf01ff,
            SERVICE_CHANGE_CONFIG = 2,
            SERVICE_ENUMERATE_DEPENDENTS = 8,
            SERVICE_INTERROGATE = 0x80,
            SERVICE_PAUSE_CONTINUE = 0x40,
            SERVICE_QUERY_CONFIG = 1,
            SERVICE_QUERY_STATUS = 4,
            SERVICE_START = 0x10,
            SERVICE_STOP = 0x20,
            SERVICE_USER_DEFINED_CONTROL = 0x100
        }

        [Flags]
        public enum ServiceControlAccessRights : uint
        {
            SC_MANAGER_ALL_ACCESS = 0xf003f,
            SC_MANAGER_CONNECT = 1,
            SC_MANAGER_CREATE_SERVICE = 2,
            SC_MANAGER_ENUMERATE_SERVICE = 4,
            SC_MANAGER_LOCK = 8,
            SC_MANAGER_MODIFY_BOOT_CONFIG = 0x20,
            SC_MANAGER_QUERY_LOCK_STATUS = 0x10
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SC_ACTION
        {
            public SC_ACTION_TYPE Type;
            public uint Delay;
        }

        public enum SC_ACTION_TYPE : uint
        {
            SC_ACTION_NONE = 0,
            SC_ACTION_REBOOT = 2,
            SC_ACTION_RESTART = 1,
            SC_ACTION_RUN_COMMAND = 3
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SERVICE_FAILURE_ACTIONS
        {
            public uint dwResetPeriod;
            public string lpRebootMsg;
            public string lpCommand;
            public uint cActions;
            public IntPtr lpsaActions;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_FAILURE_ACTIONS_FLAG
        {
            public bool FailureActionsOnNonCrashFailures;
        }

        public enum ServiceConfig2InfoLevel : uint
        {
            SERVICE_CONFIG_DESCRIPTION = 1,
            SERVICE_CONFIG_FAILURE_ACTIONS = 2,
            SERVICE_CONFIG_FAILURE_ACTIONS_FLAG = 4
        }
    }
}