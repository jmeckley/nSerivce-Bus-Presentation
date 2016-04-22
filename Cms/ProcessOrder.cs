using System;
using System.Collections.Generic;
using System.Linq;
using Messages.Backend;
using Messages.Cms;
using Messages.Common;
using Messages.PaymentProcessing;
using NServiceBus;
using NServiceBus.Saga;

namespace Cms
{
    public class ProcessOrder
        : Saga<OrderSagaData>
        , IAmStartedByMessages<SubmitOrderCommand>
        , IHandleMessages<CreditCardPaymentProcessedMessage>
        , IHandleMessages<CustomerNotifiedOfPaymentMessage>
    {
        private static int ORDER_NUMBER = 1000;

        private readonly IProductCatalog _catalog;

        public ProcessOrder(IProductCatalog catalog)
        {
            _catalog = catalog;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        {
            mapper
                .ConfigureMapping<CreditCardPaymentProcessedMessage>(message => message.Id)
                .ToSaga(sagaData => sagaData.Id);

            mapper
                .ConfigureMapping<CustomerNotifiedOfPaymentMessage>(message => message.Id)
                .ToSaga(sagaData => sagaData.Id);
        }

        public void Handle(SubmitOrderCommand message)
        {
            Data.OrderNumber = ORDER_NUMBER++;
            Data.CustomerUsername = message.CustomerUsername;
            Data.Products = MapProducts(message.ProductIds);

            //get user's credit card number
            
            Bus.Send(new ProcessCreditCardPaymentCommand
            {
                Id = Data.Id,
                CardholderName = "John Q. Public",
                CardNumber = "5454545454545454",
                Amount = Data.Total(),
            });
        }

        public void Handle(CreditCardPaymentProcessedMessage message)
        {
            //update CMS system with payment confirmation number
            Bus.Send(new NotifyCustomerOfPaymentCommand
            {
                Id = message.Id,
                OrderNumber = Data.OrderNumber,
                CustomerUsername = Data.CustomerUsername,
                Products = Data.Products.ToArray(),
                Total = Data.Total()
            });
        }

        public void Handle(CustomerNotifiedOfPaymentMessage message)
        {
            MarkAsComplete();
            Bus.Publish(new OrderSubmittedEvent
            {
                OrderNumber = Data.OrderNumber,
                CustomerUsername = Data.CustomerUsername,
                Products = Data.Products.ToArray(),
                Total = Data.Total()
            });
        }

        private IEnumerable<ProductOrdered> MapProducts(IEnumerable<SubmitOrderCommand.Product> products)
        {
            return products
                .Select(product => new
                {
                    Product = _catalog.Get(product.Id),
                    product.Quantity
                })
                .Select(x => new ProductOrdered
                {
                    Description = x.Product.Description,
                    Price = x.Product.Price,
                    Quantity = x.Quantity
                });
        }
    }

}
