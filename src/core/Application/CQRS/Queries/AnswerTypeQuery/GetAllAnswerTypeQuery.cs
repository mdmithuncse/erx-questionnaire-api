using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.AnswerTypeQuery
{
    public class GetAllAnswerTypeQuery : IRequest<IEnumerable<AnswerType>>
    {
        public class GetAllAnswerTypeQueryHandler : IRequestHandler<GetAllAnswerTypeQuery, IEnumerable<AnswerType>>
        {
            private readonly IAppDbContext _context;

            public GetAllAnswerTypeQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<AnswerType>> Handle(GetAllAnswerTypeQuery query, CancellationToken cancellationToken)
            {
                var answerTypeList = await _context.AnswerTypes.ToListAsync();

                if (answerTypeList == null || !answerTypeList.Any())
                {
                    return default;
                }

                return answerTypeList.AsReadOnly();
            }
        }
    }
}
