using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;

using Model;


namespace GrainGrowthGui
{
        public partial class MainWindow : Window
        {
            public MainWindow()
            {
                InitializeComponent();
                var viewModel = (CelluralAutomatonViewModel)this.DataContext;
                viewModel.ImageRendered += Render;
            }

            private void Render(object sender, BitmapSource e)
            {
                this.CelluralSpaceImage.Source = e;
            }
    }
}
