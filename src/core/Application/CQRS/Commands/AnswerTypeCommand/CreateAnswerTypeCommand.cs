using Common.Enums;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.AnswerTypeCommand
{
    public class CreateAnswerTypeCommand : IRequest<long>
    {
        public InputType Type { get; set; }

        public class CreateAnswerTypeCommandHandler : IRequestHandler<CreateAnswerTypeCommand, long>
        {
            private readonly IAppDbContext _context;

            public CreateAnswerTypeCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateAnswerTypeCommand command, CancellationToken cancellationToken)
            {
                var answerType = new AnswerType
                {
                    Type = command.Type
                };

                _context.AnswerTypes.Add(answerType);
                await _context.SaveChangesAsync();

                return answerType.Id;
            }
        }
    }
}
