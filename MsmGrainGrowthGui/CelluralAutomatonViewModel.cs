﻿using System;
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
using System.IO;
using Microsoft.Win32;

namespace GrainGrowthGui
{
    // TODO: SOLID Refactor!!!!!
    class CelluralAutomatonViewModel
    {
        #region Properties
        public int SpaceSize
        {
            get { return _spaceSize; }
            set { _spaceSize = value; }
        }

        public int GrainsCount
        {
            get { return _grainsCount; }
            set { _grainsCount = value; }
        }

        public int InclusionsCount
        {
            get { return _inclusionsCount; }
            set { _inclusionsCount = value; }
        }

        public int MinRadius
        {
            get { return _minRadius; }
            set { _minRadius = value; }
        }

        public int MaxRadius
        {
            get { return _maxRadius; }
            set { _maxRadius = value; }
        }

        public List<INeighbourhood> Neighbourhoods
        {
            get { return _neighbourhoods; }
            set { }
        }

        public string Neighbourhood
        {
            get { return _neighbourhood.ToString(); }
            set { 
                _neighbourhood = ApplicationState.GetNeighbourhoodByName("Model." + value, _boundary); 
            }
        }


        public List<IBoundaryCondition> Boundaries { get { return _boundaries; } set { _boundaries = value; } }
        public string Boundary
        {
            get { return _boundary.ToString(); }
            set { _boundary = ApplicationState.GetBoundaryByName("Model." + value); } // TODO: Make some GetBoundaryByName() on IBoundary level, also dependencies in xml W/R needs changes
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
        private bool _isRunning;
        BackgroundWorker _worker;
        List<IBoundaryCondition> _boundaries;
        List<INeighbourhood> _neighbourhoods;


        #endregion

        #region constructor
        public CelluralAutomatonViewModel()
        {
            // Lazy initialization of automaton.
            _isAutomatonGenerated = false; 
            _isRunning = false;

            //Default values
            _spaceSize = 200;
            _grainsCount = 20;
            _inclusionsCount = 0;
            _minRadius = 1;
            _maxRadius = 5;
            _transition = new GrainGrowthRule();
            
            _renderEngine = new SpaceRenderingEngine();

            _boundaries = new List<IBoundaryCondition>() {
               new AbsorbingBoundary(),
               new PeriodicBoundary() };
            _boundary = _boundaries[0];

            _neighbourhoods = new List<INeighbourhood>()
            {
                new VonNeumanNeighbourhood(_boundary),
                new MooreNeighbourhood(_boundary),
                new HexagonNeighborhood(_boundary),
                new PentagonNeighbourhood(_boundary)
            };
            _neighbourhood = new VonNeumanNeighbourhood(_boundary);
        }
        #endregion

        #region GenerateCommand
        void GenerateExecute()
        {
            // TODO: Generate can be replaced by OnModelChange event. This also may enable "save" button

            // Lazy initialization of boundary.
            //_neighbourhood = new MooreNeighbourhood(_boundary);

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
        { get { return new Command(
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
        { get { return new Command(
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
            return _isAutomatonGenerated && ! _isRunning;
        }

        public ICommand Reset 
        { get { return new Command(
            ResetExecute, 
            CanResetExecute); } }
        #endregion

        #region StartCommand
        void StartExecute()
        {
            _isRunning = true;

            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += worker_DoWork;
            _worker.ProgressChanged += worker_ProgressChanged;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.RunWorkerAsync();

            //_automaton.NextStep();

            //_imageSource = _renderEngine.Render(_automaton.Space);
            // ImageRendered.Invoke(this, _imageSource);
            //render
        }


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                if (_worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                _automaton.NextStep();
                _worker.ReportProgress(0);
                System.Threading.Thread.Sleep(100); // TODO: Remove magic number! Add slider to GUI
            }
                
        }

        

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _imageSource = _renderEngine.Render(_automaton.Space);
            ImageRendered.Invoke(this, _imageSource);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

            bool CanStartExecute()
        {
            return _isAutomatonGenerated && ( ! _isRunning );
        }

        public ICommand Start
        {
            get
            {
                return new Command(
            StartExecute,
            CanStartExecute);
            }
        }
        #endregion

        #region StopCommnad
        void StopExecute()
        {
            _worker.CancelAsync();
            _isRunning = false;
           // _automaton.NextStep();

           // _imageSource = _renderEngine.Render(_automaton.Space);
           // ImageRendered.Invoke(this, _imageSource);
        }

        bool CanStopExecute()
        {
            return _isAutomatonGenerated && _isRunning;
        }

        public ICommand Stop
        {
            get
            {
                return new Command(
            StopExecute,
            CanStopExecute);
            }
        }
        #endregion

        #region OpenCommand
        void OpenExecute()
        {
            // TODO: Add file dialog!
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path = "";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            var doc = XDocument.Load(path);
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
        }

        bool CanOpenExecute()
        {
            return true;
        }

        public ICommand Open
        {
            get
            {
                return new Command(
            OpenExecute,
            CanOpenExecute);
            }
        }
        #endregion

        #region SaveAsCommand
        void SaveAsExecute()
        {
            string path;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
            }
            else
            {
                return;
            }
                

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

            doc.Save(path);
        }

        bool CanSaveAsExecute()
        {
            return _isAutomatonGenerated;
        }

        public ICommand SaveAs
        {
            get
            {
                return new Command(
            SaveAsExecute,
            CanSaveAsExecute);
            }
        }
        #endregion

        #region ExportCsvCommand
        void ExportCsvExecute()
        {
            string path;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            var formatter = new CsvSpaceFormatter();
            string csv = formatter.Format(_automaton.Space);
            CsvWriter.WriteToCsv(csv, path);
        }

        bool CanExportCsvExecute()
        {
            return _isAutomatonGenerated;
        }

        public ICommand ExportCsv
        {
            get
            {
                return new Command(
            ExportCsvExecute,
            CanExportCsvExecute);
            }
        }
        #endregion

        #region ExportPngCommand
        void ExportPngExecute()
        {
            string path;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
            }
            else
            {
                return;
            }


            var image = _imageSource;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }

        bool CanExportPngExecute()
        {
            return _isAutomatonGenerated;
        }

        public ICommand ExportPng
        {
            get
            {
                return new Command(
            ExportPngExecute,
            CanExportPngExecute);
            }
        }
        #endregion


        public event EventHandler<BitmapSource> ImageRendered;
        
    }

    



}
