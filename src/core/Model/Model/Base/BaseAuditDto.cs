using System;

namespace Model.Base
{
    public class BaseAuditDto : IBaseAuditDto
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
