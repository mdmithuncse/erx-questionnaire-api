using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.QuestionCommand
{
    public class DeleteQuestionByIdCommand : IRequest<long>
    {
        public long Id { get; set; }

        public class DeleteQuestionByIdCommandHandler : IRequestHandler<DeleteQuestionByIdCommand, long>
        {
            private readonly IAppDbContext _context;

            public DeleteQuestionByIdCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(DeleteQuestionByIdCommand command, CancellationToken cancellationToken)
            {
                var question = await _context.Questions.Include(x => x.Answers).Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (question == null)
                {
                    return default;
                }

                _context.Answers.RemoveRange(question.Answers);
                _context.Questions.Remove(question);
                
                await _context.SaveChangesAsync();

                return question.Id;
            }
        }
    }
}
