using System.ComponentModel;
using System.Runtime.CompilerServices;
using VideosCentral.CameraConfigurator.ViewModels.Annotations;

namespace VideosCentral.CameraConfigurator.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
