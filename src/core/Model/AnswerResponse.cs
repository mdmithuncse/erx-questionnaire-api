using AutoMapper;
using Domain;
using Model.Base;
using Model.Mapping;

namespace Model
{
    public class AnswerResponse : BaseIdAsLongDto, IMapFrom<Answer>
    {
        public long QuestionId { get; set; }
        public QuestionResponse Question { get; set; }

        public string Result { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Answer, AnswerResponse>();
        }
    }
}
