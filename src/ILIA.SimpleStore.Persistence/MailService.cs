using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace ILIA.SimpleStore.API.Services
{
    
    public class MailOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public interface IMailService
    {
        void SendMail(string to, string subject, string body);
    }
    public class MailService : IMailService
    {
        private SmtpClient client;
        public MailService(IOptions<MailOptions> options)
        {
            _ = options.Value.Host ?? throw new System.ArgumentNullException("The Host Need to be setted");
            if (options.Value.Port == 0)
                throw new ArgumentException("The port needs to be setted");

            client = new SmtpClient
            {
                Host = options.Value.Host,
                Port = options.Value.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
            
        }
        

        public void SendMail(string to, string subject, string body)
        {
            client.Send("Isla@Isla.com", to, subject, body);
        }
    }
}
