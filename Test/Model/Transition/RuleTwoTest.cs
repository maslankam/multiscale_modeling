using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Model;
using Model.Microelements;
using Model.Transition;
using Xunit;

namespace Test.Model.Transition
{
    public class RuleTwoTest
    {
        private static Cell _a = new Cell(new Grain(0,0, Color.Blue));
        private static Cell _b = new Cell(new Grain(1,0, Color.Blue));
        private static Cell _c = new Cell(new Grain(2,0, Color.Blue));
        private static Cell _d = new Cell(new Grain(3,0, Color.Blue));
    
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
            var neighbours = new[]{ _b, _c, _a, _d};
            ITransitionRule rule = new RuleTwo();
            var cell = _a;
            Assert.Same(_a.MicroelementMembership, rule.NextState(cell, neighbours));
        }


        private class RuleOneTestData : IEnumerable<object[]>
        {   ///x\y 0 1 2
            /// 0 |a|b|c|
            /// 1 |d|e|f|
            /// 2 |g|h|i|
            public IEnumerator<object[]> GetEnumerator()
            {                                                  
                yield return new object[] { _a, new[] { _a, _a, _a, null  } }; 
                yield return new object[] { _a, new[] { null, _a, _a, _a  } };
                yield return new object[] { _a, new[] { _a, _a, null, _a   } };  
                yield return new object[] { _a, new[] { _a, null, _a, _a   } };
                yield return new object[] { _a, new[] { _a, _a, _a, _a } };
                yield return new object[] { _a, new[] { _a, _b, _a, _a   } };
                yield return new object[] { null, new Cell[] { null, null, null, null } };
                yield return new object[] { null, new[] { _a, _b, _a, _b   } };
                yield return new object[] { null, new[] { _a, null, _a, null   } };
                yield return new object[] { null, new[] { _a, null, null, _a   } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


    }
}
