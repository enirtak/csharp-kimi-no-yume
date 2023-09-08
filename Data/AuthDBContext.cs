using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace proj_csharp_kiminoyume.Data
{
    public class AuthDBContext: IdentityUserContext<IdentityUser>
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> opt): base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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
