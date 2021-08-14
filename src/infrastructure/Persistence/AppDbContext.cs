using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerType> AnswerTypes { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantQuestion> ParticipantQuestions { get; set; }
        public DbSet<ParticipantAnswer> ParticipantAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }

        public async Task<long> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            AnswerConfiguration.OnModelCreating(builder);
            AnswerTypeConfiguration.OnModelCreating(builder);
            ParticipantConfiguration.OnModelCreating(builder);
            ParticipantQuestionConfiguration.OnModelCreating(builder);
            ParticipantAnswerConfiguration.OnModelCreating(builder);
            QuestionConfiguration.OnModelCreating(builder);
            QuestionGroupConfiguration.OnModelCreating(builder);
        }
    }
}
