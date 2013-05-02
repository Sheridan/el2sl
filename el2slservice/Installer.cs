using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Bng.EL2SL.Service
{
    //[RunInstaller(true)]
    [RunInstaller(true)]
    public class el2slInstaller : Installer
    {
        private ServiceInstaller sInstaller;
        private ServiceProcessInstaller pInstaller;
        private static string serviceName = "Sending event log to syslog server";

        public el2slInstaller()
        {
            pInstaller = new ServiceProcessInstaller();
            sInstaller = new ServiceInstaller();

            pInstaller.Account = ServiceAccount.LocalSystem;

            sInstaller.StartType = ServiceStartMode.Automatic;
            sInstaller.ServiceName = serviceName;
            sInstaller.Description = "This service send your event logs to remote syslog daemon. For configure this service use el2slconf.exe and then restart service";

            sInstaller.AfterInstall += new InstallEventHandler(sInstaller_AfterInstall);
            sInstaller.BeforeUninstall += new InstallEventHandler(sInstaller_BeforeUninstall);

            Installers.Add(pInstaller);
            Installers.Add(sInstaller);
        }

        void sInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            ServiceController r = new ServiceController(serviceName);
            if (r.Status == ServiceControllerStatus.Running)
            {
                r.Stop();
            }
        }

        void sInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController r = new ServiceController(serviceName);
            r.Start();
        }
    }
}