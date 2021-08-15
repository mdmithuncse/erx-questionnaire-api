using Common.Enums;
using Domain.Base;

namespace Domain
{
    public class AnswerType : BaseIdAsLong
    {
        public InputType Type { get; set; }
    }
}
