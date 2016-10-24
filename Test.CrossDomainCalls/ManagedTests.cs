using CrossDomainSingletonTest;
using ManagedTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Test.CrossDomainCalls.ExampleClasses;

namespace Test.CrossDomainCalls
{
    [TestFixture]
    public class ManagedTests
    {
        [Test]
        public void IterateAppDomains()
        {
            foreach (var domain in AppDomainHelper.GetAppDomains())
            {
                Console.WriteLine("FriendlyName: " + domain.FriendlyName);
                Console.WriteLine("Base directory: " + domain.BaseDirectory);
                //Console.WriteLine("LoadedAssemblies: " + string.Join(", ", AppDomainHelper.GetAppDomainAssemblyNames(domain)));
                Console.WriteLine("IsDefaultAppDomain: " + domain.IsDefaultAppDomain());
            }
        }

        [Test]
        public void AccessOtherAssembly()
        {
            ProxyClass tc = new ProxyClass();
        }

        [Test]
        public void CallSingletonFromDomains()
        {
            Console.WriteLine("Call MySingleton method from" + AppDomain.CurrentDomain.FriendlyName + " (" + AppDomain.CurrentDomain.Id + ")");
            MyCrossAppDomainSingleton.Instance.TestMethod();

            Console.WriteLine("\n\n");

            // Setting the base directory makes it work from nunit test (otherwise the new app domain would look for the CrossAppDomainSingleton dll in the nunit directory
            AppDomainSetup setup = new AppDomainSetup()
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
            };

            AppDomain domainA = AppDomain.CreateDomain("A Domain", null, setup);
            AppDomain domainB = AppDomain.CreateDomain("B Domain", null, setup);

            Type testClassType = typeof(ProxyClass);
            ProxyClass proxyA = (ProxyClass)domainA.CreateInstanceAndUnwrap(testClassType.Assembly.FullName, testClassType.FullName);
            ProxyClass proxyB = (ProxyClass)domainB.CreateInstanceAndUnwrap(testClassType.Assembly.FullName, testClassType.FullName);

            proxyA.TestMethod();
            proxyB.TestMethod();
        }
    }
}
