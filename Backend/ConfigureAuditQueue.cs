using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Backend
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