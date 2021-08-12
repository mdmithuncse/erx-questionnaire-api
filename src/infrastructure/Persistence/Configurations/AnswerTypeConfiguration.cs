using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class AnswerTypeConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<AnswerType>().ToTable("AnswerTypes").HasKey(e => e.Id);
            builder.Entity<AnswerType>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<AnswerType>().Property(e => e.Type).IsRequired().HasConversion<int>();
        }
    }
}
