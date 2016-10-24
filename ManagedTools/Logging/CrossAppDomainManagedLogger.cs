using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedTools
{
    public class CrossAppDomainManagedLogger : CrossAppDomainSingleton<CrossAppDomainManagedLogger>, IManagedLogger
    {
        public void Log(string message)
        {
            Console.WriteLine("CROSS AD LOGGER: " + message);
        }
    }
}
