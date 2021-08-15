using AutoMapper;
using Common.Enums;
using Domain;
using Model.Base;
using Model.Mapping;

namespace Model
{
    public class ParticipantResponse : BaseIdAsLongDto, IMapFrom<Participant>
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Participant, ParticipantResponse>();
        }
    }
}
