using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionQuery
{
    public class GetQuestionByIdQuery : IRequest<QuestionResponse>
    {
        public long Id;

        public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionResponse>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetQuestionByIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<QuestionResponse> Handle(GetQuestionByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _context.Questions.Include(x => x.QuestionGroup).Include(x => x.AnswerType).Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return default;
                }

                return _mapper.Map<QuestionResponse>(item);
            }
        }
    }
}
