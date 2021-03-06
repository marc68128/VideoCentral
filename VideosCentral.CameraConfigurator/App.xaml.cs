﻿using System.Windows;
using VideosCentral.CameraConfigurator.Services;
using VideosCentral.CameraConfigurator.Services.Contracts;
using VideosCentral.Kernel;
using VideosCentral.Services;
using VideosCentral.Services.Contracts;

namespace VideosCentral.CameraConfigurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            KernelConfig.RegisterInstance<IWindowsVolumeListenerService>(new WindowsVolumeListenerService());

            var encryptionService = new EncryptionService();
            var videoFileService = new VideoFileService();

            KernelConfig.RegisterInstance<IWindowService>(new WindowService());
            KernelConfig.RegisterInstance<IEncryptionService>(encryptionService);
            KernelConfig.RegisterInstance<IVideoFileService>(videoFileService);
            KernelConfig.RegisterInstance<IConfigurationFileService>(new ConfigurationFileService(encryptionService, videoFileService));

            KernelConfig.RegisterInstance();
            base.OnStartup(e);
        }
    }
}
