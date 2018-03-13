using System;
using System.Collections.Generic;

namespace VideosCentral.Domain.Model
{
    /// <summary>
    /// Informations used to identify a device 
    /// </summary>
    public class Configuration
    {
        #region Constructors

        /// <summary>
        /// Configuration constructor.
        /// </summary>
        /// <param name="lastName"><see cref="LastName"/></param>
        /// <param name="firstName"><see cref="FirstName"/></param>
        /// <param name="licenceNumber"><see cref="LicenceNumber"/></param>
        /// <param name="videoPaths"></param>
        public Configuration(string lastName, string firstName, string licenceNumber, IEnumerable<string> videoPaths)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException($"{nameof(lastName)} should not be null. Impossible to create a Configuration");
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException($"{nameof(firstName)} should not be null. Impossible to create a Configuration");
            if (string.IsNullOrWhiteSpace(licenceNumber))
                throw new ArgumentException($"{nameof(licenceNumber)} should not be null. Impossible to create a Configuration");
            if (videoPaths == null)
                throw new ArgumentException($"{nameof(videoPaths)} should not be null. Impossible to create a Configuration");

            LastName = lastName;
            FirstName = firstName;
            LicenceNumber = licenceNumber;
            VideoPaths = videoPaths;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Last name of the device owner
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// First  name of the device owner
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Licence number of the device owner
        /// </summary>
        public string LicenceNumber { get; }

        /// <summary>
        /// List of all videos on the device.
        /// </summary>
        public IEnumerable<string> VideoPaths { get; set; }

        #endregion
    }
}