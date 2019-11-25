using System;
using NUnit.Framework;

namespace Its.Onix.Core.Notifications
{
	public class LineNotificationTest
	{
        [SetUp]
        public void Setup()
        {
        }

        [TestCase]
        public void SendLineNotificationTest()
        {
            LineNotification line = new LineNotification();
            line.SetNotificationToken("geplFQ3esmEKr69ZxVQIJO3gIDGoEd8s3a6mkXEgNiX");
            line.Send("OnixCore unit testing SendLineNotificationTest()");
        } 

        [TestCase]
        public void SendLineNotificationFailedTest()
        {
            LineNotification line = new LineNotification();
            
            //To cover the code coverage
            line.SetLogger(null);
            line.GetLogger();

            line.SetNotificationToken("FAKED_TOKEN_HERE");
            line.Send("OnixCore unit testing SendLineNotificationTest()");
        }         
    }
}
