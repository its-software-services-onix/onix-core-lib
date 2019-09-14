using System;
using NUnit.Framework;

namespace Its.Onix.Core.Smtp
{
	public class SendGridSmtpContextTest
	{
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("pjame.fb@gmail.com", "noreply@unit-test.com")]
        public void SendEmailTest(string to, string from)
        {
            SendGridSmtpContext ctx = new SendGridSmtpContext();
            ctx.SetSmtpConfigByEnv("ONIX_SMTP_HOST", "ONIX_SMTP_PORT", "ONIX_SMTP_USER", "ONIX_SMTP_PASSWORD");

            Mail m = new Mail();
            m.From = from;
            m.To = to;
            m.Subject = "This is sent from unit test";
            m.IsHtmlContent = false;
            m.Body = "This is body text sent from unit test";
            m.BCC = "";
            m.CC = "";

            ctx.Send(m);
        }                      
    }
}
