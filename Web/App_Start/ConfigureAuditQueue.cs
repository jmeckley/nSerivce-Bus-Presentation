using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Web
{
    public class ConfigureAuditQueue
        : IProvideConfiguration<AuditConfig>
    {
        public AuditConfig GetConfiguration()
        {
            return new AuditConfig
            {
                QueueName = "audit"
            };
        }
    }
}