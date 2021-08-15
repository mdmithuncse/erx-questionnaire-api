using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantQuery
{
    public class GetParticipantByIdQuery : IRequest<ParticipantResponse>
    {
        public long Id { get; set; }

        public class GetParticipantByIdQueryHandler : IRequestHandler<GetParticipantByIdQuery, ParticipantResponse>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetParticipantByIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ParticipantResponse> Handle(GetParticipantByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _context.Participants.Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return default;
                }

                return _mapper.Map<ParticipantResponse>(item);
            }
        }
    }
}
