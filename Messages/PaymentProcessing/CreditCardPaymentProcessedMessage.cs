using System;
using System.Collections.Generic;
using Messages.Common;

namespace Messages.PaymentProcessing
{
    public class CreditCardPaymentProcessedMessage
    {
        public int ConfirmationNumber { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<ProductOrdered> Products { get; set; }
    }
}
