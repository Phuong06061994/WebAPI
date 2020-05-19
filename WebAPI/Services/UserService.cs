using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Entities;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        APIContext context;

        public UserService(APIContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByUserName(string userName)
        {
           return context.Users.SingleOrDefault(s => s.)
        }
    }
}
