using Domain.Base;
using System.Collections.Generic;

namespace Domain
{
    public class QuestionGroup : BaseIdAsLong
    {
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
