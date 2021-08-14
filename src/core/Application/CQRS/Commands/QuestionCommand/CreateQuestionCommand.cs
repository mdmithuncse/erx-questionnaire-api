using Common.Enums;
using Domain;
using Extension;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Application.CQRS.Commands.QuestionCommand
{
    public class CreateQuestionCommand : IRequest<long>
    {
        public long QuestionGroupId { get; set; }
        public string Quiz { get; set; }
        public long AnswerTypeId { get; set; }
        public AnswerSourceType AnswerSourceType { get; set; }
        public string AnswerDataSource { get; set; }

        public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, long>
        {
            private readonly IAppDbContext _context;

            public CreateQuestionCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
            {
                var question = new Question
                {
                    QuestionGroupId = command.QuestionGroupId,
                    Quiz = command.Quiz,
                    AnswerTypeId = command.AnswerTypeId,
                    AnswerSourceType = command.AnswerSourceType,
                    AnswerDataSource = command.AnswerSourceType == AnswerSourceType.Url ? command.AnswerDataSource : null
                };

                if (command.AnswerSourceType == AnswerSourceType.Text)
                {
                    var answers = new List<Answer>();
                    var answerList = StringExtension.GetListFromString(command.AnswerDataSource);

                    _context.Questions.Add(question);
                    await _context.SaveChangesAsync();

                    if (answerList.Any())
                    {
                        foreach (var answer in answerList)
                        {
                            answers.Add(new Answer { QuestionId = question.Id, Result = answer });
                        }
                    }

                    if (answers.Any())
                    {
                        _context.Answers.AddRange(answers);
                        await _context.SaveChangesAsync();
                    }

                    return question.Id;
                }

                _context.Questions.Add(question);
                await _context.SaveChangesAsync();

                return question.Id;
            }
        }
    }
}
