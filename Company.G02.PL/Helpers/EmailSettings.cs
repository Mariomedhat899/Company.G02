using System.Net;
using System.Net.Mail;

namespace Company.G02.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            try {
                var Client = new SmtpClient("smtp.gmail.com", 587);
                Client.EnableSsl = true;
                Client.Credentials = new NetworkCredential("mariomedhat899@gmail.com", "swiwnppvxrvbwinu");
                Client.Send("mariomedhat899@gmail.com", email.To, email.Subject, email.Body);
            }
            catch(Exception e)
            {
                return false;
            }
            //swiwnppvxrvbwinu
            return true;
        }
    }
}
