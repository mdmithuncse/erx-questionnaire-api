using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.ParticipantCommand
{
    public class CreateParticipantCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public class CreateParticipantCommandHandler : IRequestHandler<CreateParticipantCommand, long>
        {
            private readonly IAppDbContext _context;

            public CreateParticipantCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateParticipantCommand command, CancellationToken cancellationToken)
            {
                var participant = new Participant
                {
                    Name = command.Name,
                    Email = command.Email
                };

                _context.Participants.Add(participant);
                await _context.SaveChangesAsync();

                return participant.Id;
            }
        }
    }
}
