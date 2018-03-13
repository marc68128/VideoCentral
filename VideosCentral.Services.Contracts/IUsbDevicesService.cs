using System;

namespace VideosCentral.Services.Contracts
{
    /// <summary>
    /// Listen to drive events (inserting, removing) 
    /// </summary>
    public interface IUsbDevicesService
    {
        #region Events

        /// <summary>
        /// Event raised when a drive is inserted. (With the name of the drive as parameter) 
        /// </summary>
        event EventHandler<string> DriveInsertedEvent;

        /// <summary>
        /// Event raised when a drive is removed. (With the name of the drive as parameter) 
        /// </summary>
        event EventHandler<string> DriveRemovedEvent;

        #endregion
    }
}