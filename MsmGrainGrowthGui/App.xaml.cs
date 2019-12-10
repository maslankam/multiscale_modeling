using System.Windows;

namespace GrainGrowthGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
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
