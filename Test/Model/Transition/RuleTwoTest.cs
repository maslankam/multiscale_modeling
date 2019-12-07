using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Model;
using Model.Transition;
using Xunit;

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
            Grain result = (Grain)rule.NextState(null, neighbours);
            Assert.Same(expected?.MicroelementMembership, result);
        }

        [Fact]
        public void FilledCellTest()
        {
            var neighbours = new[]{ b, c, a, d};
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
                yield return new object[] { a, new[] { a, a, a, null  } }; 
                yield return new object[] { a, new[] { null, a, a, a  } };
                yield return new object[] { a, new[] { a, a, null, a   } };  
                yield return new object[] { a, new[] { a, null, a, a   } };
                yield return new object[] { a, new[] { a, a, a, a } };
                yield return new object[] { a, new[] { a, b, a, a   } };
                yield return new object[] { null, new Cell[] { null, null, null, null } };
                yield return new object[] { null, new[] { a, b, a, b   } };
                yield return new object[] { null, new[] { a, null, a, null   } };
                yield return new object[] { null, new[] { a, null, null, a   } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


    }
}
