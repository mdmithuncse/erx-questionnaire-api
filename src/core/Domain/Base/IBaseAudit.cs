using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public interface IBaseAudit
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
