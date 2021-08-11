using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public class BaseAudit : IBaseAudit
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
    }
}
