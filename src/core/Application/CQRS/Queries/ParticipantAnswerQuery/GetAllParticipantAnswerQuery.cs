using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantAnswerQuery
{
    public class GetAllParticipantAnswerQuery : IRequest<IEnumerable<ParticipantAnswer>>
    {
        public class GetAllParticipantAnswerQueryHandler : IRequestHandler<GetAllParticipantAnswerQuery, IEnumerable<ParticipantAnswer>>
        {
            private readonly IAppDbContext _context;

            public GetAllParticipantAnswerQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ParticipantAnswer>> Handle(GetAllParticipantAnswerQuery query, CancellationToken cancellationToken)
            {
                var participantAnswerList = await _context.ParticipantAnswers.ToListAsync();

                if (participantAnswerList == null || !participantAnswerList.Any())
                {
                    return default;
                }

                return participantAnswerList.AsReadOnly();
            }
        }
    }
}
