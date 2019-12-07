using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using System.Drawing;

using Model;

namespace Test
{
    public class VonNeumanNeighbourhoodTest
    {
        public static Cell a = new Cell(new Grain(0, 0, Color.Red) );
        public static Cell b = new Cell(new Grain(1, 1, Color.Green) );
        public static Cell c = new Cell(new Grain(2, 2, Color.Blue) );
        public static Cell d = new Cell(new Grain(10, 10, Color.Yellow) );
        public static Cell e = new Cell(new Grain(11, 11, Color.Violet) );
        public static Cell f = new Cell(new Grain(12, 12, Color.Gold) );
        public static Cell g = new Cell(new Grain(20, 20, Color.Coral) );
        public static Cell h = new Cell(new Grain(21, 21, Color.Orange) );
        public static Cell i = new Cell(new Grain(22, 22, Color.Azure) );
        public static CelluralSpace space;
        
       
        [Theory]
        [ClassData(typeof(AbsorbingTestData))]
        public static void AbsorbingTest(int x, int y, Cell[] expected)
        {
            space = new CelluralSpace(3);

            space.SetCellMembership(a.MicroelementMembership, 0, 0);
            space.SetCellMembership(b.MicroelementMembership, 0, 1);
            space.SetCellMembership(c.MicroelementMembership, 0, 2);
            space.SetCellMembership(d.MicroelementMembership, 1, 0);
            space.SetCellMembership(e.MicroelementMembership, 1, 1);
            space.SetCellMembership(f.MicroelementMembership, 1, 2);
            space.SetCellMembership(g.MicroelementMembership, 2, 0);
            space.SetCellMembership(h.MicroelementMembership, 2, 1);
            space.SetCellMembership(i.MicroelementMembership, 2, 2);

            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            var neighbours = neighbourhood.GetNeighbours(space, x, y);
            
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
            space = new CelluralSpace(3);

            space.SetCellMembership(a.MicroelementMembership, 0, 0);
            space.SetCellMembership(b.MicroelementMembership, 0, 1);
            space.SetCellMembership(c.MicroelementMembership, 0, 2);
            space.SetCellMembership(d.MicroelementMembership, 1, 0);
            space.SetCellMembership(e.MicroelementMembership, 1, 1);
            space.SetCellMembership(f.MicroelementMembership, 1, 2);
            space.SetCellMembership(g.MicroelementMembership, 2, 0);
            space.SetCellMembership(h.MicroelementMembership, 2, 1);
            space.SetCellMembership(i.MicroelementMembership, 2, 2);

            IBoundaryCondition boundary = new PeriodicBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            var neighbours = neighbourhood.GetNeighbours(space, x, y);
            
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
                yield return new object[] { 0, 0, new Cell[]{null, b, d, null} }; //a
                yield return new object[] { 0, 1, new Cell[]{null,c, e, a} }; //b
                yield return new object[] { 0, 2, new Cell[]{null, null, f, b} }; //c
                yield return new object[] { 1, 0, new Cell[]{a, e, g, null} }; //d
                yield return new object[] { 1, 1, new Cell[] { b, f, h, d } }; //e - center
                yield return new object[] { 1, 2, new Cell[]{c, null, i, e} }; //f
                yield return new object[] { 2, 0, new Cell[]{d, h, null, null} }; //g
                yield return new object[] { 2, 1, new Cell[]{e, i, null, g} }; //h
                yield return new object[] { 2, 2, new Cell[]{f,null, null, h} }; //i
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
                yield return new object[] { 0, 0, new Cell[]{g, b, d, c} }; //a
                yield return new object[] { 0, 1, new Cell[]{h,c, e, a} }; //b
                yield return new object[] { 0, 2, new Cell[]{i, a, f, b} }; //c
                yield return new object[] { 1, 0, new Cell[]{a, e, g, f} }; //d
                yield return new object[] { 1, 1, new Cell[] { b, f, h, d } }; //e - center
                yield return new object[] { 1, 2, new Cell[]{c, d, i, e} }; //f
                yield return new object[] { 2, 0, new Cell[]{d, h, a, i} }; //g
                yield return new object[] { 2, 1, new Cell[]{e, i, b, g} }; //h
                yield return new object[] { 2, 2, new Cell[]{f, g, c, h} }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }
    }
}
