using System.Collections.Generic;
using System.Text;
using Messages.Backend;
using Messages.Common;
using NServiceBus;

namespace Backend
{
    public class NotifyCustomerOfPaymentHandler
        : IHandleMessages<NotifyCustomerOfPaymentCommand>
    {
        private readonly IBus _bus;

        public NotifyCustomerOfPaymentHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(NotifyCustomerOfPaymentCommand message)
        {
            dynamic customer = GetCustomer(message.CustomerUsername);
            var body = string.Format(@"Thank you... Order Number: {0}.... 
Products: 
{1}

{2}", message.OrderNumber, BuildProductText(message.Products), message.Total);

            _bus.Send(new SendEmailCommand
            {
                To = customer.EmailAddress,
                From = "customerservice@retailer.com",
                Subject = "Thank you for your recent purchase from Acme, Inc.",
                Body = body
            });

            _bus.Reply(new CustomerNotifiedOfPaymentMessage {Id = message.Id});
        }

        private StringBuilder BuildProductText(IEnumerable<ProductOrdered> products)
        {
            var builder = new StringBuilder();
            foreach (var product in products)
            {
                builder
                    .AppendFormat("{2} {0} at {1:C}", product.Description, product.Price, product.Quantity)
                    .AppendLine();
            }
            return builder;
        }

        private object GetCustomer(string customerUsername)
        {
            return new
            {
                EmailAddress = "johndoe@email.com"
            };
        }
    }
}