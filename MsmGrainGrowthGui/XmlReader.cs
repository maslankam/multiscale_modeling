using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Model;
using System.Drawing;
using Model.Boundary;
using Model.Executors;
using Model.Microelements;
using Model.Neighbourhood;
using Model.Transition;

namespace GrainGrowthGui
{
    public class XmlReader
    {
        private static CellularAutomaton _automaton;
        private static int _spaceSize;
        private static int _grainsCount;
        private static int _inclusionsCount;
        private static int _minRadius;
        private static int _maxRadius;
        private static ITransitionRule _transition;
        private static INeighbourhood _neighbourhood;
        private static IBoundaryCondition _boundary;
        private static bool _isAutomatonGenerated;
        private static bool _isSaved;
        private List<Grain> _grains;
        private List<Inclusion> _inclusions;
        private int _step; // TODO: Add step
        private ISimulationExecutor _executor;


        public ApplicationState Read(XDocument doc)
        {
            ReadWindowVariables(doc);

            ReadGrains(doc);

            ReadInclusions(doc);
            
            //TODO: VS quick refactor here !

            var elements = JoinLists(_grains, _inclusions);

            int xSize = Convert.ToInt32(GetValueFromElement(doc, "xSize", "Cells"));
            int ySize = Convert.ToInt32(GetValueFromElement(doc, "ySize", "Cells"));


            var cells = from c in doc.Root?.Descendants()
                        where c.Name == "C"
                        select new { id = c.Attribute("i")?.Value, phase = c.Attribute("p")?.Value};

            int i = 0;
            int j = 0;

            Cell[,] cellsArray = new Cell[xSize, ySize];

            foreach(var cell in cells)
            {
                if(j > ySize - 1)
                {
                    j = 0;
                    i++;
                }

                if( ! String.IsNullOrEmpty( cell.id ))
                {
                    int id = Convert.ToInt32(cell.id);
                    int phase = Convert.ToInt32(cell.phase);

                    var element = (from g in elements
                                where (g.Id == id && g.Phase == phase)
                                select g);

                    var microelements = element as Microelement[] ?? element.ToArray();
                    if (microelements.Count() > 1) throw new FormatException("Ambigious element id");

                    if (microelements.FirstOrDefault() == null) throw new FormatException("Cannot find element referenced in cell");

                    cellsArray[i, j] = new Cell(microelements.FirstOrDefault());
                    
                }
                else
                {
                    cellsArray[i, j] = new Cell();
                }
                j++;
            }

            var space = new CelluralSpace(cellsArray);

            _automaton = new CellularAutomaton(
                space,
                _grains,
                _inclusions,
                _transition,
                _neighbourhood,
                _boundary,
                _executor
            );


            return new ApplicationState
            (
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
        }

        private void ReadInclusions(XDocument doc)
        {
            _inclusions = new List<Inclusion>();

            var querry = (from g in doc.Root?.Descendants()
                          where g.Name == "Inclusion"
                          select new Inclusion(
                                        Convert.ToInt32(g.Attribute("Id")?.Value),
                                        Convert.ToInt32(g.Attribute("P")?.Value),
                                        Convert.ToInt32(g.Attribute("Rad")?.Value),
                                        Color.FromArgb(
                                            Convert.ToInt32(g.Attribute("A")?.Value),
                                            Convert.ToInt32(g.Attribute("R")?.Value),
                                            Convert.ToInt32(g.Attribute("G")?.Value),
                                            Convert.ToInt32(g.Attribute("B")?.Value)
                                        )));
            foreach(var inclusion in querry)
            {
                _inclusions.Add(inclusion);
            }
        }

        private void ReadGrains(XDocument doc)
        {
            _grains = new List<Grain>();

            var querry = (from g in doc.Root?.Descendants()
                              where g.Name == "Grain"
                              select new Grain(
                                          Convert.ToInt32(g.Attribute("Id")?.Value),
                                          Convert.ToInt32(g.Attribute("P")?.Value),
                                          Color.FromArgb(
                                              Convert.ToInt32(g.Attribute("A")?.Value),
                                              Convert.ToInt32(g.Attribute("R")?.Value),
                                              Convert.ToInt32(g.Attribute("G")?.Value),
                                              Convert.ToInt32(g.Attribute("B")?.Value)
                                          )));
            foreach(var grain in querry)
            {
                _grains.Add(grain);
            }
        }

        private void ReadWindowVariables(XDocument doc)
        {
            try
            {
                _spaceSize = Convert.ToInt32(GetValueFromElement(doc, "SpaceSize"));

                _grainsCount = Convert.ToInt32(GetValueFromElement(doc, "GrainsCount"));

                _inclusionsCount = Convert.ToInt32(GetValueFromElement(doc, "InclusionsCount"));

                _minRadius = Convert.ToInt32(GetValueFromElement(doc, "MinRadius"));

                _maxRadius = Convert.ToInt32(GetValueFromElement(doc, "MaxRadius"));

                string transitionName = (GetValueFromElement(doc, "Transition"));
                _transition = ApplicationState.GetTransitionByName(transitionName);

                string boundaryName = (GetValueFromElement(doc, "Boundary"));
                _boundary = ApplicationState.GetBoundaryByName(boundaryName);

                string neighbourhoodName = (GetValueFromElement(doc, "Neighbourhood"));
                _neighbourhood = ApplicationState.GetNeighbourhoodByName(neighbourhoodName, _boundary);

                _isSaved = GetValueFromElement(doc, "IsSaved") == "true";
                _isAutomatonGenerated = GetValueFromElement(doc, "IsGenerated") == "true";

                _step = Convert.ToInt32(GetValueFromElement(doc, "Step"));

                string executorName = (GetValueFromElement(doc, "Executor"));
                _executor = ApplicationState.GetExecutorByName(executorName, _step);
            }
            catch (Exception e)
            {
                throw new FormatException("WindowVariables wrong format", e);
            }
        }

        public string GetValueFromElement(XDocument doc, string name, string element = "WindowVariables")
        {
           

            var result = (from v in doc.Root?.Elements()
                          where v.Name == element
                          select v.Attribute(name)?.Value).First();
            return result;
        }

        private List<Microelement> JoinLists(List<Grain> grains, List<Inclusion> inclusions)
        {
            var elements = new List<Microelement>();

            foreach(var grain in grains)
            {
                elements.Add(grain);
            }

            foreach(var inclusion in inclusions)
            {
                elements.Add(inclusion);
            }

            return elements;
        }
       

    }
}
