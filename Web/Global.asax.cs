using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NServiceBus;

namespace Web
{
    public class MvcApplication 
        : HttpApplication
    {
        public static ISendOnlyBus Bus { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var configuration = new BusConfiguration();
            configuration.EndpointName("web");
            configuration.UseTransport<MsmqTransport>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.AssembliesToScan(AllAssemblies.Except("Razor"));
            configuration
                .Conventions()
                .DefiningCommandsAs(type => FilterByName(type, "Command"))
                .DefiningEventsAs(type => FilterByName(type, "Event"));

            Bus = NServiceBus.Bus.CreateSendOnly(configuration);
        }

        private bool FilterByName(Type type, string suffix)
        {
            if (type.Namespace == null) return false;
            if (type.Namespace.StartsWith("Messages") == false) return false;
            return type.Name.EndsWith(suffix);
        }
    }
}
