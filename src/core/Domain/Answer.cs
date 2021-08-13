using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Answer : BaseIdAsLong
    {
        public long QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public string Result { get; set; }

        public ParticipantAnswer UserAnswer { get; set; }
    }
}
