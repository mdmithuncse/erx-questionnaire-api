using Domain;
using MediatR;
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
