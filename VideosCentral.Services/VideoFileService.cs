using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideosCentral.Services.Contracts;

namespace VideosCentral.Services
{
    public class VideoFileService : IVideoFileService
    {
        private readonly List<string> _videoExtentions = new List<string> { ".mp4", ".MP4" };

        public IEnumerable<string> GetAllVideoFiles(string drivePath)
        {
            return Directory.GetFiles(drivePath, "*", SearchOption.AllDirectories).Where(path => _videoExtentions.Any(path.EndsWith)).Select(RemoveDriveLetter);
        }

        private string RemoveDriveLetter(string path)
        {
            return path.Substring(Path.GetPathRoot(path).Length);
        }
    }
}
