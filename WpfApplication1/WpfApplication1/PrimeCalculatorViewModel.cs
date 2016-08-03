using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class PrimeCalculatorViewModel
    {
        public IEnumerable<int> CountPrimes(int first, int last)
        {
            return Enumerable.Range(first, last).Where(IsPrime);
        }

        private static bool IsPrime(int arg)
        {
            bool result = true;
            Parallel.For(2,  (int)Math.Sqrt(arg), (x, state) =>
            {
                if (arg % x == 0)
                {
                    result = false;
                    state.Break();
                }
            });
            return result;
        }
    }
}
