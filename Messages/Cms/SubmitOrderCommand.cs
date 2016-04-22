using System;
using System.Collections.Generic;

namespace Messages.Cms
{
    public class SubmitOrderCommand
    {
        public string CustomerUsername { get; set; }
        public DateTime DateSubmitted { get; set; }
        public IEnumerable<Product> ProductIds { get; set; }

        public class Product 
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }
    }
}
