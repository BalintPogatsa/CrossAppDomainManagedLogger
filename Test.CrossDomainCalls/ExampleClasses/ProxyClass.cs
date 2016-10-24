using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Test.CrossDomainCalls.ExampleClasses
{
    public class ProxyClass : MarshalByRefObject
    {
        public void TestMethod()
        {
            Console.WriteLine("Test method running in: " + AppDomain.CurrentDomain.FriendlyName + " (" + AppDomain.CurrentDomain.Id + ")");
            MyCrossAppDomainSingleton.Instance.TestMethod();
            Console.WriteLine("\n\n");
        }
    }
}
