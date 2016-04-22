using NServiceBus;

namespace Invoicing
{
    public class EndpointConfiguration 
        : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("invoicing");
            configuration.UseTransport<MsmqTransport>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();
        }
    }
}
