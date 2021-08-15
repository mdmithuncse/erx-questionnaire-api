using AutoMapper;
using Common.Enums;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Service;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionQuery
{
    public class GetParticipantQuestionQuery : IRequest<QuestionResponse>
    {
        public long ParticipantId { get; set; }

        public class GetParticipantQuestionQueryHandler : IRequestHandler<GetParticipantQuestionQuery, QuestionResponse>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;
            private readonly ICountryService _service;

            public GetParticipantQuestionQueryHandler(IAppDbContext context, IMapper mapper, ICountryService service)
            {
                _context = context;
                _mapper = mapper;
                _service = service;
            }

            private static IList<Answer> BuildAnswers(IEnumerable<CountryResponse> inputs)
            {
                if (!inputs.Any())
                {
                    return default;
                }

                var answers = new List<Answer>();

                foreach (var input in inputs)
                {
                    answers.Add(new Answer
                    {
                        Result = input.Name
                    });
                }

                return answers;
            }

            private async Task<long> SaveParticipantQuestionAsync(long questionId, long participantId)
            {
                if (questionId <= 0 || participantId <= 0)
                {
                    return default;
                }

                var participantQuestion = new ParticipantQuestion
                {
                    QuestionId = questionId,
                    ParticipantId = participantId
                };

                _context.ParticipantQuestions.Add(participantQuestion);
                return await _context.SaveChangesAsync();
            }

            public async Task<QuestionResponse> Handle(GetParticipantQuestionQuery query, CancellationToken cancellationToken)
            {
                var participantQuestions = await _context.ParticipantQuestions.Where(x => x.ParticipantId == query.ParticipantId).ToListAsync();
                var questions = await _context.Questions.Include(x => x.QuestionGroup).Include(x => x.AnswerType).Include(x => x.Answers).ToListAsync();

                if (!participantQuestions.Any() && questions.Any())
                {
                    var question = questions.FirstOrDefault();

                    if (question.AnswerSourceType == AnswerSourceType.Url && question.Quiz.Contains("Country", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var countries = await _service.GetCountriesAsync();

                        if (countries.Any())
                        {
                            var answers = BuildAnswers(countries);

                            question.Answers = answers;
                        }
                    }

                    await SaveParticipantQuestionAsync(question.Id, query.ParticipantId);

                    return _mapper.Map<QuestionResponse>(question);
                }

                var unAssignedQuestions = questions.Where(x => !participantQuestions.Any(c => c.QuestionId == x.Id));

                if (unAssignedQuestions.Any())
                {
                    var question = unAssignedQuestions.FirstOrDefault();

                    if (question.AnswerSourceType == AnswerSourceType.Url && question.Quiz.Contains("Country", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var countries = await _service.GetCountriesAsync();

                        if (countries.Any())
                        {
                            var answers = BuildAnswers(countries);

                            question.Answers = answers;
                        }
                    }

                    await SaveParticipantQuestionAsync(question.Id, query.ParticipantId);

                    return _mapper.Map<QuestionResponse>(question);
                }

                return default;
            }
        }
    }
}
