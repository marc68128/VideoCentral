namespace VideosCentral.Domain.Enum
{
    /// <summary>
    /// Enum used to identify the type of action (inserted, removed) done by a drive. 
    /// </summary>
    public enum DriveEventType
    {
        /// <summary>
        /// The drive has been inserted
        /// </summary>
        Inserted = 2,

        /// <summary>
        /// The drive has been removed
        /// </summary>
        Removed = 3
    }
}