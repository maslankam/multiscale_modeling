using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Drawing;
using Model;

namespace Test.Model.Microelements
{
    public class InclusionsTest
    {
        [Fact]
        public void MinMaxTest()
        {
            {
                Assert.Equal(0, Inclusion.MinRadius);
                Assert.Equal(0, Inclusion.MaxRadius);


                var i1 = new Inclusion(0, 1, 5, Color.White);

                Assert.Equal(5, Inclusion.MinRadius);
                Assert.Equal(5, Inclusion.MaxRadius);

                var i2 = new Inclusion(0, 1, 1, Color.White);

                Assert.Equal(1, Inclusion.MinRadius);
                Assert.Equal(5, Inclusion.MaxRadius);

                var i3 = new Inclusion(0, 1, 10, Color.White);

                Assert.Equal(1, Inclusion.MinRadius);
                Assert.Equal(10, Inclusion.MaxRadius);
            }
        }

    }
}
