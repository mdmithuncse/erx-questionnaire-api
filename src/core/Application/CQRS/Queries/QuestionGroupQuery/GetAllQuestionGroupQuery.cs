using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionGroupQuery
{
    public class GetAllQuestionGroupQuery : IRequest<IEnumerable<QuestionGroupResponse>>
    {
        public class GetAllQuestionGroupQueryHandler : IRequestHandler<GetAllQuestionGroupQuery, IEnumerable<QuestionGroupResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAllQuestionGroupQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<QuestionGroupResponse>> Handle(GetAllQuestionGroupQuery query, CancellationToken cancellationToken)
            {
                var items = await _context.QuestionGroups.ToListAsync();

                if (items == null || !items.Any())
                {
                    return default;
                }

                return _mapper.Map<IEnumerable<QuestionGroupResponse>>(items.AsReadOnly());
            }
        }
    }
}
