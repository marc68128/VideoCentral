using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using VideosCentral.CameraConfigurator.Services.Contracts;

namespace VideosCentral.CameraConfigurator.Services
{
    public class WindowService : IWindowService
    {
        private readonly List<Tuple<string, Window>> _windows;

        public WindowService()
        {
            _windows = new List<Tuple<string, Window>>();
        }

        public void ShowWindow(object viewModel, string id)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var win = new MetroWindow
                {
                    Height = 350, 
                    Width = 500,
                    Content = viewModel,
                    DataContext = viewModel
                };

                _windows.Add(new Tuple<string, Window>(id, win));

                win.Show();

            }));
        }

        public void HideWindow(string windowId)
        {
            foreach (var window in _windows.Where(tuple => tuple.Item1 == windowId))
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    window.Item2.Close();
                }), DispatcherPriority.Normal);
            }
        }
    }
}
