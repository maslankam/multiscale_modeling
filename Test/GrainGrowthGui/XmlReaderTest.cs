using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using GrainGrowthGui;
using Model;
using Model.Boundary;
using Model.Executors;
using Model.Microelements;
using Model.Neighbourhood;
using Model.Transition;
using Xunit;

namespace Test.GrainGrowthGui
{
    public class XmlReaderTest
    {

        [Fact]
        public void BasicTest()
        {
            ApplicationState expected = BuildMockState();
            XDocument input = BuildMockInput();   

            var reader = new XmlReader();

            var result = reader.Read(input);

            Assert.Equal(expected.Automaton.Step, result.Automaton.Step);
            Assert.Equal(expected.Boundary.GetType(), result.Boundary.GetType());
            Assert.Equal(expected.Neighbourhood.GetType(), result.Neighbourhood.GetType()); 
            Assert.Equal(expected.Transition.GetType(), result.Transition.GetType());
            Assert.Equal(expected.InclusionsCount, result.InclusionsCount );
            Assert.Equal(expected.GrainsCount, result.GrainsCount);
            Assert.Equal(expected.IsAutomatonGenerated, result.IsAutomatonGenerated);
            Assert.Equal(expected.IsSaved, result.IsSaved);
            Assert.Equal(expected.MaxRadius, result.MaxRadius);
            Assert.Equal(expected.MinRadius, result.MinRadius);
            Assert.Equal(expected.SpaceSize, result.SpaceSize);
            Assert.Equal(expected.Executor.GetType(), result.Executor.GetType());

            int i = 0;
            foreach(var grain in expected.Automaton.Grains)
            {
                var resultGrain = result.Automaton.Grains[i];
                Assert.Equal(grain.Id, resultGrain.Id);
                Assert.Equal(grain.Phase, resultGrain.Phase);
                Assert.Equal(grain.Color, resultGrain.Color);
                i++;
            }

            i = 0;
            foreach(var inclusion in expected.Automaton.Inclusions)
            {
                var resultInclusion = result.Automaton.Inclusions[i];
                Assert.Equal(inclusion.Id, resultInclusion.Id);
                Assert.Equal(inclusion.Phase, resultInclusion.Phase);
                Assert.Equal(inclusion.Color, resultInclusion.Color);
                Assert.Equal(inclusion.Radius, resultInclusion.Radius);
                i++;
            }

            var exSpace = expected.Automaton.Space;
            var reSpace = result.Automaton.Space;

            for(i = 0; i < exSpace.GetXLength(); i++)
            {
                for(int j = 0; j < exSpace.GetYLength(); j++)
                {
                    Microelement exElement = exSpace.GetCell(i, j).MicroelementMembership;
                    Microelement reElement = reSpace.GetCell(i, j).MicroelementMembership;
                    if(exElement?.Id == null)
                    {
                        Assert.Null(reElement?.Id);
                    }
                    else
                    {
                        Assert.Equal(exElement.Id, reElement.Id);
                        Assert.Equal(exElement.Phase, reElement.Phase);
                    }

                    
                }
            }


            
        }

        private ApplicationState BuildMockState()
        {
            int spaceSize = 3;
            int grainsCount = 2;
            int inclusionsCount = 2;
            int minRadius = 1;
            int maxRadius = 1;
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);
            ISimulationExecutor executor = new SimulationExecutor();

            var grains = new List<Grain>
            {
                new Grain(0, 0, Color.FromArgb(1, 2, 3, 4)), new Grain(1, 0, Color.FromArgb(5, 6, 7, 8))
            };
            var inclusions = new List<Inclusion>
            {
                new Inclusion(0, 1, 1, Color.FromArgb(1, 2, 3, 4)),
                new Inclusion(1, 1, 1, Color.FromArgb(5, 6, 7, 8))
            };

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

            return new ApplicationState(
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
        }

        private XDocument BuildMockInput()
        {
            string xmlState =
@"<Document>
  <WindowVariables SpaceSize=""3"" GrainsCount=""2"" InclusionsCount=""2"" MinRadius=""1"" MaxRadius=""1"" Transition=""Model.Transition.GrainGrowthRule"" Neighbourhood=""Model.Neighbourhood.VonNeumanNeighbourhood"" Boundary=""Model.Boundary.AbsorbingBoundary"" IsGenerated=""false"" IsSaved=""false"" Step=""0"" Executor=""Model.Executors.SimulationExecutor"" />
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

            return XDocument.Parse(xmlState);
        }



    }
}
