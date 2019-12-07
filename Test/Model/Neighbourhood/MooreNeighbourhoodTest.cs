using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Model;
using Xunit;

namespace Test
{
    public class MooreNeighbourhoodTest
    {
        private static readonly Cell A = new Cell(new Grain(0, 0, Color.Red));
        private static Cell b = new Cell(new Grain(1, 0, Color.Green));
        private static Cell c = new Cell(new Grain(2, 0, Color.Blue));
        private static Cell d = new Cell(new Grain(10, 0, Color.Yellow));
        private static Cell e = new Cell(new Grain(11, 0, Color.Violet));
        private static Cell f = new Cell(new Grain(12, 0, Color.Gold));
        private static Cell g = new Cell(new Grain(20, 0, Color.Coral));
        private static Cell h = new Cell(new Grain(21, 0, Color.Orange));
        private static Cell i = new Cell(new Grain(22, 0, Color.Azure));
        private static CelluralSpace _space;


        [Theory]
        [ClassData(typeof(AbsorbingTestData))]
        public static void AbsorbingTest(int x, int y, Cell[] expected)
        {
            _space = new CelluralSpace(3);

            _space.SetCellMembership(A.MicroelementMembership, 0, 0);
            _space.SetCellMembership(b.MicroelementMembership, 0, 1);
            _space.SetCellMembership(c.MicroelementMembership, 0, 2);
            _space.SetCellMembership(d.MicroelementMembership, 1, 0);
            _space.SetCellMembership(e.MicroelementMembership, 1, 1);
            _space.SetCellMembership(f.MicroelementMembership, 1, 2);
            _space.SetCellMembership(g.MicroelementMembership, 2, 0);
            _space.SetCellMembership(h.MicroelementMembership, 2, 1);
            _space.SetCellMembership(i.MicroelementMembership, 2, 2);

            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new MooreNeighbourhood(boundary);

            var neighbours = neighbourhood.GetNeighbours(_space, x, y);

            for (int index = 0; index < 8; index++)
            {
                if (expected[index] == null)
                {
                    Assert.Null(neighbours[index]);
                }
                else
                {
                    Assert.Same(expected[index].MicroelementMembership, neighbours[index].MicroelementMembership);
                }
            }

        }

        [Theory]
        [ClassData(typeof(PeriodicTestData))]
        public static void PeriodicTest(int x, int y, Cell[] expected)
        {
            _space = new CelluralSpace(3);

            _space.SetCellMembership(A.MicroelementMembership, 0, 0);
            _space.SetCellMembership(b.MicroelementMembership, 0, 1);
            _space.SetCellMembership(c.MicroelementMembership, 0, 2);
            _space.SetCellMembership(d.MicroelementMembership, 1, 0);
            _space.SetCellMembership(e.MicroelementMembership, 1, 1);
            _space.SetCellMembership(f.MicroelementMembership, 1, 2);
            _space.SetCellMembership(g.MicroelementMembership, 2, 0);
            _space.SetCellMembership(h.MicroelementMembership, 2, 1);
            _space.SetCellMembership(i.MicroelementMembership, 2, 2);

            IBoundaryCondition boundary = new PeriodicBoundary();
            INeighbourhood neighbourhood = new MooreNeighbourhood(boundary);

            var neighbours = neighbourhood.GetNeighbours(_space, x, y);

            for (int index = 0; index < 8; index++)
            {
                if (expected[index] == null)
                {
                    Assert.Null(neighbours[index]);
                }
                else
                {
                    Assert.Same(expected[index].MicroelementMembership, neighbours[index].MicroelementMembership);
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
                yield return new object[] { 0, 0, new[] { null, null, b   ,e   , d   , null, null,   null } }; //a
                yield return new object[] { 0, 1, new[] { null, null, c   ,f   , e   , d   , A   ,   null } }; //b
                yield return new object[] { 0, 2, new[] { null, null, null,null, f   , e   , b   ,   null } }; //c
                yield return new object[] { 1, 0, new[] { A   , b   ,    e,h   , g   , null, null,   null } }; //d
                yield return new object[] { 1, 1, new[] { b   , c   ,    f,i   , h   , g   , d   ,   A    } }; //e - center
                yield return new object[] { 1, 2, new[] { c   , null, null,null, i   , h   , e   ,   b    } }; //f
                yield return new object[] { 2, 0, new[] { d   , e   ,    h,null, null, null, null,   null } }; //g
                yield return new object[] { 2, 1, new[] { e   , f   ,    i,null, null, null, g   ,   d    } }; //h
                yield return new object[] { 2, 2, new[] { f   , null, null,null, null, null, h   ,   e    } }; //i
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
                yield return new object[] { 0, 0, new[] { g,h, b,e, d,f, c,i  } }; //a
                yield return new object[] { 0, 1, new[] { h,i, c,f, e,d, A,g  } }; //b
                yield return new object[] { 0, 2, new[] { i,g, A,d, f,e, b,h  } }; //c
                yield return new object[] { 1, 0, new[] { A,b, e,h, g,i, f,c  } }; //d
                yield return new object[] { 1, 1, new[] { b,c, f,i, h,g, d,A  } }; //e - center
                yield return new object[] { 1, 2, new[] { c,A, d,g, i,h, e,b  } }; //f
                yield return new object[] { 2, 0, new[] { d,e, h,b, A,c, i,f  } }; //g
                yield return new object[] { 2, 1, new[] { e,f, i,c, b,A, g,d  } }; //h
                yield return new object[] { 2, 2, new[] { f,d, g,A, c,b, h,e  } }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}
