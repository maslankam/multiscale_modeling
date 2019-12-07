using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;



namespace GrainGrowthGui
{
        public partial class MainWindow : Window
        {
            private CelluralAutomatonViewModel _viewModel;
            public MainWindow()
            {
                InitializeComponent();
                _viewModel = (CelluralAutomatonViewModel)this.DataContext;
                _viewModel.ImageRendered += Render;
                _viewModel.PropertyChanged += ExecutionModeChange; // TODO: WTF???
            }

            private void Render(object sender, BitmapSource e)
            {
                this.CelluralSpaceImage.Source = e;
            }

            private void ExecutionModeChange(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Executor")
                {
                    if (_viewModel.Executor == "SimulationExecutor")
                    {
                        this.AutomatonOptions_StackPanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.AutomatonOptions_StackPanel.Visibility = Visibility.Hidden;
                    }
                }
            }
    }
}
