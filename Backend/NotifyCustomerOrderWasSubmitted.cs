using System;
using Messages.Cms;
using NServiceBus;

namespace Backend
{
    public class OrderWasSubmittedHandler
        : IHandleMessages<OrderSubmittedEvent>
    {
        public void Handle(OrderSubmittedEvent message)
        {
            Console.WriteLine("Kickoff another process once the order is submitted.");
        }
    }
}
