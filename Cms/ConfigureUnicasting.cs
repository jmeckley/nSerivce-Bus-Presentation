using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Cms
{
    public class ConfigureUnicasting
        : IProvideConfiguration<UnicastBusConfig>
    {
        public UnicastBusConfig GetConfiguration()
        {
            return new UnicastBusConfig
            {
                MessageEndpointMappings =
                {
                    new MessageEndpointMapping
                    {
                        AssemblyName = "Messages",
                        Namespace = "Messages.PaymentProcessing",
                        Endpoint = "paymentprocessing"
                    },
                    new MessageEndpointMapping
                    {
                        AssemblyName = "Messages",
                        Namespace = "Messages.Backend",
                        Endpoint = "backend"
                    },
                    new MessageEndpointMapping
                    {
                        AssemblyName = "Messages",
                        Namespace = "Messages.PaymentProcessing",
                        Endpoint = "paymentprocessing"
                    }
                }
            };
        }
    }
}