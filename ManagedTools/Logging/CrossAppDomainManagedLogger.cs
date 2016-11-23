using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedTools
{
    public class CrossAppDomainManagedLogger : CrossAppDomainSingleton<CrossAppDomainManagedLogger>, IManagedLogger
    {
        //public IManagedLogger logger;

        public void Log(string message)
        {
            FileLogger.WriteLine("AppDomain: " + AppDomain.CurrentDomain.FriendlyName);
            FileLogger.WriteLine("CROSS AD LOGGER: " + message);
            //LogUsingDelegate(message);
        }

        //public void LogUsingDelegate(string message)
        //{
        //    if (logger != null)
        //        logger.Log("CROSS AD FUNC: " + message);
        //}
    }
}
