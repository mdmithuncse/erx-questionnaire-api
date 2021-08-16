using AutoMapper;
using Common.Enums;
using Domain;
using Model.Base;
using Model.Mapping;

namespace Model
{
    public class AnswerTypeResponse : BaseIdAsLongDto, IMapFrom<AnswerType>
    {
        public InputType Type { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnswerType, AnswerTypeResponse>();
        }
    }
}
