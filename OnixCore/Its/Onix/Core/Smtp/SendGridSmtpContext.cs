using SendGrid;
using SendGrid.Helpers.Mail;

namespace Its.Onix.Core.Smtp
{
	public class SendGridSmtpContext : SmtpContextBase
	{
        public SendGridSmtpContext() : base()
        {
        }

        public override void Send(Mail mail)
        {
            string apiKey = GetSmtpPassword();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(mail.From, mail.FromName);
            var to = new EmailAddress(mail.To);
            var msg = MailHelper.CreateSingleEmail(from, to, mail.Subject, mail.Body, mail.Body);
            client.SendEmailAsync(msg);
        }        
    }    
}
