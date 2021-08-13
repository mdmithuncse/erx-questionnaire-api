using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.ParticipantCommand
{
    public class DeleteParticipantByIdCommand : IRequest<long>
    {
        public long Id { get; set; }

        public class DeleteParticipantByIdCommandHandler : IRequestHandler<DeleteParticipantByIdCommand, long>
        {
            private readonly IAppDbContext _context;

            public DeleteParticipantByIdCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(DeleteParticipantByIdCommand command, CancellationToken cancellationToken)
            {
                var participant = await _context.Participants.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (participant == null)
                {
                    return default;
                }

                _context.Participants.Remove(participant);
                await _context.SaveChangesAsync();

                return participant.Id;
            }
        }
    }
}
