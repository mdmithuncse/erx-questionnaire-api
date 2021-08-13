using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppDbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerType> AnswerTypes { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantAnswer> ParticipantAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }

        public Task<long> SaveChangesAsync();
    }
}
