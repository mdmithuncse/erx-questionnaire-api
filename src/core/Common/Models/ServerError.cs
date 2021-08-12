using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ServerError
    {
        public string TraceId { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
    }
}
