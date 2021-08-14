using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionQuery
{
    public class GetAllQuestionQuery : IRequest<IEnumerable<Question>>
    {
        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, IEnumerable<Question>>
        {
            private readonly IAppDbContext _context;

            public GetAllQuestionQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Question>> Handle(GetAllQuestionQuery query, CancellationToken cancellationToken)
            {
                var questionList = await _context.Questions.Include(x => x.QuestionGroup).Include(x => x.AnswerType).ToListAsync();

                if (questionList == null || !questionList.Any())
                {
                    return default;
                }

                return questionList.AsReadOnly();
            }
        }
    }
}
