using System.Collections.Generic;

namespace VideosCentral.Services.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVideoFileService
    {
        IEnumerable<string> GetAllVideoFiles(string drivePath); 
    }
}