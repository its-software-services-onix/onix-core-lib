namespace Its.Onix.Core.Smtp
{
	public class Mail
	{
        public string From {get; set;}
        public string FromName {get; set;}
        public string To {get; set;}
        public string CC {get; set;}
        public string BCC {get; set;}
        public string Body {get; set;}
        public string Subject {get; set;}
        public bool IsHtmlContent {get; set;}        
    }    
}
