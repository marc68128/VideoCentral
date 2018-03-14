using System;
using VideosCentral.CameraConfigurator.Services.Contracts;
using VideosCentral.Services.Contracts;

namespace VideosCentral.CameraConfigurator.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IWindowsVolumeListenerService m_WindowsVolumeListenerService;
        private readonly IWindowService _windowService;
        private readonly IConfigurationFileService _configurationFileService;
        private readonly IVideoFileService _videoFileService;

        public MainWindowViewModel(IWindowsVolumeListenerService windowsVolumeListenerService, IWindowService windowService, IConfigurationFileService configurationFileService, IVideoFileService videoFileService)
        {
            m_WindowsVolumeListenerService = windowsVolumeListenerService;
            _windowService = windowService;
            _configurationFileService = configurationFileService;
            _videoFileService = videoFileService;

            m_WindowsVolumeListenerService.DriveInsertedEvent += DriveInserted;
            m_WindowsVolumeListenerService.DriveRemovedEvent += DriveRemoved;
        }

        ~MainWindowViewModel()
        {
            m_WindowsVolumeListenerService.DriveInsertedEvent -= DriveInserted;
            m_WindowsVolumeListenerService.DriveRemovedEvent -= DriveRemoved;
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
