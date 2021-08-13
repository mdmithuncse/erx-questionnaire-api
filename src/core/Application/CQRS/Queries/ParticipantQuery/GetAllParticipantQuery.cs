using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantQuery
{
    public class GetAllParticipantQuery : IRequest<IEnumerable<Participant>>
    {
        public class GetAllParticipantQueryHandler : IRequestHandler<GetAllParticipantQuery, IEnumerable<Participant>>
        {
            private readonly IAppDbContext _context;

            public GetAllParticipantQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Participant>> Handle(GetAllParticipantQuery query, CancellationToken cancellationToken)
            {
                var participantList = await _context.Participants.ToListAsync();

                if (participantList == null || !participantList.Any())
                {
                    return default;
                }

                return participantList.AsReadOnly();
            }
        }
    }
}
