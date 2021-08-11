using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public class BaseIdAsLong : BaseAudit, IBaseIdAsLong
    {
        public long Id { get; set; }
    }
}
