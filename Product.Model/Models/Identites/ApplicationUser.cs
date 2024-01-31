using Microsoft.AspNetCore.Identity;

namespace Product.Model.Models.Identites
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string RoleId { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
