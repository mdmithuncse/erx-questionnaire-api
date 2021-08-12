using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionGroupQuery
{
    public class GetAllQuestionGroupQuery : IRequest<IEnumerable<QuestionGroup>>
    {
        public class GetAllQuestionGroupQueryHandler : IRequestHandler<GetAllQuestionGroupQuery, IEnumerable<QuestionGroup>>
        {
            private readonly IAppDbContext _context;

            public GetAllQuestionGroupQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<QuestionGroup>> Handle(GetAllQuestionGroupQuery query, CancellationToken cancellationToken)
            {
                var questionGroupList = await _context.QuestionGroups.ToListAsync();

                if (questionGroupList == null || !questionGroupList.Any())
                {
                    return default;
                }

                return questionGroupList.AsReadOnly();
            }
        }
    }
}
