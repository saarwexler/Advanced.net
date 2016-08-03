using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            var p= new Program();
           var task =  p.Print();
            task.Wait();
            Console.ReadLine();


        }

        public async Task Print()
        {
            Console.Write(await ReturnToString());
        }


        public async Task<int> AccessTheWebAsync()
        {
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");
            Thread.Sleep(10000);
            Console.WriteLine("somthing");
            var content = await getStringTask;
            Console.WriteLine(content);
            return content.Length;
            Func<string> f= 
        }

        public  int AccessTheWeb()
        {
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");
            Thread.Sleep(500);
             getStringTask.Wait();
            return getStringTask.Result.Length;
        }

        public Task<int> AccessTheWebAsyncNoAwait()
        {
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");
            Thread.Sleep(500);
            return getStringTask.ContinueWith((task) =>task.Result.Length );
        }

        public async Task<string> ReturnToString()
        {
            int x = await AccessTheWebAsync();
            if (x > 3)
                return "Jon Snow";
            return "Red woman";
        }
    }
}
