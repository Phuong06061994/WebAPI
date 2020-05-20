using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<string> Authenticate(AuthenticateModel model);
        Task<bool> Create(CreateUserModel modle);
    }
}
