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
                        this.AutomatonOptions_StackPanel.Visibility = _viewModel.Executor == "SimulationExecutor"
                            ? Visibility.Visible
                            : Visibility.Hidden;
                        break;
                    case "IsGenerated":
                        if (_viewModel.IsGenerated)
                        {
                            this.Boundary_ComboBox.IsEnabled = false;
                            this.Executor_ComboBox.IsEnabled = false;
                            this.Neighbourhood_ComboBox.IsEnabled = false;
                            this.Grains_TextBox.IsEnabled = false;
                            this.InclusionsCount_TextBox.IsEnabled = false;
                            this.MaxRadius_TextBox.IsEnabled = false;
                            this.SpaceSize_TextBox.IsEnabled = false;
                            this.MinRadius_TextBox.IsEnabled = false;
                        }
                        else
                        {
                        this.Boundary_ComboBox.IsEnabled = true;
                        this.Executor_ComboBox.IsEnabled = true;
                        this.Neighbourhood_ComboBox.IsEnabled = true;
                        this.Grains_TextBox.IsEnabled = true;
                        this.InclusionsCount_TextBox.IsEnabled = true;
                        this.MaxRadius_TextBox.IsEnabled = true;
                        this.SpaceSize_TextBox.IsEnabled = true;
                        this.MinRadius_TextBox.IsEnabled = true;
                    }

                        break;
                }


                if (e.PropertyName == "Executor")
                {
                    
                }
            }

    }
}
