namespace Domain.Base
{
    public class BaseIdAsString : BaseAudit, IBaseIdAsString
    {
        public string Id { get; set; }
    }
}
