using AutoMapper;
using Common.Enums;
using Domain;
using Model.Base;
using Model.Mapping;

namespace Model
{
    public class QuestionResponse : BaseIdAsLongDto, IMapFrom<Question>
    {
        public long QuestionGroupId { get; set; }
        public QuestionGroupResponse QuestionGroup { get; set; }
        public string Quiz { get; set; }
        public long AnswerTypeId { get; set; }
        public AnswerTypeResponse AnswerType { get; set; }
        public AnswerSourceType AnswerSourceType { get; set; }
        public string AnswerDataSource { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Question, QuestionResponse>();
        }
    }
}
