using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Pagination;
using Pagination.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionGroupQuery
{
    public class GetAllQuestionGroupQuery : IRequest<PagedResult<QuestionGroupResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllQuestionGroupQueryHandler : IRequestHandler<GetAllQuestionGroupQuery, PagedResult<QuestionGroupResponse>>
        {
            private readonly IAppDbContext _context;

            public GetAllQuestionGroupQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<PagedResult<QuestionGroupResponse>> Handle(GetAllQuestionGroupQuery query, CancellationToken cancellationToken)
            {
                var result = await _context.QuestionGroups.GetPagedItemsAsync(query.Page, query.PageSize);

                if (result == null || !result.Items.Any())
                {
                    return default;
                }

                var items = new List<QuestionGroupResponse>();

                if (result.Items.Any())
                {
                    result.Items.ToList().ForEach(x => items.Add(new QuestionGroupResponse
                    {
                        Id = x.Id,
                        Created = x.Created,
                        Updated = x.Updated,
                        Name = x.Name
                    }));
                }

                return new PagedResult<QuestionGroupResponse>
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
