using Common.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.AnswerTypeCommand
{
    public class UpdateAnswerTypeCommand : IRequest<long>
    {
        public long Id { get; set; }
        public InputType Type { get; set; }

        public class UpdateAnswerTypeCommandHandler : IRequestHandler<UpdateAnswerTypeCommand, long>
        {
            private readonly IAppDbContext _context;

            public UpdateAnswerTypeCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(UpdateAnswerTypeCommand command, CancellationToken cancellationToken)
            {
                var answerType = await _context.AnswerTypes.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (answerType == null)
                {
                    return default;
                }

                answerType.Type = command.Type;
                answerType.Updated = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return answerType.Id;
            }
        }
    }
}
