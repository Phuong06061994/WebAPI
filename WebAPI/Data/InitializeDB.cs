using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Data
{
    public static class InitializeDB
    {
        private static UserManager<User> userManager;

        public async static void Initialize()
        {
            var user = new User()
            {
                UserName = "Admin",
                Password = "23042016Np@"
            };

            var result = await userManager.CreateAsync(user, user.Password);

        }
    }
}
