using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuantConnect.QCStudioPlugin
{
    [Serializable]
    public class LeanProxyFactory
    {
        public AppDomain domain;
        public LeanProxy proxy;

        public LeanProxy CreateProxy()
        {
            if (proxy != null) return proxy;

            var thisdll = Assembly.GetExecutingAssembly();
            string thisclass = typeof(LeanProxy).FullName;

            var domainSetup = new AppDomainSetup { PrivateBinPath = thisdll.Location };
            domain = AppDomain.CreateDomain("LeanDomain", null, domainSetup);
            domain.AssemblyResolve += (object sender, ResolveEventArgs args) =>
            {
                //Console.WriteLine("Resolving..." + args.Name);
                //if (args.Name == thisclass)
                return thisdll;
                //else
                //return proxy.assemblies.Values.First(x => x.FullName == args.Name);
            };

            proxy = domain.CreateInstanceFromAndUnwrap(thisdll.Location, thisclass) as LeanProxy;

            return proxy;
        }

        public void DestroyProxy()
        {
            proxy = null;
            if (domain != null) AppDomain.Unload(domain);
        }
    }
}
