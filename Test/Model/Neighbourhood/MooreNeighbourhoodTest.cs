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
    public static class MooreNeighbourhoodTest
    {
        private static Cell A = new Cell(new Grain(0, 0, Color.Red));
        private static Cell _b = new Cell(new Grain(1, 0, Color.Green));
        private static Cell _c = new Cell(new Grain(2, 0, Color.Blue));
        private static Cell _d = new Cell(new Grain(10, 0, Color.Yellow));
        private static Cell _e = new Cell(new Grain(11, 0, Color.Violet));
        private static Cell _f = new Cell(new Grain(12, 0, Color.Gold));
        private static Cell _g = new Cell(new Grain(20, 0, Color.Coral));
        private static Cell _h = new Cell(new Grain(21, 0, Color.Orange));
        private static Cell _i = new Cell(new Grain(22, 0, Color.Azure));
        private static CelluralSpace _space;


        [Theory]
        [ClassData(typeof(AbsorbingTestData))]
        public static void AbsorbingTest(int x, int y, Cell[] expected)
        {
            _space = new CelluralSpace(3);

            _space.SetCellMembership(A.MicroelementMembership, 0, 0);
            _space.SetCellMembership(_b.MicroelementMembership, 0, 1);
            _space.SetCellMembership(_c.MicroelementMembership, 0, 2);
            _space.SetCellMembership(_d.MicroelementMembership, 1, 0);
            _space.SetCellMembership(_e.MicroelementMembership, 1, 1);
            _space.SetCellMembership(_f.MicroelementMembership, 1, 2);
            _space.SetCellMembership(_g.MicroelementMembership, 2, 0);
            _space.SetCellMembership(_h.MicroelementMembership, 2, 1);
            _space.SetCellMembership(_i.MicroelementMembership, 2, 2);

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
            _space.SetCellMembership(_b.MicroelementMembership, 0, 1);
            _space.SetCellMembership(_c.MicroelementMembership, 0, 2);
            _space.SetCellMembership(_d.MicroelementMembership, 1, 0);
            _space.SetCellMembership(_e.MicroelementMembership, 1, 1);
            _space.SetCellMembership(_f.MicroelementMembership, 1, 2);
            _space.SetCellMembership(_g.MicroelementMembership, 2, 0);
            _space.SetCellMembership(_h.MicroelementMembership, 2, 1);
            _space.SetCellMembership(_i.MicroelementMembership, 2, 2);

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
                yield return new object[] { 0, 0, new[] { null, null, _b   ,_e   , _d   , null, null,   null } }; //a
                yield return new object[] { 0, 1, new[] { null, null, _c   ,_f   , _e   , _d   , A   ,   null } }; //b
                yield return new object[] { 0, 2, new[] { null, null, null,null, _f   , _e   , _b   ,   null } }; //c
                yield return new object[] { 1, 0, new[] { A   , _b   ,    _e,_h   , _g   , null, null,   null } }; //d
                yield return new object[] { 1, 1, new[] { _b   , _c   ,    _f,_i   , _h   , _g   , _d   ,   A    } }; //e - center
                yield return new object[] { 1, 2, new[] { _c   , null, null,null, _i   , _h   , _e   ,   _b    } }; //f
                yield return new object[] { 2, 0, new[] { _d   , _e   ,    _h,null, null, null, null,   null } }; //g
                yield return new object[] { 2, 1, new[] { _e   , _f   ,    _i,null, null, null, _g   ,   _d    } }; //h
                yield return new object[] { 2, 2, new[] { _f   , null, null,null, null, null, _h   ,   _e    } }; //i
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
                yield return new object[] { 0, 0, new[] { _g,_h, _b,_e, _d,_f, _c,_i  } }; //a
                yield return new object[] { 0, 1, new[] { _h,_i, _c,_f, _e,_d, A,_g  } }; //b
                yield return new object[] { 0, 2, new[] { _i,_g, A,_d, _f,_e, _b,_h  } }; //c
                yield return new object[] { 1, 0, new[] { A,_b, _e,_h, _g,_i, _f,_c  } }; //d
                yield return new object[] { 1, 1, new[] { _b,_c, _f,_i, _h,_g, _d,A  } }; //e - center
                yield return new object[] { 1, 2, new[] { _c,A, _d,_g, _i,_h, _e,_b  } }; //f
                yield return new object[] { 2, 0, new[] { _d,_e, _h,_b, A,_c, _i,_f  } }; //g
                yield return new object[] { 2, 1, new[] { _e,_f, _i,_c, _b,A, _g,_d  } }; //h
                yield return new object[] { 2, 2, new[] { _f,_d, _g,A, _c,_b, _h,_e  } }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}
