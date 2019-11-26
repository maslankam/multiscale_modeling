using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Model;
using System.Drawing;

namespace GrainGrowthGui
{
    public class XmlReader
    {
        private static CelluralAutomaton _automaton;
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
        private int _step;


        public ApplicationState Read(string path)
        {
            if (!Uri.IsWellFormedUriString(path, UriKind.Absolute)) throw new UriFormatException();
            if (!File.Exists(path)) throw new FileNotFoundException();
            if (Path.GetExtension(path) != "xml") throw new FormatException();

            var doc = XDocument.Load(path);

            ReadWindowVariables(doc);

            ReadGrains(doc);

            ReadInclusions(doc);
            
            //TODO: VS quick refactor here !

            var elements = JoinLists(_grains, _inclusions);

            int xSize = Convert.ToInt32(GetValueFromElement(doc, "xSize", "Cells"));
            int ySize = Convert.ToInt32(GetValueFromElement(doc, "ySize", "Cells"));


            var cells = from r in doc.Root.Descendants("Cells")
                        from c in r.Descendants()
                        select new { id = c.Attribute("i").Value, phase = c.Attribute("p").Value};

            int i = 0;
            int j = 0;

            Cell[,] cellsArray = new Cell[xSize, ySize];

            foreach(var cell in cells)
            {
                if(j > ySize - 1)
                {
                    i = 0;
                    j++;
                }

                if( ! String.IsNullOrEmpty( cell.id ))
                {
                    int id = Convert.ToInt32(cell.id);
                    int phase = Convert.ToInt32(cell.phase);

                    var element = (from g in elements
                                where g.Id == id && g.Id == phase
                                select g).FirstOrDefault();

                    cellsArray[i, j] = new Cell(element);
                }
                i++;
            }

            var space = new CelluralSpace(cellsArray);

            _automaton = new CelluralAutomaton(
                space,
                _grains,
                _inclusions,
                _transition,
                _neighbourhood,
                _boundary,
                _step
            );


            return new ApplicationState
            {
                automaton = _automaton,
                spaceSize = _spaceSize,
                grainsCount = _grainsCount,
                inclusionsCount = _inclusionsCount,
                minRadius = _minRadius,
                maxRadius = _maxRadius,
                transition = _transition,
                neighbourhood = _neighbourhood,
                boundary = _boundary,
                isAutomatonGenerated = _isAutomatonGenerated,
                isSaved = _isSaved
            };
        }

        private void ReadInclusions(XDocument doc)
        {
            _inclusions = (List<Inclusion>)(from g in doc.Root.Descendants("Inclusions")
                                            select new Inclusion(
                                                  Convert.ToInt32(g.Attribute("Id").Value),
                                                  Convert.ToInt32(g.Attribute("P").Value),
                                                  Convert.ToInt32(g.Attribute("Rad").Value),
                                                  Color.FromArgb(
                                                      Convert.ToInt32(g.Attribute("A").Value),
                                                      Convert.ToInt32(g.Attribute("R").Value),
                                                      Convert.ToInt32(g.Attribute("G").Value),
                                                      Convert.ToInt32(g.Attribute("B").Value)
                                                  )));
        }

        private void ReadGrains(XDocument doc)
        {
            _grains = (List<Grain>)(from g in doc.Root.Descendants("Grains")
                                    select new Grain(
                                          Convert.ToInt32(g.Attribute("Id").Value),
                                          Convert.ToInt32(g.Attribute("P").Value),
                                          Color.FromArgb(
                                              Convert.ToInt32(g.Attribute("A").Value),
                                              Convert.ToInt32(g.Attribute("R").Value),
                                              Convert.ToInt32(g.Attribute("G").Value),
                                              Convert.ToInt32(g.Attribute("B").Value)
                                          )));
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

                _isSaved = GetValueFromElement(doc, "isSaved") == "true" ? true : false;
                _isAutomatonGenerated = GetValueFromElement(doc, "isGenerated") == "true" ? true : false;
            }
            catch (Exception e)
            {
                throw new FormatException("WindowVariables wrong format", e);
            }
        }

        public string GetValueFromElement(XDocument doc, string name, string element = "WindowVariables")
        {
            return (from v in doc.Root.Descendants(element)
                    where v.Name == name
                    select v.Value).First();
        }

        private List<Microelement> JoinLists(List<Grain> grains, List<Inclusion> inclusions)
        {
            var elements = new List<Microelement>();

            foreach(var grain in grains)
            {
                elements.Add((Microelement)grain);
            }

            foreach(var inclusion in inclusions)
            {
                elements.Add((Microelement)inclusion);
            }

            return elements;
        }
       

    }
}
