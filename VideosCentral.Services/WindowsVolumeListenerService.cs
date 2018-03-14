using System;
using System.Management;
using VideosCentral.Domain.Enum;
using VideosCentral.Services.Contracts;

namespace VideosCentral.Services
{
    /// <summary>
    /// Implementation of <see cref="IWindowsVolumeListenerService"/> using <see cref="ManagementEventWatcher"/>.
    /// </summary>
    public class WindowsVolumeListenerService : IWindowsVolumeListenerService
    {
        #region Instance variables

        private ManagementEventWatcher _watcher;

        #endregion

        #region Constructors

        /// <summary>
        /// UsbDevicesService parameterless constructor
        /// </summary>
        public WindowsVolumeListenerService()
        {
            _watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent");
            _watcher.EventArrived += WatcherOnEventArrived;
            _watcher.Query = query;
            _watcher.Start();
        }

        #endregion

        #region Destructors

        ~WindowsVolumeListenerService()
        {
            _watcher.Stop();
            _watcher.Dispose();
        }

        #endregion

        #region Events

        /// <summary>
        /// <see cref="IWindowsVolumeListenerService.DriveInsertedEvent"/>
        /// </summary>
        public event EventHandler<string> DriveInsertedEvent;

        /// <summary>
        /// <see cref="IWindowsVolumeListenerService.DriveRemovedEvent"/>
        /// </summary>
        public event EventHandler<string> DriveRemovedEvent;

        #endregion

        #region Methods

        private void WatcherOnEventArrived(object sender, EventArrivedEventArgs e)
        {
            string driveName = e.NewEvent.Properties["DriveName"].Value.ToString();
            DriveEventType eventType = (DriveEventType)Convert.ToInt16(e.NewEvent.Properties["EventType"].Value);

            if (eventType == DriveEventType.Inserted)
                DriveInsertedEvent?.Invoke(this, driveName);
            else if (eventType == DriveEventType.Removed)
                DriveRemovedEvent?.Invoke(this, driveName);
        }

        #endregion
    }
}