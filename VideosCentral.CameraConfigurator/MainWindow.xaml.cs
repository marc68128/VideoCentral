using MahApps.Metro.Controls;
using VideosCentral.CameraConfigurator.Services.Contracts;
using VideosCentral.CameraConfigurator.ViewModels;
using VideosCentral.Services.Contracts;

namespace VideosCentral.CameraConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel(
                Kernel.Kernel.Resolve<IUsbDevicesService>(), 
                Kernel.Kernel.Resolve<IWindowService>(),
                Kernel.Kernel.Resolve<IConfigurationFileService>(),
                Kernel.Kernel.Resolve<IVideoFileService>()); 
        }
    }
}
