namespace Spc.SharePoint.Utils.Core.Helper
{
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.ServiceProcess;

    public class WinSvcUtil : IDisposable
    {
        #region "Properties"

        private WinServiceHandle _svcHandle = WinNativeMethods.OpenSCManager(null, null, WinNativeMethods.ServiceControlAccessRights.SC_MANAGER_CONNECT);

        #endregion

        #region "Constructors"

        [SecurityCritical]
        public WinSvcUtil()
        {
            if (this._svcHandle.IsInvalid)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        #endregion

        #region "Methods"

        internal WinServiceHandle OpenService(string serviceName, WinNativeMethods.ServiceAccessRights desiredAccess)
        {
            WinServiceHandle hndle = WinNativeMethods.OpenService(this._svcHandle, serviceName, desiredAccess);
            if (hndle.IsInvalid)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return hndle;
        }

        public void RemoveRecoveryActions(string serviceName)
        {
            this.SetRestartActions(serviceName, 0, 0, 0, false);
        }

        [SecurityCritical]
        public void SetRestartActions(string serviceName, int restartActionsCount, int resetDays, int delayMinutes, bool restartOnNonCrash)
        {
            uint num = (uint)((delayMinutes * 60) * 1000);
            uint num2 = (uint)(((resetDays * 24) * 60) * 60);
            WinServiceHandle serviceHandle = null;
            IntPtr zero = IntPtr.Zero;
            IntPtr ptr = IntPtr.Zero;
            IntPtr hglobal = IntPtr.Zero;
            try
            {
                serviceHandle = this.OpenService(serviceName, WinNativeMethods.ServiceAccessRights.SERVICE_CHANGE_CONFIG | WinNativeMethods.ServiceAccessRights.SERVICE_START);
                hglobal = Marshal.AllocHGlobal((int)(Marshal.SizeOf(typeof(WinNativeMethods.SC_ACTION)) * restartActionsCount));
                for (int i = 0; i < restartActionsCount; i++)
                {
                    WinNativeMethods.SC_ACTION sc_action = new WinNativeMethods.SC_ACTION
                    {
                        Type = WinNativeMethods.SC_ACTION_TYPE.SC_ACTION_RESTART,
                        Delay = num
                    };
                    Marshal.StructureToPtr(sc_action, (IntPtr)(((long)hglobal) + (i * Marshal.SizeOf(typeof(WinNativeMethods.SC_ACTION)))), false);
                }
                WinNativeMethods.SERVICE_FAILURE_ACTIONS structure = new WinNativeMethods.SERVICE_FAILURE_ACTIONS
                {
                    dwResetPeriod = num2,
                    cActions = (uint)restartActionsCount,
                    lpsaActions = hglobal,
                    lpRebootMsg = null,
                    lpCommand = null
                };
                zero = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WinNativeMethods.SERVICE_FAILURE_ACTIONS)));
                Marshal.StructureToPtr(structure, zero, false);
                if (WinNativeMethods.ChangeServiceConfig2(serviceHandle, WinNativeMethods.ServiceConfig2InfoLevel.SERVICE_CONFIG_FAILURE_ACTIONS, zero) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                if (restartOnNonCrash)
                {
                    WinNativeMethods.SERVICE_FAILURE_ACTIONS_FLAG service_failure_actions_flag = new WinNativeMethods.SERVICE_FAILURE_ACTIONS_FLAG
                    {
                        FailureActionsOnNonCrashFailures = true
                    };
                    ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WinNativeMethods.SERVICE_FAILURE_ACTIONS_FLAG)));
                    Marshal.StructureToPtr(service_failure_actions_flag, ptr, false);
                    if (WinNativeMethods.ChangeServiceConfig2(serviceHandle, WinNativeMethods.ServiceConfig2InfoLevel.SERVICE_CONFIG_FAILURE_ACTIONS_FLAG, ptr) == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(zero);
                }
                if (hglobal != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(hglobal);
                }
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
                if (serviceHandle != null)
                {
                    serviceHandle.Dispose();
                }
            }
        }

        public void SetRestartsOnFailure(string serviceName, int resetDays, int delayMinutes)
        {
            this.SetRestartsOnFailure(serviceName, resetDays, delayMinutes, false);
        }

        public void SetRestartsOnFailure(string serviceName, int resetDays, int delayMinutes, bool restartOnNonCrashFailures)
        {
            this.SetRestartActions(serviceName, 3, resetDays, delayMinutes, restartOnNonCrashFailures);
        }

        public ServiceController[] GetAllWindowsServices()
        {
            return ServiceController.GetServices();
        }

        public static void StartWindowsService(string serviceName, string hostName)
        {
            using (ServiceController sc = new ServiceController(serviceName, hostName))
            {
                if (sc.Status == ServiceControllerStatus.StopPending)
                {
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(180.0));
                }
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    if (sc.Status != ServiceControllerStatus.StartPending)
                    {
                        sc.Start();
                    }
                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(90.0));
                }
            }
        }

        public static void StopWindowsService(string serviceName, string hostName)
        {
            using (ServiceController sc = new ServiceController(serviceName, hostName))
            {
                if (sc.Status == ServiceControllerStatus.StartPending)
                {
                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(60.0));
                }
                if (sc.Status != ServiceControllerStatus.Stopped)
                {
                    if (sc.Status != ServiceControllerStatus.StopPending)
                    {
                        if (sc.CanStop)
                        {
                            sc.Stop();
                        }
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(180.0));
                    }
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(180.0));
                }
            }
        }

        public static void RestartWindowsService(string serviceName, string hostName)
        {
            StopWindowsService(serviceName, hostName);
            StartWindowsService(serviceName, hostName);
        }

        public static void RestartWindowsService(string serviceName)
        {
            if (StringUtil.IsNullOrWhitespace(serviceName))
            {
                return;
            }
            using (ServiceController sc = new ServiceController(serviceName))
            {
                int num = 0;
                while (sc.Status != ServiceControllerStatus.Stopped)
                {
                    try
                    {
                        if (sc.CanStop)
                        {
                            sc.Stop();
                        }
                    }
                    catch
                    {
                    }
                    sc.Refresh();
                    if (++num > 120)
                    {
                        break;
                    }
                }
                sc.Refresh();
                if (sc.Status != ServiceControllerStatus.Stopped)
                {
                    throw new Win32Exception("Could not stop the service named " + serviceName);
                }
                num = 0;
                while (sc.Status != ServiceControllerStatus.Running)
                {
                    try
                    {
                        sc.Start();
                    }
                    catch
                    {
                    }
                    sc.Refresh();
                    if (++num > 120)
                    {
                        break;
                    }
                }
                sc.Refresh();
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    throw new Win32Exception("Could not start the service named " + serviceName);
                }
            }
        }

        #endregion

        #region "IDisposable Implementation"

        public void Dispose()
        {
            if (this._svcHandle != null)
            {
                this._svcHandle.Dispose();
                this._svcHandle = null;
            }
        }

        #endregion
    }

    public sealed class WinServiceHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        #region "Constructors"

        internal WinServiceHandle()
            : base(true)
        {
        }

        #endregion

        #region "Methods"

        protected override bool ReleaseHandle()
        {
            if (base.handle != IntPtr.Zero)
            {
                bool flag = WinNativeMethods.CloseServiceHandle(base.handle);
                base.handle = IntPtr.Zero;
                return flag;
            }
            return false;
        }

        #endregion
    }
}
