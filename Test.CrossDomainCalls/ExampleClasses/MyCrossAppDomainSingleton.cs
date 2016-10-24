using ManagedTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.CrossDomainCalls.ExampleClasses
{
    class MyCrossAppDomainSingleton : CrossAppDomainSingleton<MyCrossAppDomainSingleton>
    {
        public void TestMethod()
        {
            Console.WriteLine("MySingleton method from domain: " + AppDomain.CurrentDomain.FriendlyName + " (" + AppDomain.CurrentDomain.Id + ")");
        }
    }
}