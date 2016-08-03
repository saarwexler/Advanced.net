using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = new[]
            {
                "Ido", "Ido", "Danny", "Eli", "Bashar", "Victor", "Alon", "Kamel", "Ahmed", "Idan", "Yacov", "Alex",
                "Amichai", "Heli", "Mariana" ,"Semion"
            };

            var surNames = new[]
            {
                'p', 'b', 'k', 'k', 'e', 'k', 'r', 'k', 'h', 'm', 'g', 'k', 'v', 'b', 'a', 'e' 
            };

            var list = ManipulateNames(names, surNames);
            names[6] = "Saar!!!!!!";

            PrintList(list);
            Console.ReadLine();
        }

        private static IEnumerable<string> ManipulateNames(IEnumerable<string> names, IEnumerable<char> surNames)
        {
            return names.Zip1(surNames, (x,y) => x + " "+ char.ToUpper(y)) ;

          
        }

        private static void PrintList(IEnumerable list)
        {
            foreach (var name in list)
            {

                Console.WriteLine(name);      
            }
        }
    }

    public static class Ext
    {
        public static IEnumerable<TR> Zip1<TX, TY,TR>(this IEnumerable<TX> x, IEnumerable<TY> y, Func<TX, TY, TR> f)
        {
            var enx = x.GetEnumerator();
            var eny = y.GetEnumerator();
            while (enx.MoveNext() && eny.MoveNext())
            {
                yield return f(enx.Current, eny.Current);
            }
        }

    }
}

