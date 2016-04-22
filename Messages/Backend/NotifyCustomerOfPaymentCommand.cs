using System;
using System.Collections.Generic;
using Messages.Common;

namespace Messages.Backend
{
    public class NotifyCustomerOfPaymentCommand
    {
        public NotifyCustomerOfPaymentCommand()
        {
            Products = new ProductOrdered[0];
        }

        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string CustomerUsername { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<ProductOrdered> Products { get; set; }
    }
}
