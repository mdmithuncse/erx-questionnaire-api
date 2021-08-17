using AutoMapper;
using MediatR;
using Model;
using Pagination;
using Pagination.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.AnswerTypeQuery
{
    public class GetAllAnswerTypeQuery : IRequest<PagedResult<AnswerTypeResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllAnswerTypeQueryHandler : IRequestHandler<GetAllAnswerTypeQuery, PagedResult<AnswerTypeResponse>>
        {
            private readonly IAppDbContext _context;

            public GetAllAnswerTypeQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<PagedResult<AnswerTypeResponse>> Handle(GetAllAnswerTypeQuery query, CancellationToken cancellationToken)
            {
                var result = await _context.AnswerTypes.GetPagedItemsAsync(query.Page, query.PageSize);

                if (result == null || !result.Items.Any())
                {
                    return default;
                }

                var items = new List<AnswerTypeResponse>();
                
                if (result.Items.Any())
                {
                    result.Items.ToList().ForEach(x => items.Add(new AnswerTypeResponse 
                    { 
                        Id = x.Id,
                        Created = x.Created,
                        Updated = x.Updated,
                        Type = x.Type
                    })); 
                }

                return new PagedResult<AnswerTypeResponse>
                {
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    PageCount = result.PageCount,
                    RowCount = result.RowCount,
                    Items = items
                };
            }
        }
    }
}
