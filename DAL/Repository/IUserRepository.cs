using DAL.Request.User;
using DAL.Response;
using DAL.Response.User;
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
        Task<IEnumerable<UserResponse>> GetAll();
        Task<UserDetailResponse> GetUserById(Guid id);
    }
}
