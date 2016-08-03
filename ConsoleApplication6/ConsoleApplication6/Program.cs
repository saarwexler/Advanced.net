using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication6
{

    
    class Program
    {
        public static string func1( int x)
        {
            return x.ToString();
        }



        static void Main(string[] args)
        {
            var syncObject = new object();
            Func<int, string> myFunc = i =>
            {
              //  Thread.Sleep(2000);
                return i.ToString();
            };
            var result =  myFunc.BeginInvoke(5, x => Console.WriteLine(myFunc.EndInvoke(x) ) , syncObject);
            new Action(() =>
            {
//Thread.Sleep(10);
                Console.WriteLine("blbla");
            }).BeginInvoke(null,null);

            Console.ReadLine();
        }
    }

}
