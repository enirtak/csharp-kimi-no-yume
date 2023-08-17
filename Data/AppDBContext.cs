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
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Projects> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration

            // 1 Category : M Dream
            modelBuilder.Entity<DreamCategory>()
                .HasMany(x => x.DreamDictionary)
                .WithOne(o => o.DreamCategory)
                .HasForeignKey(p => p.DreamCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Person>()
                .ToTable(x => x.HasTrigger("trigger_person_updated_date"));

            modelBuilder.Entity<Address>()
                .ToTable(x => x.HasTrigger("trigger_address_updated_date"));

            modelBuilder.Entity<Employer>()
                .ToTable(x => x.HasTrigger("trigger_Employer_updated_date"));

            modelBuilder.Entity<WorkExperience>()
                .ToTable(x => x.HasTrigger("trigger_workexp_updated_date"));

            modelBuilder.Entity<Skills>()
                .ToTable(x => x.HasTrigger("trigger_skills_updated_date"));

            modelBuilder.Entity<Projects>()
                .ToTable(x => x.HasTrigger("trigger_projects_updated_date"));

            // 1 Person : M Address
            modelBuilder.Entity<Person>()
                .HasMany(x => x.Addresses)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            // 1 Person : M Employer
            modelBuilder.Entity<Person>()
                .HasMany(x => x.Addresses)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            // 1 Employee : M WorkExperiences
            modelBuilder.Entity<Employer>()
                .HasMany(x => x.WorkExperience)
                .WithOne(x => x.Employer)
                .HasForeignKey(x => x.EmployerId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            // 1 Person : M Skills
            modelBuilder.Entity<Person>()
                .HasMany(x => x.Skills)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            // 1 Person : M Projects
            modelBuilder.Entity<Person>()
                .HasMany(x => x.Projects)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:ApplicationDatabase");
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
