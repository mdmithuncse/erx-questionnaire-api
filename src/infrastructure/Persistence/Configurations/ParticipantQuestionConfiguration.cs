using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class ParticipantQuestionConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<ParticipantQuestion>().ToTable("ParticipantQuestions").HasKey(e => e.Id);
            builder.Entity<ParticipantQuestion>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<ParticipantQuestion>().Property(e => e.QuestionId).IsRequired();
            builder.Entity<ParticipantQuestion>().Property(e => e.ParticipantId).IsRequired();
        }
    }
}
