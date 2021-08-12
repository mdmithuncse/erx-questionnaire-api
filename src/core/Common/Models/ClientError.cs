using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ClientError
    {
        public string Message { get; set; }
        public string Field { get; set; }
        public string Code { get; set; }
        public dynamic CustomMessage { get; set; }
    }
}
