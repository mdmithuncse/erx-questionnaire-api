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

namespace Application.CQRS.Queries.ParticipantQuery
{
    public class GetAllParticipantQuery : IRequest<PagedResult<ParticipantResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllParticipantQueryHandler : IRequestHandler<GetAllParticipantQuery, PagedResult<ParticipantResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAllParticipantQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedResult<ParticipantResponse>> Handle(GetAllParticipantQuery query, CancellationToken cancellationToken)
            {
                var result = await _context.Participants.GetPagedItemsAsync(query.Page, query.PageSize);

                if (result == null || !result.Items.Any())
                {
                    return default;
                }

                //var items = new List<ParticipantResponse>();

                //if (result.Items.Any())
                //{
                //    result.Items.ToList().ForEach(x => items.Add(new ParticipantResponse
                //    {
                //        Id = x.Id,
                //        Created = x.Created,
                //        Updated = x.Updated,
                //        Name = x.Name,
                //        Email = x.Email
                //    }));
                //}

                return new PagedResult<ParticipantResponse>
                {
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    PageCount = result.PageCount,
                    RowCount = result.RowCount,
                    Items = _mapper.Map<IList<ParticipantResponse>>(result.Items)
                };
            }
        }
    }
}
