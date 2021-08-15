using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantAnswerQuery
{
    public class GetParticipantAnswerByParticipantIdQuery : IRequest<IEnumerable<ParticipantAnswerResponse>>
    {
        public long ParticipantId { get; set; }

        public class GetParticipantAnswerByParticipantIdQueryHandler : IRequestHandler<GetParticipantAnswerByParticipantIdQuery, IEnumerable<ParticipantAnswerResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetParticipantAnswerByParticipantIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ParticipantAnswerResponse>> Handle(GetParticipantAnswerByParticipantIdQuery query, CancellationToken cancellationToken)
            {
                var items = await _context.ParticipantAnswers.Where(x => x.ParticipantId == query.ParticipantId).ToListAsync();

                if (items == null || !items.Any())
                {
                    return default;
                }

                return _mapper.Map<IEnumerable<ParticipantAnswerResponse>>(items);
            }
        }
    }
}
