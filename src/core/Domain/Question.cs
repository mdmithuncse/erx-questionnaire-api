using Common.Enums;
using Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Question : BaseIdAsLong
    {
        public long QuestionGroupId { get; set; }
        [ForeignKey("QuestionGroupId")]
        public QuestionGroup QuestionGroup { get; set; }

        public string Quiz { get; set; }

        public long AnswerTypeId { get; set; }
        [ForeignKey("AnswerTypeId")]
        public AnswerType AnswerType { get; set; }

        public AnswerSourceType AnswerSourceType { get; set; }

        public string AnswerDataSource { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ParticipantAnswer UserAnswer { get; set; }
    }
}
