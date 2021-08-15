using AutoMapper;
using Domain;
using Model.Base;
using Model.Mapping;

namespace Model
{
    public class ParticipantQuestionResponse : BaseIdAsLongDto, IMapFrom<ParticipantQuestion>
    {
        public long QuestionId { get; set; }
        public QuestionResponse Question { get; set; }

        public long ParticipantId { get; set; }
        public ParticipantResponse Participant { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ParticipantQuestion, ParticipantQuestionResponse>();
        }
    }
}
