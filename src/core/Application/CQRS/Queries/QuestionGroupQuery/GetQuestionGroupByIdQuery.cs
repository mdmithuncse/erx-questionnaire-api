using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionGroupQuery
{
    public class GetQuestionGroupByIdQuery : IRequest<QuestionGroup>
    {
        public long Id { get; set; }

        public class GetQuestionGroupByIdQueryHandler: IRequestHandler<GetQuestionGroupByIdQuery, QuestionGroup>
        {
            private readonly IAppDbContext _context;

            public GetQuestionGroupByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<QuestionGroup> Handle(GetQuestionGroupByIdQuery query, CancellationToken cancellationToken)
            {
                var questionGroup = await _context.QuestionGroups.Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (questionGroup == null)
                {
                    return default;
                }

                return questionGroup;
            }
        }
    }
}
