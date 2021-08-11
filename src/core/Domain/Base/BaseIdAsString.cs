using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public class BaseIdAsString : BaseAudit, IBaseIdAsString
    {
        public string Id { get; set; }
    }
}
