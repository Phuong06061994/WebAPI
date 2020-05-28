using DAL.Request;
using DAL.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface INewsRepository
    {
        Task<IEnumerable<NewsResponse>> GetAll();
        Task<int> Create(NewsRequest request);
        Task<int> Update(NewsRequest request);
        Task<int> Delete(int id);
        Task<NewsResponse> GetById(int id);
    }
}
