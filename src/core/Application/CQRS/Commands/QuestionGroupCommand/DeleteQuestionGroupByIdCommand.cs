using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.QuestionGroupCommand
{
    public class DeleteQuestionGroupByIdCommand : IRequest<long>
    {
        public long Id { get; set; }

        public class DeleteQuestionGroupByIdCommandHandler : IRequestHandler<DeleteQuestionGroupByIdCommand, long>
        {
            private readonly IAppDbContext _context;

            public DeleteQuestionGroupByIdCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(DeleteQuestionGroupByIdCommand command, CancellationToken cancellationToken)
            {
                var questionGroup = await _context.QuestionGroups.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (questionGroup == null)
                {
                    return default;
                }

                _context.QuestionGroups.Remove(questionGroup);
                await _context.SaveChangesAsync();

                return questionGroup.Id;
            }
        }
    }
}
