using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Services
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAll();
        Task<int> Save(News model);
        Task<News> GetById(int id);
        Task<int> Delete(int id);
    }
}
