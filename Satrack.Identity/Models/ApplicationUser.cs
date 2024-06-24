

using Microsoft.AspNetCore.Identity;

namespace Satrack.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
    }
}

