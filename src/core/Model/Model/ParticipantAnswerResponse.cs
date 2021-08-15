using AutoMapper;
using Domain;
using Model.Base;
using Model.Mapping;

namespace Model
{
    public class ParticipantAnswerResponse : BaseIdAsLongDto, IMapFrom<ParticipantAnswer>
    {
        public long QuestionId { get; set; }
        public QuestionResponse Question { get; set; }

        public long AnswerId { get; set; }
        public AnswerResponse Answer { get; set; }

        public long ParticipantId { get; set; }
        public ParticipantResponse Participant { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Participant, ParticipantResponse>();
        }
    }
}
