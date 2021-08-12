using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.QuestionGroupCommand
{
    public class CreateQuestionGroupCommand : IRequest<long>
    {
        public string Name { get; set; }

        public class CreateQuestionGroupCommandHandler : IRequestHandler<CreateQuestionGroupCommand, long>
        {
            private readonly IAppDbContext _context;

            public CreateQuestionGroupCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateQuestionGroupCommand command, CancellationToken cancellationToken)
            {
                var questionGroup = new QuestionGroup
                {
                    Name = command.Name
                };

                _context.QuestionGroups.Add(questionGroup);
                await _context.SaveChangesAsync();

                return questionGroup.Id;
            }
        }
    }
}
