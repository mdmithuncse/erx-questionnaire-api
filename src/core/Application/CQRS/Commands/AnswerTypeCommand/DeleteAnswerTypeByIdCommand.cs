using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.AnswerTypeCommand
{
    public class DeleteAnswerTypeByIdCommand : IRequest<long>
    {
        public long Id { get; set; }

        public class DeleteAnswerTypeByIdCommandHandler : IRequestHandler<DeleteAnswerTypeByIdCommand, long>
        {
            private readonly IAppDbContext _context;

            public DeleteAnswerTypeByIdCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(DeleteAnswerTypeByIdCommand command, CancellationToken cancellationToken)
            {
                var answerType = await _context.AnswerTypes.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (answerType == null)
                {
                    return default;
                }

                _context.AnswerTypes.Remove(answerType);
                await _context.SaveChangesAsync();

                return answerType.Id;
            }
        }
    }
}
