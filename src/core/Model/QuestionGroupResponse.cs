using AutoMapper;
using Domain;
using Model.Base;
using Model.Mapping;

namespace Model
{
    public class QuestionGroupResponse : BaseIdAsLongDto, IMapFrom<QuestionGroup>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<QuestionGroup, QuestionGroupResponse>();
        }
    }
}
