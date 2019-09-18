using System;
using NUnit.Framework;
using Its.Onix.Core.Factories;

namespace Its.Onix.Core.Smtp
{
	public class SendGridSmtpContextTest
	{
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("pjame.fb@gmail.com", "noreply@unit-test.com", null)]
        [TestCase("pjame.fb@gmail.com", "noreply@unit-test.com", "Seubpong Monsar")]
        public void SendEmailTest(string to, string from, string fromName)
        {
            var ctx = FactorySmtpContext.CreateSmtpObject("SendGridSmtpContext");
            ctx.SetSmtpConfigByEnv("ONIX_SMTP_HOST", "ONIX_SMTP_PORT", "ONIX_SMTP_USER", "ONIX_SMTP_PASSWORD");

            Mail m = new Mail();
            m.FromName = fromName;
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
