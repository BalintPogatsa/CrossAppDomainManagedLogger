using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedTools
{
    public class LogProxy : MarshalByRefObject
    {
        public void LogFromUnmanagedThread(string message)
        {
            CrossAppDomainManagedLogger.Instance.Log(message);
        }
    }
}
