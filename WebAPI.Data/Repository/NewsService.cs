using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Data.DTO;

namespace WebAPI.Services
{
    public class NewsService : INewsService
    {
        public NewsService()
        {
        }

        public Task<IEnumerable<News>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
