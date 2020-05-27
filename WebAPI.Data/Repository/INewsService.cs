using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.DTO
{
    public interface INewsService
    {
        //Task<IEnumerable<News>> GetAllAsync();
        //Task<int> Save(News model);
        //Task<int> Create(News model);
        //Task<News> GetById(int id);
        //Task<int> Delete(int id);
        Task<IEnumerable<News>> GetAll();
    }
}
