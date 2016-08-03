using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class PrimeCalculatorViewModelShould
    {
        private PrimeCalculatorViewModel _target;

        [TestInitialize]
        public void Init()
        {
            _target = new PrimeCalculatorViewModel();
        }


        [TestMethod]
        public void Rerturn3When2And4()
        {
           var result =  _target.CountPrimes(2, 4);
            int[] expectedResult = {2,3};
            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Rerturn3and5When2And6()
        {
            
            var result = _target.CountPrimes(2, 6);
            int[] expectedResult = {2, 3, 5 };
            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }


    }
}
