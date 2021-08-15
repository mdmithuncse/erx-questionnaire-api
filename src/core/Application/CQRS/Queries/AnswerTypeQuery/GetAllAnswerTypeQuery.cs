using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.AnswerTypeQuery
{
    public class GetAllAnswerTypeQuery : IRequest<IEnumerable<AnswerTypeResponse>>
    {
        public class GetAllAnswerTypeQueryHandler : IRequestHandler<GetAllAnswerTypeQuery, IEnumerable<AnswerTypeResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAllAnswerTypeQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<AnswerTypeResponse>> Handle(GetAllAnswerTypeQuery query, CancellationToken cancellationToken)
            {
                var items = await _context.AnswerTypes.ToListAsync();

                if (items == null || !items.Any())
                {
                    return default;
                }

                return _mapper.Map<IEnumerable<AnswerTypeResponse>>(items.AsReadOnly());
            }
        }
    }
}
