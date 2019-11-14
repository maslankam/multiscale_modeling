using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

using Model;

namespace Test
{
    public class VonNeumanNeighborhoodTest
    {
        private static Cell a,b,c,d,e,f,g,h,i ;    
        private static Cell[,] space = {
                {a, b, c },
                {c, d, e },
                {f, g, h }
            };

        public VonNeumanNeighborhoodTest(){
            a = new Cell();
            b = new Cell();
            c = new Cell();
            d = new Cell();
            e = new Cell();
            f = new Cell();
            g = new Cell();
            h = new Cell();
            i = new Cell();
        }


        [Theory]
        [ClassData(typeof(AbsorbingTestData))]
        public void AbsorbingTest(int x, int y, Cell[] expected)
        {
            var neighbours = VonNeumanNeighborhood.Neighbours(space , x, y, AbsorbingBoundary.BoundaryCondition);

            Assert.Equal(neighbours, expected);
        }

        private class AbsorbingTestData : IEnumerable<object[]>
        {   ///    0 1 2
            /// 0 |a|b|c|
            /// 1 |d|e|f|
            /// 2 |g|h|i|
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 1, 1, new Cell[]{b, f, h, d} }; //e - center
                yield return new object[] { 0, 0, new Cell[]{null, b, d, null} }; //a
                yield return new object[] { 0, 1, new Cell[]{null,c, e, a} }; //b
                yield return new object[] { 0, 2, new Cell[]{null, null, f, b} }; //c
                yield return new object[] { 1, 0, new Cell[]{a, e, g, null} }; //d
                yield return new object[] { 1, 2, new Cell[]{c, null, i, e} }; //f
                yield return new object[] { 2, 0, new Cell[]{d, h, null, null} }; //g
                yield return new object[] { 2, 1, new Cell[]{e, i, null, g} }; //h
                yield return new object[] { 2, 2, new Cell[]{f,null, null, h} }; //i
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }

    }
}
