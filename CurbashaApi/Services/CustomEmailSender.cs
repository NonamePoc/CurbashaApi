using System;
using CurbashaApi.Areas.Identity.Entity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CurbashaApi.Services
{
    public interface ICustomEmailSender : IEmailSender
    { 

        public Task SendEmailConfirmAsync(string email, string url);

        public Task SendResetPassAsync(string email, string url);

        public Task SendOrderConfirmAsync(string email, string username, IEnumerable<AspOrderItem> items, int orderId);

        public Task ExecuteWithTempl(string email, string name, string templateId, dynamic dynamicTemplateData);

    }
}

