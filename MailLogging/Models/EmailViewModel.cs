using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailLogging.Models
{
    public class EmailViewModel
    {
            public string Email { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
        }
}