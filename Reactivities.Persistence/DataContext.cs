using Microsoft.EntityFrameworkCore;

namespace Reactivities.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Activity> Activities { get; set; }
    }
}