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


namespace MsmGrainGrowthGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int spaceSize;

        private CelluralAutomaton automaton;
        private Grain redGrain;
        private Grain blueGrain;
        private Grain cyanGrain;
        private Grain magentaGrain;
        private Grain yellowGrain;
        private Grain greenGrain;
        private Grain darkGreenGrain;
        private Cell[,] space;
        private Cell[,] previousSpace;

        private string input;
        private int step;

        public CelluralAutomaton Automaton { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            this.spaceSize = 500;
            
            this.automaton = new CelluralAutomaton(this.spaceSize);

            this.redGrain = new Grain(0, System.Drawing.Color.Red);
            this.blueGrain = new Grain(1, System.Drawing.Color.Blue);
            this.cyanGrain = new Grain(2, System.Drawing.Color.Cyan);
            this.magentaGrain = new Grain(3, System.Drawing.Color.Magenta);
            this.yellowGrain = new Grain(4, System.Drawing.Color.Yellow);
            this.greenGrain = new Grain(5, System.Drawing.Color.Green);
            this.darkGreenGrain = new Grain(6, System.Drawing.Color.DarkGreen);

            space = automaton.Space.currentState;
            previousSpace = automaton.Space.lastState;

            space[10, 10].GrainMembership = redGrain;
            space[260, 80].GrainMembership = blueGrain;
            space[0, 290].GrainMembership = cyanGrain;
            space[130, 190].GrainMembership = magentaGrain;
            space[260, 210].GrainMembership = yellowGrain;
            space[280, 140].GrainMembership = greenGrain;
            space[120, 120].GrainMembership = darkGreenGrain;
            
            string input = "";
            int step = 0;
            
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            PixelFormat pf = PixelFormats.Bgr32;
            int width = this.spaceSize;
            int height = this.spaceSize;
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
            //Image myImage = new Image();
            //myImage.Width = 200;
            // Set image source.
            CelluralSpaceView.Source = bitmap;

        }
        
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PixelFormat pf = PixelFormats.Bgr32;
            int width = this.spaceSize;
            int height = this.spaceSize;
            int rawStride = (width * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * height];
            int rawImageIndex = 0;

            // Initialize the image with data.
            Random value = new Random();

            
            for (int i = 0; i < space.GetLength(0); i++)
            {
                for (int j = 0; j < space.GetLength(1); j++)
                {
                    previousSpace[i, j] = space[i, j];
                    space[i,j] = new Cell();
                }
            }

               
                for (int i = 0; i < space.GetLength(0); i++)
                {
                    for (int j = 0; j < space.GetLength(1); j++)
                    {
                        if (step == 0)
                        {
                            space[i, j].GrainMembership = previousSpace[i, j].GrainMembership;
                        }
                        else
                        {
                            space[i, j].GrainMembership = TransitionRule.NextState(previousSpace, i, j);
                        }

                        System.Drawing.Color pixelColor = space?[i, j]?.GrainMembership?.Color ?? System.Drawing.Color.White;

                        // write byte[index] with pixelColor
                        if(rawImageIndex >= rawImage.Length)
                        {
                            System.Diagnostics.Trace.WriteLine($"pixel [{i},{j}], rawIndex: {rawImageIndex}, outide of {rawImage.Length} bound");
                        }
                        else
                        {
                            rawImage[rawImageIndex++] = pixelColor.R;
                            rawImage[rawImageIndex++] = pixelColor.G;
                            rawImage[rawImageIndex++] = pixelColor.B;
                            rawImage[rawImageIndex++] = pixelColor.A;
                        }
                    }
                this.step++;
                }
               
            
        

        // Create a BitmapSource.
        BitmapSource bitmap = BitmapSource.Create(width, height,
                this.spaceSize, this.spaceSize, pf, null,
                rawImage, rawStride);

            // Create an image element;
            //Image myImage = new Image();
            //myImage.Width = 200;
            // Set image source.
            CelluralSpaceView.Source = bitmap;
            
        }
    }
}
