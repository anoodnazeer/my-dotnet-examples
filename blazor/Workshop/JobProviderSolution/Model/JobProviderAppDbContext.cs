using Microsoft.EntityFrameworkCore;

namespace JobProviderSolution.Model
{
    public class JobProviderAppDbContext : DbContext
    {
        public JobProviderAppDbContext(DbContextOptions<JobProviderAppDbContext> options)
          : base(options)
        {
        }

        public DbSet<JobProvider> JobProviders { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
