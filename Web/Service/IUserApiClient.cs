using DAL.Response;
using DAL.Response.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.User;

namespace Web.Service
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(AuthenticateModel model);

        Task<bool> Create(UserModel registerRequest);

        Task<IEnumerable<UserResponse>> GetAll();
        Task<bool> RoleAssign(Guid id, RoleAssignRequest request);

        Task<bool> ChangePassword(UserChangePasswordModel model);
        //Task<UserModel> GetById(Guid id);

        Task<UserDetailResponse> GetById(Guid id);
    }
}
