using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.ParticipantCommand
{
    public class UpdateParticipantCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public class UpdateParticipantCommandHandler : IRequestHandler<UpdateParticipantCommand, long>
        {
            private readonly IAppDbContext _context;

            public UpdateParticipantCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(UpdateParticipantCommand command, CancellationToken cancellationToken)
            {
                var participant = await _context.Participants.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (participant == null)
                {
                    return default;
                }

                participant.Name = command.Name;
                participant.Email = command.Email;
                participant.Updated = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return participant.Id;
            }
        }
    }
}
