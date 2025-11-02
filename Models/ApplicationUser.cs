using Microsoft.AspNetCore.Identity;

namespace ApiProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
