using Microsoft.EntityFrameworkCore;
using ImageAndResume.Models;

namespace ImageAndResume.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<FileDocument> FileDocuments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FileDocument>().ToTable("FileDocuments");
        }
    }
}
