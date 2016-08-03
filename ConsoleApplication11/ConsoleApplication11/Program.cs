using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication11
{
     
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Thread Id when enter: " + Thread.CurrentThread.ManagedThreadId);
            var result = Wait(5000);
            Console.ReadLine();
        }

        private static async Task Wait(int delay)
        {
            Console.WriteLine("Thread is on enter " +  Thread.CurrentThread.ManagedThreadId);
            var foo = new Foo {Delay = delay};
            await foo;
            Console.WriteLine("Thread is on exit " + Thread.CurrentThread.ManagedThreadId);
        }

        private static string GetString()
        {
            Console.WriteLine("the Thread Id before wait " + Thread.CurrentThread.ManagedThreadId);
            WebClient wc = new WebClient();
            var str =  wc.DownloadStringTaskAsync("http://microsoft.com");
            str.Wait();
            Console.WriteLine("Thread id after wait ");
            return str + "the thread id " + Thread.CurrentThread.ManagedThreadId;
        }

        private static async Task<string> AddAnotherString()
        {
            WebClient wc = new WebClient();
            var googleString =  wc.DownloadStringTaskAsync(new Uri("http://www.codevalue.net"));
            var t1 =  GetStringAsync();
            return  await t1 + (await googleString).Substring(0,300);
        }

        private static  Task<string> AddAnotherString2()
        {
            WebClient wc = new WebClient();
            var googleString = wc.DownloadStringTaskAsync(new Uri("http://www.codevalue.net"));
             var first = GetStringAsync();
            return Task.WhenAll(first, googleString).ContinueWith(a => a.Result[0] + a.Result[1]);
        }

        private static async Task<string> GetStringAsync()
        {
            Console.WriteLine("the Thread Id before await " + Thread.CurrentThread.ManagedThreadId );
            WebClient wc = new WebClient();
            var str = await wc.DownloadStringTaskAsync("http://microsoft.com");
            Console.WriteLine("Thread id after await ");
            return str + "the thread id " + Thread.CurrentThread.ManagedThreadId;
        }

        private static  Task<string> GetStringAsync2()
        {
            Console.WriteLine("the Thread Id before await " + Thread.CurrentThread.ManagedThreadId);
            WebClient wc = new WebClient();
            var str =  wc.DownloadStringTaskAsync("http://microsoft.com");
           return str.ContinueWith((x) =>
            {
                Console.WriteLine("Thread id after await ");
                return x.Result + "the thread id " + Thread.CurrentThread.ManagedThreadId;
            });
        }
    }
}
