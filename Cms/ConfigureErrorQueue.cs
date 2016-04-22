using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Cms
{
    public class ConfigureErrorQueue 
        : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "error"
            };
        }
    }
}