using System.Collections.Generic;

namespace VideosCentral.Services.Contracts
{
    public interface IVideoFileService
    {
        IEnumerable<string> GetAllVideoFiles(string drivePath); 
    }
}