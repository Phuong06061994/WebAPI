using DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<string>> GetAllByUserId(Guid id);
    }
}
