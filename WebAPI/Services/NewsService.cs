using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Entities;

namespace WebAPI.Services
{
    public class NewsService : INewsService
    {
        APIContext context;

        public NewsService(APIContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await context.News.ToListAsync<News>();
        }
    }
}
