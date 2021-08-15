using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantAnswerQuery
{
    public class GetParticipantAnswerByQuestionIdQuery : IRequest<IEnumerable<ParticipantAnswer>>
    {
        public long QuestionId { get; set; }

        public class GetParticipantAnswerByQuestionIdQueryHandler : IRequestHandler<GetParticipantAnswerByQuestionIdQuery, IEnumerable<ParticipantAnswer>>
        {
            public IAppDbContext _context;

            public GetParticipantAnswerByQuestionIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ParticipantAnswer>> Handle(GetParticipantAnswerByQuestionIdQuery query, CancellationToken cancellationToken)
            {
                var participantAnswerList = await _context.ParticipantAnswers.Where(x => x.QuestionId == query.QuestionId).ToListAsync();

                if (participantAnswerList == null || !participantAnswerList.Any())
                {
                    return default;
                }

                return participantAnswerList.AsReadOnly();
            }
        }
    }
}
