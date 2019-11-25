using System;
using Model;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp1
{
   


    class Program
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

        static void Main(string[] args)
        {



            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new PeriodicBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(boundary);
            _automaton = new CelluralAutomaton(500, 40, 30, 1, 5, transition, neighbourhood, boundary);

            _spaceSize = 500;
            _grainsCount = 20;
            _inclusionsCount = 0;
            _minRadius = 1;
            _minRadius = 5;
            _transition = new GrainGrowthRule();
            _boundary = new AbsorbingBoundary();
            _neighbourhood = new VonNeumanNeighborhood(_boundary);


            var doc = new XDocument(new XElement("Document"));

            var widowVariables = new XElement("WindowVariables");
            widowVariables.Add(new XAttribute("SpaceSize", _spaceSize));
            widowVariables.Add(new XAttribute("GrainsCount", _grainsCount));
            widowVariables.Add(new XAttribute("InclusionsCount", _inclusionsCount));
            widowVariables.Add(new XAttribute("MinRadius", _minRadius));
            widowVariables.Add(new XAttribute("MaxRadius", _inclusionsCount));
            widowVariables.Add(new XAttribute("Transition", _transition.GetType()));
            widowVariables.Add(new XAttribute("Neighbourhood", _neighbourhood.GetType()));
            widowVariables.Add(new XAttribute("Boundary", _boundary.GetType()));
            widowVariables.Add(new XAttribute("isGenerated", _isAutomatonGenerated));
            widowVariables.Add(new XAttribute("isSaved", _isSaved));
            doc.Root.Add(widowVariables);

            var grains = new XElement("Grains");
            foreach (var grain in _automaton.Grains)
            {
                var grainXmlElement = new XElement("Grain");

                var id = new XAttribute("Id", grain.Id);
                grainXmlElement.Add(id);

                var phase = new XAttribute("P", grain.Phase);
                grainXmlElement.Add(phase);
                grainXmlElement.Add(new XAttribute("A", grain.Color.A));
                grainXmlElement.Add(new XAttribute("R", grain.Color.R));
                grainXmlElement.Add(new XAttribute("G", grain.Color.G));
                grainXmlElement.Add(new XAttribute("B", grain.Color.B));

                grains.Add(grainXmlElement);
            }
            doc.Root.Add(grains);

            var inclusions = new XElement("Inclusions");
            foreach (var inclusion in _automaton.Inclusions)
            {
                var inclusionXmlElement = new XElement("Grain");

                var id = new XAttribute("Id", inclusion.Id);
                inclusionXmlElement.Add(id);

                var phase = new XAttribute("P", inclusion.Phase);
                inclusionXmlElement.Add(phase);
                inclusionXmlElement.Add(new XAttribute("rad", inclusion.Radius));
                inclusionXmlElement.Add(new XAttribute("A", inclusion.Color.A));
                inclusionXmlElement.Add(new XAttribute("R", inclusion.Color.R));
                inclusionXmlElement.Add(new XAttribute("G", inclusion.Color.G));
                inclusionXmlElement.Add(new XAttribute("B", inclusion.Color.B));

                inclusions.Add(inclusionXmlElement);
            }

            doc.Root.Add(inclusions);


            var cells = new XElement("Cells");
            for (int i = 0; i < _automaton.Space.GetXLength(); i++)
            {
                var row = new XElement("Row");
                row.Add(new XAttribute("x", i));
                for (int j = 0; j < _automaton.Space.GetYLength(); j++)
                {
                    //Console.WriteLine($"{i},{j}");
                    var cell = _automaton.Space.GetCell(i, j);

                    var c = new XElement("c");
                    c.Add(new XAttribute("y", j));
                    c.Add(new XAttribute("p", cell?.Phase?.ToString() ?? ""));
                    c.Add(new XAttribute("i", cell?.MicroelementMembership?.Id.ToString() ?? ""));

                    row.Add(c);
                }
                cells.Add(row);
            }
            doc.Root.Add(cells);

            //Console.WriteLine("Saving");
            doc.Save(@"C:\Users\mikim\Desktop\Multiscale Modeling\ConsoleApp1\Nowy dokument tekstowy.xml");
        }
    }
}
