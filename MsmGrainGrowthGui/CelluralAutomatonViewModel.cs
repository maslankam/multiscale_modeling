using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using GrainGrowthGui;
using Model;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Media;

namespace GrainGrowthGui
{
    class CelluralAutomatonViewModel : INotifyPropertyChanged
    {
        #region Properties
        public int SpaceSize
        {
            get { return _spaceSize; }
            set
            {
                _spaceSize = value;
                RaisePropertyChanged("ArtistName");
            }
        }

        public int GrainsCount
        {
            get { return _grainsCount; }
            set
            {
                _grainsCount = value;
                RaisePropertyChanged("GrainsCount");
            }
        }

        public int InclusionsCount
        {
            get { return _inclusionsCount; }
            set
            {
                _inclusionsCount = value;
                RaisePropertyChanged("InclusionsCount");
            }
        }

        public int MinRadius
        {
            get { return _minRadius; }
            set
            {
                _minRadius = value;
                RaisePropertyChanged("MinRadius");
            }
        }

        public int MaxRadius
        {
            get { return _maxRadius; }
            set
            {
                _maxRadius = value;
                RaisePropertyChanged("MaxRadius");
            }
        }

        public BitmapSource ImageSource {get; set;}
        #endregion

        #region private members
        private CelluralAutomaton _automaton;
        private int _spaceSize; 
        private int _grainsCount;
        private int _inclusionsCount;
         private int _minRadius;
        private int _maxRadius;
        private ITransitionRule _transition;
        private INeighbourhood _neighbourhood; 
        private IBoundaryCondition _boundary;
        private bool _isAutomatonGenerated;
        private readonly SpaceRenderingEngine _renderEngine;
        #endregion

        #region constructor
        public CelluralAutomatonViewModel()
        {
            _isAutomatonGenerated = false; //lazy initialization of automaton
            //Default values
            _spaceSize = 500;
            _grainsCount = 20;
            _inclusionsCount = 0;
            _minRadius = 1;
            _maxRadius = 5;
            _transition = new GrainGrowthRule();
            _boundary = new AbsorbingBoundary();
            _neighbourhood = new VonNeumanNeighborhood(_boundary);
            _renderEngine = new SpaceRenderingEngine();
        }
        #endregion

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region GenerateCommand
        void GenerateExecute()
        {
            _automaton = new CelluralAutomaton(
                _spaceSize,
                _grainsCount,
                _inclusionsCount,
                _minRadius,
                _maxRadius,
                _transition,
                _neighbourhood, 
                _boundary
            );
            _isAutomatonGenerated = true;

            //ImageSource = _renderEngine.Render(_automaton.Space);
            ImageSource = Render(_automaton.Space);
            //render 0 step
        }

        bool CanGenerateExecute()
        {
            return ! _isAutomatonGenerated;
        }

        public ICommand Generate 
        { get { return new GenerateCommand(
            GenerateExecute, 
            CanGenerateExecute); } }
        #endregion

        #region NextCommand
        void NextExecute()
        {
           _automaton.NextStep();
            ImageSource = _renderEngine.Render(_automaton.Space);
            //render
        }

        bool CanNextExecute()
        {
            return _isAutomatonGenerated;
        }

        public ICommand Next 
        { get { return new NextCommand(
            NextExecute, 
            CanNextExecute); } }
        #endregion

        #region ResetCommand
            void ResetExecute()
        {
           _automaton = new CelluralAutomaton(
                _spaceSize,
                _grainsCount,
                _inclusionsCount,
                _minRadius,
                _maxRadius,
                _transition,
                _neighbourhood, 
                _boundary
            );
           //render
        }

        bool CanResetExecute()
        {
            return _isAutomatonGenerated;
        }

        public ICommand Reset 
        { get { return new ResetCommand(
            ResetExecute, 
            CanResetExecute); } }
        #endregion

        #region StartCommand
        #endregion

        #region StopCommnad
        #endregion

        private BitmapSource Render(CelluralSpace space) // BitmapSource is WPF class
        {
            System.Windows.Media.PixelFormat pf = PixelFormats.Bgr32;
            int width = space.GetXLength();
            int height = space.GetYLength();
            int rawStride = (width * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * height];
            int rawImageIndex = 0;

            for (int i = 0; i < space.GetXLength(); i++)
            {
                for (int j = 0; j < space.GetYLength(); j++)
                {
                    System.Drawing.Color pixelColor = System.Drawing.Color.FromArgb(155, 255, 0, 0);
                    //space?.GetCell(i,j)?.MicroelementMembership?.Color ?? System.Drawing.Color.White;

                    //write byte[index] with pixelColor
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
            BitmapSource bitmap = BitmapSource.Create(width, height,
                space.GetXLength(), space.GetYLength(), pf, null,
                rawImage, rawStride);

            return bitmap;
        }
    }
}
