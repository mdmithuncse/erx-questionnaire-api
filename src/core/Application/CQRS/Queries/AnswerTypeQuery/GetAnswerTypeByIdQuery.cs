using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.AnswerTypeQuery
{
    public class GetAnswerTypeByIdQuery : IRequest<AnswerTypeResponse>
    {
        public long Id { get; set; }

        public class GetAnswerTypeByIdQueryHandler : IRequestHandler<GetAnswerTypeByIdQuery, AnswerTypeResponse>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAnswerTypeByIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AnswerTypeResponse> Handle(GetAnswerTypeByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _context.AnswerTypes.Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return default;
                }

                return _mapper.Map<AnswerTypeResponse>(item);
            }
        }
    }
}
