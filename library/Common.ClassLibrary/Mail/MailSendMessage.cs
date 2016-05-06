using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary.Mail
{
    public class MailSendMessage : System.Net.Mail.MailMessage
    {
        public String MailTemplate{get;set;}
    }
}
