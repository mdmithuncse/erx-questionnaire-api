using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantQuery
{
    public class GetAllParticipantQuery : IRequest<IEnumerable<ParticipantResponse>>
    {
        public class GetAllParticipantQueryHandler : IRequestHandler<GetAllParticipantQuery, IEnumerable<ParticipantResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAllParticipantQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ParticipantResponse>> Handle(GetAllParticipantQuery query, CancellationToken cancellationToken)
            {
                var items = await _context.Participants.ToListAsync();

                if (items == null || !items.Any())
                {
                    return default;
                }

                return _mapper.Map<IEnumerable<ParticipantResponse>>(items.AsReadOnly());
            }
        }
    }
}
