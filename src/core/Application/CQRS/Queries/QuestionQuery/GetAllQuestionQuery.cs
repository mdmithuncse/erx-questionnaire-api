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

namespace Application.CQRS.Queries.QuestionQuery
{
    public class GetAllQuestionQuery : IRequest<PagedResult<QuestionResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, PagedResult<QuestionResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAllQuestionQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedResult<QuestionResponse>> Handle(GetAllQuestionQuery query, CancellationToken cancellationToken)
            {
                var result = await _context.Questions.Include(x => x.QuestionGroup).Include(x => x.AnswerType).GetPagedItemsAsync(query.Page, query.PageSize);

                if (result == null || !result.Items.Any())
                {
                    return default;
                }

                //var items = new List<QuestionResponse>();

                //if (result.Items.Any())
                //{
                //    result.Items.ToList().ForEach(x => items.Add(new QuestionResponse
                //    {
                //        Id = x.Id,
                //        Created = x.Created,
                //        Updated = x.Updated,
                //        QuestionGroupId = x.QuestionGroupId,
                //        QuestionGroup = new QuestionGroupResponse 
                //        { 
                //            Id = x.QuestionGroup.Id,
                //            Created = x.QuestionGroup.Created,
                //            Updated = x.QuestionGroup.Updated,
                //            Name = x.QuestionGroup.Name
                //        },
                //        Quiz = x.Quiz,
                //        AnswerTypeId = x.AnswerTypeId,
                //        AnswerType = new AnswerTypeResponse
                //        {
                //            Id = x.AnswerType.Id,
                //            Created = x.AnswerType.Created,
                //            Updated = x.AnswerType.Updated,
                //            Type = x.AnswerType.Type
                //        },
                //        AnswerSourceType = x.AnswerSourceType,
                //        AnswerDataSource = x.AnswerDataSource
                //    }));
                //}

                return new PagedResult<QuestionResponse>
                {
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    PageCount = result.PageCount,
                    RowCount = result.RowCount,
                    Items = _mapper.Map<IList<QuestionResponse>>(result.Items)
                };
            }
        }
    }
}
