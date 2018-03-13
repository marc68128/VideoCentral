using VideosCentral.Domain.Model;

namespace VideosCentral.Services.Contracts
{
    /// <summary>
    /// Set of methods used to create the configuration file
    /// </summary>
    public interface IConfigurationFileService
    {
        /// <summary>
        /// Create the configuration file on the drive passed as parameter with the configuration provided. 
        /// </summary>
        /// <param name="driveName">Name of the drive to configure (Path)</param>
        /// <param name="configuration"><see cref="Configuration"/> used to create the configuration file</param>
        void CreateConfigurationFile(string driveName, Configuration configuration);

        /// <summary>
        /// If the drive passed as parameter is configured, return the matching configuration else return null.
        /// </summary>
        /// <param name="driveName">Name of the drive (Path)</param>
        /// <returns>The configuration if it exist, null otherwise</returns>
        Configuration GetConfigurationFile(string driveName); 
    }
}