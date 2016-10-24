using mscoree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagedTools
{
    public class CrossAppDomainSingleton<T> : MarshalByRefObject where T : new()
    {
        private static readonly string AppDomainName = "Singleton AppDomain";
        private static T _instance;

        private static AppDomain GetAppDomain(string friendlyName)
        {
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
                    if (appDomain.FriendlyName.Equals(friendlyName))
                    {
                        return appDomain;
                    }
                }
            }
            finally
            {
                host.CloseEnum(enumHandle);
                Marshal.ReleaseComObject(host);
                host = null;
            }
            return null;
        }


        public static T Instance
        {
            get
            {
                if (null == _instance)
                {
                    AppDomain appDomain = GetAppDomain(AppDomainName);
                    
                    if (null == appDomain)
                    {
                        // Setting the base directory makes it work from nunit test (otherwise the new app domain would look for the CrossAppDomainSingleton dll in the nunit directory
                        AppDomainSetup setup = new AppDomainSetup()
                        {
                            ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
                        };

                        appDomain = AppDomain.CreateDomain(AppDomainName, null, setup);
                    }
                    Type type = typeof(T);
                    T instance = (T)appDomain.GetData(type.FullName);
                    if (null == instance)
                    {
                        instance = (T)appDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
                        appDomain.SetData(type.FullName, instance);
                    }
                    _instance = instance;
                }

                return _instance;
            }
        }
    }
}
