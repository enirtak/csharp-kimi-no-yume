using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Models.JobApplication;

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
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobAppCustomField> JobApplicationCustomFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            NavigationProperties(modelBuilder);
            IgnorePropertiesOnUpdate(modelBuilder);
            SQLTriggers(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void NavigationProperties(ModelBuilder modelBuilder)
        {
            // https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration

            #region Navigation Properties

            // 1 Category : M Dream
            var dreamCategory = modelBuilder.Entity<DreamCategory>();

            dreamCategory
                .HasMany(x => x.DreamDictionary)
                .WithOne(o => o.DreamCategory)
                .HasForeignKey(p => p.DreamCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

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
            #endregion
        }

        private void IgnorePropertiesOnUpdate(ModelBuilder modelBuilder)
        {
            #region Ignore Properties on Update https://stackoverflow.com/a/49926351
            modelBuilder.Entity<DreamCategory>(builder =>
            {
                builder.Property(x => x.CreatedDate).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                builder.Property(x => x.CreatedBy).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            modelBuilder.Entity<DreamDictionary>(builder =>
            {
                builder.Property(x => x.CreatedDate).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                builder.Property(x => x.CreatedBy).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });
            #endregion
        }

        private void SQLTriggers(ModelBuilder modelBuilder)
        {
            #region Triggers
            modelBuilder.Entity<DreamCategory>()
                .ToTable(x => x.HasTrigger("trigger_category_updated_date"));

            modelBuilder.Entity<DreamDictionary>()
                .ToTable(x => x.HasTrigger("trigger_dictionary_updated_date"));

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

            modelBuilder.Entity<JobApplication>()
                .ToTable(x => x.HasTrigger("trigger_jobapp_updated_date"));
            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseSqlServer("name=ConnectionStrings:ApplicationDatabase");
            }
        }
    }
}
