using Common.Enums;
using Domain.Base;
using System.Collections.Generic;

namespace Domain
{
    public class AnswerType : BaseIdAsLong
    {
        public InputType Type { get; set; }
    }
}
