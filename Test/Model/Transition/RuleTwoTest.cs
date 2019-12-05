using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Xunit;
using Model;
using System.Drawing;
using Model.Transition;

namespace Test.Model.Transition
{
    public class RuleTwoTest
    {
        private static Cell a = new Cell(new Grain(0,0, Color.Blue));
        private static Cell b = new Cell(new Grain(1,0, Color.Blue));
        private static Cell c = new Cell(new Grain(2,0, Color.Blue));
        private static Cell d = new Cell(new Grain(3,0, Color.Blue));
    
        [Theory]
        [ClassData(typeof(RuleOneTestData))]
        public void EmptyCellTest(Cell expected, Cell[] neighbours)
        {
            ITransitionRule rule = new RuleTwo();
            Cell cell = null;
            Grain result = (Grain)rule.NextState(cell, neighbours);
            Assert.Same(expected?.MicroelementMembership, result);
        }

        [Fact]
        public void FilledCellTest()
        {
            var neighbours = new Cell[]{ b, c, a, d};
            ITransitionRule rule = new RuleTwo();
            var cell = a;
            Assert.Same(a.MicroelementMembership, rule.NextState(cell, neighbours));
        }


        private class RuleOneTestData : IEnumerable<object[]>
        {   ///x\y 0 1 2
            /// 0 |a|b|c|
            /// 1 |d|e|f|
            /// 2 |g|h|i|
            public IEnumerator<object[]> GetEnumerator()
            {                                                  
                yield return new object[] { a, new Cell[] { a, a, a, null  } }; 
                yield return new object[] { a, new Cell[] { null, a, a, a  } };
                yield return new object[] { a, new Cell[] { a, a, null, a   } };  
                yield return new object[] { a, new Cell[] { a, null, a, a   } };
                yield return new object[] { a, new Cell[] { a, a, a, a } };
                yield return new object[] { a, new Cell[] { a, b, a, a   } };
                yield return new object[] { null, new Cell[] { null, null, null, null } };
                yield return new object[] { null, new Cell[] { a, b, a, b   } };
                yield return new object[] { null, new Cell[] { a, null, a, null   } };
                yield return new object[] { null, new Cell[] { a, null, null, a   } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


    }
}
