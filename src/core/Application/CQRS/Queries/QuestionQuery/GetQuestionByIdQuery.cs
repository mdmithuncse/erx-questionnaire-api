using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionQuery
{
    public class GetQuestionByIdQuery : IRequest<Question>
    {
        public long Id;

        public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, Question>
        {
            private readonly IAppDbContext _context;

            public GetQuestionByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Question> Handle(GetQuestionByIdQuery query, CancellationToken cancellationToken)
            {
                var question = await _context.Questions.Include(x => x.QuestionGroup).Include(x => x.AnswerType).Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (question == null)
                {
                    return default;
                }

                return question;
            }
        }
    }
}
