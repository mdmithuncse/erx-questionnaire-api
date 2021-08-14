using Common.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.QuestionCommand
{
    public class UpdateQuestionCommand : IRequest<long>
    {
        public long Id { get; set; }
        public long QuestionGroupId { get; set; }
        public string Quiz { get; set; }
        public long AnswerTypeId { get; set; }
        public AnswerSourceType AnswerSourceType { get; set; }
        public string AnswerDataSource { get; set; }

        public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, long>
        {
            private readonly IAppDbContext _context;

            public UpdateQuestionCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(UpdateQuestionCommand command, CancellationToken cancellationToken)
            {
                var question = await _context.Questions.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (question == null)
                {
                    return default;
                }

                question.QuestionGroupId = command.QuestionGroupId;
                question.Quiz = command.Quiz;
                question.AnswerTypeId = command.AnswerTypeId;
                question.AnswerSourceType = command.AnswerSourceType;
                question.AnswerDataSource = command.AnswerSourceType == AnswerSourceType.Url ? command.AnswerDataSource : null;
                question.Updated = DateTime.UtcNow;

                if (question.AnswerSourceType == AnswerSourceType.Text && question.AnswerSourceType != command.AnswerSourceType)
                {
                    // Todo: Delete all existing answers
                }

                if (question.AnswerSourceType == AnswerSourceType.Url && question.AnswerSourceType != command.AnswerSourceType)
                {
                    // Todo: Insert all new answers
                }

                await _context.SaveChangesAsync();

                return question.Id;
            }
        }
    }
}
