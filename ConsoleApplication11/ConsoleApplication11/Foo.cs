using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication11
{
    public static class FooExt
    {
        public static TaskAwaiter GetAwaiter(this IFoo foo)
        {
            Console.WriteLine("Inside get awaiter " + Thread.CurrentThread.ManagedThreadId);
            var awaiter =  Task.Delay(foo.Delay).ContinueWith(t => Console.WriteLine(Thread.CurrentThread.ManagedThreadId)).GetAwaiter();
            Console.WriteLine("before return the awaiter" + Thread.CurrentThread.ManagedThreadId);
            return awaiter;
        }
    }

   

     public class Foo :IFoo
    {
        public int Delay { get; set; }
    }

    public interface IFoo
    {
         int Delay { get; set; }
    }
}
