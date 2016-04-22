using System;
using System.Collections.Generic;
using System.Linq;
using Messages.Common;
using NServiceBus.Saga;

namespace Cms
{
    public class OrderSagaData 
        : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
        
        public int OrderNumber { get; set; }
        public string CustomerUsername { get; set; }
        public IEnumerable<ProductOrdered> Products { get; set; }

        public decimal Total()
        {
            return Products.Select(p => p.Quantity * p.Price).Sum();
        }
    }
}