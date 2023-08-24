using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace muddraWebApp.Contexts
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        //public DbSet<UserProfileEntity> UserProfiles { get; set; }
        //public DbSet<AddressEntity> Addresses { get; set; }
        //public DbSet<ContactFormEntity> ContactForm { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
