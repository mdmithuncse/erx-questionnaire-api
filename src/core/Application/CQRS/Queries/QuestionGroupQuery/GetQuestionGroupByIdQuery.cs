using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionGroupQuery
{
    public class GetQuestionGroupByIdQuery : IRequest<QuestionGroupResponse>
    {
        public long Id { get; set; }

        public class GetQuestionGroupByIdQueryHandler: IRequestHandler<GetQuestionGroupByIdQuery, QuestionGroupResponse>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetQuestionGroupByIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<QuestionGroupResponse> Handle(GetQuestionGroupByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _context.QuestionGroups.Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return default;
                }

                return _mapper.Map<QuestionGroupResponse>(item);
            }
        }
    }
}
