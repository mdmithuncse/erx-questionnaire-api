using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<QuestionGroup> QuestionGroups { get; set; }
        public DbSet<AnswerType> AnswerTypes { get; set; }

        public async Task<long> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
