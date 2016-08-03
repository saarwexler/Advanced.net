using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication10
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new[]
            {
                "Ran",
                "Idan",
                "Samion",
                "Eli",
                "Alex",
                "Ahmed",
                "Dani",
                "Bashar",
                "Heli",
                "Mahmud",
                "Alon",
                "Elad",
                "Camel",
                "Amihai",
                "Ramadan",
                "Mariana",
                "Michal",
                "Ido",
                "Yakov"
            };

            var result = collection.AsParallel().WithMergeOptions(ParallelMergeOptions.FullyBuffered). Select(o => o + "Want to go home");
           
            foreach (var name in result)
                Console.WriteLine(name);
            Console.ReadLine();
        }

       
    }
}
