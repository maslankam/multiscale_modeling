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

using Model;

namespace MsmGrainGrowthGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CelluralAutomaton Automaton { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PixelFormat pf = PixelFormats.Bgr32;
            int width = 200;
            int height = 200;
            int rawStride = (width * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * height];

            // Initialize the image with data.
            Random value = new Random();
            value.NextBytes(rawImage);

            // Create a BitmapSource.
            BitmapSource bitmap = BitmapSource.Create(width, height,
                96, 96, pf, null,
                rawImage, rawStride);

            // Create an image element;
            Image myImage = new Image();
            myImage.Width = 200;
            // Set image source.
            CelluralSpaceView.Source = bitmap;

            //Uri resourceUri = new Uri("/Images/Lama.jpg", UriKind.Relative);
            //imgDynamic.Source = new BitmapImage(resourceUri);

        }

    }
}
