using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ParticipantAnswer : BaseIdAsLong
    {
        public long QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public long AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }

        public long ParticipantId { get; set; }
        [ForeignKey("ParticipantId")]
        public Participant Participant { get; set; }
    }
}
