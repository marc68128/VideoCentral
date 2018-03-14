using System;

namespace VideosCentral.Services.Contracts
{
    /// <summary>
    /// Listen to volume events (inserting, removing) 
    /// </summary>
    public interface IWindowsVolumeListenerService
    {
        #region Events

        /// <summary>
        /// Event raised when a drive is inserted. (With the drive letter as parameter) 
        /// </summary>
        event EventHandler<string> DriveInsertedEvent;

        /// <summary>
        /// Event raised when a drive is removed. (With the drive letter as parameter) 
        /// </summary>
        event EventHandler<string> DriveRemovedEvent;

        #endregion
    }
}