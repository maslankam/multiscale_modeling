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
        //TODO: Add string validation this is really important!
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

        public string StepsPerClick
        {
            get { return this._stepsPerClick.ToString(); }
            set { this._stepsPerClick = Convert.ToInt32(value); }
        }

        private int _stepsPerClick;
        private int _grainCount;
        private int _spaceSize;
        private CelluralAutomaton automaton;
        private Grain[] grains;
        private Cell[,] space;
        private Cell[,] previousSpace;
        private int _step;

        public CelluralAutomaton Automaton { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.GrainCount = "10";
            this.StepsPerClick = "1";
            this.SpaceSize = "40";            
            this.Step = 0;
            StepLabel.Content = Step;
        }

       
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            for(int k = 0; k < Convert.ToInt32(this.StepsPerClick); k++)
            {
                if (_step == 0)
                {
                    this.automaton = new CelluralAutomaton(this._spaceSize);

                    grains = new Grain[Convert.ToInt32(GrainCount)];

                    var r = new Random();

                    space = automaton.Space.currentState;
                    previousSpace = automaton.Space.lastState;

                    for (int i = 0; i < this.grains.Length; i++)
                    {
                        this.grains[i] = new Grain(i, System.Drawing.Color.FromArgb(0, r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))); // TODO: get random from HSV for fancy colors
                        space[r.Next(0, this._spaceSize - 1), r.Next(0, this._spaceSize - 1)].GrainMembership = this.grains[i]; //TODO: check if not hiting occuppied cell
                    }

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
                        space[i, j] = new Cell();
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
                        if (rawImageIndex >= rawImage.Length)
                        {
                            System.Diagnostics.Trace.WriteLine($"pixel [{i},{j}], rawIndex: {rawImageIndex}, outide of {rawImage.Length} bound");
                        }
                        else
                        {
                            rawImage[rawImageIndex++] = pixelColor.R;
                            rawImage[rawImageIndex++] = pixelColor.G;
                            rawImage[rawImageIndex++] = pixelColor.B;
                            rawImage[rawImageIndex++] = 0;
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
