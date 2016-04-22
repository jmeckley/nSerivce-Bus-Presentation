using System;
using Messages.PaymentProcessing;
using NServiceBus;

namespace PaymentProcessing
{
    public class ProcessCreditCardPaymentHandler
        : IHandleMessages<ProcessCreditCardPaymentCommand>
    {
        private readonly IBus _bus;

        public ProcessCreditCardPaymentHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ProcessCreditCardPaymentCommand message)
        {
            Console.WriteLine("Charing {0:C} to credit card  {1}", message.Amount, message.CardNumber);
            _bus.Reply(new CreditCardPaymentProcessedMessage
            {
                Id = message.Id,
                ConfirmationNumber = 101
            });
        }
    }
}
