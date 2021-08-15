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
    public class GetParticipantAnswerByQuestionIdQuery : IRequest<IEnumerable<ParticipantAnswerResponse>>
    {
        public long QuestionId { get; set; }

        public class GetParticipantAnswerByQuestionIdQueryHandler : IRequestHandler<GetParticipantAnswerByQuestionIdQuery, IEnumerable<ParticipantAnswerResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetParticipantAnswerByQuestionIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ParticipantAnswerResponse>> Handle(GetParticipantAnswerByQuestionIdQuery query, CancellationToken cancellationToken)
            {
                var items = await _context.ParticipantAnswers.Where(x => x.QuestionId == query.QuestionId).ToListAsync();

                if (items == null || !items.Any())
                {
                    return default;
                }

                return _mapper.Map<IEnumerable<ParticipantAnswerResponse>>(items.AsReadOnly());
            }
        }
    }
}
