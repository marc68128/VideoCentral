using System;
using System.IO;
using Newtonsoft.Json;
using VideosCentral.Domain.Model;
using VideosCentral.Services.Contracts;

namespace VideosCentral.Services
{
    /// <summary>
    /// Implementation of <see cref="IConfigurationFileService"/>
    /// </summary>
    public class ConfigurationFileService : IConfigurationFileService
    {
        private const string FileName = "DoNotRemove.VideosCentral";
        private const string EncryptionKey = "MACLEACHANGER";
        private readonly IEncryptionService _encryptionService;
        private readonly IVideoFileService _videoFileService;

        public ConfigurationFileService(IEncryptionService encryptionService, IVideoFileService videoFileService)
        {
            _encryptionService = encryptionService;
            _videoFileService = videoFileService;
        }

        /// <summary>
        /// <see cref="IConfigurationFileService.CreateConfigurationFile"/>
        /// </summary>
        public void CreateConfigurationFile(string driveName, Configuration configuration)
        {
            File.WriteAllText(GetConfigurationFilePath(driveName), _encryptionService.Encrypt(JsonConvert.SerializeObject(configuration), EncryptionKey));
        }

        /// <summary>
        /// <see cref="IConfigurationFileService.GetConfigurationFile"/>
        /// </summary>
        public Configuration GetConfigurationFile(string driveName)
        {
            var configurationFilePath = GetConfigurationFilePath(driveName);
            if (!File.Exists(configurationFilePath))
                return null;

            var configurationjson = _encryptionService.Decrypt(File.ReadAllText(configurationFilePath), EncryptionKey);
            return JsonConvert.DeserializeObject<Configuration>(configurationjson);
        }

        private string GetConfigurationFilePath(string driveName)
        {
            return Path.Combine(driveName, FileName);
        }
    }
}
