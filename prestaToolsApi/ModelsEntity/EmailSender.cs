using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Drawing.Text;
using System.Net;
using System.Net.Mail;

namespace prestaToolsApi.ModelsEntity
{
    public class EmailSender : IEmailSender
    {
        public Task SendAsyncronousEmail(string email, string subject, string message)
        {
            var origin_email = "ventas@prestatools.cl";
            var password = "password";
            var client = new SmtpClient("smtp mail", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(origin_email, password)
            };
            return client.SendMailAsync(new MailMessage(from:origin_email,to:email, subject, message));
            //throw new NotImplementedException();
        }
    }
}
