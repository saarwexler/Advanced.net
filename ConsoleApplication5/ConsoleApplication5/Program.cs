using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{

   
   
    class Program
    {

        private static string PrivateImp(string name)
        {
            return name + " Kuku";
        }

        private static bool PrivateImp2(object name)
        {
            return true;
        }

        private static Predicate<string> _myInstance;

        static void Main(string[] args)
        {
            _myInstance = PrivateImp2;


            Console.WriteLine(_myInstance("string"));
            Console.ReadLine();
        }

       
    }

    internal class AgeAndName

    {
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
