using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Model;
using Model.Boundary;
using Model.Microelements;
using Model.Neighbourhood;
using Xunit;

namespace Test.Model.Neighbourhood
{
    public class VonNeumanNeighbourhoodTest
    {
        private static Cell A = new Cell(new Grain(0, 0, Color.Red) );
        private static Cell B = new Cell(new Grain(1, 1, Color.Green) );
        private static Cell C = new Cell(new Grain(2, 2, Color.Blue) );
        private static Cell D = new Cell(new Grain(10, 10, Color.Yellow) );
        private static Cell E = new Cell(new Grain(11, 11, Color.Violet) );
        private static Cell F = new Cell(new Grain(12, 12, Color.Gold) );
        private static Cell G = new Cell(new Grain(20, 20, Color.Coral) );
        private static Cell H = new Cell(new Grain(21, 21, Color.Orange) );
        private static Cell I = new Cell(new Grain(22, 22, Color.Azure) );
        private static CelluralSpace _space;
        
       
        [Theory]
        [ClassData(typeof(AbsorbingTestData))]
        public static void AbsorbingTest(int x, int y, Cell[] expected)
        {
            _space = new CelluralSpace(3);

            _space.SetCellMembership(A.MicroelementMembership, 0, 0);
            _space.SetCellMembership(B.MicroelementMembership, 0, 1);
            _space.SetCellMembership(C.MicroelementMembership, 0, 2);
            _space.SetCellMembership(D.MicroelementMembership, 1, 0);
            _space.SetCellMembership(E.MicroelementMembership, 1, 1);
            _space.SetCellMembership(F.MicroelementMembership, 1, 2);
            _space.SetCellMembership(G.MicroelementMembership, 2, 0);
            _space.SetCellMembership(H.MicroelementMembership, 2, 1);
            _space.SetCellMembership(I.MicroelementMembership, 2, 2);

            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            var neighbours = neighbourhood.GetNeighbours(_space, x, y);
            
            for(int i = 0; i < 4; i++)
            {
                if(expected[i] == null)
                {
                    Assert.Null(neighbours[i]);
                }
                else
                {
                    Assert.Same(expected[i].MicroelementMembership , neighbours[i].MicroelementMembership);
                }
            }
 
        }

        [Theory]
        [ClassData(typeof(PeriodicTestData))]
        public static void PeriodicTest(int x, int y, Cell[] expected)
        {
            _space = new CelluralSpace(3);

            _space.SetCellMembership(A.MicroelementMembership, 0, 0);
            _space.SetCellMembership(B.MicroelementMembership, 0, 1);
            _space.SetCellMembership(C.MicroelementMembership, 0, 2);
            _space.SetCellMembership(D.MicroelementMembership, 1, 0);
            _space.SetCellMembership(E.MicroelementMembership, 1, 1);
            _space.SetCellMembership(F.MicroelementMembership, 1, 2);
            _space.SetCellMembership(G.MicroelementMembership, 2, 0);
            _space.SetCellMembership(H.MicroelementMembership, 2, 1);
            _space.SetCellMembership(I.MicroelementMembership, 2, 2);

            IBoundaryCondition boundary = new PeriodicBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            var neighbours = neighbourhood.GetNeighbours(_space, x, y);
            
            for(int i = 0; i < 4; i++)
            {
                if(expected?[i]?.MicroelementMembership == null)
                {
                    Assert.Null(neighbours[i].MicroelementMembership);
                }
                else
                {
                    Assert.False(neighbours?[i]?.MicroelementMembership == null);
                    Assert.Same(expected[i].MicroelementMembership , neighbours[i].MicroelementMembership);
                }
            }
 
        }


        private class AbsorbingTestData : IEnumerable<object[]>
        {   ///x\y 0 1 2
            /// 0 |a|b|c|
            /// 1 |d|e|f|
            /// 2 |g|h|i|
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0, 0, new[]{null, B, D, null} }; //a
                yield return new object[] { 0, 1, new[]{null,C, E, A} }; //b
                yield return new object[] { 0, 2, new[]{null, null, F, B} }; //c
                yield return new object[] { 1, 0, new[]{A, E, G, null} }; //d
                yield return new object[] { 1, 1, new[] { B, F, H, D } }; //e - center
                yield return new object[] { 1, 2, new[]{C, null, I, E} }; //f
                yield return new object[] { 2, 0, new[]{D, H, null, null} }; //g
                yield return new object[] { 2, 1, new[]{E, I, null, G} }; //h
                yield return new object[] { 2, 2, new[]{F,null, null, H} }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

            private class PeriodicTestData : IEnumerable<object[]>
            {   ///x\y 0 1 2
                /// 0 |a|b|c|
                /// 1 |d|e|f|
                /// 2 |g|h|i|
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0, 0, new[]{G, B, D, C} }; //a
                yield return new object[] { 0, 1, new[]{H,C, E, A} }; //b
                yield return new object[] { 0, 2, new[]{I, A, F, B} }; //c
                yield return new object[] { 1, 0, new[]{A, E, G, F} }; //d
                yield return new object[] { 1, 1, new[] { B, F, H, D } }; //e - center
                yield return new object[] { 1, 2, new[]{C, D, I, E} }; //f
                yield return new object[] { 2, 0, new[]{D, H, A, I} }; //g
                yield return new object[] { 2, 1, new[]{E, I, B, G} }; //h
                yield return new object[] { 2, 2, new[]{F, G, C, H} }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
    }
}
