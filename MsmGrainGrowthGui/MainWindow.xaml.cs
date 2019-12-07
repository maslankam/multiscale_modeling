using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;



namespace GrainGrowthGui
{
        public partial class MainWindow
        {
            private CelluralAutomatonViewModel _viewModel;
            public MainWindow()
            {
                InitializeComponent();
                _viewModel = (CelluralAutomatonViewModel)this.DataContext;
                _viewModel.ImageRendered += Render;
                _viewModel.PropertyChanged += PropertyChangedHandler; 
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
                        this.AutomatonOptionsStackPanel.Visibility = _viewModel.Executor == "SimulationExecutor"
                            ? Visibility.Visible
                            : Visibility.Hidden;
                        break;
                    case "IsGenerated":
                        if (_viewModel.IsGenerated)
                        {
                            this.BoundaryComboBox.IsEnabled = false;
                            this.ExecutorComboBox.IsEnabled = false;
                            this.NeighbourhoodComboBox.IsEnabled = false;
                            this.GrainsTextBox.IsEnabled = false;
                            this.InclusionsCountTextBox.IsEnabled = false;
                            this.MaxRadiusTextBox.IsEnabled = false;
                            this.SpaceSizeTextBox.IsEnabled = false;
                            this.MinRadiusTextBox.IsEnabled = false;
                        }
                        else
                        {
                        this.BoundaryComboBox.IsEnabled = true;
                        this.ExecutorComboBox.IsEnabled = true;
                        this.NeighbourhoodComboBox.IsEnabled = true;
                        this.GrainsTextBox.IsEnabled = true;
                        this.InclusionsCountTextBox.IsEnabled = true;
                        this.MaxRadiusTextBox.IsEnabled = true;
                        this.SpaceSizeTextBox.IsEnabled = true;
                        this.MinRadiusTextBox.IsEnabled = true;
                    }

                        break;
                }


                if (e.PropertyName == "Executor")
                {
                    
                }
            }



    }
}
