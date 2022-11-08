using Microsoft.AspNetCore.Mvc;

namespace CurbashaApi.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

        //Need Fixing

        //[HttpPost]
        //public async Task Contact(string name, string email, string subject, string message)
        //{
        //    var emailMessage = new MimeMessage();

        //    emailMessage.To.Add(new MailboxAddress("Curbasha Admins", "cspam.bspam.aspam@gmail.com"));
        //    emailMessage.From.Add(new MailboxAddress(name, email));
        //    emailMessage.Subject = subject;
        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //    {
        //        Text = message
        //    };

        //    using (var client = new SmtpClient())
        //    {
        //        await client.ConnectAsync("");
        //        await client.AuthenticateAsync("cspam.bspam.aspam@gmail.com", "password");
        //        await client.SendAsync(emailMessage);

        //        await client.DisconnectAsync(true);
        //    }
        //}
    }
}
