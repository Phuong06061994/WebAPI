using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly APIContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IConfiguration config;

        public UserService(APIContext context, UserManager<User> userManager, SignInManager<User> signInManager, 
            RoleManager<Role> roleManager, IConfiguration config)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.config = config;
        }

        public async Task<string> Authenticate(AuthenticateModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return null;
            }
            var result = await signInManager.PasswordSignInAsync(model.UserName,model.Password,false,false);
            if (!result.Succeeded)
            {
                return null;
            }
            var role = userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.Role, string.Join(";", role))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(config["Tokens:Issuer"], config["Tokens:Issuer"], claims, expires: DateTime.Now.AddHours(2), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Create(UserModel model)
        {
            var user = new User()
            {
                UserName = model.Username,
                Password = model.Password,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<UserModel> GetById(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            var roles = await userManager.GetRolesAsync(user);

            var userModel = new UserModel()
            {
                Username = user.UserName,
                Roles = roles
            };
            return userModel;
        }

        public async Task<bool> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
          /*  if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }*/
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }

            return true;
        }

        public User GetByUserName(string userName)
        {
            return context.Users.SingleOrDefault(s => s.UserName == userName);
        }
    }
}
