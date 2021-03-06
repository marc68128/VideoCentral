﻿using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Timers;
using VideosCentral.Domain.Enum;
using VideosCentral.Domain.Model;
using VideosCentral.Kernel;
using VideosCentral.Logger;
using VideosCentral.Logger.Interface;
using VideosCentral.Services;
using VideosCentral.Services.Contracts;

namespace VideosCentral.VideosTransferer
{
    public partial class VideosTransfererService : ServiceBase
    {
        private readonly IConfigurationFileService _configurationFileService;
        private readonly IWindowsVolumeListenerService m_WindowsVolumeListenerService;
        private readonly IVideoFileService _videoFileService;
        private readonly ILogger _logger;

        public VideosTransfererService()
        {
            var encryptionService = new EncryptionService();

            _videoFileService = new VideoFileService();
            _configurationFileService = new ConfigurationFileService(encryptionService, _videoFileService);
            m_WindowsVolumeListenerService = new WindowsVolumeListenerService();
            _logger = new FileLogger(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));

            KernelConfig.RegisterInstance<ILogger>(_logger);
            KernelConfig.RegisterInstance<IEncryptionService>(encryptionService);
            KernelConfig.RegisterInstance<IVideoFileService>(_videoFileService);
            KernelConfig.RegisterInstance<IConfigurationFileService>(_configurationFileService);
            KernelConfig.RegisterInstance<IWindowsVolumeListenerService>(m_WindowsVolumeListenerService);

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            m_WindowsVolumeListenerService.DriveInsertedEvent += DriveInserted;
            _logger.LogInfo("Service started");
        }

        protected override void OnStop()
        {
            m_WindowsVolumeListenerService.DriveInsertedEvent -= DriveInserted;
            _logger.LogInfo("Service stopped");
        }

        private void DriveInserted(object sender, string s)
        {
            _logger.LogInfo($"\"{s}\" : Inserted");

            var configurationFile = _configurationFileService.GetConfigurationFile(s);
            if (configurationFile == null)
            {
                _logger.LogInfo($"\"{s}\" : No configuration file found on drive - Use the CameraConfigurator to create one.");
                return;
            }

            var videosOnDrive = _videoFileService.GetAllVideoFiles(s);
            var videosInConfig = configurationFile.VideoPaths.ToList();

            var newVideos = videosOnDrive.Except(videosInConfig).Select(newVideo => Path.Combine(s, newVideo)).ToList();

            if (!newVideos.Any())
            {
                _logger.LogInfo($"\"{s}\" : There's no new videos on the drive - Nothing to copy.");
                return;
            }

            foreach (var newVideo in newVideos)
            {
                var ext = Path.GetExtension(newVideo); 
                var counter = 1; 
                var destinationFolder = Path.Combine(ConfigurationManager.AppSettings["VideoFolderPath"], $"{DateTime.Now:yyyy-MM-dd}") ;
                var destinationFileName = $"{configurationFile.LastName}_{configurationFile.FirstName}_{counter}{ext}";
                var destinationPath = Path.Combine(destinationFolder, destinationFileName);

                if (!Directory.Exists(destinationFolder))
                    Directory.CreateDirectory(destinationFolder);

                while (File.Exists(destinationPath))
                {
                    counter++;
                    destinationFileName = $"{configurationFile.LastName}_{configurationFile.FirstName}_{counter}{ext}";
                    destinationPath = Path.Combine(destinationFolder, destinationFileName);
                }


                _logger.LogInfo($"\"{s}\" : Start copying {newVideo} to {destinationPath}.");
                File.Copy(newVideo, destinationPath);
                _logger.LogInfo($"\"{s}\" : End copying {newVideo} to {destinationPath}.");

                videosInConfig.Add(newVideo);

                _configurationFileService.CreateConfigurationFile(s, new Domain.Model.Configuration(configurationFile.LastName, configurationFile.FirstName, configurationFile.LicenceNumber, videosInConfig));

                _logger.LogInfo($"\"{s}\" : Configuration file updated.");
            }
        }

    }
}
