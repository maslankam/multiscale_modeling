using System.Collections;
using System.Collections.Generic;
using Xunit;
using System.Drawing;

using Model;

namespace Test
{
    public class MooreNeighbourhoodTest
    {
        public static Cell a = new Cell(new Grain(0, 0, Color.Red));
        public static Cell b = new Cell(new Grain(1, 0, Color.Green));
        public static Cell c = new Cell(new Grain(2, 0, Color.Blue));
        public static Cell d = new Cell(new Grain(10, 0, Color.Yellow));
        public static Cell e = new Cell(new Grain(11, 0, Color.Violet));
        public static Cell f = new Cell(new Grain(12, 0, Color.Gold));
        public static Cell g = new Cell(new Grain(20, 0, Color.Coral));
        public static Cell h = new Cell(new Grain(21, 0, Color.Orange));
        public static Cell i = new Cell(new Grain(22, 0, Color.Azure));
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
            INeighbourhood neighbourhood = new MooreNeighbourhood(boundary);

            var neighbours = neighbourhood.GetNeighbours(space, x, y);

            for (int i = 0; i < 8; i++)
            {
                if (expected[i] == null)
                {
                    Assert.Null(neighbours[i]);
                }
                else
                {
                    Assert.Same(expected[i].MicroelementMembership, neighbours[i].MicroelementMembership);
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
            INeighbourhood neighbourhood = new MooreNeighbourhood(boundary);

            var neighbours = neighbourhood.GetNeighbours(space, x, y);

            for (int i = 0; i < 8; i++)
            {
                if (expected[i] == null)
                {
                    Assert.Null(neighbours[i]);
                }
                else
                {
                    Assert.Same(expected[i].MicroelementMembership, neighbours[i].MicroelementMembership);
                }
            }

        }


        private class AbsorbingTestData : IEnumerable<object[]>
        {   ///x\y 0 1 2
            /// 0 |a|b|c|
            /// 1 |d|e|f|
            /// 2 |g|h|i|
            public IEnumerator<object[]> GetEnumerator()
            {                                                   //N  NE   E     SE   S     SW     W      NW    
                yield return new object[] { 0, 0, new Cell[] { null, null, b   ,e   , d   , null, null,   null } }; //a
                yield return new object[] { 0, 1, new Cell[] { null, null, c   ,f   , e   , d   , a   ,   null } }; //b
                yield return new object[] { 0, 2, new Cell[] { null, null, null,null, f   , e   , b   ,   null } }; //c
                yield return new object[] { 1, 0, new Cell[] { a   , b   ,    e,h   , g   , null, null,   null } }; //d
                yield return new object[] { 1, 1, new Cell[] { b   , c   ,    f,i   , h   , g   , d   ,   a    } }; //e - center
                yield return new object[] { 1, 2, new Cell[] { c   , null, null,null, i   , h   , e   ,   b    } }; //f
                yield return new object[] { 2, 0, new Cell[] { d   , e   ,    h,null, null, null, null,   null } }; //g
                yield return new object[] { 2, 1, new Cell[] { e   , f   ,    i,null, null, null, g   ,   d    } }; //h
                yield return new object[] { 2, 2, new Cell[] { f   , null, null,null, null, null, h   ,   e    } }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class PeriodicTestData : IEnumerable<object[]>
        {   ///x\y 0 1 2
            /// 0 |a|b|c|
            /// 1 |d|e|f|
            /// 2 |g|h|i|
            public IEnumerator<object[]> GetEnumerator()
            {                                                //N NE E SE S SW W NW                 
                yield return new object[] { 0, 0, new Cell[] { g,h, b,e, d,f, c,i  } }; //a
                yield return new object[] { 0, 1, new Cell[] { h,i, c,f, e,d, a,g  } }; //b
                yield return new object[] { 0, 2, new Cell[] { i,g, a,d, f,e, b,h  } }; //c
                yield return new object[] { 1, 0, new Cell[] { a,b, e,h, g,i, f,c  } }; //d
                yield return new object[] { 1, 1, new Cell[] { b,c, f,i, h,g, d,a  } }; //e - center
                yield return new object[] { 1, 2, new Cell[] { c,a, d,g, i,h, e,b  } }; //f
                yield return new object[] { 2, 0, new Cell[] { d,e, h,b, a,c, i,f  } }; //g
                yield return new object[] { 2, 1, new Cell[] { e,f, i,c, b,a, g,d  } }; //h
                yield return new object[] { 2, 2, new Cell[] { f,d, g,a, c,b, h,e  } }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}
