using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoCentral.VideoLibrairy.ViewModels
{
    public class MediaPlayerViewModel : BaseViewModel
    {
        private Uri _videoSource;
        private TimeSpan _currentPosition;

        public MediaPlayerViewModel()
        {
            VideoSource = new Uri(@"C:\Users\marc.unterseh\Desktop\VideosCentral\2018-03-15\Unterseh_Marc_1.MP4");
            CurrentPosition = new TimeSpan(0,0,10);
        }


        public TimeSpan CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                _currentPosition = value;
                OnPropertyChanged();
            }
        }
        public Uri VideoSource
        {
            get { return _videoSource; }
            set
            {
                _videoSource = value;
                OnPropertyChanged();
            }
        }

    }
}
