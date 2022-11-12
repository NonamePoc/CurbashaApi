using System;
using System.Security.Policy;
using CurbashaApi.Areas.Identity.Entity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CurbashaApi.Services
{
    public class EmailSender: IEmailSender, ICustomEmailSender
    {
        private string apiKey = @"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";
        private string confirmtionTempl  = "d-f36414e602d94447acd6f7020db5be19";
        private string resetPassTempl    = "d-498faee9418e4e5c8da01833e24e6293";
        private string orderConfirmTempl = "d-5404f824b596436788d2b95c95deff11";

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute (subject, htmlMessage, email);
        }

        public Task Execute(string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("shopcurbasha@gmail.com", "Curbasha Shop"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo("shopcurbasha@gmail.com", "Curbasha Shop");

            // Disable click tracking. 
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

        public Task SendEmailConfirmAsync(string email, string url)
        {
            var dynamicTemplateData = new {
                Sender_Name = email,
                Url = url
            };
            return ExecuteWithTempl(email, email, confirmtionTempl, dynamicTemplateData);
        }

        public Task SendResetPassAsync(string email, string url)
        {
            var dynamicTemplateData = new
            {
                Url = url
            };
            return ExecuteWithTempl(email, email, resetPassTempl, dynamicTemplateData);
        }

        public Task SendOrderConfirmAsync(string email, string username, IEnumerable<AspOrderItem> items, int orderId)
        {
            var dynamicTemplateData = new
            {
                OrderId = orderId,
                Sender_Name = username,                
                Date = DateTime.Now,
                Item = items
            };
            return ExecuteWithTempl(email, username, orderConfirmTempl, dynamicTemplateData);
        }

        public Task ExecuteWithTempl(string email, string name, string templateId, object dynamicTemplateData)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("shopcurbasha@gmail.com", "Curbasha Shop");
            var to = new EmailAddress(email);

            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);

            // Disable click tracking. 
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}

