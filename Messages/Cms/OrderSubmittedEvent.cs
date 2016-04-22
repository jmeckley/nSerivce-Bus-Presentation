using System.Collections.Generic;
using Messages.Common;

namespace Messages.Cms
{
    public class OrderSubmittedEvent
    {
        public OrderSubmittedEvent()
        {
            Products = new ProductOrdered[0];
        }

        public int OrderNumber { get; set; }
        public string CustomerUsername { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<ProductOrdered> Products { get; set; }
    }
}
