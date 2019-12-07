using Xunit;
using System.Drawing;
using System.Collections.Generic;
using Model;
using GrainGrowthGui;
using Model.Transition;

namespace Test
{
    public class XmlFactoryTest
    {

        [Fact]
        public void BasicTest()
        {
            #region expectedXml
            string expected =
@"<Document>
  <WindowVariables SpaceSize=""3"" GrainsCount=""2"" InclusionsCount=""2"" MinRadius=""1"" MaxRadius=""1"" Transition=""Model.Transition.GrainGrowthRule"" Neighbourhood=""Model.VonNeumanNeighbourhood"" Boundary=""Model.AbsorbingBoundary"" IsGenerated=""false"" IsSaved=""false"" Step=""0"" Executor=""Model.SimulationExecutor"" />
  <Grains>
    <Grain Id=""0"" P=""0"" A=""1"" R=""2"" G=""3"" B=""4"" />
    <Grain Id=""1"" P=""0"" A=""5"" R=""6"" G=""7"" B=""8"" />
  </Grains>
  <Inclusions>
    <Inclusion Id=""0"" P=""1"" Rad=""1"" A=""1"" R=""2"" G=""3"" B=""4"" />
    <Inclusion Id=""1"" P=""1"" Rad=""1"" A=""5"" R=""6"" G=""7"" B=""8"" />
  </Inclusions>
  <Cells xSize=""3"" ySize=""3"">
    <Row x=""0"">
      <C y=""0"" p=""0"" i=""0"" />
      <C y=""1"" p=""0"" i=""1"" />
      <C y=""2"" p=""1"" i=""0"" />
    </Row>
    <Row x=""1"">
      <C y=""0"" p=""1"" i=""1"" />
      <C y=""1"" p="""" i="""" />
      <C y=""2"" p="""" i="""" />
    </Row>
    <Row x=""2"">
      <C y=""0"" p="""" i="""" />
      <C y=""1"" p="""" i="""" />
      <C y=""2"" p="""" i="""" />
    </Row>
  </Cells>
</Document>";
            #endregion

            var factory = new XmlFactory();

            int spaceSize = 3;
            int grainsCount = 2;
            int inclusionsCount = 2;
            int minRadius = 1;
            int maxRadius = 1;
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);
            ISimulationExecutor executor = new SimulationExecutor();

            var grains = new List<Grain>();
            grains.Add(new Grain(0, 0, Color.FromArgb(1,2,3,4)));
            grains.Add(new Grain(1, 0, Color.FromArgb(5,6,7,8)));
            var inclusions = new List<Inclusion>();
            inclusions.Add(new Inclusion(0, 1, 1, Color.FromArgb(1,2,3,4)));
            inclusions.Add(new Inclusion(1, 1, 1, Color.FromArgb(5,6,7,8)));

            var cells = new Cell[spaceSize,spaceSize];
            cells[0,0] = new Cell(grains[0]);
            cells[0,1] = new Cell(grains[1]);
            cells[0,2] = new Cell(inclusions[0]);
            cells[1,0] = new Cell(inclusions[1]);
            cells[1,1] = new Cell();
            cells[1,2] = new Cell();
            cells[2,0] = new Cell();
            cells[2,1] = new Cell();
            cells[2,2] = new Cell();

            var space = new CelluralSpace(cells);

            
            var automaton = new CellularAutomaton(
                space,
                grains,
                inclusions,
                transition,
                neighbourhood,
                boundary,
                executor
            );

            var state = new ApplicationState(
                    automaton,
                    spaceSize,
                    grainsCount,
                    inclusionsCount,
                    minRadius,
                    maxRadius,
                    transition,
                    neighbourhood,
                    boundary,
                    false,
                    false,
                    executor
                    );
            
            var document = factory.GetXDocument(state);

            var result = document.ToString();

            Assert.Equal(expected, result);
        }
    }
}
