using CurbashaApi.Models;
using CurbashaApi.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CurbashaApi.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICustomEmailSender _emailSender;

        public ContactController(ICustomEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Name,Subject,Email,Body")] ContactModel contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            var body = $"Name:{contact.Name}<br>E-mail:{contact.Email}<br>"  + contact.Body;
            var response = _emailSender.SendEmailAsync(contact.Email, contact.Subject, body);

            if (response != null)
            {
                ViewBag.Message = "Successfully sent";
            }
            else
            {
                ViewBag.Message = "Sorry, try again later";
            }
            return View();
        }
    }
}
