using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class AnswerConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<Answer>().ToTable("Answers").HasKey(e => e.Id);
            builder.Entity<Answer>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<Answer>().Property(e => e.QuestionId).IsRequired();
            builder.Entity<Answer>().Property(e => e.Result).HasMaxLength(200).IsRequired();
        }
    }
}
