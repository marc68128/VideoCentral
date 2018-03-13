using System.Collections.Generic;
using VideosCentral.CameraConfigurator.Domain.Commands;
using VideosCentral.CameraConfigurator.Services.Contracts;
using VideosCentral.Domain.Model;
using VideosCentral.Services.Contracts;

namespace VideosCentral.CameraConfigurator.ViewModels
{
    public class CameraConfiguratorViewModel : BaseViewModel
    {
        private readonly IEnumerable<string> _videoPaths; 

        private readonly IWindowService _windowService;
        private readonly IConfigurationFileService _configurationFileService;
        private readonly IVideoFileService _videoFileService;

        private string _firstName;
        private string _lastName;
        private string _licenceNumber;

        public CameraConfiguratorViewModel(string driveName, IWindowService windowService, IConfigurationFileService configurationFileService, IVideoFileService videoFileService)
        {
            DriveName = driveName;

            _windowService = windowService;
            _configurationFileService = configurationFileService;
            _videoFileService = videoFileService;

            var config = _configurationFileService.GetConfigurationFile(DriveName);

            FirstName = config?.FirstName;
            LastName = config?.LastName;
            LicenceNumber = config?.LicenceNumber;

            _videoPaths = _videoFileService.GetAllVideoFiles(driveName);

            InitCommands();

            this.PropertyChanged += (sender, args) =>
            {
                ConfigureCommand.IsEnabled = CheckFormValues();
            };
        }

        public string DriveName { get; }

        public string LicenceNumber
        {
            get { return _licenceNumber; }
            set
            {
                _licenceNumber = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand ConfigureCommand { get; set; }

        private void InitCommands()
        {
            ConfigureCommand = new ActionCommand(() =>
            {
                _configurationFileService.CreateConfigurationFile(DriveName, new Configuration(LastName, FirstName, LicenceNumber, _videoPaths));
                _windowService.HideWindow(DriveName);
            }, false);
        }

        private bool CheckFormValues()
        {
            return
                !string.IsNullOrWhiteSpace(LastName) &&
                !string.IsNullOrWhiteSpace(FirstName) &&
                !string.IsNullOrWhiteSpace(LicenceNumber);
        }
    }
}