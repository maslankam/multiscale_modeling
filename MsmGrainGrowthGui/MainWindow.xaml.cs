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
        public string SpaceSize
        {
            get { return this._spaceSize.ToString(); }
            set { this._spaceSize = Convert.ToInt32(value);}
        }

        public string GrainCount
        {
            get { return this._grainCount.ToString(); }
            set { this._grainCount = Convert.ToInt32(value); }
        }

        public int Step
        {
            get { return this._step; }
            set {  this._step = value; }
        }

        private int _grainCount;
        private int _spaceSize;
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
        private int _step;

        public CelluralAutomaton Automaton { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.SpaceSize = "40";            
            this.Step = 0;
            StepLabel.Content = Step;


        }

       
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_step == 0)
            {
                this.automaton = new CelluralAutomaton(this._spaceSize);

                this.redGrain = new Grain(0, System.Drawing.Color.Red);
                this.blueGrain = new Grain(1, System.Drawing.Color.Blue);
                this.cyanGrain = new Grain(2, System.Drawing.Color.Cyan);
                this.magentaGrain = new Grain(3, System.Drawing.Color.Magenta);
                this.yellowGrain = new Grain(4, System.Drawing.Color.Yellow);
                this.greenGrain = new Grain(5, System.Drawing.Color.Green);
                this.darkGreenGrain = new Grain(6, System.Drawing.Color.DarkGreen);

                space = automaton.Space.currentState;
                previousSpace = automaton.Space.lastState;

                var r = new Random();

                space[r.Next(0, this._spaceSize - 1), r.Next(0, this._spaceSize - 1)].GrainMembership = redGrain;
                space[r.Next(0, this._spaceSize - 1), r.Next(0, this._spaceSize - 1)].GrainMembership = blueGrain;
                space[r.Next(0, this._spaceSize - 1), r.Next(0, this._spaceSize - 1)].GrainMembership = cyanGrain;
                space[r.Next(0, this._spaceSize - 1), r.Next(0, this._spaceSize - 1)].GrainMembership = magentaGrain;
                space[r.Next(0, this._spaceSize - 1), r.Next(0, this._spaceSize - 1)].GrainMembership = yellowGrain;
                space[r.Next(0, this._spaceSize - 1), r.Next(0, this._spaceSize - 1)].GrainMembership = greenGrain;
                space[r.Next(0, this._spaceSize - 1), r.Next(0, this._spaceSize - 1)].GrainMembership = darkGreenGrain;

                this.Grains_TextBox.IsEnabled = false;
                this.SpaceSize_TextBox.IsEnabled = false;
            }


            PixelFormat pf = PixelFormats.Bgr32;
            int width = this._spaceSize;
            int height = this._spaceSize;
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
                        if (_step == 0)
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
                
                
                }
            this.Step++;
            this.StepLabel.Content = Step;




            // Create a BitmapSource.
            BitmapSource bitmap = BitmapSource.Create(width, height,
                this._spaceSize, this._spaceSize, pf, null,
                rawImage, rawStride);

            CelluralSpaceView.Source = bitmap;
            
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.Step = 0;
            this.StepLabel.Content = Step;
            this.Grains_TextBox.IsEnabled = true;
            this.SpaceSize_TextBox.IsEnabled = true;

        }
    }
}
