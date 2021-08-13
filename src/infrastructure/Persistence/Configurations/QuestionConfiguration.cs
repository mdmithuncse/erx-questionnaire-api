using Domain;
using Microsoft.EntityFrameworkCore;
namespace Persistence.Configurations
{
    public class QuestionConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<Question>().ToTable("Questions").HasKey(e => e.Id);
            builder.Entity<Question>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<Question>().Property(e => e.QuestionGroupId).IsRequired();
            builder.Entity<Question>().Property(e => e.Quiz).HasMaxLength(100).IsRequired();
            builder.Entity<Question>().Property(e => e.AnswerTypeId).IsRequired().HasConversion<int>();
            builder.Entity<Question>().Property(e => e.AnswerSourceType).IsRequired().HasConversion<int>();
            builder.Entity<Question>().Property(e => e.AnswerDataSource).HasMaxLength(500);
        }
    }
}
