using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideosCentral.CameraConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent");
            watcher.EventArrived += new EventArrivedEventHandler(DriveChanged);
            watcher.Query = query;
            watcher.Start();
        }

        private void DriveChanged(object sender, EventArrivedEventArgs e)
        {
            
        }
    }
}
