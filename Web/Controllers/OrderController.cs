using System;
using System.Web.Mvc;
using Messages.Cms;
using NServiceBus;

namespace Web.Controllers
{
    public class OrderController 
        : Controller
    {
        private readonly ISendOnlyBus _bus = MvcApplication.Bus;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit()
        {
            _bus.Send(new SubmitOrderCommand
            {
                CustomerUsername = "john_doe",
                DateSubmitted = DateTime.UtcNow,
                ProductIds = new[]
                {
                    new SubmitOrderCommand.Product
                    {
                        Id = 100,
                        Quantity = 1
                    },
                    new SubmitOrderCommand.Product
                    {
                        Id = 101,
                        Quantity = 2
                    },
                    new SubmitOrderCommand.Product
                    {
                        Id = 102,
                        Quantity = 3
                    },
                },
            });

            return RedirectToAction("OrderComplete");
        }

        public ActionResult OrderComplete()
        {
            return View();
        }
    }
}