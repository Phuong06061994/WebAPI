using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Service
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(AuthenticateModel model);

        Task<bool> Create(UserModel registerRequest);

        Task<IEnumerable<UserModel>> GetAll(string bearerToken);
        Task<bool> RoleAssign(Guid id, RoleAssignRequest request);

        Task<UserModel> GetById(Guid id);
    }
}
