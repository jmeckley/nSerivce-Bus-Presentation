using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Backend
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
                    /*
                    new MessageEndpointMapping
                    {
                        AssemblyName = "Messages",
                        Namespace = "Messages.Cms",
                        Endpoint = "cms"
                    },
                    */
                    new MessageEndpointMapping
                    {
                        AssemblyName = "Messages",
                        Namespace = "Messages.Backend",
                        Endpoint = "backend"
                    }
                }
            };
        }
    }
}