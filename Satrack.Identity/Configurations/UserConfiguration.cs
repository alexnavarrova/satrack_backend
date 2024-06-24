using Satrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Satrack.Identity.Models;

namespace Satrack.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "f284b3fd-f2cf-476e-a9b6-6560689cc48c",
                        Email = "admin@locahost.com",
                        NormalizedEmail = "admin@locahost.com",
                        Name = "Administrator",
                        Lastname = "admin",
                        UserName = "admin",
                        NormalizedUserName = "admin",
                        PasswordHash = hasher.HashPassword(null, "Admin2050$"),
                        EmailConfirmed = true,
                    },
                    new ApplicationUser
                    {
                        Id = "294d249b-9b57-48c1-9689-11a91abb6447",
                        Email = "operator@locahost.com",
                        NormalizedEmail = "operator@locahost.com",
                        Name = "Operator",
                        Lastname = "Operator",
                        UserName = "operator",
                        NormalizedUserName = "Operator",
                        PasswordHash = hasher.HashPassword(null, "Operator2050$"),
                        EmailConfirmed = true,
                    }
                );
        }
    }
}
