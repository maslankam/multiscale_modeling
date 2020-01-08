using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Model;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.IO;
using Microsoft.Win32;
using Model.Transition;
using System.Runtime.CompilerServices;
using GrainGrowthGui.Commands;
using Model.Boundary;
using Model.Executors;
using Model.Neighbourhood;
using Utility;

namespace GrainGrowthGui
{
    // TODO: SOLID Refactor!!!!!
    class CelluralAutomatonViewModel : INotifyPropertyChanged
    {
        #region Properties

        public CellularAutomaton Automaton
        {
            get => _automaton;
        }

        public int SpaceSize
        {
            get => _spaceSize;
            set => _spaceSize = value;
        }

        public int GrainsCount
        {
            get => _grainsCount;
            set => _grainsCount = value;
        }

        public int InclusionsCount
        {
            get => _inclusionsCount;
            set => _inclusionsCount = value;
        }

        public int MinRadius
        {
            get => _minRadius;
            set => _minRadius = value;
        }

        public int MaxRadius
        {
            get => _maxRadius;
            set => _maxRadius = value;
        }

        public List<INeighbourhood> Neighbourhoods { get; set; }

        public string Neighbourhood
        {
            get => _neighbourhood.ToString();
            set => _neighbourhood = ApplicationState.GetNeighbourhoodByName("Model.Neighbourhood." + value, _boundary);
        }

        public List<ISimulationExecutor> Executors { get; set; }

        public string Executor
        {
            get => _executor.ToString();
            set
            {
                _executor = ApplicationState.GetExecutorByName("Model.Executors." + value);
                if (_executor is CurvatureExecutor)
                {
                    (_executor as CurvatureExecutor).Threshold = _threshold;
                }
                NotifyPropertyChanged();
            }
        }

        public string Threshold
        {
            get => _threshold.ToString(); 
            set => _threshold = Convert.ToInt32(value);
        }

        public List<IBoundaryCondition> Boundaries { get => _boundaries;
            set => _boundaries = value;
        }
        public string Boundary
        {
            get => _boundary.ToString();
            set => _boundary = ApplicationState.GetBoundaryByName("Model.Boundary." + value);
        // TODO: Make some GetBoundaryByName() on IBoundary level, also dependencies in xml W/R needs changes
        }

        public bool IsGenerated
        {
            get => _isAutomatonGenerated;
            set
            {
                _isAutomatonGenerated = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                _isDeleting = value;
                NotifyPropertyChanged();
            }
        }
       

        #endregion

        #region private members
        private CellularAutomaton _automaton;
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
        private List<IBoundaryCondition> _boundaries;
        private ISimulationExecutor _executor;
        private int _threshold;
        private bool _isDeleting;

        #endregion

        #region constructor
        public CelluralAutomatonViewModel()
        {
            // Lazy initialization of automaton.
            IsGenerated = false; 
            _isRunning = false;

            //Default values
            _spaceSize = 200;
            _grainsCount = 20;
            _inclusionsCount = 0;
            _minRadius = 1;
            _maxRadius = 5;
            _transition = new GrainGrowthRule();
            _threshold = 90;
            

            _renderEngine = new SpaceRenderingEngine();

            _boundaries = new List<IBoundaryCondition>() {
               new AbsorbingBoundary(),
               new PeriodicBoundary() };
            _boundary = _boundaries[0];

            Neighbourhoods = new List<INeighbourhood>()
            {
                new VonNeumanNeighbourhood(_boundary),
                new MooreNeighbourhood(_boundary),
                new HexagonNeighborhood(_boundary),
                new PentagonNeighbourhood(_boundary)
            };
            _neighbourhood = new VonNeumanNeighbourhood(_boundary);

            Executors = new List<ISimulationExecutor>()
            {
                new SimulationExecutor(),
                new CurvatureExecutor()
            };
            _executor = new SimulationExecutor();
        }
        #endregion

        #region GenerateCommand
        void GenerateExecute()
        {
            // TODO: Generate can be replaced by OnModelChange event. This also may enable "save" button

            // Lazy initialization of boundary.
            //_neighbourhood = new MooreNeighbourhood(_boundary);

            _automaton = new CellularAutomaton(
                _spaceSize,
                _grainsCount,
                _inclusionsCount,
                _minRadius,
                _maxRadius,
                _transition,
                _neighbourhood, 
                _boundary,
                _executor
            );
            IsGenerated = true;

            Render();
        }

        bool CanGenerateExecute()
        {
            return ! _isAutomatonGenerated;
        }

        public ICommand Generate =>
            new Command(
                GenerateExecute, 
                CanGenerateExecute);

        #endregion

        #region NextCommand
        void NextExecute()
        {
           
            _automaton.NextStep();
           Render();
        }

        bool CanNextExecute()
        {
            return _isAutomatonGenerated;
        }

        public ICommand Next =>
            new Command(
                NextExecute, 
                CanNextExecute);

        #endregion

        #region ResetCommand
            void ResetExecute()
        {
           _automaton = new CellularAutomaton(
                2,
                0,
                0,
                _minRadius,
                _maxRadius,
                _transition,
                _neighbourhood, 
                _boundary,
                _executor
            );
            IsGenerated = false;

           Render();
        }

        bool CanResetExecute()
        {
            return _isAutomatonGenerated && ! _isRunning;
        }

        public ICommand Reset =>
            new Command(
                ResetExecute, 
                CanResetExecute);

        #endregion

        #region StartCommand
        void StartExecute()
        {
            _isRunning = true;

            _worker = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = true};
            _worker.DoWork += worker_DoWork;
            _worker.ProgressChanged += worker_ProgressChanged;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.RunWorkerAsync();

        }


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                if (_worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                _automaton.NextStep();
                _worker.ReportProgress(0);
                System.Threading.Thread.Sleep(80); // TODO: Remove magic number! Add slider to GUI
            }
                
        }

        

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Render();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

            bool CanStartExecute()
        {
            return _isAutomatonGenerated && ( ! _isRunning );
        }

        public ICommand Start =>
            new Command(
                StartExecute,
                CanStartExecute);

        #endregion

        #region StopCommnad
        void StopExecute()
        {
            _worker.CancelAsync();
            _isRunning = false;
           
        }

        bool CanStopExecute()
        {
            return _isAutomatonGenerated && _isRunning;
        }

        public ICommand Stop =>
            new Command(
                StopExecute,
                CanStopExecute);

        #endregion

        #region OpenCommand
        void OpenExecute()
        {
            // TODO: Add file dialog!
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path;
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

            _automaton = state.Automaton;
            _spaceSize = state.SpaceSize;
            _grainsCount = state.GrainsCount;
            _inclusionsCount = state.InclusionsCount;
            _minRadius = state.MinRadius;
            _maxRadius = state.MaxRadius;
            _transition = state.Transition;
            _neighbourhood = state.Neighbourhood;
            _boundary = state.Boundary;
            IsGenerated = state.IsAutomatonGenerated;
            _isSaved = state.IsSaved;
            _executor = state.Executor;

            //IsGenerated = true;

            Render();
        }

        bool CanOpenExecute()
        {
            return true;
        }

        public ICommand Open =>
            new Command(
                OpenExecute,
                CanOpenExecute);

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
                    _isSaved,
                    _executor
                    );
            var factory = new XmlFactory();
            var doc = factory.GetXDocument(state);

            doc.Save(path);
        }

        bool CanSaveAsExecute()
        {
            return _isAutomatonGenerated;
        }

        public ICommand SaveAs =>
            new Command(
                SaveAsExecute,
                CanSaveAsExecute);

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

        public ICommand ExportCsv =>
            new Command(
                ExportCsvExecute,
                CanExportCsvExecute);

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
            using var fileStream = new FileStream(path, FileMode.Create);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(fileStream);
        }

        bool CanExportPngExecute()
        {
            return _isAutomatonGenerated;
        }

        public ICommand ExportPng =>
            new Command(
                ExportPngExecute,
                CanExportPngExecute);

        #endregion

        #region AddSecondPhaseCommand
        void AddSecondPhaseExecute()
        {
            
        }

        bool CanAddSecondPhaseExecute()
        {
            return IsGenerated;
        }

        public ICommand AddSecondPhase =>
            new Command(
                AddSecondPhaseExecute,
                CanAddSecondPhaseExecute);

        #endregion

        #region DeleteGrainCommand
        void DeleteGrainExecute()
        {
            IsDeleting = IsDeleting ? false : true ;
        }

        bool CanDeleteGrainExecute()
        {
            return IsGenerated;
        }

        public ICommand DeleteGrain =>
            new Command(
               DeleteGrainExecute,
                CanDeleteGrainExecute);

        #endregion

        #region ShowBoundaryCommand
        void ShowBoundaryExecute()
        {

        }

        bool CanShowBoundaryExecute()
        {
            return IsGenerated;
        }

        public ICommand ShowBoundary =>
            new Command(
                ShowBoundaryExecute,
                CanShowBoundaryExecute);

        #endregion

        public event EventHandler<BitmapSource> ImageRendered;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Render()
        {
            _imageSource = _renderEngine.Render(_automaton.Space);
            ImageRendered?.Invoke(this, _imageSource);
        }



    }




}
