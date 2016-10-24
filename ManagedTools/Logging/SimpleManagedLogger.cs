using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedTools
{
    public class SimpleManagedLogger : MarshalByRefObject, IManagedLogger
    {
        public void Log(string message)
        {
            Console.WriteLine("SIMPLE LOGGER: " + message);
        }
    }
}
