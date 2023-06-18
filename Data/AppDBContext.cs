using Microsoft.EntityFrameworkCore;
using proj_csharp_kiminoyume.Models;

namespace proj_csharp_kiminoyume.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base (options) { }

        public DbSet<DreamDictionary> DreamDictionaries { get; set; }
        public DbSet<DreamCategory> DreamCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamCategory>()
                .HasMany(x => x.DreamDictionary)
                .WithOne(o => o.DreamCategory)
                .HasForeignKey(p => p.DreamCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:ApplicationDatabase");
            }
        }
    }
}
