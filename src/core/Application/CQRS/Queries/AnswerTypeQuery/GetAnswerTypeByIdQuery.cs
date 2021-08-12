using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.AnswerTypeQuery
{
    public class GetAnswerTypeByIdQuery : IRequest<AnswerType>
    {
        public long Id { get; set; }

        public class GetAnswerTypeByIdQueryHandler : IRequestHandler<GetAnswerTypeByIdQuery, AnswerType>
        {
            private readonly IAppDbContext _context;

            public GetAnswerTypeByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<AnswerType> Handle(GetAnswerTypeByIdQuery query, CancellationToken cancellationToken)
            {
                var answerType = await _context.AnswerTypes.Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (answerType == null)
                {
                    return default;
                }

                return answerType;
            }
        }
    }
}
