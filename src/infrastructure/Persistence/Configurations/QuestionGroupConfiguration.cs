using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class QuestionGroupConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<QuestionGroup>().ToTable("QuestionGroups").HasKey(e => e.Id);
            builder.Entity<QuestionGroup>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<QuestionGroup>().Property(e => e.Name).HasMaxLength(100).IsRequired();
        }
    }
}
