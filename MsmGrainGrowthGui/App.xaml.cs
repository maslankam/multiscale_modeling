using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;

using Model;

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
               MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message + " "+ e.Exception.Source + " " + e.Exception.StackTrace, 
                   "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
               e.Handled = true;
               //TODO: Add log file
               App.Current.Shutdown();
           };

            
        }

    }
}
