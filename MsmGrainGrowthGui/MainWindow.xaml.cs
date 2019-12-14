using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;



namespace GrainGrowthGui
{
        public partial class MainWindow
        {
            private readonly CelluralAutomatonViewModel _viewModel;
            public MainWindow()
            {
                InitializeComponent();
                _viewModel = (CelluralAutomatonViewModel)this.DataContext;
                _viewModel.ImageRendered += Render;
                _viewModel.PropertyChanged += PropertyChangedHandler; 
                _viewModel.Executor = "SimulationExecutor";
        }

            private void Render(object sender, BitmapSource e)
            {
                this.CelluralSpaceImage.Source = e;
            }

            private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
            {
                switch (e.PropertyName)
                {
                    case "Executor":
                        AutomatonOptionsStackPanel.Visibility = 
                            _viewModel.Executor == "SimulationExecutor"
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                       
                        ThresholdStackPanel.Visibility =
                            _viewModel.Executor == "CurvatureExecutor"
                            ? Visibility.Visible
                            : Visibility.Collapsed;

                    break;
                    case "IsGenerated":
                        if (_viewModel.IsGenerated)
                        {
                            BoundaryComboBox.IsEnabled = 
                            ExecutorComboBox.IsEnabled = 
                            NeighbourhoodComboBox.IsEnabled = 
                            GrainsTextBox.IsEnabled = 
                            InclusionsCountTextBox.IsEnabled = 
                            MaxRadiusTextBox.IsEnabled =
                            SpaceSizeTextBox.IsEnabled = 
                            MinRadiusTextBox.IsEnabled = 
                            ThresholdTextBox.IsEnabled = false;
                        }
                        else
                        {
                            BoundaryComboBox.IsEnabled =
                            ExecutorComboBox.IsEnabled = 
                            NeighbourhoodComboBox.IsEnabled = 
                            GrainsTextBox.IsEnabled =
                            InclusionsCountTextBox.IsEnabled =
                            MaxRadiusTextBox.IsEnabled = 
                            SpaceSizeTextBox.IsEnabled = 
                            MinRadiusTextBox.IsEnabled =
                            ThresholdTextBox.IsEnabled = true;
                        }

                        break;
                }


                if (e.PropertyName == "Executor")
                {
                    
                }
            }



    }
}
