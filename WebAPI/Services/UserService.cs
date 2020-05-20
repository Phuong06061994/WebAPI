using Microsoft.AspNetCore.Identity;
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
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return null;
            }
            var result = await signInManager.PasswordSignInAsync(user,model.Password,false,false);
            if (!result.Succeeded)
            {
                return null;
            }
            var role = userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, string.Join(";", role))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(config["Tokens : Issuer"], config["Tokens : Issuer"], claims, expires: DateTime.Now.AddHours(2), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
            

        }

        public async Task<bool> Create(CreateUserModel model)
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

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByUserName(string userName)
        {
            return context.Users.SingleOrDefault(s => s.UserName == userName);
        }
    }
}
