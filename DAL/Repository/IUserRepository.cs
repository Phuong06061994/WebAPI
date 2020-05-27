using DAL.Request.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IUserRepository
    {
        Task<String> Authenticate(AuthenticateRequest request);
        Task<bool> Create(AuthenticateRequest model);
    }
}
