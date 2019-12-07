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
        private static Cell _a = new Cell(new Grain(0, 0, Color.Red) );
        private static Cell _b = new Cell(new Grain(1, 1, Color.Green) );
        private static Cell _c = new Cell(new Grain(2, 2, Color.Blue) );
        private static Cell _d = new Cell(new Grain(10, 10, Color.Yellow) );
        private static Cell _e = new Cell(new Grain(11, 11, Color.Violet) );
        private static Cell _f = new Cell(new Grain(12, 12, Color.Gold) );
        private static Cell _g = new Cell(new Grain(20, 20, Color.Coral) );
        private static Cell _h = new Cell(new Grain(21, 21, Color.Orange) );
        private static Cell _ = new Cell(new Grain(22, 22, Color.Azure) );
        private static CelluralSpace _space;
        
       
        [Theory]
        [ClassData(typeof(AbsorbingTestData))]
        public static void AbsorbingTest(int x, int y, Cell[] expected)
        {
            _space = new CelluralSpace(3);

            _space.SetCellMembership(_a.MicroelementMembership, 0, 0);
            _space.SetCellMembership(_b.MicroelementMembership, 0, 1);
            _space.SetCellMembership(_c.MicroelementMembership, 0, 2);
            _space.SetCellMembership(_d.MicroelementMembership, 1, 0);
            _space.SetCellMembership(_e.MicroelementMembership, 1, 1);
            _space.SetCellMembership(_f.MicroelementMembership, 1, 2);
            _space.SetCellMembership(_g.MicroelementMembership, 2, 0);
            _space.SetCellMembership(_h.MicroelementMembership, 2, 1);
            _space.SetCellMembership(_.MicroelementMembership, 2, 2);

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

            _space.SetCellMembership(_a.MicroelementMembership, 0, 0);
            _space.SetCellMembership(_b.MicroelementMembership, 0, 1);
            _space.SetCellMembership(_c.MicroelementMembership, 0, 2);
            _space.SetCellMembership(_d.MicroelementMembership, 1, 0);
            _space.SetCellMembership(_e.MicroelementMembership, 1, 1);
            _space.SetCellMembership(_f.MicroelementMembership, 1, 2);
            _space.SetCellMembership(_g.MicroelementMembership, 2, 0);
            _space.SetCellMembership(_h.MicroelementMembership, 2, 1);
            _space.SetCellMembership(_.MicroelementMembership, 2, 2);

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
                yield return new object[] { 0, 0, new[]{null, _b, _d, null} }; //a
                yield return new object[] { 0, 1, new[]{null,_c, _e, _a} }; //b
                yield return new object[] { 0, 2, new[]{null, null, _f, _b} }; //c
                yield return new object[] { 1, 0, new[]{_a, _e, _g, null} }; //d
                yield return new object[] { 1, 1, new[] { _b, _f, _h, _d } }; //e - center
                yield return new object[] { 1, 2, new[]{_c, null, _, _e} }; //f
                yield return new object[] { 2, 0, new[]{_d, _h, null, null} }; //g
                yield return new object[] { 2, 1, new[]{_e, _, null, _g} }; //h
                yield return new object[] { 2, 2, new[]{_f,null, null, _h} }; //i
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
                yield return new object[] { 0, 0, new[]{_g, _b, _d, _c} }; //a
                yield return new object[] { 0, 1, new[]{_h,_c, _e, _a} }; //b
                yield return new object[] { 0, 2, new[]{_, _a, _f, _b} }; //c
                yield return new object[] { 1, 0, new[]{_a, _e, _g, _f} }; //d
                yield return new object[] { 1, 1, new[] { _b, _f, _h, _d } }; //e - center
                yield return new object[] { 1, 2, new[]{_c, _d, _, _e} }; //f
                yield return new object[] { 2, 0, new[]{_d, _h, _a, _} }; //g
                yield return new object[] { 2, 1, new[]{_e, _, _b, _g} }; //h
                yield return new object[] { 2, 2, new[]{_f, _g, _c, _h} }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
    }
}
