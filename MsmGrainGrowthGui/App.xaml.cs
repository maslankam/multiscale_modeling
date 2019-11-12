using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MsmGrainGrowthGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Application.Current.DispatcherUnhandledException += (s, e) =>
           {
               MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
               e.Handled = true;
               //TODO: Add log file
               App.Current.Shutdown();
           };
        }

    }
}
