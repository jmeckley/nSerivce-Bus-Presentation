using System;
using NServiceBus;

namespace Cms
{
    public class EndpointConfiguration
        : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("cms");
            configuration.UseTransport<MsmqTransport>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration
                .Conventions()
                .DefiningMessagesAs(type => FilterByName(type, "Message"))
                .DefiningCommandsAs(type => FilterByName(type, "Command"))
                .DefiningEventsAs(type => FilterByName(type, "Event"));

            configuration.RegisterComponents(configure => configure.ConfigureComponent<ProductCatalog>(DependencyLifecycle.InstancePerCall));
        }

        private bool FilterByName(Type type, string suffix)
        {
            if (type.Namespace == null) return false;
            if (type.Namespace.StartsWith("Messages") == false) return false;
            return type.Name.EndsWith(suffix);
        }
    }
}
