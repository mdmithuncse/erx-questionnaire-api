using AutoMapper;
using MediatR;
using Model;
using Pagination;
using Pagination.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ParticipantAnswerQuery
{
    public class GetParticipantAnswerByQuestionIdQuery : IRequest<PagedResult<ParticipantAnswerResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long QuestionId { get; set; }

        public class GetParticipantAnswerByQuestionIdQueryHandler : IRequestHandler<GetParticipantAnswerByQuestionIdQuery, PagedResult<ParticipantAnswerResponse>>
        {
            private readonly IAppDbContext _context;

            public GetParticipantAnswerByQuestionIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<PagedResult<ParticipantAnswerResponse>> Handle(GetParticipantAnswerByQuestionIdQuery query, CancellationToken cancellationToken)
            {
                var result = await _context.ParticipantAnswers.Where(x => x.QuestionId == query.QuestionId).GetPagedItemsAsync(query.Page, query.PageSize); ;

                if (result == null || !result.Items.Any())
                {
                    return default;
                }

                var items = new List<ParticipantAnswerResponse>();

                if (result.Items.Any())
                {
                    result.Items.ToList().ForEach(x => items.Add(new ParticipantAnswerResponse
                    {
                        Id = x.Id,
                        Created = x.Created,
                        Updated = x.Updated,
                        QuestionId = x.QuestionId,
                        Question = new QuestionResponse
                        {
                            Id = x.Question.Id,
                            Created = x.Question.Created,
                            Updated = x.Question.Updated,
                            Quiz = x.Question.Quiz,
                            QuestionGroupId = x.Question.QuestionGroupId,
                            QuestionGroup = new QuestionGroupResponse
                            {
                                Id = x.Question.QuestionGroup.Id,
                                Created = x.Question.QuestionGroup.Created,
                                Updated = x.Question.QuestionGroup.Updated,
                                Name = x.Question.QuestionGroup.Name
                            }
                        },
                        AnswerId = x.AnswerId,
                        Answer = new AnswerResponse
                        {
                            Id = x.Answer.Id,
                            Created = x.Answer.Created,
                            Updated = x.Answer.Updated,
                            Result = x.Answer.Result
                        },
                        ParticipantId = x.ParticipantId,
                        Participant = new ParticipantResponse
                        {
                            Id = x.Participant.Id,
                            Created = x.Participant.Created,
                            Updated = x.Participant.Updated,
                            Name = x.Participant.Name,
                            Email = x.Participant.Email
                        }
                    }));
                }

                return new PagedResult<ParticipantAnswerResponse>
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
