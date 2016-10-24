using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using mscoree;
using ManagedTools;
using System.Threading;

namespace CrossDomainSingletonTest
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test different solutions to handle callback from unmanaged code");
            Console.WriteLine("Default appdomain: " + AppDomain.CurrentDomain.FriendlyName + " (" + AppDomain.CurrentDomain.Id + ")");
            ProcessWrapper pw = new ProcessWrapper();
            
            AppDomain additionalDomain = AppDomain.CreateDomain("Additional AppDomain");
            Type processWrapperClassType = typeof(ProcessWrapper);
            ProcessWrapper pwDomain = (ProcessWrapper)additionalDomain.CreateInstanceAndUnwrap(processWrapperClassType.Assembly.FullName, processWrapperClassType.FullName);


            Console.WriteLine("\n--- Simple logger TEST 1 - Callback from unmanaged code - same thread");
            pw.TestSimpleManagedLogger();

            Console.WriteLine("\n--- Simple logger TEST 2 - Callback from unmanaged code - another thread");
            pw.TestSimpleManagedLoggerFromThread();
            Thread.Sleep(1000);

            Console.WriteLine("\n--- Simple logger TEST 3 - Callback from unmanaged code - same thread - different appdomain");
            pwDomain.TestSimpleManagedLogger();

            Console.WriteLine("\n--- Simple logger TEST 4 - Callback from unmanaged code - another thread - different appdomain");
            pwDomain.TestSimpleManagedLoggerFromThread();

            Thread.Sleep(1000);

            Console.WriteLine();

            Console.WriteLine("\n--- Cross AD logger TEST 1 - Callback from unmanaged code - same thread");
            pw.TestCrossAppDomainManagedLogger();

            Console.WriteLine("\n--- Cross AD logger TEST 2 - Callback from unmanaged code - another thread");
            pw.TestCrossAppDomainManagedLoggerFromThread();
            Thread.Sleep(1000);

            Console.WriteLine("\n--- Cross AD logger TEST 3 - Callback from unmanaged code - same thread - different appdomain");
            pwDomain.TestCrossAppDomainManagedLogger();

            Console.WriteLine("\n--- Cross AD logger TEST 4 - Callback from unmanaged code - another thread - different appdomain");
            pwDomain.TestCrossAppDomainManagedLoggerFromThread();


            // keep handle to stop pw from garbage collected
            //Console.WriteLine(pw.ToString());
            Console.ReadLine();
        }
    }
}
