using Microsoft.AspNetCore.Identity;
using Product.App.Contract.Dto;
using Product.App.Contract.IServices;
using Product.Infrastructure.SeedData;
using Product.Model.Models.Identites;

namespace Product.App.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ITokenService tokenService;

        public UserService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }
        public string Login(LoginDto dto)
        {
            string result = "";
            var user = userManager.FindByNameAsync(dto.UserName).GetAwaiter().GetResult();

            var isAccept = userManager.CheckPasswordAsync(user, dto.Password).GetAwaiter().GetResult();

            if (isAccept)
            {
                var roles = userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();
                result = tokenService.GenerateToken(user.Id, roles);
            }


            return result;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
            };

            user.ApplicationUserRoles = new List<ApplicationUserRole>()
                {
                    new ApplicationUserRole
                    {
                         UserId = user.Id,
                         RoleId = RoleSeedData.RoleId
                    }
             };

            var hashedPassword = userManager.PasswordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashedPassword;
            await userManager.CreateAsync(user);
            // userManager.AddToRoleAsync(user, RoleSeedData.UserRoleName).GetAwaiter().GetResult();
            return user.Id;
        }
    }
}
