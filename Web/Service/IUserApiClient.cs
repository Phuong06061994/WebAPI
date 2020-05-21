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
    }
}
