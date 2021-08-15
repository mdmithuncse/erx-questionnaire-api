using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionQuery
{
    public class GetAllQuestionQuery : IRequest<IEnumerable<QuestionResponse>>
    {
        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, IEnumerable<QuestionResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAllQuestionQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<QuestionResponse>> Handle(GetAllQuestionQuery query, CancellationToken cancellationToken)
            {
                var items = await _context.Questions.Include(x => x.QuestionGroup).Include(x => x.AnswerType).ToListAsync();

                if (items == null || !items.Any())
                {
                    return default;
                }

                return _mapper.Map<IEnumerable<QuestionResponse>>(items.AsReadOnly());
            }
        }
    }
}
