using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{

   [PrintableClass( Color = "Black")]
    class A
    {
        private int x;

        private class innerClass
        {
            private int y;
        }

    }

    
    class B : A
    {
        public void Exp(Expression<Action<string>> expression)
        {
            Console.WriteLine();
        }

        [PrintableClassAttribute]
        public void PubliMethod()
        {
           
            Console.WriteLine("I am A");
        }

        private void PrivateMethod()
        {
            return;
        }
    }

    class Program
    {
        
     
        
        
        static void Main(string[] args)
        {

            dynamic aa = new B();
            Expression<Action<string>> ex = s => Console.WriteLine(s);
             aa.Exp1(ex);

            new B().Exp((s) => Console.WriteLine(s));
            var atType = typeof (PrintableClassAttribute);
            Console.WriteLine(typeof(B));     
            var x = new[] {new A(), new B()};
            foreach (var a in x)
            {

                var type = a.GetType();
                var attType = typeof (PrintableClassAttribute);
                
                if (type.IsDefined(attType))
                    Console.WriteLine(a);
            }
           Console.WriteLine();

                Console.Read();
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PrintableClassAttribute : Attribute
    {
        public string Color { get;  set; }

        
    }
}
