using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  NUnit.Framework;
using NUnit;
using NUnit.Common;
using PrimeFactorKata;

namespace PrimeFactorTest
{
    [TestFixture]
    public class PrimeFactorShould
    {
        [Test]
        public void Return2For2()
        {
            var target = new PrimeFactorsCalculator();
            int[] result  =target.CalculateFactors(2);
            var expected = new[] {2};

            Assert.True(expected.SequenceEqual(result));
        }

        [Test]
        public void Return3for3()
        {
            var target = new PrimeFactorsCalculator();
            int[] result = target.CalculateFactors(3);
            var expected = new[] { 3 };

            Assert.True(expected.SequenceEqual(result));
        }

        [Test]
        public void ReturnTwice2for4()
        {
            var target = new PrimeFactorsCalculator();
            int[] result = target.CalculateFactors(4);
            var expected = new[] { 2,2 };

            Assert.True(expected.SequenceEqual(result));
        }

        [Test]
        public void ReturnTwice5for5()
        {
            var target = new PrimeFactorsCalculator();
            int[] result = target.CalculateFactors(5);
            var expected = new[] { 5};

            Assert.True(expected.SequenceEqual(result));
        }

        [Test]
        public void Return2and3for6()
        {
            var target = new PrimeFactorsCalculator();
            int[] result = target.CalculateFactors(6);
            var expected = new[] { 2,3 };

            Assert.True(expected.SequenceEqual(result));
        }


        [Test]
        public void Return7for7()
        {
            var target = new PrimeFactorsCalculator();
            int[] result = target.CalculateFactors(7);
            var expected = new[] {7 };

            Assert.True(expected.SequenceEqual(result));
        }

        [Test]
        public void Return222for8()
        {
            var target = new PrimeFactorsCalculator();
            int[] result = target.CalculateFactors(8);
            var expected = new[] { 2,2,2 };

            Assert.True(expected.SequenceEqual(result));
        }


        [Test]
        public void Return33for9()
        {
            var target = new PrimeFactorsCalculator();
            int[] result = target.CalculateFactors(9);
            var expected = new[] { 3,3};

            Assert.True(expected.SequenceEqual(result));
        }



    }
}
