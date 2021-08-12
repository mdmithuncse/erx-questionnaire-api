using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.QuestionGroupCommand
{
    public class UpdateQuestionGroupCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public class UpdateQuestionGroupCommandHandler : IRequestHandler<UpdateQuestionGroupCommand, long>
        {
            private readonly IAppDbContext _context;

            public UpdateQuestionGroupCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(UpdateQuestionGroupCommand command, CancellationToken cancellationToken)
            {
                var questionGroup = await _context.QuestionGroups.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (questionGroup == null)
                {
                    return default;
                }

                questionGroup.Name = command.Name;
                questionGroup.Updated = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return questionGroup.Id;
            }
        }
    }
}
