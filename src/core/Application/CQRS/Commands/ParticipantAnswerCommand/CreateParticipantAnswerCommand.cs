using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.ParticipantAnswerCommand
{
    public class CreateParticipantAnswerCommand : IRequest<long>
    {
        public long QuestionId { get; set; }
        public long AnswerId { get; set; }
        public long ParticipantId { get; set; }

        public class CreateParticipantAnswerCommandHandler : IRequestHandler<CreateParticipantAnswerCommand, long>
        {
            private readonly IAppDbContext _context;

            public CreateParticipantAnswerCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateParticipantAnswerCommand command, CancellationToken cancellationToken)
            {
                var questionList = await _context.Questions.Include(x => x.Answers).Where(x => x.Quiz.Contains("Country", StringComparison.InvariantCultureIgnoreCase)).ToListAsync();

                if (questionList.Any(x => x.Id == command.QuestionId && 
                                     (x.Answers.Any(x => x.Result.Contains("Cambodia", StringComparison.InvariantCultureIgnoreCase)) ||
                                      x.Answers.Any(x => x.Result.Contains("Myanmar", StringComparison.InvariantCultureIgnoreCase)) ||
                                      x.Answers.Any(x => x.Result.Contains("Pakistan", StringComparison.InvariantCultureIgnoreCase)))))
                {
                    return default;
                }
                
                var participantAnswer = new ParticipantAnswer
                {
                    QuestionId = command.QuestionId,
                    AnswerId = command.AnswerId,
                    ParticipantId = command.ParticipantId
                };

                _context.ParticipantAnswers.Add(participantAnswer);
                await _context.SaveChangesAsync();

                return participantAnswer.Id;
            }
        }
    }
}
