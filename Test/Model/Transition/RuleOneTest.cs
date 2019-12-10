using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Model;
using Model.Microelements;
using Model.Transition;
using Xunit;

namespace Test.Model.Transition
{
    public class RuleOneTest
    {
        private static Cell _a = new Cell(new Grain(0,0, Color.Blue));
        private static Cell _b = new Cell(new Grain(1,0, Color.Blue));
        private static Cell _c = new Cell(new Grain(2,0, Color.Blue));
        private static Cell _d = new Cell(new Grain(3,0, Color.Blue));
        private static Cell _e = new Cell(new Grain(4,0, Color.Blue));
        private static Cell _f = new Cell(new Grain(5,0, Color.Blue));
        private static Cell _g = new Cell(new Grain(6,0, Color.Blue));


        [Theory]
        [ClassData(typeof(RuleOneTestData))]
        public void EmptyCellTest(Cell expected, Cell[] neighbours)
        {
            ITransitionRule rule = new RuleOne();
            var cell = new Cell();
            Microelement result = rule.NextState(cell, neighbours);
            Assert.Same(expected?.MicroelementMembership, result);
        }

        [Fact]
        public void FilledCellTest()
        {
            var neighbours = new[]{ _b, _c, _a, _a, _a, _a, _a, _a };
            ITransitionRule rule = new RuleOne();
            var cell = _c;
            Assert.Same(_c.MicroelementMembership, rule.NextState(cell, neighbours));
        }


        private class RuleOneTestData : IEnumerable<object[]>
        {   ///x\y 0 1 2
            /// 0 |a|b|c|
            /// 1 |d|e|f|
            /// 2 |g|h|i|
            public IEnumerator<object[]> GetEnumerator()
            {                                                  
                yield return new object[] { _a, new[] { _a, _a, _a, _a, _a, null, null, null  } }; 
                yield return new object[] { _a, new[] { null, _a, _a, _a, _a, _a, null, null  } }; 
                yield return new object[] { _a, new[] { null, null, _a, _a, _a, _a, _a, null  } }; 
                yield return new object[] { _a, new[] { null, null, null, _a, _a, _a, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, null, null, null, _a, _a, _a, _a  } };
                yield return new object[] { _a, new[] { _a, _a, null, null, null, _a, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, _a, _a, null, null, null, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, _a, _a, _a, null, null, null, _a  } }; 

                yield return new object[] { _a, new[] { _a, _a, _a, _a, _a, _a, null, null  } }; 
                yield return new object[] { _a, new[] { null, _a, _a, _a, _a, _a, _a, null  } }; 
                yield return new object[] { _a, new[] { null, null, _a, _a, _a, _a, _a, _a  } };
                yield return new object[] { _a, new[] { _a,null, null,  _a, _a, _a, _a, _a  } };
                yield return new object[] { _a, new[] { _a, _a,null, null, _a, _a, _a, _a } };   
                yield return new object[] { _a, new[] { _a, _a, _a, null, null , _a, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, _a, _a, _a, null , null, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, _a, _a, _a, _a , null, null, _a  } }; 

                yield return new object[] { _a, new[] { _a, _a, _a, _a, _a, _a, _a, null  } }; 
                yield return new object[] { _a, new[] { null, _a, _a, _a, _a, _a, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, null, _a, _a, _a, _a, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, _a, _a, null, _a, _a, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, _a, _a, _a, null, _a, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, _a, _a, _a, _a, null, _a, _a  } }; 
                yield return new object[] { _a, new[] { _a, _a, _a, _a, _a, _a, null, _a  } }; 

                
                yield return new object[] { _a, new[] { _a, _a, _a, _a, _a, _a, _a, _a  } };
                yield return new object[] { _a, new[] { _b, _c, _a, _a, _a, _a, _a, _b  } };
                yield return new object[] { null, new[] { _a, _b, _c, _d, _e, _f, _g, _a  } }; 
                yield return new object[] { null, new[] { _a, _a, _a, _a, _e, _f, _g, _e  } }; 
                yield return new object[] { null, new[] { null, _a, _a, _a, _e, _a,_e, _a  } }; 
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


    }
}
