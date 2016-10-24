﻿using ManagedTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Test.CrossDomainCalls.ExampleClasses;

namespace Test.CrossDomainCalls
{
    [TestFixture]
    public class UnmanagedTests
    {
        [Test]
        public void TestSimpleManagedLogger()
        {
            ProcessWrapper pw = new ProcessWrapper();

            Console.WriteLine("\n--- Simple logger TEST 1 - Callback from unmanaged code - same thread");
            pw.TestSimpleManagedLogger();
        }

        [Test]
        [Ignore("This test would crash the nunit UI runner")]
        public void TestSimpleManagedLoggerFromThread()
        {
            ProcessWrapper pw = new ProcessWrapper();

            Console.WriteLine("\n--- Simple logger TEST 2 - Callback from unmanaged code - another thread");
            pw.TestSimpleManagedLoggerFromThread();
        }

        [Test]
        public void TestCrossDomainManagedLogger()
        {
            ProcessWrapper pw = new ProcessWrapper();

            Console.WriteLine("\n--- Cross AD logger TEST 1 - Callback from unmanaged code - same thread");
            pw.TestCrossAppDomainManagedLogger();
        }

        [Test]
        // You have to copy the cross app domain singleton dll next to the nunit runner - otherwise it cannot load the dll
        // This is done by a post build event
        public void TestCrossDomainManagedLoggerFromThread()
        {
            ProcessWrapper pw = new ProcessWrapper();

            Console.WriteLine("\n--- Cross AD logger TEST 2 - Callback from unmanaged code - another thread");
            pw.TestCrossAppDomainManagedLoggerFromThread();
        }
    }
}
