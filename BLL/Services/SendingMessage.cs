using System.Net.Mail;
using BLL.Interfaces;
using System.Configuration;

namespace BLL.Services
{
    public class SendingMessage : IMessage
    {
        public void Send(string mail, string subject, string message)
        {
            SmtpClient smtp = new SmtpClient();
            var from = new MailAddress(ConfigurationManager.AppSettings["EmailID"], "Andrey");
            var to = new MailAddress(mail);
            var myMessage = new MailMessage(from, to)
            {
                Subject = subject,
                Body = message
            };
            smtp.Send(myMessage);
        }
    }
}
