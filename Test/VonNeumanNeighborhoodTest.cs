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
        public static Cell a = new Cell { GrainMembership = new Grain(0, Color.Red) };
        public static Cell b = new Cell{ GrainMembership = new Grain(1, Color.Green) };
        public static Cell c = new Cell{ GrainMembership = new Grain(2, Color.Blue) };
        public static Cell d = new Cell{ GrainMembership = new Grain(10, Color.Yellow) };
        public static Cell e = new Cell{ GrainMembership = new Grain(11, Color.Violet) };
        public static Cell f = new Cell{ GrainMembership = new Grain(12, Color.Gold) };
        public static Cell g = new Cell{ GrainMembership = new Grain(20, Color.Coral) };
        public static Cell h = new Cell{ GrainMembership = new Grain(21, Color.Orange) };
        public static Cell i = new Cell{ GrainMembership = new Grain(22, Color.Azure) };   
        public static Cell[,] space = new Cell[3, 3]
            {
                { a, b, c },
                { d, e, f },
                { g, h, i }
            };


        [Theory]
        [ClassData(typeof(AbsorbingTestData))]
        public static void AbsorbingTest(int x, int y, Cell[] expected)
        {
            var neighbours = VonNeumanNeighborhood.Neighbours(space , x, y, AbsorbingBoundary.BoundaryCondition);
            
            for(int i = 0; i < 4; i++)
            {
                if(expected[i] == null)
                {
                    Assert.Null(neighbours[i]);
                }
                else
                {
                    Assert.Same(neighbours[i], expected[i]);
                }
            }
 
        }

        public static IEnumerable<object[]> GetNumbers()
        {
            yield return new object[] { 1, 1, new Cell[] { b, f, h, d } }; //e - center
            yield return new object[] { 0, 0, new Cell[] { null, b, d, null } }; //a
        }



        /*public static IEnumerable<object[]> SplitCountData
        {
            get
            {
                // Or this could read from a file. :)
                return new[]
                {
                new object[] { 1, 1, new Cell[]{b, f, h, d} }, //e - center
                new object[] { 0, 0, new Cell[] { null, b, d, null } }, //a
                new object[] { 0, 1, new Cell[] { null, c, e, a } }, //b
                new object[] { 0, 2, new Cell[] { null, null, f, b } }, //c
                new object[] { 1, 0, new Cell[] { a, e, g, null } }, //d
                new object[] { 1, 2, new Cell[] { c, null, i, e } }, //f
                new object[] { 2, 0, new Cell[] { d, h, null, null } }, //g
                new object[] { 2, 1, new Cell[] { e, i, null, g } }, //h
                new object[] { 2, 2, new Cell[] { f, null, null, h } } //i
            };
            }
        }*/

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

        

    }
}
