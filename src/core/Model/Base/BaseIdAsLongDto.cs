namespace Model.Base
{
    public class BaseIdAsLongDto : BaseAuditDto, IBaseIdAsLongDto
    {
        public long Id { get; set; }
    }
}
