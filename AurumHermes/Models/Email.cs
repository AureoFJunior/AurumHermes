using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurumHermes.Models
{
    public class Email
    {
        public Email() { }

        public Email(string body, string subject, IEnumerable<string> recipientEmails)
        {
            Body = body;
            Subject = subject;
            RecipientEmails = recipientEmails;
        }

        public string Body { get; set; }
        public string Subject { get; set; }
        public IEnumerable<string> RecipientEmails { get; set; }
    }
}
