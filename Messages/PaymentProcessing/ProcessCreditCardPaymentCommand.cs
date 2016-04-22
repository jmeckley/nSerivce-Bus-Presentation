using System;

namespace Messages.PaymentProcessing
{
    public class ProcessCreditCardPaymentCommand
    {
        public Guid Id { get; set; }
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
