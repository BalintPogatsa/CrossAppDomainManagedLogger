using mscoree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ManagedTools
{
    public class AppDomainHelper
    {
        public static IEnumerable<AppDomain> GetAppDomains()
        {
            List<AppDomain> result = new List<AppDomain>();

            IntPtr enumHandle = IntPtr.Zero;
            ICorRuntimeHost host = new CorRuntimeHost();
            try
            {
                host.EnumDomains(out enumHandle);

                object domain = null;
                while (true)
                {
                    host.NextDomain(enumHandle, out domain);
                    if (domain == null)
                    {
                        break;
                    }
                    AppDomain appDomain = (AppDomain)domain;
                    result.Add(appDomain);
                }
            }
            finally
            {
                host.CloseEnum(enumHandle);
                Marshal.ReleaseComObject(host);
                host = null;
            }
            return result;
        }

        public static AppDomain GetDefaultAppDomain()
        {
            return GetAppDomains().Where(ad => ad.IsDefaultAppDomain()).FirstOrDefault();
        }
    }
}
