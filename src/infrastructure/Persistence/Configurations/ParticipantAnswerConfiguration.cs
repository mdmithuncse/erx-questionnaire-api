using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class ParticipantAnswerConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<ParticipantAnswer>().ToTable("ParticipantAnswers").HasKey(e => e.Id);
            builder.Entity<ParticipantAnswer>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<ParticipantAnswer>().Property(e => e.QuestionId).IsRequired();
            builder.Entity<ParticipantAnswer>().Property(e => e.AnswerId).IsRequired();
            builder.Entity<ParticipantAnswer>().Property(e => e.ParticipantId).IsRequired();
        }
    }
}
