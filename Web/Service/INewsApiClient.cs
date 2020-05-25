using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.News;

namespace Web.Service
{
    public interface INewsApiClient
    {
        Task<IEnumerable<NewsModel>> GetAll();

        Task<bool> Create(NewsCreateModel model);
        public Task<bool> Update(NewsModel model);
        Task<NewsModel> GetById(int id);
        Task<bool> Delete(int id);
    }
}
