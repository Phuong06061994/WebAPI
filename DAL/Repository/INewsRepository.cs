using DAL.Request;
using DAL.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace DAL.Repository
{
    public interface INewsRepository
    {
        Task<IEnumerable<NewsResponse>> GetAll();
        Task<int> Create(NewsRequest request);
        Task<int> Update(NewsRequest request);
        Task<int> Delete(int id);
    }
}
