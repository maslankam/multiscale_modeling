using System;
using System.Xml.Linq;

namespace GrainGrowthGui
{
    public class XmlFactory
    {
        public XDocument GetXDocument(ApplicationState state)
        {
            var doc = new XDocument(new XElement("Document"));
            
            AddVariables(state, doc);

            AddGrains(state, doc);

            AddInclusions(state, doc);

            AddCells(state, doc);

            return doc;
        }

        private static void AddVariables(ApplicationState state, XDocument doc)
        {
            if(doc == null || state == null) throw new ArgumentNullException();

            var widowVariables = new XElement("WindowVariables");
            widowVariables.Add(new XAttribute("SpaceSize", state.SpaceSize));
            widowVariables.Add(new XAttribute("GrainsCount", state.GrainsCount));
            widowVariables.Add(new XAttribute("InclusionsCount", state.InclusionsCount));
            widowVariables.Add(new XAttribute("MinRadius", state.MinRadius));
            widowVariables.Add(new XAttribute("MaxRadius", state.MaxRadius));
            widowVariables.Add(new XAttribute("Transition", state.Transition.GetType()));
            widowVariables.Add(new XAttribute("Neighbourhood", state.Neighbourhood.GetType()));
            widowVariables.Add(new XAttribute("Boundary", state.Boundary.GetType()));
            widowVariables.Add(new XAttribute("IsGenerated", state.IsAutomatonGenerated));
            widowVariables.Add(new XAttribute("IsSaved", state.IsSaved));
            widowVariables.Add(new XAttribute("Step", state.Automaton.Step));
            widowVariables.Add(new XAttribute("Executor", state.Executor.GetType()));
            doc.Root?.Add(widowVariables);
        }

        private static void AddCells(ApplicationState state, XDocument doc)
        {
            var cells = new XElement("Cells");
            cells.Add(new XAttribute("xSize", state.Automaton.Space.GetXLength()));
            cells.Add(new XAttribute("ySize", state.Automaton.Space.GetYLength()));
            for (int i = 0; i < state.Automaton.Space.GetXLength(); i++)
            {
                var row = new XElement("Row");
                row.Add(new XAttribute("x", i));
                for (int j = 0; j < state.Automaton.Space.GetYLength(); j++)
                {
                    var cell = state.Automaton.Space.GetCell(i, j);

                    var c = new XElement("C");
                    c.Add(new XAttribute("y", j));
                    c.Add(new XAttribute("p", cell?.Phase?.ToString() ?? ""));
                    c.Add(new XAttribute("i", cell?.MicroelementMembership?.Id.ToString() ?? ""));

                    row.Add(c);
                }
                cells.Add(row);
            }
            doc.Root?.Add(cells);
        }

        private static void AddInclusions(ApplicationState state, XDocument doc)
        {
            var inclusions = new XElement("Inclusions");
            foreach (var inclusion in state.Automaton.Inclusions)
            {
                var inclusionXmlElement = new XElement("Inclusion");

                var id = new XAttribute("Id", inclusion.Id);
                inclusionXmlElement.Add(id);

                var phase = new XAttribute("P", inclusion.Phase);
                inclusionXmlElement.Add(phase);
                inclusionXmlElement.Add(new XAttribute("Rad", inclusion.Radius));
                inclusionXmlElement.Add(new XAttribute("A", inclusion.Color.A));
                inclusionXmlElement.Add(new XAttribute("R", inclusion.Color.R));
                inclusionXmlElement.Add(new XAttribute("G", inclusion.Color.G));
                inclusionXmlElement.Add(new XAttribute("B", inclusion.Color.B));

                inclusions.Add(inclusionXmlElement);
            }

            doc.Root?.Add(inclusions);
        }

        private static void AddGrains(ApplicationState state, XDocument doc)
        {
            var grains = new XElement("Grains");
            foreach (var grain in state.Automaton.Grains)
            {
                var grainXmlElement = new XElement("Grain");

                grainXmlElement.Add(new XAttribute("Id", grain.Id));
                grainXmlElement.Add(new XAttribute("P", grain.Phase));
                grainXmlElement.Add(new XAttribute("A", grain.Color.A));
                grainXmlElement.Add(new XAttribute("R", grain.Color.R));
                grainXmlElement.Add(new XAttribute("G", grain.Color.G));
                grainXmlElement.Add(new XAttribute("B", grain.Color.B));

                grains.Add(grainXmlElement);
            }
            doc.Root?.Add(grains);
        }
    }
}
