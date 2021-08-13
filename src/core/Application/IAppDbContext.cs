using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppDbContext
    {
        DbSet<QuestionGroup> QuestionGroups { get; set; }
        DbSet<AnswerType> AnswerTypes { get; set; }
        DbSet<Participant> Participants { get; set; }

        Task<long> SaveChangesAsync();
    }
}
