using Satrack.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Satrack.Identity.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Satrack.Identity.Persistence
{
    public class SatrackManagerIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SatrackManagerIdentityDbContext(DbContextOptions<SatrackManagerIdentityDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

        }
    }
}
