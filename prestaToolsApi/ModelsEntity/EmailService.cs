using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using MimeKit;
using System.Drawing.Text;
using System.Net;
using System.Runtime.Intrinsics.X86;

namespace prestaToolsApi.ModelsEntity
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _config;
        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Task SendAsyncronousEmail(string email, string subject, string message)
        {

            /*
             * NO FUNCIONO 
             * 
             * 
            var origin_email = "ventas@prestatools.cl";
            var password = "Prestatools.2023";
            var client = new SmtpClient("smtp.hostinger.com", 465)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(origin_email, password)
            };
            return client.SendMailAsync(new MailMessage(from:origin_email,to:email, subject, message));
            
            */
            throw new NotImplementedException();
        }

        void IEmailService.SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Para));
            email.Subject = request.Asunto;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = request.Contenido    
            };

            using var smtp = new SmtpClient();
            smtp.Connect(
                _config.GetSection("Email:Host").Value,
                 Convert.ToInt32(_config.GetSection("Email:Port").Value),
                 SecureSocketOptions.StartTls
                 
               );
            smtp.Authenticate(
                _config.GetSection("Email:UserName").Value,
                _config.GetSection("Email:Password").Value
                );

            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
