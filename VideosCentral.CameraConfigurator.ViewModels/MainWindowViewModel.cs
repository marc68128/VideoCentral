using System;
using VideosCentral.CameraConfigurator.Services.Contracts;
using VideosCentral.Services.Contracts;

namespace VideosCentral.CameraConfigurator.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IUsbDevicesService _usbDevicesService;
        private readonly IWindowService _windowService;
        private readonly IConfigurationFileService _configurationFileService;
        private readonly IVideoFileService _videoFileService;

        public MainWindowViewModel(IUsbDevicesService usbDevicesService, IWindowService windowService, IConfigurationFileService configurationFileService, IVideoFileService videoFileService)
        {
            _usbDevicesService = usbDevicesService;
            _windowService = windowService;
            _configurationFileService = configurationFileService;
            _videoFileService = videoFileService;

            _usbDevicesService.DriveInsertedEvent += DriveInserted;
            _usbDevicesService.DriveRemovedEvent += DriveRemoved;
        }

        ~MainWindowViewModel()
        {
            _usbDevicesService.DriveInsertedEvent -= DriveInserted;
            _usbDevicesService.DriveRemovedEvent -= DriveRemoved;
        }

        private void DriveInserted(object sender, string driveName)
        {
            _windowService.ShowWindow(new CameraConfiguratorViewModel(driveName, _windowService, _configurationFileService, _videoFileService), driveName);
        }

        private void DriveRemoved(object sender, string driveName)
        {
            _windowService.HideWindow(driveName);
        }
    }
}
