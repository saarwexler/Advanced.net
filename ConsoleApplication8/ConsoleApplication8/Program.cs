using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Test
    {
        static void Main()
        {
            PriorityTest priorityTest = new PriorityTest();

            Thread thread1 = new Thread(priorityTest.ThreadMethod);
            Thread thread2 = new Thread(priorityTest.ThreadMethod);
            Thread thread3 = new Thread(priorityTest.ThreadMethod);
            thread1.Start();
           thread2.Start();
            thread3.Start();
            Console.ReadLine();
            priorityTest.LoopSwitch = false;
            Console.ReadLine();
            priorityTest.Pulse();
            Console.Read();
        }

       
    }

    // The example displays output like the following:
    //    ThreadOne   with      Normal priority has a count =   755,897,581
    //    ThreadThree with AboveNormal priority has a count =   778,099,094
    //    ThreadTwo   with BelowNormal priority has a count =     7,840,984
}

