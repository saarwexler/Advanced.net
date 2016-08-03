using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeFactorKata
{
    public class PrimeFactorsCalculator
    {
        public int[] CalculateFactors(int i)
        {

            if (i > 3)
            {
                return Recurse(i);
            }
            return new[] {i};
        }

        private int[] Recurse(int i)
        {
            int devider = 2;
            while (i%devider != 0)
                devider++;
            return CalculateFactors(new[] { devider }, i / devider);
           
        }

        private int[] CalculateFactors(int[] curr, int i)
        {
            if (i == 1)
                return curr;
            return curr.Concat(CalculateFactors(i)).ToArray();

        }
    }
}
