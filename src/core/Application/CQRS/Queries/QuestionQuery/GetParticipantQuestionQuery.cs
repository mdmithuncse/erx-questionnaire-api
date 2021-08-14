using Common.Enums;
using Domain;
using Extension;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionQuery
{
    public class GetParticipantQuestionQuery : IRequest<Question>
    {
        public long ParticipantId { get; set; }

        public class GetParticipantQuestionQueryHandler : IRequestHandler<GetParticipantQuestionQuery, Question>
        {
            private readonly IAppDbContext _context;
            private readonly HttpClient _client;

            public GetParticipantQuestionQueryHandler(IAppDbContext context, HttpClient client)
            {
                _context = context;
                _client = client;
            }

            public async Task<Question> Handle(GetParticipantQuestionQuery query, CancellationToken cancellationToken)
            {
                var participantQuestions = await _context.ParticipantQuestions.Where(x => x.ParticipantId == query.ParticipantId).ToListAsync();
                var questions = await _context.Questions.Include(x => x.QuestionGroup).Include(x => x.AnswerType).Include(x => x.Answers).ToListAsync();

                if (!participantQuestions.Any() && questions.Any())
                {
                    var question = questions.FirstOrDefault();

                    if (question.AnswerSourceType == AnswerSourceType.Url)
                    {
                        var response = await _client.GetAsync("https://restcountries.eu/rest/v2/all");

                        if (response.IsSuccessStatusCode)
                        {
                            var answers = await response.Deserialize<Answer>();
                        }
                    }

                    var participantQuestion = new ParticipantQuestion
                    {
                        QuestionId = question.Id,
                        ParticipantId = query.ParticipantId
                    };

                    _context.ParticipantQuestions.Add(participantQuestion);
                    await _context.SaveChangesAsync();

                    return question;
                }

                var unAssignedQuestions = questions.Where(x => !participantQuestions.Any(c => c.QuestionId == x.Id));

                if (unAssignedQuestions.Any())
                {
                    var question = unAssignedQuestions.FirstOrDefault();

                    if (question.AnswerSourceType == AnswerSourceType.Url)
                    {
                        var response = await _client.GetAsync("https://restcountries.eu/rest/v2/all");

                        if (response.IsSuccessStatusCode)
                        {
                            var answers = await response.Deserialize<Answer>();
                        }
                    }

                    var participantQuestion = new ParticipantQuestion
                    {
                        QuestionId = question.Id,
                        ParticipantId = query.ParticipantId
                    };

                    _context.ParticipantQuestions.Add(participantQuestion);
                    await _context.SaveChangesAsync();

                    return question;
                }

                return default;
            }
        }
    }
}
