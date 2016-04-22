using System;
using NServiceBus;

namespace PaymentProcessing
{
    public class EndpointConfiguration
        : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("paymentprocessing");
            configuration.UseTransport<MsmqTransport>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration
                .Conventions()
                .DefiningMessagesAs(type => FilterByName(type, "Message"))
                .DefiningCommandsAs(type => FilterByName(type, "Command"))
                .DefiningEventsAs(type => FilterByName(type, "Event"));
        }

        private bool FilterByName(Type type, string suffix)
        {
            if (type.Namespace == null) return false;
            if (type.Namespace.StartsWith("Messages") == false) return false;
            return type.Name.EndsWith(suffix);
        }
    }
}
