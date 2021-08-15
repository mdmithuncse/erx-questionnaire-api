using System;

namespace Model.Base
{
    public interface IBaseAuditDto
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
