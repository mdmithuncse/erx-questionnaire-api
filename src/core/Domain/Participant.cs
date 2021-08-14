using Domain.Base;
using System.Collections.Generic;

namespace Domain
{
    public class Participant : BaseIdAsLong
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<ParticipantQuestion> ParticipantQuestions { get; set; }
        public ICollection<ParticipantAnswer> ParticipantAnswers { get; set; }
    }
}
