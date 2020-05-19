using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        User Authenticate(string username, string password);
    }
}
