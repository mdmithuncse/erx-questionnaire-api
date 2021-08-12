namespace Domain.Base
{
    public class BaseIdAsLong : BaseAudit, IBaseIdAsLong
    {
        public long Id { get; set; }
    }
}
