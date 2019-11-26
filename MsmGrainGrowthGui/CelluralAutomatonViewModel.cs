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
using System.Xml.Linq;

namespace GrainGrowthGui
{
    class CelluralAutomatonViewModel
    {
        #region Properties
        public int SpaceSize
        {
            get { return _spaceSize; }
            set
            {
                _spaceSize = value;

            }
        }

        public int GrainsCount
        {
            get { return _grainsCount; }
            set
            {
                _grainsCount = value;
            }
        }

        public int InclusionsCount
        {
            get { return _inclusionsCount; }
            set
            {
                _inclusionsCount = value;
            }
        }

        public int MinRadius
        {
            get { return _minRadius; }
            set
            {
                _minRadius = value;
            }
        }

        public int MaxRadius
        {
            get { return _maxRadius; }
            set
            {
                _maxRadius = value;
            }
        }

        
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
        private BitmapSource _imageSource;
        private bool _isSaved;


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
            _neighbourhood = new MooreNeighbourhood(_boundary);
            _renderEngine = new SpaceRenderingEngine();
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

            _imageSource = _renderEngine.Render(_automaton.Space);
            ImageRendered?.Invoke(this, _imageSource);
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
            _imageSource = _renderEngine.Render(_automaton.Space);
            ImageRendered.Invoke(this, _imageSource);
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
                2,
                0,
                0,
                _minRadius,
                _maxRadius,
                _transition,
                _neighbourhood, 
                _boundary
            );
            _isAutomatonGenerated = false;

            _imageSource = _renderEngine.Render(_automaton.Space);
            ImageRendered.Invoke(this, _imageSource);
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

        #region OpenCommand
        void OpenExecute()
        {
            // TODO: Add file dialog!

            var doc = XDocument.Load(@"C:\Users\mikim\Desktop\Nowy folder\hello2.xml");
            var reader = new XmlReader();
            var state = reader.Read(doc);

            _automaton = state.automaton;
            _spaceSize = state.spaceSize;
            _grainsCount = state.grainsCount;
            _inclusionsCount = state.inclusionsCount;
            _minRadius = state.minRadius;
            _maxRadius = state.maxRadius;
            _transition = state.transition;
            _neighbourhood = state.neighbourhood;
            _boundary = state.boundary;
            _isAutomatonGenerated = state.isAutomatonGenerated;
            _isSaved = state.isSaved;

            _isAutomatonGenerated = true;

            _imageSource = _renderEngine.Render(_automaton.Space);
            ImageRendered.Invoke(this, _imageSource);

            System.Windows.MessageBox.Show("File Opened");
        }

        bool CanOpenExecute()
        {
            return true;
        }

        public ICommand Open
        {
            get
            {
                return new OpenCommand(
            OpenExecute,
            CanOpenExecute);
            }
        }
        #endregion

        #region SaveAsCommand
        void SaveAsExecute()
        {
            var state = new ApplicationState(
                    _automaton,
                    _spaceSize,
                    _grainsCount,
                    _inclusionsCount,
                    _minRadius,
                    _maxRadius,
                    _transition,
                    _neighbourhood,
                    _boundary,
                    _isAutomatonGenerated,
                    _isSaved
                    );
            var factory = new XmlFactory();
            var doc = factory.GetXDocument(state);

            doc.Save(@"C:\Users\mikim\Desktop\Nowy folder\hello2.xml");


            System.Windows.MessageBox.Show("File Saved");
        }

        bool CanSaveAsExecute()
        {
            return true;
        }

        public ICommand SaveAs
        {
            get
            {
                return new SaveAsCommand(
            SaveAsExecute,
            CanSaveAsExecute);
            }
        }
        #endregion

        #region ExportCsvCommand
        void ExportCsvExecute()
        {
            System.Windows.MessageBox.Show("Export Csv");
        }

        bool CanExportCsvExecute()
        {
            return true;
        }

        public ICommand ExportCsv
        {
            get
            {
                return new ExportCsvCommand(
            ExportCsvExecute,
            CanExportCsvExecute);
            }
        }
        #endregion

        #region ExportPngCommand
        void ExportPngExecute()
        {
            System.Windows.MessageBox.Show("Export Png");
        }

        bool CanExportPngExecute()
        {
            return true;
        }

        public ICommand ExportPng
        {
            get
            {
                return new ExportPngCommand(
            ExportPngExecute,
            CanExportPngExecute);
            }
        }
        #endregion


        public event EventHandler<BitmapSource> ImageRendered;
        
    }


}
