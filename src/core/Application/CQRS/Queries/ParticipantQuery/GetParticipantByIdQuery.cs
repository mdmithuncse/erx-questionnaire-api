using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantQuery
{
    public class GetParticipantByIdQuery : IRequest<Participant>
    {
        public long Id { get; set; }

        public class GetParticipantByIdQueryHandler : IRequestHandler<GetParticipantByIdQuery, Participant>
        {
            private readonly IAppDbContext _context;

            public GetParticipantByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Participant> Handle(GetParticipantByIdQuery query, CancellationToken cancellationToken)
            {
                var participant = await _context.Participants.Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (participant == null)
                {
                    return default;
                }

                return participant;
            }
        }
    }
}
