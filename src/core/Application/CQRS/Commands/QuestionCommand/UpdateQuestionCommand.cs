using Common.Enums;
using Domain;
using Extension;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

                if (question.AnswerSourceType == AnswerSourceType.Text && question.AnswerSourceType != command.AnswerSourceType)
                {
                    var answers = await _context.Answers.Where(x => x.QuestionId == question.Id).ToListAsync();
                    _context.Answers.RemoveRange(answers);
                }

                if (question.AnswerSourceType == AnswerSourceType.Url && question.AnswerSourceType != command.AnswerSourceType)
                {
                    var answers = new List<Answer>();
                    var answerList = StringExtension.GetListFromString(command.AnswerDataSource);

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
                    }
                }

                question.QuestionGroupId = command.QuestionGroupId;
                question.Quiz = command.Quiz;
                question.AnswerTypeId = command.AnswerTypeId;
                question.AnswerSourceType = command.AnswerSourceType;
                question.AnswerDataSource = command.AnswerSourceType == AnswerSourceType.Url ? command.AnswerDataSource : null;
                question.Updated = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();

                return question.Id;
            }
        }
    }
}
