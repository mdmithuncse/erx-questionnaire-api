using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantAnswerQuery
{
    public class GetParticipantAnswerByParticipantIdQuery : IRequest<IEnumerable<ParticipantAnswer>>
    {
        public long ParticipantId { get; set; }

        public class GetParticipantAnswerByParticipantIdQueryHandler : IRequestHandler<GetParticipantAnswerByParticipantIdQuery, IEnumerable<ParticipantAnswer>>
        {
            public IAppDbContext _context;

            public GetParticipantAnswerByParticipantIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ParticipantAnswer>> Handle(GetParticipantAnswerByParticipantIdQuery query, CancellationToken cancellationToken)
            {
                var participantAnswerList = await _context.ParticipantAnswers.Where(x => x.ParticipantId == query.ParticipantId).ToListAsync();

                if (participantAnswerList == null || !participantAnswerList.Any())
                {
                    return default;
                }

                return participantAnswerList.AsReadOnly();
            }
        }
    }
}
