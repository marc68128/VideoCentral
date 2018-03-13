using System;
using System.IO;
using System.Management;
using System.ServiceProcess;
using System.Timers;
using VideosCentral.Domain.Enum;
using VideosCentral.Kernel;
using VideosCentral.Logger;
using VideosCentral.Logger.Interface;

namespace VideosCentral.VideosTransferer
{
    public partial class VideosTransfererService : ServiceBase
    {
        private ManagementEventWatcher watcher;
        public VideosTransfererService()
        {
            KernelConfig.RegisterInstance<ILogger>(new FileLogger(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")));
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent");
            watcher.EventArrived += new EventArrivedEventHandler(DriveChanged);
            watcher.Query = query;
            watcher.Start();
            Kernel.Kernel.Resolve<ILogger>().LogInfo("Service started");
        }

        protected override void OnStop()
        {
            watcher.Stop();
            Kernel.Kernel.Resolve<ILogger>().LogInfo("Service stopped");
        }

        private void DriveChanged(object sender, EventArrivedEventArgs e)
        {
            string driveName = e.NewEvent.Properties["DriveName"].Value.ToString();
            DriveEventType eventType = (DriveEventType)(Convert.ToInt16(e.NewEvent.Properties["EventType"].Value));

            string eventName = Enum.GetName(typeof(DriveEventType), eventType);

            Kernel.Kernel.Resolve<ILogger>().LogInfo($"\"{driveName}\" : {eventName}");
        }
    }
}
