using System;
using System.IO;
using System.Net.Mail;
using Messages.Backend;
using NServiceBus;

namespace Backend
{
    public class SendEmail
        : IHandleMessages<SendEmailCommand>
        , IDisposable
    {
        private readonly SmtpClient _smtp;

        public SendEmail()
        {
            _smtp = new SmtpClient("localhost", 25)
            {
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = EnsureEmailDirectoryExists.PickupDirectory,
            };
        }

        public void Handle(SendEmailCommand message)
        {
            var emailMessage = new MailMessage
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = false,
                From = new MailAddress(message.From),
                To =
                {
                    message.To
                }
            };

            _smtp.Send(emailMessage);
        }

        public void Dispose()
        {
            _smtp.Dispose();
        }
    }
}
