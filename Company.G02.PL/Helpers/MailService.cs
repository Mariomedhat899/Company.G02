using Company.G02.PL.Helpers.Interfaces;
using Company.G02.PL.Settings;
using MailKit.Net.Smtp;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Company.G02.PL.Helpers
{
    public class MailService(IOptions<MailSettings> _options) : IMailService
    {
        public void SendEmail(Email email)
        {

            //1.bulid email message
            var mail = new MimeMessage();

            mail.Subject = email.Subject;
            mail.From.Add( new MailboxAddress( _options.Value.DisplayName ,_options.Value.Email));

            mail.To.Add(MailboxAddress.Parse(email.To));


            var Builder = new BodyBuilder();

            Builder.TextBody = email.Body;
            mail.Body = Builder.ToMessageBody();

            //2.Establish connection to  server

            using var smtp = new SmtpClient();
            smtp.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Value.Email, _options.Value.Password);
            //3.send email

             smtp.Send(mail);

            
        }
    }
}
