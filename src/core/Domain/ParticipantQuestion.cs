using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class ParticipantQuestion : BaseIdAsLong
    {
        public long QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public long ParticipantId { get; set; }
        [ForeignKey("ParticipantId")]
        public Participant Participant { get; set; }
    }
}
